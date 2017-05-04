angular.module('systemHelp', [])
    .controller('SystemHelpController', ['$scope', '$rootScope', 'MenuService', function ($scope, $rootScope, MenuService) {
    var menu = {
        "Id": 2,
        "Name": "System help",
        "location": "Quickbar",
        "position": 0,
        "description": "This data will be pulled through the translate service on display",
        "hint": "To be pulled through the translation service",
        "iconClass": "icon-question",
        "url": "",
        "subMenus": "",
        "securityRole": ""
    }
    $scope.data = MenuService.registerMenuData(menu);
}]);