app.expandControllerProductDetails = function ($scope, AJService, $window) {
    
    //view product detail for all except Contract and License
    $scope.ProductSerach = function (Id, AuthorContract, ProductLicCode) {

        if (Id != null) {

            var ProductData = {
                Id: Id,
                AuthorContract: AuthorContract,
                ProductLicCode: ProductLicCode,
            };


            // blockUI.start();
            // call API to fetch temp Department list basis on the FlatId
            $('#hid_productid').val(Id);
            //debugger

            var ProductStatus = AJService.PostDataToAPI('ProductMaster/ProductDetails', ProductData);
            ProductStatus.then(function (msg) {

                $scope.ProductDetails = msg.data;

                $scope.SeriesCode_Available = false;

                //In case of Impression
                $scope.Impression_ISBN = msg.data[0].OupIsbn;

            }, function () {
                //alert('Error in getting Product list');
            });
        }
    };

    //view product detail for contract
    $scope.ProductSerachContract = function (Id, AuthorContract, ProductLicCode) {
       
        if (Id != null) {
           
            var ProductData = {
                Id: Id,
                AuthorContract: AuthorContract,
                //ProductLicCode: ProductLicCode,
            };

           
           // blockUI.start();
            // call API to fetch temp Department list basis on the FlatId
            $('#hid_productid').val(Id);
            //debugger

            var ProductStatus = AJService.PostDataToAPI('ProductMaster/ProductDetailsForContract', ProductData);
            ProductStatus.then(function (msg) {
               
                $scope.ProductDetails = msg.data;

                $scope.SeriesCode_Available = false;
                               
            }, function () {
                //alert('Error in getting Product list');
            });
        }
    };

    //view product detail for License
    $scope.ProductSerachLicense = function (Id, AuthorContract, ProductLicCode) {

        if (Id != null) {

            var ProductData = {
                Id: Id,
                //AuthorContract: AuthorContract,
                ProductLicCode: ProductLicCode,
            };


            // blockUI.start();
            // call API to fetch temp Department list basis on the FlatId
            $('#hid_productid').val(Id);
            //debugger

            var ProductStatus = AJService.PostDataToAPI('ProductMaster/ProductDetailsForLicense', ProductData);
            ProductStatus.then(function (msg) {

                $scope.ProductDetails = msg.data;

                $scope.SeriesCode_Available = false;

            }, function () {
                //alert('Error in getting Product list');
            });
        }
    };

    //$scope.ProductSerach = function (Id) {

    //    if (Id != null) {
    //        var ProductData = {
    //            MultipleId: "\'"+Id+"\'"
    //        };
    //       // blockUI.start();
    //        // call API to fetch temp Department list basis on the FlatId
    //        //$('#hid_productid').val(Id);
    //        var ProductStatus = AJService.PostDataToAPI('ProductMaster/ProductDetails', ProductData);
    //        ProductStatus.then(function (msg) {
    //            $scope.ProductDetails = msg.data;
    //        }, function () {
    //            alert('Error in getting Product list');
    //        });
    //    }
    //};


    $scope.ProductViewMode = function (Id) {
       
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


                    if (msg.data[0].InstituteCompanyName != null || msg.data[0].AffiliationDesignation != null || msg.data[0].AffiliationDepartment != "" || msg.data[0].AffiliationCountry != null || msg.data[0].AffiliationOtherCountry != null || msg.data[0].AffiliationState != null || msg.data[0].AffiliationOtherState != null || msg.data[0].AffiliationCity != null || msg.data[0].AffiliationOtherCity != null || msg.data[0].AffiliationPinCode != null || msg.data[0].AffiliationAddress != null || msg.data[0].AffiliationPhone != null || msg.data[0].AffiliationEmail != null || msg.data[0].AffiliationWebSite != null) {
                        $scope.ReqAffiliationDetailsView = true
                    }
                    else {
                        $scope.ReqAffiliationDetailsView = false;
                    }

                    if (msg.data[0].AuthorCode != null) {
                        $scope.AuthorCode = msg.data[0].AuthorCode
                        $scope.AuthorCodeReq = true
                    }
                    else {
                        $scope.AuthorCodeReq = false
                    }

                    if (msg.data[0].Type != null) {
                        $scope.Type = msg.data[0].Type
                        $scope.TypeReq = true
                    }
                    else {
                        $scope.TypeReq = false
                    }


                    if (msg.data[0].FirstName != null) {
                        $scope.FirstName = msg.data[0].FirstName
                        $scope.FirstNameReq = true
                    }
                    else {
                        $scope.FirstNameReq = false
                    }

                    if (msg.data[0].LastName != null) {
                        $scope.LastName = msg.data[0].LastName
                        $scope.LastNameReq = true
                    }
                    else {
                        $scope.LastNameReq = false
                    }

                    if (msg.data[0].Address != null) {
                        $scope.Address = msg.data[0].Address
                        $scope.AddressReq = true
                    }
                    else {
                        $scope.AddressReq = false
                    }


                    if (msg.data[0].ResidencyStatus != null) {
                        $scope.ResidencyStatus = msg.data[0].ResidencyStatus
                        $scope.ResidencyStatusReq = true
                    }
                    else {
                        $scope.ResidencyStatusReq = false
                    }


                    if (msg.data[0].AuthorCountry != null) {
                        $scope.Country = msg.data[0].AuthorCountry
                        $scope.CountryReq = true
                    }
                    else {
                        $scope.CountryReq = false
                    }

                    if (msg.data[0].OtherCountry != null) {
                        $scope.OtherCountry = msg.data[0].OtherCountry
                        $scope.OtherCountryReq = true
                    }
                    else {
                        $scope.OtherCountryReq = false
                    }

                    if (msg.data[0].AuthorState != null) {
                        $scope.State = msg.data[0].AuthorState
                        $scope.StateReq = true
                    }
                    else {
                        $scope.StateReq = false
                    }

                    if (msg.data[0].OtherState != null) {
                        $scope.OtherState = msg.data[0].OtherState
                        $scope.OtherStateReq = true
                    }
                    else {
                        $scope.OtherStateReq = false
                    }

                    if (msg.data[0].AuthorCity != null) {
                        $scope.City = msg.data[0].AuthorCity
                        $scope.CityReq = true
                    }
                    else {
                        $scope.CityReq = false
                    }

                    if (msg.data[0].OtherCity != null) {
                        $scope.OtherCity = msg.data[0].OtherCity
                        $scope.OtherCityReq = true
                    }
                    else {
                        $scope.OtherCityReq = false
                    }

                    if (msg.data[0].PinCode != null) {
                        $scope.PinCode = msg.data[0].PinCode
                        $scope.PinCodeReq = true
                    }
                    else {
                        $scope.PinCodeReq = false
                    }

                    if (msg.data[0].Email != null) {
                        $scope.Email = msg.data[0].Email
                        $scope.EmailReq = true
                    }
                    else {
                        $scope.EmailReq = false
                    }


                    if (msg.data[0].Phone != null) {
                        $scope.Phone = msg.data[0].Phone
                        $scope.PhoneReq = true
                    }
                    else {
                        $scope.PhoneReq = false
                    }


                    if (msg.data[0].Mobile != null) {
                        $scope.Mobile = msg.data[0].Mobile
                        $scope.MobileReq = true
                    }
                    else {
                        $scope.MobileReq = false
                    }



                    if (msg.data[0].Fax != null) {
                        $scope.Fax = msg.data[0].Fax
                        $scope.FaxReq = true
                    }
                    else {
                        $scope.FaxReq = false
                    }


                    if (msg.data[0].PANNo != null) {
                        $scope.PANNo = msg.data[0].PANNo
                        $scope.PANNoReq = true
                    }
                    else {
                        $scope.PANNoReq = false
                    }


                    if (msg.data[0].AdharCardNo != null) {
                        $scope.AdharCardNo = msg.data[0].AdharCardNo
                        $scope.AdharCardNoReq = true
                    }
                    else {
                        $scope.AdharCardNoReq = false
                    }

                    if (msg.data[0].DateOfBirth != null) {
                        $scope.DateOfBirth = msg.data[0].DateOfBirth
                        $scope.DateOfBirthReq = true
                    }
                    else {
                        $scope.DateOfBirthReq = false
                    }

                    if (msg.data[0].DeathDate != null) {
                        $scope.DeathDate = msg.data[0].DeathDate
                        $scope.DeathDateReq = true
                    }
                    else {
                        $scope.DeathDateReq = false
                    }


                    if (msg.data[0].AccountNo != null) {
                        $scope.AccountNo = msg.data[0].AccountNo
                        $scope.AccountNoReq = true
                    }
                    else {
                        $scope.AccountNoReq = false
                    }


                    if (msg.data[0].BankName != null) {
                        $scope.BankName = msg.data[0].BankName
                        $scope.BankNameReq = true
                    }
                    else {
                        $scope.BankNameReq = false
                    }


                    if (msg.data[0].BranchName != null) {
                        $scope.BranchName = msg.data[0].BranchName
                        $scope.BranchNameReq = true
                    }
                    else {
                        $scope.BranchNameReq = false
                    }

                    if (msg.data[0].IFSECode != null) {
                        $scope.IFSECode = msg.data[0].IFSECode
                        $scope.BranchNameReq = true
                    }
                    else {
                        $scope.BranchNameReq = false
                    }

                    if (msg.data[0].InstituteCompanyName != null) {
                        $scope.InstituteCompanyName = msg.data[0].InstituteCompanyName
                        $scope.InstituteReq = true
                    }
                    else {
                        $scope.InstituteReq = false
                    }


                    if (msg.data[0].AffiliationDesignation != null) {
                        $scope.AffiliationDesignation = msg.data[0].AffiliationDesignation
                        $scope.AffiliationDesignationReq = true
                    }
                    else {
                        $scope.AffiliationDesignationReq = false
                    }


                    if (msg.data[0].AffiliationDepartment != "") {
                        $scope.AffiliationDepartment = msg.data[0].AffiliationDepartment
                        $scope.AffiliationDepartmentReq = true
                    }
                    else {
                        $scope.AffiliationDepartmentReq = false
                    }


                    if (msg.data[0].AffiliationCountry != null) {
                        $scope.AffiliationCountry = msg.data[0].AffiliationCountry
                        $scope.AffiliationCountryReq = true
                    }
                    else {
                        $scope.AffiliationCountryReq = false
                    }


                    if (msg.data[0].AffiliationOtherCountry != null) {
                        $scope.AffiliationOtherCountry = msg.data[0].AffiliationOtherCountry
                        $scope.AffiliationOtherCountryReq = true
                    }
                    else {
                        $scope.AffiliationOtherCountryReq = false
                    }


                    if (msg.data[0].AffiliationState != null) {
                        $scope.AffiliationState = msg.data[0].AffiliationState
                        $scope.AffiliationStateReq = true
                    }
                    else {
                        $scope.AffiliationStateReq = false
                    }

                    if (msg.data[0].AffiliationOtherState != null) {
                        $scope.AffiliationOtherState = msg.data[0].AffiliationOtherState
                        $scope.AffiliationOtherStateReq = true
                    }
                    else {
                        $scope.AffiliationOtherStateReq = false
                    }


                    if (msg.data[0].AffiliationCity != null) {
                        $scope.AffiliationCity = msg.data[0].AffiliationCity
                        $scope.AffiliationCityReq = true
                    }
                    else {
                        $scope.AffiliationCityReq = false
                    }


                    if (msg.data[0].AffiliationOtherCity != null) {
                        $scope.AffiliationOtherCity = msg.data[0].AffiliationOtherCity
                        $scope.AffiliationOtherCityReq = true
                    }
                    else {
                        $scope.AffiliationOtherCityReq = false
                    }

                    if (msg.data[0].AffiliationPinCode != null) {
                        $scope.AffiliationPinCode = msg.data[0].AffiliationPinCode
                        $scope.AffiliationPinCodeReq = true
                    }
                    else {
                        $scope.AffiliationPinCodeReq = false
                    }


                    if (msg.data[0].AffiliationAddress != null) {
                        $scope.AffiliationAddress = msg.data[0].AffiliationAddress
                        $scope.AffiliationAddressReq = true
                    }
                    else {
                        $scope.AffiliationAddressReq = false
                    }

                    if (msg.data[0].AffiliationPhone != null) {
                        $scope.AffiliationPhone = msg.data[0].AffiliationPhone
                        $scope.AffiliationPhoneReq = true
                    }
                    else {
                        $scope.AffiliationPhoneReq = false
                    }


                    if (msg.data[0].AffiliationEmail != null) {
                        $scope.AffiliationEmail = msg.data[0].AffiliationEmail
                        $scope.AffiliationEmailReq = true
                    }
                    else {
                        $scope.AffiliationEmailReq = false
                    }


                    if (msg.data[0].AffiliationWebSite != null) {
                        $scope.AffiliationWebSite = msg.data[0].AffiliationWebSite
                        $scope.AffiliationWebSiteReq = true
                    }
                    else {
                        $scope.AffiliationWebSiteReq = false
                    }

                    if (msg.data[0].BeneficiaryName != null) {
                        $scope.BeneficiaryName = msg.data[0].BeneficiaryName
                        $scope.BeneficiaryNameReq = true
                    }
                    else {
                        $scope.BeneficiaryNameReq = false
                    }


                    if (msg.data[0].BeneficiaryRelation != null) {
                        $scope.BeneficiaryRelation = msg.data[0].BeneficiaryRelation
                        $scope.BeneficiaryRelationReq = true
                    }
                    else {
                        $scope.BeneficiaryRelationReq = false
                    }


                    if (msg.data[0].BeneficiaryEmail != null) {
                        $scope.BeneficiaryEmail = msg.data[0].BeneficiaryEmail
                        $scope.BeneficiaryEmailReq = true
                    }
                    else {
                        $scope.BeneficiaryEmailReq = false
                    }



                    if (msg.data[0].BeneficiaryAddress != null) {
                        $scope.BeneficiaryAddress = msg.data[0].BeneficiaryAddress
                        $scope.BeneficiaryAddressReq = true
                    }
                    else {
                        $scope.BeneficiaryAddressReq = false
                    }

                    if (msg.data[0].BeneficiaryCountry != null) {
                        $scope.BeneficiaryCountry = msg.data[0].BeneficiaryCountry
                        $scope.BeneficiaryCountryReq = true
                    }
                    else {
                        $scope.BeneficiaryCountryReq = false
                    }


                    if (msg.data[0].BeneficiaryOtherCountry != null) {
                        $scope.BeneficiaryOtherCountry = msg.data[0].BeneficiaryOtherCountry
                        $scope.BeneficiaryOtherCountryReq = true
                    }
                    else {
                        $scope.BeneficiaryOtherCountryReq = false
                    }

                    if (msg.data[0].BeneficiaryState != null) {
                        $scope.BeneficiaryState = msg.data[0].BeneficiaryState
                        $scope.BeneficiaryStateReq = true
                    }
                    else {
                        $scope.BeneficiaryStateReq = false
                    }

                    if (msg.data[0].BeneficiaryOtherState != null) {
                        $scope.BeneficiaryOtherState = msg.data[0].BeneficiaryOtherState
                        $scope.BeneficiaryOtherStateReq = true
                    }
                    else {
                        $scope.BeneficiaryOtherStateReq = false
                    }

                    if (msg.data[0].BeneficiaryCity != null) {
                        $scope.BeneficiaryCity = msg.data[0].BeneficiaryCity
                        $scope.BeneficiaryCityReq = true
                    }
                    else {
                        $scope.BeneficiaryCityReq = false
                    }


                    if (msg.data[0].BeneficiaryOtherCity != null) {
                        $scope.BeneficiaryOtherCity = msg.data[0].BeneficiaryOtherCity
                        $scope.BeneficiaryOtherCityReq = true
                    }
                    else {
                        $scope.BeneficiaryOtherCityReq = false
                    }

                    if (msg.data[0].BeneficiaryPinCode != null) {
                        $scope.BeneficiaryPinCode = msg.data[0].BeneficiaryPinCode
                        $scope.BeneficiaryPinCodeReq = true
                    }
                    else {
                        $scope.BeneficiaryPinCodeReq = false
                    }

                    if (msg.data[0].BeneficiaryPhone != null) {
                        $scope.BeneficiaryPhone = msg.data[0].BeneficiaryPhone
                        $scope.BeneficiaryPhoneReq = true
                    }
                    else {
                        $scope.BeneficiaryPhoneReq = false
                    }

                    if (msg.data[0].BeneficiaryMobile != null) {
                        $scope.BeneficiaryMobile = msg.data[0].BeneficiaryMobile
                        $scope.BeneficiaryMobileReq = true
                    }
                    else {
                        $scope.BeneficiaryMobileReq = false
                    }


                    if (msg.data[0].BeneficiaryPanNo != null) {
                        $scope.BeneficiaryPanNo = msg.data[0].BeneficiaryPanNo
                        $scope.BeneficiaryPanNoReq = true
                    }
                    else {
                        $scope.BeneficiaryPanNoReq = false
                    }


                    if (msg.data[0].BeneficiaryAccountNo != null) {
                        $scope.BeneficiaryAccountNo = msg.data[0].BeneficiaryAccountNo
                        $scope.BeneficiaryAccountNoReq = true
                    }
                    else {
                        $scope.BeneficiaryAccountNoReq = false
                    }

                    if (msg.data[0].BeneficiaryBankName != null) {
                        $scope.BeneficiaryBankName = msg.data[0].BeneficiaryBankName
                        $scope.BeneficiaryBankNameReq = true
                    }
                    else {
                        $scope.BeneficiaryBankNameReq = false
                    }

                    if (msg.data[0].BeneficiaryBranchName != null) {
                        $scope.BeneficiaryBranchName = msg.data[0].BeneficiaryBranchName
                        $scope.BeneficiaryBranchNameReq = true
                    }
                    else {
                        $scope.BeneficiaryBranchNameReq = false
                    }


                    if (msg.data[0].BeneficiaryIFSECode != null) {
                        $scope.BeneficiaryIFSECode = msg.data[0].BeneficiaryIFSECode
                        $scope.BeneficiaryIFSECodeReq = true
                    }
                    else {
                        $scope.BeneficiaryIFSECodeReq = false
                    }


                    if (msg.data[0].BeneficiaryFax != null) {
                        $scope.BeneficiaryFax = msg.data[0].BeneficiaryFax
                        $scope.BeneficiaryFaxReq = true
                    }
                    else {
                        $scope.BeneficiaryFaxReq = false
                    }




                    if (msg.data[0].NomineeName != null || msg.data[0].NomineeRelation != null || msg.data[0].NomineeEmail != null || msg.data[0].NomineeAddress != null || msg.data[0].NomineeCountry != null || msg.data[0].NomineeOtherCountry != null || msg.data[0].NomineeState != null || msg.data[0].NomineeOtherState != null || msg.data[0].NomineeCity != null || msg.data[0].NomineeOtherCity != null || msg.data[0].NomineePinCode != null || msg.data[0].NomineePhone != null || msg.data[0].NomineeMobile != null || msg.data[0].NomineePanNo != null || msg.data[0].NomineeFax != null || msg.data[0].Remark != null) {
                        $scope.ReqNomineeDetails = true
                    }
                    else {
                        $scope.ReqNomineeDetails = false;
                    }



                    if (msg.data[0].NomineeName != null) {
                        $scope.NomineeName = msg.data[0].NomineeName
                        $scope.NomineeNameReq = true
                    }
                    else {
                        $scope.NomineeNameReq = false
                    }


                    if (msg.data[0].NomineeRelation != null) {
                        $scope.NomineeRelation = msg.data[0].NomineeRelation
                        $scope.NomineeRelationReq = true
                    }
                    else {
                        $scope.NomineeRelationReq = false
                    }


                    if (msg.data[0].NomineeEmail != null) {
                        $scope.NomineeEmail = msg.data[0].NomineeEmail
                        $scope.NomineeEmailReq = true
                    }
                    else {
                        $scope.NomineeEmailReq = false
                    }

                    if (msg.data[0].NomineeAddress != null) {
                        $scope.NomineeAddress = msg.data[0].NomineeAddress
                        $scope.NomineeAddressReq = true
                    }
                    else {
                        $scope.NomineeAddressReq = false
                    }

                    if (msg.data[0].NomineeCountry != null) {
                        $scope.NomineeCountry = msg.data[0].NomineeCountry
                        $scope.NomineeCountryReq = true
                    }
                    else {
                        $scope.NomineeCountryReq = false
                    }


                    if (msg.data[0].NomineeOtherCountry != null) {
                        $scope.NomineeOtherCountry = msg.data[0].NomineeOtherCountry
                        $scope.NomineeOtherCountryReq = true
                    }
                    else {
                        $scope.NomineeOtherCountryReq = false
                    }

                    if (msg.data[0].NomineeState != null) {
                        $scope.NomineeState = msg.data[0].NomineeState
                        $scope.NomineeStateReq = true
                    }
                    else {
                        $scope.NomineeStateReq = false
                    }

                    if (msg.data[0].NomineeOtherState != null) {
                        $scope.NomineeOtherState = msg.data[0].NomineeOtherState
                        $scope.NomineeOtherStateReq = true
                    }
                    else {
                        $scope.NomineeOtherStateReq = false
                    }


                    if (msg.data[0].NomineeCity != null) {
                        $scope.NomineeCity = msg.data[0].NomineeCity
                        $scope.NomineeCityReq = true
                    }
                    else {
                        $scope.NomineeCityReq = false
                    }

                    if (msg.data[0].NomineeOtherCity != null) {
                        $scope.NomineeOtherCity = msg.data[0].NomineeOtherCity
                        $scope.NomineeOtherCityReq = true
                    }
                    else {
                        $scope.NomineeOtherCityReq = false
                    }


                    if (msg.data[0].NomineePinCode != null) {
                        $scope.NomineePinCode = msg.data[0].NomineePinCode
                        $scope.NomineePinCodeReq = true
                    }
                    else {
                        $scope.NomineePinCodeReq = false
                    }

                    if (msg.data[0].NomineePhone != null) {
                        $scope.NomineePhone = msg.data[0].NomineePhone
                        $scope.NomineePhoneReq = true
                    }
                    else {
                        $scope.NomineePhoneReq = false
                    }

                    if (msg.data[0].NomineeMobile != null) {
                        $scope.NomineeMobile = msg.data[0].NomineeMobile
                        $scope.NomineeMobileReq = true
                    }
                    else {
                        $scope.NomineeMobileReq = false
                    }

                    if (msg.data[0].NomineePanNo != null) {
                        $scope.NomineePanNo = msg.data[0].NomineePanNo
                        $scope.NomineePanNoReq = true
                    }
                    else {
                        $scope.NomineePanNoReq = false
                    }


                    if (msg.data[0].NomineeFax != null) {
                        $scope.NomineeFax = msg.data[0].NomineeFax
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



                    //blockUI.stop();

                }
                else {
                    SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                  //  blockUI.stop();
                }

            });


        }
    }


}


