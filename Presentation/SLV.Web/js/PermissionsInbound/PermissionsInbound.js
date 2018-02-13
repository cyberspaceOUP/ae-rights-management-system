app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {

    /*Expand common.master.js Controller*/
    app.expandControllerA($scope, AJService, $window);
   // app.expandControllerAuthorContractDetails($scope, AJService, $window);

   // app.expandControllerProductLicense($scope, AJService, $window);

    app.expandControllerProductDetails($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);

    $scope.RoyaltyRecurringReq = false;


    $scope.ProductSerach($('#hid_ProductId').val());

    setTimeout(function () {
        //fetch Kit Details List
        app.expandControllerKitISBNLIst($scope, AJService);
        angular.element(document.getElementById('angularid')).scope().KitISBNDetailsList($('#hid_ProductId').val());
    }, 300);

    //$scope.SubsidiaryRightsAuthorContract

    //if ($('#hid_Type').val() == "C") {
    //    $scope.AuthorContract($("#hid_AuthorContract").val());

    //    $scope.Req_ContractDeatil = true;
    //    $scope.Req_ProductLicense = false;
    //}
    //else if ($('#hid_Type').val() == "L") {

    //    $scope.Req_ProductLicense = true;
    //    $scope.Req_ContractDeatil = false;

    //    $scope.ProductLicenseSerach($("#hid_AuthorContract").val());
    //}


    $scope.InsertInBoundPermission = true;

    //$scope.func_Value = function ()
    //{
    //  
    //    $scope.PermissionsInboundModel.AssetstypeImage;
    //   
    //}


    //app.expandControllerRoyaltySlab($scope, AJService, $window);


    $scope.SetContractDate = function (datetext) {
        $scope.Permission = $(datetext).val();

    }


    $scope.setContractEffectiveDate = function (datetext) {

        $scope.ContractEffectiveDate = $(datetext).val();
    }


    $scope.setRecurringFromPeriod = function (datetext) {
        $scope.RecurringFromPeriod = $(datetext).val();
    }

    $scope.setRecurringToPeriod = function (datetext) {
        $scope.RecurringToPeriod = $(datetext).val();
    }






    $scope.funct_RoyaltyRecurringYes = function () {

        $scope.RoyaltyRecurringReq = true;
    }
    $scope.funct_RoyaltyRecurringNo = function () {
        $scope.RoyaltyRecurringReq = false;
    }



    $scope.func_InvoiceApplicableNo = function () {

        $scope.ReqInvoiceNo = false;
        $scope.ReqInvoiceValue = false;
        $scope.ReqInvoiceDescription = false;

        $scope.ReqRemarks = true;
    }


    $scope.func_CopiestobereceivedYes = function () {

        $scope.reqNumberofcopies = true;

    }


    $scope.func_CopiestobereceivedNo = function () {

        $scope.reqNumberofcopies = false;

    }
    $("#Country").attr("disabled", "disabled").removeAttr("required");
    $("#state").attr("disabled", "disabled");
    $("#city").attr("disabled", "disabled");
    $("#pincode").attr("disabled", "disabled");
    $("#geogdiv").find('span').attr('style', 'display:none');

    $scope.GeogList = function () {
        //blockUI.start();
        var GeogType = {
            geogtype: "country",
            parentid: null,
        };
        var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
        GetgeogList.then(function (GetgeogList) {
            $scope.CountryList = GetgeogList.data;
            $scope.OthersNot = true;
            $scope.OthersYes = false;
            $scope.otherCities = false;
            $scope.OtherCountry = false;
            $scope.sates = [];
        }, function () {
            //alert('Error in getting Geographical list');
        });
    }

    $scope.getCountryStates = function () {
        var GeogType = {
            geogtype: "state",
            parentid: $scope.Country,
        };
        if ($.trim($("#Country option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.OthersNot = false;
            $scope.OthersYes = true;
            $scope.OtherCountry = true;
            $scope.cities = [];
        }
        else {
            $scope.OthersNot = true;
            $scope.OthersYes = false;
            $scope.OtherCountry = false;
            $scope.cities = [];
            var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
            GetgeogList.then(function (GetgeogList) {
                $scope.sates = GetgeogList.data;
                $scope.cities = [];
            }, function () {
                //alert('Error in getting Geographical list');
            });
        }
    }
    $scope.getStateCities = function () {
        var GeogType = {
            geogtype: "city",
            parentid: $scope.State,
        };
        if ($.trim($("#state option:selected").html()).toLowerCase().indexOf("others") > -1) {
            //$scope.OthersNot = false;
            //$scope.OthersYes = true;

            $('.othersCityHide').css("display", "none");
            $scope.OthersStateYes = true;
            $scope.cities = [];
        }

        var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
        GetgeogList.then(function (GetgeogList) {
            $scope.cities = GetgeogList.data;
        }, function () {
            //alert('Error in getting Geographical list');
        });

    }

    $scope.showState = function () {
        if ($.trim($("#Country option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.State.css("display", "none");
            $scope.cities = [];
        }
    }

    $scope.ChangeCitiesCities = function () {
        if ($.trim($("#city option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.otherCities = true;
        }
        else {
            $scope.otherCities = false;

        }
    }

    $scope.PermissionsInboundForm = function (PermissionsInboundModel) {
               
        if (fn_validateForm(PermissionsInboundModel) == 0) {
            return false;
        }

        //Start of submitting form
        $scope.submitted = true;
        if ($scope.userForm.$valid) {
            if ($scope.userForm.$valid) {
                $scope.PermissionsOutboundEntry(PermissionsInboundModel);
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    }

    //setting the date textbox and array for request of multiple date
    $scope.SetDateValue = function (obj) {
        $scope.DateRequest[$(obj).attr("index")] = $(obj).val();
    };
    $scope.FillExpiry = function (obj) {
        $scope.PermissionsInboundModel.PermissionExpirydate = $(obj).val() == "" ? null : $(obj).val();
    };
    //end


    $scope.PermissionsInboundRightsModel = [];
    $scope.PrintRunGrantedForPrint = [];
    $scope.NumberPrint = [];
    $scope.DateRequest = [];
    $scope.dateRequestList = ["1st", "2nd", "3rd", "4th"];
    $scope.DateRequestdata = [];
    //for(var i=1;i<5;i++)
    //{

    //}
   
        $scope.AssetstypeImage = [];
        $scope.SelectAtleastone = true;
        $scope.ValideationProperty = function () {
            if ($("input[type=checkbox][name*=AssetstypeOthers]:checked").length == 0) {
                $("input[type=checkbox][name*=AssetstypeOthers]:checked").attr("required", "true");
            }
            else {
                $("input[type=checkbox][name*=AssetstypeOthers]:checked").removeAttr("required");
            }
        };
   
    
  

    function convertDate(date) {
        if (date != undefined) {
            var datearray = date.split("/");
            return datearray[1] + '/' + datearray[0] + '/' + datearray[2];
        }
        else {
            return null;
        }

    }

    function convertDateForInsert(dateVal) {

        if (dateVal == "") {
            dateVal = null
        }
        else {

            var RequestDate = dateVal;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            return yy + "/" + mm + "/" + dd;
        }
    }

    $scope.PermissionsOutboundEntry = function () {
        /***********************************************************************************************
           This object will store the Other Inbound permission data  seprately
       **************************************************************************************************/
        $scope.PermissionRightsObject = [];
        for (i = 0; i < $scope.OtherRightsMasterList.length; i++) {
            $scope.PermissionRightsObject[i] = {
                RightsId: $($(".rightsObject")[i]).attr("Rightsid"),
                Status: $scope.PermissionsInboundRightsModel[i],
                RunGranted: $scope.PrintRunGrantedForPrint[i],
                Number: $scope.NumberPrint[i]
            }
        }

        for (var i = 0; i < $scope.dateRequestList.length; i++) {
            $scope.DateRequestdata[i] =
            {
                DateOf: $scope.dateRequestList[i],
                DateValue: $scope.DateRequest[i] != undefined && $scope.DateRequest[i] !=""? convertDate($scope.DateRequest[i]) : null
            }
        }
        //if ($("select[name*=Currency]").find("option:selected").text() == "Indian Rupee") {
        //    $scope.PermissionsInboundModel.CurrencyValue = $("select[name*=Currency]").val();
        //}


        /***********************************************************************************************
          End
        **************************************************************************************************/

        FileNameArray = $('#dropZone0').find('.fileNameClass');
        var array = [];
        FileNameArray.each(function () {
            array.push(
                $(this).val()
            );
        });


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

            var PermissionInboundObject = {
                ProductId: $("#hid_ProductId").val(),
                TypeFor: $("#hid_Type").val(),
                LicenseId: null, // $("#hid_Type").val() == "C" ? null : $("#hid_AuthorContract").val(),
                AuthorContractId: null, // $("#hid_Type").val() == "L" ? null : $("#hid_AuthorContract").val(),
                AssetsType: $(".AssetTypeChk:checked").length == 2 ? "B" : $(".AssetTypeChk:checked").val(),
                EnteredBy: $("input[type=hidden][id*=enterdBy]").val(),
                DocFileName: $("input[type=hidden][id=hid_Uploads1]").val() == "" ? null : $("input[type=hidden][id=hid_Uploads1]").val().slice(1, -1),
                PermissionsInboundDataModel: $scope.PermissionsInboundModel,
                PermissionRightsObject: $scope.PermissionRightsObject,
                DateRequestObject: $scope.DateRequestdata
               // ImageBankId: $scope.ImageBankId
            };
         
         // debugger;
       var PermissionInboundObject = AJService.PostDataToAPI('PermissionsInbound/InsertPermissionsOutbound', PermissionInboundObject);

        PermissionInboundObject.then(function (msg) {
            if (msg.data.status != "OK" && msg.data.status != "E") {
                SweetAlert.swal('Error', 'Try again', "error");
            }
            else if (msg.data.status == "E") {
                //SweetAlert.swal('Error', 'Excel sheet contains invalid data. Please refer downloaded csv file for invalid data reason.', "error");
                SweetAlert.swal({
                    title: "Validation",
                    text: "Excel sheet contains invalid data. Please refer downloaded csv file for invalid data reason.",
                    type: "warning"
                },
                    function () {
                        if (msg.data._validateDataTable.length > 0) {
                            JSONToCSVConvertor(msg.data._validateDataTable);
                            //location.href = window.location.href;
                        }
                    });
            }
            else {
                SweetAlert.swal({
                    title: "Done",
                    text: "Inbound Permission has been Insert successfully.Inbound Permission Code is " + msg.data.code,
                    type: "success"
                },
           function () {

               //if ($("#hid_Type").val() == "C") {
               //    $window.location.href = "../../PermissionsInbound/PermissionsInbound/ViewInbound/?Id=" + $("#hid_AuthorContract").val() + "&type=C" + $("#hid_ProductId").val() + "&InboundId=" + msg.data.code + "";
               //}
               //else if ($("#hid_Type").val() == "L") {
               //    $window.location.href = "../../PermissionsInbound/PermissionsInbound/ViewInbound/?Id=" + $("#hid_AuthorContract").val() + "&type=L" + $("#hid_ProductId").val() + "&InboundId=" + msg.data.code + "";
               //}

               //$window.location.href = "../../PermissionsInbound/PermissionsInbound/ViewInbound?type=P" + $("#hid_ProductId").val() + "&InboundId=" + msg.data.code + "";
               $window.location.href = GlobalredirectPath + "PermissionsInbound/PermissionsInbound/UpdateInbound?type=P" + $("#hid_ProductId").val() + "&InboundId=" + msg.data.code + "";
           });
            }

        },
        function () {
            alert('There is some error in the system');
        });

                   }

               });
    }



    /*****************************************************************************************
    This is the parent function will be used to validating Whole inbound Permission Form
    *****************************************************************************************/
    function fn_validateForm(obj) {



            if ($(".AssetTypeChk:checked").length == 0) {
                SweetAlert.swal("Validation", "Please select Assets type", "warning");
                $($(".AssetTypeChk")[0]).focus();
                return 0;
            }
            else {
                if ($("input[type=checkbox][name*=AssetstypeImage]").is(":checked")) {
                    return fn_validateImageBank();
                }
                if ($("input[type=checkbox][name*=AssetstypeOthers]").is(":checked")) {
                    return fn_validateOthers();
                }
                if ($(".AssetTypeChk:checked").length == 2) {
                    return fn_bothInboundProcess();
                }
            }
       
    }




    /*****************************************************************************************
    This is the parent function will be used to validating Whole inbound Permission Form
    *****************************************************************************************/
    function fn_validPending()
    {
        if($('#Signed_Contract_Sent_DateValue').val() == "")
        {
            SweetAlert.swal("Validation", "Please enter Signed Contract Sent Date", "warning");
            $('#Signed_Contract_Sent_DateValue').focus();
            return 0;
        }
        else if ($('[name=PendingRemarks]').val() == "")
        {
            SweetAlert.swal("Validation", "Please enter Remarks", "warning");
            $('[name=PendingRemarks]').focus();
            return 0;
        }
    }


    function fn_validIssue() {
        if ($('#DateOfAgreementValue').val() == "") {
            SweetAlert.swal("Validation", "Please enter Agreement Date ", "warning");
            $('#DateOfAgreementValue').focus();
            return 0;
        }
        else if ($('#EffectiveDate').val() == "") {
            SweetAlert.swal("Validation", "Please enter Effective Date ", "warning");
            $('#EffectiveDate').focus();
            return 0;
        }

        else if ($('[name=ContractperiodUpload]').val() == "") {
            SweetAlert.swal("Validation", "Please enter Contract period (In months)", "warning");
            $('[name=ContractperiodUpload]').focus();
            return 0;
        }

        else if ($('#Signed_Contract_Sent_DateValue').val() == "") {
            SweetAlert.swal("Validation", "Please enter Signed Contract Sent Date", "warning");
            $('#Signed_Contract_Sent_DateValue').focus();
            return 0;
        }

        else if ($('#Signed_Received_Sent_DateValue').val() == "") {
            SweetAlert.swal("Validation", "Please enter Signed Contract Received Date", "warning");
            $('#Signed_Received_Sent_DateValue').focus();
            return 0;
        }
    }

    function fn_validCancel() {
        if ($('#Cancellation_DateValue').val() == "") {
            SweetAlert.swal("Validation", "Please enter Cancellation Date", "warning");
            $('#Cancellation_DateValue').focus();
            return 0;
        }
        else if ($('[name=Cancellation_Reason]').val() == "") {
            SweetAlert.swal("Validation", "Please enter Cancellation Reason", "warning");
            $('[name=Cancellation_Reason]').focus();
            return 0;
        }
    }


    $scope.HasFile = function () {

        errorDiv = document.getElementById("fileid");
        errorDiv.innerHTML = "";
        errormsg = "";


        var errorDiv;
        var errormsg = '';
        $scope.msg = "";
        FileNameArray = [];
        FileNameArray = $('#dropZone0').find('.fileNameClass');
        var array = [];

        if (FileNameArray.length == 0) {
            errorDiv = document.getElementById("fileid");
            errorDiv.innerHTML = "Please select a file";
            errormsg = "Please select a file";
            $scope.userForm.$valid = false;
        }
        else {
            FileNameArray.each(function () {
                array.push(
               $(this).val());

                for (i = 0; i < array.length; i++) {
                    if (array[i] == "") {
                        errorDiv = document.getElementById("fileid");
                        errorDiv.innerHTML = "Please enter file name";
                        errormsg = "Please enter file name";
                        $scope.userForm.$valid = false;
                        return 0;
                    }
                    else {
                        $scope.userForm.$valid = true;
                    }
                }
            });
        }
       
    }

    /*****************************************************************************************
    This is the parent Pending Request for Permissions Inbound Form
    *****************************************************************************************/
    function fn_valiPandingRequest(obj) {
   

        if ($(".Contractstatus:checked").length == 0) {
            SweetAlert.swal("Validation", "Please select Contract Status", "warning");
            $($(".Contractstatus")[0]).focus();
            return 0;
        }
        else {
            if ($("input[name='Contractstatus']:checked").val() == "Pending") {
                return fn_validPending();
            }
            if ($("input[name='Contractstatus']:checked").val() == "Issued") {
                $scope.HasFile();
                return fn_validIssue();
                //return 
            }
            if ($("input[name='Contractstatus']:checked").val() == "Cancelled") {
                return fn_validCancel();
            }
        }

    }






    /*****************************************************************************************
    This is the parent function will be used to validating Whole inbound Permission Form
    *****************************************************************************************/
    $scope.OtherContractStatus = true;
    function fn_validateImageBank() {
        //if ($("select[name*=PartyName]").val() == "") {
        //    SweetAlert.swal("Validation", "Please select Party Name", "warning");
        //    $("select[name*=PartyName]").focus();
        //    return 0;
        //}
        if ($("#hid_Uploads1").val() == "") {
            SweetAlert.swal("Validation", "Please Upload Contract", "warning");
            $("#dropZone0").css("border-color", "red");
            $("[id*=btn_Uploader_1]").focus();
            return 0;
        }
        return 1;

    }
    /*****************************************************************************************
        This is the parent function will be used to validating Whole inbound Permission Form
    *****************************************************************************************/
    function fn_validateOthers() {
        if ($("#CopyRightHolder").val() == "") {
            SweetAlert.swal("Validation", "Please select CopyRight Holder  ", "warning");
            $("[id*=CopyRightHolder]").focus();
            return 0;
        }
        if ($("select[name*=Status]").val() == "") {
            SweetAlert.swal("Validation", "Please select Status ", "warning");
            $("select[name*=Status]").focus();
            return 0;
        }

    }

    /**********************************************************************
    Apply the minimum validation on the basis of status selection
    **************************************************************************/
    $scope.remarks = false;
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


    $scope.CalculateExpiry = function () {

        var PeriodIdValue = $scope.PermissionsInboundModel.PermissionPeriod;
        var RequestDate = $("[name$=RequestDate]").val();

        var date = RequestDate;
        var d = new Date(date.split("/").reverse().join("-"));
        var dd = d.getDate();
        var mm = d.getMonth() + 1;
        var yy = d.getFullYear();
        var newdate = yy + "/" + mm + "/" + dd;




        if (PeriodIdValue == undefined || CurrentDate == "") {
            $scope.ProductModel.ExpiryDate = "";
            return false;
        }

        var CurrentDate = new Date(newdate);

        CurrentDate.setMonth(CurrentDate.getMonth() + parseInt(PeriodIdValue));
        var today = CurrentDate;
        var dd = today.getDate();
        var mm = today.getMonth() + 1;

        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd
        }
        if (mm < 10) {
            mm = '0' + mm
        }
        var today = dd + '/' + mm + '/' + yyyy;
        $scope.PermissionsInboundModel.ExpiryDate = today;
    }



    $scope.GetValue = function (obj) {

        $scope.SupplyRunQuantityById = obj;

        //setTimeout(function () {
        //    $scope.removeValidation();
        //}, 100)
    }




    $scope.getContractPartyType = function () {
        var getContractPartyType = AJService.GetDataFromAPI("PermissionsInbound/getContractPartyType", null);
        getContractPartyType.then(function (PartyType) {
            $scope.ContractPartyTypeList = PartyType.data;
        }, function () {
            //alert('Error in getting Vendor Type List');
        });
    }



    $scope.getAssetSubType = function () {
        var getAssetSubType = AJService.GetDataFromAPI("PermissionsInbound/getAssetSubType", null);
        getAssetSubType.then(function (msg) {
            $scope.AssetSubTypeList = msg.data;
        }, function () {
            //alert('Error in getting Asset Sub-Type List');
        });
    }

    $scope.getStatus = function () {
        var getStatus = AJService.GetDataFromAPI("PermissionsInbound/getStatus", null);
        getStatus.then(function (msg) {
            $scope.StatusList = msg.data;
        }, function () {
            //alert('Error in getting Status List');
        });
    }


    $scope.getCopyRightHolder = function () {
        var getCopyRightHolder = AJService.GetDataFromAPI("PermissionsInbound/getCopyRightHolder", null);
        getCopyRightHolder.then(function (msg) {
            $scope.CopyRightHolderList = msg.data;
        }, function () {
            //alert('Error in getting Status List');
        });
    }



    $scope.imageBankList = [];
    $scope.videoBankList = [];

    $scope.onchnagPartyDetail = function () {

        if ($scope.PermissionsInboundModel.PartyName == undefined) {
            $scope.imageBankList = [];
            $scope.videoBankList = [];
            return false;
        }

        var PartyDetail = {
            Id: $scope.PermissionsInboundModel.PartyName,
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

    $scope.getOtherRightsMaster = function () {
        var getCopyRightHolder = AJService.GetDataFromAPI("PermissionsInbound/getOtherRightsMaster", null);
        getCopyRightHolder.then(function (msg) {
            $scope.OtherRightsMasterList = msg.data;
        }, function () {
            //alert('Error in getting Other Rights Master List');
        });
    }




    $scope.onCopyRightHolder = function () {

        if ($scope.PermissionsInboundModel.CopyRightHolder == undefined)
        {
            $scope.ContactPerson = undefined;
            $scope.Mobile = undefined;
            $scope.CopyRightHolderAddress=undefined;
            $(".fadeInout").fadeOut("slow");
            return false;
        }

        var CopyRightHolderDetail = {
            Id: $scope.PermissionsInboundModel.CopyRightHolder,
        };

        // call API to fetch temp product type list basis on the FlatId
        var CopyRightHolderStatus = AJService.PostDataToAPI('PermissionsInbound/CopyRightHolderById', CopyRightHolderDetail);
        CopyRightHolderStatus.then(function (msg) {
            if (msg != null) {


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
    $scope.func_Willbematerialbetranslated_yes = function () {
        $scope.LanguageReq = true;
    }
    $scope.func_Willbematerialbetranslated_No = function () {
        $scope.LanguageReq = false;
    }
    $scope.EditPermissionsOutBound = function (Id) {
        var EditPermissionsOutBoundDetail = {
            PermissionsoutboundId: Id,

        };

        // call API to fetch temp product type list basis on the FlatId
        var EditPermissionsOutBoundDetailStatus = AJService.PostDataToAPI('PermissionsOutbound/PermissionsOutBoundDetails', EditPermissionsOutBoundDetail);
        EditPermissionsOutBoundDetailStatus.then(function (msg) {
            if (msg != null) {


                $scope.PermissionsInboundModel.Id = msg.data[0].licenseeid;
                $scope.PermissionsInboundModel.ContactPerson = msg.data[0].contactperson;

                $scope.PermissionsInboundModel.LicenseeId = msg.data[0].licenseeid;

                $scope.PermissionsInboundModel.Licenseecode = msg.data[0].licenseecode;


                $scope.PermissionsInboundModel.PublisherMobile = msg.data[0].mobile;
                $scope.PermissionsInboundModel.PublisherEmail = msg.data[0].email;
                $scope.PermissionsInboundModel.PublisherAddress = msg.data[0].address;

                $scope.PermissionsInboundModel.URL = msg.data[0].URL;

                $scope.pincode = msg.data[0].pincode;
                $scope.Country = msg.data[0].countryid;

                $scope.getCountryStates();
                $scope.State = msg.data[0].stateid;

                $scope.getStateCities();
                $scope.City = msg.data[0].cityid;

                setTimeout(function () {
                    $scope.getStateCities();
                    $scope.City = msg.data[0].cityid;
                }, 250);

                $scope.PermissionsInboundModel.RequestDate = msg.data[0].RequestDateView;
                $scope.PermissionsInboundModel.LicenseePublicationTitle = msg.data[0].licenseepublicationtitle;
                $scope.PermissionsInboundModel.RequestDate = msg.data[0].RequestDateView;
                $scope.PermissionsInboundModel.LicenseePublicationTitle = msg.data[0].licenseepublicationtitle;
                $scope.Permission = msg.data[0].DateOfPermissionView;
                $scope.PermissionsInboundModel.PermissionPeriod = msg.data[0].permissionperiod;
                $scope.PermissionsInboundModel.ExpiryDate = msg.data[0].DateExpiryView;
                $scope.PermissionsInboundModel.RequestMaterial = msg.data[0].requestmaterial;


                if (msg.data[0].will_be_material_be_translated == "Yes") {
                    $scope.LanguageReq = true;
                }
                else {
                    $scope.LanguageReq = false;
                }
                $scope.PermissionsInboundModel.Willbematerialbetranslated = msg.data[0].will_be_material_be_translated;
                $scope.PermissionsInboundModel.WillbematerialbeAdepted = msg.data[0].will_be_material_be_adepted;
                $scope.PermissionsInboundModel.LanguageId = msg.data[0].languageid;
                $scope.PermissionsInboundModel.Extent = msg.data[0].extent;
                $scope.PermissionsInboundModel.TerritoryRightId = msg.data[0].territoryid;


            }
            else {
                SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                blockUI.stop();
            }

        });

    }

    /*download csv of json data send in table type format*/

    function JSONToCSVConvertor(JSONData) {
        //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
        var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;
        var ReportTitle = ""
        var CSV = '';
        //Set Report title in first row or line

        CSV += ReportTitle + '\r\n\n';
        ShowLabel = true;

        //This condition will generate the Label/Header
        if (ShowLabel) {
            var row = "";

            //This loop will extract the label from 1st index of on array
            for (var index in arrData[0]) {

                //Now convert each value to string and comma-seprated
                row += index + ',';
            }

            row = row.slice(0, -1);

            //append Label row with line break
            CSV += row + '\r\n';
        }

        //1st loop is to extract each row
        for (var i = 0; i < arrData.length; i++) {
            var row = "";

            //2nd loop will extract each column and convert it in string comma-seprated
            for (var index in arrData[i]) {
                row += '"' + arrData[i][index] + '",';
            }

            row.slice(0, row.length - 1);

            //add a line break after each row
            CSV += row + '\r\n';
        }

        if (CSV == '') {
            alert("Invalid data");
            return;
        }

        //Generate a file name
        var fileName = "InvalidIsbn";
        //this will remove the blank-spaces from the title and replace it with an underscore


        //Initialize file format you want csv or xls
        var uri = 'data:text/csv;charset=utf-8,' + escape(CSV);

        // Now the little tricky part.
        // you can use either>> window.open(uri);
        // but this will not work in some browsers
        // or you will not get the correct file extension    

        //this trick will generate a temp <a /> tag
        var link = document.createElement("a");
        link.href = uri;

        //set the visibility hidden so it will not effect on your web-layout
        link.style = "visibility:hidden";
        link.download = fileName + ".csv";

        //this part will append the anchor tag and remove it after automatic click
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    }

    /*******************************************************************************************
        Created By  : Dheeraj kumar sharma
        Reason      : In this section add more copyright holder for a inbound permission will do
     *****************************************************************************************/

    //Section for getting inbound permission details based on inbound id
    $scope.InbounDetailsList = [];
    $scope._cpyhlderlst = [];
    $scope.AlreadyUsedCpyRght = [];
    $scope.getInboundProcessDetails = function (code) {
        var InboundProcessDetails = AJService.GetDataFromAPI('PermissionsInbound/getCopyRightHolderById?code=' + code, null);
        InboundProcessDetails.then(function (InboundProcessDetails) {
            if (InboundProcessDetails != null) {
                if ($scope.InbounDetailsList.length == 0) {
                    $scope.InbounDetailsList.push(InboundProcessDetails.data.InboundObject);
                }
                for (i = 0; i < InboundProcessDetails.data.list.length; i++)
                {
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

    /*****************************************************
        Created By  :   Dheeraj Kumar Sharma
        Created On  :   17th aug 2016
    *********************************************************/
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

    /*****************************************************
       Created By  :   check Uncheck parent checkbox
       Created On  :   17th aug 2016
   *********************************************************/
    $scope.ChkParent = "";
    $scope.CpyHlderList = [];
    $scope.checkUncheckcheckbox = function () {
        if($scope.ChkParent)
        {
            for (var i = 0; i < $scope.newCpyHlderList.length; i++)
            {
                $scope.CpyHlderList.push($scope.newCpyHlderList[i].Id);
            }
            
        }
        else
        {
            $scope.CpyHlderList = [];
        }
    };

    $scope.AddMoreCopyrightHolder = function (PermissionsInboundModel) {
        //Start of submitting form
        $scope.submitted = true;
        if ($scope.userForm.$valid) {
            if ($scope.userForm.$valid) {
                $scope.AddMultipleCopyRightHolder(PermissionsInboundModel);
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    };

    $scope.AddMultipleCopyRightHolder = function () {

        /***********************************************************************************************
          This object will store the Other Inbound permission data  seprately
      **************************************************************************************************/
        $scope.PermissionRightsObject = [];
        for (i = 0; i < $scope.OtherRightsMasterList.length; i++) {
            $scope.PermissionRightsObject[i] = {
                RightsId: $($(".rightsObject")[i]).attr("Rightsid"),
                Status: $scope.PermissionsInboundRightsModel[i],
                RunGranted: $scope.PrintRunGrantedForPrint[i],
                Number: $scope.NumberPrint[i]
            }
        }

        for (var i = 0; i < $scope.dateRequestList.length; i++) {
            $scope.DateRequestdata[i] =
            {
                DateOf: $scope.dateRequestList[i],
                DateValue: $scope.DateRequest[i] != undefined ? convertDate($scope.DateRequest[i]) : null
            }
        }
        if ($("select[name*=Currency]").find("option:selected").text() == "Indian Rupee") {
            $scope.PermissionsInboundModel.CurrencyValue = $("select[name*=Currency]").val();
        }
        $scope.PermissionsInboundModel.Code = $("input[type=hidden][id*=hid_piid]").val();

        /***********************************************************************************************
          End
        **************************************************************************************************/
        var PermissionInboundObject = {
            EnteredBy: $("input[type=hidden][id*=enterdBy]").val(),
            PermissionsInboundDataModel: $scope.PermissionsInboundModel,
            PermissionRightsObject: $scope.PermissionRightsObject,
            DateRequestObject: $scope.DateRequestdata,
         };
        var PermissionInboundObject = AJService.PostDataToAPI('PermissionsInbound/postCopyRightHolderData', PermissionInboundObject);
        PermissionInboundObject.then(function (msg) {
            if (msg.data != "OK") {
                SweetAlert.swal('Error', 'Try again', "error");
            }
             else {
                SweetAlert.swal({
                    title: "Done",
                    text: "Copyright holder has been added to Inbound Permission successfully",
                    type: "success"
                },
           function () {
               location.href = window.location.href;
           });
            }

        },
        function () {
            alert('There is some error in the system');
        });
    };




    /***********************************************************************************************
        Added by Saddam on 30/08/2016 for Display Assets type
    **************************************************************************************************/

    $scope.AssetTypeViewMode = function (Id) {
    
        var AssetTypeViewModeViewStatus = AJService.GetDataFromAPI('PermissionsInbound/GetAssetTypeDetails?id=' + Id, null);

        AssetTypeViewModeViewStatus.then(function (msg) {
            if (msg.data != "") {

              
                if (msg.data._permissionInbound.AssetsType != null) {
                    if (msg.data._permissionInbound.AssetsType == "I") {
                        $scope.AssetstypeView = 'Image Bank/ Video Bank'
                    }
                    else if (msg.data._permissionInbound.AssetsType == "O") {
                        $scope.AssetstypeView = 'Others'
                    }
                    else if (msg.data._permissionInbound.AssetsType == "B") {
                        $scope.AssetstypeView = 'Image Bank/ Video Bank And Others'
                    }
                }
                else {
                    $scope.AssetstypeView = "---"
                }

                
            }
            else {
                
               SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
            }
        }, function () {
            //alert('Error in getting copy right holder list which is not used in permission inbound');
        });
    }



    //if ($('#hid_InboundId').val() != "") {
    //    if ($('#hid_User').val() == "rt") {
           
    //        $scope.InsertInBoundPermission = false;
    //        $scope.ViewInBoundDetail = true;

    //        $scope.AssetTypeViewMode($('#hid_InboundId').val());

    //        app.expandControllerImageVideoBankViewDetails($scope, AJService, $window);
    //        $scope.ImageVideoBankViewMode($('#hid_InboundId').val());

    //        app.expandControllerOthersViewDetails($scope, AJService, $window);
    //        $scope.OthersViewViewMode($('#hid_InboundId').val());

    //        app.expandControllerPendingRequestInsertDetails($scope, AJService, $window);

           

          
    //    }


    //}


    $scope.InbounDetailsList = [];
    $scope.OtherContractList = [];
    $scope.InbounDetailsEntered = false;
    if ($('#hid_ProductId').val() != undefined && $('#hid_ProductId').val() != null && $('#hid_ProductId').val() != '') {
        var PermissionInboundDetails = AJService.GetDataFromAPI('PermissionsInbound/checkInboundDetailsByProductId?productId=' + $('#hid_ProductId').val(), null);
        PermissionInboundDetails.then(function (msg) {
            if (msg.data != "") {

                setTimeout(function () {

                var PermissionInboundData = AJService.GetDataFromAPI('PermissionsInbound/GetMultipleImageVideoBankDetails?Code=' + msg.data.Code, null);
                PermissionInboundData.then(function (msg) {
                    if (msg.data != "") {
                        if ($scope.InbounDetailsList.length == 0) {
                            $scope.InbounDetailsEntered = true;
                            $scope.InbounDetailsList.push(msg.data.InboundObject);
                        }
                        $scope.PermissionInboundImageVideoBankView = true;
                        $scope.PermissionInboundImageVideoBankDataList = [];
                       
                        if (msg.data._GetImageVideoBankDetailsList.length > 0) {

                            $scope.PermissionInboundImageVideoBankDataList = msg.data._GetImageVideoBankDetailsList
                        }



                        $scope.PermissionInboundOthersView = true;
                        $scope.PermissionInboundImageVideoBankView = true;
                        for (j = 0; j < msg.data._cpyhlderdataList.length; j++) {

                            for (i = 0; i < msg.data._cpyhlderdataList[j].length; i++) {
                                $scope.OtherContractList.push(msg.data._cpyhlderdataList[j][i]);
                            }

                        }


                        $scope.partyDetails = msg.data.partyDetails;
                        msg.data.AssetsType
                    }
                }, function () {
                    $scope.InbounDetailsEntered = false;
                    //alert('Error in getting copy right holder list which is not used in permission inbound');
                });

                }, 200);
            }
        });

    }

    $scope.ViewAssetMode = function (Id) {

        var ViewAssetStatus = AJService.GetDataFromAPI('PermissionsInbound/GetViewAssetStatus?id=' + Id, null);

        ViewAssetStatus.then(function (msg) {
            if (msg.data != "") {



                $scope.StatusView = (msg.data._mobj_PermissionInboundOthers.status == null ? '---' : msg.data._mobj_PermissionInboundOthers.status);
                $scope.AssetSubTypeView = (msg.data._mobj_PermissionInboundOthers.AssetSubType == null ? '---' : msg.data._mobj_PermissionInboundOthers.AssetSubType);
                $scope.AssetDescriptionView = (msg.data._mobj_PermissionInboundOthers.AssetDescription == null ? '---' : msg.data._mobj_PermissionInboundOthers.AssetDescription);
                $scope.ExtentView = (msg.data._mobj_PermissionInboundOthers.Extent == null ? '---' : msg.data._mobj_PermissionInboundOthers.Extent);
                $scope.GratiscopytobesentView = (msg.data._mobj_PermissionInboundOthers.Gratiscopytobesent == null ? '---' : msg.data._mobj_PermissionInboundOthers.Gratiscopytobesent);
                $scope.NoofcopyView = (msg.data._mobj_PermissionInboundOthers.Noofcopy == null ? '---' : msg.data._mobj_PermissionInboundOthers.Noofcopy);
                $scope.OriginalSourceView = (msg.data._mobj_PermissionInboundOthers.OriginalSource == null ? '---' : msg.data._mobj_PermissionInboundOthers.OriginalSource);
                $scope.RestrictionView = (msg.data._mobj_PermissionInboundOthers.Restriction == null ? '---' : msg.data._mobj_PermissionInboundOthers.Restriction);
                $scope.SubLicensingView = (msg.data._mobj_PermissionInboundOthers.SubLicensing == null ? '---' : msg.data._mobj_PermissionInboundOthers.SubLicensing);

                if (msg.data._mobj_PermissionInboundOthers.status == 'Cleared' && msg.data._mobj_PermissionInboundOthers.Fee == null) {
                    $scope.FeeView = 'Gratis';
                }
                else {
                    $scope.FeeView = (msg.data._mobj_PermissionInboundOthers.Fee == null ? '---' : msg.data._mobj_PermissionInboundOthers.Fee);
                }

                $scope.CurrencyView = (msg.data._mobj_PermissionInboundOthers.Currency == null ? '---' : msg.data._mobj_PermissionInboundOthers.Currency);
                $scope.InvoiceNumberView = (msg.data._mobj_PermissionInboundOthers.InvoiceNumber == null ? '---' : msg.data._mobj_PermissionInboundOthers.InvoiceNumber);
                $scope.InvoiceValueView = (msg.data._mobj_PermissionInboundOthers.Invoicevalue == null ? '---' : msg.data._mobj_PermissionInboundOthers.Invoicevalue);



                $scope.PermissionExpirydateView = (msg.data._mobj_PermissionInboundOthers.PermissionExpirydate == null ? '---' : ConvertDateDDMMYYFormat(msg.data._mobj_PermissionInboundOthers.PermissionExpirydate));





                $scope.AcknowledgementlineView = (msg.data._mobj_PermissionInboundOthers.Acknowledgementline == null ? '---' : msg.data._mobj_PermissionInboundOthers.Acknowledgementline);
                $scope.InboundRemarks = (msg.data._mobj_PermissionInboundOthers.InboundRemarks == null ? '---' : msg.data._mobj_PermissionInboundOthers.InboundRemarks);


                if (msg.data._mobj_PermissionInboundOthers.TerritoryRights != null) {
                    $scope.TerritoryRightsName(msg.data._mobj_PermissionInboundOthers.TerritoryRights);
                }
                else {
                    $scope.TerritoryRightsView = '---';

                }

                // $scope.TerritoryRightsView = (msg.data._mobj_PermissionInboundOthers.TerritoryRights == null ? '---' : msg.data._mobj_PermissionInboundOthers.TerritoryRights);



                $scope.PermissionInboundOthersRightsDataList = [];

                if (msg.data._Rights.length > 0) {

                    for (var i = 0; i < msg.data._Rights.length; i++) {
                        if (msg.data._Rights[i].RightsName != null && msg.data._Rights[i].status != null) {
                            $scope.PermissionInboundOthersRightsDataList[i] = {
                                Number: msg.data._Rights[i].Number, RightsName: msg.data._Rights[i].RightsName, RunGranted: msg.data._Rights[i].RunGranted, status: msg.data._Rights[i].status, RightsName: msg.data._Rights[i].RightsName
                            }
                        }

                    }

                }

                $scope.PermissionInboundOthersRightsDataList = $.grep($scope.PermissionInboundOthersRightsDataList, function (n) { return n == 0 || n });

                $scope.OtherContractDateRequestList = [];

                if (msg.data._DateRequest.length > 0) {
                    for (var i = 0 ; i < msg.data._DateRequest.length; i++) {
                        if (msg.data._DateRequest[i].dateOf != null && msg.data._DateRequest[i].dateValue != "---") {
                            $scope.OtherContractDateRequestList[i] = {
                                dateOf: msg.data._DateRequest[i].dateOf, dateValue: msg.data._DateRequest[i].dateValue
                            }
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



});