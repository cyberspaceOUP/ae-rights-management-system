

app.expandControllerTopSearch = function ($scope, AJService, $window) {

    $scope.TopSearchsubmitForm = function () {
           
            var mstr_SearchValue = $('#TopSearch').val();
            var mstr_TopSearchValue = $('#txt_TopSearchValue').val();

            if (mstr_SearchValue != "" && mstr_TopSearchValue != "") {
                if (mstr_SearchValue == "OtherContract") {

                    var geTopSearchsubmitList = AJService.PostDataToAPI("OtherContract/TopSearch?Code=" + mstr_TopSearchValue);
                    geTopSearchsubmitList.then(function (msg) {

                        if (msg.data._OtherContract.othercontractcode != undefined) {
                            window.location.href = GlobalredirectPath + "Contract/OtherContract/OtherContractView?id=" + msg.data._OtherContract.othercontractcode + ""
                            // window.location.href = GlobalredirectPath + "/Contract/OtherContract/OtherContractView?id=" + msg.data._OtherContract.othercontractcode + ""
                            //  window.location.href = "http://localhost/RMS.Web" + "/Contract/OtherContract/OtherContractView?id=" + msg.data._OtherContract.othercontractcode + ""

                        }
                        else {
                            swal("No record", 'No record found', "warning");
                        }

                    }, function () {
                        swal("No record", 'No record found', "warning");
                    });
                }

                if (mstr_SearchValue == "Product") {

                    var geTopSearchsubmitList = AJService.PostDataToAPI("ProductMaster/TopSearch?Code=" + mstr_TopSearchValue);
                    geTopSearchsubmitList.then(function (msg) {

                        if (msg.data._ProductMasterValue.Id != undefined) {

                            window.location.href = GlobalredirectPath + "Product/ProductMaster/ProductDetailsView?Id=" + msg.data._ProductMasterValue.Id + ""

                            // window.location.href = "http://localhost/RMS.Web" + "/Product/ProductMaster/ProductDetailsView?Id=" + msg.data._ProductMasterValue.Id + ""

                        }
                        else {
                            swal("No record", 'No record found', "warning");
                        }

                    }, function () {
                        swal("No record", 'No record found', "warning");
                    });
                }



                if (mstr_SearchValue == "AuthorContract") {

                    var geTopSearchsubmitList = AJService.PostDataToAPI("AuthorContact/TopSearch?Code=" + mstr_TopSearchValue);
                    geTopSearchsubmitList.then(function (msg) {

                        if (msg.data._AuthorContractValue.Id != undefined) {

                          
                            window.location.href = GlobalredirectPath + "/Contract/AuthorContract/view?Id=" + msg.data._AuthorContractValue.Id + ""


                            // window.location.href = "http://localhost/RMS.Web" + "/Contract/AuthorContract/view?Id=" + msg.data._AuthorContractValue.Id + ""

                        }
                        else {
                            swal("No record", 'No record found', "warning");
                        }

                    }, function () {
                        swal("No record", 'No record found', "warning");
                    });
                }




                if (mstr_SearchValue == "ProductLicense") {

                    var geTopSearchsubmitList = AJService.PostDataToAPI("ProductLicense/TopSearch?Code=" + mstr_TopSearchValue);
                    geTopSearchsubmitList.then(function (msg) {

                        if (msg.data._ProductLicenseMasterValue.Id != undefined) {

                            window.location.href = GlobalredirectPath + "Product/ProductLicense/view?Id=" + msg.data._ProductLicenseMasterValue.Id + ""



                            //  window.location.href = "http://localhost/RMS.Web" + "/Product/ProductLicense/view?Id=" + msg.data._ProductLicenseMasterValue.Id + ""

                        }
                        else {
                            swal("No record", 'No record found', "warning");
                        }

                    }, function () {
                        swal("No record", 'No record found', "warning");
                    });
                }



                if (mstr_SearchValue == "RightsSelling") {

                    var geTopSearchsubmitList = AJService.PostDataToAPI("RightsSelling/TopSearch?Code=" + mstr_TopSearchValue);
                    geTopSearchsubmitList.then(function (msg) {

                        if (msg.data._RightsSellingMasterValue.Id != undefined) {



                            window.location.href = GlobalredirectPath + "RightsSelling/RightsSelling/RightsSellingView?Id=" + msg.data._RightsSellingMasterValue.CommeId + "&type=A" + msg.data._RightsSellingMasterValue.ProuductId + "&RightsSellingId=" + msg.data._RightsSellingMasterValue.Id + "";

                            // window.location.href = "http://localhost/RMS.Web" + "/RightsSelling/RightsSelling/RightsSellingView?Id=" + msg.data._RightsSellingMasterValue.CommeId + "&type=" + msg.data._RightsSellingMasterValue.ProuductId + "&RightsSellingId=" + msg.data._RightsSellingMasterValue.Id + "";

                        }
                        else {
                            swal("No record", 'No record found', "warning");
                        }

                    }, function () {
                        swal("No record", 'No record found', "warning");
                    });
                }



                if (mstr_SearchValue == "PermissionsOutbound") {

                    var geTopSearchsubmitList = AJService.PostDataToAPI("PermissionsOutbound/TopSearch?Code=" + mstr_TopSearchValue);
                    geTopSearchsubmitList.then(function (msg) {

                        if (msg.data._PermissionsOutboundMasterValue.Id != undefined) {

                            window.location.href = GlobalredirectPath + "PermissionsOutbound/PermissionsOutbound/PermissionsOutboundMaster?Id=" + msg.data._PermissionsOutboundMasterValue.CommeId + "&type=" + msg.data._PermissionsOutboundMasterValue.ProuductId + "&OutboundView=" + msg.data._PermissionsOutboundMasterValue.Id + "";

                            // window.location.href = "http://localhost/RMS.Web" + "/PermissionsOutbound/PermissionsOutbound/PermissionsOutboundMaster?Id=" + msg.data._PermissionsOutboundMasterValue.CommeId + "&type=" + msg.data._PermissionsOutboundMasterValue.ProuductId + "&OutboundView=" + msg.data._PermissionsOutboundMasterValue.Id + "";


                        }
                        else {
                            swal("No record", 'No record found', "warning");
                        }

                    }, function () {
                        swal("No record", 'No record found', "warning");
                    });
                }



                if (mstr_SearchValue == "PermissionsInbound") {

                    var geTopSearchsubmitList = AJService.PostDataToAPI("PermissionsInbound/TopSearch?Code=" + mstr_TopSearchValue);
                    geTopSearchsubmitList.then(function (msg) {

                        if (msg.data._PermissionInboundMasterValue.Id != undefined) {


                            window.location.href = GlobalredirectPath + "PermissionsInbound/PermissionsInbound/ViewInbound?Id=" + msg.data._PermissionInboundMasterValue.CommeId + "&type=" + msg.data._PermissionInboundMasterValue.ProuductId + "&InboundId=" + msg.data._PermissionInboundMasterValue.Id + "";




                          //  window.location.href = "http://localhost/RMS.Web" + "/PermissionsInbound/PermissionsInbound/ViewInbound?Id=" + msg.data._PermissionInboundMasterValue.CommeId + "&type=" + msg.data._PermissionInboundMasterValue.ProuductId + "&InboundId=" + msg.data._PermissionInboundMasterValue.Id + "";



                        }
                        else {
                            swal("No record", 'No record found', "warning");
                        }

                    }, function () {
                        swal("No record", 'No record found', "warning");
                    });
                }
            }
            else {
                //if (mstr_SearchValue == "")
                //{
              
                    swal("", 'Please Enter Code or Product ISBN and Select Type', "warning");
                    $('#TopSearch').focus();
                    
                   // return;
                //}
            }
                
            }
    }
  


