app.expandControllerCopyRightsInsertDetails = function ($scope, AJService, $window) {
    function ConvertDateDDMMYYFormat(dateVal) {

        if (dateVal != null) {
            mstr_Date = dateVal.slice(0, 10).split('-');
            return mstr_Date[2] + "/" + mstr_Date[1] + "/" + mstr_Date[0]


        }
    }
    $scope.dateRequestList = ["1st", "2nd", "3rd", "4th"];
    var j = 0
    setTimeout(function () {
        for (var i = 0; i < $scope.dateRequestList.length; i++) {
            if (j != 0) {
                $('#Date1stRequest_' + [i] + '').prop('disabled', true);
            }
            j++;

        }
    }, 1500)
    

    $scope.Filldate = function (Value)
    {

        //debugger;
        $('#' + Value.id + '')

        $('#' + Value.id + '').closest(".Patent").next("div").find("input").prop('disabled', false);
    }

   // $scope.dateRequestList = [];
    $scope.getCurrencyList();
    $scope.InbounDetailsList = [];
    $scope._cpyhlderlst = [];
    $scope.AlreadyUsedCpyRght = [];
    $scope.getInboundProcessDetails = function (id) {
        var InboundProcessDetails = AJService.GetDataFromAPI('PermissionsInbound/getCopyRightHolderById?id=' + id, null);
        InboundProcessDetails.then(function (InboundProcessDetails) {
            if (InboundProcessDetails != null) {
                if ($scope.InbounDetailsList.length == 0) {
                    $scope.InbounDetailsList.push(InboundProcessDetails.data.InboundObject);
                }
                $scope._cpyhlderlst = InboundProcessDetails.data._cpyrgthlderobject;
                for (i = 0; i < $scope._cpyhlderlst.length; i++) {
                    $scope.AlreadyUsedCpyRght.push($scope._cpyhlderlst[i].CopyRightHolderCode);
                }
                $scope.filterCopyrightHolder($scope.AlreadyUsedCpyRght);
            };
        });
    };


    $scope.getInboundProcessDetails($('#hid_InboundId').val());
    $scope.newCpyHlderList = [];
    $scope.filterCopyrightHolder = function (_List) {
        var getCopyRightHolder = AJService.GetDataFromAPI("PermissionsInbound/getNotUsedCopyRightHolder?cpyIds=" + _List.join(","), null);
        getCopyRightHolder.then(function (msg) {
            $scope.newCpyHlderList = msg.data._list;

        }, function () {
            //alert('Error in getting copy right holder list which is not used in permission inbound');
        });
    };

   
    $scope.getStatus = function () {
        var getStatus = AJService.GetDataFromAPI("PermissionsInbound/getStatus", null);
        getStatus.then(function (msg) {
            $scope.StatusList = msg.data;
        }, function () {
            //alert('Error in getting Status List');
        });
    }
    $scope.getStatus();


    $scope.getAssetSubType = function () {
        var getAssetSubType = AJService.GetDataFromAPI("PermissionsInbound/getAssetSubType", null);
        getAssetSubType.then(function (msg) {
            $scope.AssetSubTypeList = msg.data;
        }, function () {
            //alert('Error in getting Asset Sub-Type List');
        });
    }
    $scope.getAssetSubType();


 






    $scope.onCopyRightHolderInsert = function () {

      
        if ($scope.userForm.CopyRightHolder.$modelValue == undefined) {
            $scope.ContactPerson = undefined;
            $scope.Mobile = undefined;
            $scope.CopyRightHolderAddress = undefined;
            $(".fadeInout").fadeOut("slow");
            return false;
        }

        var CopyRightHolderDetail = {
            Id: $scope.userForm.CopyRightHolder.$modelValue,
        };

        // call API to fetch temp product type list basis on the FlatId
        var CopyRightHolderStatus = AJService.PostDataToAPI('PermissionsInbound/CopyRightHolderById', CopyRightHolderDetail);
        CopyRightHolderStatus.then(function (msg) {
            if (msg != null) {

                $('.fadeInout').css("display", "inline");
              
                $scope.ContactPerson = msg.data.ContactPerson;
                $scope.CopyRightHolderCode = msg.data.CopyRightHolderCode;

                $scope.Mobile = msg.data.Mobile;

                $scope.CopyRightHolderAddress = msg.data.Address;

                $scope.CopyRightHolderEmail = msg.data.Email;
                $scope.CopyRightHolderURL = msg.data.URL;

                $scope.CopyRightHolderAccountNo = msg.data.AccountNo;
                $scope.CopyRightHolderBankName = msg.data.BankName;

                $scope.CopyRightHolderBankAddress = msg.data.BankAddress;
                $scope.CopyRightHolderIFSCCode = msg.data.IFSCCode;
                $scope.CopyRightHolderPANNo = msg.data.PANNo;

                $scope.pincode = msg.data.Pincode;
                $scope.Country = msg.data.CountryId;

                $scope.getCountryStates();
                $scope.State = msg.data.Stateid;

                $scope.getStateCities();
                $scope.City = msg.data.Cityid;

                setTimeout(function () {
                    $scope.getStateCities();
                    $scope.City = msg.data.Cityid;
                    $(".fadeInout").fadeIn("slow");
                }, 250);


            }
            else {
                SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                blockUI.stop();
            }

        });

    }



  

   
    $scope.NoResponse = false;
    $scope.MinimumValidationRequired = function () {
        var obj = $(event.target);

        if ($(obj).find("option:selected").text().toLowerCase().indexOf("cleared") > -1) {
            $scope.OtherContractStatus = true;
            $scope.remarks = false;
        }
        else {
            $scope.OtherContractStatus = false;
            $scope.NoResponse = false;
            $scope.remarks = true;
        }

        if ($(obj).find("option:selected").text().toLowerCase().indexOf("no response") > -1 ||
            $(obj).find("option:selected").text().toLowerCase().indexOf("pending") > -1 ||
            $(obj).find("option:selected").text().toLowerCase().indexOf("no trace") > -1 ) {
            $scope.NoResponse = true;
            $scope.remarks = false;
        }
    };
   

    $scope.getInsertOtherRightsMaster = function () {
        var getCopyRightHolder = AJService.GetDataFromAPI("PermissionsInbound/getOtherRightsMaster", null);
        getCopyRightHolder.then(function (msg) {
            $scope.OtherRightsMasterInsertList = msg.data;
        }, function () {
            //alert('Error in getting Other Rights Master List');
        });
    }
    $scope.getInsertOtherRightsMaster();


    $scope.Clear = function ()
    {

       $scope.ContactPerson ="",
         $scope.CopyRightHolderCode="",
       $scope.Mobile="",
       $scope.CopyRightHolderAddress="",
       $scope.CopyRightHolderEmail="",
      $scope.CopyRightHolderURL="",
       $scope.CopyRightHolderAccountNo="",
       $scope.CopyRightHolderBankName="",
        $scope.CopyRightHolderBankAddress="",
       $scope.CopyRightHolderIFSCCode="",
         $scope.CopyRightHolderPANNo="" ,
      $scope.pincode="",
      $scope.Country="",
        $scope.State="",
        $scope.City = ""
       $scope.CopyRightHolder = "",
       $scope.InboundOthersId = "",

         $scope.Status = "",
          $scope.AssetSubType = "",
       $scope.AssetDescription = "",
        $scope.Extent = "",
      $scope.Gratiscopytobesent = "",
        $scope.Noofcopy  = "",
        $scope.OriginalSource = "",
         $scope.Restriction  = "",
         $scope.PermissionsInboundRightsModel = "",
         $scope.PrintRunGrantedForPrint = "",
         $scope.NumberPrint  = "",
           $scope.SubLicensing = "",
          $scope.Fee = "",
        $scope.Currency  = "",
         $scope.InvoiceNumber = "",
         $scope.InvoiceValue = "",
        //  ($('#PermissionExpirydate').val() == "" ? null :convertDateForInsert($('#PermissionExpirydate').val())),
         $scope.PermissionExpirydate = "",
          $scope.DateRequest = "",
        $scope.Acknowledgementline = "",
        $scope.InboundRemarks = "",
           $scope.InboundOthersId = "",
        $scope.DateRequestdata = "",

         $scope.PermissionRightsObject  = ""



    }


    $scope.ViewCopyRightHolderByIdValue = function () {

        $scope.Clear();



        // alert(value);

       // debugger;

      //  $scope.CopyRightHolder = "";

       // $scope.CopyRightHolder = value;

     //   $scope.InboundOthersId = "";

        //setTimeout(function () {
        //    $scope.VewonCopyRightHolder();
        //    // alert(Value2)
        //    $scope.UpdateAssetDetail(Value2);
        //}, 300)


    }


  
   // 
    
}
