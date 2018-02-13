
app.controller("MainCtrl", function ($scope, AJService, $window, SweetAlert) {

    $scope.submitForm = function () {

        // Set the 'submitted' flag to true
        $scope.submitted = true;

        if ($scope.passwordForm.$valid) {
            $scope.PasswordChange();
            //to reset the controls after submit
            $scope.passwordForm.$setPristine();
            $scope.submitted = false;
        }
    };


    $scope.PasswordChange = function () {


        var Contact = {
            Id: $('#hdnContactId').val(),
            Password: $scope.newpassword,
            executiveName: $scope.oldpassword
        };

        var postContactPassword = AJService.PostDataToAPI("Contact/ChangePassword", Contact);  //For update password of existing contact

        postContactPassword.then(function (msg) {
            if (msg.data = "Password Changed Successfully") {
                //  debugger;
                SweetAlert.swal('Password change successfully.', '', "success");
              //  $scope.msg = msg.data;
                $scope.oldpassword = null;
                $scope.newpassword = null;
                $scope.confirmpassword = null;
              //  $window.location.href = '/';
               // SweetAlert.swal('insert successfully.', '', "success");
               // $scope.msg = msg.data;
            }
            else if (msg.data = "OldPassword Not Match")
            {
                SweetAlert.swal('Old Password not match.', '', "success");
                $scope.oldpassword = null;
                $scope.newpassword = null;
                $scope.confirmpassword = null;
               // $window.location.href = '/';
            }
            //else {
               
                
                
            //}
        }, function () {
            $scope.msg = "please validate data";
        });

    }


});


// match the password with new password directive
app.directive('ngCompare', function () {
    return {
        require: 'ngModel',
        link: function (scope, currentEl, attrs, ctrl) {
            var comparefield = document.getElementsByName(attrs.ngCompare)[0]; //getting first element
            compareEl = angular.element(comparefield);

            //current field key up
            currentEl.on('keyup', function () {
                if (compareEl.val() != "") {
                    var isMatch = currentEl.val() === compareEl.val();
                    ctrl.$setValidity('compare', isMatch);
                    scope.$digest();
                }
            });

            //Element to compare field key up
            compareEl.on('keyup', function () {
                if (currentEl.val() != "") {
                    var isMatch = currentEl.val() === compareEl.val();
                    ctrl.$setValidity('compare', isMatch);
                    scope.$digest();
                }
            });
        }
    }
});