ViewModel.Contact = function () {
    var self = this;
    self.message;

    self.loadInformation = function () {
        if (urlData["status"] && urlData["status"].length > 0) {
            Service.getData("/About/GetConstants", { query: urlData["status"] })
                .done(function (response) {
                    self.message = JSON.parse(response["data"]);                    

                    if (self.message.length > 0) {
                        $("#modalTitle").text(self.message[0]["title"]);
                        $("#modalBody").text(self.message[0]["text"]);
                        $("#modalMessage").modal("toggle");
                    }
                });
        }
    }
}