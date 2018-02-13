app.expandControllerProprietorDetails = function ($scope, AJService, $window) {

    /*Expand PublishPubCenter Controller*/
    app.expandControllerPublishPubCenter($scope, AJService, $window);
    /*Expand AuthorSuggestion Controller*/
    app.expandControllerAuthorSuggestion($scope, AJService, $window);

    //angular.element(document.getElementById('angularid')).scope().getImprintList();
    angular.element(document.getElementById('angularid')).scope().getImprintListForProprietorDetails();

    angular.element(document.getElementById('angularid')).scope().getLanguageList();
    angular.element(document.getElementById('angularid')).scope().getCurrencyList();

    

}


