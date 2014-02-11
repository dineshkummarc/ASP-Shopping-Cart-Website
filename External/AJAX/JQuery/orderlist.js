/// <reference path="../../Scripts/jquery-1.8.2.js" />

$(document).ready(function () {

    var OrderType = "";

    $("#btnEditContents").live("click", function () {

        if (($("#txtUserID").val() != undefined) && ($("#txtSupplierID").val() == undefined)) {
            OrderType = "User";

        }
        else if (($("#txtUserID").val() == undefined) && ($("#txtSupplierID").val() != undefined)) {
            OrderType = "Supplier";
        }

        LoadProducts();
    });

    $("#btnUserSearch").live("click", function () {
        $("#OrderItems").html("");
    });

    $("#btnSupplierSearch").live("click", function () {
        $("#OrderItems").html("");
    });

    $("#btnPrintContents").live("click", function () {

        var OrderID = "";

        if (($("#txtUserID").val() != undefined) && ($("#txtSupplierID").val() == undefined)) {
            OrderType = "User";

        }
        else if (($("#txtUserID").val() == undefined) && ($("#txtSupplierID").val() != undefined)) {
            OrderType = "Supplier";
        }

        if (OrderType == "User") {
            OrderID = $("#txtUserID").val();
        }
        else if (OrderType == "Supplier") {
            OrderID = $("#txtSupplierID").val();
        }

        window.open("/printorder.aspx?id=" + OrderID);


    });

    function LoadProducts() {

        var OrderID = "";

        if (OrderType == "User") {
            OrderID = $("#txtUserID").val();
        }
        else if (OrderType == "Supplier") {
            OrderID = $("#txtSupplierID").val();
        }

        $.ajax({
            url: "/AJAX/Services/orderlist.asmx/LoadProducts",
            data: "{ \"OrderID\": \"" + OrderID + "\"}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {

                $("#OrderItems").html("");
                $("#OrderItems").html(data.d);

                $("div[Use=ErrorDiv]").hide();

            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/orderlist.aspx";
            }
        });
    }

    $("input[Use=Remove]").live("click", function () {

        var ProductToDelete = $(this).attr("ProductID");

        $("div[Use=ErrorDiv]").hide();

        var OrderID = "";

        if (OrderType == "User") {
            OrderID = $("#txtUserID").val();
        }
        else if (OrderType == "Supplier") {
            OrderID = $("#txtSupplierID").val();
        }

        $.ajax({

            url: "/AJAX/Services/orderlist.asmx/RemoveItem",
            data: "{ \"OrderID\": \"" + OrderID +
                "\", \"ProductID\": \"" + ProductToDelete +
                "\", \"OrderType\": \"" + OrderType + "\"}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {

                if (data.d == false) {

                    //get previous quantity
                    $("div[ProductID=" + ProductToDelete + "]").show();

                    //show error
                }
                else {
                    LoadProducts();
                }


            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/orderlist.aspx";
            }

        });



    });

    $("input[type=button]").live("click", function () {

        if ($(this).attr("Use") == undefined) {
            $("#OrderItems").html("");
        }

    });

    $("input[Use=Update]").live("click", function () {

        var ProductToUpdate = $(this).attr("ProductID");

        $("div[Use=ErrorDiv]").hide();

        var OrderID = "";

        if (OrderType == "User") {
            OrderID = $("#txtUserID").val();
        }
        else if (OrderType == "Supplier") {
            OrderID = $("#txtSupplierID").val();
        }

        if (ValidateQuantity(ProductToUpdate)) {
            var Quantity = $("#" + ProductToUpdate).val();

            $.ajax({

                url: "/AJAX/Services/orderlist.asmx/UpdateItem",
                data: "{ \"OrderID\": \"" + OrderID +
                "\", \"ProductID\": \"" + ProductToUpdate +
                "\", \"OrderType\": \"" + OrderType +
                "\", \"Quantity\": \"" + Quantity + "\"}",
                dataType: "json",
                type: "POST",
                contentType: "application/json",
                success: function (data) {

                    if (data.d == false) {

                        //get previous quantity
                        $("div[ProductID=" + ProductToUpdate + "]").show();

                        //show error
                    }


                },
                error: function (data, status, jqXHR) {
                    window.location = "/error.aspx?aspxerrorpath=/administrator/orderlist.aspx";
                }

            });
        }
        else {
            alert("Invalid Quantity");
        }

    });

    function ValidateQuantity(ProductID) {

        var isValid = true;

        if ($("#" + ProductID).val().trim() % 1 != 0) {
            isValid = false;
        }
        else if ($("#" + ProductID).val().trim() < 1) {
            isValid = false;
        }
        else if ($("#" + ProductID).val().trim() > 20000) {
            isValid = false;
        }

        return isValid;
    }
});
