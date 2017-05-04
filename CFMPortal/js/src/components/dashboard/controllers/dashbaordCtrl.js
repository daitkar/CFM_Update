angular.module('dashboard', [])
    .controller('DashbaordCtrl', ['$scope', '$rootScope', 'MenuService', function ($scope, $rootScope, MenuService) {
    var menu = {
        "Id": 4,
        "Name": "Dashbaords",
        "location": "Sidebar",
        "position": 2,
        "description": "This data will be pulled through the translate service on display",
        "hint": "To be pulled through the translation service",
        "iconClass": "icon-wrench",
        "url": "/dashbaords",
        "subMenus": [
                        {
                            "Name": "Dashboard 1",
                            "Url": "/dashbaords/dashbaord1",
                            "iconClass": "icon-puzzle",
                            "position": 0
                        },
                        {
                            "Name": "Dashboard 2",
                            "Url": "/dashbaords/dashboard2",
                            "iconClass": "icon-calendar",
                            "position": 1
                        }
        ],
        "securityRole": ""
    }
    $scope.data = MenuService.registerMenuData(menu);
}]);