app.expandControllerAuthorContractDetails = function ($scope, AJService, $window) {

    $scope.AuthorContract = function (Id) {
       
        if (Id != null) {
            var AuthorData = {
                AuthorContractId: Id
            };
           
            var AuthorStatus = AJService.PostDataToAPI('AuthorContact/AuthorContractDetails', AuthorData);
            AuthorStatus.then(function (msg) {
                $scope.AuthorDetails = msg.data;
            }, function () {
                //alert('Error in getting department list');
            });
        }
    };


   


}


