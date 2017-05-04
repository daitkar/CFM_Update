angular.module('logout', [])
    .controller('LogoutController', ['$scope', '$rootScope', 'MenuService', function ($scope, $rootScope, MenuService) {
    var menu = {
        "Id": 2,
        "Name": "Logout",
        "location": "Topbar",
        "position": 3,
        "description": "This data will be pulled through the translate service on display",
        "hint": "To be pulled through the translation service",
        "iconClass": "icon-key",
        "url": "",
        "subMenus": [
                        {
                            "Name": "Logout now",
                            "Url": "",
                            "iconClass": "icon-key",
                            "position": 0
                        },
                        {
                            "Name": "Logout later",
                            "Url": "",
                            "iconClass": "icon-key",
                            "position": 1
                        }
        ],
        "securityRole": ""
    }
    $scope.data = MenuService.registerMenuData(menu);
}]);