/*****************************************************
* Tropos UI support functions (menus)
*
* Menu structure (simplified) is repeating sets of 
*
*<LI class="ui_treecollapse open"> <a>Menu Group</a>
*   <UL>
*       <LI class="endnode"> <a href="app1.asp">Menu Item 1</a> </LI>
*       <LI class="endnode selected"> <a href="app2.asp">Menu Item 2</a> </LI>
*   </UL>
*</LI>
*
******************************************************/
var overlay = false;
var singleMenuOpen = false;
var autoMenuExpand = false;
var defaultSpeed = 300;   //Animation speed in ms
var debug = 0;  //0=no debugging, 1=debugging
var prevMenuCode;
var autoDisplayQuickcode = true;

window.onbeforeprint = doOnBeforePrint;

function initialise() {
    var InitialiseAlreadyDone = false;
    var NewScreenSelected = true;

    tperflog("initialise started");

    if ($(".developerCanvass").length > 0) {
        $(".developerCanvass").removeAttr("style");
        $(".developerCanvass").addClass("developerCanvass");
        $("#ui_panelset").show();   // Make sure the application area is visible
    }
    else {
        $("#ui_panelset").hide();
    }

    if ($(".developerButtons").length > 0) {
        $(".developerButtons").removeAttr("style");
    }


    singleMenuOpen = $(".menuSingleOption input").attr('checked');
    autoMenuExpand = $(".menuExpandOption input").attr('checked');

    // Generic functions for all screen changes
    UI_handle_buttons();

    if (typeof jQuery.ui != 'undefined') {
        $(".ui_ModalWide, .ui_Modal, #ui_Modal").draggable({ handle: 'h2' });
    }
    if ($("#ui_tree").length == 0)  //no menu so show the application area and exit
    {
        $("#ui_panelset").show();
        return;
    }

    // Check if the menu has already been initialised
    if ($("#ui_tree.menuInitialised").length == 0) {

        InitialiseAlreadyDone = false;
    }
    else {
        InitialiseAlreadyDone = true;

    }

    // Hidden field stores the current active menu node
    // Get screen ID and check if this is a new screen
    var selectedNode = $(".selectedNode").val();
    if (selectedNode == "") {
        selectedNode = getURLparam("menuUID");
        if (selectedNode == "0") {
            selectedNode = "";
        }
    }


    if (prevMenuCode != selectedNode) {
        prevMenuCode = selectedNode;
        NewScreenSelected = true;
    }
    else
        NewScreenSelected = false;


    if ((selectedNode == "") && ($(".developerCanvass").length == 0))//No application active so hide the application window
    {
        $("#ui_panelset").hide();
    }
    else
        $("#ui_panelset").show();

    //tperflog("Initialised="+InitialiseAlreadyDone+"  NewScreen="+NewScreenSelected + " ["+prevMenuCode+"]");

    if (!InitialiseAlreadyDone)   // If first run then collapse menus
    {
        var MenuState = getCookie("TroposUIMenuState");
        if (MenuState == 'closed' || MenuState == 'open')
            $(".menuState").val(MenuState);

        $("#ui_tree .ui_treecollapse.open").removeClass("open");
        // Toggle open/close menu bar 
        $("#ui_nav_navtoggle").click(function (event)
        {
            showHideMenuBar();
        });
        $('#ui_nav_splitter').css('display', 'inline');
        $('#ui_nav_splitter').mousedown(function (event)
        {
            ui_nav_splitter_mousedown();
            return false;
        });

        // Add the events for the menu area 
        // Toggle open/close on each menu item 
        // Menu item clicked action to open new application
        $(".ui_treecollapse>a").die();
        $(".ui_treecollapse>a").live("click", function(event) {
            showHideMenu($(this))
        });

        $(".endNode>a").die();
        $(".endNode>a").live('click', function() { endNodeClicked($(this)) });


        // Option to only have one menu open at a time
        $(".menuSingleOption input").click(function() {
            changeSingleMenu();
        });

        reinstateMenu();  // Display/Hide menu based on previous settings
        restoreMenuState();
        // Add dummy class as a flag to say the menu has been initialised
        $("#ui_tree").addClass("menuInitialised");
    }

    // Read the menu autoclose option
    setSingleMenu($(".menuAutoClose").val());

    // Ensure the current selected menu option is visible
    if ((NewScreenSelected) || ($(".applicationMenu").html() == "Tropos"))
        forceMenuSelection();

    tperflog("initialise done");
}

function endNodeClicked(node) {
    var element = node.parent();
    var menuUID = element.attr('menuUID');
    var newpage = element.attr('newpage');
    var newURL = element.attr('ContentURL');
    if (newpage == 'False') 
    {
        if ($("#RBACIframe").length == 1 && newURL.indexOf('[RBA]') > -1)
        {
            document.title = 'Tropos';      // Set the title back to Tropos so that SetApplicationTitle will change it to the new correct value
            changeRbaPage(element);
            return;
        }
        $("#ui_panelset").show();
        $(".selectedNode").val(menuUID);
        menuItemSelected(menuUID);
    }
    else {
        endNodeSelected(element);
    }
}

function changeRbaPage(element)
{
    var currentURL = $('#RBACIframe').contents().find('#MainFrame').attr("src");
    var newURL = element.attr("ContentURL");
    newURL = newURL.replace("[RBA]", "/RBACompatibility/tropos/Apps");
    var query = currentURL.indexOf("?");
    newURL = newURL + currentURL.substring(query + 1);
    $('#RBACIframe').contents().find('#MainFrame').attr("src", newURL)
    var QC = element.attr('QuickCode');

    // We're relying on the function SetQC, which is only present in the Container.aspx page.  However, we should be safe because
    // only Container.aspx contains an element with the ID RBACIframe, which we tested earlier (endNodeClicked).
    SetQC(QC);

    $(".selectedNode").val(element.attr('menuUID'));
    forceMenuSelection();
}

function initialiseLite() {
    var s = getURLparam('Title').replace(/\+/g, " ");
    if (s != '') {
        $(".applicationMenu").text(s);
        $(".selectedNode").val(s);
        $("#dialog").attr({ title: "Help: " + s });
        $(".dialogMenuText").text(s);
        document.title = s;
    }
    hideMenuBar(0);
}

function initialiseNewPage()
{
    var s = getURLparam('Title').replace(/\+/g, " ");
    $(".applicationMenu").text(s);
    $(".selectedNode").val(s);
    $("#dialog").attr({ title: "Help: " + s });
    $(".dialogMenuText").text(s);
    document.title = s;
    try
    {
        SetTransactionTitle();
    }
    catch (ex)
    {
    }
    hideMenuBar(0);
}

function endNodeSelected(obj) {

}

/* Called by back end after a server chain */
function forceMenuSelectionOnChain(ChainedAction)
{
    showQuickCode(ChainedAction, "");
    $(".selectedNode").val(ChainedAction);
    forceMenuSelection();
}

/* Called by back end to notify us of current selected menu item */
function forceNewMenuSelection(menuUID) {
    var CurrentMenu = $(".selectedNode").val();

    //RBA's passing back the URL as the menu id which is wrong
    //Ignore the force call if the supplied menu id is longer than 5 characters and we already have a menu id
    if ((menuUID.length > 5) && (CurrentMenu != null) && (CurrentMenu > 0))
        $(".selectedNode").val(menuUID);
}

/* Ensure the current selected menu item is visible (if present) */
function forceMenuSelection() {
    if ($("#ui_tree").length == 0)  // no menu so ignore request
        return;

    var foundNode = false;
    var menuId = $(".selectedNode").val();  // Get the unique ID of the selected menu item

    if ((menuId == "") || (menuId == "0"))
        menuId = getURLparam("MenuUID"); // If empty try menu unique ID in the url

    if ((menuId == "") || (menuId == "0"))
        menuId = getURLparam("ContentCode"); // If empty try ContentCode in the url

    if ((menuId == "") || (menuId == "0"))
        menuId = getURLparam("QC");  // Then try QC in the URL

    if (menuId == "") //Nothing found so return to the default - may be "0" for not in menu
    {
        menuId = $(".selectedNode").val();
    }
    else {
        $(".selectedNode").val(menuId)
    }



    if ((menuId != "") && (menuId != "0")) {
        $("#ui_panelset").show();   // Make sure the application area is visible
        var onMenu = $("li[menuUID='" + menuId + "']");  //Try and find the menu using menu unique ID

        if (onMenu.length == 0)  // If that fails, try by old menu id
            onMenu = $("li[menuId='" + menuId + "']");

        if (onMenu.length == 0)  // If that fails, try by quickcode
            onMenu = $("li[quickcode='" + menuId + "']");

        if (onMenu.length > 0) //OK - Found a matching menu item, show in menu tree
        {
            foundNode = true;
            setThisMenuOpen(onMenu.first());
        }
    }

    // Quick code not found in the menu, check if the application title is available on the page

    if ((!foundNode) && (menuId != "")) {
        $("li.endNode.selected").removeClass("selected"); //Remove any selection items
        $("#ui_panelset").show();
        var menuText = $("#DefaultApplicationTitle").text();

        if (menuText.length < 3) {
            menuText = decodeURI(getURLparam('DAT'));
        }
        if (menuText.length < 3) {
            menuText = decodeURI(getURLparam('Title'));
        }

        if (menuText != "")
            setApplicationTitle(menuText);
    }
    else {
        var menuText = $("#DefaultApplicationTitle").text();
        var canvas = $(".developerCanvass");
        if (canvas.length > 0) {
            menuText = canvas.attr("PageTitle");
        }

        if (menuText != "")
            setApplicationTitle(menuText);
    }

    saveMenuState();
}
function setAppTitle() {
    var menuText = $("#DefaultApplicationTitle").text();
    var canvas = $(".developerCanvass");
    if (canvas.length > 0) {
        menuText = canvas.attr("PageTitle");
    }

    if (menuText != "")
        setApplicationTitle(menuText);
}


function setThisMenuOpen(menuobj) {
    //Expand the parent level menus
    var parentNode = menuobj;
    var looping = 1;
    foundNode = true;

    while (looping < 10)  // Max depth of 10 in case of recursive menu
    {
        parentNode = parentNode.parent(); //parent is <UL>
        parentNode = parentNode.parent(); //Parent LI element
        if ((parentNode != null) && (parentNode.is("li"))) {
            parentNode.addClass("open");
            looping++;
        }
        else {
            break;
        }
    }

    var menuText = menuobj.text();
    var menuParent = menuobj.parent().parent();
    var topicParent = $("a:first", menuParent).text();

    $(".applicationMenu").html(topicParent + ": <em>" + menuText + "</em>");
    if (document.title == 'Tropos')
        document.title = menuText;
    $("#dialog").attr({ title: "Help: " + menuText });
    $(".dialogMenuText").text(menuText);
    setActive(menuobj);

}

function cancelNavigation(title, menuID) {
    setApplicationTitle(title);
    //TODO: reset menu selection
}

// Set the application title in the grey box at the top of the page
function setApplicationTitle(title)
{
    title = title.replace(/\+/g, " ")
    $(".applicationMenu").html("<em>" + title + "</em>");
    if (document.title == 'Tropos')
        document.title = title;
}

function reinstateMenu() {
    if ($(".menuState").val() == "open") {
        showMenuBar(0);
    }
    else {
        hideMenuBar(0);
    }
}

// Set the current active menu item
function setActive(obj) {
    $("li.endNode.selected").removeClass("selected"); //Remove any selection items
    obj.addClass("selected");   //Set selected class on the menu item

    var TransactionList = obj.attr("transactionlist");
    var QuickCode=obj.attr("quickcode");
	if (QuickCode.length>0)
	{
		showQuickCode(QuickCode, TransactionList);
	}
	
    scrollIntoView(obj,$("#ui_nav_area"));
}

function showQuickCode(QuickCode, TransactionList) {
	if (!autoDisplayQuickcode)
		return;
		
	var QCbox=$("#ctl00_QC");
	if (QCbox.length > 0) {
	    var CurrentQC = QCbox[0].value;
	    if (TransactionList.toString().indexOf(CurrentQC, 0) >= 0)
	        return;
	    else
		    QCbox.val(QuickCode);
	}
}

function scrollIntoView(element, container)
{
    var containerTop = container.offset().top;
    var containerBottom = containerTop + container.height();
    var elemTop = element.offset().top;
    var elemBottom = elemTop + element.height();
    if (elemTop < containerTop)
    {
        var Difference = elemTop - containerTop;
        container.scrollTop(container.scrollTop() + Difference);
    } else if (elemBottom > containerBottom)
    {
        var Difference = elemBottom - container.height();
        container.scrollTop(container.scrollTop() + Difference );
    }
}

// Toggle open/closed state of the supplied menu item
function showHideMenu(menu) {
    menu = menu.parent();
    var ulClicked = $("ul:first", menu);
    if (ulClicked == null)
        return;

    var saveThisClass = menu[0].className;
    if (singleMenuOpen) {
        //Close all menues at this level and below
        $("li.ui_treecollapse", menu.parent()).removeClass("open");
    }

    if (autoMenuExpand) {
        // Open all child items below this level
        $("li.ui_treecollapse", menu).addClass("open");
    }
    else {
        //Close all child items below this level
        $("li.ui_treecollapse", menu).removeClass("open");
    }

    // Restore the original class
    menu[0].className = saveThisClass;
    
    //Toggle Open/Close
    menu.toggleClass("open");

    saveMenuState();
}


function showHideMenuBar() {
    /* if ($("#ui_nav").is(':hidden')) */
    if ($(".menuState").val() == 'closed') {
        showMenuBar(defaultSpeed);
        $(".menuState").val('open');
    }
    else {
        hideMenuBar(defaultSpeed);
        $(".menuState").val('closed');
    }
    setCookie("TroposUIMenuState", $(".menuState").val(), 365);
}

// Show the menu bar with animation speed in ms, 0 means no animation effect
function showMenuBar(speed) {
    //$("#ui_nav").show();
    if (!overlay)
    {
        $("#ui_workspace").animate({
            marginLeft: (ui_nav_splitter_left + 10 ) + 'px'
        }, speed);
    }
    $("#ui_nav").animate({
        marginLeft: '0',
        right: ui_nav_splitter_left
    }, speed);

    $("#ui_nav_navtoggle").animate({ left: (ui_nav_splitter_left - 16) + "px" }, speed);
    $("#ui_nav_navtoggle").removeClass("ui_nav_navtoggle_close");
    if ($("#ui_nav_splitter").length > 0)
    {
        $("#ui_nav_splitter").animate({ left: ui_nav_splitter_left + "px" }, speed);
    }
    positionInterface(ui_nav_splitter_left);

}

// Hide the menu bar with animation speed in ms, 0 means no animation effect
function hideMenuBar(speed) {
    $("#ui_nav").animate({
        marginLeft: '-400',
        right: "0"
    }, speed);
    if (!overlay) {
        $("#ui_workspace").animate({
            marginLeft: '0'
        }, speed);
    }
    /*$("#ui_nav").hide(speed);*/
    $("#ui_nav_navtoggle").animate({ left: "-5px" }, speed);
    if ($("#ui_nav_splitter").length > 0)
        $("#ui_nav_splitter").animate({ left: "-10px" }, speed);
    $("#ui_nav_navtoggle").addClass("ui_nav_navtoggle_close");
}

// Change the state of the single menu option (only one menu open at a time)
function changeSingleMenu() {
    singleMenuOpen = $(".menuSingleOption input").attr('checked');
    $(".menuAutoClose").val(singleMenuOpen);

    if (singleMenuOpen) {

        var counter = 0;
        $(".menuContent").each(function() {
            if ($(this).is(":visible")) {
                counter++;

                if (counter > 1)
                    $(this).slideToggle("normal");
            }
        });
        saveMenuState();
    }
}

// Set the single menu option checkbox based on the current state
function setSingleMenu(checked) {
    return;
    if (checked == 'true')
        $(".menuSingleOption input").attr('checked', true);
    else
        $(".menuSingleOption input").attr('checked', false);

}

// Save open/close state of top level menus
function saveMenuState() {
    var MenuCounter = 0;
    var menuStateArray = "";

    if ($(".menuStateControl").length == 0)
        return;

    $("#ui_tree LI.ui_treecollapse").each(function() {

        if ($(this).hasClass("open")) {
            menuStateArray += "1";
        }
        else {
            menuStateArray += "0";
        }
        MenuCounter++;
    });

    $(".menuStateControl").val(menuStateArray);
    //alert("Save = "+menuStateArray + "[" + MenuCounter +"]");

}

// Restore open/close state of top level menus
function restoreMenuState() {
    var MenuCounter = 0;

    if ($(".menuStateControl").length == 0)
        return;

    if (singleMenuOpen && this.location.href.indexOf("/url.aspx") == -1)
            // Do not restore the menu; unless we are on a url page when the menu will have been reloaded but will not reposition itself, so we need to restore it.
        return;

    menuStateArray = $(".menuStateControl").val();
    //alert("Restore = "+menuStateArray + "[" + MenuCounter +"]");

    $("#ui_tree .ui_treecollapse").removeClass("open");
    $("#ui_tree .ui_treecollapse").each(function() {
        if (menuStateArray.length > MenuCounter) {
            if (menuStateArray.charAt(MenuCounter) == 1) {
                $(this).addClass("open");
            }
        }
        MenuCounter++;
    });
    //alert("Restore Completed");
}

// Read a parameter from the url, returns empty string if not found
function getURLparam(param) {
    var url = window.location.search;
    var re = new RegExp('&' + param + '=([^&]*)', 'i');
    return (url = url.replace(/^\?/, '&').match(re)) ? url = url[1] : url = '';
}

// Invoke the client's print dialog
// TroposUIPrint.css print style sheet removes menu and header and sets print styles
// If the page has ONE scrolling region, it is hidden and an auto expanding "pre" used to show all content
function printScreen() {
    preparePrint();
    window.print();
    $(".troposScrollPrint").remove();

    return false;  // Returns false to cancel any associated click event
}

function doOnBeforePrint() {
    preparePrint();
}

function preparePrint() {
    var ScrollArea = $(".troposScrollGrid");
    var ScrollLines = $(".troposScrollLine");
    $(".troposScrollPrint").remove();
    if (ScrollArea.length == 1) {
        ScrollArea.after("<pre class='troposScrollPrint' style='white-space: pre-wrap;'/>");
        var ExpandingArea = $(".troposScrollPrint");

        SAoff = ScrollArea.position();
        ExpandingArea.css({ top: SAoff.top, left: SAoff.left });
        ExpandingArea.width(ScrollArea.width());
        var ScrollContent = "";
        for (i = 0; i < ScrollLines.length; i++) {
            ScrollContent += ScrollLines[i].innerText + "\r\n";
        }
        ExpandingArea.text(ScrollContent);
    }
}

function UI_handle_buttons() {
    $(".ui_action.disabled").removeClass("disabled");
    var xxx = $(".ui_action:disabled");
    //xxx=$(":disabled", xxx);
    xxx.addClass("disabled").attr("disabled", "");
}

function tperflog(message) {
    if (!debug) return;

    var debugArea = $("#debug");
    if (debugArea.length == 0) {
        $("#ui_tree").append("<div id='debug' style='height:200px'></div>");
        debugArea = $("#debug");
    }

    var d = new Date();
    try {
        var xx = debugArea.html() + d.valueOf() + "|" + message + "<br/>";
        debugArea.html(xx);
    }
    catch (e) { }
}

var ui_nav_splitter_drag = false;
var TroposUISplitter = parseInt(getCookie("TroposUISplitter"));
var ui_nav_splitter_left = 200;
if (isNaN(TroposUISplitter))
    ui_nav_splitter_left = 200;
else
    ui_nav_splitter_left = TroposUISplitter;

function ui_nav_splitter_mousedown()
{
    if ((event.button & 1) == 1)
    {
        ui_nav_splitter_drag = true;
        $('body').mouseup(function (event)
        {
            body_mouseup();
            return false;
        });
        $('body').mousemove(function (event)
        {
            body_mousemove();
            return false;
        });
    }
}
function body_mouseup()
{
    if ((event.button & 1) == 0)
    {
        ui_nav_splitter_drag = false;
        setCookie("TroposUISplitter", ui_nav_splitter_left, 365);
        $('body').off('mouseup');
        $('body').off('mousemove');
    }
}

function body_mousemove()
{
    if (ui_nav_splitter_drag)
    {
        var currentX = parseInt(document.all["ui_nav_splitter"].style.left);
        var x = event.clientX;
        if (x < 100)
            x = 100;
        else if (x > 400)
            x = 400;
        positionInterface(x);
        ui_nav_splitter_left = x;
        if ((event.button & 1) == 0)
        {
            // The left button has been released - so we are not dragging any more
            ui_nav_splitter_drag = false;
            setCookie("TroposUISplitter", ui_nav_splitter_left, 365);
            return false;
        }
    }
}

function positionInterface(x)
{
    if ($("#ui_nav_splitter").length > 0)
        document.all["ui_nav_splitter"].style.left = x + "px";
    document.all["ui_workspace"].style["margin-left"] = (x + 10) + "px";
    document.all["ui_nav_area"].style.width = x + "px";
    document.all["ui_navcontrol"].style.width = x + "px";
    document.all["ui_nav"].style.right = x + "px";
    document.all["ui_nav"].style.width = x + "px";
    document.all["ui_nav_navtoggle"].style.left = (x - 16) + "px";
}

function setCookie(cname, cvalue, exdays)
{
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + "; " + "path=/; " + expires;
}

function getCookie(cname)
{
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++)
    {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1);
        if (c.indexOf(name) != -1) return c.substring(name.length, c.length);
    }
    return "";
}
