'use strict'

$(window).on("load", function () {

    var init = function () {
        var self = this;

        self.reloadPage = function () {
            $("#refresh").unbind();
            $("#refresh").on("click", function (e) {
                e.preventDefault();

                self.loadPage();
            });
        }

        self.loadPage = function (laddaItem) {
            var self = this;

            loadData()
                .done(function (response) {
                    response = JSON.parse(response.data);
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
                    $("#refresh").removeAttr("style");
                    $("#refresh").css({
                        "display": "block",
                        "opacity": "0"
                    });

                    for (var i = 0; i < response.length; i++) {
                        var li = $("<li></li>")
                            .attr({
                                //"class": "panel panel-info"
                            });
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
                    $("#refresh").css({
                        "opacity": "1",
                        "transition": "opacity 2s"
                    });

                    // Draggable
                    //$(".side-navigation").sortable();
                    //$(".side-navigation").disableSelection();

                    //$("#sortable").sortable({
                    //    revert: true
                    //});

                    //$("ul, li").disableSelection();
                }).always(function () {
                    if (laddaItem) {
                        laddaItem.stop();
                    }

                    $("#cancel").click();
                });
        }

        // Add a new subject functionality
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

                // Load updates
                sendEditSubjectFunction(item);

                setTimeout(function () {
                    $(".panel_main").css({
                        "opacity": "1",
                        "transition": "opacity 2s"
                    });
                }, 10);

                return false;
            });
        }

        // Edit subject functionality
        function sendEditSubjectFunction(item) {
            var currentSelf = this;

            $("#sendEditSubject").unbind();
            $("#sendEditSubject").on("click", function () {
                var l = Ladda.create(this);
                l.start();

                sendEditSubjectUpdate(item)
                    .done(function (res) {
                        // units.trainingsData = res;
                        self.loadPage(l);
                        $(".close").click();
                        $("#sendSubjectEditTitle").val("");
                        $("#sendSubjectEditBody").val("");
                    }).always(function () {
                        $(".panel_main").css("opacity", "0");
                    });
            });
        }

        // Update
        function sendEditSubjectUpdate(item) {
            var head = $("#sendSubjectEditTitle").val();
            var body = $("#sendSubjectEditBody").val();

            body = body.replace(/&/g, '&amp;')
                .replace(/</g, '&lt;')
                .replace(/>/g, '&gt;');

            var settings = {
                "type": "PUT",
                "url": urlForm.currentServer + "/Training/SubjectUpdate",
                "data": {
                    "editID": item._id,
                    "id": item.id,
                    "author": urlForm.currentUsername,
                    "subject_title": "" + head,
                    "subject_body": "" + body
                }
            };

            return $.ajax(settings);
        }

        function compare(a, b) {
            if (Number(a.id) > Number(b.id)) {
                return -1;
            }
            if (Number(a.id) < Number(b.id)) {
                return 1;
            }
            return 0;
        }

        function loadCurrentCommentsList(commentsList, item) {
            loadComments(item)
                .done(function (response) {
                    response = JSON.parse(response.data);
                    response = response.sort(compare);

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
            $("#toggle_comments_actions").css("display", "none");

            if (item.author == urlForm.currentUsername) {
                $("#toggle_comments_actions").css("display", "block");
            }

            $("#comment_header_field").text("");
            $("#comment_header_field").text(item.subject_title);
            $("#comment_body_field").html("");
            $("#comment_body_field").html(item.subject_body);
            $("#author_field").text("");
            $("#author_field").text(item.author);

            toggleCommentsActions(item);
        }

        function toggleCommentsActions(item) {
            // Edit subject
            $("#toggle_comments_actions_edit").unbind();
            $("#toggle_comments_actions_edit").on("click", function (e) {
                e.preventDefault();
                $("#sendSubjectEditTitle").val("");
                $("#sendSubjectEditBody").val("");

                var subjectTitle = $("#comment_header_field").text();
                $("#sendSubjectEditTitle").val(subjectTitle);

                var subjectBody = $("#comment_body_field").html();
                $("#sendSubjectEditBody").val(subjectBody);

                $("#edit_subject").click();

                // edit function
            });

            // Remove subject
            $("#agree").unbind();
            $("#agree").on("click", function (e) {
                var l = Ladda.create(this);
                l.start();

                e.preventDefault();
                deleteSubjectUpdate(item)
                    .done(function (response) {
                        self.loadPage(l);
                    }).always(function () {
                        $(".panel_main").css("opacity", "0");
                        $(".close").click();
                    });
            });
        }

        // Delete subject
        function deleteSubjectUpdate(item) {
            var settings = {
                "url": urlForm.currentServer + "/Training/DataDelete",
                "type": "POST",
                "data": {
                    givenData: "subjects",
                    id: item._id,
                    currentId: item.id
                }
            }

            //author: item.author,
            //subject_title: item.subject_title

            return $.ajax(settings);
        }

        function loadComments(item) {
            var settings = {
                "type": "GET",
                "url": urlForm.currentServer + "/Training/GetData",
                "data": {
                    "givenData": "subjectComments",
                    "query": "?query={\"subject_id\": \"" + item.id + "\"}&sort={\"id\": 1}"
                }
            };

            return $.ajax(settings);
        }

        function loadData() {
            var settings = {
                "type": "GET",
                "url": urlForm.currentServer + "/Training/GetData"
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
                        if (res.status == "success") {
                            res = JSON.parse(res.data);

                            sendCurrentComment(res.count, subjectID)
                                .done(function (response) {
                                    // Clear the comment textarea.
                                    $("#comment_body").val("");
                                }).always(function () {
                                    l.stop();
                                    loadCurrentCommentsList(commentsList, subjectID);
                                });
                        }
                    });
                return false;
            });
        }

        function getAllCommentsCount() {
            var settings = {
                "type": "GET",
                "url": urlForm.currentServer + "/Training/GetData",
                "data": {
                    "givenData": "subjectComments",
                    "query": "/_count"
                }
            };

            return $.ajax(settings);
        }

        function sendCurrentComment(count, subjectID) {
            var comment = $("#comment_body").val();

            if (comment.length <= 0) {
                errorHandle("Error!", "Comment can not be empty.");
                return false;
            }

            count += 1;

            var settings = {
                "type": "POST",
                "url": urlForm.currentServer + "/Training/InsertCommentData",
                "data": {
                    id: "" + count,
                    subject_id: "" + subjectID.id,
                    author: urlForm.currentUsername,
                    comment: "" + comment
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
            errorHandle("Error!", "Write a title and a text for the given subject.");

            setTimeout(function () {
                if (laddaItem) {
                    laddaItem.stop();
                }
            }, 500);
            return dfd;
        }

        // Find max number.
        var maxNumber = 0;
        for (var i = 0; i < units.trainingsData.length; i++) {
            if (Number(units.trainingsData[i].id) > maxNumber) {
                maxNumber = Number(units.trainingsData[i].id);
            }
        }
        var index = maxNumber + 1;

        body = body.replace(/&/g, '&amp;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;');

        var settings = {
            "type": "POST",
            "url": urlForm.currentServer + "/Training/InsertSubjectData",
            "data": {
                "id": "" + index,
                "author": urlForm.currentUsername,
                "subject_title": "" + title,
                "subject_body": "" + body
            }
        };

        var obj = $.ajax(settings);

        // <ul><li>Test</li></ul>

        dfd.resolve(obj);
        return dfd;
    }

    // Init method which loads the functionality
    var pageCreation = new init();
    try {
        pageCreation.loadPage();
        pageCreation.sendSubjectFunction();
        pageCreation.reloadPage();
    } catch (e) {
        errorHandle("Error", "We have an error loading the page. Please refresh your page.");
    }

});

var units = {
    trainingsData: null,
    commentsExtended: false
};