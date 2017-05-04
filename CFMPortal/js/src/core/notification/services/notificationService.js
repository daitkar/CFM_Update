
angular.module('notification', [])
.service('notification', function ($q, $http, $timeout) {

    var data = this;
    data.collectData = function () {
        var defer = $q.defer();
        $http.get('../js/src/core/notification/assets/notification.json').then(
            function (data) {
                console.log("data", data);
                defer.resolve(data);
            }
            , function () {
                defer.reject('could not find someFile.json');
            });
        return defer.promise;
    };

    data.success = function (msg) {
        App.alert({
            container: $('#alert_container'),
            place: "Append",
            type: "success",
            message: msg,
            close: true,
            reset: true,
            focus: true,
            closeInSeconds: 5,
            alertType: "Success",
            icon: 'fa-lg fa fa-check'
        });
    };

    data.danger = function (msg) {
        App.alert({
            container: $('#alert_container'),
            place: "Append",
            type: "danger",
            message: msg,
            close: true,
            reset: true,
            focus: true,
            closeInSeconds: 5,
            alertType: "Danger",
            icon: 'fa-lg fa fa-danger '
        });
    };

    data.warning = function (msg) {
        App.alert({
            container: $('#alert_container'),
            place: "Append",
            type: "warning",
            message: msg,
            close: true,
            reset: true,
            focus: true,
            closeInSeconds: 5,
            alertType: "Warning",
            icon: 'fa-lg fa fa-warning'
        });
    };

    data.info = function (msg) {
        App.alert({
            container: $('#alert_container'),
            place: "Append",
            type: "info",
            message: msg,
            close: true,
            reset: true,
            focus: true,
            closeInSeconds: 5,
            alertType: "Info",
            icon: 'fa-lg fa fa-user'
        });
    };
   
});
