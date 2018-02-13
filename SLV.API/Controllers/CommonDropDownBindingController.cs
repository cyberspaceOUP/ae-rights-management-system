using ACS.Core.Domain.Master;
using ACS.Services.Logging;
using ACS.Services.Localization;
using ACS.Services.Common;
using ACS.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SLV.API.Controllers
{
    public class CommonDropDownBindingController : ApiController
    {
        private readonly ICommonDropDown _CommonDDLService;
        //private readonly ILocalizationService _localizationService;
        //private readonly ILogger _loggerService;


        //public CommonDropDownBindingController(ICommonDropDown CommonDDL, ILocalizationService localizationService, ILogger loggerService)
        public CommonDropDownBindingController(ICommonDropDown CommonDDL)
        {
            _CommonDDLService = CommonDDL;
            //_localizationService = localizationService;
            //_loggerService = loggerService;
        }

        // get all salutation list from MasterValue table
        [HttpPost]
        public IHttpActionResult GeographicalUserControl(GeographicalMaster geog)
        {
           return Json(_CommonDDLService.GetAllGeog(geog).ToList());
        }


    }

}
