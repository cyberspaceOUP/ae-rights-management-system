app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {

    //app.expandControllerA($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);
    /*
  
 Here is the service used for get Pub Center List in Excel

 */

    $scope.getAllPubCenterList = function () {
        debugger;
        var GetPubCenter = AJService.GetDataFromAPI("PubCenterMaster/GetPubCenterList");
        GetPubCenter.then(function (PubCenter) {
            $scope.PubCenterData = PubCenter.data;
        }, function () {
            //alert('Error in getting Pub Center list');
        });
    }

    $scope.PubCenterListExcel = function () {
        document.location = GlobalredirectPath + "List/List/exportToExcelPubCenterList";
    }

});