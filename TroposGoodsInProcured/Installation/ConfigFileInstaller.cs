using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Configuration.Install;
using System.Xml;
using System.Globalization;
using System.Windows.Forms;

namespace TroposGoodsInProcured.Installation
{
    [RunInstaller(true)]
    public class ConfigFileInstaller : Installer
    {
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
            string TcsServer = this.Context.Parameters["TcsServer"];
            string TcsInstance = this.Context.Parameters["TcsInstance"];
            string TuiServer = this.Context.Parameters["TuiServer"];
            string TuiInstance = this.Context.Parameters["TuiInstance"];
            string ProductVersion = this.Context.Parameters["ProductVersion"];
            string WindowsAuth = this.Context.Parameters["WindowsAuth"];

            string TdkVersion = "";
            if (string.IsNullOrEmpty(TcsServer))
                throw new InstallException("Tropos Communications Server not specified");

            if (string.IsNullOrEmpty(TcsInstance))
                TcsInstance = "TroposCommunicationServices";

            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string ConfigLocation = Path.GetDirectoryName(Path.GetDirectoryName(asm.Location));
            // Get rid of the filename, then the "bin" directory, to leave the installation directory
            string VersionFid = Path.Combine(ConfigLocation, "TDKVersion.txt");
            FileInfo VersionInfo = new FileInfo(VersionFid);
            if (VersionInfo.Exists)
            {
                using (StreamReader VersionFile = new StreamReader(VersionFid))
                {
                    TdkVersion = VersionFile.ReadToEnd();
                }
            }
            string ConfigFid = Path.Combine(ConfigLocation, "web.config");
            FileInfo ConfigInfo = new FileInfo(ConfigFid);
            if (!ConfigInfo.Exists)
                throw new InstallException(string.Format(CultureInfo.InvariantCulture, "Cannot find config file {0}", ConfigFid));

            XmlDocument ConfigFile = new XmlDocument();
            ConfigFile.Load(ConfigFid);
            Context.LogMessage("Editing XML Document");
            try
            {
                foreach (XmlNode Node in ConfigFile["configuration"]["system.serviceModel"]["client"])
                {
                    if (Node.Name == "endpoint")
                    {
                        if (Node.Attributes.GetNamedItem("contract").Value.StartsWith("DataReader", StringComparison.Ordinal))
                            Node.Attributes.GetNamedItem("address").Value =
                                "http://" + TcsServer + "/" + TcsInstance + "/DataReader.svc";
                        if (Node.Attributes.GetNamedItem("contract").Value.StartsWith("DataUpdater", StringComparison.Ordinal))
                            Node.Attributes.GetNamedItem("address").Value =
                                "http://" + TcsServer + "/" + TcsInstance + "/DataUpdater.svc";
                    }
                }
                XmlNode CompilationNode = ConfigFile["configuration"]["system.web"]["compilation"];
                CompilationNode.Attributes.GetNamedItem("debug").Value = "false";

                XmlNode AuthenticationNode = ConfigFile["configuration"]["system.web"]["authentication"];
                foreach (XmlNode Node in AuthenticationNode)
                {
                    if (Node.Name == "forms")
                    {
                        Node.Attributes.GetNamedItem("name").Value =
                            "." + TuiInstance.ToUpper(CultureInfo.InvariantCulture) + "AUTH";
                        Node.Attributes.GetNamedItem("loginUrl").Value =
                            "http://" + TuiServer + "/" + TuiInstance + "/Login.aspx";
                    }
                }
                if (WindowsAuth == "1")
                {
                    AuthenticationNode.Attributes["mode"].Value = "Windows";
                    try
                    {
                        foreach (XmlNode Node in ConfigFile["configuration"]["system.serviceModel"]["services"])
                            if (Node.Name == "service" && Node.Attributes.GetNamedItem("name").Value == "TDK.WebServices.TDKServices")
                                foreach (XmlNode subNode in Node.ChildNodes)
                                    if (subNode.Name == "endpoint")
                                    {
                                        XmlAttribute x = ConfigFile.CreateAttribute("bindingConfiguration");
                                        x.Value = "BindingForWindowsAuth";
                                        subNode.Attributes.Append(x);
                                    }
                    }
                    catch (NullReferenceException)
                    {
                        ;
                        // This will happen if there are no TDK services defined in the TWA
                        // In that case nothing needs changing so ignore the error
                    }
                }
                try
                {
                    foreach (XmlNode Node in ConfigFile["configuration"]["applicationSettings"]["TroposGoodsInProcured.Properties.Settings"])
                    {
                        if (Node.Name == "setting")
                        {
                            if (Node.Attributes.GetNamedItem("name").Value == "ProductVersion")
                                Node.ChildNodes[0].InnerText = ProductVersion;
                        }
                    }
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Failed to locate the ProductVersion attribute - application version not updated");
                }
                foreach (XmlNode Node in ConfigFile["configuration"]["applicationSettings"]["TroposDeveloperToolkit.Properties.Settings"])
                {
                    if (Node.Name == "setting")
                    {
                        if (Node.Attributes.GetNamedItem("name").Value == "ToolkitVersion")
                        {
                            Node.ChildNodes[0].InnerText = TdkVersion;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Context.LogMessage(ex.ToString());
                throw;
            }
            try
            {
                ConfigFile.Save(ConfigFid);
                Context.LogMessage("Successful edit of config file");
            }
            catch (XmlException ex)
            {
                Context.LogMessage("Failed to update config file: " + ex.ToString());
                throw;
            }
            catch (IOException ex)
            {
                Context.LogMessage("Failed to update config file: " + ex.ToString());
                throw;
            }

        }
    }
}
