app.expandControllerAuthorSuggestion = function ($scope, AJService, $window) {
    // Get Author Suggestion List

    $scope.AuthorList = [];
    $scope.getAuthorSuggesationList = function () {
        var Author = {
            FirstName: $scope.SuggestedAuthorName,
            Type: $scope.AuthorCategory,
           // AuthorId:$scope.AuthorId
        };
        if (typeof $scope.SuggestedAuthorName !== "undefined" && $scope.SuggestedAuthorName.length > 2) {
            var alertstatus = true;
            var AuthorSuggesationList = AJService.PostDataToAPI("CommonList/AuthorSuggesationList", Author);
            AuthorSuggesationList.then(function (AuthorSuggesationList) {
                $scope.AuthorSuggesationList = AuthorSuggesationList.data.AuthorSuggesation;
                $scope.Authortest = AuthorSuggesationList.data.AuthorSuggesation[0];

                if ($scope.AuthorSuggesationList.length ==   0) {
                     alert('No Author found for enter search character');
                }
       
            }, function () {
                alert('Error in getting Author Suggesation List');
            });
        }
        else {
             $scope.AuthorSuggesationList = null;
             alert('Please enter atleast 3 character to search author');
        }
    }

    $scope.showCheckedAuthor = function (auth, authorName) {
        if (authorName) {
            $scope.AuthorList.push(auth);
        }
        else {
            var index = $scope.AuthorList.indexOf(auth);
            $scope.AuthorList.splice(index, 1);
            $scope.AuthorName = false;
        }
    }

    $scope.CheckboxBind = function (AuthorId) {

        var index = $scope.AuthorList.map(function (item) {
            return item.AuthorId;
        }).indexOf(AuthorId);

        if (index > -1) {
            return true;
        }
        else {
            return false;
        }
    }
  
}
   





