/// <reference path="../../Scripts/jquery-1.8.2.js" />

$(document).ready(function () {


    $("#btnUpdate").hide();
    $("#btnDeAllocateRole").hide();
    $("#btnAllocateRole").hide();

    $("#btnSearch").click(function () {

        $("#lblError").text("");

        if (ValidatePage()) {

            $("#txtEmail").attr("readonly", "readonly");

            $.ajax({

                url: "/AJAX/Services/allocate.asmx/RetrieveUserType",
                data: "{ \"Email\": \"" + $("#txtEmail").val() + "\"}",
                dataType: "json",
                type: "POST",
                contentType: "application/json",
                success: function (data) {
                    if (data.d == "") {
                        $("#txtEmail").removeAttr("readonly");
                        $("#txtCurrentUserType").val("");
                        $("#ddlUserTypes").html("");
                        $("#btnUpdate").hide();
                    }
                    else {
                        $("#txtCurrentUserType").val(data.d);
                        LoadUserTypes();
                    }
                },
                error: function (data, status, jqXHR) {
                    window.location = "/error.aspx?aspxerrorpath=/administrator/allocate.aspx";
                }

            });
        }
        else {
            $("#lblError").text("Please Input a Valid Email Address");
        }

        return false;
    });

    function LoadUserTypes() {

        $.ajax({

            url: "/AJAX/Services/allocate.asmx/RetrievePossibleTypes",
            data: "{ \"UserType\": \"" + $("#txtCurrentUserType").val() + "\"}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {
                $("#ddlUserTypes").html("");
                $("#ddlUserTypes").html(data.d);
                $("#btnUpdate").show();

                LoadRoles();
            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/allocate.aspx";
            }

        });
    }


    function LoadRoles() {
        $.ajax({

            url: "/AJAX/Services/allocate.asmx/PopulateRoles",
            data: "{ \"Email\": \"" + $("#txtEmail").val() + "\"}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {

                $("#btnDeAllocateRole").hide();
                $("#btnAllocateRole").hide();

                $("#btnDeAllocateRole").show();

                if (data.d[1] != "") {
                    $("#btnAllocateRole").show();
                }

                $("#ddlCurrentRoles").html("");
                $("#ddlCurrentRoles").html(data.d[0]);
                $("#ddlAvailableRoles").html("");
                $("#ddlAvailableRoles").html(data.d[1]);

            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/allocate.aspx";
            }

        });
    }


    $("#btnUpdate").click(function () {


        $.ajax({

            url: "/AJAX/Services/allocate.asmx/AllocateType",
            data: "{ \"Email\": \"" + $("#txtEmail").val() +
                    "\", \"UserTypeID\": \"" + $("#ddlUserTypes").val() + "\"}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {
                //say saved
                $("#btnSearch").trigger("click");
            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/allocate.aspx";
            }

        });

        return false;
    });

    $("#btnDeAllocateRole").click(function () {

        $.ajax({

            url: "/AJAX/Services/allocate.asmx/DeAllocateRole",
            data: "{ \"Email\": \"" + $("#txtEmail").val() +
                    "\", \"Role\": \"" + $("#ddlCurrentRoles").val() + "\"}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {
                //say saved

                $("#lblDeAllocateError").text("");
                if (data.d != "") {
                    $("#lblDeAllocateError").text(data.d);
                }
                else {
                    LoadRoles();
                }
            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/allocate.aspx";
            }

        });

        return false;

    });


    $("#btnAllocateRole").click(function () {

        $.ajax({

            url: "/AJAX/Services/allocate.asmx/AllocateRole",
            data: "{ \"Email\": \"" + $("#txtEmail").val() +
                    "\", \"Role\": \"" + $("#ddlAvailableRoles option:selected").text() + "\"}",
            dataType: "json",
            type: "POST",
            contentType: "application/json",
            success: function (data) {
                //say saved
                LoadRoles();
            },
            error: function (data, status, jqXHR) {
                window.location = "/error.aspx?aspxerrorpath=/administrator/allocate.aspx";
            }

        });

        return false;


    });


    $("#btnClear").click(function () {

        $("#lblDeAllocateError").text("");
        $("#btnUpdate").hide();
        $("#btnDeAllocateRole").hide();
        $("#btnAllocateRole").hide();
        $("#lblError").text("");
        $("#txtEmail").removeAttr("readonly");
        $("#txtEmail").val("");
        $("#txtCurrentUserType").val("");
        $("#ddlUserTypes").html("");
        $("#ddlAvailableRoles").html("");
        $("#ddlCurrentRoles").html("");

        return false;
    });

    function ValidatePage() {

        var pattern = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;

        if (($("#txtEmail").val().trim() == "") || (pattern.test($("#txtEmail").val()) == false)) {
            return false;
        }
        else {
            return true;
        }
    }

});