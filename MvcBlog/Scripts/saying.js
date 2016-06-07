var saying = angular.module('saying', []);
saying.controller('HomeController', function ($scope, $http) {
   // debugger;
    $scope.saying = "";
    $http.get("/Home/getSaying")
    .success(function (result) {
       // debugger;
        $scope.saying = result[0];
    })
    .error(function (result) {
        console.log(result);
    });


});