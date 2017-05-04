var cfmApp = angular.module('cfmPortal', ['core',
    'components',
    'ngRoute',
    "ui.router",
    "ui.bootstrap",
    "oc.lazyLoad",
    "ngSanitize"
])
cfmApp.config(['$ocLazyLoadProvider', function ($ocLazyLoadProvider) {
    $ocLazyLoadProvider.config({
        // global configs go here
        // Changes
    });
}]);
cfmApp.run(["$rootScope", "settings", "$state", function ($rootScope, settings, $state) {
    $rootScope.$state = $state; // state to be accessed from view
    $rootScope.$settings = settings; // state to be accessed from view
}]);