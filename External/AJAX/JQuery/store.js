 /// <reference path="../../Scripts/jquery-1.8.2.js" />

$(document).ready(function () {

    var ProductsAjax = false;
    var ShoppingCartAjax = false;

    function LoadProducts() {

        if (ProductsAjax == false) {

            ProductsAjax = true;

            $.ajax({
                url: "/AJAX/Services/store.asmx/AppendProducts",
                data: "{ \"ParentID\": \"" + $("#ddlParentCategories").val() +
                    "\", \"ChildID\": \"" + $("#ddlChildCategories").val() +
                    "\", \"SearchText\": \"" + "" +
                    "\", \"LoadAll\": \"" + "false" + "\"}",
                dataType: "json",
                type: "POST",
                contentType: "application/json",
                success: function (data) {
                    //if first batch then just set initial 40 products

                    $("#tblProducts").html("");
                    $("#tblProducts").html(data.d);

                    ProductsAjax = false;
                },
                error: function (data, status, jqXHR) {
                    window.location = "/error.aspx?aspxerrorpath=/user/store.aspx";

                    ProductsAjax = false;
                }
            });
        }
    }

    function LoadChildCategories() {

        $.ajax({
            url: "/AJAX/Services/store.asmx/RetrieveChildCategories",
            data: "{ \"ParentID\": \"" + $("#ddlParentCategories").val() + "\"}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {

                $("#ddlChildCategories").html("");
                $("#ddlChildCategories").html(data.d);

                //LOAD PRODUCTS GOES HERE
                LoadProducts();
            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/user/store.aspx";
            }
        });
    }

    //Retrieving Parent Categories on Load and Populating Dropdown
    $.ajax({
        url: "/AJAX/Services/store.asmx/RetrieveParentCategories",
        data: "{}",
        dataType: "json",
        type: "POST",
        contentType: "application/json",
        success: function (data) {

            $("#ddlParentCategories").html("");
            $("#ddlParentCategories").html(data.d);

            //On Success Retrieving Child Categories for Loaded Parents and Populating Dropdown
            LoadChildCategories();

        },
        error: function (data, status, jqXHR) {
            window.location = "/error.aspx?aspxerrorpath=/user/store.aspx";
        }
    });

    $("#ddlParentCategories").change(function () {

        //Load Child Categories for Selected Parent
        LoadChildCategories();
    });


    $("#ddlChildCategories").change(function () {

        //Load Products
        LoadProducts();
    });

    //Add Remove Button Code
    $("input[type=image]").live("mouseenter", function () {

        $(this).stop().animate({ opacity: "0.8" }, "fast");

        var ClickAction = $(this).attr("ClickAction");

        if (ClickAction == "Add") {
            $("#lblTooltipText").text("Add");
        }
        else if (ClickAction == "Remove") {
            $("#lblTooltipText").text("Remove");
        }

        $("#Tooltip").css("visibility", "visible").hide().show();
        $("#Tooltip").css("top", $(this).offset().top + 3 + "px");
        $("#Tooltip").css("left", $(this).offset().left - 70 + "px");

    }).live("mouseleave", function () {

        $(this).stop().animate({ opacity: "1" }, "fast");

        $("#Tooltip").hide();

    }).live("click", function () {

        if (ProductsAjax == false) {

            var ClickAction = $(this).attr("ClickAction");
            var ProductID = $(this).attr("ProductID");

            if ($(window).scrollTop() >= 48) {
                $("#Toast").css("margin-top", "0px");
                $("#Toast").css("top", $(window).scrollTop() + "px");
            }
            else {
                $("#Toast").css("margin-top", "48px");
                $("#Toast").css("top", "0px");
            }

            if (ClickAction == "Add") {
                //ajax for adding
                //on succeed display toast



                // ADD TO CART

                if (ValidateQuantity(ProductID)) {

                    $.ajax({
                        url: "/AJAX/Services/store.asmx/AddToCart",
                        data: "{ \"ProductID\": \"" + ProductID +
                    "\", \"Quantity\": \"" + $("input[name=" + ProductID + "]").val() + "\"}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json",
                        success: function (data) {

                            $("input[name=" + ProductID + "]").val("");
                            $("#lblToastText").text("Added to Cart!");

                        },
                        error: function (data, status, jqXHR) {
                            window.location = "/error.aspx?aspxerrorpath=/user/store.aspx";
                        }
                    });

                }
                else {
                    $("#lblToastText").text("Incorrect Quantity!");

                }










            }
            else if (ClickAction == "Remove") {
                //ajax for removing
                //on succeed display toast



                $.ajax({
                    url: "/AJAX/Services/store.asmx/RemoveFromCart",
                    data: "{ \"ProductID\": \"" + ProductID + "\"}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json",
                    success: function (data) {


                        $("#lblToastText").text("Removed from Cart!");
                    },
                    error: function (data, status, jqXHR) {
                        window.location = "/error.aspx?aspxerrorpath=/user/store.aspx";
                    }
                });










            }

            //put this in method for use with on succeed
            $("#Toast").stop(true).css("visibility", "visible").hide().fadeToggle("slow");
            $("#Toast").animate({ opacity: 1 }, 2000).fadeToggle("slow");

        }
    });

    $(window).scroll(function () {
        if ($(window).scrollTop() >= 48) {
            $("#Toast").stop(true).hide();
        }
    });

    $("input[type=text]").live("focusin", function () {

        if ($(this) != $("#txtSearch")) {
            $(this).val("");
        }

    });

    function ValidateQuantity(ProductID) {

        var isValid = true;


        if ($("input[name=" + ProductID + "]").val().trim() % 1 != 0) {
            isValid = false;
        }
        else if ($("input[name=" + ProductID + "]").val().trim() < 1) {
            isValid = false;
        }
        else if ($("input[name=" + ProductID + "]").val().trim() > 20000) {
            isValid = false;
        }

        return isValid;
    }

    $("#txtSearch").keyup(function () {

        if ($("#txtSearch").val().trim() != "") {

            $("#tblProducts").show();
            $("#ddlParentCategories").hide();
            $("#ddlChildCategories").hide();
            $("#lblParent").hide();
            $("#lblChild").hide();

            if (ProductsAjax == false) {

                ProductsAjax = true;

                $.ajax({
                    url: "/AJAX/Services/store.asmx/AppendProducts",
                    data: "{ \"ParentID\": \"" + "" +
                    "\", \"ChildID\": \"" + "" +
                    "\", \"SearchText\": \"" + $("#txtSearch").val().trim() +
                    "\", \"LoadAll\": \"" + "false" + "\"}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json",
                    success: function (data) {
                        //if first batch then just set initial 40 products

                        $("#tblProducts").html("");
                        $("#tblProducts").html(data.d);

                        ProductsAjax = false;
                    },
                    error: function (data, status, jqXHR) {
                        window.location = "/error.aspx?aspxerrorpath=/user/store.aspx";

                        ProductsAjax = false;
                    }
                });
            }

        }
        else {

            $("#tblProducts").hide();

        }

    }).focusout(function () {

        if ($("#txtSearch").val().trim() == "") {

            $("#tblProducts").html("");
            LoadProducts();
            $("#ddlParentCategories").show();
            $("#ddlChildCategories").show();
            $("#lblParent").show();
            $("#lblChild").show();
            $("#tblProducts").show();
        }


    }).focusin(function () {

        if (ProductsAjax == false) {

            ProductsAjax = true;

            $.ajax({
                url: "/AJAX/Services/store.asmx/AppendProducts",
                data: "{ \"ParentID\": \"" + "" +
                    "\", \"ChildID\": \"" + "" +
                    "\", \"SearchText\": \"" + "" +
                    "\", \"LoadAll\": \"" + "true" + "\"}",
                dataType: "json",
                type: "POST",
                contentType: "application/json",
                success: function (data) {
                    //if first batch then just set initial 40 products

                    $("#tblProducts").html("");
                    $("#tblProducts").html(data.d);

                    ProductsAjax = false;
                },
                error: function (data, status, jqXHR) {
                    window.location = "/error.aspx?aspxerrorpath=/user/store.aspx";

                    ProductsAjax = false;
                }
            });
        }

        $("#tblProducts").hide();
        $("#ddlParentCategories").hide();
        $("#ddlChildCategories").hide();
        $("#lblParent").hide();
        $("#lblChild").hide();

    }).keypress(function (event) {

        if (event.keyCode == 13) {
            event.preventDefault();
        }

    });

});