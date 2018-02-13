app.expandControllerDivisionSubDivision = function ($scope, AJService, $window) {
    angular.element(document.getElementById('angularid')).scope().getDivisionList();

    // Get Sub Division by DivisionId List
    $scope.getSubDivisionbyDivisionIdList = function () {
        var DivisionModel = {
            Id: $scope.DivSubDivCntrl.Division,
        };
        var SubDivisionList = AJService.PostDataToAPI("CommonList/SubDivisionListByDivisionId", DivisionModel);
        SubDivisionList.then(function (SubDivisionList) {
            $scope.SubDivisionList = SubDivisionList.data.divisionData;
        }, function () {
            alert('Error in getting SubDivision List');
        });

        var SeriesModel = {
            divisionid: $scope.DivSubDivCntrl.Division,
        };
        var SeriesListByDivisionId = AJService.PostDataToAPI("CommonList/SeriesListbyDivisionSubDivisionId", SeriesModel);
        SeriesListByDivisionId.then(function (SeriesList) {
            $scope.SeriesList = SeriesList.data.SeriesData;
        }, function () {
            //alert('Error in getting Series List');
        });
    }


    // Get Sub Division by DivisionId List
    $scope.getSeriesbySubDivisionIdList = function () {
        var Series = {
            divisionid: $scope.DivSubDivCntrl.Division,
            Subdivisionid: $scope.DivSubDivCntrl.SubDivision,
        };
        var SeriesListByDivisionId = AJService.PostDataToAPI("CommonList/SeriesListbyDivisionSubDivisionId", Series);
        SeriesListByDivisionId.then(function (SeriesList) {
            $scope.SeriesList = SeriesList.data.SeriesData;
        }, function () {
            //alert('Error in getting Series List');
        });
    }
}