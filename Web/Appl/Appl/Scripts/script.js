﻿'use strict'

window.onload = function () {
    var location = window.location.href;
    var arr = location.split("/");
    var res = [];

    for (var i = 0; i < arr.length; i++) {
        if (arr[i] !== "") {
            res.push(arr[i]);
        }
    }

    if (res.length <= 2) {
        editLink("home_location");
    } else if (res[2] === "Training") {
        editLink("training_location");
    } else if (res[2] === "Messages") {
        editLink("messages_location");
    } else if (res[2] === "Account") {
        editLink("loginLink");
    }

    function editLink(givenID) {
        var button = document.getElementById(givenID);
        button.setAttribute('href', "#");
    }

    $(".page_body_content").css({
        "opacity": "1",
        "transition": "opacity 1s"
    });
    
    setTimeout(function () {
        $(".body-content")
            .mouseenter(function (e) {
                e.preventDefault();
                $(this).css({
                    "background-color": "#FFF",
                    "transition": "background-color 2s"
                });
            }).mouseleave(function (e) {
                e.preventDefault();
                $(this).css({
                    "background-color": "#e6e6e6",
                    "transition": "background-color 2s"
                });
            });
    }, 1);

}