'use strict';

angular.module('Authentication', [])
.factory('AuthenticationService',
    ['$http', '$cookieStore', '$rootScope',
    function ($http, $cookieStore, $rootScope) {
        var service = {};

        service.SetCredentials = function (username, isLogued) {
            $rootScope.globals = {
                currentUser: {
                    username: username,
                    isLogued: isLogued
                }
            };

            $cookieStore.put('globals', $rootScope.globals);
        };

        service.ClearCredentials = function () {
            $rootScope.globals = {};
            $cookieStore.remove('globals');
        };

        return service;
    }])
.controller('LoginCtrl', ['$scope', '$rootScope', '$location', '$http', 'AuthenticationService',
    function ($scope, $rootScope, $location, $http, AuthenticationService) {
        AuthenticationService.ClearCredentials();

        $scope.Login = function () {
            var result = DevExpress.validationEngine.validateGroup('ValGroup');
            if (result.isValid) {
                $('#loadContainer').dxLoadPanel('instance').show();
                var username = $('#dtbUserName').dxTextBox('instance').option('value');
                var password = $('#txtPassword').dxTextBox('instance').option('value');
                var objl = { COD_USUA: username, ALF_PASS: password };
                var pars = JSON.stringify(objl);

                $http({
                    method: 'POST', url: 'http://localhost:28665/loginusers', data: pars, headers: { 'Content-Type': 'application/json; charset=utf-8' }
                }).success(function (data) {
                    $('#loadContainer').dxLoadPanel('instance').hide();
                    if (data === 0) {
                        DevExpress.ui.notify('Acceso denegado, verificar usuario y contraseña.', 'error', 4000);
                    }
                    else {
                        AuthenticationService.SetCredentials(username, true);
                        $rootScope.user = {
                            name: username,
                            job: 'ng-Dev',
                            picture: 'app/img/user/02.jpg'
                        };
                        $location.path('/app/reservation');
                    }
                }).error(function (data) {
                    $('#loadContainer').dxLoadPanel('instance').hide();
                    DevExpress.ui.notify(data, 'error', 4000);
                });
            }
        };
    }]);