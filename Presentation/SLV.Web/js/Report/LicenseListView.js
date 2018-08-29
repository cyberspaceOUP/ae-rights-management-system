app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {

    /*Expand common.master.js Controller*/
    //app.expandControllerA($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);

    //---------Product License Expired on 25/10/2016
    $scope.GetProductLicenseExpired = function () {

        var _Flag = {

            Flag: "List",
            ExecutiveId: parseInt($("#enterdBy").val())
        };

        var ExecutiveStatus = AJService.PostDataToAPI('Report/GetProductLicenseExpired', _Flag);
        ExecutiveStatus.then(function (msg) {
            if (msg.data.length != 0) {
                $scope.ProductLicenseExpiredList = msg.data;
            }
            else {
                SweetAlert.swal("No record", 'No record found', "warning");
            }
        });        

    }

    //Export Excel File
    $scope.InvoiceReportReportExcel = function () {

        var _Flag = {

            Flag: "List"

        };

        document.location = GlobalredirectPath + "Report/Report/exportToExcelLicenseList?Flag=List";
    }


});


