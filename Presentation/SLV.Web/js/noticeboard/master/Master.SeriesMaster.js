app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {
    app.expandControllerA($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);
    /******************** Start DivisionSubDivision Control********************/

    app.expandControllerDivisionSubDivision($scope, AJService, $window);
    $scope.DivisionMandatory = true;
    $scope.SubDivisionMandatory = false;

    /********************End DivisionSubDivision Control********************/


    //Fill SeriesMaster 
    $scope.fillSeriesMaster = function (SeriesObj) {
        var Series = SeriesObj.data.SeriesM;

        $scope.DivSubDivCntrl = {
            Division: Series.DivisionId,
            SubDivision: Series.SubdivisionId,
        }
    }




    //Insert/Update Series
    $scope.AddSeries = function () {
        blockUI.start();

        var SeriesMaster = {
            divisionid: $scope.DivSubDivCntrl.Division,
            Subdivisionid: $scope.DivSubDivCntrl.SubDivision,
            Seriesname: $scope.name,
            Id: $('#hid_recordid').val() == "" ? 0 : $('#hid_recordid').val(),
            EnteredBy: $("#enterdBy").val()
        };

        var SeriesStatus = AJService.PostDataToAPI('SeriesMaster/InsertSeries', SeriesMaster);
        SeriesStatus.then(function (msg) {

            if (msg.data != "OK") {
                SweetAlert.swal("Message!", "Duplicate. already exists !", "", "error");
            }
            else {
                if ($('#hid_recordid').val() != "") {
                    SweetAlert.swal('Updated successfully.', '', "success");
                }
                else {
                    SweetAlert.swal('Inserted successfully.', '', "success");
                }

                $scope.name = "";
                $('#hid_recordid').val("");
                $('#btnSubmit').html("Submit");

                $scope.DivSubDivCntrl = {
                    Value : ""
                }

                angular.element(document.getElementById('angularid')).scope().GetSeriesList();
            }
        },

        function () {
            alert('Please validate details');
        });
        blockUI.stop();
    }

    //Edit Series
    $scope.EditSeriesData = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var Series = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            blockUI.start();
            // call API to fetch temp Series list basis on the Series Id
            var Series = AJService.PostDataToAPI('SeriesMaster/Series', Series);
            Series.then(function (msg) {
                if (msg != null) {
                    $scope.name = msg.data.Seriesname
                    $scope.DivSubDivCntrl.Division = msg.data.divisionid
                    $scope.getSubDivisionbyDivisionIdList();
                    $scope.DivSubDivCntrl.SubDivision  = msg.data.Subdivisionid
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

    //Delete Series
    $scope.DeleteSeries = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var Series = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this Series detail! ",
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
                     // var SeriesStatus = AJService.PostDataToAPI("SeriesMaster/DeleteSeries", Series);
                     var SeriesStatus = AJService.PostDataToAPI("SeriesMaster/SeriesDelete", Series);
                     SeriesStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             blockUI.stop();
                         }
                         {
                             angular.element(document.getElementById('angularid')).scope().GetSeriesList();
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

    //Get All Series
    $scope.GetSeriesList = function () {
        var GetSeries = AJService.GetDataFromAPI("SeriesMaster/GetSeriesByDivisionSubdivision?Id=" + $("#enterdBy").val());
        GetSeries.then(function (Series) {
            $scope.SeriesData = Series.data;
        }, function () {
            alert('Error in getting Series list');
        });
    }

    //To submit form iif valid (all validation is true)
    $scope.submitForm = function () {
        $scope.submitted = true;
        if ($scope.userForm.$valid) {
            if ($scope.update_city == "True") {

                // set form default state
                $scope.userForm.$setPristine();

                // set form is no submitted
                $scope.submitted = false;

                return;
            }

            else if ($scope.userForm.$valid) {
                $scope.AddSeries();
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    };
});