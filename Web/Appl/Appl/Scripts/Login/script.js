'use strict'

$(window).on("load", function () {

    var currentUserame = "";
    var currentPassword = "";

    $("#logIn").unbind();
    $("#logIn").on("click", function (event) {
        event.preventDefault();

        if (!validForm("username") || !validForm("password")) {
            errorHandle("Error!", "Write username or password!");
        }

        queryLogin()
            .done(function (response) {
                if (response.isFound) {
                    window.location.href = urlContent.UrlPath + "/";
                } else {
                    errorHandle("Error!", "Username or password is not correct!");
                }
            });
    });

    function validForm(fieldName) {
        var x = document.forms["userLogin"][fieldName].value;
        if (x == null || x == "") {
            return false;
        }

        return true;
    }

    function queryLogin() {
        currentUserame = $("#username").val();
        currentPassword = $("#password").val();

        var settings = {
            "type": "POST",
            "url": urlContent.UrlPath + "/Account/UserLogin",
            "data": {
                "Username": currentUserame,
                "Password": currentPassword
            }
        }

        var obj = $.ajax(settings);

        return obj;
    }
    
});