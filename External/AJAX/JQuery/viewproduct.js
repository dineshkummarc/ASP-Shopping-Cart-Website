/// <reference path="../../Scripts/jquery-1.8.2.js" />

$(document).ready(function () {

    var ProductsAjax = false;
    var ShoppingCartAjax = false;


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
            var ProductID = $("#hdnProductID").val();

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
                if(ValidateQuantity())
                {

                $.ajax({
                    url: "/AJAX/Services/store.asmx/AddToCart",
                    data: "{ \"ProductID\": \"" + ProductID +
                    "\", \"Quantity\": \"" + $("#txtQuantity").val() + "\"}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json",
                    success: function (data) {

                        $("#txtQuantity").val("");
                        $("#lblToastText").text("Added to Cart!");

                    },
                    error: function (data, status, jqXHR) {
                        window.location = "/error.aspx?aspxerrorpath=/user/viewproduct.aspx";
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
                        window.location = "/error.aspx?aspxerrorpath=/user/viewproduct.aspx";
                    }
                });










            }

            //put this in method for use with on succeed
            $("#Toast").stop(true).css("visibility", "visible").hide().fadeToggle("slow");
            $("#Toast").animate({ opacity: 1 }, 2000).fadeToggle("slow");

        }
    });

    function ValidateQuantity() {

        var isValid = true;


        if ($("#txtQuantity").val().trim() % 1 != 0) {
            isValid = false;
        }
        else if ($("#txtQuantity").val().trim() < 1) {
            isValid = false;
        }
        else if ($("#txtQuantity").val().trim() > 20000) {
            isValid = false;
        }

        return isValid;
    }

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

});
