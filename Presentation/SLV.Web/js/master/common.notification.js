
app.expandControllerNotification = function ($scope, AJService, $window, SweetAlert, blockUI) {

    // Get notification list
    $scope.GetNotificationListByIdAndFlatId = function (contactId, flatId) {

        // check that FlatId exists or not

        if (contactId != null && flatId != null) {
            // intialize variable
            var Notification = {
                contactId: contactId,
                flatId: flatId
            };


            // call API for fetching all contacts of the flat
            contactData = AJService.PostDataToAPI("Notification/notificationListByIdAndFlatId", Notification);
            contactData.then(function (notifications) {
                // intialize scope variable
                $scope.notificationList = notifications.data.NotifiacationData;
                $scope.notificationCount = notifications.data.NotificationCount;
                

            }, function () {
                SweetAlert.swal("Error in getting notification list", "", "error");
            });
        }
        else {
            SweetAlert.swal("FlatId or Contact Id not found", "", "error");
           // SweetAlert.swal("Error occured while saving data", "", "error");
        }
    }
    
    // Delete notification details if RefTypeId and RefId is not null

    $scope.deleteNotification = function (value) {
        if (value != null) {
            var Common = {
                Value: value
            };
            var postNotificationData = AJService.PostDataToAPI("Notification/removeNotification", Common);
            postNotificationData.then(function (msg) {
                if (msg.data != "OK") {

                }
                else {

                }
            });

        }

    }
}
