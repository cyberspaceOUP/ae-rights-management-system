app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI, $parse, $timeout) {
    app.expandControllerA($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);




    //$scope.CurrencyByDefault = function () {
    //    var CurrencyByDefaultStatus = AJService.PostDataToAPI('OtherContract/WebGetaCUrrencyByDefault', null);
    //    CurrencyByDefaultStatus.then(function (msg) {
    //        if (msg.data._currencyList != null) {

    //            $scope.CurrencyValue = msg.data._currencyList.CurrencyCode;

    //            if ($scope.CurrencyValue == null) {
    //                $scope.CurrencyReq = true;

    //            }

    //        }
    //    });
    //    }


    function convertDate(dateVal) {

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


    function ConvertDateDDMMYYFormat(dateVal) {

        if (dateVal != null) {
            mstr_Date = dateVal.slice(0, 10).split('-');
            return mstr_Date[2] + "/" + mstr_Date[1] + "/" + mstr_Date[0]


        }
    }

    function ConvertDateDDMMYYFormatInsert(Value) {
        Value.slice(0, 10).split('-');
        Value = mstr_CancellationDate[2] + "/" + mstr_CancellationDate[1] + "/" + mstr_CancellationDate[0]


    }


    $scope.CurrencyValue = 6

    if ($scope.CurrencyValue == null) {
        $scope.CurrencyReq = true;

    }
    $scope.IndianCurrency = "";

    $scope.PageHeadding = "Entry";
    $scope.getCurrencyList = function () {
        var getCurrencyList = AJService.GetDataFromAPI("CommonList/getCurrencyList", null);
        getCurrencyList.then(function (Currency) {
            $scope.CurrencyValueList = Currency.data;
            for (i = 0; i < $scope.CurrencyValueList.length; i++) {

                if ($('#hid_OtherContractEntryId').val() != "1") {
                    if (Currency.data[i].Symbol == "INR") {

                        $scope.CICurrency = Currency.data[i].Id;
                        $scope.IICurrency = Currency.data[i].Id;
                        $scope.VHCurrency = Currency.data[i].Id;
                        $scope.VMCurrency = Currency.data[i].Id;
                        $scope.VLCurrency = Currency.data[i].Id;
                        $scope.IndianCurrency = Currency.data[i].Id;
                        setTimeout(function () { $("[id*=ddlCurrency]").val($scope.IndianCurrency); }, 3000);


                    }
                }

            }
        }, function () {
            //alert('Error in getting Currency List');
        });
    }

    $scope.UploadContractReq = false;
    $scope.ForImageBankReq = false
    $scope.PrintRightsReq = false
    $scope.ElectronicRightsReq = false
    $scope.EBookRightsReq = false
    $scope.CostReq = false
    $scope.RestricitionsReq = false
    $scope.PartyDetailEntryUpdateView = false;

    $scope.PartyDetailEntryUpdate = true;
    $scope.ContractDetailView = false;
    $scope.ContractDetailEntryMode = true;

    $scope.ImageBankView = false;
    $scope.multipleUpload = false;

    $scope.ContractTypeReq = true;
    $scope.DivisionReq = true;
    $scope.ContractstatusReq = false;
    //This section will be used to make the image and video bank section mandatory or non mandatory according to requirement
    $scope.CICurrencyValidation = false;
    $scope.CICostValidation = false;
    $scope.IICostValidation = true;
    $scope.IICurrencyValidation = false;
    $scope.VHCostValidation = false;
    $scope.VMCostValidation = false;
    $scope.VLCostValidation = false;

    $("#hid_FileUpload").val('')
    //end section
    $scope.AddOtherContract = function () {

        var array = [];
        var Array1 = [];
        var Array2 = [];
        if ($('#hid_Uploads').val() != "") {
            var _FileName = $('#hid_Uploads').val().split(",")
            var Index = 0
            var k = 0;
            $('.dropZone').each(function () {

                array = [];
                var FileName = "";
                var _ttlFile = $(this).find('.fileNameClass').length;
                for (var i = 0; i < _ttlFile; i++) {
                    array[i] = $($(this).find('.fileNameClass')[i]).val();
                    FileName = FileName + _FileName[Index] + ",";
                    Index++;
                }
                if (k == 0) {
                    $('#hid_UploadsFile1').val(FileName);
                    Array1 = array;
                }
                else {
                    $('#hid_UploadsFile2').val(FileName);
                    Array2 = array;
                }
                k++;
            })
        }


        var mstr_ForImageBank;
        var mstr_RequestDate = $('#RequestDate').val();
        //   var mstr_ContractDate = $('#ContractDate').val();
        var mstr_PendingRequest;


        var mstr_Signed_Contract_Sent_Date = $('#Signed_Contract_Sent_Date').val();
        var mstr_Signed_Contract_received_Date = $('#Signed_Received_Sent_Date').val();
        var mstr_Cancellation_Date = $('#Cancellation_Date').val();


        if (mstr_RequestDate == "") {
            mstr_RequestDate = null
        }

        //if (mstr_ContractDate == "") {
        //    mstr_ContractDate = null
        //}

        if ($scope.ForImageBankReq == true) {
            mstr_ForImageBank = 1
        }
        else {
            mstr_ForImageBank = 0
        }

        if (mstr_Signed_Contract_Sent_Date == "") {
            mstr_Signed_Contract_Sent_Date = null
        }
        if (mstr_Signed_Contract_received_Date == "") {
            mstr_Signed_Contract_received_Date = null
        }
        if (mstr_Cancellation_Date == "") {
            mstr_Cancellation_Date = null
        }

        var ImageBankList = [];
        $(".imagebank").each(function (index) {
            var cost = $(this).find("input[name*=Cost]");
            var Currency = $(this).find("select[name*=Currency]");
            if (cost.val() == "" || Currency.val() == "") {
                return true;
            }
            ImageBankList[index] = {
                Type: cost.attr("banktype"),
                ShortName: cost.attr("shortname"),
                Fullname: cost.attr("fullname"),
                CurrencyId: Currency.val(),
                Cost: cost.val()
            }

        });






        // blockUI.start();
        var OtherContract = {
            partyname: $scope.PartyName,
            natureofserviceid: $scope.Service,
            natureofsubserviceid: $scope.sub_service,
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
            Requestdate: mstr_RequestDate == null ? null : convertDate(mstr_RequestDate),
            ProjectTitle: $scope.ProjectTitle,
            ProjectISBN: $scope.ProjectIsbn,
            Contracttypeid: $('#ddlContractType').val(),//$scope.ContractType,
            //ContractDate: mstr_ContractDate,
            //Periodofagreement: $scope.AgreementPeriod,
            //Expirydate: $scope.ExpiryDate,
            Territoryrightsid: $scope.TerritoryRight,
            Payment: $scope.Payment,
            paymentperiodid: $('#ddlPaymentPeriod').val(),
            NatureOfWork: $scope.NatureOfWork,
            Division: $scope.Division,
            ContractSignedByExecutiveid: $scope.Executive,
            Remarks: $scope.Remarks,

            Printrunquantity: $scope.printQuantity,
            PrintRights: $scope.PrintRights,
            electronicrights: $scope.ElectronicRights,
            ebookrights: $scope.EBookRights,
            cost: $scope.Cost,
            currencyid: $scope.Currency,
            restriction: $scope.Restricitions,

            Documentname: Array1,
            documentfile: $("#hid_UploadsFile1").val(),


            Contractstatus: $("input[name='Contractstatus']:checked").val() == null ? null : $("input[name='Contractstatus']:checked").val(),//$scope.Contractstatus,


            SignedContractSentDate: ($('#Signed_Contract_Sent_Date').val() == "" ? null : convertDate($('#Signed_Contract_Sent_Date').val())),
            SignedContractReceived_Date: ($('#Signed_Received_Sent_Date').val() == "" ? null : convertDate($('#Signed_Received_Sent_Date').val())),
            CancellationDate: ($('#Cancellation_Date').val() == "" ? null : convertDate($('#Cancellation_Date').val())),
            AgreementDate: ($('#DateOfAgreementValue').val() == "" ? null : convertDate($('#DateOfAgreementValue').val())),

            Effectivedate: ($('#EffectiveDate').val() == "" ? null : convertDate($('#EffectiveDate').val())),


            Expirydate: ($('#ExpiryDate').val() == "" ? null : convertDate($('#ExpiryDate').val())),

            //Contractperiodinmonth: $scope.ContractperiodUpload,

            Cancellation_Reason: $scope.Cancellation_Reason,
            PendingRequest: mstr_PendingRequest,

            DocumentnameLink: Array2,
            documentfileLink: $("#hid_UploadsFile2").val(),
            UpdateRight: $('#hid_updateRight').val(),

            ForImageBank: mstr_ForImageBank,

            Id: $('#hid_OtherContractId').val() == "" ? 0 : $('#hid_OtherContractId').val(),
            EnteredBy: $("#enterdBy").val(),

            PendingRemarks: $('textarea[name=PendingRemarks]').val() == "" ? null : $('textarea[name=PendingRemarks]').val(), //$scope.PendingRemarks,
            VideoImageBank: ImageBankList,
            PaymentAmount: $scope.Payment == "Yes" ? $scope.PaymentAmount : null,
            CurrencyMasterId: $('#ddlPaymentCurrency').val(),
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
                  // alert(Effectivedate);
                  var OtherContractStatus = AJService.PostDataToAPI('OtherContract/InsertOtherContract', OtherContract);
                  OtherContractStatus.then(function (msg) {
                      if (msg.data.status == "OK") {
                          if ($('#hid_OtherContractId').val() != "") {
                             // $('#hid_OtherContractId').val("");

                              SweetAlert.swal({
                                  title: "Updated successfully.", // + $('#hid_OtherContractId').val(),
                                  text: 'Other Contract Code : ' + msg.data.OtherContactCode + '',
                                  type: "success"
                              },
                             function () {
                                 //  $window.location.href = '../OtherContract/OtherContractSearch?For=BackToSearch';

                                 //location.href = "../../Home/Dashboard/Dashboard"; 
                                 location.href = GlobalredirectPath + 'Contract/OtherContract/OtherContractView?id=' + $('#hid_OtherContractId').val() + "&for=view";
                             });



                              ////SweetAlert.swal('Updated successfully.', '', "success");
                              ////// $scope.EditOtherContractData($('#hid_OtherContractId').val());

                              ////if ($('#hid_OtherContractEntryId').val() == "1")
                              ////{
                              ////    setTimeout(function () {
                              ////        $window.location.href = '../OtherContract/OtherContractSearch?For=BackToSearch';
                              ////    }, 3000);
                              ////    //$scope.Clear();
                              ////}

                              $scope.Clear();
                          }

                          else {

                              SweetAlert.swal({
                                  title: "Insert successfully.",
                                  text: 'Other Contract Code : ' + msg.data.OtherContactCode + '',
                                  type: "success"
                              },
                             function () {
                                 //$window.location.href = $window.location.href;
                                 location.href = GlobalredirectPath + 'Contract/OtherContract/OtherContractView?id=' + msg.data.OtherContractIdId + "&for=view";;
                             });

                              //SweetAlert.swal('Insert successfully.', '', "success");
                              //$scope.Clear();

                              //setTimeout(function () { $window.location.href = $window.location.href; }, 4000);

                          }
                      }
                      else {


                          SweetAlert.swal("Error!", "There is an error. Please try again.", "error");


                      }

                  },


                  function () {
                      alert('Please validate details');
                  });
                  blockUI.stop();




              }

          });
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
        $scope.PaymentPeriod = "";
        $scope.NatureOfWork = "";
        $scope.Division = "";
        $scope.Executive = "";
        $scope.Remarks = "";
        $scope.PaymentCurrency = "";

        if ($scope.ForImageBankReq == true) {
            $scope.printQuantity = "";
            $scope.PrintRights = "";
            $scope.ElectronicRights = "";
            $scope.EBookRights = "";
            $scope.Cost = "";
            $scope.Currency = "";
            $scope.Restricitions = "";

        }



        if ($scope.PendingRequestReq == true) {
            $scope.Pendingdocumentshow = false;
            //$('#fileid1').hide();
            $('#btn_Uploader').val("Select File");

            $scope.Contractstatus = "";
            $scope.Signed_Contract_Sent_Date = "";
            $scope.Signed_Contract_received_Date = "";
            $scope.Cancellation_Date = "";
            $scope.Cancellation_Reason = "";

        }


        $('#fileid').hide();
        $('.cstmProgressBar').hide();
        $('.uploadedFileName').hide();
        $scope.documentshow = false;






        $scope.Pendingdocumentshow = false;
        $('#btn_Uploader').val("Select File");

        $('#dropZone0').css("height", "150px");
        $('.fileNameClass').val('');
    }

    $scope.ExecutiveDepartment = function () {
        var Department = {
            //Id: $('#hid_DepartmentId').val()
            Id: $("#enterdBy").val()
        };
        // blockUI.start();
        // call API to fetch temp Department list basis on the FlatId

        var ExecutiveDepartmentStatus = AJService.PostDataToAPI('OtherContract/ContractSignedByExecutive', Department);
        ExecutiveDepartmentStatus.then(function (Executive) {
            $scope.ExecutiveDepartment = Executive.data;
        }, function () {
            //alert('Error in getting Division list');
        });

    }


    $scope.submitForm = function () {

        var errorDiv;
        var errormsg = '';
        $scope.msg = "";

        FileNameArray = $('#dropZone0').find('.fileNameClass');
        var array = [];

        FileNameArray.each(function () {
            array.push(
           $(this).val()
       );

            for (i = 0; i < array.length; i++) {
                if (array[i] == "") {
                    errorDiv = document.getElementById("fileid");
                    errorDiv.innerHTML = "Please enter file name";
                    errormsg = "Please enter file name";
                    $scope.userForm.$valid = false;

                }
                else {
                    $scope.userForm.$valid = true;
                }

            }

        });

        if ($.trim($('.note-editor').find('.note-editable').html()) == '') {
            $scope.descmsg = "Please enter description";
            //   $scope.userForm.$valid = false;

        }

        if ($('#hid_Req').val() == "0") {

            if ($scope.Contractstatus != null) {

                //if ($("#hid_FileUpload").val() == "")
                //{
                if ($scope.Contractstatus.toLowerCase() == "issued") {

                    FileNameArray = $('#dropZone1').find('.fileNameClass');


                    if ($('#Signed_Contract_Sent_Date').val() != "" && $('#Signed_Received_Sent_Date').val() != "") {
                        $scope.userForm.$valid = true;
                    }

                    //commented by Prakash
                    //if (FileNameArray[0] == null) {

                    //    if ($scope.Docurl1.length == 0) {

                    //        $scope.UploadContractReq = true;

                    //        $scope.userForm.$valid = false;
                    //    }

                    //}
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
                }

                else if ($scope.Contractstatus.toLowerCase() == "cancelled" && $scope.Cancellation_Reason != null) {
                    if ($('#Cancellation_Date').val() != "") {
                        $scope.userForm.$valid = true;
                    }
                }
                else if ($scope.Contractstatus.toLowerCase() == "draft" && $scope.Cancellation_Reason != null) {
                    if ($('#Cancellation_Date').val() != "") {
                        $scope.userForm.$valid = true;
                    }
                }
                else if ($scope.Contractstatus.toLowerCase() == "pending") {



                    if ($('[name=PendingRemarks]').val() != "") {
                        $scope.userForm.$valid = true;
                    }
                    else {
                        $scope.userForm.$valid = false;
                    }

                }

                //}





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
            //alert('Error in getting  Sub-Service List By ServiceId List');
        });
    }


    $scope.GetForImageBank = function (ContractType) {
        if (ContractType == 4) {
            $scope.ForImageBankReq = true
            $scope.PrintRightsReq = true
            $scope.ElectronicRightsReq = true
            $scope.EBookRightsReq = true
            $scope.CostReq = true
            $scope.RestricitionsReq = true
            $scope.getCurrencyListView();
            setTimeout(function () { $("[id*=ddlCurrency]").val($scope.IndianCurrency); }, 3000);
        }
        else {
            $scope.ForImageBankReq = false
            $scope.PrintRightsReq = false
            $scope.ElectronicRightsReq = false
            $scope.EBookRightsReq = false
            $scope.CostReq = false
            $scope.RestricitionsReq = false

        }

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
            //alert('Error in getting Geographical list');
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
                //alert('Error in getting Geographical list');
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
            //alert('Error in getting Geographical list');
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


    $scope.function_PaymentYes = function () {
        $scope.PaymentperiodReq = true
        $scope.PaymentAmountReq = true
    }

    $scope.function_PaymentNo = function () {
        $scope.PaymentperiodReq = false
        $scope.PaymentAmountReq = false
        $scope.PaymentAmount = '';
        $scope.PaymentCurrency = '';
        $scope.PaymentPeriod = '';
    }

    $scope.getCurrencyListView = function () {
        var getCurrencyListView = AJService.GetDataFromAPI("CommonList/getCurrencyList", null);
        getCurrencyListView.then(function (Currency) {
            $scope.CurrencyValueList = Currency.data;
            for (i = 0; i < $scope.CurrencyValueList.length; i++) {


                if (Currency.data[i].Symbol == "INR") {

                    $scope.IndianCurrency = Currency.data[i].Id;


                }

            }
        }, function () {
            //alert('Error in getting Currency List');
        });
    }


    $scope.EditOtherContractData = function (Id) {

        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var OtherContractData = {
                Id: Id
            };
            blockUI.start();
            $('#hid_OtherContractId').val(Id);

            if ($('#hid_ViewPage').val() == "View") {
                $scope.PartyDetailEntryUpdateView = true;
                $scope.PartyDetailEntryUpdate = false;

                $scope.ContractstatusReq = true;
                $scope.ContractDetailView = true;
                $scope.ContractDetailEntryMode = false;

                $scope.ImageBankView = true;
                $scope.ForImageBankReq = false;

                // if ($('#hid_updateRight').val() == "ed") {
                $scope.PendingRequestReq = false;
                //    $('#btnSubmit').css("display", "none");
                //}
                //else {
                //    $scope.PendingRequestReq = true;

                //}

                $scope.multipleUpload = true;




                var OtherContractStatus = AJService.PostDataToAPI('OtherContract/OtherContractSerchView', OtherContractData);
                OtherContractStatus.then(function (msg) {



                    if (msg != null) {
                        //history.pushState(null, "", location.href.split("?")[0]);

                        var e = 0;
                        var d = 0;
                        var docNames = '';
                        var Docurl = '';
                        $scope.Docurl = [];

                        if (msg.data.OtherContractDocuments.Documentname != '') {

                            $scope.documentshow = true;
                            var docNames = msg.data.OtherContractDocuments.Documentname.slice(',');
                            var DName = msg.data.OtherContractDocuments.Documentname.slice(',');

                            var DId = msg.data.OtherContractDocuments.DocumentIds.slice(',');

                            var Docurl = msg.data.OtherContractDocuments.documentfile.split(',');
                            //   $scope.Docurl = [];
                            for (var i = 0; i < Docurl.length - 1; i++) {
                                //for (var j = 0; j < docNames.length; j++) {   
                                for (var j = 0, k = 0; j < docNames.length && k < DId.length ; j++, k++) {
                                    if (e == 0 && d == 0) {
                                        //  $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[j].toString(), DocId: DId[k].toString() });
                                        $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[j].toString(), DocId: DId[k].toString() });
                                        //  $scope.Docurl.push({ text: "ABC.DOC", name: "123456TEST", DocId: 10 });

                                        $("#hid_DocumentID").val($("#hid_DocumentID").val() + Docurl[i].toString() + ',');
                                    }
                                    else {
                                        $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[e].toString(), DocId: DId[d].toString() });
                                    }

                                    e = j + 1;
                                    d = k + 1;
                                    i = i + 1;
                                }



                            }

                        }
                        else if ($scope.documentshow == true) {
                            $scope.documentshow = false;
                        }
                        if (msg.data.OtherContractDocuments2.DocumentnameLink != null) {
                            var e1 = 0;
                            var d1 = 0;
                            var docNames1 = '';
                            var Docurl1 = '';
                            $scope.Docurl1 = [];

                            if (msg.data.OtherContractDocuments2.DocumentnameLink != '') {

                                $scope.Pendingdocumentshow = true;
                                var docNames1 = msg.data.OtherContractDocuments2.DocumentnameLink.slice(',');
                                var DName1 = msg.data.OtherContractDocuments2.DocumentnameLink.slice(',');

                                var DId1 = msg.data.OtherContractDocuments2.DocumentlinkIds.slice(',');

                                var Docurl1 = msg.data.OtherContractDocuments2.documentfileLink.split(',');
                                //   $scope.Docurl = [];
                                for (var i = 0; i < Docurl1.length - 1; i++) {
                                    //for (var j = 0; j < docNames.length; j++) {   
                                    for (var j = 0, k = 0; j < docNames1.length && k < DId1.length ; j++, k++) {
                                        if (e1 == 0 && d1 == 0) {
                                            //  $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[j].toString(), DocId: DId[k].toString() });
                                            $scope.Docurl1.push({ text1: Docurl1[i].toString(), name1: docNames1[j].toString(), DocId1: DId1[k].toString() });
                                            // $scope.Docurl1.push({ text: "ABC.DOC", name: "123456TEST", DocId: 10 });

                                            $("#hid_DocumentID").val($("#hid_DocumentID").val() + Docurl1[i].toString() + ',');
                                        }
                                        else {
                                            $scope.Docurl1.push({ text1: Docurl1[i].toString(), name1: docNames1[e1].toString(), DocId1: DId1[d1].toString() });
                                        }

                                        e1 = j + 1;
                                        d1 = k + 1;
                                        i = i + 1;
                                    }



                                }

                            }
                            else if ($scope.Pendingdocumentshow == true) {
                                $scope.Pendingdocumentshow = false;
                            }
                        }




                        if (msg.data._GetOtherContractSearch[0].Status == "0") {
                            $scope.PendingRequestReq = false;
                            $scope.PendingRequestViewReq = true;

                            if (id[1].split('=')[1] == 'update') {
                                $scope.PageHeadding = "Update";
                            }
                            else { $scope.PageHeadding = "View"; }
                            //$scope.PageHeadding = "View";


                            $('#btnSubmit').css("display", "none");


                            $scope.PendingRemarksView = (msg.data._GetOtherContractSearch[0].PendingRemarks == null ? '---' : msg.data._GetOtherContractSearch[0].PendingRemarks)

                            $scope.ContractStatusView = (msg.data._GetOtherContractSearch[0].Contractstatus == null ? '---' : msg.data._GetOtherContractSearch[0].Contractstatus)

                            $scope.SignedContractSentDateView = (msg.data._GetOtherContractSearch[0].SignedContractSentDateValue == null ? '---' : msg.data._GetOtherContractSearch[0].SignedContractSentDateValue)

                            $scope.SignedContractReceivedDateView = (msg.data._GetOtherContractSearch[0].SignedContractReceived_DateValue == null ? '---' : msg.data._GetOtherContractSearch[0].SignedContractReceived_DateValue)

                            $scope.CancellationDateView = (msg.data._GetOtherContractSearch[0].CancellationDateValue == null ? '---' : msg.data._GetOtherContractSearch[0].CancellationDateValue)

                            $scope.CancellationReasonView = (msg.data._GetOtherContractSearch[0].Cancellation_Reason == null ? '---' : msg.data._GetOtherContractSearch[0].Cancellation_Reason)




                            $scope.AgreementDateValueView = (msg.data._GetOtherContractSearch[0].AgreementDateValue == null ? '---' : msg.data._GetOtherContractSearch[0].AgreementDateValue)

                            $scope.EffectivedateValueView = (msg.data._GetOtherContractSearch[0].EffectivedateValue == null ? '---' : msg.data._GetOtherContractSearch[0].EffectivedateValue)

                            $scope.PeriodofagreementView = (msg.data._GetOtherContractSearch[0].Periodofagreement == null ? '---' : msg.data._GetOtherContractSearch[0].Periodofagreement)

                            $scope.ExpirydateValueView = (msg.data._GetOtherContractSearch[0].ExpirydateValue == null ? '---' : msg.data._GetOtherContractSearch[0].ExpirydateValue)





                        }


                        $scope.ContractCodeView = (msg.data._GetOtherContractSearch[0].othercontractcode == null ? '---' : msg.data._GetOtherContractSearch[0].othercontractcode);

                        $scope.PartyNameView = (msg.data._GetOtherContractSearch[0].partyname == null ? '---' : msg.data._GetOtherContractSearch[0].partyname);

                        $scope.ServiceView = (msg.data._GetOtherContractSearch[0].Service == null ? '---' : msg.data._GetOtherContractSearch[0].Service);

                        $scope.sub_serviceView = (msg.data._GetOtherContractSearch[0].SubService == null ? '---' : msg.data._GetOtherContractSearch[0].SubService);

                        $scope.AddressView = (msg.data._GetOtherContractSearch[0].Address == null ? '---' : msg.data._GetOtherContractSearch[0].Address);



                        if (msg.data._GetOtherContractSearch[0].OtherContractCountry != null) {
                            $scope.CountryView = (msg.data._GetOtherContractSearch[0].OtherContractCountry == null ? '---' : msg.data._GetOtherContractSearch[0].OtherContractCountry)
                            //  $scope.CountryReq = true;
                        }

                        if (msg.data._GetOtherContractSearch[0].othercountry != null) {
                            $scope.OtherCountryView = (msg.data._GetOtherContractSearch[0].othercountry == null ? '---' : msg.data._GetOtherContractSearch[0].othercountry)
                            $scope.OtherCountryReq = true;
                        }


                        if (msg.data._GetOtherContractSearch[0].OtherContractState != null) {
                            $scope.StateView = (msg.data._GetOtherContractSearch[0].OtherContractState == null ? '---' : msg.data._GetOtherContractSearch[0].OtherContractState)
                            // $scope.StateReq = true;
                        }

                        if (msg.data._GetOtherContractSearch[0].otherstate != null) {
                            $scope.OtherStateView = (msg.data._GetOtherContractSearch[0].otherstate == null ? '---' : msg.data._GetOtherContractSearch[0].otherstate)
                            $scope.OtherStateReq = true;
                        }

                        if (msg.data._GetOtherContractSearch[0].OtherContractCity != null) {
                            $scope.CityView = (msg.data._GetOtherContractSearch[0].OtherContractCity == null ? '---' : msg.data._GetOtherContractSearch[0].OtherContractCity)
                            $scope.CityReq = true;
                        }

                        if (msg.data._GetOtherContractSearch[0].othercity != null) {
                            $scope.OtherCityView = (msg.data._GetOtherContractSearch[0].othercity == null ? '---' : msg.data._GetOtherContractSearch[0].othercity)
                            $scope.OtherCityReq = true;
                        }

                        $scope.EmailView = (msg.data._GetOtherContractSearch[0].Email == null ? '---' : msg.data._GetOtherContractSearch[0].Email)

                        $scope.MobileView = (msg.data._GetOtherContractSearch[0].Mobile == null ? '---' : msg.data._GetOtherContractSearch[0].Mobile)

                        $scope.PanNoView = (msg.data._GetOtherContractSearch[0].PANNo == null ? '---' : msg.data._GetOtherContractSearch[0].PANNo)

                        $scope.PinCodeView = (msg.data._GetOtherContractSearch[0].Pincode == null ? '---' : msg.data._GetOtherContractSearch[0].Pincode)





                        $scope.RequestDateView = (msg.data._GetOtherContractSearch[0].RequestdateValue == null ? '---' : msg.data._GetOtherContractSearch[0].RequestdateValue)


                        $scope.ProjectTitleView = (msg.data._GetOtherContractSearch[0].ProjectTitle == null ? '---' : msg.data._GetOtherContractSearch[0].ProjectTitle)

                        $scope.ProjectIsbnView = (msg.data._GetOtherContractSearch[0].ProjectISBN == null ? '---' : msg.data._GetOtherContractSearch[0].ProjectISBN)


                        $scope.ContractTypeView = (msg.data._GetOtherContractSearch[0].contractname == null ? '---' : msg.data._GetOtherContractSearch[0].contractname)

                        // $scope.ContractDateView = (msg.data._GetOtherContractSearch[0].ContractDate == null ? '---' : msg.data._GetOtherContractSearch[0].ContractDate)


                        //     $scope.AgreementPeriodView = (msg.data._GetOtherContractSearch[0].Periodofagreement == null ? '---' : msg.data._GetOtherContractSearch[0].Periodofagreement)

                        //   $scope.ExpiryDateView = (msg.data._GetOtherContractSearch[0].Expirydate == null ? '---' : msg.data._GetOtherContractSearch[0].Expirydate)


                        $scope.DivisionView = (msg.data._GetOtherContractSearch[0].divisionname == null ? '---' : msg.data._GetOtherContractSearch[0].divisionname)

                        $scope.ExecutiveView = (msg.data._GetOtherContractSearch[0].executiveName == null ? '---' : msg.data._GetOtherContractSearch[0].executiveName)

                        $scope.TerritoryRightView = (msg.data._GetOtherContractSearch[0].territoryrights == null ? '---' : msg.data._GetOtherContractSearch[0].territoryrights);

                        $scope.PaymentView = (msg.data._GetOtherContractSearch[0].Payment == null ? '---' : msg.data._GetOtherContractSearch[0].Payment)


                        $scope.PaymentPeriodView = (msg.data._GetOtherContractSearch[0].paymenttype == null ? '---' : msg.data._GetOtherContractSearch[0].paymenttype)

                        $scope.NatureOfWorkView = (msg.data._GetOtherContractSearch[0].NatureOfWork == null ? '---' : msg.data._GetOtherContractSearch[0].NatureOfWork)

                        $scope.RemarksView = (msg.data._GetOtherContractSearch[0].Remarks == null ? '---' : msg.data._GetOtherContractSearch[0].Remarks)




                        if (msg.data._GetOtherContractSearch[0].printrunquantity == null && msg.data._GetOtherContractSearch[0].PrintRights == null && msg.data._GetOtherContractSearch[0].electronicrights == null && msg.data._GetOtherContractSearch[0].ebookrights == null && msg.data._GetOtherContractSearch[0].cost == null) {
                            $scope.ImageBankView = false;
                        }


                        $scope.PrintRunQuantityView = (msg.data._GetOtherContractSearch[0].Printrunquantity == null ? '---' : msg.data._GetOtherContractSearch[0].Printrunquantity)


                        $scope.PrintRightsView = (msg.data._GetOtherContractSearch[0].PrintRights == null ? '---' : msg.data._GetOtherContractSearch[0].PrintRights)

                        $scope.ElectronicRightsView = (msg.data._GetOtherContractSearch[0].electronicrights == null ? '---' : msg.data._GetOtherContractSearch[0].electronicrights)

                        $scope.EBookRightsView = (msg.data._GetOtherContractSearch[0].ebookrights == null ? '---' : msg.data._GetOtherContractSearch[0].ebookrights)

                        // $scope.CostView = (msg.data._GetOtherContractSearch[0].cost == null ? '---' : msg.data._GetOtherContractSearch[0].cost)


                        //  $scope.CurrencyView = (msg.data._GetOtherContractSearch[0].currencyname == null ? '---' : msg.data._GetOtherContractSearch[0].currencyname)

                        $scope.RestricitionsView = (msg.data._GetOtherContractSearch[0].restriction == null ? '---' : msg.data._GetOtherContractSearch[0].restriction)





                        $scope.OthorContractMultiDivisionLink = msg.data.OtherContractDivisionLink;

                        $scope.ImageOtherContact = msg.data.ImageTypeOtherContact;
                        $scope.VideoOtherContact = msg.data.VideoTypeOtherContact;

                        $scope.PaymentAmount = msg.data._GetOtherContractSearch[0].PaymentAmount == null ? '---' : msg.data._GetOtherContractSearch[0].PaymentAmount;

                        $scope.PaymentCurrency = msg.data._GetOtherContractSearch[0].Symbol == null ? '---' : msg.data._GetOtherContractSearch[0].Symbol;

                        //$('#btnSubmit').html("Update");

                        $('#btnSubmit').hide();

                        $('#hid_Req').val(0);

                        blockUI.stop();
                    }
                    else {
                        SweetAlert.swal("Error!", "Error in system. Please try again", "error");
                        blockUI.stop();
                    }
                });
            }
            else {

                if ($('#hid_updateRight').val() == "rt" || $('#hid_updateRight').val() == "ed") {  //chage in rt

                    $scope.PartyDetailEntryUpdateView = true;
                    $scope.PartyDetailEntryUpdate = false;

                    $scope.ContractstatusReq = true;
                    $scope.ContractDetailView = true;
                    $scope.ContractDetailEntryMode = false;

                    $scope.ImageBankView = true;
                    $scope.ForImageBankReq = false;

                    $scope.PendingRequestReq = true;

                    if ($('#hid_updateRight').val() == "ed") {
                        $scope.PendingRequestReq = false;
                        $('#btnSubmit').css("display", "none");
                    }
                    else {
                        $scope.PendingRequestReq = true;

                    }

                    $scope.multipleUpload = true;




                    var OtherContractStatus = AJService.PostDataToAPI('OtherContract/OtherContractSerchView', OtherContractData);
                    OtherContractStatus.then(function (msg) {



                        if (msg != null) {
                            //history.pushState(null, "", location.href.split("?")[0]);

                            var e = 0;
                            var d = 0;
                            var docNames = '';
                            var Docurl = '';
                            $scope.Docurl = [];

                            if (msg.data.OtherContractDocuments.Documentname != '') {

                                $scope.documentshow = true;
                                var docNames = msg.data.OtherContractDocuments.Documentname.slice(',');
                                var DName = msg.data.OtherContractDocuments.Documentname.slice(',');

                                var DId = msg.data.OtherContractDocuments.DocumentIds.slice(',');

                                var Docurl = msg.data.OtherContractDocuments.documentfile.split(',');
                                //   $scope.Docurl = [];
                                for (var i = 0; i < Docurl.length - 1; i++) {
                                    //for (var j = 0; j < docNames.length; j++) {   
                                    for (var j = 0, k = 0; j < docNames.length && k < DId.length ; j++, k++) {
                                        if (e == 0 && d == 0) {
                                            //  $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[j].toString(), DocId: DId[k].toString() });
                                            $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[j].toString(), DocId: DId[k].toString() });
                                            //  $scope.Docurl.push({ text: "ABC.DOC", name: "123456TEST", DocId: 10 });

                                            $("#hid_DocumentID").val($("#hid_DocumentID").val() + Docurl[i].toString() + ',');
                                        }
                                        else {
                                            $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[e].toString(), DocId: DId[d].toString() });
                                        }

                                        e = j + 1;
                                        d = k + 1;
                                        i = i + 1;
                                    }



                                }

                            }
                            else if ($scope.documentshow == true) {
                                $scope.documentshow = false;
                            }
                            if (msg.data.OtherContractDocuments2.DocumentnameLink != null) {
                                var e1 = 0;
                                var d1 = 0;
                                var docNames1 = '';
                                var Docurl1 = '';
                                $scope.Docurl1 = [];

                                if (msg.data.OtherContractDocuments2.DocumentnameLink != '') {

                                    $scope.Pendingdocumentshow = true;
                                    var docNames1 = msg.data.OtherContractDocuments2.DocumentnameLink.slice(',');
                                    var DName1 = msg.data.OtherContractDocuments2.DocumentnameLink.slice(',');

                                    var DId1 = msg.data.OtherContractDocuments2.DocumentlinkIds.slice(',');

                                    var Docurl1 = msg.data.OtherContractDocuments2.documentfileLink.split(',');
                                    //   $scope.Docurl = [];
                                    for (var i = 0; i < Docurl1.length - 1; i++) {
                                        //for (var j = 0; j < docNames.length; j++) {   
                                        for (var j = 0, k = 0; j < docNames1.length && k < DId1.length ; j++, k++) {
                                            if (e1 == 0 && d1 == 0) {
                                                //  $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[j].toString(), DocId: DId[k].toString() });
                                                $scope.Docurl1.push({ text1: Docurl1[i].toString(), name1: docNames1[j].toString(), DocId1: DId1[k].toString() });
                                                // $scope.Docurl1.push({ text: "ABC.DOC", name: "123456TEST", DocId: 10 });

                                                $("#hid_DocumentID").val($("#hid_DocumentID").val() + Docurl1[i].toString() + ',');
                                            }
                                            else {
                                                $scope.Docurl1.push({ text1: Docurl1[i].toString(), name1: docNames1[e1].toString(), DocId1: DId1[d1].toString() });
                                            }

                                            e1 = j + 1;
                                            d1 = k + 1;
                                            i = i + 1;
                                        }



                                    }

                                }
                                else if ($scope.Pendingdocumentshow == true) {
                                    $scope.Pendingdocumentshow = false;
                                }
                            }




                            //if (msg.data._GetOtherContractSearch[0].Status == "0") {
                            //  $scope.PendingRequestReq = false;
                            $scope.PendingRequestViewReq = true;

                            if (id[1].split('=')[1] == 'update') {
                                $scope.PageHeadding = "Update";
                            }
                            else { $scope.PageHeadding = "View"; }
                            //$scope.PageHeadding = "View";

                            //  $('#btnSubmit').css("display", "none");


                            if (msg.data._GetOtherContractSearch[0].Contractstatus != 'Issued') {

                                $scope.Contractstatus = msg.data._GetOtherContractSearch[0].Contractstatus




                                $scope.Signed_Contract_Sent_Date = (msg.data._GetOtherContractSearch[0].SignedContractSentDateValue == null ? null : (msg.data._GetOtherContractSearch[0].SignedContractSentDateValue)),
                                $scope.Signed_Contract_received_Date = (msg.data._GetOtherContractSearch[0].SignedContractReceived_DateValue == null ? null : (msg.data._GetOtherContractSearch[0].SignedContractReceived_DateValue)),

                                $scope.Cancellation_Date = (msg.data._GetOtherContractSearch[0].CancellationDateValue == null ? null : (msg.data._GetOtherContractSearch[0].CancellationDateValue)),
                                   $scope.Cancellation_Reason = msg.data._GetOtherContractSearch[0].Cancellation_Reason,

                                $scope.DateOfAgreement = (msg.data._GetOtherContractSearch[0].AgreementDateValue == null ? null : (msg.data._GetOtherContractSearch[0].AgreementDateValue))

                                $scope.EffectiveDate = (msg.data._GetOtherContractSearch[0].EffectivedateValue == null ? null : (msg.data._GetOtherContractSearch[0].EffectivedateValue))

                                $scope.ContractperiodUpload = msg.data._GetOtherContractSearch[0].Periodofagreement

                                $scope.ExpiryDate = (msg.data._GetOtherContractSearch[0].ExpirydateValue == null ? null : (msg.data._GetOtherContractSearch[0].ExpirydateValue))
                                $scope.PendingRemarks = msg.data._GetOtherContractSearch[0].PendingRemarks
                            }
                            else {


                                $scope.PendingRemarksView = (msg.data._GetOtherContractSearch[0].PendingRemarks == null ? '---' : msg.data._GetOtherContractSearch[0].PendingRemarks)

                                $scope.ContractStatusView = (msg.data._GetOtherContractSearch[0].Contractstatus == null ? '---' : msg.data._GetOtherContractSearch[0].Contractstatus)

                                $scope.SignedContractSentDateView = (msg.data._GetOtherContractSearch[0].SignedContractSentDateValue == null ? '---' : msg.data._GetOtherContractSearch[0].SignedContractSentDateValue)

                                $scope.SignedContractReceivedDateView = (msg.data._GetOtherContractSearch[0].SignedContractReceived_DateValue == null ? '---' : msg.data._GetOtherContractSearch[0].SignedContractReceived_DateValue)

                                $scope.CancellationDateView = (msg.data._GetOtherContractSearch[0].CancellationDateValue == null ? '---' : msg.data._GetOtherContractSearch[0].CancellationDateValue)

                                $scope.CancellationReasonView = (msg.data._GetOtherContractSearch[0].Cancellation_Reason == null ? '---' : msg.data._GetOtherContractSearch[0].Cancellation_Reason)




                                $scope.AgreementDateValueView = (msg.data._GetOtherContractSearch[0].AgreementDateValue == null ? '---' : msg.data._GetOtherContractSearch[0].AgreementDateValue)

                                $scope.EffectivedateValueView = (msg.data._GetOtherContractSearch[0].EffectivedateValue == null ? '---' : msg.data._GetOtherContractSearch[0].EffectivedateValue)

                                $scope.PeriodofagreementView = (msg.data._GetOtherContractSearch[0].Periodofagreement == null ? '---' : msg.data._GetOtherContractSearch[0].Periodofagreement)

                                $scope.ExpirydateValueView = (msg.data._GetOtherContractSearch[0].ExpirydateValue == null ? '---' : msg.data._GetOtherContractSearch[0].ExpirydateValue)


                            }






                            //}


                            $scope.ContractCodeView = (msg.data._GetOtherContractSearch[0].othercontractcode == null ? '---' : msg.data._GetOtherContractSearch[0].othercontractcode);

                            $scope.PartyNameView = (msg.data._GetOtherContractSearch[0].partyname == null ? '---' : msg.data._GetOtherContractSearch[0].partyname);

                            $scope.ServiceView = (msg.data._GetOtherContractSearch[0].Service == null ? '---' : msg.data._GetOtherContractSearch[0].Service);

                            $scope.sub_serviceView = (msg.data._GetOtherContractSearch[0].SubService == null ? '---' : msg.data._GetOtherContractSearch[0].SubService);

                            $scope.AddressView = (msg.data._GetOtherContractSearch[0].Address == null ? '---' : msg.data._GetOtherContractSearch[0].Address);



                            if (msg.data._GetOtherContractSearch[0].OtherContractCountry != null) {
                                $scope.CountryView = (msg.data._GetOtherContractSearch[0].OtherContractCountry == null ? '---' : msg.data._GetOtherContractSearch[0].OtherContractCountry)
                                //  $scope.CountryReq = true;
                            }

                            if (msg.data._GetOtherContractSearch[0].othercountry != null) {
                                $scope.OtherCountryView = (msg.data._GetOtherContractSearch[0].othercountry == null ? '---' : msg.data._GetOtherContractSearch[0].othercountry)
                                $scope.OtherCountryReq = true;
                            }


                            if (msg.data._GetOtherContractSearch[0].OtherContractState != null) {
                                $scope.StateView = (msg.data._GetOtherContractSearch[0].OtherContractState == null ? '---' : msg.data._GetOtherContractSearch[0].OtherContractState)
                                // $scope.StateReq = true;
                            }

                            if (msg.data._GetOtherContractSearch[0].otherstate != null) {
                                $scope.OtherStateView = (msg.data._GetOtherContractSearch[0].otherstate == null ? '---' : msg.data._GetOtherContractSearch[0].otherstate)
                                $scope.OtherStateReq = true;
                            }

                            if (msg.data._GetOtherContractSearch[0].OtherContractCity != null) {
                                $scope.CityView = (msg.data._GetOtherContractSearch[0].OtherContractCity == null ? '---' : msg.data._GetOtherContractSearch[0].OtherContractCity)
                                $scope.CityReq = true;
                            }

                            if (msg.data._GetOtherContractSearch[0].othercity != null) {
                                $scope.OtherCityView = (msg.data._GetOtherContractSearch[0].othercity == null ? '---' : msg.data._GetOtherContractSearch[0].othercity)
                                $scope.OtherCityReq = true;
                            }

                            $scope.EmailView = (msg.data._GetOtherContractSearch[0].Email == null ? '---' : msg.data._GetOtherContractSearch[0].Email)

                            $scope.MobileView = (msg.data._GetOtherContractSearch[0].Mobile == null ? '---' : msg.data._GetOtherContractSearch[0].Mobile)

                            $scope.PanNoView = (msg.data._GetOtherContractSearch[0].PANNo == null ? '---' : msg.data._GetOtherContractSearch[0].PANNo)

                            $scope.PinCodeView = (msg.data._GetOtherContractSearch[0].Pincode == null ? '---' : msg.data._GetOtherContractSearch[0].Pincode)







                            $scope.RequestDateView = (msg.data._GetOtherContractSearch[0].RequestdateValue == null ? '---' : msg.data._GetOtherContractSearch[0].RequestdateValue)


                            $scope.ProjectTitleView = (msg.data._GetOtherContractSearch[0].ProjectTitle == null ? '---' : msg.data._GetOtherContractSearch[0].ProjectTitle)

                            $scope.ProjectIsbnView = (msg.data._GetOtherContractSearch[0].ProjectISBN == null ? '---' : msg.data._GetOtherContractSearch[0].ProjectISBN)


                            $scope.ContractTypeView = (msg.data._GetOtherContractSearch[0].contractname == null ? '---' : msg.data._GetOtherContractSearch[0].contractname)

                            // $scope.ContractDateView = (msg.data._GetOtherContractSearch[0].ContractDate == null ? '---' : msg.data._GetOtherContractSearch[0].ContractDate)


                            //     $scope.AgreementPeriodView = (msg.data._GetOtherContractSearch[0].Periodofagreement == null ? '---' : msg.data._GetOtherContractSearch[0].Periodofagreement)

                            //   $scope.ExpiryDateView = (msg.data._GetOtherContractSearch[0].Expirydate == null ? '---' : msg.data._GetOtherContractSearch[0].Expirydate)


                            $scope.DivisionView = (msg.data._GetOtherContractSearch[0].divisionname == null ? '---' : msg.data._GetOtherContractSearch[0].divisionname)

                            $scope.ExecutiveView = (msg.data._GetOtherContractSearch[0].executiveName == null ? '---' : msg.data._GetOtherContractSearch[0].executiveName)

                            $scope.TerritoryRightView = (msg.data._GetOtherContractSearch[0].territoryrights == null ? '---' : msg.data._GetOtherContractSearch[0].territoryrights);

                            $scope.PaymentView = (msg.data._GetOtherContractSearch[0].Payment == null ? '---' : msg.data._GetOtherContractSearch[0].Payment)


                            $scope.PaymentPeriodView = (msg.data._GetOtherContractSearch[0].paymenttype == null ? '---' : msg.data._GetOtherContractSearch[0].paymenttype)

                            $scope.NatureOfWorkView = (msg.data._GetOtherContractSearch[0].NatureOfWork == null ? '---' : msg.data._GetOtherContractSearch[0].NatureOfWork)

                            $scope.RemarksView = (msg.data._GetOtherContractSearch[0].Remarks == null ? '---' : msg.data._GetOtherContractSearch[0].Remarks)




                            if (msg.data._GetOtherContractSearch[0].printrunquantity == null && msg.data._GetOtherContractSearch[0].PrintRights == null && msg.data._GetOtherContractSearch[0].electronicrights == null && msg.data._GetOtherContractSearch[0].ebookrights == null && msg.data._GetOtherContractSearch[0].cost == null) {
                                $scope.ImageBankView = false;
                            }


                            $scope.PrintRunQuantityView = (msg.data._GetOtherContractSearch[0].Printrunquantity == null ? '---' : msg.data._GetOtherContractSearch[0].Printrunquantity)


                            $scope.PrintRightsView = (msg.data._GetOtherContractSearch[0].PrintRights == null ? '---' : msg.data._GetOtherContractSearch[0].PrintRights)

                            $scope.ElectronicRightsView = (msg.data._GetOtherContractSearch[0].electronicrights == null ? '---' : msg.data._GetOtherContractSearch[0].electronicrights)

                            $scope.EBookRightsView = (msg.data._GetOtherContractSearch[0].ebookrights == null ? '---' : msg.data._GetOtherContractSearch[0].ebookrights)

                            // $scope.CostView = (msg.data._GetOtherContractSearch[0].cost == null ? '---' : msg.data._GetOtherContractSearch[0].cost)


                            //  $scope.CurrencyView = (msg.data._GetOtherContractSearch[0].currencyname == null ? '---' : msg.data._GetOtherContractSearch[0].currencyname)

                            $scope.RestricitionsView = (msg.data._GetOtherContractSearch[0].restriction == null ? '---' : msg.data._GetOtherContractSearch[0].restriction)





                            $scope.OthorContractMultiDivisionLink = msg.data.OtherContractDivisionLink;

                            $scope.ImageOtherContact = msg.data.ImageTypeOtherContact;
                            $scope.VideoOtherContact = msg.data.VideoTypeOtherContact;

                            $scope.PaymentAmount = msg.data._GetOtherContractSearch[0].PaymentAmount == null ? '---' : msg.data._GetOtherContractSearch[0].PaymentAmount;

                            $scope.PaymentCurrency = msg.data._GetOtherContractSearch[0].Symbol == null ? '---' : msg.data._GetOtherContractSearch[0].Symbol;

                            $('#btnSubmit').html("Update");

                            $('#hid_Req').val(0);

                            blockUI.stop();

                        }
                        else {
                            SweetAlert.swal("Error!", "Error in system. Please try again", "error");
                            blockUI.stop();
                        }

                    });

                }
                else if ($('#hid_updateRight').val() == "ad" || $('#hid_updateRight').val() == "sa") {
                    var OtherContractStatus = AJService.PostDataToAPI('OtherContract/WebGetaOtherContractById', OtherContractData);
                    $scope.PartyDetailEntryUpdateView = false;
                    $scope.PartyDetailEntryUpdate = true;
                    $scope.PendingRequestReq = true;
                    $scope.multipleUpload = true;
                    $scope.ContractstatusReq = true;
                    OtherContractStatus.then(function (msg) {


                        if (msg.data._ContractImageBank != null) {
                            $scope.ForImageBankReq = true;

                            $scope.printQuantity = msg.data._ContractImageBank.Printrunquantity;

                            $scope.PrintRights = msg.data._ContractImageBank.PrintRights;
                            $scope.ElectronicRights = msg.data._ContractImageBank.electronicrights;

                            $scope.EBookRights = msg.data._ContractImageBank.ebookrights;
                            $scope.Cost = msg.data._ContractImageBank.cost;



                            setTimeout(function () {
                                $scope.CurrencyValue = msg.data._ContractImageBank.currencyid;
                            }, 1500)


                            $scope.Restricitions = msg.data._ContractImageBank.restriction;

                        }

                        if (msg != null) {
                            //history.pushState(null, "", location.href.split("?")[0]);
                            var e = 0;
                            var d = 0;
                            var docNames = '';
                            var Docurl = '';
                            $scope.Docurl = [];

                            if (msg.data.OtherContractDocuments.Documentname != '') {
                                // 
                                $scope.documentshow = true;
                                var docNames = msg.data.OtherContractDocuments.Documentname.slice(',');
                                var DName = msg.data.OtherContractDocuments.Documentname.slice(',');

                                var DId = msg.data.OtherContractDocuments.DocumentIds.slice(',');

                                var Docurl = msg.data.OtherContractDocuments.documentfile.split(',');
                                //   $scope.Docurl = [];
                                for (var i = 0; i < Docurl.length - 1; i++) {
                                    //for (var j = 0; j < docNames.length; j++) {   
                                    for (var j = 0, k = 0; j < docNames.length && k < DId.length ; j++, k++) {
                                        if (e == 0 && d == 0) {
                                            //  $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[j].toString(), DocId: DId[k].toString() });
                                            $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[j].toString(), DocId: DId[k].toString() });
                                            //  $scope.Docurl.push({ text: "ABC.DOC", name: "123456TEST", DocId: 10 });

                                            $("#hid_DocumentID").val($("#hid_DocumentID").val() + Docurl[i].toString() + ',');
                                        }
                                        else {
                                            $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[e].toString(), DocId: DId[d].toString() });
                                        }

                                        e = j + 1;
                                        d = k + 1;
                                        i = i + 1;
                                    }



                                }

                            }
                            else if ($scope.documentshow == true) {
                                $scope.documentshow = false;
                            }



                            if (msg.data.OtherContractDocuments2.DocumentnameLink != null) {

                                var e1 = 0;
                                var d1 = 0;
                                var docNames1 = '';
                                var Docurl1 = '';
                                $scope.Docurl1 = [];

                                if (msg.data.OtherContractDocuments2.DocumentnameLink != '') {

                                    $scope.Pendingdocumentshow = true;
                                    var docNames1 = msg.data.OtherContractDocuments2.DocumentnameLink.slice(',');
                                    var DName1 = msg.data.OtherContractDocuments2.DocumentnameLink.slice(',');

                                    var DId1 = msg.data.OtherContractDocuments2.DocumentlinkIds.slice(',');

                                    var Docurl1 = msg.data.OtherContractDocuments2.documentfileLink.split(',');
                                    //   $scope.Docurl = [];
                                    for (var i = 0; i < Docurl1.length - 1; i++) {
                                        //for (var j = 0; j < docNames.length; j++) {   
                                        for (var j = 0, k = 0; j < docNames1.length && k < DId1.length ; j++, k++) {
                                            if (e1 == 0 && d1 == 0) {
                                                //  $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[j].toString(), DocId: DId[k].toString() });
                                                $scope.Docurl1.push({ text1: Docurl1[i].toString(), name1: docNames1[j].toString(), DocId1: DId1[k].toString() });
                                                // $scope.Docurl1.push({ text: "ABC.DOC", name: "123456TEST", DocId: 10 });
                                                $("#hid_FileUpload").val(0)
                                                $("#hid_DocumentID").val($("#hid_DocumentID").val() + Docurl1[i].toString() + ',');
                                            }
                                            else {
                                                $scope.Docurl1.push({ text1: Docurl1[i].toString(), name1: docNames1[e1].toString(), DocId1: DId1[d1].toString() });
                                            }

                                            e1 = j + 1;
                                            d1 = k + 1;
                                            i = i + 1;
                                        }



                                    }

                                }
                                else if ($scope.Pendingdocumentshow == true) {
                                    $scope.Pendingdocumentshow = false;
                                }
                            }


                            setTimeout(function () {
                                $scope.getStateCities()
                                $scope.City = msg.data._OtherContract.Cityid
                                $scope.ContractType = msg.data._OtherContract.Contracttypeid
                                $scope.DivisionValue = msg.data._OtherContract.divisionid
                                $scope.PaymentPeriod = msg.data._OtherContract.paymentperiodid
                                $scope.PaymentCurrency = msg.data._OtherContract.CurrencyMasterId
                            }, 2000);


                            if (msg.data._OtherContract.Contracttypeid != null) {
                                $scope.ContractTypeReq = false;
                            }

                            if (msg.data._OtherContract.divisionid != null) {
                                $scope.DivisionReq = false;
                            }

                            var mstr_Requestdate = null
                            if (msg.data._OtherContract.Requestdate != null) {
                                mstr_Requestdate = msg.data._OtherContract.Requestdate.slice(0, 10).split('-');
                                var Value = mstr_Requestdate[2] + "/" + mstr_Requestdate[1] + "/" + mstr_Requestdate[0]
                                mstr_Requestdate = Value

                            }

                            //  alert(mstr_CancellationDate);


                            $scope.PartyName = msg.data._OtherContract.partyname,
                      $scope.Service = msg.data._OtherContract.natureofserviceid,

                      $scope.GetSubServiceListByServiceId(msg.data._OtherContract.natureofserviceid);

                            if (msg.data._OtherContract.natureofsubserviceid != null) {
                                $scope.sub_service = msg.data._OtherContract.natureofsubserviceid
                            }


                            $scope.Address = msg.data._OtherContract.Address,


                            $scope.Country = msg.data._OtherContract.CountryId,

                            $scope.getCountryStates();
                            $scope.State = msg.data._OtherContract.Stateid,

                            $scope.getStateCities()
                            $scope.City = msg.data._OtherContract.Cityid,

                             $scope.CountryName = msg.data._OtherContract.OtherCountry,
                             $scope.stateName = msg.data._OtherContract.OtherState,
                             $scope.cityName = msg.data._OtherContract.OtherCity,
                             $scope.pincode = msg.data._OtherContract.Pincode,
                             $scope.Mobile = msg.data._OtherContract.Mobile,

                             $scope.Email = msg.data._OtherContract.Email,
                             $scope.PanNo = msg.data._OtherContract.PANNo,




                            //  alert(ConvertDateDDMMYYFormatInsert(msg.data._OtherContract.Requestdate));



                             $scope.RequestDate = mstr_Requestdate == null ? null : mstr_Requestdate,
                             $scope.ProjectTitle = msg.data._OtherContract.ProjectTitle,
                             $scope.ProjectIsbn = msg.data._OtherContract.ProjectISBN,

                            //setTimeout(function () {
                            //    $scope.DivisionValue = msg.data._OtherContract.divisionid
                            //   $scope.ContractType = msg.data._OtherContract.Contracttypeid

                            //},500)
                            ///  alert(msg.data._OtherContract.Contracttypeid)
                            setTimeout(function () {
                                $scope.ContractType = msg.data._OtherContract.Contracttypeid
                            }, 1500)
                            //   $scope.ContractType = msg.data._OtherContract.Contracttypeid,

                            $scope.ContractDate = msg.data._OtherContract.ContractDate,

                            $scope.AgreementPeriod = msg.data._OtherContract.Periodofagreement,
                            $scope.ExpiryDate = msg.data._OtherContract.Expirydate,
                            $scope.TerritoryRight = msg.data._OtherContract.Territoryrightsid,

                            $scope.Payment = msg.data._OtherContract.Payment,

                            // alert(msg.data._OtherContract.paymentperiodid)


                            $scope.NatureOfWork = msg.data._OtherContract.NatureOfWork,

                           setTimeout(function () {
                               $scope.PaymentPeriod = msg.data._OtherContract.paymentperiodid
                               $scope.PaymentCurrency = msg.data._OtherContract.CurrencyMasterId
                           }, 400)


                            //   $scope.DivisionValue = msg.data._OtherContract.divisionid,
                            /// alert(msg.data.ImageTypeOtherContact.Cost);








                            //debugger
                            $scope.Division = [];
                            if (msg.data.OtherContractDivisionLink != null) {

                                for (var i = 0; i <= msg.data.OtherContractDivisionLink.length - 1; i++) {

                                    $scope.Division.push("" + msg.data.OtherContractDivisionLink[i].divisionid + "");
                                    // $scope.Division.push(95);
                                }

                            }
                            // alert(msg.data._OtherContract.ContractSignedByExecutiveid);

                            if (msg.data._OtherContract.ContractSignedByExecutiveid != null) {
                                setTimeout(function () {
                                    $scope.Executive = msg.data._OtherContract.ContractSignedByExecutiveid
                                }, 1800)


                            }
                            else {
                                $('#ddlexecutive').val($("#ddlexecutive option:first").val());
                            }

                            // $scope.Executive = 12,

                            $scope.Remarks = msg.data._OtherContract.Remarks,






                             $scope.Contractstatus = msg.data._OtherContractLintList.Contractstatus,




                            $scope.Signed_Contract_Sent_Date = (msg.data._OtherContractLintList.SignedContractSentDate == null ? null : ConvertDateDDMMYYFormat(msg.data._OtherContractLintList.SignedContractSentDate)),
                            $scope.Signed_Contract_received_Date = (msg.data._OtherContractLintList.SignedContractReceived_Date == null ? null : ConvertDateDDMMYYFormat(msg.data._OtherContractLintList.SignedContractReceived_Date)),

                            $scope.Cancellation_Date = (msg.data._OtherContractLintList.CancellationDate == null ? null : ConvertDateDDMMYYFormat(msg.data._OtherContractLintList.CancellationDate)),
                            $scope.Cancellation_Reason = msg.data._OtherContractLintList.Cancellation_Reason,

                            $scope.DateOfAgreement = (msg.data._OtherContractLintList.AgreementDate == null ? null : ConvertDateDDMMYYFormat(msg.data._OtherContractLintList.AgreementDate))

                            $scope.EffectiveDate = (msg.data._OtherContractLintList.Effectivedate == null ? null : ConvertDateDDMMYYFormat(msg.data._OtherContractLintList.Effectivedate))

                            $scope.ContractperiodUpload = msg.data._OtherContractLintList.Contractperiodinmonth

                            $scope.ExpiryDate = (msg.data._OtherContractLintList.Expirydate == null ? null : ConvertDateDDMMYYFormat(msg.data._OtherContractLintList.Expirydate))
                            $scope.PendingRemarks = msg.data._OtherContractLintList.Remarks


                            if (msg.data.ImageTypeOtherContact != null) {
                                var count = 0;
                                for (var i = 0 ; i < msg.data.ImageTypeOtherContact.length; i++) {

                                    if (count == 0) {
                                        $scope.CICost = msg.data.ImageTypeOtherContact[i].Cost
                                        if (msg.data.ImageTypeOtherContact[i].CurrencyId != null) {
                                            $scope.CICurrency = msg.data.ImageTypeOtherContact[i].CurrencyId
                                        }
                                        else {
                                            $scope.CICurrency = $scope.IndianCurrency;
                                        }


                                    }

                                    else if (count == 1) {
                                        $scope.IICost = msg.data.ImageTypeOtherContact[i].Cost
                                        if (msg.data.ImageTypeOtherContact[i].CurrencyId != null) {
                                            $scope.IICurrency = msg.data.ImageTypeOtherContact[i].CurrencyId
                                        }
                                        else {
                                            $scope.IICurrency = $scope.IndianCurrency;
                                        }


                                    }
                                    count++;
                                }
                            }




                            if (msg.data.VideoTypeOtherContact != null) {
                                var count = 0;
                                for (var i = 0 ; i < msg.data.VideoTypeOtherContact.length; i++) {

                                    if (count == 0) {
                                        if (msg.data.VideoTypeOtherContact[i].Cost != null) {
                                            $scope.VHCost = msg.data.VideoTypeOtherContact[i].Cost
                                        }

                                        if (msg.data.VideoTypeOtherContact[i].CurrencyId != null) {
                                            $scope.VHCurrency = msg.data.VideoTypeOtherContact[i].CurrencyId
                                        }
                                        else {

                                            $scope.VHCurrency = $scope.IndianCurrency;
                                        }


                                    }

                                    else if (count == 1) {
                                        $scope.vmCost = msg.data.VideoTypeOtherContact[i].Cost
                                        if (msg.data.VideoTypeOtherContact[i].CurrencyId != null) {
                                            $scope.VMCurrency = msg.data.VideoTypeOtherContact[i].CurrencyId
                                        }
                                        else {
                                            $scope.VMCurrency = $scope.IndianCurrency;
                                        }


                                    }

                                    else if (count == 2) {
                                        $scope.vlCost = msg.data.VideoTypeOtherContact[i].Cost
                                        if (msg.data.VideoTypeOtherContact[i].CurrencyId != null) {
                                            $scope.VLCurrency = msg.data.VideoTypeOtherContact[i].CurrencyId
                                        }
                                        else {
                                            $scope.VLCurrency = $scope.IndianCurrency;
                                        }


                                    }




                                    count++;
                                }
                            }

                            if (msg.data._OtherContract.Payment.toLowerCase() == 'yes') {
                                $scope.PaymentperiodReq = true
                                $scope.PaymentAmountReq = true
                            }
                            $scope.PaymentAmount = msg.data._OtherContract.PaymentAmount != null ? parseInt(msg.data._OtherContract.PaymentAmount) : '';


                            $('#btnSubmit').html("Update");
                            $('#hid_Req').val(0);

                            // $('#hid_Execid').val(msg.data.Id);

                            blockUI.stop();

                        }
                        else {
                            SweetAlert.swal("Error!", "Error in system. Please try again", "error");
                            blockUI.stop();
                        }

                    });
                }
            }




        }
    }

    var URL = window.location.href;

    if (URL.indexOf("id") >= 0) {

        var id = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');

        if (id != "" && id != "undefined") {
            var Author_id = id[0].split('=')[1]
            $('#hid_OtherContractEntryId').val(1)
            $scope.EditOtherContractData(Author_id);

            if (id[1].split('=')[1] == 'update') {
                $scope.PageHeadding = "Update";
            }
            else { $scope.PageHeadding = "View"; }

            $('.backToList').css("display", "block");
        }

    }
    else {
        $('.backToList').css("display", "none");
        $('#hid_OtherContractEntryId').val(0)
    }

    $scope.ReqContractSentDate = false;
    $scope.ReqContractReceivedDate = false;
    $scope.ReqCancellationDate = false;
    $scope.ReqCancellationReason = false;

    $scope.functionIssue = function () {
        //$scope.ReqContractSentDate = true;
        //$scope.ReqContractReceivedDate = true;
        //$scope.ReqCancellationDate = false;
        //$scope.ReqCancellationReason = false;
        //$scope.ReqUploadContract = true;

        var today = new Date();
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


        $scope.EffectiveDate = today;
        $scope.DateOfAgreement = today;

    }



    $scope.functionPending = function () {
        $scope.EffectiveDate = "";
        $scope.DateOfAgreement = "";
    }
    $scope.functionCancelled = function () {
        $scope.EffectiveDate = "";
        $scope.DateOfAgreement = "";

    }
    $scope.RemoveDocumentById = function (docid, file) {

        SweetAlert.swal({
            title: "Are you sure?",
            text: "You will not be able to recover this detail !",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#8CD4F5",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false,
            closeOnCancel: true
        },
          function (Confirm) {
              if (Confirm) {
                  //  alert($scope.NoticeBoard.NBId);
                  var AuthorDocument = { Id: docid, EnteredBy: $("#enterdBy").val() };
                  var DeleteDocument = AJService.PostDataToAPI("OtherContract/RemoveAuhtorDocument", AuthorDocument);

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
                                            $scope.EditOtherContractData($('#hid_OtherContractId').val());
                                        }
                                    });

                                  }

                              },
                              error: function (xhr, ajaxOptions, thrownError) {
                              }
                          });


                      }
                  }, function () {

                      SweetAlert.swal("Oops...", "Please retry!", "error");

                  });
              }

          });
    }




    $scope.RemoveDocumentLinkById = function (docid, file) {

        SweetAlert.swal({
            title: "Are you sure?",
            text: "You will not be able to recover this detail !",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#8CD4F5",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false,
            closeOnCancel: true
        },
         function (Confirm) {
             if (Confirm) {
                 //  alert($scope.NoticeBoard.NBId);
                 var AuthorDocument = { Id: docid, EnteredBy: $("#enterdBy").val() };
                 var DeleteDocument = AJService.PostDataToAPI("OtherContract/RemoveAuhtorDocumentLink", AuthorDocument);

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
                                            $scope.EditOtherContractData($('#hid_OtherContractId').val());
                                        }
                                    });

                                 }
                             },
                             error: function (xhr, ajaxOptions, thrownError) {
                             }
                         });


                     }
                 }, function () {

                     SweetAlert.swal("Oops...", "Please retry!", "error");

                 });
             }

         });
    }




    $scope.SetSigned_Contract_Sent_DateDate = function (datetext1) {

        //if ($scope.Signed_Contract_Sent_Date == "") {

        //    if ($scope.Contractstatus.toLowerCase() == "issued") {
        //        $scope.ReqContractSentDate = true;
        //        $scope.Signed_Contract_Sent_Date = $(datetext1).val();
        //    }
        //    else {
        //        $scope.ReqContractSentDate = false;
        //    }

        //}
        //else {
        //    $scope.ReqContractSentDate = false;
        //}
        $scope.Signed_Contract_Sent_Date = $(datetext1).val();
    }

    $scope.SetSigned_Received_Sent_Date = function (datetext) {
        //if ($scope.Signed_Contract_received_Date == "") {

        //    if ($scope.Contractstatus.toLowerCase() == "issued") {
        //        $scope.ReqContractReceivedDate = true;
        //        $scope.Signed_Contract_received_Date = $(datetext).val();
        //    }
        //    else {
        //        $scope.ReqContractReceivedDate = false;
        //    }

        //}
        //else {
        //    $scope.ReqContractReceivedDate = false;
        //}
        $scope.Signed_Contract_received_Date = $(datetext).val();
    }



    $scope.SetCancellation_Date = function (datetext3) {

        //if ($scope.Cancellation_Date == "") {
        //    if ($scope.Contractstatus.toLowerCase() == "cancelled") {
        //        $scope.ReqCancellationDate = true;
        //        $scope.Cancellation_Date = $(datetext3).val();
        //    }
        //    else {
        //        $scope.ReqCancellationDate = false;
        //    }

        //}
        //else {
        //    $scope.ReqCancellationDate = false;
        //}

        $scope.Cancellation_Date = $(datetext3).val();
    }

    $scope.SetExpiryDate = function (datetext4) {
        $scope.ExpiryDate = $(datetext4).val();
    }

    $scope.ContractDateValue = function (datetext) {

        $scope.ContractDate = $(datetext).val();
        PeriodIdValue = $scope.AgreementPeriod;
        var CDate = $("#ContractDate").val();


        if (PeriodIdValue == undefined || PeriodIdValue == "0" || PeriodIdValue == "" || CDate == undefined || CDate == "" || CDate == "undefined//undefined") {
            $scope.ExpiryDate = "";
            return false;
        }


        var RequestDate = $("#ContractDate").val();

        var date = RequestDate;
        var d = new Date(date.split("/").reverse().join("-"));
        var dd = d.getDate();
        var mm = d.getMonth() + 1;
        var yy = d.getFullYear();
        var newdate = yy + "/" + mm + "/" + dd;

        if (PeriodIdValue == undefined || CurrentDate == "") {
            $scope.ExpiryDate = "";
            return false;
        }

        var CurrentDate = new Date(newdate);

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
        $("#ExpiryDate").val(today)
    }



    $scope.DateOfAgreementValue = function (datetext) {


        $scope.DateOfAgreement = $(datetext).val();

        $("#DateOfAgreementValue").val($(datetext).val());

        $scope.EffectiveDate = $(datetext).val();

        $('#EffectiveDate').val($(datetext).val());

        //if ($scope.ContractperiodUpload > 0) {
        //    $scope.CalculateExpiry();
        //}


        // $scope.checkdate(datetext);

    }



    $scope.CalculateExpiry = function () {


        //PeriodIdValue = $scope.ContractperiodUpload; //$scope.userForm.ContractperiodUpload.$modelValue;
        var CDate = $("#DateOfAgreementValue").val();
        //if (PeriodIdValue == undefined || PeriodIdValue == "0" || PeriodIdValue == "" || CDate == undefined || CDate == "" || CDate == "undefined//undefined") {
        //    $scope.ExpiryDate = "";
        //    return false;
        //}


        var RequestDate = $("#DateOfAgreementValue").val();

        var date = RequestDate;
        var d;
        if (navigator.userAgent.search("Safari") >= 0 && navigator.userAgent.search("Chrome") < 0) {
            d = new Date(parseInt(date.split("/")[2]), parseInt(date.split("/")[1] - 1), parseInt(date.split("/")[0]));
        }
        else {
            d = new Date(date.split("/").reverse().join("-"));
        }

        var dd = d.getDate();
        var mm = d.getMonth() + 1;
        var yy = d.getFullYear();
        var newdate = yy + "/" + mm + "/" + dd;

        var CurrentDate = new Date(newdate);
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
        $("[name$=ExpiryDate]").val(today);
    }


    $scope.GetAllPaymentPeriodList = function () {
        var getPaymentPeriodList = AJService.GetDataFromAPI("CommonList/getAllPaymentPeriodList", null);
        getPaymentPeriodList.then(function (PaymentPeriod) {
            $scope.PaymentPeriodeList = PaymentPeriod.data.query;
        }, function () {
            //alert('Error in getting Payment Period list');
        });
    }


    //Get Currency List //added on 18 Nov, 2017
    $scope.getCurrencyMasterList = function () {
        var getCurrencyMasterList = AJService.GetDataFromAPI("CommonList/getCurrencyList", null);
        getCurrencyMasterList.then(function (CurrencyMaster) {
            $scope.CurrencyMasterList = CurrencyMaster.data;
        }, function () {
            //alert('Error in getting Currency List');
        });
    }

});
