using ACS.Core.Data;
using ACS.Core.Domain.Master;
using ACS.Data;
using ACS.Services.Localization;
using ACS.Services.Logging;
using ACS.Services.Master;
using System.Linq;
using System.Web.Http;

namespace SLV.API.Controllers.Master
{
    public class SubProductTypeMasterController : ApiController
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
        public SubProductTypeMasterController(ICommonListService mobjCommonListService,
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
        /// Api Method to get list of all SubProductType
        /// </summary>
        /// <returns>list of all SubProductType</returns>
        [HttpGet]
        public IHttpActionResult getSubProductTypeList()
        {
            var subProductTypeList = (from sub in _mobjProductTypeMaster.Table.Where(a => a.Deactivate == "N")

                                      join d in _mobjProductMaster.Table.Where(a => a.Deactivate == "N")
                                      on sub.Id equals d.SubProductTypeId into output
                                      from j in output.DefaultIfEmpty()
                                      select new
                                      {
                                          subProductId = sub.Id,
                                          subProductName = sub.typeName,
                                          productName = sub.ProductTypeM.typeName,
                                          typeLevel = sub.typelevel,
                                          flag = j.SubProductTypeId != null ? "1" : "0",
                                      }).Distinct().Where(a => a.typeLevel == 2);

            return Json(subProductTypeList);
        }
        #endregion
    }
}