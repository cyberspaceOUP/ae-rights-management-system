
app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {
    $scope.Title = "Entry";
    /*Expand common.master.js Controller*/
    app.expandControllerA($scope, AJService, $window);
    app.expandControllerProductDetails($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);
    /*Expand Royalty Slab Controller*/
    app.expandControllerRoyaltySlab($scope, AJService, $window);

    //for Kit Details List
    app.expandControllerKitISBNLIst($scope, AJService);

    //var _href = location.href.toLocaleLowerCase();
    
    //if (_href.trim().indexOf("productlicense/productlicense?id") > 0) {
        
    //    $scope.ProductDetailsThirdPartyPermsion = true;
    //}else if(_href.trim().indexOf("productlicense/updateproductlicense") > 0)
    //{
      
    //    $scope.ProductDetailsThirdPartyPermsion = true;
    //}
    //else {
    //    $scope.ProductDetailsThirdPartyPermsion = false;
    //}

    ////----for third party permission
    //$scope.ProductIdValuesCheck = '';
    //if ($('[id*=hid_productid_PL]').val() != "" && $('[id*=hid_productid_PL]').val() != undefined && $('[id*=hid_productid_PL]').val() != 0) {
    //    $scope.ProductIdValuesCheck = parseInt($('[id*=hid_productid_PL]').val());
    //}

    //$scope.UpdateMode = false;
    //$scope.LicenseeCodeValuesCheck = '';
    ////----end for third party permission

    $scope.changeTerm = function (term) {
        if (term == 'Royalty') {
            $scope.ProductModel.PaymentAmount = '';
            $scope.ProductModel.PaymentAmountCurrency = '';
        }
    }

    $scope.getSubsidiaryList = function () {
        var subsidiaryList = AJService.GetDataFromAPI("AuthorContact/getSubsidiaryList", null);
        subsidiaryList.then(function (subsidiaryList) {
            $scope._subsidiaryList = subsidiaryList.data.query;
            setTimeout(function () {
                if ($scope.SubsidiaryRightsUpdate != null) {
                    var mobjSubsidiaryRights = $scope.SubsidiaryRightsUpdate;
                    $(document).ready(function () {
                        for (i = 0; i < mobjSubsidiaryRights.length; i++) {
                            var SubsidiaryRights = mobjSubsidiaryRights[i];
                            var index = SubsidiaryRights.subsidiaryrightsid;
                            $("#lblPublisherPercentage_" + index).val(SubsidiaryRights.publisherpercentage == "0" ? "" : SubsidiaryRights.publisherpercentage);
                            $("#hid_PublisherPercentage_" + index).val(SubsidiaryRights.publisherpercentage == "0" ? "" : SubsidiaryRights.publisherpercentage);
                            $("#lblOUPPercentage_" + index).val(SubsidiaryRights.ouppercentage == "0" ? "" : SubsidiaryRights.ouppercentage);
                            $("#OUPPercentage_" + index).val(SubsidiaryRights.ouppercentage == "0" ? "" : SubsidiaryRights.ouppercentage);
                            var pub = SubsidiaryRights.publisherpercentage == "0" ? "" : parseFloat(SubsidiaryRights.publisherpercentage);
                            var oup = SubsidiaryRights.ouppercentage == "0" ? "" : parseFloat(SubsidiaryRights.ouppercentage);
                            var total = pub + oup == "0" ? "" : pub + oup;
                            $("#OUPPercentage_" + index).parent().next().find('span').html(total);
                        }
                    });
                }
            }, 3000);
        }, function () {
            //alert('Error in getting subsidiary List');
        });
    }






    /*Expand Royalty Slab Controller*/
    $("#Country").attr("disabled", "disabled").removeAttr("required");
    $("#state").attr("disabled", "disabled");
    $("#city").attr("disabled", "disabled");
    $("#pincode").attr("disabled", "disabled");
    $("#geogdiv").find('span').attr('style', 'display:none');


    // Get Country List
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


    //$scope.onchnagPublisherCompany = function () {
      
    //    var PublishingCompanyData = {
    //        Id: $scope.ProductModel.PublisherCompany,
    //        EnteredBy: $("#enterdBy").val()
    //    };
    //    if ($scope.ProductModel.PublisherCompany == undefined) {
    //        $scope.ProductModel.ContactPerson = "";
    //        $scope.ProductModel.PublisherPhone = "";
    //        $scope.ProductModel.PublisherMobile = "";
    //        $scope.ProductModel.PublisherEmail = "";
    //        $scope.ProductModel.PublisherAddress = "";
    //        $scope.pincode = "";
    //        $scope.Country = "";
    //        $scope.State = "";
    //        $scope.City = "";

    //    }
    //    // call API to fetch temp product type list basis on the FlatId
    //    var pubCompanyStatus = AJService.PostDataToAPI('PublishingCompanyMaster/PublishingCompany', PublishingCompanyData);
    //    pubCompanyStatus.then(function (msg) {
    //        if (msg != null) {


    //            $scope.ProductModel.PublisherCompany = $scope.ProductModel.PublisherCompany;
    //            if ($('#hid_ForProductContractPerson').val() == "") {
    //                $scope.ProductModel.ContactPerson = msg.data.ContactPerson;
    //            }
    //            else {
    //                if ($scope.ProductLicenseId > 0)
    //                { }
    //                else
    //                {
    //                    $scope.ProductModel.ContactPerson = msg.data.ContactPerson;
    //                }
    //            }

               
    //            $scope.ProductModel.PublisherPhone = msg.data.Phone;
    //            $scope.ProductModel.PublisherMobile = msg.data.Mobile;
    //            $scope.ProductModel.PublisherEmail = msg.data.Email;
    //            $scope.ProductModel.PublisherAddress = msg.data.Address;

    //            $scope.pincode = msg.data.Pincode;
    //            $scope.Country = msg.data.CountryId;

    //            $scope.getCountryStates();
    //            $scope.State = msg.data.Stateid;

    //            $scope.getStateCities();
    //            $scope.City = msg.data.Cityid;

    //            setTimeout(function () {
    //                $scope.getStateCities();
    //                $scope.City = msg.data.Cityid;
    //            }, 3000);

    //            $('#hid_ForProductContractPerson').val('');
    //        }
    //        else {
    //            SweetAlert.swal("Error!", "Error in system. Please try again",  "error");
    //            blockUI.stop();
    //        }

    //    });

    //}

    $scope.bindPublisherCompanyDetails = function (id) {

        var PublishingCompanyData = {
            Id: $scope.ProductModel.PublisherCompany,
            EnteredBy: $("#enterdBy").val()
        };
        if ($scope.ProductModel.PublisherCompany == undefined) {
            $scope.ProductModel.ContactPerson = "";
            $scope.ProductModel.PublisherPhone = "";
            $scope.ProductModel.PublisherMobile = "";
            $scope.ProductModel.PublisherEmail = "";
            $scope.ProductModel.PublisherAddress = "";
            $scope.pincode = "";
            $scope.Country = "";
            $scope.State = "";
            $scope.City = "";

        }
        // call API to fetch temp product type list basis on the FlatId
        var pubCompanyStatus = AJService.PostDataToAPI('PublishingCompanyMaster/PublishingCompany', PublishingCompanyData);
        pubCompanyStatus.then(function (msg) {
            if (msg != null) {


                $scope.ProductModel.PublisherCompany = $scope.ProductModel.PublisherCompany;
                if ($('#hid_ForProductContractPerson').val() == "") {
                    $scope.ProductModel.ContactPerson = msg.data.ContactPerson;
                }
                else {
                    if ($scope.ProductLicenseId > 0)
                    { }
                    else
                    {
                        $scope.ProductModel.ContactPerson = msg.data.ContactPerson;
                    }
                }


                $scope.ProductModel.PublisherPhone = msg.data.Phone;
                $scope.ProductModel.PublisherMobile = msg.data.Mobile;
                $scope.ProductModel.PublisherEmail = msg.data.Email;
                $scope.ProductModel.PublisherAddress = msg.data.Address;

                $scope.pincode = msg.data.Pincode;
                $scope.Country = msg.data.CountryId;

                $scope.getCountryStates();
                $scope.State = msg.data.Stateid;

                $scope.getStateCities();
                $scope.City = msg.data.Cityid;

                setTimeout(function () {
                    $scope.getStateCities();
                    $scope.City = msg.data.Cityid;
                }, 3000);

                $('#hid_ForProductContractPerson').val('');
            }
            else {
                SweetAlert.swal("Error!", "Error in system. Please try again", "error");
                blockUI.stop();
            }

        });

    }

    //---Added on 05 Dec, 2017
    //---Autocomplete for Licensee
    setTimeout(function () {
        AutoCompletePublisherCompany();
    }, 200);
    function AutoCompletePublisherCompany() {
        var obj = $("[name$=PublisherCompany]");

        var PublishingCompanyList = [];

        var getList = AJService.GetDataFromAPI("PublishingCompanyMaster/GetAllPublishingCompany", null);
        getList.then(function (PublishingCompany) {
            //$scope.PublishingCompanyList = PublishingCompany.data;
            for (i = 0; i < PublishingCompany.data.length; i++) {
                PublishingCompanyList[i] = { "label": PublishingCompany.data[i].CompanyName, "value": PublishingCompany.data[i].CompanyName, "data": PublishingCompany.data[i].Id };
            }

            $(obj).autocomplete({
                source: function (request, response) {
                    var matcher = new RegExp(request.term, "i"); //RegExp("^" + request.term, "i"); //RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
                    response($.grep(PublishingCompanyList, function (item) {
                        return matcher.test(item.label);
                    }));
                },

                autoFocus: true,
                select: function (event, ui) {
                    $scope.PublisherCompany = ui.item.value;
                    $scope.ProductModel.PublisherCompany = ui.item.data;

                    //------------Fill licensee Details
                    var PublishingCompanyData = {
                        Id: $scope.ProductModel.PublisherCompany,
                        EnteredBy: $("#enterdBy").val()
                    };
                    if ($scope.ProductModel.PublisherCompany == undefined) {
                        $scope.ProductModel.ContactPerson = "";
                        $scope.ProductModel.PublisherPhone = "";
                        $scope.ProductModel.PublisherMobile = "";
                        $scope.ProductModel.PublisherEmail = "";
                        $scope.ProductModel.PublisherAddress = "";
                        $scope.pincode = "";
                        $scope.Country = "";
                        $scope.State = "";
                        $scope.City = "";

                    }
                    // call API to fetch temp product type list basis on the FlatId
                    var pubCompanyStatus = AJService.PostDataToAPI('PublishingCompanyMaster/PublishingCompany', PublishingCompanyData);
                    pubCompanyStatus.then(function (msg) {
                        if (msg != null) {


                            $scope.ProductModel.PublisherCompany = $scope.ProductModel.PublisherCompany;
                            if ($('#hid_ForProductContractPerson').val() == "") {
                                $scope.ProductModel.ContactPerson = msg.data.ContactPerson;
                            }
                            else {
                                if ($scope.ProductLicenseId > 0)
                                { }
                                else
                                {
                                    $scope.ProductModel.ContactPerson = msg.data.ContactPerson;
                                }
                            }


                            $scope.ProductModel.PublisherPhone = msg.data.Phone;
                            $scope.ProductModel.PublisherMobile = msg.data.Mobile;
                            $scope.ProductModel.PublisherEmail = msg.data.Email;
                            $scope.ProductModel.PublisherAddress = msg.data.Address;

                            $scope.pincode = msg.data.Pincode;
                            $scope.Country = msg.data.CountryId;

                            $scope.getCountryStates();
                            $scope.State = msg.data.Stateid;

                            $scope.getStateCities();
                            $scope.City = msg.data.Cityid;

                            setTimeout(function () {
                                $scope.getStateCities();
                                $scope.City = msg.data.Cityid;
                            }, 3000);

                            $('#hid_ForProductContractPerson').val('');
                        }
                        else {
                            SweetAlert.swal("Error!", "Error in system. Please try again", "error");
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



    //$scope.CalculateExpiry = function () {
    //    var PeriodIdValue = $scope.ProductModel.ContractPeriod;
    //    var requestDate = $("[name$=RequestDate]").val();

    //    if (PeriodIdValue == undefined || CurrentDate == "") {
    //        $scope.ProductModel.ExpiryDate = "";
    //        return false;
    //    }

    //    var CurrentDate = new Date(requestDate);
    //    CurrentDate.setMonth(CurrentDate.getMonth() + parseInt(PeriodIdValue));
    //    var today = CurrentDate;
    //    var dd = today.getDate();
    //    var mm = today.getMonth() + 1;

    //    var yyyy = today.getFullYear();
    //    if (dd < 10) {
    //        dd = '0' + dd
    //    }
    //    if (mm < 10) {
    //        mm = '0' + mm
    //    }
    //    var today = dd + '/' + mm + '/' + yyyy;
    //    $scope.ProductModel.ExpiryDate = today;
    //}

//    $scope.CalculateExpiry = function () {
//        PeriodIdValue = $scope.ProductModel.ContractPeriod;
//        //var CDate = $("[name$=RequestDate]").val();

//        var CDate = $("[name$=ContractDate]").val();

//    if (PeriodIdValue == undefined || PeriodIdValue == "0" || PeriodIdValue == "" || CDate == undefined || CDate == "" || CDate == "undefined//undefined") {
//        $scope.ExpiryDate = "";
//        return false;
//    }

//        //var CurrentDate = new Date(convertDate($("[name$=RequestDate]").val()));
//    var CurrentDate = new Date(convertDate($("[name$=ContractDate]").val()));
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
//    $scope.ProductModel.ExpiryDate = today;
//    $("[name$=ExpiryDate]").val(today);
//}


    $scope.SetRequestDate = function () {

        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1;

        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd
        }
        if (mm < 10) {
            mm = '0' + mm
        }
        //var today = mm + '/' + dd + '/' + yyyy;
        var today = dd + '/' + mm + '/' + yyyy;
        $scope.ProductModel.RequestDate = today;
    }

    $scope.SetContractDate = function (datetext) {
        $scope.ProductModel.ContractDate = $(datetext).val();
        $('#EffectiveDate').val($(datetext).val());
        //$scope.CalculateExpiry();
    }
    $scope.SetEffectiveDate = function (datetext) {
        if ($scope.ProductModel.EffectiveDate == undefined && $scope.ProductModel.EffectiveDate !== $(datetext).val()) {
            $scope.ProductModel.EffectiveDate = $(datetext).val();
        }
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

    $scope.SetFirstImpressionWithinDate = function (datetext) {
        if ($scope.ProductModel.FirstImpressionWithinDate == undefined && $scope.ProductModel.FirstImpressionWithinDate !== $(datetext).val()) {
            $scope.ProductModel.FirstImpressionWithinDate = $(datetext).val();
        }
    }

    /*[Set Value on Update Mode]*/
    $scope.ProductLicenseId = $("input:hidden[id$=hid_ProductLicenseId]").val();

    if ($scope.ProductLicenseId > 0) {
        var ProductLicense = {
            Id: $scope.ProductLicenseId,
        }

        var ProductLicenseDetails = AJService.PostDataToAPI("ProductLicense/getProductLicenseById", ProductLicense);
        ProductLicenseDetails.then(function (ProductLicense) {
            $scope.fillProductLicenseDetails(ProductLicense);
        }, function () {
            //alert('Error in Getting Product License Details');
        });

        $scope.Title = "Update";

    }

    $scope.chnageDateFormat = function (datestring) {
        var returndate = "";

        if (datestring != null) {
            if (datestring.indexOf("T") > 1) {
                var datestringformat = datestring.split("T")[0];
                var marr_date = datestringformat.split("-");
                returndate = marr_date[2] + "/" + marr_date[1] + "/" + marr_date[0]
            }
            else {
                returndate = datestring;
            }
        }

        return returndate;

    };

    $scope.fillProductLicenseDetails = function (ProductLicenseObj) {
        var ProductLicense = ProductLicenseObj.data.ProductLicenseM;
        var temp_advanceAmount = [];
        var temp_oneTimePaymentAmount = [];

        //angular.element(document.getElementById('angularid')).scope().ProductSerach(ProductLicense.productid, null, ProductLicense.ProductLicensecode);
        //for third party permission
        if (ProductLicense.ProductLicensecode != null || ProductLicense.ProductLicensecode != "") {
            $scope.UpdateMode = true;
            $scope.LicenseeCodeValuesCheck = ProductLicense.ProductLicensecode;
        }
        //end for third party permission
        angular.element(document.getElementById('angularid')).scope().ProductSerachLicense(ProductLicense.productid, null, ProductLicense.ProductLicensecode);

        setTimeout(function () {
            //fetch Kit Details List
            app.expandControllerKitISBNLIst($scope, AJService);
            angular.element(document.getElementById('angularid')).scope().KitISBNDetailsList(ProductLicense.productid);
        }, 300);
       
        if (ProductLicense.thirdpartypermission.toLocaleLowerCase() == "n")
        {
            ProductLicense.thirdpartypermission = "2"
        } else if (ProductLicense.thirdpartypermission.toLocaleLowerCase() == "y") {
            ProductLicense.thirdpartypermission = "1"
        } 
               

        $scope.ProductModel = {
            ProductId: ProductLicense.productid,
            PublisherCompany: ProductLicense.publishingcompanyid,
            ContactPerson: ProductLicense.ContactPerson,
                                               
            RequestDate: $scope.chnageDateFormat(ProductLicense.Requestdate),
            /*Commented & Added by Rajneesh Singh on 24/08/2016  Update By Ankush on 15/09/2016*/
            ContractDate: (ProductLicense.IProductLicenseUpdateDetails.length > 0 ? $scope.chnageDateFormat(ProductLicense.IProductLicenseUpdateDetails[0].AgreementDate) : ""),
            EffectiveDate: (ProductLicense.IProductLicenseUpdateDetails.length > 0 ? $scope.chnageDateFormat(ProductLicense.IProductLicenseUpdateDetails[0].Effectivedate) : ""),
            //ContractPeriod: (ProductLicense.IProductLicenseUpdateDetails.length > 0 ? ProductLicense.IProductLicenseUpdateDetails[0].Contractperiodinmonth != "" ? parseFloat(ProductLicense.IProductLicenseUpdateDetails[0].Contractperiodinmonth) : "" : ""),
            ExpiryDate: (ProductLicense.IProductLicenseUpdateDetails.length > 0 ? $scope.chnageDateFormat(ProductLicense.IProductLicenseUpdateDetails[0].Expirydate) : ""),
            LicensorCopiesSentDate: (ProductLicense.IProductLicenseUpdateDetails.length > 0 ? $scope.chnageDateFormat(ProductLicense.IProductLicenseUpdateDetails[0].LicensorCopiesSentDate) : ""),
            EFilesCost: (ProductLicense.IProductLicenseUpdateDetails.length > 0 ? ProductLicense.IProductLicenseUpdateDetails[0].EFilesCost : ""),
            EFilesRequestDate: (ProductLicense.IProductLicenseUpdateDetails.length > 0 ? $scope.chnageDateFormat(ProductLicense.IProductLicenseUpdateDetails[0].EFilesRequestDate) : ""),
            EFilesReceivedDate: (ProductLicense.IProductLicenseUpdateDetails.length > 0 ? $scope.chnageDateFormat(ProductLicense.IProductLicenseUpdateDetails[0].EFilesReceivedDate) : ""),
            Mode: (ProductLicense.IProductLicenseUpdateDetails.length > 0 ? ProductLicense.IProductLicenseUpdateDetails[0].Mode : ""),
            /*Ended by Rajneesh Singh Update By Ankush*/
            TerritoryRights: ProductLicense.Territoryrightsid,
            FirstImpressionWithinDate: $scope.chnageDateFormat(ProductLicense.Impressionwithindate),
            NoOfImpressions: (ProductLicense.noofimpressions == 0 ? "" : ProductLicense.noofimpressions),
            PrintQuantityType: ProductLicense.printquantitytype,
            PrintQuantity: (ProductLicense.printquantity == 0 ? "" : ProductLicense.printquantity),
            RoyaltyTerms: ProductLicense.RoyalityTerms,

            //PaymentAmount: (ProductLicense.PaymentAmount == 0 ? "" : ProductLicense.PaymentAmount),
            //AdvanceAmount: (ProductLicense.AdvancedAmount == 0 ? "" : ProductLicense.AdvancedAmount),          

            CopiesforLicensor: (ProductLicense.copiesforlicensor == 0 ? "" : ProductLicense.copiesforlicensor),
            PriceType: ProductLicense.pricetype,
            Currency: (ProductLicense.Currencyid == 0 ? "" : ProductLicense.Currencyid),
            Price: (ProductLicense.price == 0 ? "" : ProductLicense.price),
            
            //ThirdPartyPermission: ProductLicense.thirdpartypermission,
            Remarks: ProductLicense.Remarks,
        };

        //--bind Advance Amount & currency
        if (ProductLicense.AdvancedAmount != null) {
            temp_advanceAmount = ProductLicense.AdvancedAmount.trim().split(' ');
            $scope.ProductModel.AdvanceAmount = temp_advanceAmount[1];
            $scope.ProductModel.AdvanceAmountCurrency = temp_advanceAmount[0];

            setTimeout(function () {
                $('#AdvanceAmountCurrency').val(temp_advanceAmount[0]);
            }, 100);
        }
        else {
            $scope.ProductModel.AdvanceAmount = '';
            $scope.ProductModel.AdvanceAmountCurrency = '';
        }

        //--bind One-Time Payment Amount & currency
        if (ProductLicense.PaymentAmount != null) {
            temp_oneTimePaymentAmount = ProductLicense.PaymentAmount.trim().split(' ');
            $scope.ProductModel.PaymentAmount = temp_oneTimePaymentAmount[1];
            $scope.ProductModel.PaymentAmountCurrency = temp_oneTimePaymentAmount[0];

            setTimeout(function () {
                $('#PaymentAmountCurrency').val(temp_oneTimePaymentAmount[0]);
            }, 100);
        }
        else {
            $scope.ProductModel.PaymentAmount = '';
            $scope.ProductModel.PaymentAmountCurrency = '';
        }
                
        $scope.ThirdPartyPermission = ProductLicense.thirdpartypermission;
       
        //$scope.onchnagPublisherCompany();
        ////--bind  publishingcompanyName for autocomplete
        $scope.PublisherCompany = ProductLicenseObj.data.publishingcompanyName;
        $scope.bindPublisherCompanyDetails(ProductLicense.publishingcompanyid);
       
        $scope.setRoyaltySlab(ProductLicense.PProductLicenseRoyality);
        if (ProductLicense.PProductLicenseSubsidiaryRights.length > 0) {
            $scope.ProductModel.SubsidiaryRights = 'Yes';
            $scope.SubsidiaryRightsUpdate = ProductLicense.PProductLicenseSubsidiaryRights;
        }
        else {
            $scope.ProductModel.SubsidiaryRights = 'No';
        }

        //$scope.Docurl = [];

        if (ProductLicense.IProductLicenseUpdateDetails.length > 0) {
            $scope.documentshow = true;
            $scope.Docurl = ProductLicense.IProductLicenseUpdateDetails[0].ILicenseUpdateFileDetails;
            if ($scope.Docurl.length > 0) {
                $scope.documentshow = true;
            }
            else {
                $scope.documentshow = false;
            }
        }
    }

    $scope.setRoyaltySlab = function (mobjRoyaltySlab) {
        for (i = 1; i < mobjRoyaltySlab.length; i++) {
            var i = $scope.RoyaltyslabList.length + 1;
            $scope.RoyaltyslabList.push(i);
        }
        setTimeout(function () {
            for (i = 0; i < mobjRoyaltySlab.length; i++) {
                var RoyaltySlabDetails = mobjRoyaltySlab[i];
                $("#SubProductType_" + i).val(RoyaltySlabDetails.ProductSubTypeId);
                $("#CopiesFrom_" + i).val(RoyaltySlabDetails.copiesfrom);
                $("#CopiesTo_" + i).val((RoyaltySlabDetails.copiesto > 0 ? RoyaltySlabDetails.copiesto : ""));
                $("#RyPercentage_" + i).val(RoyaltySlabDetails.percentage);
            }
        }, 2000);
    }

    //function convertDate(date) {
    //    var datearray = date.split("/");
    //    return datearray[1] + '/' + datearray[0] + '/' + datearray[2];
    //}

    $scope.CalculateExpiryDate_Set = function (obj) {
        if (obj != "") {
            $scope.ProductModel.ExpiryDate = obj;
        }
    }

    $scope.fn_blank = function () {
        $scope.ProductModel.PrintQuantity = '';
    }

    //Call on Button Submit
    $scope.productLicenseEntryForm = function (ProductModel) {
        $scope.ValidateRyaltySlab();
        $scope.submitted = true;
        if ($scope.userForm.$valid) {
            var copiesto = 0;
            var printqty = ($("[name$=PrintQuantity]").val() == "" ? 0 : $("[name$=PrintQuantity]").val());
            var totallength = $("[id$=TblOwnerList]").find("select[name$=SubProductType]").length - 1;
            //added by dheeraj sharma
            if ($scope.ValidateRyaltySlab() == 1) {
                $scope.userForm.$valid = false;
            }
            //end by dheeraj
            
            var count = 0;
            if ($('#tblsubsidiary').is(':visible') == true && $scope.userForm.$valid == true) {
                $('#tblsubsidiary').find('tr:not(:has(th))').each(function () {
                    var _ttlpubPer = $(this).find('input[type = number][id *= lblPublisherPercentage]').val() == "" ? 0 : parseFloat($(this).find("input[type = number][id *= lblPublisherPercentage]").val());
                    var _ttlOupPer = $(this).find('input[type = number][id *= lblOUPPercentage]').val() == "" ? 0 : parseFloat($(this).find("input[type = number][id *= lblOUPPercentage]").val());

                    var _tr = $(this);

                    if (_ttlpubPer == "0") {
                        count++;
                        return true;
                    }

                    if (_ttlpubPer == '0') {
                        $scope.userForm.$valid = false;
                        SweetAlert.swal("Validation!", "Please Enter Publisher Percentage grater than 0 !", "info");
                        $(this).find('input[type = number][id *= lblPublisherPercentage]').focus();
                        $(_tr).addClass("has-error");
                        return false;
                    }
                    else if (_ttlpubPer > 99) {
                        $scope.userForm.$valid = false;
                        SweetAlert.swal("Validation!", "Please Enter Publisher Percentage less than 99 !", "info");
                        $(this).find('input[type = number][id *= lblPublisherPercentage]').focus();
                        $(_tr).addClass("has-error");
                        return false;
                    }
                    else
                        if (_ttlpubPer != 0 && parseFloat(_ttlpubPer + _ttlOupPer) != 100) {
                        SweetAlert.swal("Validation!", "Sum of Publisher and OUP percentage should be 100 ", "info");
                        //$(_tr).css("border-color", "#a94442");
                        $(_tr).addClass("has-error");
                        return false;
                    }
                    else {
                        $(_tr).removeClass("has-error");
                    }


                });
            }

            if ($scope.userForm.$valid == true && $('#tblsubsidiary').is(':visible') == true && count == $('#tblsubsidiary').find('tr:not(:has(th))').length) {
                SweetAlert.swal("Validation !", "Please enter atleaset one subsidiary rights", "info");
                //$(_tr).css("border-color", "#a94442");
                $($('#tblsubsidiary').find('tr:not(:has(th))')[0]).addClass("has-error");
                return false;
            }

            if ($('form[name$=userForm]').find(".has-error").length > 0) {

                $scope.userForm.$valid = false;
            }
            else {
                $scope.userForm.$valid = true;
            }


            if ($scope.userForm.$valid) {
                $scope.productLicenseEntry(ProductModel);
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    }

    function unique(list) {
        var result = [];
        $.each(list, function (i, e) {
            if ($.inArray(e, result) == -1) result.push(e);
        });
        return result;
    }

    $scope.ValidateRyaltySlab = function () {
        if (unique($("select[name*=SubProductType]").map(function () { return $(this).val() }).get())[0] == "" && $("input[type=radio][name*=RoyaltyTerms]:checked").val() == "Royalty") {
            SweetAlert.swal("Validation !", "Please enter atleaset one royalty slab !", "info");
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
                        SweetAlert.swal("Validation !", "Last Copies to should be blank !", "info");
                        $(_lastTr).find('input[name*=CopiesTo]').focus();
                        $scope.submitted = false;
                        returnstatus = true;
                        return false;
                    }
                }
                if ($(this).find('input[name=RyPercentage]').val() == "" && $(this).find("select[name*=SubProductType]").val() != "") {
                    $scope.userForm.$valid = false;
                    SweetAlert.swal("Validation !", "Please Enter Copies percentage !", "info");
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


        //$("[id$=TblOwnerList]").find("select[name$=SubProductType]").each(function (i) {
        //    var SubProductType = $(this);
        //    var returnstatus = false;
        //    if ($(SubProductType).val() > 0) {
        //        var CopiesFrom = $(SubProductType).closest("tr").find("[name$=CopiesFrom]");
        //        var CopiesTo = $(SubProductType).closest("tr").find("[name$=CopiesTo]");
        //        var RyPercentage = $(SubProductType).closest("tr").find("[name$=RyPercentage]");

        //        if ($(CopiesFrom).val() == '') {
        //            $scope.userForm.$valid = false;
        //            SweetAlert.swal("Validation!", "Please Enter Copies from !", "error");
        //            $(CopiesFrom).focus();
        //            $scope.submitted = false;
        //            returnstatus = true;
        //        }
        //        else if (!($(CopiesFrom).val() > 0)) {
        //            $scope.userForm.$valid = false;
        //            SweetAlert.swal("Validation!", "Please Enter valid Copies from !", "error");
        //            $(CopiesFrom).focus();
        //            $scope.submitted = false;
        //            returnstatus = true;
        //        }
        //        else if (parseInt($(CopiesFrom).val()) < copiesto) {
        //            $scope.userForm.$valid = false;
        //            SweetAlert.swal("Validation!", "Please Enter Copies from grater than previous Copies to !", "error");
        //            $(CopiesFrom).focus();
        //            $scope.submitted = false;
        //            returnstatus = true;
        //        }
        //        else if ($(CopiesTo).val() != '' && totallength == i) {
        //            $scope.userForm.$valid = false;
        //            SweetAlert.swal("Validation!", "Last Copies to should be blank !", "", "error");
        //            $(CopiesTo).focus();
        //            $scope.submitted = false;
        //            returnstatus = true;
        //        }
        //        else if ($(CopiesTo).val() == '' && totallength != i) {
        //            $scope.userForm.$valid = false;
        //            SweetAlert.swal("Validation!", "Please Enter Copies to !", "", "error");
        //            $(CopiesTo).focus();
        //            $scope.submitted = false;
        //            returnstatus = true;
        //        }
        //        else if (!($(CopiesTo).val() > 0) && totallength != i) {
        //            $scope.userForm.$valid = false;
        //            SweetAlert.swal("Validation!", "Please Enter valid Copies to !", "", "error");
        //            $(CopiesTo).focus();
        //            $scope.submitted = false;
        //            returnstatus = true;
        //        }
        //        else if ($(CopiesFrom).val() > $(CopiesTo).val() && totallength != i) {
        //            $scope.userForm.$valid = false;
        //            SweetAlert.swal("Validation!", "Please Enter Copies to value grater than Copies from!", "", "error");
        //            $(CopiesTo).focus();
        //            $scope.submitted = false;
        //            returnstatus = true;
        //        }
        //        else if ($(CopiesTo).val() > printqty && printqty > 0) {
        //            $scope.userForm.$valid = false;
        //            SweetAlert.swal("Validation!", "Please Enter Copies to value less than Print Quantity!", "", "error");
        //            $(CopiesTo).focus();
        //            $scope.submitted = false;
        //            returnstatus = true;
        //        }
        //        else if ($(RyPercentage).val() == '') {
        //            $scope.userForm.$valid = false;
        //            SweetAlert.swal("Validation!", "Please Enter Copies percentage !", "", "error");
        //            $(RyPercentage).focus();
        //            $scope.submitted = false;
        //            returnstatus = true;
        //        }
        //        else if (!($(RyPercentage).val() > 0)) {
        //            $scope.userForm.$valid = false;
        //            SweetAlert.swal("Validation!", "Please Enter valid Copies percentage !", "", "error");
        //            $(RyPercentage).focus();
        //            $scope.submitted = false;
        //            returnstatus = true;
        //        }
        //        copiesto = $(CopiesTo).val();
        //    }
        //    if (returnstatus) {
        //        return 1;
        //    }
        //});

    }

    $scope.PublisherPercentageArray = [];
    
    //added by Ankush
    $scope.calculatePublisherValue = function (Id) {

        //debugger;
        //var _crrobj = $(event.target);
        var _crrobj = $('#' + Id.id);

        //$scope.PublisherPercentageArray[parseInt($(_crrobj).attr("indexNo"))] = (100.00 - parseFloat($(_crrobj).val()));

        var oup = $(_crrobj).closest("tr").find("input[type=number][name*=lblOUPPercentage]").val() == "" ? 0 : parseFloat($(_crrobj).closest("tr").find("input[type=number][name*=lblOUPPercentage]").val());
        var Pub = $(_crrobj).closest("tr").find("input[type=number][name*=lblPublisherPercentage]").val() == "" ? 0 : parseFloat($(_crrobj).closest("tr").find("input[type=number][name*=lblPublisherPercentage]").val());
        //$scope.ToatlPercentage = oup + Pub;
        $(_crrobj).closest("tr").find("span[id*=spnPercentage]").html(oup + Pub);

    }

    //Start by dheeraj
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
            //$(_table).find("select[name*=SubProductType]").find('option[value="' + $(obj).val() + '"]:selected').closest("select").closest("tr").each(function (index, value)
            //{


            //    $(this)

            //    if (index >= 1 && $(_table).find("select[name*=SubProductType]").find('option[value="' + $(obj).val() + '"]:selected').closest("select").closest("tr").length > 1) {
            //        var _copiesFrom = $(this).prev().find("input[name*=CopiesFrom]").val();
            //        var _CopiesTo = $(this).prev().find("input[name*=CopiesTo]").val();

            //        if (_CopiesTo != "") {
            //            $(this).find("input[name*=CopiesFrom]").val(parseInt(_CopiesTo) + 1);
            //            $(this).find("input[name*=CopiesFrom]").attr("disabled", true);
            //        }
            //        else {
            //            $(this).find("input[name*=CopiesFrom]").val("");
            //            $(this).find("input[name*=CopiesFrom]").removeAttr("disabled", true);
            //        }

            //    }
            //    else {
            //        $(obj).closest("tr").find("input[name*=CopiesFrom]").val("");
            //        $(obj).closest("tr").find("input[name*=CopiesFrom]").removeAttr("disabled");
            //    }



            //});


        }

        if (obj.val() == "") {
            $(obj).closest("tr").find("input[name*=CopiesFrom]").val("");
            $(obj).closest("tr").find("input[name*=CopiesFrom]").removeAttr("disabled");
        }
    };
    //end by dheeraj

    $scope.setCurrencyId = function (Currency, CurrencyType) {
        if (Currency == undefined) {
            $scope.Currency = {
                Currency: CurrencyType.Id

            }
        }
    }

    function convertDate(date) {
        if (date != undefined) {
            var datearray = date.split("/");
            return datearray[1] + '/' + datearray[0] + '/' + datearray[2];
        }
        else {
            return null;
        }

    }

    $scope.productLicenseEntry = function () {


        var marr_royaltyslab = $scope.getRoyaltySlab();
        var marr_subsidiaryRights = [];
        var marr_UpdateDetails = [];

        if ($('input[type=radio][name=SubsidiaryRights]:checked').val() == 'Yes') {
            $("input:hidden[name$=SubsidiaryRightsId]").each(function () {
                var mint_subsidiaryrightsid = $(this).val();
                var mint_PublisherPercentage = $(this).closest('tr').find("input:hidden[name$=PublisherPercentage]").val();
                var mint_OUPPercentage = $(this).closest('tr').find("input:hidden[name$=OUPPercentage]").val();
                var totalPercentage = $(this).closest('tr').find("span[id*=spnPercentage]").html();
                if (totalPercentage == "") {
                    return true;
                }
                var mobj_subsidaryrights = {
                    publisherpercentage: mint_PublisherPercentage,
                    ouppercentage: mint_OUPPercentage,
                    subsidiaryrightsid: mint_subsidiaryrightsid,
                }
                marr_subsidiaryRights.push(mobj_subsidaryrights);
            });
        }


        if ($scope.ProductLicenseId > 0) {

            var marr_fileDetails = [];
            var FileNameArray = $('#dropZone0').find('.fileNameClass');
            var UploadFileNameArray = $("#hid_Uploads").val().split(",");

            FileNameArray.each(function (i) {
                var mobj_fileDetails = {
                    LicenseId: $scope.ProductLicenseId,
                    FileName: $(this).val(),
                    UploadFileName: UploadFileNameArray[i],
                }
                marr_fileDetails.push(mobj_fileDetails);
            });




            var mobj_UpdateDetails = {
                LicensorCopiesSentDate: $("input[type=text][name*=LicensorCopiesSentDate]").val() == "" ? null : convertDate($("input[type=text][name*=LicensorCopiesSentDate]").val()),
                EFilesCost: $scope.ProductModel.EFilesCost,
                EFilesRequestDate: $("input[type=text][name*=EFilesRequestDate]").val() == "" ? null : convertDate($("input[type=text][name*=EFilesRequestDate]").val()),
                EFilesReceivedDate: $("input[type=text][name*=EFilesReceivedDate]").val() == "" ? null : convertDate($("input[type=text][name*=EFilesReceivedDate]").val()),
                Mode: $scope.ProductModel.Mode,
                AgreementDate: $("[name*=ContractDate]").val() == "" ? null : convertDate($("[name*=ContractDate]").val()),
                effectivedate: $('#EffectiveDate').val() == "" ? null : convertDate($('#EffectiveDate').val()),
                //contractperiodinmonth: 0, //$scope.ProductModel.ContractPeriod, //change by prakash on 30 May, 2017
                Expirydate: $('#ExpiryDate').val() == "" ? null : convertDate($('#ExpiryDate').val()),
                ILicenseUpdateFileDetails: marr_fileDetails,
            }
            marr_UpdateDetails.push(mobj_UpdateDetails);


        }


        ////--- Third party permission add in license table
        //var _ThirdPartyPermission = $('[name*=ThirdPartyPermission_]');
        //$scope.ThirdPartyPermission_value = "";
        //if (_ThirdPartyPermission.length > 0) {

        //    for (var i = 0; i < _ThirdPartyPermission.length; i++) {
        //        if ($('[name*=ThirdPartyPermission_' + [i] + ']').is(':checked') == true) {
        //            if ($('[name*=ThirdPartyPermission_' + [i] + ']').val() != undefined && $('[name*=ThirdPartyPermission_' + [i] + ']').val() != "") {

        //                if ($('[name*=ThirdPartyPermission_' + [i] + ']:checked').val() == "1")
        //                {
        //                    $('[name*=ThirdPartyPermission_' + [i] + ']:checked').val("Y")
        //                } else if ($('[name*=ThirdPartyPermission_' + [i] + ']:checked').val() == "2") {
        //                    $('[name*=ThirdPartyPermission_' + [i] + ']:checked').val("N")
        //                }

        //                $scope.ThirdPartyPermission_value += $('[name*=ThirdPartyPermission_' + [i] + ']:checked').val() + ",";
        //            }


        //        }

        //    }
        //}

        //--validate Advance Amount & Currency
        var obj_AdvanceAmount_Currency = null;
        if (($scope.ProductModel.AdvanceAmount != null && $scope.ProductModel.AdvanceAmount != "") 
            && ($scope.ProductModel.AdvanceAmountCurrency == "" || $scope.ProductModel.AdvanceAmountCurrency == undefined)) {
            SweetAlert.swal("", 'Please select Advance Amount Currency.', "info");
            return;
        }
        else if (($scope.ProductModel.AdvanceAmount == null || $scope.ProductModel.AdvanceAmount == "") 
                && ($scope.ProductModel.AdvanceAmountCurrency != "" && $scope.ProductModel.AdvanceAmountCurrency != undefined)) {
            SweetAlert.swal("", 'Please enter Advance Amount.', "info");
            return;
        }
        else if (($scope.ProductModel.AdvanceAmount != null && $scope.ProductModel.AdvanceAmount != "") 
                && ($scope.ProductModel.AdvanceAmountCurrency != "" && $scope.ProductModel.AdvanceAmountCurrency != undefined)) {
            obj_AdvanceAmount_Currency = $scope.ProductModel.AdvanceAmountCurrency + " " + $scope.ProductModel.AdvanceAmount;
        }
        else {
            obj_AdvanceAmount_Currency = null;
        }

        //--validate One-Time Payment Amount & Currency
        var obj_OneTimePaymentAmount_Currency = null;
        if (($scope.ProductModel.PaymentAmount != null && $scope.ProductModel.PaymentAmount != "")
            && ($scope.ProductModel.PaymentAmountCurrency == "" || $scope.ProductModel.PaymentAmountCurrency == undefined)) {
            SweetAlert.swal("", 'Please select Advance Amount Currency.', "info");
            return;
        }
        else if (($scope.ProductModel.PaymentAmount == null || $scope.ProductModel.PaymentAmount == "")
                && ($scope.ProductModel.PaymentAmountCurrency != "" && $scope.ProductModel.PaymentAmountCurrency != undefined)) {
            SweetAlert.swal("", 'Please enter Advance Amount.', "info");
            return;
        }
        else if (($scope.ProductModel.PaymentAmount != null && $scope.ProductModel.PaymentAmount != "")
                && ($scope.ProductModel.PaymentAmountCurrency != "" && $scope.ProductModel.PaymentAmountCurrency != undefined)) {
            obj_OneTimePaymentAmount_Currency = $scope.ProductModel.PaymentAmountCurrency + " " + $scope.ProductModel.PaymentAmount;
        }
        else {
            obj_OneTimePaymentAmount_Currency = null;
        }

        var _mobjProductLicense = {
            Id: $scope.ProductLicenseId,
            productid: $scope.ProductModel.ProductId,
            publishingcompanyid: $scope.ProductModel.PublisherCompany,
            ContactPerson: $scope.ProductModel.ContactPerson,
            Address: $scope.ProductModel.PublisherAddress,
            CountryId: $scope.Country,
            OtherCountry: $scope.CountryName,
            Stateid: $scope.State,
            OtherState: $scope.stateName,
            Cityid: $scope.City,
            OtherCity: $scope.cityName,
            Pincode: $scope.pincode,
            Mobile: $scope.ProductModel.PublisherMobile,
            Email: $scope.ProductModel.PublisherEmail,
            Requestdate: $scope.ProductModel.RequestDate == "" ? null : convertDate($scope.ProductModel.RequestDate),
            //ContractDate: convertDate($("[name*=ContractDate]").val()),
            //effectivedate: $scope.ProductModel.EffectiveDate,
            //contractperiodinmonth: $scope.ProductModel.ContractPeriod,
            Expirydate: $scope.ProductModel.ExpiryDate == "" ? null : convertDate($scope.ProductModel.ExpiryDate),
            Territoryrightsid: $scope.ProductModel.TerritoryRights,
            Impressionwithindate: $scope.ProductModel.FirstImpressionWithinDate,
            noofimpressions: $scope.ProductModel.NoOfImpressions,
            printquantitytype: $scope.ProductModel.PrintQuantityType,
            printquantity: $scope.ProductModel.PrintQuantity,
            RoyalityTerms: $scope.ProductModel.RoyaltyTerms,
            PaymentAmount: obj_OneTimePaymentAmount_Currency, // $scope.ProductModel.PaymentAmount,
            AdvancedAmount: obj_AdvanceAmount_Currency, // $scope.ProductModel.AdvanceAmount,
            copiesforlicensor: $scope.ProductModel.CopiesforLicensor,
            pricetype: $scope.ProductModel.PriceType,
            //Currencyid: $scope.ProductModel.Currency,
            Currencyid: $('select[name=Currency]').val(),
            //price: $scope.ProductModel.Price,
            price: $('input[name="Price"]').val(),
            //thirdpartypermission: $scope.ProductModel.ThirdPartyPermission,
            thirdpartypermission: 0, // $scope.ThirdPartyPermission_value.slice(0, -1),
            Remarks: $scope.ProductModel.Remarks,
            PProductLicenseRoyality: marr_royaltyslab,
            PProductLicenseSubsidiaryRights: marr_subsidiaryRights,
            IProductLicenseUpdateDetails: marr_UpdateDetails,
            EnteredBy: $("#enterdBy").val(),

        };

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

        if ($scope.ProductLicenseId > 0) {
            var ProductStatus = AJService.PostDataToAPI('ProductLicense/UpdateProductLicense', _mobjProductLicense);

        }
        else {
            var ProductStatus = AJService.PostDataToAPI('ProductLicense/InsertProductLicense', _mobjProductLicense);
        }


        ProductStatus.then(function (msg) {

            if (msg.data.status == "Duplicate") {
                SweetAlert.swal("Duplicate !", "Already exist !", "warning");
            }
            else if (msg.data.status != "OK") {
                SweetAlert.swal('Error !', 'There is some problem. !', "error");
            }
            else {
                if ($scope.ProductLicenseId > 0) {
                    $("#hid_ProductId").val($scope.productId);
                    SweetAlert.swal({
                        title: "Success",
                        text: "Updated successfully.",
                        type: "success"
                    },
                   function () {
                       location.href = GlobalredirectPath + "Product/ProductLicense/view?Id=" + _mobjProductLicense.Id;
                   });
                }
                else {
                    $("#hid_ProductId").val(msg.data.Id);
                    SweetAlert.swal({
                        title: "Success",
                        text: "Product license successfully inserted. Code is " + msg.data.ProductLicensecode + ".",
                        type: "success"
                    },
                   function () {
                       location.href = GlobalredirectPath + "Product/ProductLicense/view?Id=" + msg.data.Id;
                   });
                }
            }

        },
        function () {
            //--alert('There is some error in the system');
        });


    }

});
    }

    $scope.RemoveDocumentById = function (mobjurl) {
        var docid = mobjurl.Id;
        var file = mobjurl.UploadFileName;
        //  alert($scope.NoticeBoard.NBId);
        var FileDetails = { Id: docid };
        var DeleteDocument = AJService.PostDataToAPI("ProductLicense/DeleteFile", FileDetails);

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
                            //var NoticeBoard = {
                            //    Id: $scope.NoticeBoard.NBId,
                            //    Published: false

                            //};
                            var index = $scope.Docurl.indexOf(mobjurl);
                            $scope.Docurl.splice(index, 1);
                            if ($scope.Docurl.length == 0) {
                                $scope.documentshow = false;
                            }

                        }
                        //   angular.element('#profileContainer').scope().$apply();
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                    }
                });


            }
        }, function () {

            SweetAlert.swal("Oops...", "Please retry!", "error");

        });
    }

    function convertDateMDY(date) {
        if (date != undefined) {
            var datearray = date.split("/");
            return datearray[1] + '/' + datearray[0] + '/' + datearray[2];
        }
        else {
            return null;
        }

    }

    if ($('#hid_price').val() != "") {

        setTimeout(function () {

            if ($('#hid_currencyid').val() == "" || $('#hid_currencyid').val() == undefined) { }
            else {
               
                //$('select[name=Currency]').attr('value', $('#hid_currencyid').val()).attr('selected', 'selected');
                $('select[name=Currency]').val($('#hid_currencyid').val());
            }

            if ($('#hid_price').val() == "" || $('#hid_price').val() == undefined) {
            }
            else
            {
                $('input[name="Price"]').val($('#hid_price').val());
                $('input[name="Price"]').attr("disabled", true);
                $("#Currency ").attr("disabled", true);
            }

        }, 2000)
    }


});


