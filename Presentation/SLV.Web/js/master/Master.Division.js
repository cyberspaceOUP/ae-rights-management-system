
app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {
    app.expandControllerTopSearch($scope, AJService, $window);
    /*
    
   Here is the service used for inserting the Division and subdivision List.
   Bind data In the object and send to api for inserting and update.
   Common function has used for both updation and Insertion
   
   */

    app.expandControllerA($scope, AJService, $window);
    $scope.AddDivision = function () {
        blockUI.start();
        var subDivision = {
            parentdivisionid: $scope.Division,
            divisionName: $scope.subdivision,
            Id: $('#hid_recordid').val() == "" ? 0 : $('#hid_recordid').val(),
            EnteredBy: $("#enterdBy").val()
        };

        //var DivisionStatus = AJService.PostDataToAPI('Master/InsertDivision', subDivision);
        var DivisionStatus = AJService.PostDataToAPI('DivisionMaster/InsertDivision', subDivision);
        DivisionStatus.then(function (msg) {
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
                    $scope.Division = "";
                    $scope.subdivision = "";

                    $('#btnSubmit').html("Submit");
                    $('#hid_recordid').val("");

                    angular.element(document.getElementById('angularid')).scope().getDivisionListMst();
                    angular.element(document.getElementById('angularid')).scope().getSubDivisionListMst();
                }

            }
            {
                $scope.name = "";
            }
        },


        function () {
            alert('Please validate details');
        });
        blockUI.stop();
    }

    /*
    
   From here common service has called for division and subdivision master.
   Before changing anything in master please check both.
   
   */
    // Delete Department  details on basis of ID
    $scope.DeleteDivision = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var DivisionData = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this Division detail! ",
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
                     var DivisionStatus = AJService.PostDataToAPI('DivisionMaster/DivisionDelete', DivisionData);
                     DivisionStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                             //blockUI.stop();
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             blockUI.stop();
                         }
                         {
                             angular.element(document.getElementById('angularid')).scope().getDivisionListMst();
                             angular.element(document.getElementById('angularid')).scope().getSubDivisionListMst();
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

    // Delete Sub Department  details on basis of ID
    $scope.DeleteSubDivision = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var DivisionData = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this Sub-Division detail! ",
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
                     var DivisionStatus = AJService.PostDataToAPI('DivisionMaster/DivisionDelete', DivisionData);
                     DivisionStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                             //blockUI.stop();
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             blockUI.stop();
                         }
                         {
                             angular.element(document.getElementById('angularid')).scope().getDivisionListMst();
                             angular.element(document.getElementById('angularid')).scope().getSubDivisionListMst();
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
   
  Form here the common service has called for populating the data on the basis of recordId for
  Division and sub division
  
  */


    //Edit Division basis on The recordID
    $scope.EditSubDivision = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var DivisionData = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            blockUI.start();
            // call API to fetch temp Department list basis on the FlatId
            var DivisionStatus = AJService.PostDataToAPI('DivisionMaster/SubDivision', DivisionData);
            DivisionStatus.then(function (msg) {
                if (msg != null) {
                    $scope.Division = msg.data.parentdivisionid,
                    $scope.subdivision = msg.data.divisionName
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

    /*

        Here all the formalities performed for submitting the form.

*/

    // Get Division List
    $scope.getDivisionListMst = function () {
        var getDivisionList = AJService.GetDataFromAPI("DivisionMaster/getDivisionList", null);
        getDivisionList.then(function (Division) {
            $scope.DivisionList = Division.data;
        }, function () {
            //alert('Error in getting Division list');
        });
    }

    $scope.getSubDivisionListMst = function () {
        var getSubDivisionList = AJService.GetDataFromAPI("DivisionMaster/getSubDivisionList", null);
        getSubDivisionList.then(function (Division) {
            $scope.SubDivisionList = Division.data;
        }, function () {
            alert('Error in getting SubDivision list');
        });
    }

    $scope.submitForm = function () {
        $scope.submitted = true;
        if ($scope.subDivisionForm.$valid) {
            if ($scope.update_city == "True") {

                $scope.subDivisionForm.$setPristine();
                $scope.submitted = false;

                return;
            }

            else if ($scope.subDivisionForm.$valid) {
                $scope.AddDivision();
                // set form default state
                $scope.subDivisionForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    };
});
