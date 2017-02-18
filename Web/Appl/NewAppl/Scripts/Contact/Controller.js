Controller.Contact = (function () {
    var self = this;
    var model = new ViewModel.Contact();

    self.init = function () {
        model.loadInformation();
    }

    return {
        init: self.init
    }
}());