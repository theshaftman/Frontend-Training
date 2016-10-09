'use strict'

$(window).on("load", function () {
    
    var init = function () {
        var self = this;

        self.loadPage = function (laddaItem) {
            var self = this;

            loadData()
                .done(function (response) {
                    units.trainingsData = response;
                    //console.log(response);
                    var nav = $(".side-navigation");
                    nav.html("");
                    nav.removeAttr("style");
                    nav.css("opacity", "0");
                    $("#add_subject").removeAttr("style");
                    $("#add_subject").css({
                        "display": "block",
                        "opacity": "0"
                    });

                    for (var i = 0; i < response.length; i++) {
                        var li = $("<li></li>");
                        var link = $("<a></a>")
                            .attr({
                                "href": "#",
                                "attr-name": response[i].id
                            })
                            .text(response[i].subject_title);

                        //if (i === 0) {
                        //    li.addClass("active");
                        //}

                        li.append(link);
                        nav.append(li);
                    }

                    liClick();
                    nav.css({
                        "opacity": "1",
                        "transition": "opacity 2s"
                    });
                    $("#add_subject").css({
                        "opacity": "1",
                        "transition": "opacity 2s"
                    });
                }).always(function () {
                    if (laddaItem) {
                        laddaItem.stop();
                    }                  

                    $("#cancel").click();
                });
        }
        
        self.sendSubjectFunction = function () {
            var self = this;

            $("#sendSubject").unbind();
            $("#sendSubject").on("click", function () {
                var l = Ladda.create(this);
                l.start();

                sendSubjectUpdate(l)
                    .done(function (res) {
                        units.trainingsData = res;
                        self.loadPage(l);

                        $("#sendSubjectTitle").val("");
                        $("#sendSubjectBody").val("");
                    });
            });
        }
        
        function liClick() {
            var self = this;

            $(".side-nav li").unbind();
            $(".side-nav li").on("click", function (e) {
                e.preventDefault();

                if ($(this).hasClass("active")) {
                    return false;
                }

                // Hide comments.
                units.commentsExtended = false;
                var currentIcon = $("#toggle_comments i");
                currentIcon.removeAttr("class");
                currentIcon.addClass("glyphicon glyphicon-chevron-down");
                $(".comment_list").css("display", "none");

                // Clear comment field.
                $("#comment_body").val("");

                var commentsList = $(".comment_list ul");
                commentsList.html("");

                $(".panel_main").removeAttr("style");
                $(".panel_main").css("opacity", "0");
                $(".side-nav li").removeClass("active");
                $(this).addClass("active");

                var activeIndex = Number($(this).find("a").attr("attr-name"));

                function findObject(object, value) {
                    return object.id == activeIndex;
                }

                var item = units.trainingsData.find(findObject);
                editFields(item);

                // Load comments
                loadCurrentCommentsList(commentsList, item);

                setTimeout(function () {
                    $(".panel_main").css({
                        "opacity": "1",
                        "transition": "opacity 2s"
                    });
                }, 10);

                return false;
            });
        }

        function loadCurrentCommentsList(commentsList, item) {
            loadComments(item)
                .done(function (response) {
                    for (var i = 0; i < response.length; i++) {
                        var li = $("<li></li>");
                        var header = $("<h5></h5>").text(response[i].author);
                        var comment = $("<span></span>").text(response[i].comment);
                        var line = $("<hr />");

                        header.css("font-weight", "bold");

                        li.append(header);
                        li.append(comment);
                        li.append(line);
                        commentsList.append(li);
                    }

                    showCommentsFunction();
                    sendCommentFunction(commentsList, item);
                });
        }

        function editFields(item) {
            $("#comment_header_field").text("");
            $("#comment_header_field").text(item.subject_title);
            $("#comment_body_field").html("");
            $("#comment_body_field").html(item.subject_body);
            $("#author_field").text("");
            $("#author_field").text(item.author);
        }

        function loadComments(item) {
            var settings = {
                "type": "GET",
                "url": "https://baas.kinvey.com/appdata/kid_rJ-gHb40/subjectComments?query={\"subject_id\": \"" + item.id + "\"}&sort={\"id\": 1}",
                "headers": {
                    "authorization": "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk"
                }
            };

            return $.ajax(settings);
        }

        function loadData() {
            var link = unHash(urlForm.getSubjectsData);

            var settings = {
                "type": "GET",
                "url": link,
                "headers": {
                    "authorization": "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk"
                }
            };

            return $.ajax(settings);
        }

        function showCommentsFunction() {
            $("#toggle_comments").unbind();
            $("#toggle_comments").on("click", function (e) {
                e.preventDefault();

                var currentIcon = $(this).find("i");
                units.commentsExtended = !units.commentsExtended;
                currentIcon.removeAttr("class");

                if (units.commentsExtended) {
                    currentIcon.addClass("glyphicon glyphicon-chevron-up");
                    $(".comment_list").css("display", "block");
                } else {
                    currentIcon.addClass("glyphicon glyphicon-chevron-down");
                    $(".comment_list").css("display", "none");
                }
            });
        }

        function sendCommentFunction(commentsList, subjectID) {
            $("#send_comment").unbind();
            $("#send_comment").on("click", function (e) {
                e.preventDefault();
                var l = Ladda.create(this);
                l.start();

                // Clear the unordered list.
                $(".comment_list ul").html("");

                getAllCommentsCount()
                    .done(function (res) {
                        sendCurrentComment(res.count, subjectID)
                            .done(function (response) {
                                // Clear the comment textarea.
                                $("#comment_body").val("");
                            }).always(function () {
                                l.stop();
                                loadCurrentCommentsList(commentsList, subjectID);
                            });
                    });
                return false;
            });
        }

        function getAllCommentsCount() {
            var settings = {
                "type": "GET",
                "url": "https://baas.kinvey.com/appdata/kid_rJ-gHb40/subjectComments/_count",
                "headers": {
                    "authorization": "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk"
                }
            };

            return $.ajax(settings);
        }

        function sendCurrentComment(count, subjectID) {
            var comment = $("#comment_body").val();

            if (comment.length <= 0) {
                alert("Comment can not be empty.");
                return false;
            }

            count += 1;

            var settings = {
                "type": "POST",
                "url": "https://baas.kinvey.com/appdata/kid_rJ-gHb40/subjectComments",
                "data": {
                    id: count,
                    subject_id: subjectID.id,
                    author: urlForm.currentUsername,
                    comment: comment
                },
                "headers": {
                    "authorization": "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk"
                }
            };

            return $.ajax(settings);
        }

    };

    function sendSubjectUpdate(laddaItem) {
        var title = $("#sendSubjectTitle").val();
        var body = $("#sendSubjectBody").val();
        var dfd = $.Deferred();

        if (!title || title.length <= 0 ||
                !body || body.length <= 0) {
            alert("Write a title and a text for the given subject.");

            setTimeout(function () {
                if (laddaItem) {
                    laddaItem.stop();
                }
            }, 500);
            return dfd;
        }

        var index = units.trainingsData.length + 1;

        if ("" + index == "NaN") {
            return dfd;
        }

        var link = unHash(urlForm.postSubject);

        var obj = $.ajax({
            "type": "POST",
            "url": link,
            "data": {
                id: index,
                author: urlForm.currentUsername,
                subject_title: title,
                subject_body: body
            },
            "headers": {
                "authorization": "Basic a2lkX3JKLWdIYjQwOmMxNDBmN2UwMDEyZDQ3YjE5YTUzMjc4ZTExYWM1NjRk"
            }
        });

        dfd.resolve(obj);
        return dfd;
    }

    var pageCreation = new init();
    pageCreation.loadPage();
    pageCreation.sendSubjectFunction();
    
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

var units = {
    trainingsData: null,
    commentsExtended: false
};