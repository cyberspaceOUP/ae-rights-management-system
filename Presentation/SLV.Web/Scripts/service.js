var appPath = "http://localhost/RMSAPI/api/";    //login/
//var appPath = "http://192.50.50.142/RMSAPI/api/";
app.service("AJService", function ($http) {

    //Angular JS Service to call Post Methods of Web API.
    this.PostDataToAPI = function (ApiReference, obj) {
        var response = $http.post(appPath + ApiReference, obj);
        return response;
    }

    //Angular JS Service to call Post Methods of Web API.
    this.GetDataFromAPI = function (ApiReference, obj) {
        var response = $http.get(appPath + ApiReference, obj);
        return response;
    }

    this.GetDataFromAPIParam = function (ApiReference, id) {
        var response = $http.get(appPath + ApiReference, {
            params: { id: id }
        });
        return response;
    }
}
);

    