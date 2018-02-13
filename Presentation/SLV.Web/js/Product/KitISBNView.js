app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {
    
    var kitId = $('#KitId').val();
    if (kitId != undefined && kitId != null && kitId != "" && kitId != "0") {

        var mstr_ISBN = kitId == undefined ? "0" : kitId;

        var GetKitISBN = AJService.GetDataFromAPI("ProductMaster/GetKitIsbnDataById?Id=" + mstr_ISBN);
        GetKitISBN.then(function (msg) {
            if (msg.data.length == 0) {
                swal("No record", 'No record found', "warning");
                blockUI.stop();
            }
            else {
                $scope.KitIsbnDetails = msg.data;

                //convert ISBN from 13 digit to 10 digit
                setTimeout(function () {
                    var KitISBNConvert = AJService.GetDataFromAPI("ProductMaster/GetIsbnConvert?isbn13=" + $scope.KitIsbnDetails[0].ISBN);
                    KitISBNConvert.then(function (isbn) {
                        $scope.IsbnConverted = isbn.data;
                    });
                }, 100);

            }
        });
        return;      

    }

   


});