app.controller("MainCtrl", function ($scope, AJService, $window) {
    //debugger;
    GetVisitorList();
    //get pending visitor list
    function GetVisitorList() {
        var getVisitorList = AJService.GetDataFromAPI("Visitor/getAllVisitors", null);
        getVisitorList.then(function (visitor) {
            $scope.VisitorEntry = visitor.data;
        }, function () {
            alert('Error in getting visitor list');
             
        });
    }

    $scope.UpdateVisitorDetail = function () {
        debugger;
        var getVisitor = AJService.GetDataFromAPI("Visitor/getAllVisitors", VisitorEntry.Id);

        getVisitor.then(function (visitor) {
            $scope.VisitorDetail = visitor.data;
            $scope.main.VName = VisitorDetail.VName;
        }, function () {
            alert('Error in getting visitor details');
        });

    }
});