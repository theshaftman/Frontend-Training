ViewModel.Contact = function () {
    var self = this;
    self.myInformation;

    self.loadInformation = function () {
        Service.Contact.getData("/About/GetInformation")
            .done(function (response) {
                if (response.status.toLowerCase() === "completed") {
                    self.myInformation = JSON.parse(response["data"])[0];

                    $("#myInformation").text(self.myInformation["myInformation"]);
                }
            });
    }
}