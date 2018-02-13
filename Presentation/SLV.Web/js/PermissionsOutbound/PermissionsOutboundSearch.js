
app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {
    app.expandControllerTopSearch($scope, AJService, $window);
    /*Expand common.master.js Controller*/
    app.expandControllerA($scope, AJService, $window);

    app.expandControllerInvoiceView($scope, AJService, $window);
   

    $("#InvoiceTodate").attr("disabled", true);
    $("#RequestToDate").attr("disabled", true);
    $("#PermissionToDate").attr("disabled", true);
    TypeFor = $('#hid_InvoiceView').val();

    $scope.PermissionsOutboundListForm = false;
    $scope.PermissionsOutboundSearch = true;


    $scope.SetContractDate = function (datetext) {
      
        $scope.PermissionFromDate = $(datetext).val();
       
    }


    $scope.setContractEffectiveDate = function (datetext) {

        $scope.ContractEffectiveDate = $(datetext).val();
    }


    $scope.setRecurringFromPeriod = function (datetext) {
        $scope.RecurringFromPeriod = $(datetext).val();
    }

    $scope.setRecurringToPeriod = function (datetext) {
        $scope.RecurringToPeriod = $(datetext).val();
    }



    $scope.GetTypeOfRightsList = function () {
        var getTypeOfRightsList = AJService.GetDataFromAPI("TypeOfRightsMaster/GetTypeOfRightsList?Id=" + $("#enterdBy").val());
        getTypeOfRightsList.then(function (TypeOfRights) {
            $scope.TypeOfRightsList = TypeOfRights.data;
        }, function () {
            //alert('Error in getting Type Of Rights list');
        });
    }


    $scope.funct_RoyaltyRecurringYes = function () {

        $scope.RoyaltyRecurringReq = true;
    }
    $scope.funct_RoyaltyRecurringNo = function () {
        $scope.RoyaltyRecurringReq = false;
    }


    $scope.func_InvoiceApplicableYes = function () {

        var mstr_Rights = '';
        var mstr_Ouantity = '';
        var mstr_TypeOfrights = '';
        var mstr_PrintOuantity = '';
        if ($('#TypeofRights').val() != null) {
            if ($('#TypeofRights').val().length > 0) {
                for (var i = 0; i < $('#TypeofRights').val().length; i++) {
                    mstr_Rights = mstr_Rights + "," + $(($('#TypeofRights option:selected')[i])).text();//$('#TypeofRights option:selected').text().split(/(?=[A-Z])/)[i];
                    mstr_Ouantity = mstr_Ouantity + ", " + $(($('#TypeofRights option:selected')[i])).text() + ' - ' + +$($('[name*=PrintRunQuantity]')[i]).val();// + '--' + $($('[name*=PrintRunQuantity]')[i]).val();
                }
                mstr_TypeOfrights = mstr_Rights.slice(1)
                mstr_PrintOuantity = mstr_Ouantity.slice(1)
            }
        }





        $scope.InvoiceDescription = "Request Material : " + ($scope.RequestMaterial == undefined ? '---' : $scope.RequestMaterial) + "\n" + "Type of Rights : " + (mstr_TypeOfrights == '' ? '---' : mstr_TypeOfrights) + "\n" + "Language : " + ($('#LanguageId option:selected').text() == 'Please Select' ? '---' : $('#LanguageId option:selected').text()) + "\n" + "Print Run Quantity : " + (mstr_PrintOuantity == '' ? '---' : mstr_PrintOuantity) + "\n" + "Territory : " + ($('#ddlTerritory option:selected').text() == 'Please Select' ? '---' : $('#ddlTerritory option:selected').text())

        $scope.ReqInvoiceNo = true;
        $scope.ReqInvoiceValue = true;
        $scope.ReqInvoiceDescription = true;
        $scope.ReqRemarks = false;
    }

    $scope.func_InvoiceApplicableNo = function () {

        $scope.ReqInvoiceNo = false;
        $scope.ReqInvoiceValue = false;
        $scope.ReqInvoiceDescription = false;

        $scope.ReqRemarks = true;
    }


    $scope.func_CopiestobereceivedYes = function () {

        $scope.reqNumberofcopies = true;

    }


    $scope.func_CopiestobereceivedNo = function () {

        $scope.reqNumberofcopies = false;

    }

    $scope.PermissionsOutboundSearch = function () {
        var OutboundList = AJService.GetDataFromAPI("PermissionsOutbound/GetPermissionsOutboundList?SessionId=" + $("#hid_sessionId").val() + "", null);
        OutboundList.then(function (msg) {
           
            if (msg.data.length != 0) {
                $scope.OutboundList = [];
                $scope.OutboundList = msg.data;
                $scope.PermissionsOutboundListForm = true;
                $scope.PermissionsOutboundSearch = false;

            }
            else {
                swal("No record", 'No record found', "warning");
            }
        });
    }
   

    $scope.DisplayDiv = function (count) {
        $('.tooltip-toggle').css("display", "none");
        $('.tooltip-toggle').removeAttr("id");
        $($('.tooltip-toggle')[count - 1]).attr("id", "tooltip-toggle");
    }
    $scope.clear = function () {
        $scope.Licensee = "";
        $scope.Licenseecode = "";
        // $('#OrganizationName option:selected').text('')
        $scope.ContactPerson = "";
        $scope.PublisherAddress = "";
        $scope.Country = "";
        $scope.State = "";
        $scope.City = "";
        $scope.pincode = "";
        $scope.PublisherMobile = "";
        $scope.PublisherEmail = "";
        $scope.URL = "";

        $scope.LicenseePublicationTitle = "";
        $scope.Permission = "";
        $scope.PermissionPeriod = "";
        $scope.ExpiryDate = "";
        $scope.RequestMaterial = "";
        $scope.Willbematerialbetranslated = "";
        $scope.WillbematerialbeAdepted = "";
        $scope.Language = "";
        $scope.Extent = "";
        $scope.TerritoryRight = "";
        $scope.DateofInvoice = "";
        $scope.InvoiceApplicable = "";
        $scope.InvoiceNo = "";
        $scope.InvoiceValue = "";
        $scope.InvoiceDescription = "";
        $scope.Copiestobereceived = "";
        $scope.Numberofcopies = "";
        $scope.PaymentReceived = "";
        $scope.Remarks = "";
        $scope.TypeofRights = "";
        $scope.SupplyRunQuantityById = "";
        $scope.LanguageReq = false;
        $scope.reqNumberofcopies = false;
        $scope.ReqRemarks = false;
        $scope.ReqInvoiceNo = false;
        $scope.ReqInvoiceValue = false;
        $scope.ReqInvoiceDescription = false;
    }



    $scope.PermissionsOutboundEntry = function () {

        var TypeOfrights = "";
        var TypeOfrightsValue = null;
        var SupplyTypeOfRightsInsert = [];
        if ($scope.SupplyRunQuantityById != undefined) {
            for (var i = 0; i < $scope.SupplyRunQuantityById.length; i++) {
                TypeOfrights = TypeOfrights + $scope.SupplyRunQuantityById[i].Id + ','
            }
            
        }

        if (TypeOfrights != null)
        {
            TypeOfrightsValue =    TypeOfrights.slice(0, -1)
        }


        var mstr_RequestFromDate = $('#RequestFromDate').val();
        var mstr_RequestToDate = $('#RequestToDate').val();
        
        var mstr_PermissionFromDate = $('#PermissionFromDate').val();
        var mstr_PermissionToDate = $('#PermissionToDate').val();
        
        
        var mstr_InvoiceFromdate = $('#InvoiceFromdate').val();
        var mstr_InvoiceTodate = $('#InvoiceTodate').val();
        

        var mstr_ExpiryDate = $('#ExpiryDate').val();
      


        if (mstr_RequestFromDate == "") {
            mstr_RequestFromDate = null
        }
        else {

            var RequestDate = mstr_RequestFromDate;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            mstr_RequestFromDate = yy + "/" + mm + "/" + dd;
        }



        if (mstr_PermissionToDate == "") {
            mstr_PermissionToDate = null
        }
        else {

            var RequestDate = mstr_PermissionToDate;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            mstr_PermissionToDate = yy + "/" + mm + "/" + dd;
        }



        if (mstr_RequestToDate == "") {
            mstr_RequestToDate = null
        }
        else {

            var RequestDate = mstr_RequestToDate;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            mstr_RequestToDate = yy + "/" + mm + "/" + dd;
        }


        if (mstr_PermissionFromDate == "") {
            mstr_PermissionFromDate = null
        }
        else {

            var RequestDate = mstr_PermissionFromDate;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            mstr_PermissionFromDate = yy + "/" + mm + "/" + dd;
        }




        if (mstr_InvoiceFromdate == "") {
            mstr_InvoiceFromdate = null
        }
        else {

            var RequestDate = mstr_InvoiceFromdate;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            mstr_InvoiceFromdate = yy + "/" + mm + "/" + dd;
        }


        if (mstr_InvoiceTodate == "") {
            mstr_InvoiceTodate = null
        }
        else {

            var RequestDate = mstr_InvoiceTodate;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            mstr_InvoiceTodate = yy + "/" + mm + "/" + dd;
        }




        if (mstr_ExpiryDate == "") {
            mstr_ExpiryDate = null
        }
        else {

            var RequestDate = mstr_ExpiryDate;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            mstr_ExpiryDate = yy + "/" + mm + "/" + dd;
        }

        var ContractStatus = "";
        $('input[type=checkbox][name=ContractStatus]:visible:checked').each(function (index, value) {
            ContractStatus = ContractStatus + $(this).val() + ",";
        });
        ContractStatus = ContractStatus.substring(0, ContractStatus.length - 1);

        var _mobPermissionsOutbound = {

           ProductCode: $scope.ProductCode,
           ContractCode: $scope.ContractCode,

            PermissionsOutboundCode: $scope.PermissionsOutboundCode,

            LicenseeName: $scope.LicenseeName,

            RequestFromDate: mstr_RequestFromDate,
            RequestToDate: mstr_RequestToDate,

            LicenseePublicationTitle: $scope.LicenseePublicationTitle,

            PermissionFromDate: mstr_PermissionFromDate,// $scope.Permission ,
            PermissionToDate: mstr_PermissionToDate,


           
            DateExpiry: mstr_ExpiryDate,
            RequestMaterial: $scope.RequestMaterial,
          
            LanguageId: $scope.Language,
            Extent: $scope.Extent,
            TerritoryId: $scope.TerritoryRight,


            InvoiceFromdate: mstr_InvoiceFromdate,//$scope.DateofInvoice,
            InvoiceTodate : mstr_InvoiceTodate,

            InvoiceApplicable: $scope.InvoiceApplicable,
            InvoiceNo: $scope.InvoiceNo,
            InvoiceCurrency : $scope.InvoiceCurrency,
            InvoiceValue: $scope.InvoiceValue,
            InvoiceDescription: $scope.InvoiceDescription,
            Copies_To_Be_Received: $scope.Copiestobereceived,
            NumberOfCopies: $scope.Numberofcopies,
            /// PaymentReceived: $scope.PaymentReceived,
            Remarks: $scope.Remarks,
            TypeOfrightsId: TypeOfrightsValue,
            EnteredBy: $("#enterdBy").val(),
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            ContractStatus: ContractStatus,
            TypeFor: TypeFor
          
          
        };
    
        var PermissionsOutboundStatus = AJService.PostDataToAPI('PermissionsOutbound/PermissionsOutboundSearch', _mobPermissionsOutbound);

        PermissionsOutboundStatus.then(function (msg) {

            if (msg.data == "OK") {
                $scope.PermissionsOutboundSearch();
            }

        },
        function () {
            alert('There is some error in the system');
        });
    }



    $scope.CalculateExpiry = function () {

        var PeriodIdValue = $scope.PermissionPeriod;
        var RequestDate = $("[name$=RequestDate]").val();

        var date = RequestDate;
        var d = new Date(date.split("/").reverse().join("-"));
        var dd = d.getDate();
        var mm = d.getMonth() + 1;
        var yy = d.getFullYear();
        var newdate = yy + "/" + mm + "/" + dd;




        if (PeriodIdValue == undefined || CurrentDate == "") {
            $scope.ProductModel.ExpiryDate = "";
            return false;
        }

        var CurrentDate = new Date(newdate);

        CurrentDate.setMonth(CurrentDate.getMonth() + parseInt(PeriodIdValue));
        var today = CurrentDate;
        var dd = today.getDate();
        var mm = today.getMonth() + 1;

        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd
        }
        if (mm < 10) {
            mm = '0' + mm
        }
        var today = dd + '/' + mm + '/' + yyyy;
        $scope.ExpiryDate = today;
    }



    $scope.GetValue = function (obj) {

        $scope.SupplyRunQuantityById = obj;
    }


    $scope.PermissionsOutboundSearchForm = function () {

        $scope.submitted = true;
        if ($scope.userForm.$valid) {

            if ($scope.userForm.$valid) {
                $scope.PermissionsOutboundEntry();
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    }


  


    $scope.func_Willbematerialbetranslated_yes = function () {
        $scope.LanguageReq = true;
    }
    $scope.func_Willbematerialbetranslated_No = function () {
        $scope.LanguageReq = false;
    }



   

    $scope.DateOfAgreement = function (datetext) {

        $scope.DateOfAgreement = $(datetext).val();
    }


    $scope.BackToserch = function () {

        var mstr_history = document.referrer;

        if (mstr_history.indexOf("OutboundView") > 0) {
            window.location.href = "PermissionsOutboundSearchMaster?For=View";
        }
        else if (mstr_history.indexOf("OutboundId") > 0) {
            window.location.href = "PermissionsOutboundSearchMaster?For=Update";

        }
        else {
            window.location.href = "PermissionsOutboundSearchMaster?For=View";
        }
       
    }

    if ($('#hid_BackToSearch').val() != "") {
        $scope.PermissionsOutboundSearch();
    }



    $scope.ExcelReport = function () {

        document.location = GlobalredirectPath + "PermissionsOutbound/PermissionsOutbound/exportToExcelProductList?SessionId=" + $("#hid_sessionId").val() + "";

    }

    $scope.PermissionsOutboundReportExcel = function () {

        $scope.ExcelReport();
    }


    //For Delete Permissions Outbound // Added by Prakash on 20 Sep, 2017
    $scope.DeletePermissionsOutbound = function (poId, role) {
        var mobj_delete = {
            Id: poId == undefined ? 0 : poId,
            //Role: role == undefined ? null : role,
            DeactivateBy: parseInt($("#enterdBy").val()),
        };

        SweetAlert.swal({
            title: "Are you sure?",
            text: "You will not be able to recover this detail ! ",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false,
            closeOnCancel: true
        },
        function (Confirm) {
            if (Confirm) {
                //blockUI.start();

                var Delete = AJService.PostDataToAPI("PermissionsOutbound/DeletePermissionsOutboundSet", mobj_delete);
                Delete.then(function (msg) {
                    if (msg.data == "OK") {                      
                        SweetAlert.swal({
                            title: "Deleted!",
                            text: "Your record  has been deleted.",
                            type: "success",
                            confirmButtonText: "OK",
                            closeOnConfirm: true
                        },
                        function (Confirm) {
                            if (Confirm) {
                                //blockUI.stop();
                                //$scope.PermissionsOutboundSearch();
                                window.location.href = "PermissionsOutboundSearchMaster?For=Delete";
                            }
                        });

                    }
                });


            }

        });

    }



    //$scope.Exporttopdf = function (Id) {

    //    var EditPermissionsOutBoundDetail = {
    //        PermissionsoutboundId: Id,

    //    };

    //    var EditPermissionsOutBoundDetailStatus = AJService.PostDataToAPI('PermissionsOutbound/PermissionsOutBoundDetails', EditPermissionsOutBoundDetail);
    //    EditPermissionsOutBoundDetailStatus.then(function (msg) {
    //        if (msg != null) {

    //            if (msg.data._GetPermissionsOutBoundUpdate != null) {

    //                $window.sessionStorage.LicenseeView = (msg.data._GetPermissionsOutBound[0].organizationname == null ? '---' : msg.data._GetPermissionsOutBound[0].organizationname);
    //                $window.sessionStorage.ContactPersonView = (msg.data._GetPermissionsOutBound[0].contactperson == null ? '---' : msg.data._GetPermissionsOutBound[0].contactperson);
    //                $window.sessionStorage.AddressView = (msg.data._GetPermissionsOutBound[0].address == null ? '---' : msg.data._GetPermissionsOutBound[0].address)
    //                $window.sessionStorage.CountryView = (msg.data._GetPermissionsOutBound[0].Country == null ? '---' : msg.data._GetPermissionsOutBound[0].Country);
    //                $window.sessionStorage.StateView = (msg.data._GetPermissionsOutBound[0].State == null ? '---' : msg.data._GetPermissionsOutBound[0].State);
    //                $window.sessionStorage.CityView = (msg.data._GetPermissionsOutBound[0].City == null ? '---' : msg.data._GetPermissionsOutBound[0].City);
    //                $window.sessionStorage.PincodeView = (msg.data._GetPermissionsOutBound[0].pincode == null ? '---' : msg.data._GetPermissionsOutBound[0].pincode);

    //                $window.sessionStorage.DateInvoiceView = (msg.data._GetPermissionsOutBound[0].DateOfInvoiceView == null ? '---' : msg.data._GetPermissionsOutBound[0].DateOfInvoiceView);
    //                $window.sessionStorage.InvoiceNoView = (msg.data._GetPermissionsOutBound[0].invoiceno == null ? '---' : msg.data._GetPermissionsOutBound[0].invoiceno);
    //                $window.sessionStorage.InvoiceValueView = (msg.data._GetPermissionsOutBound[0].invoicevalue == null ? '---' : msg.data._GetPermissionsOutBound[0].invoicevalue);
    //                $window.sessionStorage.InvoiceDescriptionView = (msg.data._GetPermissionsOutBound[0].invoicedescription == null ? '---' : msg.data._GetPermissionsOutBound[0].invoicedescription);


    //                if (msg.data._GetPermissionsOutBoundUpdate[0] != undefined) {

    //                    $window.sessionStorage.CurrencyView = (msg.data._GetPermissionsOutBoundUpdate[0].currencyname == null ? '---' : msg.data._GetPermissionsOutBoundUpdate[0].currencyname);
    //                }
                    
    //                setTimeout(function () {
    //                    window.location.href = "PDF?OutboundView=" + Id;
    //                }, 100);
    //                //window.location.href = "PermissionsOutboundSearchMaster?For=Update";

    //            }

    //        }
    //    });

    //}

});


