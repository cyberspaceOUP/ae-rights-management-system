app.controller("loginController", function ($scope, AJService, $window) {
    
    //<input type='hidden' id='hiddenValue'  value="10">
    //$scope.value = document.getElementById('hiddenValue').value;
    //alert($scope.value);

    GetEmployee();
    $scope.IsError = true;
    $scope.IsWarning = true;
    $scope.IsSuccess = true;


    $scope.DoLogin = function () {
        $scope.IsWarning = false;
        setInterval(function () {
            var Employee = {
                Username: $scope.Username,
                Password: $scope.Password,
            };

            //var getEmployeeData = AJService.DoLogin(Employee);
            var getEmployteeData = AJService.PostDataToAPI('Login', Employee);

            $scope.IsWarning = true;
            getEmployeeData.then(function (msg) {
                if (msg.data != "error") {
                    $scope.IsSuccess = false;

                    setInterval(function () {
                        $scope.IsError = true;
                        $scope.IsWarning = true;
                        $scope.IsSuccess = true;
                        window.location = msg.data;
                    }, 1000);
                }
                else
                    $scope.IsError = false;




            }, function () {
                $scope.IsError = false;
            });

        }, 2000);



    }

    $scope.DoRegister = function () {

        $scope.IsWarning = false;

        setInterval(function () {

            var Employee = {
                Name: $scope.Name,
                Email: $scope.Email,
                Username: $scope.Username,
                Password: $scope.Password,
            };

            var getEmployeeData = AJService.DoRegister(Employee);
            getEmployeeData.then(function (msg) {
                $scope.IsWarning = true;
                $scope.IsSuccess = false;
                setInterval(function () {
                    $scope.IsError = true;
                    $scope.IsWarning = true;
                    $scope.IsSuccess = true;
                    window.location = "/Home/UserList";
                }, 1000);

            }, function () {
                $scope.IsError = false;
            });

        }, 2000);


    }




    // Get Employee List
    function GetEmployee() {
        var getEmployeeData = AJService.getList();
        getEmployeeData.then(function (employee) {
            $scope.UserList = employee.data;
        }, function () {
            alert('Error in getting records');
        });
    }

});





//app.controller("DemoController", function ($scope, $http) {
//    $http.get('https://api.github.com/users/angular/repos')
//        .success(function (repos) {
//            $scope.repos = repos
//        });
//});