app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {
    app.expandControllerTopSearch($scope, AJService, $window);
    
    //Upload File Masters
    $scope.uploadDocumentForm = function () {
        $scope.submitted = true;

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
                $scope.userForm.$valid = true;
            }
        }

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
                        $scope.userForm.$valid = true;
                    }
                }
            });
        }

        var marr_fileDetails = [];
        var FileNameArray = $('#dropZone0').find('.fileNameClass');
        var UploadFileNameArray = $("#hid_Uploads").val().split(",");

        FileNameArray.each(function (i) {
            var mobj_fileDetails = {
                FileName: $(this).val(),
                UploadFileName: UploadFileNameArray[i],
            }
            marr_fileDetails.push(mobj_fileDetails);
        });


        var _mobj = {
            MasterName: $scope.MasterName,
            MasterId: $('[name*=MasterId] option:selected').val(),
            FileDetails: marr_fileDetails,
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
                var ProductStatus = AJService.PostDataToAPI('Master/UploadDocumentCommon', _mobj);
                ProductStatus.then(function (msg) {
                    if (msg.data != "OK") {
                        SweetAlert.swal("Try agian", 'There is some problem.', 'error');
                    }
                    else {
                        SweetAlert.swal({
                            title: "Insert successfully.",
                            text: "",
                            type: "success"
                        },
                        function () {
                            window.location.reload();
                        });
                    }
                },
            function () {
                SweetAlert.swal("Try agian", 'There is some problem.', 'error');
            });
            }
        });


    }

    //Licensee List
    var List = [];
    $scope.getMasterIdList = function () {
        if ($scope.MasterName == "Licensee") {
            var getLicenseeList = AJService.GetDataFromAPI("RightsSelling/getLicenseeListNew", null);
            getLicenseeList.then(function (msg) {
                $scope.List = msg.data;
            }, function () {
                //alert('Error in getting Language List');
            });
        }
    }


});