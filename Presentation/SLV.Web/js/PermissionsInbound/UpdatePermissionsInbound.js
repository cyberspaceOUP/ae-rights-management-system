app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {

    /*Expand common.master.js Controller*/
    app.expandControllerA($scope, AJService, $window);
    app.expandControllerAuthorContractDetails($scope, AJService, $window);

    app.expandControllerProductLicense($scope, AJService, $window);

    app.expandControllerProductDetails($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);

    
    $scope.ProductSerach($('#hid_ProductId').val());

    setTimeout(function () {
        //fetch Kit Details List
        app.expandControllerKitISBNLIst($scope, AJService);
        angular.element(document.getElementById('angularid')).scope().KitISBNDetailsList($('#hid_ProductId').val());
    }, 300);

    $scope.AssetstypeImage = [];
    $scope.InsertAssetstypeImage = [];
    //$scope.UploadPendingFile = true;

    $scope.ReqAssetstypeImage = true;

    var mstr_AssetType = "";
    var mobj_buttonData = "";
    var mobj_buttonAttr = "";

    //if ($('#hid_Type').val() == "A") {
    //    $scope.AuthorContract($("#hid_AuthorContract").val());

    //    $scope.Req_ContractDeatil = true;
    //    $scope.Req_ProductLicense = false;
    //}
    //else if ($('#hid_Type').val() == "P") {

    //    $scope.Req_ProductLicense = true;
    //    $scope.Req_ContractDeatil = false;

    //    $scope.ProductLicenseSerach($("#hid_AuthorContract").val());
    //}


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

    $scope.DateRequestdata = [];


    
    $scope.PermissionsOutboundEntry = function () {

        mobj_buttonData = $("input[type=submit][clicked=true]").prevObject.context.activeElement;
        mobj_buttonAttr = $(mobj_buttonData).data('index');


        SweetAlert.swal({
            title: "Are you sure?",
            text: "",
            type: "info",
            showCancelButton: true,
            confirmButtonColor: "#8CD4F5",
            confirmButtonText: "Yes",
            closeOnConfirm: false,
            closeOnCancel: true,
            showLoaderOnConfirm: true
        },
            function (Confirm) {
                if (Confirm) {




                    FileNameArray = $('#dropZone0').find('.fileNameClass');
                    var array = [];
                    FileNameArray.each(function () {
                        array.push(
                            $(this).val()
                        );
                    });





                    if ($('#hid_User').val() == "ad" || $('#hid_User').val() == "rt" || $('#hid_User').val() == "sa") {

                        for (var i = 0; i < $scope.dateRequestList.length; i++) {
                            $scope.DateRequestdata[i] =
                            {
                                DateOf: $scope.dateRequestList[i],
                                ///  DateValue: $scope.DateRequest[i] != undefined ? convertDate($scope.DateRequest[i]) : null

                                DateValue: $('#Date1stRequest_' + [i] + '').val() != "" ? convertDate($('#Date1stRequest_' + [i] + '').val()) : null
                            }
                        }
                        $scope.PermissionRightsObject = [];

                        //if ($("#CopyRightHolder option:selected").text() != "Please Select" && $("#CopyRightHolder option:selected").text() != "") {
                        if ($scope.OtherValue.hid_CopyrightholderName != undefined && $scope.OtherValue.hid_CopyrightholderName != "") {
                            for (i = 0; i < $scope.OtherRightsMasterList.length; i++) {
                                $scope.PermissionRightsObject[i] = {
                                    RightsId: $($(".rightsObject")[i]).attr("Rightsid"),
                                    Status: ($scope.PermissionsInboundRightsModel[i] == undefined ? null : $scope.PermissionsInboundRightsModel[i]),
                                    RunGranted: ($scope.PrintRunGrantedForPrint[i] == undefined ? null : $scope.PrintRunGrantedForPrint[i]),
                                    Number: ($scope.NumberPrint[i] == undefined ? null : $scope.NumberPrint[i])
                                }
                            }
                        }





                        var PermissionInboundObject = {
                            ProductId: $("#hid_ProductId").val(),
                            TypeFor: $("#hid_Type").val(),
                            LicenseId: null, // $("#hid_Type").val() == "C" ? null : $("#hid_AuthorContract").val(),
                            AuthorContractId: null, // $("#hid_Type").val() == "L" ? null : $("#hid_AuthorContract").val(),
                            AssetsType: $(".AssetTypeChk:checked").length == 2 ? "B" : $(".AssetTypeChk:checked").val(),

                            ContractTypes: $scope.userForm.ContractTypes.$modelValue,
                            imagevideobankid: $scope.userForm.imagevideobankid.$modelValue,
                            Description: $scope.userForm.Description.$modelValue,

                            invoiceno: $scope.userForm.invoiceno.$modelValue,
                            invoicevalue: $scope.userForm.invoicevalue.$modelValue,
                            invoicedate: ($('#invoicedate').val() == "" ? null : convertDateForInsert($('#invoicedate').val())),
                            printquantity: $scope.userForm.printquantity.$modelValue,
                            permissionexpirydate: ($('#permissionexpirydate').val() == "" ? null : convertDateForInsert($('#permissionexpirydate').val())),
                            weblink: $scope.userForm.weblink.$modelValue,
                            creditlines: $scope.userForm.creditlines.$modelValue,

                            EditorialonlyType: $scope.userForm.EditorialonlyType.$modelValue,
                            Remarks: $scope.userForm.Remarks.$modelValue,

                            hid_ImageVideoBankId: $scope.userForm.hid_ImageVideoBankId.$modelValue,

                            Usage: $scope.userForm.Usage.$modelValue,
                            PartyName: $scope.userForm.PartyName.$modelValue,


                            ImageVideoCurrency: $('#CurrencyValueData').val() == "" ? null : $('#CurrencyValueData').val(), //$scope.userForm.Currency.$modelValue,

                            //CopyRightHolderName: ($("#CopyRightHolder option:selected").text() == "Please Select" ? null : $("#CopyRightHolder option:selected").text()),
                            CopyRightHolderName: $scope.OtherValue.hid_CopyrightholderName == undefined ? null : $scope.OtherValue.hid_CopyrightholderName.trim(),

                            hid_CopyrightholderId: $scope.OtherValue.hid_CopyrightholderId == undefined ? null : $scope.OtherValue.hid_CopyrightholderId,


                            ContactPerson: $scope.OtherValue.ContactPerson,
                            CopyRightHolderCode: $scope.OtherValue.CopyRightHolderCode,
                            Mobile: $scope.OtherValue.Mobile,
                            CopyRightHolderAddress: $scope.OtherValue.CopyRightHolderAddress,
                            CopyRightHolderEmail: $scope.OtherValue.CopyRightHolderEmail,
                            CopyRightHolderURL: $scope.OtherValue.CopyRightHolderURL,
                            CopyRightHolderAccountNo: $scope.OtherValue.CopyRightHolderAccountNo,
                            CopyRightHolderBankName: $scope.OtherValue.CopyRightHolderBankName,
                            CopyRightHolderBankAddress: $scope.OtherValue.CopyRightHolderBankAddress,
                            CopyRightHolderIFSCCode: $scope.OtherValue.CopyRightHolderIFSCCode,
                            CopyRightHolderPANNo: $scope.OtherValue.CopyRightHolderPANNo,
                            Pincode: $scope.OtherValue.pincode,
                            Country: $scope.OtherValue.Country,
                            State: $scope.OtherValue.State,
                            City: $scope.OtherValue.City,


                            TerritoryRights: $('#TerritoryRights').val() == "" ? null : $('#TerritoryRights').val(),


                            Status: $scope.userForm.Status.$modelValue,
                            AssetSubType: $scope.userForm.AssetSubType.$modelValue,
                            AssetDescription: $scope.userForm.AssetDescription.$modelValue,
                            Extent: $scope.userForm.Extent.$modelValue,
                            Gratiscopytobesent: $scope.userForm.Gratiscopytobesent.$modelValue,
                            Noofcopy: $scope.userForm.Noofcopy.$modelValue,
                            OriginalSource: $scope.userForm.OriginalSource.$modelValue,
                            Restriction: $scope.userForm.Restriction.$modelValue,
                            PermissionsInboundRightsModel: $scope.PermissionsInboundRightsModel,
                            PrintRunGrantedForPrint: $scope.PrintRunGrantedForPrint,
                            NumberPrint: $scope.NumberPrint,
                            SubLicensing: $scope.userForm.SubLicensing.$modelValue,
                            Fee: $scope.userForm.Fee.$modelValue,
                            CurrencyValue: $scope.userForm.Currency.$modelValue,
                            InvoiceNumber: $scope.userForm.InvoiceNumber.$modelValue,
                            InvoiceValue: $scope.userForm.InvoiceValue.$modelValue,
                            PermissionExpirydate: ($('#PermissionExpirydate').val() == "" ? null : convertDateForInsert($('#PermissionExpirydate').val())),
                            DateRequest: $scope.DateRequest,
                            Acknowledgementline: $scope.userForm.Acknowledgementline.$modelValue,
                            InboundRemarks: $scope.userForm.InboundRemarks.$modelValue,
                            InboundOthersId: $scope.InboundOthersId,
                            DateRequestObject: $scope.DateRequestdata,

                            PermissionRightsObject: $scope.PermissionRightsObject,
                            EnteredBy: $("#enterdBy").val(),
                            Code: $('#hid_InboundId').val() == "" ? 0 : $('#hid_InboundId').val(),
                            UpdateRight: $('#hid_User').val(),
                            ImageVedioId: $('#hid_ImageVedio').val(),
                            ImageBankId: $scope.ImageBankId,

                            PendingRemarks: $scope.userForm.PendingRemarks.$modelValue,
                            Documentname: array,
                            DocumentFile: $("#hid_Uploads").val()
                        };




                        ///           debugger;
                        var PermissionInboundObject = AJService.PostDataToAPI('PermissionsInbound/UpdatePendingRequestPermissions', PermissionInboundObject);


                        PermissionInboundObject.then(function (msg) {

                            if (msg.data == "notvalid") {
                                SweetAlert.swal("Try agian", "There is some problem.", "", "error");

                                //SweetAlert.swal("Message", "Invalid  Vendor Name.", "warning");
                            }
                            else if (msg.data == "OK") {
                                SweetAlert.swal({
                                    title: "Updated successfully.",
                                    text: "",
                                    type: "success"
                                },
                               function () {

                                   if (mobj_buttonAttr != 'validationrequiredpendingrequest') {
                                       $window.location.href = $window.location.href;
                                   }
                                   else {

                                       //if ($("#hid_Type").val() == "C") {
                                       //    $window.location.href = "../../PermissionsInbound/PermissionsInbound/ViewInbound/?Id=" + $("#hid_AuthorContract").val() + "&type=C" + $("#hid_ProductId").val() + "&InboundId=" + $('#hid_InboundId').val() + "";
                                       // }
                                       //else if ($("#hid_Type").val() == "L") {
                                       //    $window.location.href = "../../PermissionsInbound/PermissionsInbound/ViewInbound/?Id=" + $("#hid_AuthorContract").val() + "&type=L" + $("#hid_ProductId").val() + "&InboundId=" + $('#hid_InboundId').val() + "";
                                       //}

                                       $window.location.href = "../../PermissionsInbound/PermissionsInbound/ViewInbound/?type=P" + $("#hid_ProductId").val() + "&InboundId=" + $('#hid_InboundId').val() + "";

                                   }

                                   $('.CloseValue').trigger("click");

                               });
                            }
                            else {
                                SweetAlert.swal("Try agian", "There is some problem.", "", "error");
                            }

                        },
                        function () {
                            SweetAlert.swal("", "Please validate details.", "warning");
                            //alert('There is some error in the system');
                        });

                    }


                    //else if ($('#hid_User').val() == "rt") {

                    //    var PermissionInboundObject = {
                    //        Documentname: array,
                    //        DocumentFile: $("#hid_Uploads").val(),
                    //        PendingRemarks: $scope.userForm.PendingRemarks.$modelValue,//$scope.PermissionsInboundModel.PendingRemarks,
                    //        EnteredBy: $("#enterdBy").val(),
                    //        Code: $('#hid_InboundId').val() == "" ? 0 : $('#hid_InboundId').val(),
                    //        UpdateRight: $('#hid_User').val()
                    //    };
                    //    var PermissionInboundObject = AJService.PostDataToAPI('PermissionsInbound/UpdatePendingRequestPermissions', PermissionInboundObject);


                    //    PermissionInboundObject.then(function (msg) {

                    //        if (msg.data != "OK") {
                    //            SweetAlert.swal("Try agian", "There is some problem.", "", "error");
                    //        }
                    //        else {
                    //            SweetAlert.swal({
                    //                title: "Updated successfully.",
                    //                text: "",
                    //                type: "success"
                    //            },
                    //           function () {

                    //               if ($("#hid_Type").val() == "C") {

                    //                   $window.location.href = "../../PermissionsInbound/PermissionsInbound/ViewInbound/?Id=" + $("#hid_AuthorContract").val() + "&type=C" + $("#hid_ProductId").val() + "&InboundId=" + $('#hid_InboundId').val() + "";


                    //               }
                    //               else if ($("#hid_Type").val() == "L") {
                    //                   $window.location.href = "../../PermissionsInbound/PermissionsInbound/ViewInbound/?Id=" + $("#hid_AuthorContract").val() + "&type=L" + $("#hid_ProductId").val() + "&InboundId=" + $('#hid_InboundId').val() + "";


                    //               }


                    //              // location.href = "../../Home/Dashboard/Dashboard";
                    //           });
                    //        }

                    //    },
                    //    function () {
                    //        alert('There is some error in the system');
                    //    });
                    //}
                }

            });

    }
           
    /*****************************************************************************************
    This is the parent function will be used to validating Whole inbound Permission Form
    *****************************************************************************************/
   


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
                if (mobj_buttonAttr == 'validationrequiredpendingrequest') {
                    $scope.HasFile();
                }
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
  
    //function fn_validateImageBank() {
    //    //if ($("select[name*=PartyName]").val() == "") {
    //    //    SweetAlert.swal("Validation", "Please select Party Name", "warning");
    //    //    $("select[name*=PartyName]").focus();
    //    //    return 0;
    //    //}
    //    //if ($("#hid_Uploads1").val() == "") {
    //    //    SweetAlert.swal("Validation", "Please Upload Contract", "warning");
    //    //    $("#dropZone0").css("border-color", "red");
    //    //    $("[id*=btn_Uploader_1]").focus();
    //    //    return 0;
    //    //}
    //    //return 1;

    //}

    /*****************************************************************************************
        This is the parent function will be used to validating Whole inbound Permission Form
    *****************************************************************************************/
    function fn_validateOthers() {
        if ($("#CopyRightHolder1").val() == "" || $("#CopyRightHolder1").val() == undefined) {
            SweetAlert.swal("Validation", "Please select CopyRight Holder  ", "warning");
            //$("[id*=CopyRightHolder]").focus();
            $("[id*=CopyRightHolder1]").focus();
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
  
    //$scope.MinimumValidationRequired = function () {
    //    var obj = $(event.target);

    //    if ($(obj).find("option:selected").text().toLowerCase().indexOf("cleared") > -1) {
    //        $scope.OtherContractStatus = true;
    //        $scope.remarks = false;
    //       
    //    }
    //    else {
    //        
    //        $scope.OtherContractStatus = false;
    //        $scope.remarks = true;

    //    }
    //};


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
    /***********************************************************************************************
        Added by Saddam on 30/08/2016 for Display Assets type
    **************************************************************************************************/

    $scope.AssetTypeViewMode = function (Id) {
        

       
        var AssetTypeViewModeViewStatus = AJService.GetDataFromAPI('PermissionsInbound/GetMultipleAssetTypeDetails?Code=' + Id, null);

        

        AssetTypeViewModeViewStatus.then(function (msg) {
            if (msg.data != "") {

               
                //if ($('#hid_User').val() == "rt")
                //{
                   
                  
                //    for (var i = 0; i < msg.data._permissionInbound.length;i++)
                //    {
                //        mstr_AssetType = mstr_AssetType + "," + msg.data._permissionInbound[i].AssetsType;
                //    }


                //     var mstr_AssetTypeValue =  mstr_AssetType.substring(1)
                
                //     if (mstr_AssetTypeValue != null) {
                //         if (mstr_AssetTypeValue.indexOf("I") > -1) {
                //            $scope.AssetstypeView = 'Image Bank/ Video Bank'
                //        }
                //         else if (msgmstr_AssetTypeValue.indexOf("O") > -1) {
                //            $scope.AssetstypeView = 'Others'
                //        }
                //         else if (msgmstr_AssetTypeValue.indexOf("B") > -1) {
                //            $scope.AssetstypeView = 'Image Bank/ Video Bank And Others'
                //        }
                //    }
                //    else {
                //        $scope.AssetstypeView = "---"
                //    }


                //}
                if ($('#hid_User').val() == "ad" || $('#hid_User').val() == "rt" || $('#hid_User').val() == "sa") {


                    //  setTimeout(function () {


                   

                    if (msg.data._permissionInbound.length > 0) {
                        $scope.ReqAssetstypeImage = false;

                     
                        for (var i = 0; i < msg.data._permissionInbound.length; i++) {

                            if (msg.data._permissionInbound[i].AssetsType == "I") {
                                $('.AssetTypeChk')[0].checked = true;
                                $scope.AddMoreImageVedioData = true;
                              //  $scope.AssetstypeImage[0] = "I";
                                $scope.AddMoreOther = false;

                            }
                            else if (msg.data._permissionInbound[i].AssetsType == "O") {
                                $('.AssetTypeChk')[1].checked = true;
                                $scope.AddMoreOther = true;
                               // AssetstypeImage[1] = "O";

                            }
                            else if (msg.data._permissionInbound[i].AssetsType == "B") {
                                $('.AssetTypeChk')[0].checked = true;
                                $('.AssetTypeChk')[1].checked = true;

                                $scope.AddMoreOther = true;
                                $scope.AddMoreImageVedioData = true;
                                //AssetstypeImage[0] = "I";
                                //AssetstypeImage[1] = "O";

                            }

                        }



                            
                        }
                        else {
                            $scope.ReqAssetstypeImage = true;
                        }
                    //}, 300)
                  

                  
                }

                
            }
            else {
                
               SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
            }
        }, function () {
            //alert('Error in getting copy right holder list which is not used in permission inbound');
        });
    }


    


    /*****************************************************************************************
   This is the parent function will be used to validating Whole inbound Permission Form
   *****************************************************************************************/
    function fn_validPending() {
        if ($('#Signed_Contract_Sent_DateValue').val() == "") {
            SweetAlert.swal("Validation", "Please enter Signed Contract Sent Date", "warning");
            $('#Signed_Contract_Sent_DateValue').focus();
            return 0;
        }
        else if ($('[name=PendingRemarks]').val() == "") {
            SweetAlert.swal("Validation", "Please enter Remarks", "warning");
            $('[name=PendingRemarks]').focus();
            return 0;
        }
    }


    //function fn_validIssue() {
    //    if ($('#DateOfAgreementValue').val() == "") {
    //        SweetAlert.swal("Validation", "Please enter Agreement Date ", "warning");
    //        $('#DateOfAgreementValue').focus();
    //        return 0;
    //    }
    //    else if ($('#EffectiveDate').val() == "") {
    //        SweetAlert.swal("Validation", "Please enter Effective Date ", "warning");
    //        $('#EffectiveDate').focus();
    //        return 0;
    //    }

    //    else if ($('[name=ContractperiodUpload]').val() == "") {
    //        SweetAlert.swal("Validation", "Please enter Contract period (In months)", "warning");
    //        $('[name=ContractperiodUpload]').focus();
    //        return 0;
    //    }

    //    else if ($('#Signed_Contract_Sent_DateValue').val() == "") {
    //        SweetAlert.swal("Validation", "Please enter Signed Contract Sent Date", "warning");
    //        $('#Signed_Contract_Sent_DateValue').focus();
    //        return 0;
    //    }

    //    else if ($('#Signed_Received_Sent_DateValue').val() == "") {
    //        SweetAlert.swal("Validation", "Please enter Signed Contract Received Date", "warning");
    //        $('#Signed_Received_Sent_DateValue').focus();
    //        return 0;
    //    }
    //}

    //function fn_validCancel() {
    //    if ($('#Cancellation_DateValue').val() == "") {
    //        SweetAlert.swal("Validation", "Please enter Cancellation Date", "warning");
    //        $('#Cancellation_DateValue').focus();
    //        return 0;
    //    }
    //    else if ($('[name=Cancellation_Reason]').val() == "") {
    //        SweetAlert.swal("Validation", "Please enter Cancellation Reason", "warning");
    //        $('[name=Cancellation_Reason]').focus();
    //        return 0;
    //    }
    //}


    //$scope.HasFile = function () {

    //    errorDiv = document.getElementById("fileid");
    //    errorDiv.innerHTML = "";
    //    errormsg = "";


    //    var errorDiv;
    //    var errormsg = '';
    //    $scope.msg = "";
    //    FileNameArray = [];
    //    FileNameArray = $('#dropZone0').find('.fileNameClass');
    //    var array = [];

    //    if (FileNameArray.length == 0) {
    //        errorDiv = document.getElementById("fileid");
    //        errorDiv.innerHTML = "Please select a file";
    //        errormsg = "Please select a file";
    //        $scope.userForm.$valid = false;
    //    }
    //    else {
    //        FileNameArray.each(function () {
    //            array.push(
    //           $(this).val());

    //            for (i = 0; i < array.length; i++) {
    //                if (array[i] == "") {
    //                    errorDiv = document.getElementById("fileid");
    //                    errorDiv.innerHTML = "Please enter file name";
    //                    errormsg = "Please enter file name";
    //                    $scope.userForm.$valid = false;
    //                    return 0;
    //                }
    //                else {
    //                    $scope.userForm.$valid = true;
    //                }
    //            }
    //        });
    //    }

    //}

    /*****************************************************************************************
    This is the parent Pending Request for Permissions Inbound Form
    *****************************************************************************************/
    //function fn_valiPandingRequest(obj) {


    //    if ($(".Contractstatus:checked").length == 0) {
    //        SweetAlert.swal("Validation", "Please select Contract Status", "warning");
    //        $($(".Contractstatus")[0]).focus();
    //        return 0;
    //    }
    //    else {
           
    //        if ($("input[name='Contractstatus']:checked").val() == "Issued") {
    //            $scope.HasFile();
    //           // return fn_validIssue();
    //            //return 
    //        }
         
    //    }

    //}



    function fn_validateImageBank() {
        //if ($("select[name*=PartyName]").val() == "") {
        //   SweetAlert.swal("Validation", "Please select Party Name", "warning");
        //    $("select[name*=PartyName]").focus();
        //    return 0;
        //}
        //if ($("#hid_Uploads1").val() == "") {
        //    SweetAlert.swal("Validation", "Please Upload Contract", "warning");
        //    $("#dropZone0").css("border-color", "red");
        //    $("[id*=btn_Uploader_1]").focus();
        //    return 0;
        //}
        //return 1;

    }


    function fn_validateForm(obj) {

     

        if ($(".AssetTypeChk:checked").length == 0) {
            SweetAlert.swal("Validation", "Please select Assets type", "warning");
            $($(".AssetTypeChk")[0]).focus();
            return 0;
        }
        else {
            $scope.ReqAssetstypeImage = false;

            
                if ($("input[type=checkbox][name*=AssetstypeImage]").is(":checked")) {
                    return fn_validateImageBank();
                }
            

                //debugger;
                if ($("input[type=checkbox][name*=AssetstypeOthers]").is(":checked")) {

                    if ($('[name=Check_OtherUpdate]').is(":checked") == true || $($('.clickData')[0]).attr("style") == "display: block;")
                    {
                        return fn_validateOthers();
                    }

                 
                }
           
            
            if ($(".AssetTypeChk:checked").length == 2) {
                return fn_bothInboundProcess();
            }
        }

    }



    $scope.ContractstatusReq = true;

    $scope.PermissionsInboundForm = function (PermissionsInboundModel) {
        mobj_buttonData = $("input[type=submit][clicked=true]").prevObject.context.activeElement;
        mobj_buttonAttr = $(mobj_buttonData).data('index');
       
        //if ($('#hid_User').val() == "rt") {
        //    if (mobj_buttonAttr == 'validationrequiredpendingrequest') {
        //        $scope.HasFile();
        //    }
        //}
        //else
        if ($('#hid_User').val() == "ad" || $('#hid_User').val() == "rt" || $('#hid_User').val() == "sa")
        {
         
            //if (mobj_buttonAttr == 'validationrequiredpendingrequest') {
            //    if ($('[name=PendingRemarks]').val() == "") {
            //        SweetAlert.swal("Validation", "Please enter Pending Request Remarks", "warning");
            //        $('.CloseValue').trigger("click")
            //        $('[name=PendingRemarks]').focus();
            //        $scope.PendingRemarkRrequired = true;
            //        return 0;
            //    }
            //    else {
            //        $scope.PendingRemarkRrequired = false;
            //    }
            //}

            if (fn_validateForm(PermissionsInboundModel) == 0) {
              
                return false;
             
             }

            if ($('#hid_UploadFileReq').val() == "")
            {
                if (mobj_buttonAttr == 'validationrequiredpendingrequest') {
                    $scope.HasFile();
                }
                    
            }
            //debugger;
            //if ($scope.OtherValue.hid_ValidateReqOther == "InsertReq") {
            //    $scope.AssetstypeImage[1] = true;
            //    return fn_validateOthers();
            //}
            //else {
            //    $scope.AssetstypeImage[1] = false;
            //}
       

        }


      
        if ($scope.userForm.PartyName.$modelValue != null && $scope.userForm.PartyName.$modelValue != "" && $scope.userForm.PartyName.$modelValue != undefined) {
            $scope.userForm.$valid = true;
        }
      
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



    $scope.func_DisplayImageVedioData = function ()
    {
       
        if ($('.CheckImageVedio').is(":Checked")) {
            $scope.AddMoreImageVedioData = true;
            $scope.AssetstypeImageReq = true;

            if ($scope.PermissionInboundImageVideoBankDataList != null)
            {
                $scope.AddMoreImageVedioList = true;
            }
           
        }
        else {
            $scope.AddMoreImageVedioData = false;
            $scope.AssetstypeImageReq = false;
            $scope.AddMoreImageVedioList = false;
        }

      
    }
    $scope.func_DisplayOtherData = function ()
    {

        if ($('.CheckOther').is(":Checked")) {
            $scope.PermissionInboundOthersView = true;
            $scope.AddMoreOther = true;
        }
        else {
            $scope.PermissionInboundOthersView = false;
            $scope.AddMoreOther = false;
        }
    }


  
   
    if ($('#hid_InboundId').val() != "") {
        //if ($('#hid_User').val() == "rt") {

        //    $scope.InsertInBoundPermission = false;
        //    $scope.ViewInBoundDetail = true;

        //    $scope.AssetTypeViewMode($('#hid_InboundId').val());

        //    app.expandControllerImageVideoBankViewDetails($scope, AJService, $window);
        //  //  $scope.ImageVideoBankViewMode($('#hid_InboundId').val());

        //   $scope.ImageVideoBankViewModeAll($('#hid_InboundId').val());
            
        //    app.expandControllerOthersViewDetails($scope, AJService, $window);
        //    //$scope.OthersViewViewMode($('#hid_InboundId').val());

        //    app.expandControllerPendingRequestInsertDetails($scope, AJService, $window);
        // //  $scope.GetPermissionInboundUpdateList($('#hid_InboundId').val());
        //    app.expandControllerViewAssetDetails($scope, AJService, $window);

         
        //}

        if ($('#hid_User').val() == "ad" || $('#hid_User').val() == "rt" || $('#hid_User').val() == "sa") {
            $scope.AssetTypeViewMode($('#hid_InboundId').val());
          //  app.expandControllerImageVideoBankUpdateDetails($scope, AJService, $window);
           // $scope.ImageVideoBankUpdateMode($('#hid_InboundId').val());
            app.expandControllerImageVideoBankViewDetails($scope, AJService, $window, SweetAlert);

            $scope.ImageVideoBankViewModeAll($('#hid_InboundId').val());

            app.expandControllerImageVideoUpdateDetails($scope, AJService, $window);
            app.expandControllerOthersViewDetails($scope, AJService, $window);
            //$scope.OthersViewViewMode($('#hid_InboundId').val());
            app.expandControllerCopyRightsUpdateDetails($scope, AJService, $window);
            // app.expandControllerCopyRightsInsertDetails($scope, AJService, $window);
                        
            // $scope.getInboundProcessDetails($('#hid_InboundId').val());
            app.expandControllerPendingRequestInsertDetails($scope, AJService, $window, SweetAlert);
            $scope.GetPermissionInboundUpdateList($('#hid_InboundId').val());

            setTimeout(function () { $('#PenddingRemarksViewValue').css("display", "none"); }, 200)
           
        }
    }

    $scope.keyChange = function () {
        $scope.PendingRemarkRrequired = false;
    }

    
});



