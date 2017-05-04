cfmApp.service('SidebarService', [function () {
    return {
        getMenu: function () {
            var menu =
               {
                   "Menus": [
                               {
                                   "Id": 1,
                                   "Name": "Home",
                                   "location": "Sidebar",
                                   "position": 0,
                                   "description": "This data will be pulled through the translate service on display",
                                   "hint": "To be pulled through the translation service",
                                   "iconClass": "icon-home",
                                   "url": "dashboard",
                                   "subMenus": "",
                                   "securityRole": ""
                               },
                               {
                                   "Id": 2,
                                   "Name": "Dashbaord",
                                   "location": "Sidebar",
                                   "position": 4,
                                   "description": "This data will be pulled through the translate service on display",
                                   "hint": "To be pulled through the translation service",
                                   "iconClass": "icon-settings",
                                   "url": "/dashbaord",
                                   "subMenus": [
                                                   {
                                                       "Name": "Project1",
                                                       "url": "/dashbaord/project1",
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
                                                       "position": 1
                                                   }
                                   ],
                                   "securityRole": ""
                               },
                               {
                                   "Id": 3,
                                   "Name": "Closed Loop Feedback",
                                   "location": "Sidebar",
                                   "position": 3,
                                   "description": "This data will be pulled through the translate service on display",
                                   "hint": "To be pulled through the translation service",
                                   "iconClass": "icon-user",
                                   "url": "/closedloopfeedback",
                                   "subMenus": "",
                                   "securityRole": ""
                               },
                               {
                                   "Id": 4,
                                   "Name": "Dashbaords",
                                   "location": "Sidebar",
                                   "position": 2,
                                   "description": "This data will be pulled through the translate service on display",
                                   "hint": "To be pulled through the translation service",
                                   "iconClass": "icon-wrench",
                                   "url": "/dashbaords",
                                   "subMenus": [
                                                   {
                                                       "Name": "Dashboard 1",
                                                       "Url": "/dashbaords/dashbaord1",
                                                       "iconClass": "icon-puzzle",
                                                       "position": 0
                                                   },
                                                   {
                                                       "Name": "Dashboard 2",
                                                       "Url": "/dashbaords/dashboard2",
                                                       "iconClass": "icon-calendar",
                                                       "position": 1
                                                   }
                                   ],
                                   "securityRole": ""
                               },
                               {
                                   "Id": 5,
                                   "Name": "Report Automation",
                                   "location": "Sidebar",
                                   "position": 1,
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
                               },
                               {
                                   "Id": 6,
                                   "Name": "Marketing tools",
                                   "location": "Sidebar",
                                   "position": 5,
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
                               },
					        {
					            "Id": 7,
					            "Name": "Analist Research Manager",
					            "location": "Sidebar",
					            "position": 6,
					            "description": "This data will be pulled through the translate service on display",
					            "hint": "To be pulled through the translation service",
					            "iconClass": "icon-user",
					            "url": "/analistreasearchmanager",
					            "subMenus": "",
					            "securityRole": ""
					        },
                   ]
               }
            return menu;
        }
    }
}]);