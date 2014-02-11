/// <reference path="../../Scripts/jquery-1.8.2.js" />

$(document).ready(function () {

    var ProductsAjax = false;
    var ShoppingCartAjax = false;

    LoadProducts();

    function LoadProducts() {

        if (ProductsAjax == false) {

            ProductsAjax = true;

            $.ajax({
                url: "/AJAX/Services/cart.asmx/LoadShoppingCartItems",
                data: "{}",
                dataType: "json",
                type: "POST",
                contentType: "application/json",
                success: function (data) {
                    $("#ShoppingCart").html("");
                    $("#ShoppingCart").html(data.d);

                    ProductsAjax = false;
                },
                error: function (data, status, jqXHR) {

                    window.location = "/error.aspx?aspxerrorpath=/user/cart.aspx";

                    ProductsAjax = false;
                }
            });
        }
    }

    //Add Remove Button Code
    $("input[type=image]").live("mouseenter", function () {

        $(this).stop().animate({ opacity: "0.8" }, "fast");

        var ClickAction = $(this).attr("ClickAction");

        if (ClickAction == "Add") {
            $("#lblTooltipText").text("Update");
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


                            LoadProducts();
                            $("#lblToastText").text("Quantity Updated!");

                            
                        },
                        error: function (data, status, jqXHR) {
                            window.location = "/error.aspx?aspxerrorpath=/user/cart.aspx";
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

                        LoadProducts();
                        $("#lblToastText").text("Removed from Cart!");
                        $("#Tooltip").hide();
                    },
                    error: function (data, status, jqXHR) {
                        window.location = "/error.aspx?aspxerrorpath=/user/cart.aspx";
                    }
                });

            }

            //put this in method for use with on succeed
            $("#Toast").stop(true).css("visibility", "visible").hide().fadeToggle("slow");
            $("#Toast").animate({ opacity: 1 }, 2000).fadeToggle("slow");

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

    $(window).scroll(function () {
        if ($(window).scrollTop() >= 48) {
            $("#Toast").stop(true).hide();
        }
    });

});