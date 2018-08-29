app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {
    // Get list for which SAP agreement number not entered 
    app.expandControllerTopSearch($scope, AJService, $window);
    //app.AuthorContractView($scope, AJService, $window);
$scope.ProductDetails = [];
$scope.ProductLicense = [];

    $scope.GetNotEnteredSAPAgreementNoList = function () {
        var GetSAPArgList = AJService.GetDataFromAPI("Dashboard/getNotEnteredSAPAgreementNoList", null);
        GetSAPArgList.then(function (SAP) {
            $scope.NotEnteredSAPAgrNoList = SAP.data;
        }, function () {
            //alert('Error in getting not entered sap agrument number list');
        });
    }


    // Get product list for isbn not entered 
    $scope.GetNotEnteredISBNForProductList = function () {
        var GetProductList = AJService.GetDataFromAPI("Dashboard/getNotEnteredISBNForProductList", null);
        GetProductList.then(function (Product) {
            $scope.ProductListForNotEnteredISBN = Product.data;
        }, function () {
            //alert('Error in getting product list in which isbn not entered');
        });
    }

    // Get product list for isbn not entered 
    $scope.AuthorContractNotEntered = function () {
        var GetProductList = AJService.GetDataFromAPI("Dashboard/AuthorContractProduct?ExecutiveId=" + parseInt($("#enterdBy").val()), null);
        GetProductList.then(function (Product) {
            $scope.ProductAuthorContractNotEntered = Product.data;
        }, function () {
            //alert('Error in getting productAuthor Contract');
        });
    }

    // Get product list for isbn not entered 
    $scope.ListProductLicenseNotEntered = function () {
        var GetProductList = AJService.GetDataFromAPI("Dashboard/ProductLicenseNotEntered?ExecutiveId=" + parseInt($("#enterdBy").val()), null);
        GetProductList.then(function (Product) {
            $scope.ProductLicense = Product.data;
        }, function () {
            //alert('Error in getting productAuthor Licence');
        });
    }

    

    //Added By Saddam on 23/06/2016
    $scope.ListPendingRequestOtherContract = function () {
        var GetPendingRequest = AJService.GetDataFromAPI("Dashboard/PendingRequestOtherContract", null);
        GetPendingRequest.then(function (PendingRequest) {
            $scope.PendingRequestList = PendingRequest.data;
        }, function () {
            //alert('Error in getting productAuthor Licence');
        });
    }


    $scope.getProductLicensesList = function () {
        var getProductLicensesList = AJService.PostDataToAPI("Dashboard/ProductLicenses", null);
        getProductLicensesList.then(function (mdt) {
            $scope.ProductLicensesList = mdt.data;
        }, function () {
            //alert('Error in getting Product Licenses list');
        });
    }

    $scope.getProductLicensesAddendumsList = function () {
        var getProductLicensesAddendumsList = AJService.PostDataToAPI("Dashboard/ProductLicensesAddendums", null);
        getProductLicensesAddendumsList.then(function (mdt) {
            $scope.ProductLicensesAddendumsListList = mdt.data;
        }, function () {
            //alert('Error in getting Product Licenses Addendums list');
        });
    }


    $scope.getProduct_ISBN_enteredList = function () {
        var getProduct_ISBN_enteredList = AJService.PostDataToAPI("Dashboard/Product_ISBN_entered?ExecutiveId=" + parseInt($("#enterdBy").val()), null);
        getProduct_ISBN_enteredList.then(function (mdt) {
            $scope.Product_ISBN_Not_enteredList = mdt.data;
        }, function () {
            //alert('Error in getting Product ISBN list');
        });
    }

    //Added by Suranjana on 21/07/2016
    $scope.getProductWIthISBNList = function () {
        var getProductWIthISBNList = AJService.PostDataToAPI("Dashboard/Product_ISBN_Is_Not_Null?ExecutiveId=" + parseInt($("#enterdBy").val()), null);
        getProductWIthISBNList.then(function (mdt) {
            $scope.ProductWithISBNList = mdt.data;
        }, function () {
            //alert('Error in getting Product ISBN list');
        });
    }

    $scope.AuthorContractRequestStatus = function () {
        var AuthorContractRequestStatus = AJService.PostDataToAPI("Dashboard/AuthorContractRequestStatus", null);
        AuthorContractRequestStatus.then(function (status) {
            $scope.AuthorContractRequestStatusList = status.data;
        }, function () {
            //alert('Error in getting Author Contract list');
        });
    }
    //Ended by Suranjana


    $scope.getProduct_SAP_Agr_No_Not_EnteredList = function () {
        var getProduct_SAP_Agr_No_Not_EnteredList = AJService.PostDataToAPI("Dashboard/Product_SAP_Agr_No_Not_Entered?ExecutiveId=" + parseInt($("#enterdBy").val()), null);
        getProduct_SAP_Agr_No_Not_EnteredList.then(function (mdt) {
            $scope.Product_SAP_Agr_No_Not_EnteredList = mdt.data;
        }, function () {
            //alert('Error in getting Product SAP Agreement list');
        });
    }


    //-----Added by Prakash 

    //---------Pending Author Contract Request on 30/08/2016
    $scope.PendingAuthorContractRequest = function () {
        var PendingAuthorContractRequest = AJService.PostDataToAPI("Dashboard/PendingAuthorContractRequest?ExecutiveId=" + parseInt($("#enterdBy").val()), null);
        PendingAuthorContractRequest.then(function (status) {
            $scope.PendingAuthorContractRequestList = status.data;
            angular.forEach(status.data, function (row, index) {
                if (row.Id == 0 && row.Id != undefined && row.Id != null) {
                    $scope.Pendingseries = true;
                }
                if (row.Id != 0 && row.Id != undefined && row.Id != null) {
                    $scope.Pendingcontract = true;
                }
            });
            //$scope.PendingAuthorContractRequestListLength = status.data.length;
        }, function () {
            //alert('Error in getting Pending Author Contract list');
        });
    }

    //---------Draft and Issued Author Contract Request Status on 10 July, 2017
    $scope.IssuedDraftAuthorContractDetails = function () {
        var IssuedDraftAuthorContractDetails = AJService.PostDataToAPI("Dashboard/IssuedDraftAuthorContractDetails?ExecutiveId=" + parseInt($("#enterdBy").val()), null);
        IssuedDraftAuthorContractDetails.then(function (status) {
            $scope.IssuedDraftAuthorContractDetailsList = status.data;
            angular.forEach(status.data, function (row, index){
                if (row.Id == 0 && row.Id != undefined && row.Id != null) {
                    $scope.Issuedseries = true;
                }
                if (row.Id != 0 && row.Id != undefined && row.Id != null) {
                    $scope.Issuedcontract = true;
                }
            });
           
        }, function () {
            //alert('Error in getting Issue and Draft Author Contract Request Status list');
        });
    }

    //--------Inbound Permission Not Entered By Author Contract on 02/09/2016
    $scope.ListInboundPermissionNotEntered = function () {
        var ListInboundPermissionNotEntered = AJService.PostDataToAPI("Dashboard/InboundPermissionNotEntered?ExecutiveId=" + parseInt($("#enterdBy").val()), null);
        ListInboundPermissionNotEntered.then(function (status) {
            $scope.InboundPermissionNotEntered_List = status.data;
        }, function () {
            //alert('Error in Inbound Permission Not Entered list');
        });
    }

    //--------Inbound Permission Not Entered by Product License on 13/09/2016
    $scope.ListInboundPermissionNotEnteredByProductLicense = function () {
        var ListInboundPermissionNotEnteredByProductLicense = AJService.PostDataToAPI("Dashboard/InboundPermissionNotEnteredByProductLicense", null);
        ListInboundPermissionNotEnteredByProductLicense.then(function (status) {
            $scope.InboundPermissionNotEnteredByProductLicense_List = status.data;
        }, function () {
            //alert('Error in Inbound Permission Not Entered list');
        });
    }

    //---------Final Publishing Date Not Entered on 02/09/2016
    $scope.ListFinalPublishingDateNotEntered = function () {
        var ListFinalPublishingDateNotEntered = AJService.PostDataToAPI("Dashboard/FinalPublishingDateNotEntered?ExecutiveId=" + parseInt($("#enterdBy").val()), null);
        ListFinalPublishingDateNotEntered.then(function (status) {
            $scope.FinalPublishingDateNotEntered_List = status.data;
        }, function () {
            //alert('Error in Final Publishing Date Not Entered list');
        });
    }

    //---------Final Publishing Date Is Entered But Impression Not Entered on 02/09/2016
    $scope.ListImpressionNotEntered = function () {
        var ListImpressionNotEntered = AJService.PostDataToAPI("Dashboard/ImpressionNotEntered?ExecutiveId=" + parseInt($("#enterdBy").val()), null);
        ListImpressionNotEntered.then(function (status) {
            $scope.ImpressionNotEntered_List = status.data;
        }, function () {
            //alert('Error in Impression Not Entered list');
        });
    }

    //---------Final Publishing Date Is Entered But Impression Not Entered on 26/09/2016 Added by Ankush
    $scope.ListImpressionNotEntered_AuthorContract = function () {
        var ListImpressionNotEntered_AuthorContract = AJService.PostDataToAPI("Dashboard/ImpressionNotEntered_AuthorContract?ExecutiveId=" + parseInt($("#enterdBy").val()), null);
        ListImpressionNotEntered_AuthorContract.then(function (status) {
            $scope.ImpressionNotEntered_List_AuthorContract = status.data;
        }, function () {
            //alert('Error in Impression Not Entered list');
        });
    }

    //---------Right Sale Contract Expiring Within 3 Months on 05/09/2016
    $scope.ListRightSaleContractExpiring = function () {
        var ListRightSaleContractExpiring = AJService.PostDataToAPI("Dashboard/RightSaleContractExpiring?ExecutiveId=" + parseInt($("#enterdBy").val()), null);
        ListRightSaleContractExpiring.then(function (status) {
            $scope.RightSaleContractExpiring_List = status.data;
        }, function () {
            //alert('Error in Right Sale Contract Expiring Within 3 Months list');
        });
    }

    //---------Right Sale License Expiring Within 3 Months on 26/09/2016 Added By Ankush
    $scope.ListRightSaleContractExpiring_License = function () {
        var ListRightSaleContractExpiring_License = AJService.PostDataToAPI("Dashboard/RightSaleContractExpiring_License", null);
        ListRightSaleContractExpiring_License.then(function (status) {
            $scope.RightSaleContractExpiring_List_License = status.data;
        }, function () {
            //alert('Error in Right Sale Contract Expiring Within 3 Months list');
        });
    }

    //---------Right Sales Payment Not Receive By ContractId on 06/09/2016
    $scope.ListRightSalePaymentNotReceiveByContractId = function () {
        var ListRightSalePaymentNotReceiveByContractId = AJService.PostDataToAPI("Dashboard/RightSalePaymentNotReceiveByContractId?ExecutiveId=" + parseInt($("#enterdBy").val()), null);
        ListRightSalePaymentNotReceiveByContractId.then(function (status) {
            $scope.RightSalePaymentNotReceive_ByContractId_List = status.data;
        }, function () {
            //alert('Error in Right Sales Payment Not Receive By Contract list');
        });
    }

    //---------Right Sales Payment Not Receive By LicenseId on 06/09/2016
    $scope.ListRightSalePaymentNotReceiveByLicenseId = function () {
        var ListRightSalePaymentNotReceiveByLicenseId = AJService.PostDataToAPI("Dashboard/RightSalePaymentNotReceiveByLicenseId??ExecutiveId=" + parseInt($("#enterdBy").val()), null);
        ListRightSalePaymentNotReceiveByLicenseId.then(function (status) {
            $scope.RightSalePaymentNotReceive_ByLicenseId_List = status.data;
        }, function () {
            //alert('Error in Right Sales Payment Not Receive By License list');
        });
    }

    //---------Permission Outbound Payment Not Receive By ContractId on 06/09/2016
    $scope.ListPermissionOutboundPaymentNotReceiveByContractId = function () {
        var ListPermissionOutboundPaymentNotReceiveByContractId = AJService.PostDataToAPI("Dashboard/PermissionOutboundPaymentNotReceiveByContractId?ExecutiveId=" + parseInt($("#enterdBy").val()), null);
        ListPermissionOutboundPaymentNotReceiveByContractId.then(function (status) {
            $scope.PermissionOutboundPaymentNotReceive_ByContractId_List = status.data;
        }, function () {
            //alert('Error in Permission Outbound Payment Not Receive By Contract list');
        });
    }

    //---------Permission Outbound Payment Not Receive By LicenseId on 06/09/2016
    $scope.ListPermissionOutboundPaymentNotReceiveByLicenseId = function () {
        var ListPermissionOutboundPaymentNotReceiveByLicenseId = AJService.PostDataToAPI("Dashboard/PermissionOutboundPaymentNotReceiveByLicenseId?ExecutiveId=" + parseInt($("#enterdBy").val()), null);
        ListPermissionOutboundPaymentNotReceiveByLicenseId.then(function (status) {
            $scope.PermissionOutboundPaymentNotReceive_ByLicenseId_List = status.data;
        }, function () {
            //alert('Error in Permission Outbound Payment Not Receive By License list');
        });
    }
    
    

    //-------------Pending Author Contract on 01/09/2016
    $scope.PendingAuthorContract_submitForm = function () {
      
        var AuthorContact = {
            Contractstatus: 'notagreement', //'Pending',
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            For_: $("#hid_Addendum").val() != "" ? $("#hid_Addendum").val() : null,
        };

        var AuthorStatus = AJService.PostDataToAPI('AuthorContact/AuthorContractSearch', AuthorContact);
        AuthorStatus.then(function (msg) {
            blockUI.stop();

            if (msg.data == "OK") {
                // $scope.ProductLicenseListResult();
                window.location.href = GlobalredirectPath + 'Contract/AuthorContract/AuthorContractSearch?from=Listing&show=dashboard&ViewMore=notagreement';
            }

        }, function () {
            $scope.IsError = false;
        });

        return;
    };

    //-------------Issue and Draft Author Contract Status on 10 July, 2017
    $scope.IssueDraftAuthorContract_submitForm = function () {

        var AuthorContact = {
            Contractstatus: 'Pending,Draft',
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            For_: $("#hid_Addendum").val() != "" ? $("#hid_Addendum").val() : null,
        };

        var AuthorStatus = AJService.PostDataToAPI('AuthorContact/AuthorContractSearch', AuthorContact);
        AuthorStatus.then(function (msg) {
            blockUI.stop();

            if (msg.data == "OK") {
                // $scope.ProductLicenseListResult();
                window.location.href = GlobalredirectPath + 'Contract/AuthorContract/AuthorContractSearch?from=Listing&show=dashboard&ViewMore=IssuedDraftAC';
            }

        }, function () {
            $scope.IsError = false;
        });

        return;
    };

    //-------Pending Series Request on 19 May, 2017
    $scope.PendingSeries_submitForm = function () {
        window.location.href = GlobalredirectPath + 'Contract/AuthorContract/AuthorContractSearch?from=Listing&show=dashboard&ViewMore=PendingSeries';
    }

    //-------Issued and Draft Series Request Status on 10 June, 2017
    $scope.IssuedDraftSeries_submitForm = function () {
        window.location.href = GlobalredirectPath + 'Contract/AuthorContract/AuthorContractSearch?from=Listing&show=dashboard&ViewMore=IssuedDraftSeries';
    }

    //-------------Product License Not Entered on 01/09/2016
    $scope.ProductLicenseNotEntered_submitForm = function () {
               
        var Product = {
            DepartmentId: $("#hid_DepartmentId").val(),
            TypeFor: $('#hid_for').val(),
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            Status: 'PL_N',
            License: $("#hid_License").val() !== "undefined" ? $("#hid_License").val() : null,
        };

        var productStatus = AJService.PostDataToAPI('Author/ProductSearch', Product);
        productStatus.then(function (msg) {
            if (msg.data == "OK") {
                // $scope.SearchingListResult();
                window.location.href = GlobalredirectPath + 'Product/ProductMaster/ProductSearch?from=backtoSearch&show=dashboard&ViewMore=NoLicense';
            }
        }, function () {
            $scope.IsError = false;
        });
        blockUI.stop();

        return;
    };
    
    //-------------ISBN Not Enterd all List on 01/09/2016
    $scope.ISBNNotEnterd_submitForm = function () {
        var Product = {
            DepartmentId: $("#hid_DepartmentId").val(),
            TypeFor: 'isbnassign',
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            Status: 'ISBN_N',
        };

        var productStatus = AJService.PostDataToAPI('Author/ProductSearch', Product);
        productStatus.then(function (msg) {          
            if (msg.data == "OK") {
                 //$scope.SearchingListResult();
                window.location.href = GlobalredirectPath + 'Product/ProductMaster/ProductSearch?from=backtoSearch&for=isbnassign&show=dashboard&ViewMore=NoISBN';
            }
        }, function () {
            $scope.IsError = false;
        });
        blockUI.stop();
    };

    //-------------ISBN Not Enterd By Product on 01/09/2016
    $scope.ISBNNotEnterdByProductCode_submitForm = function (isProductCode) {
              
        var Product = {
            DepartmentId: $("#hid_DepartmentId").val(),
            TypeFor: $('#hid_for').val(),
            ProductCode: isProductCode,
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            License: $("#hid_License").val() !== "undefined" ? $("#hid_License").val() : null,
        };

        var productStatus = AJService.PostDataToAPI('Author/ProductSearch', Product);
        productStatus.then(function (msg) {
            if (msg.data == "OK") {
                // $scope.SearchingListResult();
                window.location.href = GlobalredirectPath + 'Product/ProductMaster/ProductSearch?from=backtoSearch&for=isbnassign&show=dashboard';
            }
        }, function () {
            $scope.IsError = false;
        });
        blockUI.stop();

        return;
    };

    //-------------SAP Agreement Number Not Entered on 01/09/2016
    $scope.SAPAgreementNumberNotEntered_submitForm = function () {
        
        var Product = {
            DepartmentId: $("#hid_DepartmentId").val(),
            TypeFor: $('#hid_for').val(),
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            Status: 'SAP_N',
            License: $("#hid_License").val() !== "undefined" ? $("#hid_License").val() : null,
        };

        var productStatus = AJService.PostDataToAPI('Author/ProductSearch', Product);
        productStatus.then(function (msg) {
            if (msg.data == "OK") {
                // $scope.SearchingListResult();
                window.location.href = GlobalredirectPath + 'Product/ProductMaster/ProductSearch?from=backtoSearch&for=sapaggrement&show=dashboard&ViewMore=NoSAP';
            }
        }, function () {
            $scope.IsError = false;
        });
        blockUI.stop();

        return;
    };
    
    //-------------- Pending Request Other Contract on 2/09/2016
    $scope.PendingRequestOtherContract_submitForm = function () {

        var OtherContract = {
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            ContractStatus: 'Pending'
        };

        var OtherContractStatus = AJService.PostDataToAPI('OtherContract/OtherContractSerch', OtherContract);
        OtherContractStatus.then(function (msg) {
            blockUI.stop();
            if (msg.data == "OK") {
                //$scope.OtherContractListResult();
                window.location.href = GlobalredirectPath + 'Contract/OtherContract/OtherContractSearch?For=BackToSearch&show=dashboard&ViewMore=PendingOC';
            }
        },
        function () {
            //alert('Please validate details');
            $scope.IsError = false;
        });
        blockUI.stop();
    }

    //-------------Product Author Contract Not Entered  on 02/09/2016
    $scope.ProductAuthorContractNotEntered_submitForm = function () {

        var Product = {
            DepartmentId: $("#hid_DepartmentId").val(),
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            License: 'No_AC',
        };

        var productStatus = AJService.PostDataToAPI('Author/ProductSearch', Product);
        productStatus.then(function (msg) {
            if (msg.data == "OK") {
                // $scope.SearchingListResult();
                window.location.href = GlobalredirectPath + 'Product/ProductMaster/ProductSearch?from=backtoSearch&show=dashboard&ViewMore=NoContract';
            }
        }, function () {
            $scope.IsError = false;
        });
        blockUI.stop();

        return;
    };

    //-------------ISBN Enterd all List on 02/09/2016
    $scope.ProductWithISBNList_submitForm = function () {

        var Product = {
            DepartmentId: $("#hid_DepartmentId").val(),
            TypeFor: $('#hid_for').val(),
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            Status: 'ISBN_Y',
            License: $("#hid_License").val() !== "undefined" ? $("#hid_License").val() : null,
        };

        var productStatus = AJService.PostDataToAPI('Author/ProductSearch', Product);
        productStatus.then(function (msg) {
            if (msg.data == "OK") {
                // $scope.SearchingListResult();
                window.location.href = GlobalredirectPath + 'Product/ProductMaster/ProductSearch?from=backtoSearch&show=dashboard&ViewMore=ISBN';
            }
        }, function () {
            $scope.IsError = false;
        });
        blockUI.stop();

        return;
    };

    //-------------Inbound Permission Not Entered all List By Author Contract on 02/09/2016
    $scope.InboundPermissionNotEntered_submitForm = function () {
  
        var Product = {
            DepartmentId: $("#hid_DepartmentId").val(),
            TypeFor: 'Original',
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            Status: 'PI_N',
            License: $("#hid_License").val() !== "undefined" ? $("#hid_License").val() : null,
        };

        var productStatus = AJService.PostDataToAPI('Author/ProductSearch', Product);
        productStatus.then(function (msg) {
            if (msg.data == "OK") {
                // $scope.SearchingListResult();
                window.location.href = GlobalredirectPath + 'Contract/AuthorContract/AuthorContractSearch?from=backtoSearch&For=PermissionsInbound&show=dashboard&ViewMore=NoInbound';
            }
        }, function () {
            $scope.IsError = false;
        });
        blockUI.stop();

        return;
    };

    //-------------Inbound Permission Not Entered all List by Product License on 13/09/2016
    $scope.InboundPermissionNotEntered_ByProductLicense_submitForm = function () {

        var Product = {
            DepartmentId: $("#hid_DepartmentId").val(),
            TypeFor: 'Original',
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            Status: 'PI_N',
            License: $("#hid_License").val() !== "undefined" ? $("#hid_License").val() : null,
        };

        var productStatus = AJService.PostDataToAPI('Author/ProductSearch', Product);
        productStatus.then(function (msg) {
            if (msg.data == "OK") {
                // $scope.SearchingListResult();
                window.location.href = GlobalredirectPath + 'Product/ProductLicense/ProductLicenseSearch?For=PermissionsInbound&Back=BackToserach&show=dashboard&ViewMore=NoInbound';
            }
        }, function () {
            $scope.IsError = false;
        });
        blockUI.stop();

        return;
    };

    //-------------Final Publishing Date Not Entered all List on 02/09/2016 
    $scope.FinalPublishingDateNotEntered_submitForm = function () {

        var Product = {
            DepartmentId: $("#hid_DepartmentId").val(),
            TypeFor: 'finalproductentry',
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            Status: '',
            License: $("#hid_License").val() !== "undefined" ? $("#hid_License").val() : null,
        };

        var productStatus = AJService.PostDataToAPI('Author/ProductSearch', Product);
        productStatus.then(function (msg) {
            if (msg.data == "OK") {
                // $scope.SearchingListResult();
                window.location.href = GlobalredirectPath + 'Product/ProductMaster/ProductSearch?from=backtoSearch&for=finalproductentry&show=dashboard&ViewMore=NoFinalPublishingDate';
            }
        }, function () {
            $scope.IsError = false;
        });
        blockUI.stop();

        return;
    };

    //-------------Right Sale Contract Expire all List on 02/09/2016
    $scope.RightSaleContractExpire_submitForm = function () {
        var date = new Date();
        var mdat_currentDate = date.getDate() + "/" + (date.getMonth() + 1) + "/" + date.getFullYear()
        var mdat_within3MonthDate = date.getDate() + "/" + (date.getMonth() + 4) + "/" + date.getFullYear()
        //alert(mdat_currentDate + " " + mdat_within3MonthDate);
        var _mobjRightsSellingSearch = {
            //RequestFromDate: mdat_currentDate, //$scope.RequestFromDate,
            //RequestToDate: mdat_within3MonthDate, //$scope.RequestToDate,
            EntryDate: new Date(),
            Dateofexpiry: 3,
            Dateofexpiry_opr : "<=",
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            ContractStatus: ''
        }

        var AuthorStatus = AJService.PostDataToAPI('RightsSelling/InsertRightsSellingHistory', _mobjRightsSellingSearch);
        AuthorStatus.then(function (msg) {
            //blockUI.stop();
            if (msg.data == "OK") {
               // $scope.RightsSellingListResult();
                window.location.href = "../../RightsSelling/RightsSelling/RightsSellingSearch?For=BackToList&ViewMore=RightsExpire";
            }

        }, function () {
            $scope.IsError = false;
        });

    }
    
    //-------------Author Contract Requiest Status on 13/09/2016
    $scope.AuthorContractRequiestStatus_submitForm = function () {

        var AuthorContact = {
            Contractstatus: 'Issued,Cancelled',
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            For_: $("#hid_Addendum").val() != "" ? $("#hid_Addendum").val() : null,
        };

        var AuthorStatus = AJService.PostDataToAPI('AuthorContact/AuthorContractSearch', AuthorContact);
        AuthorStatus.then(function (msg) {
            blockUI.stop();

            if (msg.data == "OK") {
                // $scope.ProductLicenseListResult();
                window.location.href = GlobalredirectPath + 'Contract/AuthorContract/AuthorContractSearch?from=Listing&show=dashboard&ViewMore=ACStatus';
            }

        }, function () {
            $scope.IsError = false;
        });

        return;
    };
    //-----End by Prakash

    //Added By Ankush On 28/09/2016

    //---------Product License Expired on 28/09/2016
    $scope.ProductLicenseExpired = function () {
        var ProductLicenseExpired = AJService.PostDataToAPI("Dashboard/ProductLicenseExpired?ExecutiveId=" + parseInt($("#enterdBy").val()), null);
        ProductLicenseExpired.then(function (status) {
            $scope.ProductLicenseExpiredList = status.data;
        }, function () {
            //alert('Error in getting Product License Expired List');
        });
    }

    //------------Product License Expired on 28/09/2016
    $scope.ProductLicenseExpired_submitForm = function () {

        var Product = {
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            For_: 'LicenseExpired',
        };

        var productStatus = AJService.PostDataToAPI('ProductLicense/ProductLicenseSearch', Product);
        productStatus.then(function (msg) {
            if (msg.data == "OK") {
                // $scope.SearchingListResult();
                window.location.href = GlobalredirectPath + 'Product/ProductLicense/ProductLicenseSearch?For=BackToserach&show=dashboard&ViewMore=LicenseExpired';
            }
        }, function () {
            $scope.IsError = false;
        });
        blockUI.stop();

        return;
    };


    //---------Product License Expired on 28/09/2016
    $scope.ProductLicenseAddendumExpired = function () {
        var ProductLicenseAddendumExpired = AJService.PostDataToAPI("Dashboard/ProductLicenseAddendumExpired", null);
        ProductLicenseAddendumExpired.then(function (status) {
            $scope.ProductLicenseAddendumExpiredList = status.data;
        }, function () {
            //alert('Error in getting Product License Addendum Expired List');
        });
    }

    //------------Product License Expired on 28/09/2016
    $scope.ProductLicenseAddendumExpired_submitForm = function () {

        var Product = {
            DepartmentId: $("#hid_DepartmentId").val(),
            TypeFor: $('#hid_for').val(),
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            Status: 'ADD_3M',
        };

        var productStatus = AJService.PostDataToAPI('Author/ProductSearch', Product);
        productStatus.then(function (msg) {
            if (msg.data == "OK") {
                // $scope.SearchingListResult();
                window.location.href = GlobalredirectPath + 'Product/ProductMaster/ProductSearch?from=backtoSearch&show=dashboard&ViewMore=LicenseAddendumExpired';
            }
        }, function () {
            $scope.IsError = false;
        });
        blockUI.stop();

        return;
    };


    $scope.InboundPermissionQuantityLess25 = function () {
        var InboundPermissionQuantityLess25 = AJService.PostDataToAPI("Dashboard/InboundPermissionQuantityLess25?ExecutiveId=" + parseInt($("#enterdBy").val()), null);
        InboundPermissionQuantityLess25.then(function (status) {
            $scope.InboundPermissionQuantityLess25_List = status.data;
        }, function () {
            //alert('Error in getting Inbound Permission Quantity Less 25% List');
        });
    }


    //End by Ankush 

    $scope.getPermissionOutboundExpiryDate = function () {
        var getPermissionOutboundExpiryDate = AJService.PostDataToAPI("Dashboard/PermissionOutboundExpiryDate_List?ExecutiveId=" + parseInt($("#enterdBy").val()), null);
        getPermissionOutboundExpiryDate.then(function (status) {
            $scope.PermissionOutboundExpiryDate_List = status.data;
        }, function () {
            ////alert('Error in getting Inbound Permission Quantity Less 25% List');
        });
    }
    

    $scope.Inbound_Permission_Not_Entered_dash = function () {

        var Product = {
            DepartmentId: $("#hid_DepartmentId").val(),
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            Status: 'PI_N',
            ThirdPartyPermission: 1,
        };

        var productStatus = AJService.PostDataToAPI('Author/ProductSearch', Product);
        productStatus.then(function (msg) {
            if (msg.data == "OK") {
                //window.location.href = GlobalredirectPath + 'Contract/AuthorContract/AuthorContractSearch?from=backtoSearch&For=PermissionsInbound&show=dashboard&ViewMore=NoInbound';
                document.location = GlobalredirectPath + "/Product/ProductMaster/ProductSearch?For=Dashboard&ForPI=PermissionsInbound";
            }
        }, function () {
            $scope.IsError = false;
        });
        blockUI.stop();

        return;
    };

    
    $scope.Inbound_Permission_Expiring_Within_Months_das = function () {

        var _temp = 0;
        var CurrentDate = new Date();
        CurrentDate.setMonth(CurrentDate.getMonth() + 3);
        var date = (CurrentDate.getMonth()+1) + '/' + CurrentDate.getDate() + '/' + CurrentDate.getFullYear();
        //if ((CurrentDate.getMonth() + 4) > 12) {
        //    _temp = (CurrentDate.getMonth() + 4) - 12;
        //    date = _temp + '/' + CurrentDate.getDate() + '/' + (CurrentDate.getFullYear() + 1);
        //}
        
        var PermissionsInboundModel = {
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            //PermissionExpirydate: "12/29/2018",
            PermissionExpirydate: date,
            For: 'dashboard',
        };

        var PermissionInboundObjectList = AJService.PostDataToAPI('PermissionsInbound/InsertIntoSearchHistory', PermissionsInboundModel);
        PermissionInboundObjectList.then(function (msg) {
            if (msg.data == "OK") {
                window.location.href = GlobalredirectPath + 'PermissionsInbound/PermissionsInbound/search?Type=View&For=Dashboard';
            }
        }, function () {
            $scope.IsError = false;
        });
        blockUI.stop();

        return;
    };

    $scope.InboundPermission_LessQuantity_submitForm = function () {
         window.location.href = GlobalredirectPath + 'PermissionsInbound/PermissionsInbound/search?Type=View&For=Dashboard&Data=Less';
    };
    

});

