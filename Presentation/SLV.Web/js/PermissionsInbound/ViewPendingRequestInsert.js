﻿app.expandControllerViewPendingRequestInsertDetails = function ($scope, AJService, $window, SweetAlert) {

   
    function convertDateDDMMYYYY(inputFormat) {
        function pad(s) { return (s < 10) ? '0' + s : s; }
        var d = new Date(inputFormat);
        return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/');
    }

    $scope.GetViewPermissionInboundUpdateList = function (Id) {
    
        var getPermissionInboundUpdateDetail = AJService.GetDataFromAPI("PermissionsInbound/GetAllPermissionInboundUpdateList?Code=" + Id, null);
        getPermissionInboundUpdateDetail.then(function (msg) {
            if (msg.data != "") {

                $scope.ViewPenddingRequestData = true;
                if (msg.data.PendingRequestPermissionsInboundDetails.Documentname != null) {
                    var e1 = 0;
                    var d1 = 0;
                    var docNames1 = '';
                    var Docurl1 = '';
                    $scope.Docurl1 = [];

                    if (msg.data.PendingRequestPermissionsInboundDetails.Documentname != '') {

                        $scope.Pendingdocumentshow = true;
                        var docNames1 = msg.data.PendingRequestPermissionsInboundDetails.Documentname.slice(',');
                        var DName1 = msg.data.PendingRequestPermissionsInboundDetails.Documentname.slice(',');

                        var DId1 = msg.data.PendingRequestPermissionsInboundDetails.DocumentIds.slice(',');

                        var Docurl1 = msg.data.PendingRequestPermissionsInboundDetails.DocumentFile.split(',');
                        //   $scope.Docurl = [];
                        for (var i = 0; i < Docurl1.length - 1; i++) {
                            //for (var j = 0; j < docNames.length; j++) {   
                            for (var j = 0, k = 0; j < docNames1.length && k < DId1.length ; j++, k++) {
                                if (e1 == 0 && d1 == 0) {
                                    //  $scope.Docurl.push({ text: Docurl[i].toString(), name: docNames[j].toString(), DocId: DId[k].toString() });
                                    $scope.Docurl1.push({ text1: Docurl1[i].toString(), name1: docNames1[j].toString(), DocId1: DId1[k].toString() });
                                    // $scope.Docurl1.push({ text: "ABC.DOC", name: "123456TEST", DocId: 10 });

                                    // $("#hid_DocumentID").val($("#hid_DocumentID").val() + Docurl1[i].toString() + ',');
                                }
                                else {
                                    $scope.Docurl1.push({ text1: Docurl1[i].toString(), name1: docNames1[e1].toString(), DocId1: DId1[d1].toString() });
                                }

                                e1 = j + 1;
                                d1 = k + 1;
                                i = i + 1;
                            }



                        }

                    }
                    else if ($scope.Pendingdocumentshow == true) {
                        $scope.Pendingdocumentshow = false;
                    }
                }


                $scope.ContractStatusView = (msg.data._InboundUpdate.Contractstatus == null ? '' : msg.data._InboundUpdate.Contractstatus);
                $scope.AgreementDateView = (msg.data._InboundUpdate.AgreementDate == null ? '' : convertDateDDMMYYYY(msg.data._InboundUpdate.AgreementDate));
                $scope.EffectiveDateView = (msg.data._InboundUpdate.Effectivedate == null ? '' : convertDateDDMMYYYY(msg.data._InboundUpdate.Effectivedate));
                $scope.ContractperiodView = (msg.data._InboundUpdate.Contractperiodinmonth == null ? '' : (msg.data._InboundUpdate.Contractperiodinmonth));
                $scope.ExpiryDateView = (msg.data._InboundUpdate.Expirydate == null ? '' : convertDateDDMMYYYY(msg.data._InboundUpdate.Expirydate));
                $scope.SignedContractSentDateView = (msg.data._InboundUpdate.SignedContractSentDate == null ? '' : convertDateDDMMYYYY(msg.data._InboundUpdate.SignedContractSentDate));
                $scope.SignedContractReceivedDateView = (msg.data._InboundUpdate.SignedContractReceived_Date == null ? '' : convertDateDDMMYYYY(msg.data._InboundUpdate.SignedContractReceived_Date));
                $scope.CancellationDateView = (msg.data._InboundUpdate.CancellationDate == null ? '' : convertDateDDMMYYYY(msg.data._InboundUpdate.CancellationDate));
                $scope.CancellationReasonView = (msg.data._InboundUpdate.Cancellation_Reason == null ? '' : msg.data._InboundUpdate.Cancellation_Reason);
                $scope.PenddingRemarksView = (msg.data._InboundUpdate.PendingRemarks == null ? '' : msg.data._InboundUpdate.PendingRemarks);

                if (msg.data._InboundUpdate.PendingRemarks == null) {
                    $scope.ViewPenddingRequestData = false;
                }

            }
            else {
                $scope.ViewPenddingRequestData = false;
            }
        }
    )
    };


    $scope.RemoveDocumentLinkById = function (docid, file) {

        SweetAlert.swal({
            title: "Are you sure?",
            text: "You will not be able to recover this detail! ",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false,
            closeOnCancel: true
        },
          function (Confirm) {
              if (Confirm) {

                  var ID = { Id: docid, EnteredBy: $("#enterdBy").val() };
                  var DeleteDocument = AJService.PostDataToAPI("PermissionsInbound/RemovePermissionsInboundDocument", ID);

                  DeleteDocument.then(function (msg) {
                      if (msg.data != "OK") {
                          SweetAlert.swal("Oops...", "Please retry!", "error");

                      }
                      else {


                          var obj = {};
                          obj.filename = file;
                          $.ajax({
                              cache: false,
                              type: "POST",
                              contentType: 'application/json; charset=utf-8',
                              url: GlobalredirectPath + "/Common/deletedocument",
                              data: JSON.stringify(obj),
                              dataType: "json",
                              success: function (result) {
                                  if (result == "Deleted") {

                                      $scope.GetPermissionInboundUpdateList($('#hid_InboundId').val());

                                  }

                              },
                              error: function (xhr, ajaxOptions, thrownError) {
                              }
                          });


                      }
                  }, function () {

                      SweetAlert.swal("Oops...", "Please retry!", "error");

                  });

                  SweetAlert.swal("Deleted!", "Your record  has been deleted.", "success");
              }
          });
    }



}
