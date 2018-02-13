

app.AuthorViewcontroller = function  ($scope, AJService, $window) {
   
    $scope.EditAuthorDataView = function (Id) {


        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var AuthorData = {
                Id: Id
            };
           // blockUI.start();
            // call API to fetch temp Department list basis on the FlatId

            var ExecutiveStatus = AJService.PostDataToAPI('Author/AuthorSerchView', AuthorData);


            ExecutiveStatus.then(function (msg) {
                if (msg != null) {
                   

                    if (msg.data[0].InstituteCompanyName != null || msg.data[0].AffiliationDesignation != null || msg.data[0].AffiliationDepartment != "" || msg.data[0].AffiliationCountry != null || msg.data[0].AffiliationOtherCountry != null || msg.data[0].AffiliationState != null || msg.data[0].AffiliationOtherState != null || msg.data[0].AffiliationCity != null || msg.data[0].AffiliationOtherCity != null || msg.data[0].AffiliationPinCode != null || msg.data[0].AffiliationAddress != null || msg.data[0].AffiliationPhone != null || msg.data[0].AffiliationEmail != null || msg.data[0].AffiliationWebSite != null)
                    {
                    $scope.ReqAffiliationDetailsView = true
                    }
                else
                {
                    $scope.ReqAffiliationDetailsView = false;
                }


                    if (msg.data[0].AuthorCode != null) {
                        $scope.AuthorCodeView = msg.data[0].AuthorCode
                        $scope.AuthorCodeReq = true
                    }
                    else {
                        $scope.AuthorCodeReq = false
                    }

                    if (msg.data[0].Type != null) {
                        $scope.TypeView = msg.data[0].Type
                        $scope.TypeReq = true
                    }
                    else {
                        $scope.TypeReq = false
                    }


                    if (msg.data[0].FirstName != null) {
                        $scope.FirstNameView = msg.data[0].FirstName
                        $scope.FirstNameReq = true
                    }
                    else {
                        $scope.FirstNameReq = false
                    }

                    if (msg.data[0].LastName != null) {
                        $scope.LastNameView = msg.data[0].LastName
                        $scope.LastNameReq = true
                    }
                    else {
                        $scope.LastNameReq = false
                    }

                    if (msg.data[0].Address != null) {
                        $scope.AddressView = msg.data[0].Address
                        $scope.AddressReq = true
                    }
                    else {
                        $scope.AddressReq = false
                    }


                    if (msg.data[0].ResidencyStatus != null) {
                        $scope.ResidencyStatusView = msg.data[0].ResidencyStatus
                        $scope.ResidencyStatusReq = true
                    }
                    else {
                        $scope.ResidencyStatusReq = false
                    }


                    if (msg.data[0].AuthorCountry != null) {
                        $scope.CountryView = msg.data[0].AuthorCountry
                        $scope.CountryReq = true
                    }
                    else {
                        $scope.CountryReq = false
                    }

                    if (msg.data[0].OtherCountry != null) {
                        $scope.OtherCountryView = msg.data[0].OtherCountry
                        $scope.OtherCountryReq = true
                    }
                    else {
                        $scope.OtherCountryReq = false
                    }

                    if (msg.data[0].AuthorState != null) {
                        $scope.StateView = msg.data[0].AuthorState
                        $scope.StateReq = true
                    }
                    else {
                        $scope.StateReq = false
                    }

                    if (msg.data[0].OtherState != null) {
                        $scope.OtherStateView = msg.data[0].OtherState
                        $scope.OtherStateReq = true
                    }
                    else {
                        $scope.OtherStateReq = false
                    }

                    if (msg.data[0].AuthorCity != null) {
                        $scope.CityView = msg.data[0].AuthorCity
                        $scope.CityReq = true
                    }
                    else {
                        $scope.CityReq = false
                    }

                    if (msg.data[0].OtherCity != null) {
                        $scope.OtherCityView = msg.data[0].OtherCity
                        $scope.OtherCityReq = true
                    }
                    else {
                        $scope.OtherCityReq = false
                    }

                    if (msg.data[0].PinCode != null) {
                        $scope.PinCodeView = msg.data[0].PinCode
                        $scope.PinCodeReq = true
                    }
                    else {
                        $scope.PinCodeReq = false
                    }

                    if (msg.data[0].Email != null) {
                        $scope.EmailView = msg.data[0].Email
                        $scope.EmailReq = true
                    }
                    else {
                        $scope.EmailReq = false
                    }


                    if (msg.data[0].Phone != null) {
                        $scope.PhoneView = msg.data[0].Phone
                        $scope.PhoneReq = true
                    }
                    else {
                        $scope.PhoneReq = false
                    }


                    if (msg.data[0].Mobile != null) {
                        $scope.MobileView = msg.data[0].Mobile
                        $scope.MobileReq = true
                    }
                    else {
                        $scope.MobileReq = false
                    }



                    if (msg.data[0].Fax != null) {
                        $scope.FaxView = msg.data[0].Fax
                        $scope.FaxReq = true
                    }
                    else {
                        $scope.FaxReq = false
                    }


                    if (msg.data[0].PANNo != null) {
                        $scope.PANNoView = msg.data[0].PANNo
                        $scope.PANNoReq = true
                    }
                    else {
                        $scope.PANNoReq = false
                    }


                    if (msg.data[0].AdharCardNo != null) {
                        $scope.AdharCardNoView = msg.data[0].AdharCardNo
                        $scope.AdharCardNoReq = true
                    }
                    else {
                        $scope.AdharCardNoReq = false
                    }

                    if (msg.data[0].DateOfBirth != null) {
                        $scope.DateOfBirthView = msg.data[0].DateOfBirth
                        $scope.DateOfBirthReq = true
                    }
                    else {
                        $scope.DateOfBirthReq = false
                    }

                    if (msg.data[0].DeathDate != null) {
                        $scope.DeathDateView = msg.data[0].DeathDate
                        $scope.DeathDateReq = true
                    }
                    else {
                        $scope.DeathDateReq = false
                    }


                    if (msg.data[0].AccountNo != null) {
                        $scope.AccountNoView = msg.data[0].AccountNo
                        $scope.AccountNoReq = true
                    }
                    else {
                        $scope.AccountNoReq = false
                    }


                    if (msg.data[0].BankName != null) {
                        $scope.BankNameView = msg.data[0].BankName
                        $scope.BankNameReq = true
                    }
                    else {
                        $scope.BankNameReq = false
                    }


                    if (msg.data[0].BranchName != null) {
                        $scope.BranchNameView = msg.data[0].BranchName
                        $scope.BranchNameReq = true
                    }
                    else {
                        $scope.BranchNameReq = false
                    }

                    if (msg.data[0].IFSECode != null) {
                        $scope.IFSECodeView = msg.data[0].IFSECode
                        $scope.BranchNameReq = true
                    }
                    else {
                        $scope.BranchNameReq = false
                    }

                    if (msg.data[0].InstituteCompanyName != null) {
                        $scope.InstituteCompanyNameView = msg.data[0].InstituteCompanyName
                        $scope.InstituteReq = true
                    }
                    else {
                        $scope.InstituteReq = false
                    }


                    if (msg.data[0].AffiliationDesignation != null) {
                        $scope.AffiliationDesignationView = msg.data[0].AffiliationDesignation
                        $scope.AffiliationDesignationReq = true
                    }
                    else {
                        $scope.AffiliationDesignationReq = false
                    }



                    
                    if (msg.data[0].AffiliationDepartment != "") {
                        $scope.AffiliationDepartmentView = msg.data[0].AffiliationDepartment
                        $scope.AffiliationDepartmentReq = true
                    }
                    else {
                        $scope.AffiliationDepartmentReq = false
                    }


                    if (msg.data[0].AffiliationCountry != null) {
                        $scope.AffiliationCountryView = msg.data[0].AffiliationCountry
                        $scope.AffiliationCountryReq = true
                    }
                    else {
                        $scope.AffiliationCountryReq = false
                    }


                    if (msg.data[0].AffiliationOtherCountry != null) {
                        $scope.AffiliationOtherCountryView = msg.data[0].AffiliationOtherCountry
                        $scope.AffiliationOtherCountryReq = true
                    }
                    else {
                        $scope.AffiliationOtherCountryReq = false
                    }


                    if (msg.data[0].AffiliationState != null) {
                        $scope.AffiliationStateView = msg.data[0].AffiliationState
                        $scope.AffiliationStateReq = true
                    }
                    else {
                        $scope.AffiliationStateReq = false
                    }

                    if (msg.data[0].AffiliationOtherState != null) {
                        $scope.AffiliationOtherStateView = msg.data[0].AffiliationOtherState
                        $scope.AffiliationOtherStateReq = true
                    }
                    else {
                        $scope.AffiliationOtherStateReq = false
                    }


                    if (msg.data[0].AffiliationCity != null) {
                        $scope.AffiliationCityView = msg.data[0].AffiliationCity
                        $scope.AffiliationCityReq = true
                    }
                    else {
                        $scope.AffiliationCityReq = false
                    }


                    if (msg.data[0].AffiliationOtherCity != null) {
                        $scope.AffiliationOtherCityView = msg.data[0].AffiliationOtherCity
                        $scope.AffiliationOtherCityReq = true
                    }
                    else {
                        $scope.AffiliationOtherCityReq = false
                    }

                    if (msg.data[0].AffiliationPinCode != null) {
                        $scope.AffiliationPinCodeView = msg.data[0].AffiliationPinCode
                        $scope.AffiliationPinCodeReq = true
                    }
                    else {
                        $scope.AffiliationPinCodeReq = false
                    }


                    if (msg.data[0].AffiliationAddress != null) {
                        $scope.AffiliationAddressView = msg.data[0].AffiliationAddress
                        $scope.AffiliationAddressReq = true
                    }
                    else {
                        $scope.AffiliationAddressReq = false
                    }

                    if (msg.data[0].AffiliationPhone != null) {
                        $scope.AffiliationPhoneView = msg.data[0].AffiliationPhone
                        $scope.AffiliationPhoneReq = true
                    }
                    else {
                        $scope.AffiliationPhoneReq = false
                    }


                    if (msg.data[0].AffiliationEmail != null) {
                        $scope.AffiliationEmailView = msg.data[0].AffiliationEmail
                        $scope.AffiliationEmailReq = true
                    }
                    else {
                        $scope.AffiliationEmailReq = false
                    }


                    if (msg.data[0].AffiliationWebSite != null) {
                        $scope.AffiliationWebSiteView = msg.data[0].AffiliationWebSite
                        $scope.AffiliationWebSiteReq = true
                    }
                    else {
                        $scope.AffiliationWebSiteReq = false
                    }

                    if (msg.data[0].BeneficiaryName != null) {
                        $scope.BeneficiaryNameView = msg.data[0].BeneficiaryName
                        $scope.BeneficiaryNameReq = true
                    }
                    else {
                        $scope.BeneficiaryNameReq = false
                    }


                    if (msg.data[0].BeneficiaryRelation != null) {
                        $scope.BeneficiaryRelationView = msg.data[0].BeneficiaryRelation
                        $scope.BeneficiaryRelationReq = true
                    }
                    else {
                        $scope.BeneficiaryRelationReq = false
                    }


                    if (msg.data[0].BeneficiaryEmail != null) {
                        $scope.BeneficiaryEmailView = msg.data[0].BeneficiaryEmail
                        $scope.BeneficiaryEmailReq = true
                    }
                    else {
                        $scope.BeneficiaryEmailReq = false
                    }



                    if (msg.data[0].BeneficiaryAddress != null) {
                        $scope.BeneficiaryAddressView = msg.data[0].BeneficiaryAddress
                        $scope.BeneficiaryAddressReq = true
                    }
                    else {
                        $scope.BeneficiaryAddressReq = false
                    }

                    if (msg.data[0].BeneficiaryCountry != null) {
                        $scope.BeneficiaryCountryView = msg.data[0].BeneficiaryCountry
                        $scope.BeneficiaryCountryReq = true
                    }
                    else {
                        $scope.BeneficiaryCountryReq = false
                    }


                    if (msg.data[0].BeneficiaryOtherCountry != null) {
                        $scope.BeneficiaryOtherCountryView = msg.data[0].BeneficiaryOtherCountry
                        $scope.BeneficiaryOtherCountryReq = true
                    }
                    else {
                        $scope.BeneficiaryOtherCountryReq = false
                    }

                    if (msg.data[0].BeneficiaryState != null) {
                        $scope.BeneficiaryStateView = msg.data[0].BeneficiaryState
                        $scope.BeneficiaryStateReq = true
                    }
                    else {
                        $scope.BeneficiaryStateReq = false
                    }

                    if (msg.data[0].BeneficiaryOtherState != null) {
                        $scope.BeneficiaryOtherStateView = msg.data[0].BeneficiaryOtherState
                        $scope.BeneficiaryOtherStateReq = true
                    }
                    else {
                        $scope.BeneficiaryOtherStateReq = false
                    }

                    if (msg.data[0].BeneficiaryCity != null) {
                        $scope.BeneficiaryCityView = msg.data[0].BeneficiaryCity
                        $scope.BeneficiaryCityReq = true
                    }
                    else {
                        $scope.BeneficiaryCityReq = false
                    }


                    if (msg.data[0].BeneficiaryOtherCity != null) {
                        $scope.BeneficiaryOtherCityView = msg.data[0].BeneficiaryOtherCity
                        $scope.BeneficiaryOtherCityReq = true
                    }
                    else {
                        $scope.BeneficiaryOtherCityReq = false
                    }

                    if (msg.data[0].BeneficiaryPinCode != null) {
                        $scope.BeneficiaryPinCodeView = msg.data[0].BeneficiaryPinCode
                        $scope.BeneficiaryPinCodeReq = true
                    }
                    else {
                        $scope.BeneficiaryPinCodeReq = false
                    }

                    if (msg.data[0].BeneficiaryPhone != null) {
                        $scope.BeneficiaryPhoneView = msg.data[0].BeneficiaryPhone
                        $scope.BeneficiaryPhoneReq = true
                    }
                    else {
                        $scope.BeneficiaryPhoneReq = false
                    }

                    if (msg.data[0].BeneficiaryMobile != null) {
                        $scope.BeneficiaryMobileView = msg.data[0].BeneficiaryMobile
                        $scope.BeneficiaryMobileReq = true
                    }
                    else {
                        $scope.BeneficiaryMobileReq = false
                    }


                    if (msg.data[0].BeneficiaryPanNo != null) {
                        $scope.BeneficiaryPanNoView = msg.data[0].BeneficiaryPanNo
                        $scope.BeneficiaryPanNoReq = true
                    }
                    else {
                        $scope.BeneficiaryPanNoReq = false
                    }


                    if (msg.data[0].BeneficiaryAccountNo != null) {
                        $scope.BeneficiaryAccountNoView = msg.data[0].BeneficiaryAccountNo
                        $scope.BeneficiaryAccountNoReq = true
                    }
                    else {
                        $scope.BeneficiaryAccountNoReq = false
                    }

                    if (msg.data[0].BeneficiaryBankName != null) {
                        $scope.BeneficiaryBankNameView = msg.data[0].BeneficiaryBankName
                        $scope.BeneficiaryBankNameReq = true
                    }
                    else {
                        $scope.BeneficiaryBankNameReq = false
                    }

                    if (msg.data[0].BeneficiaryBranchName != null) {
                        $scope.BeneficiaryBranchNameView = msg.data[0].BeneficiaryBranchName
                        $scope.BeneficiaryBranchNameReq = true
                    }
                    else {
                        $scope.BeneficiaryBranchNameReq = false
                    }


                    if (msg.data[0].BeneficiaryIFSECode != null) {
                        $scope.BeneficiaryIFSECodeView = msg.data[0].BeneficiaryIFSECode
                        $scope.BeneficiaryIFSECodeReq = true
                    }
                    else {
                        $scope.BeneficiaryIFSECodeReq = false
                    }


                    if (msg.data[0].BeneficiaryFax != null) {
                        $scope.BeneficiaryFaxView = msg.data[0].BeneficiaryFax
                        $scope.BeneficiaryFaxReq = true
                    }
                    else {
                        $scope.BeneficiaryFaxReq = false
                    }




                    if (msg.data[0].NomineeName != null || msg.data[0].NomineeRelation != null || msg.data[0].NomineeEmail != null || msg.data[0].NomineeAddress != null || msg.data[0].NomineeCountry != null || msg.data[0].NomineeOtherCountry != null || msg.data[0].NomineeState != null || msg.data[0].NomineeOtherState != null || msg.data[0].NomineeCity != null || msg.data[0].NomineeOtherCity != null || msg.data[0].NomineePinCode != null || msg.data[0].NomineePhone != null || msg.data[0].NomineeMobile != null || msg.data[0].NomineePanNo != null || msg.data[0].NomineeFax != null || msg.data[0].Remark != null || msg.data[0].NomineeAccountNo != null || msg.data[0].NomineeBranchName != null || msg.data[0].NomineeBankName != null || msg.data[0].NomineeIFSECode != null) {
                        $scope.NomineeDetailsValueReq = true
                    }
                    else {
                        $scope.NomineeDetailsValueReq = false;
                    }


                    if (msg.data[0].NomineeName != null) {
                        $scope.NomineeNameView = msg.data[0].NomineeName
                        $scope.NomineeNameReq = true
                    }
                    else {
                        $scope.NomineeNameReq = false
                    }


                    if (msg.data[0].NomineeRelation != null) {
                        $scope.NomineeRelationView = msg.data[0].NomineeRelation
                        $scope.NomineeRelationReq = true
                    }
                    else {
                        $scope.NomineeRelationReq = false
                    }


                    if (msg.data[0].NomineeEmail != null) {
                        $scope.NomineeEmailView = msg.data[0].NomineeEmail
                        $scope.NomineeEmailReq = true
                    }
                    else {
                        $scope.NomineeEmailReq = false
                    }

                    if (msg.data[0].NomineeAddress != null) {
                        $scope.NomineeAddressView = msg.data[0].NomineeAddress
                        $scope.NomineeAddressReq = true
                    }
                    else {
                        $scope.NomineeAddressReq = false
                    }

                    if (msg.data[0].NomineeCountry != null) {
                        $scope.NomineeCountryView = msg.data[0].NomineeCountry
                        $scope.NomineeCountryReq = true
                    }
                    else {
                        $scope.NomineeCountryReq = false
                    }


                    if (msg.data[0].NomineeOtherCountry != null) {
                        $scope.NomineeOtherCountryView = msg.data[0].NomineeOtherCountry
                        $scope.NomineeOtherCountryReq = true
                    }
                    else {
                        $scope.NomineeOtherCountryReq = false
                    }

                    if (msg.data[0].NomineeState != null) {
                        $scope.NomineeStateView = msg.data[0].NomineeState
                        $scope.NomineeStateReq = true
                    }
                    else {
                        $scope.NomineeStateReq = false
                    }

                    if (msg.data[0].NomineeOtherState != null) {
                        $scope.NomineeOtherStateView = msg.data[0].NomineeOtherState
                        $scope.NomineeOtherStateReq = true
                    }
                    else {
                        $scope.NomineeOtherStateReq = false
                    }


                    if (msg.data[0].NomineeCity != null) {
                        $scope.NomineeCityView = msg.data[0].NomineeCity
                        $scope.NomineeCityReq = true
                    }
                    else {
                        $scope.NomineeCityReq = false
                    }

                    if (msg.data[0].NomineeOtherCity != null) {
                        $scope.NomineeOtherCityView = msg.data[0].NomineeOtherCity
                        $scope.NomineeOtherCityReq = true
                    }
                    else {
                        $scope.NomineeOtherCityReq = false
                    }


                    if (msg.data[0].NomineePinCode != null) {
                        $scope.NomineePinCodeView = msg.data[0].NomineePinCode
                        $scope.NomineePinCodeReq = true
                    }
                    else {
                        $scope.NomineePinCodeReq = false
                    }

                    if (msg.data[0].NomineePhone != null) {
                        $scope.NomineePhoneView = msg.data[0].NomineePhone
                        $scope.NomineePhoneReq = true
                    }
                    else {
                        $scope.NomineePhoneReq = false
                    }

                    if (msg.data[0].NomineeMobile != null) {
                        $scope.NomineeMobileView = msg.data[0].NomineeMobile
                        $scope.NomineeMobileReq = true
                    }
                    else {
                        $scope.NomineeMobileReq = false
                    }

                    if (msg.data[0].NomineePanNo != null) {
                        $scope.NomineePanNoView = msg.data[0].NomineePanNo
                        $scope.NomineePanNoReq = true
                    }
                    else {
                        $scope.NomineePanNoReq = false
                    }


                    if (msg.data[0].NomineeFax != null) {
                        $scope.NomineeFaxView = msg.data[0].NomineeFax
                        $scope.NomineeFaxReq = true
                    }
                    else {
                        $scope.NomineeFaxReq = false
                    }


                    if (msg.data[0].Remark != null) {
                           $scope.RemarkView = msg.data[0].Remark
                        $scope.RemarkReq = true
                    }
                    else {
                           $scope.RemarkReq = false
                    }


                    if (msg.data[0].NomineeAccountNo != null) {
                        $scope.NomineeAccountNo = msg.data[0].NomineeAccountNo
                        $scope.NomineeAccountNoReq = true
                    }
                    else {
                        $scope.NomineeAccountNoReq = false
                    }


                    if (msg.data[0].NomineeBranchName != null) {
                        $scope.NomineeBranchName = msg.data[0].NomineeBranchName
                        $scope.NomineeBranchNameReq = true
                    }
                    else {
                        $scope.NomineeBranchNameReq = false
                    }

                    if (msg.data[0].NomineeBankName != null) {
                        $scope.NomineeBankName = msg.data[0].NomineeBankName
                        $scope.NomineeBankNameReq = true
                    }
                    else {
                        $scope.NomineeBankNameReq = false
                    }


                    if (msg.data[0].NomineeIFSECode != null) {
                        $scope.NomineeIFSECode = msg.data[0].NomineeIFSECode
                        $scope.NomineeIFSECodeReq = true
                    }
                    else {
                        $scope.NomineeIFSECodeReq = false
                    }


                 //   blockUI.stop();

                }
                else {
                    SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                  //  blockUI.stop();
                }

            });


        }
    }
}
