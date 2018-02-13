app.expandControllerPermissionsOutboundLIst = function ($scope, AJService) {
    $scope.PermissionsOutbound = function (Id) {
        if (Id != null) {
            var PermissionsOutbound = {
                Id: Id
            };
            debugger;
            var PermissionsOutboundDetails = AJService.PostDataToAPI('PermissionsOutbound/PermissionsOutbound_Detail', PermissionsOutbound);
            debugger;
            PermissionsOutboundDetails.then(function (msg) {
                $scope.PermissionsOutboundList = msg.data;

              
            }, function () {
                //alert('Error in getting Kit ISBN list');
            });
        }
    };
}


