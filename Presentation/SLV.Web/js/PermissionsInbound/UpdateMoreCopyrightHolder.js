app.expandControllerCopyRightsUpdateDetails = function ($scope, AJService, $window) {
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
    
    $scope.master = {};
    $scope.ResetOther = function () {
        $scope.OtherValue = angular.copy($scope.master);
       // $scope.AssetstypeImage[1] = false;
    };

    $scope.ResetOther();
    $scope.Filldate = function (Value)
    {

      
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

    $scope.newCpyHlderList = [];
    $scope.filterCopyrightHolder = function (_List) {
        var getCopyRightHolder = AJService.GetDataFromAPI("PermissionsInbound/getCopyRightHolder", null);
        //var getCopyRightHolder = AJService.GetDataFromAPI("PermissionsInbound/getNotUsedCopyRightHolder?cpyIds=" + _List.join(","), null);
        getCopyRightHolder.then(function (msg) {
            $scope.newCpyHlderList = msg.data;
            //$scope.newCpyHlderList = msg.data._list;

        }, function () {
            //alert('Error in getting copy right holder list which is not used in permission inbound');
        });
    };
    

    ////---Start Autocomplete for 'Copyright Holder' //added on 16 April, 2018
    AutoCompleteCopyrightHolder();
    function AutoCompleteCopyrightHolder() {
        //var obj = $("[name$=CopyRightHolder1]");

        var CopyRightHolderDataList = [];

        var getCopyRightHolderDataList = AJService.GetDataFromAPI("PermissionsInbound/getCopyRightHolder", null);
        getCopyRightHolderDataList.then(function (CopyRightHolderData) {
            for (i = 0; i < CopyRightHolderData.data.length; i++) {
                CopyRightHolderDataList[i] = { "label": CopyRightHolderData.data[i].CopyRightHolderName.trim(), "value": CopyRightHolderData.data[i].CopyRightHolderName.trim(), "data": CopyRightHolderData.data[i].Id };
            }

            $("[name$=CopyRightHolder1]").autocomplete({
                source: function (request, response) {
                    var matcher = new RegExp(request.term, "i"); //RegExp("^" + request.term, "i"); //RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
                    response($.grep(CopyRightHolderDataList, function (item) {
                        return matcher.test(item.label);
                    }));
                },

                autoFocus: true,
                select: function (event, ui) {
                    $scope.CopyRightHolder1 = ui.item.value;
                    //$scope.OtherValue.CopyRightHolder = ui.item.data;
                    $scope.OtherValue.CopyRightHolderId = ui.item.data;
                    $scope.OtherValue.hid_CopyrightholderName = ui.item.label.trim();

                    $scope.temp_CopyRightHolder = ui.item.data;


                    //------------get CopyRightHolder Details
                    var CopyRightHolderDetail = {
                        Id: $scope.temp_CopyRightHolder
                    };

                    // call API to fetch temp product type list basis on the FlatId
                    var CopyRightHolderStatus = AJService.PostDataToAPI('PermissionsInbound/CopyRightHolderById', CopyRightHolderDetail);
                    CopyRightHolderStatus.then(function (msg) {
                        if (msg != null) {

                            $('.fadeInout').css("display", "inline");

                            msg.data.CopyRightHolderName


                            $scope.OtherValue.ContactPerson = msg.data.ContactPerson;
                            $scope.OtherValue.CopyRightHolderCode = msg.data.CopyRightHolderCode;

                            $scope.OtherValue.Mobile = msg.data.Mobile;

                            $scope.OtherValue.CopyRightHolderAddress = msg.data.Address;

                            $scope.OtherValue.CopyRightHolderEmail = msg.data.Email;
                            $scope.OtherValue.CopyRightHolderURL = msg.data.URL;

                            $scope.OtherValue.CopyRightHolderAccountNo = msg.data.AccountNo;
                            $scope.CopyRightHolderBankName = msg.data.BankName;

                            $scope.OtherValue.CopyRightHolderBankAddress = msg.data.BankAddress;
                            $scope.OtherValue.CopyRightHolderIFSCCode = msg.data.IFSCCode;
                            $scope.CopyRightHolderPANNo = msg.data.PANNo;

                            $scope.OtherValue.pincode = msg.data.Pincode;
                            $scope.OtherValue.Country = msg.data.CountryId;

                            $scope.getCountryStates();
                            $scope.OtherValue.State = msg.data.Stateid;

                            $scope.getStateCities();
                            $scope.OtherValue.City = msg.data.Cityid;

                            setTimeout(function () {
                                $scope.getStateCities();
                                $scope.OtherValue.City = msg.data.Cityid;
                                $(".fadeInout").fadeIn("slow");
                            }, 250);


                        }
                        else {
                            SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                            blockUI.stop();
                        }

                    });
                    //---------------------------------

                }
            });
        }, function () {
            //alert('Error in getting Licensee list');
        });
    }
    ////---End Autocomplete for 'Copyright Holder'

   
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


    $scope.getOtherRightsMaster = function () {
        var getCopyRightHolder = AJService.GetDataFromAPI("PermissionsInbound/getOtherRightsMaster", null);
        getCopyRightHolder.then(function (msg) {
            $scope.OtherRightsMasterList = msg.data;
        }, function () {
            //alert('Error in getting Other Rights Master List');
        });
    }
    $scope.getOtherRightsMaster();




    $scope.getCopyRightHolder = function () {
        var getCopyRightHolder = AJService.GetDataFromAPI("PermissionsInbound/getCopyRightHolder", null);
        getCopyRightHolder.then(function (msg) {
            $scope.CopyRightHolderList = msg.data;
        }, function () {
            //alert('Error in getting Status List');
        });
    }
    $scope.getCopyRightHolder();
    

    //$scope.onCopyRightHolder = function () {

      
    //    if ($scope.OtherValue.CopyRightHolder == undefined) { //$scope.userForm.CopyRightHolder.$modelValue
    //        $scope.OtherValue.ContactPerson = undefined;
    //        $scope.OtherValue.Mobile = undefined;
    //        $scope.OtherValue.CopyRightHolderAddress = undefined;
    //        $(".fadeInout").fadeOut("slow");
    //        return false;
    //    }

    //    var CopyRightHolderDetail = {
    //        Id: $scope.OtherValue.CopyRightHolder, //$scope.userForm.CopyRightHolder.$modelValue
    //    };

    //    // call API to fetch temp product type list basis on the FlatId
    //    var CopyRightHolderStatus = AJService.PostDataToAPI('PermissionsInbound/CopyRightHolderById', CopyRightHolderDetail);
    //    CopyRightHolderStatus.then(function (msg) {
    //        if (msg != null) {

    //            $('.fadeInout').css("display", "inline");
              
    //            msg.data.CopyRightHolderName


    //            $scope.OtherValue.ContactPerson = msg.data.ContactPerson;
    //            $scope.OtherValue.CopyRightHolderCode = msg.data.CopyRightHolderCode;

    //            $scope.OtherValue.Mobile = msg.data.Mobile;

    //            $scope.OtherValue.CopyRightHolderAddress = msg.data.Address;

    //            $scope.OtherValue.CopyRightHolderEmail = msg.data.Email;
    //            $scope.OtherValue.CopyRightHolderURL = msg.data.URL;

    //            $scope.OtherValue.CopyRightHolderAccountNo = msg.data.AccountNo;
    //            $scope.CopyRightHolderBankName = msg.data.BankName;

    //            $scope.OtherValue.CopyRightHolderBankAddress = msg.data.BankAddress;
    //            $scope.OtherValue.CopyRightHolderIFSCCode = msg.data.IFSCCode;
    //            $scope.CopyRightHolderPANNo = msg.data.PANNo;

    //            $scope.OtherValue.pincode = msg.data.Pincode;
    //            $scope.OtherValue.Country = msg.data.CountryId;

    //            $scope.getCountryStates();
    //            $scope.OtherValue.State = msg.data.Stateid;

    //            $scope.getStateCities();
    //            $scope.OtherValue.City = msg.data.Cityid;

    //            setTimeout(function () {
    //                $scope.getStateCities();
    //                $scope.OtherValue.City = msg.data.Cityid;
    //                $(".fadeInout").fadeIn("slow");
    //            }, 250);


    //        }
    //        else {
    //            SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
    //            blockUI.stop();
    //        }

    //    });

    //}



    $scope.VewonCopyRightHolder = function (id) {

     
        if (id == null) {
            $scope.OtherValue.ContactPerson = undefined;
            $scope.OtherValue.Mobile = undefined;
            $scope.OtherValue.CopyRightHolderAddress = undefined;
            $(".fadeInout").fadeOut("slow");
            return false;
        }

        var CopyRightHolderDetail = {
            Id: id,
        };

        // call API to fetch temp product type list basis on the FlatId
        var CopyRightHolderStatus = AJService.PostDataToAPI('PermissionsInbound/UpdateCopyRightHolderById', CopyRightHolderDetail);
        CopyRightHolderStatus.then(function (msg) {
            if (msg != null) {
                
                $('.fadeInout').css("display", "inline");

                $scope.OtherValue.ContactPerson = msg.data.ContactPerson;
                $scope.OtherValue.CopyRightHolderCode = msg.data.CopyRightHolderCode;

                $scope.OtherValue.Mobile = msg.data.Mobile;

                $scope.OtherValue.CopyRightHolderAddress = msg.data.CopyRightHolderAddress;

                $scope.OtherValue.CopyRightHolderEmail = msg.data.Email;
                $scope.OtherValue.CopyRightHolderURL = msg.data.URL;

                $scope.OtherValue.CopyRightHolderAccountNo = msg.data.AccountNo;
                $scope.OtherValue.CopyRightHolderBankName = msg.data.BankName;

                $scope.OtherValue.CopyRightHolderBankAddress = msg.data.BankAddress;
                $scope.OtherValue.CopyRightHolderIFSCCode = msg.data.IFSCCode;
                $scope.OtherValue.CopyRightHolderPANNo = msg.data.PANNo;

                $scope.OtherValue.pincode = msg.data.Pincode;
                $scope.OtherValue.Country = msg.data.CountryId;

                $scope.getCountryStates();
                $scope.OtherValue.State = msg.data.Stateid;

                $scope.getStateCities();
                $scope.OtherValue.City = msg.data.Cityid;

                setTimeout(function () {
                    $scope.getStateCities();
                    $scope.OtherValue.City = msg.data.Cityid;
                    $(".fadeInout").fadeIn("slow");
                }, 250);


            }
            else {
                SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                blockUI.stop();
            }

        });

    }

   

    //$scope.MinimumValidationRequired = function () {
    //    var obj = $(event.target);

    //    if ($(obj).find("option:selected").text().toLowerCase().indexOf("cleared") > -1) {
    //        $scope.OtherContractStatus = true;
    //        $scope.remarks = false;
    //    }
    //    else {
    //        $scope.OtherContractStatus = false;
    //        $scope.remarks = true;
    //    }
    //};
   



    $scope.UpdateAssetDetail = function (Id) {

        var UpdateAssetStatus = AJService.GetDataFromAPI('PermissionsInbound/GetViewAssetStatus?id=' + Id, null);

        UpdateAssetStatus.then(function (msg) {
            if (msg.data != "") {
                $scope.AssetstypeImage[1] = true;

                $scope.ReqImageVedio = false;
            //    debugger;
             
                $scope.OtherValue.Status = msg.data._mobj_PermissionInboundOthers.statusId;
                if (msg.data._mobj_PermissionInboundOthers.AssetSubTypeId != null) {
                    $scope.OtherValue.AssetSubType = msg.data._mobj_PermissionInboundOthers.AssetSubTypeId;
                }
                else {
                    $('#AssetSubType').val('');
                }


               
               

                $scope.OtherValue.AssetDescription = msg.data._mobj_PermissionInboundOthers.AssetDescription;

                $scope.OtherValue.Extent = msg.data._mobj_PermissionInboundOthers.Extent;
                $scope.OtherValue.Gratiscopytobesent = msg.data._mobj_PermissionInboundOthers.Gratiscopytobesent;
                $scope.OtherValue.Noofcopy = msg.data._mobj_PermissionInboundOthers.Noofcopy;

                $scope.OtherValue.OriginalSource = msg.data._mobj_PermissionInboundOthers.OriginalSource;
                $scope.OtherValue.Restriction = msg.data._mobj_PermissionInboundOthers.Restriction;
                $scope.OtherValue.SubLicensing = msg.data._mobj_PermissionInboundOthers.SubLicensing;
                $scope.OtherValue.Fee = (msg.data._mobj_PermissionInboundOthers.Fee == null ? null : parseInt(msg.data._mobj_PermissionInboundOthers.Fee));
              
                $scope.OtherValue.InvoiceNumber = msg.data._mobj_PermissionInboundOthers.InvoiceNumber, //(msg.data._mobj_PermissionInboundOthers.InvoiceNumber == null ? null : parseInt(msg.data._mobj_PermissionInboundOthers.InvoiceNumber));
                $scope.OtherValue.InvoiceValue = msg.data._mobj_PermissionInboundOthers.Invoicevalue;

                if (msg.data._mobj_PermissionInboundOthers.TerritoryRights != null)
                {
                    $scope.OtherValue.TerritoryRights = msg.data._mobj_PermissionInboundOthers.TerritoryRights;
                }
                else {

                    $('#TerritoryRights').val('');
                }

              



                if (msg.data._mobj_PermissionInboundOthers.CurrencyId != null) {
                    $scope.OtherValue.CurrencyValue = msg.data._mobj_PermissionInboundOthers.CurrencyId;
                }
                else {
                    $('[name=Currency]').val('');
                }
              



                $scope.OtherValue.PermissionExpirydate = (msg.data._mobj_PermissionInboundOthers.PermissionExpirydate == null ? null : ConvertDateDDMMYYFormat(msg.data._mobj_PermissionInboundOthers.PermissionExpirydate));
                $scope.OtherValue.Acknowledgementline = (msg.data._mobj_PermissionInboundOthers.Acknowledgementline == null ? null : msg.data._mobj_PermissionInboundOthers.Acknowledgementline);
                $scope.OtherValue.InboundRemarks = (msg.data._mobj_PermissionInboundOthers.InboundRemarks == null ? null : msg.data._mobj_PermissionInboundOthers.InboundRemarks);


                $scope.OtherValue.hid_CopyrightholderId = msg.data._mobj_PermissionInboundOthers.OthersDetailsCopyRightHolderId;


               $scope.PermissionInboundOthersRightsDataList = [];
               $scope.PrintRunGrantedForPrint = [];
               $scope.PermissionsInboundRightsModel = [];
               $scope.NumberPrint = [];
               $scope.DateRequest = [];


              // setTimeout(function () {

                   if (msg.data._Rights.length > 0) {

                       for (var i = 0; i < msg.data._Rights.length; i++) {


                           if (msg.data._Rights[i].status != null) {

                            
                               $scope.PermissionsInboundRightsModel[i] = msg.data._Rights[i].status;
                           }



                           if (msg.data._Rights[i].RunGrantedValue != null) {



                               $scope.PrintRunGrantedForPrint[i] = msg.data._Rights[i].RunGrantedValue;
                           }

                           if (msg.data._Rights[i].NumberValue != null) {

                               $scope.NumberPrint[i] = msg.data._Rights[i].NumberValue;
                           }
                       }

                   }

              // }, 800)

              

                $scope.PermissionInboundOthersRightsDataList = $.grep($scope.PermissionInboundOthersRightsDataList, function (n) { return n == 0 || n });

                $scope.OtherContractDateRequestList = [];

                if (msg.data._DateRequest.length > 0) {
                    for (var i = 0 ; i < msg.data._DateRequest.length; i++) {

                        if (msg.data._DateRequest[i].Date != null) {
                            // $('#Date1stRequest_' + [i] + '').val(ConvertDateDDMMYYFormat(msg.data._DateRequest[i].Date));
                            $scope.DateRequest[i] = ConvertDateDDMMYYFormat(msg.data._DateRequest[i].Date)
                            $('#Date1stRequest_' + [i] + '').closest(".Patent").next("div").find("input").prop('disabled', false);
                        }
                        else {
                            $('#Date1stRequest_' + [i] + '').val('');
                        }
                    }
                }




                //$scope.PermissionInboundOthersRightsDataList = msg.data._Rights;
                //   $scope.OtherContractDateRequestList = msg.data._DateRequest;
            }
            else {

                SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
            }
        }, function () {
            //alert('Error in getting copy right holder list which is not used in permission inbound');
        });
    }


    $scope.Clear = function () {

    
        $scope.ContactPerson = "",
          $scope.CopyRightHolderCode = "",
        $scope.Mobile = "",
        $scope.CopyRightHolderAddress = "",
        $scope.CopyRightHolderEmail = "",
       $scope.CopyRightHolderURL = "",
        $scope.CopyRightHolderAccountNo = "",
        $scope.CopyRightHolderBankName = "",
         $scope.CopyRightHolderBankAddress = "",
        $scope.CopyRightHolderIFSCCode = "",
          $scope.CopyRightHolderPANNo = "",
       $scope.pincode = "",
       $scope.Country = "",
         $scope.State = "",
         $scope.City = ""
        $scope.CopyRightHolder = "",
        $scope.InboundOthersId = "",

          $scope.Status = $scope.initial,
           $scope.AssetSubType = "",
        $scope.AssetDescription = "",
         $scope.Extent = "",
       $scope.Gratiscopytobesent = "",
         $scope.Noofcopy = "",
         $scope.OriginalSource = "",
          $scope.Restriction = "",
          $scope.PermissionsInboundRightsModel =[],
          $scope.PrintRunGrantedForPrint =[],
          $scope.NumberPrint =[],
            $scope.SubLicensing = "",
           $scope.Fee = "",
         $scope.Currency = "",
          $scope.InvoiceNumber = "",
          $scope.InvoiceValue = "",
      
          $scope.PermissionExpirydate = "",
          $scope.DateRequest = [],
         $scope.Acknowledgementline = "",
         $scope.InboundRemarks = "",
            $scope.InboundOthersId = "",
         $scope.DateRequestdata = [],

          $scope.PermissionRightsObject = []



    }


    

    $scope.ViewCopyRightHolderById = function (value, Value2, CopyRightsHolderCode, CopyRightsHolderMasterId) {

       
        $scope.UpdateCopyRights = true;
        $scope.InsertCopyRights = false
    
       $scope.ResetOther();
    $('#hid_CopyRightHolderById').val(value);


    //$("#CopyRightHolder option:selected").text(CopyRightsHolderCode);
        //$("#CopyRightHolder option:selected").val(CopyRightsHolderMasterId);

    $scope.OtherValue.CopyRightHolderId = CopyRightsHolderMasterId;
    $scope.OtherValue.hid_CopyrightholderName = CopyRightsHolderCode.trim();
    $scope.CopyRightHolder1 = CopyRightsHolderCode;

  //  $scope.OtherValue.CopyRightHolder = CopyRightsHolderCode;
   
        $scope.InboundOthersId = Value2;
        $scope.VewonCopyRightHolder(value);
            $scope.UpdateAssetDetail(Value2);
    
     setTimeout(function () { $('#element option[value="' + value + '"]').attr("selected", "selected"); }, 122)
           
    }

    $scope.ViewCopyRightHolderByIdValue = function ()
    {
        $scope.Clear()
        $scope.ResetOther();
        $scope.UpdateCopyRights = false;
        $scope.InsertCopyRights = true;

       
       // $('#hid_ValidateReqOther').val("InsertReq");
        $scope.InsertAssetstypeImage[1] = true;
        $scope.AssetstypeImage[1] = true;

        $scope.ReqImageVedio = false;

        //setTimeout(function () {
        //    app.expandControllerManageCopyRightsHolder($scope, AJService, $window);
        //}, 600);
    }
    // 


    //$scope.UpdateCopyRights = true;
    //$scope.InsertCopyRights = false


    $scope.InbounDetailsList = [];
    $scope._cpyhlderlst = [];
    $scope.AlreadyUsedCpyRght = [];
    //$scope.getInboundProcessDetails = function (id) {
    //    var InboundProcessDetails = AJService.GetDataFromAPI('PermissionsInbound/getCopyRightHolderById?id=' + id, null);
    //    InboundProcessDetails.then(function (InboundProcessDetails) {
    //        if (InboundProcessDetails != null) {
    //            $scope.InbounDetailsList.push(InboundProcessDetails.data.InboundObject);
    //            $scope._cpyhlderlst = InboundProcessDetails.data._cpyrgthlderobject;
    //            for (i = 0; i < $scope._cpyhlderlst.length; i++) {
    //                $scope.AlreadyUsedCpyRght.push($scope._cpyhlderlst[i].CopyRightHolderCode);
    //            }
    //            $scope.filterCopyrightHolder($scope.AlreadyUsedCpyRght);
    //        };
    //    });
    //};

    $scope.getInboundProcessDetails = function (code) {
        var InboundProcessDetails = AJService.GetDataFromAPI('PermissionsInbound/getCopyRightHolderById?code=' + code, null);
        InboundProcessDetails.then(function (InboundProcessDetails) {
            if (InboundProcessDetails != null) {
                if ($scope.InbounDetailsList.length == 0) {
                    $scope.InbounDetailsList.push(InboundProcessDetails.data.InboundObject);
                }
                for (i = 0; i < InboundProcessDetails.data.list.length; i++) {
                    $scope._cpyhlderlst.push(InboundProcessDetails.data.list[i][0])
                }

                //$scope._cpyhlderlst = InboundProcessDetails.data._cpyrgthlderobject;
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
        var getCopyRightHolder = AJService.GetDataFromAPI("PermissionsInbound/getCopyRightHolder", null);
        //var getCopyRightHolder = AJService.GetDataFromAPI("PermissionsInbound/getNotUsedCopyRightHolder?cpyIds=" + _List.join(","), null);
        getCopyRightHolder.then(function (msg) {
            $scope.newCpyHlderList = msg.data;
            //$scope.newCpyHlderList = msg.data._list;

        }, function () {
            //alert('Error in getting copy right holder list which is not used in permission inbound');
        });
    };


    $scope.NoResponse = false;
    $scope.MinimumValidationRequired = function (Value) {

        
      
        var obj = $('#' + Value.id+'');

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
            $(obj).find("option:selected").text().toLowerCase().indexOf("no trace") > -1) {
            $scope.NoResponse = true;
            $scope.remarks = false;
        }

    };


    $scope.getTerritoryRightsList = function () {
        var TerritoryRightsList = AJService.GetDataFromAPI("CommonList/getTerriteryRights", null);
        TerritoryRightsList.then(function (TerritoryRightsList) {
            $scope.TerritoryList = TerritoryRightsList.data.query;
        }, function () {
            //alert('Error in getting Territery Rights List');
        });
    }

    $scope.getTerritoryRightsList();
}
