/// <reference path="angular.js" />

var app = angular.module('RedApp', []);

var controller = app.controller('RedController', function ($scope) {
    $scope.orderID = 0;
    $scope.totalServiceAmount = 0;
    $scope.BookingPayment = 0;
    $scope.CardDiscount = 0;
    $scope.CardDiscountAmount = 0;
    
    $scope.SpecialDiscount = 0;
    $scope.TotalDiscount = 0;   
    $scope.PayableAmount = 0;
    $scope.AdvancePayment = 0;
    $scope.Due = 0;

  

});