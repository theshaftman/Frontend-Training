Controller.Home = (function () {
    var self = this;
    var model = new ViewModel.Home();

    self.init = function () {
        model.loadPage();
    }

    return {
        init: self.init
    }
}());