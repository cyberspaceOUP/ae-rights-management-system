
app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI, Scopes) {
    app.expandControllerTopSearch($scope, AJService, $window);
    app.expandControllerA($scope, AJService, $window);
    //var URL = document.getElementById("fileUpload").path; //"/RMS/Common/deletedocument";
    //alert(URL);


    $scope.BenificaryHide = false;

    $scope.NomineeEmailReq = false;
    $scope.NomineeNameReq = false;
    $scope.NomineeRelationReq = false;
    $scope.NomineeAddressReq = false;
    $scope.NomineeMobileReq = false;

    $scope.NomineeCountryReq = false;
    $scope.NomineeStateReq = false;
    $scope.NomineeCityReq = false;
    $scope.NomineepincodeReq = false;


    $scope.BeneficiaryNameReq = false;
    $scope.BeneficiaryRelationReq = false;
    $scope.BeneficiaryAddressReq = false;
    $scope.BeneficiaryEmailReq = false;
    $scope.BeneficiaryMobileReq = false;
    $scope.BeneficiaryAccountNoReq = false;

    $scope.BeneficiaryBankNameReq = false;
    $scope.BeneficiaryBranchNameReq = false;

    $scope.BeneficiaryCountryReq = false;

    $scope.BeneficiaryStateReq = false;
    $scope.BeneficiaryCityReq = false;

    $scope.BeneficiarypincodeReq = false;




    $('#hid_NomineeDoc').val(1);





   




    $scope.AddAuthor = function () {

        //FileNameArray = $('#dropZone0').find('.fileNameClass');
        //var array = [];
        ////  $("input[class=fileNameClass]").each(function () {
        //FileNameArray.each(function () {
        //    array.push(
        //        $(this).val()
        //    );
        //});

        var array = [];
        var Array1 = [];
        var Array2 = [];
        if ($('#hid_Uploads').val() != "") {
            var _FileName = $('#hid_Uploads').val().split(",")
            var Index = 0
            var k = 0;
            $('.dropZone').each(function () {

                array = [];
                var FileName = "";
                var _ttlFile = $(this).find('.fileNameClass').length;
                for (var i = 0; i < _ttlFile; i++) {
                    array[i] = $($(this).find('.fileNameClass')[i]).val();
                    FileName = FileName + _FileName[Index] + ",";
                    Index++;
                }
                if (k == 0) {
                    $('#hid_UploadsFile1').val(FileName);
                    Array1 = array;
                }
                else {
                    $('#hid_UploadsFile2').val(FileName);
                    Array2 = array;
                }
                k++;
            })
        }

        blockUI.start();





        var mstr_OtherCountry = ($("#Authorgeo").find("#CountryName")).find('option:selected').text();
        var mstr_OtherState = ($("#Authorgeo").find("#stateName")).find('option:selected').text();
        var mstr_OtherCity = ($("#Authorgeo").find("#cityName")).find('option:selected').text();
        var mstr_PinCode = ($("#Authorgeo").find("#pincode")).val();


        var mstr_AffiliationOtherCountry = ($("#Affiliation").find("#CountryName")).find('option:selected').text();
        var mstr_AffiliationOtherState = ($("#Affiliation").find("#stateName")).find('option:selected').text();
        var mstr_AffiliationOtherCity = ($("#Affiliation").find("#cityName")).find('option:selected').text();
        var mstr_AffiliationPinCode = $scope.userForm.Affpincode.$modelValue;






        var mstr_AffCountry = ($("#Affiliation").find("#AffCountry")).find('option:selected').val();
        var mstr_AffState = ($("#Affiliation").find("#AffState")).find('option:selected').val();
        var mstr_AffCity = ($("#Affiliation").find("#city")).find('option:selected').val();

        var mstr_NomineeName = $scope.userForm.NomineeName.$modelValue;
        var mstr_NomineeRelation = $scope.userForm.NomineeRelation.$modelValue;
        var mstr_NomineeAddress = $scope.userForm.NomineeAddress.$modelValue;
        var mstr_NomineeCountryId = ($("#Nominee").find("#NomineeCountry")).find('option:selected').val();
        var mstr_NomineeStateId = ($("#Nominee").find("#NomineeState")).find('option:selected').val();
        var mstr_NomineeCityId = ($("#Nominee").find("#city")).find('option:selected').val();

        var mstr_NomineeOtherCountry = mstr_NomineeOtherCountry;
        var mstr_NomineeOtherState = mstr_NomineeOtherState;
        var mstr_NomineeOtherCity = mstr_NomineeOtherCity;
        var mstr_NomineePinCode = mstr_NomineePinCode;

        var mstr_NomineeEmail = $scope.userForm.NomineeEmail.$modelValue;
        var mstr_NomineePhone = $scope.userForm.NomineePhone.$modelValue;
        var mstr_NomineeMobile = $scope.userForm.NomineeMobile.$modelValue;
        var mstr_NomineeFax = $scope.userForm.NomineeFax.$modelValue;
        var mstr_NomineePanNo = $scope.userForm.NomineePanNo.$modelValue;


        var mstr_NomineeOtherCountry = ($("#Nominee").find("#CountryName")).find('option:selected').text();
        var mstr_NomineeOtherState = ($("#Nominee").find("#stateName")).find('option:selected').text();
        var mstr_NomineeOtherCity = ($("#Nominee").find("#cityName")).find('option:selected').text();
        var mstr_NomineePinCode = ($("#Nominee").find("#Nomineepincode")).val();


        if (mstr_NomineeOtherCountry == "") {
            mstr_NomineeOtherCountry = null
        }
        if (mstr_NomineeOtherState == "") {
            mstr_NomineeOtherState = null
        }
        if (mstr_NomineeOtherCity == "") {
            mstr_NomineeOtherCity = null
        }
        if (mstr_NomineePinCode == "") {
            mstr_NomineePinCode = null
        }




        if (mstr_AffCountry == "") {
            mstr_AffCountry = null
        }
        if (mstr_AffState == "") {
            mstr_AffState = null
        }
        if (mstr_AffCity == "") {
            mstr_AffCity = null
        }




        if (mstr_OtherCountry == "") {
            mstr_OtherCountry = null
        }
        if (mstr_OtherState == "") {
            mstr_OtherState = null
        }
        if (mstr_OtherCity == "") {
            mstr_OtherCity = null
        }
        if (mstr_PinCode == "") {
            mstr_PinCode = null
        }


        if (mstr_AffiliationOtherCountry == "") {
            mstr_AffiliationOtherCountry = null
        }
        if (mstr_AffiliationOtherState == "") {
            mstr_AffiliationOtherState = null
        }
        if (mstr_AffiliationOtherCity == "") {
            mstr_AffiliationOtherCity = null
        }
        if (mstr_AffiliationPinCode == "") {
            mstr_AffiliationPinCode = null
        }




        if ($('[type="checkbox"]').is(":checked")) {

            var mstr_BeneficiaryName = ($scope.userForm.FirstName.$modelValue == undefined ? '' : $scope.userForm.FirstName.$modelValue) + " " + ($scope.userForm.LastName.$modelValue == undefined ? '' : $scope.userForm.LastName.$modelValue);

            var mstr_BeneficiaryAddress = $scope.userForm.Address.$modelValue;
            var mstr_BeneficiaryCountryId = $('#Country').val() //$scope.userForm.Country.$modelValue;
            var mstr_BeneficiaryStateId = $scope.userForm.State.$modelValue;;
            var mstr_BeneficiaryCityId = $scope.userForm.city.$modelValue;;

            var mstr_BeneficiaryOtherCountry = mstr_OtherCountry;
            var mstr_BeneficiaryOtherState = mstr_OtherState;
            var mstr_BeneficiaryOtherCity = mstr_OtherCity;
            var mstr_BeneficiaryPinCode = mstr_PinCode;

            var mstr_BeneficiaryEmail = $scope.userForm.Email.$modelValue;
            var mstr_BeneficiaryPhone = $scope.userForm.Phone.$modelValue;
            var mstr_BeneficiaryMobile = $scope.userForm.Mobile.$modelValue;
            var mstr_BeneficiaryFax = $scope.userForm.Fax.$modelValue;
            var mstr_BeneficiaryPanNo = $scope.userForm.PANNo.$modelValue;

            var mstr_BeneficiaryAccountNo = $scope.userForm.AccountNo.$modelValue;
            var mstr_BeneficiaryBankName = $scope.userForm.BankName.$modelValue;
            var mstr_BeneficiaryBranchName = $scope.userForm.BranchName.$modelValue;
            var mstr_BeneficiaryIFCICode = $scope.userForm.IFSECode.$modelValue;

        }
        else {

            //var mstr_BeneficiaryOtherCountrydata = ($("#Beneficiary").find("#CountryName")).find('option:selected').text();
            //var mstr_BeneficiaryOtherStatedata = ($("#Beneficiary").find("#stateName")).find('option:selected').text();
            //var mstr_BeneficiaryOtherCitydata = ($("#Beneficiary").find("#cityName")).find('option:selected').text();
            //var mstr_BeneficiaryPinCodedata = ($("#Beneficiary").find("#Beneficiarypincode")).val();


            var mstr_BeneficiaryOtherCountrydata = $scope.userForm.BeneficiaryCountryName.$modelValue;
            var mstr_BeneficiaryOtherStatedata = $scope.userForm.BeneficiarystateName.$modelValue;
            var mstr_BeneficiaryOtherCitydata = $scope.userForm.BeneficiarycityName.$modelValue;
            var mstr_BeneficiaryPinCodedata = $scope.userForm.Beneficiarypincode.$modelValue;

            //var mstr_BeneficiaryOtherCountrydata = ($("#Beneficiary").find("#CountryName")).find('option:selected').text();
            //var mstr_BeneficiaryOtherStatedata = ($("#Beneficiary").find("#stateName")).find('option:selected').text();
            //var mstr_BeneficiaryOtherCitydata = ($("#Beneficiary").find("#cityName")).find('option:selected').text();
            //var mstr_BeneficiaryPinCodedata = ($("#Beneficiary").find("#Beneficiarypincode")).val();



            if (mstr_BeneficiaryOtherCountrydata == "") {
                mstr_BeneficiaryOtherCountrydata = null
            }
            if (mstr_BeneficiaryOtherStatedata == "") {
                mstr_BeneficiaryOtherStatedata = null
            }
            if (mstr_BeneficiaryOtherCitydata == "") {
                mstr_BeneficiaryOtherCitydata = null
            }
            if (mstr_BeneficiaryPinCodedata == "") {
                mstr_BeneficiaryPinCodedata = null
            }


            var mstr_BeneficiaryName = $scope.userForm.BeneficiaryName.$modelValue;
            var mstr_BeneficiaryAddress = $scope.userForm.BeneficiaryAddress.$modelValue;
            var mstr_BeneficiaryCountryId = $scope.userForm.BeneficiaryCountry.$modelValue;
            var mstr_BeneficiaryStateId = $scope.userForm.BeneficiaryState.$modelValue;
            var mstr_BeneficiaryCityId = $scope.userForm.Beneficiarycity.$modelValue;

            var mstr_BeneficiaryOtherCountry = mstr_BeneficiaryOtherCountrydata
            var mstr_BeneficiaryOtherState = mstr_BeneficiaryOtherStatedata;
            var mstr_BeneficiaryOtherCity = mstr_BeneficiaryOtherCitydata;
            var mstr_BeneficiaryPinCode = mstr_BeneficiaryPinCodedata;


            var mstr_BeneficiaryEmail = $scope.userForm.BeneficiaryEmail.$modelValue;

               var mstr_BeneficiaryPhone = $scope.userForm.BeneficiaryPhone.$modelValue;
            var mstr_BeneficiaryMobile = $scope.userForm.BeneficiaryMobile.$modelValue;
            var mstr_BeneficiaryFax = $scope.userForm.BeneficiaryFax.$modelValue;
            var mstr_BeneficiaryAccountNo = $scope.userForm.BeneficiaryAccountNo.$modelValue;

            var mstr_BeneficiaryPanNo = $scope.userForm.BeneficiaryPanNo.$modelValue
            var mstr_BeneficiaryBankName = $scope.userForm.BeneficiaryBankName.$modelValue;


            var mstr_BeneficiaryBranchName = $scope.userForm.BeneficiaryBranchName.$modelValue
            var mstr_BeneficiaryIFCICode = $scope.userForm.BeneficiaryIFSECode.$modelValue;

            var mstr_BeneficiaryRelation = $scope.userForm.BeneficiaryRelation.$modelValue



        }
        var mstr_DateOfBirth = $('#DateOfBirth').val();
        var mstr_DeathDate = $('#DeathDate').val();

        if (mstr_DeathDate == "") {
            mstr_DeathDate = null
        }
        else {

            var RequestDate = mstr_DeathDate;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            mstr_DeathDate = yy + "/" + mm + "/" + dd;
        }

        if (mstr_DateOfBirth == "") {
            mstr_DateOfBirth = null
        } else {

            var RequestDate = mstr_DateOfBirth;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            mstr_DateOfBirth = yy + "/" + mm + "/" + dd;
        }


        var Author = {
            Type: $scope.userForm.Type.$modelValue,
            FirstName: $scope.userForm.FirstName.$modelValue,
            ResidencyStatus: $scope.userForm.ResidencyStatus.$modelValue,
            Email: $scope.userForm.Email.$modelValue,
            Mobile: $scope.userForm.Mobile.$modelValue,

            DeathDate: mstr_DeathDate,
            // DeathDate: ($('#DeathDate').val() != "" ? $('#DeathDate').val() : null),
            // DeathDate: $('#DeathDate').val(),
            IFSECode: $scope.userForm.IFSECode.$modelValue,
            Code: $scope.userForm.Code.$modelValue,
            LastName: $scope.userForm.LastName.$modelValue,
            Address: $scope.userForm.Address.$modelValue,
            Phone: $scope.userForm.Phone.$modelValue,
            Fax: $scope.userForm.Fax.$modelValue,
            PANNo: $scope.userForm.PANNo.$modelValue,
            BankName: $scope.userForm.BankName.$modelValue,
            AccountNo: $scope.userForm.AccountNo.$modelValue,
            AdharCardNo: $scope.userForm.AdharCardNo.$modelValue,

            AuthorSAPCode: $scope.userForm.AuthorSAPCode.$modelValue,

            DocumentName: Array1,
            UploadFile: $("#hid_UploadsFile1").val(),
            //  DocumentName: input.slice(0, -1),

            CountryId: ($("#Authorgeo").find("#Country")).find('option:selected').val(),
            StateId: ($("#Authorgeo").find("#state")).find('option:selected').val(),
            CityId: ($("#Authorgeo").find("#city")).find('option:selected').val(),


            OtherCountry: mstr_OtherCountry,
            OtherState: mstr_OtherState,
            OtherCity: mstr_OtherCity,

            PinCode: mstr_PinCode,

            BranchName: $scope.userForm.BranchName.$modelValue,
            //DateOfBirth: $scope.userForm.DateOfBirth.$modelValue,

            DateOfBirth: mstr_DateOfBirth,

            // DateOfBirth: $('#DateOfBirth').val(),

            InstituteCompanyName: $scope.userForm.InstituteCompanyName.$modelValue,
            AffiliationDepartment: $scope.userForm.AffiliationDepartment.$modelValue,
            AffiliationDesignation: $scope.userForm.AffiliationDesignation.$modelValue,
            AffiliationAddress: $scope.userForm.AffiliationAddress.$modelValue,
            AffiliationPhone: $scope.userForm.AffiliationPhone.$modelValue,
            AffiliationWebSite: $scope.userForm.AffiliationWebSite.$modelValue,
            AffiliationEmail: $scope.userForm.AffiliationEmail.$modelValue,


            //AffiliationCountryId: ($("#Affiliation").find("#AffCountry")).find('option:selected').val(),
            //AffiliationStateId: ($("#Affiliation").find("#AffState")).find('option:selected').val(),
            //AffiliationCityId: ($("#Affiliation").find("#city")).find('option:selected').val(),




            AffiliationCountryId: mstr_AffCountry,
            AffiliationStateId: mstr_AffState,
            AffiliationCityId: mstr_AffCity,



            AffiliationOtherCountry: mstr_AffiliationOtherCountry,
            AffiliationOtherState: mstr_AffiliationOtherState,
            AffiliationOtherCity: mstr_AffiliationOtherCity,


            AffiliationPinCode: mstr_AffiliationPinCode,
            //AffiliationPhone: $scope.userForm.AffiliationPhone.$modelValue,

            // AffiliationPhone: $('[name=AffiliationPhone]').val(),

            BeneficiaryName: mstr_BeneficiaryName,
            BeneficiaryAddress: mstr_BeneficiaryAddress,
            BeneficiaryPhone: mstr_BeneficiaryPhone,
            BeneficiaryBankName: mstr_BeneficiaryBranchName,
            BeneficiaryRelation: $scope.userForm.BeneficiaryRelation.$modelValue,
            BeneficiaryEmail: mstr_BeneficiaryEmail,
            BeneficiaryFax: mstr_BeneficiaryFax,

            BeneficiaryAccountNo: mstr_BeneficiaryAccountNo,
            BeneficiaryPanNo: mstr_BeneficiaryPanNo,
            BeneficiaryBranchName: mstr_BeneficiaryBankName,
            BeneficiaryMobile: mstr_BeneficiaryMobile,
            BeneficiaryIFSECode: mstr_BeneficiaryIFCICode,


            BeneficiaryCountryId: mstr_BeneficiaryCountryId,
            BeneficiaryStateId: mstr_BeneficiaryStateId,
            BeneficiaryCityId: mstr_BeneficiaryCityId,

            BeneficiaryOtherCountry: mstr_BeneficiaryOtherCountry,
            BeneficiaryOtherState: mstr_BeneficiaryOtherState,
            BeneficiaryOtherCity: mstr_BeneficiaryOtherCity,

            BeneficiaryPinCode: mstr_BeneficiaryPinCode,



            NomineeName: mstr_NomineeName,
            NomineeAddress: mstr_NomineeAddress,
            NomineeRelation: mstr_NomineeRelation,
            NomineeEmail: mstr_NomineeEmail,
            NomineeMobile: mstr_NomineeMobile,
            NomineePhone: mstr_NomineePhone,
            NomineeFax: mstr_NomineeFax,
            NomineePanNo: mstr_NomineePanNo,


            NomineeCountryId: mstr_NomineeCountryId,
            NomineeStateId: mstr_NomineeStateId,
            NomineeCityId: mstr_NomineeCityId,

            NomineeOtherCountry: mstr_NomineeOtherCountry,
            NomineeOtherState: mstr_NomineeOtherState,
            NomineeOtherCity: mstr_NomineeOtherCity,

            NomineePinCode: mstr_NomineePinCode,

            Remark: $scope.userForm.Remark.$modelValue,

            NomineeDocumentName : Array2,
            NomineeUploadFile: $("#hid_UploadsFile2").val(),

            NomineeAccountNo: $scope.userForm.NomineeAccountNo.$modelValue,
            NomineeBranchName: $scope.userForm.NomineeBranchName.$modelValue,
            NomineeBankName: $scope.userForm.NomineeBankName.$modelValue,
            NomineeIFSECode: $scope.userForm.NomineeIFSECode.$modelValue,



            EnteredBy: $("#enterdBy").val(),
            Id: $('#hid_Authid').val() == "" ? 0 : $('#hid_Authid').val(),

            DocumentId: $("#hid_DocumentID").val() == "" ? 0 : $('#hid_DocumentID').val(),

            NomineeDocumentId: $("#hid_NomineeDocumentId").val() == "" ? 0 : $('#hid_NomineeDocumentId').val(),

        };


        var AuthorStatus = AJService.PostDataToAPI('Author/InsertAuthor', Author);


        AuthorStatus.then(function (msg) {
            
            if (msg.data.status == "PANDuplicate")
            {
                SweetAlert.swal("Mesage", "PAN already exist !", "warning");
            }
            else if (msg.data.status ==  "AccountNoDuplicate")
            {
                SweetAlert.swal("Mesage", "Account No. already exist !", "warning");
            }

            else if (msg.data.status == "OK") {
                if ($('#hid_Authid').val() != "") {
                    
                    if (typeof $('#hid_updateid').val() != "undefined") {
                        if ($('#hid_updateid').val() != "") {
                            SweetAlert.swal({
                                title: "Updated successfully.",
                                text: "",
                                type: "success"
                            },
                   function () {
                       $('#hid_updateid').val("");
                       $("#theModal").parent().remove();
                       $('.modal-backdrop').remove();
                       blockUI.stop();
                   });
                        }
                    }
                    else {

                        SweetAlert.swal({
                            title: "Updated successfully.",
                            text: "",
                            type: "success"
                        },
                        function () {
                            $window.location.href = '../Master/AuthorMaster?viewId=' + $('#hid_Authid').val();
                            blockUI.stop();
                        });
                                               

                    }


                }

                else {

                    SweetAlert.swal({
                        title: "Insert successfully.",
                        text: 'Author Code : ' + msg.data.Author_CodeValue + '',
                        type: "success"
                    },
                    function () {
                        $window.location.href = $window.location.href;
                        $window.location.href = '../Master/AuthorMaster?viewId=' + msg.data.AuthorIdId;
                        blockUI.stop();
                    });

                }
            }
            else {
                SweetAlert.swal("Error!", "There is some problem. !", "error");
            }
        },


        function () {
            alert('Please validate details');
        });
        blockUI.stop();
    }


    $scope.DeleteAuthor = function (Id) {

        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var AuthorData = {
                Id: Id
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this Author detail! ",
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
                     var AuthorDataStatus = AJService.PostDataToAPI('Author/AuthorDelete', AuthorData);
                     AuthorDataStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             blockUI.stop();
                         }

                         angular.element(document.getElementById('angularid')).scope().GetAuthorList();
                         //{
                         //    angular.element(document.getElementById('angularid')).scope().GetDepartmentList();
                         //}
                     });
                 }

             });
        }
        else {
            SweetAlert.swal("Error!", "Record is not Deleted", "error");
        }
        blockUI.stop();
    }


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
            $scope.sates = null;
        }, function () {
            //alert('Error in getting Geographical list');
        });
    }


    $scope.getCountryStates = function () {

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
                //alert('Error in getting Geographical list');
            });
        }
    }
    $scope.getStateCities = function () {
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
            //alert('Error in getting Geographical list');
        });

    }



    $scope.getCountryStatesAff = function (Id) {

        var GeogType = {
            geogtype: "state",
            parentid: Id,
        };
        if ($.trim($("#Country option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.OthersNot = false;
            $scope.OthersYes = true;
            $scope.OtherCountry = true;
            $scope.CityValue = [];
        }
        else {
            $scope.OthersNot = true;
            $scope.OthersYes = false;
            $scope.OtherCountry = false;
            $scope.CityValue = [];
            var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
            GetgeogList.then(function (GetgeogList) {
                $scope.StateValue = GetgeogList.data;
                $scope.CityValue = [];
            }, function () {
                //alert('Error in getting Geographical list');
            });
        }
    }
    $scope.getStateCitiesAff = function (Id) {
        var GeogType = {
            geogtype: "city",
            parentid: Id,

        };
        if ($.trim($("#state option:selected").html()).toLowerCase().indexOf("others") > -1) {
            //$scope.OthersNot = false;
            //$scope.OthersYes = true;

            $('.othersCityHide').css("display", "none");
            $scope.OthersStateYes = true;
            $scope.CityValue = [];
        }

        var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
        GetgeogList.then(function (GetgeogList) {
            $scope.CityValue = GetgeogList.data;
        }, function () {
            //alert('Error in getting Geographical list');
        });

    }





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
                //alert('Error in getting Geographical list');
            });
        }
    }
    $scope.getStateCitiesNominee = function (Id) {
        var GeogType = {
            geogtype: "city",
            parentid: Id,

        };
        if ($.trim($("#state option:selected").html()).toLowerCase().indexOf("others") > -1) {
            //$scope.OthersNot = false;
            //$scope.OthersYes = true;

            $('.othersCityHide').css("display", "none");
            $scope.OthersStateYes = true;
            $scope.NomineeCityList = [];
        }

        var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
        GetgeogList.then(function (GetgeogList) {
            $scope.NomineeCityList = GetgeogList.data;
        }, function () {
            //alert('Error in getting Geographical list');
        });

    }




    $scope.getCountryStatesBenceficiary = function (Id) {

        var GeogType = {
            geogtype: "state",
            parentid: Id,
        };
        if ($.trim($("#Country option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.OthersNot = false;
            $scope.OthersYes = true;
            $scope.OtherCountry = true;
            $scope.BeneficiaryCityList = [];
        }
        else {
            $scope.OthersNot = true;
            $scope.OthersYes = false;
            $scope.OtherCountry = false;
            $scope.BeneficiaryCityList = [];
            var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
            GetgeogList.then(function (GetgeogList) {
                $scope.BeneficiarysatesList = GetgeogList.data;
                $scope.BeneficiaryCityList = [];
            }, function () {
                //alert('Error in getting Geographical list');
            });
        }
    }
    $scope.getStateCitiesBenceficiary = function (Id) {
        var GeogType = {
            geogtype: "city",
            parentid: Id,

        };
        if ($.trim($("#state option:selected").html()).toLowerCase().indexOf("others") > -1) {
            //$scope.OthersNot = false;
            //$scope.OthersYes = true;

            $('.othersCityHide').css("display", "none");
            $scope.OthersStateYes = true;
            $scope.BeneficiaryCityList = [];
        }

        var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
        GetgeogList.then(function (GetgeogList) {
            $scope.BeneficiaryCityList = GetgeogList.data;
        }, function () {
            //alert('Error in getting Geographical list');
        });

    }




    $scope.getCountryStatesVlaue = function (Id) {
      
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
                //alert('Error in getting Geographical list');
            });
        }
    }



    //$scope.showState = function () {
    //    if ($.trim($("#Country option:selected").html()).toLowerCase().indexOf("others") > -1) {
    //        $scope.State.css("display", "none");
    //        $scope.cities = [];
    //    }
    //}

    //$scope.ChangeCitiesCities = function () {
    //    if ($.trim($("#city option:selected").html()).toLowerCase().indexOf("others") > -1) {
    //        $scope.otherCities = true;
    //    }
    //    else {
    //        $scope.otherCities = false;

    //    }
    //}


    $scope.submitForm = function () {
        //   debugger;
        var errorDiv;
        var errormsg = '';
        $scope.msg = "";

        FileNameArray = $('#dropZone0').find('.fileNameClass');
        var array = [];
        //  $("input[class=fileNameClass]").each(function () {
        if (FileNameArray.length > 0) {
            FileNameArray.each(function () {
                array.push($(this).val());

                for (i = 0; i < array.length; i++) {
                    if (array[i] == "") {
                        errorDiv = document.getElementById("fileid");
                        errorDiv.innerHTML = "Please enter file name";
                        errormsg = "Please enter file name";
                        $scope.userForm.$valid = false;
                        $('.nav-tabs').find('li').removeClass("active");
                        $($('.nav-tabs').find('li')[0]).addClass("active")
                        $('#Author').removeClass("fade");
                        $('#Author').addClass("fade in");
                        $('#Author').addClass("active");
                        $('#Affiliation').removeClass("active");
                        $('#Beneficiary').removeClass("active");
                        $('#Nominee').removeClass("active");
                    }
                    else {
                        errorDiv = document.getElementById("fileid");
                        errorDiv.innerHTML = "";
                        errormsg = "";
                        $scope.userForm.$valid = true;
                    }
                }

            });
        }
        else {
            errorDiv = document.getElementById("fileid");
            errorDiv.innerHTML = "";
            errormsg = "";
            $scope.userForm.$valid = true;
        }


        if ($.trim($('.note-editor').find('.note-editable').html()) == '') {
            $scope.descmsg = "Please enter description";
            //   $scope.userForm.$valid = false;

        }



        if ($scope.userForm.Type.$modelValue == null) {
            $scope.submitted = true;
            $('.nav-tabs').find('li').removeClass("active");
            $($('.nav-tabs').find('li')[0]).addClass("active")
            $('#Author').removeClass("fade");
            $('#Author').addClass("fade in");
            $('#Author').addClass("active");
            $('#Affiliation').removeClass("active");
            $('#Beneficiary').removeClass("active");
            $('#Nominee').removeClass("active");
            return;
        }



        //if ($scope.userForm.LastName.$modelValue == null) {
        //    $scope.submitted = true;
        //    $('.nav-tabs').find('li').removeClass("active");
        //    $($('.nav-tabs').find('li')[0]).addClass("active")
        //    $('#Author').removeClass("fade");
        //    $('#Author').addClass("fade in");
        //    $('#Author').addClass("active");
        //    $('#Affiliation').removeClass("active");
        //    $('#Beneficiary').removeClass("active");
        //    $('#Nominee').removeClass("active");
        //    return;
        //}

        if ($scope.userForm.Address.$modelValue == null) {
            $scope.submitted = true;
            $('.nav-tabs').find('li').removeClass("active");
            $($('.nav-tabs').find('li')[0]).addClass("active")
            $('#Author').removeClass("fade");
            $('#Author').addClass("fade in");
            $('#Author').addClass("active");
            $('#Affiliation').removeClass("active");
            $('#Beneficiary').removeClass("active");
            $('#Nominee').removeClass("active");
            return;
        }

        if ($scope.userForm.ResidencyStatus.$modelValue == null) {
            $scope.submitted = true;
            $('.nav-tabs').find('li').removeClass("active");
            $($('.nav-tabs').find('li')[0]).addClass("active")
            $('#Author').removeClass("fade");
            $('#Author').addClass("fade in");
            $('#Author').addClass("active");
            $('#Affiliation').removeClass("active");
            $('#Beneficiary').removeClass("active");
            $('#Nominee').removeClass("active");
            return;
        }

       
        if ($('#Country').val() != "")
        {
           
            setTimeout(function () {
                $($("select[Id*=Country]")[0]).closest(".form-group").removeClass("has-error")
                $($("select[Id*=Country]")[0]).next().find("p").removeClass("ng-show").addClass("ng-hide");
            }, 150)
           
        }

        if ($('#Country').val() =="")
        {
            $scope.userForm.Country.$modelValue == null;

        if ($scope.userForm.Country.$modelValue == null) {
            $scope.submitted = true;
            $('.nav-tabs').find('li').removeClass("active");
            $($('.nav-tabs').find('li')[0]).addClass("active")
            $('#Author').removeClass("fade");
            $('#Author').addClass("fade in");
            $('#Author').addClass("active");
            $('#Affiliation').removeClass("active");
            $('#Beneficiary').removeClass("active");
            $('#Nominee').removeClass("active");
            return;
        }
        }
        

        if ($scope.userForm.State.$modelValue == null) {
            $scope.submitted = true;
            $('.nav-tabs').find('li').removeClass("active");
            $($('.nav-tabs').find('li')[0]).addClass("active")
            $('#Author').removeClass("fade");
            $('#Author').addClass("fade in");
            $('#Author').addClass("active");
            $('#Affiliation').removeClass("active");
            $('#Beneficiary').removeClass("active");
            $('#Nominee').removeClass("active");
            return;
        }

        if ($scope.userForm.city.$modelValue == null) {
            $scope.submitted = true;
            $('.nav-tabs').find('li').removeClass("active");
            $($('.nav-tabs').find('li')[0]).addClass("active")
            $('#Author').removeClass("fade");
            $('#Author').addClass("fade in");
            $('#Author').addClass("active");
            $('#Affiliation').removeClass("active");
            $('#Beneficiary').removeClass("active");
            $('#Nominee').removeClass("active");
            return;
        }

        if ($scope.userForm.pincode.$modelValue == null) {
            $scope.submitted = true;
            $('.nav-tabs').find('li').removeClass("active");
            $($('.nav-tabs').find('li')[0]).addClass("active")
            $('#Author').removeClass("fade");
            $('#Author').addClass("fade in");
            $('#Author').addClass("active");
            $('#Affiliation').removeClass("active");
            $('#Beneficiary').removeClass("active");
            $('#Nominee').removeClass("active");
            return;
        }


        if ($scope.userForm.Email.$modelValue == null) {
            $scope.submitted = true;
            $('.nav-tabs').find('li').removeClass("active");
            $($('.nav-tabs').find('li')[0]).addClass("active")
            $('#Author').removeClass("fade");
            $('#Author').addClass("fade in");
            $('#Author').addClass("active");
            $('#Affiliation').removeClass("active");
            $('#Beneficiary').removeClass("active");
            $('#Nominee').removeClass("active");
            return;
        }


        if ($scope.userForm.Phone.$modelValue == null && $scope.userForm.Mobile.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[0]).addClass("active")
                $('#Author').removeClass("fade");
                $('#Author').addClass("fade in");
                $('#Author').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Beneficiary').removeClass("active");
                $('#Nominee').removeClass("active");
                return;

        }
        //if ($scope.userForm.Phone.$modelValue == null) {
        //    $scope.submitted = true;
        //    $('.nav-tabs').find('li').removeClass("active");
        //    $($('.nav-tabs').find('li')[0]).addClass("active")
        //    $('#Author').removeClass("fade");
        //    $('#Author').addClass("fade in");
        //    $('#Author').addClass("active");
        //    $('#Affiliation').removeClass("active");
        //    $('#Beneficiary').removeClass("active");
        //    $('#Nominee').removeClass("active");
        //    return;
        //}

        //if ($scope.userForm.Mobile.$modelValue == null) {
        //    $scope.submitted = true;
        //    $('.nav-tabs').find('li').removeClass("active");
        //    $($('.nav-tabs').find('li')[0]).addClass("active")
        //    $('#Author').removeClass("fade");
        //    $('#Author').addClass("fade in");
        //    $('#Author').addClass("active");
        //    $('#Affiliation').removeClass("active");
        //    $('#Beneficiary').removeClass("active");
        //    $('#Nominee').removeClass("active");
        //    return;
        //}



        if ($scope.PANNOReqired == true) {
            if ($scope.userForm.PANNo.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[0]).addClass("active")
                $('#Author').removeClass("fade");
                $('#Author').addClass("fade in");
                $('#Author').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Beneficiary').removeClass("active");
                $('#Nominee').removeClass("active");
                return;
            }
        }


        if ($scope.AccountNoReqired == true) {
            if ($scope.userForm.AccountNo.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[0]).addClass("active")
                $('#Author').removeClass("fade");
                $('#Author').addClass("fade in");
                $('#Author').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Beneficiary').removeClass("active");
                $('#Nominee').removeClass("active");
                return;
            }
        }


        if ($scope.BankNameReq == true) {
            if ($scope.userForm.BankName.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[0]).addClass("active")
                $('#Author').removeClass("fade");
                $('#Author').addClass("fade in");
                $('#Author').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Beneficiary').removeClass("active");
                $('#Nominee').removeClass("active");
                return;
            }
        }

        if ($scope.BranchNameReq == true) {
            if ($scope.userForm.BranchName.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[0]).addClass("active")
                $('#Author').removeClass("fade");
                $('#Author').addClass("fade in");
                $('#Author').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Beneficiary').removeClass("active");
                $('#Nominee').removeClass("active");
                return;
            }
        }



        if ($scope.userForm.Type.$modelValue == 'Individual') {
            if ($('#DateOfBirth').val() == "") {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[0]).addClass("active")
                $('#Author').removeClass("fade");
                $('#Author').addClass("fade in");
                $('#Author').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Beneficiary').removeClass("active");
                $('#Nominee').removeClass("active");
                return;
            }
        }



        if ($scope.IFSECodeReq == true) {
            if ($scope.userForm.IFSECode.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[0]).addClass("active")
                $('#Author').removeClass("fade");
                $('#Author').addClass("fade in");
                $('#Author').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Beneficiary').removeClass("active");
                $('#Nominee').removeClass("active");
                return;
            }
        }


        if ($scope.BenificaryHide == true) {
            if ($scope.userForm.BeneficiaryName.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[2]).addClass("active")
                $('#Beneficiary').removeClass("fade");
                $('#Beneficiary').addClass("fade in");
                $('#Beneficiary').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Author').removeClass("active");
                $('#Nominee').removeClass("active");
                return;
            }


            if ($scope.userForm.BeneficiaryRelation.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[2]).addClass("active")
                $('#Beneficiary').removeClass("fade");
                $('#Beneficiary').addClass("fade in");
                $('#Beneficiary').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Author').removeClass("active");
                $('#Nominee').removeClass("active");
                return;
            }


            if ($scope.userForm.BeneficiaryAddress.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[2]).addClass("active")
                $('#Beneficiary').removeClass("fade");
                $('#Beneficiary').addClass("fade in");
                $('#Beneficiary').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Author').removeClass("active");
                $('#Nominee').removeClass("active");
                return;
            }


            if ($scope.userForm.BeneficiaryCountry.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[2]).addClass("active")
                $('#Beneficiary').removeClass("fade");
                $('#Beneficiary').addClass("fade in");
                $('#Beneficiary').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Author').removeClass("active");
                $('#Nominee').removeClass("active");
                return;
            }

            if ($scope.userForm.BeneficiaryState.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[2]).addClass("active")
                $('#Beneficiary').removeClass("fade");
                $('#Beneficiary').addClass("fade in");
                $('#Beneficiary').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Author').removeClass("active");
                $('#Nominee').removeClass("active");
                return;
            }

            if ($scope.userForm.Beneficiarycity.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[2]).addClass("active")
                $('#Beneficiary').removeClass("fade");
                $('#Beneficiary').addClass("fade in");
                $('#Beneficiary').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Author').removeClass("active");
                $('#Nominee').removeClass("active");
                return;
            }

            if ($scope.userForm.Beneficiarypincode.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[2]).addClass("active")
                $('#Beneficiary').removeClass("fade");
                $('#Beneficiary').addClass("fade in");
                $('#Beneficiary').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Author').removeClass("active");
                $('#Nominee').removeClass("active");
                return;
            }

            if ($scope.userForm.BeneficiaryEmail.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[2]).addClass("active")
                $('#Beneficiary').removeClass("fade");
                $('#Beneficiary').addClass("fade in");
                $('#Beneficiary').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Author').removeClass("active");
                $('#Nominee').removeClass("active");
                return;
            }


            if ($scope.userForm.BeneficiaryMobile.$modelValue == null && $scope.userForm.BeneficiaryPhone.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[2]).addClass("active")
                $('#Beneficiary').removeClass("fade");
                $('#Beneficiary').addClass("fade in");
                $('#Beneficiary').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Author').removeClass("active");
                $('#Nominee').removeClass("active");
                return;
            }


            if ($scope.userForm.ResidencyStatus.$modelValue == "Non-Resident")
            {
            if ($scope.userForm.BeneficiaryAccountNo.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[2]).addClass("active")
                $('#Beneficiary').removeClass("fade");
                $('#Beneficiary').addClass("fade in");
                $('#Beneficiary').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Author').removeClass("active");
                $('#Nominee').removeClass("active");
                return;
            }

            if ($scope.userForm.BeneficiaryBranchName.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[2]).addClass("active")
                $('#Beneficiary').removeClass("fade");
                $('#Beneficiary').addClass("fade in");
                $('#Beneficiary').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Author').removeClass("active");
                $('#Nominee').removeClass("active");
                return;
            }
            if ($scope.userForm.BeneficiaryBankName.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[2]).addClass("active")
                $('#Beneficiary').removeClass("fade");
                $('#Beneficiary').addClass("fade in");
                $('#Beneficiary').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Author').removeClass("active");
                $('#Nominee').removeClass("active");
                return;
            }

            if ($scope.userForm.BeneficiaryIFSECode.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[2]).addClass("active")
                $('#Beneficiary').removeClass("fade");
                $('#Beneficiary').addClass("fade in");
                $('#Beneficiary').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Author').removeClass("active");
                $('#Nominee').removeClass("active");
                return;
            }
            }

            if ($scope.userForm.ResidencyStatus.$modelValue == "Resident") {
                if ($scope.userForm.BeneficiaryPanNo.$modelValue == null) {
                    $scope.submitted = true;
                    $('.nav-tabs').find('li').removeClass("active");
                    $($('.nav-tabs').find('li')[2]).addClass("active")
                    $('#Beneficiary').removeClass("fade");
                    $('#Beneficiary').addClass("fade in");
                    $('#Beneficiary').addClass("active");
                    $('#Affiliation').removeClass("active");
                    $('#Author').removeClass("active");
                    $('#Nominee').removeClass("active");
                    return;

                }

            }

           
            
        }

        if ($scope.userForm.NomineeName.$modelValue != null)
        {
            
            $scope.NomineeCountryReq = true ;
            $scope.NomineeStateReq = true;
            $scope.NomineeCityReq = true;
            $scope.NomineepincodeReq = true;

         if ($scope.userForm.NomineeName.$modelValue == null) {
            $scope.submitted = true;
            $('.nav-tabs').find('li').removeClass("active");
            $($('.nav-tabs').find('li')[3]).addClass("active")
            $('#Nominee').removeClass("fade");
            $('#Nominee').addClass("fade in");
            $('#Nominee').addClass("active");
            $('#Affiliation').removeClass("active");
            $('#Author').removeClass("active");
            $('#Beneficiary').removeClass("active");
            return;
        }

        if ($scope.userForm.NomineeRelation.$modelValue == null) {
            $scope.submitted = true;
            $('.nav-tabs').find('li').removeClass("active");
            $($('.nav-tabs').find('li')[3]).addClass("active")
            $('#Nominee').removeClass("fade");
            $('#Nominee').addClass("fade in");
            $('#Nominee').addClass("active");
            $('#Affiliation').removeClass("active");
            $('#Author').removeClass("active");
            $('#Beneficiary').removeClass("active");
            return;
        }

        if ($scope.userForm.NomineeAddress.$modelValue == null) {
            $scope.submitted = true;
            $('.nav-tabs').find('li').removeClass("active");
            $($('.nav-tabs').find('li')[3]).addClass("active")
            $('#Nominee').removeClass("fade");
            $('#Nominee').addClass("fade in");
            $('#Nominee').addClass("active");
            $('#Affiliation').removeClass("active");
            $('#Author').removeClass("active");
            $('#Beneficiary').removeClass("active");
            return;
        }


        if ($scope.userForm.NomineeCountry.$modelValue == null) {
            $scope.submitted = true;
            $('.nav-tabs').find('li').removeClass("active");
            $($('.nav-tabs').find('li')[3]).addClass("active")
            $('#Nominee').removeClass("fade");
            $('#Nominee').addClass("fade in");
            $('#Nominee').addClass("active");
            $('#Affiliation').removeClass("active");
            $('#Author').removeClass("active");
            $('#Beneficiary').removeClass("active");
            return;
        }

        if ($('[name=NomineeState]').val() == "")
        {

            if ($scope.userForm.NomineeState.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[3]).addClass("active")
                $('#Nominee').removeClass("fade");
                $('#Nominee').addClass("fade in");
                $('#Nominee').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Author').removeClass("active");
                $('#Beneficiary').removeClass("active");
                return;
            }
        }
        if ($('[name=Nomineecity]').val() == "")
        {
            if ($scope.userForm.Nomineecity.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[3]).addClass("active")
                $('#Nominee').removeClass("fade");
                $('#Nominee').addClass("fade in");
                $('#Nominee').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Author').removeClass("active");
                $('#Beneficiary').removeClass("active");
                return;
            }
        }

       

        if ($scope.userForm.Nomineepincode.$modelValue == null) {
            $scope.submitted = true;
            $('.nav-tabs').find('li').removeClass("active");
            $($('.nav-tabs').find('li')[3]).addClass("active")
            $('#Nominee').removeClass("fade");
            $('#Nominee').addClass("fade in");
            $('#Nominee').addClass("active");
            $('#Affiliation').removeClass("active");
            $('#Author').removeClass("active");
            $('#Beneficiary').removeClass("active");
            return;
        }

        if ($scope.userForm.NomineeEmail.$modelValue == null) {
            $scope.submitted = true;
            $('.nav-tabs').find('li').removeClass("active");
            $($('.nav-tabs').find('li')[3]).addClass("active")
            $('#Nominee').removeClass("fade");
            $('#Nominee').addClass("fade in");
            $('#Nominee').addClass("active");
            $('#Affiliation').removeClass("active");
            $('#Author').removeClass("active");
            $('#Beneficiary').removeClass("active");
            return;
        }

        if ($scope.userForm.NomineeMobile.$modelValue == null && $scope.userForm.NomineePhone.$modelValue == null) {
            $scope.submitted = true;
            $('.nav-tabs').find('li').removeClass("active");
            $($('.nav-tabs').find('li')[3]).addClass("active")
            $('#Nominee').removeClass("fade");
            $('#Nominee').addClass("fade in");
            $('#Nominee').addClass("active");
            $('#Affiliation').removeClass("active");
            $('#Author').removeClass("active");
            $('#Beneficiary').removeClass("active");
            return;
        }
        if ($scope.userForm.ResidencyStatus.$modelValue == "Resident") {

            if ($scope.userForm.NomineePanNo.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[3]).addClass("active")
                $('#Nominee').removeClass("fade");
                $('#Nominee').addClass("fade in");
                $('#Nominee').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Author').removeClass("active");
                $('#Beneficiary').removeClass("active");
                return;
            }
        }


        if ($scope.userForm.ResidencyStatus.$modelValue == "Non-Resident") {

            if ($scope.userForm.NomineeAccountNo.$modelValue == null) {
                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[3]).addClass("active")
                $('#Nominee').removeClass("fade");
                $('#Nominee').addClass("fade in");
                $('#Nominee').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Author').removeClass("active");
                $('#Beneficiary').removeClass("active");
                return;
            }
        }

        
        if ($('#hid_NomineeDoc').val() == "1")
        {
         if ($('[name=NomineeName]').val() != "") {
            FileNameArray = $('#dropZone1').find('.fileNameClass');

            if (FileNameArray[0] == null) {

                $scope.UploadContractReq = true;

                $scope.userForm.$valid = false;

                $scope.submitted = true;
                $('.nav-tabs').find('li').removeClass("active");
                $($('.nav-tabs').find('li')[3]).addClass("active")
                $('#Nominee').removeClass("fade");
                $('#Nominee').addClass("fade in");
                $('#Nominee').addClass("active");
                $('#Affiliation').removeClass("active");
                $('#Author').removeClass("active");
                $('#Beneficiary').removeClass("active");
                return;

            }
            else {

                if ($('.fileNameClass').val() == "") {
                    $scope.UploadContractReq = false;
                    $scope.UploadExcelfileNameReq = true;

                    $scope.userForm.$valid = false;

                    $scope.submitted = true;
                    $('.nav-tabs').find('li').removeClass("active");
                    $($('.nav-tabs').find('li')[3]).addClass("active")
                    $('#Nominee').removeClass("fade");
                    $('#Nominee').addClass("fade in");
                    $('#Nominee').addClass("active");
                    $('#Affiliation').removeClass("active");
                    $('#Author').removeClass("active");
                    $('#Beneficiary').removeClass("active");
                    return false;
                }
                else {
                    $scope.UploadContractReq = false;
                    $scope.UploadExcelfileReq = false;

                    $scope.userForm.$valid = true;
                }


            }
        }
        }
        
        }
        //$scope.userForm.$valid = true;

        $scope.submitted = true;
        if ($scope.userForm.$valid) {
            if ($scope.userForm.$valid) {
                $scope.AddAuthor();
                $scope.userForm.$setPristine();
                $scope.submitted = false;
                return;
            }
        }

    };



    $scope.EditAuthorData = function (Id) {

        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var AuthorData = {
                Id: Id
            };
            blockUI.start();
            // call API to fetch temp Department list basis on the FlatId

            var ExecutiveStatus = AJService.PostDataToAPI('Author/WebGetaAuthorById', AuthorData);


            ExecutiveStatus.then(function (msg) {
                if (msg != null) {

                    var NomineeCountry = undefined;
                    var NomineeState = undefined;
                    var NomineeCity = undefined;
                    var e = 0;
                    var d = 0;
                    var docNames = '';
                    var Docurl = '';
                    $scope.Docurl = [];

                    if (msg.data.AuthorDocument.DocumentName != '') {

                        $scope.documentshow = true;
                        var docNames = msg.data.AuthorDocument.DocumentName.slice(',');
                        var DName = msg.data.AuthorDocument.DocumentName.slice(',');

                        var DId = msg.data.AuthorDocument.DocumentIds.slice(',');

                        var Docurl = msg.data.AuthorDocument.UploadFile.split(',');
                        //   $scope.Docurl = [];
                        for (var i = 0; i < Docurl.length - 1; i++) {
                            //for (var j = 0; j < docNames.length; j++) {   
                            for (var j = 0, k = 0; j < docNames.length && k < DId.length ; j++, k++) {
                                if (e == 0 && d == 0) {
                                    //  $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[j].toString(), DocId: DId[k].toString() });
                                    $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[j].toString(), DocId: DId[k].toString() });
                                    //  $scope.Docurl.push({ text: "ABC.DOC", name: "123456TEST", DocId: 10 });

                                    $("#hid_DocumentID").val($("#hid_DocumentID").val() + Docurl[i].toString() + ',');
                                }
                                else {
                                    $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[e].toString(), DocId: DId[d].toString() });
                                }

                                e = j + 1;
                                d = k + 1;
                                i = i + 1;

                            }



                        }

                    }
                    else if ($scope.documentshow == true) {
                        $scope.documentshow = false;
                    }


                    if (msg.data.NomineeAuthorDocument.NomineeDocumentName != null) {
                        var e1 = 0;
                        var d1 = 0;
                        var docNames1 = '';
                        var Docurl1 = '';
                        $scope.Docurl1 = [];

                        if (msg.data.NomineeAuthorDocument.NomineeDocumentName != '') {

                            $scope.Pendingdocumentshow = true;
                            var docNames1 = msg.data.NomineeAuthorDocument.NomineeDocumentName.slice(',');
                            var DName1 = msg.data.NomineeAuthorDocument.NomineeDocumentName.slice(',');

                            var DId1 = msg.data.NomineeAuthorDocument.NomineeDocumentIds.slice(',');

                            var Docurl1 = msg.data.NomineeAuthorDocument.NomineeUploadFile.split(',');
                            //   $scope.Docurl = [];
                            for (var i = 0; i < Docurl1.length - 1; i++) {
                                //for (var j = 0; j < docNames.length; j++) {   
                                for (var j = 0, k = 0; j < docNames1.length && k < DId1.length ; j++, k++) {
                                    if (e1 == 0 && d1 == 0) {
                                        //  $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[j].toString(), DocId: DId[k].toString() });
                                        $scope.Docurl1.push({ text1: Docurl1[i].toString(), name1: docNames1[j].toString(), DocId1: DId1[k].toString() });
                                        // $scope.Docurl1.push({ text: "ABC.DOC", name: "123456TEST", DocId: 10 });

                                        $("#hid_NomineeDocumentId").val($("#hid_NomineeDocumentId").val() + Docurl1[i].toString() + ',');
                                    }
                                    else {
                                        $scope.Docurl1.push({ text1: Docurl1[i].toString(), name1: docNames1[e1].toString(), DocId1: DId1[d1].toString() });
                                    }

                                    e1 = j + 1;
                                    d1 = k + 1;
                                    i = i + 1;

                                    $('#hid_NomineeDoc').val(0);
                                }



                            }

                        }
                        else if ($scope.Pendingdocumentshow == true) {
                            $scope.Pendingdocumentshow = false;
                        }
                    }



                    if (msg.data.AuthorList.AffiliationDepartment == null) {

                        msg.data.AuthorList.AffiliationDepartment = "";
                    }


                    if (msg.data.AuthorList.AffiliationCountryId == null) {

                        msg.data.AuthorList.AffiliationCountryId = "";
                    }
                    else {


                        $scope.AffCountry = msg.data.AuthorList.AffiliationCountryId;
                        $scope.getCountryStatesAff(msg.data.AuthorList.AffiliationCountryId);
                        $scope.AffStateValue = msg.data.AuthorList.AffiliationStateId;
                        // $scope.AffState = msg.data.AffiliationOtherState,
                        $scope.getStateCitiesAff(msg.data.AuthorList.AffiliationStateId);

                        $scope.AffCityValue = msg.data.AuthorList.AffiliationCityId;

                    }


                    if (msg.data.AuthorList.NomineeCountryId != null) {
                        NomineeCountry = msg.data.AuthorList.NomineeCountryId;
                    }
                    if (msg.data.AuthorList.NomineeStateId != null) {
                        $scope.getCountryStatesNominee(msg.data.AuthorList.NomineeCountryId);
                        NomineeState = msg.data.AuthorList.NomineeStateId;
                    }

                    if (msg.data.AuthorList.NomineeCityId != null) {

                        setTimeout(function () {
                            $scope.getStateCitiesNominee(msg.data.AuthorList.NomineeStateId);
                            NomineeCity = msg.data.AuthorList.NomineeCityId
                        }, 280)

                        
                    }

                    if (msg.data.AuthorList.PANNo != null) {
                        $scope.PANNOReqired = true;
                        $scope.AccountNoReqired = false;
                        $scope.AccountNoReqired = false;
                        $scope.BankNameReq = false;
                        $scope.BranchNameReq = false;
                        $scope.IFSECodeReq = false;
                    }
                    else {
                        $scope.PANNOReqired = false;


                        $scope.AccountNoReqired = true;
                        $scope.AccountNoReqired = true;
                        $scope.BankNameReq = true;
                        $scope.BranchNameReq = true;
                        $scope.IFSECodeReq = true;
                    }

                   



                    $scope.AuthorCode = msg.data.AuthorList.AuthorCode,
                     $scope.Type = msg.data.AuthorList.AuthorType,
                    $scope.FirstName = msg.data.AuthorList.FirstName,
                   $scope.LastName = msg.data.AuthorList.LastName,
                   $scope.Address = msg.data.AuthorList.Address,
                    $scope.ResidencyStatus = msg.data.AuthorList.ResidencyStatus,

                    $scope.AuthorSAPCode = msg.data.AuthorList.AuthorSAPCode,

                     setTimeout(function () {
                         $scope.getStateCities();

                         $scope.City = msg.data.AuthorList.CityId
                     }, 1500);




                    if (msg.data.AuthorList.BeneficiaryRelation != null) {
                        $scope.BenificaryHide = true;
                        $('[type="checkbox"]').prop("checked", false);
                    }
                    else {

                        $scope.BenificaryHide = false;
                        $('[type="checkbox"]').prop("checked", true);
                    }



                    $scope.Country = msg.data.AuthorList.CountryId,
                    $scope.getCountryStates();

                    $scope.State = msg.data.AuthorList.StateId,
                     $scope.getStateCities();

                    $scope.City = msg.data.AuthorList.CityId,


                     $scope.OtherState = msg.data.AuthorList.OtherState,

                      $scope.OtherCity = msg.data.AuthorList.OtherCity,
                    $scope.pincode = msg.data.AuthorList.PinCode,

                     $scope.Email = msg.data.AuthorList.Email,

                      $scope.Phone = msg.data.AuthorList.Phone,
                     $scope.Mobile = msg.data.AuthorList.Mobile,
                     $scope.Fax = msg.data.AuthorList.Fax,


                   

                      $scope.PANNo = msg.data.AuthorList.PANNo,

                   $scope.AdharCardNo = msg.data.AuthorList.AdharCardNo,
                       $scope.DateOfBirth = msg.data.AuthorList.DateOfBirth,
                      $scope.DeathDate = msg.data.AuthorList.DeathDate,
                     $scope.AccountNo = msg.data.AuthorList.AccountNo,
                     $scope.BankName = msg.data.AuthorList.BankName,
                      $scope.BranchName = msg.data.AuthorList.BranchName,
                     $scope.IFSECode = msg.data.AuthorList.IFSECode,
                       $scope.InstituteCompanyName = msg.data.AuthorList.InstituteCompanyName,
                      $scope.AffiliationDesignation = msg.data.AuthorList.AffiliationDesignation,

                         $scope.AffiliationDepartment = msg.data.AuthorList.AffiliationDepartment,

                     $scope.AffiliationAddress = msg.data.AuthorList.AffiliationAddress,


                      $scope.AffiliationOtherCountry = msg.data.AuthorList.AffiliationOtherCountry,

                     $scope.AffCountryName = msg.data.AuthorList.AffiliationOtherCity,
                           $scope.AffstateName = msg.data.AuthorList.AffiliationOtherState,
                           $scope.AffcityName = msg.data.AuthorList.AffiliationOtherCity,



                      $scope.Affpincode = msg.data.AuthorList.AffiliationPinCode,

                 $scope.AffiliationPhone = msg.data.AuthorList.AffiliationPhone,
                       $scope.AffiliationEmail = msg.data.AuthorList.AffiliationEmail,
                      $scope.AffiliationWebSite = msg.data.AuthorList.AffiliationWebSite,
                     $scope.BeneficiaryName = msg.data.AuthorList.BeneficiaryName,
                     $scope.BeneficiaryRelation = msg.data.AuthorList.BeneficiaryRelation,
                      $scope.BeneficiaryAddress = msg.data.AuthorList.BeneficiaryAddress,
                     $scope.BeneficiaryCountry = msg.data.AuthorList.BeneficiaryCountryId,
                     $scope.getCountryStatesBenceficiary(msg.data.AuthorList.BeneficiaryCountryId);
                    $scope.BeneficiaryState = msg.data.AuthorList.BeneficiaryStateId,

                    setTimeout(function () {
                        $scope.getStateCitiesBenceficiary(msg.data.AuthorList.BeneficiaryStateId);
                        $scope.BeneficiaryCity = msg.data.AuthorList.BeneficiaryCityId
                    }, 275)

                    $scope.getStateCitiesBenceficiary(msg.data.AuthorList.BeneficiaryStateId);
                    $scope.BeneficiaryCity = msg.data.AuthorList.BeneficiaryCityId,
                       $scope.BeneficiaryCountryName = msg.data.AuthorList.BeneficiaryOtherCountry,
                      
                     $scope.BeneficiarystateName = msg.data.AuthorList.BeneficiaryOtherState,
                  
                      $scope.BeneficiarycityName = msg.data.AuthorList.BeneficiaryOtherCity,
                    $scope.Beneficiarypincode = msg.data.AuthorList.BeneficiaryPinCode,
                       $scope.BeneficiaryEmail = msg.data.AuthorList.BeneficiaryEmail,
                      $scope.BeneficiaryPhone = msg.data.AuthorList.BeneficiaryPhone,
                     $scope.BeneficiaryMobile = msg.data.AuthorList.BeneficiaryMobile,
                     $scope.BeneficiaryFax = msg.data.AuthorList.BeneficiaryFax,
                      $scope.BeneficiaryPanNo = msg.data.AuthorList.BeneficiaryPanNo,



                       $scope.BeneficiaryAccountNo = msg.data.AuthorList.BeneficiaryAccountNo,
                       $scope.BeneficiaryBankName = msg.data.AuthorList.BeneficiaryBankName,
                      $scope.BeneficiaryBranchName = msg.data.AuthorList.BeneficiaryBranchName,
                     $scope.BeneficiaryIFSECode = msg.data.AuthorList.BeneficiaryIFSECode,
                     $scope.NomineeName = msg.data.AuthorList.NomineeName,
                      $scope.NomineeRelation = msg.data.AuthorList.NomineeRelation,
                     $scope.NomineeAddress = msg.data.AuthorList.NomineeAddress,



                       $scope.NomineeCountry = NomineeCountry,

                    $scope.NomineeStateValue = NomineeState,

                   setTimeout(function () {
                       $scope.NomineeStateValue = NomineeState,
                       $scope.NomineeCityValue = NomineeCity

                   }, 282)
                    $scope.NomineeCityValue = NomineeCity,
                    $scope.NomineeCountryName = msg.data.AuthorList.NomineeOtherCountry,

                   $scope.NomineestateName = msg.data.AuthorList.NomineeOtherState,

                  $scope.NomineecityName = msg.data.AuthorList.NomineeOtherCity,
                     $scope.Nomineepincode = msg.data.AuthorList.NomineePinCode,
                    $scope.NomineeEmail = msg.data.AuthorList.NomineeEmail,
                   $scope.NomineePhone = msg.data.AuthorList.NomineePhone,
                   $scope.NomineeMobile = msg.data.AuthorList.NomineeMobile,
                    $scope.NomineeFax = msg.data.AuthorList.NomineeFax,
                     $scope.NomineePanNo = msg.data.AuthorList.NomineePanNo,

                    $scope.Remark = msg.data.AuthorList.Remark,

                      $scope.NomineeAccountNo = msg.data.AuthorList.NomineeAccountNo,
                      $scope.NomineeBranchName = msg.data.AuthorList.NomineeBranchName,
                      $scope.NomineeBankName = msg.data.AuthorList.NomineeBankName,
                      $scope.NomineeIFSECode = msg.data.AuthorList.NomineeIFSECode,



                    $('#btnSubmit').html("Update");

                    // $('#hid_Execid').val(msg.data.Id);

                    if (msg.data.AuthorList.AffiliationCountryId == "" || msg.data.AuthorList.AffiliationCountryId == null) {

                        setTimeout(function () {
                            $('#AffState').find('option').css("display", "none");

                            $('[name=Affcity]').find('option').css("display", "none");
                        }, 2000);


                    }
                    else {
                        setTimeout(function () {
                            $('#AffState').find('option').css("display", "block");
                            $('[name=Affcity]').find('option').css("display", "block");
                        }, 2000);
                    }

                    if (msg.data.AuthorList.NomineeCountryId == null || msg.data.AuthorList.NomineeCountryId == "") {

                        setTimeout(function () {
                            $('#NomineeState').find('option').css("display", "none");
                            $('[name=Nomineecity]').find('option').css("display", "none");
                        }, 2000);
                    }
                    else {
                        setTimeout(function () {
                            $('#NomineeState').find('option').css("display", "block");
                            $('[name=Nomineecity]').find('option').css("display", "block");
                        }, 2000);
                    }
                    
                    blockUI.stop();

                }
                else {
                    SweetAlert.swal("Error!", "Error in system. Please try again", "error");
                    blockUI.stop();
                }

            });


        }
    }



    $scope.EditAuthorDataView = function (Id) {

        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var AuthorData = {
                Id: Id
            };
            blockUI.start();
            // call API to fetch temp Department list basis on the FlatId

            var ExecutiveStatus = AJService.PostDataToAPI('Author/ViewAuthorDetials', AuthorData);


            ExecutiveStatus.then(function (msg) {
                if (msg != null) {
                  
                
                    var NomineeCountry = undefined;
                    var NomineeState = undefined;
                    var NomineeCity = undefined;
                    var e = 0;
                    var d = 0;
                    var docNames = '';
                    var Docurl = '';
                    $scope.Docurl = [];

                    if (msg.data.AuthorDocument.DocumentName != '') {

                        $scope.documentshow = true;
                        var docNames = msg.data.AuthorDocument.DocumentName.slice(',');
                        var DName = msg.data.AuthorDocument.DocumentName.slice(',');

                        var DId = msg.data.AuthorDocument.DocumentIds.slice(',');

                        var Docurl = msg.data.AuthorDocument.UploadFile.split(',');
                        //   $scope.Docurl = [];
                        for (var i = 0; i < Docurl.length - 1; i++) {
                            //for (var j = 0; j < docNames.length; j++) {   
                            for (var j = 0, k = 0; j < docNames.length && k < DId.length ; j++, k++) {
                                if (e == 0 && d == 0) {
                                    //  $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[j].toString(), DocId: DId[k].toString() });
                                    $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[j].toString(), DocId: DId[k].toString() });
                                    //  $scope.Docurl.push({ text: "ABC.DOC", name: "123456TEST", DocId: 10 });

                                
                                }
                                else {
                                    $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[e].toString(), DocId: DId[d].toString() });
                                }

                                e = j + 1;
                                d = k + 1;
                                i = i + 1;

                            }



                        }

                    }
                    else if ($scope.documentshow == true) {
                        $scope.documentshow = false;
                    }


                    if (msg.data.NomineeAuthorDocument.NomineeDocumentName != null) {
                        var e1 = 0;
                        var d1 = 0;
                        var docNames1 = '';
                        var Docurl1 = '';
                        $scope.Docurl1 = [];

                        if (msg.data.NomineeAuthorDocument.NomineeDocumentName != '') {

                            $scope.Pendingdocumentshow = true;
                            var docNames1 = msg.data.NomineeAuthorDocument.NomineeDocumentName.slice(',');
                            var DName1 = msg.data.NomineeAuthorDocument.NomineeDocumentName.slice(',');

                            var DId1 = msg.data.NomineeAuthorDocument.NomineeDocumentIds.slice(',');

                            var Docurl1 = msg.data.NomineeAuthorDocument.NomineeUploadFile.split(',');
                            //   $scope.Docurl = [];
                            for (var i = 0; i < Docurl1.length - 1; i++) {
                                //for (var j = 0; j < docNames.length; j++) {   
                                for (var j = 0, k = 0; j < docNames1.length && k < DId1.length ; j++, k++) {
                                    if (e1 == 0 && d1 == 0) {
                                        //  $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[j].toString(), DocId: DId[k].toString() });
                                        $scope.Docurl1.push({ text1: Docurl1[i].toString(), name1: docNames1[j].toString(), DocId1: DId1[k].toString() });
                                        // $scope.Docurl1.push({ text: "ABC.DOC", name: "123456TEST", DocId: 10 });

                                     
                                    }
                                    else {
                                        $scope.Docurl1.push({ text1: Docurl1[i].toString(), name1: docNames1[e1].toString(), DocId1: DId1[d1].toString() });
                                    }

                                    e1 = j + 1;
                                    d1 = k + 1;
                                    i = i + 1;

                                  
                                }



                            }

                        }
                        else if ($scope.Pendingdocumentshow == true) {
                            $scope.Pendingdocumentshow = false;
                        }
                    }


                    if (msg.data._GetAuthorReport[0].OtherCountry != null) {
                        $scope.OtherCountry = msg.data._GetAuthorReport[0].OtherCountry
                        $scope.OtherCountryReq = true
                    }
                    else {
                        $scope.OtherCountryReq = false
                    }


                    if (msg.data._GetAuthorReport[0].OtherState != null) {
                        $scope.OtherState = msg.data._GetAuthorReport[0].OtherState
                        $scope.OtherStateReq = true
                    }
                    else {
                        $scope.OtherStateReq = false
                    }



                    if (msg.data._GetAuthorReport[0].OtherCity != null) {
                        $scope.OtherCity = msg.data._GetAuthorReport[0].OtherCity
                        $scope.OtherCityReq = true
                    }
                    else {
                        $scope.OtherCityReq = false
                    }



                    if (msg.data._GetAuthorReport[0].AffiliationOtherCountry != null) {
                        $scope.AffiliationOtherCountry = msg.data._GetAuthorReport[0].AffiliationOtherCountry
                        $scope.AffiliationOtherCountryReq = true
                    }
                    else {
                        $scope.AffiliationOtherCountryReq = false
                    }



                    if (msg.data._GetAuthorReport[0].AffiliationOtherState != null) {
                        $scope.AffiliationOtherState = msg.data._GetAuthorReport[0].AffiliationOtherState
                        $scope.AffiliationOtherStateReq = true
                    }
                    else {
                        $scope.AffiliationOtherStateReq = false
                    }


                    if (msg.data._GetAuthorReport[0].AffiliationOtherCity != null) {
                        $scope.AffiliationOtherCity = msg.data._GetAuthorReport[0].AffiliationOtherCity
                        $scope.AffiliationOtherCityReq = true
                    }
                    else {
                        $scope.AffiliationOtherCityReq = false
                    }


                    if (msg.data._GetAuthorReport[0].BeneficiaryOtherCountry != null) {
                        $scope.BeneficiaryOtherCountry = msg.data._GetAuthorReport[0].BeneficiaryOtherCountry
                        $scope.BeneficiaryOtherCountryReq = true
                    }
                    else {
                        $scope.BeneficiaryOtherCountryReq = false
                    }



                    if (msg.data._GetAuthorReport[0].BeneficiaryOtherState != null) {
                        $scope.BeneficiaryOtherState = msg.data._GetAuthorReport[0].BeneficiaryOtherState
                        $scope.BeneficiaryOtherStateReq = true
                    }
                    else {
                        $scope.BeneficiaryOtherStateReq = false
                    }

                    if (msg.data._GetAuthorReport[0].BeneficiaryOtherCity != null) {
                        $scope.BeneficiaryOtherCity = msg.data._GetAuthorReport[0].BeneficiaryOtherCity
                        $scope.BeneficiaryOtherCityReq = true
                    }
                    else {
                        $scope.BeneficiaryOtherCityReq = false
                    }


                    if (msg.data._GetAuthorReport[0].NomineeOtherCountry != null) {
                        $scope.NomineeOtherCountry = msg.data._GetAuthorReport[0].NomineeOtherCountry
                        $scope.NomineeOtherCountryReq = true
                    }
                    else {
                        $scope.NomineeOtherCountryReq = false
                    }

                    if (msg.data._GetAuthorReport[0].NomineeOtherState != null) {
                        $scope.NomineeOtherState = msg.data._GetAuthorReport[0].NomineeOtherState
                        $scope.NomineeOtherStateReq = true
                    }
                    else {
                        $scope.NomineeOtherStateReq = false
                    }

                    if (msg.data._GetAuthorReport[0].NomineeOtherCity != null) {
                        $scope.NomineeOtherCity = msg.data._GetAuthorReport[0].NomineeOtherCity
                        $scope.NomineeOtherCityReq = true
                    }
                    else {
                        $scope.NomineeOtherCityReq = false
                    }

                    $scope.AuthorCode = (msg.data._GetAuthorReport[0].AuthorCode == null ? '---' : msg.data._GetAuthorReport[0].AuthorCode)
                
                    $scope.Type = (msg.data._GetAuthorReport[0].Type == null ? '---' : msg.data._GetAuthorReport[0].Type)
                       
                    $scope.FirstName = (msg.data._GetAuthorReport[0].FirstName == null ? '---' : msg.data._GetAuthorReport[0].FirstName)
                   
                    $scope.LastName = (msg.data._GetAuthorReport[0].LastName == null ? '---' : msg.data._GetAuthorReport[0].LastName)

                    $scope.AuthorSAPCode = (msg.data._GetAuthorReport[0].AuthorSAPCode == null ? '---' : msg.data._GetAuthorReport[0].AuthorSAPCode)
                 
                        $scope.Address = (msg.data._GetAuthorReport[0].Address == null ? '---' : msg.data._GetAuthorReport[0].Address)
                     
                        $scope.ResidencyStatus = (msg.data._GetAuthorReport[0].ResidencyStatus == null ? '---' : msg.data._GetAuthorReport[0].ResidencyStatus)
                  
                        $scope.Country = (msg.data._GetAuthorReport[0].AuthorCountry == null ? '---' : msg.data._GetAuthorReport[0].AuthorCountry)
                     
                        $scope.State = (msg.data._GetAuthorReport[0].AuthorState == null ? '---' : msg.data._GetAuthorReport[0].AuthorState)
                        $scope.City = (msg.data._GetAuthorReport[0].AuthorCity == null ? '---' : msg.data._GetAuthorReport[0].AuthorCity)

                        $scope.PinCode = (msg.data._GetAuthorReport[0].PinCode == null ? '---' : msg.data._GetAuthorReport[0].PinCode)
                   
                        $scope.Email = (msg.data._GetAuthorReport[0].Email == null ? '---' : msg.data._GetAuthorReport[0].Email)

                        $scope.Phone = (msg.data._GetAuthorReport[0].Phone == null ? '---' : msg.data._GetAuthorReport[0].Phone)
                  
                        $scope.Mobile = (msg.data._GetAuthorReport[0].Mobile == null ? '---' : msg.data._GetAuthorReport[0].Mobile)
                    
                        $scope.Fax = (msg.data._GetAuthorReport[0].Fax == null ? '---' : msg.data._GetAuthorReport[0].Fax)

                        $scope.AdharCardNo = (msg.data._GetAuthorReport[0].AdharCardNo == null ? '---' : msg.data._GetAuthorReport[0].AdharCardNo)

                        $scope.DateOfBirth = (msg.data._GetAuthorReport[0].DateOfBirth == null ? '---' : msg.data._GetAuthorReport[0].DateOfBirth)

                        $scope.DeathDate = (msg.data._GetAuthorReport[0].DeathDate == null ? '---' : msg.data._GetAuthorReport[0].DeathDate)

                  
                        $scope.PANNo = (msg.data._GetAuthorReport[0].PANNo == null ? '---' : msg.data._GetAuthorReport[0].PANNo)

                        $scope.AccountNo = (msg.data._GetAuthorReport[0].AccountNo == null ? '---' : msg.data._GetAuthorReport[0].AccountNo)

                        $scope.BankName = (msg.data._GetAuthorReport[0].BankName == null ? '---' : msg.data._GetAuthorReport[0].BankName)

                        $scope.BranchName = (msg.data._GetAuthorReport[0].BranchName == null ? '---' : msg.data._GetAuthorReport[0].BranchName)

                        $scope.IFSECode = (msg.data._GetAuthorReport[0].IFSECode == null ? '---' : msg.data._GetAuthorReport[0].IFSECode)



                        $scope.InstituteCompanyName = (msg.data._GetAuthorReport[0].InstituteCompanyName == null ? '---' : msg.data._GetAuthorReport[0].InstituteCompanyName)

                        $scope.AffiliationDesignation = (msg.data._GetAuthorReport[0].AffiliationDesignation == null ? '---' : msg.data._GetAuthorReport[0].AffiliationDesignation)




                        if (msg.data._GetAuthorReport[0].AffiliationDepartment == null || msg.data._GetAuthorReport[0].AffiliationDepartment == "") {
                            $scope.AffiliationDepartment = "---"
                        }
                        else {
                            $scope.AffiliationDepartment = msg.data._GetAuthorReport[0].AffiliationDepartment;
                        }
                    
                       // $scope.AffiliationDepartment = (msg.data._GetAuthorReport[0].AffiliationDepartment == null ? '---' : msg.data._GetAuthorReport[0].AffiliationDepartment)

                        $scope.AffiliationCountry = (msg.data._GetAuthorReport[0].AffiliationCountry == null ? '---' : msg.data._GetAuthorReport[0].AffiliationCountry)


                 
                        $scope.AffiliationState = (msg.data._GetAuthorReport[0].AffiliationState == null ? '---' : msg.data._GetAuthorReport[0].AffiliationState)

                        $scope.AffiliationCity = (msg.data._GetAuthorReport[0].AffiliationCity == null ? '---' : msg.data._GetAuthorReport[0].AffiliationCity)

                 
                        $scope.AffiliationPinCode = (msg.data._GetAuthorReport[0].AffiliationPinCode == null ? '---' : msg.data._GetAuthorReport[0].AffiliationPinCode)

                        $scope.AffiliationAddress = (msg.data._GetAuthorReport[0].AffiliationAddress == null ? '---' : msg.data._GetAuthorReport[0].AffiliationAddress)

                        $scope.AffiliationPhone = (msg.data._GetAuthorReport[0].AffiliationPhone == null ? '---' : msg.data._GetAuthorReport[0].AffiliationPhone)

                        $scope.AffiliationEmail = (msg.data._GetAuthorReport[0].AffiliationEmail == null ? '---' : msg.data._GetAuthorReport[0].AffiliationEmail)

                        $scope.AffiliationWebSite = (msg.data._GetAuthorReport[0].AffiliationWebSite == null ? '---' : msg.data._GetAuthorReport[0].AffiliationWebSite)

                        $scope.BeneficiaryName = (msg.data._GetAuthorReport[0].BeneficiaryName == null ? '---' : msg.data._GetAuthorReport[0].BeneficiaryName)

                        $scope.BeneficiaryRelation = (msg.data._GetAuthorReport[0].BeneficiaryRelation == null ? '---' : msg.data._GetAuthorReport[0].BeneficiaryRelation)

                        $scope.BeneficiaryEmail = (msg.data._GetAuthorReport[0].BeneficiaryEmail == null ? '---' : msg.data._GetAuthorReport[0].BeneficiaryEmail)

                        $scope.BeneficiaryAddress = (msg.data._GetAuthorReport[0].BeneficiaryAddress == null ? '---' : msg.data._GetAuthorReport[0].BeneficiaryAddress)

                        $scope.BeneficiaryCountry = (msg.data._GetAuthorReport[0].BeneficiaryCountry == null ? '---' : msg.data._GetAuthorReport[0].BeneficiaryCountry)

                        $scope.BeneficiaryState = (msg.data._GetAuthorReport[0].BeneficiaryState == null ? '---' : msg.data._GetAuthorReport[0].BeneficiaryState)

                        $scope.BeneficiaryCity = (msg.data._GetAuthorReport[0].BeneficiaryCity == null ? '---' : msg.data._GetAuthorReport[0].BeneficiaryCity)

                        $scope.BeneficiaryPinCode = (msg.data._GetAuthorReport[0].BeneficiaryPinCode == null ? '---' : msg.data._GetAuthorReport[0].BeneficiaryPinCode)

                        $scope.BeneficiaryPhone = (msg.data._GetAuthorReport[0].BeneficiaryPhone == null ? '---' : msg.data._GetAuthorReport[0].BeneficiaryPhone)

                        $scope.BeneficiaryMobile = (msg.data._GetAuthorReport[0].BeneficiaryMobile == null ? '---' : msg.data._GetAuthorReport[0].BeneficiaryMobile)

                        $scope.BeneficiaryPanNo = (msg.data._GetAuthorReport[0].BeneficiaryPanNo == null ? '---' : msg.data._GetAuthorReport[0].BeneficiaryPanNo)

                        $scope.BeneficiaryAccountNo = (msg.data._GetAuthorReport[0].BeneficiaryAccountNo == null ? '---' : msg.data._GetAuthorReport[0].BeneficiaryAccountNo)

                        $scope.BeneficiaryBankName = (msg.data._GetAuthorReport[0].BeneficiaryBankName == null ? '---' : msg.data._GetAuthorReport[0].BeneficiaryBankName)

                        $scope.BeneficiaryBranchName = (msg.data._GetAuthorReport[0].BeneficiaryBranchName == null ? '---' : msg.data._GetAuthorReport[0].BeneficiaryBranchName)

                        $scope.BeneficiaryIFSECode = (msg.data._GetAuthorReport[0].BeneficiaryIFSECode == null ? '---' : msg.data._GetAuthorReport[0].BeneficiaryIFSECode)

                        $scope.BeneficiaryFax = (msg.data._GetAuthorReport[0].BeneficiaryFax == null ? '---' : msg.data._GetAuthorReport[0].BeneficiaryFax)


                        $scope.NomineeName = (msg.data._GetAuthorReport[0].NomineeName == null ? '---' : msg.data._GetAuthorReport[0].NomineeName)

                        $scope.NomineeRelation = (msg.data._GetAuthorReport[0].NomineeRelation == null ? '---' : msg.data._GetAuthorReport[0].NomineeRelation)

                        $scope.NomineeEmail = (msg.data._GetAuthorReport[0].NomineeEmail == null ? '---' : msg.data._GetAuthorReport[0].NomineeEmail)

                        $scope.NomineeAddress = (msg.data._GetAuthorReport[0].NomineeAddress == null ? '---' : msg.data._GetAuthorReport[0].NomineeAddress)

                        $scope.NomineeCountry = (msg.data._GetAuthorReport[0].NomineeCountry == null ? '---' : msg.data._GetAuthorReport[0].NomineeCountry)

                        $scope.NomineeState = (msg.data._GetAuthorReport[0].NomineeState == null ? '---' : msg.data._GetAuthorReport[0].NomineeState)

                        $scope.NomineeCity = (msg.data._GetAuthorReport[0].NomineeCity == null ? '---' : msg.data._GetAuthorReport[0].NomineeCity)

                        $scope.NomineePinCode = (msg.data._GetAuthorReport[0].NomineePinCode == null ? '---' : msg.data._GetAuthorReport[0].NomineePinCode)

                        $scope.NomineePhone = (msg.data._GetAuthorReport[0].NomineePhone == null ? '---' : msg.data._GetAuthorReport[0].NomineePhone)

                        $scope.NomineeMobile = (msg.data._GetAuthorReport[0].NomineeMobile == null ? '---' : msg.data._GetAuthorReport[0].NomineeMobile)







                        $scope.NomineePanNo = (msg.data._GetAuthorReport[0].NomineePanNo == null ? '---' : msg.data._GetAuthorReport[0].NomineePanNo)

                        $scope.NomineeFax = (msg.data._GetAuthorReport[0].NomineeFax == null ? '---' : msg.data._GetAuthorReport[0].NomineeFax)

                        $scope.NomineeAccountNo = (msg.data._GetAuthorReport[0].NomineeAccountNo == null ? '---' : msg.data._GetAuthorReport[0].NomineeAccountNo)

                        $scope.NomineeBranchName = (msg.data._GetAuthorReport[0].NomineeBranchName == null ? '---' : msg.data._GetAuthorReport[0].NomineeBranchName)

                        $scope.NomineeBankName = (msg.data._GetAuthorReport[0].NomineeBankName == null ? '---' : msg.data._GetAuthorReport[0].NomineeBankName)


                  

                        $scope.NomineeIFSECode = (msg.data._GetAuthorReport[0].NomineeIFSECode == null ? '---' : msg.data._GetAuthorReport[0].NomineeIFSECode)


                  

                    blockUI.stop();

                }
                else {
                    SweetAlert.swal("Error!", "Error in system. Please try again", "error");
                    blockUI.stop();
                }

            });


        }
    }


    if ($('#hid_ViewAuthor').val() != 0) {
      
        $scope.EditAuthorDataView($('#hid_ViewAuthor').val());
        $('#pageHeaddingAuthorView').css("display", "block");
        $('#FrmDisplay').css("display", "block");
        $('#FrmInsert').css("display", "none");

        $('.backToList').css("display", "block");

        $('#pageHeaddingAuthor').css("display", "none");
       
    }


    $scope.ShowNominee_Detials = function () {

        if ($('[type="checkbox"]').is(":checked")) {

            $scope.BenificaryHide = false;
            $scope.BeneficiaryNameReq = false;
            $scope.BeneficiaryRelationReq = false;
            $scope.BeneficiaryAddressReq = false;
            $scope.BeneficiaryEmailReq = false;
            $scope.BeneficiaryMobileReq = false;
            $scope.BeneficiaryAccountNoReq = false;

            $scope.BeneficiaryBankNameReq = false;
            $scope.BeneficiaryBranchNameReq = false;
            $scope.BeneficiaryCountryReq = false;

            $scope.BeneficiaryStateReq = false;
            $scope.BeneficiaryCityReq = false;

            $scope.BeneficiarypincodeReq = false;
            $scope.ReqBeneficiaryPhone = false;
        }
        else {

           

            $scope.BenificaryHide = true;

           // $scope.ReqBeneficiaryPhone = "!(BeneficiaryPhone.length || BeneficiaryMobile.length)";
           // $scope.BeneficiaryMobileReq = "!(BeneficiaryPhone.length || BeneficiaryMobile.length)";
           $scope.BeneficiaryNameReq = true;
            $scope.BeneficiaryRelationReq = true;
            $scope.BeneficiaryAddressReq = true;
            $scope.BeneficiaryEmailReq = true;
           
            $scope.BeneficiaryAccountNoReq = true;
          

            $scope.BeneficiaryBankNameReq = true;
            $scope.BeneficiaryBranchNameReq = true;

            $scope.BeneficiaryCountryReq = true;

            $scope.BeneficiaryStateReq = true;
            $scope.BeneficiaryCityReq = true;

            $scope.BeneficiarypincodeReq = true;


          
                //if ($('[name=BeneficiaryMobile]').val() == "" && $('[name=BeneficiaryPhone]').val() == "") {
                //    $scope.ReqBeneficiaryPhone = true;
                //    $scope.BeneficiaryMobileReq = true;

                //}
                //else if ($('[name=BeneficiaryMobile]').val() != "") {
                //    $scope.BeneficiaryMobileReq = false;
                //}
                //else if ($('[name=BeneficiaryPhone]').val() != "") {
                //    $scope.ReqBeneficiaryPhone = false;
                //}

           

           
        }

    }


    $scope.function_Resident = function () {
       
        $scope.PANNOReqired = true;

        $scope.AccountNoReqired = false;
        $scope.AccountNoReqired = false;
        $scope.BankNameReq = false;
        $scope.BranchNameReq = false;
        $scope.IFSECodeReq = false;
      
        $($('select[name*=Country]')[0]).find('option:not(:contains("India"))').css("display", "none")
        $($('select[name*=Country]')[0]).find('option:contains("India")').css("display", "block")


        //alert( $($('select[name*=Country]')[0]).find('option:contains("India")').attr('selected', 'selected'));
        setTimeout(function () {
            $($("select[Id*=Country]")[0]).closest(".form-group").removeClass("has-error");
            $($("select[Id*=Country]")[0]).next().find("p").removeClass("ng-show").addClass("ng-hide");


            $($('select[name*=Country]')[0]).find('option:contains("India")').attr('selected', 'selected');
            $scope.getCountryStatesVlaue($($('select[name*=Country]')[0]).find('option:contains("India")').attr('selected', 'selected').val());

        },200)

      

        $($('select[name*=Country]')[0]).val('');
        $($('select[name*=State]')[0]).val('');

        $($('select[name*=city]')[0]).val('');



    }

    $scope.function_NonResident = function () {
        $scope.PANNOReqired = false;

        $scope.AccountNoReqired = true;
        $scope.BankNameReq = true;
        $scope.BranchNameReq = true;
        $scope.IFSECodeReq = true;
        // $scope.GeogList();
        //
        $($('select[name*=Country]')[0]).find('option:contains("India")').css("display", "none")
        $($('select[name*=Country]')[0]).find('option:not(:contains("India"))').css("display", "block")

        if ($($('select[name*=Country]')[0]).find('option:contains("India")').text() == "India") {
            $($('select[name*=Country]')[0]).val('');
            $($('select[name*=State]')[0]).val('');
            $($('select[name*=State]')[0]).find('option:not(:contains("--Please Select--"))').css("display", "none")
            $($('select[name*=city]')[0]).val('');
            $($('select[name*=city]')[0]).find('option:not(:contains("--Please Select--"))').css("display", "none")

        }
        else {

            setTimeout(function () {
                $($('select[name*=Country]')[0]).find('option:contains("India")').attr('selected', 'selected');
                $scope.getCountryStatesVlaue($($('select[name*=Country]')[0]).find('option:contains("India")').attr('selected', 'selected').val());

            }, 200)


            $($('select[name*=State]')[0]).find('option:not(:contains("--Please Select--"))').css("display", "block")

            $($('select[name*=city]')[0]).find('option:not(:contains("--Please Select--"))').css("display", "block")

        }

        // $($('select[name*=Country]')[0]).find('option:(:contains("India"))').css("display", "none")
    }


    $scope.Clear = function () {

        $('[name=Type]').attr('checked', false);
        $scope.userForm.Type.$viewValue = ""

        $('[name=FirstName]').val("");

        $scope.userForm.FirstName.$viewValue = ""

        $('[name=LastName]').val("");

        $scope.userForm.LastName.$viewValue = ""

        $('[name=AuthorSAPCode]').val("");

        $scope.userForm.AuthorSAPCode.$viewValue = ""

        $('[name=Address]').val("");

        $scope.userForm.Address.$viewValue = ""

        $('[name=ResidencyStatus]').attr('checked', false);

        $scope.userForm.ResidencyStatus.$viewValue = 0


        $('[name=Country]').val("");

        $scope.userForm.Country.$viewValue = ""


        $('[name=State]').val("");

        $scope.userForm.State.$viewValue = ""

        $('[name=city]').val("");

        $scope.userForm.city.$viewValue = ""


        $('[name=CountryName]').val("");

        $scope.userForm.CountryName.$viewValue = ""


        $('[name=stateName]').val("");

        $scope.userForm.stateName.$viewValue = ""

        $('[name=cityName]').val("");

        $scope.userForm.cityName.$viewValue = ""


        $('[name=pincode]').val("");

        $scope.userForm.pincode.$viewValue = ""

        $('[name=Email]').val("");

        $scope.userForm.Email.$viewValue = ""

        $('[name=Phone]').val("");

        $scope.userForm.Phone.$viewValue = ""


        $('[name=Mobile]').val("");

        $scope.userForm.Mobile.$viewValue = ""

        $('[name=Fax]').val("");

        $scope.userForm.Fax.$viewValue = ""


        $('[name=PANNo]').val("");

        $scope.userForm.PANNo.$viewValue = ""

        $('[name=AdharCardNo]').val("");

        $scope.userForm.AdharCardNo.$viewValue = ""

        $('[name=DateOfBirth]').val("");

        $scope.userForm.DateOfBirth.$viewValue = ""


        $('[name=DeathDate]').val("");

        $scope.userForm.DeathDate.$viewValue = ""

        $('[name=AccountNo]').val("");

        $scope.userForm.AccountNo.$viewValue = ""


        $('[name=BankName]').val("");

        $scope.userForm.BankName.$viewValue = ""

        $('[name=BranchName]').val("");

        $scope.userForm.BranchName.$viewValue = ""

        $('[name=IFSECode]').val("");

        $scope.userForm.IFSECode.$viewValue = ""

        $('[name=Remark]').val("");

        $scope.userForm.Remark.$modelValue = ""


        $('[name=InstituteCompanyName]').val("");

        $scope.userForm.InstituteCompanyName.$viewValue = ""

        $('[name=AffiliationDesignation]').val("");

        $scope.userForm.AffiliationDesignation.$viewValue = ""

        $('[name=AffiliationDepartment]').val("");

        $scope.userForm.AffiliationDepartment.$viewValue = ""


        $('[name=AffiliationAddress]').val("");

        $scope.userForm.AffiliationAddress.$viewValue = ""

        $('[name=AffCountry]').val("");

        $scope.userForm.AffCountry.$viewValue = ""


        $('[name=AffState]').val("");

        $scope.userForm.AffState.$viewValue = ""

        $('[name=Affcity]').val("");

        $scope.userForm.Affcity.$viewValue = ""

        //$('[name=AffCountryName]').val("");

        //$scope.userForm.AffCountryName.$viewValue = ""


        //$('[name=AffstateName]').val("");

        //$scope.userForm.AffstateName.$viewValue = ""

        //$('[name=AffcityName]').val("");

        //$scope.userForm.AffcityName.$viewValue = ""


        $('[name=Affpincode]').val("");

        $scope.userForm.Affpincode.$viewValue = ""

        $('[name=AffiliationPhone]').val("");

        $scope.userForm.AffiliationPhone.$viewValue = ""

        $('[name=AffiliationWebSite]').val("");

        $scope.userForm.AffiliationWebSite.$viewValue = ""

        $('[name=AffiliationEmail]').val("");

        $scope.userForm.AffiliationEmail.$viewValue = ""



        $('[name=BeneficiaryName]').val("");

        $scope.userForm.BeneficiaryName.$viewValue = ""

        $('[name=BeneficiaryRelation]').val("");

        $scope.userForm.BeneficiaryRelation.$viewValue = ""

        $('[name=BeneficiaryAddress]').val("");

        $scope.userForm.BeneficiaryAddress.$viewValue = ""




        $('[name=BeneficiaryCountry]').val("");

        $scope.userForm.BeneficiaryCountry.$viewValue = ""


        $('[name=BeneficiaryState]').val("");

        $scope.userForm.BeneficiaryState.$viewValue = ""

        $('[name=Beneficiarycity]').val("");

        $scope.userForm.Beneficiarycity.$viewValue = ""

        //$('[name=BeneficiaryCountryName]').val("");

        //$scope.userForm.BeneficiaryCountryName.$viewValue = ""


        //$('[name=BeneficiarystateName]').val("");

        //$scope.userForm.BeneficiarystateName.$viewValue = ""
        //$('[name=BeneficiarycityName]').val("");

        //$scope.userForm.BeneficiarycityName.$viewValue = ""


        $('[name=Beneficiarypincode]').val("");

        $scope.userForm.Beneficiarypincode.$viewValue = ""

        $('[name=BeneficiaryEmail]').val("");

        $scope.userForm.BeneficiaryEmail.$viewValue = ""
        $('[name=BeneficiaryPhone]').val("");

        $scope.userForm.BeneficiaryPhone.$viewValue = ""

        $('[name=BeneficiaryMobile]').val("");

        $scope.userForm.BeneficiaryMobile.$viewValue = ""



        $('[name=BeneficiaryFax]').val("");

        $scope.userForm.BeneficiaryFax.$viewValue = ""
        $('[name=BeneficiaryPanNo]').val("");

        $scope.userForm.BeneficiaryPanNo.$viewValue = ""


        $('[name=BeneficiaryAccountNo]').val("");

        $scope.userForm.BeneficiaryAccountNo.$viewValue = ""

        $('[name=BeneficiaryBankName]').val("");

        $scope.userForm.BeneficiaryBankName.$viewValue = ""
        $('[name=BeneficiaryBranchName]').val("");

        $scope.userForm.BeneficiaryBranchName.$viewValue = ""

        $('[name=BeneficiaryIFSECode]').val("");

        $scope.userForm.BeneficiaryIFSECode.$viewValue = ""


        $('[name=NomineeName]').val("");

        $scope.userForm.NomineeName.$viewValue = ""

        $('[name=NomineeRelation]').val("");

        $scope.userForm.NomineeRelation.$viewValue = ""

        $('[name=NomineeAddress]').val("");

        $scope.userForm.NomineeAddress.$viewValue = ""




        $('[name=NomineeCountry]').val("");

        $scope.userForm.NomineeCountry.$viewValue = ""


        $('[name=NomineeState]').val("");

        $scope.userForm.NomineeState.$viewValue = ""

        $('[name=Nomineecity]').val("");

        $scope.userForm.Nomineecity.$viewValue = ""

        //$('[name=NomineeCountryName]').val("");

        //$scope.userForm.NomineeCountryName.$viewValue = ""


        //$('[name=NomineestateName]').val("");

        //$scope.userForm.NomineestateName.$viewValue = ""
        //$('[name=NomineecityName]').val("");

        //$scope.userForm.NomineecityName.$viewValue = ""


        $('[name=Nomineepincode]').val("");

        $scope.userForm.Nomineepincode.$viewValue = ""

        $('[name=NomineeEmail]').val("");

        $scope.userForm.NomineeEmail.$viewValue = ""
        $('[name=NomineePhone]').val("");

        $scope.userForm.NomineePhone.$viewValue = ""

        $('[name=NomineeMobile]').val("");

        $scope.userForm.NomineeMobile.$viewValue = ""



        $('[name=NomineeFax]').val("");

        $scope.userForm.NomineeFax.$viewValue = ""
        $('[name=NomineePanNo]').val("");

        $scope.userForm.NomineePanNo.$viewValue = ""

        $('#fileid').hide();
        $('.cstmProgressBar').hide();
        $('.uploadedFileName').hide();
        $scope.documentshow = false;
        $('#btn_Uploader').val("Select File");
        $('#dropZone0').css("height", "150px");
        $('#dropZone1').css("height", "150px");

        $scope.UploadExcelfileNameReq = false;
    }




    function getUrlVars() {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    }

    var URL = window.location.href;
    if (URL.indexOf("id") >= 0) {
        var id = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        if (id != "" && id != "undefined") {
            var Author_id = id[0].split('=')[1]
            if (URL.indexOf("&") >= 0) {
                var idValue = getUrlVars()["id"].split('%')[0];
                var Type = getUrlVars()["%20Type"].split('%')[0];
                $("#FrmInsert").css("display", "none");
                $("#pageHeaddingAuthor").css("display", "none");
                $("#FrmDisplay").css("display", "block");
                $("#pageHeaddingAuthorView").css("display", "block");

                if (id[0].split('=')[0] == "id") {
                    $("#hid_Authid").val(idValue);
                    $('#hid_UpdateValue').val(1);

                    $scope.EditAuthorDataView(idValue);

                    $('.backToList').css("display", "block");
                    $('.AuthorCodeValue').css("display", "block");
                }

            }
            else {
                var idValue = getUrlVars()["id"];
                $("#FrmInsert").css("display", "block");
                $("#pageHeaddingAuthor").css("display", "block");

                $("#FrmDisplay").css("display", "none");
                $("#pageHeaddingAuthorView").css("display", "none");
                if (id[0].split('=')[0] == "id") {
                    $("#hid_Authid").val(idValue);
                    $('#hid_UpdateValue').val(1);
                    $('.backToList').css("display", "block");
                    $('.AuthorCodeValue').css("display", "block");
                    $scope.EditAuthorData(idValue);
                }
            }

        }

    }

    else {
        $('#hid_UpdateValue').val(0);
    }


    $scope.RemoveDocumentById = function (docid, file) {

        //  alert($scope.NoticeBoard.NBId);
        var AuthorDocument = { Id: docid };
        var DeleteDocument = AJService.PostDataToAPI("Author/RemoveAuhtorDocument", AuthorDocument);

        DeleteDocument.then(function (msg) {
            if (msg.data != "OK") {
                SweetAlert.swal("Oops...", "Please retry!", "error");

            }
            else {


                var obj = {};
                obj.filename = file;
                $.ajax({
                    cache: false,
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    url: GlobalredirectPath + "/Common/deletedocument",
                    data: JSON.stringify(obj),
                    dataType: "json",
                    success: function (result) {
                        if (result == "Deleted") {
                            //var NoticeBoard = {
                            //    Id: $scope.NoticeBoard.NBId,
                            //    Published: false

                            //};
                            $scope.EditAuthorData($('#hid_Authid').val());

                        }
                        //   angular.element('#profileContainer').scope().$apply();
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                    }
                });


            }
        }, function () {

            SweetAlert.swal("Oops...", "Please retry!", "error");

        });
    }



    $scope.getAuthorDepartmentList = function () {
        var getAuthorDepartmentList = AJService.GetDataFromAPI("Author/getAuthorDepartmentList", null);
        getAuthorDepartmentList.then(function (AuhtorDepartment) {
            $scope.AuthorDepartmentList = AuhtorDepartment.data.query;
        }, function () {
            //alert('Error in getting Author Department list');
        });
    }



    $scope.RemoveDocumentLinkById = function (docid, file) {

        //  alert($scope.NoticeBoard.NBId);
        var AuthorDocument = { Id: docid, EnteredBy: $("#enterdBy").val() };
        var DeleteDocument = AJService.PostDataToAPI("Author/RemoveNomineeAuhtorDocument", AuthorDocument);

        DeleteDocument.then(function (msg) {
            if (msg.data != "OK") {
                SweetAlert.swal("Oops...", "Please retry!", "error");

            }
            else {


                var obj = {};
                obj.filename = file;
                $.ajax({
                    cache: false,
                    type: "POST",
                    contentType: 'application/json; charset=utf-8',
                    url: GlobalredirectPath + "/Common/deletedocument",
                    data: JSON.stringify(obj),
                    dataType: "json",
                    success: function (result) {
                        if (result == "Deleted") {
                          

                            setTimeout(function () {
                                $scope.EditAuthorData($('#hid_Authid').val());
                            },250)
                           

                        }

                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                    }
                });


            }
        }, function () {

            SweetAlert.swal("Oops...", "Please retry!", "error");

        });
    }


    $scope.SetDateOfBirth = function (datetext1) {

        
        $scope.DateOfBirth = $(datetext1).val();
    }


});


app.controller('AuthorTab', function ($scope, AJService, $window, SweetAlert, blockUI, Scopes) {

    //$scope.GeoCountryReq = true;
    //$scope.GeoStateReq = true;
    //$scope.GeoCityReq = true;
    //$scope.GeoPincode = true;




    $scope.getCountryStates = function () {

        setTimeout(function () {
            $($('select[name*=State]')[0]).val('');

            $($('select[name*=city]')[0]).val('');
        }, 500)


        var GeogType = {
            geogtype: "state",
            parentid: $scope.Country,
        };
        if ($.trim($("#Country option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.OthersNot = false;
            $scope.OthersYes = true;
            $scope.cities = [];

        }
        else {
            $scope.OthersNot = true;
            $scope.OthersYes = false;
            $scope.cities = [];
            var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
            GetgeogList.then(function (GetgeogList) {
                $scope.sates = GetgeogList.data;
                $scope.cities = [];
                //$('#state').val('');
                //$scope.sates = null;
            }, function () {
                //alert('Error in getting Geographical list');
            });
        }
    }

    $scope.getStateCities = function () {
        setTimeout(function () {
          
            $($('select[name*=city]')[0]).val('');
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
            //alert('Error in getting Geographical list');
        });

    }

    $scope.showState = function () {
        if ($.trim($("#Country option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.State.css("display", "none");
            $scope.cities = [];
        }
    }


});


app.controller('AffilationTab', function ($scope, AJService, $window, SweetAlert, blockUI, Scopes) {




    $scope.getCountryStates = function () {

        setTimeout(function () {
            $($('select[name*=State]')[1]).val('');

            $($('select[name*=city]')[1]).val('');
        }, 500)


        var GeogType = {
            geogtype: "state",
            parentid: $scope.AffCountry,
        };
        if ($.trim($("#AffCountry option:selected").html()).toLowerCase().indexOf("others") > -1) {


            $scope.OthersNot = false;
            $scope.OthersYes = true;
            $scope.CityValue = undefined;
            // $scope.AffCity = [];
        }
        else {

            $scope.OthersNot = true;
            $scope.OthersYes = false;
            $scope.CityValue = undefined;
            // $scope.AffCity = [];
            var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
            GetgeogList.then(function (GetgeogList) {
                $scope.StateValue = GetgeogList.data;
                $scope.CityValue = undefined;
                //  $scope.AffCity = [];

            }, function () {
                //alert('Error in getting Geographical list');
            });
        }
    }
    $scope.getStateCities = function () {

        setTimeout(function () {
        
            $($('select[name*=city]')[1]).val('');
        }, 500)


        var GeogType = {
            geogtype: "city",
            parentid: $scope.AffState,
        };
        if ($.trim($("#AffState option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.OthersNot = false;
            $scope.OthersYes = true;

            $('.othersCityHide').css("display", "none");
            $scope.OthersStateYes = true;
            // $scope.AffCity = [];
            $scope.CityValue = undefined;
        }

        var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
        GetgeogList.then(function (GetgeogList) {
            if (GetgeogList.data != "") {
                $scope.CityValue = GetgeogList.data;


            }
            else {

                $scope.CityValue = undefined;
            }

        }, function () {
            //alert('Error in getting Geographical list');
        });

    }

    $scope.showState = function () {
        if ($.trim($("#AffCountry option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.State.css("display", "none");
            $scope.AffCity = undefined;
            // $scope.AffCity = [];
        }
    }


});

app.controller('BenificaryTab', function ($scope, AJService, $window, SweetAlert, blockUI, Scopes) {

    $scope.getCountryStates = function () {

        setTimeout(function () {
            $($('select[name*=State]')[2]).val('');

            $($('select[name*=city]')[2]).val('');
        }, 500)
        var GeogType = {
            geogtype: "state",
            parentid: $scope.BeneficiaryCountry,
        };
        if ($.trim($("#BeneficiaryCountry option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.OthersNot = false;
            $scope.OthersYes = true;
            $scope.BeneficiaryCityList = [];
        }
        else {
            $scope.OthersNot = true;
            $scope.OthersYes = false;
            $scope.Beneficiarycities = [];
            var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
            GetgeogList.then(function (GetgeogList) {
                $scope.BeneficiarysatesList = GetgeogList.data;
                $scope.BeneficiaryCityList = [];
            }, function () {
                //alert('Error in getting Geographical list');
            });
        }
    }
    $scope.getStateCities = function () {
        setTimeout(function () {
          
            $($('select[name*=city]')[2]).val('');
        }, 500)

        var GeogType = {
            geogtype: "city",
            parentid: $scope.BeneficiaryState,
        };
        if ($.trim($("#BeneficiaryState option:selected").html()).toLowerCase().indexOf("others") > -1) {
            //$scope.OthersNot = false;
            //$scope.OthersYes = true;

            $('.othersCityHide').css("display", "none");
            $scope.OthersStateYes = true;
            $scope.BeneficiaryCityList = [];
        }

        var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
        GetgeogList.then(function (GetgeogList) {
            $scope.BeneficiaryCityList = GetgeogList.data;
        }, function () {
            //alert('Error in getting Geographical list');
        });

    }

    $scope.showState = function () {
        if ($.trim($("#BeneficiaryCountry option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.State.css("display", "none");
            $scope.BeneficiaryCityList = [];
        }
    }


});

app.controller('NomineeTab', function ($scope, AJService, $window, SweetAlert, blockUI, Scopes) {

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
    $scope.getStateCities = function () {
        setTimeout(function () {
           $($('select[name*=city]')[3]).val('');
        }, 500)

        var GeogType = {
            geogtype: "city",
            parentid: $scope.NomineeState,
        };
        if ($.trim($("#NomineeState option:selected").html()).toLowerCase().indexOf("others") > -1) {
            //$scope.OthersNot = false;
            //$scope.OthersYes = true;

            $('.othersCityHide').css("display", "none");
            $scope.OthersStateYes = true;
            $scope.NomineeCityList = [];
        }

        var GetgeogList = AJService.PostDataToAPI("CommonDropDownBinding/GeographicalUserControl", GeogType);
        GetgeogList.then(function (GetgeogList) {
            $scope.NomineeCityList = GetgeogList.data;
        }, function () {
            //alert('Error in getting Geographical list');
        });

    }

    $scope.showState = function () {
        if ($.trim($("#NomineeCountry option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.State.css("display", "none");
            $scope.cities = [];
        }
    }


});




app.factory('Scopes', function ($rootScope) {
    var mem = {};

    return {
        store: function (key, value) {
            $rootScope.$emit('scope.stored', key);
            mem[key] = value;
        },
        get: function (key) {
            return mem[key];
        }
    };
});