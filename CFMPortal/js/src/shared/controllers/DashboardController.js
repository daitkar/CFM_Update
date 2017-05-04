cfmApp.controller('DashboardController', ['$scope', '$rootScope', 'notification', function ($scope, $rootScope, notification) {
    $scope.$on('$viewContentLoaded', function () {
        // initialize core components
        App.initAjax();
    });

    // notification.success("success message");
    // notification.danger("danger message");
    //notification.warning("warning message!!!!");
    $scope.$on('popups', function (event, data) {
        notification.info(data);
    })
    $scope.externalFun = function () {
        var addition = add(3, 4);
        console.log("addition", addition);
    }
    $scope.externalFun();
    //SidebarService.updateMenu();
    // set sidebar closed and body solid layout mode
    $rootScope.settings.layout.pageContentWhite = true;
    $rootScope.settings.layout.pageBodySolid = false;
    $rootScope.settings.layout.pageSidebarClosed = false;
}]);