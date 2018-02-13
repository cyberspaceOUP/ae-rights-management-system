app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {
    app.expandControllerA($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);
    //Insert/Update TerritoryRights
    $scope.AddTerritoryRights = function () {
        blockUI.start();

        var TerritoryRightsMaster = {
            TerritoryRights: $scope.name,
            Id: $('#hid_recordid').val() == "" ? 0 : $('#hid_recordid').val(),
            EnteredBy: $("#enterdBy").val()
        };

        var TerritoryRightsStatus = AJService.PostDataToAPI('TerritoryRightsMaster/InsertTerritoryRights', TerritoryRightsMaster);
        TerritoryRightsStatus.then(function (msg) {

            if (msg.data != "OK") {
                SweetAlert.swal("Error!", "Duplicate. already exists !", "", "error");
            }
            else {
                if ($('#hid_recordid').val() != "") {
                    SweetAlert.swal('Updated successfully.', '', "success");

                }
                else {
                    SweetAlert.swal('Inserted successfully.', '', "success");
                }

                $('#btnSubmit').html("Submit");
                $scope.name = "";
                $('#hid_recordid').val("");
                angular.element(document.getElementById('angularid')).scope().GetTerritoryRightsList();
            }
        },


        function () {
            alert('Please validate details');
        });
        blockUI.stop();
    }

    //Edit TerritoryRights
    $scope.EditTerritoryRightsData = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var TerritoryRights = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            blockUI.start();
            // call API to fetch temp TerritoryRights list basis on the TerritoryRights Id
            var TerritoryRights = AJService.PostDataToAPI('TerritoryRightsMaster/TerritoryRights', TerritoryRights);
            TerritoryRights.then(function (msg) {
                if (msg != null) {
                    $scope.name = msg.data.Territoryrights
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

    //Delete TerritoryRights
    $scope.DeleteTerritoryRights = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var TerritoryRights = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this Territory Rights detail! ",
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
                     var TerritoryRightsStatus = AJService.PostDataToAPI("TerritoryRightsMaster/TerritoryRightsDelete", TerritoryRights);
                     TerritoryRightsStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             blockUI.stop();
                         }
                         {
                             angular.element(document.getElementById('angularid')).scope().GetTerritoryRightsList();
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

    //Get All TerritoryRights
    $scope.GetTerritoryRightsList = function () {
        var getTerritoryRightsList = AJService.GetDataFromAPI("TerritoryRightsMaster/GetTerritoryRightsList"); //?Id=" + $("#enterdBy").val()
        getTerritoryRightsList.then(function (TerritoryRights) {
            $scope.TerritoryRightsList = TerritoryRights.data;
        }, function () {
            alert('Error in getting TerritoryRights list');
        });
    }

    //Get TerritoryRights by id
    $scope.GetTerritoryRightsDetailsById = function (Id) {
        if (Id != null) {
            var TerritoryRights = {
                Id: Id
            };
            var TerritoryRightsData = AJService.PostDataToAPI("TerritoryRightsMaster/GetTerritoryRightsById", TerritoryRights);
            TerritoryRightsData.then(function (TerritoryRightsData) {
                $scope.name = TerritoryRightsData.data.TerritoryRightsName;
            }, function () {
                alert('Error in getting TerritoryRights list');
            });
        }
        else {
            alert("TerritoryRightsID not found");
        }
    }

    //To call PostVisitorEntry() function if form is valid (all validation is true)
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
                $scope.AddTerritoryRights();
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    };
});