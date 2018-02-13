app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {

    /*Expand common.master.js Controller*/
    //app.expandControllerA($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);


    $scope.ShowSearchForm = true;
    $scope.ShowListForm = false;

    $scope.btn_backToSearch = true;
    $scope.btn_backToDashboard = false;


    $scope.func_ContractCodeChk = function () {
        $scope.ContractCodeOrLicenseCodeChk = 'AuthorContractCode';
        $scope.ReqAuthorContractCode = true;
        $scope.ReqPublishingCompanyCode = false;
    }

    $scope.func_LicenseCodeChk = function () {
        $scope.ContractCodeOrLicenseCodeChk = 'ProductLicenseCode';
        $scope.ReqPublishingCompanyCode = true;
        $scope.ReqAuthorContractCode = false;
    }


    //$scope.PaymentTaggingSearch = function () {

    //    var For = $('#hid_For').val();
    //    if (For == 'Rights') {
    //        var _mobjPaymentTagging = {
    //            AuthorId: null,
    //            AuthorCode: $scope.AuthorCode,
    //            AuthorContractCode: $scope.AuthorContractCode,
    //            ProductLicenseCode : $scope.ProductLicenseCode,
    //            PublishingCompanyCode: $scope.PublishingCompanyCode
    //        }
    //        debugger;

    //        var ExecutiveStatus = AJService.PostDataToAPI('PaymentTaggingMaster/GetAuthorIdListForRights', _mobjPaymentTagging);
    //        ExecutiveStatus.then(function (msg) {
    //            if (msg.data.length != 0) {
    //                if (msg.data[0].AuthorId != null || msg.data[0].AuthorContractId != null) {
    //                    if (($scope.AuthorCode != null && $scope.AuthorCode != "") && ($scope.AuthorContractCode != null && $scope.AuthorContractCode != ""))
    //                        $window.location.href = '../../RightsSelling/RightsSelling/RightsSellingPaymentTagging?AuthorId=' + msg.data[0].AuthorId + '&AuthorContractId=' + msg.data[0].AuthorContractId;
    //                    else if ($scope.AuthorCode != null && $scope.AuthorCode != "")
    //                        $window.location.href = '../../RightsSelling/RightsSelling/RightsSellingPaymentTagging?AuthorId=' + msg.data[0].AuthorId;
    //                    else if ($scope.AuthorContractCode != null && $scope.AuthorContractCode != "")
    //                        $window.location.href = '../../RightsSelling/RightsSelling/RightsSellingPaymentTagging?AuthorContractId=' + msg.data[0].AuthorContractId;
    //                    else
    //                        SweetAlert.swal("Try agian", "There is some problem.", "", "error");
    //                }
    //                else if (msg.data[0].PublishingCompanyId != null || msg.data[0].ProductLicenseId != null) {
    //                    if ($scope.PublishingCompanyCode != null && $scope.ProductLicenseCode != null)
    //                        $window.location.href = '../../RightsSelling/RightsSelling/RightsSellingPaymentTagging?PublishingCompanyId=' + msg.data[0].PublishingCompanyId + '&ProductLicenseId=' + msg.data[0].ProductLicenseId;
    //                    else if ($scope.PublishingCompanyCode != null)
    //                        $window.location.href = '../../RightsSelling/RightsSelling/RightsSellingPaymentTagging?PublishingCompanyId=' + msg.data[0].PublishingCompanyId;
    //                    else if ($scope.ProductLicenseCode != null)
    //                        $window.location.href = '../../RightsSelling/RightsSelling/RightsSellingPaymentTagging?ProductLicenseId=' + msg.data[0].ProductLicenseId;
    //                    else
    //                        SweetAlert.swal("Try agian", "There is some problem.", "", "error");
    //                }
    //                else {
    //                    SweetAlert.swal("No record", 'No record found', "warning");
    //                }
    //            }
    //            else {
    //                SweetAlert.swal("No record", 'No record found', "warning");
    //            }
    //        });
    //    }
    //    else
    //        if (For == 'PermissionOutbound') {
    //            var _mobjPaymentTagging = {
    //                AuthorId: null,
    //                AuthorCode: $scope.AuthorCode,
    //                AuthorContractCode: $scope.AuthorContractCode,
    //                PublishingCompanyCode: $scope.PublishingCompanyCode,
    //                ProductLicenseCode : $scope.ProductLicenseCode,
    //            }
    //            debugger;

    //            var ExecutiveStatus = AJService.PostDataToAPI('PaymentTaggingMaster/GetAuthorIdListForOutbound', _mobjPaymentTagging);
    //            ExecutiveStatus.then(function (msg) {
    //                if (msg.data.length != 0) {
    //                    if (msg.data[0].AuthorId != null || msg.data[0].AuthorContractId != null) {
    //                        if (($scope.AuthorCode != null && $scope.AuthorCode != "") && ($scope.AuthorContractCode != null && $scope.AuthorContractCode != ""))
    //                            $window.location.href = '../../PermissionsOutbound/PermissionsOutbound/PermissionsOutboundPaymentTagging?AuthorId=' + msg.data[0].AuthorId + '&AuthorContractId=' + msg.data[0].AuthorContractId;
    //                        else if ($scope.AuthorCode != null && $scope.AuthorCode != "")
    //                            $window.location.href = '../../PermissionsOutbound/PermissionsOutbound/PermissionsOutboundPaymentTagging?AuthorId=' + msg.data[0].AuthorId;
    //                        else if ($scope.AuthorContractCode != null && $scope.AuthorContractCode != "")
    //                            $window.location.href = '../../PermissionsOutbound/PermissionsOutbound/PermissionsOutboundPaymentTagging?AuthorContractId=' + msg.data[0].AuthorContractId;
    //                        else
    //                            SweetAlert.swal("Try agian", "There is some problem.", "", "error");
    //                    }
    //                    else if (msg.data[0].PublishingCompanyId != null || msg.data[0].ProductLicenseId != null) {
    //                        if ($scope.PublishingCompanyCode != null && $scope.ProductLicenseCode != null)
    //                            $window.location.href = '../../PermissionsOutbound/PermissionsOutbound/PermissionsOutboundPaymentTagging?PublishingCompanyId=' + msg.data[0].PublishingCompanyId + '&ProductLicenseId=' + msg.data[0].ProductLicenseId;
    //                        else if ($scope.PublishingCompanyCode != null)
    //                            $window.location.href = '../../PermissionsOutbound/PermissionsOutbound/PermissionsOutboundPaymentTagging?PublishingCompanyId=' + msg.data[0].PublishingCompanyId;
    //                        else if ($scope.ProductLicenseCode != null)
    //                            $window.location.href = '../../PermissionsOutbound/PermissionsOutbound/PermissionsOutboundPaymentTagging?ProductLicenseId=' + msg.data[0].ProductLicenseId;
    //                        else
    //                            SweetAlert.swal("Try agian", "There is some problem.", "", "error");
    //                    }
    //                    else {
    //                        SweetAlert.swal("No record", 'No record found', "warning");
    //                    }
    //                }
    //                else {
    //                    SweetAlert.swal("No record", 'No record found', "warning");
    //                }
    //            });
    //        }
    //    else {
    //        SweetAlert.swal("Try agian", "There is some problem.", "", "error");
    //    }
    //}


    $scope.RightsSellingListResult = function () {
        var RightsSellingList = AJService.GetDataFromAPI("PaymentTaggingMaster/GetListForRights?SessionId=" + $("#hid_sessionId").val() + "", null);
        RightsSellingList.then(function (msg) {
            if (msg.data.length != 0) {
                $scope.Data = msg.data;
                $scope.ShowSearchForm = false;
                $scope.ShowListForm = true;
            }
            else {
                swal("No record", 'No record found', "warning");
            }
        });
    }

    $scope.PermissionsOutboundSearch = function () {
        var PermissionsOutboundList = AJService.GetDataFromAPI("PaymentTaggingMaster/GetListForOutbound?SessionId=" + $("#hid_sessionId").val() + "", null);
        PermissionsOutboundList.then(function (msg) {
            if (msg.data.length != 0) {
                $scope.Data = msg.data;
                $scope.ShowSearchForm = false;
                $scope.ShowListForm = true;
            }
            else {
                swal("No record", 'No record found', "warning");
            }
        });
    }

    if ($("#hid_Dashboard").val() == "dashboard") {
        var For = $('#hid_For').val();
        if (For == 'Rights') {
            var PaymentInfoList = AJService.GetDataFromAPI("PaymentTaggingMaster/GetList_PaymentNotReceived_ByDashboard?For=rights", null);
            PaymentInfoList.then(function (msg) {
                if (msg.data.length != 0) {
                    $scope.Data = msg.data;
                    $scope.ShowSearchForm = false;
                    $scope.ShowListForm = true;
                    
                    $scope.btn_backToSearch = false;
                    $scope.btn_backToDashboard = true;
                }
                else {
                    swal("No record", 'No record found', "warning");
                }
            });
        }
        else if (For == 'PermissionOutbound') {
            var PaymentInfoList = AJService.GetDataFromAPI("PaymentTaggingMaster/GetList_PaymentNotReceived_ByDashboard?For=permissionoutbound", null);
            PaymentInfoList.then(function (msg) {
                if (msg.data.length != 0) {
                    $scope.Data = msg.data;
                    $scope.ShowSearchForm = false;
                    $scope.ShowListForm = true;
                    
                    $scope.btn_backToSearch = false;
                    $scope.btn_backToDashboard = true;
                }
                else {
                    swal("No record", 'No record found', "warning");
                }
            });
        }
        else {
            SweetAlert.swal("Try agian", "There is some problem.", "", "error");
        }
    }
    
    $scope.PaymentTaggingSearch = function () {
        var For = $('#hid_For').val();

        if (For == 'Rights') {
            var _mobjPaymentTagging = {
                SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
                EntryDate: new Date(),
                EnteredBy: $("#enterdBy").val(),
                ISBN: $scope.OUPISBN,
                WorkingProduct: $scope.WorkingProduct,
                AuthorName: $scope.Authors,
                AuthorContractCode: $scope.AuthorContractCode,
                AuthorCode: $scope.AuthorCode,
                ProductLicenseCode: $scope.ProductLicenseCode,
                PublishingCompanyCode: $scope.PublishingCompanyCode,
                AuthorSAPCode: $scope.AuthorSAPCode,
            }

            var AuthorStatus = AJService.PostDataToAPI('RightsSelling/InsertRightsSellingHistory', _mobjPaymentTagging);

            AuthorStatus.then(function (msg) {
                //blockUI.stop();
                if (msg.data == "OK") {
                    $scope.RightsSellingListResult();
                }

            }, function () {
                $scope.IsError = false;
            });
        }
        else
            if (For == 'PermissionOutbound') {
                var _mobjPaymentTagging = {
                    SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
                    EntryDate: new Date(),
                    EnteredBy: $("#enterdBy").val(),
                    ISBN: $scope.OUPISBN,
                    WorkingProduct: $scope.WorkingProduct,
                    AuthorName: $scope.Authors,
                    AuthorContractCode: $scope.AuthorContractCode,
                    AuthorCode: $scope.AuthorCode,
                    ProductLicenseCode: $scope.ProductLicenseCode,
                    PublishingCompanyCode: $scope.PublishingCompanyCode,
                    AuthorSAPCode: $scope.AuthorSAPCode,
                }


                var PermissionsOutboundStatus = AJService.PostDataToAPI('PermissionsOutbound/PermissionsOutboundSearch', _mobjPaymentTagging);

                PermissionsOutboundStatus.then(function (msg) {

                    if (msg.data == "OK") {
                        $scope.PermissionsOutboundSearch();
                    }

                },
                function () {
                    alert('There is some error in the system');
                });
            }
        else {
            SweetAlert.swal("Try agian", "There is some problem.", "", "error");
        }
    }


    $scope.PaymentTaggingSearchForm = function () {
        $scope.submitted = true;

        if ($scope.userForm.$valid) {
            $scope.PaymentTaggingSearch();
            // set form default state
            $scope.userForm.$setPristine();
            // set form is no submitted
            $scope.submitted = false;
            return;
        }
    }


    if ($('#hid_BackToSearch').val() != "") {
        var For = $('#hid_For').val();
        if (For == 'Rights') {
            $scope.RightsSellingListResult();
        }
        else
            if (For == 'PermissionOutbound') {
                $scope.PermissionsOutboundSearch();
            }
        $('#hid_For').val();
    }


    $scope.BackToserch = function () {
        if ($("#hid_show").val() == "dashboard") {
            window.location.href = '../../Home/Dashboard/Dashboard';
        }
        else {
            window.location.href = window.location.href.replace("&From=BackToSearch", "");
        }

        if ($("#hid_Dashboard").val() == "dashboard") {
            window.location.href = '../../Home/Dashboard/Dashboard';
        }
        else {
            window.location.href = window.location.href.replace("&From=BackToSearch", "");
        }
    }


    $scope.DisplayDiv = function (count) {
        $('.tooltip-toggle').css("display", "none");
        $('.tooltip-toggle').removeAttr("id");
        $($('.tooltip-toggle')[count - 1]).attr("id", "tooltip-toggle");
    }


    //start block - payment information List
    $scope.getPaymentTaggingList = function () {
        var For = $('#hid_For').val();
        if (For != unescape && For != "" && For != null) {
            var paymentTaggingList = AJService.GetDataFromAPI("PaymentTaggingMaster/getPaymentTaggingList?Type=" + For + "", null);
            paymentTaggingList.then(function (msg) {
                if (msg.data.length != 0) {
                    $scope.PaymentTaggingListData = msg.data;
                }
                else {

                }

            }, function () {
                $scope.IsError = false;
            });
        }
            
    }


    $scope.DeletePaymentTaggingDetails = function (Id) {
        var mobj_delete = {
            PaymentTaggingId: Id == undefined ? 0 : (Id == null ? 0 : Id),
            DeactivateBy: parseInt($("#enterdBy").val()),
            Type: $('#hid_For').val(),
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
                //blockUI.start();

                var Delete = AJService.PostDataToAPI("PaymentTaggingMaster/DeletePaymentTaggingDetailsSet", mobj_delete);
                Delete.then(function (msg) {
                    if (msg.data == "OK") {
                        SweetAlert.swal({
                            title: "Deleted!",
                            text: "Your record  has been deleted.",
                            type: "success",
                            confirmButtonText: "OK",
                            closeOnConfirm: true
                        },
                        function (Confirm) {
                            if (Confirm) {
                                //blockUI.stop();
                                $scope.getPaymentTaggingList();
                            }
                        });

                    }
                });


            }

        });

    }

    //End block - payment information List



});


