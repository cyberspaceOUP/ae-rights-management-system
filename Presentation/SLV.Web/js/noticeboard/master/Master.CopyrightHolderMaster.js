app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {

    app.expandControllerTopSearch($scope, AJService, $window);
    /******************** Start CountryStateCity Dropdown Control********************/

    app.expandControllerA($scope, AJService, $window);
    $scope.NomineeCountryReq = false;
    $scope.NomineeStateReq = false;
    $scope.NomineeCityReq = false;
    $scope.NomineepincodeReq = false;

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

    //Insert/Update CopyrightHolder
    $scope.AddCopyrightHolder = function () {
        blockUI.start();

        var CopyrightHolderMaster = {
            CopyRightHolderName: $scope.CopyrightHolderName,
            ContactPerson: $scope.contactPerson,
            Address: $scope.Address,
            CountryId: $scope.NomineeCountry,
            StateId: $('[name=NomineeState]').val(),
            Cityid: $('[name=Nomineecity]').val(),
            Pincode: $scope.Nomineepincode,
            Mobile: $scope.Mobile,
            Email: $scope.Email,
            URL: $scope.URL,
            BankName: $scope.BankName,
            AccountNo: $scope.AccountNumber,
            BankAddress: $scope.BankAddress,
            IFSCCode: $scope.IFSCCode,
            PANNo: $scope.PANNo,
            VendorCOde: $scope.VendorCode,
            Id: $('#hid_recordid').val() == "" ? 0 : $('#hid_recordid').val(),
            EnteredBy: $("#enterdBy").val()
        };

        var CopyrightHolderStatus = AJService.PostDataToAPI('CopyrightHolderMaster/InsertCopyrightHolder', CopyrightHolderMaster);
        CopyrightHolderStatus.then(function (msg) {

            if (msg.data != "OK") {
                SweetAlert.swal("Message !", "Duplicate. already exists !", "", "error");
            }
            else {
                if ($('#hid_recordid').val() != "") {
                    SweetAlert.swal('Updated successfully.', '', "success");
                }
                else {
                    SweetAlert.swal('Inserted successfully.', '', "success");
                }

                $scope.CopyrightHolderName = "";
                $scope.contactPerson = "";
                $scope.Address = "";
                $scope.NomineeCountry = "";
                $scope.NomineeState = "";
                $scope.NomineeCity = "";
                $scope.Nomineepincode = "";
                $scope.Mobile = "";
                $scope.Email = "";
                $scope.URL = "";
                $scope.BankName = "",
                $scope.AccountNumber = "",
                $scope.BankAddress = "",
                $scope.IFSCCode = "",
                $scope.PANNo = "",
                $scope.VendorCode = "",

                $('#hid_recordid').val("");
                $('#btnSubmit').html("Submit");

                angular.element(document.getElementById('angularid')).scope().getAllCopyrightHolderList();
            }
        },

        function () {
            alert('Please validate details');
        });
        blockUI.stop();
    }

    //Edit CopyrightHolder
    $scope.EditCopyrightHolder = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var CopyrightHolder = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            blockUI.start();
            // call API to fetch temp CopyrightHolder list basis on the CopyrightHolder Id
            var CopyrightHolder = AJService.PostDataToAPI('CopyrightHolderMaster/CopyrightHolder', CopyrightHolder);
            CopyrightHolder.then(function (msg) {

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
                    $scope.CopyrightHolderName = msg.data.CopyRightHolderName;
                    $scope.contactPerson = msg.data.ContactPerson;
                    $scope.Address = msg.data.Address;
                    $scope.NomineeCountry = NomineeCountry;

                    setTimeout(function () {
                        $scope.getCountryStatesNominee(msg.data.CountryId)
                        $scope.NomineeStateValue = NomineeState

                    }, 100);

                    setTimeout(function () {
                        $scope.getStateCitiesNominee(msg.data.Stateid)
                        $scope.NomineeCityValue = NomineeCity

                    }, 150);

                    $scope.Nomineepincode = msg.data.Pincode;
                    $scope.Mobile = msg.data.Mobile;
                    $scope.Email = msg.data.Email;
                    $scope.URL = msg.data.URL;
                    $scope.BankName = msg.data.BankName,
                    $scope.AccountNumber = msg.data.AccountNo,
                    $scope.BankAddress = msg.data.BankAddress,
                    $scope.IFSCCode = msg.data.IFSCCode,
                    $scope.PANNo = msg.data.PANNo,
                    $scope.VendorCode = msg.data.VendorCOde,

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

    //Delete CopyrightHolder
    $scope.DeleteCopyrightHolder = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var CopyrightHolder = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this Copyright Holder detail! ",
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
                     var CopyrightHolderStatus = AJService.PostDataToAPI("CopyrightHolderMaster/CopyrightHolderDelete", CopyrightHolder);
                     CopyrightHolderStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             blockUI.stop();
                         }
                         {
                             angular.element(document.getElementById('angularid')).scope().getAllCopyrightHolderList();
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
                $scope.AddCopyrightHolder();
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    }

    //getAllCopyrightHolderList
    $scope.getAllCopyrightHolderList = function () {
        var GetCopyrightHolder = AJService.GetDataFromAPI("CopyrightHolderMaster/GetCopyrightHolderList?Id=" + $("#enterdBy").val());
        GetCopyrightHolder.then(function (CopyrightHolder) {
            $scope.CopyrightHolderData = CopyrightHolder.data;
        }, function () {
            alert('Error in getting CopyrightHolder list');
        });
    }
});