app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {
    app.expandControllerA($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);
    //Insert/Update ManuscriptDeliveryFormat
    $scope.AddManuscriptDeliveryFormat = function () {
        blockUI.start();

        var ManuscriptDeliveryFormatMaster = {
            ManuscriptDeliveryFormat: $scope.name,
            Id: $('#hid_recordid').val() == "" ? 0 : $('#hid_recordid').val(),
            EnteredBy: $("#enterdBy").val()
        };

        var ManuscriptDeliveryFormatStatus = AJService.PostDataToAPI('ManuscriptDeliveryFormatMaster/InsertManuscriptDeliveryFormat', ManuscriptDeliveryFormatMaster);
        ManuscriptDeliveryFormatStatus.then(function (msg) {

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
                angular.element(document.getElementById('angularid')).scope().GetManuscriptDeliveryFormatList();
            }
        },


        function () {
            alert('Please validate details');
        });
        blockUI.stop();
    }

    //Edit ManuscriptDeliveryFormat
    $scope.EditManuscriptDeliveryFormatData = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var ManuscriptDeliveryFormat = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            blockUI.start();
            // call API to fetch temp ManuscriptDeliveryFormat list basis on the ManuscriptDeliveryFormat Id
            var ManuscriptDeliveryFormat = AJService.PostDataToAPI('ManuscriptDeliveryFormatMaster/ManuscriptDeliveryFormat', ManuscriptDeliveryFormat);
            ManuscriptDeliveryFormat.then(function (msg) {
                if (msg != null) {
                    $scope.name = msg.data.ManuscriptDeliveryFormat
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

    //Delete ManuscriptDeliveryFormat
    $scope.DeleteManuscriptDeliveryFormat = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var ManuscriptDeliveryFormat = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this Manuscript Delivery Format detail! ",
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
                     var ManuscriptDeliveryFormatStatus = AJService.PostDataToAPI("ManuscriptDeliveryFormatMaster/ManuscriptDeliveryFormatDelete", ManuscriptDeliveryFormat);
                     ManuscriptDeliveryFormatStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             blockUI.stop();
                         }
                         {
                             angular.element(document.getElementById('angularid')).scope().GetManuscriptDeliveryFormatList();
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

    //Get All ManuscriptDeliveryFormat
    $scope.GetManuscriptDeliveryFormatList = function () {
        var getManuscriptDeliveryFormatList = AJService.GetDataFromAPI("ManuscriptDeliveryFormatMaster/GetManuscriptDeliveryFormatList?Id=" + $("#enterdBy").val());
        getManuscriptDeliveryFormatList.then(function (ManuscriptDeliveryFormat) {
            $scope.ManuscriptDeliveryFormatList = ManuscriptDeliveryFormat.data;
        }, function () {
            alert('Error in getting ManuscriptDeliveryFormat list');
        });
    }

    //Get ManuscriptDeliveryFormat by id
    $scope.GetManuscriptDeliveryFormatDetailsById = function (Id) {
        if (Id != null) {
            var ManuscriptDeliveryFormat = {
                Id: Id
            };
            var ManuscriptDeliveryFormatData = AJService.PostDataToAPI("ManuscriptDeliveryFormatMaster/GetManuscriptDeliveryFormatById", ManuscriptDeliveryFormat);
            ManuscriptDeliveryFormatData.then(function (ManuscriptDeliveryFormatData) {
                $scope.name = ManuscriptDeliveryFormatData.data.ManuscriptDeliveryFormatName;
            }, function () {
                alert('Error in getting ManuscriptDeliveryFormat list');
            });
        }
        else {
            alert("ManuscriptDeliveryFormatID not found");
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
                $scope.AddManuscriptDeliveryFormat();
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    };
});