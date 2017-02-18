(function () {
    var currentWindow = window.location.pathname.length > 0 ?
        window.location.pathname.split("/").filter(function (a) { return a ? a.length > 0 : false; })[0] :
        [],
        currentWebId;

    switch (currentWindow) {
        case "Recipes":
            Controller.Recipes.init();
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
        default:
            Controller.Home.init();
            currentWebId = "home";
            break;
    }

    $("#menu-item-" + currentWebId).addClass("current-menu-item");
}());