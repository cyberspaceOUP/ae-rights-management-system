app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {

    app.expandControllerA($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);
    /*
  
 Here is the service used for inserting the Language List.
 Bind data In the object and send to api for inserting and update.
 Common function has used for both updation and Insertion

 */

    $scope.AddAssetSubType = function () {
        blockUI.start();
        var _AssetSubType = {
            AssetName: $scope.name,
            Id: $('#hid_recordid').val() == "" ? 0 : $('#hid_recordid').val(),
            EnteredBy: $("#enterdBy").val()
        };

        var LanguageStatus = AJService.PostDataToAPI('AssetMaster/InsertAssetSubType', _AssetSubType);
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
                    $scope.name = "";
                    $('#btnSubmit').html("Submit");
                    $scope.update_city == "False";
                    $('#hid_recordid').val("");
                    angular.element(document.getElementById('angularid')).scope().getAssetSubType();
                }
            }

        },

        function () {
            alert('Please validate details');
        });
        blockUI.stop();
    }

    /*
    Populating the department List based on the Record id of Primary Key
    */
    //Edit AssetSubType basis on The recordID
    $scope.EditAssetSubTypeData = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            blockUI.start();
            // call API to fetch temp Department list basis on the FlatId
            var AssetSubType = AJService.GetDataFromAPI('AssetMaster/getAssetSubType?Id=' + Id);
            AssetSubType.then(function (msg) {
                if (msg != null) {
                    $scope.name = msg.data.AssetName;
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


    $scope.DeleteAssetSubTypeData = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var _AssetSubType = {
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
                     var LanguageStatus = AJService.PostDataToAPI("AssetMaster/DeleteAssetSubType", _AssetSubType);
                     LanguageStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             blockUI.stop();
                         }
                         {
                             angular.element(document.getElementById('angularid')).scope().getAssetSubType();

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


    $scope.Reset = function (userForm) {
        $scope.name = null;
        $scope.userForm.$setPristine();
        $scope.submitted = false;
    }

    $scope.cancelForm = function (userForm) {
        angular.element(document.getElementById('angularid')).scope().getAssetSubType();
        $scope.submitted = false;
    }

    // function call on page load to set values
    $scope.LoadStaffPage = function (Id) {
        if (Id != null) {

            $('#btncancel').show();
            $('#btnReset').hide();
        }
        else {
            $('#btncancel').hide();
            $('#btnReset').show();
        }
    }


    $scope.getAssetSubType = function () {
        var getAssetSubType = AJService.GetDataFromAPI("PermissionsInbound/getAssetSubType", null);
        getAssetSubType.then(function (msg) {
            $scope.AssetSubTypeList = msg.data;
        }, function () {
            //alert('Error in getting Asset Sub-Type List');
        });
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
                $scope.AddAssetSubType();
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    };


});