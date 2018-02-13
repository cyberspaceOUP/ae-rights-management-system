app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {

    //app.expandControllerA($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);
    /*
  
 Here is the service used for get Proprietor Company List in Excel

 */

    $scope.getAllPublishingCompany = function () {
        debugger;
        var getList = AJService.GetDataFromAPI("PublishingCompanyMaster/GetAllPublishingCompany", null);
        getList.then(function (PublishingCompany) {
            $scope.PublishingCompanyList = PublishingCompany.data;
        }, function () {
            //alert('Error in getting Proprietor Company List');
        });
    }

    $scope.PublishingCompanyListExcel = function () {
        document.location = GlobalredirectPath + "List/List/exportToExcelPublishingCompanyList";
    }

});