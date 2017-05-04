/* Setup Layout Part - Sidebar */
cfmApp.controller('SidebarController', ['$state', '$scope', 'SidebarService', function ($state, $scope, SidebarService) {
    $scope.$on('$includeContentLoaded', function () {
        Layout.initSidebar($state); // init sidebar
    });
    function menuData() {
        $scope.menuData = SidebarService.getMenu();
    }
    menuData();
    //dummy code for testing purpose
    //$scope.addMenu = function () {
    //    var itemToAdd =
    //        {
    //            "Id": 8,
    //            "Name": "Exports & API",
    //            "location": "Sidebar",
    //            "position": 7,
    //            "description": "This data will be pulled through the translate service on display",
    //            "hint": "To be pulled through the translation service",
    //            "iconClass": "icon-user",
    //            "url": "/exportsandapi",
    //            "subMenus": "",
    //            "securityRole": ""
    //        }
    //    $scope.menuData.Menus[7] = itemToAdd;
    //}
    //$scope.removeMenu = function () {
    //    delete $scope.menuData.Menus[7];
    //}
    //dummy code for testing purpose
}]);