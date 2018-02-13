app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {
    app.expandControllerTopSearch($scope, AJService, $window);
    app.expandControllerA($scope, AJService, $window);

    /*
  
 Here is the service used for inserting the Geographical List.
 Bind data In the object and send to api for inserting and update.
 Common function has used for both updation and Insertion

 */


    $scope.AddImprintData = function () {
        blockUI.start();
        var Imprint = {
            ImprintName: $scope.name,
            Id: $('#hid_recordid').val() == "" ? 0 : $('#hid_recordid').val(),
            EnteredBy: $("#enterdBy").val(),
            PublishingCompanyId: $scope.publishingCompany
        };

        var ImprintStatus = AJService.PostDataToAPI('ImprintMaster/InsertImprint', Imprint);
        ImprintStatus.then(function (msg) {
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

                $scope.name = "";
                $scope.publishingCompany = $scope.publishingCompany[0];
                $('#btnSubmit').html("Submit");
                $scope.update_city == "False";
                $('#hid_recordid').val("");
                angular.element(document.getElementById('angularid')).scope().GetPublishingCompanyImprintList();

            }

        },

        function () {
            alert('Please validate details');
        });
        blockUI.stop();
    };


    $scope.DeleteImprintData = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var Imprint = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this Imprint detail! ",
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
                     // call API to fetch temp Department list basis on the FlatId
                     var ImprintStatus = AJService.PostDataToAPI("ImprintMaster/DeleteImprint", Imprint);
                     ImprintStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             blockUI.stop();
                         }
                         {
                             angular.element(document.getElementById('angularid')).scope().GetPublishingCompanyImprintList();

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


    /*
Populating the department List based on the Record id of Primary Key
*/
    //Edit Division basis on The recordID
    $scope.EditImprintData = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            blockUI.start();
            // call API to fetch temp Department list basis on the FlatId
            var Imprint = AJService.GetDataFromAPI('ImprintMaster/getImprint?Id=' + Id);
            Imprint.then(function (msg) {
                if (msg != null) {
                    $scope.name = msg.data._imprintMaster.ImprintName;
                    $scope.publishingCompany = msg.data._imprintMaster.PublishingCompanyId;
                    $('#btnSubmit').html("Update");
                    $('#hid_recordid').val(msg.data._imprintMaster.Id);
                    blockUI.stop();
                }
                else {
                    SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                    blockUI.stop();
                }

            });
        }
    }




    $scope.Reset = function (userForm) {
        $scope.name = null;
        $scope.country = $scope.country[0];
        $scope.userForm.$setPristine();
        $scope.submitted = false;
    }

    $scope.cancelForm = function (userForm) {
        angular.element(document.getElementById('angularid')).scope().GetPublishingCompanyImprintList();
        $scope.submitted = false;
    }

    // function call on page load to set values
    $scope.LoadStaffPage = function (Id) {
        if (Id != null) {

            $('#btncancel').show();
            $('#btnReset').hide();
            //$('#Sign').addClass('fa fa-check');
        }
        else {
            $('#btncancel').hide();
            $('#btnReset').show();
        }
    }

    ////Commented By Ankush For Linked With common.master.js

    //$scope.GetPublishingCompanyList = function () {
    //    var GetPublishingCompanyList = AJService.GetDataFromAPI("PublishingCompanyMaster/GetAllPublishingCompany", null);
    //    GetPublishingCompanyList.then(function (PublishingCompany) {
    //        $scope.PublishingCompanyList = PublishingCompany.data;
    //    }, function () {
    //        alert('Error in getting Publishing Company list');
    //    });
    //};

    $scope.GetPublishingCompanyImprintList = function () {
        var PublishingCompanyImprintList = AJService.GetDataFromAPI("ImprintMaster/GetPublishingCompanyImprintList", null);
        PublishingCompanyImprintList.then(function (Imprint) {
            $scope.PublishingCompanyImprintList = Imprint.data;
        }, function () {
            alert('Error in getting Imprint list');
        });
    };

    //To call PostVisitorEntry() function if form is valid (all validation is true

    $scope.submitForm = function () {
        $scope.submitted = true;
        if ($scope.userForm.$valid) {
            if ($scope.update_city == "True") {

                //$scope.UpdateCity();
                // set form default state
                $scope.userForm.$setPristine();

                // set form is no submitted
                $scope.submitted = false;

                return;
            }

            else if ($scope.userForm.$valid) {
                $scope.AddImprintData();
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    };

});