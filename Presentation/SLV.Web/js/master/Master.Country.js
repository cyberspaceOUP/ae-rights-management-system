app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {

app.expandControllerA($scope, AJService, $window);
app.expandControllerTopSearch($scope, AJService, $window);
    /*
  
 Here is the service used for inserting the Geographical List.
 Bind data In the object and send to api for inserting and update.
 Common function has used for both updation and Insertion

 */

 $scope.AddCountryData = function () {
    blockUI.start();
    var Geographical = {
        geogName: $scope.name,
        Id: $('#hid_recordid').val() == "" ? 0 : $('#hid_recordid').val(),
        geogType:'Country',
        EnteredBy: $("#enterdBy").val()
    };

    var SubsidiaryRightsStatus = AJService.PostDataToAPI('GeographicalMaster/InsertGeographical', Geographical);
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
                angular.element(document.getElementById('angularid')).scope().GetCountryList();
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
 $scope.EditCountryData = function (Id) {
    // check that Id have value or not     
    if (Id != null) {
        blockUI.start();
        // call API to fetch temp Department list basis on the FlatId
        var Geographical = AJService.GetDataFromAPI('GeographicalMaster/getGeographical?Id=' + Id);
        Geographical.then(function (msg) {
            if (msg != null) {
                $scope.name = msg.data._geographicalMaster.geogName;
                $('#btnSubmit').html("Update");
                $('#hid_recordid').val(msg.data._geographicalMaster.Id);
                blockUI.stop();
            }
            else {
                SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                blockUI.stop();
            }

        });
    }
}


 $scope.DeleteCountryData = function (Id) {
    // check that Id have value or not     
    if (Id != null) {
        // initialize variable for fetching data 
        var Geographical = {
            Id: Id,
            EnteredBy: $("#enterdBy").val()
        };
        SweetAlert.swal({
            title: "Are you sure?",
            text: "You will not be able to recover this Country detail! ",
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
                 var SubsidiaryRightsStatus = AJService.PostDataToAPI("GeographicalMaster/DeleteGeographical", Geographical);
                 SubsidiaryRightsStatus.then(function (msg) {
                     if (msg.data != "OK") {
                         SweetAlert.swal(msg.data);
                     }
                     else {
                         SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                         blockUI.stop();
                     }
                     {
                         angular.element(document.getElementById('angularid')).scope().GetCountryList();

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
    $scope.name = null;
    $scope.userForm.$setPristine();
    $scope.submitted = false;
}

$scope.cancelForm = function (userForm) {
    //$window.location.href = '/Staff/staff/Staff';
    angular.element(document.getElementById('angularid')).scope().GetCountryList();
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




$scope.GetCountryList = function () {
    //var GetCountryList = AJService.GetDataFromAPI("GeographicalMaster/GetGeographicalList?geogtype=Country", null);
    var GetCountryList = AJService.GetDataFromAPI("GeographicalMaster/GetCountryList", null);
    GetCountryList.then(function (Country) {
        $scope.CountryList = Country.data;
    }, function () {
        alert('Error in getting Country list');
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
            $scope.AddCountryData();
            // set form default state
            $scope.userForm.$setPristine();
            // set form is no submitted
            $scope.submitted = false;
            return;
        }
    }
};
});