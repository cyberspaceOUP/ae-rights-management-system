var appPath = "http://localhost:50319/api/";    //login/

app.service("AJService", function ($http) {

    //Angular JS Service to call Post Methods of Web API.
    this.PostDataToAPI = function (ApiReference, obj) {
        var response = $http.post(appPath + ApiReference, obj);
        return response;
    }

    //Angular JS Service to call Post Methods of Web API.
    this.GetDataFromAPI = function (ApiReference, emp) {
        var response = $http.get(appPath + ApiReference, emp);
        return response;
    }
}
);

