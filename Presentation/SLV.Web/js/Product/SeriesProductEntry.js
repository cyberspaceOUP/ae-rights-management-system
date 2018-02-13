app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI, $parse) {


    app.expandControllerTopSearch($scope, AJService, $window);

    /*Expand common.master.js Controller*/
    app.expandControllerA($scope, AJService, $window);

    /*Calling JS of DivisionSubDivision partial view */
    app.expandControllerDivisionSubDivision($scope, AJService, $window);
    /*Mandatory Variable for Division SubDivision Control*/
    $scope.DivisionMandatory = true;
    $scope.SubDivisionMandatory = true;


    /************************************************DropDown bindings*******************************************************************************/
    angular.element(document.getElementById('angularid')).scope().getAllProductTypeList();
    angular.element(document.getElementById('angularid')).scope().getImprintListForProprietorDetails();
    angular.element(document.getElementById('angularid')).scope().getLanguageList();

    //Added by Prakash for Currency List on 09/11/2016
    angular.element(document.getElementById('angularid')).scope().getCurrencyList();

    
    //$.ajax({
    //    url: getLanguageList(),
    //    success: function () {
    //        $('#SelectLanguage').click();
    //    }
    //})

    $scope.AuthorValue = [];
    var authorids = [];

    $scope.WorkingProductReq = true;
    $scope.ProjectPublishingDateReq = true; 
    $scope.OUPImprintReq = true;
    $scope.LanguageReq = true;
    $scope.DerivativesReq = true;
    $scope.AuthorCatagoryReq = true;
        
    //$scope.WorkingSubProductReq = true;
    $scope.OUPEditionReq = true;

    $scope.thirdpartypermissionReq = true;


    $scope.getAllProductTypeList = function () {

        var ProductTypeList = AJService.PostDataToAPI("CommonList/AllProductTypeList", null);
        ProductTypeList.then(function (ProductTypeList) {
            $scope.ProductTypeList = ProductTypeList.data;
        }, function () {
            //alert('Error in getting Product Type List');
        });
    }

    $scope.setProductTypeId = function (ProductModel) {
        $scope.ProductModel = ProductModel;

        if ($('input[type=radio][name=ProductType]:checked').length != 0) {
            $('input[type=radio][name=ProductType]').closest("div").find('div').find('div').find("p").removeClass("ng-show").addClass("ng-hide");
            $('input[type=radio][name=ProductType]').closest("div").find('div').find('div').removeClass("has-error");
        }
        else {
            $('input[type=radio][name=ProductType]').closest("div").find('div').find('div').find("p").addClass("ng-show").removeClass("ng-hide");
            $('input[type=radio][name=ProductType]').closest("div").find('div').find('div').addClass("has-error");
        }

        $('select[name=SubProductType]').val('');
    }

    /************************************************DropDown binding Ended here*********************************************************************/

    /*******************************************************Get Product Category Type List************************************************************/
    $scope.getAllProductCategoryListEntry = function () {
        $scope.ProductCategoryListEntry = [];
        var ProductCategoryList = AJService.GetDataFromAPI("CommonList/getAllProductCategoryList", null);
        ProductCategoryList.then(function (ProductCategory) {

            //if ($("#hid_deptCode").val() == "ED") {
            for (var i = 0; i < ProductCategory.data.length; i++) {
                if (ProductCategory.data[i].ProductCategoryCode == "OR") {
                    $scope.ProductCategoryListEntry.push(ProductCategory.data[i]);
                    //$scope.ProductCategory = ProductCategory.data[i];
                }
            }
            //}
            //else {

            // $scope.ProductCategoryListEntry = ProductCategory.data;

            //setTimeout(function () {
            //    $('[name*=ProductCategory]').val("");
            //}, 100);
            //}
        }, function () {
            //alert('Error in getting Product Category List');
        });
    }

    //$scope.getAllProductCategoryListEntry = function () {
    //    var ProductCategoryList = AJService.GetDataFromAPI("CommonList/getAllProductCategoryList", null);
    //    ProductCategoryList.then(function (ProductCategory) {
    //        alert($("#hid_deptCode").val());
    //        if ($("#hid_deptCode").val() == "ED") {
    //            for (var i = 0; i < ProductCategory.data.length; i++) {
    //                if (ProductCategory.data[i].ProductCategoryCode == "OR") {
    //                    $scope.ProductCategoryListEntry.push(ProductCategory.data[i]);
    //                }
    //            }

    //            //$(".btn_licence").css("display", "none");

    //            //$scope.ProprietorDetailsMandatory = false;
    //            //$scope.Proprietor = 1;
    //        }
    //        else {
    //            $scope.ProductCategoryListEntry = ProductCategory.data;

    //            setTimeout(function () {
    //                $('[name*=ProductCategory]').val("");
    //            }, 100);
    //        }
    //    }, function () {
    //        alert('Error in getting Product Category List');
    //    });
    //}
    /**************************************************Get Product Category Type List Ended Here**************************************************/


    /*******************************************************Bind Imprint List*********************************************************************/
    var getOUPImprintList = AJService.GetDataFromAPI("CommonList/getOUPImprintList", null);
    getOUPImprintList.then(function (Imprint) {
        $scope.OUPImprintList = Imprint.data;
    }, function () {
        //alert('Error in getting Imprint List');
    });
    /********************************************************End Binding of imprint list**********************************************************/


    /*********************************************************Get Author Suggestion List**********************************************************/
    $scope.AuthorList = [];
    $scope.SuggestedAuthorName = [];
    $scope.AuthorCategory = [];
    $scope.AuthorSuggesationList = [];
    $scope.getAuthorSuggesationList = function (_textval, index, _type) {
        //for (var i = 0; i < $scope.SeriesListControl.length; i++) {
        var Author = [];
        if (_textval != undefined && _textval != "") {


            Author = {
                FirstName: _textval,
                Type: _type,
                // AuthorId:$scope.AuthorId
            };
        }
        else {
            Author = {
                FirstName: $("#hid_suggestedauthorname").val(),
                Type: $("#hid_suggestedauthortype").val(),
                // AuthorId:$scope.AuthorId
            };
        }


        //}
        var AuthorSuggesationList = AJService.PostDataToAPI("CommonList/AuthorSuggesationList", Author);
        AuthorSuggesationList.then(function (AuthorSuggesationList) {
            //$scope.AuthorSuggesationList = AuthorSuggesationList.data.AuthorSuggesation;
            if (AuthorSuggesationList.data.AuthorSuggesation.length > 0) {
                $scope.AuthorSuggesationList[index] = AuthorSuggesationList.data.AuthorSuggesation;
            }
            else {
                $scope.AuthorSuggesationList = [];
                //alert('No Author found. Please enter correct author name!');
                SweetAlert.swal("", "No Author found. Please enter correct author name!", "", "");
            }
            // $scope.Authortest = AuthorSuggesationList.data.AuthorSuggesation[0];

            //if ($scope.AuthorSuggesationList.length == 0) {
            //    alert('No Author found. Please enter correct author name!');
            //}
        }, function () {
            //alert('Error in getting Author Suggesation List');
        });

    }

    $scope.CompleteList = [];
    // $scope.AuthorList = [];

    //$scope.CheckboxBind = function () {


    //displaytable
    //clonetr


    //if (authorName) {
    //    $scope.AuthorList.push(auth);
    //    $scope.CompleteList[index] = $scope.AuthorList;
    //}
    //else {
    //    var index = $scope.AuthorList.indexOf(auth);
    //    $scope.AuthorList.splice(index, 1);
    //    $scope.AuthorName = false;
    //}


    //}

    //$scope.CheckboxBind = function (AuthorId, index) {

    //    var index = $scope.AuthorList.map(function (item) {
    //        return item.AuthorId;
    //    }).indexOf(AuthorId);

    //    if (index > -1) {
    //        return true;
    //    }
    //    else {
    //        return false;
    //    }
    //}
    /***********************************************************Get Author Suggestion List Ended Here************************************************/

    /***********************************************************Add dynamic control series control***************************************************/
    $scope.SeriesListControl = [];
    for (var i = 0; i < 1; i++) {
        var j = $scope.SeriesListControl.length;
        $scope.SeriesListControl.push(j);
    }




    $scope.AddMoreRow = function (obj) {
        var k = $scope.SeriesListControl.length;
        // debugger;
        if (k >= 1) {
            $("[id$='anch_seriesremove']").css({ 'display': 'block' });

            // $('.SeriesAddLink').css("display:none");

            //$(this).find('.SeriesAddLink').css("display", "none");
            setTimeout(function () { $('#TblOwnerList tbody .seriestr').not(':last').find(".SeriesAddLink").css({ 'display': 'none' }); }, 100);
            ///  $('#TblOwnerList tbody .seriestr').not(':last').find(".SeriesAddLink").css({ 'display': 'none' });
            //return false;
            for (var i = k; i < k + 1; i++) {

                //  $('#SeriesAddLink_'+k+'').css("display", "none");

                var j = $scope.SeriesListControl.length;
                $scope.SeriesListControl.push(j);
            }

        }

        setTimeout(function () {
            if ($("#SelectAuthorcategory").is(':checked')) {
                $('input[type=radio][name*=AuthorCategory]').each(function () {
                    $(this).attr('disabled', 'disabled');
                })
            }
        }, 300)

        //var newItemNo = $scope.SeriesListControl.length + 2;
        //$scope.SeriesListControl.push(newItemNo);

        //$scope.add_removeButton();
    };


    $scope.RemoveRow = function () {
               
        var lastItem = $scope.SeriesListControl.length - 1;
        $scope.SeriesListControl.splice(lastItem);
        if ($scope.SeriesListControl.length == 1) {

            $("[id$='anch_seriesremove']").css({ 'display': 'none' });
            $("[id$='anch_seriesadd']").css({ 'display': 'block' });
        }

        //$scope.add_removeButton();
    };

    //$scope.add_removeButton = function () {
    //    $('.panel_box_row').each(function () {
    //        $(this).find(".SeriesDetail").find("tr").find(".SeriesAddLink").css("display", "none");
    //        $(this).find(".SeriesDetail").find("tr").find(".SeriesRemoveLink").css("display", "table-row");
    //        $(this).find(".SeriesDetail").find("tr:last").find(".SeriesAddLink").css("display", "table-row");
    //        $(this).find(".SeriesDetail").find("tr:last").find(".SeriesRemoveLink").css("display", "none");
    //        //$(this).find(".SeriesDetail").find("tr:last").find('input[name$=CopiesTo]').val("");
    //        $(this).find(".SeriesDetail").find("tr").not("tr:first").each(function (Index, value) {
    //            $($(this).find("td")[0]).html(Index + 1);
    //        })

    //    });
    //}

    /***********************************************************Add dynamic control series control ended here******************************************/


    /***********************************************************Insert Series Details******************************************************************/

    $scope.sub = function (submittype) {
        $("#hid_submit").val(submittype);
    }

    $scope.SeriesEntryForm = function () {
       
        $scope.validatSeriesDetail();

        //if ($('input[type=radio][name=ProductType]:checked').length != 0) {
        //    $('input[type=radio][name=ProductType]').closest("div").find('div').find('div').find("p").removeClass("ng-show").addClass("ng-hide");
        //    $('input[type=radio][name=ProductType]').closest("div").find('div').find('div').removeClass("has-error");
        //}
        //else {
        //    $('input[type=radio][name=ProductType]').closest("div").find('div').find('div').find("p").addClass("ng-show").removeClass("ng-hide");
        //    $('input[type=radio][name=ProductType]').closest("div").find('div').find('div').addClass("has-error");
        //}

        $scope.submitted = true;

        if ($scope.userForm.$valid != true && $scope.userForm.$error.required.length == 1) {
            $scope.userForm.$valid = true;
        }
        else {
            if ($scope.userForm.$error.required != undefined) {
                for (var i = 0; i < $scope.userForm.$error.required.length; i++) {
                    if ($scope.userForm.$error.required[i].$name == "ProductType") {
                        $scope.userForm.$valid = true;
                    }
                    else {
                        $scope.userForm.$valid = false;
                        return false;
                    }
                }
            }
        }

        if ($scope.userForm.$valid) {
            if ($scope.userForm.$valid) {
                $scope.seriesEntry();
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    }

    $scope.OUPISBN = [];
    $scope.WorkingProduct = [];
    $scope.WorkingSubProduct = [];
    $scope.OUPEdition = [];
    $scope.CopyrightYear = [];
    $scope.OUPImprint = [];
    $scope.Language = [];
    $scope.Derivaties = [];
    $scope.OrgISBNTiTle = [];

    $scope.ProjectedPrice = [];
    $scope.ProjectedCurrency = [];

    $scope.seriesEntry = function () {

        var obj = [];
        $scope.authorobject = [];
        // var authorids = [];

        var DuplicateISBNno = [];
        $("input[name$=OUPISBN]").each(function (index, values) {
            DuplicateISBNno[index] =
        {
            OUPISBNnos: $(this).val(),
        };
        });

        for (var i = 0; i < $scope.SeriesListControl.length; i++) {

            $("#fulltr_" + i + "").find(".displaytable").each(function () {
                authorids = [];
                $(this).find("tr").not("first").each(function (index) {

                    if ($($(this).find("td")[0]).attr("authorid") != undefined && $($(this).find("td")[0]).attr("authorid") != "" && $($(this).find("td")[0]).attr("authorid") != null) {
                        authorids[index] = {
                            authorId: $($(this).find("td")[0]).attr("authorid")
                        };
                    }
                });
            });

            if ($scope.AuthorValue.length != 0) {

                if ($scope.AuthorValue != null) {
                    authorids = $scope.AuthorValue;
                }
            }

            var ImprintId = $("#OUPImprint_" + i + " option:selected").val();
            var LanguageId = $("#Language_" + i + " option:selected").val();
            var Derivative = $("input[type=radio][name$=Derivatives" + i + "]:checked").val();
            var Author = $("input[type=radio][name$=AuthorCategory" + i + "]:checked").val();

            var mstr_OUP = $("input[type=text][id=OrgISBNTiTle_" + i + "]").val();

            var CopyRightYear = $("#CopyrightYear_" + i).val();

            var ProjectedCurrencyId = $("#ProjectedCurrency_" + i + " option:selected").val();

            var WorkingProduct = $("input[type=text][id=WorkingProduct_" + i + "]").val();
            var WorkingSubProduct = $("input[type=text][id=WorkingSubProduct_" + i + "]").val();

            var OUPEdition = $("input[type=number][id=OUPEdition_" + i + "]").val();

            var obj_thirdpartypermission = $("input[type=radio][name$=thirdpartypermission" + i + "]:checked").val();

            obj[i] =
               {
                   ISBNObject: DuplicateISBNno,
                   DivisionId: $scope.DivSubDivCntrl.Division,
                   SubdivisionId: $scope.DivSubDivCntrl.SubDivision,

                   //ProductCategoryId: $scope.ProductCategory,
                   ProductCategoryId: $("#ProductCategory option:selected").val(),
                   ProductTypeId: $('input[name=ProductType]:checked').val(),
                   SubProductTypeId: $scope.SubProductType,
                   //SeriesId: $scope.Series,

                   SeriesId: $scope.SeriesId,
                   OUPISBN: $scope.OUPISBN[i],

                   //WorkingProduct: $scope.WorkingProduct[i],
                   //WorkingSubProduct: $scope.WorkingSubProduct[i],
                   //OUPEdition: $scope.OUPEdition[i],
                   WorkingProduct: WorkingProduct,
                   WorkingSubProduct: WorkingSubProduct,
                   OUPEdition: OUPEdition,

                   ProjectedPrice: $scope.ProjectedPrice[i],
                   ProjectedCurrencyId: ProjectedCurrencyId,

                   ProjectPublishingDate: convertDateMDY($('.ProjectPublishingDate').find("input:text").val()),
                   CopyrightYear: CopyRightYear,// $scope.CopyrightYear[i],
                   ImprintId: ImprintId,
                   //ImprintId: $("#OUPImprint_" + i + " option:selected").val(),
                   LanguageId: LanguageId,
                   //LanguageId: $("#Language_" + i + " option:selected").val(),
                   //ProductCode: $scope.ProductCode,
                   Derivatives: Derivative,
                   //Derivatives: $("input[type=radio][name$=Derivatives" + i + "]:checked").val(),
                   //OrgISBN: $scope.OrgISBNTiTle[i],
                  // OrgISBN: $('input[type=text][id=OrgISBNTiTle_0]').val() == ""? null : $('input[type=text][id=OrgISBNTiTle_0]').val(),
                   OrgISBN: mstr_OUP == "" ? null : mstr_OUP,
                   AuthorCategory: Author,
                   //AuthorCategory: $("input[type=radio][name$=AuthorCategory" + i + "]:checked").val(),
                   EnteredBy: $("#enterdBy").val(),
                   //AuthorObject: $scope.authorobject
                   AuthorObject: authorids,
                   ThirdPartyPermission: parseInt(obj_thirdpartypermission),
               };
           
        }

        var Object = { testArr: obj };

        var SeriesProductEntryStatus = AJService.PostDataToAPI('SeriesProductEntry/InsertSeries', Object);

        SeriesProductEntryStatus.then(function (msg) {     
            if (msg.data.status == "notOK") {
                SweetAlert.swal("Error", "There is some problem.", "error");
            }
            else if (msg.data.status == "OK") {
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
            else {
                SweetAlert.swal("", msg.data.status, "info");
            }
        },
    function () {
        alert('Please validate details');
    });

    }

    $scope.validatSeriesDetail = function () {
       
        //$('.SeriesDetail').each(function () {

        //    var result = [];
        //    result = unique($(this).find("input[name$=OUPISBN]").map(function () { return $(this).text(); }).get())
        //    for (var i = 0; i < result.length; i++) {
        //        $(this).find("input[name*=OUPISBN]").filter(":contains('" + result[i] + "')").parents("tr").each(function (index, value) {
        //            var _lastTr = $(".SeriesDetail").find("input[name*=OUPISBN]").filter(":contains('" + result[i] + "')").parents("tr:last")
        //            if ($(_lastTr).find('input[name*=WorkingProduct]').val() == "" || $(_lastTr).find('input[name*=OUPISBN]').val() == "") {

        //                $scope.userForm.$valid = false;
        //                SweetAlert.swal("Validation!", " !", "");
        //                $(_lastTr).addClass("has-error");


        //            }

        //        });
        //    }
        //});

        //Required for check duplicate value
        var OUP1 = 0;
        var OUP2 = 0;

        var WP1 = 0;
        var WP2 = "";
        //-------------------------
        $('#TblOwnerList tbody .seriestr').each(function () {
            //$(this).find('td').each(function () {
            var OUPISBN = $(this).find('input[name="OUPISBN"]').val();
            var WorkingProduct = $(this).find('input[name="WorkingProduct"]').val();
            //var WorkingSubProduct = $(this).find('input[name="WorkingSubProduct"]').val();
            var OUPEdition = $(this).find('input[name="OUPEdition"]').val();
            var ProjectPublishingDate = $(this).find('input[name="ProjectPublishingDate"]').val();
            //var CopyrightYear = $(this).find('input[name="CopyrightYear"]').val();
            var OUPImprint = $(this).find('select[name="OUPImprint"] option:selected').text();
            var Language = $(this).find('select[name="Language"] option:selected').text();
            var Derivative = $(this).find('[name*="Derivatives"]').is(':checked');
            var DerivativeVal = $(this).find('[name*="Derivatives"]:checked').val();
            var ORGISBN = $(this).find('input[name="OrgISBNTiTle"]').val();

            var AuthorCategory = $(this).find('[name*="AuthorCategory"]').is(':checked');
            var AuthorCategoryValue = $(this).find('[name*="AuthorCategory"]:checked').val();
            var SuggestedAuthorName = $(this).find('input[name="SuggestedAuthorName"]').val();

            var ProjectedPrice = $(this).find('input[name="ProjectedPrice"]').val();
            


            //if (OUPISBN == undefined || OUPISBN == "") {
            //    $(this).find('input[name="OUPISBN"]').next().find('p:first').removeClass('ng-hide').addClass("ng-show");
            //    $(this).find('input[name="OUPISBN"]').closest('.form-group').addClass("has-error");
            //}
            
            if (OUPISBN != undefined && OUPISBN != "") {
                if (OUPISBN.length > 13 || OUPISBN.length < 13) {
                    $(this).find('input[name="OUPISBN"]').next().find('p:last').removeClass('ng-hide').addClass("ng-show");
                    $(this).find('input[name="OUPISBN"]').closest('.form-group').addClass("has-error");
                }

            }

            //Check duplicate ISBN value // added by prakash on 9/9/2016
            if (OUPISBN != undefined && OUPISBN != "") {
                //alert(OUPISBN);
                if (OUP2 == OUPISBN) {
                    OUP1++;
                }
                OUP2 = OUPISBN;

                if (OUP1 > 0) {
                    $(this).find('input[name="OUPISBN"]').next().find('span:last').removeClass('ng-hide').addClass("ng-show");
                    $(this).find('input[name="OUPISBN"]').closest('.form-group').addClass("has-error");
                    OUP2 == 0;
                    OUP1--;
                }
                //else {
                //    $(this).find('input[name="OUPISBN"]').next().find('span:last').addClass('ng-hide').removeClass("ng-show");
                //    $(this).find('input[name="OUPISBN"]').closest('.form-group').removeClass("has-error");
                //}
            }

            if (WorkingProduct == undefined || WorkingProduct == "") {
                $(this).find('input[name="WorkingProduct"]').next().find('p').removeClass('ng-hide').addClass("ng-show");
                $(this).find('input[name="WorkingProduct"]').closest('.form-group').addClass("has-error");
            }
           
            //Check duplicate WorkingProduct value // added by prakash on 12/9/2016
            if (WorkingProduct == undefined || WorkingProduct != "") {
                if (WP2 == WorkingProduct) {
                    WP1++;
                }
                WP2 = WorkingProduct;
            
                if (WP1 > 0) {
                    $(this).find('input[name="WorkingProduct"]').next().find('span').removeClass('ng-hide').addClass("ng-show");
                    $(this).find('input[name="WorkingProduct"]').closest('.form-group').addClass("has-error");
                    WP2 == "";
                    WP1--;
                }
            }

            //if (WorkingSubProduct == undefined || WorkingSubProduct == "") {
            //    $(this).find('input[name="WorkingSubProduct"]').next().find('p').removeClass('ng-hide').addClass("ng-show");
            //    $(this).find('input[name="WorkingSubProduct"]').closest('.form-group').addClass("has-error");
            //}

            if (OUPEdition == undefined || OUPEdition == "") {
                $(this).find('input[name="OUPEdition"]').next().find('p:first').removeClass('ng-hide').addClass("ng-show");
                $(this).find('input[name="OUPEdition"]').closest('.form-group').addClass("has-error");
            }

            if (OUPEdition != undefined && OUPEdition != "") {
                if (OUPEdition.length > 7) {
                    $(this).find('input[name="OUPEdition"]').next().find('p:last').removeClass('ng-hide').addClass("ng-show");
                    $(this).find('input[name="OUPEdition"]').closest('.form-group').addClass("has-error");
                }

            }

            if (ProjectPublishingDate == undefined || ProjectPublishingDate == "") {
                //$(this).find('input[name="ProjectPublishingDate"]').next().find('p').removeClass('ng-hide').addClass("ng-show");
                $(this).find('input[name="ProjectPublishingDate"]').closest(".ProjectPublishingDate").next().find('p').removeClass('ng-hide').addClass("ng-show");
                $(this).find('input[name="ProjectPublishingDate"]').closest('.form-group').addClass("has-error");
            }
            //if (CopyrightYear == undefined || CopyrightYear == "") {
            //    $(this).find('input[name="CopyrightYear"]').next().find('p').removeClass('ng-hide').addClass("ng-show");
            //    $(this).find('input[name="CopyrightYear"]').closest('.form-group').addClass("has-error");
            //}
            if (OUPImprint == undefined || OUPImprint == "") {
                $(this).find('select[name="OUPImprint"]').next().find('p').removeClass('ng-hide').addClass("ng-show");
                $(this).find('select[name="OUPImprint"]').closest('.form-group').addClass("has-error");
            }
            if (Language == undefined || Language == "") {
                $(this).find('select[name="Language"]').next().find('p').removeClass('ng-hide').addClass("ng-show");
                $(this).find('select[name="Language"]').closest('.form-group').addClass("has-error");
            }
            if (Derivative == undefined || Derivative == "") {
                $(this).find('[name*="Derivatives"]').closest('label').next().find('p').removeClass('ng-hide').addClass("ng-show");
                $(this).find('[name*="Derivatives"]').closest('.form-group').addClass("has-error");
            }

            if (DerivativeVal == "Yes") {

                if (ORGISBN == undefined || ORGISBN == "") {
                    $(this).find('input[name="OrgISBNTiTle"]').next().find('p:first').removeClass('ng-hide').addClass("ng-show");
                    //$(this).find('input[name="OrgISBNTiTle"]').next().find('p:last').removeClass('ng-show').addClass("ng-hide");
                    $(this).find('input[name="OrgISBNTiTle"]').closest('.form-group').addClass("has-error");
                }

                if (ORGISBN != undefined && ORGISBN != "") {
                    if (ORGISBN.length > 13 || ORGISBN.length < 13) {
                        $(this).find('input[name="OrgISBNTiTle"]').next().find('p:first').removeClass('ng-show').addClass("ng-hide");
                        $(this).find('input[name="OrgISBNTiTle"]').next().find('p:last').removeClass('ng-hide').addClass("ng-show");
                        $(this).find('input[name="OrgISBNTiTle"]').closest('.form-group').addClass("has-error");
                    }

                }
            }

            if ($scope.AuthorValue.length == 0) {

               
                if (AuthorCategory == undefined || AuthorCategory == "") {

                    $(this).find('[name*="AuthorCategory"]').closest('label').next().find('p').removeClass('ng-hide').addClass("ng-show");
                    $(this).find('[name*="AuthorCategory"]').closest('.form-group').addClass("has-error");
                }
                if (AuthorCategoryValue == "Individual" || AuthorCategoryValue == "Institute") {
                    //    $(this).find('[name="SuggestedAuthorName"]').closest('label').next().find('p').removeClass('ng-hide').addClass("ng-show");
                    //    $(this).find('[name="SuggestedAuthorName"]').closest('.form-group').addClass("has-error");
                    if (SuggestedAuthorName == undefined || SuggestedAuthorName == "") {
                        $(this).find('input[name="SuggestedAuthorName"]').next().find('p').removeClass('ng-hide').addClass("ng-show");
                        $(this).find('input[name="SuggestedAuthorName"]').closest('.form-group').addClass("has-error");
                    }
                }
            }


            if ($scope.AuthorValue.length == 0) {
                if ((WorkingProduct == undefined || WorkingProduct == "") || (OUPEdition == undefined || OUPEdition == "") || (ProjectPublishingDate == undefined || ProjectPublishingDate == "") || (OUPImprint == undefined || OUPImprint == "") || (Language == undefined || Language == "") || (Derivative == false) || (AuthorCategory == false)) {
                    $scope.userForm.$valid = false;
                    $scope.userForm.$error.required.length = 2;
                    SweetAlert.swal("Validation", "Please enter details!", "warning");
                    //$(this).addClass("has-error");
                }
            }
            else {

                if ((WorkingProduct == undefined || WorkingProduct == "") || (OUPEdition == undefined || OUPEdition == "") || (ProjectPublishingDate == undefined || ProjectPublishingDate == "") || (OUPImprint == undefined || OUPImprint == "") || (Language == undefined || Language == "") || (Derivative == false)) {
                    $scope.userForm.$valid = false;
                    $scope.userForm.$error.required.length = 2;
                    SweetAlert.swal("Validation", "Please enter details!", "warning");
                    //$(this).addClass("has-error");
                }

            }

        });


    };


    $('input[name="OUPISBN"]').change(function () {
        alert("Handler for .change() called.");
    });

    $scope.RemoveSeriesControlValidation = function () {
        var obj = $(event.target);
        obj.closest("tr").removeClass("has-error");
    };

    function convertDateMDY(date) {
        if (date != undefined) {
            var datearray = date.split("/");
            return datearray[1] + '/' + datearray[0] + '/' + datearray[2];
        }
        else {
            return null;
        }

    }

    $scope.RemoveValidationForInputs = function (controlId) {
        //controlId.find('p').removeClass('ng-hide').addClass("ng-show");
        var mstr_val = controlId.id;
        //setTimeout(function () {
        $('#' + mstr_val).next().find('p').removeClass('ng-show').addClass("ng-hide");
        $('#' + mstr_val).closest('.form-group').removeClass("has-error");
        //},250)

    }

    //Check duplicate ISBN value // added by prakash on 9/9/2016
    $scope.checkDuplicateValue = function (value) {
        //alert(value);        
        var OUP = 0;
        $('#TblOwnerList tbody .seriestr').each(function () {
            var OUPISBN = $(this).find('input[name="OUPISBN"]').val();
            //alert(OUPISBN);
            if (OUPISBN == value) {
                OUP++;
            }

            if (OUP > 1) {
                $(this).find('input[name="OUPISBN"]').next().find('span:last').removeClass('ng-hide').addClass("ng-show");
                $(this).find('input[name="OUPISBN"]').closest('.form-group').addClass("has-error");
            }
            else {
                $(this).find('input[name="OUPISBN"]').next().find('span:last').addClass('ng-hide').removeClass("ng-show");
                $(this).find('input[name="OUPISBN"]').closest('.form-group').removeClass("has-error");
            }

        });

    }

    //Check duplicate WorkingProduct value // added by prakash on 12/9/2016
    $scope.checkDuplicateWP = function (value) {
        //alert(value);        
        var WP = 0;
        $('#TblOwnerList tbody .seriestr').each(function () {
            var WorkingProduct = $(this).find('input[name="WorkingProduct"]').val();
            //alert(WorkingProduct);
            if (WorkingProduct == value) {
                WP++;
            }

            if (WP > 1) {
                $(this).find('input[name="WorkingProduct"]').next().find('span:last').removeClass('ng-hide').addClass("ng-show");
                $(this).find('input[name="WorkingProduct"]').closest('.form-group').addClass("has-error");
            }
            else {
                $(this).find('input[name="WorkingProduct"]').next().find('span:last').addClass('ng-hide').removeClass("ng-show");
                $(this).find('input[name="WorkingProduct"]').closest('.form-group').removeClass("has-error");
            }

        });

    }

    $scope.RemoveValidationForRadioButtons = function (controlId) {
        //controlId.find('p').removeClass('ng-hide').addClass("ng-show");
        var mstr_val = controlId.id;
        //setTimeout(function () {
        $('#' + mstr_val).closest('label').next().next().find('p').removeClass('ng-show').addClass("ng-hide");
        $('#' + mstr_val).closest('.form-group').removeClass("has-error");
        //},250)

    }

    $scope.RemoveValidationForRadioButtons2 = function (controlId) {
        //controlId.find('p').removeClass('ng-hide').addClass("ng-show");
        var mstr_val = controlId.id;
        //setTimeout(function () {
        $('#' + mstr_val).closest('label').next().next().find('p').removeClass('ng-show').addClass("ng-hide");
        $('#' + mstr_val).closest('.form-group').removeClass("has-error");
        //},250)

    }

    /*Copy first Imprint dropdown value to other imprint dropdowns*/
    $('#SelectImprint').click(function (e) {

        if ($("#SelectImprint").is(':checked')) {
            $scope.OUPImprintReq = false;
        } else {
            $scope.OUPImprintReq = true;
        }

        var Imprint;
        var value = false;
        for (var i = 0; i < $scope.SeriesListControl.length; i++) {
            if (value == false) {
                Imprint = $("#OUPImprint_" + i + " option:selected").val();
                if (Imprint != "") {
                    i = 0;
                    value = true;
                }
            }
            else {
                $("#OUPImprint_" + i).val(Imprint);
            }
        }

    });


    /*Copy first Language dropdown value to other Language dropdowns*/
    $('#SelectLanguage').click(function (e) {


        if ($("#SelectLanguage").is(':checked')) {
            $scope.LanguageReq = false;
        } else {
            $scope.LanguageReq = true;
        }

        var Language;
        var value = false;
        for (var i = 0; i < $scope.SeriesListControl.length; i++) {
            if (value == false) {
                Language = $("#Language_" + i + " option:selected").val();
                if (Language != "") {
                    i = 0;
                    value = true;
                }
            }
            else {
                $("#Language_" + i).val(Language);
            }
        }

    });

    /*Copy first CopyrightYear textbox value to other CopyrightYear textbox*/
    $('#SelectCopyrightYear').click(function (e) {
        var CopyrightYear;
        var value = false;
        for (var i = 0; i < $scope.SeriesListControl.length; i++) {
            if (value == false) {
                CopyrightYear = $("#CopyrightYear_" + i).val();
                if (CopyrightYear != "") {
                    i = 0;
                    value = true;
                }
            }
            else {
                $("#CopyrightYear_" + i).val(CopyrightYear);
            }
        }

    });

    /*Copy first selected derivative radio button and OriginalISBn textbox value to other 
      CopyrightYear derivative radio button and OriginalISBn textbox*/
    $('#SelectDerivative').click(function (e) {


        if ($("#SelectDerivative").is(':checked')) {
            $scope.DerivativesReq = false;
        } else {
            $scope.DerivativesReq = true;
        }

        //if ($("#SelectDerivative").is(':checked')) {
        var rdb_Derivative = true;
        var status = false;
        var OrgISBN;
        var value = false;
        for (var i = 0; i < $scope.SeriesListControl.length; i++) {
            if (value == false) {
                //rdb_Derivative = $("#Derivatives_" + i).val();
                //rdb_Derivative = $("#Derivatives_0").prop("checked");
                if ($('.DerivativeYes').prop("checked") == true) {
                    rdb_Derivative = true;
                    status = true;
                    OrgISBN = $("#OrgISBNTiTle_" + i).val();
                    $('.derivativeshow').removeClass("ng-hide").addClass("ng-show");
                }
                else if ($('.DerivativeNo').prop("checked") == true) {
                    rdb_Derivative = false;
                    status = true;

                }

                //if (rdb_Derivative == true) {
                i = 0;
                value = true;
                //}
            }
            else {
                if (rdb_Derivative == true && status == true) {
                    $('.DerivativeYes').prop('checked', true);
                    $("#OrgISBNTiTle_" + i).val(OrgISBN);
                }
                else if (rdb_Derivative == false && status == true) {
                    $('.DerivativeNo').prop('checked', true);
                }

            }
        }
       
    });


    /*Copy first ProjectPublishingDate textbox value to other ProjectPublishingDate textbox*/
    $('#SelectProjectPubDate').click(function (e) {
       
        if ($("#SelectProjectPubDate").is(':checked')) {
            $scope.ProjectPublishingDateReq = false;
        } else {
            $scope.ProjectPublishingDateReq = true;
        }
        //if ($("#SelectProjectPubDate").is(':checked')) {

        var ProjectPublishingDate;
        var CopyrightYear;
        var value = false;
        for (var i = 0; i < $scope.SeriesListControl.length; i++) {
            if (value == false) {
                ProjectPublishingDate = $("#ProjectPublishingDate_" + i).val();
                CopyrightYear = $("#CopyrightYear_" + i).val();
                if (ProjectPublishingDate != "") {
                    i = 0;
                    value = true;
                }
            }
            else {
                $("#ProjectPublishingDate_" + i).val(ProjectPublishingDate);
                $("#CopyrightYear_" + i).val(CopyrightYear);
            }
        }

        EveryRowValidation($("input[type=text][name*=ProjectPublishingDate]"));

    });


    /*added by prakash on 22/11/2016 (Copy first Working Project textbox value to other Working Project textbox)*/
    $('#SelectWorkingProject').click(function (e) {
        
        if ($("#SelectWorkingProject").is(':checked')) {
            $scope.WorkingProductReq = false;
        } else {
            $scope.WorkingProductReq = true;
        }
        //if ($("#SelectProjectPubDate").is(':checked')) {

        var mobj_WorkingProject;
        var value = false;
        for (var i = 0; i < $scope.SeriesListControl.length; i++) {
            if (value == false) {
                mobj_WorkingProject = $("#WorkingProduct_" + i).val();
                if (mobj_WorkingProject != "") {
                    i = 0;
                    value = true;
                }
            }
            else {
                $("#WorkingProduct_" + i).val(mobj_WorkingProject);
            }
        }

        EveryRowValidation($("input[type=text][name*=WorkingProduct]"));
        

    });

    /*Added By Ankush on 10/04/2017*/
    $('#SelectSubWorkingProject').click(function (e) {

        //if ($("#SelectSubWorkingProject").is(':checked')) {
        //    $scope.WorkingSubProductReq = false;
        //} else {
        //    $scope.WorkingSubProductReq = true;
        //}

        //if ($("#SelectProjectPubDate").is(':checked')) {

        var mobj_WorkingSubProject;
        var value = false;
        for (var i = 0; i < $scope.SeriesListControl.length; i++) {
            if (value == false) {
                mobj_WorkingSubProject = $("#WorkingSubProduct_" + i).val();
                if (mobj_WorkingSubProject != "") {
                    i = 0;
                    value = true;
                }
            }
            else {
                $("#WorkingSubProduct_" + i).val(mobj_WorkingSubProject);
            }
        }

        EveryRowValidation($("input[type=text][name*=WorkingSubProduct]"));


    });

    /*Added By Ankush on 10/04/2017*/
    $('#SelectEdition').click(function (e) {

        if ($("#SelectEdition").is(':checked')) {
            $scope.OUPEditionReq = false;
        } else {
            $scope.OUPEditionReq = true;
        }
        //if ($("#SelectProjectPubDate").is(':checked')) {

        var mobj_OUPEdition;
        var value = false;
        for (var i = 0; i < $scope.SeriesListControl.length; i++) {
            if (value == false) {
                mobj_OUPEdition = $("#OUPEdition_" + i).val();
                if (mobj_OUPEdition != "") {
                    i = 0;
                    value = true;
                }
            }
            else {
                $("#OUPEdition_" + i).val(mobj_OUPEdition);
            }
        }

        EveryRowValidation($("input[type=text][name*=OUPEdition]"));


    });
    
    /*Copy first selected derivative radio button and OriginalISBn textbox value to other 
      CopyrightYear derivative radio button and OriginalISBn textbox*/
    $('#SelectAuthorcategory').click(function (e) {
      
        if ($("#SelectAuthorcategory").is(':checked')) {

            $scope.AuthorCatagoryReq = false;

            $('input[type=radio][name*=AuthorCategory]').each(function () {
                $(this).attr('disabled', 'disabled');
            })


            for (var i = 0; i < $($('.displaytable tbody')[0]).find('tr').length; i++) {
                //  $scope.AuthorValue :                  
                $scope.AuthorValue[i] = {
                    authorId: $($($($('.displaytable tbody')[0]).find('tr')[i]).find('td')[0]).attr("authorid")
                };
                // $scope.AuthorValue.push($($('.AuthorCheckBox ').closest('div').next('div').find('table').find('tr').not(":first")[i]).find("td").attr("authorid"));

                if ($('.AuthorIndividual').prop("checked") == true) {
                    $('.AuthorIndividual').prop('checked', true);
                }
                else if ($('.AuthorInstitute').prop("checked") == true) {
                    $('.AuthorInstitute').prop('checked', true);
                }
            }
        }
        else {
            $scope.AuthorCatagoryReq = true;
            //$('#TblOwnerList tbody .seriestr').not(':first').find(".AuthorIndividual").attr('disabled', false);
            //$('#TblOwnerList tbody .seriestr').not(':first').find(".AuthorInstitute ").attr('disabled', false);
           
            $('input[type=radio][name*=AuthorCategory]').each(function () {
                $(this).removeAttr('disabled');
            })

            if ($('.AuthorIndividual').prop("checked") == false) {
                $('.AuthorIndividual').prop('checked', false);
            }
            else if ($('.AuthorInstitute').prop("checked") == false) {
                $('.AuthorInstitute').prop('checked', false);
            }

        }


    });

    $('#chk_SelectTPP').click(function (e) {
        
        if ($("#chk_SelectTPP").is(':checked')) {
            $scope.thirdpartypermissionReq = false;
        } else {
            $scope.thirdpartypermissionReq = true;
        }
             
        var rdb_Derivative = true;
        var status = false;
        var value = false;
        for (var i = 0; i < $scope.SeriesListControl.length; i++) {
            if (value == false) {
                if ($('.thirdpartypermissionYes').prop("checked") == true) {
                    rdb_Derivative = true;
                    status = true;
                }
                else if ($('.thirdpartypermissionNo').prop("checked") == true) {
                    rdb_Derivative = false;
                    status = true;
                }
                i = 0;
                value = true;
            }
            else {
                if (rdb_Derivative == true && status == true) {
                    $('.thirdpartypermissionYes').prop('checked', true);
                }
                else if (rdb_Derivative == false && status == true) {
                    $('.thirdpartypermissionNo').prop('checked', true);
                }

            }
        }

    });

    
    $scope.ProductCategory = "";
    $scope.Fn_setProductCategoryId = function () {
        //debugger;
        $scope.ProductCategory =  $("#ProductCategory").val();
    }

    //Add by Ankush 21/10/02016
    $scope.ValidISBN = function (obj) {
        ISBN = obj.value;
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


    //var ProjectPublishingDate = [];
    //$scope.CopyrightYear = [];
    //$scope.SetCopyrightDate = function (datetext) {
    //    $("[id$=CopyrightYear_" + $(datetext).attr("rowNo") + "]").val($(datetext).val().split('/')[2]);
    //    $scope.CopyrightYear[parseInt($(datetext).attr("rowNo"))] = $(datetext).val().split('/')[2];
    //    if ($(datetext).val() != "" || $(datetext).val() != undefined) {
    //        $(datetext).closest(".form-group").removeClass("has-error");
    //        $(datetext).closest(".ProjectPublishingDate").next().find('p').removeClass("ng-show").addClass("ng-hide");
    //    }
    //}


    $scope.ProjectPublishingDate = [];
    $scope.CopyrightYear = [];
    $scope.SetCopyrightDate = function (datetext) {
        var mstr_Val = datetext.id

        //$scope.DataValidateForDate(mstr_Val);

        for (var i = 0; i < $scope.SeriesListControl.length; i++) {

            $scope.DataValidateForDate('ProjectPublishingDate_'+[i]);
            //if ('ProjectPublishingDate_' + [i] == mstr_Val) {
            var dd = $(datetext).val().split('/')[0];
            var mm = $(datetext).val().split('/')[1];
            var yy = $(datetext).val().split('/')[2];

            if (dd >= 1 && mm >= 7) {
                yy = parseInt(yy) + 1
            }
            $("[id$=CopyrightYear_" + [i] + "]").val(yy);
            $scope.CopyrightYear[parseInt($(datetext).attr("rowNo"))] = yy;
            $scope.ProjectPublishingDate[i] = $('#ProjectPublishingDate_' + [i]).val();
            // }


        }

    }

    $scope.DataValidateForDate = function (data) {

        dataVal = $('#' + data).val();
        if (dataVal == undefined || dataVal == "") {
            //$('#' + data).closest('div').next().find('p').removeClass('ng-hide').addClass("ng-show");
            //$('#' + data).closest('.form-group').addClass("has-error");

            $(data).closest(".form-group").addClass("has-error");
            $(data).closest(".ProjectPublishingDate").next().find('p').addClass("ng-show").removeClass("ng-hide");
        }
        else{
            //$('#' + data).closest('div').next().find('p').addClass('ng-hide').removeClass("ng-show");
            //$('#' + data).closest('.form-group').removeClass("has-error");

            $(data).closest(".form-group").removeClass("has-error");
            $(data).closest(".ProjectPublishingDate").next().find('p').removeClass("ng-show").addClass("ng-hide");
        }
    }


});