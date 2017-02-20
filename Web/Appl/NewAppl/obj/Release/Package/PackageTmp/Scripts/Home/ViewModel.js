ViewModel.Home = function () {
    var self = this;
    self.currentData;
    self.currentFiles;

    self.loadPage = function () {
        var currentFile;

        $.when(Service.getData("/Recipes/GetRecipes", 'query=sort={"recipeId": -1}&limit=3'),
            Service.getData("/About/GetFiles"))
            .done(function (responseA, responseB) {
                if (responseA[0]["status"].toLowerCase() === "completed") {
                    responseA[0]["data"] = responseA[0]["data"].replace(/\\n/g, "<br />");
                    self.currentData = JSON.parse(responseA[0]["data"]);
                    self.currentFiles = JSON.parse(responseB[0]["data"]);
                    for (var i = 0; i < self.currentData.length; i++) {
                        currentFile = self.currentFiles.filter(function (a) { return a["usage"] == self.currentData[i]["recipeId"]; })[0];

                        // add information to the top
                        $("#solidQuo").append(
                             '   <li aria-hidden="true" class="soliloquy-item soliloquy-item-7 soliloquy-image-slide soliloquy-post-32696 soliloquy-post soliloquy-type-post soliloquy-status-publish soliloquy-format-standard soliloquy-has-post-thumbnail soliloquy-category-breakfast soliloquy-category-featured soliloquy-category-gluten-free soliloquy-category-meals soliloquy-category-recipes soliloquy-category-tree-nut-free soliloquy-tag-breakfast soliloquy-tag-brunch soliloquy-tag-brussels-sprouts soliloquy-tag-cornmeal soliloquy-tag-grits soliloquy-tag-sausage soliloquy-tag-tempeh soliloquy-entry" draggable="false" style="list-style: none; float: left; position: relative; width: ' + (1149.99 / parseFloat(self.currentData.length)) + 'px; margin-right: 10px;">' +
                             '       <a href="/Recipes/Recipe?id=' + self.currentData[i]["recipeId"] + '" class="soliloquy-link" title="' + self.currentData[i]["recipeTitle"] + '">' +
                             '           <img id="soliloquy-image-32696" class="soliloquy-image soliloquy-image-7" src="' + currentFile["_downloadURL"] + '" alt="' + self.currentData[i]["recipeTitle"] + '">' +
                             '       </a>' +
                             '       <div class="soliloquy-caption soliloquy-caption-bottom">' +
                             '           <div class="soliloquy-caption-inside">' +
                             '               <div class="soliloquy-fc-caption">' +
                             '                   <h2 class="soliloquy-fc-title">' +
                             '                       <a class="soliloquy-fc-title-link" href="/Recipes/Recipe?id=' + self.currentData[i]["recipeId"] + '" title="' + self.currentData[i]["recipeTitle"] + '">' + self.currentData[i]["recipeTitle"] + '</a>' +
                             '                   </h2>' +
                             '               </div>' +
                             '           </div>' +
                             '       </div>' +
                             '   </li>');

                        // add information to the bottom
                        $("#recentPosts").append(
                            '<article id="test0' + (i + 1) + '" class="entry">' +
                            '    <header class="entry-header">' +
                            '        <h2 class="entry-title">' +
                            '            <a href="/Recipes/Recipe?id=' + self.currentData[i]["recipeId"] + '">' + self.currentData[i]["recipeTitle"] + ', ' + self.currentData[i]["dateLoaded"] + '</a>' +
                            '        </h2>' +
                            '    </header>' +
                            '    <p class="entry-meta">' +
                            '        <span class="heart-this-wrap">' +
                            '            <a href="/Recipes/Recipe?id=' + self.currentData[i]["recipeId"] + '" class="heart-this">' +
                            '                <span>0</span>' +
                            '            </a>' +
                            '        </span>' +
                            '        <time class="entry-time">' + self.currentData[i]["dateLoaded"] + '</time>' +
                            '        <!--<span class="entry-categories">' +
                            '            <a href="#test02" rel="category tag">Моркови</a>' +
                            '        </span> -->                                                                                                                                                                                                           ' +
                            '    </p>' +
                            '    <a href="/Recipes/Recipe?id=' + self.currentData[i]["recipeId"] + '" class="grid-image alignleft">' +
                            '        <img width="600" height="882" src="' + currentFile["_downloadURL"] + '" class="entry-image attachment-post" alt="No image" itemprop="image">   ' +
                            '    </a>' +
                            '    <div class="entry-content">' +
                            '        <p>' +
                            '            ' + self.currentData[i]["recipe"] + '' +
                            '        </p>' +
                            '    </div>' +
                            '    <p>' +
                            '        <a class="more-link" href="/Recipes/Recipe?id=' + self.currentData[i]["recipeId"] + '">Продължи статията...</a>' +
                            '    </p>' +
                            '</article>');
                    }

                    setTimeout(function () {
                        if (window.location.hash.length > 0) {
                            $(window).scrollTop($(window.location.hash).offset().top);
                        }
                    }, 0);

                    // console.log(self.currentData);
                }
            });
    }
}