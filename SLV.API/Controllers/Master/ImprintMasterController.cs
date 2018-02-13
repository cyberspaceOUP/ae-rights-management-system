using System;
using System.Linq;
using System.Web.Http;
using ACS.Core.Domain.Master;
using ACS.Services.Localization;
using ACS.Services.Master;
using ACS.Core;
using SLV.API.Controllers.JsonSerializer;
using ACS.Core.Data;
using ACS.Data;
using ACS.Core.Domain.Product;

namespace SLV.API.Controllers.Master
{
    public class ImprintMasterController : ApiController
    {
        #region Private Properties
        //Added by Ankush Kumar on 13/07/2016
        private readonly IImprintService _imprintService;
        private readonly ILocalizationService _localizationService;
        private readonly IDbContext _dbContext;
        private readonly IPublishingCompanyService _publishingCompanyService;
        //Added by Suranjana on 25/07/2016
        private readonly IRepository<ImprintMaster> _mobjImprintRepository;
        private readonly IRepository<ACS.Core.Domain.Product.ProductMaster> _mobjProductRepository;
        private readonly IRepository<ProprietorMaster> _mobjProprietorRepository;
        private readonly IRepository<PublishingCompanyMaster> _mobjPublishingCompanyRepository;
        #endregion

        #region Constructor
        public ImprintMasterController(
            IImprintService ImprintServiceService,
             IDbContext dbContext,
             ILocalizationService localizationService,
            IPublishingCompanyService publishingCompanyService,
            IRepository<ImprintMaster> objImprintRepository,
            IRepository<ACS.Core.Domain.Product.ProductMaster> mobjProductRepository,
            IRepository<ProprietorMaster> mobjProprietorRepository,
            IRepository<PublishingCompanyMaster> mobjPublishingCompanyRepository
            )
        {
            _imprintService = ImprintServiceService;
            _localizationService = localizationService;
            this._dbContext = dbContext;
            _publishingCompanyService = publishingCompanyService;
            _mobjImprintRepository = objImprintRepository;
            _mobjProductRepository = mobjProductRepository;
            _mobjProprietorRepository = mobjProprietorRepository;
            _mobjPublishingCompanyRepository = mobjPublishingCompanyRepository;
        }
        #endregion

        #region Api Methods
        /// <summary>
        /// Api method to insert SubsidiaryRightsMaster
        /// </summary>
        /// <param name="TypeOfRights">accepts SubsidiaryRightsMaster object as paramater </param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult InsertImprint(ImprintMaster Imprint)
        {
            string status = "";
            try
            {
                status = _imprintService.DuplicityCheck(Imprint);
                if (status == "Y")
                {
                    if (Imprint.Id == 0)
                    {
                        _imprintService.InsertImprint(Imprint);
                        status = _localizationService.GetResource("Master.API.Success.Message");
                    }

                    else
                    {
                        ImprintMaster _Imprint = _imprintService.GetImprintById(Imprint.Id);
                        _Imprint.PublishingCompanyId = Imprint.PublishingCompanyId;
                        _Imprint.ImprintName = Imprint.ImprintName;
                        _Imprint.ModifiedBy = Imprint.EnteredBy;
                        _Imprint.ModifiedDate = DateTime.Now;
                        _imprintService.UpdateImprint(_Imprint);
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
        /// Api method to delete GeographicalMaster
        /// </summary>
        /// <param name="TypeOfRights">accepts GeographicalMaster object as paramater </param>
        /// <returns></returns>
        /// 
        [HttpPost]
        public IHttpActionResult DeleteImprint(ImprintMaster Imprint)
        {

            string status = string.Empty;
            try
            {
                ImprintMaster _Imprint = _imprintService.GetImprintById(Imprint.Id);
                _Imprint.Deactivate = "Y";
                _Imprint.DeactivateBy = Imprint.EnteredBy;
                _Imprint.DeactivateDate = DateTime.Now;
                _imprintService.UpdateImprint(_Imprint);

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

        /// <summary>
        /// Api method to get element by id
        /// </summary>
        /// <param name="TypeOfRights">accepts SubsidiaryRightsMaster object as paramater </param>
        /// <returns></returns>
        /// 
        [HttpGet]
        public IHttpActionResult getImprint(int Id)
        {
            ImprintMaster _imprintMaster = _imprintService.GetImprintById(Id);
            return Json(SerializeObj.SerializeObject(new { _imprintMaster }));
        }

        [HttpGet]
        public IHttpActionResult GetImprintList()
        {
            return Json(_imprintService.GetImprintList().ToList());
        }

        [HttpGet]
        public IHttpActionResult GetPublishingCompanyImprintList()
        {
            //var ImprintList = _imprintService.GetImprintList().ToList();
            //var PublishingCompany = _publishingCompanyService.GetAllPublishingCompany().ToList();
            //var _final = ImprintList.Join(PublishingCompany, p => p.PublishingCompanyId, s => s.Id, (p, s) =>
            //            new
            //            {

            //                Id = p.Id,
            //                ImprintName = p.ImprintName,
            //                PublishingCompanyName = s.CompanyName
            //            });



            //return Json(_final);

            var mvarPublishingComapanyList = (from I in _mobjImprintRepository.Table.Where(a => a.Deactivate == "N")

                                              join product in _mobjProductRepository.Table.Where(a => a.Deactivate == "N")
                                                     on I.Id equals product.ImprintId into productGroup
                                              from P in productGroup.DefaultIfEmpty()

                                              join proprietor in _mobjProprietorRepository.Table.Where(a => a.Deactivate == "N")
                                                     on I.Id equals proprietor.ProprietorImPrintId into proprietorGroup
                                              from Pr in proprietorGroup.DefaultIfEmpty()

                                              join pub in _mobjPublishingCompanyRepository.Table.Where(a => a.Deactivate == "N")
                                                 on I.PublishingCompanyId equals pub.Id into pubGroup
                                              from A in pubGroup.DefaultIfEmpty()
                                              select new
                                                 {
                                                     Id = I.Id,
                                                     ImprintName = I.ImprintName,
                                                     PublishingCompanyName = A.CompanyName,
                                                     Flag = P.ImprintId != null || Pr.ProprietorImPrintId != null || A.PublishingCompanyCode == "PCM0002" ? "1" : "0",
                                                     IsEditable = A.PublishingCompanyCode != "PCM0002" ? "0" : "1",
                                                     Deactivate = I.Deactivate 
                                                 }).Distinct().Where(a => a.Deactivate == "N").OrderBy(o => o.ImprintName);
            return Json(mvarPublishingComapanyList);
        } 
        #endregion
    }
}