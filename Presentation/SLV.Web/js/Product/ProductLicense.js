app.expandControllerProductLicense = function ($scope, AJService, $window) {

    $scope.ProductLicenseSerach = function (Id) {
       
        if (Id != null) {
            var ProductLicenseData = {
                ProductLicenseId: Id
            };
           // blockUI.start();
            // call API to fetch temp Department list basis on the FlatId
            //commented by dheeraj sharma  //Uncomment By Ankush
            $('#hid_productid').val(Id);
            var ProductLicenseStatus = AJService.PostDataToAPI('ProductMaster/ProductLicenseDetails', ProductLicenseData);
            ProductLicenseStatus.then(function (msg) {
                $scope.ProductLicenseDetails = msg.data;
            }, function () {
                //alert('Error in getting department list');
            });
        }
    };


    


}


