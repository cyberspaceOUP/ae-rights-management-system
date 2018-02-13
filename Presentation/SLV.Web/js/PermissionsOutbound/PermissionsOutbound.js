
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

    $scope.PageTitle = "Entry";

   
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



    //  $scope.ProductLicenseSerach($("#hid_AuthorContract").val())
    /*Expand Royalty Slab Controller*/

    $("#Country").attr("disabled", "disabled").removeAttr("required");
    $("#state").attr("disabled", "disabled");
    $("#city").attr("disabled", "disabled");
    $("#pincode").attr("disabled", "disabled");
    $("#geogdiv").find('span').attr('style', 'display:none');

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



    $scope.SetContractDate = function (datetext) {
        $scope.Permission = $(datetext).val();
        PeriodIdValue = $scope.PermissionsOutboundModel.PermissionPeriod;
        var CDate = $("[name$=Permission]").val();


        if (PeriodIdValue == undefined || PeriodIdValue == "0" || PeriodIdValue == "" || CDate == undefined || CDate == "" || CDate == "undefined//undefined") {
            $scope.PermissionsOutboundModel.ExpiryDate = "";
            return false;
        }


        var RequestDate = $("[name$=Permission]").val();

        var date = RequestDate;
        var d = new Date(date.split("/").reverse().join("-"));
        var dd = d.getDate();
        var mm = d.getMonth() + 1;
        var yy = d.getFullYear();
        var newdate = yy + "/" + mm + "/" + dd;

        if (PeriodIdValue == undefined || CurrentDate == "") {
            $scope.ProductModel.ExpiryDate = "";
            return false;
        }

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
        $scope.PermissionsOutboundModel.ExpiryDate = today;
        $("[name$=ExpiryDate]").val(today)
    }


    $scope.setContractEffectiveDate = function (datetext) {

        $scope.ContractEffectiveDate = $(datetext).val();
    }


    $scope.setRecurringFromPeriod = function (datetext) {
        $scope.RecurringFromPeriod = $(datetext).val();
    }

    $scope.setRecurringToPeriod = function (datetext) {
        $scope.RecurringToPeriod = $(datetext).val();
    }



    $scope.GetTypeOfRightsList = function () {
        var getTypeOfRightsList = AJService.GetDataFromAPI("PermissionsOutbound/GetTypeOfRightsList?Id=" + $("#enterdBy").val());
        getTypeOfRightsList.then(function (TypeOfRights) {
            $scope.TypeOfRightsList = TypeOfRights.data;
        }, function () {
            //alert('Error in getting Type Of Rights list');
        });
    }


    $scope.funct_RoyaltyRecurringYes = function () {

        $scope.RoyaltyRecurringReq = true;
    }
    $scope.funct_RoyaltyRecurringNo = function () {
        $scope.RoyaltyRecurringReq = false;
    }


    $scope.func_InvoiceApplicableYes = function () {
      
        var mstr_Rights = '';
        var mstr_Ouantity = '';
        var mstr_TypeOfrights = '';
        var mstr_PrintOuantity = '';
        if ($('#TypeofRights').val() != null)
        {
            if ($('#TypeofRights').val().length > 0) {
                for (var i = 0; i < $('#TypeofRights').val().length; i++) {
                    mstr_Rights = mstr_Rights + ", " + $(($('#TypeofRights option:selected')[i])).text();//$('#TypeofRights option:selected').text().split(/(?=[A-Z])/)[i];
                    mstr_Ouantity = mstr_Ouantity + "," + $($('[name*=PrintRunQuantity]')[i]).attr("placeholder") + ' : ' + $($('[name*=PrintRunQuantity]')[i]).val();// + '--' + $($('[name*=PrintRunQuantity]')[i]).val();
                }
                mstr_TypeOfrights = mstr_Rights.slice(1)
                mstr_PrintOuantity = mstr_Ouantity.slice(1)
            }
        }
       
        $scope.PermissionsOutboundModel.InvoiceDescription = "Request Material : " + ($scope.PermissionsOutboundModel.RequestMaterial == undefined ? '---' : $scope.PermissionsOutboundModel.RequestMaterial) + "\n"
                                                            + "Type of Rights : " + (mstr_TypeOfrights == '' ? '---' : mstr_TypeOfrights + '\n' + mstr_PrintOuantity) + "\n"
                                                            + "Language : " + ($('#LanguageId option:selected').text() == 'Please Select' ? '---' : $('#LanguageId option:selected').text()) + "\n"
                                                            + "Territory : " + ($('#ddlTerritory option:selected').text() == 'Please Select' ? '---' : $('#ddlTerritory option:selected').text())

        $scope.ReqInvoiceNo = true;
        $scope.ReqInvoiceCurrency = true;
        $scope.ReqInvoiceValue = true;
        $scope.ReqInvoiceDescription = true;
        $scope.ReqRemarks = false;
    }

    $scope.func_InvoiceApplicableNo = function () {

        $scope.ReqInvoiceNo = false;
        $scope.ReqInvoiceCurrency = false;
        $scope.ReqInvoiceValue = false;
        $scope.ReqInvoiceDescription = false;
       
        $scope.ReqRemarks = true;
    }


    $scope.func_CopiestobereceivedYes = function () {

        $scope.reqNumberofcopies = true;
      
    }


    $scope.func_CopiestobereceivedNo = function () {

        $scope.reqNumberofcopies = false;
        $scope.PermissionsOutboundModel.Numberofcopies = '';
       
    }


    //$scope.func_PaymentReceivedYes = function () {

    //    $scope.ReqPaymentAmount = true;
    //    $scope.ReqCurrency = true;

    //}


    //$scope.func_PaymentReceivedNo = function () {

    //    $scope.ReqPaymentAmount = false;
    //    $scope.ReqCurrency = false;

    //}


    $scope.clear = function ()
    {
        $scope.PermissionsOutboundModel.Licensee = "";
        $scope.PermissionsOutboundModel.Licenseecode = "";
        // $('#OrganizationName option:selected').text('')
        $scope.PermissionsOutboundModel.ContactPerson = "";
        $scope.PermissionsOutboundModel.PublisherAddress = "";
        $scope.Country = "";
        $scope.State = "";
        $scope.City = "";
        $scope.pincode = "";
        $scope.PermissionsOutboundModel.PublisherMobile = "";
        $scope.PermissionsOutboundModel.PublisherEmail = "";
        $scope.PermissionsOutboundModel.URL = "";
       
        $scope.PermissionsOutboundModel.LicenseePublicationTitle = "";
        $scope.Permission = "";
        $scope.PermissionsOutboundModel.PermissionPeriod = "";
        $scope.PermissionsOutboundModel.ExpiryDate = "";
        $scope.PermissionsOutboundModel.RequestMaterial = "";
        $scope.PermissionsOutboundModel.Willbematerialbetranslated = "";
        $scope.PermissionsOutboundModel.WillbematerialbeAdepted = "";
        $scope.PermissionsOutboundModel.Language = "";
        $scope.PermissionsOutboundModel.Extent = "";
        $scope.PermissionsOutboundModel.TerritoryRight = "";
        $scope.PermissionsOutboundModel.DateofInvoice = "";
        $scope.PermissionsOutboundModel.InvoiceApplicable = "";
        $scope.PermissionsOutboundModel.InvoiceNo = "";
        $scope.PermissionsOutboundModel.InvoiceCurrency = "";
        $scope.PermissionsOutboundModel.InvoiceValue = "";
        $scope.PermissionsOutboundModel.InvoiceDescription = "";
        $scope.PermissionsOutboundModel.Copiestobereceived = "";
        $scope.PermissionsOutboundModel.Numberofcopies = "";
        $scope.PermissionsOutboundModel.PaymentReceived = "";
        $scope.PermissionsOutboundModel.Remarks = "";
        $scope.PermissionsOutboundModel.TypeofRights = "";
        $scope.SupplyRunQuantityById = "";
        $scope.LanguageReq = false;
        $scope.reqNumberofcopies = false;
        $scope.ReqRemarks = false;
        $scope.ReqInvoiceNo = false;
        $scope.ReqInvoiceCurrency = false;
        $scope.ReqInvoiceValue = false;
        $scope.ReqInvoiceDescription = false;
    }



    $scope.PermissionsOutboundEntry = function () {





        var SupplyTypeOfRightsInsert = [];
        if ($scope.SupplyRunQuantityById != undefined) {
            for (var i = 0; i < $scope.SupplyRunQuantityById.length; i++) {
                SupplyTypeOfRightsInsert[i] =
                {
                    TypeofRightsId: $scope.SupplyRunQuantityById[i].Id,
                    Quantity: $("#SupplyRunQuantityByOutboundId_" + $scope.SupplyRunQuantityById[i].Id + "").val()
                }
            }
        }

        var mstr_RequestDate = $('#RequestDate').val();
        var mstr_Permission = $('#Permission').val();
        var mstr_DateofInvoice = $('#DateofInvoice').val();
        var mstr_ExpiryDate = $('#ExpiryDate').val();
        var mstr_DateOfAgreementValue = $('#DateOfAgreementValue').val();
        var mstr_Signed_Contract_Sent_DateValue = $('#Signed_Contract_Sent_DateValue').val();
        var mstr_Signed_Received_Sent_DateValue = $('#Signed_Received_Sent_DateValue').val();

        var mstr_Cancellation_DateValue = $('#Cancellation_DateValue').val();


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



        if (mstr_Permission == "") {
            mstr_Permission = null
        }
        else {

            var RequestDate = mstr_Permission;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            mstr_Permission = yy + "/" + mm + "/" + dd;
        }




        if (mstr_DateofInvoice == "") {
            mstr_DateofInvoice = null
        }
        else {

            var RequestDate = mstr_DateofInvoice;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            mstr_DateofInvoice = yy + "/" + mm + "/" + dd;
        }





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


        if (mstr_DateOfAgreementValue == "" || mstr_DateOfAgreementValue == undefined) {
            mstr_DateOfAgreementValue = null
        }
        else {

            var RequestDate = mstr_DateOfAgreementValue;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            mstr_DateOfAgreementValue = yy + "/" + mm + "/" + dd;
        }


        if (mstr_Signed_Contract_Sent_DateValue == "" || mstr_Signed_Contract_Sent_DateValue == undefined) {
            mstr_Signed_Contract_Sent_DateValue = null
        }
        else {

            var RequestDate = mstr_Signed_Contract_Sent_DateValue;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            mstr_Signed_Contract_Sent_DateValue = yy + "/" + mm + "/" + dd;
        }


        if (mstr_Signed_Received_Sent_DateValue == "" || mstr_Signed_Received_Sent_DateValue == undefined) {
            mstr_Signed_Received_Sent_DateValue = null
        }
        else {

            var RequestDate = mstr_Signed_Received_Sent_DateValue;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            mstr_Signed_Received_Sent_DateValue = yy + "/" + mm + "/" + dd;
        }

        if (mstr_Cancellation_DateValue == "" || mstr_Cancellation_DateValue == undefined) {
            mstr_Cancellation_DateValue = null
        }
        else {

            var RequestDate = mstr_Cancellation_DateValue;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            mstr_Cancellation_DateValue = yy + "/" + mm + "/" + dd;
        }

        var mstr_EffectiveDate = $('#EffectiveDate').val();
        if (mstr_EffectiveDate == "" || mstr_EffectiveDate == undefined) {
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





        FileNameArray = $('#dropZone0').find('.fileNameClass');
        var array = [];
        FileNameArray.each(function () {
            array.push(
                $(this).val()
            );
        });


        var _mobPermissionsOutbound = {
            LicenseeID: $scope.PermissionsOutboundModel.Licensee,
            Licenseecode: $scope.PermissionsOutboundModel.Licenseecode,
            OrganizationName: $scope.Licensee, // $('#OrganizationName option:selected').text(),
            ContactPerson: $scope.PermissionsOutboundModel.ContactPerson,
            Address: $scope.PermissionsOutboundModel.PublisherAddress,
            CountryId: $scope.Country,
            Stateid: $scope.State,
            Cityid: $scope.City,
            Pincode: $scope.pincode,
            Mobile: $scope.PermissionsOutboundModel.PublisherMobile,
            Email: $scope.PermissionsOutboundModel.PublisherEmail,
            URL: $scope.PermissionsOutboundModel.URL,
            RequestDate: mstr_RequestDate,
            LicenseePublicationTitle: $scope.PermissionsOutboundModel.LicenseePublicationTitle,
            DateOfPermission: mstr_Permission,// $scope.Permission ,
            PermissionPeriod: $scope.PermissionsOutboundModel.PermissionPeriod,
            DateExpiry: mstr_ExpiryDate,
            RequestMaterial: $scope.PermissionsOutboundModel.RequestMaterial,
            Will_be_material_be_translated: $scope.PermissionsOutboundModel.Willbematerialbetranslated,
            Will_be_material_be_adepted: $scope.PermissionsOutboundModel.WillbematerialbeAdepted,
            //  LanguageId: $scope.PermissionsOutboundModel.Language,

            Language: $scope.PermissionsOutboundModel.Language,
            Extent: $scope.PermissionsOutboundModel.Extent,
            TerritoryId: $scope.PermissionsOutboundModel.TerritoryRight,
            DateOfInvoice: mstr_DateofInvoice,//$scope.PermissionsOutboundModel.DateofInvoice,
            InvoiceApplicable: $scope.PermissionsOutboundModel.InvoiceApplicable,
            InvoiceNo: $scope.PermissionsOutboundModel.InvoiceNo,
            InvoiceCurrency: $scope.PermissionsOutboundModel.InvoiceCurrency,
            InvoiceValue: $scope.PermissionsOutboundModel.InvoiceValue,
            InvoiceDescription: $scope.PermissionsOutboundModel.InvoiceDescription,
            Copies_To_Be_Received: $scope.PermissionsOutboundModel.Copiestobereceived,
            NumberOfCopies: $scope.PermissionsOutboundModel.Numberofcopies,
            /// PaymentReceived: $scope.PermissionsOutboundModel.PaymentReceived,
            Remarks: $scope.PermissionsOutboundModel.Remarks,
            SupplyTypeOfRights: SupplyTypeOfRightsInsert,
            EnteredBy: $("#enterdBy").val(),
            Id: $('#hid_PermissionsOutbound').val() == "" ? 0 : $('#hid_PermissionsOutbound').val(),

            ContractStatus: $scope.PermissionsOutboundModel.Contractstatus,
            PaymentReceived: $scope.PermissionsOutboundModel.PaymentReceived,
            PaymentAmount: $scope.PermissionsOutboundModel.PaymentAmount,
            CurrencyId: $scope.PermissionsOutboundModel.CurrencyValue,
            Date_of_agreement: mstr_DateOfAgreementValue,
            Signed_Contract_sent_date: mstr_Signed_Contract_Sent_DateValue,
            Signed_Contract_receiveddate: mstr_Signed_Received_Sent_DateValue,
            CancellationDate: mstr_Cancellation_DateValue,
            Cancellation_Reason: $scope.PermissionsOutboundModel.Cancellation_Reason,
            Contributor_Agreement: $scope.PermissionsOutboundModel.ContributorAgreement,
            PendingRemarks: $scope.PermissionsOutboundModel.PendingRemarks,
            Documentname: array,
            DocumentFile: $("#hid_Uploads").val(),
            ProductCode: $('#hid_ProductCodeValue').val(),
            UserProfile: $('#hid_UserRight').val(),
            productid: $('#hid_ProductId').val(),
            ContactId: $("#hid_AuthorContract").val(),
            Type: $('#hid_Type').val(),
            EffectiveDate: mstr_EffectiveDate,
            //AgreementDate: mstr_DateOfAgreement
            Contractperiodinmonth: 0, // $scope.PermissionsOutboundModel.ContractperiodUpload   ////----Commented by Prakash on 31 May, 2017

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

                   var PermissionsOutboundStatus = AJService.PostDataToAPI('PermissionsOutbound/InsertPermissionsOutbound', _mobPermissionsOutbound);

                   PermissionsOutboundStatus.then(function (msg) {


                       if (msg.data.status != "OK") {
                           SweetAlert.swal('There is some problem.', '', "Try agian");
                       }
                       else {
                           if ($('#hid_PermissionsOutbound').val() != "") {

                               SweetAlert.swal({
                                   title: "Updated successfully.",
                                   text: "",
                                   type: "success"
                               },
                              function () {
                                  //$('form[name*=user]').attr("method", "post");
                                  //$('form[name*=user]').submit();
                                  //if ($('#hid_Type').val() == "A") {
                                  //    $window.location.href = "../../Contract/AuthorContract/AuthorContractSearch?For=PermissionsOutbound&Back=BackToserach";
                                  //}
                                  //else if ($('#hid_Type').val() == "P") {
                                  //    $window.location.href = "../../Product/ProductLicense/ProductLicenseSearch?For=PermissionsOutbound&Back=BackToserach";
                                  //}
                                  //  $window.location.href = "../../PermissionsOutbound/PermissionsOutbound/PermissionsOutboundSearchMaster?For=BackToSearch";
                                  //location.href = "../../Home/Dashboard/Dashboard";

                                  if ($('#hid_Type').val() == "A") {
                                      location.href = "../../PermissionsOutbound/PermissionsOutbound/PermissionsOutboundMaster?Id=" + $("#hid_AuthorContract").val() + "&type=A" + $('#hid_ProductId').val() + "&OutboundView=" + $('#hid_PermissionsOutbound').val();
                                  }
                                  else if ($('#hid_Type').val() == "P") {
                                      location.href = "../../PermissionsOutbound/PermissionsOutbound/PermissionsOutboundMaster?Id=" + $("#hid_AuthorContract").val() + "&type=P" + $('#hid_ProductId').val() + "&OutboundView=" + $('#hid_PermissionsOutbound').val();
                                  }
                              });
                           }
                           else {


                               SweetAlert.swal({
                                   title: "Insert successfully.",
                                   text: "Permissions Outbound Code : " + msg.data.PermissionsOutbound_CodeValue + "",
                                   type: "success"
                               },
                              function () {
                                  //location.href = "../../Home/Dashboard/Dashboard";

                                  if ($('#hid_Type').val() == "A") {
                                      location.href = "../../PermissionsOutbound/PermissionsOutbound/PermissionsOutboundMaster?Id=" + $("#hid_AuthorContract").val() + "&type=A" + $('#hid_ProductId').val() + "&OutboundView=" + msg.data.PermissionsOutboundIdId;
                                  }
                                  else if ($('#hid_Type').val() == "P") {
                                      location.href = "../../PermissionsOutbound/PermissionsOutbound/PermissionsOutboundMaster?Id=" + $("#hid_AuthorContract").val() + "&type=P" + $('#hid_ProductId').val() + "&OutboundView=" + msg.data.PermissionsOutboundIdId;
                                  }
                              });



                               //SweetAlert.swal('Insert successfully.', 'Permissions Outbound Code : ' + msg.data.PermissionsOutbound_CodeValue + '', '', "success")


                               //setTimeout(function () {
                               //    if ($('#hid_Type').val() == "A") {
                               //        $window.location.href = "../../Contract/AuthorContract/AuthorContractSearch?For=PermissionsOutbound&Back=BackToserach";
                               //    }
                               //    else if ($('#hid_Type').val() == "P") {
                               //        $window.location.href = "../../Product/ProductLicense/ProductLicenseSearch?For=PermissionsOutbound&Back=BackToserach";
                               //    }

                               //}, 4000)


                               //  SweetAlert.swal({
                               //      title: 'Insert successfully, Permissions Outbound Code : ' + msg.data.PermissionsOutbound_CodeValue + '',
                               //      text: "",
                               //      type: "success"
                               //  },
                               //function () {

                               //    if ($('#hid_Type').val() == "A")
                               //    {
                               //        $window.location.href = "../../Contract/AuthorContract/AuthorContractSearch?For=PermissionsOutbound&Back=BackToserach";
                               //    }
                               //    else if ($('#hid_Type').val() == "P")
                               //    {
                               //        $window.location.href = "../../Product/ProductLicense/ProductLicenseSearch?For=PermissionsOutbound&Back=BackToserach";
                               //    }

                               //    //$('form[name*=user]').attr("method", "post");
                               //    //$('form[name*=user]').submit();
                               //});



                               //$('form[name*=user]').attr("method", "post");
                               //$('form[name*=user]').submit();

                               //$scope.clear();
                               //   setTimeout(function () {
                               //     window.location.href = window.location.href;
                               // }, 3000)
                               //  setImmediate(function () {

                               //  }, 3000)



                           }
                       }

                   },
                   function () {
                       alert('There is some error in the system');
                   });

               }

           });

    }
   


    //$scope.CalculateExpiry = function () {

    //    //var PeriodIdValue = $scope.PermissionsOutboundModel.PermissionPeriod;
    //    //var RequestDate = $("[name$=RequestDate]").val();

    //    //var date = RequestDate;
    //    //var d = new Date(date.split("/").reverse().join("-"));
    //    //var dd = d.getDate();
    //    //var mm = d.getMonth() + 1;
    //    //var yy = d.getFullYear();
    //    //var newdate = yy + "/" + mm + "/" + dd;




    //    //if (PeriodIdValue == undefined || CurrentDate == "") {
    //    //    $scope.ProductModel.ExpiryDate = "";
    //    //    return false;
    //    //}

    //    //var CurrentDate = new Date(newdate);

    //    //CurrentDate.setMonth(CurrentDate.getMonth() + parseInt(PeriodIdValue));
    //    //var today = CurrentDate;
    //    //var dd = today.getDate();
    //    //var mm = today.getMonth() + 1;

    //    //var yyyy = today.getFullYear();
    //    //if (dd < 10) {
    //    //    dd = '0' + dd
    //    //}
    //    //if (mm < 10) {
    //    //    mm = '0' + mm
    //    //}
    //    //var today = dd + '/' + mm + '/' + yyyy;
    //    //$scope.PermissionsOutboundModel.ExpiryDate = today;



    //    //debugger;

    //    PeriodIdValue = $scope.PermissionsOutboundModel.PermissionPeriod;
    //    var CDate = $("[name$=Permission]").val();

        
    //    if (PeriodIdValue == undefined || PeriodIdValue == "0" || PeriodIdValue == "" || CDate == undefined || CDate == "" || CDate == "undefined//undefined") {
    //        $scope.PermissionsOutboundModel.ExpiryDate = "";
    //        return false;
    //    }


    //    var RequestDate = $("[name$=Permission]").val();

    //    var date = RequestDate;
    //    var d = new Date(date.split("/").reverse().join("-"));
    //    var dd = d.getDate();
    //    var mm = d.getMonth() + 1;
    //    var yy = d.getFullYear();
    //    var newdate = yy + "/" + mm + "/" + dd;


    //    //  var CurrentDate = new Date(convertDate($("[name$=Permission]").val()));

    //    if (PeriodIdValue == undefined || CurrentDate == "") {
    //        $scope.ProductModel.ExpiryDate = "";
    //        return false;
    //    }

    //    var CurrentDate = new Date(newdate);

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
    //   // $scope.ExpiryDate = today;
    //    $scope.PermissionsOutboundModel.ExpiryDate = today;

    // ///   debugger;
    //    $("[name$=ExpiryDate]").val(today)
       
        
    //}
 


    $scope.GetValue = function (obj) {
       
        if (obj[0].SubTypeRights != null)
            $scope.SupplyRunQuantityById = obj;
        else
            $scope.SupplyRunQuantityById = [];

        //setTimeout(function () {
        //    $scope.removeValidation();
        //}, 100)
    }


    $scope.PermissionsOutboundEntryForm = function (PermissionsOutboundModel) {

        var errorDiv;
        var errormsg = '';
        $scope.msg = "";

       

     

        if ($('#hid_ReqValue').val() == "0") {

            if ($scope.PermissionsOutboundModel.Contractstatus != null) {
                if ($scope.PermissionsOutboundModel.Contractstatus.toLowerCase() == "issued") {
                    FileNameArray = $('#dropZone0').find('.fileNameClass');


                    if ($('#Signed_Contract_Sent_Date').val() != "" && $('#Signed_Received_Sent_Date').val() != "") {
                        $scope.userForm.$valid = true;
                    }

                    if (FileNameArray[0] == null) {
                       //$scope.UploadExcelfileNameReq = false;
                        if ($scope.Docurl1.length == 0) {
                            $scope.UploadContractReq = true;

                            $scope.userForm.$valid = false;
                        }

                    }
                    else {

                        if ($('.fileNameClass').val() == "") {
                            $scope.UploadContractReq = false;
                            $scope.UploadExcelfileNameReq = true;
                            $scope.userForm.$valid = false;
                            //setTimeout(function () {
                            //    $('#UploadContractReq').css("display", "none");
                            //}, 100)
                            //$('#UploadContractReq').css("display", "none");
                        }
                        else {
                            $scope.UploadContractReq = false;
                            $scope.UploadExcelfileReq = false;

                            $scope.userForm.$valid = true;
                        }


                    }


                }

                else if ($scope.PermissionsOutboundModel.Contractstatus.toLowerCase() == "cancelled" && $scope.PermissionsOutboundModel.Cancellation_Reason != null) {
                    if ($('#Cancellation_Date').val() != "") {
                        $scope.userForm.$valid = true;
                    }
                }
                else if ($scope.PermissionsOutboundModel.Contractstatus.toLowerCase() == "pending") {



                    if ($('[name=PendingRemarks]').val() != "") {
                        $scope.userForm.$valid = true;
                    }
                    else {
                        $scope.userForm.$valid = false;
                    }

                }


            }


        }

        $scope.submitted = true;
        if ($scope.userForm.$valid) {
           
            if ($scope.userForm.$valid) {
                $scope.PermissionsOutboundEntry(PermissionsOutboundModel);
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    }


    $scope.getLicenseeList = function () {
        var getLicenseeList = AJService.GetDataFromAPI("PermissionsOutbound/getLicenseeList", null);
        getLicenseeList.then(function (Language) {
            $scope.LicenseeList = Language.data;
        }, function () {
            //alert('Error in getting Language List');
        });
    }


    //$scope.onchnagLicensee = function () {

    //    var LicenseeDetail = {
    //        Id: $scope.PermissionsOutboundModel.Licensee,
    //        EnteredBy: $("#enterdBy").val()
    //    };

    //    // call API to fetch temp product type list basis on the FlatId
    //    var LicenseeDetailStatus = AJService.PostDataToAPI('PermissionsOutbound/LicenseeDetails', LicenseeDetail);
    //    LicenseeDetailStatus.then(function (msg) {
    //        if (msg != null) {


    //            $scope.PermissionsOutboundModel.Licensee = $scope.PermissionsOutboundModel.Licensee;
    //            $scope.PermissionsOutboundModel.ContactPerson = msg.data.ContactPerson;

    //            $scope.PermissionsOutboundModel.LicenseeId = msg.data.Id;

    //            $scope.PermissionsOutboundModel.Licenseecode = msg.data.Licenseecode;

    //            $scope.PermissionsOutboundModel.PublisherPhone = msg.data.Phone;
    //            $scope.PermissionsOutboundModel.PublisherMobile = msg.data.Mobile;
    //            $scope.PermissionsOutboundModel.PublisherEmail = msg.data.Email;
    //            $scope.PermissionsOutboundModel.PublisherAddress = msg.data.Address;

    //            $scope.PermissionsOutboundModel.URL = msg.data.URL;

    //            $scope.pincode = msg.data.Pincode;
    //            $scope.Country = msg.data.CountryId;

    //            $scope.getCountryStates();
    //            $scope.State = msg.data.Stateid;

    //            $scope.getStateCities();
    //            $scope.City = msg.data.Cityid;

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
        var obj = $("[name$=Licensee]");

        var LicenseeList = [];

        var getLicenseeList = AJService.GetDataFromAPI("PermissionsOutbound/getLicenseeList", null);
        getLicenseeList.then(function (LicenseeData) {
            for (i = 0; i < LicenseeData.data.length; i++) {
                LicenseeList[i] = { "label": LicenseeData.data[i].OrganizationName, "value": LicenseeData.data[i].OrganizationName, "data": LicenseeData.data[i].Id };
            }

            $(obj).autocomplete({
                source: function (request, response) {
                    var matcher = new RegExp("^" + request.term, "i"); //RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
                    response($.grep(LicenseeList, function (item) {
                        return matcher.test(item.label);
                    }));
                },

                autoFocus: true,
                select: function (event, ui) {
                    $scope.Licensee = ui.item.value;
                    $scope.PermissionsOutboundModel.Licensee = ui.item.data;

                    //------------Fill licensee Details
                    var LicenseeDetail = {
                        Id: $scope.PermissionsOutboundModel.Licensee,
                        EnteredBy: $("#enterdBy").val()
                    };

                    // call API to fetch temp product type list basis on the FlatId
                    var LicenseeDetailStatus = AJService.PostDataToAPI('PermissionsOutbound/LicenseeDetails', LicenseeDetail);
                    LicenseeDetailStatus.then(function (msg) {
                        if (msg != null) {


                            $scope.PermissionsOutboundModel.Licensee = $scope.PermissionsOutboundModel.Licensee;
                            $scope.PermissionsOutboundModel.ContactPerson = msg.data.ContactPerson;

                            $scope.PermissionsOutboundModel.LicenseeId = msg.data.Id;

                            $scope.PermissionsOutboundModel.Licenseecode = msg.data.Licenseecode;

                            $scope.PermissionsOutboundModel.PublisherPhone = msg.data.Phone;
                            $scope.PermissionsOutboundModel.PublisherMobile = msg.data.Mobile;
                            $scope.PermissionsOutboundModel.PublisherEmail = msg.data.Email;
                            $scope.PermissionsOutboundModel.PublisherAddress = msg.data.Address;

                            $scope.PermissionsOutboundModel.URL = msg.data.URL;

                            $scope.pincode = msg.data.Pincode;
                            $scope.Country = msg.data.CountryId;

                            $scope.getCountryStates();
                            $scope.State = msg.data.Stateid;

                            $scope.getStateCities();
                            $scope.City = msg.data.Cityid;

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
    //---End Autocomplete for Licensee



    $scope.func_Willbematerialbetranslated_yes = function () {
        $scope.LanguageReq = true;
    }
    $scope.func_Willbematerialbetranslated_No = function () {
        $scope.LanguageReq = false;
    }

 
    //$scope.getFormattedDate = function (date)
    //{
    //    debugger;
    //    var year = date.getFullYear();
    //    var month = (1 + date.getMonth()).toString();
    //    month = month.length > 1 ? month : '0' + month;
    //    var day = date.getDate().toString();
    //    day = day.length > 1 ? day : '0' + day;
    //    return month + '/' + day + '/' + year;
    //}

   



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

               
                $scope.PermissionsOutboundModel.Language = [];
                if (msg.data.mobj_language != null) {
                   

                    for (var i = 0; i <= msg.data.mobj_language.length - 1; i++) {

                        $scope.PermissionsOutboundModel.Language.push("" + msg.data.mobj_language[i].languageId + "");
                        // $scope.Division.push(95);
                    }

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



                $scope.PermissionsOutboundModel.Id = msg.data._GetPermissionsOutBound[0].licenseeid;
                $scope.PermissionsOutboundModel.ContactPerson = msg.data._GetPermissionsOutBound[0].contactperson;

                $scope.PermissionsOutboundModel.LicenseeId = msg.data._GetPermissionsOutBound[0].licenseeid;

                $scope.PermissionsOutboundModel.Licensee = msg.data._GetPermissionsOutBound[0].licenseeid;
                $scope.Licensee = msg.data._GetPermissionsOutBound[0].organizationname;

                $scope.PermissionsOutboundModel.Licenseecode = msg.data._GetPermissionsOutBound[0].licenseecode;

              
                $scope.PermissionsOutboundModel.PublisherMobile = msg.data._GetPermissionsOutBound[0].mobile;
                $scope.PermissionsOutboundModel.PublisherEmail = msg.data._GetPermissionsOutBound[0].email;
                $scope.PermissionsOutboundModel.PublisherAddress = msg.data._GetPermissionsOutBound[0].address;

                $scope.PermissionsOutboundModel.URL = msg.data._GetPermissionsOutBound[0].URL;

                $scope.pincode = msg.data._GetPermissionsOutBound[0].pincode;
                $scope.Country = msg.data._GetPermissionsOutBound[0].countryid;

                $scope.getCountryStates();
                $scope.State = msg.data._GetPermissionsOutBound[0].stateid;

                $scope.getStateCities();
                $scope.City = msg.data._GetPermissionsOutBound[0].cityid;

                setTimeout(function () {
                    $scope.getStateCities();
                    $scope.City = msg.data._GetPermissionsOutBound[0].cityid;
                }, 250);
               



                $scope.PermissionsOutboundCodeView = (msg.data._GetPermissionsOutBound[0].permissionsoutboundcode == null ? '---' : msg.data._GetPermissionsOutBound[0].permissionsoutboundcode);
                $scope.RequestDateView = (msg.data._GetPermissionsOutBound[0].RequestDateView ==  null ? '---' : msg.data._GetPermissionsOutBound[0].RequestDateView) ;
                $scope.LicenseePublicationTitleView = (msg.data._GetPermissionsOutBound[0].licenseepublicationtitle == null ? '---' : msg.data._GetPermissionsOutBound[0].licenseepublicationtitle);
                $scope.PermissionDateView = (msg.data._GetPermissionsOutBound[0].DateOfPermissionView == null ? '---' : msg.data._GetPermissionsOutBound[0].DateOfPermissionView);
                $scope.PermissionperiodView = (msg.data._GetPermissionsOutBound[0].permissionperiod == null ? '---' : msg.data._GetPermissionsOutBound[0].permissionperiod);
                $scope.ExpiryDateView = (msg.data._GetPermissionsOutBound[0].DateExpiryView == null ? '---' : msg.data._GetPermissionsOutBound[0].DateExpiryView);
                $scope.RequestMaterialView = (msg.data._GetPermissionsOutBound[0].requestmaterial == null ? '---' : msg.data._GetPermissionsOutBound[0].requestmaterial);
                $scope.WillmaterialtranslatedView = (msg.data._GetPermissionsOutBound[0].will_be_material_be_translated == null ? '---' : msg.data._GetPermissionsOutBound[0].will_be_material_be_translated);
                $scope.WillmaterialadeptedView = (msg.data._GetPermissionsOutBound[0].will_be_material_be_adepted == null ? '---' : msg.data._GetPermissionsOutBound[0].will_be_material_be_adepted);



                $scope.LanguageView = (msg.data._GetPermissionsOutBound[0].languagename == null ? '---' : msg.data._GetPermissionsOutBound[0].languagename);
                $scope.ExtentView = (msg.data._GetPermissionsOutBound[0].extent == null ? '---' : msg.data._GetPermissionsOutBound[0].extent);
                $scope.TerritoryView = (msg.data._GetPermissionsOutBound[0].territoryrights == null ? '---' : msg.data._GetPermissionsOutBound[0].territoryrights);
                $scope.DateInvoiceView = (msg.data._GetPermissionsOutBound[0].DateOfInvoiceView == null ? '---' : msg.data._GetPermissionsOutBound[0].DateOfInvoiceView);
                $scope.InvoiceApplicableView = (msg.data._GetPermissionsOutBound[0].invoiceapplicable == null ? '---' : msg.data._GetPermissionsOutBound[0].invoiceapplicable);
                $scope.InvoiceNoView = (msg.data._GetPermissionsOutBound[0].invoiceno == null ? '---' : msg.data._GetPermissionsOutBound[0].invoiceno);
                //$scope.InvoiceCurrencyView = (msg.data._GetPermissionsOutBound[0].InvoiceCurrency == null ? '---' : msg.data._GetPermissionsOutBound[0].InvoiceCurrency);
                $scope.InvoiceCurrencyView = (msg.data._GetPermissionsOutBound[0].InvoiceCurrencyName == null ? '---' : msg.data._GetPermissionsOutBound[0].InvoiceCurrencyName);
                $scope.InvoiceCurrencySymbol = (msg.data._GetPermissionsOutBound[0].InvoiceCurrencySymbol == null ? '---' : msg.data._GetPermissionsOutBound[0].InvoiceCurrencySymbol);
                $scope.InvoiceValueView = (msg.data._GetPermissionsOutBound[0].invoicevalue == null ? '---' : msg.data._GetPermissionsOutBound[0].invoicevalue);
                $scope.InvoiceDescriptionView = (msg.data._GetPermissionsOutBound[0].invoicedescription == null ? '---' : msg.data._GetPermissionsOutBound[0].invoicedescription);


                $scope.CopiesreceivedView = (msg.data._GetPermissionsOutBound[0].copies_to_be_received == null ? '---' : msg.data._GetPermissionsOutBound[0].copies_to_be_received);
                $scope.NumbercopiesView = (msg.data._GetPermissionsOutBound[0].numberofcopies == null ? '---' : msg.data._GetPermissionsOutBound[0].numberofcopies);
                $scope.RemarksView = (msg.data._GetPermissionsOutBound[0].remarks == null ? '---' : msg.data._GetPermissionsOutBound[0].remarks);


                if (msg.data._GetPermissionsOutBoundUpdate[0] != undefined) {
                    $scope.ContractStatusView = (msg.data._GetPermissionsOutBoundUpdate[0].contractstatus == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].contractstatus);
                    $scope.PaymentReceivedView = (msg.data._GetPermissionsOutBoundUpdate[0].paymentreceived == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].paymentreceived);
                    $scope.PaymentAmountView = (msg.data._GetPermissionsOutBoundUpdate[0].paymentamount == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].paymentamount);
                    $scope.CurrencyView = (msg.data._GetPermissionsOutBoundUpdate[0].currencyname == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].currencyname);
                    $scope.DateOfAgreementView = (msg.data._GetPermissionsOutBoundUpdate[0].Date_of_agreement == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].Date_of_agreement);
                    $scope.SignedContractSentDateView = (msg.data._GetPermissionsOutBoundUpdate[0].Signed_Contract_sent_date == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].Signed_Contract_sent_date);
                    $scope.SignedContractReceivedDateView = (msg.data._GetPermissionsOutBoundUpdate[0].Signed_Contract_receiveddate == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].Signed_Contract_receiveddate);
                    $scope.CancellationDateView = (msg.data._GetPermissionsOutBoundUpdate[0].CancellationDate == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].CancellationDate);
                    $scope.CancellationReasonView = (msg.data._GetPermissionsOutBoundUpdate[0].cancellation_reason == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].cancellation_reason);
                    $scope.PenddingRemarksView = (msg.data._GetPermissionsOutBoundUpdate[0].PendingRemarks == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].PendingRemarks);
                    $scope.ContributorAgreementView = (msg.data._GetPermissionsOutBoundUpdate[0].contributor_agreement == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].contributor_agreement);


                    $scope.EffectivedateView = (msg.data._GetPermissionsOutBoundUpdate[0].Effectivedate == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].Effectivedate);
                    $scope.ContractperiodinmonthView = (msg.data._GetPermissionsOutBoundUpdate[0].Contractperiodinmonth == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].Contractperiodinmonth);
                    $scope.ExpirydateView = (msg.data._GetPermissionsOutBoundUpdate[0].Expirydate == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].Expirydate);


                }
                else {
                    $scope.ContractstatusReq = true;
                    $scope.PermissionsOutboundUpdateView = false;
                }
            
              


                $scope.PermissionsOutboundModel.RequestDate = msg.data._GetPermissionsOutBound[0].RequestDateView;
                $scope.PermissionsOutboundModel.LicenseePublicationTitle = msg.data._GetPermissionsOutBound[0].licenseepublicationtitle;
                $scope.PermissionsOutboundModel.RequestDate = msg.data._GetPermissionsOutBound[0].RequestDateView;
                $scope.PermissionsOutboundModel.LicenseePublicationTitle = msg.data._GetPermissionsOutBound[0].licenseepublicationtitle;
              //  $scope.Permission = msg.data._GetPermissionsOutBound[0].DateOfPermissionView;
             //   $scope.PermissionsOutboundModel.PermissionPeriod = msg.data._GetPermissionsOutBound[0].permissionperiod;
               // $scope.PermissionsOutboundModel.ExpiryDate = msg.data._GetPermissionsOutBound[0].DateExpiryView;
                $scope.PermissionsOutboundModel.RequestMaterial = msg.data._GetPermissionsOutBound[0].requestmaterial;


                if (msg.data._GetPermissionsOutBound[0].will_be_material_be_translated == "Yes") {
                    $scope.LanguageReq = true;
                }
                else {
                    $scope.LanguageReq = false;
                }


               

                $scope.PermissionsOutboundModel.Willbematerialbetranslated = msg.data._GetPermissionsOutBound[0].will_be_material_be_translated;
                $scope.PermissionsOutboundModel.WillbematerialbeAdepted = msg.data._GetPermissionsOutBound[0].will_be_material_be_adepted;
                //$scope.PermissionsOutboundModel.Language = msg.data._GetPermissionsOutBound[0].languageid;
                $scope.PermissionsOutboundModel.Extent = msg.data._GetPermissionsOutBound[0].extent;
                $scope.PermissionsOutboundModel.TerritoryRight = msg.data._GetPermissionsOutBound[0].territoryid;


                $scope.PermissionsOutboundModel.DateofInvoice = msg.data._GetPermissionsOutBound[0].DateOfInvoiceView;
                $scope.PermissionsOutboundModel.InvoiceApplicable = msg.data._GetPermissionsOutBound[0].invoiceapplicable;


                if (msg.data._GetPermissionsOutBound[0].invoiceapplicable == "No")
                {
                    $scope.ReqRemarks = true;
                }
                
                if (msg.data._GetPermissionsOutBound[0].invoiceno != null)
                {
                    $scope.ReqInvoiceNo = true
                    //$scope.PermissionsOutboundModel.InvoiceNo = parseInt(msg.data._GetPermissionsOutBound[0].invoiceno);
                    $scope.PermissionsOutboundModel.InvoiceNo = msg.data._GetPermissionsOutBound[0].invoiceno;
                }

                if (msg.data._GetPermissionsOutBound[0].InvoiceCurrency != null) {
                    $scope.ReqInvoiceCurrency = true
                    $scope.PermissionsOutboundModel.InvoiceCurrency = msg.data._GetPermissionsOutBound[0].InvoiceCurrency;
                    //$('select[name=InvoiceCurrency]').attr('value', msg.data._GetPermissionsOutBound[0].InvoiceCurrency).attr('selected', 'selected');
                }
                
                if (msg.data._GetPermissionsOutBound[0].invoicevalue != null) {
                    $scope.ReqInvoiceValue = true
                    $scope.PermissionsOutboundModel.InvoiceValue = msg.data._GetPermissionsOutBound[0].invoicevalue;
                }
                
                if (msg.data._GetPermissionsOutBound[0].invoicedescription != null) {
                    $scope.ReqInvoiceDescription = true
                    $scope.PermissionsOutboundModel.InvoiceDescription = msg.data._GetPermissionsOutBound[0].invoicedescription;
                }


                $scope.PermissionsOutboundModel.Copiestobereceived = msg.data._GetPermissionsOutBound[0].copies_to_be_received;

                if (msg.data._GetPermissionsOutBound[0].numberofcopies != null)
                {
                    $scope.reqNumberofcopies = true;
                    $scope.PermissionsOutboundModel.Numberofcopies = msg.data._GetPermissionsOutBound[0].numberofcopies;
                }


              //  $scope.PermissionsOutboundModel.PaymentReceived = msg.data._GetPermissionsOutBound[0].paymentreceived;

                
               // $scope.MaterialSuppliedByAuthorList =     msg.data._OutboundTypeOfRightsMaster;
               
                $scope.PermissionsOutboundModel.Remarks = msg.data._GetPermissionsOutBound[0].remarks;

                $scope.PermissionsOutboundModel.TypeofRights = [];

                $scope.SupplyRunQuantityById = [];
                $scope.PrintRunQuantity = [];


               // $scope.PermissionsOutboundModel.TypeofRights = [];
           
                $scope.PermissionsOutboundList = msg.data._OutboundTypeOfRightsMaster;
               
                for (var i = 0; i <= $scope.PermissionsOutboundList.length - 1; i++) {
                    $scope.SupplyRunQuantityById[i] =
                    {
                        Id: $scope.PermissionsOutboundList[i].TypeofRightsId,
                        SubTypeRights: $scope.PermissionsOutboundList[i].SubTypeRights
                    }


                    
                   // $scope.PrintRunQuantity.push(parseInt($scope.PermissionsOutboundList[i].Quantity))


                    $scope.PrintRunQuantity.push(($scope.PermissionsOutboundList[i].Quantity))
               

                  
                   $scope.PermissionsOutboundModel.TypeofRights[i] = {
                       Id: $scope.PermissionsOutboundList[i].TypeofRightsId,
                      
                    }
                }
             
                var mstr_DateOfAgreementvalue = null

                if (msg.data.mobj_PermissionsOutboundUpdate !=null)
                {
                   
                    if ($('#hid_UserRight').val() == "rt") {

                        $scope.PageTitle = "View";

                        if (msg.data.mobj_PermissionsOutboundUpdate.ContractStatus == 'Issued')
                        {
                            $('.btnSubmit').css("display", "none");
                        }

                      

                    }
                    else {
                      
                        $('.btnSubmit').css("display", "block");
                    }
                }


                if (msg.data.mobj_PermissionsOutboundUpdate.Date_of_agreement != null)
                {
                    mstr_DateOfAgreementvalue = msg.data.mobj_PermissionsOutboundUpdate.Date_of_agreement.slice(0, 10).split('-');
                    var Value = mstr_DateOfAgreementvalue[2] + "/" + mstr_DateOfAgreementvalue[1] + "/" + mstr_DateOfAgreementvalue[0]
                    mstr_DateOfAgreementvalue = Value
                   
                }
                
                var mstr_Signed_Contract_sent_date = null
                if (msg.data.mobj_PermissionsOutboundUpdate.Signed_Contract_sent_date != null) {
                    mstr_Signed_Contract_sent_date = msg.data.mobj_PermissionsOutboundUpdate.Signed_Contract_sent_date.slice(0, 10).split('-');
                    var Value = mstr_Signed_Contract_sent_date[2] + "/" + mstr_Signed_Contract_sent_date[1] + "/" + mstr_Signed_Contract_sent_date[0]
                    mstr_Signed_Contract_sent_date = Value

                }
               

                var mstr_Signed_Contract_receiveddate = null
                if (msg.data.mobj_PermissionsOutboundUpdate.Signed_Contract_receiveddate != null) {
                    mstr_Signed_Contract_receiveddate = msg.data.mobj_PermissionsOutboundUpdate.Signed_Contract_receiveddate.slice(0, 10).split('-');
                    var Value = mstr_Signed_Contract_receiveddate[2] + "/" + mstr_Signed_Contract_receiveddate[1] + "/" + mstr_Signed_Contract_receiveddate[0]
                    mstr_Signed_Contract_receiveddate = Value

                }

                var mstr_CancellationDate = null
                if (msg.data.mobj_PermissionsOutboundUpdate.CancellationDate != null) {
                    mstr_CancellationDate = msg.data.mobj_PermissionsOutboundUpdate.CancellationDate.slice(0, 10).split('-');
                    var Value = mstr_CancellationDate[2] + "/" + mstr_CancellationDate[1] + "/" + mstr_CancellationDate[0]
                    mstr_CancellationDate = Value

                }

                


                $scope.PermissionsOutboundModel.Contractstatus = msg.data.mobj_PermissionsOutboundUpdate.ContractStatus;
                $scope.PermissionsOutboundModel.PaymentReceived = msg.data.mobj_PermissionsOutboundUpdate.PaymentReceived;
                $scope.PermissionsOutboundModel.PaymentAmount = msg.data.mobj_PermissionsOutboundUpdate.PaymentAmount;
                $scope.PermissionsOutboundModel.CurrencyValue = msg.data.mobj_PermissionsOutboundUpdate.CurrencyId;
                $scope.PermissionsOutboundModel.DateOfAgreement = mstr_DateOfAgreementvalue;
                $scope.PermissionsOutboundModel.Signed_Contract_Sent_Date = mstr_Signed_Contract_sent_date;
                $scope.PermissionsOutboundModel.Signed_Contract_received_Date = mstr_Signed_Contract_receiveddate;
                $scope.PermissionsOutboundModel.Cancellation_Date = mstr_CancellationDate;
                $scope.PermissionsOutboundModel.Cancellation_Reason = msg.data.mobj_PermissionsOutboundUpdate.Cancellation_Reason;
                $scope.PermissionsOutboundModel.PendingRemarks = msg.data.mobj_PermissionsOutboundUpdate.PendingRemarks;
                $scope.PermissionsOutboundModel.ContributorAgreement = msg.data.mobj_PermissionsOutboundUpdate.Contributor_Agreement;

                

                $scope.PermissionsOutboundModel.EffectiveDate = msg.data._GetPermissionsOutBoundUpdate[0].Effectivedate
                $scope.PermissionsOutboundModel.ContractperiodUpload = msg.data._GetPermissionsOutBoundUpdate[0].Contractperiodinmonth
                $scope.PermissionsOutboundModel.ExpiryDate = msg.data._GetPermissionsOutBoundUpdate[0].Expirydate




            }
            else {
                SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                blockUI.stop();
            }

        });

    }

 
    $scope.getPermissionsLanguageList = function () {



        var PermissionsLanguageList = AJService.GetDataFromAPI("PermissionsOutbound/getPermissionsLanguageList?Id=" + $('#hid_OutboundId').val() + "", null);

        PermissionsLanguageList.then(function (msg) {
            if (msg != null) {
               
                $scope.PermissionsOutBoundLanguageList = msg.data;

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
        $scope.PageTitle = "Update";
        $scope.EditPermissionsOutBound($('#hid_OutboundId').val());
        $scope.getPermissionsLanguageList();
    }

    $scope.SetExpiryDate = function (datetext) {

        $scope.PermissionsOutboundModel.ExpiryDate = $(datetext).val();
    }

    $scope.SetSigned_Contract_Sent_DateDate = function (datetext) {

        $scope.PermissionsOutboundModel.Signed_Contract_Sent_Date = $(datetext).val();
    }

    $scope.SetSigned_Received_Sent_Date = function (datetext) {

        $scope.PermissionsOutboundModel.Signed_Contract_received_Date = $(datetext).val();
    }

    $scope.SetCancellation_Date = function (datetext) {

        $scope.PermissionsOutboundModel.Cancellation_Date = $(datetext).val();
    }


    $scope.DateOfAgreement = function (datetext) {
 
        $scope.PermissionsOutboundModel.DateOfAgreement = $(datetext).val();

        $("#DateOfAgreementValue").val($(datetext).val());
    
        $scope.PermissionsOutboundModel.EffectiveDate = $(datetext).val();

        $('#EffectiveDate').val($(datetext).val());


        if ($scope.PermissionsOutboundModel.ContractperiodUpload > 0) {
            $scope.CalculateExpiry();
        }


       // $scope.checkdate(datetext);

    }



    $scope.RemoveDocumentLinkById = function (docid, file) {

        SweetAlert.swal({
            title: "Are you sure?",
            text: "You will not be able to recover this detail !",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#8CD4F5",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false,
            closeOnCancel: true
        },
          function (Confirm) {
              if (Confirm) {
                  var AuthorDocument = { Id: docid, EnteredBy: $("#enterdBy").val() };
                  var DeleteDocument = AJService.PostDataToAPI("PermissionsOutbound/RemovePermissionsOutboundDocument", AuthorDocument);

                  DeleteDocument.then(function (msg) {
                      if (msg.data != "OK") {
                          SweetAlert.swal("Oops...", "Please retry!", "error");

                      }
                      else {


                          var obj = {};
                          obj.filename = file;
                          $.ajax({
                              cache: false,
                              type: "POST",
                              contentType: 'application/json; charset=utf-8',
                              url: GlobalredirectPath + "/Common/deletedocument",
                              data: JSON.stringify(obj),
                              dataType: "json",
                              success: function (result) {
                                  if (result == "Deleted") {

                                      SweetAlert.swal({
                                          title: "Deleted!",
                                          text: "Your record  has been deleted.",
                                          type: "success",
                                          confirmButtonText: "OK",
                                          closeOnConfirm: true
                                      },
                                       function (Confirm) {
                                           if (Confirm) {
                                               //blockUI.stop();
                                               $scope.EditPermissionsOutBound($('#hid_OutboundId').val());
                                           }
                                       });

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

          });
    }


    $scope.fun_issued = function ()
    {
      
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


        $scope.PermissionsOutboundModel.EffectiveDate = today;
        $scope.PermissionsOutboundModel.DateOfAgreement = today;
    }

    $scope.fun_Pending = function ()
    {
        $scope.PermissionsOutboundModel.EffectiveDate = "";
        $scope.PermissionsOutboundModel.DateOfAgreement = "";
    }

    $scope.fun_Cancelled = function () {
        $scope.PermissionsOutboundModel.EffectiveDate = "";
        $scope.PermissionsOutboundModel.DateOfAgreement = "";
    }
  
    $scope.CalculateExpiry = function () {

       
        PeriodIdValue = $scope.PermissionsOutboundModel.ContractperiodUpload; //$scope.userForm.ContractperiodUpload.$modelValue;
        var CDate = $("#DateOfAgreementValue").val();
        if (PeriodIdValue == undefined || PeriodIdValue == "0" || PeriodIdValue == "" || CDate == undefined || CDate == "" || CDate == "undefined//undefined") {
            $scope.ExpiryDate = "";
            return false;
        }

        //var CurrentDate = new Date(convertDate($("[name$=EffectiveDate]").val()));

        var RequestDate = $("#DateOfAgreementValue").val();

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
        $scope.PermissionsOutboundModel.ExpiryDate = today;
        $("[name$=ExpiryDate]").val(today);
    }
    
});


