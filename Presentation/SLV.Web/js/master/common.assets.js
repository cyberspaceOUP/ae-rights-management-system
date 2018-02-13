
app.expandControllerAssets = function ($scope, AJService, $window, SweetAlert, blockUI) {

    // Get Assets List

    $scope.GetAssetsList = function () {
        var getAssetsList = AJService.GetDataFromAPI("Asset/getAllGlobalAssets", null);
        getAssetsList.then(function (asset) {
            $scope.assetsList = asset.data;
        }, function () {
            SweetAlert.swal("Oops..","Error in getting asset list","error");
        });
    }
}