Controller.Home = (function () {
    var self = this;
    var model = new ViewModel.Home();

    self.init = function () {
        model.loadInformation();
    }

    return {
        init: self.init
    }
}());