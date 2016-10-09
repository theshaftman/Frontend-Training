'use strict'

$(window).load(function () {

    function loadQuestions() {
        var settings = {
            "type": "GET",
            "url": "https://baas.kinvey.com/appdata/kid_rJ-gHb40/questions",
            "headers": {
                "authorization": "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk"
            }
        };

        return $.ajax(settings);
    }

    function getSortedResponse(response) {
        function compare(a, b) {
            if (Number(a.id) > Number(b.id))
                return -1;
            if (Number(a.id) < Number(b.id))
                return 1;
            return 0;
        }

        var sortedResponse = response.sort(compare);
        return sortedResponse;
    }

    function loadQuestionsData(sortedResponse, setValue) {
        var ul = $("#questions_list");
        ul.html("");

        for (var i = 0; i < sortedResponse.length; i++) {
            if (Number(sortedResponse[i].id) <= Number(setValue)) {
                continue;
            }

            var li = $("<li></li>")
                .attr("class", sortedResponse[i].id);
            var span1 = $("<span></span>")
                .attr("class", "q_id")
                .text(sortedResponse[i].id);
            var span2 = $("<span></span>")
                .attr("class", "q_header")
                .text(sortedResponse[i].author);
            var p = $("<p></p>")
                .attr("class", "q_body")
                .text(sortedResponse[i].question);

            li.append(span1);
            li.append(span2);
            li.append(p);
            ul.append(li);
        }

        return false;
    }

    function loadData(setValue) {
        loadQuestions()
            .done(function (response) {
                var sortedResponse = getSortedResponse(response);
                loadQuestionsData(sortedResponse, setValue);
            });
    }
    
    //$("#questions_refresh").unbind();
    //$("#questions_refresh").on("click", function (e) {
    //    e.preventDefault();
    //    var setValue = $("#setValue").val();

    //    if ("" + Number(setValue) == "NaN" || Number(setValue) == 0) {
    //        loadData();
    //    } else {
    //        loadData(setValue);
    //    }
    //});

    $("#set").unbind();
    $("#set").on("click", function (e) {
        e.preventDefault();
        var setValue = $("#setValue").val();

        if ("" + Number(setValue) == "NaN" || Number(setValue) == 0) {
            loadData();
        } else {
            $("#selectedValue").val("");
            $("#selectedValue").val(setValue);
            loadData(setValue);
        }
    });
});