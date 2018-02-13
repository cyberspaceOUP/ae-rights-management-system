
app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {
    app.expandControllerTopSearch($scope, AJService, $window);
    app.expandControllerA($scope, AJService, $window);

    app.expandControllerAuthorContractDetails($scope, AJService, $window);

    app.expandControllerProductLicense($scope, AJService, $window);

    app.expandControllerProductDetails($scope, AJService, $window);


    $scope.ProductSerach($('#hid_ProductId').val());

    setTimeout(function () {
        //fetch Kit Details List
        app.expandControllerKitISBNLIst($scope, AJService);
        angular.element(document.getElementById('angularid')).scope().KitISBNDetailsList($('#hid_ProductId').val());
    }, 300);

    if ($('#hid_Type').val() == "A") {
        $scope.AuthorContract($("#hid_AuthorContract").val());
        $scope.Req_ContractDeatil = true;
        $scope.Req_ProductLicense = false;
    }
    else if ($('#hid_Type').val() == "P") {

        $scope.Req_ProductLicense = true;
        $scope.Req_ContractDeatil = false;

        $scope.ProductLicenseSerach($("#hid_AuthorContract").val());
    }


    $scope.GetAuthorContractDetails = function () {

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

        $scope.GetRightsSellingDocumentList();
        $scope.GetRightsSellingUpdateList();

        $scope.RightsSellingDataView($('#hid_RightsSellingId').val());

        //angular.element(document.getElementById('angularid')).scope().GetRightsSellingDocumentList();
    }

    $scope.SetContractStatus = function (value) {

   

        $scope.disableErrorClass($('#updateRights').find('input[name=SignedContractReceivedDate]'));
        $scope.disableErrorClass($('#updateRights').find('input[name=DateofAgreement]'));
        $scope.disableErrorClass($('#updateRights').find('input[name=CancellationDate]'));
        $scope.disableErrorClass($('#updateRights').find('input[name=SignedContractSentDate]'));
    }

    $scope.SetSignedContractSentDate = function (datetext) {
        $scope.SignedContractSentDate = $(datetext).val();
        $scope.checkdate(datetext);
    }

    $scope.SetCancellationDate = function (datetext) {
        $scope.CancellationDate = $(datetext).val();
        $scope.checkdate(datetext);
    }

    $scope.SetDateofAgreement = function (datetext) {
        $scope.DateofAgreement = $(datetext).val();
        $scope.checkdate(datetext);
    }

    $scope.SetSignedContractReceivedDate = function (datetext) {
        $scope.SignedContractReceivedDate = $(datetext).val();
        $scope.checkdate(datetext);
    }

    $scope.checkdate = function (datetext) {
        if ($(datetext).val() != "") {
            $(datetext).closest(".form-group").removeClass("has-error");
            $(datetext).closest("div").next().find("p").removeClass("ng-show").addClass("ng-hide");
        }
        else {
            $(datetext).closest(".form-group").addClass("has-error");
            $(datetext).closest("div").next().find("p").addClass("ng-show").removeClass("ng-hide");
        }
    }

    $scope.disableErrorClass = function (value) {
        $(value).closest(".form-group").removeClass("has-error");
        $(value).closest("div").next().find("p").removeClass("ng-show").addClass("ng-hide");
    }

    $scope.GetRightsSellingDocumentList = function () {
        var getDocumentList = AJService.GetDataFromAPI("RightsSelling/GetRightsSellingDocumentList?RightsSellingId=" + $('#hid_RightsSellingId').val(), null);
        //var getAddendumDocumentList = AJService.GetDataFromAPI("AuthorContact/getAlltheAddendumDocument?ContractId=92",null);
        getDocumentList.then(function (mdt) {
            if (mdt.data != null) {

                $scope.DocumentList = mdt.data;
                //mstr_AddendumValue = mdt.data.DocumentFile;
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

    $scope.GetRightsSellingUpdateList = function () {
        var getAddendumDetail = AJService.GetDataFromAPI("RightsSelling/GetRightsSellingUpdateList?RightsSellingId=" + $('#hid_RightsSellingId').val(), null);
        getAddendumDetail.then(function (mdt) {
            if (mdt.data != '') {

                if (mdt.data.ContractStatus != '' && mdt.data.ContractStatus !=null)
                    $scope.ContractStatus_View = mdt.data.ContractStatus;
                else $scope.ContractStatus_View = '--';

                if (mdt.data.Date_of_agreement != null && mdt.data.Date_of_agreement != '')
                    $scope.DateofAgreement_View = mdt.data.Date_of_agreement == null?"--":convertDateDDMMYYYY(new Date(mdt.data.Date_of_agreement))
                else $scope.DateofAgreement_View = '--';

                if (mdt.data.Signed_Contract_sent_date != null && mdt.data.Signed_Contract_sent_date != '')
                    $scope.SignedContractSentDate_View = mdt.data.Signed_Contract_sent_date == null ? "--" : convertDateDDMMYYYY(new Date(mdt.data.Signed_Contract_sent_date))
                else $scope.SignedContractSentDate_View = '--';

                if (mdt.data.Signed_Contract_receiveddate != null && mdt.data.Signed_Contract_receiveddate != '')
                    $scope.SignedContractReceivedDate_View = mdt.data.Signed_Contract_receiveddate == null ? "--" : convertDateDDMMYYYY(new Date(mdt.data.Signed_Contract_receiveddate))
                else $scope.SignedContractReceivedDate_View = '--';

                if (mdt.data.CancellationDate != null && mdt.data.CancellationDate != '')
                    $scope.CancellationDate_View = mdt.data.CancellationDate == null ? "--" : convertDateDDMMYYYY(new Date(mdt.data.CancellationDate))
                else $scope.CancellationDate_View = '--';

                if (mdt.data.Cancellation_Reason != '' && mdt.data.Cancellation_Reason != null)
                    $scope.CancellationReason_View = mdt.data.Cancellation_Reason == "" ? "--" : mdt.data.Cancellation_Reason;
                else $scope.CancellationReason_View = '--';

                if (mdt.data.Remarks != null && mdt.data.Remarks != '')
                    $scope.Remarks_View = mdt.data.Remarks;
                else $scope.Remarks_View = '--';

                if (mdt.data.Effectivedate != null && mdt.data.Effectivedate != '')
                    $scope.Effectivedate_View = mdt.data.Effectivedate == null ? "--" : convertDateDDMMYYYY(new Date(mdt.data.Effectivedate))
                else $scope.Effectivedate_View = '--';


               

                if (mdt.data.Contractperiodinmonth != null && mdt.data.Contractperiodinmonth != '')
                    $scope.Contractperiodinmonth_View = mdt.data.Contractperiodinmonth;
                else $scope.Contractperiodinmonth_View = '--';


                if (mdt.data.Expirydate != null && mdt.data.Expirydate != '')
                    $scope.ExpiryDate_View = convertDateDDMMYYYY(new Date(mdt.data.Expirydate))
                else $scope.ExpiryDate_View = '--';
            }
            else {

                $scope.ContractStatus_View = '--';
                $scope.DateofAgreement_View = '--';
                $scope.SignedContractSentDate_View = '--';
                $scope.SignedContractReceivedDate_View = '--';
                $scope.CancellationDate_View = '--';
                $scope.CancellationReason_View = '--';
                $scope.Remarks_View = '--';
                $scope.Effectivedate_View = '--';
                $scope.Contractperiodinmonth_View = '--';
                $scope.ExpiryDate_View = '--';
            }
        }
    )
    };

    function convertDateDDMMYYYY(inputFormat) {
        function pad(s) { return (s < 10) ? '0' + s : s; }
        var d = new Date(inputFormat);
        return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
    }


    $scope.RightsSellingUpdate = function () {
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

        var _mobjRightsSelling = {
            DocumentName: Array1,
            UploadFile: $("#hid_Uploads").val(),
            //ContractStatus: $scope.ContractStatus,
            ContractStatus: $('#updateRights').find('select[name=ContractStatus]').val(),
            Date_of_agreement: $scope.DateofAgreement,
            Signed_Contract_sent_date: $scope.SignedContractSentDate,
            Signed_Contract_receiveddate: $scope.SignedContractReceivedDate,
            CancellationDate: $scope.CancellationDate,
            //Cancellation_Reason: $scope.CancellationReason,
            Cancellation_Reason: $('#updateRights').find('input[name=CancellationReason]').val(),
            //Remarks: $scope.Remarks,
            Remarks: $('#updateRights').find('input[name=Remarks]').val(),
            RightsSellingID: $('#hid_RightsSellingId').val(),
            EnteredBy: $('#enterdBy').val()
        }

        var ProductStatus = AJService.PostDataToAPI('RightsSelling/InsertRightsSellingUpdate', _mobjRightsSelling);


        ProductStatus.then(function (msg) {

            if (msg.data == "Duplicate") {
                SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
            }
            else if (msg.data != "OK") {
                SweetAlert.swal("Try agian", "There is some problem.", "", "error");
            }
            else {
                SweetAlert.swal({
                    title: "Updated successfully.",
                    text: "",
                    type: "success"
                },
               function () {
                   $('form[name*=user]').attr("method", "post");
                   $('form[name*=user]').submit();
               });
            }


        },
        function () {
            alert('There is some error in the system');
        });
    }


    $scope.RightSalesUpdateForm = function () {
        $scope.submitted = true;

        var ContractStatus = $('#updateRights').find('select[name=ContractStatus]').val();

        if (ContractStatus == "Issued" || ContractStatus == "Cancelled") {
            $scope.HasFile();
        }
        if (ContractStatus == 'Issued' || ContractStatus == 'Pending') {
            $scope.checkdateOnSubmit($("input[name*=SignedContractSentDate]"));
        }

        if (ContractStatus == 'Issued') {
            $scope.checkdateOnSubmit($("input[name*=DateofAgreement]"));
            $scope.checkdateOnSubmit($("input[name*=SignedContractSentDate]"));
            $scope.checkdateOnSubmit($("input[name*=SignedContractReceivedDate]"));
        }

        if (ContractStatus == 'Cancelled') {
            $scope.checkdateOnSubmit($("input[name*=CancellationDate]"));
        }

        if ($("form[name*=userForm]").find(".has-error").length > 0) {
            $scope.userForm.$valid = false;
        }


        if ($scope.userForm.$valid) {
            $scope.RightsSellingUpdate();
            // set form default state
            $scope.userForm.$setPristine();
            // set form is no submitted
            $scope.submitted = false;
            return;
        }
    }

    $scope.checkdateOnSubmit = function (date) {
        if (date.val() != "") {
            date.closest(".form-group").removeClass("has-error");
            date.closest("div").next().find("p").removeClass("ng-show").addClass("ng-hide");
        }
        else {
            date.closest(".form-group").addClass("has-error");
            date.closest("div").next().find("p").addClass("ng-show").removeClass("ng-hide");
        }
    }


    $scope.HasFile = function () {

        errorDiv = document.getElementById("fileid");
        errorDiv.innerHTML = "";
        errormsg = "";


        var errorDiv;
        var errormsg = '';
        $scope.msg = "";
        FileNameArray = [];
        FileNameArray = $('#dropZone0').find('.fileNameClass');
        var array = [];

        if (FileNameArray.length == 0) {
            errorDiv = document.getElementById("fileid");
            errorDiv.innerHTML = "Please select a file";
            errormsg = "Please select a file";
            $scope.userForm.$valid = false;
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
    }

    $scope.RemoveRightsSellingDocument = function (docid, file) {
        var AuthorDocument = { Id: docid, EnteredBy: $("#enterdBy").val() };
        var DeleteDocument = AJService.PostDataToAPI("RightsSelling/RemoveRightsSellingDocument", AuthorDocument);

        DeleteDocument.then(function (msg) {
            if (msg.data != "Deleted") {
                SweetAlert.swal("Oops...", "Please retry!", "error");
            }
            else {
                $scope.GetRightsSellingDocumentList();
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
                            $scope.GetRightsSellingDocumentList();
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


    $scope.RightsSellingDataView = function (Id) {


        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var Data = {
                Id: Id
            };
            // blockUI.start();
            // call API to fetch temp Department list basis on the FlatId

            var ExecutiveStatus = AJService.PostDataToAPI('RightsSelling/RightsSellingSerchView', Data);


            ExecutiveStatus.then(function (msg) {
                if (msg != null) {
                    if (msg.data._GetRightsSelling[0].OrganizationName != null)
                        $scope.Licensee = msg.data._GetRightsSelling[0].OrganizationName;
                    else $scope.Licensee = '--';

                    if (msg.data._GetRightsSelling[0].ContactPerson != null)
                        $scope.ContactPerson = msg.data._GetRightsSelling[0].ContactPerson;

                    else $scope.ContactPerson = '--';

                    if (msg.data._GetRightsSelling[0].Mobile != null)
                        $scope.Mobile = msg.data._GetRightsSelling[0].Mobile;
                    else $scope.Mobile = '--';

                    if (msg.data._GetRightsSelling[0].Email != null)
                        $scope.Email = msg.data._GetRightsSelling[0].Email;
                    else $scope.Email = '--';

                    if (msg.data._GetRightsSelling[0].URL != null)
                        $scope.URL = msg.data._GetRightsSelling[0].URL;
                    else $scope.URL = '--';

                    if (msg.data._GetRightsSelling[0].Address != null)
                        $scope.Address = msg.data._GetRightsSelling[0].Address;
                    else $scope.Address = '--';

                    if (msg.data._GetRightsSelling[0].Country != null)
                        $scope.Country = msg.data._GetRightsSelling[0].Country;
                    else $scope.Country = '--';

                    if (msg.data._GetRightsSelling[0].OtherCountry != null) {
                        $scope.CountryName = msg.data._GetRightsSelling[0].OtherCountry;
                        $scope.OtherCountry = true
                    }

                    if (msg.data._GetRightsSelling[0].State != null)
                        $scope.State = msg.data._GetRightsSelling[0].State;
                    else $scope.State = '--';

                    if (msg.data._GetRightsSelling[0].OtherState != null) {
                        $scope.StateName = msg.data._GetRightsSelling[0].OtherState;
                        $scope.OtherState = true
                    }

                    if (msg.data._GetRightsSelling[0].City != null)
                        $scope.City = msg.data._GetRightsSelling[0].City;
                    else $scope.City = '--';

                    if (msg.data._GetRightsSelling[0].OtherCity != null) {
                        $scope.CityName = msg.data._GetRightsSelling[0].OtherCity;
                        $scope.OtherCity = true
                    }

                    if (msg.data._GetRightsSelling[0].Pincode != null)
                        $scope.pincode = msg.data._GetRightsSelling[0].Pincode;
                    else $scope.pincode = '--';


                    /////////////////////////////////////////////////////////////////


                    if (msg.data._GetRightsSelling[0].RequestDate != null)
                        $scope.RequestDate = msg.data._GetRightsSelling[0].RequestDate;
                    else $scope.RequestDate = '--';

                    if (msg.data._GetRightsSelling[0].FirstPublicationDate != null)
                        $scope.FirstPublicationDate = msg.data._GetRightsSelling[0].FirstPublicationDate;
                    else $scope.FirstPublicationDate = '--';

                    if (msg.data._GetRightsSelling[0].DateContract != null)
                        $scope.ContractDate = msg.data._GetRightsSelling[0].DateContract;
                    else $scope.ContractDate = '--';

                    if (msg.data._GetRightsSelling[0].ContractPeriod != null)
                        $scope.Contractperiod = msg.data._GetRightsSelling[0].ContractPeriod;
                    else $scope.Contractperiod = '--';

                    if (msg.data._GetRightsSelling[0].First_Impression_within_date != null)
                        $scope.FirstImpressionwithindate = msg.data._GetRightsSelling[0].First_Impression_within_date;
                    else $scope.FirstImpressionwithindate = '--';

                    if (msg.data._GetRightsSelling[0].DateExpiry != null)
                        $scope.ExpiryDate = msg.data._GetRightsSelling[0].DateExpiry;
                    else $scope.ExpiryDate = '--';

                    if (msg.data._GetRightsSelling[0].Contract_Effective_Date != null)
                        $scope.ContractEffectiveDate = msg.data._GetRightsSelling[0].Contract_Effective_Date;
                    else $scope.ContractEffectiveDate = '--';

                    if (msg.data._GetRightsSelling[0].ProductCategory != null)
                        $scope.ProductCategory = msg.data._GetRightsSelling[0].ProductCategory;
                    else $scope.ProductCategory = '--';

                    if (msg.data._GetRightsSelling[0].Will_be_material_be_translated != null)
                        $scope.Willbematerialbetranslated = msg.data._GetRightsSelling[0].Will_be_material_be_translated;
                    else $scope.Willbematerialbetranslated = '--';

                    //if (msg.data._GetRightsSelling[0].Language != null)
                    //    $scope.Language = msg.data._GetRightsSelling[0].Language;
                    //else $scope.Language = '--';

                    //added by Prakash on 10 July, 2017
                    if (msg.data._GetRightsSelling[0].Print_Run_Quantity_Type != null)
                        $scope.PrintQuantityType = msg.data._GetRightsSelling[0].Print_Run_Quantity_Type;
                    else $scope.PrintQuantityType = '--';
                    //-----------------

                    if (msg.data._GetRightsSelling[0].Print_Run_Quantity_Allowed != null)
                        $scope.PrintRunQuantity = msg.data._GetRightsSelling[0].Print_Run_Quantity_Allowed;
                    else $scope.PrintRunQuantity = '--';

                    if (msg.data._GetRightsSelling[0].Number_of_Impression_Allowed != null)
                        $scope.NumberofImpression = msg.data._GetRightsSelling[0].Number_of_Impression_Allowed;
                    else $scope.NumberofImpression = '--';

                    if (msg.data._GetRightsSelling[0].Advance_Payment != null)
                        $scope.AdvancePayment = msg.data._GetRightsSelling[0].Advance_Payment;
                    else $scope.AdvancePayment = '--';

                    if (msg.data._GetRightsSelling[0].Currency != null)
                        $scope.Currency = msg.data._GetRightsSelling[0].Currency;
                    else $scope.Currency = '--';

                    if (msg.data._GetRightsSelling[0].CurrencySymbol != null)
                        $scope.CurrencySymbol = msg.data._GetRightsSelling[0].CurrencySymbol;
                    else $scope.CurrencySymbol = '--';

                    if (msg.data._GetRightsSelling[0].Payment_Term != null)
                        $scope.PaymentTerm = msg.data._GetRightsSelling[0].Payment_Term;
                    else $scope.PaymentTerm = '--';

                    if (msg.data._GetRightsSelling[0].Payment_Amount != null)
                        $scope.PaymentAmount = msg.data._GetRightsSelling[0].Payment_Amount;
                    else $scope.PaymentAmount = '--';

                    if (msg.data._GetRightsSelling[0].Territory_Rights != null)
                        $scope.TerritoryRight = msg.data._GetRightsSelling[0].Territory_Rights;
                    else $scope.TerritoryRight = '--';

                    if (msg.data._GetRightsSelling[0].Advance_Royalty_Amount != null)
                        $scope.AdvanceRoyaltyAmount = msg.data._GetRightsSelling[0].Advance_Royalty_Amount;
                    else $scope.AdvanceRoyaltyAmount = '--';

                    if (msg.data._GetRightsSelling[0].RoyaltyType != null)
                        $scope.RoyaltyType = msg.data._GetRightsSelling[0].RoyaltyType;
                    else $scope.RoyaltyType = '--';

                    if (msg.data._GetRightsSelling[0].Royalty_Recurring != null)
                        $scope.RoyaltyRecurring = msg.data._GetRightsSelling[0].Royalty_Recurring;
                    else $scope.RoyaltyRecurring = '--';

                    if (msg.data._GetRightsSelling[0].Recurring_From_Period != null)
                        $scope.RecurringFromPeriod = msg.data._GetRightsSelling[0].Recurring_From_Period;
                    else $scope.RecurringFromPeriod = '--';

                    if (msg.data._GetRightsSelling[0].Recurring_To_Period != null)
                        $scope.RecurringToPeriod = msg.data._GetRightsSelling[0].Recurring_To_Period;
                    else $scope.RecurringToPeriod = '--';

                    if (msg.data._GetRightsSelling[0].Frequency != null)
                        $scope.Frequency = msg.data._GetRightsSelling[0].Frequency;
                    else $scope.Frequency = '--';

                    if (msg.data._GetRightsSelling[0].Remarks != null)
                        $scope.Remarks_Details = msg.data._GetRightsSelling[0].Remarks;
                    else $scope.Remarks_Details = '--';

                    if (msg.data._GetRightsSelling[0].RightsSellingCode != null)
                        $scope.RightsSellingCodeView = msg.data._GetRightsSelling[0].RightsSellingCode;
                    else $scope.RightsSellingCodeView = '--';


                    $scope.RightsSellingRoyaltySalbView();

                }
            });
        }
    }



    $scope.RightsSellingRoyaltySalbView = function () {


        var _mobjRightsSelling = {
            ContractId: $('#hid_Type').val() == "A" ? $("#hid_AuthorContract").val() : null,
            ProductLicenseId: $('#hid_Type').val() == "P" ? $("#hid_AuthorContract").val() : null,
            RightsSellingID: ($('#hid_RightsSellingId').val() =="" ? 0 : $('#hid_RightsSellingId').val())
        };

        var ExecutiveStatus = AJService.PostDataToAPI('RightsSelling/RightsSellingRoyaltySalbView', _mobjRightsSelling);


        ExecutiveStatus.then(function (msg) {
            if (msg.data != null) {
                $scope.RoyaltyslabList = msg.data;
            }
        });
    }


    $scope.BackToList = function () {
        if ($('#hid_for').val() == "Dashboard") {
            $window.location.href = '../../Home/Dashboard/Dashboard';
        }
        else {
            $window.location.href = '../RightsSelling/RightsSellingSearch?For=BackToList';
        }
    }




    $scope.getRightsSellingLanguageList = function () {



        var RightsSellingLanguageList = AJService.GetDataFromAPI("RightsSelling/getRightsSellingLanguageList?Id=" + $('#hid_RightsSellingId').val() + "", null);

        RightsSellingLanguageList.then(function (msg) {
            if (msg != null) {

                $scope.RightLanguageList = msg.data;

            }
            else {
                SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                blockUI.stop();
            }

        });

    }


    if ($('#hid_RightsSellingId').val() != null) {
        $scope.getRightsSellingLanguageList();
    }



});


