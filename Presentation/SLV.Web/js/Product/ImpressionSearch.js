

app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {
 
    app.expandControllerTopSearch($scope, AJService, $window);

    $scope.ImpressionORList = [];
    $scope.ImpressionNOList = [];

    $scope.ShowSearchForm = true;

    $scope.submitForm = function (ImpressionSearch) {
        $scope.ImpressionORList = [];
        $scope.ImpressionNOList = [];
        $scope.submitted = true;
        if ($scope.userForm.$valid) {
            if ($scope.userForm.$valid) {
                $scope.impressionSearch(ImpressionSearch);
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    };

    $scope.impressionSearch = function (ImpressionSearch) {

        var productStatus = AJService.PostDataToAPI('ProductLicense/ImpressionSearch', ImpressionSearch);

        productStatus.then(function (msg) {
            blockUI.stop();
            if (msg.data.length != 0) {
                $scope.ShowSearchForm = false;

                if ($scope.ImpressionSearch.ProductCategory == "OR") {
                    $scope.ImpressionORList = msg.data;
                }
                else {
                    $scope.ImpressionNOList = msg.data;
                }
                
            }
            else {
                swal("No record", 'No record found', "warning");
            }
        }, function () {
            $scope.IsError = false;
        });
        blockUI.stop();
    }

    $scope.DisplayDiv = function (count) {
        $('.tooltip-toggle').css("display", "none");
        $('.tooltip-toggle').removeAttr("id");
        $($('.tooltip-toggle')[count - 1]).attr("id", "tooltip-toggle");
    }

    $scope.BackToserch = function () {
        //$scope.ShowSearchForm = true;
        window.location.href = window.location.href;
    }

});