/// <reference path="../../Scripts/jquery-1.8.2.js" />

$(document).ready(function () {

    $("#lblCreditCard").hide();
    $("#txtCreditCard").hide();
    $("#btnCheckout").hide();
    $("#txtCartTotal").hide();
    $("#lblCartTotal").hide();

    LoadShoppingCartItems();

    function LoadShoppingCartItems() {
        $.ajax({
            url: "/AJAX/Services/checkout.asmx/LoadShoppingCartItems",
            data: "{ }",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {

                if (data.d == "<table></table>") {
                    $("#lblCreditCard").hide();
                    $("#txtCreditCard").hide();
                    $("#btnCheckout").hide();
                    $("#txtCartTotal").hide();
                    $("#lblCartTotal").hide();
                    $("#lblError").text("There are no items to be checked out");
                }
                else {
                    $("#ShoppingCartItems").html(data.d);

                    $("div[Use=ErrorDiv]").hide();
                    $("#lblCreditCard").show();
                    $("#txtCreditCard").show();
                    $("#btnCheckout").show();

                    $("#txtCartTotal").show();
                    $("#lblCartTotal").show();
                    RetrieveCartTotal();
                }


            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/user/checkout.aspx";
            }
        });
    }

    $("#btnCheckout").click(function () {

        $("div[Use=ErrorDiv]").hide();

        //Checking For Violating ID's
        $.ajax({
            url: "/AJAX/Services/checkout.asmx/CheckProductQuantities",
            data: "{}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {

                var FormIsValid = true;

                for (var i = 0; i < data.d.length; i++) {
                    $("div[ProductID=" + data.d[i] + "]").show();
                    FormIsValid = false;
                }

                if ((ValidateNumeric() == true) & (FormIsValid == true) & (ValidateCreditCard() == true)) {

                    //completing checkout
                    $.ajax({
                        url: "/AJAX/Services/checkout.asmx/CompleteCheckout",
                        data: "{ \"CreditCard\": \"" + $("#txtCreditCard").val() + "\"}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json",
                        success: function () {

                            $("#ShoppingCartItems").html("");
                            LoadShoppingCartItems();

                            RedirectUser();
                        },
                        error: function (data, status, jqXHR) {
                            window.location = "/error.aspx?aspxerrorpath=/user/checkout.aspx";
                        }
                    });
                }

            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/user/checkout.aspx";
            }
        });

        return false;
    });

    function RetrieveCartTotal() {

        $.ajax({
            url: "/AJAX/Services/checkout.asmx/RetrieveCartTotal",
            data: "{}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {
                $("#txtCartTotal").text(data.d);
            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/user/checkout.aspx";
            }
        });


    }

    function RedirectUser() {

        var Success = false;

        $.ajax({
            url: "/AJAX/Services/checkout.asmx/RetrieveUserOrder",
            data: "{}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {

                window.open("/printorder.aspx?id=" + data.d);
                
            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/user/checkout.aspx";
            }
        });

    }

    function ValidateCreditCard() {

        var isValid = true;

        if ($("#txtCreditCard").val().trim() == "") {
            isValid = false;
        }

        if (isValid == false) {
            $("#lblCreditCardError").text("Insert a Valid Credit Card Number");
        }
        else {
            $("#lblCreditCardError").html("&nbsp;");
        }

        return isValid;
    }

    function ValidateNumeric() {

        var isValid = true;

        $("input[Use=Quantity]").each(function () {

            if (this.value.trim() == "") {
                isValid = false;
            }
            else if (this.value.trim() % 1 != 0) {
                isValid = false;
            }
            else if (this.value.trim() < 1) {
                isValid = false;
            }
            else if (this.value.trim() > 1000) {
                isValid = false;
            }

        });

        if (isValid == false) {
            $("#lblError").text("One or More Products have Incorrect Values");
        }
        else {
            $("#lblError").html("&nbsp;");
        }

        isValid;

        return isValid;
    }

    $("input[Use=Quantity]").live("focusout", function () {

        var ProductID = $(this).attr("id");

        if (ValidateNumeric()) {

            $.ajax({
                url: "/AJAX/Services/store.asmx/AddToCart",
                data: "{ \"ProductID\": \"" + ProductID +
                "\", \"Quantity\": \"" + $("#" + ProductID).val() + "\"}",
                dataType: "json",
                type: "POST",
                contentType: "application/json",
                success: function (data) {
                    QuantitiesValid = true;
                    $("#txtCartTotal").show();
                    $("#lblCartTotal").show();
                    RetrieveCartTotal();
                },
                error: function (data, status, jqXHR) {
                    window.location = "/error.aspx?aspxerrorpath=/user/checkout.aspx";
                }
            });
        }

    });

});