ViewModel.About = function () {
    var self = this;
    self.myInformation
    self.myFiles;

    self.loadInformation = function () {
        var myInfo,
            myPicture,
            currentInfo;

        $.when(Service.About.getData("/About/GetInformation"),
            Service.About.getData("/About/GetFiles"))
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

                        currentInfo = self.myInformation.filter(function (a) { return a["index"] === "info" })[0];
                        $("#blogInformationTitle").text(currentInfo["infoTitle"]);
                        $("#blogInformation").text(currentInfo["infoBody"]);

                        $(window).scrollTop($(window.location.hash).offset().top);
                    }
                });

    }
}