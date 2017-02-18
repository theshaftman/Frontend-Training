ViewModel.Home = function () {
    var self = this;
    self.myInformation;
    self.myFiles;

    self.loadInformation = function () {
        var myInfo,
            myPicture;

        $.when(Service.Home.getData("/About/GetInformation"),
            Service.Home.getData("/About/GetFiles"))
            .done(function (responseA, responseB) {
                if (responseA[0]["status"].toLowerCase() === "completed" &&
                        responseB[0]["status"].toLowerCase() === "completed") {
                    self.myInformation = JSON.parse(responseA[0]["data"]);
                    self.myFiles = JSON.parse(responseB[0]["data"]);

                    myPicture = self.myFiles.filter(function (a) { return a["usage"] === "me" })[0]["_downloadURL"];
                    $("#myInformationPicture").attr("src", myPicture);
                    
                    myInfo = self.myInformation.filter(function (a) { return a["index"] === "me" })[0];
                    $("#myInformationTitle").text(myInfo["infoTitle"]);
                    $("#myInformation").text(myInfo["infoBody"]);
                }
            });
    }
}