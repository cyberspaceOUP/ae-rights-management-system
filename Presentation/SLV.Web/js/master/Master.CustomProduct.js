
app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI, $parse) {
    app.expandControllerA($scope, AJService, $window);
    // debugger;

    app.expandControllerTopSearch($scope, AJService, $window);

    var URL = window.location.href;
  
    if (URL.indexOf("Id") >= 0) {
        var id = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
       
        if (id != "" && id != "undefined") {
            var Author_id = id[0].split('=')[1]
            $("#hid_ProductId").val(Author_id);
           }

    }
    

   app.expandControllerProprietorDetails($scope, AJService, $window);

   app.expandControllerAuthorSuggestion($scope, AJService, $window);
   app.expandControllerProductDetails($scope, AJService, $window);
 
   $scope.ProductSerach($("#hid_ProductId").val())

   $scope.PublishingCompanyMandatory = true;
   $scope.PubCenterMandatory = true;
   $scope.ProprietorISBN = true;
   $scope.ProprietorProduct = true;
   $scope.ProprietorEdition = true;
   $scope.ProprietorCopyrightYear = true;
   $scope.ProprietorImprint = true;
  
   $scope.AddCustomProduct = function () {
      
       blockUI.start();

       if ($scope.AuthorList.length > 0)
       {
           var input = "";
           for (var i = 0; i < $scope.AuthorList.length; i++)
           {
               // input.push($scope.AuthorList[i].AuthorId);
               input += $scope.AuthorList[i].AuthorId + ",";
           }
           
       }
       var marr_proprAuthor = $scope.AuthorList;

       var CustomProduct = {
           //  ProductId: $scope.ProductId,
           ProductId: $("#hid_ProductId").val(),
           ProprietorISBN: $scope.ProprDetailCntrl.ProprietorISBN,
           ProprietorProduct: $scope.ProprDetailCntrl.ProprietorProduct,
           ProprietorEdition: $scope.ProprDetailCntrl.ProprietorEdition,
           ProprietorCopyrightYear: $scope.ProprDetailCntrl.ProprietorCopyrightYear,
           PublishingCompanyId: $scope.PublishPubCntrl.PublishingCompany,
           ProprietorPubCenterId: $scope.PublishPubCntrl.PubCenter,
           ProprietorImPrintId: $scope.ProprDetailCntrl.ProprietorImprint,
           ProprietorAuthorLink: marr_proprAuthor,

           AuthorId: input.slice(0, -1),
           EnteredBy: $("#enterdBy").val(),
           Id: $('#hid_Custd').val() == "" ? 0 : $('#hid_Custd').val(),
       };

       if($('#hid_Custd').val() == "")
       {
           var ExutiveStatus = AJService.PostDataToAPI('CustomProduct/InsertProprietor', CustomProduct);
       }
   else
   {
           var ExutiveStatus = AJService.PostDataToAPI('CustomProduct/UpdateProprietor', CustomProduct);
       }

      
        ExutiveStatus.then(function (msg) {

          

            if (msg.data != "OK") {

                SweetAlert.swal("Error!", "Custom Product already exist !", "", "error");
            }
            else {
                if ($('#hid_Custd').val() != "") {
                    SweetAlert.swal('Updated successfully.', '', "success");

                }
                else {
                    SweetAlert.swal('Insert successfully.', '', "success");
                }
             
                angular.element(document.getElementById('angularid')).scope().GetCustomProductList();
          

            }
            {
                $scope.ProprDetailCntrl.ProprietorISBN = "";
                $scope.ProprDetailCntrl.ProprietorProduct = "";
                $scope.ProprDetailCntrl.ProprietorEdition = "";
                $scope.ProprDetailCntrl.ProprietorCopyrightYear = "";
                $scope.PublishPubCntrl.PublishingCompany = "";
                $scope.PublishPubCntrl.PubCenter = "";
                $scope.ProprDetailCntrl.ProprietorISBN = "";
                $scope.ProprDetailCntrl.ProprietorImprint = "";
                $scope.AuthorList.length = 0;
                $scope.SuggestedAuthorName = "";
                $scope.AuthorCategory = "";
                $('#hid_Custd').val("");
                $('#btnSubmit').html("Submit");
            }
        },


        function () {
            alert('Please validate details');
        });
        blockUI.stop();
    }

    

    // Delete Department  details on basis of ID
   $scope.DeleteCustom = function (Id) {
      
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var CustomData = {
                Id: Id,
                EnteredBy: $("#enterdBy").val()
            };
            SweetAlert.swal({
                title: "Are you sure?",
                text: "You will not be able to recover this Custom Product detail! ",
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
                     var CustomStatus = AJService.PostDataToAPI('CustomProduct/CustomDelete', CustomData);
                     CustomStatus.then(function (msg) {
                         if (msg.data != "OK") {
                             SweetAlert.swal(msg.data);
                         }
                         else {
                             SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
                             blockUI.stop();
                         }
                         //{
                         angular.element(document.getElementById('angularid')).scope().GetCustomProductList();
                         //}
                     });
                 }

             });
        }
        else {
            SweetAlert.swal("Error!", "Record is not Deleted", "", "error");
        }
        blockUI.stop();
    }



    $scope.EditCustomData = function (Id) {
        // check that Id have value or not     
        if (Id != null) {
            // initialize variable for fetching data 
            var CustomData = {
                Id: Id
            };
            blockUI.start();
            // call API to fetch temp Department list basis on the FlatId
            var CustomStatus = AJService.PostDataToAPI('CustomProduct/WebGetProprietorById', CustomData);
            CustomStatus.then(function (msg) {
               
                if (msg != null) {
                  
                    $scope.ProprDetailCntrl.ProprietorISBN = msg.data.Proprietor.ProprietorISBN;
                    $scope.ProprDetailCntrl.ProprietorProduct = msg.data.Proprietor.ProprietorProduct;
                    $scope.ProprDetailCntrl.ProprietorEdition = msg.data.Proprietor.ProprietorEdition;
                    $scope.ProprDetailCntrl.ProprietorCopyrightYear = msg.data.Proprietor.ProprietorCopyrightYear;
                    $scope.ProprDetailCntrl.ProprietorImprint = msg.data.Proprietor.ProprietorImPrintId;
                    $scope.PublishPubCntrl.PublishingCompany = msg.data.Proprietor.PublishingCompanyId;
                  //  $scope.getPubCenterByCompanyIdListInner(msg.Proprietor.PublishingCompanyId);
                    $scope.PublishPubCntrl.PubCenter = msg.data.Proprietor.ProprietorPubCenterId;

                    $scope.AuthorList = msg.data.ProprietorAuthor;
                    $scope.getPubCenterByCompanyIdList();
               
                    $('#btnSubmit').html("Update");
                    $('#hid_Custd').val(msg.data.Proprietor.Id);
                  
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
        if ($scope.userForm.$valid) {
            
            $scope.AddCustomProduct();
                // set form default state
                $scope.userForm.$setPristine();
                // set form is no submitted
                $scope.submitted = false;
                return;
          
        }
    };



    $scope.SerchAuther = function (Id)
    {
        if(Id !=  null)
        {
            var AuthorData = {
                Id: Id
            };
   
            var AuthorSuggesationList = AJService.PostDataToAPI("Author/AuthorList", AuthorData);
            AuthorSuggesationList.then(function (AuthorSuggesationList) {
               
             
                $scope.AuthorSuggesationList = AuthorSuggesationList.data.AuthorSuggesation;


               $scope.Authortest = AuthorSuggesationList.data.AuthorSuggesation[0];

            }, function () {
                alert('Error in getting Author Suggesation List');
            });



        }
    };
  

    $scope.getPubCenterByCompanyIdListInner = function (ID) {

        var PublishingCompany = {
            Id: ID,
        };
        var PubCenternList = AJService.PostDataToAPI("CommonList/PubCenterByCompanyIdList", PublishingCompany);
        PubCenternList.then(function (PubCenter) {
            $scope.PubCenterList = PubCenter.data;
        }, function () {
            alert('Error in getting Pub Center List');
        });
    }
   


});
