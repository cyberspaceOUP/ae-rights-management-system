app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {

app.expandControllerA($scope, AJService, $window);

app.expandControllerTopSearch($scope, AJService, $window);
    /*
  
 Here is the service used for inserting the SubsidiaryRights List.
 Bind data In the object and send to api for inserting and update.
 Common function has used for both updation and Insertion

 */



$scope.AddSubsidiaryRights = function () {
    blockUI.start();
    var SubsidiaryRights = {
        SubsidiaryRights: $scope.name,
        Id: $('#hid_recordid').val() == "" ? 0 : $('#hid_recordid').val(),
        EnteredBy: $("#enterdBy").val()
    };

    var SubsidiaryRightsStatus = AJService.PostDataToAPI('SubsidiaryRightsMaster/InsertSubsidiaryRights', SubsidiaryRights);
    SubsidiaryRightsStatus.then(function (msg) {
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
                $('#btnSubmit').html("Submit");
                $scope.update_city == "False";
                $('#hid_recordid').val("");
                //angular.element(document.getElementById('angularid')).scope().GetSubsidiaryRightsList();
                angular.element(document.getElementById('angularid')).scope().GetSubsidiaryRightsList();
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
$scope.EditSubsidiaryRightsData = function (Id) {
    // check that Id have value or not     
    if (Id != null) {
        // initialize variable for fetching data 
        //var SubsidiaryRights = {
        //    Id: Id,
        //    EnteredBy: $("#enterdBy").val()
        //};
        blockUI.start();
        // call API to fetch temp Department list basis on the FlatId
        //var SubsidiaryRights = AJService.PostDataToAPI('SubsidiaryRightsMaster/getSubsidiaryRights', SubsidiaryRights);
        var SubsidiaryRights = AJService.GetDataFromAPI('SubsidiaryRightsMaster/getSubsidiaryRights?Id=' + Id);
        SubsidiaryRights.then(function (msg) {
            if (msg != null) {
                $scope.name = msg.data._SubsidiaryRightsMaster.SubsidiaryRights;
                $('#btnSubmit').html("Update");
                $('#hid_recordid').val(msg.data._SubsidiaryRightsMaster.Id);
                blockUI.stop();
            }
            else {
                SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                blockUI.stop();
            }

        });
    }
}


$scope.DeleteSubsidiaryRights = function (Id) {
    // check that Id have value or not     
    if (Id != null) {
        // initialize variable for fetching data 
        var SubsidiaryRights = {
            Id: Id,
            EnteredBy: $("#enterdBy").val()
        };
        SweetAlert.swal({
            title: "Are you sure?",
            text: "You will not be able to recover this Subsidiary Rights detail! ",
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
                 var SubsidiaryRightsStatus = AJService.PostDataToAPI("SubsidiaryRightsMaster/DeleteSubsidiaryRights", SubsidiaryRights);
                 SubsidiaryRightsStatus.then(function (msg) {
                     if (msg.data != "OK") {
                         SweetAlert.swal(msg.data);
                     }
                     else {
                         SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                         blockUI.stop();
                     }
                     {
                         //angular.element(document.getElementById('angularid')).scope().GetDepartmentList();
                         angular.element(document.getElementById('angularid')).scope().GetSubsidiaryRightsList();

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


$scope.GetSubsidiaryRightsDetailsById = function (Id) {
    if (Id != null) {
        //var SubsidiaryRights = {
        //    Id: Id
        //};
        //var SubsidiaryRightsData = AJService.PostDataToAPI("SubsidiaryRightsMaster/GetSubsidiaryRightsById", SubsidiaryRights);

        var Id = Id;
        var SubsidiaryRightsData = AJService.PostDataToAPI("SubsidiaryRightsMaster/GetSubsidiaryRightsById?Id="+ Id, null);

        SubsidiaryRightsData.then(function (SubsidiaryRightsData) {
            $scope.name = SubsidiaryRightsData.data.SubsidiaryRights;
        }, function () {
            alert('Error in getting Subsidiary Rights list');
        });
    }
    else {
        alert("SubsidiaryRightsID not found");
    }
}

//$scope.UpdateSubsidiaryRights = function () {

//    var SubsidiaryRights = {
//        Id: $scope.Id,
//        DepartmentName: $scope.name,
//    };
//    blockUI.start();
//    var SubsidiaryRightsStatus = AJService.PostDataToAPI('SubsidiaryRightsMaster/UpdateSubsidiaryRights', SubsidiaryRights);
//    SubsidiaryRightsStatus.then(function (msg) {

//        if (msg.data != "OK") {
//            SweetAlert.swal("Oops...", "Please retry!", "error");
//        }
//        else {
//            SweetAlert.swal('Updated successfully.', '', "success");

//        }
//        {
//            $scope.name = "";
//            //angular.element(document.getElementById('angularid')).scope().GetDepartmentList();
//            GetSubsidiaryRightsList();
//        }
//    },


//    function () {
//        alert('Please validate details');
//    });

//}

$scope.Reset = function (userForm) {
    $scope.name = null;
    $scope.userForm.$setPristine();
    $scope.submitted = false;
}

$scope.cancelForm = function (userForm) {
    //$window.location.href = '/Staff/staff/Staff';
    //angular.element(document.getElementById('angularid')).scope().GetDepartmentList();
    $scope.GetSubsidiaryRightsList();
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




$scope.GetSubsidiaryRightsList = function () {
    var GetSubsidiaryRightsList = AJService.GetDataFromAPI("SubsidiaryRightsMaster/GetSubsidiaryRightsList", null);
    GetSubsidiaryRightsList.then(function (SubsidiaryRights) {
        $scope.SubsidiaryRightsList = SubsidiaryRights.data;
    }, function () {
        alert('Error in getting Subsidiary Rights list');
    });
};

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
            $scope.AddSubsidiaryRights();
            //$scope.GetSubsidiaryRightsList();
            // set form default state
            $scope.userForm.$setPristine();
            // set form is no submitted
            $scope.submitted = false;
            return;
        }
    }
};
});