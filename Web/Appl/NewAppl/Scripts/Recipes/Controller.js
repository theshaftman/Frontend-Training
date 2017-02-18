Controller.Recipes = (function () {
    var self = this;
    var model = new ViewModel.Recipes();

    self.init = function () {
        model.loadInformation();
    }

    return {
        init: self.init
    }
}());