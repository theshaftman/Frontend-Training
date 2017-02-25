ViewModel.Recipes = function () {
    var self = this;
    self.currentData;
    self.currentFile;
    self.currentFiles;

    self.currentCategoryGroups;
    self.currentCategories;

    self.loadInformation = function () {
        var currentFile,
            currentFileHTML = "",
            categoryHTML = "",
            currentCategoryGroup = [];

        $("#fulltext").val(urlPath["query"]);

        $.when(Service.getData("/Recipes/GetRecipes", 'sort={"recipeId": -1}'),
            Service.getData("/About/GetFiles"),
            Service.getData("/Categories/GetCategoryGroups"),
            Service.getData("/Categories/GetCategories"))
            .done(function (responseA, responseB, responseC, responseD) {
                if (responseA[0]["status"].toLowerCase() === "completed" && responseB[0]["status"].toLowerCase() === "completed" &&
                        responseC[0]["status"].toLowerCase() === "completed" && responseD[0]["status"].toLowerCase() === "completed") {
                    responseA[0]["data"] = responseA[0]["data"].replace(/\\n/g, "<br />");
                    self.currentData = JSON.parse(responseA[0]["data"]);
                    self.currentFiles = JSON.parse(responseB[0]["data"]);

                    self.currentCategoryGroups = JSON.parse(responseC[0]["data"]);
                    self.currentCategories = JSON.parse(responseD[0]["data"]);

                    for (var i = 0; i < self.currentCategoryGroups.length; i++) {
                        categoryHTML += '<div class="facetwp-title">' +
                            '     <span>' + self.currentCategoryGroups[i]["groupName"] + '</span>' +
                            ' </div>';

                        currentCategoryGroup = [];
                        currentCategoryGroup = self.currentCategories.filter(function (obj) {
                            return obj["categoryGroup"] == self.currentCategoryGroups[i]["groupId"];
                        });
                        for (var j = 0; j < currentCategoryGroup.length; j++) {
                            categoryHTML += '<div class="facetwp-checkbox" data-value="' + currentCategoryGroup[j]["categoryName"] + '">' +
                                '    <span class="choose"></span><span class="categoryName">' + currentCategoryGroup[j]["categoryName"] + '</span> <!--<span class="facetwp-counter">(2)</span>-->' +
                                '</div>';
                        }
                    }

                    $("#categoryView").append(categoryHTML);

                    if (urlPath["query"].length > 0) {
                        self.currentData = self.currentData.filter(function (obj) {
                            return obj["recipeTitle"].toLowerCase().indexOf(urlPath["query"].toLowerCase()) > -1 ? obj : null;
                        });
                    }

                    for (var i = 0; i < self.currentData.length; i++) {
                        currentFile = self.currentFiles.filter(function (a) { return a["usage"] == self.currentData[i]["recipeId"]; })[0];

                        currentFileHTML += '<article class="simple-grid one-fourth">' +
                            '    <header class="entry-header">' +
                            '        <a class="entry-image-link" href="/Recipes/Recipe?id=' + self.currentData[i]["recipeId"] + '" aria-hidden="true"><img src="' + currentFile["_downloadURL"] + '" class="post-image entry-image" alt="No image" itemprop="image"></a>' +
                            '        <h2 class="entry-title" itemprop="headline"><a href="/Recipes/Recipe?id=' + self.currentData[i]["recipeId"] + '" rel="bookmark">' + self.currentData[i]["recipeTitle"] + '</a></h2>' +
                            '    </header><div class="entry-content"></div><footer class="entry-footer"></footer>' +
                            '</article>';
                    }

                    $(".recipe-index").append(currentFileHTML);
                }
            });
    }

    self.loadRecipeInformation = function () {
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