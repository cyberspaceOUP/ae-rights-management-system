app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert) {
    app.expandControllerProductDetails($scope, AJService, $window);

    app.expandControllerTopSearch($scope, AJService, $window);

    app.expandControllerKitISBNLIst($scope, AJService);

    $scope.SAPRow = [];
    $scope.AuthorRow = [];
    $scope.SAPRow.push(1);

    var mstr_authorIdValue = 0;
    var count = 0;
    var count2 = 0;
    var mstr_authorId = $('#hid_SapAggValue')
    $scope.getProductAuthorList = function () {

        var _productMaster = {
            ProductId: $("[name$=hid_sapproductId]").val(),
        }

        var ProductAuthorList = AJService.PostDataToAPI("ProductMaster/getProductAuthor", _productMaster)
        ProductAuthorList.then(function (AuthorList) {
            var Author = AuthorList.data;

            for (i = 0; i < Author.length; i++) {
                $scope.AuthorRow.push(Author[i]);
            }

        },
        function () {
            //alert('There is some error in the system');
        });

    }

    $scope.getProductAuthorList();
    $scope.addSapRow = function () {
        var i = $scope.SAPRow.length + 1;
        $scope.SAPRow.push(i);
    }

    $scope.removeSapRow = function (index) {
        $scope.SAPRow.splice(index, 1);
    }

    //Call on Button Submit
    $scope.SAPAggrementEntryForm = function () {
        $scope.submitted = true;
        if ($scope.userForm.$valid) {
            var marr_SAPAggremenet = [];
            var marr_SAPAggrementNo = [];
            var marr_AuthorCode = [];
            var marr_AuthorId = [];

            $("[name*=SAPAggrementNo]").each(function () {
                if ($(this).val() != "" && $(this).val() != undefined) {
                    var _mobj = {
                        OUPISBN: $("[name$=hid_oupisbn]").val(),
                        SAPagreementNo: $(this).val(),
                        AuthorId: $("#hid_SapAggAuthorId_" + count2).val(),
                        EnteredBy: $("#enterdBy").val(),
                        ProductCategory: $("#hid_ProductCategory_" + count2).val(),
                    }
                    count2 = count2 + 1;
                    marr_SAPAggremenet.push(_mobj);
                    marr_SAPAggrementNo.push(_mobj.SAPagreementNo);
                }
            });

            $("[name*=AuthorCode]").each(function () {
                if ($(this).val() != undefined && $(this).val() != "") {                 

                            var marr_AuthorId = $("#hid_SapAggAuthorId_" + i).val();

                            var _mobj = {
                                OUPISBN: $("[name$=hid_oupisbn]").val(),
                                AuthorCode: $(this).val(),
                                AuthorId: $("#hid_SapAggAuthorId_" + count).val(),//$("[name$=hid_SapAggAuthorId]").val(),
                                EnteredBy: $("#enterdBy").val(),
                                ProductCategory: $("#hid_ProductCategory_" + count).val(),
                            }

                            count= count + 1;
                       
                    marr_SAPAggremenet.push(_mobj);
                    marr_AuthorCode.push(_mobj);
                }
            });

           
            

          


            //$('.saprow').each(function () {

            //    if ($(this).find('input[name*=SAPAggrementNo]').val() != undefined && $(this).find('input[name*=SAPAggrementNo]').val() != "" &&
            //        $(this).find('input[name*=AuthorCode]').val() != undefined && $(this).find('input[name*=AuthorCode]').val() != ""
            //        ) {
            //        var _mobj = {
            //            OUPISBN: $("[name$=hid_oupisbn]").val(),
            //            AuthorCode: $(this).find('input[name*=AuthorCode]').val(),
            //            SAPagreementNo: $(this).find('input[name*=SAPAggrementNo]').val(),
            //            AuthorId: $("[name$=hid_sapAuthorId]").val(),
            //            EnteredBy: $("#enterdBy").val(),
            //        }

            //        //alert("first Box Value : " + $(this).find('input[name*=SAPAggrementNo]').val() + " Second Box value" + $(this).find('input[name*=AuthorCode]').val());
            //        marr_SAPAggremenet.push(_mobj);

            //        marr_SAPAggrementNo.push(_mobj.SAPagreementNo);
            //        marr_AuthorCode.push(_mobj.AuthorCode);

            //    }

            //});


            var duplicates = marr_SAPAggrementNo.reduce(function (acc, el, i, arr) {
                if (arr.indexOf(el) !== i && acc.indexOf(el) < 0) acc.push(el); return acc;
            }, []);

            if (duplicates.length != 0)
            {

                SweetAlert.swal("Message", "SAP Agreement Nos. are duplicate.", "warning");
               // alert('SAP Agreement Nos. are duplicate.');
                return false;
            }

            var duplicates = marr_AuthorCode.reduce(function (acc, el, i, arr) {
                if (arr.indexOf(el) !== i && acc.indexOf(el) < 0) acc.push(el); return acc;
            }, []);

            if (duplicates.length != 0) {
                alert('Author Code are duplicate.');
                return false;
            }


            if (marr_SAPAggremenet.length == 0) {
                var textbox = $("[name*=SAPAggrementNo]")[0];

                if ($("[name*=SAPAggrementNo]").length > 1) {
                    SweetAlert.swal("Validation", "Please enter at least one SAP Agreement No and Author Code", "error");
                }
                else {
                    SweetAlert.swal("Validation", "Please enter SAP Agreement No and Author Code", "error");
                }
              
                $(textbox).focus();
                $scope.Serchform.$valid = false;
            }


            if ($scope.userForm.$valid) {
                $scope.SAPEntry(marr_SAPAggremenet);
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
            }
        }
    }
    
    $scope.SAPEntry = function (marr_SAPAggremenet) {        
    
      var ProductStatus = AJService.PostDataToAPI('ProductMaster/InsertSAPAggrementDetails', marr_SAPAggremenet);     
      ProductStatus.then(function (msg) {
            if (msg.data != "OK") {
                SweetAlert.swal(msg.data, '', "error");
            }
            else {
                SweetAlert.swal({
                    title: "SAP Agreement No/ Author Code Entred successfully.",
                    text: "",
                    type: "success"
                },
                function () {
                    $("#hid_ProductId").val($scope.ProductLicenseId);
                    window.location.href = "ProductSearch?for=sapaggrement&from=backtoSearch";
                });
               
            }

        },
        function () {
            alert('There is some error in the system');
        });
    }
    /*End Update Case*/




    $scope.getSAPAggreementAuthorDetails = function () {


        var URL = window.location.href;
        if (URL.indexOf("Id") >= 0) {
            var id = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            if (id != "" && id != "undefined") {
                var ProductId = id[0].split('=')[1]
                


                var SAPAgreement = {
                    id: ProductId
                };

                var getSAPAggreementAuthorDetails = AJService.PostDataToAPI('ProductMaster/SAPAggreementAuthorDetails', SAPAgreement);
                getSAPAggreementAuthorDetails.then(function (msg) {

                    $scope.SAPAggreementAuthorDetails = msg.data;

                }, function () {
                    //alert('Error in getting Product list');
                });


            }

        }

    };

    $scope.getSAPAggreementAuthorDetails();




    

    //$scope.getProductAuthorFill = function () {

    //    var _SAPAggrementAssignFill = {
    //        OUPISBN: $('#hid_oup_isbn').val(),
    //    }
        
    //    var ProductAuthorListFill = AJService.PostDataToAPI("ProductMaster/getProductAuthorFill", _SAPAggrementAssignFill)
    //    ProductAuthorListFill.then(function (AuthorListUpdate) {
    //        debugger;
    //        //var Author = AuthorList.data;

    //        //for (i = 0; i < Author.length; i++) {
    //        //    $scope.AuthorRow.push(Author[i]);
    //        //}

    //    },
    //    function () {
    //        alert('There is some error in the system');
    //    });

    //}
    //if ($('#hid_oup_isbn').val() != "" && $('#hid_oup_isbn').val() != undefined) {

       
    //    $scope.getProductAuthorFill();

    //}


});