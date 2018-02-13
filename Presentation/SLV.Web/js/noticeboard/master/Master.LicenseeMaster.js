app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {
    app.expandControllerTopSearch($scope, AJService, $window);

    /******************** Start CountryStateCity Dropdown Control********************/

    app.expandControllerA($scope, AJService, $window);
    $scope.NomineeCountryReq = true;
    $scope.NomineeStateReq = true;
    $scope.NomineeCityReq = true;
    $scope.NomineepincodeReq = true;
    $scope.LicenseeReq = true;

    //Get Country List
    $scope.GeogList = function () {
        var GeogType = {
            geogtype: "country",
            parentid: null,

        };
        var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
        GetgeogList.then(function (GetgeogList) {
            $scope.CountryList = GetgeogList.data;
            $scope.OthersNot = true;
            $scope.OthersYes = false;
            $scope.sates = null;
        }, function () {
            alert('Error in getting Geographical list');
        });
    }

    //Get State List
    $scope.getCountryStates = function () {
        setTimeout(function () {
            $($('select[name*=State]')[3]).val('');

            $($('select[name*=city]')[3]).val('');
        }, 500)
        var GeogType = {
            geogtype: "state",
            parentid: $scope.NomineeCountry,
        };
        if ($.trim($("#NomineeCountry option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.OthersNot = false;
            $scope.OthersYes = true;
            $scope.NomineeCityList = [];
        }
        else {
            $scope.OthersNot = true;
            $scope.OthersYes = false;
            $scope.cities = [];
            var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
            GetgeogList.then(function (GetgeogList) {
                $scope.NomineeStateList = GetgeogList.data;
                $scope.NomineeCityList = [];
            }, function () {
                alert('Error in getting Geographical list');
            });
        }
    }

    //Get City List
    $scope.getStateCities = function () {
        setTimeout(function () {
            $($('select[name*=city]')[3]).val('');
        }, 500)
        var GeogType = {
            geogtype: "city",
            parentid: $scope.NomineeState,
        };
        if ($.trim($("#NomineeState option:selected").html()).toLowerCase().indexOf("others") > -1) {

            $('.othersCityHide').css("display", "none");
            $scope.OthersStateYes = true;
            $scope.NomineeCityList = [];
        }

        var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
        GetgeogList.then(function (GetgeogList) {
            $scope.NomineeCityList = GetgeogList.data;
        }, function () {
            alert('Error in getting Geographical list');
        });

    }

    $scope.showState = function () {
        if ($.trim($("#NomineeCountry option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.State.css("display", "none");
            $scope.cities = [];
        }
    }

    //Get State in Edit Mode
    $scope.getCountryStatesNominee = function (Id) {

        var GeogType = {
            geogtype: "state",
            parentid: Id,
        };
        if ($.trim($("#Country option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.OthersNot = false;
            $scope.OthersYes = true;
            $scope.OtherCountry = true;
            $scope.NomineeCityList = [];
        }
        else {
            $scope.OthersNot = true;
            $scope.OthersYes = false;
            $scope.OtherCountry = false;
            $scope.NomineeCityList = [];
            var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
            GetgeogList.then(function (GetgeogList) {
                $scope.NomineeStateList = GetgeogList.data;
                $scope.NomineeCityList = [];
            }, function () {
                alert('Error in getting Geographical list');
            });
        }
    }

    //Get City in Edit Mode
    $scope.getStateCitiesNominee = function (Id) {


        var GeogType = {
            geogtype: "city",
            parentid: Id,

        };
        if ($.trim($("#state option:selected").html()).toLowerCase().indexOf("others") > -1) {

            $('.othersCityHide').css("display", "none");
            $scope.OthersStateYes = true;
        }

        var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
        GetgeogList.then(function (GetgeogList) {
            $scope.NomineeCityList = GetgeogList.data;
        }, function () {
            alert('Error in getting Geographical list');
        });

    }
    
    /********************End CountryStateCity Dropdown Control********************/

    //Get LicenseeMaster List
    $scope.getAllLicenseeList = function () {
        var GetLicensee = AJService.GetDataFromAPI("LicenseeMaster/GetLicenseeList?Id=" + $("#enterdBy").val());
        GetLicensee.then(function (Licensee) {
            $scope.LicenseeData = Licensee.data;
        }, function () {
            alert('Error in getting Licensee list');
        });
    }

    //Insert/Update Licensee
    $scope.AddLicensee = function () {
        blockUI.start();

        var LicenseeMaster = {
            OrganizationName: $scope.OrganizationName,
            ContactPerson: $scope.contactPerson,
            Address: $scope.Address,
            CountryId: $scope.NomineeCountry,
            StateId: $('[name=NomineeState]').val(),
            Cityid: $('[name=Nomineecity]').val(),
            Pincode: $scope.Nomineepincode,
            Mobile: $scope.Mobile,
            Email: $scope.Email,
            URL: $scope.URL,
            Id: $('#hid_recordid').val() == "" ? 0 : $('#hid_recordid').val(),
            EnteredBy: $("#enterdBy").val()
        };

        var LicenseeStatus = AJService.PostDataToAPI('LicenseeMaster/InsertLicensee', LicenseeMaster);
        LicenseeStatus.then(function (msg) {

            if (msg.data == "Duplicate") {
                SweetAlert.swal("Message", "Duplicate. Licensee already exists !", "", "error");
            }
            else if (msg.data != "OK") {
                SweetAlert.swal("Error!", "There is some problem. !", "", "error");
            }
            else {
                if ($('#hid_recordid').val() != "") {
                    SweetAlert.swal('Updated successfully.', '', "success");
                }
                else {
                    SweetAlert.swal('Inserted successfully.', '', "success");
                } 

                $scope.OrganizationName = "";
                $scope.contactPerson = "";
                $scope.Address = "";
                $scope.NomineeCountry = "";
                $scope.NomineeState = "";
                $scope.NomineeCity = "";
                $scope.Nomineepincode = "";
                $scope.Mobile = "";
                $scope.Email = "";
                $scope.URL = "";
                $('#hid_recordid').val("");
                $('#btnSubmit').html("Submit");

                angular.element(document.getElementById('angularid')).scope().getAllLicenseeList();
            }
        },

        function () {
            alert('Please validate details');
        });
        blockUI.stop();
    }

    //Edit Licensee
    $scope.EditLicensee = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var Licensee = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            blockUI.start();
            // call API to fetch temp Licensee list basis on the Licensee Id
            var Licensee = AJService.PostDataToAPI('LicenseeMaster/Licensee', Licensee);
            Licensee.then(function (msg) {

                var NomineeCountry = undefined;
                var NomineeState = undefined;
                var NomineeCity = undefined;

                if (msg.data.CountryId != 0) {
                    NomineeCountry = msg.data.CountryId;
                }
                if (msg.data.Stateid != 0) {
                    $scope.getCountryStatesNominee(msg.data.CountryId)
                    NomineeState = msg.data.Stateid;
                }
                if (msg.data.Cityid != 0) {

                    $scope.getStateCitiesNominee(msg.data.Stateid);
                    NomineeCity = msg.data.Cityid;
                }

                if (msg != null) {
                    $scope.OrganizationName = msg.data.OrganizationName;
                    $scope.contactPerson = msg.data.ContactPerson;
                    $scope.Address = msg.data.Address;
                    $scope.NomineeCountry = msg.data.CountryId;

                    setTimeout(function () {
                        $scope.getCountryStatesNominee(msg.data.CountryId)
                        $scope.NomineeStateValue = NomineeState
                        $scope.NomineeState = NomineeState


                    }, 100);

                    setTimeout(function () {
                        $scope.getStateCitiesNominee(msg.data.Stateid)
                        $scope.NomineeCityValue = NomineeCity
                        $scope.NomineeCity = NomineeCity

                    }, 150);

                    $scope.Nomineepincode = msg.data.Pincode;
                    $scope.Mobile = msg.data.Mobile;
                    $scope.Email = msg.data.Email;
                    $scope.URL = msg.data.URL;

                    $('#btnSubmit').html("Update");
                    $('#hid_recordid').val(msg.data.Id);

                    $scope.getAllPublishingCompany();
                    blockUI.stop();
                }
                else {
                    SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                    blockUI.stop();
                }
            });
        }
    }

    //Delete Licensee
    $scope.DeleteLicensee = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var Licensee = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this Licensee detail! ",
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
                     var LicenseeStatus = AJService.PostDataToAPI("LicenseeMaster/LicenseeDelete", Licensee);
                     LicenseeStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             blockUI.stop();
                         }
                         {
                             angular.element(document.getElementById('angularid')).scope().getAllLicenseeList();
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

    //To submit form if valid (all validation is true)
    $scope.submitForm = function () {
        $scope.submitted = true;
        if ($scope.userForm.$valid) {
            if ($scope.userForm.$valid) {
                $scope.AddLicensee();
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    }

    //getAllLicenseeList
    $scope.getAllLicenseeList = function () {
        var GetLicensee = AJService.GetDataFromAPI("LicenseeMaster/GetLicenseeList?Id=" + $("#enterdBy").val());
        GetLicensee.then(function (Licensee) {
            $scope.LicenseeData = Licensee.data;
        }, function () {
            alert('Error in getting Licensee list');
        });
    }



});