app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {

    /*Expand common.master.js Controller*/
    app.expandControllerA($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);
    $scope.PermissionsInboundForm = function (PermissionsInboundModel) {
        //validate the form before insert
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
    $scope.OtherContractStatus = true;
    function fn_validateImageBank() {
        if ($("select[name*=PartyName]").val() == "") {
            SweetAlert.swal("Validation", "Please select Vendor Name", "warning");
            $("select[name*=PartyName]").focus();
            return 0;
        }
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
            $(obj).find("option:selected").text().toLowerCase().indexOf("no trace") > -1) {
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

        if ($scope.PermissionsInboundModel.CopyRightHolder == undefined) {
            $scope.ContactPerson = undefined;
            $scope.Mobile = undefined;
            $scope.CopyRightHolderAddress = undefined;
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
        if ($scope.ChkParent) {
            for (var i = 0; i < $scope.newCpyHlderList.length; i++) {
                $scope.CpyHlderList.push($scope.newCpyHlderList[i].Id);
            }

        }
        else {
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



    $scope.ShowHideAssetBox = function () {
        var obj = $(event.target);
        if (obj.is(":checked")) {
            $(".imageBankSlide").slideDown("slow");
        }
        else {
            $(".imageBankSlide").slideUp("slow");
        }
    };
    $scope.OthersSlideUpDown = function () {
        var obj = $(event.target);
        if (obj.is(":checked")) {
            $(".otherSlide").slideDown("slow");
        }
        else {
            $(".otherSlide").slideUp("slow");
        }
    };

    $scope.ShowExtraSearch = function (val) {

        if (val == 1) {
            $(".otherSlideAfterStatus").slideDown("slow");
        }
        else {
            $(".otherSlideAfterStatus").slideUp("slow");
        }
    };
    $scope.DateRequest = [];
    $scope.dateRequestList = ["1st", "2nd", "3rd", "4th"];
    $scope.DateRequestdata = [];
    $scope.SetDateValue = function (obj) {
        $scope.DateRequest[$(obj).attr("index")] = $(obj).val();
    };
    $scope.FillExpiry = function (obj) {
        $scope.PermissionsInboundModel.PermissionExpirydate = $(obj).val() == "" ? null : convertDate($(obj).val());
    };

    /*****************************************************************************************************************************
       This function will collect all the searching parameter and store them into the search history table based on session id
    *****************************************************************************************************************************/
    $scope.PermissionsInboundModel;
    $scope.Fn_SearchInboundPermission = function (PermissionsInboundModel) {
        $scope.submitted = false;
        $scope.DateRequestdata = [];
        for (var i = 0, j = 0; i < $scope.dateRequestList.length; i++) {
            if ($scope.DateRequest[i] != undefined && $scope.DateRequest[i] != "") {
                $scope.DateRequestdata[j] = $scope.dateRequestList[i] + "#" + convertDate($scope.DateRequest[i]);
                j++;
            }
        }

        var mstr_AssetsType = null;
        if ($(".AssetTypeChk:checked").length != 0)
        {
            mstr_AssetsType =   $(".AssetTypeChk:checked").length == 2 || $(".AssetTypeChk:checked").length == 0 ? "B" : $(".AssetTypeChk:checked").val();
        }


        PermissionsInboundModel.Assetstype = mstr_AssetsType;
        PermissionsInboundModel.DateRequest = $scope.DateRequestdata.join(",");
        PermissionsInboundModel.SessionId = $("#hid_sessionId").val();
        PermissionsInboundModel.EnteredBy = $("input[type=hidden][id*=enterdBy]").val();
        PermissionsInboundModel.InboundPermissionCode = PermissionsInboundModel.InboundPermissionCode == "" ? null : PermissionsInboundModel.InboundPermissionCode;
        PermissionsInboundModel.ProductCode = PermissionsInboundModel.ProductCode == "" ? null : PermissionsInboundModel.ProductCode;
        PermissionsInboundModel.AuthorContractCode = PermissionsInboundModel.AuthorContractCode == "" ? null : PermissionsInboundModel.AuthorContractCode;
        PermissionsInboundModel.LicenseCode = PermissionsInboundModel.LicenseCode == "" ? null : PermissionsInboundModel.LicenseCode;


        PermissionsInboundModel.ProductName = PermissionsInboundModel.ProductName == "" ? null : PermissionsInboundModel.ProductName;
        PermissionsInboundModel.SubProductName = PermissionsInboundModel.SubProductName == "" ? null : PermissionsInboundModel.SubProductName;

        PermissionsInboundModel.AuthorName = PermissionsInboundModel.AuthorName == "" ? null : PermissionsInboundModel.AuthorName;

        PermissionsInboundModel.ISBN = PermissionsInboundModel.ISBN == "" ? null : PermissionsInboundModel.ISBN;



        if (mstr_AssetsType == "B") {
            PermissionsInboundModel.Flag = $('#hid_AddCopyrightHolder').val() == "CopyrightHolder" ? "B" : null;
        }
        else {
            PermissionsInboundModel.Flag = $('#hid_AddCopyrightHolder').val() == "CopyrightHolder" ? "O" : null;
        }

      

        var PermissionInboundObjectList = AJService.PostDataToAPI('PermissionsInbound/InsertIntoSearchHistory', PermissionsInboundModel);
        PermissionInboundObjectList.then(function (msg) {
            if (msg.data != "OK") {
                SweetAlert.swal('Error', 'Try again', "error");
            }
            else {
                $scope.FetchSearchResult($("#hid_sessionId").val());
                $scope.submitted = false;
                return;
            }

        },
        function () {
            alert('There is some error in the system');
        });
    }

    /*********************************************************************************************
        This function will fetch the Search Result based on session id and display the listing
    ************************************************************************************************/
    $scope.FetchSearchResult = function (SessionId) {
        var ResultList = AJService.GetDataFromAPI("PermissionsInbound/getInboundPermissionSearchResult?SessionId=" + SessionId, null);
        ResultList.then(function (msg) {
          if (msg.data.length != 0) {
              $scope.ResultList = msg.data;
              $(".InboundList").fadeIn("fast");
              $(".inboundSearch").fadeOut("fast");              
            }
            else {
              swal("No record", 'No record found', "warning");
              document.location = GlobalredirectPath + "PermissionsInbound/PermissionsInbound/search?Type=View";
            }
        });
    }


    $scope.DisplayDiv = function (count) {
        $('.tooltip-toggle').css("display", "none");
        $('.tooltip-toggle').removeAttr("id");
        $($('.tooltip-toggle')[count - 1]).attr("id", "tooltip-toggle");
    }
    $scope.BackToserch = function () {
        //$(".InboundList").fadeOut("fast");
        //$(".inboundSearch").fadeIn("fast");
        //window.location.href = window.location.href;

        //var url = window.location.href;
        //var q_string = "BackToSearch";

        //if (url.indexOf(q_string) != -1) {
        //    var data = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        //    window.location.href = "search?" + data[0];
        //}
        //else
        //    window.location.href = window.location.href;

        if ($('#hid_Action').val().trim().toLowerCase() == "dashboard") {
            window.location.href = GlobalredirectPath + 'Home/Dashboard/Dashboard';
        }
        else {
            var mstr_history = document.referrer;

            if (mstr_history.indexOf("UpdateInbound") > 0) {
                window.location.href = "search?Type=View";
            }
            else if (mstr_history.indexOf("ViewInbound") > 0) {
                window.location.href = "search?Type=Update";

            }
            else {
                window.location.href = window.location.href;
            }
        }

    };

    if ($('#hid_BackToSearch').val() != "") {
        $scope.FetchSearchResult($("#hid_sessionId").val());
    }


    $scope.ExcelReport = function () {
        document.location = GlobalredirectPath + "PermissionsInbound/PermissionsInbound/exportToExcelPermissionsInboundList?SessionId=" + $("#hid_sessionId").val() + "";
    }

    $scope.PermissionsInboundReportExcel = function () {
        $scope.ExcelReport();
    }

    $scope.PermissionsInboundLessQuantityReport = function () {
        document.location = GlobalredirectPath + "PermissionsInbound/PermissionsInbound/exportToExcelPermissionsInboundLessQuantityList";
    }


    //For Delete Permission Inbound // Added by Prakash on 05 May, 2017
    $scope.DeletePermissionInbound = function (PICode, role) {
        var mobj_delete = {
            Code: PICode == undefined ? null : PICode,
            //Role: role == undefined ? null : role,
            DeactivateBy: $("#enterdBy").val(),
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

                var Delete = AJService.PostDataToAPI("PermissionsInbound/DeletePermissionInboundSet", mobj_delete);
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
                                $scope.FetchSearchResult($("#hid_sessionId").val());
                            }
                        });
                    }
                });


            }

        });

    }


    if($('#hid_Action').val() != "" && $('#hid_Action').val() != null && $('#hid_Action').val() != undefined){
        if ($('#hid_Action').val().trim().toLowerCase() == "dashboard") {
            var DashboardLessData = $('#hid_AddDasData').val();
            if (DashboardLessData != null && DashboardLessData != "" && DashboardLessData != undefined && DashboardLessData.toLowerCase() == "less") {
                var ResultList = AJService.GetDataFromAPI("PermissionsInbound/getInboundPermissionSearchResultLess?Data=" + DashboardLessData + "&ExecutiveId=" + parseInt($("#enterdBy").val()), null);
                ResultList.then(function (msg) {
                    if (msg.data.length != 0) {
                        $scope.ResultList = msg.data;
                        $(".InboundList").fadeIn("fast");
                        $(".inboundSearch").fadeOut("fast");
                    }
                    else {
                        swal("No record", 'No record found', "warning");
                        document.location = GlobalredirectPath + "PermissionsInbound/PermissionsInbound/search?Type=View";
                    }
                });
            }
            else {
                $scope.FetchSearchResult($("#hid_sessionId").val());
                $('#btn_backtosearch').hide();
                $('#btn_backtodashboard').show();
            }
        }
    }


});