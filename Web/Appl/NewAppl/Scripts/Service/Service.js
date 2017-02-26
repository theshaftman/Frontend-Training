Service = (function () {
    var self = this;

    self.getData = function (url, data) {
        var settings = {
            type: "GET",
            url: window.location.origin + url,
            dataType: "json",
            data: data
        };

        return $.ajax(settings);
    }

    self.postData = function (url, data) {
        var settings = {
            type: "POST",
            url: window.location.origin + url,
            dataType: "json",
            data: data
        };

        return $.ajax(settings);
    }

    return {
        getData: self.getData,
        postData: self.postData
    }
}());