using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ACS.Core.Domain.Master;
using ACS.Services.Logging;
using ACS.Services.Localization;
using ACS.Services.Master;
using ACS.Core;
using SLV.Model.Common;
using Autofac.Integration.WebApi;
using SLV.API.Controllers.JsonSerializer;
using ACS.Core.Infrastructure;
using System.Web.Http.Description;
using System.Transactions;
using ACS.Services.Security;
using ACS.Services.AuthorContract;
using ACS.Services.Product;

namespace SLV.API.Controllers.Master
{
    public class SubsidiaryRightsMasterController : ApiController
    {
        //Added by Ankush Kumar on 11/07/2016
        private readonly ISubsidiaryRightsService _subsidiaryRightsService;
        private readonly ILocalizationService _localizationService;
        private readonly IAuthorContractService _IAuthorContractService;
        private readonly IProductLicenseService _ProductLicenseService;

        public SubsidiaryRightsMasterController(
            ISubsidiaryRightsService subsidiaryRightsService,
             ILocalizationService localizationService,
            IAuthorContractService IAuthorContractService,
            IProductLicenseService productLicenseService
            )
        {
            _subsidiaryRightsService = subsidiaryRightsService;
            _localizationService = localizationService;
            _IAuthorContractService = IAuthorContractService;
            _ProductLicenseService = productLicenseService;
        }



        //Added By Ankush Kumar on 11/07/2016
        #region SubsidiaryRightsMaster

        /// <summary>
        /// Api method to get element by id
        /// </summary>
        /// <param name="TypeOfRights">accepts SubsidiaryRightsMaster object as paramater </param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public IHttpActionResult getSubsidiaryRights(int Id)
        {
            SubsidiaryRightsMaster _SubsidiaryRightsMaster = _subsidiaryRightsService.GetSubsidiaryRightsById(Id);
            return Json(SerializeObj.SerializeObject(new { _SubsidiaryRightsMaster }));
        }

        /// <summary>
        /// Api method to insert SubsidiaryRightsMaster
        /// </summary>
        /// <param name="TypeOfRights">accepts SubsidiaryRightsMaster object as paramater </param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult InsertSubsidiaryRights(SubsidiaryRightsMaster SubsidiaryRights)
        {
            string status = "";
            try
            {
                status = _subsidiaryRightsService.DuplicityCheck(SubsidiaryRights);
                if (status == "Y")
                {
                    if (SubsidiaryRights.Id == 0)
                    {
                        _subsidiaryRightsService.InsertSubsidiaryRights(SubsidiaryRights);
                        status = _localizationService.GetResource("Master.API.Success.Message");
                    }

                    else
                    {
                        SubsidiaryRightsMaster _SubsidiaryRights = _subsidiaryRightsService.GetSubsidiaryRightsById(SubsidiaryRights.Id);
                        _SubsidiaryRights.SubsidiaryRights = SubsidiaryRights.SubsidiaryRights;
                        _SubsidiaryRights.ModifiedBy = SubsidiaryRights.EnteredBy;
                        _SubsidiaryRights.ModifiedDate = DateTime.Now;
                        _subsidiaryRightsService.UpdateSubsidiaryRights(_SubsidiaryRights);
                        status = _localizationService.GetResource("Master.API.Success.Message");
                    }
                }
                else
                {
                    status = "Duplicate";
                }

            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }
            return Json(status);
        }


        /// <summary>
        /// Api method to update SubsidiaryRightsMaster
        /// </summary>
        /// <param name="TypeOfRights">accepts SubsidiaryRightsMaster object as paramater </param>
        /// <returns></returns>
        public IHttpActionResult UpdateSubsidiaryRights(SubsidiaryRightsMaster SubsidiaryRights)
        {
            string status = string.Empty;
            try
            {
                SubsidiaryRightsMaster _SubsidiaryRights = _subsidiaryRightsService.GetSubsidiaryRightsById(SubsidiaryRights.Id);
                _SubsidiaryRights.Deactivate = SubsidiaryRights.SubsidiaryRights;
                _SubsidiaryRights.ModifiedBy = SubsidiaryRights.EnteredBy;
                _SubsidiaryRights.ModifiedDate = DateTime.Now;
                _subsidiaryRightsService.UpdateSubsidiaryRights(SubsidiaryRights);
            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }

            return Json(status);
        }

        /// <summary>
        /// Api method to delete SubsidiaryRightsMaster
        /// </summary>
        /// <param name="TypeOfRights">accepts SubsidiaryRightsMaster object as paramater </param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public IHttpActionResult DeleteSubsidiaryRights(SubsidiaryRightsMaster SubsidiaryRights)
        {

            string status = string.Empty;
            try
            {
                SubsidiaryRightsMaster _SubsidiaryRights = _subsidiaryRightsService.GetSubsidiaryRightsById(SubsidiaryRights.Id);
                _SubsidiaryRights.Deactivate = "Y";
                _SubsidiaryRights.DeactivateBy = SubsidiaryRights.EnteredBy;
                _SubsidiaryRights.DeactivateDate = DateTime.Now;
                _subsidiaryRightsService.UpdateSubsidiaryRights(_SubsidiaryRights);

                status = "OK";
            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }

            return Json(status);
        }

        [HttpGet]
        public IHttpActionResult GetSubsidiaryRightsList()
        {
            var SubsidiaryRights = _subsidiaryRightsService.GetAllSubsidiaryRights().ToList();
            var AuthorContractSubsidiaryRightsList = _IAuthorContractService.GetAuthorContractSubsidiaryRightsList().ToList();
            var ProductLicenseSubsidiaryRights = _ProductLicenseService.GetProductLicenseSubsidiaryRightsList().ToList();

            var leftList = (from subsidiaryRights in SubsidiaryRights

                            //Check subsidiaryRights Used in AuthorContractSubsidiaryRightsList
                            join authorContractSubsidiaryRightsList in AuthorContractSubsidiaryRightsList
                            on subsidiaryRights.Id equals authorContractSubsidiaryRightsList.subsidiaryrightsid into authorContractSubsidiaryRightsList_New
                            from authorContractSubsidiaryRightsList in authorContractSubsidiaryRightsList_New.DefaultIfEmpty()

                            //Check subsidiaryRights Used in AuthorContractSubsidiaryRightsList
                            join productLicenseSubsidiaryRights in ProductLicenseSubsidiaryRights
                            on subsidiaryRights.Id equals productLicenseSubsidiaryRights.subsidiaryrightsid into productLicenseSubsidiaryRights_New
                            from productLicenseSubsidiaryRights in productLicenseSubsidiaryRights_New.DefaultIfEmpty()

                            select new
                            {
                                Id = subsidiaryRights.Id,
                                SubsidiaryRights = subsidiaryRights.SubsidiaryRights,
                                Flag = (productLicenseSubsidiaryRights != null ? productLicenseSubsidiaryRights.subsidiaryrightsid != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                         (authorContractSubsidiaryRightsList != null ? authorContractSubsidiaryRightsList.subsidiaryrightsid != 0 ? "1" : "0" : "0") == "1" ? "1" :
                                        "0"
                            }).Distinct().ToList();

            return Json(leftList);
            //return Json(_subsidiaryRightsService.GetAllSubsidiaryRights().ToList());
        }

        #endregion
        //Ended By Ankush Kumar
	}
}