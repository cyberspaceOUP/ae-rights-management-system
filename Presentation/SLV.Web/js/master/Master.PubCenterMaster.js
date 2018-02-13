app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {
    app.expandControllerTopSearch($scope, AJService, $window);

    /******************** Start CountryStateCity Dropdown Control********************/

    app.expandControllerA($scope, AJService, $window);
    $scope.NomineeCountryReq = false;
    $scope.NomineeStateReq = false;
    $scope.NomineeCityReq = false;
    $scope.NomineepincodeReq = false;

    $('#NomineeCountry').attr("disabled", true);

    $('#NomineeState').attr("disabled", true);


    $('[name=Nomineecity]').attr("disabled", true);
    
    $("#Nomineepincode").attr("disabled", true);
    



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

    //GetAll PublishingCompany (DDL bind)
    $scope.getAllPublishingCompany = function () {

        var getList = AJService.GetDataFromAPI("PublishingCompanyMaster/GetAllPublishingCompany", null);
        getList.then(function (PublishingCompany) {
            $scope.PublishingCompanyList = PublishingCompany.data;
        }, function () {
         //   alert('Error in getting PublishingCompany List');
        });
    }

    //Fill PubCenterMaster 
    $scope.fillPubCenterMaster = function (PubCenterObj) {
        var PubCenter = PubCenterObj.data.PubCenterM;

        var PubCenterCountryId = ($("#Nominee").find("#NomineeCountry")).find('option:selected').val();
        var PubCenterStateId = ($("#Nominee").find("#NomineeState")).find('option:selected').val();
        var PubCenterCityId = ($("#Nominee").find("#NomineeCity")).find('option:selected').val();
        var PubCenterPincode = ($("#Nominee").find("#Nomineepincode")).val();
    }

    //Insert/Update PubCenter
    $scope.AddPubCenter = function () {
        blockUI.start();

        var PubCenterMaster = {
            PublishingCompanyid: $('#PublishingCompany').val(),
            CenterName: $scope.CenterName,
            ContactPerson: $scope.contactPerson,
            PublishingCompanyDivision: $scope.PublishingCompanyDivision,
            Address: $scope.Address,
            CountryId: $scope.NomineeCountry,
            StateId: $('[name=NomineeState]').val(),
            Cityid: $('[name=Nomineecity]').val(),
            Pincode: $scope.Nomineepincode,
            Phone: $scope.Phone,
            Mobile: $scope.Mobile,
            Email: $scope.Email,
            Fax: $scope.Fax,
            Id: $('#hid_pubCenterId').val() == "" ? 0 : $('#hid_pubCenterId').val(),
            EnteredBy: $("#enterdBy").val()
        };

        var PubCenterStatus = AJService.PostDataToAPI('PubCenterMaster/InsertPubCenter', PubCenterMaster);
        PubCenterStatus.then(function (msg) {

            if (msg.data != "OK") {
                SweetAlert.swal("Error!", "Duplicate. already exists !", "", "error");
            }
            else {
                if ($('#hid_pubCenterId').val() != "") {
                    SweetAlert.swal('Updated successfully.', '', "success");
                }
                else {
                    SweetAlert.swal('Inserted successfully.', '', "success");
                }

                $('#PublishingCompany').val("");
                $scope.CenterName = "";
                $scope.contactPerson = "";
                $scope.PublishingCompanyDivision = "";
                $scope.Address = "";
                $scope.NomineeCountry = "";
                $scope.NomineeState = "";
                $scope.NomineeCity = "";
                $scope.Nomineepincode = "";
                $scope.Phone = "";
                $scope.Mobile = "";
                $scope.Email = "";
                $scope.Fax = "";
                $('#hid_pubCenterId').val("");
                $('#btnSubmit').html("Submit");

                angular.element(document.getElementById('angularid')).scope().getAllPubCenterList();
            }
        },

        function () {
            alert('Please validate details');
        });
        blockUI.stop();
    }

    //Edit PubCenter
    $scope.EditPubCenter = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var PubCenter = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            blockUI.start();
            // call API to fetch temp PubCenter list basis on the PubCenter Id
            var PubCenter = AJService.PostDataToAPI('PubCenterMaster/PubCenter', PubCenter);
            PubCenter.then(function (msg) {

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

                    $scope.PublishingCompany = msg.data.PublishingCompanyid;
                    $scope.CenterName = msg.data.CenterName;
                    $scope.contactPerson = msg.data.ContactPerson;
                    $scope.PublishingCompanyDivision = msg.data.PublishingCompanyDivision;
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
                    $scope.Phone = msg.data.Phone;
                    $scope.Mobile = msg.data.Mobile;
                    $scope.Email = msg.data.Email;
                    $scope.Fax = msg.data.Fax;

                    $('#btnSubmit').html("Update");
                    $('#hid_pubCenterId').val(msg.data.Id);


                    $scope.$scope.getPublishingCompanyList();

                    //$scope.getAllPublishingCompany();
                    blockUI.stop();
                }
                else {
                    SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                    blockUI.stop();
                }
            });
        }
    }

    //Delete PubCenter
    $scope.DeletePubCenter = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var PubCenter = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this Pub Center detail! ",
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
                     var PubCenterStatus = AJService.PostDataToAPI("PubCenterMaster/PubCenterDelete", PubCenter);
                     PubCenterStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             blockUI.stop();
                         }
                         {
                             angular.element(document.getElementById('angularid')).scope().getAllPubCenterList();
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
                $scope.AddPubCenter();
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    }

    //getAllPubCenterList
    $scope.getAllPubCenterList = function () {
        var GetPubCenter = AJService.GetDataFromAPI("PubCenterMaster/GetPubCenterList"); //?Id=" + $("#enterdBy").val()
        GetPubCenter.then(function (PubCenter) {
            $scope.PubCenterData = PubCenter.data;
        }, function () {
            //alert('Error in getting Pub Center list');
        });
    }

    //$scope.getPublishingCompanyName = function ()
    //{

    //    var GetPubCenter = AJService.GetDataFromAPI("PubCenterMaster/GetPubCenterList?Id=" + $("#enterdBy").val());
    //    GetPubCenter.then(function (PubCenter) {
    //        $scope.PubCenterData = PubCenter.data;
    //    }, function () {
    //        alert('Error in getting PubCenter list');
    //    });

    //    alert($scope.PublishingCompany);
    //}



    $scope.getPublishingCompanyName = function () {
       
     
            // initialize variable for fetching data 
            var PublishingCompanyData = {
                Id: $scope.PublishingCompany
               
            };
           
            // call API to fetch temp product type list basis on the FlatId
            var pubCompanyStatus = AJService.PostDataToAPI('PublishingCompanyMaster/PublishingCompany', PublishingCompanyData);
            pubCompanyStatus.then(function (msg) {
                if (msg != null) {

                    //  $scope.producttype = msg.data.typeName
                    //$scope.companyName = msg.data.CompanyName;
                    //$scope.contactPerson = msg.data.ContactPerson;
                    $scope.Address = msg.data.Address;
                    $scope.Phone = msg.data.Phone;
                    $scope.Mobile = msg.data.Mobile;
                    $scope.webSite = msg.data.Website;
                    $scope.Email = msg.data.Email;
                    $scope.Nomineepincode = msg.data.Pincode;
                    $scope.NomineeCountry = msg.data.CountryId;

                   

                    setTimeout(function () {
                        $scope.getCountryStatesNominee(msg.data.CountryId)
                        $scope.NomineeStateValue = msg.data.Stateid;

                    }, 100);

                    setTimeout(function () {
                        $scope.getStateCitiesNominee(msg.data.Stateid)
                        $scope.NomineeCityValue = msg.data.Cityid;

                    }, 150);
                   
                }
                else {
                    SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                    blockUI.stop();
                }

            });
        
    }


});