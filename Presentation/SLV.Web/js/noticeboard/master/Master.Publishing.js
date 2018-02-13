app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {
    app.expandControllerA($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);
    // Get Country List
    $scope.GeogList = function () {
        //blockUI.start();
        var GeogType = {
            geogtype: "country",
            parentid: null,
        };
        var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
        GetgeogList.then(function (GetgeogList) {
            $scope.CountryList = GetgeogList.data;
            $scope.OthersNot = true;
            $scope.OthersYes = false;
            $scope.otherCities = false;
            $scope.OtherCountry = false;
            $scope.sates = [];
        }, function () {
            alert('Error in getting Geographical list');
        });
    }

    $scope.getCountryStates = function () {
        setTimeout(function () {
            $($('select[name*= State]')).val('');

            $($('select[name*= city]')).val('');
        }, 500)
        var GeogType = {
            geogtype: "state",
            parentid: $scope.Country,
        };
        if ($.trim($("#Country option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.OthersNot = false;
            $scope.OthersYes = true;
            $scope.OtherCountry = true;
            $scope.cities = [];
        }
        else {
            $scope.OthersNot = true;
            $scope.OthersYes = false;
            $scope.OtherCountry = false;
            $scope.cities = [];
            var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
            GetgeogList.then(function (GetgeogList) {
                $scope.sates = GetgeogList.data;
                $scope.cities = [];
            }, function () {
                alert('Error in getting Geographical list');
            });
        }
    }
    $scope.getStateCities = function () {
        setTimeout(function () {
            $($('select[name*= city]')).val('');
        }, 500)

        var GeogType = {
            geogtype: "city",
            parentid: $scope.State,
        };
        if ($.trim($("#state option:selected").html()).toLowerCase().indexOf("others") > -1) {
            //$scope.OthersNot = false;
            //$scope.OthersYes = true;

            $('.othersCityHide').css("display", "none");
            $scope.OthersStateYes = true;
            $scope.cities = [];
        }

        var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
        GetgeogList.then(function (GetgeogList) {
            $scope.cities = GetgeogList.data;
        }, function () {
            alert('Error in getting Geographical list');
        });

    }

    $scope.showState = function () {
        if ($.trim($("#Country option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.State.css("display", "none");
            $scope.cities = [];
        }
    }

    $scope.ChangeCitiesCities = function () {
        if ($.trim($("#city option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.otherCities = true;
        }
        else {
            $scope.otherCities = false;

        }
    }

    //Get state by Id on edit
    $scope.getCountryStatesValue = function (Id) {

        var GeogType = {
            geogtype: "state",
            parentid: Id,
        };
        if ($.trim($("#Country option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.OthersNot = false;
            $scope.OthersYes = true;
            $scope.OtherCountry = true;
            $scope.cities = [];
        }
        else {
            $scope.OthersNot = true;
            $scope.OthersYes = false;
            $scope.OtherCountry = false;
            $scope.cities = [];
            var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
            GetgeogList.then(function (GetgeogList) {
                $scope.sates = GetgeogList.data;
                $scope.cities = [];
            }, function () {
                alert('Error in getting Geographical list');
            });
        }
    }

    //Get City By Id on edit
    $scope.getStateCitiesValue = function (Id) {
        var GeogType = {
            geogtype: "city",
            parentid: Id,

        };
        if ($.trim($("#state option:selected").html()).toLowerCase().indexOf("others") > -1) {
            //$scope.OthersNot = false;
            //$scope.OthersYes = true;

            $('.othersCityHide').css("display", "none");
            $scope.OthersStateYes = true;
            $scope.cities = [];
        }

        var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
        GetgeogList.then(function (GetgeogList) {
            $scope.cities = GetgeogList.data;
        }, function () {
            alert('Error in getting Geographical list');
        });

    }

    $scope.submitForm = function () {
        $scope.submitted = true;
        if ($scope.userForm.$valid) {
            if ($scope.userForm.$valid) {
                $scope.AddPublishingCompany();
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    }

    $scope.AddPublishingCompany = function () {
        
        blockUI.start();
        var publishingCompany =
            {
                CompanyName: $scope.companyName,
                ContactPerson: $scope.contactPerson,
                Address: $scope.Address,
                Phone: $scope.Phone,
                Mobile: $scope.Mobile,
                Email: $scope.Email,
                Website: $scope.webSite,
                //
                CountryId: ($("#publisherGeo").find("#Country")).find('option:selected').val(),
                Stateid: ($("#publisherGeo").find("#state")).find('option:selected').val(),
                Cityid: ($("#publisherGeo").find("#city")).find('option:selected').val(),

                OtherCountry: ($("#publisherGeo").find("#CountryName")).find('option:selected').val(),
                OtherState: ($("#publisherGeo").find("#stateName")).find('option:selected').val(),
                OtherCity: ($("#publisherGeo").find("#cityName")).find('option:selected').val(),

                Pincode: ($("#publisherGeo").find("#pincode")).val(),
                //
                Id: $('#hid_publishingid').val() == "" ? 0 : $('#hid_publishingid').val()
            };

        var publisherStatus = AJService.PostDataToAPI('PublishingCompanyMaster/InsertPublishingCompany', publishingCompany);

        publisherStatus.then(function (msg) {
            if (msg.data != "OK") {

                SweetAlert.swal("Error!", "Publisher already exist !", "", "error");
            }
            else {
                if ($('#hid_publishingid').val() != "") {
                    SweetAlert.swal('Updated successfully.', '', "success");

                }
                else {
                    SweetAlert.swal('Insert successfully.', '', "success");
                }
                {
                    $scope.companyName = "";
                    $scope.contactPerson = "";
                    $scope.Phone = "";
                    $scope.Email = "";
                    $scope.Mobile = "";
                    $scope.webSite = "";
                    $scope.Address = "";
                    $scope.Country = "";
                    $scope.CountryName = "";
                    $scope.state = "";
                    $scope.stateName = "";
                    $scope.City = "";
                    $scope.cityName = "";
                    $scope.pincode = "";
                    $('#hid_publishingid').val("");
                    $('#btnSubmit').html("Submit");
                    angular.element(document.getElementById('angularid')).scope().GeogList();
                    angular.element(document.getElementById('angularid')).scope().getAllPublishingCompany();
                }
             }
            {
              //  $scope.name = "";
            }
        });
        blockUI.stop();
    }

    $scope.getAllPublishingCompany = function () {
        var getList = AJService.GetDataFromAPI("PublishingCompanyMaster/GetAllPublishingCompany", null);
        getList.then(function (PublishingCompany) {
            $scope.PublishingCompanyList = PublishingCompany.data;
        }, function () {
            alert('Error in getting PublishingCompany List');
        });
    }

    $scope.DeletePublishingCompany = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var publisherCompanyData = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this Publishing Company detail! ",
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
                     // call API to fetch temp Department list basis on the recordid
                     var publisherCompanyStatus = AJService.PostDataToAPI('PublishingCompanyMaster/PublishingCompanyDelete', publisherCompanyData);
                     publisherCompanyStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                             blockUI.stop();
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             angular.element(document.getElementById('angularid')).scope().GeogList();
                             angular.element(document.getElementById('angularid')).scope().getAllPublishingCompany();
                             blockUI.stop();
                         }

                     });
                 }

             });
        }
        else {
            SweetAlert.swal("Error!", "Record is not Deleted", "", "error");
        }
        blockUI.stop();
    }

    $scope.EditPublishingCompany = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var PublishingCompanyData = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            blockUI.start();
            // call API to fetch temp product type list basis on the FlatId
            var pubCompanyStatus = AJService.PostDataToAPI('PublishingCompanyMaster/PublishingCompany', PublishingCompanyData); 
            pubCompanyStatus.then(function (msg) {
                if (msg != null) {
                 
                    //  $scope.producttype = msg.data.typeName
                    $scope.companyName = msg.data.CompanyName;
                    $scope.contactPerson = msg.data.ContactPerson;
                    $scope.Address = msg.data.Address;
                    $scope.Phone = msg.data.Phone;
                    $scope.Mobile = msg.data.Mobile;
                    $scope.webSite = msg.data.Website;
                    $scope.Email = msg.data.Email;
                    $scope.pincode = msg.data.Pincode;
                    $scope.Country = msg.data.CountryId;
                     
                    setTimeout(function () {
                        $scope.getCountryStatesValue(msg.data.CountryId);
                        $scope.State = msg.data.Stateid;

                    }, 100);
                    setTimeout(function () {
                        $scope.getStateCitiesValue(msg.data.Stateid);
                        $scope.City = msg.data.Cityid;

                    }, 150);
                     $('#btnSubmit').html("Update");
                     $('#hid_publishingid').val(msg.data.Id);
                    blockUI.stop();
                }
                else {
                    SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                    blockUI.stop();
                }

            });
        }
    }
    
    //$scope.requiredField = "Y";
});