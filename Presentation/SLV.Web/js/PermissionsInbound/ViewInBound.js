/***********************************************************************************************
        Added by Saddam on 31/08/2016 for View Permission In Bound
    **************************************************************************************************/

app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {
    app.expandControllerTopSearch($scope, AJService, $window);
    /*Expand common.master.js Controller*/
    app.expandControllerA($scope, AJService, $window);
    //app.expandControllerAuthorContractDetails($scope, AJService, $window);

    //app.expandControllerProductLicense($scope, AJService, $window);

    app.expandControllerProductDetails($scope, AJService, $window);


    
    $scope.ProductSerach($('#hid_ProductId').val());
    
    setTimeout(function () {
        //fetch Kit Details List
        app.expandControllerKitISBNLIst($scope, AJService);
        angular.element(document.getElementById('angularid')).scope().KitISBNDetailsList($('#hid_ProductId').val());
    }, 300);


    //if ($('#hid_Type').val() == "C") {
    //    $scope.AuthorContract($("#hid_AuthorContract").val());

    //    $scope.Req_ContractDeatil = true;
    //    $scope.Req_ProductLicense = false;
    //}
    //else if ($('#hid_Type').val() == "L") {

    //    $scope.Req_ProductLicense = true;
    //    $scope.Req_ContractDeatil = false;

    //    $scope.ProductLicenseSerach($("#hid_AuthorContract").val());
    //}
       
    
  

    function convertDate(date) {
        if (date != undefined) {
            var datearray = date.split("/");
            return datearray[1] + '/' + datearray[0] + '/' + datearray[2];
        }
        else {
            return null;
        }

    }

    function convertDateForInsert(dateVal) {

        if (dateVal == "") {
            dateVal = null
        }
        else {

            var RequestDate = dateVal;

            var date = RequestDate;
            var d = new Date(date.split("/").reverse().join("-"));
            var dd = d.getDate();
            var mm = d.getMonth() + 1;
            var yy = d.getFullYear();
            return yy + "/" + mm + "/" + dd;
        }
    }

   
    

    $scope.AssetTypeViewMode = function (Id) {
    
        var AssetTypeViewModeViewStatus = AJService.GetDataFromAPI('PermissionsInbound/GetMultipleAssetTypeDetails?Code=' + Id, null);

        AssetTypeViewModeViewStatus.then(function (msg) {
            if (msg.data != "") {

                //for (var i = 0; i < msg.data._permissionInbound.length; i++) {
                //    mstr_AssetType = mstr_AssetType + "," + msg.data._permissionInbound[i].AssetsType;
                //}

              
                ////if (msg.data._permissionInbound.AssetsType != null) {
                ////    if (msg.data._permissionInbound.AssetsType == "I") {
                ////        $scope.AssetstypeView = 'Image Bank/ Video Bank'
                ////    }
                ////    else if (msg.data._permissionInbound.AssetsType == "O") {
                ////        $scope.AssetstypeView = 'Others'
                ////    }
                ////    else if (msg.data._permissionInbound.AssetsType == "B") {
                ////        $scope.AssetstypeView = 'Image Bank/ Video Bank And Others'
                ////    }
                ////}
                ////else {
                ////    $scope.AssetstypeView = "---"
                ////}
                //var mstr_AssetTypeValue = mstr_AssetType.substring(1)

                
                //if (mstr_AssetTypeValue != null) {
                //    if (mstr_AssetTypeValue.indexOf("I") > -1) {
                //        $scope.AssetstypeView = 'Image Bank/ Video Bank'
                //    }
                //    else if (msgmstr_AssetTypeValue.indexOf("O") > -1) {
                //        $scope.AssetstypeView = 'Others'
                //    }
                //    else if (msgmstr_AssetTypeValue.indexOf("B") > -1) {
                //        $scope.AssetstypeView = 'Image Bank/ Video Bank And Others'
                //    }
                //}
                //else {
                //    $scope.AssetstypeView = "---"
                //}

                
            }
            else {
                
               SweetAlert.swal("Error!", "Error in system. Please try again", "", "error");
            }
        }, function () {
            //alert('Error in getting copy right holder list which is not used in permission inbound');
        });
    }


    if ($('#hid_InboundId').val() != "") {
         
        

            $scope.AssetTypeViewMode($('#hid_InboundId').val());

            app.expandControllerImageVideoBankViewDetails($scope, AJService, $window, SweetAlert);
            //$scope.ImageVideoBankViewMode($('#hid_InboundId').val());
            $scope.ImageVideoBankViewModeAll($('#hid_InboundId').val());
            app.expandControllerOthersViewDetails($scope, AJService, $window);
            //$scope.OthersViewViewMode($('#hid_InboundId').val());

            app.expandControllerViewPendingRequestInsertDetails($scope, AJService, $window, SweetAlert);
            $scope.GetViewPermissionInboundUpdateList($('#hid_InboundId').val());


          
            app.expandControllerViewAssetDetails($scope, AJService, $window);


     


    }


    $scope.ContractstatusReq = true;


    $scope.ExcelMultipleReport = function (ImageVideo, CopyRight, Product, InBound) {

        document.location = GlobalredirectPath + "PermissionsInbound/PermissionsInbound/PermissionsInboundViewExcel?code=" + $("#hid_InboundId").val() + "&ImageVideo=" + ImageVideo + "&CopyRight=" + CopyRight + "&Product=" + Product + "&InBound=" + InBound + "";

    }

    

    $scope.PermissionsInboundReportExcelImageVideo = function()
    {
        $scope.ExcelMultipleReport("ImageVideoBank", null, "Product", "InBound");
    }

    $scope.PermissionsInboundReportExcelCopyrightholderDetails = function () {
       
        $scope.ExcelMultipleReport(null, "CopyRightHolder", "Product", "InBound");
    }

    $scope.PermissionsInboundReportExcelAll = function () {

        $scope.ExcelMultipleReport("ImageVideoBank", "CopyRightHolder", "Product", "InBound");
    }

});