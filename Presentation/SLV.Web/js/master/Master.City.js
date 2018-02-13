app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {

    app.expandControllerA($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);
    /*
  
 Here is the service used for inserting the Geographical List.
 Bind data In the object and send to api for inserting and update.
 Common function has used for both updation and Insertion

 */
    $scope.AddCityData = function () {
        blockUI.start();
        var Geographical = {
            geogName: $scope.name,
            Id: $('#hid_recordid').val() == "" ? 0 : $('#hid_recordid').val(),
            geogType: 'City',
            EnteredBy: $("#enterdBy").val(),
            parentid: $scope.state
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
                    $scope.country = "";
                    $scope.state = "";
                    $('#btnSubmit').html("Submit");
                    $scope.update_city == "False";
                    $('#hid_recordid').val("");
                    angular.element(document.getElementById('angularid')).scope().GetCountryStateCityList(); 
                    angular.element(document.getElementById('angularid')).scope().StateRefresh();
                }
            }

        },

        function () {
            alert('Please validate details');
        });
        blockUI.stop();
    };


    $scope.DeleteCityData = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var Geographical = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this City detail! ",
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
                             angular.element(document.getElementById('angularid')).scope().GetCountryStateCityList();

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
    $scope.EditCityData = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            blockUI.start();
            // call API to fetch temp Department list basis on the FlatId
            var Geographical = AJService.GetDataFromAPI('GeographicalMaster/GetCountryStateCityList?Id=' + Id);
            Geographical.then(function (msg) {
                if (msg != null) {
                    $scope.name = msg.data[0].CityName;
                    $scope.country = msg.data[0].CountryId;
                    angular.element(document.getElementById('angularid')).scope().StateRefresh();
                    $scope.state = msg.data[0].StateId;
                    $('#btnSubmit').html("Update");
                    $('#hid_recordid').val(msg.data[0].Id);
                    blockUI.stop();
                }
                else {
                    SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                    blockUI.stop();
                }

            });
        }
    }




    $scope.Reset = function (cityForm) {
        $scope.name = null;
        $scope.country = $scope.country[0];
        $scope.userForm.$setPristine();
        $scope.submitted = false;
    }

    $scope.cancelForm = function (cityForm) {
        angular.element(document.getElementById('angularid')).scope().GetCountryStateCityList();
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

    $scope.GetSelectedState = function () {
        var parentid = $scope.country
        if (parentid == undefined) {
            parentid = 0;
        }
        var GetStateList = AJService.GetDataFromAPI("GeographicalMaster/GetGeographicalList?geogtype=State&parentid=" + parentid, null);
        GetStateList.then(function (State) {
            $scope.StateList = State.data;
            $scope.state = '';
        }, function () {
            alert('Error in getting State list');
        });
    };

    $scope.StateRefresh = function () {
        var parentid = $scope.country
        if (parentid == undefined) {
            parentid = 0;
        }
        var GetStateList = AJService.GetDataFromAPI("GeographicalMaster/GetGeographicalList?geogtype=State&parentid=" + parentid, null);
        GetStateList.then(function (State) {
            $scope.StateList = State.data;

            setTimeout(function () {
                $('[name*=state]').val($scope.state);
            }, 100);
        }, function () {
            alert('Error in getting State list');
        });
    };

    $scope.GetCountryStateCityList = function () {
        var GetCityList = AJService.GetDataFromAPI("GeographicalMaster/GetCountryStateCityList", null);
        GetCityList.then(function (City) {
            $scope.CountryStateCityList = City.data;
        }, function () {
            //alert('Error in getting City list');
        });
    };

    //To call PostVisitorEntry() function if form is valid (all validation is true

    $scope.submitForm = function () {
        $scope.submitted = true;
        if ($scope.cityForm.$valid) {
            if ($scope.update_city == "True") {

                //$scope.UpdateCity();
                // set form default state
                $scope.cityForm.$setPristine();

                // set form is no submitted
                $scope.submitted = false;

                return;
            }

            else if ($scope.cityForm.$valid) {
                $scope.AddCityData();
                // set form default state
                $scope.cityForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    };

});