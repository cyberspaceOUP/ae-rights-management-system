
app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {
    
    app.expandControllerA($scope, AJService, $window);
    app.expandControllerProductDetails($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);

    $scope.PageMode = $("input:hidden[id$=hid_UpdateMode]").val();
   

    $scope.AddendumId = $("input:hidden[id$=hid_AddendumId]").val();

    if ($scope.AddendumId > 0) {
        var mobj_Addendum = {
            Id: $scope.AddendumId,
        }

        var AddendumDetails = AJService.PostDataToAPI("Addendum/getAddendumDetailsByAddendumId", mobj_Addendum);
        AddendumDetails.then(function (Addendum) {
            var AddendumDetails = Addendum.data.AddendumDetails1;

            var Addendumtype = "";
            var BalanceQuantityCarryForward ="";


            if (AddendumDetails.AddendumType == "T") {
                Addendumtype = "Term Addendum";
            }
            else if (AddendumDetails.AddendumType == "Q") {
                Addendumtype = "Quantity Addendum";
            }
            else if (AddendumDetails.AddendumType == "B") {
                Addendumtype = "Balance Stock Addendum";
            }
            else if (AddendumDetails.AddendumType == "R") {
                Addendumtype = "Royalty Change Addendum";
            }
            else if (AddendumDetails.AddendumType == "TQ") {
                Addendumtype = "Term and Quantity Addendum";
            }
            else if (AddendumDetails.AddendumType == "TR") {
                Addendumtype = "Term and Royalty Change Addendum";
            }
            else if (AddendumDetails.AddendumType == "QR") {
                Addendumtype = "Quantity and Royalty Change Addendum";
            }
            else if (AddendumDetails.AddendumType == "TQR") {
                Addendumtype = "Term, Quantity and Royalty Change Addendum";
            }


            if (AddendumDetails.BalanceQuantityCarryForward == "Y") {
                BalanceQuantityCarryForward = "Yes";
            }
            else if (AddendumDetails.BalanceQuantityCarryForward == "N") {
                BalanceQuantityCarryForward = "No";
            }


            $scope.ProductModel = {
                AddendumDate: $scope.chnageDateFormat(AddendumDetails.AddendumDate),
                AddendumType: Addendumtype,
                Periodofagreement: AddendumDetails.Periodofagreement,
                ExpiryDate: $scope.chnageDateFormat(AddendumDetails.ExpiryDate),
                Remarks: AddendumDetails.Remarks,
                FirstImpressionWithinDate: $scope.chnageDateFormat(AddendumDetails.FirstImpressionWithinDate),
                NoOfImpressions: AddendumDetails.NoOfImpressions == 0 ? "" : AddendumDetails.NoOfImpressions,
                BalanceQuantityCarryForward: BalanceQuantityCarryForward,
                AddendumQuantity: AddendumDetails.AddendumQuantity == 0 ? "" : AddendumDetails.AddendumQuantity,
                RoyaltyTerms: AddendumDetails.RoyaltyTerms,
            };

            if (AddendumDetails.AddendumDetailsRoyalty != null) {
                $scope.RoyalslabList = AddendumDetails.AddendumDetailsRoyalty;                
            }

            if (AddendumDetails.IAddendumFileDetails.length > 0) {
                $scope.documentshow = true;
                $scope.Docurl = AddendumDetails.IAddendumFileDetails;
                if ($scope.Docurl.length > 0) {
                    $scope.documentshow = true;
                }
                else {
                    $scope.documentshow = false;
                }
            }



        }, function () {
            //alert('Error in Getting Addendum Details');
        });

        $scope.Title = "Update";

    }

    $scope.ProductLicenseId = $("input:hidden[id$=hid_licenseId]").val();
    $scope.Checkprintquantitytype = true;
    if ($scope.ProductLicenseId > 0) {
        var ProductLicense = {
            Id: $scope.ProductLicenseId,
        }

        var ProductLicenseDetails = AJService.PostDataToAPI("Addendum/getLicenseDetailsByLicenseId", ProductLicense);
        ProductLicenseDetails.then(function (ProductLicense) {
            var ProductLicense = ProductLicense.data;
            $scope.ProductLicenseDetails = [ProductLicense];
            $scope.ProductId = ProductLicense.ProductId;

            angular.element(document.getElementById('angularid')).scope().ProductSerach(ProductLicense.ProductId);

            setTimeout(function () {
                //fetch Kit Details List
                app.expandControllerKitISBNLIst($scope, AJService);
                angular.element(document.getElementById('angularid')).scope().KitISBNDetailsList(ProductLicense.ProductId);
            }, 300);

            //hide "Balance Quantity Carry Forward" and "Additional Number of Copies" in case of print quantity type is Unrestricted
            if (ProductLicense.printquantitytype == "Unrestricted") {
                $scope.Checkprintquantitytype = false;
            }

        }, function () {
            //alert('Error in Getting Product License Details');
        });

    }

    if ($scope.ProductLicenseId > 0) {
        LicenseId = $scope.ProductLicenseId;

        var getAddendumDetailsByLicenseId = AJService.PostDataToAPI("Addendum/getAddendumDetailsByLicenseId?LicenseId=" + LicenseId, null);
        getAddendumDetailsByLicenseId.then(function (AddendumList) {
            $scope.AddendumList = AddendumList.data.AddendumDetails;
        }, function () {
            //alert('Error in Getting Product License Details');
        });

    }

    $scope.chnageDateFormat = function (datestring) {
        var returndate = "";

        if (datestring != null) {
            if (datestring.indexOf("T") > 1) {
                var datestringformat = datestring.split("T")[0];
                var marr_date = datestringformat.split("-");
                //returndate = marr_date[1] + "/" + marr_date[2] + "/" + marr_date[0]
                returndate = marr_date[2] + "/" + marr_date[1] + "/" + marr_date[0]
            }
            else {
                returndate = datestring;
            }
        }

        return returndate;

    };

    $scope.AddendumDocEntry = function (ProductModel) {
        $scope.submitted = true;
        if ($scope.userForm.$valid) {
            var marr_fileDetails = [];
            var FileNameArray = $('#dropZone0').find('.fileNameClass');
            var UploadFileNameArray = $("#hid_Uploads").val().split(",");

            FileNameArray.each(function (i) {
                var mobj_fileDetails = {
                    AddendumId: $scope.AddendumId,
                    LicenseId: $scope.ProductLicenseId,
                    FileName: $(this).val(),
                    UploadFileName: UploadFileNameArray[i],
                }
                marr_fileDetails.push(mobj_fileDetails);
            });

            if (marr_fileDetails.length == 0) {
                $scope.userForm.$valid = false;
                SweetAlert.swal("Validation!", "Please upload atleast one document !", "", "error");
            }

            if ($scope.userForm.$valid) {
                $scope.AddendumEntry(marr_fileDetails);
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
        $scope.userForm.$valid = true;
    }


    $scope.AddendumEntry = function (marr_fileDetails) {

        var _mobjAddendum = {
            Id: $scope.AddendumId,
            ProductId: $scope.ProductId,
            LicenseId: $scope.ProductModel.ProductLicenseId,
            IAddendumFileDetails: marr_fileDetails,
            EnteredBy: $("#enterdBy").val(),

        };

        var ProductStatus = AJService.PostDataToAPI('Addendum/InsertAddendumFileDetails', _mobjAddendum);

        ProductStatus.then(function (msg) {

            if (msg.data == "Duplicate") {
                SweetAlert.swal("Error!", "Duplicate. already exist !", "", "error");
            }
            else if (msg.data != "OK") {
                SweetAlert.swal('There is some problem.', '', "Try agian");
            }
            else {
                SweetAlert.swal({
                    title: "Insert successfully.",
                    text: "",
                    type: "success"
                },
                   function () {
                       $('form[name*=user]').attr("method", "post");
                       $('form[name*=user]').submit();
                   });
            }

        },
        function () {
            alert('There is some error in the system');
        });
    }


    $scope.RemoveDocumentById = function (mobjurl) {
        var docid = mobjurl.Id;
        var file = mobjurl.UploadFileName;
        //  alert($scope.NoticeBoard.NBId);
        var FileDetails = { Id: docid };
        var DeleteDocument = AJService.PostDataToAPI("Addendum/DeleteFile", FileDetails);

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
                            var index = $scope.Docurl.indexOf(mobjurl);
                            $scope.Docurl.splice(index, 1);
                            if ($scope.Docurl.length == 0) {
                                $("#doclistid").attr('style', 'display:none');
                                $scope.documentshow = false;
                            }

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




    
    
 
        $scope.ViewAddenDumDetailsList = function (Value) {
    
        if (Value > 0) {
            var mobj_Addendum = {
                Id: Value,
            }

            var AddendumDetails = AJService.PostDataToAPI("Addendum/getAddendumDetailsByAddendumId", mobj_Addendum);
            AddendumDetails.then(function (Addendum) {
                var AddendumDetails = Addendum.data.AddendumDetails1;
                var AddendumtypeView = "";
                var BalanceQuantityCarryForward = "";
              
                if (AddendumDetails.AddendumType == "T") {
                    AddendumtypeView = "Term Addendum";
                }
                else if (AddendumDetails.AddendumType == "Q") {
                    AddendumtypeView = "Quantity Addendum";
                }
                else if (AddendumDetails.AddendumType == "B") {
                    AddendumtypeView = "Balance Stock Addendum";
                }
                else if (AddendumDetails.AddendumType == "R") {
                    AddendumtypeView = "Royalty Change Addendum";
                }
                else if (AddendumDetails.AddendumType == "TQ") {
                    AddendumtypeView = "Term and Quantity Addendum";
                }
                else if (AddendumDetails.AddendumType == "TR") {
                    AddendumtypeView = "Term and Royalty Change Addendum";
                }
                else if (AddendumDetails.AddendumType == "QR") {
                    AddendumtypeView = "Quantity and Royalty Change Addendum";
                }
                else if (AddendumDetails.AddendumType == "TQR") {
                    AddendumtypeView = "Term, Quantity and Royalty Change Addendum";
                }


                if (AddendumDetails.BalanceQuantityCarryForward == "Y") {
                    BalanceQuantityCarryForward = "Yes";
                }
                else if (AddendumDetails.BalanceQuantityCarryForward == "N") {
                    BalanceQuantityCarryForward = "No";
                }
                $scope.ProductModelView = {
                    AddendumDateView: $scope.chnageDateFormat(AddendumDetails.AddendumDate),
                    AddendumTypeView: AddendumtypeView,
                    PeriodofagreementView: AddendumDetails.Periodofagreement,
                    ExpiryDateView: $scope.chnageDateFormat(AddendumDetails.ExpiryDate),
                    RemarksView: AddendumDetails.Remarks,
                    FirstImpressionWithinDateView: $scope.chnageDateFormat(AddendumDetails.FirstImpressionWithinDate),
                    NoOfImpressionsView: AddendumDetails.NoOfImpressions == 0 ? "" : AddendumDetails.NoOfImpressions,
                    BalanceQuantityCarryForwardView: BalanceQuantityCarryForward,
                    AddendumQuantityView: AddendumDetails.AddendumQuantity == 0 ? "" : AddendumDetails.AddendumQuantity,
                    RoyaltyTermsView: AddendumDetails.RoyaltyTerms,
                    AddendumCodeView: Addendum.data.AddendumDetails1.AddendumCode,
                };

                if (AddendumDetails.AddendumDetailsRoyalty != null) {
                    $scope.RoyalslabListView_List = AddendumDetails.AddendumDetailsRoyalty;
                } else {
                    $scope.RoyalslabListView_List = [];
                }

                if (AddendumDetails.IAddendumFileDetails.length > 0) {
                    $scope.documentshow = true;
                    $scope.DocurlView = AddendumDetails.IAddendumFileDetails;
                    if ($scope.DocurlView.length > 0) {
                        $scope.documentshow = true;
                    }
                    else {
                        $scope.documentshow = false;
                    }
                }



            }, function () {
                //alert('Error in Getting Addendum Details');
            });



        }

    }

   

});


