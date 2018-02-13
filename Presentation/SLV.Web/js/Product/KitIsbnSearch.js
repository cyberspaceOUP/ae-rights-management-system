/// <reference path="../master/Master.Division.js" />

app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {
    app.expandControllerTopSearch($scope, AJService, $window);

    /*Expand common.master.js Controller*/
    app.expandControllerA($scope, AJService, $window);
    $scope.Productcategory = "";
    /******************** Start DivisionSubDivision Control********************/

    app.expandControllerDivisionSubDivision($scope, AJService, $window);
    angular.element(document.getElementById('angularid')).scope().getAllProductTypeList();
    angular.element(document.getElementById('angularid')).scope().getAllProductTypeList();

    $scope.ShowKitISBNListForm = false;
    $scope.ShowKitISBNSearchForm = true;

    $scope.kitIsbnSearchForm = function () {
        var kitIsbnModel = {
            Division: $scope.DivSubDivCntrl.Division,
            Subdivision: $scope.DivSubDivCntrl.SubDivision,
            ProductCategory: $scope.ProductCategory,
            WorkingProduct: $scope.WorkingProduct,
            SubWorkingProduct: $scope.WorkingSubProduct,
            ISBN: $scope.KitISBN,
            ProductISBN: $scope.LinkedISBN,
            ProductTypeId: $('input[name=ProductType]:checked').val() != undefined ? parseInt($('input[name=ProductType]:checked').val()) : $('input[name=ProductType]:checked').val(),
        };
        //var mstr_ISBN = $scope.KitISBN == undefined ? "0" : $scope.KitISBN;

        $scope.submitted = true;
        $scope.userForm.$valid = true;
        if ($scope.userForm.$valid) {
            //var GetKitISBN = AJService.GetDataFromAPI("ProductMaster/GetKitIsbnData?mstr_ISBN=" + mstr_ISBN);
            var ProductStatus = AJService.PostDataToAPI('ProductMaster/GetKitIsbnData', kitIsbnModel);
            ProductStatus.then(function (msg) {
                if (msg.data.length == 0) {
                    $scope.ShowKitISBNListForm = false;
                    swal("No record", 'No record found', "warning");
                    blockUI.stop();
                }
                else {
                    //var KitISBNList = [];
                    //var _temp = '';
                    //for (var i = 0; i < msg.data.length; i++) {
                    //    if (_temp != msg.data[i].ISBN) {
                    //        //$scope.KitIsbnDataLength = i;
                    //        KitISBNList.push({ 'ISBN': msg.data[i].ISBN, 'KitISBNId': msg.data[i].KitISBNId });
                    //        _temp = msg.data[i].ISBN;
                    //    }
                    //}

                    //$scope.KitIsbnDataLength = KitISBNList;

                    $scope.KitIsbnData = msg.data;
                    $scope.ShowKitISBNSearchForm = false;
                    $scope.ShowKitISBNListForm = true; 
                }
            });
            return;
        }
        else {
            return false;
        }

    }

    $scope.BackToserch = function () {
        $scope.ShowKitISBNListForm = false;
        $scope.ShowKitISBNSearchForm = true;
    }

    $scope.DisplayDiv = function (count) {
        $('.tooltip-toggle').css("display", "none");
        $('.tooltip-toggle').removeAttr("id");
        $($('.tooltip-toggle')[count - 1]).attr("id", "tooltip-toggle");
    }

    //Added by prakash on 19 June, 2017
    //Autocomplete of KitISBN
    //$("[name$=KitISBN]").each(function () {
    //    var obj = $(this);

    //    var ISBNList = [];

    //    var GetKitISBN = AJService.GetDataFromAPI("ProductMaster/GetKitIsbnData?mstr_ISBN=" + "autocomplete");
    //    GetKitISBN.then(function (ISBN) {
    //        for (i = 0; i < ISBN.data.length; i++) {
    //            ISBNList[i] = { "label": ISBN.data[i], "value": ISBN.data[i], "data": ISBN.data[i] };
    //        }

    //        $(obj).autocomplete({
    //            source: function (request, response) {
    //                var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex(request.term), "i");
    //                response($.grep(ISBNList, function (item) {
    //                    return matcher.test(item.label);
    //                }));
    //            },

    //            autoFocus: true,
    //            select: function (event, ui) {
    //                $scope.PreviousProductId = ui.item.data;
    //                if ($scope.PreviousProductId == 0) {
    //                    $scope.Req_ISBNNO = true;
    //                }
    //                else {
    //                    $scope.Req_ISBNNO = false;
    //                   // $('#btnSubmit').trigger("click");
    //                }
    //            }
    //        });

    //    }, function () {
    //        alert('Error in getting KitISBN list');
    //    });

    //});

});