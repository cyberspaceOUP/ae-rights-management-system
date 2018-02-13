var appPath = "http://localhost/RMSAPI/api/";    //login/

app.service("AJService", function ($http) {

    //Angular JS Service to call Post Methods of Web API.
    this.PostDataToAPI = function (ApiReference, obj) {
        var response = $http.post(appPath + ApiReference, obj);
        return response;
    }

    //Angular JS Service to call Get Methods of Web API.
    this.GetDataFromAPI = function (ApiReference, obj) {
        var response = $http.get(appPath + ApiReference, obj);
        return response;
    }

    this.GetDataFromAPIValue = function (ApiReference, id) {
        var req = {
            method: 'get',
            url: appPath,
            data: { id: id }
        }
        var response = $http(req);
        return response;
    }


}
);

