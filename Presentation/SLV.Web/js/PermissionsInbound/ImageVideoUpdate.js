app.expandControllerImageVideoUpdateDetails = function ($scope, AJService, $window) {
 
    function ConvertDateDDMMYYFormat(dateVal) {

        if (dateVal != null) {
            mstr_Date = dateVal.slice(0, 10).split('-');
            return mstr_Date[2] + "/" + mstr_Date[1] + "/" + mstr_Date[0]


        }
    }



    $scope.ClearImageDate = function () {

      
        if ($scope.ContractTypes != undefined) {
            $scope.ContractTypes = "";
        
        }
        if ($scope.imagevideobankid != undefined) {
            $scope.imagevideobankid = "";
        }

        if ($scope.Description != undefined) {
            $scope.Description = "";
        }
        if ($scope.invoiceno != undefined) {
            $scope.invoiceno = "";
        }

        if ($scope.invoicevalue != undefined) {
            $scope.invoicevalue = "";
        }
        if ($scope.invoicedate != undefined) {
            $scope.invoicedate = "";
        }
        if ($scope.printquantity != undefined) {
            $scope.printquantity = "";
        }
        if ($scope.permissionexpirydate != undefined) {
            $scope.permissionexpirydate = "";

        }




        if ($scope.weblink != undefined) {
            $scope.weblink = "";

        }

        if ($scope.creditlines != undefined) {
            $scope.creditlines = "";

        }
        if ($scope.EditorialonlyType != undefined) {
            $scope.EditorialonlyType = "";

        }

        if ($scope.Remarks != undefined) {
            $scope.Remarks = "";

        }
        if ($scope.ImageVideoBankDataId != undefined) {
            $scope.ImageVideoBankDataId = "";

        }



        // $('#invoicedate').val('');
    }
    


    

    $scope.ImageVideoBankViewByIdMode = function (Id) {

        
        var mageVideoBankViewByIdStatus = AJService.GetDataFromAPI('PermissionsInbound/GetImageVideoBankViewByIdDetails?id=' + Id, null);



    //setTimeout(function () { 
        mageVideoBankViewByIdStatus.then(function (msg) {
            if (msg.data != "") {

                $scope.ReqImageVedio = true;

                $scope.InsertAssetstypeImage[1] = false;
                $scope.AssetstypeImage[1] = false;

                $scope.user.ContractTypes = msg.data._PermissionInboundImageVideoBankData.ContractTypes;

                $('#hid_ImageVedio').val(msg.data._PermissionInboundImageVideoBankData.ImageVideoBankDataId);

            
              $scope.user.imagevideobankid = (msg.data._PermissionInboundImageVideoBankData.imagevideobankid);

              $scope.user.Description = msg.data._PermissionInboundImageVideoBankData.Description;

              $scope.user.invoiceno = msg.data._PermissionInboundImageVideoBankData.invoicen;

          
              $scope.user.invoicevalue = msg.data._PermissionInboundImageVideoBankData.invoicevalue;

           
              $scope.user.invoicedate = (msg.data._PermissionInboundImageVideoBankData.invoicedate == null ? null : ConvertDateDDMMYYFormat(msg.data._PermissionInboundImageVideoBankData.invoicedate));

          
              $scope.user.printquantity = msg.data._PermissionInboundImageVideoBankData.printquantity == 0 ? '' : msg.data._PermissionInboundImageVideoBankData.printquantity;

          
              $scope.user.permissionexpirydate = (msg.data._PermissionInboundImageVideoBankData.permissionexpirydate == null ? null : ConvertDateDDMMYYFormat(msg.data._PermissionInboundImageVideoBankData.permissionexpirydate));

             
              $scope.user.weblink = msg.data._PermissionInboundImageVideoBankData.weblink;

        
              $scope.user.creditlines = msg.data._PermissionInboundImageVideoBankData.Credit_Lines;

              
              $scope.user.EditorialonlyType = msg.data._PermissionInboundImageVideoBankData.Editorial_Only_Type;

          
              $scope.user.Remarks = msg.data._PermissionInboundImageVideoBankData.Remarks;

              $scope.user.ImageVideoBankDataId = msg.data._PermissionInboundImageVideoBankData.ImageVideoBankDataId;

              $scope.user.Usage = msg.data._PermissionInboundImageVideoBankData.usage;

              $scope.user.PartyName = msg.data.PartyType.partyname;
              
              $scope.user.Currency = msg.data._PermissionInboundImageVideoBankData.Currency;

              $scope.user.hid_ImageVideoBankId = msg.data._PermissionInboundImageVideoBankData.ImageVideoBankId;


            }
            else {
                // $scope.PermissionInboundImageVideoBankView = false;
                SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
            }
        }, function () {
            //alert('Error in getting copy right holder list which is not used in permission inbound');
        });
      //}, 1100)


    }


 
  
 
    $scope.master = {};
    $scope.Reset = function () {
        $scope.user = angular.copy($scope.master);
    };

    $scope.Reset();

    $scope.func_validationReq = function ()
    {
        
        
        $scope.Reset();
        $scope.ClearImageDate();
        $('#hid_ImageVedio').val('');
        $scope.ReqImageVedio = true;

        $scope.InsertAssetstypeImage[1] = false;
        $scope.AssetstypeImage[1] = false;
    }


    $scope.invoicedateValue = function (datetext) {
        
      
        $scope.user.invoicedate = $(datetext).val();
    }

    $scope.permissionexpirydateValue = function (datetext) {

        $scope.user.permissionexpirydate = $(datetext).val();
    }
    
    
   
}
