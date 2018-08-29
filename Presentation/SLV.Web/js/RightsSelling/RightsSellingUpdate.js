
app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {
    app.expandControllerTopSearch($scope, AJService, $window);
    app.expandControllerA($scope, AJService, $window);

    app.expandControllerAuthorContractDetails($scope, AJService, $window);

    app.expandControllerProductLicense($scope, AJService, $window);

    app.expandControllerProductDetails($scope, AJService, $window);

    app.expandControllerRoyaltySlab($scope, AJService, $window);

    $scope.ProductSerach($('#hid_ProductId').val());

    setTimeout(function () {
        //fetch Kit Details List
        app.expandControllerKitISBNLIst($scope, AJService);
        angular.element(document.getElementById('angularid')).scope().KitISBNDetailsList($('#hid_ProductId').val());
    }, 300);

    $scope.DisplayUpdateDetails = 1;



    $scope.UploadExcelfileNameReq = false;
    $scope.UploadContractReq = false;

    $scope.PageTitle = "Update";
    setTimeout(function () {
        $("#Country").attr("disabled", "disabled").removeAttr("required");
        $("#state").attr("disabled", "disabled");
        $("#city").attr("disabled", "disabled");
        $("#pincode").attr("disabled", "disabled");
        $("#geogdiv").find('span').attr('style', 'display:none');

    }, 1000)

    
    $scope.getLicenseeList = function () {
        var getLicenseeList = AJService.GetDataFromAPI("RightsSelling/getLicenseeList", null);
        getLicenseeList.then(function (Language) {
            $scope.LicenseeList = Language.data;
        }, function () {
            //alert('Error in getting Language List');
        });
    }


    //$scope.onchnagLicensee = function () {
    //    var LicenseeDetail = {
    //        Id: $scope.userForm.Licensee.$modelValue,
    //        EnteredBy: $("#enterdBy").val()
    //    };

    //    // call API to fetch temp product type list basis on the FlatId
    //    var LicenseeDetailStatus = AJService.PostDataToAPI('RightsSelling/LicenseeDetails', LicenseeDetail);
    //    LicenseeDetailStatus.then(function (msg) {
    //        if (msg != null) {


    //            $scope.Licensee = $scope.Licensee;
    //            $scope.ContactPerson = msg.data.ContactPerson;
    //            $scope.PublisherMobile = msg.data.Mobile;
    //            $scope.PublisherEmail = msg.data.Email;
    //            $scope.PublisherAddress = msg.data.Address;

    //            $scope.pincode = msg.data.Pincode;
    //            $scope.Country = msg.data.CountryId;

    //            $scope.getCountryStates();
    //            $scope.State = msg.data.Stateid;

    //            $scope.getStateCities();
    //            $scope.City = msg.data.Cityid;

    //            $("#ContactPerson").removeAttr("disabled");

    //            $("#hid_LicenceCode").val(msg.data.Licenseecode)

    //            setTimeout(function () {
    //                $scope.getStateCities();
    //                $scope.City = msg.data.Cityid;
    //            }, 250);
    //        }
    //        else {
    //            SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
    //            blockUI.stop();
    //        }

    //    });

    //}
    

  

    //---Added by prakash on 21 July, 2017
    //---Autocomplete for Licensee 
    setTimeout(function () {
        AutoCompleteLicensee();   
    }, 200);
    function AutoCompleteLicensee() {
        var obj = $("[name$=LicenseeUpdate]"); 

        var LicenseeList = [];

        var getLicenseeList = AJService.GetDataFromAPI("RightsSelling/getLicenseeList", null);
        getLicenseeList.then(function (LicenseeData) {
            $scope.autocompdataid = [];
            $scope.autocompdataid = LicenseeData.data;
            for (i = 0; i < LicenseeData.data.length; i++) {
                LicenseeList[i] = { "label": LicenseeData.data[i].OrganizationName, "value": LicenseeData.data[i].OrganizationName, "data": LicenseeData.data[i].Id };
            }

            $(obj).autocomplete({ // change by raghvendra mishra on 26/07/2017
                source: function (request, response) {
                    var matcher = new RegExp(request.term, "i"); //RegExp("^" + request.term, "i"); //RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
                    response($.grep(LicenseeList, function (item) {
                        return matcher.test(item.label);
                    }));
                },

                autoFocus: true,
                select: function (event, ui) {
                    $scope.LicenseeUpdate = ui.item.value;
                    $scope.Licensee = ui.item.data;

                    //------------Fill licensee Details
                        var LicenseeDetail = {
                            Id: $scope.Licensee,
                            EnteredBy: $("#enterdBy").val()
                        };

                        // call API to fetch temp product type list basis on the FlatId
                        var LicenseeDetailStatus = AJService.PostDataToAPI('RightsSelling/LicenseeDetails', LicenseeDetail);
                        LicenseeDetailStatus.then(function (msg) {
                            if (msg != null) {


                                $scope.Licensee = $scope.Licensee;
                                $scope.ContactPerson = msg.data.ContactPerson;
                                $scope.PublisherMobile = msg.data.Mobile;
                                $scope.PublisherEmail = msg.data.Email;
                                $scope.PublisherAddress = msg.data.Address;

                                $scope.pincode = msg.data.Pincode;
                                $scope.Country = msg.data.CountryId;

                                $scope.getCountryStates();
                                $scope.State = msg.data.Stateid;

                                $scope.getStateCities();
                                $scope.City = msg.data.Cityid;

                                $("#ContactPerson").removeAttr("disabled");

                                $("#hid_LicenceCode").val(msg.data.Licenseecode)

                                setTimeout(function () {
                                    $scope.getStateCities();
                                    $scope.City = msg.data.Cityid;
                                }, 250);
                            }
                            else {
                                SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                                blockUI.stop();
                            }

                        });
                    //---------------------------------

                }
            });
        }, function () {
            //alert('Error in getting Licensee list');
        });
    }


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

   
        if (value == "Issued") {

            var today = new Date();
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


            $scope.EffectiveDate = today;
            $scope.DateofAgreement = today;
        }
        else {

            $scope.EffectiveDate = "";
            $scope.DateofAgreement = "";
        }

       

        $scope.disableErrorClass($('#updateRights').find('input[name=SignedContractReceivedDate]'));
        $scope.disableErrorClass($('#updateRights').find('input[name=DateofAgreement]'));
        $scope.disableErrorClass($('#updateRights').find('input[name=CancellationDate]'));
        $scope.disableErrorClass($('#updateRights').find('input[name=SignedContractSentDate]'));
      //  $scope.disableErrorClass($('#updateRights').find('input[name=EffectiveDate]'));

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

        $(".green").find("input[name*=DateofAgreement]").val($(datetext).val());
        $scope.EffectiveDate = $(datetext).val();
        $('#EffectiveDate').val($(datetext).val());
        if ($scope.userForm.ContractperiodUpload.$modelValue > 0) {
            $scope.CalculateExpiry();
        }


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

    $scope.disableErrorClass = function (value)
    {
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

               
                if ($('#hid_UserRight').val() == "rt" && mdt.data.ContractStatus == 'Issued')
                {
                 
                    $scope.DisplayUpdateDetails = 0;
                    $scope.PageTitle = "View";
                    if (mdt.data.ContractStatus != '' && mdt.data.ContractStatus != null)
                        $scope.ContractStatus_View = mdt.data.ContractStatus;
                    else $scope.ContractStatus_View = '--';

                    if (mdt.data.Date_of_agreement != null && mdt.data.Date_of_agreement != '')
                        $scope.DateofAgreement_View = convertDateDDMMYYYY(new Date(mdt.data.Date_of_agreement))
                    else $scope.DateofAgreement_View = '--';

                    if (mdt.data.Signed_Contract_sent_date != null && mdt.data.Signed_Contract_sent_date != '')
                        $scope.SignedContractSentDate_View = convertDateDDMMYYYY(new Date(mdt.data.Signed_Contract_sent_date))
                    else $scope.SignedContractSentDate_View = '--';

                    if (mdt.data.Signed_Contract_receiveddate != null && mdt.data.Signed_Contract_receiveddate != '')
                        $scope.SignedContractReceivedDate_View = convertDateDDMMYYYY(new Date(mdt.data.Signed_Contract_receiveddate))
                    else $scope.SignedContractReceivedDate_View = '--';

                    if (mdt.data.CancellationDate != null && mdt.data.CancellationDate != '')
                        $scope.CancellationDate_View = convertDateDDMMYYYY(new Date(mdt.data.CancellationDate))
                    else $scope.CancellationDate_View = '--';

                    if (mdt.data.Cancellation_Reason != '' && mdt.data.Cancellation_Reason != null)
                        $scope.CancellationReason_View = mdt.data.Cancellation_Reason;
                    else $scope.CancellationReason_View = '--';

                    if (mdt.data.Remarks != null && mdt.data.Remarks != '')
                        $scope.Remarks_View = mdt.data.Remarks;
                    else $scope.Remarks_View = '--';

                    if (mdt.data.Effectivedate != null && mdt.data.Effectivedate != '')
                        $scope.Effectivedate_View = convertDateDDMMYYYY(new Date(mdt.data.Effectivedate))
                    else $scope.Effectivedate_View = '--';


                  
                    if (mdt.data.Contractperiodinmonth != null && mdt.data.Contractperiodinmonth != '')
                        $scope.Contractperiodinmonth_View = mdt.data.Contractperiodinmonth;
                    else $scope.Contractperiodinmonth_View = '--';


                    if (mdt.data.Expirydate != null && mdt.data.Expirydate != '')
                        $scope.ExpiryDate_View = convertDateDDMMYYYY(new Date(mdt.data.Expirydate))
                    else $scope.ExpiryDate_View = '--';

                }
                else if ($('#hid_UserRight').val() == "ad" || $('#hid_UserRight').val() == "sa")
                {
                    $scope.DisplayUpdateDetails = 1;
                    $('#ContractStatusId').val(mdt.data.ContractStatus)
                    $scope.ContractStatus = mdt.data.ContractStatus
                  
                
                    $scope.DateofAgreement = (mdt.data.Date_of_agreement == null ? null : convertDateDDMMYYYY(new Date(mdt.data.Date_of_agreement)))
                    $scope.SignedContractSentDate = (mdt.data.Signed_Contract_sent_date == null ? null : convertDateDDMMYYYY(new Date(mdt.data.Signed_Contract_sent_date)))
                    $scope.SignedContractReceivedDate =   (mdt.data.Signed_Contract_receiveddate ==  null ? null : convertDateDDMMYYYY(new Date(mdt.data.Signed_Contract_receiveddate))) 
                    $scope.CancellationDate = (mdt.data.CancellationDate == null ? null : convertDateDDMMYYYY(new Date(mdt.data.CancellationDate)))
                    $scope.CancellationReason = mdt.data.Cancellation_Reason;

                    

                    $scope.EffectiveDate = (mdt.data.Effectivedate == null ? null : convertDateDDMMYYYY(new Date(mdt.data.Effectivedate)));

                    setTimeout(function () {  }, 1100)

                   
                    $scope.ExpiryDateValue = (mdt.data.Expirydate == null ? null : convertDateDDMMYYYY(new Date(mdt.data.Expirydate)));
               
                    $scope.ContractperiodUpload = mdt.data.Contractperiodinmonth;

                    
                    
                    $scope.RemarksUpdate = mdt.data.Remarks;
                }
                else if ($('#hid_UserRight').val() == "rt" && mdt.data.ContractStatus != 'Issued')
                {
                    $scope.DisplayUpdateDetails = 1;
                 $('#ContractStatusId').val(mdt.data.ContractStatus)
                    $scope.ContractStatus = mdt.data.ContractStatus
                  
                
                    $scope.DateofAgreement = (mdt.data.Date_of_agreement == null ? null : convertDateDDMMYYYY(new Date(mdt.data.Date_of_agreement)))
                    $scope.SignedContractSentDate = (mdt.data.Signed_Contract_sent_date == null ? null : convertDateDDMMYYYY(new Date(mdt.data.Signed_Contract_sent_date)))
                    $scope.SignedContractReceivedDate =   (mdt.data.Signed_Contract_receiveddate ==  null ? null : convertDateDDMMYYYY(new Date(mdt.data.Signed_Contract_receiveddate))) 
                    $scope.CancellationDate = (mdt.data.CancellationDate == null ? null : convertDateDDMMYYYY(new Date(mdt.data.CancellationDate)))
                    $scope.CancellationReason = mdt.data.Cancellation_Reason;

                    

                    $scope.EffectiveDate = (mdt.data.Effectivedate == null ? null : convertDateDDMMYYYY(new Date(mdt.data.Effectivedate)));

                    setTimeout(function () {  }, 1100)

                   
                    $scope.ExpiryDateValue = (mdt.data.Expirydate == null ? null : convertDateDDMMYYYY(new Date(mdt.data.Expirydate)));
               
                    $scope.ContractperiodUpload = mdt.data.Contractperiodinmonth;

                    
                    
                    $scope.RemarksUpdate = mdt.data.Remarks;
                }
              
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

    function convertDate(date) {
        var datearray = date.split("/");
        return datearray[1] + '/' + datearray[0] + '/' + datearray[2];
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
        if ($('#hid_UserRight').val() == "rt")
        {


            var mstr_SignedContractSentDate = $('#SignedContractSentDate').val();
            if (mstr_SignedContractSentDate == "") {
                mstr_SignedContractSentDate = null
            }
            else {

                var RequestDate = mstr_SignedContractSentDate;

                var date = RequestDate;
                var d = new Date(date.split("/").reverse().join("-"));
                var dd = d.getDate();
                var mm = d.getMonth() + 1;
                var yy = d.getFullYear();
                mstr_SignedContractSentDate = yy + "/" + mm + "/" + dd;
            }





            var mstr_CancellationDate = $('#CancellationDate').val();
            if (mstr_CancellationDate == "") {
                mstr_CancellationDate = null
            }
            else {

                var RequestDate = mstr_CancellationDate;

                var date = RequestDate;
                var d = new Date(date.split("/").reverse().join("-"));
                var dd = d.getDate();
                var mm = d.getMonth() + 1;
                var yy = d.getFullYear();
                mstr_CancellationDate = yy + "/" + mm + "/" + dd;
            }


            var mstr_DateofAgreement = $('#DateofAgreement').val();
            if (mstr_DateofAgreement == "") {
                mstr_DateofAgreement = null
            }
            else {

                var RequestDate = mstr_DateofAgreement;

                var date = RequestDate;
                var d = new Date(date.split("/").reverse().join("-"));
                var dd = d.getDate();
                var mm = d.getMonth() + 1;
                var yy = d.getFullYear();
                mstr_DateofAgreement = yy + "/" + mm + "/" + dd;
            }




            var mstr_SignedContractReceivedDate = $('#SignedContractReceivedDate').val();
            if (mstr_SignedContractReceivedDate == "") {
                mstr_SignedContractReceivedDate = null
            }
            else {

                var RequestDate = mstr_SignedContractReceivedDate;

                var date = RequestDate;
                var d = new Date(date.split("/").reverse().join("-"));
                var dd = d.getDate();
                var mm = d.getMonth() + 1;
                var yy = d.getFullYear();
                mstr_SignedContractReceivedDate = yy + "/" + mm + "/" + dd;
            }



            var mstr_EffectiveDate = $('#EffectiveDate').val();
            if (mstr_EffectiveDate == "") {
                mstr_EffectiveDate = null
            }
            else {

                var RequestDate = mstr_EffectiveDate;

                var date = RequestDate;
                var d = new Date(date.split("/").reverse().join("-"));
                var dd = d.getDate();
                var mm = d.getMonth() + 1;
                var yy = d.getFullYear();
                mstr_EffectiveDate = yy + "/" + mm + "/" + dd;
            }


            //var mstr_EffectiveDate = $('#EffectiveDate').val();
            //if (mstr_EffectiveDate == "") {
            //    mstr_EffectiveDate = null
            //}
            //else {

            //    var RequestDate = mstr_EffectiveDate;

            //    var date = RequestDate;
            //    var d = new Date(date.split("/").reverse().join("-"));
            //    var dd = d.getDate();
            //    var mm = d.getMonth() + 1;
            //    var yy = d.getFullYear();
            //    mstr_EffectiveDate = yy + "/" + mm + "/" + dd;
            //}


            var mstr_ExpiryDate = $('#ExpiryDate').val();
            if (mstr_ExpiryDate == "") {
                mstr_ExpiryDate = null
            }
            else {

                var RequestDate = mstr_ExpiryDate;

                var date = RequestDate;
                var d = new Date(date.split("/").reverse().join("-"));
                var dd = d.getDate();
                var mm = d.getMonth() + 1;
                var yy = d.getFullYear();
                mstr_ExpiryDate = yy + "/" + mm + "/" + dd;
            }


            var _mobjRightsSelling = {
               
                DocumentName: Array1,
                UploadFile: $("#hid_Uploads").val(),
                ContractStatus: $('#updateRights').find('[name=ContractStatus]:checked').val(),

                Signed_Contract_sent_date: mstr_SignedContractSentDate,
                Signed_Contract_receiveddate: mstr_SignedContractReceivedDate,
                Date_of_agreement: mstr_DateofAgreement,
                EffectiveDate: mstr_EffectiveDate,
                ContractperiodUpload: 0, // $scope.userForm.ContractperiodUpload.$modelValue, //commented by Prakash on 30 May, 2017
                ExpiryDate: mstr_ExpiryDate,
                CancellationDate: mstr_CancellationDate,
                Cancellation_Reason: $('#updateRights').find('input[name=CancellationReason]').val(),
                Remarks:$('#updateRights').find('input[name=RemarksUpdate]').val(),


                RemarksUpdate: $scope.userForm.RemarksUpdate.$modelValue,
                RightsSellingID: $('#hid_RightsSellingId').val(),
                UserType: $('#hid_UserRight').val(),
                EnteredBy: $('#enterdBy').val(),

            }
        }
       
        if ($('#hid_UserRight').val() == "ad" || $('#hid_UserRight').val() == "sa")
        {
            var RightsSellingRoyalty = [];

            $(".RoyaltyTab").each(function (index, values) {
                var obj = $(this);
                var i = 0, j = 0;

                $(obj).find('.RoyaltySlab tr:not(:has(th))').each(function () {

                    if ($(this).find('select[name$=SubProductType]').val() == "") {
                        return true;
                    }
                    RightsSellingRoyalty[i] =
                    {
                        subproducttypeid: $(this).find('select[name$=SubProductType]').val(),
                        CopiesFrom: $(this).find('input[name$=CopiesFrom]').val(),
                        CopiesTo: $(this).find('input[name$=CopiesTo]').val(),
                        Percentage: $(this).find('input[name$=RyPercentage]').val(),
                        ContractId: $('#hid_Type').val() == "A" ? $("#hid_AuthorContract").val() : null,
                        ProductLicenseId: $('#hid_Type').val() == "P" ? $("#hid_AuthorContract").val() : null,

                    }
                    i++;
                });
                j++;
            });

            var mstr_RequestDate = $('#RequestDate').val();
            if (mstr_RequestDate == "") {
                mstr_RequestDate = null
            }
            else {

                var RequestDate = mstr_RequestDate;

                var date = RequestDate;
                var d = new Date(date.split("/").reverse().join("-"));
                var dd = d.getDate();
                var mm = d.getMonth() + 1;
                var yy = d.getFullYear();
                mstr_RequestDate = yy + "/" + mm + "/" + dd;
            }

            //added by Prakash
            var mstr_FirstPublicationDate = $('#FirstPublicationDate').val();
            if (mstr_FirstPublicationDate == "") {
                mstr_FirstPublicationDate = null
            }
            else {

                var RequestDate = mstr_FirstPublicationDate;

                var date = RequestDate;
                var d = new Date(date.split("/").reverse().join("-"));
                var dd = d.getDate();
                var mm = d.getMonth() + 1;
                var yy = d.getFullYear();
                mstr_FirstPublicationDate = yy + "/" + mm + "/" + dd;
            }

            //var mstr_ContractDate = $('#ContractDate').val();
            //if (mstr_ContractDate == "") {
            //    mstr_ContractDate = null
            //}
            //else {

            //    var RequestDate = mstr_ContractDate;

            //    var date = RequestDate;
            //    var d = new Date(date.split("/").reverse().join("-"));
            //    var dd = d.getDate();
            //    var mm = d.getMonth() + 1;
            //    var yy = d.getFullYear();
            //    mstr_ContractDate = yy + "/" + mm + "/" + dd;
            //}


            //var mstr_FirstImpressionwithindate = $('#FirstImpressionwithindate').val();
            //if (mstr_FirstImpressionwithindate == "" || mstr_FirstImpressionwithindate == undefined) {
            //    mstr_FirstImpressionwithindate = null
            //}
            //else {

            //    var RequestDate = mstr_FirstImpressionwithindate;

            //    var date = RequestDate;
            //    var d = new Date(date.split("/").reverse().join("-"));
            //    var dd = d.getDate();
            //    var mm = d.getMonth() + 1;
            //    var yy = d.getFullYear();
            //    mstr_FirstImpressionwithindate = yy + "/" + mm + "/" + dd;
            //}


            var mstr_ExpiryDate = $('#ExpiryDate').val();
            if (mstr_ExpiryDate == "") {
                mstr_ExpiryDate = null
            }
            else {

                var RequestDate = mstr_ExpiryDate;

                var date = RequestDate;
                var d = new Date(date.split("/").reverse().join("-"));
                var dd = d.getDate();
                var mm = d.getMonth() + 1;
                var yy = d.getFullYear();
                mstr_ExpiryDate = yy + "/" + mm + "/" + dd;
            }

            //var mstr_ContractEffectiveDate = $('#ContractEffectiveDate').val();
            //if (mstr_ContractEffectiveDate == "") {
            //    mstr_ContractEffectiveDate = null
            //}
            //else {

            //    var RequestDate = mstr_ContractEffectiveDate;

            //    var date = RequestDate;
            //    var d = new Date(date.split("/").reverse().join("-"));
            //    var dd = d.getDate();
            //    var mm = d.getMonth() + 1;
            //    var yy = d.getFullYear();
            //    mstr_ContractEffectiveDate = yy + "/" + mm + "/" + dd;
            //}


            var mstr_RecurringFromPeriod = $('#RecurringFromPeriod').val();
            if (mstr_RecurringFromPeriod == "") {
                mstr_RecurringFromPeriod = null
            }
            else {

                var RequestDate = mstr_RecurringFromPeriod;

                var date = RequestDate;
                var d = new Date(date.split("/").reverse().join("-"));
                var dd = d.getDate();
                var mm = d.getMonth() + 1;
                var yy = d.getFullYear();
                mstr_RecurringFromPeriod = yy + "/" + mm + "/" + dd;
            }


            var mstr_RecurringToPeriod = $('#RecurringToPeriod').val();
            if (mstr_RecurringToPeriod == "") {
                mstr_RecurringToPeriod = null
            }
            else {

                var RequestDate = mstr_RecurringToPeriod;

                var date = RequestDate;
                var d = new Date(date.split("/").reverse().join("-"));
                var dd = d.getDate();
                var mm = d.getMonth() + 1;
                var yy = d.getFullYear();
                mstr_RecurringToPeriod = yy + "/" + mm + "/" + dd;
            }





            var mstr_SignedContractSentDate = $('#SignedContractSentDate').val();
            if (mstr_SignedContractSentDate == "") {
                mstr_SignedContractSentDate = null
            }
            else {

                var RequestDate = mstr_SignedContractSentDate;

                var date = RequestDate;
                var d = new Date(date.split("/").reverse().join("-"));
                var dd = d.getDate();
                var mm = d.getMonth() + 1;
                var yy = d.getFullYear();
                mstr_SignedContractSentDate = yy + "/" + mm + "/" + dd;
            }





            var mstr_CancellationDate = $('#CancellationDate').val();
            if (mstr_CancellationDate == "") {
                mstr_CancellationDate = null
            }
            else {

                var RequestDate = mstr_CancellationDate;

                var date = RequestDate;
                var d = new Date(date.split("/").reverse().join("-"));
                var dd = d.getDate();
                var mm = d.getMonth() + 1;
                var yy = d.getFullYear();
                mstr_CancellationDate = yy + "/" + mm + "/" + dd;
            }


            var mstr_DateofAgreement = $('#DateofAgreement').val();
            if (mstr_DateofAgreement == "") {
                mstr_DateofAgreement = null
            }
            else {

                var RequestDate = mstr_DateofAgreement;

                var date = RequestDate;
                var d = new Date(date.split("/").reverse().join("-"));
                var dd = d.getDate();
                var mm = d.getMonth() + 1;
                var yy = d.getFullYear();
                mstr_DateofAgreement = yy + "/" + mm + "/" + dd;
            }
            



            var mstr_SignedContractReceivedDate = $('#SignedContractReceivedDate').val();
            if (mstr_SignedContractReceivedDate == "") {
                mstr_SignedContractReceivedDate = null
            }
            else {

                var RequestDate = mstr_SignedContractReceivedDate;

                var date = RequestDate;
                var d = new Date(date.split("/").reverse().join("-"));
                var dd = d.getDate();
                var mm = d.getMonth() + 1;
                var yy = d.getFullYear();
                mstr_SignedContractReceivedDate = yy + "/" + mm + "/" + dd;
            }
            var mstr_EffectiveDate = $('#EffectiveDate').val();
            if (mstr_EffectiveDate == "") {
                mstr_EffectiveDate = null
            }
            else {

                var RequestDate = mstr_EffectiveDate;

                var date = RequestDate;
                var d = new Date(date.split("/").reverse().join("-"));
                var dd = d.getDate();
                var mm = d.getMonth() + 1;
                var yy = d.getFullYear();
                mstr_EffectiveDate = yy + "/" + mm + "/" + dd;
            }

            

            var _mobjRightsSelling = {

              
                RightsSellingRoyalty: RightsSellingRoyalty,
               
                LicenseeID: $scope.userForm.Licensee.$modelValue,
                Licenseecode: $("#hid_LicenceCode").val(),
                OrganizationName: $scope.LicenseeUpdate,
                ContactPerson:  $scope.userForm.ContactPerson.$modelValue,
                Address: $scope.userForm.PublisherAddress.$modelValue,
                CountryId:  $scope.userForm.Country.$modelValue,
                OtherCountry:  $scope.userForm.CountryName.$modelValue,
                Stateid:  $scope.userForm.State.$modelValue,
                OtherState:   $scope.userForm.stateName.$modelValue,
                Cityid:   $scope.City,
                OtherCity: $scope.userForm.cityName.$modelValue,
                Pincode:   $scope.userForm.pincode.$modelValue,
                Mobile:  $scope.userForm.PublisherMobile.$modelValue,
                Email:   $scope.userForm.PublisherEmail.$modelValue,
                URL:  $scope.userForm.URL.$modelValue,
              
                RequestDate: mstr_RequestDate,
            //    DateContract:  mstr_ContractDate,
           ///   ContractPeriod:   $scope.userForm.Contractperiod.$modelValue,
              
               // First_Impression_within_date: mstr_FirstImpressionwithindate,
                Expirydate: mstr_ExpiryDate,
              //  Contract_Effective_Date: mstr_ContractEffectiveDate,
                
                ProductCategory: $scope.userForm.ProductCategory.$modelValue,
                Will_be_material_be_translated: $scope.userForm.Willbematerialbetranslated.$modelValue,
                Language: $scope.userForm.LanguageValue.$modelValue,
                Print_Run_Quantity_Allowed: $scope.userForm.PrintQuantityType.$modelValue == 'Unrestricted' ? null : $scope.userForm.PrintRunQuantity.$modelValue,
                Number_of_Impression_Allowed: $scope.userForm.NumberofImpression.$modelValue,
                Advance_Payment: $scope.userForm.AdvancePayment.$modelValue,
                //Currency: $scope.RightSalesModel.CurrencyValue,
                Currency : $('.ng-valid-max').find('select[name=Currency]').val(),
                Payment_Term: $scope.userForm.PaymentTerm.$modelValue,
                Payment_Amount: $scope.userForm.PaymentAmount.$modelValue,
                Territory_Rights: $scope.userForm.TerritoryRight.$modelValue,
                Advance_Royalty_Amount: $scope.userForm.AdvanceRoyaltyAmount.$modelValue,
                RoyaltyType:    $scope.userForm.RoyaltyType.$modelValue ,
                Royalty_Recurring: $scope.userForm.RoyaltyRecurring.$modelValue,
               

                RemarksUpdate: $scope.userForm.RemarksUpdate.$modelValue,
                Recurring_From_Period:  mstr_RecurringFromPeriod,
                Recurring_To_Period: mstr_RecurringToPeriod,
                Frequency: $scope.userForm.Frequency.$modelValue,
                ContractId: $('#hid_Type').val() == "A" ? $("#hid_AuthorContract").val() : null,
                ProductLicenseId: $('#hid_Type').val() == "P" ? $("#hid_AuthorContract").val() : null,
                Remarks: $scope.userForm.Remarks.$modelValue,
                EnteredBy: $('#enterdBy').val(),
             
                ProuductId: $('#hid_ProductId').val(),
               Type : $('#hid_Type').val().toLocaleLowerCase(),


                DocumentName: Array1,
                UploadFile: $("#hid_Uploads").val(),
                ContractStatus: $('#updateRights').find('[name=ContractStatus]:checked').val(),
                Date_of_agreement: mstr_DateofAgreement,
                Signed_Contract_sent_date: mstr_SignedContractSentDate,
                Signed_Contract_receiveddate: mstr_SignedContractReceivedDate,
                CancellationDate: mstr_CancellationDate,
                Cancellation_Reason: $('#updateRights').find('input[name=CancellationReason]').val(),
                RemarksUpdate: $('#updateRights').find('input[name=RemarksUpdate]').val(),
                RightsSellingID: $('#hid_RightsSellingId').val(),
                UserType: $('#hid_UserRight').val(),
                Id: $('#hid_RightsSellingId').val(),
                EffectiveDate: mstr_EffectiveDate,
                ContractperiodUpload: 0, // $scope.userForm.ContractperiodUpload.$modelValue, //commented by Prakash on 30 May, 2017,
                Print_Run_Quantity_Type: $scope.userForm.PrintQuantityType.$modelValue,
                FirstPublicationDate: mstr_FirstPublicationDate,
            }
        }

        SweetAlert.swal({
            title: "Are you sure?",
            text: "",
            type: "info",
            showCancelButton: true,
            confirmButtonColor: "#8CD4F5",
            confirmButtonText: "Yes",
            closeOnConfirm: false,
            closeOnCancel: true,
            showLoaderOnConfirm: true
        },
           function (Confirm) {
               if (Confirm) {


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
                              //$('form[name*=user]').attr("method", "post");
                              //$('form[name*=user]').submit();
                              //$window.location.href = "../../RightsSelling/RightsSelling/RightsSellingSearch?For=BackToList";
                              //location.href = "../../Home/Dashboard/Dashboard";

                              if ($('#hid_Type').val() == "A") {
                                  location.href = "../../RightsSelling/RightsSelling/RightsSellingView?Id=" + $("#hid_AuthorContract").val() + "&type=A" + $('#hid_ProductId').val() + "&RightsSellingId=" + $('#hid_RightsSellingId').val();
                              }
                              else if ($('#hid_Type').val() == "P") {
                                  location.href = "../../RightsSelling/RightsSelling/RightsSellingView?Id=" + $("#hid_AuthorContract").val() + "&type=P" + $('#hid_ProductId').val() + "&RightsSellingId=" + $('#hid_RightsSellingId').val();
                              }
                          });
                       }


                   },
                   function () {
                       SweetAlert.swal("", "Please validate details.", "warning");
                       //alert('There is some error in the system');
                   });
               }
           });
    }


    $scope.RightSalesUpdateForm = function () {
        $scope.submitted = true;


        if ($('#hid_UserRight').val() == "rt")
        {
            var ContractStatus = $('#updateRights').find('[name=ContractStatus]:checked').val();

            if (ContractStatus == "Issued" || ContractStatus == "Cancelled") {
                $scope.HasFile();
            }
            if (ContractStatus == 'Issued' || ContractStatus == 'Pending') {
                $scope.checkdateOnSubmit($("input[name*=SignedContractSentDate]"));
            }

            if (ContractStatus == 'Issued') {
                $scope.checkdateOnSubmit($("input[name*=DateofAgreement]"));
                $scope.checkdateOnSubmit($("input[name*=EffectiveDate]"));
                $scope.checkdateOnSubmit($("input[name*=SignedContractSentDate]"));
                $scope.checkdateOnSubmit($("input[name*=SignedContractReceivedDate]"));
                $scope.checkdateOnSubmit($("input[name*=ExpiryDateValue]"));
            }

            if (ContractStatus == 'Cancelled') {
                $scope.checkdateOnSubmit($("input[name*=CancellationDate]"));
            }
            if (ContractStatus == 'Draft') {
                $scope.checkdateOnSubmit($("input[name*=CancellationDate]"));
            }

            if ($("form[name*=userForm]").find(".has-error").length > 0) {
                $scope.userForm.$valid = false;
            }
            else {
                $scope.userForm.$valid = true;
            }

        }




        if ($('#hid_UserRight').val() == "ad" || $('#hid_UserRight').val() == "sa") {

            $scope.checkdateOnSubmit($("input[name*=ContractDate]"));

            if ($scope.RoyaltyRecurring == 'Yes') {
                $scope.checkdateOnSubmit($("input[name*=RecurringFromPeriod]"));
                $scope.checkdateOnSubmit($("input[name*=RecurringToPeriod]"));
            }

            if ($("form[name*=userForm]").find(".has-error").length > 0) {
                $scope.userForm.$valid = false;
            }

            if ($scope.PaymentTerm == 'Royalty') {
                if ($scope.ValidateRyaltySlab() == 1) {
                    $scope.userForm.$valid = false;
                }
            }



            var ContractStatus = $('#updateRights').find('[name=ContractStatus]:checked').val();

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
            if (ContractStatus == 'Draft') {
                $scope.checkdateOnSubmit($("input[name*=CancellationDate]"));
            }
            if ($("form[name*=userForm]").find(".has-error").length > 0) {
                $scope.userForm.$valid = false;
            }
            else {
                $scope.userForm.$valid = true;
            }

           


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

            if ($scope.DocumentList.length == 0) {

                errorDiv = document.getElementById("fileid");
                errorDiv.innerHTML = "Please select a file";
                errormsg = "Please select a file";
                $scope.userForm.$valid = false;
                $scope.UploadContractReq = true;
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
                        $scope.UploadExcelfileNameReq = true;

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
                        $scope.UploadExcelfileNameReq = true;
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
                    // alert(msg.data[0].Mobile);
                    
                    if ($('#hid_UserRight').val() == "rt")
                    {
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
                        else $scope.ProductCategoryId = '--';

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

                        if (msg.data._GetRightsSelling[0].RemarksInsert != null)
                            $scope.Remarks_Details = msg.data._GetRightsSelling[0].RemarksInsert;
                        else $scope.Remarks_Details = '--';

                        if (msg.data._GetRightsSelling[0].RightsSellingCode != null)
                            $scope.RightsSellingCodeView = msg.data._GetRightsSelling[0].RightsSellingCode;
                        else $scope.RightsSellingCodeView = '--';

                        $scope.RightsSellingRoyaltySalbView();

                    }
                   
                    else if ($('#hid_UserRight').val() == "ad" || $('#hid_UserRight').val() == "sa")
                    {
                       
                        $scope.Licensee = msg.data._GetRightsSelling[0].LicenseeID;
                        $scope.LicenseeUpdate = msg.data._GetRightsSelling[0].OrganizationName;
                        $scope.ContactPerson = msg.data._GetRightsSelling[0].ContactPerson;
                        $scope.PublisherMobile = msg.data._GetRightsSelling[0].Mobile
                        $scope.PublisherEmail = msg.data._GetRightsSelling[0].Email;
                        $scope.PublisherAddress = msg.data._GetRightsSelling[0].Address;

                        $scope.pincode = msg.data._GetRightsSelling[0].Pincode;
                        $scope.Country = msg.data._GetRightsSelling[0].CountryId;

                        $scope.getCountryStates();
                        $scope.State = msg.data._GetRightsSelling[0].Stateid;

                        $scope.getStateCities();
                        $scope.City = msg.data._GetRightsSelling[0].Cityid;

                        $("#ContactPerson").removeAttr("disabled");

                        $("#hid_LicenceCode").val(msg.data._GetRightsSelling[0].Licenseecode)

                        setTimeout(function () {
                            $scope.getStateCities();
                            $scope.City = msg.data._GetRightsSelling[0].Cityid;
                        }, 250);
                        $scope.RequestDate = msg.data._GetRightsSelling[0].RequestDate;
                        $scope.FirstPublicationDate = msg.data._GetRightsSelling[0].FirstPublicationDate;
                        $scope.ContractDate = msg.data._GetRightsSelling[0].DateContract;
                        $scope.Contractperiod = msg.data._GetRightsSelling[0].ContractPeriod;
                        $scope.FirstImpressionwithindate = msg.data._GetRightsSelling[0].First_Impression_within_date;
                        $scope.ExpiryDate = msg.data._GetRightsSelling[0].DateExpiry;
                        $scope.ContractEffectiveDate = msg.data._GetRightsSelling[0].Contract_Effective_Date;

                        if (msg.data._GetRightsSelling[0].ProductCategoryId != null) {
                            $scope.ProductCategory = msg.data._GetRightsSelling[0].ProductCategoryId;
                        }
                        else {
                            $('#ProductCategory').val($('#ProductCategory option:first').val());
                        }
                        $scope.Willbematerialbetranslated = msg.data._GetRightsSelling[0].Will_be_material_be_translated;

                        //if (msg.data[0].LanguageId != null) {
                        //    $scope.Language = msg.data[0].LanguageId;
                        //}
                        //else {
                        //    $('#LanguageId').val($('#LanguageId option:first').val());
                        //}
                      

                        $scope.PrintRunQuantity = parseInt(msg.data._GetRightsSelling[0].Print_Run_Quantity_Allowed);
                        $scope.PrintQuantityType = msg.data._GetRightsSelling[0].Print_Run_Quantity_Type;

                        $scope.NumberofImpression = msg.data._GetRightsSelling[0].Number_of_Impression_Allowed;
                        $scope.AdvancePayment = parseInt(msg.data._GetRightsSelling[0].Advance_Payment);

                        if (msg.data._GetRightsSelling[0].CurrencyId != null) {
                            $scope.CurrencyValue = msg.data._GetRightsSelling[0].CurrencyId;
                        }
                        else {
                            $('#CurrencyId').val($('#CurrencyId option:first').val());
                        }
                      
                        $scope.PaymentTerm = msg.data._GetRightsSelling[0].Payment_Term;
                        if (msg.data._GetRightsSelling[0].Payment_Term == 'Royalty')
                            $scope.func_Royalty()
                        else
                            $scope.func_OneTimePayment()
                        $scope.PaymentAmount = parseInt(msg.data._GetRightsSelling[0].Payment_Amount);

                        if (msg.data._GetRightsSelling[0].TerritoryRightsId != null) {
                            $scope.TerritoryRight = msg.data._GetRightsSelling[0].TerritoryRightsId;
                        }
                        else {
                            $('#ddlTerritory').val($('#ddlTerritory option:first').val());
                        }


                       
                        $scope.AdvanceRoyaltyAmount = parseInt(msg.data._GetRightsSelling[0].Advance_Royalty_Amount);
                        $scope.RoyaltyType = msg.data._GetRightsSelling[0].RoyaltyType;

                   
                        if (msg.data._GetRightsSelling[0].Royalty_Recurring != null)
                        {
                            $scope.RoyaltyRecurring = msg.data._GetRightsSelling[0].Royalty_Recurring;
                            if (msg.data._GetRightsSelling[0].Royalty_Recurring.toLowerCase() == "yes") {
                                $scope.RoyaltyRecurringReq = true;
                            }
                            else {
                                $scope.RoyaltyRecurringReq = false;
                            }
                        }

                        $scope.RecurringFromPeriod = msg.data._GetRightsSelling[0].Recurring_From_Period;
                        $scope.RecurringToPeriod = msg.data._GetRightsSelling[0].Recurring_To_Period;
                       

                        if (msg.data._GetRightsSelling[0].FrequencyID != null) {                                                      
                            $scope.Frequency = msg.data._GetRightsSelling[0].FrequencyID;
                        }
                        else {
                            setTimeout(function () { $('#Frequency').val($('#Frequency option:first').val()); }, 1200)
                          
                        }




                        $scope.LanguageValue = [];
                        if (msg.data.mobj_language != null) {
                           

                            for (var i = 0; i <= msg.data.mobj_language.length - 1; i++) {

                                $scope.LanguageValue.push("" + msg.data.mobj_language[i].languageId + "");
                                // $scope.Division.push(95);
                            }

                        }


                        $scope.Remarks = msg.data._GetRightsSelling[0].RemarksInsert;


                    $scope.RightsSellingRoyaltySalbUpdate();
                       
                    }
                   

                }
            });
            }
    }

    $scope.fn_blank = function () {
        //$scope.PrintRunQuantity = '';
        $('input[name=PrintRunQuantity]').val('');
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


    function unique(list) {
        var result = [];
        $.each(list, function (i, e) {
            if ($.inArray(e, result) == -1) result.push(e);
        });
        return result;
    }
    $scope.removeroyalslab = function (obj) {
        
        var _table = $(obj).closest(".RoyaltySlab");
        $(obj).closest("tr").remove();

        $scope.RoyaltySlabManagement();
    }
    $scope.addroyalslabbyJquery = function (obj) {
       
        var _trClone = $(obj).closest("tr").clone(true);
        _trClone.find('input').val("");
        _trClone.find('select').val("");
        _trClone.find('input').removeAttr("disabled");
        $(obj).closest(".RoyaltySlab").append(_trClone);
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


    $scope.RightsSellingRoyaltySalbUpdate = function () {
     
        var _mobjRightsSelling = {
            ContractId: $('#hid_Type').val() == "A" ? $("#hid_AuthorContract").val() : null,
            ProductLicenseId: $('#hid_Type').val() == "P" ? $("#hid_AuthorContract").val() : null,
            RightsSellingID: ($('#hid_RightsSellingId').val() =="" ? 0 : $('#hid_RightsSellingId').val())
        };

        var ExecutiveStatus = AJService.PostDataToAPI('RightsSelling/RightsSellingRoyaltySalbView', _mobjRightsSelling);


        ExecutiveStatus.then(function (msg) {
            if (msg.data[0] != null) {

                $scope.RoyaltyslabList = msg.data;
               

                setTimeout(function () {
                 
                    if (msg.data.length > 0) {
                        var authorBox = $('.AuthorBoxAddendum');
                        for (var k = 0, s = 0; k < msg.data.length; k++) {
                            var FirstSlab = $($(authorBox)[k]);
                            if (msg.data[k].subproducttypeid != null && msg.data[k].subproducttypeid !="")
                            {
                                $('#SubProductType_' + [k] + '').val(msg.data[k].subproducttypeid);
                            }
                            if (msg.data[k].CopiesFrom != null && msg.data[k].CopiesFrom != "")
                            {
                                $('#CopiesFrom_' + [k] + '').val(msg.data[k].CopiesFrom);
                            }
                         
                            if (msg.data[k].CopiesTo != 0 && msg.data[k].CopiesTo !="")
                            {
                                $('#CopiesTo_' + [k] + '').val(msg.data[k].CopiesTo);
                            }
                          
                            if (msg.data[k].Percentage !=null && msg.data[k].Percentage !="" )
                            {
                                $('#RyPercentage_' + [k] + '').val(msg.data[k].Percentage);
                            }

                            $(FirstSlab).find('.RoyaltySlabLink').css("display", "none");
                            $(FirstSlab).find('.RoyaltySlabnotRemove').css("display", "table-row");
                            s++;
                        }
                    }
                }, 1900)
                
               
            }
        });
    }



    $scope.BackToList = function(){
        $window.location.href = '../RightsSelling/RightsSellingSearch?For=BackToList';
    }



    $scope.GeogList = function () {
        //blockUI.start();
        var GeogType = {
            geogtype: "country",
            parentid: null,
        };
        var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
        GetgeogList.then(function (GetgeogList) {
            $scope.CountryList = GetgeogList.data;
            $scope.OthersNot = true;
            $scope.OthersYes = false;
            $scope.otherCities = false;
            $scope.OtherCountry = false;
            $scope.sates = [];
        }, function () {
            //alert('Error in getting Geographical list');
        });
    }

    $scope.getCountryStates = function () {
        var GeogType = {
            geogtype: "state",
            parentid: $scope.Country,
        };
        if ($.trim($("#Country option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.OthersNot = false;
            $scope.OthersYes = true;
            $scope.OtherCountry = true;
            $scope.cities = [];
        }
        else {
            $scope.OthersNot = true;
            $scope.OthersYes = false;
            $scope.OtherCountry = false;
            $scope.cities = [];
            var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
            GetgeogList.then(function (GetgeogList) {
                $scope.sates = GetgeogList.data;
                $scope.cities = [];
            }, function () {
                //alert('Error in getting Geographical list');
            });
        }
    }
    $scope.getStateCities = function () {
        var GeogType = {
            geogtype: "city",
            parentid: $scope.State,
        };
        if ($.trim($("#state option:selected").html()).toLowerCase().indexOf("others") > -1) {
            //$scope.OthersNot = false;
            //$scope.OthersYes = true;

            $('.othersCityHide').css("display", "none");
            $scope.OthersStateYes = true;
            $scope.cities = [];
        }

        var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
        GetgeogList.then(function (GetgeogList) {
            $scope.cities = GetgeogList.data;
        }, function () {
            //alert('Error in getting Geographical list');
        });

    }

    $scope.showState = function () {
        if ($.trim($("#Country option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.State.css("display", "none");
            $scope.cities = [];
        }
    }

    $scope.ChangeCitiesCities = function () {
        if ($.trim($("#city option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.otherCities = true;
        }
        else {
            $scope.otherCities = false;

        }
    }
    

    $scope.ValidateRoyaltySlabInsert = function (obj) {
        var _table = $(obj).closest(".RoyaltySlab");
        if ($(_table).find("select[name*=SubProductType]").find('option[value="' + $(obj).val() + '"]:selected').closest("select").length == 1) {
            $(obj).closest("tr").find("input[name*=CopiesFrom]").val(1);
            $(obj).closest("tr").find("input[name*=CopiesFrom]").attr("disabled", true);
            //$(obj).closest("tr").find("input[name*=CopiesTo]").attr("disabled", true);
        }
        else {
            var _copiesto = $($(_table).find("select[name*=SubProductType]").find('option[value="' + $(obj).val() + '"]:selected').parents("tr")[1]).find('input[name$=CopiesTo]').val()
            $(obj).closest("tr").find('input[name*=CopiesFrom]').val(parseInt(_copiesto) + 1);
            $(obj).closest("tr").find('input[name*=CopiesFrom]').attr("disabled", true);

            if (obj.val() == "") {
                $(obj).closest("tr").find("input[name*=CopiesFrom]").val("");
                $(obj).closest("tr").find("input[name*=CopiesFrom]").removeAttr("disabled");
            }
        }
    }

    $scope.ValidateRyaltySlab = function () {

        if (unique($("select[name*=SubProductType]").map(function () { return $(this).val() }).get())[0] == "") {
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



    $scope.SetContractDate = function (datetext) {

        $scope.EffectiveDate = $(datetext).val();
        $scope.checkdate(datetext);
    }

    $scope.CalculateExpiry = function () {
      
      
        PeriodIdValue = $scope.userForm.ContractperiodUpload.$modelValue;
        var CDate = $("[name$=DateofAgreement]").val();
        if (PeriodIdValue == undefined || PeriodIdValue == "0" || PeriodIdValue == "" || CDate == undefined || CDate == "" || CDate == "undefined//undefined") {
            $scope.ExpiryDateValue = "";
            return false;
        }

        //var CurrentDate = new Date(convertDate($("[name$=EffectiveDate]").val()));

        var RequestDate = $("[name$=DateofAgreement]").val();

        var date = RequestDate;
        var d = new Date(date.split("/").reverse().join("-"));
        var dd = d.getDate();
        var mm = d.getMonth() + 1;
        var yy = d.getFullYear();
        var newdate = yy + "/" + mm + "/" + dd;

        var CurrentDate = new Date(newdate);
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
        $scope.ExpiryDateValue = today;
        //$scope.ExpiryDate = today;
        $("[name$=ExpiryDateValue]").val(today);
    }


    $scope.getRightProductCategoryList = function () {
        var getRightProductCategoryList = AJService.GetDataFromAPI("RightsSelling/getRightProductCategoryList", null);
        getRightProductCategoryList.then(function (RightProductCategory) {
            $scope.ProductCategoryRightList = RightProductCategory.data;
        }, function () {
            //alert('Error in getting Product Category List');
        });
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


    if ($('#hid_RightsSellingId').val() != null)
    {
        $scope.getRightsSellingLanguageList();
    }

    $scope.func_OneTimePayment = function () {

        $scope.Royalty_Req = false;
        $scope.OneTimePayment_req = true;
    }

    $scope.func_Royalty = function () {
        $scope.OneTimePayment_req = false;
        $scope.Royalty_Req = true;
    }


});


