angular.module('home', [])
.controller('HomeController', ['$scope', '$rootScope', 'MenuService', function ($scope, $rootScope, MenuService) {
    var menu = {
        "Id": 1,
        "Name": "Home",
        "location": "Sidebar",
        "position": 1,
        "description": "This data will be pulled through the translate service on display",
        "hint": "To be pulled through the translation service",
        "iconClass": "icon-home",
        "url": "dashboard",
        "subMenus": "",
        "securityRole": ""
    }
    $scope.data = MenuService.registerMenuData(menu);
}]);