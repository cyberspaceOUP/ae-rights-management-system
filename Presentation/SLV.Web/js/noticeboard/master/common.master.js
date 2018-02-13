app.expandControllerA = function ($scope, AJService, $window) {

    // Get Department List
    $scope.GetDepartmentList = function () {
        var GetDepartmentList = AJService.GetDataFromAPI("CommonList/getDepartmentList", null);
        GetDepartmentList.then(function (Department) {
            $scope.DepartmentList = Department.data;
        }, function () {
            alert('Error in getting department list');
        });
    }

    /*added by dheeraj Sharma*/
    // Get Division List
    $scope.getDivisionList = function () {
        var getDivisionList = AJService.GetDataFromAPI("CommonList/getDivisionList?Id=" + $("#enterdBy").val());
        getDivisionList.then(function (Division) {
            $scope.DivisionList = Division.data;
        }, function () {
            alert('Error in getting Division list');
        });
    }

    $scope.getSubDivisionList = function () {
        var getSubDivisionList = AJService.PostDataToAPI("CommonList/getSubDivisionList", null);
        getSubDivisionList.then(function (Division) {
            $scope.SubDivisionList = Division.data.divisionData;
        }, function () {
            alert('Error in getting SubDivision list');
        });
    }


    /*added by Saddam on 05/05/2016*/
    // Get ExecutiveMaster Values

    $scope.GetExecutiveList = function () {

        var getExecutiveList = AJService.GetDataFromAPI("CommonList/getExecutiveList", null);
        getExecutiveList.then(function (Executive) {
            $scope.ExecutiveList = Executive.data.query;
        }, function () {
            alert('Error in getting ExecutiveList list');
        });
    }

    //$scope.GetAuthorList = function () {
    //    var getAuthorList = AJService.GetDataFromAPI("CommonList/getAuthorList", null);
    //    getAuthorList.then(function (Author) {
    //        $scope.AuthorList = Author.data;
    //    }, function () {
    //        alert('Error in getting department list');
    //    });
    //}


    $scope.GetAuthorList = function () {

        var getAuthorList = AJService.GetDataFromAPI("CommonList/getAuthorList", null);
        getAuthorList.then(function (Author) {
            $scope.AuthorList = Author.data.query;
        }, function () {
            alert('Error in getting author list');
        });
    }


    // Get Product Category Type List
    $scope.getAllProductCategoryList = function () {
        var ProductCategoryList = AJService.GetDataFromAPI("CommonList/getAllProductCategoryList", null);
        ProductCategoryList.then(function (ProductCategory) {
            $scope.ProductCategoryList = ProductCategory.data;
        }, function () {
            alert('Error in getting Product Category List');
        });
    }




  



    $scope.getAllProductTypeList = function () {

        var ProductTypeList = AJService.PostDataToAPI("CommonList/AllProductTypeList", null);
        ProductTypeList.then(function (ProductTypeList) {
            $scope.ProductTypeList = ProductTypeList.data;
        }, function () {
            alert('Error in getting Product Type List');
        });
    }

    /* commented and added By DHarmveer on 23 May 2016 for get Sub Product Type on Product Change*/
    /*   $scope.getSubProductTypeList = function () {
           var ProductType = {
               Id: $scope.ProductType,
           };
   */
    $scope.getSubProductTypeList = function (value) {
        $scope.SelectedTrue = true;
        setTimeout(function () {
            $('#SubProductType').val('');
        }, 150);
        var ProductType = {
            Id: value,
        };
        /* end here*/
        var SubProductTypeList = AJService.PostDataToAPI("CommonList/SubProductTypeList", ProductType);
        /*commented and added by DHarmveer on 23 mAy May to Change VAriable Name for fetch record */
        // ProductTypeList.then(function (SubProductTypeList) {
        SubProductTypeList.then(function (SubProductTypeList) {
            $scope.SubProductTypeList = SubProductTypeList.data;
        }, function () {
            alert('Error in getting SubProduct Type List');
        });
    }


    // Added by sanjeet on 16th may 2016
    // Get ProductTypeMaster
    $scope.getProductTypeList = function () {
        var getProductTypeList = AJService.GetDataFromAPI("CommonList/getProductTypeList", null);
        getProductTypeList.then(function (ProductType) {
            $scope.ProductTypeList = ProductType.data;
        }, function () {
            alert('Error in getting ProductType list');
        });
    }
    //Get subProduct type list

    $scope.getSubProductList = function () {
        var getSubProductTypeList = AJService.GetDataFromAPI("CommonList/getSubProductTypeList", null);
        getSubProductTypeList.then(function (subProduct) {
            $scope.SubProductTypeList = subProduct.data.subProductData;
        }, function () {
            alert('Error in getting SubProduct type list');
        });
    }

    //Get Imprint List
    $scope.getImprintList = function () {
        var getImprintList = AJService.GetDataFromAPI("CommonList/getImprintList", null);
        getImprintList.then(function (Imprint) {
            $scope.ImprintList = Imprint.data;
        }, function () {
            alert('Error in getting Imprint List');
        });
    }

    //Get Imprint List for Proprietor Details
    $scope.getImprintListForProprietorDetails = function () {
        var getImprintList = AJService.GetDataFromAPI("CommonList/GetImprintListForProprietorDetails", null);
        getImprintList.then(function (Imprint) {
            $scope.PROPImprintList = Imprint.data;
        }, function () {
            alert('Error in getting Imprint List');
        });
    }

    //Get Imprint List
    $scope.getLanguageList = function () {
        var getLanguageList = AJService.GetDataFromAPI("CommonList/getLanguageList", null);
        getLanguageList.then(function (Language) {
            $scope.LanguageList = Language.data;
        }, function () {
            alert('Error in getting Language List');
        });
    }

    //Get Currency List
    $scope.getCurrencyList = function () {
        var getCurrencyList = AJService.GetDataFromAPI("CommonList/getCurrencyList", null);
        getCurrencyList.then(function (Currency) {
            $scope.CurrencyList = Currency.data;
        }, function () {
            alert('Error in getting Currency List');
        });
    }

    //Get Publishing Company List
    $scope.getPublishingCompanyList = function () {
        var getPublishingCompanyList = AJService.GetDataFromAPI("CommonList/GetPublishingCompanyList", null);
        getPublishingCompanyList.then(function (PublishingCompany) {
            $scope.PublishingCompanyList = PublishingCompany.data;
        }, function () {
            alert('Error in getting Publishing Company list');
        });
    }






    /*added by Saddam on 24/05/2016*/

    // CustomProduct / getCustomProductList

    $scope.GetCustomProductList = function () {
        var getCustomProductList = AJService.GetDataFromAPI("CustomProduct/getCustomProductList", null);
        getCustomProductList.then(function (Custom) {
            $scope.CustomProductList = Custom.data;
        }, function () {
            alert('Error in getting department list');
        });
    }

    //$scope.getRoleList = function () {
    //    var RoleListData = AJService.PostDataToAPI("Master/getRole");
    //    RoleListData.then(function (RoleListData) {
    //        $scope.RoleList = RoleListData.data;
    //    }, function () {
    //        alert('Error in RoleList By List');
    //    });
    //}

    //$scope.GetCustomProductList = function (value) {
    //    
    //    var ProductType = {
    //        Id: value,
    //    };
    //    /* end here*/
    //    var getCustomProductList = AJService.PostDataToAPI("CommonList/getCustomProductList", ProductType);
    //    /*commented and added by DHarmveer on 23 mAy May to Change VAriable Name for fetch record */
    //    // ProductTypeList.then(function (SubProductTypeList) {
    //    getCustomProductList.then(function (SubProductTypeList) {
    //        $scope.CustomProductList = SubProductTypeList.data;
    //    }, function () {
    //        alert('Error in getting SubProduct Type List');
    //    });
    //}
    //
    /*
    // Get Gender Values
    $scope.GetGenderList = function () {
        var getGenderList = AJService.GetDataFromAPI("master/getGenderList", null);
        getGenderList.then(function (gender) {
            $scope.gender = gender.data.genderData;
        }, function () {
            alert('Error in getting gender list');
        });
    }

    // Get Language Values
    $scope.GetLanguageList = function () {
        var getLanguageList = AJService.GetDataFromAPI("master/getLanguageList", null);
        getLanguageList.then(function (language) {
            $scope.language = language.data.languageData;
        }, function () {
            alert('Error in getting language list');
        });
    }

    // Get Profession Values
    $scope.GetProfessionList = function () {
        var getProfessionList = AJService.GetDataFromAPI("master/getProfessionList", null);
        getProfessionList.then(function (profession) {
            $scope.profession = profession.data.professionData;
        }, function () {
            alert('Error in getting profession list');
        });
    }

    // Get Hobbies Values
    $scope.GetHobbiesList = function () {
        var getHobbiesList = AJService.GetDataFromAPI("master/getHobbiesList", null);
        getHobbiesList.then(function (hobbies) {
            $scope.hobbies = hobbies.data.hobbiesData;
        }, function () {
            alert('Error in getting hobbies list');
        });
    }

    // Get Area Of Interest Values
    $scope.GetAreaOfInterestList = function () {        
        var getAreaOfInterestList = AJService.GetDataFromAPI("master/getAreaOfInterestList", null);
        getAreaOfInterestList.then(function (areaOfInterest) {
            $scope.areaOfInterest = areaOfInterest.data.areaOfInterestData;
        }, function () {
            alert('Error in getting area of interest list');
        });
    }

    // Get Job Nature Values
    $scope.GetJobNatureList = function () {
        var getJobNatureList = AJService.GetDataFromAPI("master/getJobNatureList", null);
        getJobNatureList.then(function (jobNature) {
            $scope.jobNature = jobNature.data.jobNatureData;
        }, function () {
            alert('Error in getting job nature list');
        });
    }

    // Get Job Type Values
    $scope.GetJobTypeList = function () {
        var getJobTypeList = AJService.GetDataFromAPI("master/getJobTypeList", null);
        getJobTypeList.then(function (jobType) {
            $scope.jobType = jobType.data.jobTypeData;
        }, function () {
            alert('Error in getting job type list');
        });
    }

    // Get Resident Values
    $scope.GetResidentList = function () {
        var getResidentList = AJService.GetDataFromAPI("master/getResidentList", null);
        getResidentList.then(function (resident) {
            $scope.resident = resident.data.residentData;
        }, function () {
            alert('Error in getting resident list');
        });
    }

    // Get Id Type Values
    $scope.GetIdTypeList = function () {
        var getIdTypeList = AJService.GetDataFromAPI("master/getIdTypeList", null);
        getIdTypeList.then(function (idType) {
            $scope.idType = idType.data.idTypeData;
        }, function () {
            alert('Error in getting id type list');
        });
    }

    // Get Country List
    function getCountryList() {
        var getCountryList = AJService.GetDataFromAPI("geography/getcountrylist", null);
        getCountryList.then(function (country) {
            $scope.CountryList = country.data;
        }, function () {
            alert('Error in getting records');
        });
    }

    // Get State List
    $scope.GetStateList = function() {
        var stateList = AJService.GetDataFromAPI("geography/GetAllState", null);
        stateList.then(function (stateList) {
            $scope.stateList = stateList.data;
                    }, function () {
            alert('Error in getting state list');
        });
    }

    // Get all city list
    $scope.GetAllCities = function () {
        var cityList = AJService.GetDataFromAPI("geography/GetAllCities", null);
        cityList.then(function (cityList) {
            $scope.cityList = cityList.data;
        }, function () {
            alert('Error in getting state list');
        });
    }

    // Get all locality list
    $scope.GetAllLocalities = function () {
        var Geography = {
            Code: "",
            Name: $scope.nameLocality,
            ParentId: $scope.selectedOptionCity,
            PinCode: $scope.Pin
        };
        var localityList = AJService.GetDataFromAPI("geography/getAllLocalities ", null);
        localityList.then(function (localitylist) {
            $scope.localties = localitylist.data;
        }, function () {
            alert('Error in getting state list');
        });
    }

    // Get State List by state id
    $scope.GetCityListByStateId = function (StateId) {
        if (StateId != null) {
            var State = {
                Id: StateId
            };
            var cityList = AJService.PostDataToAPI("geography/CityListByStateId", State);
            cityList.then(function (cityList) {
                $scope.cityList = cityList.data;
            }, function () {
                alert('Error in getting city list');
            });
        }
        else {
            alert("StateId not found");
        }
    }

    // Get Relation Values
    $scope.GetRelationList = function () {
        var getRelationList = AJService.GetDataFromAPI("master/getRelationList", null);
        getRelationList.then(function (relation) {
            $scope.relationList = relation.data.relationData;
        }, function () {
            alert('Error in getting relation list');
        });
    }

    // Get Validation Type Values
    $scope.GetValidationTypeList = function () {
        var getValidationTypeList = AJService.GetDataFromAPI("master/getValidationTypeList", null);
        getValidationTypeList.then(function (validationType) {
            $scope.validationType = validationType.data.validationTypeData;
        }, function () {
            alert('Error in getting validation type list');
        });
    }
    */

    $scope.GetPaymentPeriodList = function () {
        var getPaymentPeriodList = AJService.GetDataFromAPI("CommonList/getPaymentPeriodList", null);
        getPaymentPeriodList.then(function (PaymentPeriod) {
            $scope.PaymentPeriodeList = PaymentPeriod.data.query;
        }, function () {
            alert('Error in getting Payment Period list');
        });
    }

    $scope.GetContractTypeList = function () {
        var getContractTypeList = AJService.GetDataFromAPI("CommonList/getContractTypeList", null);
        getContractTypeList.then(function (ContractType) {
            $scope.ContractTypeList = ContractType.data.query;
        }, function () {
            alert('Error in getting contact type list');
        });
    }


    $scope.getTerritoryRightsList = function () {
        var TerritoryRightsList = AJService.GetDataFromAPI("CommonList/getTerriteryRights", null);
        TerritoryRightsList.then(function (TerritoryRightsList) {
            $scope.TerritoryList = TerritoryRightsList.data.query;
        }, function () {
            alert('Error in getting Territery Rights List');
        });
    }

    $scope.GetServiceList = function () {
        var ServiceList = AJService.GetDataFromAPI("CommonList/getServiceList", null);
        ServiceList.then(function (Service) {
            $scope._serviceList = Service.data.query;
        }, function () {
            alert('Error in getting Service List');
        });
    }

    $scope.GetSubServiceList = function () {
        var ServiceList = AJService.GetDataFromAPI("CommonList/getSubServiceList", null);
        ServiceList.then(function (Service) {
            $scope._subServiceList = Service.data.query;
        }, function () {
            alert('Error in getting Service List');
        });
    }


    $scope.GetAuthorTypeList = function () {
        var AuthorTypeList = AJService.GetDataFromAPI("CommonList/getAuthorTypeList", null);
        AuthorTypeList.then(function (AuthorType) {
            $scope._authorTypeList = AuthorType.data.query;
        }, function () {
            alert('Error in getting Author Type List');
        });
    }


    $scope.getFrequencyList = function () {
        var getFrequencyList = AJService.GetDataFromAPI("CommonList/GetFrequencyList", null);
        getFrequencyList.then(function (Frequency) {
            $scope.FrequencyList = Frequency.data;
        }, function () {
            alert('Error in getting Frequency List');
        });
    }

    //Get Common Uploaded Document 
    var temp_MasterName = '';
    var temp_MasterId = 0;
    $scope.GetCommonUploadDocumentList = function (Name, Id) {
        temp_MasterName = Name;
        temp_MasterId = Id;
        var DocumentList = AJService.GetDataFromAPI("Master/GetCommonUploadDocumentList?MasterName=" + Name + "&MasterId=" + Id, null);
        DocumentList.then(function (msg) {
            $scope.Docurl = msg.data;
        }, function () {
            alert('Error in getting Document list');
        });
    }

    //Delete Common Uploaded Document 
    $scope.RemoveCommonUploadDocument = function (UploadDocumentId) {
        var _mobj = {
            Id: UploadDocumentId,
            EnteredBy: $("#enterdBy").val(),
        };
        //SweetAlert.swal({
        //    title: "Are you sure?",
        //    text: "You will not be able to recover this Licensee detail! ",
        //    type: "warning",
        //    showCancelButton: true,
        //    confirmButtonColor: "#DD6B55",
        //    confirmButtonText: "Yes, delete it!",
        //    closeOnConfirm: false,
        //    closeOnCancel: true
        //},
        //function (Confirm) {
        //    if (Confirm) {
        //        blockUI.start();
                var Status = AJService.PostDataToAPI("Master/DeleteCommonUploadDocumentDetails", _mobj);
                Status.then(function (msg) {
                    if (msg.data != "OK") {
                        //SweetAlert.swal(msg.data);
                        alert('Error in delete document details.');
                    }
                    else {
                        //SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                        //blockUI.stop();
                        $scope.GetCommonUploadDocumentList(temp_MasterName, temp_MasterId);
                    }
                    //{
                    //    angular.element(document.getElementById('angularid')).scope().$scope.GetCommonUploadDocumentList(temp_MasterName, temp_MasterId);
                    //}
                });
        //    }
        //});
        //blockUI.stop();
    }


}


