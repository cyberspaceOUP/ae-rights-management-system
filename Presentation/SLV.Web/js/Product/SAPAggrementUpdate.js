app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {
    app.expandControllerProductDetails($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);

    $scope.SAPRow = [];
    $scope.AuthorRow = [];
    $scope.SAPRow.push(1);

    var mstr_authorIdValue = 0;
    var count = 0;
    var count2 = 0;
    var mstr_authorId = $('#hid_SapAggValue')

    $scope.getProductAuthorList = function () {

        var _productMaster = {
            ProductId: $("[name$=hid_sapproductId]").val(),
        }

        var ProductAuthorList = AJService.PostDataToAPI("ProductMaster/getProductAuthor", _productMaster)
        ProductAuthorList.then(function (AuthorList) {
            var Author = AuthorList.data;

            for (i = 0; i < Author.length; i++) {
                $scope.AuthorRow.push(Author[i]);
            }

        },
        function () {
            //alert('There is some error in the system');
        });

    }

    $scope.getProductAuthorList();
    $scope.addSapRow = function () {
        var i = $scope.SAPRow.length + 1;
        $scope.SAPRow.push(i);
    }

    $scope.removeSapRow = function (index) {
        $scope.SAPRow.splice(index, 1);
    }

    //Call on Button Submit
    $scope.SAPAggrementEntryForm = function () {
        $scope.submitted = true;

        var _SAPAggrementNo = $('[name*=SAPAggrementNo_]');
        var _AuthorCode = $('[name*=AuthorCode_]');

        for (var i = 0; i < _SAPAggrementNo.length; i++) {
            if ($('[name*=SAPAggrementNo_' + [i] + ']').val() == "") {
                SweetAlert.swal("Message", "Please enter SAP Agreement No", "warning");
                $('[name*=SAPAggrementNo_' + [i] + ']').focus();
                return false;
            }
        }

        for (var i = 0; i < _AuthorCode.length; i++) {
            if ($('[name*=AuthorCode_' + [i] + ']').val() == "") {
                SweetAlert.swal("Message", "Please enter Author Code", "warning");
                $('[name*=AuthorCode_' + [i] + ']').focus();
                return false;
            }
        }



        if ($scope.userForm.$valid) {
            var marr_SAPAggremenet = [];
            var marr_SAPAggrementNo = [];
            var marr_AuthorCode = [];
            //var marr_AuthorId = [];

            $("[name*=SAPAggrementNo_]").each(function () {
                if ($(this).val() != "" && $(this).val() != undefined) {
                    var _mobj = {
                        OUPISBN: $("[name$=hid_oupisbn]").val(),
                        SAPagreementNo: $(this).val(),
                        //AuthorId: $("#hid_SapAggAuthorId_" + count2).val(),
                        Id: $("#hid_SAPAggrementId_" + count2).val(),

                        EnteredBy: $("#enterdBy").val(),
                    }
                    count2 = count2 + 1;
                    marr_SAPAggremenet.push(_mobj);
                    marr_SAPAggrementNo.push(_mobj.SAPagreementNo);
                }
            });

            $("[name*=AuthorCode_]").each(function () {
                if ($(this).val() != undefined && $(this).val() != "") {                   
                            //var marr_AuthorId = $("#hid_SapAggAuthorId_" + i).val();
                            var _mobj = {
                                OUPISBN: $("[name$=hid_oupisbn]").val(),
                                AuthorCode: $(this).val(),
                                //AuthorId: $("#hid_SapAggAuthorId_" + count).val(),
                                Id: $("#hid_AuthorCodeId_" + count).val(),

                                EnteredBy: $("#enterdBy").val(),
                            }                     
                    count= count + 1;                       
                    marr_SAPAggremenet.push(_mobj);
                    marr_AuthorCode.push(_mobj);
                }
            });


            var duplicates = marr_SAPAggrementNo.reduce(function (acc, el, i, arr) {
                if (arr.indexOf(el) !== i && acc.indexOf(el) < 0) acc.push(el); return acc;
            }, []);

            if (duplicates.length != 0)
            {

                SweetAlert.swal("Message", "SAP Agreement Nos. are duplicate.", "warning");
               // alert('SAP Agreement Nos. are duplicate.');
                return false;
            }

            var duplicates = marr_AuthorCode.reduce(function (acc, el, i, arr) {
                if (arr.indexOf(el) !== i && acc.indexOf(el) < 0) acc.push(el); return acc;
            }, []);

            if (duplicates.length != 0) {
                alert('Author Code are duplicate.');
                return false;
            }


            if (marr_SAPAggremenet.length == 0) {
                var textbox = $("[name*=SAPAggrementNo]")[0];

                if ($("[name*=SAPAggrementNo]").length > 1) {
                    SweetAlert.swal("Validation", "Please enter at least one SAP Agreement No and Author Code", "error");
                }
                else {
                   // $(textbox).closest(".form-group").addClass("has-error");
                    // $(textbox).next().find('p').addClass('ng-show').removeClass("ng-hide");

                    SweetAlert.swal("Validation", "Please enter SAP Agreement No and Author Code", "error");
                }

               

               // $('.validCheck').css("display", "none");
              
                $(textbox).focus();
                $scope.Serchform.$valid = false;
            }


            if ($scope.userForm.$valid) {
                $scope.SAPEntry(marr_SAPAggremenet);
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    }
            

    $scope.SAPEntry = function (marr_SAPAggremenet) {


        // check that Id have value or not     
       
            // initialize variable for fetching data 
          
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
                   
                     // call API to fetch temp Department list basis on the FlatId
                     var ProductStatus = AJService.PostDataToAPI('ProductMaster/UpdateSAPAggrementDetails', marr_SAPAggremenet);
                     ProductStatus.then(function (msg) {

                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data, '', "error");
                         }
                         else {


                             SweetAlert.swal({
                                 title: "SAP Agreement No/ Author Code Updated successfully.",
                                 text: "",
                                 type: "success"
                             },
                             function () {
                                 window.location.href = window.location.href;

                             });

                         }

                     },
                function () {
                    SweetAlert.swal("", "Please validate details.", "warning");
                    //alert('There is some error in the system');
        
                });
                 }

             });
       
    }
    
    $scope.getProductDetails = function () {

        var _SAPProductDetails = {
            OUPISBN: $('#hid_oup_isbn').val(),
        }
        
        var ProductAuthorListFill = AJService.PostDataToAPI("ProductMaster/getSapProductDetails", _SAPProductDetails)
        ProductAuthorListFill.then(function (msg) {
            
           
            $scope.ProductId = msg.data;
            $scope.ProductSerach($scope.ProductId);
            $('[id*=hid_productId]').val($scope.ProductId)

        },
        function () {
            //alert('There is some error in the system');
        });

    }

    if ($('#hid_oup_isbn').val() != "" && $('#hid_oup_isbn').val() != undefined && $('#hid_oup_isbn').val() != null) {       
        $scope.getProductDetails();
    }

    $scope.getSAPAggreementAuthorDetails = function () {

      
        var SAPAgreement = {
            OUPISBN: $('#hid_oup_isbn').val()
        };

        var getSAPAggreementAuthorDetails = AJService.PostDataToAPI('ProductMaster/SAPAggreementAuthor_viewDetails', SAPAgreement);
        getSAPAggreementAuthorDetails.then(function (msg) {
           
            $scope.SAPAggreementAuthorDetails = msg.data;

        }, function () {
            //alert('Error in getting Product list');
        });

    };

    setTimeout(function () {
        $scope.getSAPAggreementAuthorDetails();      
    }, 300);
   
    setTimeout(function () {
        $scope.getProductAuthorFill();
    }, 500)
    
    $scope.getProductAuthorFill = function () {

        var _SAPAggrementAssignFill = {
            OUPISBN: $('#hid_oup_isbn').val(),
        }

        var ProductAuthorListFill = AJService.PostDataToAPI("ProductMaster/getProductAuthorFill", _SAPAggrementAssignFill)
        ProductAuthorListFill.then(function (AuthorListUpdate) {

            
            if (AuthorListUpdate.data._SAPagreementNo.length > 0) {

                for (var i = 0; i < AuthorListUpdate.data._SAPagreementNo.length; i++) {
                    $('[name*=SAPAggrementNo_' + [i] + ']').val(AuthorListUpdate.data._SAPagreementNo[i].SAPagreementNo);
                    $('[name*=hid_SAPAggrementId_' + [i] + ']').val(AuthorListUpdate.data._SAPagreementNo[i].Id);
               
                   
                }
            }


            if (AuthorListUpdate.data._AuthorCode.length > 0) {

                for (var i = 0; i < AuthorListUpdate.data._AuthorCode.length; i++) {
                    $('[name*=AuthorCode_' + [i] + ']').val(AuthorListUpdate.data._AuthorCode[i].AuthorCode);
                    $('[name*=hid_AuthorCodeId_' + [i] + ']').val(AuthorListUpdate.data._AuthorCode[i].Id);
                   
                }
            }
          //  $scope.RequireValid = false;

        },
        function () {
            alert('There is some error in the system');
        });

    }
   

});