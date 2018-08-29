app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {

    /*Expand common.master.js Controller*/
    //app.expandControllerA($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);


    $scope.LicenseValueId = null;
    $scope.PublishingCompanyId = null;
    $scope.AuthorContractValueId = null;
    $scope.AuthorId = null;

    $scope.IsInvoice = true;


    $scope.AuthorContractValueData = false;

    //------show hide fields according to checkbox
    $scope.func_AuthorContractCodeChk = function () {
        $scope.ContractCodeOrLicenseCodeChk = 'AuthorContractCode';
        $scope.ReqAuthorContractCode = true;
        $scope.ReqLicenseCode = false;
    }

    $scope.func_ProductLicenseCodeChk = function () {
        $scope.ContractCodeOrLicenseCodeChk = 'ProductLicenseCode';
        $scope.ReqLicenseCode = true;
        $scope.ReqAuthorContractCode = false;
    }
    //------End show hide fields according to checkbox



    //------ Back function for returm on Search form
    $scope.func_GoBack = function () {
        var For = $('#hid_For').val();
        if (For == 'rights') {
            window.location.href = "../../Report/Report/StatementSearch?For=Rights&Type=Report";
        }
        else {
            window.location.href = "../../Report/Report/StatementSearch?For=PermissionsOutbound&Type=Report";
        }
    }



    //------ Search form Submit
    $scope.AuthorPublisherStatementSearchForm = function () {
        $scope.submitted = true;

        //if ($scope.FromYear != '' || $scope.FromYear != null || $scope.FromYear != undefined) {
        //    if ($scope.FromMonth == '' || $scope.FromMonth != null || $scope.FromMonth != undefined) {
        //        $scope.userForm.$valid = false;
        //    }
        //}
        //if ($scope.FromMonth != '' || $scope.FromMonth != null || $scope.FromMonth != undefined) {

        //}

        if ($scope.userForm.$valid) {
            $scope.AuthorPublisherStatementSearch();
            // set form default state
            $scope.userForm.$setPristine();
            // set form is no submitted
            $scope.submitted = false;
            return;
        }
    }


    $.extend({
        distinct: function (anArray) {
            var result = [];
            $.each(anArray, function (i, v) {
                if ($.inArray(v, result) == -1) result.push(v);
            });
            return result;
        }
    });
    //------ Search & Get list of Author / Publisher Statement
    $scope.AuthorPublisherStatementSearch = function () {
        $('#hid_ChecboxType').val($scope.ContractCodeOrLicenseCodeChk);

        var For = $('#hid_For').val();
        if (For == 'rights') {

            var _mobjAuthorPubSt = {
                //Year: $scope.Year,
                //Month: $scope.Month,
                EntryFromYear: $scope.EntryFromYear,
                EntryFromMonth: $scope.EntryFromMonth,
                EntryToYear: $scope.EntryToYear,
                EntryToMonth: $scope.EntryToMonth,

                FromYear: $scope.FromYear,
                FromMonth: $scope.FromMonth,
                ToYear: $scope.ToYear,
                ToMonth: $scope.ToMonth,

                Type: $scope.ContractCodeOrLicenseCodeChk,

                AuthorContractCode: $scope.AuthorContractCode,
                AuthorCode: $scope.AuthorCode,
                AuthorName: $scope.AuthorName,

                ProductLicenseCode: $scope.ProductLicenseCode,
                PublishingCompanyCode: $scope.PublishingCompanyCode,
                PublishingCompanyName: $scope.PublishingCompanyName
            }

            var ExecutiveStatus = AJService.PostDataToAPI('Report/GetRightsStatementList', _mobjAuthorPubSt);
            ExecutiveStatus.then(function (msg) {
                if (msg.data.length != 0) {
                    $scope.AuthorPublisherStatement_List = msg.data;

                    //for list Heading
                    if (msg.data[0].AuthorContractCode != null)
                        $scope.headingStatement = "Author Statement List - Rights";
                    else
                        $scope.headingStatement = "Publisher Statement List - Rights";

                    $('#AuthorPublisherStatement_Search').hide();
                    $('#AuthorPublisherStatement_List').show();
                      $('#backToSearch').show();
                    
                    //$('#backToSearch').css("display", "block");
                }
                else {
                    SweetAlert.swal("No record", 'No record found', "warning");
                }
            });
        }
        else if (For == 'permissionsoutbound') {

            var _mobjAuthorPubSt = {
                //Year: $scope.Year,
                //Month: $scope.Month,
                EntryFromYear: $scope.EntryFromYear,
                EntryFromMonth: $scope.EntryFromMonth,
                EntryToYear: $scope.EntryToYear,
                EntryToMonth: $scope.EntryToMonth,

                FromYear: $scope.FromYear,
                FromMonth: $scope.FromMonth,
                ToYear: $scope.ToYear,
                ToMonth: $scope.ToMonth,

                Type: $scope.ContractCodeOrLicenseCodeChk,

                AuthorContractCode: $scope.AuthorContractCode,
                AuthorCode: $scope.AuthorCode,
                AuthorName: $scope.AuthorName,

                ProductLicenseCode: $scope.ProductLicenseCode,
                PublishingCompanyCode: $scope.PublishingCompanyCode,
                PublishingCompanyName: $scope.PublishingCompanyName
            }

            var ExecutiveStatus = AJService.PostDataToAPI('Report/GetPermissionsOutboundStatementList', _mobjAuthorPubSt);
            ExecutiveStatus.then(function (msg) {
                if (msg.data.length != 0) {
                    $scope.AuthorPublisherStatement_List = msg.data;
                   
                    //for list Heading
                    if (msg.data[0].AuthorContractCode != null)
                        $scope.headingStatement = "Author Statement List - Permissions Outbound";
                    else
                        $scope.headingStatement = "Publisher Statement List - Permissions Outbound";

                    $('#AuthorPublisherStatement_Search').hide();
                    $('#AuthorPublisherStatement_List').show();
                      $('#backToSearch').show();

                 //   $('#backToSearch').css("display","block");
                }
                else {
                    SweetAlert.swal("No record", 'No record found', "warning");
                }
            });
        }
        else {
            SweetAlert.swal("Try agian", "There is some problem.", "", "error");
        }
    }
    //------ End Search & Get list of Author / Publisher Statement

 

    $scope.RightsStatement_Detail = false;
    $scope.GetRightsStatementDetail = function (_mobjAuthorPubSt) {

        var ExecutiveStatus = AJService.PostDataToAPI('Report/GetRightsStatementDetail', _mobjAuthorPubSt);
        ExecutiveStatus.then(function (msg) {
            if (msg.data.length != 0) {
              
                $scope.IsInvoice = false;

                $scope.AuthorPublisherStatement_Detail = msg.data;
                $scope.RightsStatement_Detail = true;

                //for get total amount
                var amtTotal = 0;
                var author_amtTotal = 0;
                var oup_amtTotal = 0;
                for (i = 0; i < msg.data.length; i++) {
                    //amtTotal += msg.data[i].Amount;
                    author_amtTotal += msg.data[i].AuthorAmount;
                    oup_amtTotal += msg.data[i].OupAmount;
                    $scope.RecpetAmtValue.push(msg.data[i].Amount);
                }


                if ($scope.RecpetAmtValue.length > 0) {
                  
                    for (i = 0; i < $.distinct($scope.RecpetAmtValue).length; i++) {
                        amtTotal += $.distinct($scope.RecpetAmtValue)[i];
                       
                    }
                }
               
               
                $scope.totalAmount = amtTotal;
                $scope.totalAuthorAmount = author_amtTotal.toFixed(2);


             // $scope.totalOupAmount = oup_amtTotal.toFixed(2);
                $scope.totalOupAmount = (amtTotal - author_amtTotal.toFixed(2)).toFixed(2);
                //$scope.totalOupAmount = msg.data[0].OupAmount.toFixed(2);
                //-------------------------------------

                //for detail list Heading
                if (msg.data[0].AuthorContractId != null) {
                    $scope.topheadingStatement = "Author Statement";
                    $scope.headingStatement = " - Rights";
                }
                else {
                    $scope.topheadingStatement = "Publisher Statement";
                    $scope.headingStatement = " - Rights";
                }

            }
            else {
                SweetAlert.swal("No record", 'No record found', "warning");
            }
        });
    }

    $scope.RecpetAmtValue = [];

    $scope.PermissionsOutboundStatement_Detail = false;
    $scope.GetPermissionsOutboundStatementDetail = function (_mobjAuthorPubSt) {

        var ExecutiveStatus = AJService.PostDataToAPI('Report/GetPermissionsOutboundStatementDetail', _mobjAuthorPubSt);
        ExecutiveStatus.then(function (msg) {
            if (msg.data.length != 0) {

                $scope.IsInvoice = true;

                $scope.AuthorPublisherStatement_Detail = msg.data;
                $scope.PermissionsOutboundStatement_Detail = true;
               
                //for get total amount
                var amtTotal = 0;
                var author_amtTotal = 0;
                var oup_amtTotal = 0;
                for (i = 0; i < msg.data.length; i++) {
                    // amtTotal += msg.data[i].Amount;
                  
                    author_amtTotal += msg.data[i].AuthorAmount;
                    oup_amtTotal += msg.data[i].OupAmount;
                    $scope.RecpetAmtValue.push(msg.data[i].Amount);
                }


                if ($scope.RecpetAmtValue.length > 0) {
                 
                    for (i = 0; i < $.distinct($scope.RecpetAmtValue).length; i++) {
                        amtTotal += $.distinct($scope.RecpetAmtValue)[i];

                    }
                }


                $scope.totalAmount = amtTotal;
                $scope.totalAuthorAmount = author_amtTotal.toFixed(2);
               
                $scope.totalOupAmount = (amtTotal - author_amtTotal.toFixed(2)).toFixed(2);
                //$scope.totalOupAmount = oup_amtTotal.toFixed(2);
                //-------------------------------------

                //for detail list Heading
                if (msg.data[0].AuthorContractId != null) {
                    $scope.topheadingStatement = "Author Statement";
                    $scope.headingStatement = " - Permissions Outbound";
                }
                else {
                    $scope.topheadingStatement = "Publisher Statement";
                    $scope.headingStatement = " - Permissions Outbound";
                }

            }
            else {
                SweetAlert.swal("No record", 'No record found', "warning");
            }
        });
    }
    //------ End Get detail list for StatementView page


    //------ Get detail list for StatementView page
    $scope.AuthorStatement = function () {

        
        var _mobjAuthorPubSt = {
            ContractId: $scope.AuthorContractValueId,
            AuthorId: $scope.AuthorId
        }
        //debugger;

        var For = $('#hid_For').val();
        if (For == 'rights') {
            $scope.AuthorContractValueData = true;
            $scope.GetRightsStatementDetail(_mobjAuthorPubSt);
        }
        else {
            $scope.AuthorContractValueData = true;
            $scope.GetPermissionsOutboundStatementDetail(_mobjAuthorPubSt);
        }
    }

    $scope.PublisherStatement = function () {



        var _mobjAuthorPubSt = {
            LicenseId: $scope.LicenseValueId,
            PublishingCompanyId: $scope.PublishingCompanyId
        }
        // debugger;

        var For = $('#hid_For').val();
        if (For == 'rights') {
            $scope.GetRightsStatementDetail(_mobjAuthorPubSt);
        }
        else {
            $scope.GetPermissionsOutboundStatementDetail(_mobjAuthorPubSt);
        }
    }



    if ($('#hid_LicenseId').val() != "" && $('#hid_LicenseId').val() != undefined) {
        if ($('#hid_LicenseId').val().indexOf(",") > -1) {
            var res = $('#hid_LicenseId').val().split(",");
            var array = [];
            array = array.concat(res);

            for (var i = 0; i < array.length; i++) {
                if (array[i].split("_")[0] == "L")
                    $scope.LicenseValueId = array[i].split("_")[1];

                if (array[i].split("_")[0] == "P")
                    $scope.PublishingCompanyId = array[i].split("_")[1];


            }
        }
        else {

            if ($('#hid_LicenseId').val().split("_")[0] == "L")
                $scope.LicenseValueId = $('#hid_LicenseId').val().split("_")[1];

            if ($('#hid_LicenseId').val().split("_")[0] == "P")
                $scope.PublishingCompanyId = $('#hid_LicenseId').val().split("_")[1];

        }

        $scope.PublisherStatement();
    }

    if ($('#hid_contractid').val() != "" && $('#hid_contractid').val() != undefined) {

        if ($('#hid_contractid').val().indexOf(",") > -1) {
            var res = $('#hid_contractid').val().split(",");
            var array = [];
            array = array.concat(res);

            for (var i = 0; i < array.length; i++) {
                if (array[i].split("_")[0] == "C")
                    $scope.AuthorContractValueId = array[i].split("_")[1];

                if (array[i].split("_")[0] == "A")
                    $scope.AuthorId = array[i].split("_")[1];


            }
        }
        else {

            if ($('#hid_contractid').val().split("_")[0] == "C")
                $scope.AuthorContractValueId = $('#hid_contractid').val().split("_")[1];

            if ($('#hid_contractid').val().split("_")[0] == "A")
                $scope.AuthorId = $('#hid_contractid').val().split("_")[1];

        }
        
        $scope.AuthorStatement();
    }


    //Export Excel of List
    $scope.StatementSearchReportExcel = function () {
        $scope.ExcelReport();
    }

    $scope.ExcelReport = function () {
        //document.location = GlobalredirectPath + "Report/Report/exportToExcelStatementSearchReportList?Year=" + $scope.Year + "&Month=" + $scope.Month + "&AuthorContractCode=" + $scope.AuthorContractCode + "&AuthorCode=" + $scope.AuthorCode + "&AuthorName=" + $scope.AuthorName + "&ProductLicenseCode=" + $scope.ProductLicenseCode + "&PublishingCompanyCode=" + $scope.PublishingCompanyCode + "&PublishingCompanyName=" + $scope.PublishingCompanyName + "";

        //changed by Prakash
        var For = $('#hid_For').val();

        document.location = GlobalredirectPath + "Report/Report/exportToExcelStatementSearchReportList"
                                                + "?FromYear=" + $scope.FromYear
                                                + "&FromMonth=" + $scope.FromMonth
                                                + "&ToYear=" + $scope.ToYear
                                                + "&ToMonth=" + $scope.ToMonth

                                                + "&Type=" + $('#hid_ChecboxType').val()

                                                + "&AuthorContractCode=" + $scope.AuthorContractCode
                                                + "&AuthorCode=" + $scope.AuthorCode
                                                + "&AuthorName=" + $scope.AuthorName

                                                + "&ProductLicenseCode=" + $scope.ProductLicenseCode
                                                + "&PublishingCompanyCode=" + $scope.PublishingCompanyCode
                                                + "&PublishingCompanyName=" + $scope.PublishingCompanyName
                                                + "&For=" + For + "";

    }
    //End Export Excel of List

    //Export Excel of Details
    $scope.StatementSearchReportExcelDetails = function () {
        $scope.ExcelReportDetails();
    }

    $scope.ExcelReportDetails = function () {
        var For = $('#hid_For').val();

        document.location = GlobalredirectPath + "Report/Report/exportToExcelStatementSearchReportDetails"
                                                + "?FromYear=" + $scope.FromYear
                                                + "&FromMonth=" + $scope.FromMonth
                                                + "&ToYear=" + $scope.ToYear
                                                + "&ToMonth=" + $scope.ToMonth

                                                + "&Type=" + $('#hid_ChecboxType').val()

                                                + "&AuthorContractCode=" + $scope.AuthorContractCode
                                                + "&AuthorCode=" + $scope.AuthorCode
                                                + "&AuthorName=" + $scope.AuthorName

                                                + "&ProductLicenseCode=" + $scope.ProductLicenseCode
                                                + "&PublishingCompanyCode=" + $scope.PublishingCompanyCode
                                                + "&PublishingCompanyName=" + $scope.PublishingCompanyName
                                                + "&For=" + For + "";

    }
    //End Export Excel of Details


});


