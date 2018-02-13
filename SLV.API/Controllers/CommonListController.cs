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
using System.Data;

using System.Data.SqlClient;
using ACS.Data;
using ACS.Core.Data;
using ACS.Core.Domain.Product;
using ACS.Core.Domain.Product;
using SLV.Model.Product;

namespace SLV.API.Controllers
{
    public class CommonListController : ApiController
    {
        #region Private Properties
        private readonly ICommonListService _CommonListService;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _loggerService;
        private readonly IDbContext _dbContext;
        private readonly IRepository<ACS.Core.Domain.Product.ProductMaster> _ProductMaster;
        private readonly IRepository<ACS.Core.Domain.Master.ProductTypeMaster> _ProductTypeMaster;
        private readonly IRepository<ImprintMaster> _ImprintRepository;
        private readonly IRepository<PublishingCompanyMaster> _PublishingCompanyRepository;
        private readonly IRepository<PubCenterMaster> _PubCenterRepository;
        #endregion

        #region Constructor
        public CommonListController(ICommonListService CommonListService, ILocalizationService localizationService,
            ILogger loggerService, IDbContext dbContext, IRepository<ACS.Core.Domain.Product.ProductMaster> ProductMaster,
            IRepository<ACS.Core.Domain.Master.ProductTypeMaster> ProductTypeMaster,
            IRepository<ImprintMaster> ImprintRepository, IRepository<PublishingCompanyMaster> PublishingCompanyRepository,
            IRepository<PubCenterMaster> PubCenterRepository)
        {
            _CommonListService = CommonListService;
            _ImprintRepository = ImprintRepository;
            _PublishingCompanyRepository = PublishingCompanyRepository;
            _localizationService = localizationService;
            _loggerService = loggerService;
            this._dbContext = dbContext;
            _ProductMaster = ProductMaster;
            _ProductTypeMaster = ProductTypeMaster;
            _PubCenterRepository = PubCenterRepository;
        }
        #endregion

        #region Api Methods
        // get all salutation list from MasterValue table
        [HttpGet]
        public IHttpActionResult getDepartmentList()
        {
            return Json(_CommonListService.GetAllDepartments().ToList());
        }

        [HttpGet]
        public IHttpActionResult getDivisionList(int Id)
        {
            return Json(_CommonListService.GetAllDivisions(Id).ToList());
        }


        [HttpGet]
        public IHttpActionResult getSubDivisionList()
        {
            IList<DivisionMaster> _divisionList = _CommonListService.GetAllSubDivisions().ToList();
            var divisionData = _divisionList.Select(dl => new
                {
                    DivisionId = dl.Id,
                    SubDivisionName = dl.divisionName,
                    DivisionName = dl.parentdivision.divisionName
                });
            return Json(SerializeObj.SerializeObject(new { divisionData = divisionData }));
        }

        #region SubDivisionListByDivisionId
        /// <summary>
        /// Description	      :SubDevision List By DevisionId
        /// Function Name     :SubDivisionListByDivisionId
        /// OutPut parameter  :DataSet
        /// Create Date	      : 11 May 2016
        /// Author Name	      : Vishal Verma
        /// </summary>   
        /// <remarks>
        ///****************************  Modification History  *************************************************************
        /// Sr No:	Date		    Modified by	    Tracker                 Description
        ///*****************************************************************************************************************
        ///*****************************************************************************************************************
        /// 
        ///</remarks>
        public IHttpActionResult SubDivisionListByDivisionId(DivisionMaster Division)
        {
            IList<DivisionMaster> _divisionList = _CommonListService.GetAllSubDivisionsbyDivisonId(Division).ToList();
            var divisionData = _divisionList.Select(dl => new
            {
                SubDivisionId = dl.Id,
                SubDivisionName = dl.divisionName
            });
            return Json(SerializeObj.SerializeObject(new { divisionData = divisionData }));
        }
        #endregion


        //Added by Saddam on 05/05/2016
        [HttpGet]
        public IHttpActionResult getExecutiveList()
        {
            var query = _CommonListService.GetAllExecutive().ToList();
            return Json(SerializeObj.SerializeObject(new { query }));
        }


        //

        ////Added by Saddam on 09/05/2016
        //[HttpGet]
        //public IHttpActionResult getAuthorList()
        //{
        //    return Json(_CommonListService.GetAllAuthor().ToList());
        //}


        public IHttpActionResult AuthorSuggesationList(AuthorMaster AuthorMaster)
        {
            IList<AuthorMaster> _AuthorList = _CommonListService.GetAuthorSuggesationList(AuthorMaster).ToList();
            var AuthorSuggesation = _AuthorList.Select(Au => new
            {
                AuthorId = Au.Id,
                AuthorName = Au.FirstName + " " + Au.LastName,
                AuthorCode = Au.AuthorCode,
                AuthorSAPCode = Au.AuthorSAPCode,
            });
            return Json(SerializeObj.SerializeObject(new { AuthorSuggesation }));
        }


        #region getAllProductTypeList
        /// <summary>
        /// Description	      :getAllProductTypeList
        /// Function Name     :getProductTypeList
        /// OutPut parameter  :Json Object
        /// Create Date	      : 17 May 2016
        /// Author Name	      : Sanjeet singh
        /// </summary>   
        /// <remarks>
        ///****************************  Modification History  *************************************************************
        /// Sr No:	Date		    Modified by	    Tracker                 Description
        ///*****************************************************************************************************************
        ///*****************************************************************************************************************
        /// 
        ///</remarks>
        ///

        [HttpGet]
        public IHttpActionResult getProductTypeList()
        {
            return Json(_CommonListService.GetAllProductType().ToList());
        }



        //[HttpGet]
        //public IHttpActionResult getProductTypeList()
        //{
        //    IList<ProductTypeMaster> mobj_procductType = _CommonListService.GetAllProductType().ToList();


        //    //var stateList = _ProductMaster.Table.Where(a => a.Deactivate == "N");
        //    //var CountryList = _CommonListService.GetAllProductType().ToList();
        //    //var ListProduct = stateList.Join(CountryList, p => p.ProductTypeId, s => s.Id, (p, s) =>
        //    //            new
        //    //            {

        //    //                Id = p.Id,
        //    //                typeName = s.typeName,
        //    //                Flag = j.ProductTypeId != null ? "1" : "0"
        //    //            }).Distinct();



        //    //return Json(ListProduct);

        //    var leftList = (from emp in _ProductTypeMaster.Table.Where(a => a.Deactivate == "N")

        //                    join d in _ProductMaster.Table.Where(a => a.Deactivate == "N")
        //                    on emp.Id equals d.ProductTypeId into output
        //                    from j in output.DefaultIfEmpty()
        //                    select new
        //                    {
        //                        id = emp.Id,
        //                        typeName = emp.typeName,
        //                        Flag = j.ProductTypeId != null ? "1" : "0",
        //                        TypeLavel = emp.typelevel
        //                    }).Distinct().Where(a => a.TypeLavel==1);

        //    return Json(leftList);

        //}
        #endregion


        #region getAllSubProductTypeList
        /// <summary>
        /// Description	      :getAllSubProductTypeList
        /// Function Name     :getSubProductTypeList
        /// OutPut parameter  :Json Object
        /// Create Date	      : 17 May 2016
        /// Author Name	      : Sanjeet singh
        /// </summary>   
        /// <remarks>
        ///****************************  Modification History  *************************************************************
        /// Sr No:	Date		    Modified by	    Tracker                 Description
        ///*****************************************************************************************************************
        ///*****************************************************************************************************************
        /// 
        ///</remarks>
        ///
        [HttpGet]
        public IHttpActionResult getSubProductTypeList()
        {

            IList<ProductTypeMaster> subProductList = _CommonListService.GetSubProductType().ToList();

            var subProductData = subProductList.Select(p => new
            {
                subProductId = p.Id,
                subProductName = p.typeName,
                productName = p.ProductTypeM.typeName
            });
            return Json(SerializeObj.SerializeObject(new { subProductData = subProductData }));

            //var leftList = (from emp in _ProductTypeMaster.Table.Where(a => a.Deactivate == "N")

            //                join d in _ProductMaster.Table.Where(a => a.Deactivate == "N")
            //                on emp.Id equals d.SubProductTypeId into output
            //                from j in output.DefaultIfEmpty()
            //                select new
            //                {
            //                    id = emp.Id,
            //                    typeName = emp.typeName,
            //                    Flag = j.SubProductTypeId != null ? "1" : "0",
            //                    TypeLavel = emp.typelevel,
            //                }).Distinct().Where(a => a.TypeLavel == 2);

            //return Json(leftList);
        }

        #endregion

        #region getAllProductCategoryList
        /// <summary>
        /// Description	      :getAllProductCategoryList
        /// Function Name     :getAllProductCategoryList
        /// OutPut parameter  :Json Object
        /// Create Date	      : 12 May 2016
        /// Author Name	      : Vishal Verma
        /// </summary>   
        /// <remarks>
        ///****************************  Modification History  *************************************************************
        /// Sr No:	Date		    Modified by	    Tracker                 Description
        ///*****************************************************************************************************************
        ///*****************************************************************************************************************
        /// 
        ///</remarks>
        ///

        [HttpGet]
        public IHttpActionResult getAllProductCategoryList()
        {
            return Json(_CommonListService.GetAllProductCategory().ToList());
        }

        #endregion









        #region getAllProductTypeList
        /// <summary>
        /// Description	      :getSubProductTypeList
        /// Function Name     :getSubProductTypeList
        /// OutPut parameter  :Json Object
        /// Create Date	      : 12 May 2016
        /// Author Name	      : Vishal Verma
        /// </summary>   
        /// <remarks>
        ///****************************  Modification History  *************************************************************
        /// Sr No:	Date		    Modified by	    Tracker                 Description
        ///*****************************************************************************************************************
        ///*****************************************************************************************************************
        /// 
        ///</remarks>
        ///


        public IHttpActionResult AllProductTypeList()
        {


            return Json(_CommonListService.GetAllProductType().ToList());
        }


        #endregion

        #region SubProductTypeList
        /// <summary>
        /// Description	      :getSubProductTypeList
        /// Function Name     :getSubProductTypeList
        /// OutPut parameter  :Json Object
        /// Create Date	      : 12 May 2016
        /// Author Name	      : Vishal Verma
        /// </summary>   
        /// <remarks>
        ///****************************  Modification History  *************************************************************
        /// Sr No:	Date		    Modified by	    Tracker                 Description
        ///*****************************************************************************************************************
        ///*****************************************************************************************************************
        /// 
        ///</remarks>
        ///


        public IHttpActionResult SubProductTypeList(ProductTypeMaster ProductType)
        {
            return Json(_CommonListService.GetAllSubProductType(ProductType).ToList());
        }


        #endregion

        #region getImprintList
        /// <summary>
        /// Description	      :getImprintList
        /// Function Name     :getImprintList
        /// OutPut parameter  :Json Object
        /// Create Date	      : 12 May 2016
        /// Author Name	      : Vishal Verma
        /// </summary>   
        /// <remarks>
        ///****************************  Modification History  *************************************************************
        /// Sr No:	Date		    Modified by	    Tracker                 Description
        ///*****************************************************************************************************************
        ///*****************************************************************************************************************
        /// 
        ///</remarks>
        ///

        [HttpGet]
        public IHttpActionResult getImprintList()
        {
            return Json(_CommonListService.GetImprintList().ToList());
        }


        #endregion

        #region getLanguageList
        /// <summary>
        /// Description	      :getLanguageList
        /// Function Name     :getLanguageList
        /// OutPut parameter  :Json Object
        /// Create Date	      : 12 May 2016
        /// Author Name	      : Vishal Verma
        /// </summary>   
        /// <remarks>
        ///****************************  Modification History  *************************************************************
        /// Sr No:	Date		    Modified by	    Tracker                 Description
        ///*****************************************************************************************************************
        ///*****************************************************************************************************************
        /// 
        ///</remarks>
        ///

        [HttpGet]
        public IHttpActionResult getLanguageList()
        {
            return Json(_CommonListService.GetLanguageList().ToList());
        }


        #endregion

        #region getCurrencyList
        /// <summary>
        /// Description	      :getCurrencyList
        /// Function Name     :getCurrencyList
        /// OutPut parameter  :Json Object
        /// Create Date	      : 12 May 2016
        /// Author Name	      : Vishal Verma
        /// </summary>   
        /// <remarks>
        ///****************************  Modification History  *************************************************************
        /// Sr No:	Date		    Modified by	    Tracker                 Description
        ///*****************************************************************************************************************
        ///*****************************************************************************************************************
        /// 
        ///</remarks>
        ///

        [HttpGet]
        public IHttpActionResult getCurrencyList()
        {
            return Json(_CommonListService.GetCurrencyList().ToList());
        }


        #endregion

        #region GetPublishingCompanyList
        /// <summary>
        /// Description	      :GetPublishingCompanyList
        /// Function Name     :GetPublishingCompanyList
        /// OutPut parameter  :Json Object
        /// Create Date	      : 12 May 2016
        /// Author Name	      : Vishal Verma
        /// </summary>   
        /// <remarks>
        ///****************************  Modification History  *************************************************************
        /// Sr No:	Date		    Modified by	    Tracker                 Description
        ///*****************************************************************************************************************
        ///*****************************************************************************************************************
        /// 
        ///</remarks>
        ///

        [HttpGet]
        public IHttpActionResult GetPublishingCompanyList()
        {
            return Json(_CommonListService.GetPublishingCompanyList().ToList());
        }


        #endregion

        #region PubCenterByCompanyIdList
        /// <summary>
        /// Description	      :GetPublishingCompanyList
        /// Function Name     :GetPublishingCompanyList
        /// OutPut parameter  :Json Object
        /// Create Date	      : 12 May 2016
        /// Author Name	      : Vishal Verma
        /// </summary>   
        /// <remarks>
        ///****************************  Modification History  *************************************************************
        /// Sr No:	Date		    Modified by	    Tracker                 Description
        ///*****************************************************************************************************************
        ///*****************************************************************************************************************
        /// 
        ///</remarks>
        ///


        public IHttpActionResult PubCenterByCompanyIdList(PublishingCompanyMaster PublishingCompany)
        {
            return Json(_CommonListService.GetPubCenterByCompanyIdList(PublishingCompany).ToList());
        }
        
        //Added by Prakash on 07/11/2016
        #region GetImprintListByPublishingCompany
        /// <summary>
        /// Method to get Imprint List Depend on Publishing Company
        /// </summary>
        /// <returns>returns list of imprint list</returns>
        [HttpPost]
        public IHttpActionResult GetImprintListByPublishingCompany(PublishingCompanyMaster PublishingCompany)
        {
            var ImprintList = (from I in _ImprintRepository.Table.Where(a => a.Deactivate == "N")
                               join pub in _PublishingCompanyRepository.Table.Where(a => a.Deactivate == "N")
                               on I.PublishingCompanyId equals pub.Id into pubGroup
                               from S in pubGroup.DefaultIfEmpty()
                               where I.PublishingCompanyId == PublishingCompany.Id
                               select new
                               {
                                   Id = I.Id,
                                   ImprintName = I.ImprintName,
                                   PublishingCompanyCode = S.PublishingCompanyCode,
                                   Deactivate = I.Deactivate,
                               }).Distinct().Where(a => a.Deactivate == "N" && a.PublishingCompanyCode != "PCM0002").OrderBy(o => o.ImprintName);
            return Json(ImprintList);
        }
        #endregion
        
        #endregion

        #region SeriesListbyDivisionSubDivisionId
        /// <summary>
        /// Description	      :Series List by SubDivisionId
        /// Function Name     :Series List by SubDivisionId
        /// OutPut parameter  :DataSet
        /// Create Date	      : 23 May 2016
        /// Author Name	      : Vishal Verma
        /// </summary>   
        /// <remarks>
        ///****************************  Modification History  *************************************************************
        /// Sr No:	Date		    Modified by	    Tracker                 Description
        ///*****************************************************************************************************************
        ///*****************************************************************************************************************
        /// 
        ///</remarks>
        public IHttpActionResult SeriesListbyDivisionSubDivisionId(SeriesMaster Series)
        {
            IList<SeriesMaster> _SeriesList = _CommonListService.GetSeriesList(Series).Where(x => x.Deactivate == "N").ToList();
            var SeriesData = _SeriesList.Select(sr => new
            {
                SeriesId = sr.Id,
                Id = sr.Id,
                SeriesName = sr.Seriesname
            });
            return Json(SerializeObj.SerializeObject(new { SeriesData }));
        }
        #endregion


        [HttpGet]
        public IHttpActionResult getPaymentPeriodList()//Without Payment as per Schedule
        {
            var query = _CommonListService.GetAllPamentPeriod().ToList();
            return Json(SerializeObj.SerializeObject(new { query }));
        }

        [HttpGet]
        public IHttpActionResult getAllPaymentPeriodList()
        {
            var query = _CommonListService.GetAllPamentPeriodList().ToList();
            return Json(SerializeObj.SerializeObject(new { query }));
        }

        [HttpGet]
        public IHttpActionResult getContractTypeList()
        {
            var query = _CommonListService.GetAllContractType().ToList();
            return Json(SerializeObj.SerializeObject(new { query }));
        }


        [HttpGet]
        public IHttpActionResult getServiceList()
        {
            var query = _CommonListService.GetAllServiceList().ToList();
            return Json(SerializeObj.SerializeObject(new { query }));
        }

        [HttpGet]
        public IHttpActionResult getSubServiceList()
        {
            var query = _CommonListService.GetAllSubServiceList().ToList();
            return Json(SerializeObj.SerializeObject(new { query }));
        }


        [HttpGet]
        public IHttpActionResult getTerriteryRights()
        {
            var query = _CommonListService.TerriteryRights().ToList();
            return Json(SerializeObj.SerializeObject(new { query }));
        }

        [HttpGet]
        public IHttpActionResult getAuthorTypeList()
        {
            var query = _CommonListService.GetAllAuthorTypeList().ToList();
            return Json(SerializeObj.SerializeObject(new { query }));
        }

        [HttpGet]
        public IHttpActionResult getAuthorList()
        {
            var query = _CommonListService.GetAllAuthor().ToList().Select(i => new
            {
                Id = i.Id,
                FirstName = i.FirstName,
                LastName = i.LastName
            });

            query = query.OrderBy(x => x.FirstName);

            return Json(SerializeObj.SerializeObject(new { query }));
        }

        [HttpGet]
        public IHttpActionResult SubServiceListByServiceId(string ServiceId)
        {
            if (ServiceId != "undefined" && ServiceId != "")
            {
                var query = _CommonListService.GetAllSubServiceListByServiceId(int.Parse(ServiceId)).ToList();
                return Json(SerializeObj.SerializeObject(new { query }));
            }
            else
            {
                ServiceId = "0";
                var query = _CommonListService.GetAllSubServiceListByServiceId(int.Parse(ServiceId)).ToList();
                return Json(SerializeObj.SerializeObject(new { query }));
            }


        }


        [HttpPost]
        public IHttpActionResult GetISBNList(ProductTypeMaster Ptype)
        {
            var _list =  _CommonListService.GetAllISBNByProductTypeId(Ptype.Id).Where(a => a.Blocked == null).ToList();
            var query = _list.Select(i => new
            {
                Id = i.Id,
                ISBN = i.ISBN,
            }).OrderBy(a => a.Id);
            return Json(SerializeObj.SerializeObject(new { query }));


            
        }

        [HttpPost]
        public IHttpActionResult GetAllISBNBagList(int productType)
        {
            var _list = _CommonListService.GetAllISBNBagList().Where(a => a.Blocked == null && a.ProductTypeid == productType).ToList();
            var query = _list.Select(i => new
            {
                Id = i.Id,
                ISBN = i.ISBN,
            }).OrderBy(a => a.Id);
            return Json(SerializeObj.SerializeObject(new { query }));

        }

        /// <summary>
        /// Method to get PubCenter List for OUP Details
        /// </summary>
        /// <returns>returns list of PubCenter name</returns>
        [HttpGet]
        public IHttpActionResult getOUPPubCenterList()
        {
            //PublishingCompanyMaster PublishingCompany = _CommonListService.GetPublishingCompanyList().ToList().FirstOrDefault();
            //return Json(_CommonListService.GetPubCenterByCompanyIdList(PublishingCompany).ToList());

            //Modified by Suranjana on 26/07/2016
            var PubCenterList = (from I in _PubCenterRepository.Table.Where(a => a.Deactivate == "N")
                                 join pub in _PublishingCompanyRepository.Table.Where(a => a.Deactivate == "N")
                                 on I.PublishingCompanyid equals pub.Id into pubGroup
                                 from S in pubGroup.DefaultIfEmpty()

                                 select new
                                 {
                                     Id = I.Id,
                                     CenterName = I.CenterName,
                                     PublishingCompanyCode = S.PublishingCompanyCode,
                                     Deactivate = I.Deactivate,
                                 }).Distinct().Where(a => a.Deactivate == "N" && a.PublishingCompanyCode == "PCM0002").OrderBy(o => o.CenterName);
            return Json(PubCenterList);
        }

        //Added by Suranjana on 26/07/2016
        #region GetImprintListForProprietorDetails
        /// <summary>
        /// Method to get Imprint List in case of Proprietor Details
        /// </summary>
        /// <returns>returns list of imprint list</returns>

        public IHttpActionResult GetImprintListForProprietorDetails()
        {
            var ImprintList = (from I in _ImprintRepository.Table.Where(a => a.Deactivate == "N")
                               join pub in _PublishingCompanyRepository.Table.Where(a => a.Deactivate == "N")
                               on I.PublishingCompanyId equals pub.Id into pubGroup
                               from S in pubGroup.DefaultIfEmpty()

                               select new
                               {
                                   Id = I.Id,
                                   ImprintName = I.ImprintName,
                                   PublishingCompanyCode = S.PublishingCompanyCode,
                                   Deactivate = I.Deactivate,
                               }).Distinct().Where(a => a.Deactivate == "N" && a.PublishingCompanyCode != "PCM0002").OrderBy(o => o.ImprintName);
            return Json(ImprintList);
        }
        #endregion
               

        #region getOUPImprintList
        /// <summary>
        /// Method gets all Imprint name as in OUP
        /// </summary>
        /// <returns>returns a list of ImprintMaster as in OUP</returns>
        [HttpGet]
        public IHttpActionResult getOUPImprintList()
        {
            var ImprintList = (from I in _ImprintRepository.Table.Where(a => a.Deactivate == "N")
                                 join pub in _PublishingCompanyRepository.Table.Where(a => a.Deactivate == "N")
                                 on I.PublishingCompanyId equals pub.Id into pubGroup
                                 from S in pubGroup.DefaultIfEmpty()

                                 select new
                                 {
                                     Id = I.Id,
                                     ImprintName = I.ImprintName,
                                     PublishingCompanyCode = S.PublishingCompanyCode,
                                     Deactivate = I.Deactivate,
                                 }).Distinct().Where(a => a.Deactivate == "N" && a.PublishingCompanyCode == "PCM0002").OrderBy(o => o.ImprintName);
            return Json(ImprintList);
        }
        #endregion
        //Ended by Suranjana on 26/07/2016 



        //Added By Ankush on 10/08/2016
        [HttpGet]
        public IHttpActionResult getFrequencyList()
        {
            return Json(_CommonListService.GetFrequencyList().ToList());
        }
        //Ended By Ankush on 10/08/2016
        #endregion

        //added by Prakash on 25 April, 2017
        [HttpPost]
        public IHttpActionResult GetProductISBNList()
        {
            //var query1 = _CommonListService.GetProductISBNList().ToList().Select(i => new
            //{
            //    Id = i.Id,
            //    ISBN = i.OUPISBN,
            //}).OrderBy(a => a.Id);

            var query = _dbContext.ExecuteStoredProcedureListNewData<ProductISBN>("Proc_GetProductISBNList_get").ToList();

            return Json(SerializeObj.SerializeObject(new { query }));
        }


    }
}
