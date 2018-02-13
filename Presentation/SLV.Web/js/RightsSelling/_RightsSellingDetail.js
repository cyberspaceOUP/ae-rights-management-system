app.expandControllerRightsSellingLIst = function ($scope, AJService) {
    $scope.RightsSelling_Detail = function (Id) {
        if (Id != null) {
            var RightsSelling = {
                Id: Id
            };
            var RightsSellingDetail = AJService.PostDataToAPI('RightsSelling/RightsSelling_Detail', RightsSelling);
            RightsSellingDetail.then(function (msg) {
                $scope.RightsSellingDetailList = msg.data;
            }, function () {
                //alert('Error in getting Rights Selling Detail');
            });
        }
    };
}


