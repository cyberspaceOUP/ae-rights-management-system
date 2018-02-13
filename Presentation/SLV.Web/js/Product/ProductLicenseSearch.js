

app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {
    app.expandControllerA($scope, AJService, $window);
    app.expandControllerDivisionSubDivision($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);

    $("#RequestToDate").attr("disabled", true); 
    $("#ExpiryToDate").attr("disabled", true);

    $scope.DivisionMandatory = false;
    $scope.SubDivisionMandatory = false;
    $scope.ShowSearchForm = true;
    angular.element(document.getElementById('angularid')).scope().getCurrencyList();
    angular.element(document.getElementById('angularid')).scope().getImprintList();
    angular.element(document.getElementById('angularid')).scope().getLanguageList();
    angular.element(document.getElementById('angularid')).scope().GetAuthorList();
    angular.element(document.getElementById('angularid')).scope().GetExecutiveList();
    angular.element(document.getElementById('angularid')).scope().getAllProductTypeList();

    $scope.req_ProjectCode = false;

    $scope.ProductLicenseListResult = function () {
        var ProductLicenseList = AJService.GetDataFromAPI("ProductLicense/GetProductLicenseSearchList?SessionId=" + $("#hid_sessionId").val() + "", null);
        ProductLicenseList.then(function (msg) {
            blockUI.stop();
            if (msg.data.length != 0) {
                $scope.ProductLicenseList = [];
                $scope.ProductLicenseList = msg.data;
                $scope.ShowSearchForm = false;

            }
            else {
                swal("No record", 'No record found', "warning");
            }
        });
    }


    if ($('#Hid_backToList').val() == "BackToserach")
   {
      
       $scope.ProductLicenseListResult();
    }



    $scope.BackToserch = function () {

        if ($('#hid_show').val() == 'dashboard') {
            window.location.href = "../../Home/Dashboard/Dashboard";
        }
        else {
            //var url = window.location.href;
            //var q_string = "BackToserach";

            //$('#Hid_backToList').val("");

            //if (url.indexOf(q_string) != -1)
            //    window.location.href = "ProductLicenseSearch";
            //else
            //    window.location.href = window.location.href;

            //$scope.ShowSearchForm = true;
            //$('#Hid_backToList').val("");
            //window.location.href = window.location.href;


            var mstr_history = document.referrer;

            if (mstr_history.indexOf("view") > 0) {
                window.location.href = "ProductLicenseSearch?For=View";
            }
            else if (mstr_history.indexOf("UpdateProductLicense") > 0) {
                window.location.href = "ProductLicenseSearch?For=Update";

            }
            else {
                window.location.href = window.location.href;
            }
        }

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


    $scope.submitForm = function (ProductModel) {

        $scope.submitted = true;

        $scope.productLicenseSearch(ProductModel);
        // set form default state
        $scope.userForm.$setPristine();
        // set form is no submitted
        $scope.submitted = false;
        return;
    }; 

    $scope.productLicenseSearch = function (ProductModel) {

        var _For = null;        
        if ($("#hid_PermissionsInbound").val() != "") {
            _For = $("#hid_PermissionsInbound").val();
        }

        if ($scope.ProductModel == undefined) {
            $scope.ProductModel = "";
        }
        var mobj_ThirpartyPermisiion = null;
        if ($('#hid_PermissionsInbound').val() != "") {

            mobj_ThirpartyPermisiion = "Y";
        }
        else {
            mobj_ThirpartyPermisiion = $scope.ProductModel.ThridPartyPermission;
        }

        var LicenseStatus = "";
        $('input[type=checkbox][name=LicenseStatus]:visible:checked').each(function (index, value) {
            LicenseStatus = LicenseStatus + $(this).val() + ",";
        });
        LicenseStatus = LicenseStatus.substring(0, LicenseStatus.length - 1);

        if ($scope.ProductModel.ProductName != "" && $scope.ProductModel.ProductName != undefined && $scope.ProductModel.ProductName != null) {
            if ($scope.ProductModel.ProductName.indexOf("'") != -1) {
                $scope.ProductModel.ProductName = $scope.ProductModel.ProductName.replace("'", "''");
            }
        }

        if ($scope.ProductModel.SubProductName != "" && $scope.ProductModel.SubProductName != undefined && $scope.ProductModel.SubProductName != null) {
            if ($scope.ProductModel.SubProductName.indexOf("'") != -1) {
                $scope.ProductModel.SubProductName = $scope.ProductModel.SubProductName.replace("'", "''");
            }
        }


        var Product = {
            SAPAgreementNo: $scope.ProductModel.SAPAgreementNo,
            ProjectCode: $scope.ProductModel.ProjectCode,
            ProductCode: $scope.ProductModel.ProductCode,
            ProductName: $scope.ProductModel.ProductName,
            SubProductName: $scope.ProductModel.SubProductName,
            ISBN: $scope.ProductModel.ISBN,
            DivisionId: $scope.DivSubDivCntrl.Division,
            SubDivisionId: $scope.DivSubDivCntrl.SubDivision,
            //ProductType: $scope.ProductModel.ProductType,
            ProductType : $('input[type=radio][name=ProductType]:checked').val(),
            SubProductType: $scope.ProductModel.SubProductType,
            ImprintId: $scope.ProductModel.ProprietorImprint,
            LanguageId: $scope.ProductModel.Language,
            AuthorId: $scope.ProductModel.Authors,
            SeriesId: $scope.ProductModel.SeriesList,
            ProjectedPriceCond: $scope.ProductModel.ProjectedPriceType,
            ProjectedPrice: $scope.ProductModel.ProjectedPrice,
            ProjectCurrencyId: $scope.ProductModel.ProjectedCurrency,
            ProjectHandleBy: $scope.ProductModel.ProjectHandledBy,
            ProductLicenseCode: $scope.ProductModel.ProductLicenseCode,
            ProductFromDate: $('input[type=text][name=RequestFromDate]').val() == "" ? null : convertDate($('input[type=text][name=RequestFromDate]').val()),
            ProductToDate: $('input[type=text][name=RequestToDate]').val() == "" ? null : convertDate($('input[type=text][name=RequestToDate]').val()),
            ExpiryFromDate: $('input[type=text][name=ExpiryFromDate]').val() == "" ? null : convertDate($('input[type=text][name=ExpiryFromDate]').val()),
            ExpiryToDate: $('input[type=text][name=ExpiryToDate]').val() == "" ? null : convertDate($('input[type=text][name=ExpiryToDate]').val()),
            ISBNAssigned: $scope.ProductModel.ISBNAssigned,
            SAPAgreementUploaded: $scope.ProductModel.SAPAgreementUploaded,
            RoyaltyTerms: $scope.ProductModel.RoyaltyTerms,
            AdvanceAmount: $scope.ProductModel.AdvanceAmount,
            PriceType: $scope.ProductModel.PriceType,
            PriceTypeCond: $scope.ProductModel.PriceTypeCond,
            Price: $scope.ProductModel.Price,
            CurrencyId: $scope.ProductModel.Currency,
            ThridPartyPermission: mobj_ThirpartyPermisiion,
            //LicenseStatus: $scope.ProductModel.LicenseStatus,
            LicenseStatus : LicenseStatus,
            Remarks: $scope.ProductModel.Remarks,

            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            For_: _For
        };

        var productStatus = AJService.PostDataToAPI('ProductLicense/ProductLicenseSearch', Product);

        productStatus.then(function (msg) {
            blockUI.stop();
            //if (msg.data.length != 0) {
            //    $scope.ProductLicenseList = msg.data;
            //    $scope.ShowSearchForm = false;
            //}
            //else {
            //    swal("No record", 'No record found', "warning");
            //}

            if (msg.data == "OK") {
                $scope.ProductLicenseListResult();
            }
        }, function () {
            $scope.IsError = false;
        });
        blockUI.stop();
    }

    $scope.DisplayDiv = function (count) {
        $('.tooltip-toggle').css("display", "none");
        $('.tooltip-toggle').removeAttr("id");
        $($('.tooltip-toggle')[count - 1]).attr("id", "tooltip-toggle");
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



    $scope.ExcelReport = function () {

        document.location = GlobalredirectPath + "Product/ProductLicense/exportToExcelProductLicenseList?SessionId=" + $("#hid_sessionId").val() + "";

    }



    $scope.ProductLicenseReportExcel = function () {



        $scope.ExcelReport();
    }

    //Added by prakash on 17 April, 2017
    $scope.GetSeriesList = function () {
        var GetSeries = AJService.GetDataFromAPI("SeriesMaster/GetProductSeries", null);
        GetSeries.then(function (Series) {
            $scope.SeriesList = Series.data;
        }, function () {
            //alert('Error in getting Series list');
        });
    }


});