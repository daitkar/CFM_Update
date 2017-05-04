angular.module('lockScreen', [])
    .controller('LockScreenController', ['$scope', '$rootScope', 'MenuService', function ($scope, $rootScope, MenuService) {
    var menu = {
        "Id": 2,
        "Name": "Lock Screen",
        "location": "Topbar",
        "position": 3,
        "description": "This data will be pulled through the translate service on display",
        "hint": "To be pulled through the translation service",
        "iconClass": "icon-lock",
        "url": "",
        "subMenus": "",
        "securityRole": ""
    }
    $scope.data = MenuService.registerMenuData(menu);
}]);