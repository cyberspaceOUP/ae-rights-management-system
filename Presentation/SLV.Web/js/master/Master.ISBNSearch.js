
app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {
    app.expandControllerA($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);
    


    $scope.SerachISBN = function () {
        var SerachISBN = AJService.PostDataToAPI("ISBNMaster/ISBNSerch", null);
        SerachISBN.then(function (msg) {
            $scope.ISbnList = msg.data;
        }, function () {
            //alert('Error in getting SubDivision list');
        });
    }


    $scope.ISbnListExcel = function () {
        document.location = GlobalredirectPath + "Master/ISBNBag/exportToExcelISbnList";
    }





});




