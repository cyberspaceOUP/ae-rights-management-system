app.expandControllerImageVideoBankUpdateDetails = function ($scope, AJService, $window) {
 
    $scope.ImageVideoBankUpdateMode = function (Id) {
    
        var ImageVideoBankStatus = AJService.GetDataFromAPI('PermissionsInbound/GetImageVideoBankDetails?id=' + Id, null);
        
        ImageVideoBankStatus.then(function (msg) {
            if (msg.data != "") {
               
              
                $scope.AssetstypeImageReq = true;


                $scope.PartyName = msg.data._partname.Id;
               
                if (msg.data._partname.Id != null)
                {
                    $scope.ViewonchnagPartyDetail();
                }

                if (msg.data._PermissionInboundImageVideoBankData != null) {
                    $scope.AddMoreImageVedioList = true;
                    $scope.PermissionInboundImageVideoBankDataList = msg.data._PermissionInboundImageVideoBankData;

                }
                else {
                    $scope.AddMoreImageVedioList = false;
                }
               
            }
            else {
                $scope.PermissionInboundImageVideoBankView = false;
                //SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
            }
        }, function () {
            //alert('Error in getting copy right holder list which is not used in permission inbound');
        });
    }


    $scope.getContractPartyType = function () {
        var getContractPartyType = AJService.GetDataFromAPI("PermissionsInbound/getContractPartyType", null);
        getContractPartyType.then(function (PartyType) {
            $scope.ContractPartyTypeList = PartyType.data;
        }, function () {
            //alert('Error in getting Vendor Type List');
        });
    }

    $scope.getContractPartyType();


    $scope.onchnagPartyDetail = function () {
      
        if ($scope.userForm.PartyName.$modelValue == undefined) {
            $scope.imageBankList = [];
            $scope.videoBankList = [];
            return false;
        }

        var PartyDetail = {
            Id: $scope.userForm.PartyName.$modelValue,
            //EnteredBy: $("#enterdBy").val()
        };

        // call API to fetch temp product type list basis on the FlatId
        var PartyDetailStatus = AJService.PostDataToAPI('PermissionsInbound/PartyDetailById', PartyDetail);
        PartyDetailStatus.then(function (msg) {
            if (msg != null) {
                
               
                $scope.Restriction = msg.data.mobj_partyDetails.Restriction;
                $scope.PrintRights = msg.data.mobj_partyDetails.PrintRights;

                $scope.Electronicrights = msg.data.mobj_partyDetails.Electronicrights;
                $scope.ImageBankId = msg.data.mobj_partyDetails.Id;
                $scope.Ebookrights = msg.data.mobj_partyDetails.Ebookrights;
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


    $scope.ViewonchnagPartyDetail = function () {

        if ($scope.PartyName == undefined) {
            $scope.imageBankList = [];
            $scope.videoBankList = [];
            return false;
        }

        var PartyDetail = {
            Id: $scope.PartyName,
            //EnteredBy: $("#enterdBy").val()
        };

        // call API to fetch temp product type list basis on the FlatId
        var PartyDetailStatus = AJService.PostDataToAPI('PermissionsInbound/PartyDetailById', PartyDetail);
        PartyDetailStatus.then(function (msg) {
            if (msg != null) {


                $scope.Restriction = msg.data.mobj_partyDetails.Restriction;
                $scope.PrintRights = msg.data.mobj_partyDetails.PrintRights;

                $scope.Electronicrights = msg.data.mobj_partyDetails.Electronicrights;
                $scope.ImageBankId = msg.data.mobj_partyDetails.Id;
                $scope.Ebookrights = msg.data.mobj_partyDetails.Ebookrights;
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
}
