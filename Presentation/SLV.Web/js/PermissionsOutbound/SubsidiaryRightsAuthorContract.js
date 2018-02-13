app.expandControllerSubsidiaryRightsAuthorContract = function ($scope, AJService, $window) {

    $scope.SubsidiaryRightsAuthorContract = function (Id) {
        
        if (Id != null) {

            var SubsidiaryRightsAuthorContractData = {
                Id: Id
            };
            // blockUI.start();
            // call API to fetch temp Department list basis on the FlatId
            //$('#hid_productid').val(Id);
            var SubsidiaryRightsAuthorContractStatus = AJService.PostDataToAPI('PermissionsOutbound/SubsidiaryRightsOutBound', SubsidiaryRightsAuthorContractData);
            SubsidiaryRightsAuthorContractStatus.then(function (msg) {
                $scope.SubsidiaryRightsAuthorContractDetails = msg.data;                
            }, function () {
                //alert('Error in getting Subsidiary Rights list');
            });
        }
    };

    $scope.calcTotalPer = function (SubsidiaryId) {
        var _ttl = 0;
        var oupPercentage = 0;
        for (var i = 0; i < $scope.SubsidiaryRightsAuthorContractDetails.length; i++) {
            if ($scope.SubsidiaryRightsAuthorContractDetails[i].SubsidiaryId == SubsidiaryId) {
                _ttl = _ttl + $scope.SubsidiaryRightsAuthorContractDetails[i].AuthorPercentage
                oupPercentage = parseFloat($scope.SubsidiaryRightsAuthorContractDetails[i].Ouppercentage)
            }
        }
        return parseFloat(_ttl) + oupPercentage;
    }

}
