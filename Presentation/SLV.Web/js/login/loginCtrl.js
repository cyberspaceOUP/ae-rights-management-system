
app.controller("MainCtrl", function ($scope, AJService) {
    


    $scope.Login = function () {

        $scope.InvalidMsg = "Please wait...";
        var Employee = {
            Emailid: $scope.Emailid,
            Password: $scope.password,
        };
       
        var EmpStatus = AJService.PostDataToAPI('Login/Login', Employee);



        EmpStatus.then(function (msg) {

            if (msg.data.status != "Successful") {
                $scope.InvalidMsg = msg.data.status;
                return false;
               // SweetAlert.swal('Successfully Login.', '', "success");
            }
            else {
                this.myform.method = "post";
                this.myform.submit();
                
            }


        }, function () {

        });

    }


    $scope.ForgotPassword = function () {
        $scope.InvalidMsg = "Please wait...";
        var Employee = {
            EmailId: $scope.email,
            Id: $('#hdnContactId').val()
        };

        var ForgotStatus = AJService.PostDataToAPI('Login/ForgetPassword', Employee);
                
        ForgotStatus.then(function (msg) {

            if (msg.data.status == "OK") {
                $scope.InvalidMsg = "Your login details has been sent to your email id."

                return false;
                //$('#forget-password').click();
               
                // SweetAlert.swal('Successfully Login.', '', "success");
            }
            else {
                $scope.InvalidMsg = "This Email is not registered with us.";
                return false;

            }


        }, function () {

        });

    }



});




