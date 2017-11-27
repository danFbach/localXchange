$(document).ready(function () {

/*BEGIN GLOBAL VARS*/
    var filePrefix = "http://localhost:38119/";
    var loc = $(location).attr('href');
    var x = $("label");
    x.each(function(){
        if($(this).hasClass('required')){
            $(this).html($(this).text() + "*");
        }
    });
    function goBack() {
        window.history.back();
    };
    $("span, a").click(function(){
        console.log(document.activeElement);
    });
/*END*/
/*BEGIN IMG PULSE*/
    $("dt#displayImageContainer").click(function() {
        $(this).toggleClass('fullScreen');
        if($(this).hasClass('fullScreen')) { $(this).attr("title", "Click to Close."); }
        else { $(this).attr("title", "Click to View Full-size."); }
    });
/*END*/
/*BEGIN message addressList dropDown*/
    $("#selectUser").click(function() {
        if($(".addressDropList").hasClass('active')){
            addressCollapse();
        }
        else{
            var newListHeight = $(".addressDropList li").length;
            newListHeight = newListHeight * 24;
            $(".addressDropList").addClass('active');
            $(".addressDropList.active").css("height", newListHeight);
            $("#downArrow").text("X");
            $("#downArrow").css({
                transform: 'rotate(0deg)',
                position: 'relative',
                right: '6px',
                top: '5px'
            });
        }
        $(this).focusout(function() {
            console.log("focusout");
            addressCollapse();
        });
        function addressCollapse(){
            $(".addressDropList").removeClass('active');
            $(".addressDropList").css("height", 25);
            $("#downArrow").text(">");
            $("#downArrow").css({
                transform: 'rotate(90deg)',
                position: 'relative',
                right: '4px',
                top: '1px'
            });            
        }
    });
/*END*/
/*BEGIN product details image selector script*/
    if(loc.toLowerCase().includes("product/details")){
        $("img.imageThumbs").click(function(){
            if($(this).attr("id") === "addPhotoIcon")
            {
                //do nothing
            }
            else
            {
                var imgLoc = $(this).attr("src");
                $("img#displayImage").attr("src", imgLoc);
                if(imgLoc === $("img#displayImage").attr("src")){
                    if($("img.imageThumbs").hasClass('currentImg')){
                        $("img.imageThumbs").removeClass('currentImg');                    
                    }
                    $(this).addClass('currentImg');
                }
            }
        });
    }
/*END*/
/*BEGIN picture upload script, adds extra upload slots*/
    if(loc.toLowerCase().includes("addpictures")){
        var indexNum = Number($(".photoGroup .formItem input").last().attr("id").split('_')[1]);
        $(".addPhotoButton").click(function () {                
            var newInput = document.getElementById("files_" + indexNum);
            $(newInput).clone().attr("type", "").attr("type", "file").attr("name", "files[" + (indexNum + 1) + "]").attr("id","files_" +  (indexNum + 1) ).insertAfter(newInput).last();
            indexNum += 1;
        });
    }
/*END*/
/*BEGIN NAVBAR DROP MENUS*/
    $("ul.nav.navbar-nav li a").click(function () {
        $(this).next().removeClass("preload").toggleClass("active");
        $(this).toggleClass("active");
        removeLinkClass(this);
    });
    $("ul.nav.navbar-nav li a").focusout(function() {
        if($(this).attr("id") !== "openLoginForm"){
            setTimeout(function(){
                console.log("focusout click");
                removeLinkClass();                    
            }, 150);            
        }
    });
    function removeLinkClass(curObjID){
        $("ul.nav.navbar-nav li a").each(function(){
            if(!$(this).is(":focus")){
                if ($(this).next().attr("id") !== $(curObjID).next().attr("id")) {
                    $(this).next().removeClass('active');
                }
            }
        });
    }
/*END*/
/*BEGIN CURRENT LOCATION*/
    
    if(loc.toLowerCase().includes("account/register") || loc.toLowerCase().includes("product/create")){
        geolocator.config({
            language: "en",
            google: {
                version: "3",
                key: "AIzaSyAi-ncEn7LiyqYrbFm0Cwy9Sus9Xhmy2p4"
            }
        });
        var options = {
            enableHighAccuracy: true,
            timeout: 5000,
            maximumWait: 5000,
            maximumAge: 0,          // disable cache
            desiredAccuracy: 20,    // meters
            fallbackToIP: true,     // fallback to IP if Geolocation fails or rejected
            addressLookup: true
        };
        geolocator.locate(options, function (err, location) {
            if (err) { $(".curLocBtn").text("Current Location - Error").addClass('err'); return console.log(err); }
            else{
                $("button.curLocBtn").removeClass('inactive');
                $("button.curLocBtn").text("Use Current Location");
                $("button.curLocBtn").click(function () {
                    if(loc.includes("Account/Register")){
                        $("select#newProfile_state option").each(function () {
                            if ($(this).attr("value").includes(location.address.state)) { $(this).attr("selected", true); }
                        });
                        $("#newProfile_city").attr("value", location.address.city);
                        $("#newProfile_zipcode").attr("value", location.address.postalCode);
                        $("#newProfile_address").attr("value", location.address.streetNumber + " " + location.address.street);
                    }
                    else if(loc.includes("Product/Create")){
                        $("select#state option").each(function () {
                            if ($(this).attr("value").includes(location.address.state)) { $(this).attr("selected", true); }
                        });
                        $("#city").attr("value", location.address.city);
                        $("#zipcode").attr("value", location.address.postalCode);
                        $("#address").attr("value", location.address.streetNumber + " " + location.address.street);
                    }
                });
            }
        });
    }
/*END*/
/*BEGIN manage profile animation*/
    $("#changeUN").click(function () {
        $("#usernameForm").slideToggle(500);
        $(this).next(".x").toggle();
    });
    $("#changePW").click(function () {
        $("#changePwForm").slideToggle(500);
        $(this).next(".x").toggle();
    });
/*END*/
/*BEGIN category selectlist selected option*/
    if(loc.toLowerCase().includes("product/publiclisting")){
        var rows = $(".rowTitle");
        var opts = $("select option");
        opts.each(function () {
            $(this).removeAttr("selected");
        });
        if (rows.length === 1) {
            opts.each(function () {
                if ($(this).text() == rows[0].children[0].firstChild.data)
                    $(this).attr("selected","selected");
            });
        }
    }   
/*END*/
});