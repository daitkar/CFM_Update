cfmApp.controller('IntroController', ['$scope', '$rootScope', '$location', '$timeout', function ($scope, $rootScope, $location, $timeout) {
    // set sidebar closed and body solid layout mode
    $scope.$on('$viewContentLoaded', function () {
        $('#introduction').modal('show');
    });

    $scope.toMain = function () {
        $timeout(function () {
            $location.url('/#dashboard.html');
        }, 300);
    }
}]);