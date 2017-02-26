ViewModel.Account = function () {
    var self = this;
    self.message;

    self.loadInformation = function () {
        var text = "";
        if (currentStatus.trim().length > 0) {
            Service.getData("/About/GetConstants", { query: currentStatus })
                .done(function (response) {
                    self.message = JSON.parse(response["data"]);
                    text = self.message[0]["title"] ? self.message[0]["title"] : "";
                    text += self.message[0]["text"] ? "<br />" + self.message[0]["text"] : "";
                    $("#currentStatus").html(text);
                });
        }
    }
}