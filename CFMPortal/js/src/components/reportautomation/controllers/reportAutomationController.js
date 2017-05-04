angular.module('reportAutomation', [])
    .controller('ReportAutomationController', ['$scope', '$rootScope', 'MenuService', function ($scope, $rootScope, MenuService) {
    var menu = {
        "Id": 5,
        "Name": "Report Automation",
        "location": "Sidebar",
        "position": 5,
        "description": "This data will be pulled through the translate service on display",
        "hint": "To be pulled through the translation service",
        "iconClass": "icon-wrench",
        "url": "/reportautomation",
        "subMenus": [
                        {
                            "Name": "Triggers",
                            "Url": "/reportautomation/triggers",
                            "iconClass": "icon-puzzle",
                            "position": 0
                        },
                         {
                             "Name": "Push reports",
                             "Url": "/reportautomation/pushreports",
                             "iconClass": "icon-calendar",
                             "position": 1
                         }
        ],
        "securityRole": ""
    }
    $scope.data = MenuService.registerMenuData(menu);
}]);