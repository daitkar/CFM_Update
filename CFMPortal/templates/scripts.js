
var currentLanguage = "nl";
(function () {
    "use strict";

    (function () {
        var app = angular.module("securityApp", ['pascalprecht.translate']);
        app.config(function ($translateProvider) {
            for (var lang in languages) {
                $translateProvider.translations(lang, languages[lang]);
            }
            $translateProvider.preferredLanguage(currentLanguage);
        });

        app.controller("securityCtrl", function ($scope, Model, $translate) {
            $scope.model = Model;
            $scope.languages = languages;
            $scope.currentLanguage = currentLanguage;

            $scope.$watch('model.username', function () { $scope.passwordForgottenLinkF() });

            $scope.passwordForgottenLinkF = function () {
                var el = $('#passwordForgottenLink');
                if ( $scope.model.username == '' || $scope.model.username == null  ) {
                    el.removeAttr('href');
                    $translate("enterUserNameFirst").then(function (enterUserNameFirst) { el.attr('title', enterUserNameFirst); });
                } else {
                    el.attr('href', "./plus/security/passwordforgotten?lang=" + $scope.currentLanguage + "&portal=" + $scope.model.custom + "&userName=" + $scope.model.username + "&loginPage=" + $scope.model.loginUrl);
                    $translate("clickToSetNewPwd").then(function (clickToSetNewPwd) { el.attr('title', clickToSetNewPwd) });
                    
                }
            };

            $scope.setLanguage = function (ln) {
                currentLanguage = ln;
                $scope.currentLanguage = currentLanguage;
                $translate.use(ln);
                $scope.passwordForgottenLinkF();
            }
        });

        app.directive("antiForgeryToken", function () {
            return {
                restrict: 'E',
                replace: true,
                scope: {
                    token: "="
                },
                template: "<input type='hidden' name='{{token.name}}' value='{{token.value}}'>"
            };
        });

        app.directive("focusIf", function ($timeout) {
            return {
                restrict: 'A',
                scope: {
                    focusIf: '='
                },
                link: function (scope, elem, attrs) {
                    if (scope.focusIf) {
                        $timeout(function () {
                            elem.focus();
                        }, 100);
                    }
                }
            };
        });
    })();

    (function () {
        var model = identityServer.getModel();
        angular.module("securityApp").constant("Model", model);
        if (model.autoRedirect && model.redirectUrl) {
            if (model.autoRedirectDelay < 0) {
                model.autoRedirectDelay = 0;
            }
            window.setTimeout(function () {
                window.location = model.redirectUrl;
            }, model.autoRedirectDelay * 1000);
        }
    })();

})();