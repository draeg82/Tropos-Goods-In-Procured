using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using TroposUI.Common.Context;
using TroposUI.Transactions;
using System.Data;
using System.Globalization;
using TroposUI.Common.DAL;

namespace TDK.WebServices
{
    [ServiceContract(Namespace = "WebServices")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class TDKServices
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        public List<KeyValuePair<string, string>> GetDavl(string dataname, string davlType, string sqlStatement, string[] sqlParameters)
        {
            UserContext Context = (UserContext)HttpContext.Current.Session["UserContext"];

            TransactionExecution Execution = new TransactionExecution(Context);
            List<KeyValuePair<string, string>> ReturnValue = new List<KeyValuePair<string, string>>();
            DataTable FieldChoiceValues = null;
            string CacheKey = "";
            if (davlType == "SQL")
            {
                CacheKey = davlType + "_" +
                    sqlStatement.GetHashCode().ToString() + "_" +
                    sqlStatement.ToUpperInvariant().GetHashCode().ToString() + "_" +
                    Context.TroposServer + "_" + Context.TroposDatabase + "_" +
                    Context.TroposSession.Language;
                if (sqlParameters != null)
                {
                    int i = 0;
                    foreach (string sqlParameter in sqlParameters)
                        i = i ^ sqlParameter.GetHashCode();
                    CacheKey = CacheKey + "_" + i.ToString();
                }
            }
            else
                CacheKey = davlType + "_" + dataname + "_" + Context.TroposServer + "_" + Context.TroposDatabase + "_" + Context.TroposSession.Language;
            if (HttpContext.Current.Cache[CacheKey] == null)
            {
                switch (davlType)
                {
                    case "UOM":
                        FieldChoiceValues = Execution.GetUOMChoices();
                        break;
                    case "YorN":
                    case "YorM":
                        FieldChoiceValues = Execution.GetFieldChoiceValues(davlType.ToUpper(CultureInfo.InvariantCulture));
                        break;
                    case "SQL":
                        FieldChoiceValues = GetSqlFieldChoices(Context, sqlStatement, sqlParameters);
                        break;
                    case "Yes":
                        FieldChoiceValues = Execution.GetFieldChoiceValues(dataname);
                        break;
                    default:
                        FieldChoiceValues = new DataTable();
                        break;
                }
                HttpContext.Current.Cache.Insert(CacheKey, FieldChoiceValues, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(60));
            }
            else
            {
                FieldChoiceValues = (DataTable)HttpContext.Current.Cache[CacheKey];
            }
            if (FieldChoiceValues.Rows.Count == 0)
            {
                ReturnValue.Add(new KeyValuePair<string, string>("?", "No field choices available"));
                ;
            }
            else
            {
                foreach (DataRow Row in FieldChoiceValues.Rows)
                {
                    ReturnValue.Add(new KeyValuePair<string, string>((string)Row["Code"], (string)Row["Description"]));
                }
            }
            return ReturnValue;
        }

        private DataTable GetSqlFieldChoices(UserContext context, string sqlStatement, string[] sqlParameters)
        {
            DataTable dt = new DataTable();
            dt.Locale = CultureInfo.InvariantCulture;
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Description", typeof(string));

            if (!string.IsNullOrEmpty(sqlStatement))
            {
                var ds = TroposCS.ReadView(context, sqlStatement, sqlParameters);
                if (ds != null && ds.Tables.Count > 0)
                {
                    var t = ds.Tables[0];
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Code"] = t.Rows[i][0];
                        dr["Description"] = t.Rows[i][1];
                        dt.Rows.Add(dr);
                    }
                }
            }

            return dt;
        }
        // Add more operations here and mark them with [OperationContract]
    }
}
