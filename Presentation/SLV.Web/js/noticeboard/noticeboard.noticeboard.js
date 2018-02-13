app.controller("MainCtrl", function ($scope, AJService, $window, $compile, SweetAlert, blockUI, $timeout) {
   

    // location: js/society/common.society.js
    app.expandControllerSociety($scope, AJService, $window, SweetAlert, blockUI);

    $scope.dateCheck = function () {
        $scope.isRequired = false;
    }

    $scope.dateCheckExpiry = function () {
        $scope.isRequiredExpiry = false;
    }


    GetNoticeBoardList();


    function GetNoticeBoardList(){
       blockUI.start();
        var societyId = $('#hidSocietyId').val();
        var getNoticeBoardList = AJService.GetDataFromAPIParam("NoticeBoard/getAllNoticeBoard", societyId);
        getNoticeBoardList.then(function (notice) {
            $scope.NoticeBoardDetail = notice.data;
        }, function () {
            SweetAlert.swal("Oops...", "Please retry!", "error");
        });
        blockUI.stop();
    }

    $scope.isPaidCheck = false;

    // Get Notice Board Visibilities
    $scope.GetNoticeBoardVisibilities = function () {
        blockUI.start();
        var getGetNoticeBoardVisibilityList = AJService.GetDataFromAPI("NoticeBoard/noticeBoardVisibility", null);
        getGetNoticeBoardVisibilityList.then(function (visibilities) {
            $scope.noticeBoardVisibilities = visibilities.data;
        }, function () {
            SweetAlert.swal("Oops...", "Please retry!", "error");
        });
        blockUI.stop();
    }

    $scope.submitNoticeBoardForm = function () {


         // $scope.NoticeBoard.Description = $('.note-editor').find('.note-editable').html();


      //  var $sendmail = $('input[name=checksend]:checked');
     //   alert($sendmail.length);

        // $scope.NoticeBoard = {};
       
       // alert($("#hid_Uploads").val());
      //  alert($(".fileNameClass").val());
        // var filename = $(".fileNameClass").val();
       
        //alert($(".fileNameClass").val($(".fileNameClass").val() + filename + ','));

         
         // $('.note-editor').find('.note-editable').html();

        var errorDiv;
        var errormsg = '';
       
        FileNameArray = $('#dropZone0').find('.fileNameClass');
        var array = [];
        //  $("input[class=fileNameClass]").each(function () {
        FileNameArray.each(function () {     
                array.push(
               $(this).val()
           );
           
                for (i = 0; i < array.length; i++) {
                    if(array[i]=="")
                    {                     
                        errorDiv = document.getElementById("fileid");
                        errorDiv.innerHTML = "Please enter file name";
                        errormsg = "Please enter file name";
                    }
                   
                }
           
        });
       

        $scope.submitted = true;

        if ($scope.NoticeBoardForm.$valid) {
            //   $scope.PostNoticeDetail();
            //to reset the controls after submit
            $scope.NoticeBoardForm.$setPristine();           
            $scope.submitted = false;
        }


        $scope.isRequired = true;
        $scope.isRequiredExpiry = true;

        $scope.NoticeBoard.NoticeDate = $('#NoticeDateId').val();
        $scope.NoticeBoard.ExpiryDate = $('#ExpiryDateId').val();

        // validation for blocks and tower in case of inserttion 
        if (typeof $scope.NoticeBoard.NBId === "undefined" || $scope.NoticeBoard.NBId == null) {

            if ($scope.NoticeBoard.AccessibleId == 51952) {
                var Towerdetails = [];
                angular.forEach($scope.towers, function (value, key) {
                    if ($scope.towers[key].Checked) {
                        // alert(Towerdetails.push(key + ': ' + value));
                        Towerdetails.push($scope.towers[key].Id);

                    }
                });

                if (Towerdetails.length > 0)
                    $scope.msg = "";
                else
                    $scope.msg = 'Please select atleast one tower';
            }

            if ($scope.NoticeBoard.AccessibleId == 51951) {
                var Blockdetails = [];
                angular.forEach($scope.blocks, function (value, key) {
                    if ($scope.blocks[key].Checked) {
                        Blockdetails.push($scope.blocks[key].Id);
                    }
                });

                if (Blockdetails.length > 0)
                    $scope.msg = "";
                else
                    $scope.msg = 'Please select atleast one block';
            }

        }
            // validation for blocks and tower in case of updation 
        else
        {
            // check type of visibility
            if ($scope.NoticeBoard.AccessibleId == 51951) {
                var $block = $('input[name=blockname]:checked');                     
                if ($block.length == 0) {
                    $scope.msg = 'Please select atleast one block';
                }
                else
                {
                    $scope.msg = "";
                }


            }
            if ($scope.NoticeBoard.AccessibleId == 51952) {
              
                var $tower = $('input[name=towername]:checked');              
                if ($tower.length == 0) {
                    $scope.msg = 'Please select atleast one tower';
                }
                else {
                    $scope.msg = "";
                }
            }

        }
        //set form validation true or false based on condition
        var a = $('.note-editor').find('.note-editable').html();
        if (($scope.msg != '') || errormsg != '' || $scope.NoticeBoard.NoticeDate == '' || $scope.NoticeBoard.ExpiryDate == '' || typeof $scope.NoticeBoard.Heading === 'undefined' || ($('.note-editor').find('.note-editable').html() == '') || ($scope.isPaidCheck == true && (typeof $scope.NoticeBoard.Amount === "undefined" || typeof $scope.NoticeBoard.ContactName === "undefined")))
        {
            if ($scope.isPaidCheck == true && (typeof $scope.NoticeBoard.Amount === "undefined" || typeof $scope.NoticeBoard.ContactName === "undefined")) {
                $scope.msgg = "Please enter resident";
            }

            if ($scope.NoticeBoard.NoticeDate == '' || typeof $scope.NoticeBoard.NoticeDate === 'undefined')
            {

                $scope.isRequired = true;
            }


            if ($scope.NoticeBoard.ExpiryDate == '' || typeof $scope.NoticeBoard.ExpiryDate === 'undefined') {

                $scope.isRequiredExpiry = true;
            }
           
            if ($.trim(a) == '')
            {
                $scope.descmsg = "Please enter description";
            }
            $scope.NoticeBoardForm.$valid = false;
        }
        else
        {
            if ($scope.isPaidCheck == true && (typeof $scope.NoticeBoard.Amount === "undefined" || typeof $scope.NoticeBoard.ContactName === "undefined")) {
                $scope.msgg = "Please enter resident";
                $scope.isRequired = false;
                $scope.isRequiredExpiry = false;
                $scope.NoticeBoardForm.$valid = false;
            }

            else
            {
                $scope.msgg = "";
                $scope.NoticeBoardForm.$valid = true;

            }
            
        }
        
        // Set the 'submitted' flag to true
        $scope.submitted = true;

        if ($scope.NoticeBoardForm.$valid) {
           $scope.PostNoticeDetail();  // insert and update function call
            //to reset the controls after submit
            $scope.NoticeBoardForm.$setPristine();           
            $scope.submitted = false;
        }
    };

    $scope.getChecked = function () {
        $scope.msg = "";
    };


    // Insert and update Notice Detail
    $scope.PostNoticeDetail=function()
    {
       // var $sendmail = $('input[name=checksend]:checked');
       // alert($sendmail.length);


     
        FileNameArray = $('#dropZone0').find('.fileNameClass');
        var array = [];
        //  $("input[class=fileNameClass]").each(function () {
        FileNameArray.each(function () {
            array.push(
                $(this).val()
            );
        });
        // then to get the JSON string
        //var jsonFileName = JSON.stringify(array);


        //convert datetime format dd/mm/yyyy fromat to mm/dd/yyyy format

        //Notice date
        var TempNoticeDateTime = $('#NoticeDateId').val();
        var NoticeDateTimeSplit = TempNoticeDateTime.split("/");
        var NoticeDateTime = NoticeDateTimeSplit[1] + "/" + NoticeDateTimeSplit[0] + "/" + NoticeDateTimeSplit[2];


        //Expiry date
        var TempExpiryDateTime = $('#ExpiryDateId').val();
        var ExpiryDateTimeSplit = TempExpiryDateTime.split("/");
        var ExpiryDateTime = ExpiryDateTimeSplit[1] + "/" + ExpiryDateTimeSplit[0] + "/" + ExpiryDateTimeSplit[2];


     //paid check
        if ($scope.isPaidCheck == true) {
            $scope.isPaidCheck = 1;
        }
        else {
            $scope.isPaidCheck = 0;
        }

        // flash check
        if ($scope.NoticeBoard.IsFlashNotice == true)
        {
            $scope.NoticeBoard.IsFlashNotice = 1;
        }
        else
        {
            $scope.NoticeBoard.IsFlashNotice = 0;
        }

        // validated check 
        if ($scope.NoticeBoard.Validated == true) {
            $scope.NoticeBoard.Validated = 1;
        }
        else {
            $scope.NoticeBoard.Validated = 0;
        }


        if ($scope.NoticeBoard.AccessibleId == 51952) {
            var Typedetails = [];
            angular.forEach($scope.towers, function (value, key) {
                if ($scope.towers[key].Checked) {
                   // alert(Towerdetails.push(key + ': ' + value));
                    Typedetails.push($scope.towers[key].Id);

                }
            });

        }

        if ($scope.NoticeBoard.AccessibleId == 51951) {
            var Typedetails = [];
            angular.forEach($scope.blocks, function (value, key) {
                if ($scope.blocks[key].Checked) {                  
                    Typedetails.push($scope.blocks[key].Id);
                }
            });
        }



        var NoticeBoard = {
            Heading: $scope.NoticeBoard.Heading,
            // Description: $scope.NoticeBoard.Description,
            Description: $('.note-editor').find('.note-editable').html(),
            Date: NoticeDateTime,
            ExpiryDate: ExpiryDateTime,
            IsFlashNotice: $scope.NoticeBoard.IsFlashNotice,
            Validated: $scope.NoticeBoard.Validated,
            IsPaid: $scope.isPaidCheck,
            Amount:$scope.NoticeBoard.Amount,
            ContactId: $('#hdnContactId').val(),
            SocietyId: $('#hidSocietyId').val(),
            VisibleTo: $scope.NoticeBoard.AccessibleId,
            Type: 1,
            AccessibleTypesIdsArray: Typedetails,
            DocumentURL:$("#hid_Uploads").val(),
            DocumentName: array  
        };

        

        if (typeof $scope.NoticeBoard.NBId === "undefined" || $scope.NoticeBoard.NBId == null) {
            var PostNoticeDetail = AJService.PostDataToAPI("NoticeBoard/insertNoticeBoard", NoticeBoard); //for insert new notice details
            var showmsg = "Notice board details entered successfully !!"

        }
        else
        {
            NoticeBoard.Id = $scope.NoticeBoard.NBId;
            var PostNoticeDetail = AJService.PostDataToAPI("NoticeBoard/updateNoticeBoard", NoticeBoard);  //For update existing notice details
            var showmsg = " Notice board details updated successfully  !!";
        }

        PostNoticeDetail.then(function (msg) {
            if (msg.data != "OK") {          
                SweetAlert.swal("Oops...", "Please retry!", "error");
            }
            else {

                SweetAlert.swal(showmsg, '', "success");             
                $scope.clearNoticeBoard(NoticeBoardForm);
                GetNoticeBoardList();           
            }
        }, function () {
            SweetAlert.swal("Oops...", "Please retry!", "error");
          
        });

    }

    //Clear Notice Detail
    $scope.clearNoticeBoard = function (NoticeBoardForm) {
        $scope.NoticeBoard.Heading = null;
        $scope.NoticeBoard.Description = null;
        $scope.NoticeBoard.NoticeDate = '';
        $scope.NoticeBoard.ExpiryDate = '';     
        $('#ChkFlashId span').removeClass('checked');   //to reset the checkbox 
        $('#ChkValidatedId span').removeClass('checked');   
        $('#ChkPaidId span').removeClass('checked');
        $scope.NoticeBoard.Amount = null;
        $scope.NoticeBoard.ContactName = null;
        $scope.NoticeBoard.AccessibleId = '';
        $scope.NoticeBoard.NBId = null;
       
      //  $scope.tower.Checked = null;
       // $scope.block.Checked = null;
      //  angular.forEach($scope.towers, function (tower) {
          //  $scope.tower.Checked = [];
       // });
              
        $('.cstmProgressBar').hide();      
        $('.uploadedFileName').hide();
        $('#btn_Uploader').val("Select File");
        $('#fileid').hide();
        $('#doclistid').hide();
        $('.note-editor').find('.note-editable').empty();
        $scope.msg = '';
        $scope.descmsg = '';
        $scope.NoticeBoardForm.$setPristine();
        $scope.submitted = false;
    }

    //publish Notice
    $scope.PublishNotice=function(noticeid)
    {
        SweetAlert.swal({
            title: "Are you sure?",
            text: "You will not be able to edit this notice detail!",
            type: "warning", 
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, Publish it!",
            closeOnConfirm: false,
            closeOnCancel: true
        }, function (isPublish) {
            if (isPublish) {

        var PublishNoticeDetail = AJService.GetDataFromAPIParam("NoticeBoard/getpublishNoticeBoard", noticeid);
       
        PublishNoticeDetail.then(function (msg) {
            if (msg.data != "OK") {
                SweetAlert.swal("Oops...", "Please retry!", "error");
            }
            else {

                SweetAlert.swal("Notice board details published successfully !!", '', "success");              
                GetNoticeBoardList();
            }
        }, function () {

            SweetAlert.swal("Oops...", "Please retry!", "error");

        });
            }
        });

    }


    // Auto Fill the Contact Name
    $scope.AutoFillContact = function (contactId) {

        var getContact = AJService.GetDataFromAPIParam("Contact/ContactDetailById", contactId);
        getContact.then(function (_contact) {
            $scope.NoticeBoard.ContactName = _contact.data.Name;
        }, function () {
            SweetAlert.swal("Oops...", "Please retry!", "error");
        });
    }


    //Update Notice Detail
    $scope.getNoticeBoardDetail = function (Notice)
    {
       
        $scope.NoticeBoard = {};
        $scope.Docurl = [];
        $scope.NoticeBoard.isPaidCheck = false;
        if (Notice.Published == false)
        {
            var getNoticeDetail = AJService.PostDataToAPI("NoticeBoard/noticeBoardDetail", Notice);
            getNoticeDetail.then(function (_noticedetail) {
                $('#headingid').focus();
                $('.note-editor').find('.note-editable').html(_noticedetail.data.Description);
               
                $scope.NoticeBoard.NBId = _noticedetail.data.Id;
                $scope.NoticeBoard.Heading = _noticedetail.data.Heading;
              //  $scope.NoticeBoard.Description = _noticedetail.data.Description;
                $scope.NoticeBoard.NoticeDate = _noticedetail.data.Date;
                $scope.NoticeBoard.ExpiryDate = _noticedetail.data.ExpiryDate;              
                $scope.NoticeBoard.isPaidCheck = _noticedetail.data.IsPaid;               
                if (_noticedetail.data.IsFlashNotice == true) {
                    $('#ChkFlashId span').addClass('checked');
                }
                if (_noticedetail.data.Validated == true) {
                    $('#ChkValidatedId span').addClass('checked');

                }

                if (_noticedetail.data.IsPaid == true) {
                    $('#ChkPaidId span').addClass('checked');
                    $scope.NoticeBoard.isPaidCheck = true;
                   
                     $('#PaidId').removeClass('ng-hide');
                }
                else {
                    $('#ChkPaidId span').removeClass('checked');
                    $scope.NoticeBoard.isPaidCheck = false;
                      $('#PaidId').addClass('ng-hide');
                }
                $scope.NoticeBoard.Amount = _noticedetail.data.Amount;
                $scope.NoticeBoard.ContactName = _noticedetail.data.ContactName;
                $scope.NoticeBoard.AccessibleId = _noticedetail.data.VisibleTo;
                //$scope.blocks = _noticedetail.data.AccessibleTypesIdsArray;

                // check type of visibility
                if (_noticedetail.data.VisibleTo == 51951) {
                    $timeout(function () {
                        var blocks = _noticedetail.data.AccessibleTypesIdsArray.slice(',');
                        for (var i = 0; i < blocks.length ; i++) {
                            $("#chkBlock-" + blocks[i] + "").prop("checked", "checked");
                        }
                    }, 1000);
                }
                if (_noticedetail.data.VisibleTo == 51952) {
                    $timeout(function () {
                        var towers = _noticedetail.data.AccessibleTypesIdsArray.slice(',');
                        for (var i = 0; i < towers.length ; i++) {

                            $("#chkTower-" + towers[i] + "").prop("checked", "checked");
                        }
                    }, 1000);
                }

                var e = 0;
                var d = 0;
                var docNames = '';
                var Docurl = '';
                if (_noticedetail.data.DocumentName != '') {
                    var docNames = _noticedetail.data.DocumentName.slice(',');
                    var DName = _noticedetail.data.DocumentName.slice(',');

                    var DId = _noticedetail.data.NoticeBoardDocumentIds.slice(',');
                    
                    var Docurl = _noticedetail.data.DocumentURL.split(',');
                    //   $scope.Docurl = [];
                    for (var i = 0; i < Docurl.length - 1; i++) {                  
                        //for (var j = 0; j < docNames.length; j++) {   
                        for (var j = 0, k = 0; j < docNames.length && k < DId.length ; j++,k++) {
                            if (e == 0 && d==0) {
                                $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[j].toString(), DocId: DId[k].toString() });
                            }
                            else {
                                $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[e].toString(), DocId: DId[d].toString() });      
                            }
                            e = j + 1;
                            d = k + 1;
                            i = i + 1;
                        }



                    }
                }
            });
        }      
        else
        {
            SweetAlert.swal("Oops...", "Published notice cannot update!", "error");
        }
    }
});

function getContactAutoFill(ControlID, SelectedControlID) {
 
    var societyId = parseInt($('#hidSocietyId').val());
      
    ControlID.autocomplete({

        source: function (request, response) {
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: appPath + "NoticeBoard/contactByName?name=" + ControlID.val() + "&societyid=" + societyId + "",
                dataType: "json",
                success: function (data) {


                    response($.map(data, function (v, i) {
                        var text = v.name;
                        if (text && (!request.name || matcher.test(text))) {
                            return {
                                label: v.name,
                                val: v.Id
                            };
                        }
                    }));

                },

                error: function (result) {
                }
            });

        },
        select: function (e, i) {
            $('input[type=hidden][id=' + SelectedControlID + ']').val(i.item.val);
   
            var ContactId = $('#hdnContactId').val();
        
             angular.element('#NoticeBoardId').scope().AutoFillContact(ContactId);
            angular.element('#NoticeBoardId').scope().$apply();
            
        },
        minLength: 1
    });
}



