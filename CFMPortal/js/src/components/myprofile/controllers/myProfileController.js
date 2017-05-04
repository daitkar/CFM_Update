angular.module('myProfile', [])
    .controller('MyProfileController', ['$scope', '$rootScope', 'MenuService', function ($scope, $rootScope, MenuService) {
    var menu = {
                                   "Id": 2,
                                   "Name": "My Profile",
                                   "location": "Topbar",
                                   "position": 0,
                                   "description": "This data will be pulled through the translate service on display",
                                   "hint": "To be pulled through the translation service",
                                   "iconClass": "icon-user",
                                   "url": "",
                                   "subMenus": "",
                                   "securityRole": ""
                }
    $scope.data = MenuService.registerMenuData(menu);
}]);