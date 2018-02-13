
app.expandControllerProductKitIsbnDetilsList = function ($scope, AJService) {
    $scope.ProductKitIsbnListDetails = function (Id, Flag) {
        if (Id != null) {
            var KitISBN = {
                Id: Id,
                Flag: Flag
            };
            var ProductKitIsbnDetails = AJService.PostDataToAPI('ProductMaster/KitISBNDetails', KitISBN);
            ProductKitIsbnDetails.then(function (msg) {
                $scope.ProductKitIsbnDetailsList = msg.data;
            }, function () {
                //alert('Error in getting Product Kit ISBN list');
            });
        }
    };
}




