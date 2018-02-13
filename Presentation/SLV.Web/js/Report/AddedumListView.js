app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {

    /*Expand common.master.js Controller*/
    //app.expandControllerA($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);

    //---------Product License Addendum Expired on 25/10/2016
    $scope.GetProductLicenseAddendumExpired = function () {
    
        var _Flag = {

            Flag: "List"

        };

        var ExecutiveStatus = AJService.PostDataToAPI('Report/GetProductLicenseAddendumExpired', _Flag);
        ExecutiveStatus.then(function (msg) {
            if (msg.data.length != 0) {
                $scope.ProductLicenseAddendumExpiredList = msg.data;
            }
            else {
                SweetAlert.swal("No record", 'No record found', "warning");
            }
        });

    }


    //Export Excel File
    $scope.InvoiceReportReportExcel = function () {
        
        document.location = GlobalredirectPath + "Report/Report/exportToExcelAddendumList?Flag=List";
    }


});


