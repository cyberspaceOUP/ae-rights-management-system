app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {

    //app.expandControllerA($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);
    /*
  
 Here is the service used for get Copyright Holder List in Excel

 */

    $scope.getAllCopyrightHolder = function () {
        debugger;
        var getList = AJService.GetDataFromAPI("CopyrightHolderMaster/GetCopyrightHolderList", null);
        getList.then(function (CopyrightHolder) {
            $scope.CopyrightHolderList = CopyrightHolder.data;
        }, function () {
            //alert('Error in getting Copyright Holder List');
        });
    }

    $scope.CopyrightHolderListExcel = function () {
        document.location = GlobalredirectPath + "List/List/exportToExcelCopyrightHolderList";
    }

});