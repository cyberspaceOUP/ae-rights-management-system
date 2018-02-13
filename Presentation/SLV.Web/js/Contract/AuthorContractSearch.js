

app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {

    app.expandControllerTopSearch($scope, AJService, $window);
    app.expandControllerA($scope, AJService, $window);
    app.expandControllerProductLicense($scope, AJService, $window);
    app.expandControllerDivisionSubDivision($scope, AJService, $window);
    $scope.DivisionMandatory = false;
    $scope.SubDivisionMandatory = false;
    $scope.ShowSearchForm = true;
    $scope.ShowListForm = false;
    $scope.ShowProductSeriesListForm = false;

    $scope.TitleLinking = false;
    $scope.AuthorList = fetchAuthor();

    $("#DateofAgreementToDate").attr("disabled", true);
    $("#RequestToDate").attr("disabled", true);

    $('#hid_Series').val('');

    angular.element(document.getElementById('angularid')).scope().getCurrencyList();
    angular.element(document.getElementById('angularid')).scope().getImprintList();
    angular.element(document.getElementById('angularid')).scope().getLanguageList();
    angular.element(document.getElementById('angularid')).scope().GetAuthorList();
    angular.element(document.getElementById('angularid')).scope().GetExecutiveList();

    //var for_childforContract = '';
    //if ($('#hid_contractsearch').val() != null && $('#hid_contractsearch').val() != '' && $('#hid_contractsearch').val() != undefined) {
    //    for_childforContract = $('#hid_contractsearch').val();
    //}
    //var for_childforInbound = '';
    //if ($('#hid_PermissionsInboundAuthor').val() != null && $('#hid_PermissionsInboundAuthor').val() != '' && $('#hid_PermissionsInboundAuthor').val() != undefined) {
    //    for_childforInbound = $('#hid_PermissionsInboundAuthor').val();
    //}
    $scope.ProductLicenseListResult = function () {
        var ProductLicenseList = AJService.GetDataFromAPI("AuthorContact/GetAuthorContractListing?SessionId=" + $("#hid_sessionId").val() + "", null);
        ProductLicenseList.then(function (msg) {
            blockUI.stop();
            if (msg.data.length != 0) {
                $scope.ProductLicenseList = msg.data;
                $scope.ShowSearchForm = false;
                $scope.ShowListForm = true;

            }
            else {
                //swal("No record", 'No record found', "warning");
                //document.location = GlobalredirectPath + "/Contract/AuthorContract/AuthorContractSearch";

                SweetAlert.swal({
                    title: "No record",
                    text: "No record found",
                    type: "warning"
                },
                function () {
                    document.location = GlobalredirectPath + "/Contract/AuthorContract/AuthorContractSearch";
                });
            }
        });
    }
        
    if ($("#hid_from").val() != "") {
        $scope.ProductLicenseListResult();
    }

    if ($("#hid_BackToserach").val() != "")
    {    
        $scope.ProductLicenseListResult();
    }

    $scope.SetRequestFromDate = function (datetext) {
        $scope.ProductModel.RequestFromDate = $(datetext).val();
    }

    $scope.SetExpiryFromDate = function (datetext) {
        $scope.ProductModel.ExpiryFromDate = $(datetext).val();
    }

    $scope.SetExpiryToDate = function (datetext) {
        $scope.ProductModel.ExpiryToDate = $(datetext).val();
    }


    $scope.getContractType = function () {
        var ContactList = AJService.GetDataFromAPI("AuthorContact/getAuthorContract", null);
        ContactList.then(function (ContactList) {
            $scope.ContactType = ContactList.data.query;
        }, function () {
            //alert('Error in getting Contract List');
        });
    }

    $scope.getTerritoryRightsList = function () {
        var TerritoryRightsList = AJService.GetDataFromAPI("AuthorContact/getTerriteryRights", null);
        TerritoryRightsList.then(function (TerritoryRightsList) {
            $scope.TerritoryList = TerritoryRightsList.data.query;
        }, function () {
            //alert('Error in getting Contract List');
        });
    }

    //function convertDate(date) {
    //    var datearray = date.split("/");
    //    return datearray[1] + '/' + datearray[0] + '/' + datearray[2];
    //}


    function convertDate(dateVal) {

        if (dateVal == "") {
            dateVal = null
        }
        else {

            var RequestDate = dateVal;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            return yy + "/" + mm + "/" + dd;
        }
    }

    $scope.submitForm = function (AuthorContract) {

        $scope.submitted = true;

        $scope.AuthorContractSearch(AuthorContract);
        // set form default state
        $scope.userForm.$setPristine();
        // set form is no submitted
        $scope.submitted = false;
        return;
    };

    $scope.GetValue = function (obj) {
        $scope.SupplyMaterialByAuthor = obj;
    }
    $scope.getSupplyMaterialList = function () {
        var _subsidiaryList = AJService.GetDataFromAPI("AuthorContact/getSupplyMaterialList", null);
        _subsidiaryList.then(function (_subsidiaryList) {
            $scope.SupplyMaterialList = _subsidiaryList.data.query;
        }, function () {
            //alert('Error in getting SupplyMaterial List');
        });
    }

    $scope.getMenuScriptDeliveryFormat = function () {
        var DeliveryFormat = AJService.GetDataFromAPI("AuthorContact/getMenuScriptDeliveryFormat", null);
        DeliveryFormat.then(function (DeliveryFormat) {
            $scope._DeliveryFormat = DeliveryFormat.data.query;

        }, function () {
            //alert('Error in getting Delivery Format List');
        });
    }


    $scope.handledBy = function (UserId, DepartmentId) {
        var Executive = {
            Id: UserId,
            DepartmentId: DepartmentId
        };
        var ExecutiveList = AJService.PostDataToAPI("AuthorContact/getHandledByContract", Executive);
        ExecutiveList.then(function (ExecutiveList) {
            $scope.HandledBy = ExecutiveList.data.query;
            if (ExecutiveList.data.code != "RT" && ExecutiveList.data.code != "AD" && ExecutiveList.data.code != "SA") {
                $('[name$=HandledByExecutive]').prop("disabled", true);
                $scope.ByExecutive = ExecutiveList.data.query[0].Id;
            }
            else {
                $scope.DeptCode = "RT";
            }
            $scope.DeptCode = ExecutiveList.data.code;
        }, function () {
            //alert('Error in getting Handled By List');
        });
    }


    $scope.getCurrencyList = function () {
        var _CurrencyList = AJService.GetDataFromAPI("AuthorContact/getCurrencyList", null);
        _CurrencyList.then(function (_CurrencyList) {
            $scope.CurrencyList = _CurrencyList.data.query;

        }, function () {
            //alert('Error in getting Currency List');
        });
    }

    var mstr_SeriesCode = '';
    var mstr_AC_Code = '';
    var temp_SeriesCode = [];
    var temp_AutherData = [];
    $scope.GetProductSeriesDetails = function (Id, For) {        
        localStorage["SeriesId"] = Id+"#"+For;
        var GetProductSeriesList = AJService.GetDataFromAPI("AuthorContact/GetProductSeriesContract?SeriesId=" + Id+'&For='+For);
        //var GetProductSeriesList = AJService.GetDataFromAPI("ProductMaster/GetProductSeriesContract?SeriesId=" + Id);
        GetProductSeriesList.then(function (ProductSeries) {
            $scope.ProductSeriesList = ProductSeries.data;
            $scope.ShowSearchForm = false;
            $scope.ShowListForm = false;
            $scope.ShowProductSeriesListForm = true;

            var Flag = 0;
            
            for (var i = 0; i < ProductSeries.data.length; i++) {

                if (mstr_SeriesCode != ProductSeries.data[i].SeriesCode) {

                    mstr_SeriesCode = ProductSeries.data[i].SeriesCode;

                    if (temp_SeriesCode.length > 0) {
                        Flag = 0;
                        for (var m = 0; m < temp_SeriesCode.length; m++)  {
                            if (mstr_SeriesCode == temp_SeriesCode[m].SeriesCode) {
                                Flag++;
                            }
                        }
                    }

                    if (Flag == 0) {
                        for (var j = 0; j < ProductSeries.data.length; j++) {
                            if (mstr_SeriesCode == ProductSeries.data[j].SeriesCode) {
                                temp_AutherData.push({
                                    'AuthorContractId': ProductSeries.data[j].AuthorContractId,
                                    'SeriesCode': ProductSeries.data[j].SeriesCode,
                                    'AuthorContractCode': ProductSeries.data[j].AuthorContractCode,
                                    'ContractEntryDate': ProductSeries.data[j].ContractEntryDate,
                                    'ContractExpiryDate': ProductSeries.data[j].ContractExpiryDate,
                                    'ProductName': ProductSeries.data[j].ProductName,
                                    'ProductCode': ProductSeries.data[j].ProductCode,
                                    'OUPISBN': ProductSeries.data[j].OUPISBN,
                                    //'Flag': ProductSeries.data[j].Flag
                                });
                            }
                        }

                        temp_SeriesCode.push({
                            'SeriesCode': ProductSeries.data[i].SeriesCode,
                            'Flag': ProductSeries.data[i].Flag,
                            'AuthorData': temp_AutherData
                        });
                    }

                }
            }

            $scope.series_code = temp_SeriesCode;


            //for (var i = 0; i < ProductSeries.data.length; i++) {
                                
            //    if (mstr_SeriesCode != ProductSeries.data[i].SeriesCode) {
                    
            //        mstr_SeriesCode = ProductSeries.data[i].SeriesCode;

            //        for (var j = 0; j < ProductSeries.data.length; j++) {
            //            if (mstr_AC_Code != ProductSeries.data[j].AuthorContractCode && mstr_SeriesCode == ProductSeries.data[j].SeriesCode) {
            //                temp_AutherData.push({
            //                    'AuthorContractId': ProductSeries.data[j].AuthorContractId,
            //                    'SeriesCode': ProductSeries.data[j].SeriesCode,
            //                    'AuthorContractCode': ProductSeries.data[j].AuthorContractCode,
            //                    'ContractEntryDate': ProductSeries.data[j].ContractEntryDate,
            //                    'ContractExpiryDate': ProductSeries.data[j].ContractExpiryDate,
            //                    'ProductName': ProductSeries.data[j].ProductName,
            //                    'ProductCode': ProductSeries.data[j].ProductCode,
            //                    'OUPISBN': ProductSeries.data[j].OUPISBN,
            //                    'Flag': ProductSeries.data[j].Flag
            //                });
            //            }
            //            mstr_AC_Code = ProductSeries.data[j].AuthorContractCode;
            //        }

            //        temp_SeriesCode.push({
            //            'SeriesCode': ProductSeries.data[i].SeriesCode,
            //            'AuthorData': temp_AutherData
            //        });

            //    }                
            //}

            //$scope.series_code = temp_SeriesCode;

        }, function () {
            //alert("Error in getting Product Series List");
        });
    }

    //-------Pending Series Request on 19 May, 2017
    if ($("#hid_show").val().toLowerCase() == "dashboard" && ($("#hid_PendingSeries").val().toLowerCase() == 'pendingseries' || $("#hid_PendingSeries").val().toLowerCase() == 'issueddraftseries')) {
        $('.ShowAuthorContractList').hide();
        $scope.ShowProductSeriesListForm = true;
        
        var GetProductSeriesList = AJService.GetDataFromAPI("AuthorContact/GetPendingProductSeriesContract?param=" + $("#hid_PendingSeries").val().toLowerCase(), null);
        GetProductSeriesList.then(function (ProductSeries) {
            $scope.ProductSeriesList = ProductSeries.data;
            $scope.ShowSearchForm = false;
            $scope.ShowListForm = false;
            $scope.ShowProductSeriesListForm = true;

            for (var i = 0; i < ProductSeries.data.length; i++) {

                if (mstr_SeriesCode != ProductSeries.data[i].SeriesCode) {

                    mstr_SeriesCode = ProductSeries.data[i].SeriesCode;

                    for (var j = 0; j < ProductSeries.data.length; j++) {
                        if (mstr_SeriesCode == ProductSeries.data[j].SeriesCode) {
                            temp_AutherData.push({
                                'AuthorContractId': ProductSeries.data[j].AuthorContractId,
                                'SeriesCode': ProductSeries.data[j].SeriesCode,
                                'AuthorContractCode': ProductSeries.data[j].AuthorContractCode,
                                'ContractEntryDate': ProductSeries.data[j].ContractEntryDate,
                                'ContractExpiryDate': ProductSeries.data[j].ContractExpiryDate,
                                'ProductName': ProductSeries.data[j].ProductName,
                                'ProductCode': ProductSeries.data[j].ProductCode,
                                'OUPISBN': ProductSeries.data[j].OUPISBN,
                                'Flag': ProductSeries.data[j].Flag
                            });
                        }
                    }

                    temp_SeriesCode.push({
                        'SeriesCode': ProductSeries.data[i].SeriesCode,
                        'AuthorData': temp_AutherData
                    });

                }
            }

            $scope.series_code = temp_SeriesCode;


            //for (var i = 0; i < ProductSeries.data.length; i++) {

            //    if (mstr_SeriesCode != ProductSeries.data[i].SeriesCode) {

            //        mstr_SeriesCode = ProductSeries.data[i].SeriesCode;

            //        for (var j = 0; j < ProductSeries.data.length; j++) {
            //            if (mstr_AC_Code != ProductSeries.data[j].AuthorContractCode && mstr_SeriesCode == ProductSeries.data[j].SeriesCode) {
            //                temp_AutherData.push({
            //                    'AuthorContractId': ProductSeries.data[j].AuthorContractId,
            //                    'SeriesCode': ProductSeries.data[j].SeriesCode,
            //                    'AuthorContractCode': ProductSeries.data[j].AuthorContractCode,
            //                    'ContractEntryDate': ProductSeries.data[j].ContractEntryDate,
            //                    'ContractExpiryDate': ProductSeries.data[j].ContractExpiryDate,
            //                    'ProductName': ProductSeries.data[j].ProductName,
            //                    'ProductCode': ProductSeries.data[j].ProductCode,
            //                    'OUPISBN': ProductSeries.data[j].OUPISBN,
            //                    'Flag': ProductSeries.data[j].Flag
            //                });
            //            }
            //            mstr_AC_Code = ProductSeries.data[j].AuthorContractCode;
            //        }

            //        temp_SeriesCode.push({
            //            'SeriesCode': ProductSeries.data[i].SeriesCode,
            //            'AuthorData': temp_AutherData
            //        });

            //    }
            //}

            //$scope.series_code = temp_SeriesCode;

        }, function () {
            //alert("Error in getting Product Series List");
        });
    }


    $scope.clear = function () {
        $scope.AuthorContract.AuthorContractCode = "";
        $scope.AuthorContract.SAPAgreementNo = ""
        $scope.AuthorContract.ProjectCode = ""
        $scope.AuthorContract.ProductCode = ""
        $scope.AuthorContract.ProductName = ""
        $scope.AuthorContract.SubProductName = ""
        $scope.AuthorContract.ISBN = ""
        $scope.DivSubDivCntrl.Division = ""
        $scope.DivSubDivCntrl.SubDivision = ""
        $scope.AuthorContract.ProductType = ""
        $scope.AuthorContract.SubProductType = ""
        $scope.AuthorContract.ProprietorImprint = ""
        $scope.AuthorContract.Language = ""
        $scope.AuthorContract.Authors = ""
        $scope.AuthorContract.SeriesList = ""
        $scope.AuthorContract.ProjectedPriceType = ""
        $scope.AuthorContract.ProjectedPrice = ""
        $scope.AuthorContract.ProjectedCurrency = ""
        $scope.AuthorContract.ProjectHandledBy = ""

        $scope.AuthorContract.ExpiryFromDate = ""
        $scope.AuthorContract.ExpiryToDate = ""
        $scope.AuthorContract.ISBNAssigned = ""
        $scope.AuthorContract.SAPAgreementUploaded = ""
        $scope.AuthorContract.RoyaltyTerms = ""
        $scope.AuthorContract.AdvanceAmount = ""

        $scope.AuthorContract.PriceType = ""

        $scope.AuthorContract.CurrencyValue = ""
        $scope.AuthorContract.ThridPartyPermission = ""
        $scope.AuthorContract.LicenseStatus = ""


        // $scope.userForm.AuthorContractCode.$modelvalue = ""
        $scope.AuthorContract.AuthorName = ""

        $scope.AuthorContract.contractType = ""

        $scope.AuthorContract.Territoryrights = ""
        $scope.AuthorContract.ThirdPartyPermission = ""
        $scope.AuthorContract.Royalty = ""


        $scope.AuthorContract.MediumofDelivery = ""



        $scope.AuthorContract.MenuScriptDeliveryFormat = ""
        $scope.AuthorContract.Contractstatus = ""
        $scope.AuthorContract.Remarks = ""
        $scope.AuthorContract.Amendment = ""

        $scope.AuthorContract.ByExecutive = ""
        $('[name=RequestFromDate]').val("");
        $('[name=RequestToDate]').val("");
        $('[name=DateofAgreementFromDate]').val("");
        $('[name=DateofAgreementToDate]').val("");
        $('[name=ExpiryDate]').val("");
        $scope.SelectedSupplyMaterialByAuthor = ""
    }


    $scope.AuthorContractSearch = function (AuthorContract) {
        if ($scope.AuthorContract == undefined) {
            $scope.AuthorContract = "";
        }

        if ($('[name=RequestFromDate]').val() != "") {
            var mstr_RequestFromDate = convertDate($('[name=RequestFromDate]').val())/// $('[name=RequestFromDate]').val(); 
        }
        if ($('[name=RequestToDate]').val() != "") {
            var mstr_RequestToDate = convertDate($('[name=RequestToDate]').val());
        }
        var mstr_DateofAgreementFromDate = null;
        var mstr_DateofAgreementToDate = null;

        if ($('#DateofAgreementFromDate').val() != "") {
            mstr_DateofAgreementFromDate = $('#DateofAgreementFromDate').val()
        }

        if ($('#DateofAgreementToDate').val() != "") {
            mstr_DateofAgreementToDate = $('#DateofAgreementToDate').val()
        }

        //if ($('[name=DateofAgreementFromDate]').val() != "") {
        //    var mstr_DateofAgreementFromDate = convertDate($('[name=DateofAgreementFromDate]').val());
        //}

        //if ($('[name=DateofAgreementToDate]').val() != "") {
        //    var mstr_DateofAgreementToDate = convertDate($('[name=DateofAgreementToDate]').val());
        //}

        //if ($('[name=ExpiryDate]').val() != "") {
        //    var mstr_ExpiryDate = convertDate($('[name=ExpiryDate]').val());
           
        //}
        //if ($('[name=ExpiryDate]').val() != "") {
        //    var mstr_ExpiryDate = ($('[name=ExpiryDate]').val());
        //}

        var mstr_ExpiryDate = null
        if ($('#ExpiryDate').val() != "")
        {
            mstr_ExpiryDate = $('#ExpiryDate').val()
        }
           
        var mstr_value = "";
        var mstr_Vlaue1 = "";
        if ($scope.SelectedSupplyMaterialByAuthor != null) {
            for (var i = 0; i < $scope.SelectedSupplyMaterialByAuthor.length; i++) {
                mstr_value = mstr_value + $scope.SelectedSupplyMaterialByAuthor[i].Id + ","
            }


        }
        if (mstr_value != "") {
            mstr_Vlaue1 = mstr_value.slice(0, -1)
        }
        else {
            mstr_Vlaue1 = null;
        }


       // alert(convertDate(mstr_ExpiryDate))

        if (mstr_RequestFromDate == "") {
            mstr_RequestFromDate = null;
        }


        if (mstr_RequestToDate == null) {
            mstr_RequestToDate = null;
        }
        if (mstr_DateofAgreementFromDate == null) {
            mstr_DateofAgreementFromDate = null;
        }
        if (mstr_DateofAgreementToDate == "") {
            mstr_DateofAgreementToDate = null;
        }
        if (mstr_ExpiryDate == "") {
            mstr_ExpiryDate = null;
        }
        var mobj_ThirdPartyValue = null;
        if ($('#hid_PermissionsInboundAuthor').val() != "") {
            mobj_ThirdPartyValue = '1';
        }
        else {
            mobj_ThirdPartyValue = $scope.AuthorContract.ThirdPartyPermission;
        }

        var Contractstatus = "";
        $('input[type=checkbox][name=Contractstatus]:visible:checked').each(function (index, value) {
            Contractstatus = Contractstatus + $(this).val() + ",";
        });

        var _For = null;
        if ($("#hid_Addendum").val() != "")
        {
            _For = $("#hid_Addendum").val();
        }
        if ($("#hid_Rights").val() != "") {
            _For = $("#hid_Rights").val();
        }
        if ($("#hid_permissionsoutbound").val() != "") {
            _For = $("#hid_permissionsoutbound").val();
        }
        if ($("#hid_PermissionsInboundAuthor").val() != "") {
            _For = $("#hid_PermissionsInboundAuthor").val();
        }

       
        Contractstatus = Contractstatus.substring(0, Contractstatus.length - 1);


        if ($scope.AuthorContract.ProductName!= "" && $scope.AuthorContract.ProductName != undefined && $scope.AuthorContract.ProductName != null) {
            if ($scope.AuthorContract.ProductName.indexOf("'") != -1) {
                $scope.AuthorContract.ProductName = $scope.AuthorContract.ProductName.replace("'", "''");
            }
        }

        if ($scope.AuthorContract.SubProductName != "" && $scope.AuthorContract.SubProductName != undefined && $scope.AuthorContract.SubProductName != null) {
            if ($scope.AuthorContract.SubProductName.indexOf("'") != -1) {
                $scope.AuthorContract.SubProductName = $scope.AuthorContract.SubProductName.replace("'", "''");
            }
        }

        var AuthorContact = {
            SAPAgreementNo: $scope.AuthorContract.SAPAgreementNo,
            ProjectCode: $scope.AuthorContract.ProjectCode,
            ProductCode: $scope.AuthorContract.ProductCode,
            ProductName: $scope.AuthorContract.ProductName,
            SubProductName: $scope.AuthorContract.SubProductName,
            ISBN: $scope.AuthorContract.ISBN,
            DivisionId: $scope.DivSubDivCntrl.Division,
            SubDivisionId: $scope.DivSubDivCntrl.SubDivision,
            ProductType: $scope.AuthorContract.ProductType,
            SubProductType: $scope.AuthorContract.SubProductType,
            ImprintId: $scope.AuthorContract.ProprietorImprint,
            LanguageId: $scope.AuthorContract.Language,
            AuthorId: $scope.AuthorContract.Authors,
            SeriesId: $scope.AuthorContract.Series,
            //SeriesId: $scope.Series,
            ProjectedPriceCond: $scope.AuthorContract.ProjectedPriceType,
            ProjectedPrice: $scope.AuthorContract.ProjectedPrice,
            ProjectCurrencyId: $scope.AuthorContract.ProjectedCurrency,
            ProjectHandleBy: $scope.AuthorContract.ProjectHandledBy,

            ExpiryFromDate: $scope.AuthorContract.ExpiryFromDate,
            ExpiryToDate: $scope.AuthorContract.ExpiryToDate,
            ISBNAssigned: $scope.AuthorContract.ISBNAssigned,
            SAPAgreementUploaded: $scope.AuthorContract.SAPAgreementUploaded,
            RoyaltyTerms: $scope.AuthorContract.RoyaltyTerms,
            AdvanceAmount: $scope.AuthorContract.AdvanceAmount,
            //   PriceType: $scope.AuthorContract.PriceType,
            PriceTypeCond: $scope.AuthorContract.PriceType,

            CurrencyId: $scope.AuthorContract.CurrencyValue,
            //ThridPartyPermission: $scope.AuthorContract.ThridPartyPermission,
            LicenseStatus: $scope.AuthorContract.LicenseStatus,


            AuthorContractCode: $scope.userForm.AuthorContractCode.$viewValue,
            AuthorName: $scope.AuthorContract.AuthorName,

            RequestFromDate: mstr_RequestFromDate,

            RequestToDate: mstr_RequestToDate,

            ContractType: $scope.AuthorContract.contractType,

            DateofagreementFromdate: mstr_DateofAgreementFromDate == null ? null : convertDate(mstr_DateofAgreementFromDate),

            DateofagreementTodate: mstr_DateofAgreementToDate == null ? null : convertDate(mstr_DateofAgreementToDate),
            ExpiryDate: mstr_ExpiryDate == null ? null : convertDate(mstr_ExpiryDate),

            Territory: $scope.AuthorContract.Territoryrights,
            ThirdPartyPermission: mobj_ThirdPartyValue,
            Royalty: $scope.AuthorContract.Royalty,
            MaterialSupplied: mstr_Vlaue1,

            MediumofDelivery: $scope.AuthorContract.MediumofDelivery,



            ManuscriptDeliveryFormat: $scope.AuthorContract.MenuScriptDeliveryFormat,
            //Contractstatus: $scope.AuthorContract.Contractstatus,
            Contractstatus: Contractstatus,
            Remarks: $scope.AuthorContract.Remarks,
            Amendment: $scope.AuthorContract.Amendment,

            ProjectHandleBy: $scope.AuthorContract.ByExecutive,
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            For_: _For,
            //AgreementStatus: AgreementStatus
        };

        if ($('#hid_Series').val() != "") {
            $scope.GetProductSeriesDetails($('#hid_Series').val(), $("#hid_Addendum").val());
        }
        else if ($("#hid_PendingSeries").val().toLowerCase() != 'pendingseries')  {
            var AuthorStatus = AJService.PostDataToAPI('AuthorContact/AuthorContractSearch', AuthorContact);

            AuthorStatus.then(function (msg) {
                blockUI.stop();

                if (msg.data == "OK") {
                    $scope.ProductLicenseListResult();                    
                }

            }, function () {
                $scope.IsError = false;
            });
            blockUI.stop();
        }

    }


    $scope.BackToserch = function () {
        if ($("#hid_show").val().toLowerCase() == "dashboard") {
            window.location.href = '../../Home/Dashboard/Dashboard';
        }
        else {
            var url = window.location.href;
            var q_string = "Listing";

            $('#hid_BackToserach').val("");

            if (url.indexOf(q_string) != -1)
                window.location.href = "AuthorContractSearch";
            else
                // window.location.href = window.location.href;
                if ($("#hid_PermissionsInboundAuthor").val() != "") {
                    window.location.href = GlobalredirectPath + 'Contract/AuthorContract/AuthorContractSearch?For=PermissionsInbound';
                }
                else {
                    window.location.href = GlobalredirectPath + 'Contract/AuthorContract/AuthorContractSearch';
                }


            $scope.ShowSearchForm = true;
            $scope.ShowListForm = false;
            $scope.ShowProductSeriesListForm = false;
            $scope.clear();


            //  $window.location.href = '../AuthorContract/AuthorContractSearch';

            //$('#hid_BackToserach').val("");
            //window.location.href = "AuthorContractSearch";
            //$scope.ShowSearchForm = true;
            //$scope.ShowListForm = false;
            //$scope.ShowProductSeriesListForm = false;
            //$scope.clear();
        }
    }




    $scope.DisplayDiv = function (count) {
        $('.tooltip-toggle').css("display", "none");
        $('.tooltip-toggle').removeAttr("id");
        $($('.tooltip-toggle')[count - 1]).attr("id", "tooltip-toggle");
    }
    $(".SuggestedAuthorName").autocomplete({

    });


    $scope.AutoComplete = function () {
        var obj = $(event.target);

        $(obj).autocomplete({
            source: $scope.AuthorList,
            autoFocus: true
        });
    }
    function fetchAuthor() {
        var AuthorList = [];
        var getAuthorList = AJService.GetDataFromAPI("CommonList/getAuthorList", null);
        getAuthorList.then(function (Author) {

            for (i = 0; i < Author.data.query.length; i++) {
                AuthorList[i] = Author.data.query[i].FirstName;
            }

        }, function () {
            //alert('Error in getting author list');
        });

        return AuthorList;
    }


    /*============================================================================================================
    Here is the section will used for open the author Contract form in view mode
    ============================================================================================================*/
    /******************************************************************************
    *******************************************************************************
    Created By  :  Dheeraj Kumar Sharma
    Created on  :  07/06/2016
    Created For :  Get the product details based on email Id
    
    *******************************************************************************
    *******************************************************************************/
    $scope.ProductSerach = function (Id) {

        if (Id != null) {
            var ProductData = {
                Id: Id
            };
            // blockUI.start();
            // call API to fetch temp Department list basis on the FlatId
            $('#hid_productid').val(Id);
            var ProductStatus = AJService.PostDataToAPI('ProductMaster/ProductDetails', ProductData);
            ProductStatus.then(function (msg) {
                $scope.ProductDetails = msg.data;
            }, function () {
                //alert('Error in getting department list');
            });
        }
    };

    $scope.SeriesProductSerach = function (Id) {

        if (Id != null) {
            var ProductData = {
                Id: Id
            };
            // blockUI.start();
            // call API to fetch temp Department list basis on the FlatId
            $('#hid_productid').val(Id);
            var ProductStatus = AJService.PostDataToAPI('ProductMaster/ProductDetails', ProductData);
            ProductStatus.then(function (msg) {
                $scope.ProductDetails = msg.data;
            }, function () {
                //alert('Error in getting department list');
            });
        }
    };


    $scope.SubsidiaryListorigional = [];
    $scope._subsidiaryListDataDisplay = [];
    $scope.AgreementStatus = "";
    $scope.ContributorAgreement = "";
    $scope.AgreementDate = "";
    $scope.ContributorDoc = [];
    $scope.AgreementDoc = [];
    $scope.GetAuthorContractDetails = function (Id) {

        $scope.ContractId = Id;

        //if ($('#hid_Addendum').val() != "" && typeof ($('#hid_Addendum').val()) !== "undefined") {
        //    if ($('#hid_User').val() == "admin") {
        //        $scope.UploadFIleReq = true;
        //        $scope.documentshow = true;
        //    }
        //    else if ($('#hid_User').val() == "Rights") {
        //        $scope.UploadFIleReq = true;
        //        $scope.documentshow = true;
        //    }
        //    else if ($('#hid_User').val() == "Editorial") {
        //        $scope.documentshow = true;
        //    }
        //    $scope.getAddendumDocumentList();
        //}

        var _ContractDetails = AJService.GetDataFromAPI("AuthorContact/GetAuthorContractDetails?Id=" + Id);
        _ContractDetails.then(function (_ContractDetails) {

            //angular.element(document.getElementById('angularid')).scope().ProductSerach(_ContractDetails.data._AuhtorContract.ProductId);
            angular.element(document.getElementById('angularid')).scope().ProductSerachContract(_ContractDetails.data._AuhtorContract.ProductId);

            $scope.HandledBy = _ContractDetails.data._AuhtorContract.HandledByName;
            if (_ContractDetails.data._AuhtorContract.LicenceId != undefined) {
                $scope.ProductLicenseSerach(_ContractDetails.data._AuhtorContract.LicenceId);
                LicenseId = _ContractDetails.data._AuhtorContract.LicenceId;
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
            $scope.MinWords = _ContractDetails.data._AuhtorContract.MinWords == 0 ? "---" : _ContractDetails.data._AuhtorContract.MinWords;
            $scope.MaxWords = _ContractDetails.data._AuhtorContract.MaxWords == 0 ? "---" : _ContractDetails.data._AuhtorContract.MaxWords;
            $scope.MinPages = _ContractDetails.data._AuhtorContract.MinPages == 0 ? "---" : _ContractDetails.data._AuhtorContract.MinPages;
            $scope.MaxPages = _ContractDetails.data._AuhtorContract.MaxPages == 0 ? "---" : _ContractDetails.data._AuhtorContract.MaxPages;
            $scope.PriceType = _ContractDetails.data._AuhtorContract.PriceType == null ? "---" : _ContractDetails.data._AuhtorContract.PriceType;
            $scope.Currency = _ContractDetails.data._AuhtorContract.Currency != null ? _ContractDetails.data._AuhtorContract.Currency : "---";
            $scope.Price = _ContractDetails.data._AuhtorContract.Price == 0 ? "---" : _ContractDetails.data._AuhtorContract.Price;
            $scope.MediumofDelivery = _ContractDetails.data._AuhtorContract.MediumofDelivery == null ? "---" : _ContractDetails.data._AuhtorContract.MediumofDelivery;
            $scope.Deliveryschedule = _ContractDetails.data._AuhtorContract.Deliveryschedule == null ? "---" : _ContractDetails.data._AuhtorContract.Deliveryschedule;
            $scope.ProductRemarks = _ContractDetails.data._AuhtorContract.ProductRemarks == "" ? "---" : _ContractDetails.data._AuhtorContract.ProductRemarks;
            $scope.MenuScriptDelivery = _ContractDetails.data._AuhtorContract.MenuScriptDelivery != "" ? _ContractDetails.data._AuhtorContract.MenuScriptDelivery : "---";
            $scope.ContributorList = _ContractDetails.data._contributor;
            $scope.MaterialSuppliedByAuthorList = _ContractDetails.data._MaterialDate;
            $scope.AuthorBox = _ContractDetails.data._AuthorList;
            

            $scope.RoyaltyslabList = _ContractDetails.data._royalty;
            $scope.TblList = _ContractDetails.data.TblList;
            $scope.SubsidiaryListorigional = _ContractDetails.data._susidiaryRightsList;
            $scope.ttlSubsidiary = _ContractDetails.data._ttlSusidiary;
            $scope._subsidiaryListDataDisplay = [];
            for (i = 0; i < $scope.SubsidiaryListorigional.length; i++) {
                if ($scope.SubsidiaryListorigional[i].OupPercentage != 100 && $scope.SubsidiaryListorigional[i].Percentage != 0) {
                    $scope._subsidiaryListDataDisplay.push($scope.SubsidiaryListorigional[i]);
                }
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
                //$scope.DocumentList = _ContractDetails.data._agreementDoc;
            }

            else {
                $scope.ContributorAgreement = "No";
            }

            if (_ContractDetails.data._agreementDoc != null && _ContractDetails.data._agreementDoc.length > 0) {
                for (var i = 0; i < _ContractDetails.data._agreementDoc.length; i++) {
                    if (_ContractDetails.data._agreementDoc[i].DocumentTypeId == 1) {
                        $scope.AgreementDoc.push(_ContractDetails.data._agreementDoc[i]);
                    }
                    else {

                        $scope.ContributorDoc.push(_ContractDetails.data._agreementDoc[i]);
                    }
                }
            }

            $scope.ContributorAgreement = $scope.ContributorDoc.length > 0 ? "Yes" : "No";
            $('#myModalAuthor').modal('show');
            /*this section is used to populate the section of contract agreement insert after the contrct already created*/
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


    $scope.GetSeriesList = function () {
        var GetSeries = AJService.GetDataFromAPI("SeriesMaster/GetProductSeries", null);
        GetSeries.then(function (Series) {
            $scope.SeriesList = Series.data;
        }, function () {
            //alert('Error in getting Series list');
        });
    }

    $scope.changeItem = function (iem) {
        $('#hid_Series').val(iem);
    }


    //$scope.isOptionsRequired = function () {
       
    //    return !$scope.volunteerOptions.some(function (options) {
    //        return options.selected;
    //    });
    //}
  


    function convertDateDDMMYYYY(inputFormat) {
        function pad(s) { return (s < 10) ? '0' + s : s; }
        var d = new Date(inputFormat);
        return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
    }


    $scope.ExcelReport = function () {
       
        document.location = GlobalredirectPath + "Contract/AuthorContract/exportToExcelProductList?SessionId=" + $("#hid_sessionId").val() + "";
      
    }

    $scope.AuthorContractReportExcel = function () {

        $scope.ExcelReport();
    }

    $scope.ExcelReportSeries = function () {

        var Id = localStorage["SeriesId"].split('#')[0];
        var For = localStorage["SeriesId"].split('#')[1];
        document.location = GlobalredirectPath + "Contract/AuthorContract/exportToExcelProductList?SessionId="
                                                                                                        + $("#hid_sessionId").val()
                                                                                                        + "&SeriesId=" + Id
                                                                                                        + "&SeriesName=Series List"
                                                                                                        + "&For=" + For
                                                                                                        + "";

    }

    $scope.AuthorContractSeriesReportExcel = function () {

        $scope.ExcelReportSeries();
    }


    var hid_for = $('#hid_for').val();
    //if (hid_for != "" && hid_for != null && hid_for != undefined)
    if (hid_for == "list")
    {
        angular.element(document.getElementById('angularid')).scope().ProductLicenseListResult();
    }
    if(hid_for == "serieslist")
    {
        var Id = localStorage["SeriesId"].split('#')[0];
        var For = localStorage["SeriesId"].split('#')[1];
        angular.element(document.getElementById('angularid')).scope().GetProductSeriesDetails(Id, For);
    }


    //added by Prakash on 17 Aug, 2017
    $scope.getProductSeriesList = function (series_Code) {
        //var series_Id = $('#hid_Series').val();
        localStorage["series_Code"] = series_Code;
        $scope.GetProductSeries_List($('#hid_Series').val());
    }

    $scope.GetProductSeries_List = function (Id) {
        var GetProductSeriesList = AJService.GetDataFromAPI("ProductMaster/GetProductSeriesList?ProductId=" + Id);
        GetProductSeriesList.then(function (ProductSeries) {
            $scope.ProductSeries_List = ProductSeries.data;
            blockUI.stop();
        }, function () {
            //alert("Error in getting Product Series List");
        });
    }

    $scope.setRestProductSeries = function (productIds) {
        setTimeout(function () { 
            $scope.resetProductSeriesData(productIds);
        }, 300);
    }

    $scope.resetProductSeriesData = function (productIds) {
        var restProductSeries = AJService.GetDataFromAPI("AuthorContact/InsertRestProductSeriesDetails?ProductId=" + productIds + '&SeriesCode=' + localStorage["series_Code"]);
        restProductSeries.then(function (msg) {
            if (msg.data.indexOf("OK") >= 0) {
                //SweetAlert.swal({
                //    title: "Success",
                //    text: text + "Insert successfully.",
                //    type: "success"
                //},
                //function () {
                    location.href = GlobalredirectPath + "Contract/AuthorContract/AuthorContractSearch?For=SeriesList"
                //});
            }
            else if (msg.data.indexOf("Exist") >= 0) {
                SweetAlert.swal("Error!", msg.data, "", "error");
            }
            else {
                SweetAlert.swal("Error!", msg.data, "", "error");
            }

        });
    }


    //For Delete Assignment Contract // Added by Prakash on 20 Sep, 2017
    $scope.DeleteContract = function (contractId, role, seriesCode) {
        var mobj_AuthorContract = {
            AuthorContractId: contractId == undefined ? 0 : (contractId == null ? 0 : contractId),
            Role: role == undefined ? null : role,
            DeactivateBy: parseInt($("#enterdBy").val()),
            SeriesCode: seriesCode == undefined ? "" : (seriesCode == null ? "" : seriesCode),
        };

        SweetAlert.swal({
            title: "Are you sure?",
            text: "You will not be able to recover this detail ! ",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false,
            closeOnCancel: true
        },
        function (Confirm) {
            if (Confirm) {
                blockUI.start();

                var ContractDelete = AJService.PostDataToAPI("AuthorContact/DeleteContractSet", mobj_AuthorContract);
                ContractDelete.then(function (msg) {
                    if (msg.data == "OK") {                      
                        SweetAlert.swal({
                            title: "Deleted!",
                            text: "Your record has been deleted.",
                            type: "success",
                            confirmButtonText: "OK",
                            closeOnConfirm: true
                        },
                        function (Confirm) {
                            if (Confirm) {
                                blockUI.stop();
                                if (seriesCode != undefined && seriesCode != null) {
                                    location.href = GlobalredirectPath + "Contract/AuthorContract/AuthorContractSearch?For=SeriesList"
                                }
                                else {
                                    angular.element(document.getElementById('angularid')).scope().ProductLicenseListResult();
                                }
                            }
                        });

                    }
                });


            }

        });

    }

    
});