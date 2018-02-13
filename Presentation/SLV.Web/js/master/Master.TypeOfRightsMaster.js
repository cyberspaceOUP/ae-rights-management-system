app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {
    app.expandControllerA($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);
    //Insert/Update TypeOfRights
    $scope.AddTypeOfRights = function () {
        blockUI.start();

        var TypeOfRightsMaster = {
            TypeOfRights: $scope.name,
            Id: $('#hid_recordid').val() == "" ? 0 : $('#hid_recordid').val(),
            EnteredBy: $("#enterdBy").val()
        };

        var TypeOfRightsStatus = AJService.PostDataToAPI('TypeOfRightsMaster/InsertTypeOfRights', TypeOfRightsMaster);
        TypeOfRightsStatus.then(function (msg) {

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
                angular.element(document.getElementById('angularid')).scope().GetTypeOfRightsList();
            }
        },


        function () {
            alert('Please validate details');
        });
        blockUI.stop();
    }

    //Edit TypeOfRights
    $scope.EditTypeOfRightsData = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var TypeOfRights = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            blockUI.start();
            // call API to fetch temp TypeOfRights list basis on the TypeOfRights Id
            var TypeOfRights = AJService.PostDataToAPI('TypeOfRightsMaster/TypeOfRights', TypeOfRights);
            TypeOfRights.then(function (msg) {
                if (msg != null) {
                    $scope.name = msg.data.TypeOfRights
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

    //Delete TypeOfRights
    $scope.DeleteTypeOfRights = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var TypeOfRights = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this Type of Rights detail! ",
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
                     // var TypeOfRightsStatus = AJService.PostDataToAPI("TypeOfRightsMaster/DeleteTypeOfRights", TypeOfRights);
                     var TypeOfRightsStatus = AJService.PostDataToAPI("TypeOfRightsMaster/TypeOfRightsDelete", TypeOfRights);
                     TypeOfRightsStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             blockUI.stop();
                         }
                         {
                             angular.element(document.getElementById('angularid')).scope().GetTypeOfRightsList();
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

    //Get All TypeOfRights
    $scope.GetTypeOfRightsList = function () {
        var getTypeOfRightsList = AJService.GetDataFromAPI("TypeOfRightsMaster/GetTypeOfRightsList?Id=" + $("#enterdBy").val());
        getTypeOfRightsList.then(function (TypeOfRights) {
            $scope.TypeOfRightsList = TypeOfRights.data;
        }, function () {
            //alert('Error in getting TypeOfRights list');
        });
    }

    //Get TypeOfRights by id
    $scope.GetTypeOfRightsDetailsById = function (Id) {
        if (Id != null) {
            var TypeOfRights = {
                Id: Id
            };
            var TypeOfRightsData = AJService.PostDataToAPI("TypeOfRightsMaster/GetTypeOfRightsById", TypeOfRights);
            TypeOfRightsData.then(function (TypeOfRightsData) {
                $scope.name = TypeOfRightsData.data.TypeOfRightsName;
            }, function () {
                //alert('Error in getting TypeOfRights list');
            });
        }
        else {
            //alert("TypeOfRightsID not found");
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
                $scope.AddTypeOfRights();
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    };
});