app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {

    //app.expandControllerA($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);
    /*
  
 Here is the service used for Get Licensee List in Excel

 */

    $scope.getAllLicensee = function () {
        debugger;
        var getList = AJService.GetDataFromAPI("LicenseeMaster/GetLicenseeList", null);
        getList.then(function (Licensee) {
            $scope.LicenseeList = Licensee.data;
        }, function () {
            //alert('Error in getting Licensee List');
        });
    }

    $scope.LicenseeListExcel = function () {
        document.location = GlobalredirectPath + "List/List/exportToExcelLicenseeList";
    }

});