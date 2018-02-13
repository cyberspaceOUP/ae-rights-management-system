/// <reference path="../master/Master.Division.js" />

app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, Scopes) {

    app.expandControllerTopSearch($scope, AJService, $window);

    /*Expand common.master.js Controller*/
    app.expandControllerA($scope, AJService, $window);
    $scope.Productcategory = "";
    /******************** Start DivisionSubDivision Control********************/

    app.expandControllerDivisionSubDivision($scope, AJService, $window);
    /*Mandatory Variable for Division SubDivision Control*/
    $scope.DivisionMandatory = true;
    $scope.SubDivisionMandatory = true;


    $scope.ProductCatCode = "";
    /*Mandatory Variable for Division SubDivision Control*/

    /********************End DivisionSubDivision Control********************/

    //Variable to show OUP Author Suggestion
    $scope.OUPAuthorSuggestionCntrl = true;

    //Variable to show Proprietor div
    $scope.ProprietorDetailsMandatory = true;
    $scope.MultipleProprietor = false;


    /********************Start PublishPubCenter Control********************/

    /*Expand PublishPubCenter Controller*/
    app.expandControllerPublishPubCenter($scope, AJService, $window);
    /*Mandatory Variable for PublishPubCenter Control*/
    $scope.PublishingCompanyMandatory = true;
    $scope.PubCenterMandatory = true;
    $scope.Proprietor = 0;

    $scope.ProductCategoryListEntry = []
    /*Mandatory Variable for PublishPubCenter Control*/

    /********************End PublishPubCenter Control********************/

    /*[Set Value on Update Mode]*/
    $scope.productId = $("input:hidden[id$=hid_ProductId]").val();

    if ($scope.productId > 0) {

        /*Added by dheeraj Kumar sharma*
        //Back to search list option
        */

        $scope.AuthorReq = false;

        $('.backToList').css("display", "block");

        var Product = {
            Id: $scope.productId,
        }

        var ProductDetails = AJService.PostDataToAPI("ProductMaster/GetProductById", Product);
        ProductDetails.then(function (Product) {
            $scope.fillProductDetails(Product);
        }, function () {
            //alert('Error in Product Mastar Details');
        });

        //$scope.Division = $scope.ProductMasterDetails;

        //$scope.ProductModel.ProductCategory = $scope.ProductMasterDetails.ProductCategoryId;
        //$scope.ProductModel.ProductCategory = $scope.ProductMasterDetails.ProductCategoryId;

        //Added by Suranjana on 30/07/2016
        $scope.ProductCodeVisible = true;
    }
    else {

        $scope.AuthorReq = true;
    }
    function convertDate(date) {
        var datearray = date.split("-");
        return datearray[2] + '/' + datearray[1] + '/' + datearray[0];
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

    $scope.setProductTypeId = function (ProductModel) {
        $scope.ProductModel = ProductModel;
    }

    /***************Start function to Fill Product Master  Details *************/
    $scope.fillProductDetails = function (ProductObj) {
        var Product = ProductObj.data.ProductM;
        var Proprietor = ProductObj.data.ProprietorM;
        var ProductAuthor = ProductObj.data.ProductAuthor;
        var ProprietorAuthor = ProductObj.data.ProprietorAuthor;
        var ProductPreviousProduct = ProductObj.data.ProductPreviousProduct;


        var mstr_FinalProductName = ProductObj.data.ProductM.FinalProductName;


        if (mstr_FinalProductName != null)
        {
           // $('[name=ProjectedPublishingDate]').attr("disabled", "disabled"); //commented by prakash on 22 May, 2017

            $('.FInalProductentry').attr("disabled", "disabled");

           
            
        }



        $scope.DivSubDivCntrl = {
            Division: Product.DivisionId,
            SubDivision: Product.SubdivisionId,
        }


        $scope.getSubDivisionbyDivisionIdList();
        $scope.getSeriesbySubDivisionIdList();
        $scope.getSubProductTypeList(Product.ProductTypeId);

        $scope.ProductModel = {
            ProductCategory: Product.ProductCategoryId,
            ProductType: Product.ProductTypeId,
            SubProductType: Product.SubProductTypeId,
            ProductCode: Product.ProductCode,
            ProjectCode: Product.ProjectCode,
            OUPISBN: Product.OUPISBN,
            WorkingProduct: Product.WorkingProduct,
            WorkingSubProduct: Product.WorkingSubProduct,
            OUPEdition: Product.OUPEdition,
            Volume: Product.Volume,
            OUPCopyrightYear: Product.CopyrightYear,
            OUPImprint: Product.ImprintId,
            Language: Product.LanguageId,
            Series: (Product.SeriesId != null ? Product.SeriesId : ""),
            //Derivaties: Product.Derivatives,
            //OrgISBN: Product.OrgISBN,
            ProjectedPublishingDate: (Product.ProjectedPublishingDate != null ? convertDate(Product.ProjectedPublishingDate.split("T")[0]) : ""),
            ProjectedPrice: Product.ProjectedPrice,
            ProjectedCurrency: Product.ProjectedCurrencyId,
            OUPPubCenter: (Product.PubCenterId != null ? Product.PubCenterId : ""),

            //LinkingWithProduct: Product.OrgISBN != null ? "No" : (ProductPreviousProduct.length == 0 ? "No" : "Yes"),
            //PreviousProductISBN: Product.OrgISBN != null ? "No" : (ProductPreviousProduct.length == 0 ? "" : ProductPreviousProduct[0].PreviousISBN),
            LinkingWithProduct: ProductPreviousProduct.length == 0 ? "No" : "Yes",
            PreviousProductISBN: ProductPreviousProduct.length == 0 ? "" : ProductPreviousProduct[0].PreviousISBN,

            ThirdPartyPermission: Product.ThirdPartyPermission,
        }
        //$("input:text[name$=ProjectedPublishingDate]").val(Product.ProjectedPublishingDate);
        setTimeout(function () {
            if ($("select[name*=ProductCategory]").val() == "" && Product.ProductCategoryId != undefined) {
                $("select[name*=ProductCategory]").val(Product.ProductCategoryId);
                $scope.onchnageProductCategoryUpdate();
            }
            if ($('input[type=text][name=PreviousProductISBN]').val() != "") {
                $scope.ValidISBN($('input[type=text][name=PreviousProductISBN]').val())
            }
                //if($('input[type=text][name=OrgISBN]').val()!="")
                //{
                //    $scope.ValidISBN($('input[type=text][name=OrgISBN]').val());
                //}
                //if ($('input[type=text][name=PreviousProductISBN]').val() != "") {
                //    $scope.ValidISBN($('input[type=text][name=PreviousProductISBN]').val());
                //}

            //$("select[name*=Imprint]").val(Product.ImprintId);


        }, 2000);

        $("select[name*=Imprint]").val(Product.ImprintId);
        $scope.ProductCatCode = Product.ProductProductCategory.ProductCategoryCode;

        //if ($scope.ProductCatCode == "OR")
        //{
        //    $scope.Proprietor = 1;
        //}
        //else
        //{
        //    $scope.Proprietor = 0;
        //}
        $scope.onchnageProductCategoryUpdate();


        if (Proprietor != null && Proprietor != "") {
            Scopes.get('ProprietorController').setProprietorControllerScope(Proprietor, ProprietorAuthor);
        }

        if (ProductAuthor != null && ProductAuthor != "") {
            Scopes.get('OUPAuthorSuggestionController').setOUPAuthorSuggestionControllerScope(ProductAuthor);
        }


    }

    /**************End function to Fill Product Master  Details *************/
    $scope.setProductType = function (productType) {
        //alert(productType);
        $scope.ProductModel.ProductType = productType;

    }

    $scope.onchnageProductCategoryUpdate = function () {
        var productcategoryVar = "";
        //$('.ProprietorDetailsMandatory').css("display", "block");
        $scope.Proprietor = 0;

        //if ($scope.ProductCatCode != "") {
        //    productcategoryVar = $scope.ProductCatCode;
        //}
        //else {
        //    $scope.Productcategory = $("[name$=ProductCategory] option:selected").attr("catcode");
        //}
        //else {
        //    productcategoryVar = $("[name$=ProductCategory] option:selected").attr("catcode");
        //}

        if ($("[name$=ProductCategory] option:selected").attr("catcode") == "OR") {
            $scope.reqProductCategory = false;
        }
        else {
            $scope.reqProductCategory = true;
        }

        $(".btn_licence").css("display", "table-row");

        if ($scope.ProductCatCode == "RP") {
            $scope.OUPAuthorSuggestionCntrl = false;
            $scope.AuthorReq = false;
            $('.OUPDetails').find("[name*=SuggestedAuthorName]").removeAttr("required");
        }
        else {

            $scope.OUPAuthorSuggestionCntrl = true;
            $('.OUPDetails').find("[name*=SuggestedAuthorName]").attr("required", "required");
        }

        if ($scope.ProductCatCode == "OR") {
            $(".btn_licence").css("display", "none");
            $(".btn_contract").css("display", "table-row");
            $(".ProprietorDetailsMandatory").css("display", "none");

            $scope.ProprietorDetailsMandatory = false;
            $scope.Proprietor = 1;
            //$('.ProprietorDetailsMandatory').css("display", "none");
        }
        else {
            $(".btn_contract").css("display", "none");
            $(".btn_licence").css("display", "table-row");
            $(".ProprietorDetailsMandatory").css("display", "block");
        }
        if ($scope.ProductCatCode == "CU") {
            $scope.ProprietorDetailsMandatory = true;
            $scope.MultipleProprietor = true;
            Scopes.get('ProprietorController1').setProprietorControllerMandatory($scope.MultipleProprietor);
        }
        else  {
            $scope.ProprietorDetailsMandatory = true;
            $scope.MultipleProprietor = false;
            Scopes.get('ProprietorController1').setProprietorControllerMandatory($scope.MultipleProprietor);
        }
        Scopes.get('ProprietorController').setProprietorControllerMandatory($scope.ProprietorDetailsMandatory);
        Scopes.get('OUPAuthorSuggestionController').setOUPAuthorSuggestionMandatory($scope.OUPAuthorSuggestionCntrl);

        if ($("input:hidden[id$=hid_ProductId]").val() == "") { $scope.AddRemoveValidation(); }
    };


    //if ($("[name$=ProductCategory] option:selected").attr("catcode") == "OR") {
    //    $scope.reqProductCategory = false;
    //}
    //else {
    //    $scope.reqProductCategory = true;
    //}

    /**************Chnage Function of Product Category*******************/
    $scope.onchnageProductCategory = function () {
        var productcategoryVar = "";
        //$('.ProprietorDetailsMandatory').css("display", "block");
        $scope.Proprietor = 0;

        //if ($scope.ProductCatCode != "") {
        //    productcategoryVar = $scope.ProductCatCode;
        //}
        //else {
        //    $scope.Productcategory = $("[name$=ProductCategory] option:selected").attr("catcode");
        //}
        //else {
        //    productcategoryVar = $("[name$=ProductCategory] option:selected").attr("catcode");
        //}

        if ($("[name$=ProductCategory] option:selected").attr("catcode") == "OR") {
            $scope.reqProductCategory = false;
        }
        else {
            $scope.reqProductCategory = true;
        }

        $(".btn_licence").css("display", "table-row");

        if ($("[name$=ProductCategory] option:selected").attr("catcode") == "RP") {
            $scope.OUPAuthorSuggestionCntrl = false;
            $scope.AuthorReq = false;
            $('.OUPDetails').find("[name*=SuggestedAuthorName]").removeAttr("required");
        }
        else {
           // if ($scope.AuthorSuggesationList != null)
            //{
                $scope.OUPAuthorSuggestionCntrl = true;
                $scope.AuthorReq = true;
                //$('.OUPDetails').find("[name*=SuggestedAuthorName]").attr("required", "required");
           // }
        }

        if ($("[name$=ProductCategory] option:selected").attr("catcode") == "OR") {
            $(".btn_licence").css("display", "none");
            $(".btn_contract").css("display", "table-row");
            $(".ProprietorDetailsMandatory").css("display", "none");

            $scope.ProprietorDetailsMandatory = false;
            $scope.Proprietor = 1;
            //$('.ProprietorDetailsMandatory').css("display", "none");
        }
        else {
            $(".btn_contract").css("display", "none");
            $(".btn_licence").css("display", "table-row");
            $(".ProprietorDetailsMandatory").css("display", "block");
        }
        if ($("[name$=ProductCategory] option:selected").attr("catcode") == "CU") {
            $scope.ProprietorDetailsMandatory = true;
            $scope.MultipleProprietor = true;
            Scopes.get('ProprietorController1').setProprietorControllerMandatory($scope.MultipleProprietor);
        }
        else {
            $scope.ProprietorDetailsMandatory = true;
            $scope.MultipleProprietor = false;
            Scopes.get('ProprietorController1').setProprietorControllerMandatory($scope.MultipleProprietor);
        }
        Scopes.get('ProprietorController').setProprietorControllerMandatory($scope.ProprietorDetailsMandatory);
        Scopes.get('OUPAuthorSuggestionController').setOUPAuthorSuggestionMandatory($scope.OUPAuthorSuggestionCntrl);

        if ($("input:hidden[id$=hid_ProductId]").val() == "") { $scope.AddRemoveValidation(); }



    };
    /**************Chnage Function of Product Category*******************/

    /********************Start Get OUP Pub Center List********************/
    // Get Pub Center List by PublishingCompanyId
    //$scope.getPubCenterByCompanyIdList = function () {

    //var PublishingCompany = {
    //    Id: 1,
    //};

    var PubCenternList = AJService.GetDataFromAPI("CommonList/getOUPPubCenterList", null);
    PubCenternList.then(function (PubCenter) {
        $scope.OUPPubCenterList = PubCenter.data;
    }, function () {
        //alert('Error in getting Pub Center List');
    });
    //}
    /********************End Get OUP Pub Center List********************/

    /********************Start Get OUP Imprint List********************/
    //Added by Suranjana on 26/07/2016

    // Get OUP Imprint List
    var getOUPImprintList = AJService.GetDataFromAPI("CommonList/getOUPImprintList", null);
    getOUPImprintList.then(function (Imprint) {
        $scope.OUPImprintList = Imprint.data;
    }, function () {
        //alert('Error in getting Imprint List');
    });
    /********************End Get OUP Pub Center List********************/

    /**************Call Function For Checking Valid ISBN*******************/
    $scope.ValidISBN = function (ISBN) {
        var Product = {
            OUPISBN: ISBN,
        };
        if (ISBN.length == 13) {
            var previousproduct = AJService.PostDataToAPI("ProductMaster/ValidISBN", Product);
            previousproduct.then(function (product) {
                $scope.PreviousProductId = product.data;
                if ($scope.PreviousProductId == 0) {
                    // alert('Error. Invalid ISBN');
                    $scope.Req_ISBNNO = true;
                }
                else {
                    // alert('ISBN valid!');
                    $scope.Req_ISBNNO = false;
                }
            }, function () {
                alert('Error. Invalid ISBN');
            });
        }
        else {
            $scope.PreviousProductId = 0;
        }

    };

    //Added by prakash on 25 April, 2017
    //Autocomplete of product OUPISBN
    $("[name$=PreviousProductISBN]").each(function () {
        var obj = $(this);
 
            var ISBNList = [];

            var getISBNList = AJService.PostDataToAPI("CommonList/GetProductISBNList", null);
            getISBNList.then(function (ISBN) {
                for (i = 0; i < ISBN.data.query.length; i++) {
                    ISBNList[i] = { "label": ISBN.data.query[i].ISBN, "value": ISBN.data.query[i].ISBN, "data": ISBN.data.query[i].Id };
                }

                $(obj).autocomplete({
                    source: function (request, response) {
                        var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
                        response($.grep(ISBNList, function (item) {
                            return matcher.test(item.label);
                        }));
                    },

                    autoFocus: true,
                    select: function (event, ui) {
                        $scope.PreviousProductId = ui.item.data;
                        if ($scope.PreviousProductId == 0) {
                            $scope.Req_ISBNNO = true;
                        }
                        else {
                            $scope.Req_ISBNNO = false;
                            $('#btnSubmit').trigger("click");
                        }

                       
                    }
                });
            }, function () {
                //alert('Error in getting ISBN list');
            });
    });


    /**************Chnage Function of Product Category*******************/

    // Get Product Category Type List
    $scope.getAllProductCategoryListEntry = function () {
        var ProductCategoryList = AJService.GetDataFromAPI("CommonList/getAllProductCategoryList", null);
        ProductCategoryList.then(function (ProductCategory) {

            if ($("#hid_deptCode").val() == "ED") {
                for (var i = 0; i < ProductCategory.data.length; i++) {
                    if (ProductCategory.data[i].ProductCategoryCode == "OR") {
                        $scope.ProductCategoryListEntry.push(ProductCategory.data[i]);
                        setTimeout(function () { $scope.onchnageProductCategory(); }, 1000);
                    }
                }

                $(".btn_licence").css("display", "none");

                $scope.ProprietorDetailsMandatory = false;
                $scope.Proprietor = 1;
            }
            else {
                $scope.ProductCategoryListEntry = ProductCategory.data;

                setTimeout(function () {
                    $('[name*=ProductCategory]').val("");
                }, 100);
            }
        }, function () {
            //alert('Error in getting Product Category List');
        });
    }


    $scope.ValidORGISBN = function (ISBN) {
        var Product = {
            OUPISBN: ISBN,
        };
        if (ISBN.length == 13) {
            var previousproduct = AJService.PostDataToAPI("ProductMaster/ValidISBN", Product);
            previousproduct.then(function (product) {
                if (product.data > 0) {
                    $scope.CheckOrgISBN = product.data;
                }
                else {
                    $scope.userForm.OrgISBN.$setUntouched();
                }

            }, function () {
                alert('Error in Validate ISBN');
            });
        }
        else {
            $scope.PreviousProductId = 0;
        }

    };

    /********************Start Get List for DropDownList********************/

    angular.element(document.getElementById('angularid')).scope().getAllProductCategoryListEntry();
    angular.element(document.getElementById('angularid')).scope().getAllProductTypeList();
    angular.element(document.getElementById('angularid')).scope().getImprintList();
    angular.element(document.getElementById('angularid')).scope().getLanguageList();
    angular.element(document.getElementById('angularid')).scope().getCurrencyList();

    /********************End Get List for DropDownList********************/
    $scope.sub = function (submittype) {
        $("#hid_submit").val(submittype);
    }

    $scope.AddRemoveValidation = function () {

        var obj = $(event.target);

        if (obj.val() != "") {
            obj.closest(".form-group").removeClass("has-error");
            obj.next().find("p").removeClass("ng-show").addClass("ng-hide");
        }
        else {
            obj.closest(".form-group").addClass("has-error");
            obj.next().find("p").addClass("ng-show").removeClass("ng-hide");
        }
    };

    $scope.productEntryForm = function (ProductModel) {
        $scope.submitted = true;

        if ($scope.userForm.$valid) {

            if ($('input[type=radio][name=ProductType]:checked').length != 0) {
                $('input[type=radio][name=ProductType]').closest("div").find('div').find('div').find("p").removeClass("ng-show").addClass("ng-hide");
                $('input[type=radio][name=ProductType]').closest("div").find('div').find('div').removeClass("has-error");
            }
            else {
                $('input[type=radio][name=ProductType]').closest("div").find('div').find('div').find("p").addClass("ng-show").removeClass("ng-hide");
                $('input[type=radio][name=ProductType]').closest("div").find('div').find('div').addClass("has-error");
            }


            if ($("#OUPImprint").val() != "") {

                $("#OUPImprint").closest(".form-group").removeClass("has-error");
                $("#OUPImprint").next().find("p").removeClass("ng-show").addClass("ng-hide");
            }
            else {

                $("#OUPImprint").closest(".form-group").addClass("has-error");
                $("#OUPImprint").next().find("p").addClass("ng-show").removeClass("ng-hide");
            }

            if ($("[name$=ProductCategory] option:selected").attr("catcode") == "OR" && $("[name*=SuggestedAuthorName]:visible").val() == "" && $("input:hidden[id$=hid_ProductId]").val() == "") {
                $("[name*=SuggestedAuthorName]:visible").closest(".form-group").addClass("has-error");
                $("[name*=SuggestedAuthorName]:visible").closest(".form-group").addClass("has-error");
                $("[name*=SuggestedAuthorName]:visible").next().find("p").addClass("ng-show").removeClass("ng-hide");
            }
            if ($("select[name$=ProductCategory]").val() == "") {
                $("select[name$=ProductCategory]").closest(".form-group").addClass("has-error");
                $("select[name$=ProductCategory]").closest(".form-group").addClass("has-error");
                $("select[name$=ProductCategory]").next().find("p").addClass("ng-show").removeClass("ng-hide");
            }

            if ($("input[name$=ProjectedPublishingDate]").val() == "") {
                $("input[name$=ProjectedPublishingDate]").closest(".form-group").addClass("has-error");
                $("input[name$=ProjectedPublishingDate]").closest(".datePicker").next().find("p").addClass("ng-show").removeClass("ng-hide");
            }
            else {
                $("input[name$=ProjectedPublishingDate]").closest(".form-group").removeClass("has-error");
                $("input[name$=ProjectedPublishingDate]").closest(".datePicker").next().find("p").removeClass("ng-show").addClass("ng-hide");
            }

            if ($("form[name*=userForm]").find(".has-error").length > 0) {
                $scope.userForm.$valid = false;
            }
            else {
                $scope.userForm.$valid = true;
            }

            if ($scope.userForm.$valid != true && $scope.userForm.$error.required.length == 1 && $scope.userForm.$error.required[0].$name == "SuggestedAuthorName") {
                $scope.userForm.$valid = true;
            }
        }

        if ($scope.userForm.$valid) {
            if ($scope.userForm.$valid) {
                $scope.productEntry(ProductModel);
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    }

    $scope.productEntry = function () {
       
        var arr_Proprietor;
        var marr_oupAuthor;
        var marr_PreviousProduct;

        var mProdCategory = $('select[name=ProductCategory]').find(":selected").attr("catcode");

        /********Start for Product category is not Indian Original [value=5]****************************/
        if (mProdCategory != "OR") {
            /***get Proprietor Controller Scope Value *********/
            $PropContscope = Scopes.get('ProprietorController').getProprietorControllerScope();
            /***get Proprietor Author List *********/
            var marr_proprAuthor = $PropContscope.AuthorList;

            /***********set Proprietor Details ****************/
            var _mobjProprietorDetails = {
                Id: (typeof ($PropContscope.PrprietorId) !== "undefined" ? $PropContscope.PrprietorId : null),
                ProprietorISBN: (typeof ($PropContscope.ProprDetailCntrl.ProprietorISBN) !== "undefined" ? $PropContscope.ProprDetailCntrl.ProprietorISBN : null),
                ProprietorProduct: (typeof ($PropContscope.ProprDetailCntrl.ProprietorProduct) !== "undefined" ? $PropContscope.ProprDetailCntrl.ProprietorProduct : null),
                ProprietorEdition: (typeof ($PropContscope.ProprDetailCntrl.ProprietorEdition) !== "undefined" ? $PropContscope.ProprDetailCntrl.ProprietorEdition : null),
                ProprietorCopyrightYear: (typeof ($PropContscope.ProprDetailCntrl.ProprietorCopyrightYear) !== "undefined" ? $PropContscope.ProprDetailCntrl.ProprietorCopyrightYear : null),
                PublishingCompanyId: (typeof ($PropContscope.PublishPubCntrl.PublishingCompany) !== "undefined" ? $PropContscope.PublishPubCntrl.PublishingCompany : null),
                ProprietorPubCenterId: (typeof ($PropContscope.PublishPubCntrl.PubCenter) !== "undefined" ? $PropContscope.PublishPubCntrl.PubCenter : null),
                ProprietorImPrintId: (typeof ($PropContscope.ProprDetailCntrl.ProprietorImprint) !== "undefined" ? $PropContscope.ProprDetailCntrl.ProprietorImprint : null),
                //ProprietorAuthorLink: marr_proprAuthor,
                ProprietorAuthorName: (typeof ($PropContscope.ProprDetailCntrl.ProprietorAuthorName) !== "undefined" ? $PropContscope.ProprDetailCntrl.ProprietorAuthorName : null),
            }
            arr_Proprietor = [_mobjProprietorDetails];

            if (mProdCategory == "CU") {
                /***get Proprietor Controller Scope Value *********/
                $PropContscope1 = Scopes.get('ProprietorController1').getProprietorControllerScope();
                /***get Proprietor Author List *********/
                var marr_proprAuthor1 = $PropContscope1.AuthorList;

                /***********set Proprietor Details ****************/
                var _mobjProprietorDetails1 = {
                    Id: (typeof ($PropContscope1.PrprietorId) !== "undefined" ? $PropContscope1.PrprietorId : null),
                    ProprietorISBN: (typeof ($PropContscope1.ProprDetailCntrl1.ProprietorISBN1) !== "undefined" ? $PropContscope1.ProprDetailCntrl1.ProprietorISBN1 : null),
                    ProprietorProduct: (typeof ($PropContscope1.ProprDetailCntrl1.ProprietorProduct1) !== "undefined" ? $PropContscope1.ProprDetailCntrl1.ProprietorProduct1 : null),
                    ProprietorEdition: (typeof ($PropContscope1.ProprDetailCntrl1.ProprietorEdition1) !== "undefined" ? $PropContscope1.ProprDetailCntrl1.ProprietorEdition1 : null),
                    ProprietorCopyrightYear: (typeof ($PropContscope1.ProprDetailCntrl1.ProprietorCopyrightYear1) !== "undefined" ? $PropContscope1.ProprDetailCntrl1.ProprietorCopyrightYear1 : null),
                    PublishingCompanyId: (typeof ($PropContscope1.ProprDetailCntrl1.PublishingCompany1) !== "undefined" ? $PropContscope1.ProprDetailCntrl1.PublishingCompany1 : null),
                    ProprietorPubCenterId: (typeof ($PropContscope1.ProprDetailCntrl1.PubCenter1) !== "undefined" ? $PropContscope1.ProprDetailCntrl1.PubCenter1 : null),
                    ProprietorImPrintId: (typeof ($PropContscope1.ProprDetailCntrl1.ProprietorImprint1) !== "undefined" ? $PropContscope1.ProprDetailCntrl1.ProprietorImprint1 : null),
                    //ProprietorAuthorLink: marr_proprAuthor1,
                    ProprietorAuthorName: (typeof ($PropContscope1.ProprDetailCntrl1.ProprietorAuthorName1) !== "undefined" ? $PropContscope1.ProprDetailCntrl1.ProprietorAuthorName1 : null),
                }
                arr_Proprietor.push(_mobjProprietorDetails1);
            }

        }
        /********End for Product category is Indian Original [value=5]****************************/

        /********Start for Product category is Indian Original [value=5]****************************/
        if (mProdCategory !== "RP") {
            /***get OUP Author Suggestion Controller Scope Value *********/
            $OUPContscope = Scopes.get('OUPAuthorSuggestionController').getOUPAuthorControllerScope();
            /***get OUP Author List *********/
            marr_oupAuthor = $OUPContscope.AuthorList;
        }
        /********End for Product category is Indian Original [value=5]****************************/

        /********Start for For Product Previous Link****************************/
        //$('input[type=radio][name=Derivaties]:checked').val() == "Yes" ||
        if ($('input[type=radio][name=LinkingWithProduct]:checked').val() == "Yes") {
            if ($scope.PreviousProductId != null && $scope.PreviousProductId != "" && $scope.PreviousProductId >= 0) {
                var _mobjProductPreviousProductLink = {
                    PreviousProductId: $scope.PreviousProductId,
                }
                marr_PreviousProduct = [_mobjProductPreviousProductLink];
            }
        }
        /********End for Product category is Indian Original [value=5]****************************/

        var _mobjProduct = {
            Id: $scope.productId,
            DivisionId: $scope.DivSubDivCntrl.Division,
            SubdivisionId: $scope.DivSubDivCntrl.SubDivision,
            ProductCategoryId: $scope.ProductModel.ProductCategory,
            ProductTypeId: $scope.ProductModel.ProductType,
            SubProductTypeId: $scope.ProductModel.SubProductType,
            ProjectCode: $scope.ProductModel.ProjectCode,
            OUPISBN: $scope.ProductModel.OUPISBN,
            WorkingProduct: $scope.ProductModel.WorkingProduct,
            WorkingSubProduct: $scope.ProductModel.WorkingSubProduct,
            OUPEdition: $scope.ProductModel.OUPEdition,
            Volume: $scope.ProductModel.Volume,
            CopyrightYear: $("input[name*=OUPCopyrightYear]").val(),
            ImprintId: $("select[name$=OUPImprint]").val(),
            LanguageId: $("select[name$=Language]").val(),
            SeriesId: $scope.ProductModel.Series,
            Derivatives: 'No', //$scope.ProductModel.Derivaties,
            OrgISBN: null, //($scope.ProductModel.Derivaties == "Yes" ? $scope.ProductModel.OrgISBN : null),
            ProjectedPublishingDate: convertDateMDY($('.datePicker').find("input:text").val()),
            ProjectedPrice: $scope.ProductModel.ProjectedPrice,
            ProjectedCurrencyId: $("#ProjectedCurrency").val(),
            PubCenterId: $scope.ProductModel.OUPPubCenter,
            ProductProprietorMaster: arr_Proprietor,
            ProductProductAuthorLink: marr_oupAuthor,
            ProductPreviousProductLink: marr_PreviousProduct,
            EnteredBy: $("#enterdBy").val(),
            ThirdPartyPermission: $scope.ProductModel.ThirdPartyPermission,
        };
        $("#hid_ProductTypeId").val($scope.ProductModel.ProductType);


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




                     if ($scope.productId > 0) {
                         var ProductStatus = AJService.PostDataToAPI('ProductMaster/UpdateProduct', _mobjProduct);

                     }
                     else {
                         var ProductStatus = AJService.PostDataToAPI('ProductMaster/InsertProduct', _mobjProduct);
                     }


                     ProductStatus.then(function (msg) {

                         if (msg.data.status == "Duplicate") {
                             SweetAlert.swal("Duplicate!", "Already exist !", "warning");
                         }
                         else if (msg.data.status == "DuplicateISBN") {
                             SweetAlert.swal("Duplicate!", "ISBN already exist !", "warning");
                         }
                         else if (msg.data.status == "NoAuthorCode") {
                             SweetAlert.swal("", "Linked Product does not have any Contract or License !", "warning");
                         }
                         else if (msg.data.status != "OK") {
                             SweetAlert.swal("Error!", "There is some problem. !", "error");
                         }
                         else {
                             if ($scope.productId > 0) {
                                 $("#hid_ProductId").val($scope.productId);
                                 SweetAlert.swal({
                                     title: "Done",
                                     text: "Updated successfully.",
                                     type: "success"
                                 },
                                 function () {
                                     if ($("#hid_submit").val() == "2") {
                                         document.location = GlobalredirectPath + "Contract/AuthorContract/Index?ProductId=" + $("#hid_ProductId").val();
                                     }
                                     else
                                         if ($("#hid_submit").val() == "3") {
                                             document.location = GlobalredirectPath + "Product/ProductLicense/ProductLicense?Id=" + $("#hid_ProductId").val() + "&typeId=" + $("#hid_ProductTypeId").val();
                                         }
                                         else {
                                             location.href = "../../Product/ProductMaster/ProductDetailsView?Id=" + $("#hid_ProductId").val();
                                         }
                                 });
                             }
                             else {
                                 $("#hid_ProductId").val(msg.data.Id);
                                 SweetAlert.swal({
                                     title: "Done",
                                     text: "Insert successfully.",
                                     type: "success"
                                 },
                                 function () {

                                     if ($("#hid_submit").val() == "2") {
                                         document.location = GlobalredirectPath + "Contract/AuthorContract/Index?ProductId=" + $("#hid_ProductId").val();
                                     }
                                     else
                                         if ($("#hid_submit").val() == "3") {
                                             document.location = GlobalredirectPath + "Product/ProductLicense/ProductLicense?Id=" + $("#hid_ProductId").val() + "&typeId=" + $("#hid_ProductTypeId").val();
                                         }
                                         else {
                                             location.href = "../../Product/ProductMaster/ProductDetailsView?Id=" + msg.data.Id;
                                         }

                                 });
                             }
                         }

                     },
                     function () {
                         alert('Please validate details');
                     });

                 }

             });

    }

    $scope.setProductCategoryId = function (ProductModel, ProductCategory) {
        if (ProductModel == undefined) {
            if (ProductCategory.ProductCategoryCode == "OR") {
                $scope.ProductModel = {
                    ProductCategory: ProductCategory.Id
                }

            }
        }
    }

    $scope.setLanguageId = function (ProductModel, Language) {
        if (ProductModel == undefined) {
            if (Language.LanguageCode == "ENG") {
                $scope.ProductModel = {
                    Language: Language.Id
                }


            }
        }
    }

    $scope.SetCopyrightDate = function (datetext) {
      
        var date = $(datetext).val() == "" ? $scope.ProductModel.ProjectedPublishingDate : $(datetext).val();
        var d;
        if (navigator.userAgent.search("Safari") >= 0 && navigator.userAgent.search("Chrome") < 0) {
            d = new Date(parseInt(date.split("/")[2]), parseInt(date.split("/")[1] - 1), parseInt(date.split("/")[0]));
        }
        else {
            d = new Date(date.split("/").reverse().join("-"));
        }

        var dd = d.getDate();
        var mm = d.getMonth() + 1;
        var yy = d.getFullYear();
        
        if (dd >= 1 && mm >= 7)
        {
            yy = yy + 1
        }
      
        setTimeout(function () {
            $('[name=OUPCopyrightYear]').val(yy);
        }, 100)


        if ($(datetext).val() != "") {
            $scope.ProductModel.ProjectedPublishingDate = date;
            $(this).closest(".form-group").removeClass("has-error");
            $(this).closest(".datePicker").next().find("p").removeClass("ng-show").addClass("ng-hide");
            
            if ($(datetext).val() == "") {
                $(datetext).closest(".form-group").addClass("has-error");
                $(datetext).closest(".datePicker").next().find("p").addClass("ng-show").removeClass("ng-hide");
            }
            else {
                $(datetext).closest(".form-group").removeClass("has-error");
                $(datetext).closest(".datePicker").next().find("p").removeClass("ng-show").addClass("ng-hide");
            }
        }
        else {
            $(".datePicker").datepicker("setDate", date);
        }

    }

    //Added by Suranjana on 30/07/2016
    $scope.setOUPImprintId = function (ProductModel, Imprint) {
        if (ProductModel == undefined) {
            if (Imprint.ImprintName == "Oxford University Press") {
                $scope.ProductModel = {
                    OUPImprint: Imprint.Id
                }
            }
        }
    }

    $scope.btn_BackToList = function () {
        document.location = GlobalredirectPath + "/Product/ProductMaster/ProductSearch?For=List";
    }


});


/******************** Start Controller For Proprietor Control********************/
app.controller('ProprietorController', function ($scope, AJService, $window, $compile, SweetAlert, blockUI, Scopes) {
    $scope.ProprietorISBN = false;
    $scope.ProprietorProduct = false;
    $scope.ProprietorEdition = false;
    $scope.ProprietorCopyrightYear = false;
    $scope.ProprietorImprint = false;
    $scope.ProprietorImprint = false;
    $scope.PublishingCompanyMandatory = false;
    $scope.PubCenterMandatory = false;
    /*Expand ProprietorDetails Controller*/
    app.expandControllerProprietorDetails($scope, AJService, $window);

    Scopes.store('ProprietorController', $scope);
    $scope.getProprietorControllerScope = function () {
        return $scope;
    };

    $scope.setProprietorControllerMandatory = function (mandatory) {
        if (mandatory) {
            $scope.ProprietorISBN = true;
            $scope.ProprietorProduct = true;
            $scope.ProprietorEdition = true;
            $scope.ProprietorCopyrightYear = true;
            $scope.ProprietorImprint = true;
            $scope.ProprietorImprint = true;
            $scope.PublishingCompanyMandatory = true;
            $scope.PubCenterMandatory = true;
        }
        else {
            $scope.ProprietorISBN = false;
            $scope.ProprietorProduct = false;
            $scope.ProprietorEdition = false;
            $scope.ProprietorCopyrightYear = false;
            $scope.ProprietorImprint = false;
            $scope.ProprietorImprint = false;
            $scope.PublishingCompanyMandatory = false;
            $scope.PubCenterMandatory = false;
        }
    };


    $scope.setProprietorControllerScope = function (Proprietor, ProprietorAuthor) {
        $scope.ProprDetailCntrl = {
            ProprietorISBN: Proprietor.ProprietorISBN,
            ProprietorProduct: Proprietor.ProprietorProduct,
            ProprietorEdition: Proprietor.ProprietorEdition,
            ProprietorCopyrightYear: Proprietor.ProprietorCopyrightYear,
            ProprietorImprint: Proprietor.ProprietorImPrintId,
            ProprietorAuthorName: Proprietor.ProprietorAuthorName,
        }
        $scope.PublishPubCntrl = {
            PublishingCompany: Proprietor.PublishingCompanyId,
            PubCenter: Proprietor.ProprietorPubCenterId,
        }
        $scope.PrprietorId = Proprietor.Id;

        $scope.AuthorList = ProprietorAuthor;
        $scope.getPubCenterByCompanyIdList(); 
        $scope.getImprintByCompanyIdList();
        setTimeout(function () { $($("select[name*=Imprint]")[0]).val(Proprietor.ProprietorImPrintId) }, 1000);

    };

});
/******************** End Controller For Proprietor Control********************/


/******************** Start Controller For Proprietor Control********************/
app.controller('ProprietorController1', function ($scope, AJService, $window, $compile, SweetAlert, blockUI, Scopes) {
    //angular.element(document.getElementById('angularid')).scope().getImprintList();
    angular.element(document.getElementById('angularid')).scope().getImprintListForProprietorDetails();
    angular.element(document.getElementById('angularid')).scope().getLanguageList();
    angular.element(document.getElementById('angularid')).scope().getCurrencyList();

    $scope.ProprietorISBN = false;
    $scope.ProprietorProduct = false;
    $scope.ProprietorEdition = false;
    $scope.ProprietorCopyrightYear = false;
    $scope.ProprietorImprint = false;
    $scope.ProprietorImprint = false;
    $scope.PublishingCompanyMandatory = false;
    $scope.PubCenterMandatory = false;

    ///*Expand PublishPubCenter Controller*/
    app.expandControllerPublishPubCenter1($scope, AJService, $window);
    ///*Expand AuthorSuggestion Controller*/
    app.expandControllerAuthorSuggestion1($scope, AJService, $window);

    Scopes.store('ProprietorController1', $scope);
    $scope.getProprietorControllerScope = function () {
        return $scope;
    };

    $scope.setProprietorControllerMandatory = function (mandatory) {
        if (mandatory) {
            $scope.ProprietorISBN = true;
            $scope.ProprietorProduct = true;
            $scope.ProprietorEdition = true;
            $scope.ProprietorCopyrightYear = true;
            $scope.ProprietorImprint = true;
            $scope.ProprietorImprint = true;
            $scope.PublishingCompanyMandatory = true;
            $scope.PubCenterMandatory = true;
        }
        else {
            $scope.ProprietorISBN = false;
            $scope.ProprietorProduct = false;
            $scope.ProprietorEdition = false;
            $scope.ProprietorCopyrightYear = false;
            $scope.ProprietorImprint = false;
            $scope.ProprietorImprint = false;
            $scope.PublishingCompanyMandatory = false;
            $scope.PubCenterMandatory = false;
        }
    };


    $scope.setProprietorControllerScope = function (Proprietor, ProprietorAuthor) {
        $scope.ProprDetailCntrl = {
            ProprietorISBN: Proprietor.ProprietorISBN,
            ProprietorProduct: Proprietor.ProprietorProduct,
            ProprietorEdition: Proprietor.ProprietorEdition,
            ProprietorCopyrightYear: Proprietor.ProprietorCopyrightYear,
            ProprietorImprint: Proprietor.ProprietorImPrintId,
            //ProprietorAuthorName: Proprietor.ProprietorAuthorName,
        }
        $scope.PublishPubCntrl = {
            PublishingCompany: Proprietor.PublishingCompanyId,
            PubCenter: Proprietor.ProprietorPubCenterId,
        }
        $scope.PrprietorId = Proprietor.Id;

        $scope.AuthorList = ProprietorAuthor;
        $scope.getPubCenterByCompanyIdList(); 
        $scope.getImprintByCompanyIdList();


    };

});
/******************** End Controller For Proprietor Control********************/

app.expandControllerAuthorSuggestion1 = function ($scope, AJService, $window) {
    // Get Author Suggestion List

    $scope.AuthorList = [];

    $scope.getAuthorSuggesationList = function () {
        var Author = {
            FirstName: $scope.SuggestedAuthorName,
            Type: $scope.AuthorCategory,
            // AuthorId:$scope.AuthorId
        };

        if (typeof $scope.SuggestedAuthorName !== "undefined" && $scope.SuggestedAuthorName.length > 3) {
            var AuthorSuggesationList = AJService.PostDataToAPI("CommonList/AuthorSuggesationList", Author);
            AuthorSuggesationList.then(function (AuthorSuggesationList) {
                $scope.AuthorSuggesationList = AuthorSuggesationList.data.AuthorSuggesation;
                $scope.Authortest = AuthorSuggesationList.data.AuthorSuggesation[0];

                if ($scope.AuthorSuggesationList.length == 0) {
                    alert('No Author found for enter search character');
                }
            }, function () {
                alert('Error in getting Author Suggesation List');
            });
        }
        else {
            $scope.AuthorSuggesationList = null;
            alert('Please enter atleast 3 character to search author');
        }
    }

    $scope.showCheckedAuthor = function (auth, authorName) {
        if (authorName) {
            $scope.AuthorList.push(auth);
        }
        else {
            var index = $scope.AuthorList.indexOf(auth);
            $scope.AuthorList.splice(index, 1);
            $scope.AuthorName = false;
        }
    }

    $scope.CheckboxBind = function (AuthorId) {

        var index = $scope.AuthorList.map(function (item) {
            return item.AuthorId;
        }).indexOf(AuthorId);

        if (index > -1) {
            return true;
        }
        else {
            return false;
        }
    }

}

app.expandControllerPublishPubCenter1 = function ($scope, AJService, $window) {

    app.expandControllerPublishPubCenterAB($scope, AJService, $window);
    angular.element(document.getElementById('angularid')).scope().getPublishingCompanyList();

}


app.expandControllerPublishPubCenterAB = function ($scope, AJService, $window) {
    // Get Pub Center List by PublishingCompanyId
    $scope.getPubCenterByCompanyIdList = function () {

        var PublishingCompany = {
            Id: $scope.ProprDetailCntrl1.PublishingCompany1,
        };
        alert(PublishingCompany.Id)
        var PubCenternList = AJService.PostDataToAPI("CommonList/PubCenterByCompanyIdList", PublishingCompany);
        PubCenternList.then(function (PubCenter) {
            $scope.PubCenterList = PubCenter.data;
        }, function () {
            //alert('Error in getting Pub Center List');
        });
    }

    //Added by Prakash for Imprint List on 08/11/2016
    $scope.getImprintByCompanyIdList = function () {
        var PublishingCompany = {
            Id: $scope.PublishPubCntrl.PublishingCompany,
        };
        
        var getImprintList = AJService.PostDataToAPI("CommonList/GetImprintListByPublishingCompany", PublishingCompany);
        getImprintList.then(function (Imprint) {
            $scope.Imprint_List = Imprint.data;
        }, function () {
            //alert('Error in getting Imprint List');
        });
    }

}


/******************** Start Controller For OUPAuthorSuggestion Control********************/
app.controller('OUPAuthorSuggestionController', function ($scope, AJService, $window, $compile, SweetAlert, blockUI, Scopes) {
    /********************Start AuthorSuggestion Control********************/
    $scope.AuthorListMandatory = false;
    /*Expand AuthorSuggestion Controller*/
    app.expandControllerAuthorSuggestion($scope, AJService, $window);
    // Get Author Suggestion List

    /********************End AuthorSuggestion Control********************/

    Scopes.store('OUPAuthorSuggestionController', $scope);

    $scope.getOUPAuthorControllerScope = function () {
        //Scopes.store('OUPAuthorSuggestionController', $scope);
        return $scope;
    };

    $scope.setOUPAuthorSuggestionControllerScope = function (ProductAuthorList) {
        $scope.AuthorList = ProductAuthorList;
    };

    $scope.setOUPAuthorSuggestionMandatory = function (Mandatory) {
        if (Mandatory) {
            $scope.AuthorListMandatory = true;
        }
        else {
            $scope.AuthorListMandatory = false;
        }
    };
    
});
/******************** End Controller For OUPAuthorSuggestion Control********************/

app.factory('Scopes', function ($rootScope) {
    var mem = {};

    return {
        store: function (key, value) {
            $rootScope.$emit('scope.stored', key);
            mem[key] = value;
        },
        get: function (key) {
            return mem[key];
        }
    };
    
});

























