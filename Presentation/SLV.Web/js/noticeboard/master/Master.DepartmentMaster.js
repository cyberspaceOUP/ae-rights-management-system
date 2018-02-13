
app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {
    /*
     

    Here is the service used for inserting the department List.
    Bind data In the object and send to api for inserting and update.
    Common function has used for both updation and Insertion
    
    */
    app.expandControllerTopSearch($scope, AJService, $window);
    app.expandControllerA($scope, AJService, $window);
       $scope.AddDepartment = function () {
        blockUI.start();

        var Department = {
            DepartmentName: $scope.name,
            Id: $('#hid_recordid').val() == "" ? 0 : $('#hid_recordid').val(),
            EnteredBy: $("#enterdBy").val()
        };

        var DepartmentStatus = AJService.PostDataToAPI('Master/insertDepartment', Department);
        DepartmentStatus.then(function (msg) {

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
                    $scope.name = "";
                    angular.element(document.getElementById('angularid')).scope().GetDepartmentList();
                }
            }
           
        },


        function () {
            alert('Please validate details');
        });
        blockUI.stop();
    }

    /*
    Populating the department List based on the Record id of Primary Key
    */
    //Edit Division basis on The recordID
       $scope.EditDepartmentData = function (Id) {
           // check that Id have value or not     
           if (Id != null) {
               // initialize variable for fetching data 
               var Department = {
                   Id: Id,
                   EnteredBy: $("#enterdBy").val()
               };
               blockUI.start();
               // call API to fetch temp Department list basis on the FlatId
               var Department = AJService.PostDataToAPI('Master/Department', Department);
               Department.then(function (msg) {
                   if (msg != null) {
                       $scope.name = msg.data.DepartmentName
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

    
    $scope.DeleteDepartment = function (Id) {
       // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var Department = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this Department detail! ",
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
                     var DepartmentStatus = AJService.PostDataToAPI("Master/DepartmentDelete", Department);
                     DepartmentStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             blockUI.stop();
                         }
                          {                        
                              angular.element(document.getElementById('angularid')).scope().GetDepartmentList();
                             
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
    
   
    $scope.GetDepartmentDetailsById = function (Id) {
        if (Id != null) {
            var Department = {
                Id: Id
            };
            var DepartmentData = AJService.PostDataToAPI("Master/GetDepartmentById", Department);
            DepartmentData.then(function (DepartmentData) {
                $scope.name = DepartmentData.data.DepartmentName;
            }, function () {
                alert('Error in getting Department list');
            });
        }
        else {
            alert("DepartmentID not found");
        }
    }

    $scope.UpdateDepartment = function () {

        var Department = {
            Id: $scope.Id,           
            DepartmentName: $scope.name,
        };
        blockUI.start();
        var DepartmentStatus = AJService.PostDataToAPI('Master/UpdateDepartment', Department);
        DepartmentStatus.then(function (msg) {

            if (msg.data != "OK") {
                SweetAlert.swal("Oops...", "Please retry!", "error");
            }
            else {
                SweetAlert.swal('Updated successfully.', '', "success");
               
            }
            {              
                $scope.name = "";
                angular.element(document.getElementById('angularid')).scope().GetDepartmentList();
            }
        },


        function () {
            alert('Please validate details');
        });

    }

    $scope.Reset = function (userForm)
    {
        $scope.name = null;
        $scope.userForm.$setPristine();
        $scope.submitted = false;
    }

    $scope.cancelForm = function (userForm) {
        //$window.location.href = '/Staff/staff/Staff';
        angular.element(document.getElementById('angularid')).scope().GetDepartmentList();
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

//To call PostVisitorEntry() function if form is valid (all validation is true)
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
                $scope.AddDepartment();
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    };
});
