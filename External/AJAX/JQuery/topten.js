/// <reference path="../../Scripts/jquery-1.8.2.js" />

$(document).ready(function () {

    $("#btnPrint").live("click", function () {

        if (($("#txtStartDate").val().trim() != "") && ($("#txtEndDate").val().trim() != "")) {

            window.open("/administrator/printtopten.aspx?sd=" + $("#txtStartDate").val().trim() + "&" + "ed=" + $("#txtEndDate").val().trim());
        
        }
        
    });

    $("#btnPrintClients").live("click", function () {

        window.open("/administrator/printtopten.aspx?u=y");

    });

});