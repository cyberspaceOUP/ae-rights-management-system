app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {

    //Unblock ISBN
    $scope.unblockISBNset = function () {        
        var unblockISBN = AJService.PostDataToAPI("Scheduler/unblockISBNset", null);
        unblockISBN.then(function (status) {
            $scope.unblockISBNresult = status.data;
        }, function () {
            //   alert('Error');
        });
    }


});