//Author Contract Expiry Report Search Added by Ankush Kumar on 25/10/2016


app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {

    app.expandControllerTopSearch($scope, AJService, $window);

    $scope.ShowSearchForm = true;
    $scope.ShowListForm = false;
    $scope.backToSearch = false;
    $scope.headingStatement = "Author Contract Expiry Report Search";
    $("#ExpiryToDate").attr("disabled", "disabled");

    //------ Back function for returm on Search form
    $scope.func_GoBack = function () {
        window.location.href = window.location.href;
        $scope.backToSearch = false;
        $scope.headingStatement = "Author Contract Expiry Report Search";

    }

    $scope.SetExpiryFromDate = function (ExpiryFromDate) {
        if ($(ExpiryFromDate).val() == "") {
            $scope.ExpiryToDate = null;
            $scope.ExpiryFromDate = null;
        }
        else {

            $scope.ExpiryFromDate = $(ExpiryFromDate).val();
        }
    }

    $scope.SetExpiryToDate = function (ExpiryToDate) {
        $scope.ExpiryToDate = $(ExpiryToDate).val();
    }

    //------ Search form Submit
    $scope.AuthorContractExpiryReportSearchForm = function () {
        $scope.submitted = true;

        if ($scope.userForm.$valid) {
            $scope.AuthorContractExpiryReportSearch();
            // set form default state
            $scope.userForm.$setPristine();
            // set form is no submitted
            $scope.submitted = false;
            return;
        }
    }

    //------ Search & Get list of Author Contract Expiry Report
    $scope.AuthorContractExpiryReportSearch = function () {
        debugger;

        var _mobjAuthorPubSt = {
            ExpiryFromDate: $scope.ExpiryFromDate == null ? null : convertDate($scope.ExpiryFromDate),
            ExpiryToDate: $scope.ExpiryToDate == null ? null : convertDate($scope.ExpiryToDate),
            ProductCode: $scope.ProductCode,
            ProductName: $scope.ProductName,
            ISBN: $scope.ISBN,
            Authors: $scope.Authors
        }

        var ExecutiveStatus = AJService.PostDataToAPI('Report/AuthorContractExpiryReportList', _mobjAuthorPubSt);
        ExecutiveStatus.then(function (msg) {

            if (msg.data.length != 0) {
                $scope.AuthorContractList = msg.data;
                $scope.headingStatement = "Author Contract Expiry Report";
                $scope.backToSearch = true;
                $scope.ShowSearchForm = false;
                $scope.ShowListForm = true;
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
    //------ End Search & Get list of Author Contract Expiry Report

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

});


