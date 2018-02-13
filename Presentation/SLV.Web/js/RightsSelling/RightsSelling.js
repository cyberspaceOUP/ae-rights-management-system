
app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {
    app.expandControllerTopSearch($scope, AJService, $window);

    /*Expand common.master.js Controller*/
    app.expandControllerA($scope, AJService, $window);
    app.expandControllerAuthorContractDetails($scope, AJService, $window);

    app.expandControllerProductLicense($scope, AJService, $window);

    app.expandControllerProductDetails($scope, AJService, $window);

    app.expandControllerRoyaltySlab($scope, AJService, $window);


    $scope.RoyaltyRecurringReq = false;

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



    //  $scope.ProductLicenseSerach($("#hid_AuthorContract").val())
    /*Expand Royalty Slab Controller*/

    $("#Country").attr("disabled", "disabled").removeAttr("required");
    $("#state").attr("disabled", "disabled");
    $("#city").attr("disabled", "disabled");
    $("#pincode").attr("disabled", "disabled");
    $("#geogdiv").find('span').attr('style', 'display:none');


    $scope.SetContractDate = function (datetext) {
        
        $scope.RightSalesModel.ContractEffectiveDate = $(datetext).val();
        $(".green").find("input[name*=ContractEffectiveDate]").val($(datetext).val());
        $scope.RightSalesModel.ContractDate = $(datetext).val();

        if ($scope.RightSalesModel.Contractperiod > 0)
        {
            $scope.CalculateExpiry();
        }

        $scope.checkdate(datetext);

    }

    $scope.setContractEffectiveDate = function (datetext) {

        $scope.ContractEffectiveDate = $(datetext).val();
    }


    $scope.setRecurringFromPeriod = function (datetext) {
        $scope.RecurringFromPeriod = $(datetext).val();

        $scope.checkdate(datetext);

    }

    $scope.setRecurringToPeriod = function (datetext) {
        $scope.RecurringToPeriod = $(datetext).val();

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


    $scope.funct_RoyaltyRecurringYes = function () {

        $scope.RoyaltyRecurringReq = true;
    }

    $scope.funct_RoyaltyRecurringNo = function () {
        $scope.RoyaltyRecurringReq = false;
    }


    $scope.func_OneTimePayment = function () {

        $scope.Royalty_Req = false;
        $scope.OneTimePayment_req = true;
    }

    $scope.func_Royalty = function () {
        $scope.OneTimePayment_req = false;
        $scope.Royalty_Req = true;
    }

    $scope.CalculateExpiry = function () {

        //$scope.AddendumDate = $(datetext).val();

        PeriodIdValue = $scope.RightSalesModel.Contractperiod;
        var CDate = $("[name$=ContractDate]").val();
        if (PeriodIdValue == undefined || PeriodIdValue == "0" || PeriodIdValue == "" || CDate == undefined || CDate == "" || CDate == "undefined//undefined") {
            $scope.ExpiryDate = "";
            return false;
        }

        var CurrentDate = new Date(convertDate($("[name$=ContractDate]").val()));
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
        $scope.RightSalesModel.ExpiryDate = today;
        $("[name$=ExpiryDate]").val(today);
    }

    function convertDateDDMMYYYY(inputFormat) {
        function pad(s) { return (s < 10) ? '0' + s : s; }
        var d = new Date(inputFormat);
        return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
    }


    function convertDate(date) {
        var datearray = date.split("/");
        return datearray[1] + '/' + datearray[0] + '/' + datearray[2];
    }

    $scope.RightsSellingEntry = function (RightSalesModel) {


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


        var mstr_ContractDate = $('#ContractDate').val();
        if (mstr_ContractDate == "") {
            mstr_ContractDate = null
        }
        else {

            var RequestDate = mstr_ContractDate;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            mstr_ContractDate = yy + "/" + mm + "/" + dd;
        }


        var mstr_FirstImpressionwithindate = $('#FirstImpressionwithindate').val();
        if (mstr_FirstImpressionwithindate == "") {
            mstr_FirstImpressionwithindate = null
        }
        else {

            var RequestDate = mstr_FirstImpressionwithindate;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            mstr_FirstImpressionwithindate = yy + "/" + mm + "/" + dd;
        }


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

        var mstr_ContractEffectiveDate = $('#ContractEffectiveDate').val();
        if (mstr_ContractEffectiveDate == "") {
            mstr_ContractEffectiveDate = null
        }
        else {

            var RequestDate = mstr_ContractEffectiveDate;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            mstr_ContractEffectiveDate = yy + "/" + mm + "/" + dd;
        }


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

        
        var _mobjRightsSelling = {

            RightsSellingRoyalty: RightsSellingRoyalty,
            RightsSellingCode: null,
            LicenseeID: $scope.RightSalesModel.Licensee,
            Licenseecode: $("#hid_LicenceCode").val(),
            OrganizationName: $scope.Licensee, //$(".green").find('select[name*=Licensee]').find('option:selected').text(),
            ContactPerson: $scope.RightSalesModel.ContactPerson,
            Address: $scope.RightSalesModel.PublisherAddress,
            CountryId: $scope.Country,
            OtherCountry: $scope.CountryName,
            Stateid: $scope.State,
            OtherState: $scope.stateName,
            Cityid: $scope.City,
            OtherCity: $scope.cityName,
            Pincode: $scope.pincode,
            Mobile: $scope.RightSalesModel.PublisherMobile,
            Email: $scope.RightSalesModel.PublisherEmail,
            URL: $scope.RightSalesModel.URL,
            //RequestDate: $scope.RightSalesModel.RequestDate,
            RequestDate: mstr_RequestDate, //$('.ng-valid-max').find('input[name=RequestDate]').val(),
            DateContract:  mstr_ContractDate,//$scope.RightSalesModel.ContractDate,
            ContractPeriod: $scope.RightSalesModel.Contractperiod,
            //First_Impression_within_date: $scope.RightSalesModel.FirstImpressionwithindate,
            First_Impression_within_date: mstr_FirstImpressionwithindate,//$('.ng-valid-max').find('input[name=FirstImpressionwithindate]').val(),
            DateExpiry: mstr_ExpiryDate,//$scope.RightSalesModel.ExpiryDate,
            Contract_Effective_Date: mstr_ContractEffectiveDate,//$scope.RightSalesModel.ContractEffectiveDate,
            //TypeofRights: null,
            ProductCategory: $scope.RightSalesModel.ProductCategory,
            Will_be_material_be_translated: $scope.RightSalesModel.Willbematerialbetranslated,
            Language: $scope.RightSalesModel.Language,
            Print_Run_Quantity_Allowed: $scope.RightSalesModel.PrintRunQuantity,
            Number_of_Impression_Allowed: $scope.RightSalesModel.NumberofImpression,
            Advance_Payment: $scope.RightSalesModel.AdvancePayment,
            //Currency: $scope.RightSalesModel.CurrencyValue,
            // Currency : $('.ng-valid-max').find('select[name=Currency]').val(),
            Currency: $('[name*=Currency]').val() == "" ? null : $('[name*=Currency]').val(),

            Payment_Term: $scope.RightSalesModel.PaymentTerm,
            Payment_Amount: $scope.RightSalesModel.PaymentAmount,
            Territory_Rights: $scope.RightSalesModel.TerritoryRight,
            Advance_Royalty_Amount: $scope.RightSalesModel.AdvanceRoyaltyAmount,
            RoyaltyType: $scope.RightSalesModel.RoyaltyType,
            Royalty_Recurring: $scope.RightSalesModel.RoyaltyRecurring,
            //Recurring_From_Period: $scope.RightSalesModel.RecurringFromPeriod,
            //Recurring_To_Period: $scope.RightSalesModel.RecurringToPeriod,
            Recurring_From_Period:  mstr_RecurringFromPeriod,// $('.ng-valid-max').find('input[name=RecurringFromPeriod]').val(),
            Recurring_To_Period: mstr_RecurringToPeriod,//$('.ng-valid-max').find('input[name=RecurringToPeriod]').val(),
            Frequency: RightSalesModel.Frequency,
            ContractId: $('#hid_Type').val() == "A" ? $("#hid_AuthorContract").val() : null,
            ProductLicenseId: $('#hid_Type').val() == "P" ? $("#hid_AuthorContract").val() : null,
            Status: null,
            Remarks: RightSalesModel.Remarks,
            EnteredBy: $('#enterdBy').val(),
            ProuductCode: $('#hid_ProductCodeValue').val(),
            ProuductId: $('#hid_ProductId').val(),
            Print_Run_Quantity_Type: $scope.RightSalesModel.PrintQuantityType,
            FirstPublicationDate: mstr_FirstPublicationDate,
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
        var ProductStatus = AJService.PostDataToAPI('RightsSelling/InsertRightsSellingMaster', _mobjRightsSelling);


        ProductStatus.then(function (msg) {

            if (msg.data.status == "Duplicate") {
                SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
            }
            else if (msg.data.status != "OK") {
                SweetAlert.swal("Try Again!", "Error in system. Please try again", "", "error");
            }
            else {
                if ($scope.AddendumId > 0) {
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
                else {
                    
                    SweetAlert.swal({
                        title: "Insert successfully.",
                        text:  "Rights Selling Code : " + msg.data.RightsSellingCode + "",
                        type: "success"
                    },
                   function () {                      
                       //location.href = "../../Home/Dashboard/Dashboard";

                       if ($('#hid_Type').val() == "A") {
                           location.href = GlobalredirectPath + "RightsSelling/RightsSelling/RightsSellingView?Id=" + $("#hid_AuthorContract").val() + "&type=A" + $('#hid_ProductId').val() + "&RightsSellingId=" + msg.data.id;
                       }
                       else if ($('#hid_Type').val() == "P") {
                           location.href = GlobalredirectPath + "RightsSelling/RightsSelling/RightsSellingView?Id=" + $("#hid_AuthorContract").val() + "&type=P" + $('#hid_ProductId').val() + "&RightsSellingId=" + msg.data.id;
                       }
                    });


                    //SweetAlert.swal('Insert successfully.', 'Rights Selling Code : ' + msg.data.RightsSellingCode + '', '', "success")

                    //location.href = "../../Home/Dashboard/Dashboard";
                    //setTimeout(function () {
                    //    if ($('#hid_Type').val() == "A") {
                    //                $window.location.href = "../../Contract/AuthorContract/AuthorContractSearch?For=Rights&Back=BackToserach";
                    //            }
                    //            else if ($('#hid_Type').val() == "P") {
                    //                $window.location.href = "../../Product/ProductLicense/ProductLicenseSearch?For=Rights&Back=BackToserach";
                    //            }

                    //}, 4000)
                }
            }

        },
        function () {
            alert('There is some error in the system');
        });


    }

});

    }

    $scope.fn_blank = function () {
        $scope.RightSalesModel.PrintRunQuantity = '';
    }

    $scope.RightSalesEntryForm = function (RightSalesModel) {
        $scope.submitted = true;

        $scope.checkdateOnSubmit($("input[name*=ContractDate]"));

        if ($scope.RightSalesModel.RoyaltyRecurring == 'Yes')
        {
            $scope.checkdateOnSubmit($("input[name*=RecurringFromPeriod]"));
            $scope.checkdateOnSubmit($("input[name*=RecurringToPeriod]"));
        }

        if ($("form[name*=userForm]").find(".has-error").length > 0) {
            $scope.userForm.$valid = false;
        }

        if ($scope.RightSalesModel.PaymentTerm == 'Royalty') {
            if ($scope.ValidateRyaltySlab() == 1) {
                $scope.userForm.$valid = false;
            }
        }

        if ($scope.userForm.$valid) {
            $scope.RightsSellingEntry(RightSalesModel);
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


    $scope.getLicenseeList = function () {
        var getLicenseeList = AJService.GetDataFromAPI("RightsSelling/getLicenseeList", null);
        getLicenseeList.then(function (Language) {
            $scope.LicenseeList = Language.data;
        }, function () {
           // alert('Error in getting Language List');
        });
    }

    //$scope.onchnagLicensee = function () {

    //    var LicenseeDetail = {
    //        Id: $scope.RightSalesModel.Licensee,
    //        EnteredBy: $("#enterdBy").val()
    //    };
       
    //    // call API to fetch temp product type list basis on the FlatId
    //    var LicenseeDetailStatus = AJService.PostDataToAPI('RightsSelling/LicenseeDetails', LicenseeDetail);
    //    LicenseeDetailStatus.then(function (msg) {
    //        if (msg != null) {


    //            $scope.RightSalesModel.Licensee = $scope.RightSalesModel.Licensee;
    //            $scope.RightSalesModel.ContactPerson = msg.data.ContactPerson;
    //            $scope.RightSalesModel.PublisherMobile = msg.data.Mobile;
    //            $scope.RightSalesModel.PublisherEmail = msg.data.Email;
    //            $scope.RightSalesModel.PublisherAddress = msg.data.Address;
    //            $scope.RightSalesModel.URL = msg.data.URL;

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
        var obj = $("[name$=Licensee]"); 

        var LicenseeList = [];

        var getLicenseeList = AJService.GetDataFromAPI("RightsSelling/getLicenseeList", null);
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
                    $scope.RightSalesModel.Licensee = ui.item.data;

                    //------------Fill licensee Details
                    var LicenseeDetail = {
                        Id: $scope.RightSalesModel.Licensee,
                        EnteredBy: $("#enterdBy").val()
                    };

                    //--- call API to fetch temp product type list basis on the FlatId
                    var LicenseeDetailStatus = AJService.PostDataToAPI('RightsSelling/LicenseeDetails', LicenseeDetail);
                    LicenseeDetailStatus.then(function (msg) {
                        if (msg != null) {


                            $scope.RightSalesModel.Licensee = $scope.RightSalesModel.Licensee;
                            $scope.RightSalesModel.ContactPerson = msg.data.ContactPerson;
                            $scope.RightSalesModel.PublisherMobile = msg.data.Mobile;
                            $scope.RightSalesModel.PublisherEmail = msg.data.Email;
                            $scope.RightSalesModel.PublisherAddress = msg.data.Address;
                            $scope.RightSalesModel.URL = msg.data.URL;

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
    //---End Autocomplete for Licensee

    $scope.func_RoyaltyType = function (value)
    {
        $scope.RoyaltyType = value;
    }

    $scope.func_Willbematerialbetranslated_yes = function ()
    {
        $scope.LanguageReq = true;
    }
    $scope.func_Willbematerialbetranslated_No = function () {
        $scope.LanguageReq = false;
    }


    //$scope.ValidateRoyaltySlabInsert = function () {
    //    obj = $(event.target);
    //    var _table = $(obj).closest(".RoyaltySlab");
    //    if ($(_table).find("select[name*=SubProductType]").find('option[value="' + $(obj).val() + '"]:selected').closest("select").length == 1) {
    //        $(obj).closest("tr").find("input[name*=CopiesFrom]").val(1);
    //        $(obj).closest("tr").find("input[name*=CopiesFrom]").attr("disabled", true);
    //    }
    //    else {

    //        var _copiesto = $($(_table).find("select[name*=SubProductType]").find('option[value="' + $(obj).val() + '"]:selected').parents("tr")[1]).find('input[name$=CopiesTo]').val()
    //        $(obj).closest("tr").find('input[name*=CopiesFrom]').val(parseInt(_copiesto) + 1);
    //        $(obj).closest("tr").find('input[name*=CopiesFrom]').attr("disabled", true);
    //    }

    //    if (obj.val() == "") {
    //        $(obj).closest("tr").find("input[name*=CopiesFrom]").val("");
    //        $(obj).closest("tr").find("input[name*=CopiesFrom]").removeAttr("disabled");
    //    }
    //};

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
           // alert('Error in getting Geographical list');
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



   
    $scope.getRightProductCategoryList = function () {
        var getRightProductCategoryListData = AJService.GetDataFromAPI("RightsSelling/getRightProductCategoryList", null);
        getRightProductCategoryListData.then(function (RightProductCategory) {
            $scope.ProductCategoryRightList = RightProductCategory.data;
        }, function () {
            //alert('Error in getting Product Category List');
        });
    }


});


