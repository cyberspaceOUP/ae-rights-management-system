app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {

    app.expandControllerA($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);
    /*
  
 Here is the service used for inserting the Geographical List.
 Bind data In the object and send to api for inserting and update.
 Common function has used for both updation and Insertion

 */


    $scope.AddStateData = function () {
        blockUI.start();
        var Geographical = {
            geogName: $scope.name,
            Id: $('#hid_recordid').val() == "" ? 0 : $('#hid_recordid').val(),
            geogType: 'State',
            EnteredBy: $("#enterdBy").val(),
            parentid: $scope.country
        };

        var GeographicalStatus = AJService.PostDataToAPI('GeographicalMaster/InsertGeographical', Geographical);
        GeographicalStatus.then(function (msg) {
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
                    //$scope.country = $scope.country[0];
                    $('#btnSubmit').html("Submit");
                    $scope.update_city == "False";
                    $('#hid_recordid').val("");
                    angular.element(document.getElementById('angularid')).scope().GetCountryStateList();
                    setTimeout(function () {
                        $('[name*=country]').val("");
                    }, 100);
                }
            }

        },

        function () {
            alert('Please validate details');
        });
        blockUI.stop();
    };


    $scope.DeleteStateData = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var Geographical = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this State detail! ",
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
                     var SubsidiaryRightsStatus = AJService.PostDataToAPI("GeographicalMaster/DeleteGeographical", Geographical);
                     SubsidiaryRightsStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             blockUI.stop();
                         }
                         {
                             angular.element(document.getElementById('angularid')).scope().GetCountryStateList();

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


    /*
Populating the department List based on the Record id of Primary Key
*/
    //Edit Division basis on The recordID
    $scope.EditStateData = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            blockUI.start();
            // call API to fetch temp Department list basis on the FlatId
            var Geographical = AJService.GetDataFromAPI('GeographicalMaster/getGeographical?Id=' + Id);
            Geographical.then(function (msg) {
                if (msg != null) {
                    $scope.name = msg.data._geographicalMaster.geogName;
                    $scope.country = msg.data._geographicalMaster.parentid;
                    $('#btnSubmit').html("Update");
                    $('#hid_recordid').val(msg.data._geographicalMaster.Id);
                    blockUI.stop();
                }
                else {
                    SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                    blockUI.stop();
                }

            });
        }
    }




    $scope.Reset = function (stateForm) {
        $scope.name = null;
        $scope.country = $scope.country[0];
        $scope.userForm.$setPristine();
        $scope.submitted = false;
    }

    $scope.cancelForm = function (stateForm) {
        angular.element(document.getElementById('angularid')).scope().GetCountryStateList();
        $scope.submitted = false;
    }

    // function call on page load to set values
    $scope.LoadStaffPage = function (Id) {
        if (Id != null) {

            $('#btncancel').show();
            $('#btnReset').hide();
            //$('#Sign').addClass('fa fa-check');
        }
        else {
            $('#btncancel').hide();
            $('#btnReset').show();
        }
    }


    $scope.GetCountryList = function () {
        var GetCountryList = AJService.GetDataFromAPI("GeographicalMaster/GetGeographicalList?geogtype=Country", null);
        GetCountryList.then(function (Country) {
            $scope.CountryList = Country.data;
        }, function () {
            alert('Error in getting Country list');
        });
    };

    $scope.GetCountryStateList = function () {
        var GetStateList = AJService.GetDataFromAPI("GeographicalMaster/GetCountryStateList", null);
        GetStateList.then(function (State) {
            $scope.CountryStateList = State.data;
        }, function () {
            alert('Error in getting State list');
        });
    };

    //To call PostVisitorEntry() function if form is valid (all validation is true

    $scope.submitForm = function () {
        $scope.submitted = true;
        if ($scope.stateForm.$valid) {
            if ($scope.update_city == "True") {

                //$scope.UpdateCity();
                // set form default state
                $scope.stateForm.$setPristine();

                // set form is no submitted
                $scope.submitted = false;

                return;
            }

            else if ($scope.stateForm.$valid) {
                $scope.AddStateData();
                // set form default state
                $scope.stateForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    };

});