
app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {
    app.expandControllerA($scope, AJService, $window);

    app.AuthorViewcontroller($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);



    $("#search").css("display", "none");

  

  

    $scope.Clear = function () {
        $scope.userForm.AuthorCode.$viewValue=""
        $scope.userForm.Type.$viewValue = ""
        $scope.userForm.FirstName.$viewValue = ""
        $scope.userForm.LastName.$viewValue = ""
        $scope.userForm.Address.$viewValue = ""
        $scope.userForm.ResidencyStatus.$viewValue = 0
        $scope.userForm.Country.$viewValue = ""
        $scope.userForm.State.$viewValue = ""
        $scope.userForm.city.$viewValue = ""
        $scope.userForm.CountryName.$viewValue = ""
        $scope.userForm.stateName.$viewValue = ""
        $scope.userForm.cityName.$viewValue = ""
        $scope.userForm.pincode.$viewValue = ""
        $scope.userForm.Email.$viewValue = ""
        $scope.userForm.Phone.$viewValue = ""
        $scope.userForm.Mobile.$viewValue = ""
        $scope.userForm.Fax.$viewValue = ""
        $scope.userForm.PANNo.$viewValue = ""
        $scope.userForm.AdharCardNo.$viewValue = ""
        $scope.userForm.DateOfBirth.$viewValue = ""
        $scope.userForm.DeathDate.$viewValue = ""
        $scope.userForm.AccountNo.$viewValue = ""
        $scope.userForm.BankName.$viewValue = ""
        $scope.userForm.BranchName.$viewValue = ""
        $scope.userForm.IFSECode.$viewValue = ""
        $scope.userForm.Remark.$modelValue = ""
        $scope.userForm.InstituteCompanyName.$viewValue = ""
        $scope.userForm.AffiliationDesignation.$viewValue = ""
        $scope.userForm.AffiliationDepartment.$viewValue = ""
        $scope.userForm.AffiliationAddress.$viewValue = ""
        $scope.userForm.AffCountry.$viewValue = ""
        $scope.userForm.AffState.$viewValue = ""
        $scope.userForm.Affcity.$viewValue = ""
        $scope.userForm.Affpincode.$viewValue = ""
        $scope.userForm.AffiliationPhone.$viewValue = ""
        $scope.userForm.AffiliationWebSite.$viewValue = ""
        $scope.userForm.AffiliationEmail.$viewValue = ""
        $scope.userForm.BeneficiaryName.$viewValue = ""
        $scope.userForm.BeneficiaryRelation.$viewValue = ""
        $scope.userForm.BeneficiaryAddress.$viewValue = ""
        $scope.userForm.BeneficiaryCountry.$viewValue = ""
        $scope.userForm.BeneficiaryState.$viewValue = ""
        $scope.userForm.Beneficiarycity.$viewValue = ""
        $scope.userForm.Beneficiarypincode.$viewValue = ""
        $scope.userForm.BeneficiaryEmail.$viewValue = ""
        $scope.userForm.BeneficiaryPhone.$viewValue = ""
        $scope.userForm.BeneficiaryMobile.$viewValue = ""
        $scope.userForm.BeneficiaryFax.$viewValue = ""
        $scope.userForm.BeneficiaryPanNo.$viewValue = ""
        $scope.userForm.BeneficiaryAccountNo.$viewValue = ""
        $scope.userForm.BeneficiaryBankName.$viewValue = ""
        $scope.userForm.BeneficiaryBranchName.$viewValue = ""
        $scope.userForm.BeneficiaryIFSECode.$viewValue = ""
        $scope.userForm.NomineeName.$viewValue = ""
        $scope.userForm.NomineeRelation.$viewValue = ""
        $scope.userForm.NomineeAddress.$viewValue = ""
        $scope.userForm.NomineeCountry.$viewValue = ""
        $scope.userForm.NomineeState.$viewValue = ""
        $scope.userForm.Nomineecity.$viewValue = ""
        $scope.userForm.Nomineepincode.$viewValue = ""
        $scope.userForm.NomineeEmail.$viewValue = ""
        $scope.userForm.NomineePhone.$viewValue = ""
        $scope.userForm.NomineeMobile.$viewValue = ""
        $scope.userForm.NomineeFax.$viewValue = ""
        $scope.userForm.NomineePanNo.$viewValue = ""


    }




    $scope.AuthorSearchListResult = function () {
        var AuthorList = AJService.GetDataFromAPI("Author/GetAuthorSearchList?SessionId=" + $("#hid_sessionId").val() + "", null);
        AuthorList.then(function (msg) {
            blockUI.stop();
            if (msg.data.length != 0) {
                $scope.AuthorList = [];
                $scope.AuthorList = msg.data;
                $("#Author_serch").css("display", "none");
                $("#search").css("display", "block");
                $("#AuthorSearchHeading").css("display", "none");
                $("#AuthorViewHeading").css("display", "block");
            
            }
            else {
                swal("No record", 'No record found', "warning");
            }
        });
    }

    if ($('#hid_BackToSearch').val() != "") {
      
        
            $scope.AuthorSearchListResult();
      
   
    }

    function convertDateForInsert(dateVal) {

        if (dateVal == "") {
            dateVal = null
        }
        else {

            var RequestDate = dateVal;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            return yy + "/" + mm + "/" + dd;
        }
    }

    $scope.AddAuthor = function () {
        blockUI.start();

        var mstr_DeathDate = $('#DeathDate').val();
        var mstr_DateOfBirth = $('#DateOfBirth').val();
        if (mstr_DeathDate == "") {
            mstr_DeathDate = null
        }

        if (mstr_DateOfBirth == "") {
            mstr_DateOfBirth = null
        }

        var mstr_OtherCountry = ($("#Authorgeo").find("#CountryName")).find('option:selected').text();
        var mstr_OtherState = ($("#Authorgeo").find("#stateName")).find('option:selected').text();
        var mstr_OtherCity = ($("#Authorgeo").find("#cityName")).find('option:selected').text();
        var mstr_PinCode = ($("#Authorgeo").find("#pincode")).val();


        var mstr_AffiliationOtherCountry = ($("#Affiliation").find("#CountryName")).find('option:selected').text();
        var mstr_AffiliationOtherState = ($("#Affiliation").find("#stateName")).find('option:selected').text();
        var mstr_AffiliationOtherCity = ($("#Affiliation").find("#cityName")).find('option:selected').text();
        var mstr_AffiliationPinCode = ($("#Affiliation").find("#pincode")).val();


        var mstr_BeneficiaryOtherCountry = ($("#Beneficiary").find("#CountryName")).find('option:selected').text();
        var mstr_BeneficiaryOtherState = ($("#Beneficiary").find("#stateName")).find('option:selected').text();
        var mstr_BeneficiaryOtherCity = ($("#Beneficiary").find("#cityName")).find('option:selected').text();
        var mstr_BeneficiaryPinCode = ($("#Beneficiary").find("#pincode")).val();



        var mstr_NomineeOtherCountry = ($("#Nominee").find("#CountryName")).find('option:selected').text();
        var mstr_NomineeOtherState = ($("#Nominee").find("#stateName")).find('option:selected').text();
        var mstr_NomineeOtherCity = ($("#Nominee").find("#cityName")).find('option:selected').text();
        var mstr_NomineePinCode = ($("#Nominee").find("#pincode")).val();



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

        //if (mstr_AffiliationPhone == "") {
        //    mstr_AffiliationPhone = null
        //}

        if (mstr_BeneficiaryOtherCountry == "") {
            mstr_BeneficiaryOtherCountry = null
        }
        if (mstr_BeneficiaryOtherState == "") {
            mstr_BeneficiaryOtherState = null
        }
        if (mstr_BeneficiaryOtherCity == "") {
            mstr_BeneficiaryOtherCity = null
        }
        if (mstr_BeneficiaryPinCode == "") {
            mstr_BeneficiaryPinCode = null
        }


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


        var Author = {
            Type: $scope.userForm.Type.$modelValue,
            FirstName: $scope.userForm.FirstName.$modelValue,
            ResidencyStatus: $scope.userForm.ResidencyStatus.$modelValue,
            Email: $scope.userForm.Email.$modelValue,
            Mobile: $scope.userForm.Mobile.$modelValue,
            // DeathDate:  $scope.userForm.DeathDate.$modelValue,

            DeathDate: (mstr_DeathDate == null ? null : convertDateForInsert(mstr_DeathDate)),

            IFSECode: $scope.userForm.IFSECode.$modelValue,
            AuthorCode: $scope.userForm.AuthorCode.$modelValue,
            LastName: $scope.userForm.LastName.$modelValue,
            Address: $scope.userForm.Address.$modelValue,
            Phone: $scope.userForm.Phone.$modelValue,
            Fax: $scope.userForm.Fax.$modelValue,
            PANNo: $scope.userForm.PANNo.$modelValue,
            BankName: $scope.userForm.BankName.$modelValue,
            AccountNo: $scope.userForm.AccountNo.$modelValue,
            AdharCardNo: $scope.userForm.AdharCardNo.$modelValue,

            AuthorSAPCode: $scope.userForm.AuthorSAPCode.$modelValue,

            CountryId: ($("#Authorgeo").find("#Country")).find('option:selected').val(),
            StateId: ($("#Authorgeo").find("#state")).find('option:selected').val(),
            CityId: ($("#Authorgeo").find("#city")).find('option:selected').val(),

            OtherCountry: mstr_OtherCountry,
            OtherState: mstr_OtherState,
            OtherCity: mstr_OtherCity,

            PinCode: mstr_PinCode,

            BranchName: $scope.userForm.BranchName.$modelValue,
            //DateOfBirth: $scope.userForm.DateOfBirth.$modelValue,

            DateOfBirth: (mstr_DateOfBirth == null ? null : convertDateForInsert(mstr_DateOfBirth)),

            InstituteCompanyName: $scope.userForm.InstituteCompanyName.$modelValue,
            AffiliationDepartment: $scope.userForm.AffiliationDepartment.$modelValue,
            AffiliationDesignation: $scope.userForm.AffiliationDesignation.$modelValue,
            AffiliationAddress: $scope.userForm.AffiliationAddress.$modelValue,
            AffiliationPhone: $scope.userForm.AffiliationPhone.$modelValue,
            AffiliationWebSite: $scope.userForm.AffiliationWebSite.$modelValue,
            AffiliationEmail: $scope.userForm.AffiliationEmail.$modelValue,


            AffiliationCountryId: ($("#Affiliation").find("#Country")).find('option:selected').val(),
            AffiliationStateId: ($("#Affiliation").find("#state")).find('option:selected').val(),
            AffiliationCityId: ($("#Affiliation").find("#city")).find('option:selected').val(),

            AffiliationOtherCountry: mstr_AffiliationOtherCountry,
            AffiliationOtherState: mstr_AffiliationOtherState,
            AffiliationOtherCity: mstr_AffiliationOtherCity,


            AffiliationPinCode: mstr_AffiliationPinCode,

            AffiliationPhone: $scope.userForm.AffiliationPhone.$modelValue,




            BeneficiaryName: $scope.userForm.BeneficiaryName.$modelValue,
            BeneficiaryAddress: $scope.userForm.BeneficiaryAddress.$modelValue,
            BeneficiaryPhone: $scope.userForm.BeneficiaryPhone.$modelValue,
            BeneficiaryBankName: $scope.userForm.BeneficiaryBankName.$modelValue,
            BeneficiaryRelation: $scope.userForm.BeneficiaryRelation.$modelValue,
            BeneficiaryEmail: $scope.userForm.BeneficiaryEmail.$modelValue,
            BeneficiaryFax: $scope.userForm.BeneficiaryFax.$modelValue,

            BeneficiaryAccountNo: $scope.userForm.BeneficiaryAccountNo.$modelValue,
            BeneficiaryPanNo: $scope.userForm.BeneficiaryPanNo.$modelValue,
            BeneficiaryBranchName: $scope.userForm.BeneficiaryBranchName.$modelValue,
            BeneficiaryMobile: $scope.userForm.BeneficiaryMobile.$modelValue,
            BeneficiaryIFSECode: $scope.userForm.BeneficiaryIFSECode.$modelValue,


            BeneficiaryCountryId: ($("#Beneficiary").find("#Country")).find('option:selected').val(),
            BeneficiaryStateId: ($("#Beneficiary").find("#state")).find('option:selected').val(),
            BeneficiaryCityId: ($("#Beneficiary").find("#city")).find('option:selected').val(),

            BeneficiaryOtherCountry: mstr_BeneficiaryOtherCountry,
            BeneficiaryOtherState: mstr_BeneficiaryOtherState,
            BeneficiaryOtherCity: mstr_BeneficiaryOtherCity,

            BeneficiaryPinCode: mstr_BeneficiaryPinCode,



            NomineeName: $scope.userForm.NomineeName.$modelValue,
            NomineeAddress: $scope.userForm.NomineeAddress.$modelValue,
            NomineeRelation: $scope.userForm.NomineeRelation.$modelValue,
            NomineeEmail: $scope.userForm.NomineeEmail.$modelValue,
            NomineeMobile: $scope.userForm.NomineeMobile.$modelValue,
            NomineePhone: $scope.userForm.NomineePhone.$modelValue,
            NomineeFax: $scope.userForm.NomineeFax.$modelValue,
            NomineePanNo: $scope.userForm.NomineePanNo.$modelValue,


            NomineeCountryId: ($("#Nominee").find("#Country")).find('option:selected').val(),
            NomineeStateId: ($("#Nominee").find("#state")).find('option:selected').val(),
            NomineeCityId: ($("#Nominee").find("#city")).find('option:selected').val(),

            NomineeOtherCountry: mstr_NomineeOtherCountry,
            NomineeOtherState: mstr_NomineeOtherState,
            NomineeOtherCity: mstr_NomineeOtherCity,

            NomineePinCode: mstr_NomineePinCode,


            Id: $('#hid_Authid').val() == "" ? 0 : $('#hid_Authid').val(),
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val()

        };




        if (typeof ($('#hid_Report').val() !== "undefined") && $('#hid_Report').val()) {

          
            $('#hid_Report').val("");

            document.location = GlobalredirectPath + "/Master/Master/exportToExcelAuthorList?Type=" + $scope.userForm.Type.$modelValue + "&FirstName=" + $scope.userForm.FirstName.$modelValue + "&LastName=" + $scope.userForm.LastName.$modelValue + "&Address=" + $scope.userForm.Address.$modelValue + "&ResidencyStatus=" + $scope.userForm.ResidencyStatus.$modelValue + "&CountryId=" + ($("#Authorgeo").find("#Country")).find('option:selected').val() + "&StateId=" + ($("#Authorgeo").find("#state")).find('option:selected').val() + "&CityId=" + ($("#Authorgeo").find("#city")).find('option:selected').val() + "&PinCode=" + mstr_PinCode + "&Email=" + $scope.userForm.Email.$modelValue + "&Phone=" + $scope.userForm.Phone.$modelValue + "&Mobile=" + $scope.userForm.Mobile.$modelValue + "&PANNo=" + $scope.userForm.PANNo.$modelValue + "&AdharCardNo=" + $scope.userForm.AdharCardNo.$modelValue + "&DeathDate=" + (mstr_DeathDate == null ? null : convertDateForInsert(mstr_DeathDate)) + "&AccountNo=" + $scope.userForm.AccountNo.$modelValue + "&BankName=" + $scope.userForm.BankName.$modelValue + "&BranchName=" + $scope.userForm.BranchName.$modelValue + "&IFSECode=" + $scope.userForm.IFSECode.$modelValue + "&OtherCountry=" + mstr_OtherCountry + "&OtherState=" + mstr_OtherState + "&OtherCity=" + mstr_OtherCity + "&InstituteCompanyName=" + $scope.userForm.InstituteCompanyName.$modelValue + "&AffiliationDepartment=" + $scope.userForm.AffiliationDepartment.$modelValue + "&AffiliationDesignation=" + $scope.userForm.AffiliationDesignation.$modelValue + "&AffiliationCountryId=" + ($("#Affiliation").find("#Country")).find('option:selected').val() + "&AffiliationStateId=" + ($("#Affiliation").find("#state")).find('option:selected').val() + "&AffiliationCityId=" + ($("#Affiliation").find("#city")).find('option:selected').val() + "&AffiliationPhone=" + $scope.userForm.AffiliationPhone.$modelValue + "&AffiliationPhone=" + $scope.userForm.AffiliationPhone.$modelValue + "&AffiliationWebSite=" + $scope.userForm.AffiliationWebSite.$modelValue + "&AffiliationEmail=" + $scope.userForm.AffiliationEmail.$modelValue + "&AffiliationOtherCountry=" + mstr_AffiliationOtherCountry + "&AffiliationOtherState=" + mstr_AffiliationOtherState + "&AffiliationOtherCity=" + mstr_AffiliationOtherCity + "&BeneficiaryName=" + $scope.userForm.BeneficiaryName.$modelValue + "&BeneficiaryRelation=" + $scope.userForm.BeneficiaryRelation.$modelValue + "&BeneficiaryEmail=" + $scope.userForm.BeneficiaryEmail.$modelValue + "&BeneficiaryMobile=" + $scope.userForm.BeneficiaryMobile.$modelValue + "&BeneficiaryPanNo=" + $scope.userForm.BeneficiaryPanNo.$modelValue + "&BeneficiaryAccountNo=" + $scope.userForm.BeneficiaryAccountNo.$modelValue + "&BeneficiaryBankName=" + $scope.userForm.BeneficiaryBankName.$modelValue + "&BeneficiaryBranchName=" + $scope.userForm.BeneficiaryBranchName.$modelValue + "&BeneficiaryPhone=" + $scope.userForm.BeneficiaryPhone.$modelValue + "&BeneficiaryIFSECode=" + $scope.userForm.BeneficiaryIFSECode.$modelValue + "&NomineeName=" + $scope.userForm.NomineeName.$modelValue + "&NomineeRelation=" + $scope.userForm.NomineeRelation.$modelValue + "&NomineeRelation=" + $scope.userForm.NomineeRelation.$modelValue + "&NomineeEmail=" + $scope.userForm.NomineeEmail.$modelValue + "&DateOfBirth=" + (mstr_DateOfBirth == null ? null : convertDateForInsert(mstr_DateOfBirth)) + "&NomineePhone=" + $scope.userForm.NomineePhone.$modelValue + "&NomineeMobile=" + $scope.userForm.NomineeMobile.$modelValue + "&NomineePanNo=" + $scope.userForm.NomineePanNo.$modelValue + "&AuthorCode=" + $scope.userForm.AuthorCode.$modelValue + "&CountryName=" + ($("#Authorgeo").find("#Country")).find('option:selected').text() + "&StateName=" + ($("#Authorgeo").find("#state")).find('option:selected').text() + "&CityName=" + ($("#Authorgeo").find("#city")).find('option:selected').text() + "&AffiliationCountryName=" + ($("#Affiliation").find("#Country")).find('option:selected').text() + "&AffiliationStateName=" + ($("#Affiliation").find("#state")).find('option:selected').text() + "&AffiliationCityName=" + ($("#Affiliation").find("#city")).find('option:selected').text() + "";
            blockUI.stop();

        }
        else {
            var AuthorStatus = AJService.PostDataToAPI('Author/AuthorSerch', Author);

            AuthorStatus.then(function (msg) {
                blockUI.stop();


                if (msg.data == "OK") 
                {
                    $scope.AuthorSearchListResult();
                }
               
                //else {
                //    swal("No record", 'No record found', "warning");
                //    //$('#tblOutBoundSmsReportList').dataTable().fnDestroy();
                //    //$('#tbl_OutBoundDataList').hide();
                //}
            }, function () {
                $scope.IsError = false;
            });
            blockUI.stop();

        }



       
    }


    $scope.DeleteAuthor = function (Id) {

        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var AuthorData = {
                Id: Id, EnteredBy: $("#enterdBy").val()
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

                         //$("#search").css("display", "none");
                         //$("#Author_serch").css("display", "block");

                         //$("#AuthorSearchHeading").css("display", "block");
                         //$("#AuthorViewHeading").css("display", "none");
                         //angular.element(document.getElementById('angularid')).scope().AuthorList();
                         $scope.AuthorSearchListResult();

                     });
                 }

             });
        }
        else {
            SweetAlert.swal("Error!", "Record is not Deleted", "", "error");
        }
        blockUI.stop();
    }


    $scope.EditAuthorData = function (Id) {


        if (Id != null) {

            var ExecutiveData = {
                Id: Id
            };
            blockUI.start();

            $window.location.href = '../Master/AuthorMaster?id=' + Id;


        }
    }
   

    $scope.BackToserch = function () {

        //debugger;
        //$('#hid_BackToSearch').val('');

        //$("#search").css("display", "none");

        if ($('#hid_Action').val() == "View")
        {
            $window.location.href = '../Master/AuthorSearch?For=View';
        }
        if ($('#hid_Action').val() == "Update") {
            $window.location.href = '../Master/AuthorSearch?For=Update';
        }
        if ($('#hid_Action').val() == "Delete") {
            $window.location.href = '../Master/AuthorSearch?For=Delete';
        }

        if ($('#hid_ReportValue').val() == "Report") {
            $window.location.href = '../Master/AuthorSearch?For=Report';
        }

        if ($('#hid_BackToSearch').val() == "BackToSearch") {
           
            var mstr_history = document.referrer;

            if (mstr_history.indexOf("view") > 0)
            {
                $window.location.href = '../Master/AuthorSearch?For=View';
            }

            if (mstr_history.indexOf("id") > 0) {
               
                $window.location.href = '../Master/AuthorSearch?For=Update';
            }
       
        }
        

        $scope.model = {};

        // 
        //$scope.userForm.FirstName.$modelValue = null;

        $("#Author_serch").css("display", "block");


        $("#AuthorSearchHeading").css("display", "block");
        $("#AuthorViewHeading").css("display", "none");

        $scope.Clear();
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
            $scope.sates = [];
        }, function () {
            alert('Error in getting Geographical list');
        });
    }




    $scope.submitForm = function () {

        $scope.submitted = true;
        if ($scope.userForm.$valid) {

            if ($scope.userForm.$valid) {
                $scope.AddAuthor();
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    };


    $scope.getAuthorDepartmentList = function () {
        var getAuthorDepartmentList = AJService.GetDataFromAPI("Author/getAuthorDepartmentList", null);
        getAuthorDepartmentList.then(function (AuhtorDepartment) {
            $scope.AuthorDepartmentList = AuhtorDepartment.data.query;
        }, function () {
            alert('Error in getting Author Department list');
        });
    }


    $scope.AuthorReportExcel = function () {

        $('#hid_Report').val(1);

        $scope.AddAuthor();
    }



});


app.controller('AuthorTab', function ($scope, AJService, $window, SweetAlert, blockUI) {

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
            }, function () {
                alert('Error in getting Geographical list');
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
            alert('Error in getting Geographical list');
        });

    }

    $scope.showState = function () {
        if ($.trim($("#Country option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.State.css("display", "none");
            $scope.cities = [];
        }
    }


});


app.controller('AffilationTab', function ($scope, AJService, $window, SweetAlert, blockUI) {

    $scope.getCountryStates = function () {
        setTimeout(function () {
            $($('select[name*=State]')[1]).val('');

            $($('select[name*=city]')[1]).val('');
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
            }, function () {
                alert('Error in getting Geographical list');
            });
        }
    }
    $scope.getStateCities = function () {
        setTimeout(function () {

            $($('select[name*=city]')[1]).val('');
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


});

app.controller('BenificaryTab', function ($scope, AJService, $window, SweetAlert, blockUI) {

    $scope.getCountryStates = function () {
        setTimeout(function () {
            $($('select[name*=State]')[2]).val('');

            $($('select[name*=city]')[2]).val('');
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
            }, function () {
                alert('Error in getting Geographical list');
            });
        }
    }
    $scope.getStateCities = function () {
        setTimeout(function () {

            $($('select[name*=city]')[2]).val('');
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


});

app.controller('NomineeTab', function ($scope, AJService, $window, SweetAlert, blockUI) {

    $scope.getCountryStates = function () {
        setTimeout(function () {
            $($('select[name*=State]')[3]).val('');

            $($('select[name*=city]')[3]).val('');
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


});



