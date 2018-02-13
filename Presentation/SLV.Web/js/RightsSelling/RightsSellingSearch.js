
app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {
    app.expandControllerTopSearch($scope, AJService, $window);
    app.expandControllerA($scope, AJService, $window);
    $scope.ShowSearchForm = true;
    $scope.ShowListForm = false;
    $("#RequestToDate").attr("disabled", true);
    $("#ContractTodate").attr("disabled", true);



    $scope.SetRequestFromDate = function (RequestFromDate) {
        $scope.RequestFromDate = $(RequestFromDate).val();
    }

    $scope.SetRequestToDate = function (RequestToDate) {
        $scope.RequestToDate = $(RequestToDate).val();
    }

    $scope.SetContractFromDate = function (ContractFromDate) {
        $scope.ContractFromDate = $(ContractFromDate).val();
    }

    $scope.SetContractTodate = function (ContractTodate) {
        $scope.ContractTodate = $(ContractTodate).val();
    }

    $scope.SetDateofexpiry = function (Dateofexpiry) {
        $scope.Dateofexpiry = $(Dateofexpiry).val();
    }


    $scope.getListBack = function ()
    {
        if ($("#hid_BackToList").val() != "") {

            $scope.RightsSellingListResult();
        }
    }

    $scope.RightsSellingListResult = function () {
        var RightsSellingList = AJService.GetDataFromAPI("RightsSelling/GetRightsSellingSearchList?SessionId=" + $("#hid_sessionId").val() + "", null);
        RightsSellingList.then(function (msg) {
            if (msg.data.length != 0) {
                $scope.RightsSellingList = msg.data;
                $scope.ShowSearchForm = false;
                $scope.ShowListForm = true;
            }
            else {
                //swal("No record", 'No record found', "warning");
                SweetAlert.swal({
                    title: "No record",
                    text: "No record found",
                    type: "warning"
                },
               function () {
                   var data = $("#hid_for").val();
                   document.location = GlobalredirectPath + "/RightsSelling/RightsSelling/RightsSellingSearch?For=" + data;
               });
            }
        });
    }



    $scope.getTerritoryRightsList = function () {
        var TerritoryRightsList = AJService.GetDataFromAPI("AuthorContact/getTerriteryRights", null);
        TerritoryRightsList.then(function (TerritoryRightsList) {
            $scope.TerritoryList = TerritoryRightsList.data.query;
        }, function () {
            //alert('Error in getting Territery Rights List');
        });
    }

    $scope.BackToserch = function () {

        //var url = window.location.href;
        //var q_string = "BackToList";

        //$('#hid_BackToList').val("");

        //if (url.indexOf(q_string) != -1)
        //    window.location.href = "RightsSellingSearch";
        //else
        //    window.location.href = window.location.href;

        var mstr_history = document.referrer;

        if (mstr_history.indexOf("RightsSellingUpdate") > 0) {
            window.location.href = "RightsSellingSearch?For=View";
        }
        else if (mstr_history.indexOf("RightsSellingView") > 0) {
            window.location.href = "RightsSellingSearch?For=Update";

        } else {
            window.location.href = window.location.href;
        }
    }


    function convertDateDDMMYYYY(inputFormat) {
        function pad(s) { return (s < 10) ? '0' + s : s; }
        var d = new Date(inputFormat);
        return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
    }

    $scope.RightSalesSearchForm = function () {
        $scope.submitted = true;
        $scope.userForm.$valid = true;
        if ($scope.userForm.$valid) {
            $scope.RightsSellingSearch();
            // set form default state
            $scope.userForm.$setPristine();
            // set form is no submitted
            $scope.submitted = false;
            return;
        }
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

    function convertDate(date) {
        if (date != undefined) {
            var datearray = date.split("/");
            return datearray[1] + '/' + datearray[0] + '/' + datearray[2];
        }
        else {
            return null;
        }

    }

    $scope.RightsSellingSearch = function ()
    {
        
        var ContractStatus = "";
        $('input[type=checkbox][name=ContractStatus]:visible:checked').each(function (index, value) {
            ContractStatus = ContractStatus + $(this).val() + ",";
        });
        ContractStatus = ContractStatus.substring(0, ContractStatus.length - 1);
        if ($scope.WorkingProduct != "" && $scope.WorkingProduct != undefined && $scope.WorkingProduct != null) {
            if ($scope.WorkingProduct.indexOf("'") != -1) {
                $scope.WorkingProduct = $scope.WorkingProduct.replace("'", "''");
            }
        }

        if ($scope.SUbWorkingProduct != "" && $scope.SUbWorkingProduct != undefined && $scope.SUbWorkingProduct != null) {
            if ($scope.SUbWorkingProduct.indexOf("'") != -1) {
                $scope.SUbWorkingProduct = $scope.SUbWorkingProduct.replace("'", "''");
            }
        }


        var _mobjRightsSellingSearch = {
          
            RequestFromDate: $('#RequestFromDate').val() == "" ? null : convertDate($('#RequestFromDate').val()),
            RequestToDate: $('#RequestToDate').val() == "" ? null : convertDate($('#RequestToDate').val()),
            Licensee:$scope.Licensee,
            ContractFromDate: $('#ContractFromDate').val() == "" ? null : convertDate($('#ContractFromDate').val()),
            ContractToDate: $('#ContractTodate').val() == "" ? null : convertDate($('#ContractTodate').val()),
            Dateofexpiry_opr: $('#Dateofexpiry_opr').val() == "" ? null : $('#Dateofexpiry_opr').val(),
            Dateofexpiry: $('#Dateofexpiry').val() == "" ? null : $('#Dateofexpiry').val(),
            ProductCategory:$scope.ProductCategory,
            LanguageId:$scope.Language,
            Payment_Term:$scope.PaymentTerm,
            Payment_Amount:$scope.PaymentAmount,
            AuthorName:$scope.Author,
            Territory:$scope.TerritoryRight,
            Remarks: $scope.Remarks,
            EntryDate: new Date(),
            SessionId: $("#hid_sessionId").val() != "" ? $("#hid_sessionId").val() : null,
            EnteredBy: $("#enterdBy").val(),
            ContractStatus: ContractStatus,
            ISBN: $scope.ISBN,
            WorkingProduct: $scope.WorkingProduct,
            SUbWorkingProduct: $scope.SUbWorkingProduct,
            ProductCode: $scope.ProductCode,
            RightsSalesCode: $scope.RightsSalesCode
        }

        var AuthorStatus = AJService.PostDataToAPI('RightsSelling/InsertRightsSellingHistory', _mobjRightsSellingSearch);

            AuthorStatus.then(function (msg) {
                //blockUI.stop();
                if (msg.data == "OK") {
                    $scope.RightsSellingListResult();
                }

            }, function () {
                $scope.IsError = false;
            });
       
    }



    $scope.DisplayDiv = function (count) {
        $('.tooltip-toggle').css("display", "none");
        $('.tooltip-toggle').removeAttr("id");
        $($('.tooltip-toggle')[count - 1]).attr("id", "tooltip-toggle");
    }

    $scope.ExcelReport = function () {
        document.location = GlobalredirectPath + "RightsSelling/RightsSelling/exportToExcelRightsSellingList?SessionId=" + $("#hid_sessionId").val() + "";
    }

    $scope.RightsSellingReportExcel = function () {
        $scope.ExcelReport();
    }

    //For Delete Rights Selling  // Added by Prakash on 05 May, 2017
    $scope.DeleteRightsSelling = function (rsId, role) {
        var mobj_delete = {
            Id: rsId == undefined ? 0 : rsId,
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
                blockUI.start();

                var Delete = AJService.PostDataToAPI('RightsSelling/DeleteRightsSellingSet', mobj_delete);
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
                                blockUI.stop();
                                $scope.RightsSellingListResult();
                            }
                        });

                    }
                });


            }

        });

    }


});


