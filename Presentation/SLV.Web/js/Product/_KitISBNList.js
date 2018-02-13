app.expandControllerKitISBNLIst = function ($scope, AJService) {

    $scope.KitISBNList = function (Id, Flag) {
        if (Id != null) {
            var KitISBN = {
                Id: Id,
                Flag: Flag
            };
            var KitISBNDetails = AJService.PostDataToAPI('ProductMaster/KitISBNDetails', KitISBN);
            KitISBNDetails.then(function (msg) {
                $scope.KitISBNDetailsList = msg.data;
                $scope.productDetailsByKit = false;
            }, function () {
                //alert('Error in getting Kit ISBN list');
            });
        }
    };

    //Kit ISBN Detail List // By ProductId // Call from other pages
    $scope.KitISBNDetailsList = [];
    $scope.KitISBNDetailsList = function (Id) {
        if (Id != null) {
            var KitISBNDetails = AJService.PostDataToAPI('ProductMaster/KitISBNDetailsListByProductId?ProductId=' + Id, null);
            KitISBNDetails.then(function (msg) {
                $scope.KitISBNDetailsList = msg.data;
                $scope.productDetailsByKit = true;
            }, function () {
                //alert('Error in getting Kit ISBN list');
            });
        }
    };


}


