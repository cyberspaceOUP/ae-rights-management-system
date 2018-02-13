using ACS.Core.Data;
using ACS.Core.Domain.Master;
using ACS.Data;
using ACS.Services.Localization;
using ACS.Services.Logging;
using ACS.Services.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SLV.API.Controllers.Master
{
    public class ProductTypeMasterController : ApiController
    {
        #region Private Property
        private readonly ICommonListService _CommonListService;
        private readonly ILocalizationService _mobjLocalizationService;
        private readonly ILogger _mobjLoggerService;
        private readonly IDbContext _dbContext;
        private readonly IRepository<ProductTypeMaster> _mobjProductTypeMaster;
        private readonly IRepository<ACS.Core.Domain.Product.ProductMaster> _mobjProductMaster;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="mobjCommonListService">accepts  SubProductTypeService interface object as parameter</param>
        /// <param name="mobjLocalizationService">accepts LocalizationService interface object as parameter</param>
        public ProductTypeMasterController(ICommonListService mobjCommonListService,
                                      ILocalizationService mobjLocalizationService,
                                      ILogger mobjLoggerService,
                                      IDbContext dbContext,
                                      IRepository<ProductTypeMaster> mobjProductTypeMaster,
                                      IRepository<ACS.Core.Domain.Product.ProductMaster> mobjProductMaster)
        {
            _CommonListService = mobjCommonListService;
            _mobjLocalizationService = mobjLocalizationService;
            _mobjLoggerService = mobjLoggerService;
            _dbContext = dbContext;
            _mobjProductTypeMaster = mobjProductTypeMaster;
            _mobjProductMaster = mobjProductMaster;
        }
        #endregion

        #region Api Methods
        /// <summary>
        /// Api Method to get list of all ProductType
        /// </summary>
        /// <returns>list of all ProductType</returns>
        [HttpGet]
        public IHttpActionResult getProductTypeList()
        {
            IList<ProductTypeMaster> mobj_procductType = _CommonListService.GetAllProductType().ToList();

            var leftList = (from emp in _mobjProductTypeMaster.Table.Where(a => a.Deactivate == "N")

                            join d in _mobjProductMaster.Table.Where(a => a.Deactivate == "N")
                            on emp.Id equals d.ProductTypeId into output
                            from j in output.DefaultIfEmpty()
                            select new
                            {
                                id = emp.Id,
                                typeName = emp.typeName,
                                Flag = j.ProductTypeId != null ? "1" : "0",
                                TypeLavel = emp.typelevel
                            }).Distinct().Where(a => a.TypeLavel == 1);

            return Json(leftList);
        }
        #endregion
    }
}