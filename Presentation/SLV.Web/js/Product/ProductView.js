app.expandControllerProductViewDetails = function ($scope, AJService, $window) {
        
    $scope.OUPAuthorSuggestionCntrl = true;

    $scope.ProductDetailsViewMode = function (Id) {

        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var ProductData = {
                Id: Id
            };
            // call API to fetch temp Department list basis on the FlatId
            var ViewStatus = AJService.PostDataToAPI('ProductMaster/GetProductDetails', ProductData);

            ViewStatus.then(function (msg) {
                if (msg != null) {

                    //Populate Product Details block
                    if (msg.data[0].ProductCode != null) {
                        $scope.ProductCode = msg.data[0].ProductCode
                    }

                    if (msg.data[0].Division != null) {
                        $scope.Division = msg.data[0].Division
                    }
                    else {
                        $scope.Division = "--"
                    }
                    if (msg.data[0].SubDivision != null) {
                        $scope.SubDivision = msg.data[0].SubDivision
                    }
                    else {
                        $scope.SubDivision = "--"
                    }
                    if (msg.data[0].ProductCategory != null) {
                        $scope.ProductCategory = msg.data[0].ProductCategory
                    }
                    else {
                        $scope.ProductCategory = "--"
                    }
                    if (msg.data[0].ProductType != null) {
                        $scope.ProductType = msg.data[0].ProductType
                    }
                    else {
                        $scope.ProductType = "--"
                    }
                    if (msg.data[0].SubProductType != null) {
                        $scope.SubProductType = msg.data[0].SubProductType
                    }
                    else {
                        $scope.SubProductType = "--"
                    }

                    //Populate Proprietor Details block
                    if (msg.data[0].ProprietorIsbn != null) {
                        $scope.ProprietorIsbn = msg.data[0].ProprietorIsbn
                    }
                    else {
                        $scope.ProprietorIsbn = "--"
                    }
                    if (msg.data[0].ProprietorProduct != null) {
                        $scope.ProprietorProduct = msg.data[0].ProprietorProduct
                    }
                    else {
                        $scope.ProprietorProduct = "--"
                    }
                    if (msg.data[0].ProprietorEdition != null) {
                        $scope.ProprietorEdition = msg.data[0].ProprietorEdition
                    }
                    else {
                        $scope.ProprietorEdition = "--"
                    }
                    if (msg.data[0].ProprietorCopyright != null) {
                        $scope.ProprietorCopyright = msg.data[0].ProprietorCopyright
                    }
                    else {
                        $scope.ProprietorCopyright = "--"
                    }
                    if (msg.data[0].ProprietorPublishingCompany != null) {
                        $scope.ProprietorPublishingCompany = msg.data[0].ProprietorPublishingCompany
                    }
                    else {
                        $scope.ProprietorPublishingCompany = "--"
                    }
                    if (msg.data[0].ProprietorPubCenter != null) {
                        $scope.ProprietorPubCenter = msg.data[0].ProprietorPubCenter
                    }
                    else {
                        $scope.ProprietorPubCenter = "--"
                    }
                    if (msg.data[0].ProprietorImprint != null) {
                        $scope.ProprietorImprint = msg.data[0].ProprietorImprint
                    }
                    else {
                        $scope.ProprietorImprint = "--"
                    }
                    if (msg.data[0].ProprietorAuthorCategory != null) {
                        $scope.ProprietorAuthorCategory = msg.data[0].ProprietorAuthorCategory
                    }
                    else {
                        $scope.ProprietorAuthorCategory = "--"
                    }
                    if (msg.data[0].ProprietorAuthorName != null) {
                        $scope.ProprietorAuthorName = msg.data[0].ProprietorAuthorName == "" ? "--" : msg.data[0].ProprietorAuthorName;
                    }
                    else {
                        $scope.ProprietorAuthorName = "--"
                    }
                    
                    //Populate OUP Details block
                    if (msg.data[0].OupIsbn != null) {
                        $scope.OupIsbn = msg.data[0].OupIsbn
                    }
                    else {
                        $scope.OupIsbn = "--"
                    }
                    if (msg.data[0].WorkingProduct != null) {
                        $scope.WorkingProduct = msg.data[0].WorkingProduct
                    }
                    else {
                        $scope.WorkingProduct = "--"
                    }
                    if (msg.data[0].WorkingSubProduct != null) {
                        $scope.WorkingSubProduct = msg.data[0].WorkingSubProduct
                    }
                    else {
                        $scope.WorkingSubProduct = "--"
                    }
                    if (msg.data[0].OupEdition != null) {
                        $scope.OupEdition = msg.data[0].OupEdition
                    }
                    else {
                        $scope.OupEdition = "--"
                    }
                    if (msg.data[0].Volume != null && msg.data[0].Volume != "") {
                        $scope.Volume = msg.data[0].Volume
                    }
                    else {
                        $scope.Volume = "--"
                    }
                    if (msg.data[0].imprintname != null) {
                        $scope.imprintname = msg.data[0].imprintname
                    }
                    else {
                        $scope.imprintname = "--"
                    }
                    if (msg.data[0].Language != null) {
                        $scope.Language = msg.data[0].Language
                    }
                    else {
                        $scope.Language = "--"
                    }
                    if (msg.data[0].Series != null) {
                        $scope.Series = msg.data[0].Series
                    }
                    else {
                        $scope.Series = "--"
                    }
                    if (msg.data[0].Derivatives != null) {
                        $scope.Derivatives = msg.data[0].Derivatives
                    }
                    else {
                        $scope.Derivatives = "--"
                    }
                    if (msg.data[0].orgisbn != null) {
                        $scope.orgisbnVisible = true
                        $scope.orgisbn = msg.data[0].orgisbn
                    }
                    else {
                        $scope.orgisbnVisible = false
                    }
                    if (msg.data[0].ProjectedDate != null) {
                        $scope.ProjectedDate = msg.data[0].ProjectedDate
                    }
                    else {
                        $scope.ProjectedDate = "--"
                    }
                    if (msg.data[0].CopyrightYear != null) {
                        $scope.CopyrightYear = msg.data[0].CopyrightYear
                    }
                    else {
                        $scope.CopyrightYear = "--"
                    }
                    if (msg.data[0].ProjectedPrice != null) {
                        $scope.ProjectedPrice = msg.data[0].ProjectedPrice
                    }
                    else {
                        $scope.ProjectedPrice = "--"
                    }
                    if (msg.data[0].ProjectedCurrency != null) {
                        $scope.ProjectedCurrency = msg.data[0].ProjectedCurrency
                    }
                    else {
                        $scope.ProjectedCurrency = "--"
                    }
                    if (msg.data[0].ProjectedCurrencySymbol != null) {
                        $scope.ProjectedCurrencySymbol = msg.data[0].ProjectedCurrencySymbol
                    }
                    else {
                        $scope.ProjectedCurrencySymbol = "--"
                    }

                    if (msg.data[0].OUPPubCenter != null) {
                        $scope.OUPPubCenter = msg.data[0].OUPPubCenter
                    }
                    else {
                        $scope.OUPPubCenter = "--"
                    }
                    if (msg.data[0].AuthorCategory != null) {
                        $scope.AuthorCategory = msg.data[0].AuthorCategory
                    }
                    else {
                        $scope.AuthorCategory = "--"
                    }
                    if (msg.data[0].AuthorName != null) {
                        $scope.AuthorName = msg.data[0].AuthorName
                    }
                    else {
                        $scope.AuthorName = "--"
                    }
                    if (msg.data[0].ProductCategory != null) {
                        if (msg.data[0].ProductCategory == 'Reprint') {
                            $scope.OUPAuthorSuggestionCntrl = false;
                        }
                    }
                    /*if (msg.data[0].LinkedProducttoPreviousEdition != null) { 
                        $scope.LinkedProducttoPreviousEditionVisible = true
                        $scope.LinkedProducttoPreviousEdition = msg.data[0].LinkedProducttoPreviousEdition
                    }
                    else {
                        $scope.LinkedProducttoPreviousEditionVisible = false
                    }
                    if (msg.data[0].PreviousProductISBN != null) {
                        $scope.PreviousProductISBNVisible = true
                        $scope.PreviousProductISBN = msg.data[0].PreviousProductISBN
                    }
                    else {
                        $scope.PreviousProductISBNVisible = false
                    }*/

                    if (msg.data[0].PreviousProductISBN != null) {
                        if (msg.data[0].orgisbn != null) {
                            $scope.PreviousProductISBNVisible = false
                            $scope.LinkedProducttoPreviousEditionVisible = false
                        }
                        else {
                            $scope.LinkedProducttoPreviousEditionVisible = true
                            $scope.LinkedProducttoPreviousEdition = "Yes"
                            $scope.PreviousProductISBNVisible = true
                            $scope.PreviousProductISBN = msg.data[0].PreviousProductISBN
                        }
                    }
                    else {
                        $scope.PreviousProductISBNVisible = false
                        $scope.LinkedProducttoPreviousEditionVisible = false
                    }

                    if (msg.data[0].FinalPublishingDate != null) {
                        $scope.FinalPublishingDateVisible = true
                        $scope.FinalPublishingDate = msg.data[0].FinalPublishingDate
                    }
                    else {
                        $scope.FinalPublishingDateVisible = false
                    }

                    if (msg.data[0].FinalPublishedTitle != null) {
                        $scope.FinalPublishedTitleVisible = true
                        $scope.FinalPublishedTitle = msg.data[0].FinalPublishedTitle
                    }
                    else {
                        $scope.FinalPublishedTitleVisible = false
                    }

                    if ($scope.ProductCategory == 'Original') {
                        $(".Proprietor").remove()
                    }

                    if (msg.data[0].thirdpartypermission != null) {
                        $scope.thirdpartypermission = msg.data[0].thirdpartypermission
                    }
                    else {
                        $scope.thirdpartypermission = "--"
                    }

                    //convert ISBN from 13 digit to 10 digit
                    setTimeout(function () {
                        var KitISBNConvert = AJService.GetDataFromAPI("ProductMaster/GetIsbnConvert?isbn13=" + msg.data[0].OupIsbn);
                        KitISBNConvert.then(function (isbn) {
                            $scope.IsbnConverted = isbn.data;
                        });
                    }, 100);

                    setTimeout(function () {
                        //fetch Kit Details List
                        app.expandControllerKitISBNLIst($scope, AJService);
                        angular.element(document.getElementById('angularid')).scope().KitISBNDetailsList(Id);
                    }, 300);

                }
                else {
                    SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                }
            });
        }
    }

    $scope.Product_AuthorContractLinks = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var ProductData = {
                Id: Id
            };
            // call API to fetch temp Department list basis on the FlatId
            var Product_AuthorContractLinks = AJService.PostDataToAPI('ProductMaster/GetAuthorContractLinks', ProductData);
            Product_AuthorContractLinks.then(function (status) {
                $scope.AuthorContractLinks_List = status.data;
            }, function () {
                //alert('Error in getting Author Contract Links list');
            });
        }
    }

    $scope.Product_ProductLicenseLinks = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var ProductData = {
                Id: Id
            };
            // call API to fetch temp Department list basis on the FlatId
            var ProductLicenseLinks = AJService.PostDataToAPI('ProductMaster/GetProductLicenseLinks', ProductData);
            ProductLicenseLinks.then(function (status) {
                $scope.ProductLicenseLinks_List = status.data;
            }, function () {
                //alert('Error in getting Product License Links list');
            });
        }
    }

    $scope.Product_RightsSellingMasterLinks = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var ProductData = {
                Id: Id
            };
            // call API to fetch temp Department list basis on the FlatId
            var RightsSellingMasterLinks = AJService.PostDataToAPI('ProductMaster/GetRightsSellingMasterLinks', ProductData);
            RightsSellingMasterLinks.then(function (status) {
                $scope.RightsSellingMasterLinks_List = status.data;
            }, function () {
                //alert('Error in getting Rights Selling Master Links list');
            });
        }
    }

    $scope.Product_PermissionsOutboundMasterLinks = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var ProductData = {
                Id: Id
            };           
            // call API to fetch temp Department list basis on the FlatId
            var PermissionsOutboundMaster = AJService.PostDataToAPI('ProductMaster/GetPermissionsOutboundMasterLinks', ProductData);
            PermissionsOutboundMaster.then(function (status) {
                $scope.PermissionsOutboundMasterLinks_List = status.data;
            }, function () {
                //alert('Error in getting Permissions Outbound Master Links list');
            });
        }
    }




}
