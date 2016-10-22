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

    // Load updates
    function loadUpdates() {
        getUpdates()
            .done(function (response) {
                var updates = response.modificationsData,
                    updatesList,
                    updateLink,
                    li,
                    link,
                    currentText,
                    authorHeader,
                    authorHeaderText;

                if (updates.length > 0) {
                    updatesList = document.getElementById("updatesInformationList");
                    updatesList.innerHTML = "";

                    updateLink = document.getElementById("updatesInformation");
                    updateLink.style.color = "#c95a5a";
                    
                    for (var i = 0; i < updates.length; i++) {
                        li = document.createElement("LI");
                        link = document.createElement("A");
                        link.setAttribute("href", "#");

                        authorHeader = document.createElement("H4");
                        authorHeaderText = document.createTextNode(updates[i]["Username"]);
                        authorHeader.appendChild(authorHeaderText);

                        currentText = document.createTextNode(updates[i]["Modification"]);

                        link.appendChild(authorHeader);
                        link.appendChild(currentText);
                        li.appendChild(link);

                        updatesList.appendChild(li);
                    }
                }
            });
    }

    var updates = document.getElementById("updatesInformation");
    updates.addEventListener("click", updatesOnClick, false);

    function updatesOnClick(e) {
        e.preventDefault();
        $.ajax({
            "url": window.location.origin + "/Updates/Update",
            "type": "POST"
        }).done(function (response) {
            document.getElementById("updatesInformation").removeAttribute("style");
        });
    }
    
    function getUpdates() {
        return $.ajax({
            "url": window.location.origin + "/Updates/GetUpdates",
            "type": "GET"
        });
    }

    loadUpdates();
    
}

function errorHandle(header, text) {
    $("#error_modal_header").text("");
    $("#error_modal_header").text(header);

    $("#error_modal_body").text("");
    $("#error_modal_body").text(text);

    $("#errorModalButton").click();
}
