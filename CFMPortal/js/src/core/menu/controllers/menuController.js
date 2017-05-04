angular.module('menu', [])
    .controller('MenuController', ['$scope', '$rootScope', '$state', 'MenuService', function ($scope, $rootScope, $state, MenuService) {
    $scope.$on('$includeContentLoaded', function () {
        //Layout.initHeader(); // init header
        Layout.initSidebar($state); // init sidebar 
    });
    $scope.textSearch = "";
    function menuData() {
        $scope.menuData = MenuService.getMenuData();
    }
    menuData();

    $scope.removeItem = function(item) {
        var index = $scope.menuData.indexOf(item);
        $scope.menuData.splice(index, 1);
    }

    $scope.addItem = function () {
        var indexToAdd = $scope.menuData.length;
        var obj = {
                    "Id": 1,
                    "Name": "Portal",
                    "location": "Quickbar",
                    "position": 0,
                    "description": "This data will be pulled through the translate service on display",
                    "hint": "To be pulled through the translation service",
                    "iconClass": "icon-home",
                    "url": "",
                    "subMenus": "",
                    "securityRole": ""
        }

        $scope.menuData.splice(indexToAdd, 0, obj);
    }
}]);