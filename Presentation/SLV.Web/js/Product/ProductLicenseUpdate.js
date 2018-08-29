app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {
    app.expandControllerViewProductDetails($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);

    setTimeout(function () {
        //$scope.ProductModel.EFilesCost = '';
        //$scope.ProductModel.EFilesReceivedDate = '';
        //$scope.ProductModel.EFilesRequestDate = '';
        //$scope.ProductModel.Mode = '';
        //$scope.ProductModel.LicensorCopiesSentDate = '';

        if ($scope.ProductModel.EFilesCost == '' || $scope.ProductModel.EFilesCost == 'undefined') {
            $('select[name=Mode]').prop("selectedIndex", 1);
        }

    }, 10);

    //Call on Button Submit
    //$scope.productLicenseEntryForm = function (ProductModel) {


    //    FileNameArray = $('#dropZone0').find('.fileNameClass');

    //    if (FileNameArray[0] == null) {

    //        $scope.UploadContractReq = true;

    //        $scope.userForm.$valid = false;

    //    }
    //    else {
    //        if ($('.fileNameClass').val() == "") {
    //            $scope.UploadContractReq = false;
    //            $scope.UploadExcelfileNameReq = true;
    //            $scope.userForm.$valid = false;
    //        }
    //        else {
    //            $scope.UploadContractReq = false;
    //            $scope.UploadExcelfileReq = false;

    //            $scope.userForm.$valid = true;
    //        }


    //    }


    //    $scope.submitted = true;
    //    if ($scope.userForm.$valid) {
    //        $scope.productLicenseUpdate(ProductModel);
    //        // set form default state
    //        $scope.userForm.$setPristine();
    //        // set form is no submitted
    //        $scope.submitted = false;
    //        return;
    //    }
    //}


    $scope.productLicenseEntryForm = function () {


        FileNameArray = $('#dropZone0').find('.fileNameClass');

        if (FileNameArray[0] == null) {

            $scope.UploadContractReq = true;
            $scope.UploadExcelfileNameReq = false;
            $scope.userForm.$valid = false;

        }
        else {
            if ($('.fileNameClass').val() == "") {
                $scope.UploadContractReq = false;
                $scope.UploadExcelfileNameReq = true;
                $scope.userForm.$valid = false;
            }
            else {
                $scope.UploadContractReq = false;
                $scope.UploadExcelfileReq = false;

                $scope.userForm.$valid = true;
            }


        }

        //if ($scope.ProductModel.EFilesCost == "--") {
        //    $scope.userForm.ProductModel.EFilesCost.$error.required = true;
        //    $scope.userForm.$valid = false;
        //}


        $scope.submitted = true;
        if ($scope.userForm.$valid) {
            $scope.productLicenseUpdate();
            // set form default state
            $scope.userForm.$setPristine();
            // set form is no submitted
            $scope.submitted = false;
            return;
        }
    }

    $scope.productLicenseUpdate = function () {

        var marr_UpdateDetails = [];

        var marr_fileDetails = [];
        var FileNameArray = $('#dropZone0').find('.fileNameClass');
        var UploadFileNameArray = $("#hid_Uploads").val().split(",");

        FileNameArray.each(function (i) {
            var mobj_fileDetails = {
                LicenseId: $scope.ProductLicenseId,
                FileName: $(this).val(),
                UploadFileName: UploadFileNameArray[i],
            }
            marr_fileDetails.push(mobj_fileDetails);
        });




        var mobj_UpdateDetails = {
            LicenseId: $scope.ProductLicenseId,
            //LicensorCopiesSentDate: $scope.ProductModel.LicensorCopiesSentDate,
            LicensorCopiesSentDate: convertDate($("input[type=text][name*=LicensorCopiesSentDate]").val()),
            EFilesCost: $scope.ProductModel.EFilesCost,
            //EFilesRequestDate: $scope.ProductModel.EFilesRequestDate,
            //EFilesReceivedDate: $scope.ProductModel.EFilesReceivedDate,
            EFilesRequestDate: convertDate($("input[type=text][name*=EFilesRequestDate]").val()),
            EFilesReceivedDate: convertDate($("input[type=text][name*=EFilesReceivedDate]").val()),
            Mode: $scope.ProductModel.Mode,
            ILicenseUpdateFileDetails: marr_fileDetails,
            EnteredBy: $("#enterdBy").val(),
            //AgreementDate: $scope.ProductModel.AgreementDate,
            AgreementDate: convertDate($("[name*=AgreementDate]").val()),
            effectivedate: convertDate($('#EffectiveDate').val()),
            contractperiodinmonth: 0, //$scope.ProductModel.ContractPeriod, //change by prakash on 30 May, 2017
            Expirydate: convertDateMDY($('#ExpiryDate').val()),
        }


        SweetAlert.swal({
            title: "Are you sure?",
            text: "",
            type: "info",
            showCancelButton: true,
            confirmButtonColor: "#8CD4F5",
            confirmButtonText: "Yes",
            closeOnConfirm: false,
            closeOnCancel: true,
            showLoaderOnConfirm: true
        },
            function (Confirm) {
                if (Confirm) {


                    var ProductStatus = AJService.PostDataToAPI('ProductLicense/InsertProductLicenseUpdateDetails', mobj_UpdateDetails);



                    ProductStatus.then(function (msg) {

                        if (msg.data.status == "Duplicate") {
                            SweetAlert.swal("Error!", "Duplicate. already exist !", "", "error");
                        }
                        else if (msg.data.status != "OK") {
                            SweetAlert.swal('Error', 'There is some problem.', "error");
                        }
                        else {
                            if ($scope.ProductLicenseId > 0) {
                                SweetAlert.swal('Updated successfully.', '', "success");
                                $("#hid_ProductId").val($scope.productId);
                            }
                            else {
                                SweetAlert.swal('Insert successfully.', '', "success");
                                $("#hid_ProductId").val(msg.data.Id);
                            }
                            //$('form[name*=user]').attr("method", "post");
                            //$('form[name*=user]').submit();

                            //  location.href = $(".backtosearch").attr("href");

                            //location.href = "../../Home/Dashboard/Dashboard";
                            location.href = "../../Product/ProductLicense/view?Id=" + mobj_UpdateDetails.LicenseId;
                        }

                    },
                    function () {
                        SweetAlert.swal("", "Please validate details.", "warning");
                        //alert('There is some error in the system');
                    });

                }

            });
    }

    /*For Update Case*/
    $scope.SetLicensorCopiesSentDate = function (datetext) {
        if ($scope.ProductModel.LicensorCopiesSentDate == undefined && $scope.ProductModel.LicensorCopiesSentDate !== $(datetext).val()) {
            $scope.ProductModel.LicensorCopiesSentDate = $(datetext).val();
        }
    }

    $scope.SetEFilesRequestDate = function (datetext) {
        if ($scope.ProductModel.EFilesRequestDate == undefined && $scope.ProductModel.EFilesRequestDate !== $(datetext).val()) {
            $scope.ProductModel.EFilesRequestDate = $(datetext).val();
        }
    }

    $scope.SetEFilesReceivedDate = function (datetext) {
        if ($scope.ProductModel.EFilesReceivedDate == undefined && $scope.ProductModel.EFilesReceivedDate !== $(datetext).val()) {
            $scope.ProductModel.EFilesReceivedDate = $(datetext).val();
        }
    }

    $scope.SetAggrementDate = function (datetext) {
        $scope.ProductModel.AgreementDate = $(datetext).val();
        $('#EffectiveDate').val($(datetext).val());
    }


    $scope.CalculateExpiry = function () {
        PeriodIdValue = $scope.ProductModel.ContractPeriod;
        //var CDate = $("[name$=RequestDate]").val();

        var CDate = $("[name$=AgreementDate]").val();

        if (PeriodIdValue == undefined || PeriodIdValue == "0" || PeriodIdValue == "" || CDate == undefined || CDate == "" || CDate == "undefined//undefined") {
            $scope.ExpiryDate = "";
            return false;
        }

        //var CurrentDate = new Date(convertDate($("[name$=RequestDate]").val()));
        var CurrentDate = new Date(convertDate($("[name$=AgreementDate]").val()));
        CurrentDate.setMonth(CurrentDate.getMonth() + parseInt(PeriodIdValue));
        var today = CurrentDate;
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!

        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd
        }
        if (mm < 10) {
            mm = '0' + mm
        }
        var today = dd + '/' + mm + '/' + yyyy;
        $scope.ProductModel.ExpiryDate = today;
        $("[name$=ExpiryDate]").val(today);
    }

    //$scope.SetEffectiveDate = function (datetext) {
    //    if ($scope.ProductModel.EffectiveDate == undefined && $scope.ProductModel.EffectiveDate !== $(datetext).val()) {
    //        $scope.ProductModel.EffectiveDate = $(datetext).val();
    //    }
    //}

    function convertDateMDY(date) {
        if (date != undefined) {
            var datearray = date.split("/");
            return datearray[1] + '/' + datearray[0] + '/' + datearray[2];
        }
        else {
            return null;
        }

    }

    function convertDate(date) {
        if (date != undefined) {
            var datearray = date.split("/");
            return datearray[1] + '/' + datearray[0] + '/' + datearray[2];
        }
        else {
            return null;
        }

    }
    /*End Update Case*/

    
    
    var _href = location.href.toLocaleLowerCase();


    if (_href.trim().indexOf("product/productlicense/view") > 0) {
        $scope.ThirdpartypermissionProdunctFlag = true;

    } else if (_href.trim().indexOf("productlicense/updateproductlicense") > 0) {
        $scope.ThirdpartypermissionProdunctFlag = true;
    }
    else {
        $scope.ThirdpartypermissionProdunctFlag = false;
        $scope.ThirdpartypermissionProdunctFlag = false;
    }


    //For Addendum List 
    $scope.ProductLicenseId = $("input:hidden[id$=hid_LicenseeId]").val();
    if ($scope.ProductLicenseId > 0) {
        LicenseId = $scope.ProductLicenseId;

        var getAddendumDetailsByLicenseId = AJService.PostDataToAPI("Addendum/getAddendumDetailsByLicenseId?LicenseId=" + LicenseId, null);
        getAddendumDetailsByLicenseId.then(function (AddendumList) {
            $scope.AddendumList = AddendumList.data.AddendumDetails;
        }, function () {
            //alert('Error in Getting Product License Details');
        });

    }

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

            //hide "Balance Quantity Carry Forward" and "Additional Number of Copies" in case of print quantity type is Unrestricted
            if (ProductLicense.printquantitytype == "Unrestricted") {
                $scope.Checkprintquantitytype = false;
            }

            setTimeout(function () {
                //fetch Kit Details List
                app.expandControllerKitISBNLIst($scope, AJService);
                angular.element(document.getElementById('angularid')).scope().KitISBNDetailsList(ProductLicense.ProductId);
            }, 300);

        }, function () {
            //alert('Error in Getting Product License Details');
        });

    }

    //For Addendum Detail View //added on 01 Sep, 2017
    $scope.AddendumId = $("input:hidden[id$=hid_AddendumId]").val();
    $scope.AddendumDetailsInLicense = '';
    if ($scope.AddendumId > 0) {
        var mobj_Addendum = {
            Id: $scope.AddendumId,
        }

        var AddendumDetails = AJService.PostDataToAPI("Addendum/getAddendumDetailsByAddendumId", mobj_Addendum);
        AddendumDetails.then(function (Addendum) {
            var AddendumDetails = Addendum.data.AddendumDetails1;
            $scope.AddendumDetailsInLicense = Addendum.data.AddendumDetails1.AddendumCode;

            var Addendumtype = "";
            var BalanceQuantityCarryForward = "";


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


          
                $scope.AddendumDate= $scope.chnageDateFormat(AddendumDetails.AddendumDate)
                $scope.AddendumType= Addendumtype
                $scope.Periodofagreement= AddendumDetails.Periodofagreement
                $scope.ExpiryDate= $scope.chnageDateFormat(AddendumDetails.ExpiryDate)
                $scope.Remarks= AddendumDetails.Remarks
                $scope.FirstImpressionWithinDate= $scope.chnageDateFormat(AddendumDetails.FirstImpressionWithinDate)
                $scope.NoOfImpressions = AddendumDetails.NoOfImpressions == 0 ? "" : AddendumDetails.NoOfImpressions;
                $scope.BalanceQuantityCarryForward= BalanceQuantityCarryForward
                $scope.AddendumQuantity= AddendumDetails.AddendumQuantity
                $scope.RoyaltyTerms= AddendumDetails.RoyaltyTerms
            

            if (AddendumDetails.AddendumDetailsRoyalty != null) {
                $scope.RoyalslabList = AddendumDetails.AddendumDetailsRoyalty;
            }

            if (AddendumDetails.IAddendumFileDetails.length > 0) {
                //$scope.documentshow = true;
                $scope.Docurl = AddendumDetails.IAddendumFileDetails;
                //if ($scope.Docurl.length > 0) {
                //    $scope.documentshow = true;
                //}
                //else {
                //    $scope.documentshow = false;
                //}
            }

        }, function () {
            //alert('Error in Getting Addendum Details');
        });

        //$scope.Title = "Update";

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
    //end For Addendum Detail View 
   
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