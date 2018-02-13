app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {

    /*Expand common.master.js Controller*/
    //app.expandControllerA($scope, AJService, $window);

    $scope.func_AuthorContractCodeChk = function () {
        $scope.ContractCodeOrLicenseCodeChk = 'AuthorContractCode';
        $scope.ReqAuthorContractCode = true;
        $scope.ReqLicenseCode = false;
    }

    $scope.func_ProductLicenseCodeChk = function () {
        $scope.ContractCodeOrLicenseCodeChk = 'ProductLicenseCode';
        $scope.ReqLicenseCode = true;
        $scope.ReqAuthorContractCode = false;
    }

    $scope.func_GoBack = function () {
        //$('#AuthorPublisherStatement_Search').show();
        //$('#AuthorPublisherStatement_List').hide();
        //$('#backToSearch').hide();
        window.location.href = "../../PaymentTagging/PaymentTagging/AuthorPublisherStatementSearch";
    }

    $scope.AuthorPublisherStatementSearchForm = function () {
        $scope.submitted = true;

        if ($scope.userForm.$valid) {
            $scope.AuthorPublisherStatementSearch();
            // set form default state
            $scope.userForm.$setPristine();
            // set form is no submitted
            $scope.submitted = false;
            return;
        }
    }

    $scope.AuthorPublisherStatementSearch = function () {
        
       // var For = $('#hid_For').val();
        //if (For == 'Rights') {
            var _mobjAuthorPubSt = {
                Year: $scope.Year,
                Month: $scope.Month,
                AuthorContractCode: $scope.AuthorContractCode,
                AuthorCode: $scope.AuthorCode,
                AuthorName: $scope.AuthorName,
                ProductLicenseCode: $scope.ProductLicenseCode,
                PublishingCompanyCode: $scope.PublishingCompanyCode,
                PublishingCompanyName: $scope.PublishingCompanyName
            }
            debugger;

            var ExecutiveStatus = AJService.PostDataToAPI('PaymentTaggingMaster/GetAuthorPublisherStatement', _mobjAuthorPubSt);
            ExecutiveStatus.then(function (msg) {
                if (msg.data.length != 0) {
                    $scope.AuthorPublisherStatement_List = msg.data;

                    $('#AuthorPublisherStatement_Search').hide();
                    $('#AuthorPublisherStatement_List').show(); 
                    $('#backToSearch').show();
                }
                else {
                    SweetAlert.swal("No record", 'No record found', "warning");
                }
            });
        //}
        //else {
        //    SweetAlert.swal("Try agian", "There is some problem.", "", "error");
        //}
    }

    //for View page
    $scope.AuthorPublisherStatement = function (Id) {
       
        var _mobjAuthorPubSt = {
            ContractId: Id,
        }
        debugger;

        var ExecutiveStatus = AJService.PostDataToAPI('PaymentTaggingMaster/GetAuthorPublisherStatementDetail', _mobjAuthorPubSt);
        ExecutiveStatus.then(function (msg) {
            if (msg.data.length != 0) {
                $scope.AuthorPublisherStatement_Detail = msg.data;
                
                var amt = 0;
                for (i = 0; i < msg.data.length; i++) {
                    amt += msg.data[i].Amount;
                }
                $scope.totalAmount = amt;

            }
            else {
                SweetAlert.swal("No record", 'No record found', "warning");
            }
        });
    }


});


