
app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI) {

    app.expandControllerA($scope, AJService, $window);
    app.expandControllerTopSearch($scope, AJService, $window);

    clearForm();
    $scope.RoleReq = true;
    $scope.ReqRole = true;

    //--insert executive master data
    $scope.AddExutive = function () {

        SweetAlert.swal({
            title: "Are you sure?",
            text: "",
            type: "info",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes",
            closeOnConfirm: false,
            closeOnCancel: true
        },
        function (Confirm) {
            if (Confirm) {
                blockUI.start();
                var Executive = {
                    executiveName: $scope.Name,
                    executivecode: $scope.Code,
                    Emailid: $scope.Email,
                    Password: $scope.Password,
                    Mobile: $scope.Mobile,
                    Phoneno: $scope.Phone,
                    DepartmentId: $scope.Department,
                    ProcessTransferTo: $scope.ManagerList != '' ? 1 : 0, //$scope.Trensfer
                    ReportingId: $scope.ExecutiveRole == "Maneger" ? 0 : $scope.ManagerList,
                    Division: $scope.Division,
                    EnteredBy: $("#enterdBy").val(),
                    Id: $('#hid_Execid').val() == "" ? 0 : $('#hid_Execid').val(),
                    RoleName: $('input[name=Role]:checked').val().toLowerCase(),
                };

                var ExutiveStatus = AJService.PostDataToAPI('Master/insertExecutive', Executive);
                ExutiveStatus.then(function (msg) {
                    if (msg.data != "OK") {
                        SweetAlert.swal("Error!", "Executive already exist !", "", "error");
                    }
                    else {
                        if ($('#hid_Execid').val() != "") {

                            SweetAlert.swal('Updated successfully.', '', "success");
                            clearForm();
                            $('.reportingRow').css("display", "none");
                            $("input[type=radio]").prop("disable", true);
                        }
                        else {

                            SweetAlert.swal('Insert successfully.', '', "success");
                            $('.reportingRow').css("display", "none");
                            clearForm();
                            $("input[type=radio]").prop("disable", true);
                        }
                        angular.element(document.getElementById('angularid')).scope().GetExecutiveList();
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
        });
    }

    $scope.Reportingto = null;
    $scope.Division = [];
    $scope.button = "Submit"
    $scope.toBeDeleted = 0;

    //-- Delete Department  details on basis of ID
    $scope.DeleteExecutive = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var ExecutiveData = {
                Id: Id
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this Executive detail! ",
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
                     var DivisionStatus = AJService.PostDataToAPI('Master/ExecutiveDelete', ExecutiveData);
                     DivisionStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             angular.element(document.getElementById('angularid')).scope().GetExecutiveList();
                             SweetAlert.swal(msg.data);
                         }
                         else {
                             angular.element(document.getElementById('angularid')).scope().GetExecutiveList();
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             blockUI.stop();
                         }
                     });
                 }

             });
        }
        else {
            SweetAlert.swal("Error!", "Record is not Deleted", "", "error");
        }
        blockUI.stop();
    }

    $scope.EditExecutiveData = function (Id) {
        if (Id != null) {

            var ExecutiveData = {
                Id: Id
            };
            blockUI.start();
            // call API to fetch temp Department list basis on the FlatId


            //if ($('#ddlDepartment').find(":selected").text().toLocaleLowerCase() == "admin") {
            //    $('.Rolcheck').css("display", "none");
            //}
            //else {
            //    $('.Rolcheck').css("display", "block");
            //}

          

            var ExecutiveStatus = AJService.PostDataToAPI('Master/WebGetExecutiveById', ExecutiveData);
            ExecutiveStatus.then(function (msg) {
                if (msg != null) {

                    if (msg.data.query.DepartmentId != 83 && msg.data.query.DepartmentId != 1085) {
                        $scope.ReqRole = true
                    }
                    else {
                        $scope.ReqRole = false
                    }


                    $scope.Name = msg.data.query.Executivename,
                    $scope.Code = msg.data.query.Code
                    $scope.Email = msg.data.query.Email,
                    $scope.Password = msg.data.query.Password,

                    $scope.Mobile = msg.data.query.Mobile,
                    $scope.Phone = msg.data.query.Phone
                    $scope.Department = msg.data.query.DepartmentId
                    $scope.button = "Update"
                    $('#hid_Execid').val(msg.data.query.Id);
                   
                    if (msg.data.reporting != null) {
                        if (msg.data.query.DepartmentId != 83 && msg.data.query.DepartmentId != 1085) {
                            $("input[type=radio][value=Executive]").prop("checked", true);
                            $('.reportingRow').css("display", "block");
                            $scope.Reportingto = msg.data.reporting.reportingidto;
                            $scope.ReportingTo("Executive", $scope.Department);

                            //  $scope.ReqRole = true;
                        }
                    }
                    else {
                        
                        $("input[type=radio][value=Manager]").prop("checked", true);
                        // $scope.ReqRole = false;


                        if (msg.data.query.DepartmentId == 83 || msg.data.query.DepartmentId == 1085) {
                            $('.reportingRow').css("display", "none");
                        }
                        else {
                            $('.reportingRow').css("display", "block");
                        }
                      
                        
                        
                    }
                    ////----added on 20 Sep, 2017
                    if (msg.data.query.DepartmentId == 83 || msg.data.query.DepartmentId == 1085) {
                        $('.reportingRow').css("display", "none");
                    }
                    else {
                        if (msg.data.reporting != null) {
                            $('.reportingRow').css("display", "block");
                        }
                        else {
                            $('.reportingRow').css("display", "none");
                        }
                    }
                    //----------------------------

                    $scope.Division = [];
                    if (msg.data.divisionLinks != null) {
                        for (var i = 0; i <= msg.data.divisionLinks.length - 1; i++) {
                            $scope.Division.push("" + msg.data.divisionLinks[i].divisionid + "");
                        }
                    }

                    blockUI.stop();
                }
                else {
                    SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
                    blockUI.stop();
                }

            });
        }
    }

    $scope.submitForm = function () {

        $scope.submitted = true;
        if ($scope.Name == undefined || $scope.Name == "")
        {
            $scope.ExecutiveForm.$valid = false;
            return false;
        }
        if ($scope.Code == undefined || $scope.Code == "") {
            $scope.ExecutiveForm.$valid = false;
            return false;
        }
        if ($scope.Email == undefined || $scope.Email == "") {
            $scope.ExecutiveForm.$valid = false;
             return false;
        }
        if ($scope.Password == undefined || $scope.Password == "") {
            $scope.ExecutiveForm.$valid = false;
            return false;
        }
        if ($scope.Phone == "" && $scope.Phone == undefined) {
            $scope.ExecutiveForm.$valid = false;
            return false;
        }
        if ($scope.Department == undefined) {
            $scope.ExecutiveForm.$valid = false;
            return false;
        }
        if ($scope.Division.length==0) {
            $scope.ExecutiveForm.$valid = false;
            return false;
        }
        if ($('#ddlDepartment').find(":selected").text().toLocaleLowerCase() != "admin") {
            if ($('input[type=radio][name$=Role]:checked').length == 0) {
                $scope.ExecutiveForm.$valid = false;
                //$scope.RoleReq = true;
                $scope.ReqRolValid = true;
                return false;
                
            }
            if ($('input[type=radio][name$=Role]:checked').val().toLowerCase() == 'executive') {
                if ($('#managerList_ddl').val() == "") {
                    $scope.REQReportingValue = true;
                    $scope.ExecutiveForm.$valid = false;

                    return false;
                }
                else {
                    $scope.REQReportingValue = false;
                }
            }
        }
        else {
           
            if ($('input[type=radio][name$=Role]:checked').length == 0) {
               
                $scope.ExecutiveForm.$valid = false;
               // return false;
            }
        }


        //if ($('input[type=radio][name$=Role]:checked').length == 0) {
        //    $scope.ExecutiveForm.$valid = false;
        //    return false;
        //}
       
        if ($('input[type=radio][name$=Role]:checked').val() != "Manager" && $scope.ManagerList ==undefined) {
            $scope.ExecutiveForm.$valid = false;
            return false;
        }
       $scope.ExecutiveForm.$valid = true;

        if ($scope.ExecutiveForm.$valid) {
            if ($scope.update_city == "True") {
                $scope.ExecutiveForm.$setPristine();
                $scope.submitted = false;

                return;
            }

            else if ($scope.ExecutiveForm.$valid) {
                $scope.AddExutive();
                // set form default state
                $scope.ExecutiveForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    };

    $scope.getRoleList = function () {
        var RoleListData = AJService.GetDataFromAPI("Master/getRole", null);
        RoleListData.then(function (RoleListData) {
            $scope.RoleList = RoleListData.data;
        }, function () {
            alert('Error in RoleList By List');
        });
    }

    $scope.ReportingTo = function (role, department) {
        $scope.ReqRolValid = false;
        $scope.RoleReq = false
        if (role == "Manager") {
            $scope.mgrList = null;
            return false;
        }
        if (department == undefined) {
            $scope.mgrList = null;
            return false;
        }
        var dep = {
            Id: department
        }

        var getManagerList = AJService.PostDataToAPI("Master/getManagerList", dep);
        getManagerList.then(function (getManagerList) {

            if (getManagerList.data == null) {
                SweetAlert.swal("Oops..", "There is no manager for this department", "Error");
                return false;
            }

            if ($('#hid_Execid').val() != null && $('#hid_Execid').val() != '' && $('#hid_Execid').val() != undefined) {
                var _list = [];
                var _temp = $('#hid_Execid').val();
                for (var i = 0; i < getManagerList.data.length; i++) {
                    if (_temp != getManagerList.data[i].Id) {
                        _list.push({ 'Id': getManagerList.data[i].Id, 'Manager': getManagerList.data[i].Manager });
                    }
                }

                $scope.mgrList = _list;
            }
            else {
                $scope.mgrList = getManagerList.data;
            }

            if ($scope.Reportingto != undefined) {
                $scope.ManagerList = $scope.Reportingto;
            }
        }, function () {
            alert('Error in getting manager list');
        });
    }

    $scope.Func_ReportingToValue = function (Id)
    {

        var dep = {
            Id: Id
        }

        var getManagerList = AJService.PostDataToAPI("Master/getManagerList", dep);
        getManagerList.then(function (getManagerList) {

            if (getManagerList.data == null) {
                SweetAlert.swal("Oops..", "There is no manager for this department", "Error");
                return false;
            }
            $scope.mgrList = getManagerList.data;
            if ($scope.Reportingto != undefined) {
                $scope.ManagerList = $scope.Reportingto;
            }
        }, function () {
            alert('Error in getting manager list');
        });
    }
    $scope.ResetRole = function () {
      
        if ($('#ddlDepartment').find(":selected").text().toLocaleLowerCase() == "admin" || $('#ddlDepartment').find(":selected").text().toLocaleLowerCase() == "super admin") {
            $scope.ReqRole = false;
            $scope.RoleReq = false;
           
            $('.reportingRow').css("display", "none");
        }
        else {
            $scope.ReqRole = true;
            $scope.RoleReq = true;
            $('.reportingRow').css("display", "block");
        }
        

        $scope.mgrList = null;
        $scope.ExecutiveRole = false
        
        $scope.Func_ReportingToValue($('#ddlDepartment').val());
    }

    $scope.TransferToExecutiveList = function (Executive) {
        var object = {
            Id: Executive.Id,
            DepartmentId: Executive.DepartmentId
        }
        var ExecutiveList = AJService.PostDataToAPI("Master/TransferToExecutiveList", object);
        ExecutiveList.then(function (ExecutiveList) {
            $scope.TransferList = ExecutiveList.data;
            $('#hid_deletedExecutive').val(Executive.Id);
        }, function () {
            alert('Error in Executive List');
        });
    }

    $scope.DeleteExecutiveAfterTransfer = function () {
        if ($scope.TransferTo == undefined) {
            $("select[name$=Trensfer]").closest("div").addClass("has-error");
            $("select[name$=Trensfer]").next().find("P").removeClass("ng-hide");
            return false;
        }

        if ($('#hid_deletedExecutive').val() != "") {
            var ExecutiveData = {
                Id: $('#hid_deletedExecutive').val(),
                ProcessTransferTo: $scope.TransferTo,
                EnteredBy: $("#enterdBy").val(),
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this Executive detail! ",
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
                     var DivisionStatus = AJService.PostDataToAPI('Master/ExecutiveDelete', ExecutiveData);
                     DivisionStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                         }
                         else {

                             angular.element(document.getElementById('angularid')).scope().GetExecutiveList();
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             $('.close').click();
                             blockUI.stop();
                         }
                     });
                 }

             });
        }
        else {
            SweetAlert.swal("Error!", "Record is not Deleted", "", "error");
        }
        blockUI.stop();
    }

    function clearForm() {
       
        $scope.Name = "";
        $scope.Code = "";
        $scope.Email = "";
        $scope.Password = "";
        $scope.Mobile = "";
        $scope.Phone = "";
        $scope.Department = "";
        $scope.Division = [];
        $scope.ManagerList = "";
        $(".RoleChange").prop("checked", false);
    }

    // Get Division List
    $scope.getDivisionListMst = function () {
        var getDivisionList = AJService.GetDataFromAPI("DivisionMaster/getDivisionList", null);
        getDivisionList.then(function (Division) {
            $scope.DivisionList = Division.data;
        }, function () {
            //alert('Error in getting Division list');
        });
    }



});
