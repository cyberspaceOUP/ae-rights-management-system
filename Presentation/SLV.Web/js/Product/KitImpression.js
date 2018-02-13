/// <reference path="../master/Master.Division.js" />

app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {
   
    app.expandControllerTopSearch($scope, AJService, $window);

    app.expandControllerKitISBNLIst($scope, AJService);
   
    app.expandControllerProductKitIsbnDetilsList($scope, AJService);


    $scope.getImpressionDetails = function (Id, Flag) {
        var KitISBN = {
            Id: Id,
            Flag: Flag
        };
        var ImpressionDetails = AJService.PostDataToAPI('ProductMaster/KitISBNDetails', KitISBN);
        ImpressionDetails.then(function (ImpressionData) {
            $scope.ImpressionList = ImpressionData.data;
        }, function () {
            //alert('Error in Getting Impression Details');
        });

    }


   
    if ($('[id*=hid_KitIsbnId]').val() != "" && $('[id*=hid_KitIsbnId]').val() != undefined ) {
        $scope.KitISBNList($('[id*=hid_KitIsbnId]').val(), "K");
        $scope.ProductKitIsbnListDetails($('[id*=hid_KitIsbnId]').val(), "P");
        $scope.getImpressionDetails($('[id*=hid_KitIsbnId]').val(), "e");
    }

    



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

    
    function ValidionCheck() {
     
        var _hid_TotalQty = $('[id*=hid_TotalQty_]');
        var hid_NoOffImpr = $('[id*=hid_NoOffImpr_]');
        var hid_balanceqty = $('[id*=hid_balanceqty_]');
        var txtImpressionQty = $('[id*=txtImpressionQty]');
        var txtImpressionDate = $('[id*=txtImpressionDate]');
        if (txtImpressionQty.val() != "" && txtImpressionDate.val() != "")
        {
            for (var i =0; i < _hid_TotalQty.length; i++) {
                if ($(_hid_TotalQty[i]).val().trim().toLowerCase() != "" && $(_hid_TotalQty[i]).val().trim().toLowerCase() != "unrestricted") {

                    if ($(hid_NoOffImpr[i]).val() == 0 && $(hid_NoOffImpr[i]).val().trim() !="")
                    {
                       
                        SweetAlert.swal("Validation!", "There is no impression remaining", "", "error");
                        return 1;

                    }
                    else {
                        if($(hid_balanceqty[i]).val().trim() !="")
                        {
                            if (parseFloat(txtImpressionQty.val()) > parseFloat($(hid_balanceqty[i]).val().trim())) {
                               
                                SweetAlert.swal("Validation!", "Please enter valid impression, impression can not more than product license balance  qty", "", "error");
                                txtImpressionQty.focus();
                                return 1;
                                
                            }
                        } else if ($(_hid_TotalQty[i]).val().trim() != "") {
                            if (parseFloat(txtImpressionQty.val()) > parseFloat($(_hid_TotalQty[i]).val().trim())) {
                               
                                SweetAlert.swal("Validation!", "Please enter valid impression, impression can not more than product license total qty", "", "error");
                                txtImpressionQty.focus();
                                return 1;

                            } 

                        }

                    }
                   
                }

            }

       }

    }
    

    $scope.kitIsbnEntryForm = function () {
                        
        $scope.submitted = true;
     //   $scope.ValidionCheck();

     
        if (ValidionCheck() == 1) {
            $scope.userForm.$valid = false;
            return false;
        }
        

            if ($scope.userForm.$valid) {
                $scope.KitISBNEntry();
                // set form default state
                //$scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = true;
                return;
            }      
    }

    $scope.KitISBNEntry = function () {
       
        $scope.submitted = true;
        if ($scope.userForm.$valid) {

            var obj = [];
            var _Id = 1;
            var _hid_TotalQty = $('[id*=hid_TotalQty_]');
            for (var i = 0; i < _hid_TotalQty.length; i++) {

                var mint_ProductLicense = $("#hid_ProductLic_" + _Id).val();
                var mint_ProductId = $("#hid_ProductId_" + _Id).val();
                if ($("#hid_balanceqty_" + _Id).val().trim() != "") {
                    var mint_balanceqty =  parseFloat($("#hid_balanceqty_" + _Id).val() - parseFloat($('#txtImpressionQty').val()));
                } else {
                    var mint_balanceqty = parseFloat($('#txtImpressionQty').val());
                }
                if ($("#hid_NoOffImpr_" + _Id).val().trim() != "") {
                    var mint_NoOfImpressions = parseFloat($("#hid_NoOffImpr_" + _Id).val()) - 1;
                } else {
                    var mint_NoOfImpressions = ""
                }
                var mint_ProductId = $("#hid_ProductId_" + _Id).val();
                var mint_ImpressionNo = 0;
                var mstr_ImpressionDate = ConverDate($('#txtImpressionDate').val());
                var mint_QunatityPrinted = parseFloat($('#txtImpressionQty').val());
                var mint_AuthorContractId = $("#hid_AuthorContractId_" + _Id).val();
                var mstr_Check_NoOfImpressions = $("#hid_Check_NoOfImpressions_" + _Id).val();
                var mint_KitIsbnId = $('#hid_KitIsbnId').val();
                var mstr_Unrestricted_Check = $("#hid_Unrestricted_Check_" + _Id).val();

                obj[i] =
               {
                   ProductLicenseId: mint_ProductLicense,
                   ProductId: mint_ProductId,
                   BalanceQty: mint_balanceqty,
                   NoOfImpressions: mint_NoOfImpressions,
                   ImpressionNo: mint_ImpressionNo,
                   ImpressionDate: mstr_ImpressionDate,
                   QunatityPrinted: mint_QunatityPrinted,
                   ContractId: mint_AuthorContractId,
                   KitISBNId: mint_KitIsbnId,
                   Check_NoOfImpressions: mstr_Check_NoOfImpressions,
                   EnteredBy: $("#enterdBy").val(),
                   Unrestricted_Check: mstr_Unrestricted_Check
                   };

                _Id = _Id + 1;
            }
            var Object = { ProductKitIsbn: obj };

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
                         var ProductStatus = AJService.PostDataToAPI('ProductMaster/InsertProductKitISBN', Object);
                       
                         ProductStatus.then(function (msg) {
                            
                             if (msg.data.status != "OK") {
                                 SweetAlert.swal("Error!", "There is some problem. !", "", "error");
                             } else {
                                 SweetAlert.swal({
                                     title: "Insert successfully.",
                                     text: "",
                                     type: "success"

                                 },function () {
                                     window.location.href = window.location.href;

                                 });

                             }

                         },
                         function () {
                             SweetAlert.swal("Error!", "There is some problem. !", "", "error");
                             return false;
                        
                         });

                     }

                 });
        }
    }

    $scope.filldata = function() {


        var GetKitISBN = AJService.GetDataFromAPI("ProductMaster/GetKitIsbnData?mstr_ISBN=" + "0");
        GetKitISBN.then(function (msg) {
            if (msg.data.length == 0) {
                //$scope.ShowKitISBNListForm = false;
                //swal("No record", 'No record found', "warning");
                blockUI.stop();
            }
            else {
                var KitISBNList = [];
                var _temp = '';
                for (var i = 0; i < msg.data.length; i++) {
                    if (_temp != msg.data[i].ISBN) {
                        KitISBNList.push({ 'ISBN': msg.data[i].ISBN, 'KitISBNId': msg.data[i].KitISBNId });
                        _temp = msg.data[i].ISBN;
                    }
                }

                $scope.KitIsbnDataLength = KitISBNList;
                $scope.KitIsbnData = msg.data;

            }
        });
    }

    $scope.filldata();

    $scope.SetEffectiveDate = function (datetext) {
        if ($scope.txtImpressionDate == undefined && $scope.txtImpressionDate !== $(datetext).val()) {
            $scope.txtImpressionDate = $(datetext).val();
        }
    }
    

 


    function ConverDate(value) {

        var _date = value.split("/")
        var DD = _date[0];
        var MM = _date[1];
        var YYYY = _date[2];
        return MM + "/" + DD + "/" + YYYY;
    }

});

