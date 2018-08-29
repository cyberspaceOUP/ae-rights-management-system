/// <reference path="../master/Master.Division.js" />

app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {

    app.expandControllerTopSearch($scope, AJService, $window);

    /*Expand common.master.js Controller*/
    app.expandControllerA($scope, AJService, $window);
    $scope.Productcategory = "";
    /******************** Start DivisionSubDivision Control********************/

    app.expandControllerDivisionSubDivision($scope, AJService, $window);
    /*Mandatory Variable for Division SubDivision Control*/
    $scope.DivisionMandatory = true;
    $scope.SubDivisionMandatory = true;

    $scope.ProductCategoryListEntry = []
    $scope.LinkedISBNList = [];
    $scope.LinkedISBNList.push(1);
    $scope.LinkedISBN = []
    $scope.PreviousProductId = []


   
    // Get Product Category Type List
    $scope.getAllProductCategoryListEntry = function () {
        var ProductCategoryList = AJService.GetDataFromAPI("CommonList/getAllProductCategoryList", null);
        ProductCategoryList.then(function (ProductCategory) {

                $scope.ProductCategoryListEntry = ProductCategory.data;

        }, function () {
            //alert('Error in getting Product Category List');
        });
    }

    angular.element(document.getElementById('angularid')).scope().getAllProductCategoryListEntry();
    angular.element(document.getElementById('angularid')).scope().getAllProductTypeList();
    angular.element(document.getElementById('angularid')).scope().getCurrencyList();

    /**************Call Function For Checking Valid ISBN*******************/
    $scope.ValidISBN = function (ISBN) {
       
        var Product = {
            OUPISBN: ISBN.value,
        };
        $scope.Req_ISBNNO = false;
        if (ISBN.value.length == 13) {
            var previousproduct = AJService.PostDataToAPI("ProductMaster/ValidISBN", Product);
            previousproduct.then(function (product) {
                ISBN.nextElementSibling.value = product.data;
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

    var _productType = 0; //$('input[name=ProductType]:checked').val();
    $scope.getSubProductTypeList = function (Id) {
        //$scope.ISBN = "";
        _productType = Id;
        if (_productType != null && _productType != "" && _productType != undefined && _productType != 0) {
            AutoCompleteISBN_Bag(Id);

            setTimeout(function () {
                AutoCompleteISBN();
            }, 300);

        }
    }
    

    $('input[name=ISBN]').keypress(function () {
        if (_productType == null || _productType == "" || _productType == undefined || _productType == 0) {
            SweetAlert.swal("", "Please select Product Type !", "info", "info");
            return false;
        }
    });

    //bind ISBN from ISBN Bag
    function AutoCompleteISBN_Bag(_productType) {
        var objBag = $("[name$=ISBN]");
        var ISBNBagList = [];

        var getISBNBagList = AJService.PostDataToAPI("CommonList/GetAllISBNBagList?productType=" + _productType, null);
        getISBNBagList.then(function (ISBNBag) {
            for (i = 0; i < ISBNBag.data.query.length; i++) {
                ISBNBagList[i] = { "label": ISBNBag.data.query[i].ISBN, "value": ISBNBag.data.query[i].ISBN, "data": ISBNBag.data.query[i].Id };
            }

            $(objBag).autocomplete({
                source: function (request, response) {
                    var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
                    response($.grep(ISBNBagList, function (item) {
                        return matcher.test(item.label);
                    }));
                },

                autoFocus: true,
                select: function (event, ui) {
                    var ISBN_Id = ui.item.data;

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

                    event.target.nextElementSibling.value = ISBN_Id;
                }

            });
        }, function () {
            //alert('Error in getting ISBN list');
        });
    }

    //bind ISBN list for libked with Kit
    function AutoCompleteISBN() {
        var obj = $("[name$=LinkedISBN]");
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
                    var productId = ui.item.data;
                    event.target.nextElementSibling.value = productId;
                    if (productId == 0) {
                        $scope.Req_ISBNNO = true;
                    }
                    else {
                        $scope.Req_ISBNNO = false;
                        //$('#btnSubmit').trigger("click");
                        //$scope.submitted = true;
                    }
                }
            });
        }, function () {
            //alert('Error in getting ISBN list');
        });
    }

    var _temp_count = 1;
    $scope.addLinkedIsbnRow = function () {

        var i = $scope.LinkedISBNList.length + 1;
        $scope.LinkedISBNList.push(i);

        setTimeout(function () {
            AutoCompleteISBN();
                   
        }, 200);

        if ($scope.LinkedISBNList.length == 1) {
            $scope.btnRemoveShow = false;
        }
        if ($scope.LinkedISBNList.length > 1) {
            $scope.btnRemoveShow = true;
        }

    };

    //$scope.btnRemoveShow = false;
    $scope.removeLinkedIsbnRow = function () {
       
        var i = $scope.LinkedISBNList.length - 1;
        $scope.LinkedISBN[i] = '';
        $scope.LinkedISBNList.pop(i);
       
        if ($scope.LinkedISBNList.length == 1) {
            $scope.btnRemoveShow = false;
            $scope.submitted = true;
        }     
    };


    $scope.kitIsbnEntryForm = function () {
                        
        $scope.submitted = true;
        if (_productType != null && _productType != "" && _productType != undefined && _productType != 0) {
            $scope.userForm.$valid = true;
        }
            if ($scope.userForm.$valid) {
                $scope.KitISBNEntry();
                $scope.submitted = true;
                return;
            }      
    }

    $scope.KitISBNEntry = function () {
       
        $scope.submitted = true;
        if ($scope.userForm.$valid) {
            $scope.ListLinkedISBN = [];
            var results = [];
            var i = 0;

            $('input[type=hidden][name=PreviousProductId]').each(function () {
                if ($(this).val() != "" && $(this).val() != "0") {
                    $scope.ListLinkedISBN[i] = $(this).val();
                    i = i + 1;
                }
                else {
                    results.push($(this).val());
                }
            });
            if (results.length != 0) {
                SweetAlert.swal("Error!", "Please select valid Linked ISBN !", "", "error");
                return false;
            }

            //start check duplicate linked isbn entered
            var arr = $scope.ListLinkedISBN;
            var sorted_arr = arr.slice().sort();         
            results = [];
            for (var i = 0; i < arr.length - 1; i++) {
                if (sorted_arr[i + 1] == sorted_arr[i]) {
                    results.push(sorted_arr[i]);
                }
            }
            if (results.length != 0) {
                SweetAlert.swal("Error!", "Duplicate. Linked ISBN !", "", "error");
                return false;
            }
            //end check duplicate
              
            //add in list for entry data
            var kitIsbnModel =
                {
                    Id: $('#hid_KitIsbnId').val() == "" ? 0 : $('#hid_KitIsbnId').val(),
                    Division: $scope.DivSubDivCntrl.Division,
                    Subdivision: $scope.DivSubDivCntrl.SubDivision,
                    ProductCategory: $scope.ProductCategory,
                    WorkingProduct: $scope.WorkingProduct,
                    SubWorkingProduct: $scope.WorkingSubProduct,
                    ProjectedPrice: $scope.ProjectedPrice,
                    ProjectedCurrency: $("#ProjectedCurrency").val(),
                    ISBN: $scope.ISBN,
                    ProductIds: $scope.ListLinkedISBN,
                    EnteredBy: $("#enterdBy").val(),
                    ProductTypeId: parseInt($('input[name=ProductType]:checked').val()), // $scope.ProductType,
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
                         var ProductStatus = AJService.PostDataToAPI('ProductMaster/InsertKitISBN', kitIsbnModel);

                         ProductStatus.then(function (msg) {
                             if (msg.data.status == "notexistinisbnbag") {
                                 SweetAlert.swal("Try again!", "Entered Kit ISBN not exist in ISBN Bag List !", "info");
                             }
                             else if (msg.data.status == "otheruser") {
                                 SweetAlert.swal("Try again!", "This ISBN selected from other user !", "info");
                             }
                             else if (msg.data.status == "NotLicenseorContractEntered") {
                                 SweetAlert.swal("Error!", "Linked Product (" + msg.data.ISBN + ") has no any License or Contract !", "", "error");
                             }
                             else if (msg.data.status == "duplicateISBN") {
                                 SweetAlert.swal("Error!", "Duplicate. Kit ISBN already exist !", "", "error");
                             }
                             else if (msg.data.status != "OK") {
                                 SweetAlert.swal("Error!", "There is some problem. !", "", "error");
                             }
                             else {

                                 if ($('#hid_KitIsbnId').val() == "") {
                                     SweetAlert.swal({
                                         title: "Done",
                                         text: "Insert successfully.",
                                         type: "success"
                                     },
                                     function () {
                                         $('#hid_KitIsbnId').val();
                                         //document.location = GlobalredirectPath + "Product/ProductMaster/KitIsbn";
                                         document.location = GlobalredirectPath + "Product/ProductMaster/KitIsbnView?Id=" + msg.data.KitISBNId;
                                     });
                                 }
                                 else {
                                     SweetAlert.swal({
                                         title: "Done",
                                         text: "Updated successfully.",
                                         type: "success"
                                     },
                                     function () {
                                         $('#hid_KitIsbnId').val();
                                         //document.location = GlobalredirectPath + "Product/ProductMaster/KitIsbn";
                                         document.location = GlobalredirectPath + "Product/ProductMaster/KitIsbnView?Id=" + msg.data.KitISBNId;
                                     });
                                 }

                             }

                         },
                         function () {
                             SweetAlert.swal("Error!", "Please validate details!", "", "error");
                             return false;
                         });

                     }

                 });
        }
    }

    //$scope.filldata = function() {


    //    var GetKitISBN = AJService.GetDataFromAPI("ProductMaster/GetKitIsbnData?mstr_ISBN=" + "0");
    //    GetKitISBN.then(function (msg) {
    //        if (msg.data.length == 0) {
    //            //$scope.ShowKitISBNListForm = false;
    //            //swal("No record", 'No record found', "warning");
    //            blockUI.stop();
    //        }
    //        else {
    //            var KitISBNList = [];
    //            var _temp = '';
    //            for (var i = 0; i < msg.data.length; i++) {
    //                if (_temp != msg.data[i].ISBN) {
    //                    KitISBNList.push({ 'ISBN': msg.data[i].ISBN, 'KitISBNId': msg.data[i].KitISBNId });
    //                    _temp = msg.data[i].ISBN;
    //                }
    //            }

    //            $scope.KitIsbnDataLength = KitISBNList;
    //            $scope.KitIsbnData = msg.data;

    //        }
    //    });
    //}

    //$scope.filldata();

    //$scope.OnUpdateClick = function (id) {
    //    $scope.LinkedISBNList = [];

    //    for (var i = 0; i < $scope.KitIsbnDataLength.length; i++) {
    //        if ($scope.KitIsbnDataLength[i].KitISBNId == id) {
    //            $scope.ISBN = $scope.KitIsbnDataLength[i].ISBN;
    //            $('#hid_KitIsbnId').val($scope.KitIsbnDataLength[i].KitISBNId);

    //            var count = 0;
    //            for (var j = 0; j < $scope.KitIsbnData.length; j++) {
    //                if ($scope.KitIsbnData[j].KitISBNId == $scope.KitIsbnDataLength[i].KitISBNId) {
    //                    $scope.LinkedISBNList.push(count + 1);
    //                    $scope.LinkedISBN[count] = $scope.KitIsbnData[j].ProductISBN;
    //                    $scope.PreviousProductId[count] = $scope.KitIsbnData[j].ProductId;
    //                    count++;
    //                }
    //            }
               
    //            if ($scope.LinkedISBNList.length > 1) {
    //                $scope.btnRemoveShow = true;
    //            }
    //            else {
    //                $scope.btnRemoveShow = false;
    //            }
    //            setTimeout(function () {
    //                AutoCompleteISBN();
    //                var count = 0;
    //                $('input[type=text][name=LinkedISBN]').each(function () {
    //                    $(this).val($scope.LinkedISBN[count])
    //                    $(this)[0].nextElementSibling.value = $scope.PreviousProductId[count];
    //                    count++;
    //                });
    //            }, 200);
    //            break;
    //        }
    //    }
    //}


    $scope.OnDeleteClick = function (id) {
        var kitIsbnModel =
             {
                 Id: id,
                 EnteredBy: $("#enterdBy").val(),
             }

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
                     var ProductStatus = AJService.PostDataToAPI('ProductMaster/DeleteKitISBN', kitIsbnModel);

                     ProductStatus.then(function (msg) {

                         if (msg.data != "OK") {
                             SweetAlert.swal("Error!", "There is some problem. !", "", "error");
                         }
                         else {

                             SweetAlert.swal({
                                 title: "Done",
                                 text: "Delete successfully.",
                                 type: "success"
                             },
                             function () {
                                 $('#hid_KitIsbnId').val();
                                 document.location = GlobalredirectPath + "Product/ProductMaster/KitIsbn";

                             });
                         }

                     },
                     function () {
                         alert('Please validate details');
                     });

                 }

             });
    }

    var kitId = $('#KitId').val();
    if (kitId != undefined && kitId != null && kitId != "" && kitId != "0") {
        var mstr_ISBN = kitId == undefined ? "0" : kitId;

        var GetKitISBN = AJService.GetDataFromAPI("ProductMaster/GetKitIsbnDataById?Id=" + mstr_ISBN);
        GetKitISBN.then(function (msg) {
            if (msg.data.length == 0) {
                swal("No record", 'No record found', "warning");
                blockUI.stop();
            }
            else {
                $scope.KitIsbnDetails = msg.data;

                $scope.LinkedISBNList = [];

                if ($scope.KitIsbnDetails[0].KitISBNId == mstr_ISBN) {
                    $scope.DivSubDivCntrl.Division = $scope.KitIsbnDetails[0].Division;
                    //$scope.DivSubDivCntrl.SubDivision = $scope.KitIsbnDetails[0].SubDivision;
                    setTimeout(function () { 
                        $scope.DivSubDivCntrl = {
                            Division: $scope.KitIsbnDetails[0].Division,
                            SubDivision: $scope.KitIsbnDetails[0].SubDivision,
                        }
                        $scope.getSubDivisionbyDivisionIdList();
                    }, 100);

                    $scope.ProductCategory = $scope.KitIsbnDetails[0].ProductCategory;
                    $scope.ISBN = $scope.KitIsbnDetails[0].ISBN;
                    $scope.WorkingProduct = $scope.KitIsbnDetails[0].WorkingProduct;
                    $scope.WorkingSubProduct = $scope.KitIsbnDetails[0].SubWorkingProduct;
                    $scope.ProjectedPrice = $scope.KitIsbnDetails[0].ProjectedPrice;
                    $scope.ProjectedCurrency = $scope.KitIsbnDetails[0].ProjectedCurrency;
                    $('#hid_KitIsbnId').val($scope.KitIsbnDetails[0].KitISBNId);
                    $scope.ProductType = $scope.KitIsbnDetails[0].ProductTypeId;

                    var count = 0;
                    var linkedISBN = [];
                    var linkedISBNId = [];
                    linkedISBN = $scope.KitIsbnDetails[0].ProductISBN.split(',');
                    linkedISBNId = $scope.KitIsbnDetails[0].ProductISBNId.split(',');
                    for (var j = 0; j < linkedISBN.length; j++) {
                            $scope.LinkedISBNList.push(count + 1);
                            $scope.LinkedISBN[count] = linkedISBN[j];
                            $scope.PreviousProductId[count] = linkedISBNId[j];
                            count++;
                        }
                    

                    if ($scope.LinkedISBNList.length > 1) {
                        $scope.btnRemoveShow = true;
                    }
                    else {
                        $scope.btnRemoveShow = false;
                    }

                    setTimeout(function () {
                        AutoCompleteISBN();
                        var count = 0;
                        $('input[type=text][name=LinkedISBN]').each(function () {
                            $(this).val($scope.LinkedISBN[count])
                            $(this)[0].nextElementSibling.value = $scope.PreviousProductId[count];
                            count++;
                        });
                    }, 200);

                        
                    }
              


            }
        });
        return;
    }

});

