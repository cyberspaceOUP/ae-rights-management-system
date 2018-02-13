
app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {
    app.expandControllerTopSearch($scope, AJService, $window);
    /*Expand common.master.js Controller*/
    app.expandControllerA($scope, AJService, $window);
    app.expandControllerAuthorContractDetails($scope, AJService, $window);

    app.expandControllerProductLicense($scope, AJService, $window);

    app.expandControllerProductDetails($scope, AJService, $window);

    app.expandControllerSubsidiaryRightsAuthorContract($scope, AJService, $window);

    app.expandControllerSubsidiaryRightsProductLicense($scope, AJService, $window);

  
    $scope.PermissionsOutboundUpdate = true;

    $scope.RoyaltyRecurringReq = false;

    $scope.ProductSerach($('#hid_ProductId').val());

    setTimeout(function () {
        //fetch Kit Details List
        app.expandControllerKitISBNLIst($scope, AJService);
        angular.element(document.getElementById('angularid')).scope().KitISBNDetailsList($('#hid_ProductId').val());
    }, 300);

    if ($('#hid_UserRight').val() != "") {
        $scope.ContractstatusReq = true;
      
        if ($('#hid_UserRight').val() == "rt")
        {
           
            $scope.PermissionsOutboundView = true;
            $scope.PermissionsOutboundUpdate = false;
        }

    }
    else {
        $scope.ContractstatusReq = false;
    }

    $scope.SubsidiaryRightsAuthorContract

    if ($('#hid_Type').val() == "A") {
        $scope.AuthorContract($("#hid_AuthorContract").val());
        $scope.SubsidiaryRightsAuthorContract($('#hid_AuthorContract').val());
        $scope.Req_ContractDeatil = true;
        $scope.Req_ProductLicense = false;
    }
    else if ($('#hid_Type').val() == "P") {

        $scope.Req_ProductLicense = true;
        $scope.Req_ContractDeatil = false;
        $scope.SubsidiaryRightsProductLicense($('#hid_AuthorContract').val());
        $scope.ProductLicenseSerach($("#hid_AuthorContract").val());
    }


    $scope.EditPermissionsOutBound = function (Id)
    {
       

        var EditPermissionsOutBoundDetail = {
            PermissionsoutboundId: Id,
           
        };
        $('#hid_PermissionsOutbound').val(Id)
        $('#hid_ReqValue').val(0)
        // call API to fetch temp product type list basis on the FlatId
        var EditPermissionsOutBoundDetailStatus = AJService.PostDataToAPI('PermissionsOutbound/PermissionsOutBoundDetails', EditPermissionsOutBoundDetail);
        EditPermissionsOutBoundDetailStatus.then(function (msg) {
            if (msg != null) {

                $scope.getPermissionsOutboundLanguageList();

                if (msg.data.PermissionsoutboundDetialsDocuments.Documentname != null) {
                    var e1 = 0;
                    var d1 = 0;
                    var docNames1 = '';
                    var Docurl1 = '';
                    $scope.Docurl1 = [];

                    if (msg.data.PermissionsoutboundDetialsDocuments.Documentname != '') {

                        $scope.Pendingdocumentshow = true;
                        var docNames1 = msg.data.PermissionsoutboundDetialsDocuments.Documentname.slice(',');
                        var DName1 = msg.data.PermissionsoutboundDetialsDocuments.Documentname.slice(',');

                        var DId1 = msg.data.PermissionsoutboundDetialsDocuments.DocumentIds.slice(',');

                        var Docurl1 = msg.data.PermissionsoutboundDetialsDocuments.DocumentFile.split(',');
                        //   $scope.Docurl = [];
                        for (var i = 0; i < Docurl1.length - 1; i++) {
                            //for (var j = 0; j < docNames.length; j++) {   
                            for (var j = 0, k = 0; j < docNames1.length && k < DId1.length ; j++, k++) {
                                if (e1 == 0 && d1 == 0) {
                                    //  $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[j].toString(), DocId: DId[k].toString() });
                                    $scope.Docurl1.push({ text1: Docurl1[i].toString(), name1: docNames1[j].toString(), DocId1: DId1[k].toString() });
                                    // $scope.Docurl1.push({ text: "ABC.DOC", name: "123456TEST", DocId: 10 });

                                   // $("#hid_DocumentID").val($("#hid_DocumentID").val() + Docurl1[i].toString() + ',');
                                }
                                else {
                                    $scope.Docurl1.push({ text1: Docurl1[i].toString(), name1: docNames1[e1].toString(), DocId1: DId1[d1].toString() });
                                }

                                e1 = j + 1;
                                d1 = k + 1;
                                i = i + 1;
                            }



                        }

                    }
                    else if ($scope.Pendingdocumentshow == true) {
                        $scope.Pendingdocumentshow = false;
                    }
                }

               
                if (msg.data._GetPermissionsOutBoundUpdate != null) {

                    if ($('#hid_UserRight').val() == "rt") {
                     
                       
                        $scope.ContractstatusReq = false;
                        $scope.PermissionsOutboundUpdateView = true;
                       
                    }
                    else {
                        $scope.ContractstatusReq = true;
                        $scope.PermissionsOutboundUpdateView = false;
                     
                    }

                 
                }
                else {
                    $scope.ContractstatusReq = true;
                    $scope.PermissionsOutboundUpdateView = false;
                }

                $scope.LicenseeView = (msg.data._GetPermissionsOutBound[0].organizationname == null ? '---' : msg.data._GetPermissionsOutBound[0].organizationname);
                $scope.ContactPersonView = (msg.data._GetPermissionsOutBound[0].contactperson == null ? '---' : msg.data._GetPermissionsOutBound[0].contactperson);

                $scope.LicenseeCodeView = (msg.data._GetPermissionsOutBound[0].licenseecode == null ? '---' : msg.data._GetPermissionsOutBound[0].licenseecode);
                $scope.MobileView = (msg.data._GetPermissionsOutBound[0].mobile == null ? '---' : msg.data._GetPermissionsOutBound[0].mobile);

                $scope.LicenseeView = (msg.data._GetPermissionsOutBound[0].organizationname == null ? '---' : msg.data._GetPermissionsOutBound[0].organizationname);
                $scope.ContactPersonView = (msg.data._GetPermissionsOutBound[0].contactperson == null ? '---' : msg.data._GetPermissionsOutBound[0].contactperson);

                $scope.EmailView = (msg.data._GetPermissionsOutBound[0].email == null ? '---' : msg.data._GetPermissionsOutBound[0].email);
                $scope.URLView = (msg.data._GetPermissionsOutBound[0].url == null ? '---' : msg.data._GetPermissionsOutBound[0].url);


                $scope.AddressView = (msg.data._GetPermissionsOutBound[0].address == null ? '---' : msg.data._GetPermissionsOutBound[0].address);

                $scope.CountryView = (msg.data._GetPermissionsOutBound[0].Country == null ? '---' : msg.data._GetPermissionsOutBound[0].Country);

                $scope.StateView = (msg.data._GetPermissionsOutBound[0].State == null ? '---' : msg.data._GetPermissionsOutBound[0].State);

                $scope.CityView = (msg.data._GetPermissionsOutBound[0].City == null ? '---' : msg.data._GetPermissionsOutBound[0].City);

                $scope.PincodeView = (msg.data._GetPermissionsOutBound[0].pincode == null ? '---' : msg.data._GetPermissionsOutBound[0].pincode);



                $scope.RequestDateView = (msg.data._GetPermissionsOutBound[0].RequestDateView == null ? '---' : msg.data._GetPermissionsOutBound[0].RequestDateView);

                $scope.PermissionsOutboundCodeView = (msg.data._GetPermissionsOutBound[0].permissionsoutboundcode == null ? '---' : msg.data._GetPermissionsOutBound[0].permissionsoutboundcode);

                $scope.LicenseePublicationTitleView = (msg.data._GetPermissionsOutBound[0].licenseepublicationtitle == null ? '---' : msg.data._GetPermissionsOutBound[0].licenseepublicationtitle);
                $scope.PermissionDateView = (msg.data._GetPermissionsOutBound[0].DateOfPermissionView == null ? '---' : msg.data._GetPermissionsOutBound[0].DateOfPermissionView);
                $scope.PermissionperiodView = (msg.data._GetPermissionsOutBound[0].permissionperiod == null ? '---' : msg.data._GetPermissionsOutBound[0].permissionperiod);
                $scope.ExpiryDateView = (msg.data._GetPermissionsOutBound[0].DateExpiryView == null ? '---' : msg.data._GetPermissionsOutBound[0].DateExpiryView);
                $scope.RequestMaterialView = (msg.data._GetPermissionsOutBound[0].requestmaterial == null ? '---' : msg.data._GetPermissionsOutBound[0].requestmaterial);
                $scope.WillmaterialtranslatedView = (msg.data._GetPermissionsOutBound[0].will_be_material_be_translated == null ? '---' : msg.data._GetPermissionsOutBound[0].will_be_material_be_translated);
                $scope.WillmaterialadeptedView = (msg.data._GetPermissionsOutBound[0].will_be_material_be_adepted == null ? '---' : msg.data._GetPermissionsOutBound[0].will_be_material_be_adepted);



                //$scope.LanguageView = (msg.data._GetPermissionsOutBound[0].languagename == null ? '---' : msg.data._GetPermissionsOutBound[0].languagename);
                $scope.ExtentView = (msg.data._GetPermissionsOutBound[0].extent == null ? '---' : msg.data._GetPermissionsOutBound[0].extent);
                $scope.TerritoryView = (msg.data._GetPermissionsOutBound[0].territoryrights == null ? '---' : msg.data._GetPermissionsOutBound[0].territoryrights);
                $scope.DateInvoiceView = (msg.data._GetPermissionsOutBound[0].DateOfInvoiceView == null ? '---' : msg.data._GetPermissionsOutBound[0].DateOfInvoiceView);
                $scope.InvoiceApplicableView = (msg.data._GetPermissionsOutBound[0].invoiceapplicable == null ? '---' : msg.data._GetPermissionsOutBound[0].invoiceapplicable);
                $scope.InvoiceNoView = (msg.data._GetPermissionsOutBound[0].invoiceno == null ? '---' : msg.data._GetPermissionsOutBound[0].invoiceno);
                $scope.InvoiceCurrencyView = (msg.data._GetPermissionsOutBound[0].InvoiceCurrencyName == null ? '---' : msg.data._GetPermissionsOutBound[0].InvoiceCurrencyName);
                $scope.InvoiceCurrencySymbol = (msg.data._GetPermissionsOutBound[0].InvoiceCurrencySymbol == null ? '---' : msg.data._GetPermissionsOutBound[0].InvoiceCurrencySymbol);
                $scope.InvoiceCurrencySymbolView = (msg.data._GetPermissionsOutBound[0].InvoiceCurrencySymbol == null ? 'inr' : msg.data._GetPermissionsOutBound[0].InvoiceCurrencySymbol);
                $scope.InvoiceValueView = (msg.data._GetPermissionsOutBound[0].invoicevalue == null ? '---' : msg.data._GetPermissionsOutBound[0].invoicevalue);
                $scope.InvoiceDescriptionView = (msg.data._GetPermissionsOutBound[0].invoicedescription == null ? '---' : msg.data._GetPermissionsOutBound[0].invoicedescription);


                $scope.CopiesreceivedView = (msg.data._GetPermissionsOutBound[0].copies_to_be_received == null ? '---' : msg.data._GetPermissionsOutBound[0].copies_to_be_received);
                $scope.NumbercopiesView = (msg.data._GetPermissionsOutBound[0].numberofcopies == null ? '---' : msg.data._GetPermissionsOutBound[0].numberofcopies);
                $scope.RemarksView = (msg.data._GetPermissionsOutBound[0].remarks == null ? '---' : msg.data._GetPermissionsOutBound[0].remarks);


                if (msg.data._GetPermissionsOutBoundUpdate[0] != undefined) {
                    $scope.ReqPendingRequest = true;
                    $scope.ContractStatusView = (msg.data._GetPermissionsOutBoundUpdate[0].contractstatus == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].contractstatus);
                    $scope.PaymentReceivedView = (msg.data._GetPermissionsOutBoundUpdate[0].paymentreceived == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].paymentreceived);
                    $scope.PaymentAmountView = (msg.data._GetPermissionsOutBoundUpdate[0].paymentamount == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].paymentamount);
                    $scope.CurrencyView = (msg.data._GetPermissionsOutBoundUpdate[0].currencyname == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].currencyname);
                    $scope.DateOfAgreementView = (msg.data._GetPermissionsOutBoundUpdate[0].Date_of_agreement == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].Date_of_agreement);
                    $scope.SignedContractSentDateView = (msg.data._GetPermissionsOutBoundUpdate[0].Signed_Contract_sent_date == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].Signed_Contract_sent_date);
                    $scope.SignedContractReceivedDateView = (msg.data._GetPermissionsOutBoundUpdate[0].Signed_Contract_receiveddate == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].Signed_Contract_receiveddate);
                    $scope.CancellationDateView = (msg.data._GetPermissionsOutBoundUpdate[0].CancellationDate == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].CancellationDate);
                    $scope.CancellationReasonView = (msg.data._GetPermissionsOutBoundUpdate[0].Cancellation_Reason == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].Cancellation_Reason);
                    $scope.PenddingRemarksView = (msg.data._GetPermissionsOutBoundUpdate[0].PendingRemarks == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].PendingRemarks);
                    $scope.ContributorAgreementView = (msg.data._GetPermissionsOutBoundUpdate[0].contributor_agreement == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].contributor_agreement);


                    $scope.EffectivedateView = (msg.data._GetPermissionsOutBoundUpdate[0].Effectivedate == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].Effectivedate);
                    $scope.ContractperiodinmonthView = (msg.data._GetPermissionsOutBoundUpdate[0].Contractperiodinmonth == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].Contractperiodinmonth);
                    $scope.ExpirydateView = (msg.data._GetPermissionsOutBoundUpdate[0].Expirydate == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].Expirydate);


                    
                }
                else {
                  
                    $scope.ReqPendingRequest = false;
                }
            
                    $scope.PermissionsOutboundList = msg.data._OutboundTypeOfRightsMaster;

               

            }
            else {
                SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                blockUI.stop();
            }

        });

    }

 
  


    if ($('#hid_OutboundId').val() != "")
    {

      
        $('.backToList').css("display", "block");


        $scope.EditPermissionsOutBound($('#hid_OutboundId').val());

        //$scope.getPermissionsOutboundLanguageList();

    }

    $scope.getPermissionsOutboundLanguageList = function () {



        var PermissionsOutboundLanguageList = AJService.GetDataFromAPI("PermissionsOutbound/getPermissionsLanguageList?Id=" + $('#hid_OutboundId').val() + "", null);

        PermissionsOutboundLanguageList.then(function (msg) {
            if (msg != null) {

                $scope.PermissionsOutboundLanguageList = msg.data;

            }
            else {
                SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                blockUI.stop();
            }

        });

    }  

    
});


