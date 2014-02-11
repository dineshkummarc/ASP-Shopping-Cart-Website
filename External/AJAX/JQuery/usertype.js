/// <reference path="../../Scripts/jquery-1.8.2.js" />

$(document).ready(function () {

    $("#btnAdd").live("click", function () {

        if (Page_ClientValidate('')) {

            $.ajax({

                url: "/AJAX/Services/usertype.asmx/AddUserType",
                data: "{ \"UserType\": \"" + $("#txtUserType").val() + "\"}",
                dataType: "json",
                type: "POST",
                contentType: "application/json",
                success: function (data) {
                    if (data.d == true) {
                        $("#txtUserType").val("");
                        $("#lblServerSideErrorBottom").text("");
                        $("#btnRefresh").trigger("click");
                    }
                    else {
                        $("#lblServerSideErrorBottom").text("User Type Already Exists");
                    }
                },
                error: function (data, status, jqXHR) {
                    window.location = "/error.aspx?aspxerrorpath=/administrator/usertype.aspx";
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

                url: "/AJAX/Services/usertype.asmx/UpdateUserType",
                data: "{ \"UserTypeID\": \"" + $("#hdnID").val() +
                    "\", \"UserType\": \"" + $("#txtUserType").val() + "\"}",
                dataType: "json",
                type: "POST",
                contentType: "application/json",
                success: function (data) {
                    if (data.d == "") {
     
                        $("#lblServerSideErrorBottom").text("");
                        $("#btnRefresh").trigger("click");
                    }
                    else {
                        $("#lblServerSideErrorBottom").text(data.d);
                    }
                },
                error: function (data, status, jqXHR) {
                    window.location = "/error.aspx?aspxerrorpath=/administrator/usertype.aspx";
                }

            });

            return true;
        }
        else {
            return true;
        }

    });

});