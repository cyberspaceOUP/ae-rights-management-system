app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {

    app.expandControllerProductViewDetails($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);


    $scope.btn_BackToList = function()
    {
        document.location = GlobalredirectPath +  "/Product/ProductMaster/ProductSearch?For=List";
    }

});