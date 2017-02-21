ViewModel.Contact = function () {
    var self = this;

    self.loadInformation = function () {
        if (urlData["data"] && urlData["data"].length > 0) {
            $("#modalTitle").text(urlData["status"]);
            $("#modalBody").text(urlData["data"]);
            $("#modalMessage").modal("toggle");
        }
    }
}