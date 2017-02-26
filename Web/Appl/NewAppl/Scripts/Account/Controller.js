Controller.Account = (function () {
    var self = this;
    var model = new ViewModel.Account();

    self.init = function () {
        model.loadInformation();
    }

    return {
        init: self.init
    }
}());