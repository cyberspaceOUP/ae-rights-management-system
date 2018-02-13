/// <reference path="../../Scripts/angular/angular.js" />

app.controller("MainCtrl", function ($scope,$compile, $rootScope, AJService, $window) {

 
    $scope.renderUserNamePartialView = function ()
    {
        $.ajax({
            cache: false,
            type: "GET",
            contentType: 'application/html; charset=utf-8',
            url: "/Home/UserName",
            data: {},
            datatype: 'html',
            success: function (result) {
                if ($.trim(result) != "") {
                    $("#divUser").html($compile(result)($scope));
                }
                else {
                    $("#divUser").html('');
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    }

    $scope.renderSocietyPartialView= function () {
        $.ajax({
            cache: false,
            type: "GET",
            contentType: 'application/html; charset=utf-8',
            url: "/Home/SocietyName",
            data: {},
            datatype: 'html',
            success: function (result) {
                $("#divSocietyName").html(result);
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    }
    $scope.renderSwitchSocietyPartialView= function() {
        $.ajax({
            cache: false,
            type: "GET",
            contentType: 'application/html; charset=utf-8',
            url: "/Home/SwitchSociety",
            data: {},
            datatype: 'html',
            success: function (result) {
                $("#ulSwitchSociety").html(result);
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    }

});