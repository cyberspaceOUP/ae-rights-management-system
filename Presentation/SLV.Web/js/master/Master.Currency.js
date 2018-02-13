app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {

    app.expandControllerA($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);
    /*
     Here is the service used for inserting the Asset Status List.
     Bind data In the object and send to api for inserting and update.
     Common function has used for both updation and Insertion
    */

    $scope.AddCurrencyMaster = function () {
        blockUI.start();
        var obj_data = {
            CurrencyName: $scope.Currencyname,
            Symbol: $scope.Symbolname,
            Id: $('#hid_recordid').val() == "" ? 0 : $('#hid_recordid').val(),
            EnteredBy: $("#enterdBy").val()
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
                    var Status = AJService.PostDataToAPI('Master/InsertCurrencyMaster', obj_data);
                    Status.then(function (msg) {
                        if (msg.data != "OK") {
                            SweetAlert.swal("Error!", "Duplicate. already exist !", "", "error");
                        }
                        else {
                            if ($('#hid_recordid').val() != "") {
                                SweetAlert.swal('Updated successfully.', '', "success");

                            }
                            else {
                                SweetAlert.swal('Insert successfully.', '', "success");
                            }
                            {
                                $scope.Currencyname = "";
                                $scope.Symbolname = "";
                                $('#btnSubmit').html("Submit");
                                $scope.update_city == "False";
                                $('#hid_recordid').val("");
                                angular.element(document.getElementById('angularid')).scope().getCurrencyMasterList();
                            }
                        }

                    },

                    function () {
                        alert('Please validate details');
                    });
                }

            });

        blockUI.stop();
    }

    /*Populating the CurrencyMaster List based on the Record id of Primary Key*/
    //Edit Currency Master basis on The recordID
    $scope.EditCurrencyMasterData = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            blockUI.start();
            // call API to fetch Currency Master list basis on the FlatId
            var Language = AJService.GetDataFromAPI('Master/getCurrencyMasterDetailsById?Id=' + Id);
            Language.then(function (msg) {
                if (msg != null) {
                    $scope.Currencyname = msg.data.CurrencyName;
                    $scope.Symbolname = msg.data.SymbolName;
                    $('#btnSubmit').html("Update");
                    $('#hid_recordid').val(msg.data.Id);
                    blockUI.stop();
                }
                else {
                    SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                    blockUI.stop();
                }

            });
        }
    }

    //Delete Currency Master basis on The recordID
    $scope.DeleteCurrencyMasterData = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var obj_data = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this detail!",
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
                     // call API to fetch Currency Master list basis on the FlatId
                     var LanguageStatus = AJService.PostDataToAPI("Master/DeleteCurrencyMaster", obj_data);
                     LanguageStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             blockUI.stop();
                         }
                         {
                             angular.element(document.getElementById('angularid')).scope().getCurrencyMasterList();
                         }
                     });
                 }

             });
        }
        else {
            SweetAlert.swal("Error!", "Record is not Deleted", "", "error");
            blockUI.stop();
        }
        blockUI.stop();
    }
    
    $scope.Reset = function (userForm) {
        $scope.Currencyname = null;
        $scope.Symbolname = null;
        $scope.userForm.$setPristine();
        $scope.submitted = false;
    }

    $scope.cancelForm = function (userForm) {
        angular.element(document.getElementById('angularid')).scope().getCurrencyMasterList();
        $scope.submitted = false;
    }

    // function call on page load to set values
    $scope.LoadStaffPage = function (Id) {
        if (Id != null) {

            $('#btncancel').show();
            $('#btnReset').hide();
        }
        else {
            $('#btncancel').hide();
            $('#btnReset').show();
        }
    }

    $scope.getCurrencyMasterList = function () {
        var getresult = AJService.GetDataFromAPI("Master/getCurrencyMaster", null);
        getresult.then(function (msg) {
            $scope.currencyMasterList = msg.data;
        }, function () {
            //------
        });
    }

    //To call PostVisitorEntry() function if form is valid (all validation is true)
    $scope.submitForm = function () {
        $scope.submitted = true;

        if ($scope.userForm.$valid) {
            $scope.AddCurrencyMaster();
            // set form default state
            $scope.userForm.$setPristine();
            // set form is no submitted
            $scope.submitted = false;
            return;
        }
    };


});