cfmApp.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    // Redirect any unmatched url
    if (typeof BlockMainRouting == 'undefined') BlockMainRouting = false;
    if (BlockMainRouting != true)
        $urlRouterProvider.otherwise("/dashboard.html");

    $stateProvider

        // Dashboard
        .state('dashboard', {
            url: "/dashboard.html",
            templateUrl: "/templates/MainPage.html",
            data: { pageTitle: 'Admin Dashboard Template' },
            controller: "DashboardController"
            //,resolve: {
            //    deps: ['$oclazyload', function ($oclazyload) {
            //        return $oclazyload.load({
            //            name: 'cfmapp',
            //            insertbefore: '#ng_load_plugins_before', // load the above css files before a link element with this id. dynamic css files must be loaded between core and theme css files
            //            files: [
            //            ]
            //        });
            //    }]
            //}
        })
        .state('intro', {
            url: "/intro.html",
            templateUrl: "/templates/Intro.html",
            data: { pageTitle: 'Introduction' },
            controller: "IntroController"
        })


}]);