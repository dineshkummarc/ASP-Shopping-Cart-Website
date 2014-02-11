/// <reference path="../Scripts/jquery-1.8.2.js" />

$(document).ready(function () {

    $('html').css('overflowY', 'scroll');

    $("input[type=text], input[type=password], textarea").live("focusin", function () {

        $(this).css("border-color", "#00B1FF");
        $(this).css("color", "#00B1FF");

        if ($(this).val() == $(this).attr("DefaultText")) {
            $(this).val("");
        }

    }).live("focusout", function () {

        $(this).css("border-color", "#777777");
        $(this).css("color", "#777777");

        if ($(this).val() == "") {
            $(this).val($(this).attr("DefaultText"));
        }

    });

    $("select").live("focusin", function () {

        $(this).css("border-color", "#00B1FF");

    }).live("focusout", function () {
        $(this).css("border-color", "#777777");
    });

    $("a").live("mouseenter", function () {

        if (($(this).parent().is(".ButtonContainer")) || ($(this).parent().is(".ButtonContainerLarge"))) {
            $(this).parent().css("background-color", "#00B1FF");
        }
        else {
            $(this).css("color", "#00B1FF");
        }

    }).live("mouseleave", function () {

        if (($(this).parent().is(".ButtonContainer")) || ($(this).parent().is(".ButtonContainerLarge"))) {
            $(this).parent().css("background-color", "#333333");
        }
        else {
            if ($(this).attr("currentlyloaded") == undefined) {
                $(this).css("color", "#777777");
            }
        }

    });

});