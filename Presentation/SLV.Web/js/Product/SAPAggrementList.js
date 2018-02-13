
app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {
   
    /*Expand common.master.js Controller*/

    app.expandControllerTopSearch($scope, AJService, $window);

    $scope.GetProductSapAggrementList = function () {
        var ProductSapAggrement = AJService.GetDataFromAPI('ProductMaster/getProductSapAggrementList');
        ProductSapAggrement.then(function (msg) {
            $scope.ProductSapAggrementList = msg.data;
            }, function () {
                //alert('Error in getting SAP Aggrement List');
            });
 
    };
   
    $scope.GetProductSapAggrementList();


    //For Delete ProductSAPAgreementMaster  // Added by Prakash on 21 Sep, 2017
    $scope.DeleteProductSAPAgreementMaster = function (isbn) {
        var mobj_delete = {
            OUPISBN: isbn == undefined ? "" : (isbn == null ? "" : isbn),
            DeactivateBy: parseInt($("#enterdBy").val()),
        };

        SweetAlert.swal({
            title: "Are you sure?",
            text: "You will not be able to recover this detail ! ",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false,
            closeOnCancel: true
        },
        function (Confirm) {
            if (Confirm) {
                //blockUI.start();

                var Delete = AJService.PostDataToAPI("ProductMaster/DeleteProductSAPAgreementMasterSet", mobj_delete);
                Delete.then(function (msg) {
                    if (msg.data == "OK") {                      
                        SweetAlert.swal({
                            title: "Deleted!",
                            text: "Your record  has been deleted.",
                            type: "success",
                            confirmButtonText: "OK",
                            closeOnConfirm: true
                        },
                        function (Confirm) {
                            if (Confirm) {
                                //blockUI.stop();
                                $scope.GetProductSapAggrementList();
                            }
                        });

                    }
                });


            }

        });

    }


});


