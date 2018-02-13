
app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {

    /*
    
   Here is the service used for inserting the Product Type List.
   Bind data In the object and send to api for inserting and update.
   Common function has used for both updation and Insertion
   
   */
    app.expandControllerTopSearch($scope, AJService, $window);
    app.expandControllerA($scope, AJService, $window);

    $scope.AddProductType = function () {
        blockUI.start();
        var productType = {
            typeName: $scope.producttype,
            Id: $('#hid_productType').val() == "" ? 0 : $('#hid_productType').val(),
            EnteredBy: $("#enterdBy").val()
        };
      
        // var productTypeStatus = AJService.PostDataToAPI('Master/InsertProductType', productType);
        var productTypeStatus = AJService.PostDataToAPI('ProductType/InsertProductType', productType);
        productTypeStatus.then(function (msg) {
            if (msg.data != "OK") {

                SweetAlert.swal("Error!", "Duplicate. already exist !", "", "error");
            }
            else {
                if ($('#hid_productType').val() != "") {
                    SweetAlert.swal('Updated successfully.', '', "success");

                }
                else {
                    SweetAlert.swal('Insert successfully.', '', "success");
                }
                {
                    $scope.producttype = "";
                    angular.element(document.getElementById('angularid')).scope().getProductTypeMasterList();

                }

            }
            {
               // $scope.name = "";
            }
        },


        function () {
            alert('Please validate details');
        });
        blockUI.stop();
    }

    $scope.AddSubProductType = function () {
        blockUI.start();
        var productType = {
            parenttypeid: $scope.productType,
            typeName: $scope.subProductType,
            Id: $('#hid_productType').val() == "" ? 0 : $('#hid_productType').val(),
            EnteredBy: $("#enterdBy").val()
        };
        
        var productTypeStatus = AJService.PostDataToAPI('ProductType/InsertProductType', productType);
        productTypeStatus.then(function (msg) {
            if (msg.data != "OK") {

                SweetAlert.swal("Error!", "Duplicate. already exist !", "", "error");
            }
            else {
                if ($('#hid_productType').val() != "") {
                    SweetAlert.swal('Updated successfully.', '', "success");

                }
                else {
                    SweetAlert.swal('Insert successfully.', '', "success");
                }
                {
                    $scope.productType = "";
                    $scope.subProductType = "";
                    angular.element(document.getElementById('angularid')).scope().getProductTypeList();
                    angular.element(document.getElementById('angularid')).scope().getSubProductTypeMasterList();

                }

            }
            {
                // $scope.name = "";
            }
        },


        function () {
            alert('Please validate details');
        });
        blockUI.stop();
    }

    //Added by Suranjana on 21/07/2016
    //Get Product Type Master List
    $scope.getProductTypeMasterList = function () {
        var getProductTypeList = AJService.GetDataFromAPI("ProductType/GetProductTypeListForMaster?Id=" + $("#enterdBy").val());
        getProductTypeList.then(function (ProductType) {
            $scope.ProductTypeListNew = ProductType.data;
        }, function () {
            alert('Error in getting Product Type list');
        });
    }

    //Get Sub Product Type Master List
    $scope.getSubProductTypeMasterList = function () {
        var getSubProductTypeList = AJService.GetDataFromAPI("ProductType/GetSubProductTypeListFroMaster?Id=" + $("#enterdBy").val());
        getSubProductTypeList.then(function (SubProductType) {
            $scope.SubProductTypeList = SubProductType.data;
        }, function () {
            alert('Error in getting Sub-Product Type list');
        });
    }
    //Ended by Suranjana

    //Delete ProductType details on basis of ID
    $scope.DeleteProductType = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            var productTypeData = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this product type detail! ",
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
                     var productTypeStatus = AJService.PostDataToAPI('ProductType/ProductTypeDelete', productTypeData);
                     productTypeStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                             blockUI.stop();
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             angular.element(document.getElementById('angularid')).scope().getProductTypeList();
                             angular.element(document.getElementById('angularid')).scope().getProductTypeMasterList();
                             angular.element(document.getElementById('angularid')).scope().getSubProductTypeMasterList();

                             blockUI.stop();
                         }
                     });
                 }
             });
        }
        else {
            SweetAlert.swal("Error!", "Record is not Deleted", "", "error");
        }
        blockUI.stop();
    }

    //Delete Sub ProductType details on basis of ID
    $scope.DeleteSubProductType = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            var productTypeData = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this sub-product type detail! ",
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
                     var productTypeStatus = AJService.PostDataToAPI('ProductType/ProductTypeDelete', productTypeData);
                     productTypeStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                             blockUI.stop();
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             angular.element(document.getElementById('angularid')).scope().getProductTypeList();
                             angular.element(document.getElementById('angularid')).scope().getProductTypeMasterList();
                             angular.element(document.getElementById('angularid')).scope().getSubProductTypeMasterList();

                             blockUI.stop();
                         }
                     });
                 }
             });
        }
        else {
            SweetAlert.swal("Error!", "Record is not Deleted", "", "error");
        }
        blockUI.stop();
    }

    //Edit Product Type basis on The recordID
    $scope.EditProductType = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var productTypeData = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            blockUI.start();
            // call API to fetch temp product type list basis on the FlatId
            var productTypeStatus = AJService.PostDataToAPI('ProductType/ProductType', productTypeData); // getProductTypeId is action controller
            productTypeStatus.then(function (msg) {
                if (msg != null) {
                 
                     $scope.producttype = msg.data.typeName
                   
                    $('#btnSubmit').html("Update");
                    $('#hid_productType').val(msg.data.Id);
                    $('#btnReset').show();
                    blockUI.stop();
                }
                else {
                    SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                    blockUI.stop();
                }

            });
        }
    }

    //Edit Sub Product Type basis on The recordID
    $scope.EditSubProductType = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var productTypeData = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            blockUI.start();
            // call API to fetch temp product type list basis on the FlatId
            var productTypeStatus = AJService.PostDataToAPI('ProductType/ProductType', productTypeData); // getProductTypeId is action controller
            productTypeStatus.then(function (msg) {
                if (msg != null) {

                    $scope.productType = msg.data.parenttypeid,
                    $scope.subProductType=msg.data.typeName

                    $('#btnSubmit').html("Update");
                    $('#hid_productType').val(msg.data.Id);
                    $('#btnReset').show();
                    blockUI.stop();
                }
                else {
                    SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                    blockUI.stop();
                }

            });
        }
    }


    $scope.submitForm = function () {
        $scope.submitted = true;
        if ($scope.productTypeForm.$valid) {
                $scope.AddProductType();
                // set form default state
                $scope.productTypeForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
    }


    $scope.submitSubProductForm = function () {
        $scope.submitted = true;
        if ($scope.subProductForm.$valid) {
            $scope.AddSubProductType();
            // set form default state
            $scope.subProductForm.$setPristine();
            // set form is no submitted
            $scope.submitted = false;
            return;
        }
    }


    $scope.resetForm = function ()
    {
        $scope.producttype = "";
        angular.element(document.getElementById('angularid')).scope().getProductTypeList();
        $('#btnSubmit').html("Submit");
        $('#btnReset').hide();
        $scope.productTypeForm.$setPristine();
    }
  
});
