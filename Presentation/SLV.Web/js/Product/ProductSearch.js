

app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {
    app.expandControllerA($scope, AJService, $window);
    $("#search").css("display", "none");
    app.expandControllerProprietorDetails($scope, AJService, $window);

    app.expandControllerAuthorSuggestion($scope, AJService, $window);

    app.expandControllerProductViewDetails($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);

    $scope.TitleLinking = false;
    $scope.ISBNAssign = false;
    $scope.FinalProductEntry = false;
    $scope.SAPAggrementEntry = false;

    //
    /******************** Start DivisionSubDivision Control********************/

    app.expandControllerDivisionSubDivision($scope, AJService, $window);
    angular.element(document.getElementById('angularid')).scope().getAllProductTypeList();

    $scope.ModelName = "ProductSearchModel";
    $scope.PublishingCompanyMandatory = false;
    $scope.ProprietorISBN = false;
    $scope.ProprietorProduct = false;
    $scope.ProprietorCopyrightYear = false;
    $scope.ProprietorEdition = false;
    $scope.Imprint = false;
    $scope.PubCenterMandatory = false;
    $scope.Sequence = 0;
    //
    $scope.countryNames = [];
    $scope.AuthorList = fetchAuthor();
    //
    $scope.ProductCategoryListEntry = [];

    StatusPermission();

    function StatusPermission() {
        if ($("#hid_deptCode").val() == "ED") {
            $('input[type=checkbox]:visible').each(function (index, value) {
                if ($(this).val() == 'PL_Y')
                    $(this).parent().hide();
                if ($(this).val() == 'PL_N')
                    $(this).parent().hide();
            });
        }
    }

    $scope.getAllProductCategoryListEntry = function () {
        var ProductCategoryList = AJService.GetDataFromAPI("CommonList/getAllProductCategoryList", null);
        ProductCategoryList.then(function (ProductCategory) {

            if ($("#hid_deptCode").val() == "ED") {
                for (var i = 0; i < ProductCategory.data.length; i++) {
                    if (ProductCategory.data[i].ProductCategoryCode == "OR" || ProductCategory.data[i].ProductCategoryCode == "CU" || ProductCategory.data[i].ProductCategoryCode == "TR" || ProductCategory.data[i].ProductCategoryCode == "AD" || ProductCategory.data[i].ProductCategoryCode == "SA") {
                        $scope.ProductCategoryListEntry.push(ProductCategory.data[i]);
                    }
                }
                $(".ProprietorDetailsMandatory").css("display", "none");
            }
            else {
                $scope.ProductCategoryListEntry = ProductCategory.data;
            }
        }, function () {
            //alert('Error in getting Product Category List');
        });
    }
    $scope.Save = function (seriesId) {
        if (seriesId != "") {
            sessionStorage.SeriesId = seriesId;
        }
        else {
            sessionStorage.SeriesId = null;
        }

    }

    $scope.getAllProductCategoryListEntry();
    $scope.searchProduct = function () {
        if ($('form[name$=userForm]').find(".has-error").length > 0) {
            return false;
        }


        blockUI.start();

        var ProjectedDate = $('#ProjectedPublishingDate').val();
        var FinalDate = $('#FinalPublishingDate').val();
        if (ProjectedDate == "") {
            ProjectedDate = null
        }

        if (FinalDate == "") {
            FinalDate = null
        }


        var mobj_thirdpartypermission = '';
        if ($('#hid_PermissionsInbound').val() == 'permissionsinbound') {
            mobj_thirdpartypermission = 1
        }
        else {
            mobj_thirdpartypermission = $scope.userForm.ThirdPartyPermission.$modelValue;
        }

        var Status = "";
        $('input[type=checkbox]:visible:checked').each(function (index, value) {
            Status = Status + $(this).val() + ",";

            if ($(this).val().toLowerCase() == "pi_y" || $(this).val().toLowerCase() == "pi_n") {
                mobj_thirdpartypermission = 1
            }

        });
        Status = Status.substring(0, Status.length - 1);

        $scope.Save($('#hid_Series').val());

        if ($scope.userForm.WorkingProduct.$modelValue != "" && $scope.userForm.WorkingProduct.$modelValue != undefined && $scope.userForm.WorkingProduct.$modelValue != null) {
            if ($scope.userForm.WorkingProduct.$modelValue.indexOf("'") != -1) {
                $scope.userForm.WorkingProduct.$modelValue = $scope.userForm.WorkingProduct.$modelValue.replace("'", "''");
            }
        }

        if ($scope.userForm.WorkingSubProduct.$modelValue != "" && $scope.userForm.WorkingSubProduct.$modelValue != undefined && $scope.userForm.WorkingSubProduct.$modelValue != null) {
            if ($scope.userForm.WorkingSubProduct.$modelValue.indexOf("'") != -1) {
                $scope.userForm.WorkingSubProduct.$modelValue = $scope.userForm.WorkingSubProduct.$modelValue.replace("'", "''");
            }
        }
        
        var Product = {
            DivisionId: $scope.userForm.Division.$modelValue,
            ProductCategoryId: $scope.userForm.ProductCategory.$modelValue,
            ProductTypeId: $('input[type=radio][name*=ProductType]:checked').val(),
            SubDivisionId: $('select[name*=SubDivision]').val(),
            SubProductType: $('select[name*=SubProductType]').val(),
            //ProjectCode: $scope.userForm.ProjectCode.$modelValue,
            ProductCode: $scope.userForm.ProjectCode.$modelValue,
            OupIsbn: $scope.userForm.OUPISBN.$modelValue,
            WorkingProduct: $scope.userForm.WorkingProduct.$modelValue,
            WorkingSubProduct: $scope.userForm.WorkingSubProduct.$modelValue,
            OupEdition: $('input[name$=OUPEdition]').val(),
            Volume: $('input[name$=Volume]').val(),
            CopyrightYear: $scope.userForm.CopyrightYear.$modelValue,
            Imprint: $scope.userForm.ProductImprint.$modelValue,
            LanguageId: $scope.userForm.Language.$modelValue,
            Series: $('select[name$=Series]').val(),
            AuthorCategory: $('.DetailsSearch').find('input[type=radio][name*=AuthorCategory]:checked').val(),
            AuthorName: $($('[name$=SuggestedAuthorName]')[0]).val(),
            Derivatives: $scope.userForm.Derivaties.$modelValue,
            ProjectedDate: $('#ProjectedPublishingDate').find('input').val() != "" ? convertDate($('#ProjectedPublishingDate').find('input').val()) : null,
            ProjectedPrice: $scope.userForm.ProjectedPrice.$modelValue,
            ProjectedCurrencyId: $scope.userForm.ProjectedCurrency.$modelValue,
            FinalProductEntered: $scope.userForm.ProductEntered.$modelValue,
            FinalProduct: $scope.userForm.finalProduct.$modelValue,
            FinalPublishingDate: $('#FinalPublishingDate').find('input').val() != "" ? convertDate($('#FinalPublishingDate').find('input').val()) : null,
            SapAuthorCode: $scope.userForm.SAPAuthorCode.$modelValue,
            SapAgreementNo: $scope.userForm.SAPAgreementNumber.$modelValue,
            ProprietorAuthorType: $('.DetailsSearch').find('input[type=radio][name*=ProprietorDetails]:checked').val(),
            //ProprietorAuthorName: $($('[name$=SuggestedAuthorName]')[1]).val(),
            ProprietorAuthorName: $($('[name$=ProprietorAuthorName]')).val(),
            ProprietorIsbn: $scope.userForm.ProprietorISBN.$modelValue,
            ProprietorEdition: $scope.userForm.ProprietorEdition.$modelValue,
            ProprietorCopyright: $scope.userForm.ProprietorCopyrightYear.$modelValue,
            ProprietorAuthorCategoryId: $scope.userForm.AuthorCategory.$modelValue,
            ProprietorPubCenterId: $scope.userForm.PubCenter.$modelValue,
            ProprietorPublishingCompanyId: $("#PublishingCompany").val(),
            ProprietorProduct: $scope.userForm.ProprietorProduct.$modelValue,
            ProprietorImprintId: $scope.userForm.Imprint.$modelValue,
            DepartmentId: $("#hid_DepartmentId").val(),
            TypeFor: $('#hid_for').val(),
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            Status: Status,
            License: $("#hid_License").val() !== "undefined" ? $("#hid_License").val() : null,
            ThirdPartyPermission: mobj_thirdpartypermission,
            KitISBN: $scope.userForm.KitISBN.$modelValue,
            KitWorkingProduct: $scope.userForm.KitWorkingProduct.$modelValue,
            KitWorkingSubProduct: $scope.userForm.KitWorkingSubProduct.$modelValue,
        };



        if (typeof ($('#hid_Report').val() !== "undefined") && $('#hid_Report').val()) {

            var mstr_ProjectedPublishingDate = ""
            var mstr_FinalPublishingDate = ""
            if ($('#ProjectedPublishingDate').find('input').val() != "") {
                mstr_ProjectedPublishingDate = $('#ProjectedPublishingDate').find('input').val();
            }
            if ($('#FinalPublishingDate').find('input').val() != "") {
                mstr_FinalPublishingDate = $('#FinalPublishingDate').find('input').val();
            }


           

            $('#hid_Report').val("");

            //document.location = "/RMS/ProductMaster/exportToExcelProductList?DivisionId=" + $scope.userForm.Division.$modelValue + "&ProductCategory=" + $scope.userForm.ProductCategory.$modelValue + "&ProductType=" + $('input[type=radio][name*=ProductType]:checked').val() + "&SubDivision=" + $scope.userForm.SubDivision.$modelValue + "&SubProductType=" + $scope.userForm.SubProductType.$modelValue + "&ProjectCode=" + $scope.userForm.ProjectCode.$modelValue + "&OupIsbn=" + $scope.userForm.OUPISBN.$modelValue + "&WorkingProduct=" + $scope.userForm.WorkingProduct.$modelValue + "&WorkingSubProduct=" + $scope.userForm.WorkingSubProduct.$modelValue + "&OupEdition=" + $('input[name$=OUPEdition]').val() + "&Volume=" + $('input[name$=Volume]').val() + "&CopyrightYear=" + $scope.userForm.CopyrightYear.$modelValue + "&Imprint=" + $scope.userForm.ProductImprint.$modelValue + "&Language=" + $scope.userForm.Language.$modelValue + "&Series=" + $('input[name$=Series]').val() + "&AuthorCategory=" + $('.DetailsSearch').find('input[type=radio][name*=AuthorCategory]:checked').val() + "&AuthorName=" + $($('[name$=SuggestedAuthorName]')[0]).val() + "&Derivatives=" + $scope.userForm.Derivaties.$modelValue + "&ProjectedDate=" + mstr_ProjectedPublishingDate + "&ProjectedPrice=" + $scope.userForm.ProjectedPrice.$modelValue + "&ProjectedCurrency=" + $scope.userForm.ProjectedCurrency.$modelValue + "&FinalProductEntered=" + $scope.userForm.ProductEntered.$modelValue + "&FinalProduct=" + $scope.userForm.finalProduct.$modelValue + "&FinalPublishingDate=" + mstr_FinalPublishingDate + "&SapAuthorCode=" + $scope.userForm.SAPAuthorCode.$modelValue + "&SapAgreementNo=" + $scope.userForm.SAPAgreementNumber.$modelValue + "&ProprietorAuthorType=" + $('.DetailsSearch').find('input[type=radio][name*=ProprietorDetails]:checked').val() + "&ProprietorAuthorName=" + $($('[name$=SuggestedAuthorName]')[1]).val() + "&ProprietorIsbn=" + $scope.userForm.ProprietorISBN.$modelValue + "&ProprietorEdition=" + $scope.userForm.ProprietorEdition.$modelValue + "&ProprietorCopyright=" + $scope.userForm.ProprietorCopyrightYear.$modelValue + "&ProprietorAuthorCategory=" + $scope.userForm.AuthorCategory.$modelValue + "&ProprietorPubCenter=" + $scope.userForm.PubCenter.$modelValue + "&ProprietorPublishingCompany=" + $("#PublishingCompany").val() + "&ProprietorProduct=" + $scope.userForm.ProprietorProduct.$modelValue + "&ProprietorImprint=" + $scope.userForm.Imprint.$modelValue + "&DepartmentId=" + $("#hid_DepartmentId").val() + mstrSearchparameter + "";

            //document.location = GlobalredirectPath + "/ProductMaster/exportToExcelProductList?Division=" + ($('#Division').val() == "" ? null : $("#Division option[value='" + $scope.userForm.Division.$modelValue + "']").text()) + "&ProductCategory=" + ($('#ProductCategory').val() == "" ? null : $("#ProductCategory option[value='" + $scope.userForm.ProductCategory.$modelValue + "']").text()) + "&ProductType=" + ($('input[type=radio][name*=ProductType]:checked').val() == "undefined" ? null : $("#ProductType option[value='" + $('input[type=radio][name*=ProductType]:checked').val() + "']").text()) + "&SubDivision=" + ($('#SubDivision').val() == "" ? null : $("#SubDivision option[value='" + $scope.userForm.SubDivision.$modelValue + "']").text()) + "&SubProductType=" + ($('#SubProductType').val() == "" ? null : $("#SubProductType option[value='" + $scope.userForm.SubProductType.$modelValue + "']").text()) + "&ProjectCode=" + $scope.userForm.ProjectCode.$modelValue + "&OupIsbn=" + $scope.userForm.OUPISBN.$modelValue + "&WorkingProduct=" + $scope.userForm.WorkingProduct.$modelValue + "&WorkingSubProduct=" + $scope.userForm.WorkingSubProduct.$modelValue + "&OupEdition=" + $('input[name$=OUPEdition]').val() + "&Volume=" + $('input[name$=Volume]').val() + "&CopyrightYear=" + $scope.userForm.CopyrightYear.$modelValue + "&Imprint=" + $scope.userForm.ProductImprint.$modelValue + "&Language=" + $scope.userForm.Language.$modelValue + "&Series=" + $('input[name$=Series]').val() + "&AuthorCategory=" + $('.DetailsSearch').find('input[type=radio][name*=AuthorCategory]:checked').val() + "&AuthorName=" + $($('[name$=SuggestedAuthorName]')[0]).val() + "&Derivatives=" + $scope.userForm.Derivaties.$modelValue + "&ProjectedDate=" + mstr_ProjectedPublishingDate + "&ProjectedPrice=" + $scope.userForm.ProjectedPrice.$modelValue + "&ProjectedCurrency=" + $scope.userForm.ProjectedCurrency.$modelValue + "&FinalProductEntered=" + $scope.userForm.ProductEntered.$modelValue + "&FinalProduct=" + $scope.userForm.finalProduct.$modelValue + "&FinalPublishingDate=" + mstr_FinalPublishingDate + "&SapAuthorCode=" + $scope.userForm.SAPAuthorCode.$modelValue + "&SapAgreementNo=" + $scope.userForm.SAPAgreementNumber.$modelValue + "&ProprietorAuthorType=" + $('.DetailsSearch').find('input[type=radio][name*=ProprietorDetails]:checked').val() + "&ProprietorAuthorName=" + $($('[name$=SuggestedAuthorName]')[1]).val() + "&ProprietorIsbn=" + $scope.userForm.ProprietorISBN.$modelValue + "&ProprietorEdition=" + $scope.userForm.ProprietorEdition.$modelValue + "&ProprietorCopyright=" + $scope.userForm.ProprietorCopyrightYear.$modelValue + "&ProprietorAuthorCategory=" + $scope.userForm.AuthorCategory.$modelValue + "&ProprietorPubCenter=" + $scope.userForm.PubCenter.$modelValue + "&ProprietorPublishingCompany=" + $("#PublishingCompany").val() + "&ProprietorProduct=" + $scope.userForm.ProprietorProduct.$modelValue + "&ProprietorImprint=" + $scope.userForm.Imprint.$modelValue + "&DepartmentId=" + $("#hid_DepartmentId").val() + "&SessionId=" + $("#hid_sessionId").val() + "";
            document.location = GlobalredirectPath + "ProductMaster/exportToExcelProductList?SessionId=" + $("#hid_sessionId").val() + "";
            blockUI.stop();

        }
        else {

            if ($('#hid_Series').val() != "" && $('#hid_type').val() == 'newcontract') {
                $scope.GetProductSeriesDetails($('#hid_Series').val());
            }
            else {

                var productStatus = AJService.PostDataToAPI('Author/ProductSearch', Product);
                productStatus.then(function (msg) {
                    if (msg.data == "OK") {
                        $scope.SearchingListResult();
                    }
                }, function () {
                    $scope.IsError = false;
                });
                blockUI.stop();
            }
        }
    }

    $scope.GetProductSeriesDetails = function (Id) {
        var GetProductSeriesList = AJService.GetDataFromAPI("ProductMaster/GetProductSeriesList?ProductId=" + Id);
        GetProductSeriesList.then(function (ProductSeries) {
            $scope.ProductSeriesList = ProductSeries.data;
            //$scope.ShowSearchForm = false;
            //$scope.ShowListForm = false;
            $scope.ShowProductSeriesListForm = true;

            $("#Product_search").css("display", "none");
            $("#headedDiv").css("display", "none");
            $("#search").css("display", "none");

            blockUI.stop();

        }, function () {
            //alert("Error in getting Product Series List");
        });
    }

    //function convertDate(date) {
    //    var datearray = date.split("/");
    //    return datearray[3] + '/' + datearray[1] + '/' + datearray[2];
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

    $scope.submitForm = function () {
        $scope.submitted = true;

        $scope.searchProduct();
        // set form default state
        $scope.userForm.$setPristine();
        // set form is no submitted
        $scope.submitted = false;
        return;
    };
    
    $scope.submitTitleLink = function () {
        $scope.submitted = true;
        var marr_addendum = [];
        var marr_ISBNBag = [];
        if ($scope.ISBNAssign) {
            $scope.Serchform.$valid = true;
        }
        if ($scope.TitleLinking) {
            $scope.Serchform.$valid = true;
        }
        if ($scope.Serchform.$valid) {
            if ($scope.TitleLinking) {
                $("[name$=hid_multipleLicenseId]").each(function () {
                    if ($(this).val() != "" && $(this).val() != "0") {
                        var productId = $(this).prev("[name=hid_multipleproductId]").val();
                        var mobj_AddendumLink = {
                            ProductId: productId,
                            LicenseId: $(this).val(),
                            Active: "Y",
                        }
                        marr_addendum.push(mobj_AddendumLink);
                    }
                });

                if (marr_addendum.length == 0) {
                    var textbox = $("[name$=productLicense]")[0];
                    $(textbox).closest(".form-group").addClass("has-error");
                    $(textbox).next().find('p').addClass('ng-show').removeClass("ng-hide");
                    $(textbox).focus();
                    $scope.Serchform.$valid = false;
                }

            }

            if ($scope.ISBNAssign) {
                $("[name$=hid_ISBNAssign]").each(function () {
                    if ($(this).val() != "" && $(this).val() != "0") {
                        var productId = $(this).parent().find("[name=hid_ISBNproductId]").val();
                        var mobj_ISBNBag = {
                            Id: $(this).val(),
                            ProductId: productId,
                            EnteredBy: $("#enterdBy").val(),
                        }
                        marr_ISBNBag.push(mobj_ISBNBag);
                    }
                });

                if (marr_ISBNBag.length == 0) {
                    var textbox = $("[name$=ISBNAssignment]")[0];
                    $(textbox).closest(".form-group").addClass("has-error");
                    $(textbox).next().find('p').addClass('ng-show').removeClass("ng-hide");
                    $(textbox).focus();
                    $scope.Serchform.$valid = false;
                }

            }

            if ($scope.Serchform.$valid) {
                if ($scope.TitleLinking) {
                    $scope.submitMultipleProductLink(marr_addendum);
                }
                if ($scope.ISBNAssign) {
                    $scope.submitISBNAssign(marr_ISBNBag);
                }
                // set form default state
                $scope.Serchform.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    };

    $scope.submitMultipleProductLink = function (marr_addendum) {

        var _mobjProductMultipleLink = {
            ProductLicenseAddendumLink: marr_addendum,
            EnteredBy: $("#enterdBy").val(),
        }

        var ProductStatus = AJService.PostDataToAPI('ProductLicense/InsertMultipleProductLinking', _mobjProductMultipleLink);

        ProductStatus.then(function (msg) {

            if (msg.data != "OK") {
                SweetAlert.swal('There is some problem.', '', "Try agian");
            }
            else {
                SweetAlert.swal({
                    title: "Insert successfully.",
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
            alert('Please validate details');
        });

    }


    $scope.submitISBNAssign = function (marr_ISBNBag) {

        var ProductStatus = AJService.PostDataToAPI('Addendum/ISBNAssign', marr_ISBNBag);

        ProductStatus.then(function (msg) {
            if (msg.data == "otheruser") {
                SweetAlert.swal("Try again!", "This ISBN selected from other user !", "info");
            }
            else if (msg.data != "OK") {
                SweetAlert.swal('There is some problem.', '', "erroe");
            }
            else {
                SweetAlert.swal({
                    title: "Success",
                    text: "Insert successfully.",
                    type: "success"
                },
                function () {
                    //$('form[name*=user]').attr("method", "post");
                    //$('form[name*=user]').submit();
                    document.location = GlobalredirectPath + "Product/ProductMaster/ProductDetailsView?Id=" + marr_ISBNBag[0].ProductId + "&For=View";
                });

            }

        },
        function () {
            alert('Please validate details');
        });

    }


    $scope.CheckLicenseCode = function (textbox) {
        var licenseCode = $(textbox).val();
        var oldlicesnecode = $(textbox).attr("oldlicensecode");
        if (licenseCode != oldlicesnecode) {
            if (licenseCode != '') {
                var mobj_LicesneDetails = {
                    ProductLicensecode: licenseCode,
                }
                $(textbox).attr("oldlicensecode", $(textbox).val());
                var getLicenseCodeResult = AJService.PostDataToAPI("ProductLicense/checkLicenseCode", mobj_LicesneDetails);
                getLicenseCodeResult.then(function (Result) {
                    if (Result.data == 0) {
                        $(textbox).closest('div').find("[name=hid_multipleLicenseId]").val(0);
                        $(textbox).closest(".form-group").addClass("has-error");
                        $(textbox).next().find('p').addClass('ng-show').removeClass("ng-hide");
                        $(textbox).focus();
                        $scope.Serchform.$valid = false;

                    }
                    else {
                        $(textbox).closest('div').find("[name=hid_multipleLicenseId]").val(Result.data);
                        $(textbox).closest(".form-group").removeClass("has-error");
                        $(textbox).next().find('p').removeClass('ng-show').addClass("ng-hide");
                    }

                }, function () {
                    alert('Error in checking product license code');
                });
            }
            else {
                $(textbox).closest('div').find("[name=hid_multipleLicenseId]").val(0);
                $(textbox).closest(".form-group").removeClass("has-error");
                $(textbox).next().find('p').removeClass('ng-show').addClass("ng-hide");
            }
        }

    }

    $scope.BackToserch = function () {
        if ($("#hid_show").val().toLowerCase() == "dashboard" || $("#hid_for_Dashboard").val().toLowerCase() == "dashboard") {
            window.location.href = GlobalredirectPath +  'Home/Dashboard/Dashboard';
        }
        else {

             $("#search").css("display", "none");
             $scope.model = {};
             $("#Product_search").css("display", "block");
             $scope.ShowProductSeriesListForm = false;

            var url = window.location.href;
            var q_string = "backtoSearch";

            if (url.indexOf(q_string) != -1)
                window.location.href = "ProductSearch";
            else
                window.location.href = window.location.href;

           // $("#search").css("display", "none");
           // $scope.model = {};
           // ////$scope.userForm.FirstName.$modelValue = null;
           // $("#Product_search").css("display", "block");
           // $scope.ShowProductSeriesListForm = false;
           //window.location.href = location.href;
        }

    }

    

    $scope.DisplayDiv = function (count) {
        $('.tooltip-toggle').css("display", "none");
        $('.tooltip-toggle').removeAttr("id");
        $($('.tooltip-toggle')[count - 1]).attr("id", "tooltip-toggle");
    }


    //$(".SuggestedAuthorName").autocomplete({
    //    source: availableTutorials,
    //    autoFocus: true
    //});
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



    $scope.AutoCompleteISBN = function () {
        var obj = $(event.target);
        if ($(obj).attr("setcompelte") == "T") {
            var ISBNList = [];

            var mobj_ProductMaster = {
                Id: $(obj).attr("productid"),
            };
            var getISBNList = AJService.PostDataToAPI("CommonList/GetISBNList", mobj_ProductMaster);
            getISBNList.then(function (ISBN) {

                for (i = 0; i < ISBN.data.query.length; i++) {
                   ISBNList[i] = ISBN.data.query[i].ISBN;
                }

                $(obj).autocomplete({
                    source: ISBNList,
                    autoFocus: true,
                    select: function (event, ui) {
                        
                        $(obj).parent('div').find("[name$=hid_ISBNAssign]").val(1);
                        $(obj).attr("disabled", "disabled");
                        $(obj).parent('div').find("i").removeAttr("style");
                        $(obj).parent('div').find("i").click(function () {
                            $(obj).val("");
                            $(obj).parent('div').find("i").attr("style", "display:none");
                            $(obj).removeAttr("disabled");
                            $(obj).parent('div').find("[name$=hid_ISBNAssign]").val("");
                        });


                    }
                })
                $(obj).attr("setcompelte", "Y");
            }, function () {
                //alert('Error in getting ISBN list');
            });
        }

    }



    $scope.ProductReportExcel = function () {
      
      $('#hid_Report').val(1);

       $scope.searchProduct();
    }

    $scope.SeriesReportExcel = function () {
        document.location = GlobalredirectPath + "/ProductMaster/exportToExcelProductList?SessionId="
                                                                                                + $("#hid_sessionId").val()
                                                                                                + "&SeriesId=" + $('#hid_Series').val()
                                                                                                + "&SeriesName=Series List"
                                                                                                + "";
    }

    $scope.ProductSeriesReportExcel = function () {

        $scope.SeriesReportExcel();
    }

    $scope.SearchingListResult = function () {
        if (sessionStorage.SeriesId != "null" && sessionStorage.SeriesId != undefined && $("#hid_show").val().toLowerCase() != "dashboard" && $('#hid_type').val() == 'newcontract') {
            $scope.GetProductSeriesDetails(sessionStorage.SeriesId);
            return false;
        }

        var productList = AJService.GetDataFromAPI("Author/GetSerachProductListing?SessionId=" + $("#hid_sessionId").val() + "", null);
        productList.then(function (msg) {
            blockUI.stop();
            if (msg.data == 'error' || msg.data.length == 0)
            {
                SweetAlert.swal({
                    title: "No record",
                    text: "No record found",
                    type: "warning"
                },
                function () {
                    document.location = GlobalredirectPath + "Product/ProductMaster/ProductSearch?for=" + $('#hid_ForType').val();
                });
            }
            else if (msg.data.length != 0) {
                $scope.ProductList = [];
                $scope.ProductList = msg.data;
                $("#Product_search").css("display", "none");
                $("#headedDiv").css("display", "none");
                $("#search").css("display", "block");
                if ($("#hid_for").val() == "multiplelinking") {
                    $scope.TitleLinking = true;
                }

                if ($("#hid_for").val() == "isbnassign") {
                    $scope.ISBNAssign = true;
                    setTimeout(function () {
                        $("[name$=ISBNAssignment]").each(function () {
                            var obj = $(this);
                            var ISBN_Id = 0;
                            if ($(obj).attr("setcompelte") == "N") {
                                var ISBNList = [];

                                var mobj_ProductMaster = {
                                    Id: $(obj).attr("productid"),
                                };
                                var getISBNList = AJService.PostDataToAPI("CommonList/GetISBNList", mobj_ProductMaster);
                                getISBNList.then(function (ISBN) {                                   
                                    
                                    for (i = 0; i < ISBN.data.query.length; i++) {
                                         ISBNList[i] = { "label": ISBN.data.query[i].ISBN, "value": ISBN.data.query[i].ISBN, "data": ISBN.data.query[i].Id };
                                    }

                                    $(obj).autocomplete({                                        
                                        //source: ISBNList,
                                        source: function (request, response) {
                                            var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i"); //RegExp("^" + request.term, "i"); //
                                           // var matcher = new RegExp("(\\b" + $.ui.autocomplete.escapeRegex(request.term) + "\\b)(?=\\s|$)(?![^<]*?>)", ["gi"]);
                                            response($.grep(ISBNList, function (item) {                                                
                                               return matcher.test(item.label);
                                            }));
                                        },
                                        
                                        autoFocus: true,
                                        select: function (event, ui) {
                                            ISBN_Id = ui.item.data;
                                            if ($($('.ui-autocomplete > li')[0]).html() != ui.item.value) {
                                                alert("Only first Isbn in list can be assign");
                                                return false;
                                                e.preventDefault();
                                            }

                                            //Start blocked selected ISBN
                                            var mobj_ISBNBlock = {
                                                Id: ISBN_Id,
                                                EnteredBy: $("#enterdBy").val(),
                                            }
                                            var ProductStatus = AJService.PostDataToAPI('Addendum/ISBNBlocked', mobj_ISBNBlock);
                                            ProductStatus.then(function (msg) {
                                                if (msg.data == "otheruser") {
                                                    alert("This ISBN selected from other user !");
                                                }
                                            });
                                            //End blocked selected ISBN

                                            var mint_titleExist = $("#tbl_ISBNtable tr input:hidden[name=hid_ISBNAssign][value=" + ui.item.data + "]:not([value=0])")
                                                        .not($(this).closest('td').find('input:hidden[id^=hid_ISBNAssign]')
                                                       ).length;

                                            if (mint_titleExist != 0) {
                                                SweetAlert.swal({
                                                    title: "Validation!",
                                                    text: "This isbn is already selected. Please select other isbn !",
                                                    type: "error"
                                                },
                                               function () {
                                                   $(obj).val("");
                                               });

                                            }
                                            else {
                                                $(obj).parent('div').find("[name$=hid_ISBNAssign]").val(ui.item.data);
                                                $(obj).$modelVale = ui.item.label;
                                                $(obj).attr("disabled", "disabled");
                                                $(obj).closest(".form-group").removeClass("has-error");
                                                $(obj).next().next().find('p').removeClass('ng-show').addClass("ng-hide");
                                                $(obj).parent('div').find("i").removeAttr("style");
                                                $(obj).parent('div').find("i").click(function () {
                                                    $(obj).val("");
                                                    $(obj).parent('div').find("i").attr("style", "display:none");
                                                    $(obj).removeAttr("disabled");
                                                    $(obj).parent('div').find("[name$=hid_ISBNAssign]").val("");
                                                    $(obj).parent('div').find("i").unbind("click");
                                                });
                                            }


                                            // }
                                            //else {
                                            //    setTimeout(function () {
                                            //        $(obj).val("");
                                            //    }, 10);
                                            //}
                                        }
                                    });
                                    $(obj).attr("setcompelte", "Y");
                                }, function () {
                                    //alert('Error in getting ISBN list');
                                });
                            }


                        });
                    }, 200);
                }

                if ($("#hid_for").val() == "finalproductentry") {
                    $scope.FinalProductEntry = true;
                }
                if ($("#hid_for").val() == "sapaggrement") {
                    $scope.SAPAggrementEntry = true;
                }


            }
            else {
                //swal("No record", 'No record found', "warning");
                //document.location = GlobalredirectPath + "/Product/ProductMaster/ProductSearch";

                if ($('#hid_PermissionsInbound').val() == 'permissionsinbound') {
                    document.location = GlobalredirectPath + "Product/ProductMaster/ProductSearch?For=PermissionsInbound";
                }
                else {
                    document.location = GlobalredirectPath + "Product/ProductMaster/ProductSearch";
                }

                //SweetAlert.swal({
                //    title: "No record",
                //    text: "No record found",
                //    type: "warning"
                //},
                //function () {
                //    if ($('#hid_PermissionsInbound').val() == 'permissionsinbound') {
                //        document.location = GlobalredirectPath + "/Product/ProductMaster/ProductSearch?For=PermissionsInbound";
                //    }
                //    else {
                //        document.location = GlobalredirectPath + "/Product/ProductMaster/ProductSearch";
                //    }
                //});
            }
        });
    }

    //$scope.SetContractDate = function (datetext) {

    //    setTimeout(function () {
    //        $('[name=CopyrightYear]').val($(datetext).val().split('/')[2]);
    //    }, 100)

    //}

  

    $scope.GetSeriesList = function () {
        var GetSeries = AJService.GetDataFromAPI("SeriesMaster/GetProductSeries", null);
        GetSeries.then(function (Series) {
            $scope.SeriesList = Series.data;
        }, function () {
            //alert('Error in getting Series list');
        });
    }

    $scope.changeItem = function (item) {
        $('#hid_Series').val(item);
    }


    //For Delete Product // Added by Prakash on 05 May, 2017
    $scope.DeleteProduct = function (productId, role) {
        var mobj_ProductMaster = {
            ProductId: productId == undefined ? 0 : productId,
            Role: role == undefined ? "" : role,
            DeactivatedBy: parseInt($("#enterdBy").val()),
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

                var ProductDelete = AJService.PostDataToAPI("ProductMaster/DeleteProductSet", mobj_ProductMaster);
                ProductDelete.then(function (msg) {
                    if (msg.data != "OK") {
                        SweetAlert.swal("Not deleted!", "Product linked with Assignment Contract or Product License.", "warning");
                        blockUI.stop();
                    }
                    else {
                        SweetAlert.swal({
                            title: "Deleted!",
                            text: "Your record  has been deleted.",
                            type: "success",
                            confirmButtonText: "OK",
                            closeOnConfirm: true
                        },
                        function (Confirm) {
                            if (Confirm) {
                                blockUI.stop();
                                angular.element(document.getElementById('angularid')).scope().SearchingListResult();
                            }
                        });

                    }
                });


            }

        });
        
    }


    var hid_for_List = $('#hid_for_List').val();
    if (hid_for_List != "" && hid_for_List != null && hid_for_List != undefined) {
        sessionStorage.SeriesId = null;
        angular.element(document.getElementById('angularid')).scope().SearchingListResult();
    }


    if ($('[id*=hid_for_Dashboard]').val() != "" && $('[id*=hid_for_Dashboard]').val() != undefined) {
        if ($('[id*=hid_for_Dashboard]').val().trim().toLowerCase() == "dashboard") {
            sessionStorage.SeriesId = null;
            $scope.SearchingListResult();
        }
    }
    
});