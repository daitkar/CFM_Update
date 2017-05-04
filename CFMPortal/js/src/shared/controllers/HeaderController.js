/* Setup Layout Part - Header */
cfmApp.controller('HeaderController', ['$scope', '$location', function ($scope, $location) {
    console.log("HeaderController");
    $scope.$on('$includeContentLoaded', function () {
        Layout.initHeader(); // init header
    });

    $scope.logOut = function () {
    }
}]);