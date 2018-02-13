app.expandControllerSubsidiaryRightsProductLicense = function ($scope, AJService, $window) {

    $scope.SubsidiaryRightsProductLicense = function (Id) {

        if (Id != null) {

            var SubsidiaryProductLicenseData = {
                Id: Id
            };
            // blockUI.start();
            // call API to fetch temp Department list basis on the FlatId
            //$('#hid_productid').val(Id);
            var SubsidiaryProductLicenseDataStatus = AJService.PostDataToAPI('PermissionsOutbound/SubsidiaryRightsLicense', SubsidiaryProductLicenseData);
            SubsidiaryProductLicenseDataStatus.then(function (msg) {
                $scope.SubsidiaryProductLicenseDataDetails = msg.data;
            }, function () {
                //alert('Error in getting Subsidiary Rights list');
            });
        }
    };

}