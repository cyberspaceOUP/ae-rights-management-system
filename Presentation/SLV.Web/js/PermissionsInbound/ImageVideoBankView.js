app.expandControllerImageVideoBankViewDetails = function ($scope, AJService, $window, SweetAlert) {
 
    $scope.ImageVideoBankViewMode = function (Id) {
    
        var ImageVideoBankStatus = AJService.GetDataFromAPI('PermissionsInbound/GetImageVideoBankDetails?id=' + Id, null);
       
        ImageVideoBankStatus.then(function (msg) {
            if (msg.data != "") {
                
                $scope.PermissionInboundImageVideoBankView = true;
               
                    
              
                $scope.PartyNameView = (msg.data._partname.PartyName == null ? '---' : msg.data._partname.PartyName);
                $scope.RestrictionView = (msg.data._OtherContractImageBank.restriction == null ? '---' : msg.data._OtherContractImageBank.restriction)
                $scope.EbookRightsView = (msg.data._OtherContractImageBank.ebookrights == null ? '---' : msg.data._OtherContractImageBank.ebookrights)
                $scope.PrintRightsView = (msg.data._OtherContractImageBank.PrintRights == null ? '---' : msg.data._OtherContractImageBank.PrintRights)
                $scope.ElectronicRightsView = (msg.data._OtherContractImageBank.electronicrights == null ? '---' : msg.data._OtherContractImageBank.electronicrights)



                $scope.imageBankViewList = [];
                $scope.videoBankViewList = [];
                for (var i = 0; i < msg.data.videoimagebankView.length; i++) {
                    if (msg.data.videoimagebankView[i].BankType == "I") {
                        $scope.imageBankViewList.push(msg.data.videoimagebankView[i]);
                    }
                    else if (msg.data.videoimagebankView[i].BankType == "V") {
                        $scope.videoBankViewList.push(msg.data.videoimagebankView[i]);
                    }
                }

                $scope.PermissionInboundImageVideoBankDataList = msg.data._PermissionInboundImageVideoBankData;

            }
            else {
                $scope.PermissionInboundImageVideoBankView = false;
                //SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
            }
        }, function () {
            //alert('Error in getting copy right holder list which is not used in permission inbound');
        });
    }

    $scope.InbounDetailsList = [];
    $scope.OtherContractList = [];
    $scope.ImageVideoBankViewModeAll = function (Id) {

        var ImageVideoBankStatus = AJService.GetDataFromAPI('PermissionsInbound/GetMultipleImageVideoBankDetails?Code=' + Id, null);

        ImageVideoBankStatus.then(function (msg) {
            if (msg.data != "") {
                if ($scope.InbounDetailsList.length==0) {
                    $scope.InbounDetailsList.push(msg.data.InboundObject);
                }
                $scope.PermissionInboundImageVideoBankView = true;
                $scope.PermissionInboundImageVideoBankDataList = [];
              //  debugger;
                //if (msg.data.list.length > 0) {
                //    for (j = 0; j < msg.data.list.length; j++) {
                //        for (i = 0; i < msg.data.list[j].length; i++) {
                //            $scope.PermissionInboundImageVideoBankDataList.push(msg.data.list[j][i]);
                //        }
                //    }

                //}
                if (msg.data._GetImageVideoBankDetailsList.length > 0)
                {

                    $scope.PermissionInboundImageVideoBankDataList = msg.data._GetImageVideoBankDetailsList
                }

               

                $scope.PermissionInboundOthersView = true;
                $scope.PermissionInboundImageVideoBankView = true;
                for (j = 0; j < msg.data._cpyhlderdataList.length; j++)
                {
                   
                    for (i = 0; i < msg.data._cpyhlderdataList[j].length; i++) {
                        $scope.OtherContractList.push(msg.data._cpyhlderdataList[j][i]);
                    }
                   
                }

                
                $scope.partyDetails = msg.data.partyDetails;
                msg.data.AssetsType
               

            }
            else {
                $scope.PermissionInboundImageVideoBankView = false;
                //SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
            }
        }, function () {
            //alert('Error in getting copy right holder list which is not used in permission inbound');
        });
    }


    $scope.ViewonchnagPartyDetail = function (id) {

        var PartyDetail = {
            Id: id,
          
        };

        var PartyDetailStatus = AJService.PostDataToAPI('PermissionsInbound/PartyDetailById', PartyDetail);
        PartyDetailStatus.then(function (msg) {
            if (msg != null) {

                
               
                $scope.PartyNameView = msg.data.mobj_partName.PartyName;

                $scope.RestrictionView = msg.data.mobj_partyDetails.Restriction;
                $scope.PrintRightsView = msg.data.mobj_partyDetails.PrintRights;

                $scope.ElectronicrightsView = msg.data.mobj_partyDetails.Electronicrights;
                $scope.ImageBankIdView = msg.data.mobj_partyDetails.Id;
                $scope.EbookrightsView = msg.data.mobj_partyDetails.Ebookrights;
                $scope.imageBankList = [];
                $scope.videoBankList = [];
                for (var i = 0; i < msg.data.videoimagebank.length; i++) {
                    if (msg.data.videoimagebank[i].BankType == "I") {
                        $scope.imageBankList.push(msg.data.videoimagebank[i]);
                    }
                    else if (msg.data.videoimagebank[i].BankType == "V") {
                        $scope.videoBankList.push(msg.data.videoimagebank[i]);
                    }
                }


            }
            else {
                SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                blockUI.stop();
            }

        });

    }


    //For Delete Entity of Image Video Bank Data // Added by Prakash on 02 Feb, 2018
    $scope.DeleteEntityImageVideoBankData = function (Id, LinkId, DataId, role) {
        var mobj_delete = {
            Id: Id == undefined ? null : Id,
            LinkId: LinkId == undefined ? null : LinkId,
            DataId: DataId == undefined ? null : DataId,
            Type: 'imagevideobank',
            //Role: role == undefined ? null : role,
            DeactivateBy: parseInt($("#enterdBy").val()),
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
                //blockUI.stop();

                var Delete = AJService.PostDataToAPI("PermissionsInbound/DeleteImageVideoBankData", mobj_delete);
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
                                window.location.reload();
                            }
                        });
                    }
                });

            }
        });
    }

    //For Delete Entity of Others Data // Added by Prakash on 02 Feb, 2018
    $scope.DeleteEntityOtherData = function (Id, OthersId, role) {
        var mobj_delete = {
            Id: Id == undefined ? null : Id,
            OthersId: OthersId == undefined ? null : OthersId,
            Type: 'others',
            //Role: role == undefined ? null : role,
            DeactivateBy: parseInt($("#enterdBy").val()),
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
                //blockUI.stop();

                var Delete = AJService.PostDataToAPI("PermissionsInbound/DeleteImageVideoBankData", mobj_delete);
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
                                window.location.reload();
                            }
                        });
                    }
                });

            }
        });
    }

    
}
