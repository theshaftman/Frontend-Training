ViewModel.Recipes = function () {
    var self = this;
    self.currentData;
    self.currentFile;

    self.loadInformation = function () {
        $.when(Service.getData("/Recipes/GetRecipes", { query: "query={\"recipeId\":\"" + urlPath["currentId"] + "\"}" }),
            Service.getData("/About/GetFiles", { query: urlPath["currentId"] }))
            .done(function (responseA, responseB) {
                responseA[0]["data"] = responseA[0]["data"].replace(/\\n/g, "<br />");
                self.currentData = JSON.parse(responseA[0]["data"]);
                self.currentData = self.currentData[0];

                $("#entryTitle").text(self.currentData["recipeTitle"]);
                $("#entryText").html(self.currentData["recipe"]);
                $("#entryTime").text(self.currentData["dateLoaded"]);

                self.currentFile = JSON.parse(responseB[0]["data"]);
                $("#entryPicture").attr("src", self.currentFile[0]["_downloadURL"]);
            });
    }
}