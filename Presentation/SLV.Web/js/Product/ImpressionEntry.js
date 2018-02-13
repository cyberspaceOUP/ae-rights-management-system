
app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {

    app.expandControllerA($scope, AJService, $window);
    app.expandControllerProductDetails($scope, AJService, $window);

    app.expandControllerAuthorContractDetails($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);

    $scope.ImpressionBalanceHidden = true;

    $scope.LicenseId = $("input:hidden[id$=hid_licenseId]").val();
    $scope.ProductId = $("input:hidden[id$=hid_productid]").val();
    $scope.ContractId = $("input:hidden[id$=hid_AuthorContractId]").val();

    if ($scope.ContractId > 0)
        $scope.AuthorContract($("input:hidden[id$=hid_AuthorContractId]").val());

    $scope.AddendumId = 0;
    if ($scope.LicenseId > 0) {
        var ProductLicense = {
            Id: $scope.LicenseId,
        }

        var ProductLicenseDetails = AJService.PostDataToAPI("Addendum/getLicenseDetailsByLicenseId", ProductLicense);
        ProductLicenseDetails.then(function (ProductLicense) {
            var ProductLicense = ProductLicense.data;
            $scope.ProductLicenseDetails = [ProductLicense];
            $scope.ProductId = ProductLicense.ProductId;
            $scope.AddendumId = ProductLicense.AddendumId;

            //if (ProductLicense.Impression == null || ProductLicense.Impression == 0) {
            //    $scope.ImpressionBalanceHidden = false;
            //    $scope.ImpressionBalance = "";
            //}
            //else {
            //    if (ProductLicense.ImpressionBalance == null || ProductLicense.ImpressionBalance == "") {
            //        $scope.ImpressionBalance = ProductLicense.Impression;
            //    }
            //    else {
            //        $scope.ImpressionBalance = ProductLicense.ImpressionBalance;
            //    }
            //}

            if (ProductLicense.BalanceQuantityCarryForward != 'N') {
                var _Licenseprintquantity = ProductLicense.Licenseprintquantity;
                var _LicenseAddendumQuantity = 0;
                var _ImpressionQuantityPrinted = 0;

                if (ProductLicense.LicenseAddendumQuantity.length > 0) {
                    for (var i = 0; i < ProductLicense.LicenseAddendumQuantity.length; i++) {
                        _LicenseAddendumQuantity += ProductLicense.LicenseAddendumQuantity[i];
                    }
                }

                if (_Licenseprintquantity != null && _Licenseprintquantity != 0) {
                    _LicenseAddendumQuantity += _Licenseprintquantity;
                }

                if (_LicenseAddendumQuantity == null || _LicenseAddendumQuantity == 0) {
                    $scope.ImpressionBalanceHidden = false;
                    $scope.ImpressionBalance = "";
                }
                else {
                    if (ProductLicense.ImpressionQuantityPrinted.length > 0) {
                        for (var i = 0; i < ProductLicense.ImpressionQuantityPrinted.length; i++) {
                            _ImpressionQuantityPrinted += ProductLicense.ImpressionQuantityPrinted[i];
                        }
                    }

                    $scope.ImpressionBalance = _LicenseAddendumQuantity - _ImpressionQuantityPrinted;

                }
            }
            else {
                var _LicenseAddendumQuantity1 = 0;
                var _ImpressionQuantityPrinted1 = 0;
                _LicenseAddendumQuantity1 = ProductLicense.LicenseAddendumQuantity1;

                if (ProductLicense.ImpressionQuantityPrinted1.length > 0) {
                    for (var i = 0; i < ProductLicense.ImpressionQuantityPrinted1.length; i++) {
                        _ImpressionQuantityPrinted1 += ProductLicense.ImpressionQuantityPrinted1[i];
                    }
                }

                $scope.ImpressionBalance = _LicenseAddendumQuantity1 - _ImpressionQuantityPrinted1;
            }

            //--------------------------------------------------------
            angular.element(document.getElementById('angularid')).scope().ProductSerach(ProductLicense.ProductId);

            setTimeout(function () {
                //fetch Kit Details List
                app.expandControllerKitISBNLIst($scope, AJService);
                angular.element(document.getElementById('angularid')).scope().KitISBNDetailsList(ProductLicense.ProductId);
            }, 300);
            //----------------------------------------------

        }, function () {
            //alert('Error in Getting Product License Details');
        });

    }

    $scope.getImpressionDetails = function (ProductId, LicenseId, ContractId) {
        var _ImpressionDetails = {
            ProductId: ProductId,
            LicenseId: LicenseId,
            ContractId: ContractId
        }
        var ImpressionDetails = AJService.PostDataToAPI("Addendum/ImpressionDetails", _ImpressionDetails);
        ImpressionDetails.then(function (ImpressionData) {
            $scope.ImpressionList = ImpressionData.data;
        }, function () {
            //alert('Error in Getting Impression Details');
        });

    }

    $scope.getImpressionDetails($scope.ProductId, $scope.LicenseId, $scope.ContractId);

    $scope.ImpressionEntryFrom = function () {
        if ($scope.ImpressionBalanceHidden == false) {
            $scope.userForm.$valid = true;
        }
        $scope.submitted = true;
        if ($scope.userForm.$valid) {

            if ($scope.userForm.$valid) {
                $scope.ImpressionEntry();
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
        $scope.userForm.$valid = true;
    }


    $scope.ImpressionEntry = function () {
        if ($('input[type=number][name=txtImpressionQty]').val() < 1) {
            alert("Qunatity Printed more than 0 !");
        }

        var ImpressionDetails = {
            ProductId: $scope.ProductId,
            LicenseId: $scope.LicenseId == null || $scope.LicenseId == 0 ? null : $scope.LicenseId,
            AddendumId: $scope.AddendumId == null || $scope.AddendumId == 0 ? null : $scope.AddendumId,
            ImpressionNo: $scope.ImpressionNo,
            ImpressionDate: convertDate($('input[type=text][name=txtImpressionDate]').val()),
            QunatityPrinted: $('input[type=number][name=txtImpressionQty]').val(),
            BalanceQty : $('input[type=number][name=txtImpressionBalance]').val(),
            ThroughAddendum: ($scope.AddendumId > 0 ? "Y" : "N"),
            EnteredBy: $("#enterdBy").val(),
            ContractId: $scope.ContractId == null || $scope.ContractId == 0 ? null : $scope.ContractId,
        }

        var ProductStatus = AJService.PostDataToAPI('Addendum/InsertImpressionEntry', ImpressionDetails);

        ProductStatus.then(function (msg) {

            if (msg.data == "Duplicate") {
                SweetAlert.swal("Error!", "Duplicate. already exist !", "", "error");
            }
            else if (msg.data != "OK") {
                if (msg.data == "No. of impression are finished") {

                    SweetAlert.swal("Try again", msg.data, "error");
                }
                else
                    if (msg.data == "No. of print are exceed") {

                        SweetAlert.swal("Try again", msg.data, "error");
                    }
                    else {
                        SweetAlert.swal("Try agian", 'There is some problem.', 'error');
                    }
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


    function convertDate(date) {
        if (date != undefined) {
            var datearray = date.split("/");
            return datearray[1] + '/' + datearray[0] + '/' + datearray[2];
        }
        else {
            return null;
        }

    }

    $scope.chnageDateFormat = function (datestring) {
        var returndate = "";
        //datestring = datestring.split("T")[0];
        if (datestring != null) {
            if (datestring.indexOf("T") > 1) {
                var datestringformat = datestring.split("T")[0];
                var marr_date = datestringformat.split("-");
                returndate = marr_date[1] + "/" + marr_date[2] + "/" + marr_date[0]
            }
            else {
                returndate = datestring;
            }
        }

        return returndate;

    };

    $scope.SetEffectiveDate = function (datetext) {
        if ($scope.ImpressionDate == undefined && $scope.ImpressionDate !== $(datetext).val()) {
            $scope.ImpressionDate = $(datetext).val();
        }
    }
});


