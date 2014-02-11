/// <reference path="../../Scripts/jquery-1.8.2.js" />

$(document).ready(function () {

    var ListCount = 0;

    $("#ddlProduct").hide();
    $("#txtPrice").hide();
    $("#txtQuantity").hide();

    $("#lblProduct").hide();
    $("#lblPrice").hide();
    $("#lblQuantity").hide();

    $("#btnNew").hide();
    $("#btnSubmit").hide();
    $("#btnCancel").hide();

    LoadOrder();
    LoadSuppliers();

    function ValidateQuantity() {

        var isValid = true;
        var ErrorMessage = "";

        if ($("#txtQuantity").val().trim() == "") {
            isValid = false;
            ErrorMessage += "Insert a Valid Quantity<br/>";
        }
        else if ($("#txtQuantity").val().trim() % 1 != 0) {
            isValid = false;
            ErrorMessage += "Insert a Valid Quantity<br/>";
        }
        else if ($("#txtQuantity").val().trim() < 1) {
            isValid = false;
            ErrorMessage += "Quantity must be Greater than 0<br/>";
        }
        else if ($("#txtQuantity").val().trim() > 20000) {
            isValid = false;
            ErrorMessage += "Quantity must be Less than 20,000<br/>";
        }

        $("#txtError").html(ErrorMessage);
        return isValid;

    }

    function LoadOrder() {
        $.ajax({

            url: "/AJAX/Services/supplierorder.asmx/RetrieveItems",
            data: "{}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {

                $("#tblOrderItems").html("");
                $("#tblOrderItems").html(data.d);

            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/supplierorder.aspx";
            }
        });
    }

    function LoadSuppliers() {
        $.ajax({
            url: "/AJAX/Services/supplierorder.asmx/PopulateSuppliers",
            data: "{}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {
                $("#ddlSupplier").html("");
                $("#ddlSupplier").html(data.d);
            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/supplierorder.aspx";
            }
        });
    }

    function LoadProducts() {
        $.ajax({
            url: "/AJAX/Services/supplierorder.asmx/PopulateProducts",
            data: "{ \"SupplierID\": \"" + $("#ddlSupplier").val() + "\"}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {
                $("#ddlProduct").html("");
                $("#ddlProduct").html(data.d);
            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/supplierorder.aspx";
            }
        });
    }

    function LoadProductPrice() {
        $.ajax({
            url: "/AJAX/Services/supplierorder.asmx/RetrieveProductPrice",
            data: "{ \"ProductID\": \"" + $("#ddlProduct").val() + "\"}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {
                $("#txtPrice").text("€ " + data.d);
            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/supplierorder.aspx";
            }
        });
    }

    function AddItemToList() {
        $.ajax({

            url: "/AJAX/Services/supplierorder.asmx/AddItemToList",
            data: "{ \"ProductID\": \"" + $("#ddlProduct").val() +
                "\", \"Price\": \"" + $("#txtPrice").text() +
                "\", \"Quantity\": \"" + $("#txtQuantity").val() + "\"}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function () {

                ListCount++;

                LoadOrder();

                ClearData();

                $("#btnSubmit").show();
            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/supplierorder.aspx";
            }
        });
    }

    function PlaceOrder() {
        $.ajax({
            url: "/AJAX/Services/supplierorder.asmx/PlaceOrder",
            data: "{ \"SupplierID\": \"" + $("#ddlSupplier").val() + "\"}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {
                ClearList();
            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/supplierorder.aspx";
            }
        });
    }

    function ClearList() {

        $.ajax({
            url: "/AJAX/Services/supplierorder.asmx/ClearList",
            data: "{}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {

                ListCount = 0;

                LoadOrder();
            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/supplierorder.aspx";
            }
        });

    };

    $("#ddlSupplier").change(function () {
        if ($("#ddlSupplier").val() != "0") {
            $("#ddlSupplier").attr("disabled", "disabled");

            $("#ddlProduct").show();
            $("#lblProduct").show();
            $("#btnCancel").show();

            LoadProducts();
        }
    });

    $("#ddlProduct").change(function () {
        if ($("#ddlProduct").val() != "0") {
            LoadProductPrice();

            $("#txtPrice").show();
            $("#txtQuantity").show();

            $("#lblPrice").show();
            $("#lblQuantity").show();

            $("#btnNew").show();
        }
        else {
            $("#txtPrice").text("").hide();
            $("#txtQuantity").val("").hide();
            $("#lblPrice").hide();
            $("#lblQuantity").hide();

            $("#btnNew").hide();
        }
    });

    $("#btnNew").click(function () {
        //validation goes here
        if (ValidateQuantity()) {
            AddItemToList();
        }

        return false;
    });

    $("input[type=image]").live("click", function () {

        $.ajax({
            url: "/AJAX/Services/supplierorder.asmx/RemoveItemFromList",
            data: "{ \"ProductID\": \"" + $(this).attr("productid") + "\"}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {

                ListCount--;

                LoadOrder();
                LoadProducts();

                $("#txtPrice").text("").hide();
                $("#txtQuantity").val("").hide();
                $("#lblPrice").hide();
                $("#lblQuantity").hide();
                $("#btnNew").hide();

                if (ListCount == 0) {
                    $("#btnSubmit").hide();
                }

            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/supplierorder.aspx";
            }
        });

    });

    $("#btnCancel").click(function () {

        ClearList();
        $("#ddlSupplier").removeAttr("disabled");
        $("#ddlSupplier").val("0");
        $("#ddlProduct").html("").hide();
        $("#txtPrice").text("").hide();
        $("#txtQuantity").val("").hide();
        $("#lblProduct").hide();
        $("#lblPrice").hide();
        $("#lblQuantity").hide();
        $("#btnNew").hide();
        $("#btnSubmit").hide();
        $("#btnCancel").hide();

        return false;
    });

    $("#btnSubmit").live("click", function () {

        PlaceOrder();

        
        $("#ddlSupplier").removeAttr("disabled");
        $("#ddlSupplier").val("0");
        $("#ddlProduct").html("").hide();
        $("#txtPrice").text("").hide();
        $("#txtQuantity").val("").hide();
        $("#lblProduct").hide();
        $("#lblPrice").hide();
        $("#lblQuantity").hide();
        $("#btnNew").hide();
        $("#btnSubmit").hide();
        $("#btnCancel").hide();

        return false;
    });

    function ClearData() {
        LoadProducts();
        $("#txtPrice").text("").hide();
        $("#txtQuantity").val("").hide();
        $("#lblPrice").hide();
        $("#lblQuantity").hide();
        $("#btnNew").hide();
    }
});