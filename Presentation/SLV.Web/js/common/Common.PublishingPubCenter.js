app.expandControllerPublishPubCenter = function ($scope, AJService, $window) {

    app.expandControllerPublishPubCenterA($scope, AJService, $window);
    angular.element(document.getElementById('angularid')).scope().getPublishingCompanyList();
    
}


app.expandControllerPublishPubCenterA = function ($scope, AJService, $window) {
    // Get Pub Center List by PublishingCompanyId
    $scope.getPubCenterByCompanyIdList = function () {
        var PublishingCompany = {
            Id: $scope.PublishPubCntrl.PublishingCompany,
        };
        var PubCenternList = AJService.PostDataToAPI("CommonList/PubCenterByCompanyIdList", PublishingCompany);
        PubCenternList.then(function (PubCenter) {
            $scope.PubCenterList = PubCenter.data;
        }, function () {
            alert('Error in getting Pub Center List');
        });
               
    }

    //Added by Prakash for Imprint List on 08/11/2016
    $scope.getImprintByCompanyIdList = function () {
        var PublishingCompany = {
            Id: $scope.PublishPubCntrl.PublishingCompany,
        };

        var getImprintList = AJService.PostDataToAPI("CommonList/GetImprintListByPublishingCompany", PublishingCompany);
        getImprintList.then(function (Imprint) {
            $scope.Imprint_List = Imprint.data; 
        }, function () {
            //alert('Error in getting Imprint List');
        });
    }
    
}