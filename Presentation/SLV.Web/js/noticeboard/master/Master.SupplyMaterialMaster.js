app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {
    app.expandControllerA($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);
    //Insert/Update SupplyMaterial
    $scope.AddSupplyMaterial = function () {
        blockUI.start();

        var SupplyMaterialMaster = {
            SupplyMaterial: $scope.name,
            Id: $('#hid_recordid').val() == "" ? 0 : $('#hid_recordid').val(),
            EnteredBy: $("#enterdBy").val()
        };

        var SupplyMaterialStatus = AJService.PostDataToAPI('SupplyMaterialMaster/InsertSupplyMaterial', SupplyMaterialMaster);
        SupplyMaterialStatus.then(function (msg) {

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
                angular.element(document.getElementById('angularid')).scope().GetSupplyMaterialList();
            }
        },


        function () {
            alert('Please validate details');
        });
        blockUI.stop();
    }

    //Edit SupplyMaterial
    $scope.EditSupplyMaterialData = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var SupplyMaterial = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            blockUI.start();
            // call API to fetch temp SupplyMaterial list basis on the SupplyMaterial Id
            var SupplyMaterial = AJService.PostDataToAPI('SupplyMaterialMaster/SupplyMaterial', SupplyMaterial);
            SupplyMaterial.then(function (msg) {
                if (msg != null) {
                    $scope.name = msg.data.SupplyMaterial
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

    //Delete SupplyMaterial
    $scope.DeleteSupplyMaterial = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var SupplyMaterial = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this Supply Material detail! ",
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
                     var SupplyMaterialStatus = AJService.PostDataToAPI("SupplyMaterialMaster/SupplyMaterialDelete", SupplyMaterial);
                     SupplyMaterialStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             blockUI.stop();
                         }
                         {
                             angular.element(document.getElementById('angularid')).scope().GetSupplyMaterialList();
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

    //Get All SupplyMaterial
    $scope.GetSupplyMaterialList = function () {
        var getSupplyMaterialList = AJService.GetDataFromAPI("SupplyMaterialMaster/GetSupplyMaterialList?Id=" + $("#enterdBy").val());
        getSupplyMaterialList.then(function (SupplyMaterial) {
            $scope.SupplyMaterialList = SupplyMaterial.data;
        }, function () {
            alert('Error in getting SupplyMaterial list');
        });
    }

    //Get SupplyMaterial by id
    $scope.GetSupplyMaterialDetailsById = function (Id) {
        if (Id != null) {
            var SupplyMaterial = {
                Id: Id
            };
            var SupplyMaterialData = AJService.PostDataToAPI("SupplyMaterialMaster/GetSupplyMaterialById", SupplyMaterial);
            SupplyMaterialData.then(function (SupplyMaterialData) {
                $scope.name = SupplyMaterialData.data.SupplyMaterialName;
            }, function () {
                alert('Error in getting SupplyMaterial list');
            });
        }
        else {
            alert("SupplyMaterialID not found");
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
                $scope.AddSupplyMaterial();
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    };
});