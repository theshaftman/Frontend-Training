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
        var dfd = $.Deferred();

        getQuestionsCount()
            .done(function (res) {
                res = JSON.parse(res["data"]);
                var currentID = Number(res["count"]) + 1;
                var comment = $("#comment").val();

                var settings = {
                    "type": "POST",
                    "url": urlForm["currentServer"] + "/Messages/SendQuestion",
                    "data": {
                        "id": "" + currentID,
                        "author": urlForm["username"],
                        "question": "" + comment
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
        return $.ajax({
            "type": "GET",
            "url": urlForm.currentServer + "/Messages/GetQuestionsCount",
        });
    }

    function clearFields() {
        $("#comment").val("");
    }
    
});