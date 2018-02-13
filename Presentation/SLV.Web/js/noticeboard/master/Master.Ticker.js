app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {

    app.expandControllerA($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);

    //set defauld date (today)
    $scope.FromDate = Getdate();

    $scope.SetFromDate = function (datetext1) {
        $scope.FromDate = $(datetext1).val();
    }

    $scope.SetToDate = function (datetext1) {
        $scope.ToDate = $(datetext1).val();
    }

    $scope.GetTickerList = function () {
        var TickerList = AJService.GetDataFromAPI("Master/GetTickerList", null);
        TickerList.then(function (msg) {
            $scope.TickerList = msg.data;
        }, function () {
            alert('Error in getting Ticker list');
        });
    };

    $scope.AddTickerData = function () {
        blockUI.start();
        var Ticker = {
            Title: $scope.TickerTitle,
            FromDate: $scope.FromDate == undefined ? null : ($scope.FromDate == "" ? null : convertDateMDY($scope.FromDate)),
            ToDate: $scope.ToDate == undefined ? null : ($scope.ToDate == "" ? null : convertDateMDY($scope.ToDate)),
            Order: $scope.Order == undefined ? 0 : ($scope.Order == "" ? 0 : $scope.Order),
            Id: $('#hid_recordid').val() == undefined ? 0 : ($('#hid_recordid').val() == "" ? 0 : $('#hid_recordid').val()),
            EnteredBy: $("#enterdBy").val()
        };

        var LanguageStatus = AJService.PostDataToAPI('Master/InsertTicker', Ticker);
        LanguageStatus.then(function (msg) {
            if (msg.data != "OK") {
                SweetAlert.swal("Error!", "Duplicate. already exist !", "", "error");
            }
            else {
                if ($('#hid_recordid').val() != "") {
                    SweetAlert.swal('Updated successfully.', '', "success");
                }
                else {
                    SweetAlert.swal('Insert successfully.', '', "success");
                }
                {
                    //$scope.TickerTitle = "";
                    //$scope.FromDate = Getdate();
                    //$scope.ToDate = "";
                    //$scope.Order = "";
                    //$('#btnSubmit').html("Submit");
                    //$('#hid_recordid').val("");
                    //angular.element(document.getElementById('angularid')).scope().GetTickerList();

                    window.location.href = GlobalredirectPath + "Master/Master/Ticker";
                }
            }

        },

        function () {
            alert('Please validate details');
        });
        blockUI.stop();
    }

    $scope.submitForm = function () {
        $scope.submitted = true;
        if ($scope.userForm.$valid) {

            $scope.AddTickerData();
            // set form default state
            $scope.userForm.$setPristine();
            // set form is no submitted
            $scope.submitted = false;
            return;

        }
    };

    $scope.EditTickerData = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            blockUI.start();
            // call API to fetch temp Department list basis on the FlatId
            var Language = AJService.GetDataFromAPI('Master/getTickerById?Id=' + Id);
            Language.then(function (msg) {
                if (msg != null) {
                    $scope.TickerTitle =  msg.data.Title;
                    $scope.FromDate = msg.data.FromDate == null ? "" : msg.data.FromDate;
                    $scope.ToDate = msg.data.ToDate == null ? "" : msg.data.ToDate;
                    $scope.Order = msg.data.Order;
                    $('#btnSubmit').html("Update");
                    $('#hid_recordid').val(msg.data.Id);
                    blockUI.stop();
                }
                else {
                    SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                    blockUI.stop();
                }

            });
        }
    }

    $scope.DeleteTickerData = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var _Ticker = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this detail! ",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, delete it!",
                closeOnConfirm: false,
                closeOnCancel: true
            },
             function (Confirm) {
                 if (Confirm) {
                     blockUI.start();
                     // call API to fetch temp Department list basis on the FlatId
                     var LanguageStatus = AJService.PostDataToAPI("Master/DeleteTickerSet", _Ticker);
                     LanguageStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             blockUI.stop();
                         }
                         {
                             //angular.element(document.getElementById('angularid')).scope().GetTickerList();

                             window.location.href = GlobalredirectPath + "Master/Master/Ticker";
                         }
                     });
                 }

             });
        }
        else {
            SweetAlert.swal("Error!", "Record is not Deleted", "", "error");
            blockUI.stop();
        }
        blockUI.stop();
    }

    function Getdate() {
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!

        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd
        }
        if (mm < 10) {
            mm = '0' + mm
        }
        var today = dd + '/' + mm + '/' + yyyy;
        return today;
    }
    
    function convertDateMDY(date) {
        if (date != undefined) {
            var datearray = date.split("/");
            return datearray[1] + '/' + datearray[0] + '/' + datearray[2];
        }
        else {
            return null;
        }
    }

});