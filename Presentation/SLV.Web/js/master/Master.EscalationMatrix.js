
app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {

    /*Expand common.master.js Controller*/

    app.expandControllerTopSearch($scope, AJService, $window);

    $scope.GetEscalationMatrixList = function () {
        var EscalationMatrixList = AJService.GetDataFromAPI('Master/getEscalationMatrixList');
        EscalationMatrixList.then(function (msg) {
            $scope.EscalationMatrixListDetailsList = msg.data;
        }, function () {
            alert('Error in getting Escalation Matrix List');
        });

    };

    $scope.GetEscalationMatrixList();


});


