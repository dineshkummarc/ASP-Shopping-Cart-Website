/// <reference path="../../Scripts/jquery-1.8.2.js" />

$(document).ready(function () {

    $("#btnAdd").live("click", function () {

        if (Page_ClientValidate('')) {

            $.ajax({

                url: "/AJAX/Services/role.asmx/AddRole",
                data: "{ \"Role\": \"" + $("#txtRole").val() + "\"}",
                dataType: "json",
                type: "POST",
                contentType: "application/json",
                success: function (data) {
                    if (data.d == true) {
                        $("#txtRole").val("");
                        $("#lblServerSideErrorBottom").text("");
                        $("#btnRefresh").trigger("click");
                    }
                    else {
                        $("#lblServerSideErrorBottom").text("Role Already Exists");
                    }
                },
                error: function (data, status, jqXHR) {
                    window.location = "/error.aspx?aspxerrorpath=/administrator/role.aspx";
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

                url: "/AJAX/Services/role.asmx/UpdateRole",
                data: "{ \"RoleID\": \"" + $("#hdnID").val() +
                    "\", \"Role\": \"" + $("#txtRole").val() + "\"}",
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
                    window.location = "/error.aspx?aspxerrorpath=/administrator/role.aspx";
                }

            });

            return true;
        }
        else {
            return true;
        }

    });

});