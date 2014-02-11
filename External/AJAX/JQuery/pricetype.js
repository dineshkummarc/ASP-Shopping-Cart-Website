/// <reference path="../../Scripts/jquery-1.8.2.js" />

$(document).ready(function () {

    var Exists = false;

    HideDiscountPane();
    HidePriceAndCheckbox();
    PopulateUserTypes();
    $("#btnDelete").hide();
    $("#btnSubmit").hide();

    function HidePriceAndCheckbox() {
        $("#lblPrice").hide();
        $("#txtPrice").hide();
        $("#lblDiscount").hide();
        $("#chkbxHasDiscount").hide();
    }

    function ShowPriceAndCheckbox() {
        $("#lblPrice").show();
        $("#txtPrice").show();
        $("#lblDiscount").show();
        $("#chkbxHasDiscount").show();
    }

    function HideDiscountPane() {
        $("#lblDateStart").hide();
        $("#txtDateStart").hide();
        $("#lblDateEnd").hide();
        $("#txtDateEnd").hide();
        $("#lblPercentage").hide();
        $("#txtPercentage").hide();
    }

    function ShowDiscountPane() {
        $("#lblDateStart").show();
        $("#txtDateStart").show();
        $("#lblDateEnd").show();
        $("#txtDateEnd").show();
        $("#lblPercentage").show();
        $("#txtPercentage").show();
    }

    function ClearDiscountPane() {
        $("#txtDateStart").val("");
        $("#txtDateEnd").val("");
        $("#txtPercentage").val("");
    }

    function ValidateForm() {

        var pattern = /^\d+(?:\.\d+)?$/;
        var isValid = true;
        var ErrorMessage = "";

        if ($("#txtPrice").val().trim() == "") {
            isValid = false;
            ErrorMessage += "Insert a Valid Price<br/>";
        }
        else if (pattern.test($("#txtPrice").val().trim()) == false) {
            isValid = false;
            ErrorMessage += "Insert a Valid Price<br/>";
        }

        if ($("#chkbxHasDiscount").is(":checked")) {

            if ($("#txtDateStart").val().trim() == "") {
                isValid = false;
                ErrorMessage += "Select a Valid Start Date<br/>";
            }

            if ($("#txtDateEnd").val().trim() == "") {
                isValid = false;
                ErrorMessage += "Select a Valid End Date<br/>";
            }

            if ($("#txtPercentage").val().trim() == "") {
                isValid = false;
                ErrorMessage += "Insert a Valid Percentage<br/>";
            }
            else if (pattern.test($("#txtPercentage").val().trim()) == false) {
                isValid = false;
                ErrorMessage += "Insert a Valid Percentage<br/>";
            }
            else if (($("#txtPercentage").val().trim() < 0.1) || ($("#txtPercentage").val().trim() > 100)) {
                isValid = false;
                ErrorMessage += "Insert a Valid Percentage<br/>";
            }
        }

        $("#txtError").html(ErrorMessage);
        return isValid;
    }

    function PopulateUserTypes() {
        $.ajax({

            url: "/AJAX/Services/pricetype.asmx/PopulateUserTypes",
            data: "{}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {

                $("#ddlUserType").html("");
                $("#ddlUserType").html(data.d);

                PopulateProducts();

            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/pricetype.aspx";
            }
        });
    }

    function PopulateProducts() {
        $.ajax({

            url: "/AJAX/Services/pricetype.asmx/PopulateProducts",
            data: "{}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {

                $("#ddlProduct").html("");
                $("#ddlProduct").html(data.d);

                LoadFields();

            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/pricetype.aspx";
            }
        });
    }

    function LoadFields() {
        $.ajax({

            url: "/AJAX/Services/pricetype.asmx/PopulateFields",
            data: "{ \"UserTypeID\": \"" + $("#ddlUserType").val() +
                "\", \"ProductID\": \"" + $("#ddlProduct").val() + "\"}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {

                if (($("#ddlUserType").val() != 0) && ($("#ddlProduct").val() != 0)) {
                    $("#btnSubmit").show();

                    ShowPriceAndCheckbox();
                }
                else {
                    $("#chkbxHasDiscount").attr("checked", false);
                    HideDiscountPane();
                    HidePriceAndCheckbox();
                    $("#btnSubmit").hide();
                    $("#btnDelete").hide();
                    Exists = false;
                }

                if (data.d == null) {
                    $("#txtPrice").val("");
                    Exists = false;
                }
                else {

                    $("#txtPrice").val(data.d[0]);

                    if ((data.d[1] == null) || (data.d[2] == null) || (data.d[3] == null)) {
                        $("#chkbxHasDiscount").attr("checked", false);
                        HideDiscountPane();
                        $("#txtDateStart").val("");
                        $("#txtDateEnd").val("");
                        $("#txtPercentage").val("");
                    }
                    else {
                        $("#chkbxHasDiscount").attr("checked", true);
                        ShowDiscountPane();
                        $("#txtDateStart").val(data.d[1]);
                        $("#txtDateEnd").val(data.d[2]);
                        $("#txtPercentage").val(data.d[3]);
                    }

                    Exists = true;

                    $("#btnDelete").show();
                }
            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/pricetype.aspx";
            }
        });

    }

    function AddPriceType() {
        $.ajax({

            url: "/AJAX/Services/pricetype.asmx/AddPriceType",
            data: "{ \"UserTypeID\": \"" + $("#ddlUserType").val() +
                "\", \"ProductID\": \"" + $("#ddlProduct").val() +
                "\", \"Price\": \"" + $("#txtPrice").val() +
                "\", \"DiscountStart\": \"" + $("#txtDateStart").val() +
                "\", \"DiscountEnd\": \"" + $("#txtDateEnd").val() +
                "\", \"DiscountPercentage\": \"" + $("#txtPercentage").val() + "\"}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {

                if (data.d == false) {
                    $("#txtDateError").text("Start Date Must Start Before End Date");
                }
                else {
                    $("#txtDateError").text("");
                    $("#ddlUserType").val("0");
                    $("#chkbxHasDiscount").attr("checked", false);
                    HideDiscountPane();
                    HidePriceAndCheckbox();
                    $("#btnSubmit").hide();
                    $("#btnDelete").hide();
                    Exists = false;
                }

            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/pricetype.aspx";
            }

        });
    }

    function UpdatePriceType() {
        $.ajax({

            url: "/AJAX/Services/pricetype.asmx/UpdatePriceType",
            data: "{ \"UserTypeID\": \"" + $("#ddlUserType").val() +
                "\", \"ProductID\": \"" + $("#ddlProduct").val() +
                "\", \"Price\": \"" + $("#txtPrice").val() +
                "\", \"DiscountStart\": \"" + $("#txtDateStart").val() +
                "\", \"DiscountEnd\": \"" + $("#txtDateEnd").val() +
                "\", \"DiscountPercentage\": \"" + $("#txtPercentage").val() + "\"}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {

                if (data.d == false) {
                    $("#txtDateError").text("Start Date Must Start Before End Date");
                }
                else {
                    $("#txtDateError").text("");
                    $("#ddlUserType").val("0");
                    $("#chkbxHasDiscount").attr("checked", false);
                    HideDiscountPane();
                    HidePriceAndCheckbox();
                    $("#btnSubmit").hide();
                    $("#btnDelete").hide();
                    Exists = false;
                }

            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/pricetype.aspx";
            }

        });
    }

    function DeletePriceType() {

        $.ajax({

            url: "/AJAX/Services/pricetype.asmx/DeletePriceType",
            data: "{ \"UserTypeID\": \"" + $("#ddlUserType").val() +
                "\", \"ProductID\": \"" + $("#ddlProduct").val() + "\"}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function () {


            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/pricetype.aspx";
            }

        });
    }

    function Clear() {

        $("#txtError").html("");
        $("#txtDateError").text("");
        $("#chkbxHasDiscount").attr("checked", false);
        $("#txtDateStart").val("");
        $("#txtDateEnd").val("");
        $("#txtPercentage").val("");
        HideDiscountPane();
        Exists = false;

    }

    $("#chkbxHasDiscount").change(function () {

        $("#txtError").html("");

        if ($(this).is(":checked")) {
            ShowDiscountPane();
        }
        else {
            ClearDiscountPane();
            HideDiscountPane();
        }
    });

    //on add check exists

    $("#btnSubmit").click(function () {

        if (ValidateForm()) {
            if (Exists == false) {

                AddPriceType();

            }
            else {

                UpdatePriceType();

            }
        }

        return false;
    });

    $("#btnDelete").click(function () {
        if (Exists == true) {

            DeletePriceType();
            Clear();
            $("#ddlUserType").val("0");
            HidePriceAndCheckbox();
            $("#btnSubmit").hide();
            $("#btnDelete").hide();

        }

        Exists = false;
        return false;
    });

    $("#ddlUserType").change(function () {
        Clear();
        LoadFields();
    });

    $("#ddlProduct").change(function () {
        Clear();
        LoadFields();
    });

});