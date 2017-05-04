angular.module('marketing', [])
    .controller('MarketingController', ['$scope', '$rootScope', 'MenuService', function ($scope, $rootScope, MenuService) {
    var menu = {
        "Id": 6,
        "Name": "Marketing tools",
        "location": "Sidebar",
        "position": 4,
        "description": "This data will be pulled through the translate service on display",
        "hint": "To be pulled through the translation service",
        "iconClass": "icon-wrench",
        "url": "/marketingtools",
        "subMenus": [
                            {
                                "Name": "Review integrations",
                                "Url": "/marketingtools/reviewintegrations",
                                "iconClass": "icon-puzzle",
                                "position": 0
                            },
                            {
                                "Name": "Marketing widgets",
                                "Url": "/marketingtools/marketingwidgets",
                                "iconClass": "icon-puzzle",
                                "position": 1
                            }
        ],
        "securityRole": ""
    }
    $scope.data = MenuService.registerMenuData(menu);
}]);