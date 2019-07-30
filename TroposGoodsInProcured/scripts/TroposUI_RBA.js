/**********************************************************
* Epicor Branding Support
***********************************************************/
var UI_Load_Counter=0;  //Global to avoid repeat loading of resources
var flipflop=0;			//Add global variable to cover errors where flipflop is used in RBA javascript but not defined
var calendarImage="ui_form_iconCal_attached2.png";
myCss='1';
var isStandAlone=false;
var RBA_Debug=false;  //Flag to show debug panel with timing information for performance tuning

/*********************************************************************
* main function to apply Epicor branding for RBA's
* called from ssi_jfactions.js (ConnectRSCb)
**********************************************************************/
function UI_screen()
{
isStandAlone=false;

	UI_Load_Counter++;
	if (typeof jQuery != 'undefined')  //check jQuery Loaded
	{
		if ($(".SSI-BODY").length)    // Is there a standard SSI Body tag we can work with
		{
			try
			{
				if (window.frameElement!=null)
				{
						var FrameID=window.frameElement.id;
						FrameID=FrameID.substr(FrameID.length - 9);
						if (FrameID.toLowerCase() == "mainframe")
						{
								isStandAlone = true;
						}
				}
				else
				{
					isStandAlone = true;
				}

				if (isStandAlone)	// Standalone and standard pages get styling
				{
					UI_create_AppPanel();		// Wrap HTML rules around the main body area				
					UI_moveButtons();			// Move buttons into the new title area				
					moveToTop();				// Move content up if possible
				}
				else							// pages embedded in further frames in these pages don't get styled
				{
					$("html,body").css({overflow: "auto","background-image": "none","background-color":"white", border: "none"});
					$(".SSI-BODY").css({"background-image": "none","background-color":"transparent", overflow: "auto", border: "none"});
					$("html").css({backgroundColor: "#f2f6fb"});
					
				}
				rbaPerfLog("Body built");
				$("#ui_panelset").show();
				$(".SSI-BODY").css({visibility: "visible"});

				UI_fixBoxSizes();			// Adjust form elements for new styling
				UI_fixTables();				// Change table styling
				UI_addStripes();
				UI_fiximagepath();
				UI_removeFrameIndicator();
				checkPopupWindowSize();		// If it's a popup window, see if we can resize it
				
			} 
			catch(e){
				alert("UI: " + e.message);
			}
			
			$("#ui_position").css({overflowX:"visible",overflowY:"visible"}); //something is changing the overflow-x value to hidden
			$("#ui_panelset").show();
			$(".SSI-BODY").css({visibility: "visible"});
			
			try {
				parent.parent.initialise();
			}
			catch (e) {}
		}
	}
	else							// JQuery isn't loaded yet, attempt load and try again up to 3 times
	{
		if (UI_Load_Counter==1)		// If this is the first attempt, try and load all resources again
		{
			try
			{
				UI_load_resources();
			}
			catch (e) {
				//Failed - is whichcss.js loaded?
			}
		}
			
		if (UI_Load_Counter<4)		// Otherwise wait for everything to load and try again
			setTimeout("UI_screen()",100*UI_Load_Counter*UI_Load_Counter);  //try again in 100ms, 400ms, 900ms
	}

	rbaPerfLog("Done");
}

function UI_create_AppPanel()
{

	WrapBodyArea();

	$("<div id='ui_panel_box'></div>").insertBefore(".ui_panel");
	
var titleboxHTML="<div class='InfoWrap'><div class='Info'><h2><em><span class='applicationMenu'>Tropos</span></em></h2>";
	titleboxHTML+="<div class='ui_panelControls'>";
	if (!isLAL())
		titleboxHTML+="<span class='ui_panelControls_Help' onclick='javascript: _ad_img1_onclick()' title='Help'><a href='#'></a></span>";
	titleboxHTML+="<span class='ui_panelControls_Save' onclick='javascript: _ssi_saveDefaults()' title='Save field defaults'><a href='#'></a></span>";
	titleboxHTML+="<span class='ui_panelControls_Clear' onclick='javascript: _ad_clearfields_onclick()' title='Clear Fields'><a href='#'></a></span>";
	titleboxHTML+="<span class='ui_panelControls_Print' onclick='javascript: _ssi_print()' title='Print'><a href='#'></a></span>";
	titleboxHTML+="<span class='ui_panelControls_Refresh' onclick='javascript: _ssi_refresh()' title='Refresh'><a href='#'></a></span>";
	titleboxHTML+="<span class='ui_panelControls_Email' onclick='javascript: showHTML();' title='Email'><a href='#'></a></span>";
	titleboxHTML+="</div></div></div>";
	titleboxHTML+="<div class='ControlsWrap'><div class='Controls'><ul class='ui_actionSetOne'><div id='TroposButtons' /></ul><ul class='ui_actionSetTwo'><div id='TroposButtons2' /></ul></div></div>";
	
	$("#ui_panel_box").html(titleboxHTML);

	
	$(".bodytop").hide();
	$("#service").hide();
	$(".sfdcborder>.bodytop").show();
	UI_setPageTitle();
}

function isLAL()
{
var screenpath=document.URL;
screenpath=screenpath.toLowerCase();
if (screenpath.match("/lal/"))
	return true;

return false;
}




function WrapBodyArea()
{

	// Decouple all the grids from their data source
	// moving them while they are active causes duplicated data occasionally
	$(".ssi-lal-table, .ssi-grid-table").each( function()
	{
		var tab=$(this);
		var tabDB=tab.attr("dataSrc");
		if ((tabDB!=null) && (tabDB.length>0))
		{
			tab.attr("dataSrc",tabDB+"_xx");
		}
	});
	
	var ui_workspace=document.createElement("div");
	ui_workspace.id="ui_workspace";
	ui_workspace.className="ui_workspace ui_workspace_full";
	ui_workspace.style.marginLeft="0px";
	ui_workspace.style.backgroundPosition="-200px 0px";
	
	var ui_panelset=document.createElement("div");
	ui_panelset.id="ui_panelset";
	
	var ui_panelWrapper=document.createElement("div");
	ui_panelWrapper.id="ui_panelWrapper";
	ui_panelWrapper.className="ui_panelWrapper ui_boxSimple";
	
	var ui_panel=document.createElement("div");
	ui_panel.id="ui_panel";
	ui_panel.className="ui_panel ui_form";
	
	var ui_fieldsetset=document.createElement("div");
	ui_fieldsetset.id="ui_fieldsetset";
	ui_fieldsetset.className="ui_fieldsetset full";
	
	var ui_position=document.createElement("div");
	ui_position.id="ui_position";
	ui_position.style.position="absolute";
	ui_position.style.top="0px";
	ui_position.style.left="0px";
	ui_position.style.overflow="visible";
	
	ui_fieldsetset.appendChild(ui_position);
	ui_panel.appendChild(ui_fieldsetset);
	ui_panelWrapper.appendChild(ui_panel);
	ui_panelset.appendChild(ui_panelWrapper);
	ui_workspace.appendChild(ui_panelset);
	

	// Move the body's children into this wrapper
	while (document.body.firstChild)
	{
		ui_position.appendChild(document.body.firstChild);
	}

	// Append the wrapper to the body
	document.body.appendChild(ui_workspace);
	
	// Restore data sources
	$(".ssi-lal-table, .ssi-grid-table").each( function()
	{
		var tab=$(this);
		var tabDB=tab.attr("dataSrc");
		if ((tabDB!=null) && (tabDB.length>0))
		{
			tabDB=tabDB.replace("_xx","");
			tab.attr("dataSrc",tabDB);
		}
	});
}

function UI_removeFrameIndicator()
{
	var ContainerNode=document.getElementById("ui_position");
	
	if (ContainerNode==null) // If in a sub-frame, revert to check on body
	{
		ContainerNode=document.getElementsByTagName('BODY')[0];
	}
	
	if (ContainerNode!=null)
	{
		var lastNode=ContainerNode.lastChild;
		if ((lastNode!=null) && (lastNode.nodeType==3))
		{
			var CClen=lastNode.nodeValue.length;
			if (CClen==1)
				lastNode.nodeValue=" ";
		}
	}
}

function UI_setPageTitle()
{

	var thisTitle=$("#bodytoptitle").text();
	thisTitle=$.trim(thisTitle);
	
	if (thisTitle.length==0)  // Sometimes LAL's are structured differently
	{
		thisTitle=$(".header",".bodytop").text();
		thisTitle=$.trim(thisTitle);
	}
	
	if (thisTitle.length==0)  // Just for common/maintaintext.htm
	{
		thisTitle=$(".MaintainTextTitle").text();
		thisTitle=$.trim(thisTitle);
	}
	
	if (thisTitle.length==0)  // Maybe this is an HTML page with a title
	{
		thisTitle=document.title;
		thisTitle=$.trim(thisTitle);
	}
	
	
	$(".applicationMenu").text(thisTitle);
}

function UI_moveButtons()
{

	var buttonArea=$("#AdButtons");

	if (buttonArea.length>0)
	{
		var buttonDetails;
		var buttonLabel;
		var buttonID;
		var buttonClick;
		var buttonActive;
		var buttonType;
		var buttonParentID;
		var buttonParentIDstr;
		var newButtonHTML="";
		
		var buttonIDs=[];
		var buttonFns=[];
		var idcounter=0;
		
		$("span.barbuttondiv2",buttonArea).each ( function() {
			buttonDetails=$(this);
			idcounter++;
			$("font", buttonDetails).remove(); //remove any WingDings font definitions and characters
			
			buttonLabel=buttonDetails.text();
			
			buttonClick=buttonDetails.attr("onclick");
			buttonID=buttonDetails.attr("id");
			buttonActive=buttonDetails.parent().hasClass("show");
			
			buttonParentID=buttonDetails.parent().attr("id");
			buttonParentIDstr="";
			if (buttonParentID!="")
			{
				buttonParentIDstr=" id='"+buttonParentID+"'";
				buttonDetails.parent().attr( { id: buttonParentID + "xXx"});
			}
			buttonIDs.push(buttonID);
			buttonFns.push(buttonClick);
			buttonDetails.attr( { id: buttonID + "xXx"});
			
			if (buttonActive)
			{
				if ((buttonID.toLowerCase()!="backbutton") && (buttonID.toLowerCase()!="btndummy")) //hide the Back Button
					newButtonHTML+="<li"+buttonParentIDstr+"><a id='"+buttonID+"' class='ui_action'>"+buttonLabel+"</a></li>";
			}
			else
			{
				if ((buttonID.toLowerCase()!="backbutton") && (buttonID.toLowerCase()!="btndummy")) //hide the Back Button
					newButtonHTML+="<li"+buttonParentIDstr+"><a id='"+buttonID+"' class='ui_action disabled'>"+buttonLabel+"</a></li>";
			}
		});
		
		if (newButtonHTML.length>0)
		{
			$("#TroposButtons").after(newButtonHTML);
			for (var i=0; i<buttonIDs.length; i++)
			{
				$("#"+buttonIDs[i]).click(buttonFns[i]);
			}
			
		}	
		buttonArea.hide();
	}

	var fullpath=window.location.href;
	fullpath=fullpath.toLowerCase();

	
	// Check for non-standard help functions
	var helpButton=$("#btnHelp");
	var helpFunction="";
	if ((helpButton!=null) && (helpButton.length>0))
	{
		helpFunction=helpButton.first().attr("onclick");
		if (helpFunction!=null)
		{
			$(".ui_panelControls_Help").removeAttr("onclick");
			$(".ui_panelControls_Help").click(helpFunction);
		}
	}
	if ((helpFunction==null) || (helpFunction==""))
	{
		var helpImage=$("img[src*='help_s.gif']");
		if (helpImage.length>0)
		{
			helpFunction=helpImage.first().attr("onclick");
			if (helpFunction!=null)
			{
				$(".ui_panelControls_Help").removeAttr("onclick");
				$(".ui_panelControls_Help").click(helpFunction);
			}
		}
	}
	
	if ((helpFunction==null) || (helpFunction==""))
	{
		helpFunction=$(".SSI-BODY").attr("onhelp");
		if (helpFunction!=null)
		{
			$(".ui_panelControls_Help").removeAttr("onclick");
			$(".ui_panelControls_Help").click(helpFunction);
		}
	}
	
	// Check for non-standard print functions
	var printButton=$("#print");
	var printFunction="";
	if ((printButton!=null) && (printButton.length>0))
	{
		printFunction=printButton.first().attr("onclick");
		if (printFunction!=null)
		{
			$(".ui_panelControls_Print").removeAttr("onclick");
			$(".ui_panelControls_Print").click(printFunction);
		}
	}

	$("input.gobutton").css({padding:"", height: ""});
	$("#divRows").css({ textAlign: "left", backgroundColor: "transparent", color: "#509298", border: "none", marginTop: "1px", paddingTop: "3px"});
	fixHardCodedFonts();
}

/*
Some RBA's have hardcoded fonts such as verdana on forms encoded as font tags.  Remove these (change to Segoe UI)
But don't do this for any special fonts such as windings used for buttons
*/
function fixHardCodedFonts()
{
	$("font").each( function() {
		var thisFontTag=$(this);
		var thisFontTagFontFace=thisFontTag.attr("face").toLowerCase();
		if  ((thisFontTagFontFace.substring(0,5)!="wingd") && (thisFontTagFontFace.substring(0,4)!="webd")) //not wingdings or webdings font
		{
			thisFontTag.attr("face","Segoe UI");
		}
	});
}


function UI_fixBoxSizes()
{
rbaPerfLog("fixBoxSizes");
	$("img[src*='navborder.gif']").hide();

	$("input.SSI-INPUT").each( function ()
	{
		var thisField=$(this);
		var hasOnPropertyChange=thisField.attr("onpropertychange");
		if (hasOnPropertyChange==null)
			thisField.css({height: "18px", margin: "0 0 0 0", padding: "0 0 0 0"});
		
	});
	
	$("select.SSI-INPUT").css({height: "20px"});
	
	$("input.SSI-PUTVAR").each( function()
	{
		var thisArea=$(this);
		var thisHeight=thisArea.css("height");
		if ((thisHeight!=null) && (thisHeight>20))
		{
			thisArea.css({margin: "0 0 0 0", padding: "0 0 0 0"});
		}
		else
		{
			thisArea.css({height: "18px", margin: "0 0 0 0", padding: "0 0 0 0"});
		}
	});
	$("input.SSI-INPUT[type='checkbox']").css({border: "none", background: "none"});
	
	// Find any lines with a bottom border of 2px solid and remove bottom padding
	// These are bold horizontal lines under headings that are otherwise too low
	var styleAttr, thisField;
	$("div.SSI-TEXT").each( function () {
		thisField=$(this);
		styleAttr=thisField.attr("style");
		if (styleAttr!=null)
		{
			styleAttr=styleAttr.toLowerCase();
			if ((styleAttr.indexOf("border-bottom")>-1) && (styleAttr.indexOf("solid")>-1) && (styleAttr.indexOf("2px")>-1))
			{
				thisField.attr( "style", styleAttr+";padding-bottom: 0px !important; height: 16px");
			}
			if ((styleAttr.indexOf("border-bottom:")>-1) && (styleAttr.indexOf("solid")>-1) && (styleAttr.indexOf("1px")>-1))
			{
				thisField.attr( "style", styleAttr+";border: none");
			}
		}
	});
	
	$("input[type=button]").each( function ()
	{
		var font=$(this).css("font-family");
		if  ((font.substring(0,5)=="wingd") || (font.substring(0,4)=="webd")) // wingdings or webdings font
			$(this).addClass("autosize");
	});
	rbaPerfLog("fixBoxSizes - done");
}

function moveToTop()
{
//Scan for first row of content
var elements=$("#ui_position .SSI-TEXT,#ui_position .SSI-PUTVAR, #ui_position .gobutton, #divHolder, #DataGrid1");  //assume all elements are absolutely positioned on the page so will have a style attribute

	var top=1000;
	var t, v;
	var topdesc;
	var counter=0;
	
	elements.each( function () {
		v=$(this).css("visibility");
		t=$(this).offset().top;
		if (v!='hidden')
			counter++;
			
		if ((t<top) && (t>0) && (v!='hidden'))   //Causing a problem with pages where initial fields were initially hidden
		{
			top=t;
			topdesc= $(this).attr("id") + ":" + $(this).html();
		}
	});
	
	if (counter<2)
	{
		//Not enough visible fields found - let's play safe
		return;
	}
	
	
	if (top>7)
		top=top-5
	else
		top=0;
	if (top > 800)  // Expecting an offset of around 200 pixels, this looks too big so don't relocate
		top=0;
	
	var origTop=$("#ui_position").offset().top;
	var newtop=origTop-top;
	
	// DIV container with id ui_position inside the forms area so we can move all the form elements up by giving it a negative top margin
	$("#ui_position").css({"top": newtop, bottom: top });
	
}

function UI_fixTables()
{
rbaPerfLog("UI_fixTables");
	$("table.ssi-grid-table, table.ssi-lal-table").attr({cellspacing: "0", cellpadding: "0"});
	$(".ssi-grid-banner, .ssi-lal-banner").css({"font-weight": ""});
	
	//$("td[style*='width: 0px']").css({"display":"none"});  //Problem with psuedo-hidden columns, workaround not working
	
	//Adjust size of the table and parent container (div)
	tables=$("table.ssi-grid-table, table.ssi-lal-table");
	tables.each( function () {
		var thisTable=$(this);
		var thisParent=thisTable.parent();
		var hasHeader=false;
		var hasBody=false;
		if ($("thead tr",thisTable).length>0) hasHeader=true;
		if ($("tbody tr",thisTable).length>0) hasBody=true;
		

		//Adjust Parent DIV
		if (thisParent.is("div"))
		{
			var thisParentHeightisAuto=false;
			var thisParentWidthisAuto=false;
			
			var thisparentHeight=thisParent.css("height");
			if ((thisparentHeight=="auto") || (thisparentHeight==""))
				thisParentHeightisAuto=true;
			
			var thisParentWidth=thisParent.css("width");
			if ((thisParentWidth=="auto") || (thisParentWidth==""))
				thisParentWidthisAuto=true;
		
			if ( (hasBody) && (thisParentHeightisAuto)) // If header and data in one table in a div with no fixed height
			{
				thisParent.css({overflow: "visible"});
			}
			if (  (hasHeader) && (!hasBody) && (thisParentWidthisAuto) ) // Separate table header
			{
				thisParent.css({overflowY: "visible", overflowX: "visible"});
				var tableLayout=thisTable.css("tableLayout");
				if (tableLayout=="fixed") //If table layout is fixed, remove any hard coded width on the table so it takes the column widths
				{
					thisTable.css({width: "auto"});
				}
			}
			
			if (  (!hasHeader) && (hasBody) ) // Separate table
			{
				var tableLayout=thisTable.css("tableLayout");
				if (tableLayout=="fixed") //If table layout is fixed, remove any hard coded width on the table so it takes the column widths
				{
					thisTable.css({width: "auto"});
				}
			}
			
			
			if (  (hasHeader) && (!hasBody) && (!thisParentWidthisAuto) ) // Separate table header
			{
				thisParent.css({overflowY: "visible", overflowX: "hidden"});
				
			}
			
			if ( (hasHeader) && (!hasBody) && (!isStandAlone)) //sub frames only
			{
				thisParent.css({overflowY: "visible", overflowX: "visible"});
			}
		}
	});
	
	joinTableAreas();
	UI_TableRemoveHiddenColumns();
	tables.bind("readystatechange", function(){ UI_stripy(this);});  //Track table updates and stripe alternate rows
	UI_addStripes();
	rbaPerfLog("UI_fixTables - done");
}

function joinTableAreas()
{
rbaPerfLog("joinTableAreas");
var tableArea=[];
var visible, top, height, left, offset;
var table1, table2, tabletmp, verticalGap, horizontalGap;

	var new_ID_Counter=0;
	/* First get all the tables on the page (in a div with class "selections") */
	$(".selections").each( function ()
	{
		new_ID_Counter++;
		offset = $(this).position();
		visible=$(this).is(":visible");
		height=$(this).height();
		width=$(this).width();
		left=offset.left;
		top=offset.top;
		id=$(this).attr("id");
		if (id=="")  //Selection doesn't have an ID - create an ID so we can move it later
		{
			var new_id="Selections_Unique_Tmp_"+new_ID_Counter;
			$(this).attr("id",new_id);
			id=new_id;
		}
		tableArea.push(new newTableArea(id, visible, top, height, left,parent));
	});
	
	/* Scan in sequence, header and body tables are (normally) together */
	for (var i=0; i<tableArea.length-1; i++)
	{
		table1=tableArea[i];
		table2=tableArea[i+1];
		
		if (table1.top>table2.top) //make sure table1 is above table2
		{
			tabletmp=table1;
			table1=table2;
			table2=tabletmp;
		}
		
		verticalGap=table2.top - table1.top - table1.height;
		horizontalGap=Math.abs(table1.left - table2.left);

		if ((verticalGap>1) && (verticalGap<17) && (horizontalGap<3)) //Tables are within 16 pixels of each other (1 line) and with the same left position
		{
			var movingTable=$("#"+table1.id);
			if (movingTable.length==1)
			{
				movingTable.height(movingTable.height()+verticalGap);
				$("#"+table1.id+">table").height(movingTable.height()+verticalGap);
				
				i++;  //Just processed two tables so skip to next pair
			}
		}
	}
	rbaPerfLog("joinTableAreas - Done");
}

function newTableArea(id, visible,top, height, left)
{
	this.id=id;
	this.visible=visible;
	this.top=top;
	this.height=height;
	this.left=left;
}

function UI_stripy(obj)
{
rbaPerfLog("UI_stripy");
	if (obj.readyState=="complete")
	{
		UI_addStripes();
		UI_TableRemoveHiddenColumns();
		UI_fiximagepath();
		UI_FixTableWidth();
	}
rbaPerfLog("UI_stripy - done");	
}

function UI_fiximagepath()
{
	$(".SSI-IMAGE").each( function () {
		var curSrc=$(this).attr("src");
		var newSrc=curSrc.replace("\/1\/1\/","\/1\/");
		
		if (newSrc!=curSrc)
		{
			$(this).attr("src",newSrc);
		}
	});
	var CalendarImagePath=UI_getPath()+"style/ui_theme/silver/ui_images/"+calendarImage;
	$("img[src*='calendar.gif']")
				.attr({
						"src" : CalendarImagePath,
						"relsrc": CalendarImagePath,
						"width" : "15",
						"height" : "20"
					})
				.css({width:"15",height:"20"});

}

function UI_TableRemoveHiddenColumns()
{
return;
rbaPerfLog("UI_TableRemoveHiddenColumns");
	// If columns are hidden by setting a width of 0 or 1, make them as small as possible and hide any content - typically a DIV
	// otherwise the "hidden" content can be visible or change the grid sizing
	
	var counter=0;
	var containedDiv;
	var needsHide=false;
	var scanCounter=0;
	$("table").each( function ()
	{
		var curTable=$(this);
		needsHide=false;
		scanCounter=0;
		if (curTable.css("visibility")!="hidden")
		{
			$(".hide",curTable).removeClass("hide");
			$("td,th",curTable).each( function ()
			{
				var cell=$(this);
				scanCounter++;
				if (cell.width()<=2)
				{
					cell.addClass("hide");
					$("div",cell).addClass("hide");
					needsHide=true;
				}
				if ((!needsHide) && (scanCounter>50))
				{
					return false;
				}
			});
			
		}
	});
	
	rbaPerfLog("UI_TableRemoveHiddenColumns - Done");
}

/*
Check for tables in a fixed width selection div that now don't require as much width
and shrink if required (assumes scroll bar is always present)
*/
function UI_FixTableWidth()
{
	var SelectionWidth;
	var TableWidth;
	var ScrollbarWidth=17;

	$(".selections").each( function ()
	{
		var thisSelection=$(this);
		if (thisSelection.css("width")!="")
		{
		
			SelectionWidth=thisSelection.width();
			// Get the width of the contained table(s)
			TableWidth=0;
			$("table",thisSelection).each( function ()
			{
				var twidth=$(this).width();
				if (twidth>TableWidth)
					TableWidth=twidth;
			});
			
			if ((TableWidth>0) && (TableWidth+ScrollbarWidth<SelectionWidth))
			{
				thisSelection.width(TableWidth+ScrollbarWidth);
			}
		}
	});
}


function UI_addStripes()
{
	UI_setPageTitle();
}

function checkPopupWindowSize()
{
	var winWidth=$(window).width();
	var winHeight=$(window).height();
	
	if ((this.opener==null) || (this.opener.name!="main"))  //not a popup? so return but do a min size first
	{
		if ($(window).attr("name")=="navframe")  //It's a sub frame on a form so don't do any resizing
			return;
			
		var changedSize=false;
		
		if (winWidth<850)
		{
			winWidth=850;
			changedSize=true;
		}
		if (winHeight<300)
		{
			winHeight=300;
			changedSize=true;
		}
		if (changedSize)
		{
			window.resizeTo(winWidth,winHeight);
		}
		
		return;
	}
	
	var formWidth=$(".ui_fieldsetset").width();
	var formMaxWidth=$(".ui_fieldsetset")[0].scrollWidth;
	var newWidth=60;
	if (formMaxWidth>formWidth+5)
	{
		//newWidth=formMaxWidth-formWidth;
	}
	
	var formHeight=$(".ui_fieldsetset").height();
	var formMaxHeight=$(".ui_fieldsetset")[0].scrollHeight;
	var newHeight=100;
	if (formMaxHeight>formHeight+5)
	{
		//newHeight=formMaxHeight-formHeight;
	}
	//Make sure the bigger window stays on the screen
	var screenHeight=screen.height;
	
	if (screenHeight-winHeight<30)  //Don't make screen bigger if it already fills the screen
		return;
	
	var windowTop=window.screenTop;
	var overflowHeight=screenHeight-(windowTop+winHeight+newHeight);
	if  ((overflowHeight<0) && (window.screenTop+overflowHeight>15)) //Move screen up as long as it stays on the page
	{
		window.moveBy(0,overflowHeight);
	}
	
	if ((newWidth>0) || (newHeight>0))
	{
		//window.resizeTo(winWidth+newWidth,winHeight+newHeight);	
		window.resizeTo(winWidth+120,winHeight+100);	
	}
}

function rbaPerfLog(message)
{
if (!RBA_Debug) return;

var debugArea=$("#RBAdebug");
if (debugArea.length==0)
{
    $(".ui_workspace").append("<div id='RBAdebug' style='position:absolute;left:400px;top:200px;height:250px;width:350px;background-color:#ffffff;border: 1px solid black;z-index: 999999;overflow: auto'></div>");
    debugArea=$("#RBAdebug");
}

var d=new Date();
try {
    var xx=debugArea.html()+ d.valueOf() + "|" + message + "<br/>";
    debugArea.html(xx);
    }
    catch(e) {}
}