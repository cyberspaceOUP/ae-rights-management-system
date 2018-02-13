app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI, $parse, $timeout) {
    app.expandControllerA($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);

    $("#search").css("display", "none");
    $("#RequestToDate").attr("disabled", true);

    $("#ContractToDate").attr("disabled", true);

    
    $scope.OtherContractListResult = function () {
        var OtherContractList = AJService.GetDataFromAPI("OtherContract/GetOtherContractSearchList?SessionId=" + $("#hid_sessionId").val() + "", null);
        OtherContractList.then(function (msg) {
            blockUI.stop();
            if (msg.data.length != 0) {
                $scope.OtherContractList = [];
                $scope.OtherContractList = msg.data;
                $("#OtherContractSerch").css("display", "none");
                $("#search").css("display", "block");

                $("#OtherContractSearchHeading").css("display", "none");
                $("#OtherContractViewHeading").css("display", "block");
                
            }
            else {
               // swal("No record", 'No record found', "warning");               

                SweetAlert.swal({
                    title: "No record",
                    text: "No record found",
                    type: "warning"
                },
                function () {
                    document.location = GlobalredirectPath + "/Contract/OtherContract/OtherContractSearch?For=View";
                });
            }
        });
    }



    if ($('#hid_OtherContractBackToSearch').val() != "") {
        
        $scope.OtherContractListResult();
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

    $scope.AddOtherContract = function () {
    
      
        var mstr_RequestFromDate =  $('#RequestFromDate').val();
        var mstr_RequestToDate = $('#RequestToDate').val();

        var mstr_ContractFromDate = $('#ContractFromDate').val();
        var mstr_ContractToDate = $('#ContractToDate').val();

        var mstr_ExpiryDate = $('#ExpiryDate').val();
       

        if (mstr_ExpiryDate == "") {
            mstr_ExpiryDate = null
        }

        if (mstr_RequestFromDate == "") {
            mstr_RequestFromDate = null
        }

        if (mstr_RequestToDate == "") {
            mstr_RequestToDate = null
        }

       
        if (mstr_ContractFromDate == "") {
            mstr_ContractFromDate = null
        }

        if (mstr_ContractToDate == "") {
            mstr_ContractToDate = null
        }

       
        var ContractStatus = "";
        $('input[type=checkbox][name=ContractStatus]:visible:checked').each(function (index, value) {
            ContractStatus = ContractStatus + $(this).val() + ",";
        });
        ContractStatus = ContractStatus.substring(0, ContractStatus.length - 1);
       
        var OtherContract = {
   
            othercontractcode : $scope.ContractCode,
            partyname : $scope.PartyName,
            natureofserviceid: $scope.Service,
            natureofsubserviceid : $scope.sub_service,
            Address: $scope.Address,
            CountryId: $scope.Country,
            OtherCountry: $scope.CountryName,
            Stateid: $scope.State,
            OtherState: $scope.stateName,
            Cityid: $scope.userForm.city.$modelValue,
            OtherCity: $scope.cityName,
            Pincode: $scope.pincode,
            Mobile: $scope.Mobile,
            Email: $scope.Email,
            PANNo: $scope.PanNo,
            RequestFromDate: mstr_RequestFromDate == null ? null : convertDateForInsert(mstr_RequestFromDate),
            RequestToDate: mstr_RequestToDate == null ? null : convertDateForInsert(mstr_RequestToDate),

            ContractFromDate: mstr_ContractFromDate == null ? null : convertDateForInsert(mstr_ContractFromDate),
            ContractToDate: mstr_ContractToDate == null ? null : convertDateForInsert(mstr_ContractToDate),



            ProjectTitle: $scope.ProjectTitle,
            ProjectISBN: $scope.ProjectIsbn,
            Contracttypeid: $scope.ContractType,

          
            Periodofagreement: $scope.AgreementPeriod,
            Expirydate: mstr_ExpiryDate == null ? null : convertDateForInsert(mstr_ExpiryDate),
            Territoryrightsid: $scope.TerritoryRight,
            Payment: $scope.Payment,
            paymentperiodid: $scope.PaymentPeriod,
            NatureOfWork: $scope.NatureOfWork,
            divisionid: $scope.Division,
            ContractSignedByExecutiveid: $scope.Executive,
            Remarks: $scope.Remarks,
          
            Printrunquantity: $scope.printQuantity,
            PrintRights: $scope.PrintRights,
            electronicrights: $scope.ElectronicRights,
            ebookrights: $scope.EBookRights,
            cost: $scope.Cost,
            currencyid: $scope.Currency,
            restriction: $scope.Restricitions,
          
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            ContractStatus: ContractStatus

       
        };

        if (typeof ($('#hid_Report').val() !== "undefined") && $('#hid_Report').val())
        {

            var mstrSearchparameter = "";

            if ($scope.PartyName != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&PartyName=" + $scope.PartyName;
            }
            if ($scope.natureofserviceid != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&natureofserviceid=" + $scope.natureofserviceid;  //$("#ddlService option:selected").text();
            }

            if ($scope.natureofsubserviceid != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&natureofsubserviceid=" + $scope.natureofsubserviceid;
            }
            
            if ($scope.Address != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&Address=" + $scope.Address;
            }
            if ($scope.Country != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&CountryId=" + $scope.Country;
            }


            if ($scope.CountryName != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&OtherCountry=" + $scope.CountryName;
            }
            if ($scope.State != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&Stateid=" + $scope.State;
            }
            if ($scope.stateName != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&OtherState=" + $scope.stateName;
            }
            if ($scope.userForm.city.$modelValue != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&Cityid=" + $scope.userForm.city.$modelValue;
            }
            if ($scope.cityName != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&OtherCity=" + $scope.cityName;
            }
            if ($scope.pincode != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&Pincode=" + $scope.pincode;
            }
            if ($scope.Mobile != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&Mobile=" + $scope.Mobile;
            }


            if ($scope.Email != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&Email=" + $scope.Email;
            }
            if ($scope.PanNo != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&PANNo=" + $scope.PanNo;
            }
            if (mstr_RequestFromDate != "null") {
                mstrSearchparameter = mstrSearchparameter + "&RequestFromDate=" + mstr_RequestFromDate;
            }
            if (mstr_RequestToDate != "null") {
                mstrSearchparameter = mstrSearchparameter + "&RequestToDate=" + mstr_RequestToDate;
            }
            if (mstr_ContractFromDate != "null") {
                mstrSearchparameter = mstrSearchparameter + "&ContractFromDate=" + mstr_ContractFromDate;
            }




            if (mstr_ContractToDate != "null") {
                mstrSearchparameter = mstrSearchparameter + "&ContractToDate=" + mstr_ContractToDate;
            }
            if ($scope.ProjectTitle != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&ProjectTitle=" + $scope.ProjectTitle;
            }
            if ($scope.ProjectIsbn != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&ProjectISBN=" + $scope.ProjectIsbn;
            }
            if ($scope.ContractType != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&Contracttypeid=" + $scope.ContractType;
            }




            if ($scope.AgreementPeriod != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&Periodofagreement=" + $scope.AgreementPeriod;
            }
            if (mstr_ExpiryDate != "null") {
                mstrSearchparameter = mstrSearchparameter + "&Expirydate=" + mstr_ExpiryDate;
            }



            if ($scope.TerritoryRight != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&Territoryrightsid=" + $scope.TerritoryRight;
            }
            if ($scope.Payment != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&Payment=" + $scope.Payment;
            }
            if ($scope.PaymentPeriod != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&paymentperiodid=" + $scope.PaymentPeriod;
            }
            if ($scope.NatureOfWork != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&NatureOfWork=" + $scope.NatureOfWork;
            }


            if ($scope.Division != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&divisionid=" + $scope.Division;
            }


            if ($scope.Executive != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&ContractSignedByExecutiveid=" + $scope.Executive;
            }
            if ($scope.Remarks != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&Remarks=" + $scope.Remarks;
            }

            if ($scope.printQuantity != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&Printrunquantity=" + $scope.printQuantity;
            }



            if ($scope.PrintRights != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&PrintRights=" + $scope.PrintRights;
            }
            if ($scope.ElectronicRights != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&electronicrights=" + $scope.ElectronicRights;
            }
            if ($scope.EBookRights != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&ebookrights=" + $scope.EBookRights;
            }
            if ($scope.Cost != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&cost=" + $scope.Cost;
            }


            if ($scope.Currency != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&currencyid=" + $scope.Currency;
            }


            if ($scope.Restricitions != undefined) {
                mstrSearchparameter = mstrSearchparameter + "&restriction=" + $scope.Restricitions;
            }
           


            $('#hid_Report').val("");
            document.location = GlobalredirectPath + "/OtherContract/exportToExcelOtherContractList?PartyName=" + $scope.PartyName + "&natureofserviceid=" + $scope.Service + "&natureofserviceid=" + $scope.Service + "&natureofsubserviceid=" + $scope.sub_service + "&Address=" + $scope.Address + "&CountryId=" + $scope.Country + "&OtherCountry=" + $scope.CountryName + "&Stateid=" + $scope.State + "&OtherState=" + $scope.stateName + "&Cityid=" + $scope.userForm.city.$modelValue + "&OtherCity=" + $scope.cityName + "&Pincode=" + $scope.pincode + "&Mobile=" + $scope.Mobile + "&Email=" + $scope.Email + "&PANNo=" + $scope.PanNo + "&RequestFromDate=" + mstr_RequestFromDate + "&RequestToDate=" + mstr_RequestToDate + "&ContractFromDate=" + mstr_ContractFromDate + "&ContractToDate=" + mstr_ContractToDate + "&ProjectTitle=" + $scope.ProjectTitle + "&ProjectISBN=" + $scope.ProjectIsbn + "&Contracttypeid=" + $scope.ContractType + "&Periodofagreement=" + $scope.AgreementPeriod + "&Expirydate=" + mstr_ExpiryDate + "&Territoryrightsid=" + $scope.TerritoryRight + "&Payment=" + $scope.Payment + "&paymentperiodid=" + $scope.PaymentPeriod + "&NatureOfWork=" + $scope.NatureOfWork + "&divisionid=" + $scope.Division + "&ContractSignedByExecutiveid=" + $scope.Executive + "&Remarks=" + $scope.Remarks + "&Printrunquantity=" + $scope.printQuantity + "&PrintRights=" + $scope.PrintRights + "&electronicrights=" + $scope.ElectronicRights + "&ebookrights=" + $scope.EBookRights + "&cost=" + $scope.Cost + "&currencyid=" + $scope.Currency + "&restriction=" + $scope.Restricitions + mstrSearchparameter + "&EnteredBy=" + $("#enterdBy").val() + "";

          
        }
        else
        {
            var OtherContractStatus = AJService.PostDataToAPI('OtherContract/OtherContractSerch', OtherContract);
            OtherContractStatus.then(function (msg) {
                blockUI.stop();
                if (msg.data == "OK") {
                    $scope.OtherContractListResult();
                }
            },


            function () {
                alert('Please validate details');
            });
            blockUI.stop();
        }
    }
      


   $scope.Clear = function () {

       $scope.PartyName = "";
       $scope.Service = "";
       $scope.sub_service = "";
       $scope.Address = "";
       $scope.Country = "";
       $scope.CountryName = "";
       $scope.State = "";
       $scope.stateName = "";
       $scope.City = "";
       $scope.cityName = "";
       $scope.pincode = "";
       $scope.Mobile = "";
       $scope.Email = "";
       $scope.PanNo = "";
       //   $scope.RequestDate = "";
       $scope.ProjectIsbn = "";
       $scope.ProjectTitle = "";
       $scope.ContractDate = "";

       $scope.ContractType = "";
       $scope.AgreementPeriod = "";
       $scope.ExpiryDate = "";
       $scope.TerritoryRight = "";
       $scope.Payment = "";
       $scope.PaymentPeriod = "",
       $scope.NatureOfWork = "";
       $scope.Division = "";
       $scope.Executive = "";
       $scope.Remarks = "";

     
           $scope.printQuantity = "";
           $scope.PrintRights = "";
           $scope.ElectronicRights = "";
           $scope.EBookRights = "";
           $scope.Cost = "";
           $scope.Currency = "";
           $scope.Restricitions = "";

    

    $scope.Pendingdocumentshow = false;
           //$('#fileid1').hide();
         
           $scope.Contractstatus = "";
           $scope.Signed_Contract_Sent_Date = "";
           $scope.Signed_Contract_received_Date = "";
           $scope.Cancellation_Date = "";
           $scope.Cancellation_Reason = "";

       
     






      


   }

   $scope.ExecutiveDepartment = function ()
   {
       var Department = {
           //Id: $('#hid_DepartmentId').val()
           Id: $("#enterdBy").val()
       };
       // blockUI.start();
       // call API to fetch temp Department list basis on the FlatId

       //var ExecutiveDepartmentStatus = AJService.PostDataToAPI('CommonList/ContractSignedByExecutive', Department);
       var ExecutiveDepartmentStatus = AJService.PostDataToAPI('OtherContract/ContractSignedByExecutive', Department);
       ExecutiveDepartmentStatus.then(function (Executive) {
           $scope.ExecutiveDepartment = Executive.data;
       }, function () {
           //alert('Error in getting Division list');
       });

   }


   $scope.submitForm = function (ReportModel) {

       if ($('#RequestFromDate').val() == "" && $('#RequestToDate').val() != "") {

           $scope.RequestFromDateReq = true;
           $scope.submitted = false;
           return
       }
       else {
           $scope.RequestFromDateReq = false;
           $scope.submitted = true;
         
       }
      

       if ($('#RequestFromDate').val() != "" && $('#RequestToDate').val() != "")
       {
           if ($.datepicker.parseDate('dd/mm/yy', $('#RequestFromDate').val()) >= $.datepicker.parseDate('dd/mm/yy', $('#RequestToDate').val())) {

               if ($.datepicker.parseDate('dd/mm/yy', $('#RequestFromDate').val()) == $.datepicker.parseDate('dd/mm/yy', $('#RequestToDate').val())) {
                   $scope.RequestToDateReq = false;
                   $scope.submitted = true;
               }
               else {
                  // $scope.RequestToDateReq = true;
                  //$scope.submitted = false;
                   //return
               }

              
           }
           else {
               $scope.RequestToDateReq = false;
               $scope.submitted = true;
              
           }
       }



       if ($('#ContractFromDate').val() == "" && $('#ContractToDate').val() != "") {

          // $scope.ContractFromDaterReq = true;
      //     $scope.submitted = false;
         //  return
       }
       else {
         //  $scope.ContractFromDaterReq = false;
         //  $scope.submitted = true;

       }


       if ($('#ContractFromDate').val() != "" && $('#ContractToDate').val() != "") {
           if ($.datepicker.parseDate('dd/mm/yy', $('#ContractFromDate').val()) >= $.datepicker.parseDate('dd/mm/yy', $('#ContractToDate').val())) {

               if ($.datepicker.parseDate('dd/mm/yy', $('#ContractFromDate').val()) == $.datepicker.parseDate('dd/mm/yy', $('#ContractToDate').val())) {
                   $scope.ContractToDateReq = false;
                   $scope.submitted = true;
               }
               else {

                   $scope.ContractToDateReq = true;
                   $scope.submitted = false;
                   return
               }

           }
           else {
               $scope.ContractToDateReq = false;
               $scope.submitted = true;

           }
       }


        $scope.submitted = true;
        if ($scope.userForm.$valid) {
            if ($scope.userForm.$valid) {
                $scope.AddOtherContract();
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    }


    $scope.GetSubServiceListByServiceId = function (ServiceId) {
        //var ServiceId = {
        //    Id: ServiceId
        //};
        var SubServiceList = AJService.GetDataFromAPI("CommonList/SubServiceListByServiceId?ServiceId=" + ServiceId + "");
        SubServiceList.then(function (SubService) {
            $scope._SubServiceListBySewrviceId = SubService.data.query;
        }, function () {
            alert('Error in getting  SubServiceListByServiceId List');
        });
    }

    $scope.BackToserch = function () {
        if ($("#hid_show").val().toLowerCase() == "dashboard") {
            window.location.href = '../../Home/Dashboard/Dashboard';
        }
        else {
            $('#hid_OtherContractBackToSearch').val('');
            $("#search").css("display", "none");
            $scope.Clear();
            $scope.model = {};
            $("#OtherContractSerch").css("display", "block");
            $("#OtherContractSearchHeading").css("display", "block");
            $("#OtherContractViewHeading").css("display", "none");



            var mstr_history = document.referrer;

            if (mstr_history.indexOf("OtherContractView") > 0) {
                window.location.href = "OtherContractSearch?For=View";
            }
            else if (mstr_history.indexOf("id") > 0) {
                window.location.href = "OtherContractSearch?For=Update";

            }
            else {
                window.location.href = window.location.href;
            }

        }
       

    }
    


 
  
    $scope.CalculateExpiry = function (PeriodIdValue) {
        if (PeriodIdValue == undefined) {
            $scope.ExpiryDate = "";
            return false;
        }

        var CurrentDate = new Date();
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
        $scope.ExpiryDate = today;
    }




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
            $scope.otherCities = false;
            $scope.OtherCountry = false;
            $scope.sates = [];
        }, function () {
            alert('Error in getting Geographical list');
        });
    }

    $scope.getCountryStates = function () {
        var GeogType = {
            geogtype: "state",
            parentid: $scope.Country,
        };
        if ($.trim($("#Country option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.OthersNot = false;
            $scope.OthersYes = true;
            $scope.OtherCountry = true;
            $scope.cities = [];
        }
        else {
            $scope.OthersNot = true;
            $scope.OthersYes = false;
            $scope.OtherCountry = false;
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

    $scope.ChangeCitiesCities = function () {
        if ($.trim($("#city option:selected").html()).toLowerCase().indexOf("others") > -1) {
            $scope.otherCities = true;
        }
        else {
            $scope.otherCities = false;

        }
    }


    $scope.function_PaymentYes = function ()
    {
        $scope.PaymentperiodReq = true
    }

    $scope.function_PaymentNo = function () {
        $scope.PaymentperiodReq = false
    }


    $scope.EditOtherContractData = function (Id) {


        if (Id != null) {

            var ExecutiveData = {
                Id: Id
            };
            blockUI.start();

            $window.location.href = '../OtherContract/OtherContractEntry?id=' + Id + "&for=update";


        }
    }


    $scope.ViewOtherContractData = function (Id) {
        if (Id != null) {

            var ExecutiveData = {
                Id: Id
            };
            blockUI.start();

            $window.location.href = '../OtherContract/OtherContractView?id=' + Id + "&for=view";


        }
    }




    $scope.OtherContractReportExcel = function () {

        $('#hid_Report').val(1);

        $scope.AddOtherContract();
    }

    $scope.DisplayDiv = function (count) {
        $('.tooltip-toggle').css("display", "none");
        $('.tooltip-toggle').removeAttr("id");
        $($('.tooltip-toggle')[count - 1]).attr("id", "tooltip-toggle");
    }




    $scope.ExcelReport = function () {

        document.location = GlobalredirectPath + "Contract/OtherContract/exportToExcelOtherContractList?SessionId=" + $("#hid_sessionId").val() + "";

    }



    $scope.OtherContractReportExcel = function () {

       

        $scope.ExcelReport();
    }



    //-------
     $('#hid_show').val(GetParameterValues("show"))

    //for get Query String Variable form Url
     function GetParameterValues(param) {
         var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
         for (var i = 0; i < url.length; i++) {
             var urlparam = url[i].split('=');
             if (urlparam[0] == param) {
                 return urlparam[1];
             }
         }
     }

    //change button if come from Dashboard
    if ($("#hid_show").val().toLowerCase() == "dashboard") {
        $('.backtoSearch').hide();
        $('.backtoDashboard').show();
    }
    else {
        $('.backtoDashboard').hide();
        $('.backtoSearch').show();
    }

    function fireMe(obj) {
        var length = $(".customizeLink").length;
        for (i = 0; i < $(".customizeLink").length; i++) {
            $($(".customizeLink")[i]).attr("sequence", i);
        }
        angular.element(document.getElementById('angularid')).scope().DisplayDiv(parseInt($(obj).attr("sequence")) + 1);
    };

    $scope.GetAllPaymentPeriodList = function () {
        var getPaymentPeriodList = AJService.GetDataFromAPI("CommonList/getAllPaymentPeriodList", null);
        getPaymentPeriodList.then(function (PaymentPeriod) {
            $scope.PaymentPeriodeList = PaymentPeriod.data.query;
        }, function () {
            alert('Error in getting Payment Period list');
        });
    }


    //For Delete Other Contract // Added by Prakash on 05 May, 2017
    $scope.DeleteOtherContract = function (othercontractId, role) {
        var OtherContract = {
            Id: othercontractId == undefined ? 0 : othercontractId,
            //Role: role == undefined ? null : role,
            DeactivateBy: $("#enterdBy").val(),
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
                blockUI.start();

                var OtherContractStatus = AJService.PostDataToAPI('OtherContract/OtherContractSearch1', OtherContract);
                OtherContractStatus.then(function (msg) {                   
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
                                blockUI.stop();
                                $scope.OtherContractListResult();
                            }
                        });

                    }
                });


            }

        });

    }


});