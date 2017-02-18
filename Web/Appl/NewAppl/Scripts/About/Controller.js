Controller.About = (function () {
    var self = this;
    var model = new ViewModel.About();

    self.init = function () {
        model.loadInformation();
    }

    return {
        init: self.init
    }
}());