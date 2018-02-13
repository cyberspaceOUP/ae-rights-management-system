app.factory('svc', function (AJService, $http) {

    return {
         
        GetSalutationList: function () {

        	var list = AJService.GetDataFromAPI("master/getSalutationList", null);
        	return list;
        }
		,
        GetGenderList: function () {

        	var list = AJService.GetDataFromAPI("master/getGenderList", null);
        	return list;
        }
		,
        GetLanguageList: function () {

        	var list = AJService.GetDataFromAPI("master/getLanguageList", null);
        	return list;
        }
		,
        GetProfessionList: function () {

        	var list = AJService.GetDataFromAPI("master/getProfessionList", null);
        	return list;
        }
		,
        GetHobbiesList: function () {

        	var list = AJService.GetDataFromAPI("master/getHobbiesList", null);
        	return list;
        }
		,
        GetAreaOfInterestList: function () {

        	var list = AJService.GetDataFromAPI("master/getAreaOfInterestList", null);
        	return list;
        }
		,
        GetStateList: function () {

        	var list = AJService.GetDataFromAPI("master/getStateList", null);
        	return list;
        }
		,
        getCountryList: function () {

        	var list = AJService.GetDataFromAPI("geography/getcountrylist", null);
        	return list;
        }

    };
});