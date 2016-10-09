'use strict'

$(window).on("load", function () {

    var currentUserame = "";
    var currentPassword = "";
    
    $("#logIn").unbind();
    $("#logIn").on("click", function (event) {
        event.preventDefault();

        if (!validForm("username") || !validForm("password")) {
            alert("Write username or password!");
        }

        queryLogin()
            .done(function (response) {
                if (response.length == 1) {
                    $.ajax({
                        "type": "POST",
                        "url": urlContent.UrlPath + "/Account/UserLogin",
                        "data": {
                            model: {
                                "Username": currentUserame,
                                "Password": currentPassword
                            },
                            url : "Home"
                        }
                    }).done(function (res) {
                        currentUserame = "";
                        currentPassword = "";

                        window.location.href = urlContent.UrlPath + "/";
                    });
                } else {
                    alert("Username or password is not correct!");
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

        var link = unHash(urlContent.GetUserCredentials);

        var obj = $.ajax({
            "type": "GET",
            "url": link + "{\"$and\":[{\"username\":\"" + currentUserame + "\", \"password\":\"" + currentPassword + "\"}]}",
            "headers": {
                "authorization": "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk"
            }
        });

        return obj;
    }
    
    function unHash(value) {
        var hash = ["aGs", "asS", "1as", "asd", "12s", "!as", ".as", "ouk", "por", "pek",
            "12d", "vbf", "mat", "qpg", "3yh", "asr", "098", "xcl", "laa", "ASe",
            "pka", "1rs", "9ps", "4p7", "993", "128",
            "ASW", "QWE", "TTR", "EWE", "AAA", "PRT", "Y6T", "LKL", "MNB", "OIP",
            "QTY", "VVB", "MNM", "ZZX", "XCX", "CVC", "VBV", "MLP", "NJI", "VGY",
            "ZSE", "XDR", "CFT", "BHU", "UHB", "TFC",
            "//1", "154", "165", "098", "1pl", "wer", "100", "--9", "mbo", "]][",
            "776", "112", "345", "111", "jkl", "hhh", "GGG"];
        var chars = ["a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            ".", "/", "_", "-", ":", "?", "="];

        var parts = value.match(/[\s\S]{1,3}/g) || [];
        var result = "";
        var index;

        for (var i = 0; i < parts.length; i++) {
            index = hash.indexOf(parts[i]);

            if (index == -1) {
                continue;
            }

            result += chars[index];
        }

        return result;
    }

    //function test(input) {
    //    var result = "";

    //    for (var i = 0; i < input.length; i++) {
    //        result += hash[chars.indexOf(input[i])];
    //    }

    //    return result;
    //}

});