/// <reference path="../../Scripts/jquery-1.8.2.js" />

$(document).ready(function () {

    $("#btnAdd").live("click", function () {

        if (Page_ClientValidate('')) {

            $.ajax({

                url: "/AJAX/Services/supplier.asmx/AddSupplier",
                data: "{ \"Supplier\": \"" + $("#txtSupplier").val() +
                    "\", \"Email\": \"" + $("#txtEmail").val() +
                    "\", \"AddressLine1\": \"" + $("#txtAddressLine1").val() +
                    "\", \"AddressLine2\": \"" + $("#txtAddressLine2").val() +
                    "\", \"Town\": \"" + $("#txtTown").val() +
                    "\", \"Postcode\": \"" + $("#txtPostcode").val() +
                    "\", \"Country\": \"" + $("#ddlCountry option:selected").text() + "\"}",
                dataType: "json",
                type: "POST",
                contentType: "application/json",
                success: function () {
                    $("#txtSupplier").val("");
                    $("#txtEmail").val("");
                    $("#txtAddressLine1").val("");
                    $("#txtAddressLine2").val("");
                    $("#txtTown").val("");
                    $("#txtPostcode").val("");
                    $("#ddlCountry").val("0");

                    $("#btnRefresh").trigger("click");
                },
                error: function (data, status, jqXHR) {
                    window.location = "/error.aspx?aspxerrorpath=/administrator/supplier.aspx";
                }

            });

            return true;
        }
        else {
            return true;
        }

    });

    $("#btnUpdate").live("click", function () {

        if (Page_ClientValidate('')) {

            $.ajax({

                url: "/AJAX/Services/supplier.asmx/UpdateSupplier",
                data: "{ \"SupplierID\": \"" + $("#hdnID").val() +
                   "\", \"Supplier\": \"" + $("#txtSupplier").val() +
                    "\", \"Email\": \"" + $("#txtEmail").val() +
                    "\", \"AddressLine1\": \"" + $("#txtAddressLine1").val() +
                    "\", \"AddressLine2\": \"" + $("#txtAddressLine2").val() +
                    "\", \"Town\": \"" + $("#txtTown").val() +
                    "\", \"Postcode\": \"" + $("#txtPostcode").val() +
                    "\", \"Country\": \"" + $("#ddlCountry option:selected").text() + "\"}",
                dataType: "json",
                type: "POST",
                contentType: "application/json",
                success: function () {
                    $("#btnRefresh").trigger("click");
                },
                error: function (data, status, jqXHR) {
                    window.location = "/error.aspx?aspxerrorpath=/administrator/supplier.aspx";
                }

            });

            return true;
        }
        else {
            return true;
        }

    });

});