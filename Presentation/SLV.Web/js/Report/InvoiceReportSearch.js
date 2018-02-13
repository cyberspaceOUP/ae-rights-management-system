app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {

    /*Expand common.master.js Controller*/
    //app.expandControllerA($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);

    //------ Back function for returm on Search form
    $scope.func_GoBack = function () {

        window.location.href = "../../Report/Report/InvoiceReport";

    }

    $scope.Title = "Invoice Report Search";

    $("#InvoiceTodate").attr("disabled", "disabled");

    //------ Search form Submit
    $scope.InvoiceReportSearchForm = function () {
        $scope.submitted = true;

        if ($scope.userForm.$valid) {
            $scope.InvoiceReportSearch();
            // set form default state
            $scope.userForm.$setPristine();
            // set form is no submitted
            $scope.submitted = false;
            return;
        }
    }

    function convertDate(dateVal) {

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
            return mm + "/" + dd + "/" + yy;
        }
    }

    //------ Search & Get list of Author / Publisher Statement
    $scope.InvoiceReportSearch = function () {

        //var For = $('#hid_For').val();
        //if (For == 'rights') {

        //var mstr_InvoiceFromdate = $('#InvoiceFromdate').val() == "" ? null : $('#InvoiceFromdate').val();
        //var mstr_InvoiceTodate = $('#InvoiceTodate').val() == "" ? null : $('#InvoiceTodate').val();

        var mstr_InvoiceFromdate = $('#InvoiceFromdate').val();
        var mstr_InvoiceTodate = $('#InvoiceTodate').val();
        if (mstr_InvoiceFromdate == "")
        {
            mstr_InvoiceFromdate = null
        }
        if (mstr_InvoiceTodate == "") {
            mstr_InvoiceTodate = null
        }
        //alert(convertDate(mstr_InvoiceFromdate));
      
        var _mobjAuthorPubSt = {
            InvoiceFromDate: mstr_InvoiceFromdate == null ? null : convertDate(mstr_InvoiceFromdate),
            InvoiceToDate:  mstr_InvoiceTodate == null ? null : convertDate(mstr_InvoiceTodate),
            InvoiceNo: $scope.InvoiceNo,
            InvoiceValue: $scope.InvoiceValue,
            InvoiceStatus: $scope.InvoiceStatus,
            LicenseeName: $scope.LicenseeName,
            Country: $scope.Country,
            State: $scope.State,
            City: $scope.City
        }
        
        var ExecutiveStatus = AJService.PostDataToAPI('Report/GetInvoiceReportList', _mobjAuthorPubSt);
        ExecutiveStatus.then(function (msg) {

            if (msg.data.length != 0) {
                $scope.InvoiceReport_List = msg.data;

                $('#InvoiceReport_Search').hide();
                $('#InvoiceReport_List').show();
                $('#backToSearch').show();

                $scope.Title = "Invoice Report";

            }
            else {
                SweetAlert.swal("No record", 'No record found', "warning");
            }
        });


        //}       
        //else {
        //    SweetAlert.swal("Try agian", "There is some problem.", "", "error");
        //}
    }
    //------ End Search & Get list of Author / Publisher Statement


    //Start geoGraphical Search
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
            alert('Error in getting Geographical list');
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
                alert('Error in getting Geographical list');
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
            alert('Error in getting Geographical list');
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
    //End geoGraphical Search


    //Start AutoComplete for LicenseeName
    //$scope.autoCompleteLicenseeName = function () {
    setTimeout(function () {

        var obj = $("[name$=LicenseeName]").val();

        var LicenseNameAC = [];

        var mobj_LicenseeName = {
            LicenseeName: obj,
        };
        var getISBNList = AJService.PostDataToAPI("Report/GetLicenseeNameList", mobj_LicenseeName);
        getISBNList.then(function (msg) {
            for (i = 0; i < msg.data.length; i++) {
                //LicenseNameAC[i] = ISBN.data[i].LicenseeOrganizationName;
                LicenseNameAC[i] = { "label": msg.data[i].LicenseeOrganizationName, "value": msg.data[i].LicenseeOrganizationName, "data": msg.data[i].LicenseeOrganizationName };
            }

            $(function () {
                $("#LicenseeName").autocomplete({
                    source: LicenseNameAC
                });
            });

        });

    });
    //}
    //End AutoComplete for LicenseeName


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


    $scope.ExcelReport = function () {

       

        var mstr_InvoiceFromdate = $('#InvoiceFromdate').val();
        var mstr_InvoiceTodate = $('#InvoiceTodate').val();
        if (mstr_InvoiceFromdate == "") {
            mstr_InvoiceFromdate = null
        }
        if (mstr_InvoiceTodate == "") {
            mstr_InvoiceTodate = null
        }
     

        var _mobjAuthorPubSt = {
            InvoiceFromDate: mstr_InvoiceFromdate == null ? null : convertDate(mstr_InvoiceFromdate),
            InvoiceToDate: mstr_InvoiceTodate == null ? null : convertDate(mstr_InvoiceTodate),
            InvoiceNo: $scope.InvoiceNo,
            InvoiceValue: $scope.InvoiceValue,
            InvoiceStatus: $scope.InvoiceStatus,
            LicenseeName: $scope.LicenseeName,
            Country: $scope.Country,
            State: $scope.State,
            City: $scope.City
        }



        document.location = GlobalredirectPath + "Report/Report/exportToExcelInvoiceReportList?InvoiceFromDate=" + (mstr_InvoiceFromdate == null ? null : convertDate(mstr_InvoiceFromdate)) + "&InvoiceToDate=" + (mstr_InvoiceTodate == null ? null : convertDate(mstr_InvoiceTodate)) + "&InvoiceNo=" + $scope.InvoiceNo + "&InvoiceValue=" + $scope.InvoiceValue + "&InvoiceStatus=" + $scope.InvoiceStatus + "&LicenseeName=" + $scope.LicenseeName + "&Country=" + $scope.Country + "&State=" + $scope.State + "&City=" + $scope.City + "";

       // document.location = GlobalredirectPath + "/Report/Report/exportToExcelInvoiceReportList?InvoiceFromDate=" + (mstr_InvoiceFromdate == null ? null : convertDateForInsert(mstr_InvoiceFromdate)) + "";

    }



    $scope.InvoiceReportReportExcel = function () {



        $scope.ExcelReport();
    }

});


