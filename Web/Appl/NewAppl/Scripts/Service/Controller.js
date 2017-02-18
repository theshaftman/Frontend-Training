Controller = (function () {
    var self = this;
    var model = new ViewModel();

    self.init = function () {
        model.loadInformation();
    }

    return {
        init: self.init
    }
}());