app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {
    

    $scope.PendingAuthorContractRequest = function () {
      
        var _Flag = {
            
            Flag: "Alert",
            ExecutiveId: parseInt($("#enterdBy").val())
           
        };

        var PendingAuthorContractRequest = AJService.PostDataToAPI("Alert/PendingAuthorContractRequestList", _Flag);
        PendingAuthorContractRequest.then(function (status) {
            $scope.PendingAuthorContractRequestList = status.data;
        }, function () {
         //   alert('Error in getting Pending Author Contract list');
        });
    }

    
    $scope.ListProductLicensePendingRequest = function () {
      

        var GetProductList = AJService.PostDataToAPI("Alert/ProductLicensePendingRequest", null);
        GetProductList.then(function (Product) {
            $scope.ProductLicense = Product.data;
        }, function () {
           // alert('Error in getting productAuthor Licence');
        });
    }


    $scope.getProduct_ISBN_enteredList = function () {
        var _Flag = {

            Flag: "Alert",
            ExecutiveId: parseInt($("#enterdBy").val())
        };
        var getProduct_ISBN_enteredList = AJService.PostDataToAPI("Alert/Product_ISBN_enteredList", _Flag);
        getProduct_ISBN_enteredList.then(function (mdt) {
            //$scope.Product_ISBN_Not_enteredList = mdt.data;
        }, function () {
         //   alert('Error in getting Product ISBN list');
        });
      
    }


    $scope.getProduct_SAP_Agr_No_Not_EnteredList = function () {
        var _Flag = {

            Flag: "Alert",
            ExecutiveId: parseInt($("#enterdBy").val())
        };
        var getProduct_SAP_Agr_No_Not_EnteredList = AJService.PostDataToAPI("Alert/Product_SAP_Agr_No_Not_Entered", _Flag);
        getProduct_SAP_Agr_No_Not_EnteredList.then(function (mdt) {
           // $scope.Product_SAP_Agr_No_Not_EnteredList = mdt.data;
        }, function () {
            //alert('Error in getting Product SAP Agreement list');
        });
    }


    $scope.ListRightSaleContractExpiring = function () {
        var _Flag = {

            Flag: "Alert",
            ExecutiveId: parseInt($("#enterdBy").val())
        };
        var ListRightSaleContractExpiring = AJService.PostDataToAPI("Alert/RightSaleContractExpiring", _Flag);
        ListRightSaleContractExpiring.then(function (status) {
            //$scope.RightSaleContractExpiring_List = status.data;
        }, function () {
          //  alert('Error in Right Sale Contract Expiring Within 3 Months list');
        });
    }


    $scope.getListAuthorContractContractExpiryDate = function () {
       
        var getListAuthorContractContractExpiryDate = AJService.PostDataToAPI("Alert/AuthorContractContractExpiryDate", null);
        getListAuthorContractContractExpiryDate.then(function (status) {
            //$scope.RightSaleContractExpiring_List = status.data;
        }, function () {
            //  alert('Error in Right Sale Contract Expiring Within 3 Months list');
        });
    }


    $scope.getListProductlicenseExpiryDateExpiryDate = function () {

        var getListProductlicenseExpiryDateExpiryDate = AJService.PostDataToAPI("Alert/ProductlicenseExpiryDateExpiryDate", null);
        getListProductlicenseExpiryDateExpiryDate.then(function (status) {
            //$scope.RightSaleContractExpiring_List = status.data;
        }, function () {
            //  alert('Error in Right Sale Contract Expiring Within 3 Months list');
        });
    }


    $scope.getListContractAddendumExpiryDate = function () {

        var getListContractAddendumExpiryDate = AJService.PostDataToAPI("Alert/ContractAddendumExpiryDate", null);
        getListContractAddendumExpiryDate.then(function (status) {
            //$scope.RightSaleContractExpiring_List = status.data;
        }, function () {
            //  alert('Error in Right Sale Contract Expiring Within 3 Months list');
        });
    }


    $scope.getListProductLicenseAddendumExpiryDate = function () {

        var getListProductLicenseAddendumExpiryDate = AJService.PostDataToAPI("Alert/ProductLicenseAddendumExpiryDate", null);
        getListProductLicenseAddendumExpiryDate.then(function (status) {
            //$scope.RightSaleContractExpiring_List = status.data;
        }, function () {
            //  alert('Error in Right Sale Contract Expiring Within 3 Months list');
        });
    }

    $scope.getListISBNleft = function () {

        var getListISBNleft = AJService.PostDataToAPI("Alert/ISBNleft", null);
        getListISBNleft.then(function (status) {
            //$scope.RightSaleContractExpiring_List = status.data;
        }, function () {
            //  alert('Error in Right Sale Contract Expiring Within 3 Months list');
        });
    }

    $scope.getContributorEntered = function () {

        var getContributorEntered = AJService.PostDataToAPI("Alert/ContributorEntered", null);
        getContributorEntered.then(function (status) {
            //$scope.RightSaleContractExpiring_List = status.data;
        }, function () {
            //  alert('Error in Right Sale Contract Expiring Within 3 Months list');
        });
    }


    $scope.getProductLicenseEntered = function () {

        var getProductLicenseEntered = AJService.PostDataToAPI("Alert/ProductLicenseEntered", null);
        getProductLicenseEntered.then(function (status) {
            //$scope.RightSaleContractExpiring_List = status.data;
        }, function () {
            //  alert('Error in Right Sale Contract Expiring Within 3 Months list');
        });
    }



  


    $scope.ListPendingRequestOtherContract = function () {
        var _Flag = {

            Flag: "Alert"
        };
        var GetPendingRequest = AJService.PostDataToAPI("Alert/PendingRequestOtherContract", _Flag);
        GetPendingRequest.then(function (PendingRequest) {
            $scope.PendingRequestList = PendingRequest.data;
        }, function () {
          //  alert('Error in getting productAuthor Licence');
        });
    }


    $scope.getOtherContractExpiryDate = function () {

        var getOtherContractExpiryDate = AJService.PostDataToAPI("Alert/OtherContractExpiryDate", null);
        getOtherContractExpiryDate.then(function (status) {
         
        }, function () {
          
        });
    }



    $scope.getBalanceQuantityAddendum = function () {
        var _Flag = {

            Flag: "Alert"
        };
        var getBalanceQuantityAddendum = AJService.PostDataToAPI("Alert/BalanceQuantityAddendum", _Flag);
        getBalanceQuantityAddendum.then(function (status) {

        }, function () {

        });
    }



    $scope.ListInboundPermissionNotEntered = function () {
        var _Flag = {

            Flag: "Alert",
            ExecutiveId: parseInt($("#enterdBy").val())
        };

        var ListInboundPermissionNotEntered = AJService.PostDataToAPI("Alert/InboundPermissionNotEntered", _Flag);
        ListInboundPermissionNotEntered.then(function (status) {
          
        }, function () {
         
        });
    }



    $scope.ListBalanceQuantityProductLicense = function () {
        var _Flag = {

            Flag: "Alert",
            ExecutiveId: parseInt($("#enterdBy").val())
        };

        var ListBalanceQuantityProductLicense = AJService.PostDataToAPI("Alert/BalanceQuantityProductLicense", _Flag);
        ListBalanceQuantityProductLicense.then(function (status) {

        }, function () {

        });
    }


    $scope.getPermissionOutboundExpiryDate = function () {

        var getPermissionOutboundExpiryDate = AJService.PostDataToAPI("Alert/PermissionOutboundExpiryDate", null);
        getPermissionOutboundExpiryDate.then(function (status) {

        }, function () {

        });
    }

    $scope.getPermissionInboundExpiryDate = function () {

        var getPermissionInboundExpiryDate = AJService.PostDataToAPI("Alert/PermissionInboundExpiryDate", null);
        getPermissionInboundExpiryDate.then(function (status) {

        }, function () {

        });
    }


    $scope.getRecurringExpiryDate = function () {

        var getRecurringExpiryDate = AJService.PostDataToAPI("Alert/RecurringExpiryDate", null);
        getRecurringExpiryDate.then(function (status) {

        }, function () {

        });
    }


    $scope.getPermissionOutboundReceivedInvoiceDate = function () {

        var getPermissionOutboundReceivedInvoiceDate = AJService.PostDataToAPI("Alert/PermissionOutboundReceivedInvoiceDate", null);
        getPermissionOutboundReceivedInvoiceDate.then(function (status) {

        }, function () {

        });
    }

    $scope.getPermissionInboundBalanceQuantity = function () {

        var getPermissionInboundBalanceQuantity = AJService.PostDataToAPI("Alert/PermissionInboundBalanceQuantity", null);
        getPermissionInboundBalanceQuantity.then(function (status) {

        }, function () {

        });
    }




    $scope.getRightsSellingAdvancePayment = function () {

        var getRightsSellingAdvancePayment = AJService.PostDataToAPI("Alert/RightsSellingAdvancePayment", null);
        getRightsSellingAdvancePayment.then(function (status) {

        }, function () {

        });
    }



    $scope.getRightsSellingAlertFrequency = function () {

        var getRightsSellingAlertFrequency = AJService.PostDataToAPI("Alert/RightsSellingAlertFrequency", null);
        getRightsSellingAlertFrequency.then(function (status) {

        }, function () {

        });
    }


    $scope.getTestMail = function () {

        var getTestMail = AJService.PostDataToAPI("Alert/TestMail", null);
        getTestMail.then(function (status) {

        }, function () {

        });
    }


});

