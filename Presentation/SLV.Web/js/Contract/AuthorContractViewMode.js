app
    .filter("Filter_TermsOfCopyRight", function ()
    {
        return function(termofcopyright)
        {
            switch(termofcopyright)
            {
                case 1:
                    return "Periodic";
                case 2:
                    return "Perpetual";
                default:
                    return "---";
            }
        }
    })
    .filter("Filter_AgreementStatus", function () {
        return function (AgreementStatus) {
            switch (AgreementStatus) {
                case "Pending":
                    return "Issued";
                case "Issued":
                    return "Received";
                default:
                    return AgreementStatus;
            }
        }
    })
     .filter("Filter_CopyRightOwner", function () {
         return function (CopyRightOwner) {
             switch (CopyRightOwner) {
                 case "J":
                     return "Joint";
                 case "P":
                     return "Publisher";
                 default:
                     return "Author";
             }
         }
     })
    .controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {

    /*Expand Royalty Slab Controller*/
    //app.expandControllerRoyaltySlab($scope, AJService, $window);
      
    app.expandControllerProductDetails($scope, AJService, $window);
    app.expandControllerProductLicense($scope, AJService, $window);
    app.AuthorViewcontroller($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);
    $scope.HandledBy = "";
    $scope.Amendment = false;
    $scope._subsidiaryList = [];
    $scope.ttlSubsidiary = 2;
    $scope.AgreementStatus = "";
    $scope.ContributorAgreement = "";
    $scope.AgreementDate = "";
    $scope.ContributorDoc = [];
    $scope.AgreementDoc = [];
    var mstr_AddendumValue = [];

    /*============================================================================================================
   Here is the section will used for open the author Contract form in view mode
   ============================================================================================================*/
    /******************************************************** **********************
   *******************************************************************************
   Created By  :  Dheeraj Kumar Sharma
   Created on  :  07/06/2016
   Created For :  Get the product details based on email Id

   *******************************************************************************
   *******************************************************************************/
   
    $scope.AuthorListForAddendum = [];
    $scope.authorNameAddendum = [];
    $scope.authorNameAddendum_ListNew = [];
    $scope.authorIdAddendum_ListNew = [];
    $scope.authorIdAddendum = [];
    $scope.SubsidiaryListorigional = [];
    $scope._subsidiaryListDataDisplay = [];
    /*******************************************************************************************************************************
   Created by  :   Dheeraj kumar sharma
   Created on  :   4th Aug
   Created for :   To bind sub product type of list in case of addendum upload
   *I*******************************************************************************************************************************/


    $scope.getImpressionDetails = function (ProductId, LicenseId, ContractId) {
        var _ImpressionDetails = {
            ProductId: ProductId,
            LicenseId: LicenseId,
            ContractId: ContractId
        }

        var ImpressionDetails = AJService.PostDataToAPI("Addendum/ImpressionDetails", _ImpressionDetails);
        ImpressionDetails.then(function (ImpressionData) {
            
            $scope.ImpressionList = ImpressionData.data;
        }, function () {
            //alert('Error in Getting Impression Details');
        });

    }




    $scope.SubProductTypeList = function () {
        var SubProductTypeList = AJService.GetDataFromAPI("CommonList/getSubProductTypeList", null);
        SubProductTypeList.then(function (SubProductTypeList) {
            $scope.SubProductTypeList = SubProductTypeList.data.subProductData;
        }, function () {
            //alert('Error in getting SubProduct Type List');
        });
    }

   
    $scope.GetAuthorContractDetails = function (Id) {

        $scope.ContractId = Id;
        $scope.getContractAddendumList(); //get previous addendun list

        if ($('#hid_Addendum').val() != "" && typeof ($('#hid_Addendum').val()) !== "undefined") {
            if ($('#hid_User').val() == "admin" || $('#hid_User').val() == "Super Admin") {
                $scope.UploadFIleReq = true;
                $scope.documentshow = true;
            }
            else if ($('#hid_User').val() == "Rights") {
                $scope.UploadFIleReq = true;
                $scope.documentshow = true;
            }
            else if ($('#hid_User').val() == "Editorial") {
                $scope.documentshow = true;
            }
        }

        $scope.getAddendumDetails();
        $scope.getAddendumDocumentList();
        $scope.SubProductTypeList();

        $('.Royalty').hide();
        $('.Term').hide();
        $('.RoyaltyChange').hide();


        var _ContractDetails = AJService.GetDataFromAPI("AuthorContact/GetAuthorContractDetails?Id=" + Id);
        _ContractDetails.then(function (_ContractDetails) {
            //debugger;
            if (_ContractDetails.data.SeriesCode == null || _ContractDetails.data.SeriesCode == "" || _ContractDetails.data.SeriesCode == undefined) {
                $scope.SeriesCode_Available = false;
               
                //angular.element(document.getElementById('angularid')).scope().ProductSerach(_ContractDetails.data._AuhtorContract.ProductId, _ContractDetails.data._AuhtorContract.AuthorContractCode, null);
                angular.element(document.getElementById('angularid')).scope().ProductSerachContract(_ContractDetails.data._AuhtorContract.ProductId, _ContractDetails.data._AuhtorContract.AuthorContractCode, null);
            }
            else {
                $scope.SeriesCode_Available = true;
                $scope.ShowProductsDetailMultiple(_ContractDetails.data.SeriesCode); //added by Prakash on 14 July, 2017
            }

            $scope.HandledBy = _ContractDetails.data._AuhtorContract.HandledByName;
            if (_ContractDetails.data._AuhtorContract.LicenceId != undefined) {
                $scope.ProductLicenseSerach(_ContractDetails.data._AuhtorContract.LicenceId);
                LicenseId = _ContractDetails.data._AuhtorContract.LicenceId;
                $scope.LicenseId = _ContractDetails.data._AuhtorContract.LicenceId;
                $("#licenseBlock").css("display", "table-row");
            }

            $scope.AuthorContractCode = _ContractDetails.data._AuhtorContract.AuthorContractCode == null ? "---" : _ContractDetails.data._AuhtorContract.AuthorContractCode;
            $scope.EntryDate = _ContractDetails.data._AuhtorContract.EntryDate;
            $scope.ContractDate = _ContractDetails.data._AuhtorContract.ContractDate;
            $scope.ContractType = _ContractDetails.data._AuhtorContract.ContractType;
            $scope.TemsOfCopyRight = _ContractDetails.data._AuhtorContract.TemsOfCopyRight;
            $scope.PeriodInMonth = _ContractDetails.data._AuhtorContract.PeriodInMonth;
            $scope.ContractExpiry = _ContractDetails.data._AuhtorContract.ContractExpiry;
            $scope.BuyBack = _ContractDetails.data._AuhtorContract.BuyBack != null ? _ContractDetails.data._AuhtorContract.BuyBack : "---";
            $scope.NatureOfWork = _ContractDetails.data._AuhtorContract.NatureOfWork;
            $scope.CopyRightOwner = _ContractDetails.data._AuhtorContract.CopyRightOwner;
            $scope.Teriterry = _ContractDetails.data._AuhtorContract.Teriterry;
            $scope.ThirdPartyPermission = _ContractDetails.data._AuhtorContract.ThirdPartyPermission;
            $scope.Amendment = _ContractDetails.data._AuhtorContract.Amendment;
            $scope.AmendmentRemarks = _ContractDetails.data._AuhtorContract.AmendmentRemarks;
            $scope.Restriction = _ContractDetails.data._AuhtorContract.Restriction == null ? "---" : _ContractDetails.data._AuhtorContract.Restriction;
            $scope.NoOfAuthors = _ContractDetails.data._AuhtorContract.NoOfAuthors;
            $scope.SubjectMatterandTreatment = _ContractDetails.data._AuhtorContract.SubjectMatterandTreatment == null ? "---" : _ContractDetails.data._AuhtorContract.SubjectMatterandTreatment;
            $scope.MinWords = _ContractDetails.data._AuhtorContract.MinWords == 0 || _ContractDetails.data._AuhtorContract.MinWords == null ? "---" : _ContractDetails.data._AuhtorContract.MinWords;
            $scope.MaxWords = _ContractDetails.data._AuhtorContract.MaxWords == 0 || _ContractDetails.data._AuhtorContract.MaxWords == null ? "---" : _ContractDetails.data._AuhtorContract.MaxWords;
            $scope.MinPages = _ContractDetails.data._AuhtorContract.MinPages == 0 || _ContractDetails.data._AuhtorContract.MinPages == null ? "---" : _ContractDetails.data._AuhtorContract.MinPages;
            $scope.MaxPages = _ContractDetails.data._AuhtorContract.MaxPages == 0 || _ContractDetails.data._AuhtorContract.MaxPages == null ? "---" : _ContractDetails.data._AuhtorContract.MaxPages;
            $scope.PriceType = _ContractDetails.data._AuhtorContract.PriceType == null ? "---" : _ContractDetails.data._AuhtorContract.PriceType;
            $scope.Currency = _ContractDetails.data._AuhtorContract.Currency != null ? _ContractDetails.data._AuhtorContract.Currency : "---";
            $scope.CurrencySymbol = _ContractDetails.data._AuhtorContract.CurrencySymbol != null ? _ContractDetails.data._AuhtorContract.CurrencySymbol : "---";
            $scope.Price = _ContractDetails.data._AuhtorContract.Price == 0 ? "---" : _ContractDetails.data._AuhtorContract.Price;
            $scope.MediumofDelivery = _ContractDetails.data._AuhtorContract.MediumofDelivery == null ? "---" : _ContractDetails.data._AuhtorContract.MediumofDelivery;
            $scope.Deliveryschedule = _ContractDetails.data._AuhtorContract.Deliveryschedule == null || _ContractDetails.data._AuhtorContract.Deliveryschedule == '' ? "---" : _ContractDetails.data._AuhtorContract.Deliveryschedule;
            $scope.ProductRemarks = _ContractDetails.data._AuhtorContract.ProductRemarks == null ? "---" : _ContractDetails.data._AuhtorContract.ProductRemarks;
            $scope.MenuScriptDelivery = _ContractDetails.data._AuhtorContract.MenuScriptDelivery != "" ? _ContractDetails.data._AuhtorContract.MenuScriptDelivery : "---";
            $scope.ContributorList = _ContractDetails.data._contributor;
            $scope.MaterialSuppliedByAuthorList = _ContractDetails.data._MaterialDate;
            $scope.AuthorBox = _ContractDetails.data._AuthorList;
            $scope.MenuScriptDeliveryFormatList = _ContractDetails.data._ManuscriptDeliveryList;

            for (var i = 0; i < $scope.AuthorBox.length; i++) {
                $scope.authorNameAddendum[i] = $scope.AuthorBox[i].Name;
                $scope.authorIdAddendum[i] = $scope.AuthorBox[i].Id;
            }

            $scope.RoyaltyslabList = _ContractDetails.data._royalty;
            $scope.TblList = _ContractDetails.data.TblList;
            $scope.SubsidiaryListorigional = _ContractDetails.data._susidiaryRightsList;
            $scope.ttlSubsidiary = _ContractDetails.data._ttlSusidiary;
            for (i = 0; i < $scope.SubsidiaryListorigional.length; i++) {
                //if ($scope.SubsidiaryListorigional[i].OupPercentage != 100 && $scope.SubsidiaryListorigional[i].Percentage != 0) {
                    $scope._subsidiaryListDataDisplay.push($scope.SubsidiaryListorigional[i]);
                //}
            }

            //Contributer Details For Rights Team
            $scope.ContributorName = [];
            $scope.ContractNameList = [];
            for (var c = 0; c < $scope.ContributorList.length; c++) {
                $scope.ContractNameList.push(c);
                $scope.ContributorName.push($scope.ContributorList[c].Name);
            }

            if ($scope.ContributorList.length > 0) {
                $scope.Contributor = 1;
            }
            else {
                $scope.Contributor = 0;
                $scope.ContractNameList.push(1);
            }
            //End Contributer Details

            $scope._ContractAgreement = _ContractDetails.data._ContractAgreement;
            if (_ContractDetails.data._ContractAgreement != null) {

                // Checked applied later to allow rights user to update agreement until status is not Received
                    if (_ContractDetails.data._ContractAgreement.contractstatus == "Issued") {
                        $scope.AgreementStatus = _ContractDetails.data._ContractAgreement.contractstatus != null ? _ContractDetails.data._ContractAgreement.contractstatus : "---";
                        $scope.AgreementDate = _ContractDetails.data._ContractAgreement.AgreementDate != null ? _ContractDetails.data._ContractAgreement.AgreementDate : "---";
                        $scope.SignedcontracDate = _ContractDetails.data._ContractAgreement.signedcontractsentdate != null ? _ContractDetails.data._ContractAgreement.signedcontractsentdate : "---";
                        $scope.contractRecieved = _ContractDetails.data._ContractAgreement.SignedContractreceived != null ? _ContractDetails.data._ContractAgreement.SignedContractreceived : "---";
                        $scope.AuthorCopiesSend = _ContractDetails.data._ContractAgreement.Authorcopiessentdate != null ? _ContractDetails.data._ContractAgreement.Authorcopiessentdate : "---";
                        $scope.CotributorCopiessend = _ContractDetails.data._ContractAgreement.Contributorcopiessentdate != null ? _ContractDetails.data._ContractAgreement.Contributorcopiessentdate : "---";
                        $scope.ContractRemarks = _ContractDetails.data._ContractAgreement.remarks != "" ? _ContractDetails.data._ContractAgreement.remarks : "---";
                    }
                    else {
                        $scope.AgreementStatus = _ContractDetails.data._ContractAgreement.contractstatus;
                        $scope.AgreementDate = _ContractDetails.data._ContractAgreement.AgreementDate;
                        $scope.SignedcontracDate = _ContractDetails.data._ContractAgreement.signedcontractsentdate;
                        $scope.contractRecieved = _ContractDetails.data._ContractAgreement.SignedContractreceived;
                        $scope.AuthorCopiesSend = _ContractDetails.data._ContractAgreement.Authorcopiessentdate;
                        $scope.CotributorCopiessend = _ContractDetails.data._ContractAgreement.Contributorcopiessentdate;
                        $scope.ContractRemarks = _ContractDetails.data._ContractAgreement.remarks;
                    }

                    $scope.CancelDate = _ContractDetails.data._ContractAgreement.cancellationdate;
                    $scope.CancellationRemarks = _ContractDetails.data._ContractAgreement.Cancellationreason;
                    $scope.AgreementId = _ContractDetails.data._ContractAgreement.AgreementId;
                    $scope.EffectiveDate = _ContractDetails.data._ContractAgreement.EffectiveDate;
                    $scope.PeriodinMonth = _ContractDetails.data._ContractAgreement.PeriodinMonth;
                    $scope.ExpiryDate = _ContractDetails.data._ContractAgreement.ExpiryDate;
                     //$scope.DocumentList = _ContractDetails.data._agreementDoc;
                }

                else {
                    $scope.ContributorAgreement = "No";
                }

            if (_ContractDetails.data._agreementDoc != null) {
                for (var i = 0; i < _ContractDetails.data._agreementDoc.length; i++) {
                    if (_ContractDetails.data._agreementDoc[i].DocumentTypeId == 1) {
                        $scope.AgreementDoc.push(_ContractDetails.data._agreementDoc[i]);
                    }
                    else {

                        $scope.ContributorDoc.push(_ContractDetails.data._agreementDoc[i]);
                        $scope.ContributorAgreement = "Yes";
                    }
                }
            }
            if ($scope.ContributorDoc.length == 0 && $scope.AgreementId != undefined) {
                $scope.ContributorAgreement = "No";
            }



            $scope.getImpressionDetails(_ContractDetails.data._AuhtorContract.ProductId, null, $scope.ContractId);


            /*this section is used to populate the section of contract agreement insert after the contrct already created*/

            setTimeout(function () {
                //fetch Kit Details List
                app.expandControllerKitISBNLIst($scope, AJService);
                angular.element(document.getElementById('angularid')).scope().KitISBNDetailsList(_ContractDetails.data._AuhtorContract.ProductId);
            }, 300);

        }, function () {
            //alert('Error in getting Author Contract Detail');
        });
    }



    $scope.calcTotal = function (AuthorId, SubsidiaryId) {
        for (var i = 0; i < $scope._subsidiaryListDataDisplay.length; i++) {
            if ($scope._subsidiaryListDataDisplay[i].subsidiaryid == SubsidiaryId && $scope._subsidiaryListDataDisplay[i].authorId == AuthorId) {
                return $scope._subsidiaryListDataDisplay[i].Percentage;
            }
        }

    }
    $scope.calcTotalPer = function (SubsidiaryId) {
        var _ttl = 0;
        var oupPercentage = 0;
        for (var i = 0; i < $scope._subsidiaryListDataDisplay.length; i++) {
            if ($scope._subsidiaryListDataDisplay[i].subsidiaryid == SubsidiaryId) {
                _ttl = _ttl + $scope._subsidiaryListDataDisplay[i].Percentage
                oupPercentage = parseFloat($scope._subsidiaryListDataDisplay[i].OupPercentage)
            }
        }
        return parseFloat(_ttl) + oupPercentage;
    }
    /******************************************************************************
    *******************************************************************************
    Created By  :  Saddam
    Created on  :  05/07/2016
    Created For :  Insert data for Addendum file Upload

    *******************************************************************************
    *******************************************************************************/

    $scope.AddendumUpload = function () {
        var array = [];
        var Array1 = [];

        if ($('#hid_Uploads').val() != "") {
            var _FileName = $('#hid_Uploads').val().split(",")
            var Index = 0
            var k = 0;
            $('.dropZone').each(function () {

                array = [];
                var FileName = "";
                var _ttlFile = $(this).find('.fileNameClass').length;
                for (var i = 0; i < _ttlFile; i++) {
                    array[i] = $($(this).find('.fileNameClass')[i]).val();
                    FileName = FileName + _FileName[Index] + ",";
                    Index++;
                }
                if (k == 0) {
                    $('#hid_UploadsFile1').val(FileName);
                    Array1 = array;
                }

                k++;
            })
        }

        var AuthorContractRoyality = [];

        $(".RoyaltyChange").each(function (index, values) {
            var obj = $(this);
            var i = 0, j = 0;

            $(obj).find('.RoyaltySlab tr:not(:has(th))').each(function () {

                AuthorContractId = $('.RoyaltyChange').find('input:hidden')[j].value
                
                if ($(this).find('select[name$=SubProductType]').val() == "") {
                    return true;
                }

                    AuthorContractRoyality[i] =
                    {
                        Id: $(this).find('input[name*=SubProductTypeId]').val(),
                        AuthorContractId: $('#hid_AuthorContractId').val(),
                        ProductSubTypeId: $(this).find('select[name$=SubProductType]').val(),
                        copiesfrom: $(this).find('input[name$=CopiesFrom]').val(),
                        copiesto: $(this).find('input[name$=CopiesTo]').val(),
                        percentage: $(this).find('input[name$=RyPercentage]').val(),
                        AuthorId: $(this).parent().parent().parent().parent().parent().parent().parent().find('[name$=hid_authorid_addendum]').val(),
                    }
                    i++;
            });
            j++;
        });
        
        var _addendumId = 0;
        if ($('#hid_addendumIdForView').val() != undefined && $('#hid_addendumIdForView').val() != "" && $('#hid_addendumIdForView').val() != null) {
            _addendumId = $('#hid_addendumIdForView').val();
        }

        blockUI.start();

        var Addendum = {
            Id: _addendumId,
            DocumentName: Array1,
            UploadFile: $("#hid_Uploads").val(),
            AuthorContrctId: $('#hid_AuthorContractId').val(),
            EnteredBy: $("#enterdBy").val(),
            AddendumDate: convertDate($('.AddendumDiv').find('input[name$=AddendumDate]').val()),
            ExpiryDate: $("input[name*=ExpiryDate]").val() != "" && $("input[name*=ExpiryDate]").val() != undefined ? convertDate($("input[name*=ExpiryDate]").val()) : null,

            AddendumType: $('.AddendumDiv').find('select[name$=AddendumType]').val(),
            Periodofagreement: 0,// $('.AddendumDiv').find('input[name$=Periodofagreement]').val(),
            Remarks: $('.AddendumDiv').find('input[name$=Remarks]').val(),
            //SameAsEntery: $scope.SameasEntry == true ? 'Y' : 'N',
            SameAsEntery: $('.Royalty').find('input[name=SameasEntry]').prop('checked') == true && $('.AddendumDiv').find('select[name$=AddendumType]').val() == 'R' ? 'Y' : 'N',
            AuthorContractRoyality: AuthorContractRoyality,
            SeriesCode: $('#hid_SeriesCode').val() == "" || $('#hid_SeriesCode').val() == null ? null : $('#hid_SeriesCode').val()
        };

        SweetAlert.swal({
            title: "Are you sure?",
            text: "",
            type: "info",
            showCancelButton: true,
            confirmButtonColor: "#8CD4F5",
            confirmButtonText: "Yes",
            closeOnConfirm: false,
            closeOnCancel: true
        },
   function (Confirm) {
       if (Confirm) {

           var AddendumStatus = AJService.PostDataToAPI('AuthorContact/InsertAddendumUpload', Addendum);
           //var AddendumStatus = AJService.PostDataToAPI('ProudctMaster/InsertAddendumUpload', Addendum);
           AddendumStatus.then(function (msg) {
               if (msg.data == "OK") {
                   SweetAlert.swal({
                       title: "",
                       text: "Addendum Upload successfully.",
                       type: "success"
                   },
                      function () {
                          setTimeout(function () {
                              //window.location.href = window.location.href;
                              if ($('#hid_addendumView').val().toLowerCase() == 'addendumupdate' && $('#hid_SeriesCode').val() != undefined && $('#hid_SeriesCode').val() != null && $('#hid_SeriesCode').val() != "") {
                                  location.href = GlobalredirectPath + "Contract/AuthorContract/view?SeriesCode=" + $('#hid_SeriesCode').val() + "&For=AddendumView";
                              }
                              else if ($('#hid_addendumView').val().toLowerCase() == 'addendumupdate') {
                                  location.href = GlobalredirectPath + "Contract/AuthorContract/view?Id=" + $('#hid_AuthorContractId').val() + "&For=AddendumView";
                              }
                              else if ($('#hid_AuthorContractId').val() != 'undefined' || $('#hid_AuthorContractId').val() != '') {
                                  location.href = GlobalredirectPath + "Contract/AuthorContract/view?Id=" + $('#hid_AuthorContractId').val() + "&For=View";
                              }
                              else {
                                  location.href = GlobalredirectPath + "Contract/AuthorContract/view?SeriesCode=" + $('#hid_SeriesCode').val() + "&For=View";
                              }
                          }, 2000)

                      });
               }
               else {
                   SweetAlert.swal("Error!", "Error in Application Try Again !", "", "error");
               }
           },
           function () {
               alert('Please validate details');
           });
           blockUI.stop();
       }
   });

    }



    /************************************************************************
    Created By  :    Dheeraj sharma
    Created on  :    1st july 2016
    Created For :    Submit the extra information of agreement when open in
                     view mode access only for r ights department
    ************************************************************************/
    $scope.SubmitAgreementForm = function () {

        $scope.submitted = true;

        if ($("#AgreementDateid").val() != "" && $("input[type=radio][name*=AgreementStatus]:checked").val() == "Issued") {
            $("#AgreementDateid").closest(".form-group").removeClass("has-error");
            $("#AgreementDateid").closest(".form-group").find(".help-block").find("p").addClass("ng-hide");
            $("#AgreementDateid").closest(".form-group").find(".help-block").find("p").removeClass("ng-show");
            if ($('#dropZone0').find('.fileNameClass').length == 0 && $scope.AgreementId == undefined) {
                SweetAlert.swal("Validation !", "Please upload contract");
                $scope.userForm.$valid = false;
                return false;
            }
        }

        if ($("input[type=radio][name*=AgreementStatus]:checked").val() == "Cancelled") {
            if ($("input[name*=CancelDate]").val() == "") {
                $("input[name*=CancelDate]").closest(".form-group").addClass("has-error");
                $("input[name*=CancelDate]").closest(".CancelDate").next().find("p").addClass("ng-show");
                $("input[name*=CancelDate]").closest(".CancelDate").next().find("p").removeClass("ng-hide");
            }
            else {
                $("input[name*=CancelDate]").closest(".form-group").removeClass("has-error");
                $("input[name*=CancelDate]").closest(".CancelDate").next().find("p").removeClass("ng-show");
                $("input[name*=CancelDate]").closest(".CancelDate").next().find("p").addClass("ng-hide");
            }
            if ($("[name*=CancellationRemarks]").val() == "") {
                $("[name*=CancellationRemarks]").closest(".form-group").addClass("has-error");
                $("[name*=CancellationRemarks]").closest(".form-group").find("p").addClass("ng-show");
                $("[name*=CancellationRemarks]").closest(".form-group").find("p").removeClass("ng-hide");
            }
            else {
                $("[name*=CancellationRemarks]").closest(".form-group").removeClass("has-error");
                $("[name*=CancellationRemarks]").closest(".form-group").find("p").removeClass("ng-show");
                $("[name*=CancellationRemarks]").closest(".form-group").find("p").addClass("ng-hide");
            }
        }

        if ($("input[type=radio][name*=AgreementStatus]:checked").val() == "Draft") {
            if ($("input[name*=CancelDate]").val() == "") {
                $("input[name*=CancelDate]").closest(".form-group").addClass("has-error");
                $("input[name*=CancelDate]").closest(".CancelDate").next().find("p").addClass("ng-show");
                $("input[name*=CancelDate]").closest(".CancelDate").next().find("p").removeClass("ng-hide");
            }
            else {
                $("input[name*=CancelDate]").closest(".form-group").removeClass("has-error");
                $("input[name*=CancelDate]").closest(".CancelDate").next().find("p").removeClass("ng-show");
                $("input[name*=CancelDate]").closest(".CancelDate").next().find("p").addClass("ng-hide");
            }
            if ($("[name*=CancellationRemarks]").val() == "") {
                $("[name*=CancellationRemarks]").closest(".form-group").addClass("has-error");
                $("[name*=CancellationRemarks]").closest(".form-group").find("p").addClass("ng-show");
                $("[name*=CancellationRemarks]").closest(".form-group").find("p").removeClass("ng-hide");
            }
            else {
                $("[name*=CancellationRemarks]").closest(".form-group").removeClass("has-error");
                $("[name*=CancellationRemarks]").closest(".form-group").find("p").removeClass("ng-show");
                $("[name*=CancellationRemarks]").closest(".form-group").find("p").addClass("ng-hide");
            }
        }


        errorDiv = document.getElementById("fileid");
        errorDiv.innerHTML = "";
        errormsg = "";

        if ($("input[type=radio][name*=AgreementStatus]:checked").val() != "Cancelled" && $("input[type=radio][name*=AgreementStatus]:checked").val() != "Draft" && $("input[type=radio][name*=AgreementStatus]:checked").val() != "Pending") {

            var errorDiv;
            var errormsg = '';
            $scope.msg = "";
            FileNameArray = [];
            FileNameArray = $('#dropZone0').find('.fileNameClass');
            var array = [];

            if (FileNameArray.length == 0) {

                if ($scope.AgreementDoc.length == 0) {

                    errorDiv = document.getElementById("fileid");
                    errorDiv.innerHTML = "Please select a file";
                    errormsg = "Please select a file";
                    $scope.userForm.$valid = false;
                }
            }
            else {
                FileNameArray.each(function () {
                    array.push(
                   $(this).val());

                    for (i = 0; i < array.length; i++) {
                        if (array[i] == "") {
                            errorDiv = document.getElementById("fileid");
                            errorDiv.innerHTML = "Please enter file name";
                            errormsg = "Please enter file name";
                            $scope.userForm.$valid = false;

                        }
                        else {
                            $scope.userForm.$valid = true;
                        }
                    }
                });
            }

        }

        if ($scope.ContributorAgreement == "Yes") {
            var errorDiv;
            var errormsg = '';
            $scope.msg = "";
            FileNameArray = [];
            FileNameArray = $('#dropZone1').find('.fileNameClass');
            FileNameArray.each(function () {
                array.push(
               $(this).val()
           );

                for (i = 0; i < array.length; i++) {
                    if (array[i] == "") {
                        $($("[id*=fileid]")[1]).html("Please enter file name");
                        errormsg = "Please enter file name";
                        $scope.userForm.$valid = false;

                    }
                    else {
                        $scope.userForm.$valid = true;
                    }

                }

            });
        }


        if ($scope.ValidateRyaltySlab() == 1) {
            $scope.userForm.$valid = false;
        }

        $scope.checkdate('#AddendumDate');

        if ($('form[name$=userForm]').find(".has-error:visible").length > 0) {

            $scope.userForm.$valid = false;
        }


        if ($scope.userForm.$valid) {
            if ($('#hid_Addendum').val() != "" && typeof ($('#hid_Addendum').val()) !== "undefined") {
                $scope.AddendumUpload();
                return;
            }
            else {
                $scope.ContractAgreement();
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
        else {
            return false;
        }
    };

    $scope.ContractAgreement = function () {
        /*=============================================================================================================
        This array will used to insert data into the AuthorContractContributor for multiple contributor
        ==============================================================================================================*/
        var ContributorName = []
        $('input[name$=ContributorName]').each(function (index, values) {
            if ($(this).val() == "") {
                return true;
            }
            ContributorName[index] =
            {
                Contributor: $(this).val(),
            };

        });
        /*=============================================================================================================
        End of array
        ==============================================================================================================*/

        var ContributorFileName = "";
        var AgreementFileName = "";
        if ($("input[type=radio][name*=ContributorAgreement]:checked").val() == "Yes") {
            $("#dropZone1").find('.fileNameClass').each(function () {
                ContributorFileName = ContributorFileName + $(this).val() + ",";
            });
        }
        if ($("input[type=radio][name*=AgreementStatus]:checked").val() != "Cancelled" && $("input[type=radio][name*=AgreementStatus]:checked").val() != "Draft") {
            $("#dropZone0").find('.fileNameClass').each(function () {

                AgreementFileName = AgreementFileName + $(this).val() + ",";
            });
        }
        var Agreement =
        {
            Id: $scope.AgreementId,
            ContractId: $scope.ContractId,
            SeriesCode: $scope.SeriesCode,
            productIds: $scope.productIds,
            ContractStatus: $("input[type=radio][name*=AgreementStatus]:checked").val(),
            PeriodOfAgreement: $("input[name*=PeriodOfAgreementAgreement]").val(),
            ExpiryDate: $("input[id*=ExpiryDate]").val() != "" ? convertDate($("input[id*=ExpiryDate]").val()) : null,
            EffectiveDate: $("input[id*=EffectiveDate]").val() != "" ? convertDate($("input[id*=EffectiveDate]").val()) : null,
            AgreementDate: $("input[name*=AgreementDate]").val() != "" ? convertDate($("input[name*=AgreementDate]").val()) : null,
            SignedcontracDate: $("input[name*=SignedcontracDate]").val() != "" ? convertDate($("input[name*=SignedcontracDate]").val()) : null,
            contractRecieved: $("input[name*=contractRecieved]").val() != "" ? convertDate($("input[name*=contractRecieved]").val()) : null,
            //AuthorCopiesSend: $("input[name*=AuthorCopiesSend]").val() != "" ? convertDate($("input[name*=AuthorCopiesSend]").val()) : null,
            AuthorCopiesSend: null,
            ContractRemarks: $("input[name*=ContractRemarks]").val(),
            CancelDate: $("input[name*=CancelDate]").val() != "" ? convertDate($("input[name*=CancelDate]").val()) : null,
            //CotributorCopiessend: $("input[name*=CotributorCopiessend]").val() != "" ? convertDate($("input[name*=CotributorCopiessend]").val()) : null,
            CotributorCopiessend: null,
            CancellationRemarks: $("[name$=CancellationRemarks]").val(),
            AgreementRemarks: $("[name$=ContractRemarks]").val(),
            ContributorAgreement: $("input[type=radio][name*=ContributorAgreement]:checked").val(),
            Doc: $("#hid_Uploads").val(),
            AgreementFileName: AgreementFileName,
            ContributorFileName: ContributorFileName,
            EnteredBy: $("input[type=hidden][id^=enterdBy]").val(),
            ContributorName: ContributorName,
        }

        SweetAlert.swal({
            title: "Are you sure?",
            text: "",
            type: "info",
            showCancelButton: true,
            confirmButtonColor: "#8CD4F5",
            confirmButtonText: "Yes",
            closeOnConfirm: false,
            closeOnCancel: true
        },
    function (Confirm) {
        if (Confirm) {


            var ContractAgreement = AJService.PostDataToAPI("AuthorContact/ContractAgreement", Agreement);
            ContractAgreement.then(function (ContractAgreement) {
                if (ContractAgreement.data == "OK") {
                    SweetAlert.swal({
                        title: "Success",
                        text: "Assignment contract details, Updated successfully.",
                        type: "success"
                    },
                       function () {
                           setTimeout(function () {
                               //location.href = $(".backtolist").find("a").attr("href");
                               if (Agreement.ContractId != 'undefined' && Agreement.ContractId != null && Agreement.ContractId != '') {
                                   location.href = GlobalredirectPath + "Contract/AuthorContract/view?Id=" + Agreement.ContractId + "&For=View";
                               }
                               else {
                                   location.href = GlobalredirectPath + "Contract/AuthorContract/view?SeriesCode=" + Agreement.SeriesCode + "&For=View";
                               }
                           }, 1000);
                           // $scope.getDocumentList();

                       });


                }
                else {
                    SweetAlert.swal("Error!", ContractAgreement.data, "", "error");
                }
            });

        }
    });


    };


    function convertDate(date) {
        if (date != "" & date != undefined) {
            var datearray = date.split("/");
            return datearray[1] + '/' + datearray[0] + '/' + datearray[2];
        }
        else
            return ""
    }



    $scope.RemoveDocumentLinkById = function (docid, file) {
        var AuthorDocument = { Id: docid, EnteredBy: $("#enterdBy").val() };
        var DeleteDocument = AJService.PostDataToAPI("AuthorContact/RemoveAuhtorDocumentLink", AuthorDocument);

        DeleteDocument.then(function (msg) {
            if (msg.data != "Deleted") {
                SweetAlert.swal("Oops...", "Please retry!", "error");
            }
            else {
                $scope.getDocumentList();
                var obj = {};
                obj.filename = file;
                $.ajax({
                    cache: false,
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    url: $("#hid_documentDeleteUrl").val(),
                    data: JSON.stringify(obj),
                    dataType: "json",
                    success: function (result) {
                        if (result == "Deleted") {

                            //$scope.EditOtherContractData($('#hid_OtherContractId').val());
                            $scope.getDocumentList();
                        }

                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                    }
                });


            }
        }, function () {

            SweetAlert.swal("Oops...", "Please retry!", "error");

        });
    }

    $scope.getDocumentList = function () {
        var getDocumentList = AJService.GetDataFromAPI("AuthorContact/getAlltheDocument?agreementid=" + $scope.AgreementId + "");
        getDocumentList.then(function (getDocumentList) {
            $scope.DocumentList = getDocumentList.data._agreementDoc;
        });
    };

    $scope.RemoveAddendumFileUploadById = function (docid, file) {
        var AuthorDocument = { Id: docid, EnteredBy: $("#enterdBy").val() };
        var DeleteDocument = AJService.PostDataToAPI("AuthorContact/RemoveAddendumFileUpload", AuthorDocument);

        DeleteDocument.then(function (msg) {
            if (msg.data != "Deleted") {
                SweetAlert.swal("Oops...", "Please retry!", "error");
            }
            else {
                $scope.getAddendumDocumentList();
                var obj = {};
                obj.filename = file;
                $.ajax({
                    cache: false,
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    url: $("#hid_documentDeleteUrl").val(),
                    data: JSON.stringify(obj),
                    dataType: "json",
                    success: function (result) {
                        if (result == "Deleted") {

                            //$scope.EditOtherContractData($('#hid_OtherContractId').val());
                            $scope.getAddendumDocumentList();
                        }

                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                    }
                });


            }
        }, function () {

            SweetAlert.swal("Oops...", "Please retry!", "error");

        });
    }

    $scope.getAddendumDocumentList = function () {
        var _addendumId = 0;
        if ($('#hid_addendumIdForView').val() != undefined && $('#hid_addendumIdForView').val() != "" && $('#hid_addendumIdForView').val() != null) {
            _addendumId = $('#hid_addendumIdForView').val();
        }

        var getAddendumDocumentList = AJService.GetDataFromAPI("AuthorContact/getAlltheAddendumDocument?ContractId=" + $scope.ContractId + "&addendumId=" + _addendumId + "");
        getAddendumDocumentList.then(function (mdt) {
            if (mdt.data != null) {
                $scope.AddendumDocumentList = mdt.data._addendumFile;

                mstr_AddendumValue = mdt.data._addendumFile;
                if ($('#hid_User').val() == "admin" || $('#hid_User').val() == "Super Admin") {
                    $scope.documentshow = true;
                }
                else {

                    $scope.documentshow = true;
                    $scope.documentshow = true;
                    // $scope.UploadFIleReq = false;
                }
            }
            else {
            }
        })

    };

    $scope.getAddendumDocumentListBySeries = function () {
        var SeriesCode = $('#hid_SeriesCode').val();
        var getAddendumDocumentList = AJService.GetDataFromAPI("AuthorContact/getAlltheAddendumDocumentBySeries?SeriesCode=" + SeriesCode, null);
        getAddendumDocumentList.then(function (mdt) {
            if (mdt.data != null) {

                $scope.AddendumDocumentList = mdt.data._addendumFile;
                mstr_AddendumValue = mdt.data._addendumFile;
                if ($('#hid_User').val() == "admin" || $('#hid_User').val() == "Super Admin") {
                    $scope.documentshow = true;
                }
                else {

                    $scope.documentshow = true;
                    $scope.documentshow = true;
                    // $scope.UploadFIleReq = false;
                }
            }
            else {
            }
        }
    )
    };

    $scope.AddendumViewOnly = false;
    $scope.AddendumViewDataOnly = false;
    if ($('#hid_addendumView').val() == "") {
        $scope.AddendumViewOnly = true;
    }
    $scope.getAddendumDetails = function () {
        var _addendumId = 0;
        if ($('#hid_addendumIdForView').val() != undefined && $('#hid_addendumIdForView').val() != "" && $('#hid_addendumIdForView').val() != null) {
            _addendumId = $('#hid_addendumIdForView').val();
        }

        var getAddendumDetail = AJService.GetDataFromAPI("AuthorContact/getAddendumDetails?ContractId=" + $scope.ContractId + "&addendumId=" + _addendumId, null);
        getAddendumDetail.then(function (mdt) {
            if (mdt.data != null && mdt.data != "") {
                if ($('#hid_Addendum').val() == 'AddendumUpdate' && _addendumId != 0) {
                    $scope.AddendumViewDataOnly = false;
                    
                    if (navigator.userAgent.search("Safari") >= 0 && navigator.userAgent.search("Chrome") < 0) {
                        $scope.AddendumDate = mdt.data.AddendumDate == null ? '' : convertDateForSafari(mdt.data.AddendumDate);
                    }
                    else {
                        $scope.AddendumDate = mdt.data.AddendumDate == null ? '' : convertDateDDMMYYYY(new Date(mdt.data.AddendumDate));
                    }

                    if(mdt.data.AddendumType != null){
                        $('[name*=AddendumType] option[value=' + mdt.data.AddendumType + ']').attr("selected", "selected");

                        if (mdt.data.AddendumType == 'T') {
                            $('.Royalty').hide();
                            $('.Term').show();
                            $('.RoyaltyChange').hide();
                        }
                        else if (mdt.data.AddendumType == 'R') {
                            $('.Royalty').show();
                            $('.Term').hide();
                            $('.RoyaltyChange').hide();
                            $scope.SameasEntry = true;
                        }
                        else {
                            $('.Royalty').hide();
                            $('.Term').hide();
                            $('.RoyaltyChange').hide();
                        }
                    }

                    $scope.Periodofagreement = mdt.data.Periodofagreement == null ? '' : mdt.data.Periodofagreement;
                    $scope.Remarks = mdt.data.Remarks == null ? '' : mdt.data.Remarks;

                    setTimeout(function () { $scope.CalculateExpiry(); }, 200)
                }
                else {
                    $scope.AddendumViewDataOnly = true;
                    
                    if (navigator.userAgent.search("Safari") >= 0 && navigator.userAgent.search("Chrome") < 0) {
                        $scope.AddendumDateView = mdt.data.AddendumDate == null ? '--' : convertDateForSafari(mdt.data.AddendumDate);
                    }
                    else {
                        $scope.AddendumDateView = mdt.data.AddendumDate == null ? '--' : convertDateDDMMYYYY(new Date(mdt.data.AddendumDate));
                    }

                    $scope.AddendumTypeView = mdt.data.AddendumType == null ? '--' : mdt.data.AddendumType;
                    $scope.PeriodofagreementView = mdt.data.Periodofagreement == null ? '--' : mdt.data.Periodofagreement;
                    
                    if (navigator.userAgent.search("Safari") >= 0 && navigator.userAgent.search("Chrome") < 0) {
                        $scope.ExpiryDateView = mdt.data.ExpiryDate == null ? '--' : convertDateForSafari(mdt.data.ExpiryDate);
                    }
                    else {
                        $scope.ExpiryDateView = mdt.data.ExpiryDate == null ? '--' : convertDateDDMMYYYY(new Date(mdt.data.ExpiryDate));
                    }

                    $scope.RemarksView = mdt.data.Remarks == null ? '--' : mdt.data.Remarks;
                }
            }
        })

        //added by Prakash //Addendum - Basic Author Detail
        $scope.AddendumViewCheck = false;
        var getAddendum_BasicDetail = AJService.GetDataFromAPI("AuthorContact/getAddendum_BasicDetails?ContractId=" + $scope.ContractId + "&addendumId=" + _addendumId, null);
        getAddendum_BasicDetail.then(function (addendumDetal) {
            if ($('#hid_addendumNew').val() == 'new') {
                $scope.AuthorBox_Detail = addendumDetal.data._AuthorList;
                $scope.RoyaltyslabList_Detail = [];
            }
            else {
                if (addendumDetal.data._royalty.length > 0) {
                    if ($('#hid_Addendum').val() == 'AddendumUpdate' && _addendumId != 0) {
                        if ($('[name*=AddendumType]').val() == "R") {
                            $scope.SameasEntry = false;
                            $('.Royalty').show();
                            $('.Term').hide();
                            $('.RoyaltyChange').show();

                            $scope.AuthorBox_Detail = addendumDetal.data._AuthorList;
                            $scope.RoyaltyslabList_Detail = addendumDetal.data._royalty;

                            setTimeout(function () {
                                for (var i = 0; i < addendumDetal.data._royalty.length; i++) {
                                    $("#SubProductTypeId_" + i).val(addendumDetal.data._royalty[i].Id);
                                    if (addendumDetal.data._royalty[i].subproductTypeId != null) {
                                        $('#SubProductType_' + i + ' option[value=' + addendumDetal.data._royalty[i].subproductTypeId + ']').attr("selected", "selected");
                                    }
                                    $("#CopiesFrom_" + i).val(addendumDetal.data._royalty[i].CopiesFrom);
                                    $("#CopiesTo_" + i).val((addendumDetal.data._royalty[i].CopiesTo > 0 ? addendumDetal.data._royalty[i].copiesto : ""));
                                    $("#RyPercentage_" + i).val(addendumDetal.data._royalty[i].Percentage);
                                }
                            }, 200)
                        }
                    }
                    else {
                        $scope.AuthorBox_Detail = addendumDetal.data._AuthorList;
                        $scope.RoyaltyslabList_Detail = addendumDetal.data._royalty;
                        $scope.AddendumViewCheck = true;
                    }
                }
                else if ($('#hid_Addendum').val() == 'AddendumUpdate' && _addendumId == 0) {
                    $scope.AuthorBox_Detail = addendumDetal.data._AuthorList;
                    $scope.RoyaltyslabList_Detail = [];
                }
            }
        })

    };



    $scope.getAddendumDetailsBySeries = function () {
        var SeriesCode = $('#hid_SeriesCode').val();
        var getAddendumDetail = AJService.GetDataFromAPI("AuthorContact/getAddendumDetailsBySeries?SeriesCode=" + SeriesCode, null);
        getAddendumDetail.then(function (mdt) {
            if (mdt.data != null) {
                
                $scope.AddendumViewDataOnly = true;
                if (navigator.userAgent.search("Safari") >= 0 && navigator.userAgent.search("Chrome") < 0) {
                    $scope.AddendumDateView = mdt.data.AddendumDate == null ? '--' : convertDateForSafari(mdt.data.AddendumDate);
                }
                else {
                    $scope.AddendumDateView = mdt.data.AddendumDate == null ? '--' : convertDateDDMMYYYY(new Date(mdt.data.AddendumDate));
                }

                $scope.AddendumTypeView = mdt.data.AddendumType == null ? '--' : mdt.data.AddendumType;
                $scope.PeriodofagreementView = mdt.data.Periodofagreement == null ? '--' : mdt.data.Periodofagreement;
                if (mdt.data.ExpiryDate != null)
                    if (navigator.userAgent.search("Safari") >= 0 && navigator.userAgent.search("Chrome") < 0) {
                        $scope.ExpiryDateView = mdt.data.ExpiryDate == null ? '--' : convertDateForSafari(mdt.data.ExpiryDate);
                    }
                    else {
                        $scope.ExpiryDateView = mdt.data.ExpiryDate == null ? '--' : convertDateDDMMYYYY(new Date(mdt.data.ExpiryDate));
                    }
                $SeriesCodeView = mdt.data.SeriesCode == null ? '--' : mdt.data.SeriesCode;
                $scope.RemarksView = mdt.data.Remarks == "" || mdt.data.Remarks == null ? '--' : mdt.data.Remarks;

            }
        }
    )
    };

    $scope.checkdate = function (datetext) {
        if ($(datetext).val() != "") {
            $(datetext).closest(".form-group").removeClass("has-error");
            $(datetext).closest("div").next().find("p").removeClass("ng-show").addClass("ng-hide");
            $scope.AddendumDate = $(datetext).val();
            //$scope.CalculateExpiry(datetext);
        }
        else {
            $(datetext).closest(".form-group").addClass("has-error");
            $(datetext).closest("div").next().find("p").addClass("ng-show").removeClass("ng-hide");
        }
    }

    $scope.checkExpirydate = function (datetext) {
        if ($(datetext).val() != "") {
            //$(datetext).closest(".form-group").removeClass("has-error");
            //$(datetext).closest("div").next().find("p").removeClass("ng-show").addClass("ng-hide");
            $scope.ExpiryDate = $(datetext).val();
        }
        //else {
        //    $(datetext).closest(".form-group").addClass("has-error");
        //    $(datetext).closest("div").next().find("p").addClass("ng-show").removeClass("ng-hide");
        //}
    }

    $scope.CalculateExpiry = function (datetext) {
        PeriodIdValue = $('.AddendumDiv').find('input[name$=Periodofagreement]').val();
        var CDate = $('.AddendumDiv').find('input[name$=AddendumDate]').val();
        if (PeriodIdValue == undefined || PeriodIdValue == "0" || PeriodIdValue == "" || CDate == undefined || CDate == "" || CDate == "undefined//undefined") {
            $scope.ExpiryDate = "";
            return false;
        }

        var CurrentDate = new Date(convertDate($('.AddendumDiv').find('input[name$=AddendumDate]').val()));
        CurrentDate.setMonth(CurrentDate.getMonth() + parseInt(PeriodIdValue));
        var today = CurrentDate;
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!


        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd
        }
        if (mm < 10) {
            mm = '0' + mm
        }
        var today = dd + '/' + mm + '/' + yyyy;
        $scope.ExpiryDate = today;
        $('.AddendumDiv').find('input[name$=ExpiryDate]').val(today)
    }

    function convertDateDDMMYYYY(inputFormat) {
        function pad(s) { return (s < 10) ? '0' + s : s; }
        var d = new Date(inputFormat);
        return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
    }

    //Start json datetime conveter for safari
        //var d = convertDateForSafari("2017-10-03T00:00:00");
        //alert(d.getDate() + "/" + (d.getMonth() + 1) + "/" + d.getFullYear());
    function convertDateForSafari(input) {
        var parts = input.match(/(\d+)/g);
        var d = new Date(parts[0], parts[1] - 1, parts[2]);

        var dd = d.getDate() < 10 ? '0' + d.getDate() : d.getDate();
        var mm = (d.getMonth() + 1) < 10 ? '0' + (d.getMonth() + 1) : (d.getMonth() + 1);
        var yy = d.getFullYear();

        return (dd + "/" + mm + "/" + yy)
    }
   //End json datetime conveter for safari

    $scope.AddendumTypeChange = function (AddendumType) {
        if (AddendumType == 'T') {
            $('.Royalty').hide();
            $('.Term').show();
            $('.RoyaltyChange').hide();
        }
        else if (AddendumType == 'R') {
            $('.Royalty').show();
            $('.Term').hide();
            $('.RoyaltyChange').hide();
            $scope.SameasEntry = true;
        }
        else {
            $('.Royalty').hide();
            $('.Term').hide();
            $('.RoyaltyChange').hide();
        }
    }

    $scope.SameasEntryChange = function (SameasEntry) {
        if (SameasEntry == false) {
            $('.Royalty').show();
            $('.Term').hide();
            $('.RoyaltyChange').show();
            //$scope.CreateDynamicTable(3);
        }
        else {
            $('.Royalty').show();
            $('.Term').hide();
            $('.RoyaltyChange').hide();
        }
    };

    $scope.ValidateRoyaltySlabInsert = function () {
        obj = $(event.target);
        var _table = $(obj).closest(".RoyaltySlab");
        if ($(_table).find("select[name*=SubProductType]").find('option[value="' + $(obj).val() + '"]:selected').closest("select").length == 1) {
            $(obj).closest("tr").find("input[name*=CopiesFrom]").val(1);
            $(obj).closest("tr").find("input[name*=CopiesFrom]").attr("disabled", true);
        }
        else {

            var _copiesto = $($(_table).find("select[name*=SubProductType]").find('option[value="' + $(obj).val() + '"]:selected').parents("tr")[1]).find('input[name$=CopiesTo]').val()
            $(obj).closest("tr").find('input[name*=CopiesFrom]').val(parseInt(_copiesto) + 1);
            $(obj).closest("tr").find('input[name*=CopiesFrom]').attr("disabled", true);
        }

        if (obj.val() == "") {
            $(obj).closest("tr").find("input[name*=CopiesFrom]").val("");
            $(obj).closest("tr").find("input[name*=CopiesFrom]").removeAttr("disabled");
        }
    };

    function unique(list) {
        var result = [];
        $.each(list, function (i, e) {
            if ($.inArray(e, result) == -1) result.push(e);
        });
        return result;
    }

    $scope.ValidateRyaltySlab = function () {

        if ($('.AddendumDiv').find('[type=checkbox]:checked').val() != 'on') {

            if (unique($("select[name*=SubProductType]").map(function () { return $(this).val() }).get())[0] == "" && $('.AddendumDiv').find('select[name$=AddendumType]').val() == "R") {
                SweetAlert.swal("validation", "Please enter atleaset one royalty slab", "error");
                return 1;
                return false;
            }

            var returnstatus;
            var result = [];
            result = unique($("[id$=TblOwnerList]").find("select[name$=SubProductType]").map(function () { return $(this).find("option:selected").text(); }).get())
            for (var i = 0; i < result.length; i++) {
                $(".RoyaltySlab").find("select[name*=SubProductType]").find('option:selected').filter(":contains('" + result[i] + "')").parents("tr").each(function (index, value) {
                    var _lastTr = $(".RoyaltySlab").find("select[name*=SubProductType]").find('option:selected').filter(":contains('" + result[i] + "')").parents("tr:last")
                    if ($(_lastTr).find('input[name*=CopiesTo]').val() != "") {
                        if ($(_lastTr).find('input[name*=CopiesTo]').val() != 9999999) {
                            $scope.userForm.$valid = false;
                            SweetAlert.swal("Validation!", "Last Copies to should be blank !", "", "error");
                            $(_lastTr).find('input[name*=CopiesTo]').focus();
                            $scope.submitted = false;
                            returnstatus = true;
                            return false;
                        }

                    }
                    if ($(this).find('input[name=RyPercentage]').val() == "" && $(this).find("select[name*=SubProductType]").val() != "") {
                        $scope.userForm.$valid = false;
                        SweetAlert.swal("Validation!", "Please Enter Copies percentage !", "", "error");
                        $(this).find('input[name=RyPercentage]').focus();
                        $scope.submitted = false;
                        returnstatus = true;
                        return false;
                    }
                });
            }
            if (returnstatus) {
                return 1;
            }
        }
        else {
            return false;
        }
    }

    $scope.CreateDynamicTable = function (TotalNumberOfAuthor) {
        $scope.AuthorBox = [];
        if (TotalNumberOfAuthor == 0) {
            return false;
        }
        for (i = 0; i < TotalNumberOfAuthor; i++) {
            $scope.AuthorBox.push(i);
        }

    }

    /******************************************************************************
      *******************************************************************************
      Created By  :  Dheeraj Kumar Sharma
      Created on  : 06/06/2016
      Created For : This section will use to bind multiple productDetails when series contract is created
      *******************************************************************************
      *******************************************************************************/
    var mstr_ProductCode = '';
    var temp_ProductCode = [];
    $scope.ShowProductsDetailMultiple = function (Ids) {
        ////commented and added By Prakash on  04 July, 2017
        //var ProductStatus = AJService.GetDataFromAPI('ProductMaster/MultipleProductDetails?Ids=' + Ids, null);
        var ProductStatus = AJService.GetDataFromAPI('ProductMaster/MultipleProductDetails?Ids=' + '&SeriesCode=' + Ids + '&For=');
        ProductStatus.then(function (msg) {

            $scope.ProductDetailsListChild = msg.data;

            for (var j = 0; j < msg.data.length; j++) {
                if (mstr_ProductCode != msg.data[j].ProductCode) {
                    temp_ProductCode.push({
                        'ProductId': msg.data[j].ProductId,
                        'ProductCode': msg.data[j].ProductCode,
                        'projectcode': msg.data[j].projectcode,
                        'ProductCategory': msg.data[j].ProductCategory,
                        'WorkingProduct': msg.data[j].WorkingProduct,
                        'OupIsbn': msg.data[j].OupIsbn,
                        'ProjectedCurrencyId': msg.data[j].ProjectedCurrencyId,
                        'ProjectedPrice': msg.data[j].ProjectedPrice,
                        'AuthorName': msg.data[j].AuthorName,
                        'projectedpublishingdate': msg.data[j].projectedpublishingdate,
                        'ParentId': msg.data[j].ParentId,
                        'WorkingSubProduct': msg.data[j].WorkingSubProduct,
                        'ProductType': msg.data[j].ProductType,
                        'SubProductType': msg.data[j].SubProductType,
                        'thirdpartypermission': msg.data[j].thirdpartypermission,
                    });
                }

                mstr_ProductCode = msg.data[j].ProductCode;
            }

            $scope.ProductDetailsList = temp_ProductCode;

        }, function () {
            //alert('Error in getting Product list');
        });
    }
    /*******************************************************************************************************************************
    Created by  :   Dheeraj kumar sharma
    Created on  :   1st aug 2016
    Created for :   Getting the series Contrcact Detail based on Series Code
    *I*******************************************************************************************************************************/

    $scope.GetAuthorContractDetailsbySeriesId = function (SeriesCode) {

        if (SeriesCode == "" || SeriesCode == undefined) {
            return false;
        }

        $scope.getContractSeriesAddendumList(); //get previous addendun list

        $('.Royalty').hide();
        $('.Term').hide();
        $('.RoyaltyChange').hide();

        if ($('#hid_Addendum').val() != "" && typeof ($('#hid_Addendum').val()) !== "undefined") {

            if ($('#hid_User').val() == "admin" || $('#hid_User').val() == "Super Admin") {
                $scope.UploadFIleReq = true;
                $scope.documentshow = true;
            }
            else if ($('#hid_User').val() == "Rights") {
                $scope.UploadFIleReq = true;
                $scope.documentshow = true;
            }
            else if ($('#hid_User').val() == "Editorial") {
                $scope.documentshow = true;
            }
        }

        $scope.SubProductTypeList();
        $scope.getAddendumDocumentListBySeries();
        $scope.getAddendumDetailsBySeries();


        var _ContractDetails = AJService.GetDataFromAPI("AuthorContact/GetAuthorContractDetailsbySeriesId?SeriesCode=" + SeriesCode);
        _ContractDetails.then(function (_ContractDetails) {
            //$('#hid_productIds').val(_ContractDetails.data._AuhtorContract.ProductId)

            ////Commented and added by Prakash on 04 July, 2017
            //$scope.ShowProductsDetailMultiple(_ContractDetails.data._AuhtorContract.ProductId); 
            $scope.SeriesCode_Available = true;
            $scope.ShowProductsDetailMultiple(_ContractDetails.data._AuhtorContract.SeriesCode);

            //angular.element(document.getElementById('angularid')).scope().AuthorListProductBased(_ContractDetails.data._AuhtorContract.ProductId);
            // $scope.contractId = _ContractDetails.data._AuhtorContract.Id;
            
            $scope.HandledBy = _ContractDetails.data._AuhtorContract.HandledByName;
            $scope.productIds = _ContractDetails.data._AuhtorContract.ProductId;
            $scope.SeriesCode = _ContractDetails.data._AuhtorContract.SeriesCode
            $scope.FirstTime = 0;
            $scope.SeriesName = _ContractDetails.data._AuhtorContract.SeriesName;
            $scope.SeriesId = _ContractDetails.data._AuhtorContract.SeriesId;
            $scope.ByExecutive = _ContractDetails.data._AuhtorContract.HandleById;
            $scope.EntryDate = _ContractDetails.data._AuhtorContract.EntryDate;
            $scope.ContractDate = _ContractDetails.data._AuhtorContract.ContractDate;
            $scope.ContractType = _ContractDetails.data._AuhtorContract.ContractType;
            $scope.TemsOfCopyRight = _ContractDetails.data._AuhtorContract.TemsOfCopyRight;
            $scope.PeriodInMonth = _ContractDetails.data._AuhtorContract.PeriodInMonth;
            $scope.ContractExpiry = _ContractDetails.data._AuhtorContract.ContractExpiry;
            $scope.BuyBack = _ContractDetails.data._AuhtorContract.BuyBack != null ? _ContractDetails.data._AuhtorContract.BuyBack : "---";
            $scope.NatureOfWork = _ContractDetails.data._AuhtorContract.NatureOfWork;
            $scope.CopyRightOwner = _ContractDetails.data._AuhtorContract.CopyRightOwner;
            $scope.Teriterry = _ContractDetails.data._AuhtorContract.Teriterry;
            $scope.ThirdPartyPermission = _ContractDetails.data._AuhtorContract.ThirdPartyPermission;
            $scope.Amendment = _ContractDetails.data._AuhtorContract.Amendment;
            $scope.AmendmentRemarks = _ContractDetails.data._AuhtorContract.AmendmentRemarks;
            $scope.Restriction = _ContractDetails.data._AuhtorContract.Restriction == null ? "---" : _ContractDetails.data._AuhtorContract.Restriction;
            $scope.NoOfAuthors = _ContractDetails.data._AuhtorContract.NoOfAuthors;
            $scope.SubjectMatterandTreatment = _ContractDetails.data._AuhtorContract.SubjectMatterandTreatment == null ? "---" : _ContractDetails.data._AuhtorContract.SubjectMatterandTreatment;
            $scope.MinWords = _ContractDetails.data._AuhtorContract.MinWords == 0 || _ContractDetails.data._AuhtorContract.MinWords == null ? "---" : _ContractDetails.data._AuhtorContract.MinWords;
            $scope.MaxWords = _ContractDetails.data._AuhtorContract.MaxWords == 0 || _ContractDetails.data._AuhtorContract.MaxWords == null ? "---" : _ContractDetails.data._AuhtorContract.MaxWords;
            $scope.MinPages = _ContractDetails.data._AuhtorContract.MinPages == 0 || _ContractDetails.data._AuhtorContract.MinPages == null ? "---" : _ContractDetails.data._AuhtorContract.MinPages;
            $scope.MaxPages = _ContractDetails.data._AuhtorContract.MaxPages == 0 || _ContractDetails.data._AuhtorContract.MaxPages == null ? "---" : _ContractDetails.data._AuhtorContract.MaxPages;
            $scope.PriceType = _ContractDetails.data._AuhtorContract.PriceType == null ? "---" : _ContractDetails.data._AuhtorContract.PriceType;
            $scope.Currency = _ContractDetails.data._AuhtorContract.Currency != null ? _ContractDetails.data._AuhtorContract.Currency : "---";
            $scope.CurrencySymbol = _ContractDetails.data._AuhtorContract.CurrencySymbol != null ? _ContractDetails.data._AuhtorContract.CurrencySymbol : "---";
            $scope.Price = _ContractDetails.data._AuhtorContract.Price == 0 ? "---" : _ContractDetails.data._AuhtorContract.Price;
            $scope.MediumofDelivery = _ContractDetails.data._AuhtorContract.MediumofDelivery == null ? "---" : _ContractDetails.data._AuhtorContract.MediumofDelivery;
            $scope.Deliveryschedule = _ContractDetails.data._AuhtorContract.Deliveryschedule == "" ? "---" : _ContractDetails.data._AuhtorContract.Deliveryschedule;
            $scope.ProductRemarks = _ContractDetails.data._AuhtorContract.ProductRemarks == null || _ContractDetails.data._AuhtorContract.ProductRemarks == "" ? "---" : _ContractDetails.data._AuhtorContract.ProductRemarks;
            //$scope.MenuScriptDelivery = _ContractDetails.data._AuhtorContract.MenuScriptDelivery != "" ? _ContractDetails.data._AuhtorContract.MenuScriptDelivery : "---";
            $scope.ContributorList = _ContractDetails.data._contributor;
            $scope.MaterialSuppliedByAuthorList = _ContractDetails.data._MaterialDate;
            $scope.AuthorBox = _ContractDetails.data._AuthorList;
            $scope.MenuScriptDeliveryFormatList = _ContractDetails.data._ManuscriptDeliveryList;
            

            for (var i = 0; i < $scope.AuthorBox.length; i++) {
                $scope.authorNameAddendum[i] = $scope.AuthorBox[i].Name;
                $scope.authorIdAddendum[i] = $scope.AuthorBox[i].Id;
            }

            $scope.RoyaltyslabList = _ContractDetails.data._royalty;
            $scope.TblList = _ContractDetails.data.TblList;
            $scope.SubsidiaryListorigional = _ContractDetails.data._susidiaryRightsList;
            $scope.ttlSubsidiary = _ContractDetails.data._ttlSusidiary;
            for (i = 0; i < $scope.SubsidiaryListorigional.length; i++) {
                //if ($scope.SubsidiaryListorigional[i].OupPercentage != 100 && $scope.SubsidiaryListorigional[i].Percentage != 0) {
                    $scope._subsidiaryList[i] = $scope.SubsidiaryListorigional[i];
                    $scope._subsidiaryListDataDisplay.push($scope.SubsidiaryListorigional[i]);
                //}
            }
            $scope._ContractAgreement = _ContractDetails.data._ContractAgreement;
            if (_ContractDetails.data._ContractAgreement != null) {
                $scope.AgreementStatus = _ContractDetails.data._ContractAgreement.contractstatus != null ? _ContractDetails.data._ContractAgreement.contractstatus : "---";
                $scope.AgreementDate = _ContractDetails.data._ContractAgreement.AgreementDate != null ? _ContractDetails.data._ContractAgreement.AgreementDate : "---";
                $scope.SignedcontracDate = _ContractDetails.data._ContractAgreement.signedcontractsentdate != null ? _ContractDetails.data._ContractAgreement.signedcontractsentdate : "---";
                $scope.contractRecieved = _ContractDetails.data._ContractAgreement.SignedContractreceived != null ? _ContractDetails.data._ContractAgreement.SignedContractreceived : "---";
                $scope.AuthorCopiesSend = _ContractDetails.data._ContractAgreement.Authorcopiessentdate != null ? _ContractDetails.data._ContractAgreement.Authorcopiessentdate : "---";
                $scope.CotributorCopiessend = _ContractDetails.data._ContractAgreement.Contributorcopiessentdate != null ? _ContractDetails.data._ContractAgreement.Contributorcopiessentdate : "---";
                $scope.ContractRemarks = _ContractDetails.data._ContractAgreement.remarks != "" ? _ContractDetails.data._ContractAgreement.remarks : "---";
                $scope.CancelDate = _ContractDetails.data._ContractAgreement.cancellationdate;
                $scope.CancellationRemarks = _ContractDetails.data._ContractAgreement.Cancellationreason;
                $scope.AgreementId = _ContractDetails.data._ContractAgreement.AgreementId;
                $scope.EffectiveDate = _ContractDetails.data._ContractAgreement.EffectiveDate;
                $scope.PeriodinMonth = _ContractDetails.data._ContractAgreement.PeriodinMonth;
                $scope.ExpiryDate = _ContractDetails.data._ContractAgreement.ExpiryDate;
                //$scope.DocumentList = _ContractDetails.data._agreementDoc;


            }
           if (_ContractDetails.data._agreementDoc != null) {
                for (var i = 0; i < _ContractDetails.data._agreementDoc.length; i++) {
                    if (_ContractDetails.data._agreementDoc[i].DocumentTypeId == 1) {
                        $scope.AgreementDoc.push(_ContractDetails.data._agreementDoc[i]);
                    }
                    else {

                        $scope.ContributorDoc.push(_ContractDetails.data._agreementDoc[i]);
                        $scope.ContributorAgreement = "Yes";
                    }
                }
            }
            if ($scope.ContributorDoc.length == 0 && $scope.AgreementId != undefined) {
                $scope.ContributorAgreement = "No";
            }



            /*this section is used to populate the section of contract agreement insert after the contrct already created*/

        }, function () {
            //alert('Error in getting Author Contract Detail by SeriesId');
        });
    }

    $scope.addroyalslabbyJquery = function (obj) {

        var _trClone = $(obj).closest("tr").clone(true);
        _trClone.find('input').val("");
        _trClone.find('select').val("");
        _trClone.find('input').removeAttr("disabled");
        $(obj).closest(".RoyaltySlab").append(_trClone);
        $scope.RoyaltySlabManagement();

    }
    $scope.removeroyalslab = function (obj) {

        var _table = $(obj).closest(".RoyaltySlab");
        $(obj).closest("tr").remove();

        //if (_table.find("input").is(":disabled") == false) {
        //    $(_table.find("tr")[1]).find("input[name*=CopiesFrom]").prop("disabled", true);
        //    $(_table.find("tr")[1]).find("input[name*=CopiesFrom]").val(1);
        //}

        $scope.RoyaltySlabManagement();
    }
    $scope.RoyaltySlabManagement = function () {
        $('.AuthorBoxAddendum').each(function () {
            $(this).find(".RoyaltySlab").find("tr").find(".RoyaltySlabnotAdd").css("display", "none");
            $(this).find(".RoyaltySlab").find("tr").find(".RoyaltySlabnotRemove").css("display", "table-row");
            $(this).find(".RoyaltySlab").find("tr:last").find(".RoyaltySlabnotAdd").css("display", "table-row");
            $(this).find(".RoyaltySlab").find("tr:last").find(".RoyaltySlabnotRemove").css("display", "none");
            $(this).find(".RoyaltySlab").find("tr:last").find('input[name$=CopiesTo]').val("");
            $(this).find(".RoyaltySlab").find("tr").not("tr:first").each(function (Index, value) {
                $($(this).find("td")[0]).html(Index + 1);
            })

        });
    }

     /*******************************************************************************
        Created By  :  Dheeraj Kumar Sharma
        Created on  : 07/06/2016
        Created For : Calculate the expiry

    *******************************************************************************
    *******************************************************************************/
    //$scope.CalculateExpiryAgreement = function (PeriodIdValue) {

    //    debugger;
    //    $scope.AgreementDate = $("input[type=text][id*=AgreementDate]").val();

    //    if (PeriodIdValue == undefined || $("input[type=text][id*=AgreementDate]").val() == "") {
    //        $scope.ExpiryDate = "";
    //        return false;
    //    }

    //    var CurrentDate = DateFormat(convertDate($("input[type=text][id*=AgreementDate]").val()));
    //    CurrentDate.setMonth(CurrentDate.getMonth() + parseInt(PeriodIdValue));
    //    var today = CurrentDate;
    //    var dd = today.getDate();
    //    var mm = today.getMonth() + 1; //January is 0!

    //    var yyyy = today.getFullYear();
    //    if (dd < 10) {
    //        dd = '0' + dd
    //    }
    //    if (mm < 10) {
    //        mm = '0' + mm
    //    }
    //    var today = dd + '/' + mm + '/' + yyyy;
    //    $scope.ExpiryDate = today;
    //}
        
    //function DateFormat(inputFormat) {
    //    function pad(s) { return (s < 10) ? '0' + s : s; }
    //    return  new Date(inputFormat);
        
    //}




    $scope.GetViewAmendmentDocumentList = function (Id) {

        var getViewAmendmentDocumentDetail = AJService.GetDataFromAPI("AuthorContact/GetViewAmendmentDocumentList?Id=" + Id, null);
        getViewAmendmentDocumentDetail.then(function (msg) {
            if (msg.data != "") {
               
              
                if (msg.data.AuthorAmendmentDocumentDetails.DocumentName != null) {
                    var e1 = 0;
                    var d1 = 0;
                    var docNames1 = '';
                    var Docurl1 = '';
                    $scope.Docurl1 = [];

                    if (msg.data.AuthorAmendmentDocumentDetails.DocumentName != '') {

                        $scope.Pendingdocumentshow = true;
                        var docNames1 = msg.data.AuthorAmendmentDocumentDetails.DocumentName.slice(',');
                        var DName1 = msg.data.AuthorAmendmentDocumentDetails.DocumentName.slice(',');

                        var DId1 = msg.data.AuthorAmendmentDocumentDetails.DocumentIds.slice(',');

                        var Docurl1 = msg.data.AuthorAmendmentDocumentDetails.UploadFile.split(',');
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




            }
           
        }
    )
    };

    if ($('#hid_AuthorContractIdValue').val() != null && $('#hid_AuthorContractIdValue').val() != undefined)
    {
        $scope.GetViewAmendmentDocumentList($('#hid_AuthorContractIdValue').val());
    }

    $scope.btn_BackToList = function()
    {
        location.href = GlobalredirectPath + "Contract/AuthorContract/AuthorContractSearch?For=List";
    }

    $scope.btn_BackToListSeries = function () {
        location.href = GlobalredirectPath + "Contract/AuthorContract/AuthorContractSearch?For=SeriesList";
    }


    ////Added by Saddam on 17/08/2017
    //var _href = location.href.toLocaleLowerCase();
   
    //if (_href.trim().indexOf("authorcontract/view") > 0) {
    //    $scope.ProductDetailsThirdPartyPermsionMultView = true;
    //    $scope.ProductDetailsThirdPartyPermsionView = true;

    //} else if (_href.trim().indexOf("authorcontract/view?seriescode") > 0) {
    //    $scope.ProductDetailsThirdPartyPermsionMultView = true;
    //}
    //else if (_href.trim().indexOf("authorcontract/view?id") > 0) {
       
    //    $scope.ProductDetailsThirdPartyPermsionMultView = true;
    //}

    //else {
    //    $scope.ProductDetailsThirdPartyPermsionView = false;
    //    $scope.ProductDetailsThirdPartyPermsionMultView = false;
    //}
    ////ened by Saddam
   

    $scope.addContributorrow = function () {
        var i = $scope.ContractNameList.length + 1;
        $scope.ContractNameList.push(i);

    };

    $scope.ContributorNamevalid = true;
    $('input[name$=ContributorName]').each(function (index, values) {
        if ($(this).val() != "") {
            $('input[name$=ContributorName]').parent().removeClass('has-error');
            $scope.ContributorNamevalid = false;
        }
    });

    //for contract addendum list contract wise// added by prakash on 27 Sep, 2017
    $scope.ContractAddendumList = [];
    $scope.getContractAddendumList = function () {
        var getAddendumList = AJService.GetDataFromAPI("AuthorContact/getContractAddendumList?ContractId=" + $scope.ContractId, null);
        getAddendumList.then(function (addendumlist) {
            if (addendumlist.data.length > 0) {
                $scope.ContractAddendumList = addendumlist.data;
            }
        })

    };

        //for contract addendum list Series wise// added by prakash on 27 Sep, 2017
    $scope.getContractSeriesAddendumList = function () {
        var getAddendumList = AJService.GetDataFromAPI("AuthorContact/getContractAddendumList?SeriesCode=" + $('#hid_SeriesCode').val(), null);
        getAddendumList.then(function (addendumlist) {
            if (addendumlist.data.length > 0) {
                $scope.ContractAddendumList = addendumlist.data;
            }
        })

    };
     
    ////Get addendum data in popup
    $scope.ViewAuthorContractAddenDumDetailsList = function (Id, ContractId, seriesCode) {
        var _addendumId = 0;
        if (Id > 0) {
            _addendumId = Id;
        }

        if (ContractId == 0 && seriesCode != "" && seriesCode != null && seriesCode != undefined) {
            ////Series wise
            
            //------------------------------------
            var SeriesCode = seriesCode;
            var getAddendumDetail = AJService.GetDataFromAPI("AuthorContact/getAddendumDetailsBySeries?SeriesCode=" + SeriesCode + "&addendumId=" + _addendumId, null);
            getAddendumDetail.then(function (mdt) {
                if (mdt.data != null) {

                    $scope.AddendumViewDataOnly_ListView = false;

                    if (navigator.userAgent.search("Safari") >= 0 && navigator.userAgent.search("Chrome") < 0) {
                        $scope.AddendumDate_ListView = mdt.data.AddendumDate == null ? '' : convertDateForSafari(mdt.data.AddendumDate);
                    }
                    else {
                        $scope.AddendumDate_ListView = mdt.data.AddendumDate == null ? '' : convertDateDDMMYYYY(new Date(mdt.data.AddendumDate));
                    }

                    if (mdt.data.AddendumType != null) {
                        $('[name*=AddendumType] option[value=' + mdt.data.AddendumType + ']').attr("selected", "selected");

                        if (mdt.data.AddendumType == 'T') {

                        }
                        else if (mdt.data.AddendumType == 'R') {

                            $scope.SameasEntry_ListView = true;
                        }
                        else {

                        }
                    }

                    $scope.Periodofagreement_ListView = mdt.data.Periodofagreement == null ? '' : mdt.data.Periodofagreement;
                    $scope.Remarks_ListView = mdt.data.Remarks == null ? '' : mdt.data.Remarks;

                    setTimeout(function () { $scope.CalculateExpiry(); }, 200)

                    $scope.AddendumViewDataOnly_ListView = true;

                    if (navigator.userAgent.search("Safari") >= 0 && navigator.userAgent.search("Chrome") < 0) {
                        $scope.AddendumDateView_ListView = mdt.data.AddendumDate == null ? '--' : convertDateForSafari(mdt.data.AddendumDate);
                    }
                    else {
                        $scope.AddendumDateView_ListView = mdt.data.AddendumDate == null ? '--' : convertDateDDMMYYYY(new Date(mdt.data.AddendumDate));
                    }
                    $scope.AddendumTypeView_ListView = mdt.data.AddendumType == null ? '--' : mdt.data.AddendumType;
                    $scope.PeriodofagreementView_ListView = mdt.data.Periodofagreement == null ? '--' : mdt.data.Periodofagreement;

                    if (navigator.userAgent.search("Safari") >= 0 && navigator.userAgent.search("Chrome") < 0) {
                        $scope.ExpiryDateView_ListView = mdt.data.ExpiryDate == null ? '--' : convertDateForSafari(mdt.data.ExpiryDate);
                    }
                    else {
                        $scope.ExpiryDateView_ListView = mdt.data.ExpiryDate == null ? '--' : convertDateDDMMYYYY(new Date(mdt.data.ExpiryDate));
                    }

                    $scope.RemarksView_ListView = mdt.data.Remarks == null ? '--' : mdt.data.Remarks;

                }
            });

            //-----------------------------------------
            var getAddendumDocumentList = AJService.GetDataFromAPI("AuthorContact/getAlltheAddendumDocumentBySeries?SeriesCode=" + SeriesCode + "&addendumId=" + _addendumId, null);
            getAddendumDocumentList.then(function (mdt) {
                if (mdt.data != null) {
                    $scope.AddendumDocumentList_ListView = mdt.data._addendumFile;

                    mstr_AddendumValue_ListView = mdt.data._addendumFile;
                    if ($('#hid_User').val() == "admin" || $('#hid_User').val() == "Super Admin") {
                        $scope.documentshow_ListView = true;
                    }
                    else {

                        $scope.documentshow_ListView = true;
                        $scope.documentshow_ListView = true;

                    }
                }
                else {
                }
            });

            //----------------------------------------
            


        }
        else {
            ////ContractId Wise            

            //----------------------------
            var _ContractDetails = AJService.GetDataFromAPI("AuthorContact/GetAuthorContractDetails?Id=" + Id);
            _ContractDetails.then(function (_ContractDetails) {

                $scope.AuthorBox_ListNew = _ContractDetails.data._AuthorList;
                for (var i = 0; i < $scope.AuthorBox_ListNew.length; i++) {
                    $scope.authorNameAddendum_ListNew[i] = $scope.AuthorBox_ListNew[i].Name;
                    $scope.authorIdAddendum_ListNew[i] = $scope.AuthorBox_ListNew[i].Id;
                }

                $scope.RoyaltyslabList_ListView = _ContractDetails.data._royalty;
                $scope.TblList_ListView = _ContractDetails.data.TblList;
                $scope.SubsidiaryListorigional_ListView = _ContractDetails.data._susidiaryRightsList;
                $scope.ttlSubsidiary_ListView = _ContractDetails.data._ttlSusidiary;

                $scope.ContributorName_ListView = [];
                $scope.ContractNameList_ListView = [];

                $scope._ContractAgreement_ListView = _ContractDetails.data._ContractAgreement;

                if (_ContractDetails.data._ContractAgreement_ListView != null) {


                }
                else {
                    $scope.ContributorAgreement_ListView = "No";
                }


            }, function () {
                //alert('Error in getting Author Contract Detail');
            });


            //------------------------------
            var getAddendumDetail = AJService.GetDataFromAPI("AuthorContact/getAddendumDetails?ContractId=" + $scope.ContractId + "&addendumId=" + _addendumId, null);
            getAddendumDetail.then(function (mdt) {
                if (mdt.data != null && mdt.data != "") {

                    $scope.AddendumViewDataOnly_ListView = false;

                    if (navigator.userAgent.search("Safari") >= 0 && navigator.userAgent.search("Chrome") < 0) {
                        $scope.AddendumDate_ListView = mdt.data.AddendumDate == null ? '' : convertDateForSafari(mdt.data.AddendumDate);
                    }
                    else {
                        $scope.AddendumDate_ListView = mdt.data.AddendumDate == null ? '' : convertDateDDMMYYYY(new Date(mdt.data.AddendumDate));
                    }

                    if (mdt.data.AddendumType != null) {
                        $('[name*=AddendumType] option[value=' + mdt.data.AddendumType + ']').attr("selected", "selected");

                        if (mdt.data.AddendumType == 'T') {

                        }
                        else if (mdt.data.AddendumType == 'R') {

                            $scope.SameasEntry_ListView = true;
                        }
                        else {

                        }
                    }

                    $scope.Periodofagreement_ListView = mdt.data.Periodofagreement == null ? '' : mdt.data.Periodofagreement;
                    $scope.Remarks_ListView = mdt.data.Remarks == null ? '' : mdt.data.Remarks;

                    setTimeout(function () { $scope.CalculateExpiry(); }, 200)

                    $scope.AddendumViewDataOnly_ListView = true;

                    if (navigator.userAgent.search("Safari") >= 0 && navigator.userAgent.search("Chrome") < 0) {
                        $scope.AddendumDateView_ListView = mdt.data.AddendumDate == null ? '--' : convertDateForSafari(mdt.data.AddendumDate);
                    }
                    else {
                        $scope.AddendumDateView_ListView = mdt.data.AddendumDate == null ? '--' : convertDateDDMMYYYY(new Date(mdt.data.AddendumDate));
                    }
                    $scope.AddendumTypeView_ListView = mdt.data.AddendumType == null ? '--' : mdt.data.AddendumType;
                    $scope.PeriodofagreementView_ListView = mdt.data.Periodofagreement == null ? '--' : mdt.data.Periodofagreement;

                    if (navigator.userAgent.search("Safari") >= 0 && navigator.userAgent.search("Chrome") < 0) {
                        $scope.ExpiryDateView_ListView = mdt.data.ExpiryDate == null ? '--' : convertDateForSafari(mdt.data.ExpiryDate);
                    }
                    else {
                        $scope.ExpiryDateView_ListView = mdt.data.ExpiryDate == null ? '--' : convertDateDDMMYYYY(new Date(mdt.data.ExpiryDate));
                    }

                    $scope.RemarksView_ListView = mdt.data.Remarks == null ? '--' : mdt.data.Remarks;



                }
            })


            //--------------------------
            var getAddendumDocumentList = AJService.GetDataFromAPI("AuthorContact/getAlltheAddendumDocument?ContractId=" + ContractId + "&addendumId=" + Id + "");
            getAddendumDocumentList.then(function (mdt) {
                if (mdt.data != null) {
                    $scope.AddendumDocumentList_ListView = mdt.data._addendumFile;

                    mstr_AddendumValue_ListView = mdt.data._addendumFile;
                    if ($('#hid_User').val() == "admin" || $('#hid_User').val() == "Super Admin") {
                        $scope.documentshow_ListView = true;
                    }
                    else {

                        $scope.documentshow_ListView = true;
                        $scope.documentshow_ListView = true;

                    }
                }
                else {
                }
            })

            //---------------------
            $scope.AddendumViewCheck_ListView = false;
            var getAddendum_BasicDetail = AJService.GetDataFromAPI("AuthorContact/getAddendum_BasicDetails?ContractId=" + ContractId + "&addendumId=" + Id, null);
            getAddendum_BasicDetail.then(function (addendumDetal) {
                if ($('#hid_addendumNew').val() == 'new') {


                    $scope.AuthorBox_Detail_ListView = addendumDetal.data._AuthorList;
                    $scope.RoyaltyslabList_Detail = [];
                }
                else {
                    if (addendumDetal.data._royalty.length > 0) {

                        $scope.SameasEntry_ListView = false;
                        $('.Royalty').show();
                        $('.Term').hide();
                        $('.RoyaltyChange').show();

                        $scope.AuthorBox_Detail_ListView = addendumDetal.data._AuthorList;
                        $scope.RoyaltyslabList_Detail_ListView = addendumDetal.data._royalty;

                        setTimeout(function () {
                            for (var i = 0; i < addendumDetal.data._royalty.length; i++) {
                                $("#SubProductTypeId_" + i).val(addendumDetal.data._royalty[i].Id);
                                if (addendumDetal.data._royalty[i].subproductTypeId != null) {
                                    $('#SubProductType_' + i + ' option[value=' + addendumDetal.data._royalty[i].subproductTypeId + ']').attr("selected", "selected");
                                }
                                $("#CopiesFrom_" + i).val(addendumDetal.data._royalty[i].CopiesFrom);
                                $("#CopiesTo_" + i).val((addendumDetal.data._royalty[i].CopiesTo > 0 ? addendumDetal.data._royalty[i].copiesto : ""));
                                $("#RyPercentage_" + i).val(addendumDetal.data._royalty[i].Percentage);
                            }
                        }, 300)

                    } else {
                        $scope.AuthorBox_Detail_ListView = [];
                        $scope.RoyaltyslabList_Detail_ListView = [];
                    }

                }
            })
        }

    }



});



