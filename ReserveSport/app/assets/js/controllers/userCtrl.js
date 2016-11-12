'use strict';
/** 
  * controller for User Profile Example
*/
app.controller('UserCtrl', ["$scope", "flowFactory", function ($scope, flowFactory) {
    $scope.removeImage = function () {
        $scope.noImage = true;
    };
    $scope.obj = new Flow();

    $scope.userInfo = {
        firstName: 'Heyssten',
        lastName: 'Manuel',
        url: 'www.example.com',
        email: 'manuel@example.com',
        phone: '994545619',
        gender: 'Masculino',
        zipCode: '51',
        city: 'Lima',
        avatar: 'assets/images/avatar-1-xl.jpg',
        twitter: '',
        github: '',
        facebook: '',
        linkedin: '',
        google: '',
        skype: 'netsshey'
    };
    if ($scope.userInfo.avatar == '') {
        $scope.noImage = true;
    }
}]);