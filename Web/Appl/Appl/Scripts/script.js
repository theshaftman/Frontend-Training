'use strict'

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
            .hover(function (e) {
                e.preventDefault();
                $(this).css({
                    "background-color": "#FFF",
                    "transition": "background-color 2s"
                });
            }, function (e) {
                e.preventDefault();
                $(this).css({
                    "background-color": "#e6e6e6",
                    "transition": "background-color 2s"
                });
            });

        $("#webFooter").css({
            "font": "15px arial, sans-serif"
        });
    }, 1);
    
}

function errorHandle(header, text) {
    $("#error_modal_header").text("");
    $("#error_modal_header").text(header);

    $("#error_modal_body").text("");
    $("#error_modal_body").text(text);

    $("#errorModalButton").click();
}
