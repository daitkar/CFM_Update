angular.module('myPortals', [])
    .controller('MyPortalsController', ['$scope', '$rootScope', 'MenuService', function ($scope, $rootScope, MenuService) {
    var menu = {
        "Id": 2,
        "Name": "My Portals",
        "location": "Topbar",
        "position": 2,
        "description": "This data will be pulled through the translate service on display",
        "hint": "To be pulled through the translation service",
        "iconClass": "icon-home",
        "url": "",
        "subMenus": "",
        "securityRole": ""
    }
    $scope.data = MenuService.registerMenuData(menu);
}]);