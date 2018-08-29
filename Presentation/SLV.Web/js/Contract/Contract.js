app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI, $parse) {
    app.expandControllerA($scope, AJService, $window);
    app.expandControllerProductDetails($scope, AJService, $window);
    app.expandAuthorBasicDetails($scope, AJService, $window);
    app.expandControllerRoyaltySlab($scope, AJService, $window);
    app.AuthorViewcontroller($scope, AJService, $window);

    app.expandControllerProductLicense($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);

    //for Kit Details List
    app.expandControllerKitISBNLIst($scope, AJService);

    //if ($('[id*=hid_productid_AC]').val() != "") {
    //    $scope.ProductSerach($('[id*=hid_productid_AC]').val(), $('[id*=hid_ContractId]').val());
    //} else if ($('[id*=hid_ContractId]').val() != "") {
    //    $scope.ProductSerach($('[id*=hid_productid_AC]').val(), $('[id*=hid_ContractId]').val());
    //}

   
   
    
    //var _href = location.href.toLocaleLowerCase();

    //    if (_href.trim().indexOf("index") > 0) {
    //        $scope.ProductDetailsThirdPartyPermsion = true;
            
    //        $scope.ProductDetailsThirdPartyPermsionMultiple = true;
    //        setTimeout(function () {
    //        if ($('[id*=hid_productid_AC]').val() != "" && $('[id*=hid_productid_AC]').val() != undefined && $('[id*=hid_productid_AC]').val() != 0) {

    //                $scope.ProductIdValuesCheck = $('[id*=hid_productid_AC]').val();
                   
    //        } else {
    //            $scope.ProductIdValuesCheck = false;
    //        }
    //        }, 500)
          
    //    } else if (_href.trim().indexOf("authorcontract/view") > 0) {
    //        $scope.ProductDetailsThirdPartyPermsionView = true;
    //    } else if (_href.trim().indexOf("authorcontract?seriesids") > 0) {
    //        $scope.ProductDetailsThirdPartyPermsionMultiple = true;
    //    }
    //    else {
    //        $scope.ProductDetailsThirdPartyPermsionMultiple = false;
    //        $scope.ProductDetailsThirdPartyPermsionView = false;
    //        $scope.ProductDetailsThirdPartyPermsion = false;
    //        $scope.ProductDetailsThirdPartyPermsionMultiple = false;
    //    }

    ////----for third party permission
    //$scope.ProductIdValuesCheck = '';
    //if ($('[id*=hid_productid_AC]').val() != "" && $('[id*=hid_productid_AC]').val() != undefined && $('[id*=hid_productid_AC]').val() != 0) {
    //    $scope.ProductIdValuesCheck = parseInt($('[id*=hid_productid_AC]').val());
    //}
    //$scope.UpdateMode = false;
    //$scope.ContractCodeValuesCheck = '';
    //if ($('[id*=hid_contractUpdate]').val() == "update") {
    //    $scope.UpdateMode = true;
    //}

    //$scope.SeriesUpdateMode = false;
    //if ($('[id*=hid_contractUpdate]').val() == "seriesupdate") {
    //    $scope.SeriesUpdateMode = true;
    //}
    ////----end for third party permission
   

    // $scope.serieslist[]
    $scope.PaymentPeriodType = [];
    if ($("#hid_licenceId").val() != "") {
        $scope.ProductLicenseSerach($("#hid_licenceId").val());
        setTimeout(function () {
            $scope.AuthorListProductBased($("#hid_productidValue").val());
            if ($("#hid_price").val() != "" && $("#hid_ContractId").val() == "0" && $("#hid_price").val() != undefined) {
                $scope.ProductPrice = $("#hid_price").val();
                $("input[name*=ProductPrice]").val($("#hid_price").val());
                $("input[name*=ProductPrice]").prop('disabled', true);
            }

        }, 2000)

    }


    $scope.getListAuthorContractStatusMail = function (Id) {
        //debugger;
        var getListAuthorContractStatusMail = AJService.PostDataToAPI("AuthorContact/getListAuthorContractStatusMail?Id=" + Id, null);
        getListAuthorContractStatusMail.then(function (status) {

        }, function () {

        });
    }

    $scope.DeptCode = "";
    $scope.TblList = [];
    $scope.TblList.push(1);
    $scope.SuppliedList = [];
    $scope.MediumofDelivery = null;
    $scope.AuthorBox = [];
    $scope.AuthorBox.push(1);
    $scope.Amendment = null;
    $scope.Contributor = null;
    $scope.ThirdPartyPermission = null;
    $scope.TemsOfCopyRight = null;
    $scope.FirstAction = "";
    $scope.flag = 0;
    $scope.HandledBy = null;
    $scope.contributor = null;
    $scope.ContractNameList = [];
    $scope.ContractNameList.push(1);
    $scope.FromPage = 1;
    $scope.ContractDate = Getdate();
    $scope.Entrydate = Getdate();
    //$scope.PeriodOfAgreement = 0;
    $scope.SubsidiaryRequired = "";
    $scope.CurrencyValue = "";
    $scope.totalPercentage = 0;
    $scope.show = 0;
    $scope.DeptCode = "";
    $scope.count = 0;
    $scope.SelectedSupplyMaterialByAuthor = [];
    $scope.counter = -1;
    $scope._subsidiaryList = [];
    $scope.ttlSubsidiary = 2;
    $scope.FirstTime = 1;
    $scope.ttlSubsidiary = $('#hid_productid_AC').val() != "" ? 100000000 : _ContractDetails.data._ttlSusidiary;
    //$scope.AuthorType = {};
    $scope.AgreementStatus = "";
    $scope.ContributorAgreement = "";
    $scope.contractId = 0;
    $scope.Flag = 0;
    $scope.ContributorDoc = [];
    $scope.AgreementDoc = [];
    $scope.HandledByList = [];
    /******************************************************************************
    *******************************************************************************
    Created By  :  Dheeraj Kumar Sharma
    Created on  :  31/05/2016
    Created For :  if user form Right department then bind dropdown other 
    wise show candidate name in the dropdown 
    *******************************************************************************
    *******************************************************************************/
    $scope.handledBy = function (UserId, UserName) {
        var executive =
        {
            Id: UserId,
            ExecutiveName: UserName
        }
        $scope.HandledByList.push(executive);
        $('select[name$=HandledByExecutive]').prop("disabled", true);
        $scope.ByExecutive = UserId

    }


    /******************************************************************************
   *******************************************************************************
   Created By  :  Dheeraj Kumar Sharma
   Created on  : 06/06/2016
   Created For : This section will use to bind multiple productDetails when series contract is created
  
   *******************************************************************************
   *******************************************************************************/
    var mstr_ProductCode = '';
    var temp_ProductCode = [];
    $scope.ShowProductsDetailMultiple = function (Ids) {
        ////commented and added By Prakash on  04 July, 2017      
        var ProductStatus = AJService.GetDataFromAPI('ProductMaster/MultipleProductDetails?Ids=' + '&SeriesCode=' + Ids + '&For=');
        ProductStatus.then(function (msg) {
       
          
            $scope.ProductDetailsListChild = msg.data;
         
            for (var j = 0; j < msg.data.length; j++) {
                if (mstr_ProductCode != msg.data[j].ProductCode) {
                    temp_ProductCode.push({
                        'ProductId': msg.data[j].ProductId,
                        'ProductCode': msg.data[j].ProductCode,
                        'projectcode': msg.data[j].projectcode,
                        'ProductCategory': msg.data[j].ProductCategory,
                        'WorkingProduct': msg.data[j].WorkingProduct,
                        'OupIsbn': msg.data[j].OupIsbn,
                        'ProjectedCurrencyId': msg.data[j].ProjectedCurrencyId,
                        'ProjectedPrice': msg.data[j].ProjectedPrice,
                        'AuthorName': msg.data[j].AuthorName,
                        'projectedpublishingdate': msg.data[j].projectedpublishingdate,
                        'ParentId': msg.data[j].ParentId,
                        'WorkingSubProduct': msg.data[j].WorkingSubProduct,
                        'ProductType': msg.data[j].ProductType,
                        'SubProductType': msg.data[j].SubProductType,
                        'thirdpartypermission': msg.data[j].thirdpartypermission,
                        //'flag': msg.data[j].flag,                      
                    });
                }

                mstr_ProductCode = msg.data[j].ProductCode;
            }
           
            $scope.ProductDetailsList = temp_ProductCode;

            //bind third party permission
            setTimeout(function () {
                if (temp_ProductCode.length > 0) {
                    for (var i = 0; i < temp_ProductCode.length; i++) {

                        if (temp_ProductCode[i].thirdpartypermission == "Yes") {
                            $($('[id*=ThirdPartyPermissionValue_' + [i] + ']').find("input")[0]).trigger("click")

                        } else {
                            $($('[id*=ThirdPartyPermissionValue_' + [i] + ']').find("input")[1]).trigger("click")
                        }
                    }
                }
            }, 2000);
            //end bind third party permission
            
        }, function () {
            //alert('Error in getting Product list');
        }); 
        
    }

    $scope.ShowProductsDetailMultipleBySeries = function () {
        if ($("#hid_productIds").val() != "" && $("#hid_productIds").val() != null && $("#hid_productIds").val() != undefined) {
            var ProductStatus = AJService.GetDataFromAPI('ProductMaster/MultipleProductDetails?Ids=' + $("#hid_productIds").val() + '&SeriesCode=&For=series');
            ProductStatus.then(function (msg) {
                $scope.ProductDetailsList_BySeries = msg.data;

            }, function () {
                //alert('Error in getting Product list');
            });
        }
    };

    /******************************************************************************
   *******************************************************************************
   Created By  :  Dheeraj Kumar Sharma
   Created on  : 06/06/2016
   Created For : Bind the contact type drop down 
  
   *******************************************************************************
   *******************************************************************************/
    $scope.getContractType = function () {
        var ContactList = AJService.GetDataFromAPI("AuthorContact/getAuthorContract", null);
        ContactList.then(function (ContactList) {
            $scope.ContactTypeList = ContactList.data.query;
            $scope.contractType = [];
        }, function () {
            //alert('Error in getting Contract List');
        });
    }

    /******************************************************************************
    *******************************************************************************
    Created By  :  Dheeraj Kumar Sharma
    Created on  : 06/06/2016
    Created For : bind the type of copyrights

    *******************************************************************************
    *******************************************************************************/
    $scope.gettermsofCopyright = function () {
        var copyrightstype = AJService.GetDataFromAPI("AuthorContact/GetCopyrightsType", null);
        copyrightstype.then(function (copyrightstype) {
            $scope.termsofCopyright = copyrightstype.data.query;
        }, function () {
            //alert('Error in getting Contract List');
        });
    }

    /******************************************************************************
        *******************************************************************************
        Created By  :  Dheeraj Kumar Sharma
        Created on  : 06/06/2016
        Created For : Get the period cycle
   
        *******************************************************************************
        *******************************************************************************/
    $scope.getPeriodofAgreement = function () {
        var PeriodofAgreement = AJService.GetDataFromAPI("AuthorContact/GetPaymentType", null);
        PeriodofAgreement.then(function (PeriodofAgreement) {
            $scope.listPeriodofAgreement = PeriodofAgreement.data.query;

        }, function () {
            //alert('Error in getting Contract List');
        });
    }


    /******************************************************************************
       *******************************************************************************
       Created By  :  Dheeraj Kumar Sharma
       Created on  : 07/28/2016
       Created For :fetch the seriesid based on product id
  
       *******************************************************************************
       *******************************************************************************/
    $scope.getSeriesDetails = function () {
        if ($("#hid_productIds").val() == "") {
            return false;
        }
        var getSeriesDetails = AJService.GetDataFromAPI("ProductMaster/GetSeriesDetails?ProductId=" + $("#hid_productIds").val().split(",")[0], null);
        getSeriesDetails.then(function (getSeriesDetails) {
            $scope.getSeriesDetails = getSeriesDetails;
            $scope.SeriesId = getSeriesDetails.data.SeriesId;
            $scope.SeriesName = getSeriesDetails.data.SeriesName;

        }, function () {
            //alert('Error in getting Series Details');
        });
    }


    /******************************************************************************
       *******************************************************************************
       Created By  :  Dheeraj Kumar Sharma
       Created on  : 06/06/2016
       Created For : Get the territory Right
  
       *******************************************************************************
       *******************************************************************************/
    $scope.getTerritoryRightsList = function () {
        var TerritoryRightsList = AJService.GetDataFromAPI("AuthorContact/getTerriteryRights", null);
        TerritoryRightsList.then(function (TerritoryRightsList) {
            $scope.TerritoryList = TerritoryRightsList.data.query;
        }, function () {
            //alert('Error in getting Contract List');
        });
    }
    /******************************************************************************
       *******************************************************************************
       Created By  :  Dheeraj Kumar Sharma
       Created on  : 06/06/2016
       Created For : Get the subsidiaryList Right
  
       *******************************************************************************
       *******************************************************************************/
    $scope.getSubsidiaryList = function () {
        var subsidiaryList = AJService.GetDataFromAPI("AuthorContact/getSubsidiaryList", null);
        subsidiaryList.then(function (subsidiaryList) {
            $scope._subsidiaryList = subsidiaryList.data.query;
        }, function () {
            //alert('Error in getting subsidiary List');
        });
    }


    /******************************************************************************
       *******************************************************************************
       Created By  : Dheeraj Kumar Sharma
       Created on  : 06/06/2016
       Created For : Create table subsidiary right online
  
       *******************************************************************************
       *******************************************************************************/
    $scope.CreateDynamicTable = function (TotalNumberOfAuthor) {
        $scope.TblList = [];
        $scope.AuthorBox = [];
        if (TotalNumberOfAuthor == 0) {
            return false;
        }
        for (i = 0; i < TotalNumberOfAuthor; i++) {
            $scope.TblList.push(i);
            $scope.AuthorBox.push(i);
        }
        setTimeout(function () {
            if ($("#hid_ContractId").val() == 0) {
                $("select[name*=PaymentPeriodType]").val(5);
            }
        }, 1000)

        //set null value during change count of Author
        $scope.AuthorValue = [];
    }


    //---start added by prakash on 25 May, 2017
    $scope.get_Author = function () {
        var mint_ddl_length = $('.fetch_Author_Name').length;
        for (var i = 1; i <= mint_ddl_length; i++) {
            if ($('#Author_Name_' + i).val() != '') {
                //alert($('#Author_Name_' + i + ' option:selected').text());
                $('#total_Author_'+i).html('( ' + $('#Author_Name_' + i + ' option:selected').text().trim() + ' )');
            }
            else {
                $('#total_Author_' + i).html('');
            }
        }
    }

    setTimeout(function () {
        if ($('#Author_Name_1').val() != '') {
            $('#total_Author_1').html('( ' + $('#Author_Name_1 option:selected').text().trim() + ' )');
        }
    }, 5000);
    //---end by prakash
    
    /******************************************************************************
      *******************************************************************************
      Created By  :  Dheeraj Kumar Sharma
      Created on  : 07/06/2016
      Created For : calculation of percentage
 
      *******************************************************************************
      *******************************************************************************/
    $scope.CalculateLast = function (Index, Total) {
        if (Index + 1 == Total) {
            return false;
        }
    }

    /******************************************************************************
      *******************************************************************************
      Created By  :  Dheeraj Kumar Sharma
      Created on  : 07/06/2016
      Created For : Enable or Disable textbox of subsidiary Rights
 
      *******************************************************************************
      *******************************************************************************/
    //$scope.Percentage = function (TotalPercentage, RowNumber) {
    //    if ($scope.TblList.length == 1) {
    //        $($("#tblsubsidiary").find('tr')[RowNumber + 1]).find('#AuthorPercentage_0').val(TotalPercentage)
    //    }
    //}

    /******************************************************************************
      *******************************************************************************
      Created By  :  Dheeraj Kumar Sharma
      Created on  : 07/06/2016
      Created For : Supply material By Author

      *******************************************************************************
      *******************************************************************************/
    $scope.getSupplyMaterialList = function () {
        var _subsidiaryList = AJService.GetDataFromAPI("AuthorContact/getSupplyMaterialList", null);
        _subsidiaryList.then(function (_subsidiaryList) {
            $scope.SupplyMaterialList = _subsidiaryList.data.query;
        }, function () {
            //alert('Error in getting SupplyMaterial List');
        });
    }

    /******************************************************************************
     *******************************************************************************
     Created By  :  Dheeraj Kumar Sharma
     Created on  : 07/06/2016
     Created For : Supply material By Author

     *******************************************************************************
     *******************************************************************************/
    $scope.getCurrencyList = function () {
        var _CurrencyList = AJService.GetDataFromAPI("AuthorContact/getCurrencyList", null);
        _CurrencyList.then(function (_CurrencyList) {
            $scope.CurrencyList = _CurrencyList.data.query;

            setTimeout(function () {
                if ($("#hid_currencyid").val() != "" && $("#hid_ContractId").val() == "0" && $("#hid_currencyid").val() != undefined) {
                    $scope.CurrencyValue = $("#hid_currencyid").val();
               
                        $("select[name*=Currency]").val($("#hid_currencyid").val());
                        $("select[name*=Currency]").prop('disabled', true);
               
                }
            }, 2000);

        }, function () {
            //alert('Error in getting Currency List');
        });
    }

    /******************************************************************************
    *******************************************************************************
    Created By  :  Dheeraj Kumar Sharma
    Created on  : 07/06/2016
    Created For : get Menu script Delivery Format

    *******************************************************************************
    *******************************************************************************/
    $scope.getMenuScriptDeliveryFormat = function () {
        var DeliveryFormat = AJService.GetDataFromAPI("AuthorContact/getMenuScriptDeliveryFormat", null);
        DeliveryFormat.then(function (DeliveryFormat) {
            $scope._DeliveryFormat = DeliveryFormat.data.query;

        }, function () {
            //alert('Error in getting Delivery Format List');
        });
    }
    /******************************************************************************
   *******************************************************************************
   Created By  :  Dheeraj Kumar Sharma
   Created on  : 07/06/2016
   Created For : Javascript function to add more authors

   *******************************************************************************
   *******************************************************************************/
    $scope.AddMoreAuthor = function () {
        var i = $scope.AuthorBox.length + 1;
        $scope.AuthorBox.push(i);
        $(".RoyaltySlabLink").css("display", "none");
        $scope.RoyaltySlabManagement();
        //$('.removeIcon').show();
        //$($('.removeIcon')[0]).hide();
    }

    /******************************************************************************
  *******************************************************************************
  Created By  :  Dheeraj Kumar Sharma
  Created on  : 07/06/2016
  Created For : Calculate the expiry

  *******************************************************************************
  *******************************************************************************/
    ////------commented by prakash on 30 may, 2017
    //$scope.CalculateExpiry = function (PeriodIdValue) {
    //    if (PeriodIdValue == undefined || $("input[type=text][id*=AgreementDate]").val() == "") {
    //        $scope.ExpiryDate = "";
    //        return false;
    //    }

    //    var CurrentDate = new Date(convertDateDDMMYYYY(convertDate($("input[type=text][id*=AgreementDate]").val())));
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
    //    $scope.ExpiryDate = today;
    //}


    $scope.AuthorListProductBased = function (ProductId) {
        if (ProductId == undefined && $("#hid_productIds").val() == "") {
            return false;
        }

        ProductId = $("#hid_productIds").val() != "" ? $("#hid_productIds").val() : ProductId;
        var getAuthorList = AJService.GetDataFromAPI("AuthorContact/getAuthorList?ProductId=" + ProductId);
        getAuthorList.then(function (Author) {
            $scope.AuthorListProduct = Author.data;
            if (Author.data.length == 1) {
                $scope.AuthorValue[0] = Author.data[0].Id;
                setTimeout(function () {
                    $("select[name*=AuthorName]").val(Author.data[0].Id);
                    $scope.UpdateAuthor($("select[name*=AuthorName]").val(), "../../Master/Master/AuthorMaster");
                }, 1000)
            }

        }, function () {
            //alert('Error in getting author list');
        });
    }


    $scope.RemoveParentBox = function () {

        $scope.AuthorBox.pop($scope.AuthorBox[$scope.AuthorBox.length - 1]);
    }
    $scope.GetValue = function (obj) {
        $scope.SupplyMaterialByAuthor = obj;

        setTimeout(function () {
            $scope.removeValidation();
        }, 100)
    }

    $scope.ManuScriptFormatList = [];
    $scope.getMenuScript = function (obj) {
        $scope.ManuScriptFormatList = obj;

    }


    $scope.removeValidation = function () {
        $('input[name*=SupplyMaterialByAuthordate]').each(function () {
            var obj = $(this);
            if (obj.val() != "") {
                obj.closest(".form-group").removeClass("has-error");
                obj.closest('div').next().find('p').removeClass('ng-show').addClass("ng-hide");
                obj.removeAttr("required");
            }
            else {
                obj.attr("required", "true");
                obj.closest(".form-group").addClass("has-error");
                obj.closest('div').next().find('p').addClass('ng-show').removeClass("ng-hide");
            }
        });
    }
    /******************************************************************************
      *******************************************************************************
      Created By  :  Dheeraj Kumar Sharma
      Created on  : 10th june 2016
      Created For : function to set the url in author master

      *******************************************************************************
      *******************************************************************************/
    $scope.UpdateAuthor = function (AuthorId, URL) {

        if (AuthorId == undefined || AuthorId == 0) {
            $scope.show = 0;
            return false;
        }
        else {
            $('#theModal').find('iframe').attr("src", URL + "?id=" + AuthorId);
        }
        $scope.show = 1;
        $('[name*=AuthorName]').each(function () {

            if ($(this).val() == "") {
                $(this).closest(".ParentClass").next().css("display", "none");
            }
            else {

                $(this).closest(".ParentClass").next().css("display", "block");
            }
        });

    }
    /******************************************************************************
     *******************************************************************************
     Created By  :  Dheeraj Kumar Sharma
     Created on  : 10th june 2016
     Created For : function to open author master in update mode

     *******************************************************************************
     *******************************************************************************/
    $scope.OpenAuthorUpdate = function (AuthorId) {
        if (AuthorId == undefined || AuthorId == 0) {
            return false;
        }
        var iframe = $('#theModal').find('iframe').contents();
        $(iframe).find(".top-header").remove();
        $(iframe).find(".menu").remove();
        $(iframe).find("footer").remove();
        $(iframe).find("#FrmDisplay").remove();
        $(iframe).find(".main-title").remove();
        //$(iframe).find(".tools").has('a[href^="/RMS/Master/Master/AuthorSearch?For=BackToSearch"]').remove();
        $(iframe).find(".tools").has('a[href^="/Master/Master/AuthorSearch?For=BackToSearch"]').replaceWith("<input type='hidden' id='hid_updateid' name='hid_updateid' ng-model='hid_updateid' value='hid_updateid' >");
        $('#theModal').modal('show');

    }


    $scope.addContributorrow = function () {
        var i = $scope.ContractNameList.length + 1;
        $scope.ContractNameList.push(i);

    };
    $scope.SubProductTypeList = function () {
        var SubProductTypeList = AJService.GetDataFromAPI("CommonList/getSubProductTypeList", null);
        SubProductTypeList.then(function (SubProductTypeList) {
            $scope.SubProductTypeList = SubProductTypeList.data.subProductData;
        }, function () {
            //alert('Error in getting SubProduct Type List');
        });
    }


    /******************************************************************************
    *******************************************************************************
    Created By  :  Dheeraj Kumar Sharma
    Created on  : 10th june 2016
    Created For : Save the data into the database for author contract

    *******************************************************************************
    *******************************************************************************/
    $scope.ContributorNamevalid = true;
    $scope.SubmitForm = function () {

        $scope.removeValidation();
        //if ($('form[name$=userForm]').find(".has-error").length > 0) {
        //    return false;
        //}

        $('.contributor').not(":first").removeClass("has-error");
        $scope.validateAuthorBox();

        //var obj = 0;
        //$('input[name$=SupplyMaterialByAuthordate]').each(function () {
        //    var date1 = new Date(convertDate($(this).val()));
        //    var date2 = new Date($("#hid_ProjectedPublishingDate").val());
        //    if (date1 > date2) {
        //        SweetAlert.swal("Validation!", "Material date submission should be less than project publishing date( " + convertDateDDMMYYYY(new Date($("#hid_ProjectedPublishingDate").val())) + ").", "info");
        //        $(this).closest("div").next().find("p").removeClass("ng-show").addClass("ng-hide");
        //        $(this).closest('.form-group').removeClass("has-error");
        //        obj = 1;
        //        return false;
        //    }
        //    if ($(this).val() == "") {
        //        $(this).closest("div").next().find("p").addClass("ng-show").removeClass("ng-hide");
        //        $(this).closest('.form-group').addClass("has-error");
        //    }
        //    else {
        //        $(this).closest("div").next().find("p").removeClass("ng-show").addClass("ng-hide");
        //        $(this).closest('.form-group').removeClass("has-error");
        //    }

        //});
        //if (obj == 1) {
        //    return false;
        //}

        if (ValidateRoyaltySlab() == 1) {
            $scope.userForm.$valid = false;
            return false;
        }
        var count = 0;
        if ($('#tblsubsidiary').is(':visible') == true && $scope.userForm.$valid == true) {
            $('#tblsubsidiary').find('tr:not(:has(th))').each(function () {
                var _ttlOupPer = $(this).find('input[type=number][id*=totalpercentage]').val()==""? 0: parseFloat($(this).find('input[type=number][id*=totalpercentage]').val());
                var _tr = $(this);

                var totalAuthorPercentage = 0;
                if (_ttlOupPer == "") {
                    count++;
                    return true;
                }
                _tr.find('input[id*=AuthorPercentage]').each(function () {
                    if ($(this).val() == "") {
                        SweetAlert.swal("Validation!", "Please enter author percentage", "info");
                        //$(_tr).css("border-color", "#a94442");
                        $(_tr).addClass("has-error");
                    }

                    totalAuthorPercentage = totalAuthorPercentage + parseFloat($(this).val());
                });

                if (_ttlOupPer!=0 && parseFloat(totalAuthorPercentage + _ttlOupPer)!=100) {
                    SweetAlert.swal("Validation!", "Sum of author(s) and OUP percentage should be 100 ", "info");
                    //$(_tr).css("border-color", "#a94442");
                    $(_tr).addClass("has-error");
                    return false;
                }


            });
        }
        if ($scope.userForm.$valid == true && $('#tblsubsidiary').is(':visible') == true && count == $('#tblsubsidiary').find('tr:not(:has(th))').length) {
            SweetAlert.swal("Validation!", "Please enter atleaset one subsidiary rights", "info");
            //$(_tr).css("border-color", "#a94442");
            $($('#tblsubsidiary').find('tr:not(:has(th))')[0]).addClass("has-error");
            return false;
        }

        if ($('input[type=radio][name*=AgreementStatus]:checked').val() == "Issued") {
            if ($('input[type=text][name*=AgreementDate]').val() == "") {
                $('input[type=text][name*=AgreementDate]').closest(".form-group").addClass("has-error");
                $('input[type=text][name*=AgreementDate]').closest("div").next().find("p").removeClass("ng-hide");
                $('input[type=text][name*=AgreementDate]').closest("div").next().find("p").addClass("ng-show");
            }
            else {
                $('input[type=text][name*=AgreementDate]').closest(".form-group").removeClass("has-error");
                $('input[type=text][name*=AgreementDate]').closest("div").next().find("p").addClass("ng-hide");
                $('input[type=text][name*=AgreementDate]').closest("div").next().find("p").removeClass("ng-show")
            }
            if ($('#dropZone1').find('.fileNameClass').length == 0 && $scope.AgreementId == undefined) {
                SweetAlert.swal("Validation !", "Please upload contract", "info");
                $scope.userForm.$valid = false;
                return false;
            }
        }
         

        if ($('input[type=radio][name*=ContributorAgreement][value=Yes]').is(":checked")) {
            if ($('#dropZone2').find('.fileNameClass').length == 0 && $scope.AgreementId == undefined) {
                SweetAlert.swal("Validation !", "Please upload file for contributor", "info");
                $scope.userForm.$valid = false;
                return false;
            }
        }

        /***************************************************************************************************
             This section will validate that user should fill either max or min page or max/min words   
        **************************************************************************************************/

        $scope.validateMinMax();


        /***************************************************************************************************
           End Section dheeraj sharma 
       **************************************************************************************************/

        $('input[name$=ContributorName]').each(function (index, values) {
            if ($(this).val() != "") {
                $('input[name$=ContributorName]').parent().removeClass('has-error');
                $scope.ContributorNamevalid = false;
            }
        });

        
        if ($('input[name*=MinWord]').val() == "" && $('input[name*=MaxWord]').val() == "" && $('input[name*=MinNoOfPages]').val() == "" && $('input[name*=MaxNoOfPages]').val() == "") {
            SweetAlert.swal("Validation !", "Please enter either Min/Max no of words or Min/Max no of pages", "info");
            $('input[name*=MinWord],input[name*=MaxWord],input[name*=MinNoOfPages],input[name*=MaxNoOfPages]').closest(".form-group").addClass("has-error");
            $scope.userForm.$valid = false;
        }
        else {
            //$scope.userForm.$valid = true;
            $('input[name*=MinWord],input[name*=MaxWord],input[name*=MinNoOfPages],input[name*=MaxNoOfPages]').closest(".form-group").removeClass("has-error");

            if ($('form[name$=userForm]').find(".has-error:visible").length > 0) {
                $scope.userForm.$valid = false;
            }
            else {
                $scope.userForm.$valid = true;
            }
        }
        
        $scope.submitted = true;
        if ($scope.userForm.$valid) {
            $scope.AuthorContract();
            $scope.userForm.$setPristine();
            // set form is no submitted
            $scope.submitted = false;
            return;

        }
        else {
            return false;
        }
    };

    //function ComparisionOfDate(obj) {
    //    _crrDate = $(obj).val();
    //    var date1 = new Date(convertDate(_crrDate));
    //    var date2 = new Date($("#hid_ProjectedPublishingDate").val());
    //    if (date1 > date2) {
    //        // obj.attr("required", "true");
    //        obj.closest(".form-group").addClass("has-error");
    //        // obj.closest('div').next().find('p').addClass('ng-show').removeClass("ng-hide");
    //        alert("Material date submission should be less than project publishing date.");
    //        return 1;
    //    }

    //}

    function convertDateDDMMYYYY(inputFormat) {
        function pad(s) { return (s < 10) ? '0' + s : s; }
        var d = new Date(inputFormat);
        return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
    }


    /******************************************************************************
      *******************************************************************************
      Created By  :  Dheeraj Kumar Sharma
      Created on  : 14th june 2016
      Created For : fetch the data for inserting from html control

      *******************************************************************************
  *******************************************************************************/
    var ManuScriptFormatList = [];
    $scope.AuthorContract = function () {


        FileNameArray = $('#dropZone0').find('.fileNameClass');
        var array = [];
        FileNameArray.each(function () {
            array.push(
                $(this).val()
            );
        });

        /*=============================================================================================================
       This array will used to get the material date and material id supplied by author
       ==============================================================================================================*/
        var SupplyMaterialbyAuthorInsert = [];
        if ($scope.SelectedSupplyMaterialByAuthor != undefined) {
            for (var i = 0; i < $scope.SelectedSupplyMaterialByAuthor.length; i++) {
                SupplyMaterialbyAuthorInsert[i] =
                {
                    MaterialId: $scope.SelectedSupplyMaterialByAuthor[i].Id,
                    materialDate: convertDate($("#SupplyMaterialByAuthordate_" + $scope.SelectedSupplyMaterialByAuthor[i].Id + "").val())
                }
            }
        }



        /*=============================================================================================================
       End of array
       ==============================================================================================================*/

        /*=============================================================================================================
        This array will used to insert data into the AuthorContractContributor for multiple contributor
        ==============================================================================================================*/
        var ContributorName = []
        $('input[name$=ContributorName]').each(function (index, values) {
            if ($(this).val() == "") {
                return true;
            }
            ContributorName[index] =
            {
                Contributor: $(this).val(),
            };

        });
        /*=============================================================================================================
        End of array
        ==============================================================================================================*/
        /*=============================================================================================================
         This array will used to get the data of multiple author against author contract
         ==============================================================================================================*/
        var AuthorArray = [];

        $(".AuthorBox").each(function (index, values) {
            var obj = $(this);
            var RoyalitySlab1 = [];
            var i = 0;
            $(obj).find('.RoyaltySlab tr:not(:has(th))').each(function () {
                if ($(this).find('select[name$=SubProductType]').val() == "") {
                    return true;
                }
                RoyalitySlab1[i] =
                {
                    AuthorId: $(obj).find('select[name*=AuthorName]').val(),
                    subproductTypeId: $(this).find('select[name$=SubProductType]').val(),
                    CopiesFrom: $(this).find('input[name$=CopiesFrom]').val(),
                    CopiesTo: $(this).find('input[name$=CopiesTo]').val(),
                    Percentage: $(this).find('input[name$=RyPercentage]').val(),

                }
                i++;
            });
            AuthorArray[index] =
            {
                AuthorTypeId: $(obj).find('select[name*=AuthorType]').val(),
                AuthorId: $(obj).find('select[name*=AuthorName]').val(),
                PaymentperiodId: $(obj).find('select[name*=PaymentPeriodType]').val(),
                AuthorCopies: $(obj).find('input[name*=AuthorCopies]').val(),
                SendMoney: $(obj).find('input[name*=SendMoney]').val(),
                OneTimePayment: $(obj).find('input[name*=OneTimePayment]').val(),
                AdvanceRoyalty: $(obj).find('input[name*=AdvanceRoyalty]').val(),
                ContractTypeId: $(obj).find('select[name$=ContactType]').val(),
                RoyaltySlab: RoyalitySlab1
            }
        });

        for (var i = 0; i < $scope.ManuScriptFormatList.length; i++) {
            ManuScriptFormatList[i] = {
                MenuScriptId: $scope.ManuScriptFormatList[i].Id
            }

        }

        /*=============================================================================================================
        End of array
        ==============================================================================================================*/

        /*=============================================================================================================
        This array will used to get the data of multiple author against author contract
        ==============================================================================================================*/

        var AllAuthorSusidiaryRights = [];
        var i = 0;
        var counts = 0;
        if ($("input[type=radio][name*=SubsidiaryRequired]:checked").val() == "1") {
            $('select[name$=AuthorName]').each(function () {
                var SusidiaryRights1 = [];
                var authorId1 = $(this).val();
                $('#tblsubsidiary').find('tr:not(:has(th))').each(function (index, value) {
                    var subsidiaryid = $(this).find('input[type=hidden][name$=hid_subsidiaryId]').val();
                    var totalPercentage = $(this).find('input[name$=totalPercentage]').val();
                    var authorPercentage = $(this).find('.author_' + i + '').val();
                    var authorId = authorId1;
                    if (totalPercentage == "") {
                        return true;
                    }
                    SusidiaryRights1[counts] =
                    {
                        subsidiaryid: subsidiaryid,
                        authorId: authorId,
                        Percentage: parseFloat(authorPercentage),
                        OupPercentage: parseFloat(totalPercentage)
                    }
                    counts++;
                });
                AllAuthorSusidiaryRights[i] = {
                    SusidiaryRights: SusidiaryRights1
                };
                counts = 0;
                i++;
            });
        }
        /*************************************************************************************************
            Section for inserting multiple menuscript delivery format for contract(Later Enhancement)(22nd Aug 2016)
         ***************************************************************************************************/

        //debugger;
        ////--- Third party permission add in contract table
        //var _hid_ThirdPartyPermsionLength = $('#hid_ThirdPartyPermsionLength').val()

        //if (_hid_ThirdPartyPermsionLength != "" && _hid_ThirdPartyPermsionLength != undefined)
        //{
        //    var _ThirdPartyPermission = $('[name*=ThirdPartyPermission_]');
        //    $scope.ThirdPartyPermission_value = "";
        //    if (_ThirdPartyPermission.length > 0) {

        //        for (var i = 0; i < _hid_ThirdPartyPermsionLength; i++) {
        //            if ($('[name*=ThirdPartyPermission_' + [i] + ']').is(':checked') == true) {
        //                if ($('[name*=ThirdPartyPermission_' + [i] + ']').val() != undefined && $('[name*=ThirdPartyPermission_' + [i] + ']').val() != "") {
        //                    $scope.ThirdPartyPermission_value += $('[name*=ThirdPartyPermission_' + [i] + ']:checked').val() + ",";
        //                }


        //            }

        //        }
        //    }
        //} else {
        //    var _ThirdPartyPermission = $('[name*=ThirdPartyPermission_]');
        //    $scope.ThirdPartyPermission_value = "";
        //    if (_ThirdPartyPermission.length > 0) {

        //        for (var i = 0; i < _ThirdPartyPermission.length; i++) {
        //            if ($('[name*=ThirdPartyPermission_' + [i] + ']').is(':checked') == true) {
        //                if ($('[name*=ThirdPartyPermission_' + [i] + ']').val() != undefined && $('[name*=ThirdPartyPermission_' + [i] + ']').val() != "") {
        //                    $scope.ThirdPartyPermission_value += $('[name*=ThirdPartyPermission_' + [i] + ']:checked').val() + ",";
        //                }


        //            }

        //        }
        //    }
        //}
       

        /*=============================================================================================================
       End of array
       ==============================================================================================================*/
        var AuthorContractObject = {
            Id: $scope.contractId,
            ProductId: $("#hid_Productlicensecode").val() != undefined && $("#hid_Productlicensecode").val() != "" ? $("#hid_productidValue").val() : $('#hid_productid_AC').val(),
            ExecutiveCode: $scope.ByExecutive,
            // ContractTypeId: $scope.contractType,
            ContractEntryDate: convertDate($('#Entrydate').val()),
            ContractDate: convertDate($("input[id$=ContractDate]").val()),
            NoofAuthors: $('input[name$=NoofAuthors]').val(),
            termsofcopyright: $scope.TemsOfCopyRight,
            periodOfAgreement: 0, // $('input[name$=PeriodOfAgreement]').val(), //change by prakash on 30 May, 2017
            ContractExpirydate: $scope.TemsOfCopyRight == 1 ? convertDate($("input[name$=ExpiryDate]").val()) : null,
            BuyBack: $("input[type=radio][name*=BuyBack]:checked").val() == undefined ? null : $("input[type=radio][name*=BuyBack]:checked").val(),
            NatureofWork: $scope.NatureofWork,
            CopyRightOwner: $scope.CopyRightOwner,
            TerritoryId: $scope.Territoryrights,
            //ThirdPartyPermission: $scope.ThirdPartyPermission,
            //ThirdPartyPermission: $scope.userForm.ThirdPartyPermission.$modelValue,
            ThirdPartyPermission: 0, // $scope.ThirdPartyPermission_value.slice(0, -1),

            Amendment: $scope.Amendment,
            AmendmentRemarks: $('textarea[name*=AmendmentRemarks]').val(),
            Restriction: $scope.Restriction,
            subjectMatterAndTreatment: $scope.SubjectMatters,
            MinNoOfWords: $scope.MinWords,
            MaxNoOfWords: $scope.MaxWords,
            MinNoOfPages: $scope.MinNoOfPages,
            MaxNoOfPages: $('[name$=MaxNoOfPages]').val(),
            PriceType: $('[name$=PriceType]:checked').val(),
            Price: $scope.ProductPrice,
            CurrencyId: $("select[name*=Currency]").val() != "" ? $("select[name*=Currency]").val() : null,
            mediumOfDelivery: $('[name$=MediumofDelivery]:checked').val(),
            MenuScriptDeliveryFormatId: null,
            deliverySchedule: $scope.deliverySchedule,
            ProductRemarks: $('textarea[name*=ProductRemarks]').val(),
            ContributorName: ContributorName,
            AuthorContactDetails: AuthorArray,
            SupplyMaterialbyAuthor: SupplyMaterialbyAuthorInsert,
            AuthorSubsidiaryRights: AllAuthorSusidiaryRights,
            EnteredBy: $('#enterdBy').val(),
            licenseId: $("#hid_Productlicensecode").val(),
            SeriesIds: $("#hid_productIds").val(),
            SeriesId: $scope.SeriesId,
            SeriesCode: $("#hid_SeriesCode").val(),
            ManuScriptFormatList: ManuScriptFormatList,
            DocumentName: array,
            UploadFile: $("#hid_Uploads").val()

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

             //debugger;

             var data = AJService.PostDataToAPI("AuthorContact/InsertAuthorContractDetails", AuthorContractObject);
             if ($scope.contractId != 0 || $("#hid_SeriesCode").val() != "") {
                 if ($('#chk_AgreementYesorNo').prop("checked") == false) {
                     $scope.ContractAgreement();
                 }
             }
             data.then(function (data) {
                 if (data.data.indexOf("OK") >= 0) {
                     var text = "";
                     if ($scope.contractId == 0 && $("#hid_SeriesCode").val() == "") {
                         var text = data.data.split(',')[1].substring(0, 2).toUpperCase() == "SR" ? "Series Contract" : "Assignment Contract";
                         SweetAlert.swal({
                             title: "Success",
                             text: text + " Insert successfully.\n  " + text + " Code " + data.data.split(',')[1] + "",
                             type: "success"
                         },
                        function () {
                            setTimeout(function () {
                                //location.href = $(".backtolist").find("a").attr("href");
                                if ($("#hid_SeriesCode").val() != "")
                                {
                                    location.href = GlobalredirectPath + "Contract/AuthorContract/view?SeriesCode=" + data.data.split(',')[1] + "&For=View";
                                }
                                else if ($("#hid_productIds").val() != "") {
                                    location.href = GlobalredirectPath + "Contract/AuthorContract/view?SeriesCode=" + data.data.split(',')[1] + "&For=View";
                                }
                                else
                                {
                                    location.href = GlobalredirectPath + "Contract/AuthorContract/view?Id=" + data.data.split(',')[2] + "&For=View";
                                }
                                
                                //location.href = "../../Home/Dashboard/Dashboard";
                            }, 2000);

                        });

                     }
                     else {
                        // $scope.getListAuthorContractStatusMail($("#hid_ContractId").val());
                         text = $("#hid_SeriesCode").val() != "" ? "Series Contract" : "Assignment Contract";
                         SweetAlert.swal({
                             title: "Success",
                             text: text + " details updated successfully.",
                             type: "success"
                         },
                       function () {
                           setTimeout(function () {
                               if ($("#hid_SeriesCode").val() != "")
                               {
                                   location.href = GlobalredirectPath + "Contract/AuthorContract/view?SeriesCode=" + $("#hid_SeriesCode").val() + "&For=View";
                               }
                               else if ($("#hid_productIds").val() != "") {
                                   location.href = GlobalredirectPath + "Contract/AuthorContract/view?SeriesCode=" + $("#hid_SeriesCode").val() + "&For=View";
                               }
                               else
                               {
                                   //location.href = $(".backtolist").find("a").attr("href");
                                   location.href = GlobalredirectPath + "Contract/AuthorContract/view?Id=" + $("#hid_ContractId").val() + "&For=View";
                                   //location.href = "../../Home/Dashboard/Dashboard";
                               }
                           }, 2000)

                       });
                     }
                 }
                 else {
                     SweetAlert.swal("Error!", data.status, "error");

                 }
             }, function () {
                 SweetAlert.swal("Error!", "Oops !! Something went worng. Please try again.", "error");
             });


         }

     });

    };



    function Getdate() {
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
        return today;
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
    function DDMMYYYY(date) {
        var datearray = date.split("/");
        return datearray[1] + '/' + datearray[0] + '/' + datearray[2];
    }



    $scope.ValidateMe = function () {
        var obj = $(event.target);

        if (obj.val() == "") {
            obj.closest(".form-group").addClass("has-error");
            obj.next().find("p").addClass("ng-show");
        }
        else {
            obj.closest(".form-group").removeClass("has-error");
            obj.next().find("p").addClass("ng-hide");
        }
    }
    $scope.PublisherPercentageArray = [];

    
    //Comment by Ankush this code is not working on IE
    //$scope.ValidatePercentage = function () {
    //    var obj = $(event.target);
    //    $(obj).closest('tr').removeClass("has-error");
    //    var ttlValue = 0;
    //    $(obj).closest("tr").find("input[type=number][id*=AuthorPercentage]").each(function () {
    //        if ($(this).val() != "") {
    //            ttlValue = ttlValue + parseFloat($(this).val());
    //        }
    //        else {
    //            return true;
    //        }
    //    });
    //    if ($(obj).closest("tr").find("input[type=number][id*=totalpercentage]").val() != "") {
    //        ttlValue = ttlValue + parseFloat($(obj).closest("tr").find("input[type=number][id*=totalpercentage]").val());
    //    }
    //    obj.closest('tr').find('[id*=AuthorPrePercentageTextbox]').val(ttlValue);
    //    $scope.PublisherPercentageArray[obj.closest('tr').attr("srno")] = ttlValue;
    //    $("#hid_oupPercentage_" + obj.closest('tr').attr("srno") + "").val(ttlValue);
    //    $scope.FirstTime = 1;
    //    //var obj = $(event.target);
    //    //$(obj).closest('tr').removeClass("has-error");
    //    //obj.closest('tr').find('input[id*=AuthorPercentage]').val("");
    //    //obj.closest('tr').find('[id*=OupPercentage_]').remove();
    //    ////$(obj).closest('tr').find(".has-error").removeClass("has-error");
    //    //$("#hid_oupPercentage_" + parseFloat($(obj).attr("indexNo")) + "").val(100 - parseFloat($(obj).val()));
    //    //$scope.PublisherPercentageArray[parseFloat($(obj).attr("indexNo"))] = (100 - parseFloat($(obj).val()));
    //    validateCommon(obj, 1);

    //}


    $scope.ValidatePercentageAuthor = function (obj) {
        $(obj).closest('tr').removeClass("has-error");
        var ttlValue = 0;
        $scope.FirstTime = 1;
        $(obj).closest("tr").find("input[type=number][id*=AuthorPercentage]").each(function () {
            if($(this).val()!="")
            {
                ttlValue = ttlValue + parseFloat($(this).val());
            }
            else
            {
                return true;
            }
        });
        if($(obj).closest("tr").find("input[type=number][id*=totalpercentage]").val()!="")
        {
            ttlValue = ttlValue + parseFloat($(obj).closest("tr").find("input[type=number][id*=totalpercentage]").val());
        }
       
        obj.closest('tr').find('[id*=AuthorPrePercentageTextbox]').val(ttlValue);
        $scope.PublisherPercentageArray[obj.closest('tr').attr("srno")] = ttlValue;
        $("#hid_oupPercentage_" + obj.closest('tr').attr("srno") + "").val(ttlValue);
    }


    //Added By Ankush
    $scope.ValidatePercentage = function (Id) {
        var obj = $('#'+ Id.id);
        $(obj).closest('tr').removeClass("has-error");
        var ttlValue = 0;
        $(obj).closest("tr").find("input[type=number][id*=AuthorPercentage]").each(function () {
            if ($(this).val() != "") {
                ttlValue = ttlValue + parseFloat($(this).val());
            }
            else {
                return true;
            }
        });
        if ($(obj).closest("tr").find("input[type=number][id*=totalpercentage]").val() != "") {
            ttlValue = ttlValue + parseFloat($(obj).closest("tr").find("input[type=number][id*=totalpercentage]").val());
        }
        obj.closest('tr').find('[id*=AuthorPrePercentageTextbox]').val(ttlValue);
        $scope.PublisherPercentageArray[obj.closest('tr').attr("srno")] = ttlValue;
        $("#hid_oupPercentage_" + obj.closest('tr').attr("srno") + "").val(ttlValue);
        $scope.FirstTime = 1;
        //var obj = $(event.target);
        //$(obj).closest('tr').removeClass("has-error");
        //obj.closest('tr').find('input[id*=AuthorPercentage]').val("");
        //obj.closest('tr').find('[id*=OupPercentage_]').remove();
        ////$(obj).closest('tr').find(".has-error").removeClass("has-error");
        //$("#hid_oupPercentage_" + parseFloat($(obj).attr("indexNo")) + "").val(100 - parseFloat($(obj).val()));
        //$scope.PublisherPercentageArray[parseFloat($(obj).attr("indexNo"))] = (100 - parseFloat($(obj).val()));
        validateCommon(obj, 1);

    }

    $scope.RemoveErrorClass = function () {
        $scope.FirstTime = 1;
        var obj = $(event.target);
        $(obj).closest('tr').removeClass("has-error");
        //$(obj).closest('tr').find(".has-error").removeClass("has-error");
        validateCommon(obj, 0);
    }

    function validateCommon(obj, from) {
        if (obj.val() == "") {
            return false;
        }
        else {

            if (obj.val() > 100) {
                obj.closest("td").addClass("has-error");
                obj.next().find("p").addClass("ng-show");
                obj.next().find("p").removeClass("ng-hide");
                obj.next().find("p").html("Percentage can't be greater than 100");
                obj.closest('td').next().find('input[id*=AuthorPercentage]').val("");
            }
            else if (obj.val() < 0) {
                obj.closest("td").addClass("has-error");
                obj.next().find("p").addClass("ng-show");
                obj.next().find("p").removeClass("ng-hide");
                obj.next().find("p").html("Percentage can't be zero");
                obj.closest('td').next().find('input[id*=AuthorPercentage]').val("");
            }
            else {

                obj.closest("td").removeClass("has-error");
                obj.next().find("p").addClass("ng-hide");
                obj.next().find("p").html("");
                if (from == 1) {
                    if ($scope.TblList.length == 1) {
                        obj.closest('td').next().find('input[id*=AuthorPercentage]').val(obj.val());
                    }
                }

            }
        }
    }

    function ValidateRoyaltySlab() {
        var returnstatus;
        $('.AuthorBox').find('.RoyaltySlab').each(function () {

            if (unique($(this).find("select[name*=SubProductType]").map(function () { return $(this).val() }).get())[0] == "" && $(this).closest('.AuthorBox').find("select[name*=ContactType]").find('option:selected').filter(":contains('Royalty')").length > 0) {
                SweetAlert.swal("validation", "Please enter atleaset one royalty slab", "info");
                $($(this).find("tr")[1]).addClass("has-error");
                returnstatus = true;
                return false;
            }

            var result = [];
            result = unique($(this).find("select[name$=SubProductType]").map(function () { return $(this).find("option:selected").text(); }).get())
            for (var i = 0; i < result.length; i++) {
                $(this).find("select[name*=SubProductType]").find('option:selected').filter(":contains('" + result[i] + "')").parents("tr").each(function (index, value) {
                    var _lastTr = $(".RoyaltySlab").find("select[name*=SubProductType]").find('option:selected').filter(":contains('" + result[i] + "')").parents("tr:last")
                    if ($(_lastTr).find('input[name*=CopiesTo]').val() != "") {

                        if ($(_lastTr).find('input[name*=CopiesTo]').val() != 9999999 && $(_lastTr).find('input[name*=CopiesTo]').val() != 999999) {
                            $scope.userForm.$valid = false;
                            SweetAlert.swal("Validation!", "Last Copies to should be blank !", "info");
                            $(_lastTr).addClass("has-error");
                        }

                    }
                    if ($(this).find('input[name=RyPercentage]').val() == "" && $(this).find("select[name*=SubProductType]").val() != "") {
                        $scope.userForm.$valid = false;
                        SweetAlert.swal("Validation!", "Please Enter Copies percentage !", "info");
                        $(this).addClass("has-error");
                        $scope.submitted = false;
                        returnstatus = true;
                        return false;
                    }
                });
            }
            if (returnstatus) {
                return 1;
            }
        });
        if (returnstatus) {
            return 1;
        }
    }

    $scope.RemoveError = function () {
        var obj = $(event.target);
        $(obj).closest('tr').removeClass("has-error");
    };
    function unique(list) {
        var result = [];
        $.each(list, function (i, e) {
            if ($.inArray(e, result) == -1) result.push(e);
        });
        return result;
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


    $scope.validateMinMax = function () {

        

        if ($('input[name*=MinWord]').val() == "" && $('input[name*=MaxWord]').val() == "" && $('input[name*=MinNoOfPages]').val() == "" && $('input[name*=MaxNoOfPages]').val() == "") {
            SweetAlert.swal("Validation !", "Please enter either Min/Max no of words or Min/Max no of pages", "");
            $('input[name*=MinWord],input[name*=MaxWord],input[name*=MinNoOfPages],input[name*=MaxNoOfPages]').closest(".form-group").addClass("has-error");
        }
        else {
            $('input[name*=MinWord],input[name*=MaxWord],input[name*=MinNoOfPages],input[name*=MaxNoOfPages]').closest(".form-group").removeClass("has-error");
        }

        if ($('input[name*=MinWord]').val() != "") {
            if ($('input[name*=MaxWord]').val() == "") {
                SweetAlert.swal("Validation", "Please enter max no of words", "info");
                $('input[name*=MaxWord]').closest(".form-group").addClass("has-error");
            }
            else {
                $('input[name*=MaxWord]').closest(".form-group").removeClass("has-error");
            }
        }

        if ($('input[name*=MaxWord]').val() != "") {
            if ($('input[name*=MinWord]').val() == "") {
                SweetAlert.swal("Error", "Please enter min no of words", "error");
                $('input[name*=MinWord]').closest(".form-group").addClass("has-error");
            }
            else {
                $('input[name*=MinWord]').closest(".form-group").removeClass("has-error");
            }
        }
        if ($('input[name*=MinNoOfPages]').val() != "") {
            if ($('input[name*=MaxNoOfPages]').val() == "") {
                SweetAlert.swal("Validation !", "Please enter max no of pages", "info");
                $('input[name*=MaxNoOfPages]').closest(".form-group").addClass("has-error");
            }
            else {
                $('input[name*=MaxNoOfPages]').closest(".form-group").removeClass("has-error");
            }
        }
        if ($('input[name*=MaxNoOfPages]').val() != "") {
            if ($('input[name*=MinNoOfPages]').val() == "") {
                SweetAlert.swal("Validation !", "Please enter min no of pages", "info");
                $('input[name*=MinNoOfPages]').closest(".form-group").addClass("has-error");
            }
            else {
                $('input[name*=MinNoOfPages]').closest(".form-group").removeClass("has-error");
            }
        }

        if (isNumber($('input[name*=MinWord]').val()) && isNumber($('input[name*=MaxWord]').val()) && parseInt($('input[name*=MinWord]').val()) > parseInt($('input[name*=MaxWord]').val()))
        {
            SweetAlert.swal("Validation !", " Max no of word must be greater than Min no of Words", "info");
            $('input[name*=MaxWord]').closest(".form-group").addClass("has-error");
            $('input[name*=MinWord],input[name*=MaxWord]').closest(".form-group").addClass("has-error");
            return false;
        }
        else
        {
            $('input[name*=MinWord],input[name*=MaxWord]').closest(".form-group").removeClass("has-error");
        }
        if (isNumber($('input[name*=MinNoOfPages]').val()) && isNumber($('input[name*=MaxNoOfPages]').val()) && parseInt($('input[name*=MinNoOfPages]').val()) > parseInt($('input[name*=MaxNoOfPages]').val())) {
            SweetAlert.swal("Validation !", "Max no of Pages must be greater than Min no of Pages", "info");
            $('input[name*=MinNoOfPages]').closest(".form-group").addClass("has-error");
            $('input[name*=MaxNoOfPages],input[name*=MinNoOfPages]').closest(".form-group").addClass("has-error");
            return false;
        }
        else {
            $('input[name*=MaxNoOfPages],input[name*=MinNoOfPages]').closest(".form-group").removeClass("has-error");
        }


    };

    /*Function for identifying whether number is numeric or not*/
    function isNumber(n) {
        return !isNaN(parseFloat(n)) && isFinite(n);
    }


    $scope.RemoveValidationOfMinMax = function () {
        $('input[name*=MinWord],input[name*=MaxWord],input[name*=MinNoOfPages],input[name*=MaxNoOfPages]').closest(".form-group").removeClass("has-error");
    };



    /*
    Create By   :   Dheeraj
    Created On  :   27/06/2016
    Created For :   To validate Author Box
    */

    $scope.validateAuthorBox = function () {
        var AuthorIdArr = [];
        var DuplicateArray = [];

        $('.AuthorBox').each(function () {

            AuthorIdArr.push($(this).find('select[name*=AuthorName]').val());

            if ($(this).find('select[name*=AuthorType]').val() == "") {
                SweetAlert.swal("Validation!", "Please select author type !", "info");
                $(this).find('select[name*=AuthorType]').closest(".form-group").addClass("has-error");
                $(this).next().find('p').addClass("ng-show").removeClass("ng-hide");
            }
            if ($(this).find('select[name*=AuthorName]').val() == "") {
                SweetAlert.swal("Validation!", "Please select author type !", "info");
                $(this).find('select[name*=AuthorName]').closest(".form-group").addClass("has- ");
                $(this).next().find('p').addClass("ng-show").removeClass("ng-hide");
            }

            //$(this).find(".authorContrractDetails").find("tr").not("thead tr").each(function () {

            //});

        });
        DuplicateArray = getDistinctArray(AuthorIdArr);
        if (DuplicateArray.length > 0) {
            SweetAlert.swal("Validation !", "You have selected duplicate authors !", "info");
            $("select[name*=AuthorName]").find('option[value="' + DuplicateArray[0] + '"]:selected').closest(".form-group").each(function () {
                $(this).addClass("has-error");
                $($(this).find(".help-block").find("p")[1]).addClass("ng-show").removeClass("ng-hide");
            })
        }
    };

    function getDistinctArray(arr) {
        var compareArray = new Array();
        var duplicateArray = new Array();
        if (arr.length > 1) {
            for (i = 0; i < arr.length; i++) {
                if (compareArray.indexOf(arr[i]) == -1) {
                    compareArray.push(arr[i]);
                }
                else {
                    duplicateArray.push(arr[i]);
                }

            }
        }
        return duplicateArray;
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
    $scope.contractType = [];

    $scope.GetAuthorContractDetails = function (Id) {
        var _ContractDetails = AJService.GetDataFromAPI("AuthorContact/GetAuthorContractDetails?Id=" + Id);
        _ContractDetails.then(function (_ContractDetails) {
            $('#hid_productid_AC').val(_ContractDetails.data._AuhtorContract.ProductId)
           
            if (_ContractDetails.data.SeriesCode == null || _ContractDetails.data.SeriesCode == "" || _ContractDetails.data.SeriesCode == undefined) {
                $scope.SeriesCode_Available = false;
               
                //angular.element(document.getElementById('angularid')).scope().ProductSerach(_ContractDetails.data._AuhtorContract.ProductId, _ContractDetails.data._AuhtorContract.AuthorContractCode, null);
                $scope.ContractCodeValuesCheck = _ContractDetails.data._AuhtorContract.AuthorContractCode;  //cheeck for third party permission
                angular.element(document.getElementById('angularid')).scope().ProductSerachContract(_ContractDetails.data._AuhtorContract.ProductId, _ContractDetails.data._AuhtorContract.AuthorContractCode, null);

            }
            else {
                $scope.SeriesCode_Available = true;
                $scope.SeriesCode_Available_Update = true;
              
                $scope.ShowProductsDetailMultiple(_ContractDetails.data.SeriesCode); //added by Prakash on 14 July, 2017
            }

            setTimeout(function () {
                //fetch Kit Details List
                angular.element(document.getElementById('angularid')).scope().KitISBNDetailsList(_ContractDetails.data._AuhtorContract.ProductId);
            }, 300);

            angular.element(document.getElementById('angularid')).scope().AuthorListProductBased(_ContractDetails.data._AuhtorContract.ProductId);
            if (_ContractDetails.data._AuhtorContract.LicenceId != undefined) {
                $scope.ProductLicenseSerach(_ContractDetails.data._AuhtorContract.LicenceId);
                LicenseId = _ContractDetails.data._AuhtorContract.LicenceId;
            }
            $scope.contractId = Id;
            $scope.SeriesCode = _ContractDetails.data._AuhtorContract.SeriesCode;
            $scope.FirstTime = 0;
            $scope.ByExecutive = _ContractDetails.data._AuhtorContract.HandleById;

            // $scope.EntryDate = _ContractDetails.data._AuhtorContract.EntryDate;

            $scope.Entrydate = _ContractDetails.data._AuhtorContract.EntryDate;


            $scope.ContractDate = _ContractDetails.data._AuhtorContract.ContractDate;
            //$scope.contractType = _ContractDetails.data._AuhtorContract.ContractTypeId;
            $scope.TemsOfCopyRight = _ContractDetails.data._AuhtorContract.TemsOfCopyRight;
            //$scope.CopyRight = _ContractDetails.data._AuhtorContract.TemsOfCopyRight;
            // $scope.PeriodOfAgreement = _ContractDetails.data._AuhtorContract.PeriodInMonth;
            //$scope.ExpiryDate = _ContractDetails.data._AuhtorContract.ContractExpiry;
            $scope.BuyBack = _ContractDetails.data._AuhtorContract.BuyBack != null ? _ContractDetails.data._AuhtorContract.BuyBack : "---";
            $scope.NatureofWork = _ContractDetails.data._AuhtorContract.NatureOfWork;
            $scope.CopyRightOwner = _ContractDetails.data._AuhtorContract.CopyRightOwner;
            $scope.Territoryrights = _ContractDetails.data._AuhtorContract.TeriterryId;
            $scope.ThirdPartyPermission = _ContractDetails.data._AuhtorContract.ThirdPartyPermission;
            $scope.Amendment = _ContractDetails.data._AuhtorContract.Amendment;
            $scope.AmendmentRemarks = _ContractDetails.data._AuhtorContract.AmendmentRemarks;
            $scope.Restriction = _ContractDetails.data._AuhtorContract.Restriction;
            $scope.NoofAuthors = _ContractDetails.data._AuhtorContract.NoOfAuthors;
            $scope.TblList = [];
            $scope.TblList = _ContractDetails.data.TblList;
            $scope.Restriction = _ContractDetails.data._AuhtorContract.Restriction == null ? "" : _ContractDetails.data._AuhtorContract.Restriction;
            $scope.SubjectMatters = _ContractDetails.data._AuhtorContract.SubjectMatterandTreatment;
            $scope.MinWords = _ContractDetails.data._AuhtorContract.MinWords == 0 ? "" : _ContractDetails.data._AuhtorContract.MinWords;
            $scope.MaxWords = _ContractDetails.data._AuhtorContract.MaxWords == 0 ? "" : _ContractDetails.data._AuhtorContract.MaxWords;
            $scope.MinNoOfPages = _ContractDetails.data._AuhtorContract.MinPages == 0 ? "" : _ContractDetails.data._AuhtorContract.MinPages;
            $scope.MaxPages = _ContractDetails.data._AuhtorContract.MaxPages == 0 ? "" : _ContractDetails.data._AuhtorContract.MaxPages;
            $scope.PriceType = _ContractDetails.data._AuhtorContract.PriceType == null ? "" : _ContractDetails.data._AuhtorContract.PriceType;
            $scope.CurrencyValue = _ContractDetails.data._AuhtorContract.CurrencyId == null ? "" : _ContractDetails.data._AuhtorContract.CurrencyId;
            $scope.ProductPrice = _ContractDetails.data._AuhtorContract.Price == 0 ? "" : _ContractDetails.data._AuhtorContract.Price;
            $scope.MediumofDelivery = _ContractDetails.data._AuhtorContract.MediumofDelivery == null ? "" : _ContractDetails.data._AuhtorContract.MediumofDelivery;
            $scope.deliverySchedule = _ContractDetails.data._AuhtorContract.Deliveryschedule == null ? "" : _ContractDetails.data._AuhtorContract.Deliveryschedule;
            $scope.ProductRemarks = _ContractDetails.data._AuhtorContract.ProductRemarks == "" ? "" : _ContractDetails.data._AuhtorContract.ProductRemarks;
            $scope.MenuScriptDeliveryFormat = _ContractDetails.data._AuhtorContract.MenuScriptDeliveryId == undefined ? "" : _ContractDetails.data._AuhtorContract.MenuScriptDeliveryId;
            $scope.ContributorList = _ContractDetails.data._contributor;
            $scope.HandledByList = [];
            var executive =
            {
                Id: _ContractDetails.data._AuhtorContract.HandleById,
                ExecutiveName: _ContractDetails.data._AuhtorContract.HandledByName
            }
            $scope.HandledByList.push(executive);
            $('select[name$=HandledByExecutive]').prop("disabled", true);
            $scope.ByExecutive = _ContractDetails.data._AuhtorContract.HandleById


            //if ($scope.ContributorList.length == 0)
            //{
            //    $scope.ContributorList.push(1);
            //}
            $scope.MaterialSuppliedByAuthorList = _ContractDetails.data._MaterialDate;
            $scope.AuthorBox = _ContractDetails.data._AuthorList;
            if (_ContractDetails.data._royalty.length > 0) {
                $scope.RoyaltyslabList = _ContractDetails.data._royalty;
            }

            $scope._subsidiaryList2 = _ContractDetails.data._susidiaryRightsList;

            if ($scope._subsidiaryList2.length == 0) {
                setTimeout(function () {
                    $('input[type=radio][name*=SubsidiaryRequired][value=0]').prop("checked", true);
                    $scope.SubsidiaryRequired = 0;
                }, 1000)
            }
            else {
                $scope.SubsidiaryRequired = 1;
            }

            $scope.ttlSubsidiary = _ContractDetails.data._ttlSusidiary;
            $scope._ContractAgreement = _ContractDetails.data._ContractAgreement;
            $scope.ContributorName = [];
            $scope.ContractNameList = [];
            $scope.SupplyMaterialByAuthor = [];
            $scope.MaterialDate = [];
            $scope.AuthorType = [];
            $scope.Percentage = [];
            $scope.SelectedSupplyMaterialByAuthor = [];
            for (var i = 0; i < $scope.ContributorList.length; i++) {
                $scope.ContractNameList.push(i);
                $scope.ContributorName.push($scope.ContributorList[i].Name);
            }


            if ($scope.ContributorList.length > 0) {
                $scope.Contributor = 1;
            }
            else {
                $scope.Contributor = 0;
                $scope.ContractNameList.push(1);
            }

            $scope.MenuScriptDeliveryFormat = [];

            for (var i = 0; i < _ContractDetails.data._ManuscriptDeliveryList.length; i++) {
                $scope.MenuScriptDeliveryFormat[i] = {
                    Id: _ContractDetails.data._ManuscriptDeliveryList[i].Id
                }
                $scope.ManuScriptFormatList[i] = {
                    Id: _ContractDetails.data._ManuscriptDeliveryList[i].Id
                }
            }


            for (var i = 0; i <= $scope.MaterialSuppliedByAuthorList.length - 1; i++) {
                $scope.SupplyMaterialByAuthor[i] =
                {
                    Id: $scope.MaterialSuppliedByAuthorList[i].MaterialId,
                    SupplyMaterial: $scope.MaterialSuppliedByAuthorList[i].Material
                }
                $scope.MaterialDate.push($scope.MaterialSuppliedByAuthorList[i].SuppliedDate)
                $scope.SelectedSupplyMaterialByAuthor[i] = {
                    Id: $scope.MaterialSuppliedByAuthorList[i].MaterialId,
                }
            }


            setAuthor($scope.AuthorBox, $scope.RoyaltyslabList);


            if (_ContractDetails.data._ContractAgreement != null) {
                $scope.AgreementStatus = _ContractDetails.data._ContractAgreement.contractstatus;
                $scope.AgreementDate = _ContractDetails.data._ContractAgreement.AgreementDate;
                $scope.SignedcontracDate = _ContractDetails.data._ContractAgreement.signedcontractsentdate;
                $scope.contractRecieved = _ContractDetails.data._ContractAgreement.SignedContractreceived;
                $scope.AuthorCopiesSend = _ContractDetails.data._ContractAgreement.Authorcopiessentdate;
                $scope.CotributorCopiessend = _ContractDetails.data._ContractAgreement.Contributorcopiessentdate;
                $scope.ContractRemarks = _ContractDetails.data._ContractAgreement.remarks;
                $scope.CancelDate = _ContractDetails.data._ContractAgreement.cancellationdate;
                $scope.CancellationRemarks = _ContractDetails.data._ContractAgreement.Cancellationreason;
                $scope.AgreementId = _ContractDetails.data._ContractAgreement.AgreementId;
                $scope.EffectiveDate = _ContractDetails.data._ContractAgreement.EffectiveDate;
                $scope.PeriodOfAgreement = _ContractDetails.data._ContractAgreement.PeriodinMonth;
                $scope.ExpiryDate = _ContractDetails.data._ContractAgreement.ExpiryDate;

                $scope.ContributorAgreement = $scope.ContributorDoc.length > 0 ? "Yes" : "No";
            }


            if (_ContractDetails.data._agreementDoc != null) {
                for (var i = 0; i < _ContractDetails.data._agreementDoc.length; i++) {
                    if (_ContractDetails.data._agreementDoc[i].DocumentTypeId == 1) {
                        $scope.AgreementDoc.push(_ContractDetails.data._agreementDoc[i]);
                    }
                    else {

                        $scope.ContributorDoc.push(_ContractDetails.data._agreementDoc[i]);
                        $scope.ContributorAgreement = $scope.ContributorDoc.length > 0 ? "Yes" : "No";
                    }
                }
            }

            /*this section is used to populate the section of contract agreement insert after the contrct already created*/

        }, function () {
            //alert('Error in getting Author Contract Detail');
        });
    }

    $scope.AuthorType = [];;
    $scope.AuthorValue = [];;

    $scope.AuthorCopies = [];
    $scope.SeedMoney = [];
    $scope.OneTimePayment = [];;
    $scope.AdvanceRoyalty = [];;

    function setAuthor(List, RoyalTySlab) {
        setTimeout(function () {
            var authorBox = $('.AuthorBox');
            for (i = 0; i < List.length; i++) {
                var FirstSlab = $($(authorBox)[i]);
                $scope.AuthorType[i] = List[i].TypeId;
                $scope.AuthorValue[i] = List[i].Id;
                $scope.PaymentPeriodType[i] = List[i].PaymentPerioodId != null ? List[i].PaymentPeriodId : "";
                $scope.AuthorCopies[i] = List[i].AuthorCopies;
                $scope.SeedMoney[i] = List[i].SeedMoney;
                $scope.OneTimePayment[i] = List[i].OneTimePayment;
                $scope.AdvanceRoyalty[i] = List[i].AdvanceRoyalty;
                $scope.contractType[i] = List[i].ContractId;
                $($(authorBox)[i]).find('select[name*=AuthorType]').val(List[i].TypeId);
                $($(authorBox)[i]).find('select[name*=AuthorName]').val(List[i].Id);
                $($(authorBox)[i]).find('select[name*=ContactType]').val(List[i].ContractId);
                $($(authorBox)[i]).find('select[name*=PaymentPeriodType]').val(List[i].PaymentPeriodId != null ? List[i].PaymentPeriodId : "");
                $($(authorBox)[i]).find('input[name*=AuthorCopies]').val(List[i].AuthorCopies != null ? List[i].AuthorCopies : "");
                $($(authorBox)[i]).find('input[name*=SendMoney]').val(List[i].SeedMoney != null ? List[i].SeedMoney : "");
                $($(authorBox)[i]).find('input[name*=OneTimePayment]').val(List[i].OneTimePayment != null ? List[i].OneTimePayment : "");
                $($(authorBox)[i]).find('input[name*=AdvanceRoyalty]').val(List[i].AdvanceRoyalty != null ? List[i].AdvanceRoyalty : "");
                if (RoyalTySlab.length >= 1) {
                    for (var k = 0, s = 0; k < RoyalTySlab.length; k++) {
                        if (List[i].RecId == RoyalTySlab[k].Id) {
                            $($(FirstSlab).find('select[name$=SubProductType]')[s]).val(RoyalTySlab[k].subproductTypeId);
                            $($(FirstSlab).find('input[name$=CopiesFrom]')[s]).val(RoyalTySlab[k].CopiesFrom);
                            if (RoyalTySlab[k].CopiesTo != 0 && RoyalTySlab[k].CopiesTo != "") {
                                $($(FirstSlab).find('input[name$=CopiesTo]')[s]).val(RoyalTySlab[k].CopiesTo);
                            }

                            $($(FirstSlab).find('input[name$=RyPercentage]')[s]).val(RoyalTySlab[k].Percentage);
                            $(FirstSlab).find('.RoyaltySlabLink').css("display", "none");
                            $(FirstSlab).find('.RoyaltySlabnotRemove').css("display", "table-row");
                            s++;
                        }
                        else if (RoyalTySlab.length > 1) {
                            $($(FirstSlab).find('select[name$=SubProductType]')[s]).closest("tr").remove()
                        }
                        //else
                        //{
                        //    setTimeout(function () { $('.RoyaltySlabnotRemove').css("display", "none"); }, 1000);

                        //}
                    }
                }

            }
            //if (RoyalTySlab.length <=1)
            //{
            //    $scope.RoyaltyslabList.push(1);
            //}



        }, 3000);

    }
    $scope.RoyaltySlabManagement = function () {
        $('.AuthorBox').each(function () {

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

    $scope.calcTotal = function (AuthorId, SubsidiaryId, crrIndex, ParentIndex) {
        if ($scope.FirstTime != 0 || SubsidiaryId == undefined) {
            return false;
        }

        setTimeout(function () {
            for (var i = 0; i < $scope._subsidiaryList2.length; i++) {
                if ($scope._subsidiaryList2[i].subsidiaryid == SubsidiaryId && $scope._subsidiaryList2[i].authorId == AuthorId) {
                    //if ($scope._subsidiaryList2[i].Percentage != 0) {
                        $('.author_' + crrIndex + '_' + ParentIndex + '').val($scope._subsidiaryList2[i].Percentage)
                    //}
                    //else {
                    //    $('.author_' + crrIndex + '_' + ParentIndex + '').val("");
                    //}


                }
            }

        }, 1000);
    }
    $scope.calcTotalPer = function (SubsidiaryId, Index) {
        if ($scope.FirstTime != 0) {
            return false;
        }

        var _ttl = 0;
        var oupPercentage = 0;
        setTimeout(function () {
            for (var i = 0; i < $scope._subsidiaryList2.length; i++) {
                if ($scope._subsidiaryList2[i].subsidiaryid == SubsidiaryId) {
                    _ttl = _ttl + $scope._subsidiaryList2[i].Percentage
                    oupPercentage = $scope._subsidiaryList2[i].OupPercentage
                }
            }
            //if (_ttl != 0) {
            if (oupPercentage > 0) {
                $('#AuthorPrePercentageTextbox_' + Index + '').val(_ttl + oupPercentage);
                $('#hid_oupPercentage_' + Index + '').val(_ttl + oupPercentage);
                $('#totalpercentage_' + Index + '').val(oupPercentage);

            }
         }, 100);
    }
    $scope.RemoveAddendumFileUploadById = function (docid, file) {
        var AuthorDocument = { Id: docid, EnteredBy: $("#enterdBy").val() };
        var DeleteDocument = AJService.PostDataToAPI("AuthorContact/RemoveAddendumFileUpload", AuthorDocument);

        DeleteDocument.then(function (msg) {
            if (msg.data != "Deleted") {
                SweetAlert.swal("Oops...", "Please retry!", "error");
            }
            else {
                $scope.getAddendumDocumentList();
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
                            //debugger;
                            //$scope.EditOtherContractData($('#hid_OtherContractId').val());
                            $scope.getAddendumDocumentList();
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
    $scope.getAddendumDocumentList = function () {
        var getAddendumDocumentList = AJService.GetDataFromAPI("AuthorContact/getAlltheAddendumDocument?ContractId=" + $scope.ContractId + "");
        getAddendumDocumentList.then(function (mdt) {
            if (mdt.data != null) {

                $scope.AddendumDocumentList = mdt.data._addendumFile;
                mstr_AddendumValue = mdt.data._addendumFile;
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
                //  $scope.documentshow = true;

                //  $scope.UploadFIleReq = true;
            }


        }


    )
    };

        $scope.ContractAgreement = function () {

            var ContributorFileName = "";
            var AgreementFileName = "";
            //if ($("input[type=radio][name*=ContributorAgreement]:checked").val() == "Yes") {
            //    $("#dropZone1").find('.fileNameClass').each(function () {
            //        ContributorFileName = ContributorFileName + $(this).val() + ",";
            //    });
            //}
            //if ($("input[type=radio][name*=AgreementStatus]:checked").val() != "Cancelled") {
            //    $("#dropZone0").find('.fileNameClass').each(function () {

            //        AgreementFileName = AgreementFileName + $(this).val() + ",";
            //    });
            //}

            if ($("input[type=radio][name*=ContributorAgreement]:checked").val() == "Yes") {
                $("#dropZone2").find('.fileNameClass').each(function () {
                    ContributorFileName = ContributorFileName + $(this).val() + ",";
                });
            }
            if ($("input[type=radio][name*=AgreementStatus]:checked").val() != "Cancelled" && $("input[type=radio][name*=AgreementStatus]:checked").val() != "Draft") {
                $("#dropZone1").find('.fileNameClass').each(function () {

                    AgreementFileName = AgreementFileName + $(this).val() + ",";
                });
            }

            var Agreement =
            {
                Id: $scope.AgreementId,
                //ContractId: $scope.SeriesCode != undefined ? null : $scope.contractId,
                //SeriesCode: $scope.SeriesCode,
                ContractId: $scope.contractId,
                SeriesCode: $scope.SeriesCode,

                productIds: $scope.productIds,
                ContractStatus: $("input[type=radio][name*=AgreementStatus]:checked").val(),
                PeriodOfAgreement: 0, //$("input[name*=PeriodOfAgreementAgreement]").val(), //change by prakash on 30 May, 2017
                //ExpiryDate: convertDate($scope.ExpiryDate),
                ExpiryDate: $("input[id*=ExpiryDate]").val() != "" ? convertDate($("input[id*=ExpiryDate]").val()) : null,
                EffectiveDate: $("input[id*=EffectiveDate]").val() != "" ? convertDate($("input[id*=EffectiveDate]").val()) : null,
                AgreementDate: $("input[name*=AgreementDate]").val() != "" ? convertDate($("input[name*=AgreementDate]").val()) : null,
                SignedcontracDate: $("input[name*=SignedcontracDate]").val() != "" ? convertDate($("input[name*=SignedcontracDate]").val()) : null,
                contractRecieved: $("input[name*=contractRecieved]").val() != "" ? convertDate($("input[name*=contractRecieved]").val()) : null,
                //AuthorCopiesSend: $("input[name*=AuthorCopiesSend]").val() != "" ? convertDate($("input[name*=AuthorCopiesSend]").val()) : null,
                AuthorCopiesSend: null,
                ContractRemarks: $("input[name*=ContractRemarks]").val(),
                CancelDate: $("input[name*=CancelDate]").val() != "" ? convertDate($("input[name*=CancelDate]").val()) : null,
                //CotributorCopiessend: $("input[name*=CotributorCopiessend]").val() != "" ? convertDate($("input[name*=CotributorCopiessend]").val()) : null,
                CotributorCopiessend: null,
                CancellationRemarks: $("[name$=CancellationRemarks]").val(),
                AgreementRemarks: $("[name$=ContractRemarks]").val(),
                ContributorAgreement: $("input[type=radio][name*=ContributorAgreement]:checked").val(),
                Doc: $("#hid_Uploads").val(),
                AgreementFileName: AgreementFileName,
                ContributorFileName: ContributorFileName,
                EnteredBy: $("input[type=hidden][id^=enterdBy]").val()
            }
            var ContractAgreement = AJService.PostDataToAPI("AuthorContact/ContractAgreement", Agreement);
        };


    $scope.RemoveDocumentLinkById = function (docid, file) {
        var AuthorDocument = { Id: docid, EnteredBy: $("#enterdBy").val() };
        var DeleteDocument = AJService.PostDataToAPI("AuthorContact/RemoveAuhtorDocumentLink", AuthorDocument);

        DeleteDocument.then(function (msg) {
            if (msg.data != "Deleted") {
                SweetAlert.swal("Oops...", "Please retry!", "error");
            }
            else {
                $scope.getDocumentList();
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
                            $scope.getDocumentList();
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

    $scope.getDocumentList = function () {
        var getDocumentList = AJService.GetDataFromAPI("AuthorContact/getAlltheDocument?agreementid=" + $scope.AgreementId + "");
        getDocumentList.then(function (getDocumentList) {
            $scope.DocumentList = getDocumentList.data._agreementDoc;
        });
    };


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


    setTimeout(function () {
        if ($("#hid_ContractId").val() == 0) {
            $("select[name*=PaymentPeriodType]").val(5);
        }
    }, 1000)



    $scope.SetScopeVariable = function (val) {
        $scope.TemsOfCopyRight = val;
        //$scope.CopyRight = val;
        
    };

    /*******************************************************************************************************************************
    Created by  :   Dheeraj kumar sharma
    Created on  :   1st aug 2016
    Created for :   Getting the series Contrcact Detail based on Series Code
    *I*******************************************************************************************************************************/

    $scope.GetAuthorContractDetailsbySeriesId = function (SeriesCode) {

        if (SeriesCode == "" || SeriesCode == undefined) {
            return false;
        }
     
        var _ContractDetails = AJService.GetDataFromAPI("AuthorContact/GetAuthorContractDetailsbySeriesId?SeriesCode=" + SeriesCode);
        _ContractDetails.then(function (_ContractDetails) {
        
            $('#hid_productIds').val(_ContractDetails.data._AuhtorContract.ProductId)
         
            //Added by Saddam on 18/08/2017
            setTimeout(function () {
                if (_ContractDetails.data._ThirdPartyContractList.length > 0) {
                    for (var i = 0; i < _ContractDetails.data._ThirdPartyContractList.length; i++) {
                      
                        if (_ContractDetails.data._ThirdPartyContractList[i].ThirdPartyPermission == 1) {
                            $($('[id*=ThirdPartyPermissionValue_' + [i] + ']').find("input")[0]).trigger("click")

                        } else {
                            $($('[id*=ThirdPartyPermissionValue_' + [i] + ']').find("input")[1]).trigger("click")
                        }                        
                    }
                }
            }, 2000);
            //Ended by Saddam

            ////Commented and added by Prakash on 04 July, 2017
            //angular.element(document.getElementById('angularid')).scope().ShowProductsDetailMultiple(_ContractDetails.data._AuhtorContract.ProductId);
            $scope.SeriesCode_Available = true;
            angular.element(document.getElementById('angularid')).scope().ShowProductsDetailMultiple(_ContractDetails.data._AuhtorContract.SeriesCode);
            

            angular.element(document.getElementById('angularid')).scope().AuthorListProductBased(_ContractDetails.data._AuhtorContract.ProductId);
            $scope.contractId = _ContractDetails.data._AuhtorContract.Id;
            $scope.FirstTime = 0;
            $scope.productIds = _ContractDetails.data._AuhtorContract.ProductId;
            $scope.SeriesCode = _ContractDetails.data._AuhtorContract.SeriesCode
            $scope.SeriesName = _ContractDetails.data._AuhtorContract.SeriesName;
            $scope.SeriesId = _ContractDetails.data._AuhtorContract.SeriesId;
            $scope.ByExecutive = _ContractDetails.data._AuhtorContract.HandleById;
            $scope.EntryDate = _ContractDetails.data._AuhtorContract.EntryDate;
            $scope.ContractDate = _ContractDetails.data._AuhtorContract.ContractDate;
            //$scope.contractType = _ContractDetails.data._AuhtorContract.ContractTypeId;
            $scope.TemsOfCopyRight = _ContractDetails.data._AuhtorContract.TemsOfCopyRight;
            //$scope.CopyRight = _ContractDetails.data._AuhtorContract.TemsOfCopyRight;
            $scope.PeriodOfAgreement = _ContractDetails.data._AuhtorContract.PeriodInMonth;
            $scope.ExpiryDate = _ContractDetails.data._AuhtorContract.ContractExpiry;
            $scope.BuyBack = _ContractDetails.data._AuhtorContract.BuyBack != null ? _ContractDetails.data._AuhtorContract.BuyBack : "";
            $scope.NatureofWork = _ContractDetails.data._AuhtorContract.NatureOfWork;
            $scope.CopyRightOwner = _ContractDetails.data._AuhtorContract.CopyRightOwner;
            $scope.Territoryrights = _ContractDetails.data._AuhtorContract.TeriterryId;


           // $scope.ThirdPartyPermission = _ContractDetails.data._AuhtorContract.ThirdPartyPermission;



            $scope.Amendment = _ContractDetails.data._AuhtorContract.Amendment;
            $scope.AmendmentRemarks = _ContractDetails.data._AuhtorContract.AmendmentRemarks;
            $scope.Restriction = _ContractDetails.data._AuhtorContract.Restriction;
            $scope.NoofAuthors = _ContractDetails.data._AuhtorContract.NoOfAuthors;
            $scope.TblList = [];
            $scope.TblList = _ContractDetails.data.TblList;
            $scope.Restriction = _ContractDetails.data._AuhtorContract.Restriction == null ? "" : _ContractDetails.data._AuhtorContract.Restriction;
            $scope.SubjectMatters = _ContractDetails.data._AuhtorContract.SubjectMatterandTreatment;
            $scope.MinWords = _ContractDetails.data._AuhtorContract.MinWords == 0 ? "" : _ContractDetails.data._AuhtorContract.MinWords;
            $scope.MaxWords = _ContractDetails.data._AuhtorContract.MaxWords == 0 ? "" : _ContractDetails.data._AuhtorContract.MaxWords;
            $scope.MinNoOfPages = _ContractDetails.data._AuhtorContract.MinPages == 0 ? "" : _ContractDetails.data._AuhtorContract.MinPages;
            $scope.MaxPages = _ContractDetails.data._AuhtorContract.MaxPages == 0 ? "" : _ContractDetails.data._AuhtorContract.MaxPages;
            $scope.PriceType = _ContractDetails.data._AuhtorContract.PriceType == null ? "" : _ContractDetails.data._AuhtorContract.PriceType;
            $scope.CurrencyValue = _ContractDetails.data._AuhtorContract.CurrencyId == null ? "" : _ContractDetails.data._AuhtorContract.CurrencyId;
            $scope.ProductPrice = _ContractDetails.data._AuhtorContract.Price == 0 ? "" : _ContractDetails.data._AuhtorContract.Price;
            $scope.MediumofDelivery = _ContractDetails.data._AuhtorContract.MediumofDelivery == null ? "" : _ContractDetails.data._AuhtorContract.MediumofDelivery;
            $scope.deliverySchedule = _ContractDetails.data._AuhtorContract.Deliveryschedule == null ? "" : _ContractDetails.data._AuhtorContract.Deliveryschedule;
            $scope.ProductRemarks = _ContractDetails.data._AuhtorContract.ProductRemarks == "" ? "" : _ContractDetails.data._AuhtorContract.ProductRemarks;
            $scope.MenuScriptDeliveryFormat = _ContractDetails.data._AuhtorContract.MenuScriptDeliveryId == undefined ? "" : _ContractDetails.data._AuhtorContract.MenuScriptDeliveryId;
            $scope.ContributorList = _ContractDetails.data._contributor;
            $scope.HandledByList = [];
            var executive =
            {
                Id: _ContractDetails.data._AuhtorContract.HandleById,
                ExecutiveName: _ContractDetails.data._AuhtorContract.HandledByName
            }
            $scope.HandledByList.push(executive);
            $('select[name$=HandledByExecutive]').prop("disabled", true);
            $scope.ByExecutive = _ContractDetails.data._AuhtorContract.HandleById

            $scope.MaterialSuppliedByAuthorList = _ContractDetails.data._MaterialDate;
            $scope.AuthorBox = _ContractDetails.data._AuthorList;
            if (_ContractDetails.data._royalty.length > 0) {
                $scope.RoyaltyslabList = _ContractDetails.data._royalty;
            }

            $scope._subsidiaryList2 = _ContractDetails.data._susidiaryRightsList;

            if ($scope._subsidiaryList2.length == 0) {
                $scope.SubsidiaryRequired = 0;
            }
            else {
                $scope.SubsidiaryRequired = 1;
            }


            $scope._subsidiaryList2 = _ContractDetails.data._susidiaryRightsList;

            if ($scope._subsidiaryList2.length == 0) {
                $scope.SubsidiaryRequired = 0;

            }
            else {
                $scope.SubsidiaryRequired = 1;
            }



            $scope.ttlSubsidiary = _ContractDetails.data._ttlSusidiary;
            $scope._ContractAgreement = _ContractDetails.data._ContractAgreement;
            $scope.ContributorName = [];
            $scope.ContractNameList = [];
            $scope.SupplyMaterialByAuthor = [];
            $scope.MaterialDate = [];
            $scope.AuthorType = [];

            $scope.Percentage = [];
            $scope.SelectedSupplyMaterialByAuthor = [];
            for (var i = 0; i < $scope.ContributorList.length; i++) {
                $scope.ContractNameList.push(i);
                $scope.ContributorName.push($scope.ContributorList[i].Name);
            }

            $scope.MenuScriptDeliveryFormat = [];

            for (var i = 0; i < _ContractDetails.data._ManuscriptDeliveryList.length; i++) {
                $scope.MenuScriptDeliveryFormat[i] = {
                    Id: _ContractDetails.data._ManuscriptDeliveryList[i].Id
                }
                $scope.ManuScriptFormatList[i] = {
                    Id: _ContractDetails.data._ManuscriptDeliveryList[i].Id
                }
            }

            if ($scope.ContributorList.length > 0) {
                $scope.Contributor = 1;
            }
            else {
                $scope.Contributor = 0;
            }
            for (var i = 0; i <= $scope.MaterialSuppliedByAuthorList.length - 1; i++) {
                $scope.SupplyMaterialByAuthor[i] =
                {
                    Id: $scope.MaterialSuppliedByAuthorList[i].MaterialId,
                    SupplyMaterial: $scope.MaterialSuppliedByAuthorList[i].Material
                }
                $scope.MaterialDate.push($scope.MaterialSuppliedByAuthorList[i].SuppliedDate)
                $scope.SelectedSupplyMaterialByAuthor[i] = {
                    Id: $scope.MaterialSuppliedByAuthorList[i].MaterialId,
                }
            }


            setAuthor($scope.AuthorBox, $scope.RoyaltyslabList);


            if (_ContractDetails.data._ContractAgreement != null) {
                $scope.AgreementStatus = _ContractDetails.data._ContractAgreement.contractstatus;
                $scope.AgreementDate = _ContractDetails.data._ContractAgreement.AgreementDate;
                $scope.SignedcontracDate = _ContractDetails.data._ContractAgreement.signedcontractsentdate;
                $scope.contractRecieved = _ContractDetails.data._ContractAgreement.SignedContractreceived;
                $scope.AuthorCopiesSend = _ContractDetails.data._ContractAgreement.Authorcopiessentdate;
                $scope.CotributorCopiessend = _ContractDetails.data._ContractAgreement.Contributorcopiessentdate;
                $scope.ContractRemarks = _ContractDetails.data._ContractAgreement.remarks;
                $scope.CancelDate = _ContractDetails.data._ContractAgreement.cancellationdate;
                $scope.CancellationRemarks = _ContractDetails.data._ContractAgreement.Cancellationreason;
                $scope.AgreementId = _ContractDetails.data._ContractAgreement.AgreementId;
                $scope.EffectiveDate = _ContractDetails.data._ContractAgreement.EffectiveDate;
                $scope.PeriodOfAgreement = _ContractDetails.data._ContractAgreement.PeriodinMonth;
                $scope.ExpiryDate = _ContractDetails.data._ContractAgreement.ExpiryDate;

            }

            if (_ContractDetails.data._agreementDoc != null) {
                for (var i = 0; i < _ContractDetails.data._agreementDoc.length; i++) {
                    if (_ContractDetails.data._agreementDoc[i].DocumentTypeId == 1) {
                        $scope.AgreementDoc.push(_ContractDetails.data._agreementDoc[i]);
                    }
                    else {

                        $scope.ContributorDoc.push(_ContractDetails.data._agreementDoc[i]);
                        $scope.ContributorAgreement = "Yes";
                    }
                }
            }
            if ($scope.ContributorDoc.length == 0 && $scope.AgreementId != undefined) {
                $scope.ContributorAgreement = "No";
            }



            /*this section is used to populate the section of contract agreement insert after the contrct already created*/


        }, function () {
            //alert('Error in getting Author Contract Detail by SeriesId');
        });
    }
    /*******************************************************************************
      Created By  :  Dheeraj Kumar Sharma
      Created on  : 07/06/2016
      Created For : Calculate the expiry

  *******************************************************************************
  *******************************************************************************/
    ////------commenter by prakash on 30 may, 2017
    //$scope.CalculateExpiryAgreement = function (PeriodIdValue) {
    //    if (PeriodIdValue == undefined || $("input[type=text][id*=AgreementDate]").val() == "") {
    //        $scope.ExpiryDate = "";
    //        return false;
    //    }

    //    var CurrentDate = DateFormat(convertDate($("input[type=text][id*=AgreementDate]").val()));
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
    //    $scope.ExpiryDate = today;
    //}
    //function DateFormat(inputFormat) {
    //    function pad(s) { return (s < 10) ? '0' + s : s; }
    //    return new Date(inputFormat);

    //}



    $scope.fun_AddAmendmentFile = function ()
    {
       
        var errorDiv;
        var errormsg = '';
        $scope.msg = "";

        FileNameArray = $('#dropZone0').find('.fileNameClass');
        var array = [];

        FileNameArray.each(function () {
            array.push(
           $(this).val()
       );

            for (i = 0; i < array.length; i++) {
                if (array[i] == "") {
                 
                    SweetAlert.swal("Validation", "Please enter File name.", "warning");
                    $('#dropZone0').focus();
                }
                else {
                    $('.CloseValue').trigger("click");
                }

            }

        });

        

    }


    //if ($('#hid_ContractId').val() != null && $('#hid_ContractId').val() != undefined) {
    //    if ($('input[name*=Amendment]:checked').val() == 1) {
    //        $('#ViewAmendmentfile').css("display", "none");
    //    }
    //    else if ($('input[name*=Amendment]:checked').val() == 0) {
    //        $('#ViewAmendmentfile').css("display", "block");
    //    }
    //}



    $scope.GetViewAmendmentDocumentList = function (Id) {

        var getViewAmendmentDocumentDetail = AJService.GetDataFromAPI("AuthorContact/GetViewAmendmentDocumentList?Id=" + Id, null);
        getViewAmendmentDocumentDetail.then(function (msg) {
            if (msg.data != "") {


                if (msg.data.AuthorAmendmentDocumentDetails.DocumentName != null) {
                    var e1 = 0;
                    var d1 = 0;
                    var docNames1 = '';
                    var Docurl1 = '';
                    $scope.Docurl1 = [];

                    if (msg.data.AuthorAmendmentDocumentDetails.DocumentName != '') {

                        $scope.Pendingdocumentshow = true;
                        var docNames1 = msg.data.AuthorAmendmentDocumentDetails.DocumentName.slice(',');
                        var DName1 = msg.data.AuthorAmendmentDocumentDetails.DocumentName.slice(',');

                        var DId1 = msg.data.AuthorAmendmentDocumentDetails.DocumentIds.slice(',');

                        var Docurl1 = msg.data.AuthorAmendmentDocumentDetails.UploadFile.split(',');
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




            }

        }
    )
    };

    if ($('#hid_ContractId').val() != null && $('#hid_ContractId').val() != undefined) {
        $scope.GetViewAmendmentDocumentList($('#hid_ContractId').val());
    }



    $scope.RemoveAmendmentDocumentLinkById = function (docid, file) {

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

            var ID = { Id: docid, EnteredBy: $("#enterdBy").val() };
            var DeleteDocument = AJService.PostDataToAPI("AuthorContact/RemoveAmendmentDocument", ID);

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
                        url: $("#hid_documentDeleteUrl").val(),
                        data: JSON.stringify(obj),
                        dataType: "json",
                        success: function (result) {
                            if (result == "Deleted") {
                                //$scope.getDocumentListAfterDelete();
                                SweetAlert.swal({
                                    title: "Success",
                                    text: "Deleted successfully",
                                    type: "success"
                                },
                                function () {
                                    window.location.reload();
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
   
    $scope.btn_BackToList = function () {
        location.href = GlobalredirectPath + "Contract/AuthorContract/AuthorContractSearch?For=List";
    }

    $scope.btn_BackToListSeries = function () {
        location.href = GlobalredirectPath + "Contract/AuthorContract/AuthorContractSearch?For=SeriesList";
    }

    $scope.RemoveDocument = function (docid, file) {

        SweetAlert.swal({
            title: "Are you sure?",
            text: "You will not be able to recover this document ",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: true,
            closeOnCancel: true
        },
           function (Confirm) {
               if (Confirm) {
                   
                   var ID = { Id: docid, EnteredBy: $("#enterdBy").val() };
                   var DeleteDocument = AJService.PostDataToAPI("AuthorContact/RemoveAuthorContractDocument", ID);

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
                               url: $("#hid_documentDeleteUrl").val(),
                               data: JSON.stringify(obj),
                               dataType: "json",
                               success: function (result) {
                                   if (result == "Deleted") {
                                       $scope.getDocumentListAfterDelete();

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
 

    $scope.getDocumentListAfterDelete = function () {
        var getDocumentList = AJService.GetDataFromAPI("AuthorContact/getAlltheDocument?agreementid=" + $scope.AgreementId + "");
        getDocumentList.then(function (getDocumentList) {
            $scope.ContributorDoc = [];
            $scope.AgreementDoc = [];
            if (getDocumentList.data != null) {

                for (var i = 0; i < getDocumentList.data._agreementDoc.length; i++) {
                    if (getDocumentList.data._agreementDoc[i].DocumentTypeId == 1) {
                        $scope.AgreementDoc.push(getDocumentList.data._agreementDoc[i]);
                    }
                    else {

                        $scope.ContributorDoc.push(getDocumentList.data._agreementDoc[i]);
                        $scope.ContributorAgreement = "Yes";
                    }
                }
            }

           // $scope.AgreementDoc = getDocumentList.data._agreementDoc;
        });
    };


    //$scope.fn_validate_OneTimePayment = function (count,name) {
    //    var _OneTimePayment = $('.oneTimePayment_validate'); 
    //    for (var i = 0; i < _OneTimePayment.length; i++) {
    //        if (i == count && name == 'Onetime payment') {
    //            _OneTimePayment[0].prop('required', 'required');
    //        }
    //    }
    //}


});





