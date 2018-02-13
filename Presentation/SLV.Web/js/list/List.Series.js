app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {

    //app.expandControllerA($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);
    /*
  
 Here is the service used for get Series List in Excel

 */

    $scope.GetSeriesList = function () {
        var GetSeries = AJService.GetDataFromAPI("SeriesMaster/GetSeriesByDivisionSubdivision");
        GetSeries.then(function (Series) {
            $scope.SeriesData = Series.data;
        }, function () {
            //alert('Error in getting Series list');
        });
    }

    $scope.SeriesListExcel = function () {
        document.location = GlobalredirectPath + "List/List/exportToExcelSeriesList";
    }

});