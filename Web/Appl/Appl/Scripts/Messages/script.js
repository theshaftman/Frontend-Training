'use strict'

$(window).on("load", function () {
    $("#submit").unbind();
    $("#submit").on("click", function (e) {
        if ($("#comment").val().trim() == "") {
            errorHandle("Error!", "Please write an message!");
            return false;
        }

        var l = Ladda.create(this);
        l.start();

        sendQuestion()
            .done(function (response) {
                clearFields();
                l.stop();

                if (response.status !== "fail") {
                    errorHandle("Success!", "Message sent successful!");
                } else {
                    errorHandle("Error!", "Try again");
                }
            });
    });

    function sendQuestion() {
        var comment = $("#comment").val();
        var dfd = $.Deferred();

        getQuestionsCount()
            .done(function (res) {
                var currentID = Number(res.count) + 1;
                var link = unHash(urlForm.postQuestion);

                var settings = {
                    "type": "POST",
                    "url": link,
                    "data": {
                        id: currentID,
                        author: urlForm.username,
                        question: comment
                    },
                    "headers": {
                        "authorization": "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk"
                    }
                };

                $.ajax(settings)
                    .done(function (response) {
                        dfd.resolve(response);
                    }).fail(function (err) {
                        dfd.resolve({ "status": "fail" });
                    });

            });

        return dfd;
    }

    function getQuestionsCount() {
        var link = unHash(urlForm.getQuestionsNumber);

        return $.ajax({
            "type": "GET",
            "url": link,
            "headers": {
                "authorization": "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk"
            }
        });
    }

    function clearFields() {
        $("#comment").val("");
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

});