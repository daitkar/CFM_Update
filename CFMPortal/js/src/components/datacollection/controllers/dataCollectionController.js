angular.module('dataCollection', [])
    .controller('DataCollectionCtrl', ['$scope', '$rootScope', 'MenuService', function ($scope, $rootScope, MenuService) {
    var menu = {
        "Id": 2,
        "Name": "Data Collection",
        "location": "Sidebar",
        "position": 1,
        "description": "This data will be pulled through the translate service on display",
        "hint": "To be pulled through the translation service",
        "iconClass": "icon-settings",
        "url": "/datacollection",
        "subMenus": [
                        {
                            "Name": "Project1",
                            "url": "/dashbaord/project1",
                            "iconClass": "icon-settings",
                            "position": 0,
                            "subMenus": [
                                    {
                                        "Name": "Data Sources",
                                        "url": "/dashbaord/project1/datasources",
                                        "iconClass": "icon-puzzle",
                                        "position": 1
                                    },
                                    {
                                        "Name": "Project DB",
                                        "url": "/dashbaord/project1/projectdb",
                                        "iconClass": "icon-puzzle",
                                        "position": 0
                                    },
                                    {
                                        "Name": "Questionnaires",
                                        "url": "/dashbaord/project1/questionnaires",
                                        "iconClass": "icon-puzzle",
                                        "position": 3
                                    },
                                    {
                                        "Name": "Direct Widget",
                                        "url": "/dashbaord/project1/directwidget",
                                        "iconClass": "icon-puzzle",
                                        "position": 2
                                    },
                                    {
                                        "Name": "Analysis",
                                        "url": "/dashbaord/project1/analysis",
                                        "iconClass": "icon-puzzle",
                                        "position": 4
                                    }
                            ]

                        },
                        {
                            "Name": "Project2",
                            "Url": "/dashbaord/project2",
                            "iconClass": "icon-settings",
                            "position": 1
                        }
        ],
        "securityRole": ""
    }
    $scope.data = MenuService.registerMenuData(menu);
}]);