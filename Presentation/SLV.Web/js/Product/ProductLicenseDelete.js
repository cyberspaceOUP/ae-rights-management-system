app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {   
    app.expandControllerViewProductDetails($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);

    //Call on Button Submit
    $scope.productLicenseEntryForm = function (ProductModel) {
        $scope.submitted = true;
        if ($scope.userForm.$valid) {

            if ($scope.userForm.$valid) {
                $scope.productLicenseDelete(ProductModel);
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    }

    $scope.productLicenseDelete = function () {




            var mobj_LicenseDeleteDetails = {
                Id: $scope.ProductLicenseId,
                DeactivateRemarks: $scope.ProductModel.DeactivateRemarks,
                DeactivateDate: $scope.ProductModel.DeactivateDate,
                EnteredBy: $("#enterdBy").val(),
            }


            var ProductStatus = AJService.PostDataToAPI('ProductLicense/DeleteProductLicenseDetails', mobj_LicenseDeleteDetails);
 
        ProductStatus.then(function (msg) {

            if (msg.data.status == "Duplicate") {
                SweetAlert.swal("Error!", "Duplicate. already exist !", "", "error");
            }
            else if (msg.data.status != "OK") {
                SweetAlert.swal('There is some problem.', '', "Try agian");
            }
            else {
                SweetAlert.swal('Delete successfully.', '', "success");
                $("#hid_ProductId").val($scope.ProductLicenseId);

                $('form[name*=user]').attr("method", "post");
                $('form[name*=user]').submit();

                window.location.href = "../../Product/ProductLicense/ProductLicenseSearch?For=delete";
            }

        },
        function () {
            alert('There is some error in the system');
        });
    }

    $scope.SetDeactivateDate = function (datetext) {
        $scope.ProductModel.DeactivateDate = $(datetext).val();
    }

    /*For Update Case*/
    $scope.SetLicensorCopiesSentDate = function (datetext) {
        if ($scope.ProductModel.LicensorCopiesSentDate == undefined && $scope.ProductModel.LicensorCopiesSentDate !== $(datetext).val()) {
            $scope.ProductModel.LicensorCopiesSentDate = $(datetext).val();
        }
    }

    $scope.SetEFilesRequestDate = function (datetext) {
        if ($scope.ProductModel.EFilesRequestDate == undefined && $scope.ProductModel.EFilesRequestDate !== $(datetext).val()) {
            $scope.ProductModel.EFilesRequestDate = $(datetext).val();
        }
    }

    $scope.SetEFilesReceivedDate = function (datetext) {
        if ($scope.ProductModel.EFilesReceivedDate == undefined && $scope.ProductModel.EFilesReceivedDate !== $(datetext).val()) {
            $scope.ProductModel.EFilesReceivedDate = $(datetext).val();
        }
    }
    /*End Update Case*/


});