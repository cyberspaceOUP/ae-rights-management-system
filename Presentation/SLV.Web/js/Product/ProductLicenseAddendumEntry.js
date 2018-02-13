
app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {
    $scope.Title = "Entry";
    /*Expand common.master.js Controller*/
    app.expandControllerA($scope, AJService, $window);
    app.expandControllerProductDetails($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);
    /*Expand Royalty Slab Controller*/
    //app.expandControllerRoyaltySlab($scope, AJService, $window);


    $scope.RoyaltyslabList = [];
    $scope.RoyaltyslabList.push(1);


    SubProductTypeList();

    function SubProductTypeList() {
        var SubProductTypeList = AJService.GetDataFromAPI("CommonList/getSubProductTypeList", null);
        SubProductTypeList.then(function (SubProductTypeList) {
            $scope.SubProductTypeList = SubProductTypeList.data.subProductData;
        }, function () {
            alert('Error in getting SubProduct Type List');
        });
    }
   

    $scope.AddendumId = $("input:hidden[id$=hid_AddendumId]").val();

    if ($scope.AddendumId > 0) {
        var mobj_Addendum = {
            Id: $scope.AddendumId,
        }

        var AddendumDetails = AJService.PostDataToAPI("Addendum/getAddendumDetailsByAddendumId", mobj_Addendum);
        AddendumDetails.then(function (Addendum) {
            var AddendumDetails = Addendum.data.AddendumDetails1;

            $scope.ProductModel = {
                AddendumDate: $scope.chnageDateFormat(AddendumDetails.AddendumDate),
                AddendumType: AddendumDetails.AddendumType,
                Periodofagreement: AddendumDetails.Periodofagreement,
                ExpiryDate: $scope.chnageDateFormat(AddendumDetails.ExpiryDate),
                Remarks: AddendumDetails.Remarks,
                FirstImpressionWithinDate: $scope.chnageDateFormat(AddendumDetails.FirstImpressionWithinDate),
                NoOfImpressions: AddendumDetails.NoOfImpressions == 0 ? "" : AddendumDetails.NoOfImpressions,
                BalanceQuantityCarryForward: AddendumDetails.BalanceQuantityCarryForward,
                AddendumQuantity: AddendumDetails.AddendumQuantity == 0 ? "" : AddendumDetails.AddendumQuantity,
                RoyaltyTerms: AddendumDetails.RoyaltyTerms,
            };
            
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

            if (AddendumDetails.AddendumDetailsRoyalty != null) {
                $scope.setRoyaltySlab(AddendumDetails.AddendumDetailsRoyalty);
            }


        }, function () {
            alert('Error in Getting Addendum Details');
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
            alert('Error in Getting Product License Details');
        });

    }


    if ($scope.ProductLicenseId > 0) {
        LicenseId = $scope.ProductLicenseId;
   
        var getAddendumDetailsByLicenseId = AJService.PostDataToAPI("Addendum/getAddendumDetailsByLicenseId?LicenseId=" + LicenseId, null);
        getAddendumDetailsByLicenseId.then(function (AddendumList) {
            $scope.AddendumList = AddendumList.data.AddendumDetails;
        }, function () {
            alert('Error in Getting Product License Details');
        });

    }

    //$scope.CalculateExpiry = function () {
    //    var PeriodIdValue = $scope.ProductModel.Periodofagreement;
    //    var AddendumDate = $("[name$=AddendumDate]").val();

    //    if (PeriodIdValue == undefined || CurrentDate == "") {
    //        $scope.ProductModel.ExpiryDate = "";
    //        return false;
    //    }


    //    var date = AddendumDate;
    //    var d = new Date(date.split("/").reverse().join("-"));
    //    var dd = d.getDate();
    //    var mm = d.getMonth() + 1;
    //    var yy = d.getFullYear();
    //    var newdate = yy + "/" + mm + "/" + dd;


    //    var CurrentDate = new Date(newdate);
    //    CurrentDate.setMonth(CurrentDate.getMonth() + parseInt(PeriodIdValue));
    //    var today = CurrentDate;
    //    var dd = today.getDate();
    //    var mm = today.getMonth() + 1;

    //    var yyyy = today.getFullYear();
    //    if (dd < 10) {
    //        dd = '0' + dd
    //    }
    //    if (mm < 10) {
    //        mm = '0' + mm
    //    }
    //    //var today = mm + '/' + dd + '/' + yyyy;
    //    var today = dd + '/' + mm + '/' + yyyy;
    //    $scope.ProductModel.ExpiryDate = today;
    //    $("[name$=ExpiryDate]").val(today);
    //}

    function convertDateDDMMYYYY(inputFormat) {
        function pad(s) { return (s < 10) ? '0' + s : s; }
        var d = new Date(inputFormat);
        return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
    }

    $scope.SetAddendumDate = function (datetext) {
        $scope.ProductModel.AddendumDate = $(datetext).val();
        //if ($scope.ProductModel.Periodofagreement > 0) {
        //    $scope.CalculateExpiry();
        //}
    }

    $scope.SetExpiryDate = function (datetext) {
        $scope.ProductModel.ExpiryDate = $(datetext).val();
    }

    $scope.SetFirstImpressionWithinDate = function (datetext) {
        if ($scope.ProductModel.FirstImpressionWithinDate == undefined && $scope.ProductModel.FirstImpressionWithinDate !== $(datetext).val()) {
            $scope.ProductModel.FirstImpressionWithinDate = $(datetext).val();
        }
    }    

    $scope.chnageDateFormat = function (datestring) {
        var returndate = "";

        if (datestring != null) {
            if (datestring.indexOf("T") > 1) {
                var datestringformat = datestring.split("T")[0];
                var marr_date = datestringformat.split("-");
                returndate = marr_date[2] + "/" + marr_date[1] + "/" + marr_date[0]
            }
            else {
                returndate = datestring;
            }
        }

        return returndate;

    };

    $scope.UploadContractReq = false;
    $scope.productAddendumEntryForm = function (ProductModel) {
        $scope.submitted = true;
        //if ($scope.userForm.$valid) {
            var copiesto = 0;
            var printqty = ($("[name$=AddendumQuantity]").val() == "" ? 0 : $("[name$=AddendumQuantity]").val());
            var totallength = $("[id$=TblOwnerList]").find("select[name$=SubProductType]").length - 1;

            if ($('#hid_Uploads').val() == "" || $('#hid_Uploads').val() == undefined) {
                $scope.UploadContractReq = true;
                $scope.UploadExcelfileNameReq = false;
                $scope.userForm.$valid = false;
                return false;
            }
            else {
                if ($('.fileNameClass').val() == "" || $('.fileNameClass').val() == undefined) {
                    $scope.UploadContractReq = false;
                    $scope.UploadExcelfileNameReq = true;
                    $scope.userForm.$valid = false;
                    return false;
                }
                else {
                    $scope.UploadContractReq = false;
                    $scope.UploadExcelfileNameReq = false;
                    $scope.UploadExcelfileReq = false;
                    $scope.userForm.$valid = true;
                }
            }


            if ($scope.ValidateRyaltySlab() == 1) {
                $scope.userForm.$valid = false;
            }
                   
            errorDiv = document.getElementById("fileid");
            errorDiv.innerHTML = "";
            errormsg = "";


            var errorDiv;
            var errormsg = '';
            $scope.msg = "";
            FileNameArray = [];
            FileNameArray = $('#dropZone0').find('.fileNameClass');
            var array = [];

            if (FileNameArray.length == 0) {
                if ($scope.Docurl.length == 0) {
                    errorDiv = document.getElementById("fileid");
                    errorDiv.innerHTML = "Please select a file";
                    errormsg = "Please select a file";
                    $scope.userForm.$valid = false;
                }
            }
            else {
                FileNameArray.each(function () {
                    array.push(
                   $(this).val());

                    for (i = 0; i < array.length; i++) {
                        if (array[i] == "") {
                            errorDiv = document.getElementById("fileid");
                            errorDiv.innerHTML = "Please enter file name";
                            errormsg = "Please enter file name";
                            $scope.userForm.$valid = false;

                        }
                        else {
                            //$scope.userForm.$valid = true;
                        }
                    }
                });
            }

            if ($('form[name$=userForm]').find(".has-error").length > 0) {

                $scope.userForm.$valid = false;
            }

            if ($scope.userForm.$valid) {
                $scope.LicenseAddendumEntry(ProductModel);
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        //}
    }
    

    $scope.LicenseAddendumEntry = function () {


        //if ($scope.AddendumId > 0) {
        var marr_fileDetails = [];
        var FileNameArray = $('#dropZone0').find('.fileNameClass');
        var UploadFileNameArray = $("#hid_Uploads").val().split(",");

        FileNameArray.each(function (i) {
            var mobj_fileDetails = {
                //AddendumId: $scope.AddendumId,
                LicenseId: $scope.ProductLicenseId,
                FileName: $(this).val(),
                UploadFileName: UploadFileNameArray[i],
            }
            marr_fileDetails.push(mobj_fileDetails);
        });
        //}


        //var array = [];
        //var Array1 = [];

        //if ($('#hid_Uploads').val() != "") {
        //    var _FileName = $('#hid_Uploads').val().split(",")
        //    var Index = 0
        //    var k = 0;
        //    $('.dropZone').each(function () {

        //        array = [];
        //        var FileName = "";
        //        var _ttlFile = $(this).find('.fileNameClass').length;
        //        for (var i = 0; i < _ttlFile; i++) {
        //            array[i] = $($(this).find('.fileNameClass')[i]).val();
        //            FileName = FileName + _FileName[Index] + ",";
        //            Index++;
        //        }
        //        if (k == 0) {
        //            $('#hid_UploadsFile1').val(FileName);
        //            Array1 = array;
        //        }

        //        k++;
        //    })
        //}

        var marr_royaltyslab = [];

        $(".AddendumDiv").each(function (index, values) {
            var obj = $(this);
            var i = 0, j = 0;

            $(obj).find('.RoyaltySlab tr:not(:has(th))').each(function () {
                if ($(this).find('select[name$=SubProductType]').val() == "") {
                    return true;
                }
                marr_royaltyslab[i] =
                {
                    ProductSubTypeId: $(this).find('select[name$=SubProductType]').val(),
                    copiesfrom: $(this).find('input[name$=CopiesFrom]').val(),
                    copiesto: $(this).find('input[name$=CopiesTo]').val(),
                    percentage: $(this).find('input[name$=RyPercentage]').val(),
                    //AuthorContractId: AuthorContractId

                }
                i++;
            });
            j++;
        });


        var _mobjAddendum = {
            Id: $scope.AddendumId,
            ProductId: $scope.ProductId,
            LicenseId: $scope.ProductModel.ProductLicenseId,
            AddendumDate: convertDate($scope.ProductModel.AddendumDate),
            AddendumType: $scope.ProductModel.AddendumType,
            //Periodofagreement: $scope.ProductModel.Periodofagreement,
            Expirydate: convertDate($scope.ProductModel.ExpiryDate),
            FirstImpressionWithinDate: convertDate($scope.ProductModel.FirstImpressionWithinDate),
            NoOfImpressions: $scope.ProductModel.NoOfImpressions == undefined ? null : ($scope.ProductModel.NoOfImpressions == '' ? null : $scope.ProductModel.NoOfImpressions),
            BalanceQuantityCarryForward: $scope.ProductModel.BalanceQuantityCarryForward == undefined ? "Y" : ($scope.ProductModel.BalanceQuantityCarryForward == "" ? "Y" : $scope.ProductModel.BalanceQuantityCarryForward),
            AddendumQuantity: $scope.ProductModel.AddendumQuantity == undefined ? null : ($scope.ProductModel.AddendumQuantity == '' ? null : $scope.ProductModel.AddendumQuantity),
            RoyaltyTerms: $scope.ProductModel.RoyaltyTerms,
            Remarks: $scope.ProductModel.Remarks,
            //AddendumDetailsRoyalty: marr_royaltyslab,
            AddendumDetailsRoyalty: marr_royaltyslab,
            IAddendumFileDetails: marr_fileDetails,
            EnteredBy: $("#enterdBy").val(),

        };


        SweetAlert.swal({
            title: "Are you sure?",
            text: "",
            type: "info",
            showCancelButton: true,
            confirmButtonColor: "#8CD4F5",
            confirmButtonText: "Yes",
            closeOnConfirm: false,
            closeOnCancel: true
        },
function (Confirm) {
    if (Confirm) {

        if ($scope.AddendumId > 0) {
            var ProductStatus = AJService.PostDataToAPI('Addendum/UpdateAddendumDetails', _mobjAddendum);

        }
        else {
            var ProductStatus = AJService.PostDataToAPI('Addendum/InsertAddendumDetails', _mobjAddendum);
        }


        ProductStatus.then(function (msg) {

            if (msg.data.status == "Duplicate") {
                SweetAlert.swal("Error!", "Duplicate. already exist !", "", "error");
            }
            else if (msg.data.status != "OK") {
                SweetAlert.swal("Try agian", 'There is some problem.', 'error');
            }
            else {
                if ($scope.AddendumId > 0) {
                    //SweetAlert.swal('Updated successfully.', '', "success");
                    //$("#hid_ProductId").val($scope.productId);
                    SweetAlert.swal({
                        title: "Updated successfully.",
                        text: "",
                        type: "success"
                    },
                   function () {
                       //$('form[name*=user]').attr("method", "post");
                       //$('form[name*=user]').submit();

                       if ($('#Hid_forPermission').val() == '') { }
                       //window.location.href = "ProductLicenseSearch?For=BackToserach";
                       window.location.href = "../../Product/ProductLicense/ViewAddendum?Id=" + $('#hid_licenseId').val() + "&AddendumId=" + $('#hid_AddendumId').val();

                   });
                }
                else {
                    //SweetAlert.swal('Insert successfully.', '', "success");
                    //$("#hid_ProductId").val(msg.data.Id);
                    SweetAlert.swal({
                        title: "Insert successfully.",
                        text: "",
                        type: "success"
                    },
                   function () {
                       //$('form[name*=user]').attr("method", "post");
                       // $('form[name*=user]').submit();

                       //window.location.href = "ProductLicenseSearch?For=BackToserach";
                       window.location.href = "../../Product/ProductLicense/ViewAddendum?Id=" + $('#hid_licenseId').val() + "&AddendumId=" + msg.data.AddendumId;

                   });
                }
            }

        },
        function () {
            SweetAlert.swal("Try agian", 'There is some problem.', 'error');
            //alert('There is some error in the system');
        });
    }
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



    $scope.ValidateRoyaltySlabInsert = function (obj) {
        //obj = $(event.target);
        var _table = $(obj).closest(".RoyaltySlab");
        if ($(_table).find("select[name*=SubProductType]").find('option[value="' + $(obj).val() + '"]:selected').closest("select").length == 1) {
            $(obj).closest("tr").find("input[name*=CopiesFrom]").val(1);
            $(obj).closest("tr").find("input[name*=CopiesFrom]").attr("disabled", true);
        }
        else {

            var _copiesto = $($(_table).find("select[name*=SubProductType]").find('option[value="' + $(obj).val() + '"]:selected').parents("tr")[1]).find('input[name$=CopiesTo]').val()
            $(obj).closest("tr").find('input[name*=CopiesFrom]').val(parseInt(_copiesto) + 1);
            $(obj).closest("tr").find('input[name*=CopiesFrom]').attr("disabled", true);
        }

        if (obj.val() == "") {
            $(obj).closest("tr").find("input[name*=CopiesFrom]").val("");
            $(obj).closest("tr").find("input[name*=CopiesFrom]").removeAttr("disabled");
        }
    };


    $scope.ValidateRyaltySlab = function () {

        if ($('.AddendumDetails').find('input[name=RoyaltyTerms]:checked').val() == "Royalty") {

            if (unique($("select[name*=SubProductType]").map(function () { return $(this).val() }).get())[0] == "" && $('.AddendumDetails').find('input[name=RoyaltyTerms]:checked').val() == "Royalty") {
                SweetAlert.swal("validation", "Please enter atleaset one royalty slab", "error");
                return 1;
                return false;
            }

            var returnstatus;
            var result = [];
            result = unique($("[id$=TblOwnerList]").find("select[name$=SubProductType]").map(function () { return $(this).find("option:selected").text(); }).get())
            for (var i = 0; i < result.length; i++) {
                $(".RoyaltySlab").find("select[name*=SubProductType]").find('option:selected').filter(":contains('" + result[i] + "')").parents("tr").each(function (index, value) {
                    var _lastTr = $(".RoyaltySlab").find("select[name*=SubProductType]").find('option:selected').filter(":contains('" + result[i] + "')").parents("tr:last")
                    if ($(_lastTr).find('input[name*=CopiesTo]').val() != "") {
                        if ($(_lastTr).find('input[name*=CopiesTo]').val() != 9999999) {
                            $scope.userForm.$valid = false;
                            SweetAlert.swal("Validation!", "Last Copies to should be blank !", "", "error");
                            $(_lastTr).find('input[name*=CopiesTo]').focus();
                            $scope.submitted = false;
                            returnstatus = true;
                            return false;
                        }

                    }
                    if ($(this).find('input[name=RyPercentage]').val() == "" && $(this).find("select[name*=SubProductType]").val() != "") {
                        $scope.userForm.$valid = false;
                        SweetAlert.swal("Validation!", "Please Enter Copies percentage !", "", "error");
                        $(this).find('input[name=RyPercentage]').focus();
                        $scope.submitted = false;
                        returnstatus = true;
                        return false;
                    }
                });
            }
            if (returnstatus) {
                return 1;
            }
        }
        else {
            return false;
        }
    }

    function unique(list) {
        var result = [];
        $.each(list, function (i, e) {
            if ($.inArray(e, result) == -1) result.push(e);
        });
        return result;
    }


    $scope.addroyalslabbyJquery = function (obj) {

        var _trClone = $(obj).closest("tr").clone(true);
        _trClone.find('input').val("");
        _trClone.find('select').val("");
        _trClone.find('input').removeAttr("disabled");
        $(obj).closest(".RoyaltySlab").append(_trClone);
        $scope.RoyaltySlabManagement();

    }
    $scope.removeroyalslab = function (obj) {

        var _table = $(obj).closest(".RoyaltySlab");
        $(obj).closest("tr").remove();

        //if (_table.find("input").is(":disabled") == false) {
        //    $(_table.find("tr")[1]).find("input[name*=CopiesFrom]").prop("disabled", true);
        //    $(_table.find("tr")[1]).find("input[name*=CopiesFrom]").val(1);
        //}

        $scope.RoyaltySlabManagement();
    }
    $scope.RoyaltySlabManagement = function () {
        $('.AddendumDiv').each(function () {
            $(this).find(".RoyaltySlab").find("tr").find(".RoyaltySlabnotAdd").css("display", "none");
            $(this).find(".RoyaltySlab").find("tr").find(".RoyaltySlabnotRemove").css("display", "table-row");
            $(this).find(".RoyaltySlab").find("tr:last").find(".RoyaltySlabnotAdd").css("display", "table-row");
            $(this).find(".RoyaltySlab").find("tr:last").find(".RoyaltySlabnotRemove").css("display", "none");
            $(this).find(".RoyaltySlab").find("tr:last").find('input[name$=CopiesTo]').val("");
            $(this).find(".RoyaltySlab").find("tr").not("tr:first").each(function (Index, value) {
                $($(this).find("td")[0]).html(Index + 1);
            })

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


    $scope.ddlAddendumTypeChange = function () {
        var _selectedData = $('#AddendumType').val();
        //alert(_selectedData)
        if (_selectedData.toLowerCase() == 'q') {
            var AddendumDetails = AJService.PostDataToAPI("Addendum/getLicenseExpiryDateById?LicenseId=" + $("input:hidden[id$=hid_licenseId]").val(), null);
            AddendumDetails.then(function (Addendum) {
                if (Addendum.data != null) {

                    $scope.ProductModel.ExpiryDate = convertDateDDMMYYYY(new Date(Addendum.data));
                    $('input[name=ExpiryDate]').prop('disabled', true);
                }
                else {
                    $scope.ProductModel.ExpiryDate = '';
                    $('input[name=ExpiryDate]').prop('disabled', false);
                }
            }, function () {
                //alert('Error in Getting Addendum Details');
            });
        }
        else {
            $scope.ProductModel.ExpiryDate = '';
            $('input[name=ExpiryDate]').prop('disabled', false);
        }
    }

    function convertDateDDMMYYYY(inputFormat) {
        function pad(s) { return (s < 10) ? '0' + s : s; }
        var d = new Date(inputFormat);
        return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
    }


});


