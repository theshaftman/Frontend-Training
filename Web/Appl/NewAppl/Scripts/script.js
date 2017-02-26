(function () {
    var currentWindow = window.location.pathname.length > 0 ?
        window.location.pathname.split("/").filter(function (a) { return a ? a.length > 0 : false; })[0] :
        [],
        currentWebId;

    switch (currentWindow) {
        case "Recipes":
            if (window.location.pathname.length > 0 &&
                    window.location.pathname.split("/").filter(function (a) { return a ? a.length > 0 : false; }).length > 1) {
                Controller.Recipes.recipeInit();
            } else {
                Controller.Recipes.init();
            }
            currentWebId = "recipes";
            break;
        case "About":
            Controller.About.init();
            currentWebId = "about";
            break;
        case "Contact":
            Controller.Contact.init();
            currentWebId = "contact";
            break;
        case "Account":
            Controller.Account.init();
            currentWebId = "account";
            break;
        default:
            Controller.Home.init();
            currentWebId = "home";
            break;
    }

    $("#menu-item-" + currentWebId).addClass("current-menu-item");
}());