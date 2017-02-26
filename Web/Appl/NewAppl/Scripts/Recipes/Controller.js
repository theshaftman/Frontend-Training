Controller.Recipes = (function () {
    var self = this;
    var model = new ViewModel.Recipes();

    self.init = function () {
        model.loadInformation();
    }

    self.recipeInit = function () {
        model.loadRecipeInformation();
        model.loadComments();
        model.postComment();
    }

    return {
        init: self.init,
        recipeInit: self.recipeInit
    }
}());