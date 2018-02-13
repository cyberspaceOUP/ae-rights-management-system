app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {
    app.expandControllerProductDetails($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);

    //Call on Button Submit
    $scope.FinalProductEntryForm = function (ProductModel) {
        $scope.submitted = true;

        if ($("form[name*=userForm]").find(".has-error").length > 0) {
            //SweetAlert.swal("validation", "Please check the required feilds", "error");
            $scope.userForm.$valid = false;
        }

        if ($scope.userForm.$valid) {

            if ($scope.userForm.$valid) {
                $scope.FinalProductEntry(ProductModel);
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    }

    $scope.FinalProductEntry = function () {

        var mobj_ProductDetails = {
            Id: $scope.ProductModel.ProductId,
            FinalProductName: $scope.ProductModel.FinalProductName,
            PublishingDate: $scope.ProductModel.PublishingDate,
            CopyrightYear: $scope.ProductModel.OUPCopyrightYear,
            EnteredBy: $("#enterdBy").val(),
        }

        var ProductStatus = AJService.PostDataToAPI('ProductMaster/InsertFinalProductDetails', mobj_ProductDetails);
        ProductStatus.then(function (msg) {

            if (msg.data != "OK") {
                SweetAlert.swal('There is some problem.', '', "Try agian");
            }
            else {
                SweetAlert.swal({
                    title: "Final Product entered successfully.",
                    text: "",
                    type: "success"
                },
                   function () {
                       $("#hid_ProductId").val($scope.ProductLicenseId);

                       $('form[name*=user]').attr("method", "post");
                       $('form[name*=user]').submit();
                   });
            }

        },
        function () {
            alert('There is some error in the system');
        });
    }


    $scope.SetPublishingDate = function (datetext) {
        if ($(datetext).val() == "") {
            $(datetext).closest(".form-group").addClass("has-error");
            $(datetext).closest(".datePicker").next().find("p").addClass("ng-show").removeClass("ng-hide");
        }
        else {
            $scope.ProductModel.PublishingDate = $(datetext).val();

            var date = $(datetext).val();
            var d;
            if (navigator.userAgent.search("Safari") >= 0 && navigator.userAgent.search("Chrome") < 0) {
                d = new Date(parseInt(date.split("/")[2]),parseInt(date.split("/")[1]-1),parseInt(date.split("/")[0]));
            }
            else {
                d = new Date(date.split("/").reverse().join("-"));
            }
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();

            if (dd >= 1 && mm >= 7) {
                yy = yy + 1
            }

            setTimeout(function () {
                //$('[name=OUPCopyrightYear]').val($(datetext).val().split('/')[2]);
                $('[name=OUPCopyrightYear]').val(yy);
                $scope.ProductModel.OUPCopyrightYear = $('[name=OUPCopyrightYear]').val();
            }, 100)
            $(datetext).closest(".form-group").removeClass("has-error");
            $(datetext).closest(".datePicker").next().find("p").removeClass("ng-show").addClass("ng-hide");
        }
    }
    /*End Update Case*/






    $scope.Product_CopyRightYear = function (Id) {
      
        if (Id != null) {
          
            var ProductId = {
                Id: Id
            };
            // call API to fetch temp Department list basis on the FlatId
            var Product_CopyRightYear = AJService.PostDataToAPI('ProductMaster/GetProductCopyrightYear', ProductId);
            Product_CopyRightYear.then(function (status) {
                $scope.ProductModel.OUPCopyrightYear = status.data.CopyrightYear;
            }, function () {
                //alert('Error in getting Author Contract Links list');
            });
        }
    }


    if ($('#hid_CopyrightYear').val() != "")
    {
        $scope.Product_CopyRightYear($('#hid_CopyrightYear').val());
    }

});