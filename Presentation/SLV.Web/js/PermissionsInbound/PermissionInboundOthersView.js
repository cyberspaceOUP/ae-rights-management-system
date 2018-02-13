app.expandControllerOthersViewDetails = function ($scope, AJService, $window) {
 
   
    function ConvertDateDDMMYYFormat(dateVal) {

        if (dateVal != null) {
            mstr_Date = dateVal.slice(0, 10).split('-');
            return mstr_Date[2] + "/" + mstr_Date[1] + "/" + mstr_Date[0]


        }
    }


  

    //$scope.OthersViewViewMode = function (Id) {
    
    //    var OthersViewStatus = AJService.GetDataFromAPI('PermissionsInbound/GetOthersDetails?id=' + Id, null);
       
    //    OthersViewStatus.then(function (msg) {
    //        if (msg.data != "") {
                
    //            $scope.PermissionInboundOthersView = true;
               
               
    //            //$scope.CopyRightHolderView = (msg.data._PermissionInboundCopyRightHolder.CopyRightHolderName == null ? '---' : msg.data._PermissionInboundCopyRightHolder.CopyRightHolderName);
    //            //$scope.ContactPersonView = (msg.data._PermissionInboundCopyRightHolder.ContactPerson == null ? '---' : msg.data._PermissionInboundCopyRightHolder.ContactPerson)
    //            //$scope.CopyRightHolderAddressView = (msg.data._PermissionInboundCopyRightHolder.Address == null ? '---' : msg.data._PermissionInboundCopyRightHolder.Address)
    //            //$scope.pincodeView = (msg.data._PermissionInboundCopyRightHolder.Pincode == null ? '---' : msg.data._PermissionInboundCopyRightHolder.Pincode)
    //            //$scope.MobileView = (msg.data._PermissionInboundCopyRightHolder.Mobile == null ? '---' : msg.data._PermissionInboundCopyRightHolder.Mobile)

    //            $scope.PermissionInboundCopyRightHolderDataList = msg.data._PermissionInboundCopyRightHolder;

    //          //  $scope.StatusView = (msg.data._mobj_PermissionInboundOthers.status == null ? '---' : msg.data._mobj_PermissionInboundOthers.status);
    //          //$scope.AssetSubTypeView = (msg.data._mobj_PermissionInboundOthers.AssetSubType == null ? '---' : msg.data._mobj_PermissionInboundOthers.AssetSubType);
    //          //  $scope.AssetDescriptionView = (msg.data._mobj_PermissionInboundOthers.AssetDescription == null ? '---' : msg.data._mobj_PermissionInboundOthers.AssetDescription);
    //          //  $scope.ExtentView = (msg.data._mobj_PermissionInboundOthers.Extent == null ? '---' : msg.data._mobj_PermissionInboundOthers.Extent);
    //          //  $scope.GratiscopytobesentView = (msg.data._mobj_PermissionInboundOthers.Gratiscopytobesent == null ? '---' : msg.data._mobj_PermissionInboundOthers.Gratiscopytobesent);
    //          //  $scope.NoofcopyView = (msg.data._mobj_PermissionInboundOthers.Noofcopy == null ? '---' : msg.data._mobj_PermissionInboundOthers.Noofcopy);
    //          //  $scope.OriginalSourceView = (msg.data._mobj_PermissionInboundOthers.OriginalSource == null ? '---' : msg.data._mobj_PermissionInboundOthers.OriginalSource);
    //          //  $scope.RestrictionView = (msg.data._mobj_PermissionInboundOthers.Restriction == null ? '---' : msg.data._mobj_PermissionInboundOthers.Restriction);
    //          //  $scope.SubLicensingView = (msg.data._mobj_PermissionInboundOthers.SubLicensing == null ? '---' : msg.data._mobj_PermissionInboundOthers.SubLicensing);
    //          //  $scope.FeeView = (msg.data._mobj_PermissionInboundOthers.Fee == null ? '---' : msg.data._mobj_PermissionInboundOthers.Fee);
    //          //$scope.CurrencyView = (msg.data._mobj_PermissionInboundOthers.Currency == null ? '---' : msg.data._mobj_PermissionInboundOthers.Currency);
    //          //  $scope.InvoiceNumberView = (msg.data._mobj_PermissionInboundOthers.InvoiceNumber == null ? '---' : msg.data._mobj_PermissionInboundOthers.InvoiceNumber);
    //          //  $scope.InvoiceValueView = (msg.data._mobj_PermissionInboundOthers.Invoicevalue == null ? '---' : msg.data._mobj_PermissionInboundOthers.Invoicevalue);



    //          //  $scope.PermissionExpirydateView = (msg.data._mobj_PermissionInboundOthers.PermissionExpirydate == null ? '---' : ConvertDateDDMMYYFormat(msg.data._mobj_PermissionInboundOthers.PermissionExpirydate));
    //          //  $scope.AcknowledgementlineView = (msg.data._mobj_PermissionInboundOthers.Acknowledgementline == null ? '---' : msg.data._mobj_PermissionInboundOthers.Acknowledgementline);

            
    //          //  $scope.PermissionInboundOthersRightsDataList = [];
              
    //          //  if (msg.data._Rights.length > 0)
    //          //  {
                   
    //          //      for (var i = 0; i < msg.data._Rights.length; i++)
    //          //      {
    //          //          if (msg.data._Rights[i].RightsName != null && msg.data._Rights[i].status != null)
    //          //          {
    //          //              $scope.PermissionInboundOthersRightsDataList[i] = {
    //          //                  Number: msg.data._Rights[i].Number, RightsName: msg.data._Rights[i].RightsName, RunGranted: msg.data._Rights[i].RunGranted, status: msg.data._Rights[i].status, RightsName: msg.data._Rights[i].RightsName
    //          //              }
    //          //          }

    //          //      }

    //          //  }

    //          //  $scope.PermissionInboundOthersRightsDataList = $.grep($scope.PermissionInboundOthersRightsDataList, function (n) { return n == 0 || n });

    //          //  $scope.OtherContractDateRequestList = [];

    //          //  if (msg.data._DateRequest.length > 0)
    //          //  {
    //          //      for (var i = 0 ; i < msg.data._DateRequest.length; i++)
    //          //      {
    //          //          if (msg.data._DateRequest[i].dateOf != null && msg.data._DateRequest[i].dateValue != "---")
    //          //          {
    //          //              $scope.OtherContractDateRequestList[i] = {
    //          //                  dateOf: msg.data._DateRequest[i].dateOf,dateValue : msg.data._DateRequest[i].dateValue
    //          //              }
    //          //          }
    //          //      }
    //          //  }

               


    //       // $scope.PermissionInboundOthersRightsDataList = msg.data._Rights;
    //        //    $scope.OtherContractDateRequestList = msg.data._DateRequest;
    //        }
    //        else {
    //            $scope.PermissionInboundOthersView = false;
    //            //SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
    //        }
    //    }, function () {
    //    //    alert('Error in getting copy right holder list which is not used in permission inbound');
    //    });
    //}


    
}
