app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {

    $scope.getProductLicensesList = function () {
        var getProductLicensesList = AJService.PostDataToAPI("Dashboard/ProductLicenses", null);
        getProductLicensesList.then(function (mdt) {
            $scope.ProductLicensesList = mdt.data;
        }, function () {
            //alert('Error in getting Product Licenses list');
        });
    }
});


