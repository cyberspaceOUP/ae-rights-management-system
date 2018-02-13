app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {
    app.expandControllerTopSearch($scope, AJService, $window);
    $scope.getAllProductTypeList = function () {

        var ProductTypeList = AJService.PostDataToAPI("CommonList/AllProductTypeList", null);
        ProductTypeList.then(function (ProductTypeList) {
            $scope.ProductTypeList = ProductTypeList.data;
        }, function () {
            //alert('Error in getting Product Type List');
        });
    }


    $scope.UploadFileReq = false;
    $scope.clear = function () {

        $("input:radio").attr("checked", false);
        $("input[name='ProductType']:checked").val("");
        $('.uploadedFileName').hide();
        $('.fileNameClass').val("");
        $('.cstmProgressBar').hide();
        $('#fileid1').hide();
        setTimeout(function () { window.location.href = location.href; }, 1000)


    }

    $scope.uploadISBN = function () {

        var ISBNupload = {
            FileName: $("#hid_Uploads1").val().slice(1, -1),
            ProductType: $("input[name='ProductType']:checked").val(),
            DocumentName: $('.fileNameClass').val(),
            EnteredBy: $("#enterdBy").val(),
            Path: $("#hid_Path").val(),
        };


        blockUI.start();
        // call API to fetch temp Department list basis on the FlatId
        var DivisionStatus = AJService.PostDataToAPI('ISBNMaster/UploadISBN', ISBNupload);
        DivisionStatus.then(function (msg) {
           if (msg != null) {
                    if (msg.data.status == "OK") {

                        SweetAlert.swal({
                            title: "Success",
                            text: "Isbn uploaded successfully.",
                            type: "success"
                        },
                      function () {
                          location.href = window.location.href;
                      });
                    }
                    else if (msg.data.status == "OK,NOK")
                    {
                        SweetAlert.swal({
                            title: "Success",
                            text: "Valid isbn have been uploaded successfully. Press OK to download csv of invalid isbn",
                            type: "warning"
                        },
                     function () {
                         if (msg.data.InvalidIsbn.length > 0) {
                             JSONToCSVConvertor(msg.data.InvalidIsbn);
                             location.href = window.location.href;
                         }

                     });

                    }
                    else
                    {
                        SweetAlert.swal({
                            title: "Error",
                            text: "No Isbn have uploaded.Press OK to download csv.",
                            type: "warning"
                        },
                        
                        function () {
                            blockUI.stop();
                            if (msg.data.InvalidIsbn.length > 0) {
                                JSONToCSVConvertor(msg.data.InvalidIsbn);
                                location.href = window.location.href;
                            }
                                //location.href = window.location.href;
                           
                        });
                        
                       
                    }
                }
         
        });

    };
    

    function JSONToCSVConvertor(JSONData) {
        //If JSONData is not an object then JSON.parse will parse the JSON string in an Object
        var arrData = typeof JSONData != 'object' ? JSON.parse(JSONData) : JSONData;
        var ReportTitle = ""
        var CSV = '';
        //Set Report title in first row or line

        CSV += ReportTitle + '\r\n\n';
        ShowLabel = true;

        //This condition will generate the Label/Header
        if (ShowLabel) {
            var row = "";

            //This loop will extract the label from 1st index of on array
            for (var index in arrData[0]) {

                //Now convert each value to string and comma-seprated
                row += index + ',';
            }

            row = row.slice(0, -1);

            //append Label row with line break
            CSV += row + '\r\n';
        }

        //1st loop is to extract each row
        for (var i = 0; i < arrData.length; i++) {
            var row = "";

            //2nd loop will extract each column and convert it in string comma-seprated
            for (var index in arrData[i]) {
                row += '"' + arrData[i][index] + '",';
            }

            row.slice(0, row.length - 1);

            //add a line break after each row
            CSV += row + '\r\n';
        }

        if (CSV == '') {
            alert("Invalid data");
            return;
        }

        //Generate a file name
        var fileName = "InvalidIsbn";
        //this will remove the blank-spaces from the title and replace it with an underscore
       

        //Initialize file format you want csv or xls
        var uri = 'data:text/csv;charset=utf-8,' + escape(CSV);

        // Now the little tricky part.
        // you can use either>> window.open(uri);
        // but this will not work in some browsers
        // or you will not get the correct file extension    

        //this trick will generate a temp <a /> tag
        var link = document.createElement("a");
        link.href = uri;

        //set the visibility hidden so it will not effect on your web-layout
        link.style = "visibility:hidden";
        link.download = fileName + ".csv";

        //this part will append the anchor tag and remove it after automatic click
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    }

    $scope.submitForm = function () {
         var flag = 0;
        $scope.msg = "";

        FileNameArray = $('#dropZone0').find('.fileNameClass');

        if (FileNameArray[0] == null) {

            $scope.UploadExcelfileReq = true;

            $scope.userForm.$valid = false;
            return false;
        }
        else {
            $scope.userForm.$valid = true;

        }

        $scope.submitted = true;

        if ($('[name*=ProductType]:checked').length == 0) {

            $scope.userForm.$valid = false;
            SweetAlert.swal("Validation !", "Please select product type", "warning");
            return false;
        }
        else {
            $scope.userForm.$valid = true;
        }


        if ($scope.userForm.$valid) {
            //$scope.PasswordChange();();
            $scope.uploadISBN();
            //to reset the controls after submit
            $scope.userForm.$setPristine();
            $scope.submitted = false;
        }





    };
});
app.directive('validFile', function () {
    return {
        require: 'ngModel',
        link: function (scope, el, attrs, ngModel) {
            //change event is fired when file is selected
            el.bind('change', function () {
                scope.$apply(function () {
                    ngModel.$setViewValue(el.val());
                    ngModel.$render();
                });
            });
        }
    }
});