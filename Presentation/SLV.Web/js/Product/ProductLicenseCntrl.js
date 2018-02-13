app.expandControllerProductDetails = function ($scope, AJService, $window) {

    $scope.ProductSerach = function (Id) {

        if (Id != null) {
            var ProductData = {
                Id: Id
            };
            // call API to fetch temp Department list basis on the FlatId
            $('#hid_productid').val(Id);
            var ProductStatus = AJService.PostDataToAPI('ProductMaster/ProductDetails', ProductData);
            ProductStatus.then(function (msg) {
                $scope.ProductDetails = msg.data;
            }, function () {
                //alert('Error in getting department list');
            });
        }
    };
}


