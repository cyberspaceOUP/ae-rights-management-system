/// <reference path="../../Areas/PaymentTagging/Views/Shared/_PaymentTaggingEntryLayout.cshtml" />
/// <reference path="../../Areas/PaymentTagging/Views/PaymentTagging/PaymentTaggingSearch.cshtml" />
/// <reference path="../../Areas/PaymentTagging/Views/PaymentTagging/PaymentTaggingSearch.cshtml" />
/// <reference path="../../Areas/PaymentTagging/Views/PaymentTagging/PaymentTaggingSearch.cshtml" />

app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {

    /*Expand common.master.js Controller*/
    app.expandControllerA($scope, AJService, $window);


    var AuthorId = $('#hid_AuthorId').val();
    var AuthorContractId = $('#hid_AuthorContractId').val();
    var PublishingCompanyId = $('#hid_PublishingCompanyId').val();
    var ProductLicenseId = $('#hid_ProductLicenseId').val();

    var RightsSellingId = $('#hid_RightsSellingId').val();

    $scope.GetPaymentTaggingSubSidiaryRights = function () {

        //--------for get Rights Selling Code
        if (RightsSellingId != "") {
            var getRightsSellingMasterdata = AJService.GetDataFromAPI("RightsSelling/GetRightsSellingMasterDetailById?RightsSellingId=" + RightsSellingId);
            getRightsSellingMasterdata.then(function (msg1) {
                if (msg1.data != 0) {
                    $scope.RightsSellingCode = msg1.data;
                }
                else {
                    SweetAlert.swal({
                        title: "Try agian",
                        text: "Rights Selling Master detail not found",
                        type: "info"
                    },
                  function () {
                      $window.location.href = GlobalredirectPath + 'PaymentTagging/PaymentTagging/PaymentTaggingSearch?For=Rights&From=BackToSearch';
                  });
                }
            }, function () {
                //alert('Error in getting data');
            });
        }
        //---------------------------

        if (AuthorId != "" || AuthorContractId != "") {
            var getPaymentTaggingSubSidiaryRightsList = AJService.GetDataFromAPI("RightsSelling/GetPaymentTaggingSubSidiaryRights?AuthorId=" + AuthorId + '&AuthorContractId=' + AuthorContractId);
            getPaymentTaggingSubSidiaryRightsList.then(function (msg) {
                if (msg.data._GetAuthorReport.length != 0) {
                    $scope.MainArray = [];
                    $scope.SecondryArray = [];

                    // var k = 0;

                 
                    for (var i = 0; i < msg.data.distictAuthorId.length; i++) {
                        var AuthorId = msg.data.distictAuthorId[i].AuthorId;
                        for (var j = 0; j < msg.data._GetAuthorReport.length; j++) {
                            if (msg.data._GetAuthorReport[j].AuthorId == AuthorId) {
                                $scope.SecondryArray.push(msg.data._GetAuthorReport[j]);
                                //  k++;
                            }
                        }
                        $scope.MainArray.push($scope.SecondryArray);

                       
                        $scope.SecondryArray = [];

                    }

                    $scope.PaymentTaggingSubSidiaryRightsList = msg.data;                    
                }
                else {
                    SweetAlert.swal({
                        title: "Try agian",
                        text: "SubSidiary Rights Not Entered",
                        type: "info"
                    },
                  function () {
                      $window.location.href = GlobalredirectPath + 'PaymentTagging/PaymentTagging/PaymentTaggingSearch?For=Rights&From=BackToSearch';
                  });
                }
            }, function () {
                //alert('Error in getting list');
            });
            
        }
        else if (PublishingCompanyId != "" || ProductLicenseId != "") {
            var getPaymentTaggingSubSidiaryRightsList = AJService.GetDataFromAPI("RightsSelling/GetPaymentTaggingSubSidiaryRightsByPublishingCompany?PublishingCompanyId=" + PublishingCompanyId + '&ProductLicenseId='+ProductLicenseId);
            getPaymentTaggingSubSidiaryRightsList.then(function (msg) {
                if (msg.data._GetAuthorReport.length != 0) {
                    $scope.MainArray = [];
                    $scope.SecondryArray = [];
                 
                    // var k = 0;


                    for (var i = 0; i < msg.data.distictAuthorId.length; i++) {
                        var AuthorId = msg.data.distictAuthorId[i].AuthorId;
                        for (var j = 0; j < msg.data._GetAuthorReport.length; j++) {
                            if (msg.data._GetAuthorReport[j].AuthorId == AuthorId) {
                                $scope.SecondryArray.push(msg.data._GetAuthorReport[j]);
                                //  k++;
                            }
                        }
                        $scope.MainArray.push($scope.SecondryArray);
                        $scope.SecondryArray = [];

                    }

                    $scope.PaymentTaggingSubSidiaryRightsList = msg.data;
                }
                else {
                    SweetAlert.swal({
                        title: "Try agian",
                        text: "SubSidiary Rights Not Entered",
                        type: "info"
                    },
                  function () {
                      $window.location.href = GlobalredirectPath + 'PaymentTagging/PaymentTagging/PaymentTaggingSearch?For=Rights&From=BackToSearch';
                  });
                }
            }, function () {
                //alert('Error in getting list');
            });
        }
    }


    $scope.BindSubSidiaryRights = function () {

        
        if (AuthorId != "" || AuthorContractId != "") {
          
            var getGetPaymentTaggingSubSidiaryRightsByAuthorcontractList = AJService.GetDataFromAPI("PermissionsOutbound/SubSidiaryRightsByAuthorcontract?authorcontractid=" + AuthorContractId);
            getGetPaymentTaggingSubSidiaryRightsByAuthorcontractList.then(function (msg) {
               
                $scope.PaymentTaggingSubSidiaryRightsByAuthorcontractList = msg.data;
             

            }, function () {
                //alert('Error in getting SubSidiary Rights list');
            });
        }
        else if (PublishingCompanyId != "" || ProductLicenseId != "") {

            var getGetPaymentTaggingSubSidiaryRightsByAuthorcontractList = AJService.GetDataFromAPI("PermissionsOutbound/SubSidiaryRightsByProductLicense?ProductLicenseId=" + ProductLicenseId);
            getGetPaymentTaggingSubSidiaryRightsByAuthorcontractList.then(function (msg) {
                $scope.PaymentTaggingSubSidiaryRightsByAuthorcontractList = msg.data;

            }, function () {
               // alert('Error in getting SubSidiary Rights list');
            });
        } else {
            $scope.PaymentTaggingSubSidiaryRightsByAuthorcontractList = undefined;
        }
    }

    $scope.BindSubSidiaryRights();

    $scope.RightsSellingPaymentTaggingList = function () {

        var RightsSelling = {
          
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val()
         
        };

        var RightsSelling = AJService.PostDataToAPI('RightsSelling/InsertRightsSellingHistory', RightsSelling);
        RightsSelling.then(function (msg) {
           // blockUI.stop();

            if (msg.data == "OK") {
                // $scope.ProductLicenseListResult();
                window.location.href = GlobalredirectPath + 'RightsSelling/RightsSelling/RightsSellingSearch?For=BackToList';
            }

        }, function () {
            $scope.IsError = false;
        });

        return;
    };

    app.expandControllerProductDetails($scope, AJService, $window);
  
    $scope.ProductSerach($('#hid_ProductId').val());
   
    setTimeout(function () {
        //fetch Kit Details List
        app.expandControllerKitISBNLIst($scope, AJService);
        angular.element(document.getElementById('angularid')).scope().KitISBNDetailsList($('#hid_ProductId').val());
    }, 300);

    app.expandControllerAuthorContractDetails($scope, AJService, $window);

    app.expandControllerProductLicense($scope, AJService, $window);

    app.expandControllerRightsSellingLIst($scope, AJService, $window);

    if ($('[id*=hid_AuthorContractId]').val() != "" && $('[id*=hid_AuthorContractId]').val() != undefined) {
        $scope.AuthorContract($("#hid_AuthorContractId").val());

        $scope.Req_ContractDeatil = true;
        $scope.Req_ProductLicense = false;
    } else if ($('[id*=hid_ProductLicenseId]').val() != "" && $('[id*=hid_ProductLicenseId]').val() != undefined) {
        $scope.Req_ProductLicense = true;
        $scope.Req_ContractDeatil = false;
        $scope.ProductLicenseSerach($("#hid_ProductLicenseId").val());
    }

    if ($('[id*=hid_RightsSellingId]').val() != "" && $('[id*=hid_RightsSellingId]').val() != undefined) {
        $scope.RightsSelling_Detail($('[id*=hid_RightsSellingId]').val());
    }

 

    $scope.ConverisonRate = 1;
   
    $scope.AuthorContract = [];
    $scope.subsidiaryrights = [];
    $scope.Percentage = [];
    $scope.PaymentMode = [];
    $scope.ChequeNumber = [];
    $scope.BankName = [];
    $scope.Amount = [];
    $scope.AuthorAmount = [];
    $scope.AuthorId = [];

    $scope.CommanDetails = [];
    $scope.CommanDetails.fill(1);
   
    $scope.PaymentTaggingEntry = function () {
        var obj = [];
        var count = 0;
        if (AuthorId != "" || AuthorContractId != "") {
            for (var i = 0; i < $scope.PaymentTaggingSubSidiaryRightsList._GetAuthorReport.length; i++) {
                if ($('#chk_Contractld_' + [i]).prop('checked') == true) {
                    obj[count] =
                              {
                                  //ContractId: $scope.AuthorContract[i],
                                  ContractId: $('#chk_Contractld_' + [i]).val(),
                                  subproducttypeid: $('#subsidiaryrights').val(),
                                  Percentage: $('#OUP_Percentage').val(),
                                  PaymentMode: $('input[name=PaymentModeList]:checked').val(),
                                  ChequeNumber: $('#ChequeNumber').val(),
                                  BankName: $('#BankName').val(),
                                  Amount: $('#Receipt_Amount').val(),
                                  AuthorAmount: $('#AuthorAmount_' + [i]).val(),
                                  OupAmount: $('#OupAmount').val(),
                                  ChequeDate: convertDateMDY($('#ChequeDate').val()),
                                  EnteredBy: $("#enterdBy").val(),
                                  ProductLicenseId: null,
                                  AuthorId: $('#hid_AuthorId_' + [i]).val(),
                                  RightsSellingId: $('#hid_RightsSellingId').val(),
                                  PublishingCompanyId: null,
                                  WithHoldingTax: $('#WHT').val(),
                                  ConverisonRate: $('#ConverisonRate').val(),
                              };
                    count++;
                }
            }
        }
        else if (PublishingCompanyId != "" || ProductLicenseId != "") {
            for (var i = 0; i < $scope.PaymentTaggingSubSidiaryRightsList._GetAuthorReport.length; i++) {
                if ($('#chk_Contractld_' + [i]).prop('checked') == true) {
                    obj[count] =
                              {
                                  //ContractId: $scope.AuthorContract[i],
                                  ContractId: null,
                                  subproducttypeid: $('#subsidiaryrights').val(),
                                  Percentage: $('#OUP_Percentage').val(),
                                  PaymentMode: $('input[name=PaymentModeList]:checked').val(),
                                  ChequeNumber: $('#ChequeNumber').val(),
                                  BankName: $('#BankName').val(),
                                  Amount: $('#Receipt_Amount').val(),
                                  AuthorAmount: $('#PublisherAmt_' + [i]).val(),
                                  OupAmount: $('#OupAmount').val(),
                                  ChequeDate: convertDateMDY($('#ChequeDate').val()),
                                  EnteredBy: $("#enterdBy").val(),
                                  ProductLicenseId: $('#chk_Contractld_' + [i]).val(),
                                  AuthorId: null,
                                  RightsSellingId: $('#hid_RightsSellingId').val(),
                                  PublishingCompanyId: $('#hid_PublishingCompanyId_' + [i]).val(),
                                  WithHoldingTax: $('#WHT').val(),
                                  ConverisonRate: $('#ConverisonRate').val(),
                              };
                    count++;
                }
            }
        }


        var Object = { RightsSellingPaymentTagging: obj };

        
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

                     var PermissionsOutboundStatus = AJService.PostDataToAPI('RightsSelling/InsertRightsSellingPaymentTagging', Object);

                     PermissionsOutboundStatus.then(function (msg) {

                         if (msg.data == "OK") {

                             SweetAlert.swal({
                                 title: "Inserted successfully.",
                                 text: "",
                                 type: "success"
                             },
                               function () {
                                  

                                   if (AuthorId != "" || AuthorContractId != "") {
                                       if (AuthorId != "" && AuthorContractId != "") {
                                           $window.location.href = GlobalredirectPath + 'Report/Report/StatementView?For=Rights&AId=' + AuthorId + '&ACId=' + AuthorContractId;
                                       }
                                       else {
                                           if (AuthorId != "") {
                                               $window.location.href = GlobalredirectPath + 'Report/Report/StatementView?For=Rights&AId=' + AuthorId;
                                           }
                                           else {
                                               $window.location.href = GlobalredirectPath + 'Report/Report/StatementView?For=Rights&ACId=' + AuthorContractId;
                                           }
                                       }
                                   }
                                   else if (PublishingCompanyId != "" || ProductLicenseId != "") {
                                       if (PublishingCompanyId != "" && ProductLicenseId != "") {
                                           $window.location.href = GlobalredirectPath + 'Report/Report/StatementView?For=Rights&PubId=' + PublishingCompanyId + '&PLId=' + ProductLicenseId;
                                       }
                                       else {
                                           if (PublishingCompanyId != "") {
                                               $window.location.href = GlobalredirectPath + 'Report/Report/StatementView?For=Rights&PubId=' + PublishingCompanyId;
                                           }
                                           else {
                                               $window.location.href = GlobalredirectPath + 'Report/Report/StatementView?For=Rights&PLId=' + ProductLicenseId;
                                           }
                                       }
                                   }

                               });

                             //SweetAlert.swal("Inserted successfully.", "", "success");

                         }

                     },
                     function () {
                         SweetAlert.swal("Try agian", "There is some problem.", "", "error");
                     });
                 }
             });
    }


    $scope.PaymentTaggingEntryForm = function () {
        if ($("form[name*=userForm]").find(".has-error").length > 0) {
            if ($('#ChequeDate').val() != "") {
                $scope.ChequeDate = $('#ChequeDate').val();
            }
            debugger;
            if (parseFloat($('#ConverisonRate').val()) <= 0) {
                $('#ConverisonRate').focus();
                $scope.userForm.$valid = false;
                SweetAlert.swal("validation", "Converison Rate can not be 0 or less", "error");


            } else {
                $('#subsidiaryrights').focus();
                SweetAlert.swal("validation", "Please check the required feilds", "error");
                $scope.userForm.$valid = false;
            }
            
        } else {
            if ($('.MainDivAuthorList').find('input[name*=AuthorContract_]:checked').length == 0) {
                if ($('#ChequeDate').val() != "") {
                    $scope.ChequeDate = $('#ChequeDate').val();
                }
                $scope.userForm.$valid = false;
                $($('[id*=chk_Contractld]')[0]).focus();
                
                SweetAlert.swal("validation", "Please select one of these", "error");
                //return false;
            } else {


               
                if ($('#subsidiaryrights').val() == "" || $('input[name=PaymentModeList]:checked').val() == undefined || $('#ChequeNumber').val() == "" || $('#ChequeDate').val() == "" || $('#BankName').val() == "" || $('#ConverisonRate').val() == "") {
                   
                    SweetAlert.swal("validation", "Please check the required feilds", "error");
                    $scope.userForm.$valid = false;
                } else {

                    if (parseFloat($('#ConverisonRate').val()) <= 0) {
                        $('#ConverisonRate').focus();
                        $scope.userForm.$valid = false;
                        SweetAlert.swal("validation", "Converison Rate can not be 0 or less", "error");
                       

                    } else {
                        $scope.userForm.$valid = true;
                    }
                    
                }


            
            }

        }

       
       
       
        $scope.submitted = true;

        if ($scope.userForm.$valid) {


            if ($scope.userForm.$valid) {
                $scope.PaymentTaggingEntry();
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    }


    $scope.func_Contract = function (Id) {

        //alert(Id.value)
        var mstr_val = Id.id;
      
        $('#' + mstr_val)

        //$('#' + mstr_val).closest("td").next("td").next("td").find("select").attr("ng-required", true);
        
        var mstr_SubsidiaryRights = $('#' + mstr_val).closest("td").next("td").next("td").find("select").attr("id")

        var mstr_Bank = $('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").find('input').attr('Id')

        var mstr_ChequeDate = $('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").next("td").next("td").find('input').attr('Id')

        var mstr_Number = $('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").next("td").find('input').attr('Id')

        var mstr_PaymentMode = $('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").find('input').attr('Id')

        
        var mstr_Amout = $('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").find('input').attr('Id')

        var mstr_AuthorAmout = $('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").find('input').attr('Id')

        var mstr_OupAmout = $('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").find('input').attr('Id')

        if ($('#' + mstr_val).attr('checked')) {
          
            $scope.DataValidate(mstr_SubsidiaryRights)
            $scope.DataValidate(mstr_Bank)
            $scope.DataValidateForDate(mstr_ChequeDate)
            $scope.DataValidate(mstr_Number)
            $scope.DataValidate(mstr_Amout)

            if ($('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").find('input[name*=PaymentMode_]:checked').length == 0) {

                var data = $('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").find('input');
                data.closest('div').find('p').addClass('ng-show').removeClass('ng-hide').parent().addClass('has-error')
               
            }
            else {
                var data = $('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").find('input');
                data.closest('div').find('p').removeClass('ng-show').addClass('ng-hide').parent().removeClass('has-error')
            }
            
            if (AuthorId != "" || AuthorContractId != "") {

                var getGetPaymentTaggingSubSidiaryRightsByAuthorcontractList = AJService.GetDataFromAPI("PermissionsOutbound/SubSidiaryRightsByAuthorcontract?authorcontractid=" + Id.value);
                getGetPaymentTaggingSubSidiaryRightsByAuthorcontractList.then(function (msg) {
                    $scope.PaymentTaggingSubSidiaryRightsByAuthorcontractList = msg.data;

                    var option = '';
                    for (var i = 0; i < msg.data.length; i++) {
                        option += '<option value="' + msg.data[i].id + '">' + msg.data[i].subsidiaryrights + '</option>';
                    }
                    $('#' + mstr_SubsidiaryRights).append(option);


                }, function () {
                    //alert('Error in getting SubSidiary Rights list');
                });
            }
            else if (PublishingCompanyId != "" || ProductLicenseId != "") {

                var getGetPaymentTaggingSubSidiaryRightsByAuthorcontractList = AJService.GetDataFromAPI("PermissionsOutbound/SubSidiaryRightsByProductLicense?ProductLicenseId=" + Id.value);
                getGetPaymentTaggingSubSidiaryRightsByAuthorcontractList.then(function (msg) {
                    $scope.PaymentTaggingSubSidiaryRightsByAuthorcontractList = msg.data;

                    var option = '';
                    for (var i = 0; i < msg.data.length; i++) {
                        option += '<option value="' + msg.data[i].id + '">' + msg.data[i].subsidiaryrights + '</option>';
                    }
                    $('#' + mstr_SubsidiaryRights).append(option);


                }, function () {
                    //alert('Error in getting SubSidiary Rights list');
                });
            }
        }
        else {
            $('#' + mstr_SubsidiaryRights).children('option:not(:first)').remove();;
            $('#' + mstr_SubsidiaryRights).closest("td").next("td").find("input").val('');
            $('#' + mstr_Amout).val('');
            $('#' + mstr_AuthorAmout).val('');
            $('#' + mstr_OupAmout).val('');
            $('#' + mstr_Bank).val('');
            $('#' + mstr_ChequeDate).val('');
            
            $('#' + mstr_PaymentMode).attr("checked", false);
            
            $('#' + mstr_Number).val('');
            
            $('#' + mstr_Amout).attr("disabled", true);
            $('#' + mstr_AuthorAmout).attr("disabled", true);

            $('#' + mstr_OupAmout).attr("disabled", true);

            $scope.RemovedValidate(mstr_SubsidiaryRights)
            $scope.RemovedValidate(mstr_Bank)
            $scope.RemovedValidateForDate(mstr_ChequeDate)
            $scope.RemovedValidate(mstr_Number)
            $scope.RemovedValidate(mstr_Amout)

            var data = $('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").find('input');
            data.closest('div').find('p').removeClass('ng-show').addClass('ng-hide').parent().removeClass('has-error')

        }
    }

    $scope.func_TextChange = function (Id) {
        var mstr_Val = Id.id
        $('#' + mstr_Val)

        $scope.DataValidate(mstr_Val);
    }

    $scope.func_RadioSelect = function (Id) {
        var mstr_Val = Id.id
        $('#' + mstr_Val).closest('div').find('p').removeClass('ng-show').addClass('ng-hide').parent().removeClass('has-error')
    }

    $scope.DataValidate = function (data)
    {
        
        dataVal = $('#' + data).val();
        if (dataVal == undefined || dataVal == "") {
            $('#' + data).next().find('p').removeClass('ng-hide').addClass("ng-show");
            $('#' + data).closest('.form-group').addClass("has-error");
        }
        if (dataVal != "") {

            $('#' + data).next().find('p').addClass('ng-hide').removeClass("ng-show");
            $('#' + data).closest('.form-group').removeClass("has-error");
        }
    }

    $scope.RemovedValidate = function (data) {

        dataVal = $('#' + data).val();
        $('#' + data).next().find('p').addClass('ng-hide').removeClass("ng-show");
        $('#' + data).closest('.form-group').removeClass("has-error");
    }

    $scope.DataValidateForDate = function (data) {

        dataVal = $('#' + data).val();
        if (dataVal == undefined || dataVal == "") {
            $('#' + data).closest('div').next().find('p').removeClass('ng-hide').addClass("ng-show");
            $('#' + data).closest('.form-group').addClass("has-error");
        }
        if (dataVal != "") {
            //$('#' + data).closest('div').next().find('p').addClass('ng-hide').removeClass("ng-show");
            //$('#' + data).closest('.form-group').removeClass("has-error");
            $scope.RemovedValidateForDate(data);
        }
    }

    $scope.RemovedValidateForDate = function (data) {

        dataVal = $('#' + data).val();
        $('#' + data).closest('div').next().find('p').addClass('ng-hide').removeClass("ng-show");
        $('#' + data).closest('.form-group').removeClass("has-error");
    }


    $scope.func_subsidiaryrights = function (Id) {
        var mstr_Val = Id.id
        $('#' + mstr_Val)
       

        $scope.DataValidate(mstr_Val);
    
       var mstr_Percentage = $('#' + mstr_Val).closest("td").next("td").find("input").attr("id")
       
       var mstr_Amout = $('#' + mstr_Val).closest("td").next("td").next("td").next("td").next("td").next("td").next("td").find('input').attr('Id')

       var mstr_AuthorAmout = $('#' + mstr_Val).closest("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").find('input').attr('Id')

       var mstr_OupAmout = $('#' + mstr_Val).closest("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").find('input').attr('Id')

       var mstr_AuthorContractId = $('#' + mstr_Val).closest("td").prev("td").prev("td").find("input").attr("id")

        //----------Get AuthorId
       var _count = mstr_Val.split('_')[1];
       var cuurrent_authorId = $('#hid_AuthorId_' + _count).val();
        //----------------------       

       if ($('#' + mstr_Val).val() != "") {
           if (AuthorId != "" || AuthorContractId != "") {
               var getGetSubSidiaryRightsByPercentageList = AJService.GetDataFromAPI("PermissionsOutbound/SubSidiaryRightsByPercentage?subsidiaryrightsid=" + $('#subsidiaryrights').val() + "&AuthorContractId=" + $('#' + mstr_AuthorContractId).val() + "&authorId=" + cuurrent_authorId);
               getGetSubSidiaryRightsByPercentageList.then(function (msg) {

                   if (msg.data != null) {
                       $('#' + mstr_Percentage).val(msg.data.AuthorPercentage);

                       var_AmountPercentage = (msg.data.AuthorPercentage / $('#' + mstr_Amout).val()) * 100

                       $('#' + mstr_AuthorAmout).val(var_AmountPercentage);

                       if ($('#' + mstr_Amout).val() != "") {
                           $('#' + mstr_OupAmout).val($('#' + mstr_Amout).val() - $('#' + mstr_AuthorAmout).val())
                       }

                       $('#' + mstr_Amout).attr("disabled", false);
                   }
               }, function () {
                   //alert('Error in getting SubSidiary Rights list');
               });
           }
           else if (PublishingCompanyId != "" || ProductLicenseId != "") {
               var getGetSubSidiaryRightsByPercentageList = AJService.GetDataFromAPI("PermissionsOutbound/SubSidiaryRightsByPercentageForProductLicense?subsidiaryrightsid=" + $('#subsidiaryrights').val() + "&ProductLicenseid=" + $('#' + mstr_AuthorContractId).val());
               getGetSubSidiaryRightsByPercentageList.then(function (msg) {

                   if (msg.data != null) {
                       $('#' + mstr_Percentage).val(msg.data.AuthorPercentage);

                       var_AmountPercentage = (msg.data.AuthorPercentage / $('#' + mstr_Amout).val()) * 100

                       $('#' + mstr_AuthorAmout).val(var_AmountPercentage);

                       if ($('#' + mstr_Amout).val() != "") {
                           $('#' + mstr_OupAmout).val($('#' + mstr_Amout).val() - $('#' + mstr_AuthorAmout).val())
                       }

                       $('#' + mstr_Amout).attr("disabled", false);
                   }
               }, function () {
                   //alert('Error in getting SubSidiary Rights list');
               });
           }
        }
        else {
            $('#' + mstr_Percentage).val('');
            $('#' + mstr_Amout).val('');
            $('#' + mstr_AuthorAmout).val('');
            $('#' + mstr_Amout).attr("disabled", true);
            $('#' + mstr_AuthorAmout).attr("disabled", true);
        }
       

    }

    $scope.ChequeDate = [];
    $scope.ChequeDateValue = function (datetext) {
        var mstr_Val = datetext.id

        $scope.DataValidateForDate(mstr_Val);
     
        for (var i = 0; i < $scope.PaymentTaggingSubSidiaryRightsList.length; i++) {

            if ('ChequeDate_' + [i] == mstr_Val)
            {
              
                $scope.ChequeDate[i] = $('#ChequeDate_' + [i]).val();
            }

           
        }

    }
   
    $scope.func_Amount = function (Id)
    {
       
        var mstr_Val = Id.id
        $('#' + mstr_Val)

        $scope.DataValidate(mstr_Val);

        var mstr_Amount = $('#' + mstr_Val).closest("td").prev('td').prev('td').prev('td').prev('td').prev('td').find('input').attr("id")
        var mstr_AuthorAmount = $('#' + mstr_Val).closest("td").next("td").find("input").attr("id")
        var mstr_OupAmount = $('#' + mstr_Val).closest("td").next("td").next("td").find("input").attr("id")

        $('#' + mstr_Amount)
        if ($('#' + mstr_Amount).val() != "") {
          
            var_AmountPercentage = ( $('#' + mstr_Amount).val() * $('#' + mstr_Val).val()) / 100
            //$('#' + mstr_AuthorAmount).attr("disabled", false);
            //$('#' + mstr_AuthorAmount).val(Math.round(var_AmountPercentage, 3))
            $('#' + mstr_AuthorAmount).val(var_AmountPercentage.toFixed(2));

            var_OupAmount = $('#' + mstr_Val).val() - var_AmountPercentage.toFixed(2);
            //$('#' + mstr_OupAmount).attr("disabled", false);
            //$('#' + mstr_OupAmount).val(Math.round(var_OupAmount, 3))
            $('#' + mstr_OupAmount).val(var_OupAmount.toFixed(2));
         
        }
     

       
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

    $scope.func_ValidateOnSubmit = function (Id) {

        var mstr_val = Id.id;

        $('#' + mstr_val)

        var mstr_SubsidiaryRights = $('#' + mstr_val).closest("td").next("td").next("td").find("select").attr("id")

        var mstr_Bank = $('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").find('input').attr('Id')

        var mstr_ChequeDate = $('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").next("td").next("td").find('input').attr('Id')

        var mstr_Number = $('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").next("td").find('input').attr('Id')

        var mstr_PaymentMode = $('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").find('input').attr('Id')


        var mstr_Amout = $('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").find('input').attr('Id')

        var mstr_AuthorAmout = $('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").find('input').attr('Id')

        var mstr_OupAmout = $('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").find('input').attr('Id')

        if ($('#' + mstr_val).attr('checked')) {

            $scope.DataValidate(mstr_SubsidiaryRights)
            $scope.DataValidate(mstr_Bank)
            $scope.DataValidateForDate(mstr_ChequeDate)
            $scope.DataValidate(mstr_Number)
            $scope.DataValidate(mstr_Amout)

            if ($('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").find('input[name*=PaymentMode_]:checked').length == 0) {

                var data = $('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").find('input');
                data.closest('div').find('p').addClass('ng-show').removeClass('ng-hide').parent().addClass('has-error')

            }
            else {
                var data = $('#' + mstr_val).closest("td").next("td").next("td").next("td").next("td").find('input');
                data.closest('div').find('p').removeClass('ng-show').addClass('ng-hide').parent().removeClass('has-error')
            }

        }
    }

    $scope.BackToList = function () {
        $window.location.href = GlobalredirectPath + 'PaymentTagging/PaymentTagging/PaymentTaggingSearch?For=Rights&From=BackToSearch';
    }


    $scope.func_Allsubsidiaryrights = function (Id) {
        var mstr_Val = Id.id
        $('#' + mstr_Val)

     
        $scope.DataValidate(mstr_Val);

        var mstr_Percentage = $('#' + mstr_Val).closest("td").next("td").find("input").attr("id")

        var mstr_Amout = $('#' + mstr_Val).closest("td").next("td").next("td").next("td").next("td").next("td").next("td").find('input').attr('Id')

        var mstr_AuthorAmout = $('#' + mstr_Val).closest("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").find('input').attr('Id')

        var mstr_OupAmout = $('#' + mstr_Val).closest("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").next("td").find('input').attr('Id')

        var mstr_AuthorContractId = $('#' + mstr_Val).closest("td").prev("td").prev("td").find("input").attr("id")

        //----------Get AuthorId
        var _count = mstr_Val.split('_')[1];
        var cuurrent_authorId = $('#hid_AuthorId_' + _count).val();
        //----------------------       

        if ($('#' + mstr_Val).val() != "") {
            if (AuthorId != "" || AuthorContractId != "") {
                var getGetSubSidiaryRightsByPercentageList = AJService.GetDataFromAPI("PermissionsOutbound/SubSidiaryRightsByPercentage?subsidiaryrightsid=" + $('#subsidiaryrights').val() + "&AuthorContractId=" + $('#' + mstr_AuthorContractId).val() + "&authorId=" + cuurrent_authorId);
                getGetSubSidiaryRightsByPercentageList.then(function (msg) {

                    if (msg.data != null) {
                        $('#' + mstr_Percentage).val(msg.data.AuthorPercentage);

                        var_AmountPercentage = (msg.data.AuthorPercentage / $('#' + mstr_Amout).val()) * 100

                        $('#' + mstr_AuthorAmout).val(var_AmountPercentage);

                        if ($('#' + mstr_Amout).val() != "") {
                            $('#' + mstr_OupAmout).val($('#' + mstr_Amout).val() - $('#' + mstr_AuthorAmout).val())
                        }

                        $('#' + mstr_Amout).attr("disabled", false);
                    }
                }, function () {
                    //alert('Error in getting SubSidiary Rights list');
                });
            }
            else if (PublishingCompanyId != "" || ProductLicenseId != "") {
                var getGetSubSidiaryRightsByPercentageList = AJService.GetDataFromAPI("PermissionsOutbound/SubSidiaryRightsByPercentageForProductLicense?subsidiaryrightsid=" + $('#subsidiaryrights').val() + "&ProductLicenseid=" + $('#' + mstr_AuthorContractId).val());
                getGetSubSidiaryRightsByPercentageList.then(function (msg) {

                    if (msg.data != null) {
                        $('#' + mstr_Percentage).val(msg.data.AuthorPercentage);

                        var_AmountPercentage = (msg.data.AuthorPercentage / $('#' + mstr_Amout).val()) * 100

                        $('#' + mstr_AuthorAmout).val(var_AmountPercentage);

                        if ($('#' + mstr_Amout).val() != "") {
                            $('#' + mstr_OupAmout).val($('#' + mstr_Amout).val() - $('#' + mstr_AuthorAmout).val())
                        }

                        $('#' + mstr_Amout).attr("disabled", false);
                    }
                }, function () {
                    //alert('Error in getting SubSidiary Rights list');
                });
            }
        }
        else {
            $('#' + mstr_Percentage).val('');
            $('#' + mstr_Amout).val('');
            $('#' + mstr_AuthorAmout).val('');
            $('#' + mstr_Amout).attr("disabled", true);
            $('#' + mstr_AuthorAmout).attr("disabled", true);
        }


    }

    $scope.Fun_OUPPercentage = function ( ) {
    
        if ($('#subsidiaryrights').val() != "" && AuthorContractId != "") {


            var getGetSubSidiaryRightsByPercentageList = AJService.GetDataFromAPI("PermissionsOutbound/OUPPercentageDettails?subsidiaryrightsid=" + $('#subsidiaryrights').val() + "&AuthorContractId=" + AuthorContractId);
            getGetSubSidiaryRightsByPercentageList.then(function (msg) {

                if (msg.data != null) {
                  

                    $scope.OUP_Percentage = msg.data.ouppercentage;

                    $('#Amount').attr("disabled", false);

                  
                    var chk_Contractld = $('[id*=chk_Contractld]');
                    
                    if (chk_Contractld.length > 0) {
                        for (var i = 0; i < chk_Contractld.length; i++) {
                            if ($($('[id*=chk_Contractld]')[i]).prop("checked") == true) {
                                var mstr_Val = $($('[id*=chk_Contractld]')[i]).attr("id")
                                $('#' + mstr_Val)
                                
                                $scope.FillPaymentinfoByAuthor($('[id*=chk_Contractld]')[i]);


                            }
                        }
                    }


                    


                }
            }, function () {
                $scope.OUP_Percentage = "";
                $('#ChequeNumber').val('');

                $('#ChequeNumber').val('');
                $('#ChequeDateName').val('');
                $('#BankName').val('');
                $('#Amount').val('');

                $('#WHT').val('');
                $('#ConverisonRate').val('');
                $scope.ConverisonRate = 1;
                $('#ConverisonRate').val(1);
                $('#Invoice_Amount').val('');
                $('#Receipt_Amount').val('');
                $('#OUP_Percentage').val('');
                $('#OupAmount').val('');


                $('#Amount').attr("disabled", true);
            });
        } else if (PublishingCompanyId != "" || ProductLicenseId != "") {
            
            var getGetPaymentTaggingSubSidiaryRightsByAuthorcontractList = AJService.GetDataFromAPI("PermissionsOutbound/OUPPercentageProductLicenseDettails?subsidiaryrightsid=" + $('#subsidiaryrights').val() + "&ProductLicenseid=" + ProductLicenseId);
            getGetPaymentTaggingSubSidiaryRightsByAuthorcontractList.then(function (msg) {
              
             
                $scope.OUP_Percentage = msg.data.ouppercentage;

                $('#Amount').attr("disabled", false);
                var chk_Contractld = $('[id*=chk_Contractld]');

                if (chk_Contractld.length > 0) {
                    for (var i = 0; i < chk_Contractld.length; i++) {
                        if ($($('[id*=chk_Contractld]')[i]).prop("checked") == true) {
                            var mstr_Val = $($('[id*=chk_Contractld]')[i]).attr("id")
                            $('#' + mstr_Val)

                            $scope.FillPaymentinfoByAuthor($('[id*=chk_Contractld]')[i]);


                        }
                    }
                }

            }, function () {
                $scope.OUP_Percentage = "";
                $('#ChequeNumber').val('');

                $('#ChequeNumber').val('');
                $('#ChequeDateName').val('');
                $('#BankName').val('');
                $('#Amount').val('');

                $('#WHT').val('');
                $('#ConverisonRate').val('');
                $scope.ConverisonRate = 1;
                $('#ConverisonRate').val(1);
                $('#Invoice_Amount').val('');
                $('#Receipt_Amount').val('');
                $('#OUP_Percentage').val('');
                $('#OupAmount').val('');


                $('#Amount').attr("disabled", true);
            });
        } else {
            $scope.OUP_Percentage = "";
            $('#ChequeNumber').val('');

            $('#ChequeNumber').val('');
            $('#ChequeDateName').val('');
            $('#BankName').val('');
            $('#Amount').val('');

            $('#WHT').val('');
            $('#ConverisonRate').val('');
            $scope.ConverisonRate = 1;
            $('#ConverisonRate').val(1);
            $('#Invoice_Amount').val('');
            $('#Receipt_Amount').val('');
            $('#OUP_Percentage').val('');
            $('#OupAmount').val('');

        }
      
    }

    function ConvertRate(RecAmt, Convert) {
        if (RecAmt != "" || Convert !="") {
         
            var ConvertAmt = parseFloat(RecAmt) * parseFloat(Convert);
            return ConvertAmt.toFixed(2);
        }

    }


    setInterval(function () {
        if ($('#subsidiaryrights').val() !="" && $('#Amount').val() != "" && $('#ConverisonRate').val() != "" && $('#Receipt_Amount').val() != "" && $('#OUP_Percentage').val() != "") {
            $scope.func_ConvertAmtVal();
        }
    }, 200);
   
 

    $scope.func_ConvertAmtVal = function () {

      
        var _RecAmt = $('#Amount')
        var Convert = $('#ConverisonRate');
        var OUP_Percentage = $('#OUP_Percentage');
     
        if (_RecAmt.val() != "" || Convert.val() != "" || OUP_Percentage.val() != "") {

            $scope.Receipt_Amount = ConvertRate(_RecAmt.val(), Convert.val());

            $('#Receipt_Amount').val(ConvertRate(_RecAmt.val(), Convert.val()));
          
            var_AmountPercentage = (parseFloat(OUP_Percentage.val()) / 100 ) * $('#Receipt_Amount').val();
          
            $scope.OupAmount = var_AmountPercentage.toFixed(2);
            $('#OupAmount').val(var_AmountPercentage.toFixed(2));



            var chk_Contractld = $('[id*=chk_Contractld]');
           
            if(chk_Contractld.length > 0)
            {
                for (var i = 0; i < chk_Contractld.length; i++) {
                    if ($($('[id*=chk_Contractld]')[i]).prop("checked") == true) {
                        var mstr_Val = $($('[id*=chk_Contractld]')[i]).attr("id")
                        $('#' + mstr_Val)

                   
                        var _count = mstr_Val.split('_')[2];

                        if ($('#AuthorPercentage_' + _count).html() != undefined && $('#AuthorPercentage_' + _count).html() != "") {

                            
                            var AuthorPercentage = $('#AuthorPercentage_' + _count).html().trim();
                            var_AmountPercentage1 = (parseFloat(AuthorPercentage) / 100 ) * $('#Receipt_Amount').val();

                            $('#AuthorAmount_' + _count + '').val(parseFloat(var_AmountPercentage1).toFixed(2));
                        }

                        if ($('#PublisherPercentage_' + _count).html() != undefined && $('#PublisherPercentage_' + _count).html() != "") {
                            var PublisherPercentage = $('#PublisherPercentage_' + _count).html().trim();
                            varPublisherPercentage = (parseFloat(PublisherPercentage) / 100) * $('#Receipt_Amount').val();

                            $('#PublisherAmt_' + _count + '').val(parseFloat(varPublisherPercentage).toFixed(2));
                        }
                     

                    }
                }
            }

        }

    }




    function func_ConvertAmtProductLice(AuthorPer) {
        var _RecAmt = $('#Amount')
        var Convert = $('#ConverisonRate');
        var _AuthorPer = AuthorPer;

        if (_RecAmt.val() != "" || Convert.val() != "" || _AuthorPer != "") {



            var_AmountPercentage = (parseFloat(_AuthorPer) / $('#Receipt_Amount').val()) * 100;

            return var_AmountPercentage;
        }
    }
   

    $scope.FillPaymentinfoByAuthor = function (Id) {
       
        var mstr_Val = Id.id
        $('#' + mstr_Val)

        //----------Get AuthorId
        var _count = mstr_Val.split('_')[2];
        var cuurrent_authorId = $('#hid_AuthorId_' + _count).val();
        //----------------------       

     
        if ($('#' + mstr_Val).prop("checked") == false) {
            $('#AuthorPercentage_' + _count + '').html('');
            $scope.Percentage[_count] = "";
            $('#AuthorAmount_' + _count + '').val('');
            $('#PublisherPercentage_' + _count + '').html('');
            $scope.Percentage[_count] = "";
            $('#PublisherAmt_' + _count + '').val('');

        }


        if ($('#' + mstr_Val).prop("checked") == true) {
            if (($('#subsidiaryrights').val() != "") && ($('#Amount').val() != "")) {


                if ($('#subsidiaryrights').val() != "" && AuthorContractId != "") {


                    var getGetSubSidiaryRightsByPercentageList = AJService.GetDataFromAPI("PermissionsOutbound/OUPAuthorPercentageDettails?subsidiaryrightsid=" + $('#subsidiaryrights').val() + "&AuthorContractId=" + AuthorContractId + "&AuthorId=" + cuurrent_authorId);
                    getGetSubSidiaryRightsByPercentageList.then(function (msg) {

                        if (msg.data != null) {
                            $('#AuthorPercentage_' + _count + '').html(msg.data.AuthorPercentage)
                         
                            var AuthorPer = func_ConvertAmtProductLice(msg.data.AuthorPercentage)

                            setTimeout(function () {
                                $('#AuthorAmount_' + _count + '').val(parseFloat(AuthorPer).toFixed(2));
                            },50);
                           


                        }
                    }, function () {
                        $scope.AuthorPercentage = ""
                    });
                } else if (PublishingCompanyId != "" || ProductLicenseId != "") {

                    var getGetPaymentTaggingSubSidiaryRightsByAuthorcontractList = AJService.GetDataFromAPI("PermissionsOutbound/OUPProductLicensePercentageDettails?subsidiaryrightsid=" + $('#subsidiaryrights').val() + "&ProductLicenseid=" + ProductLicenseId);
                    getGetPaymentTaggingSubSidiaryRightsByAuthorcontractList.then(function (msg) {
                        if (msg.data != null) {
                           
                            $('#PublisherPercentage_' + _count + '').html(msg.data.publisherpercentage)
                           
                            var publisherpercentage = func_ConvertAmtProductLice(msg.data.publisherpercentage)
                            
                            $('#PublisherAmt_' + _count + '').val(parseFloat(publisherpercentage).toFixed(2));
                         
                          



                        }


                    }, function () {
                     
                    });
                }






            } else {
                if ($('#' + mstr_Val).prop("checked") == true) {

                    if ($('#subsidiaryrights').val() == "") {
                        $('#subsidiaryrights').focus();
                    } else {
                        $('#Amount').focus();
                    }

                   
                    $('#' + mstr_Val).prop("checked", false)
                  
                    SweetAlert.swal("validation", "Please select subsidiary rights and receipt amount", "error");


                } else {
                    $('#AuthorPercentage_' + _count + '').html('');
                    $scope.AuthorAmount[_count] = "";
                    $('#AuthorAmount_' + _count + '').val('');
                }
            }

        }


         
    }

});


