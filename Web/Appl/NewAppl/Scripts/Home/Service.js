Service.Home = (function () {
    var self = this;

    self.getData = function (url) {
        var settings = {
            type: "GET",
            url: window.location.origin + url,
            dataType: "json"
        };

        return $.ajax(settings);
    }

    return {
        getData: self.getData
    }
}());