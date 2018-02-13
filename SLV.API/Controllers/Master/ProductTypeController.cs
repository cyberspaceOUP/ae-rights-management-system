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
using ACS.Core.Data;

namespace SLV.API.Controllers.Master
{
    public class ProductTypeController : ApiController
    {
        #region Private Properties
        private readonly IProductType _productTypeService;
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _loggerService;
        private readonly IRepository<ProductTypeMaster> _mobjProductTypeMaster;
        private readonly IRepository<ACS.Core.Domain.Product.ProductMaster> _mobjProductMaster;
        #endregion

        #region Constructor
        public ProductTypeController(IProductType productTypeService, ILocalizationService localizationService, ILogger loggerService,
            IRepository<ProductTypeMaster> mobjProductTypeMaster,
            IRepository<ACS.Core.Domain.Product.ProductMaster> mobjProductMaster)
        {
            _productTypeService = productTypeService;
            _localizationService = localizationService;
            _loggerService = loggerService;
            _mobjProductTypeMaster = mobjProductTypeMaster;
            _mobjProductMaster = mobjProductMaster;
        } 
        #endregion

        #region Api Methods
        //Added by sanjeet on 16th may 2016
        public IHttpActionResult InsertProductType(ProductTypeMaster ProductType)
        {

            string status = "";
            try
            {

                if (ProductType.parenttypeid == null)
                {
                    ProductType.typelevel = 1;
                }
                else
                {
                    ProductType.typelevel = 2;
                }
                status = _productTypeService.DuplicityCheck(ProductType);
                if (status == "Y")
                {
                    if (ProductType.Id == 0)
                    {
                        _productTypeService.InsertProductType(ProductType);

                    }
                    else
                    {
                        ProductTypeMaster mobj_producttype = _productTypeService.GetProductTypeById(ProductType);
                        mobj_producttype.typeName = ProductType.typeName;
                        mobj_producttype.typelevel = ProductType.typelevel;
                        mobj_producttype.parenttypeid = ProductType.parenttypeid;
                        mobj_producttype.ModifiedBy = ProductType.EnteredBy;
                        mobj_producttype.ModifiedDate = System.DateTime.Now;
                        _productTypeService.UpdateProductType(mobj_producttype);

                    }
                    status = _localizationService.GetResource("Master.API.Success.Message");

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

        public IHttpActionResult ProductTypeDelete(ProductTypeMaster _productType)
        {
            string status = string.Empty;
            try
            {

                ProductTypeMaster productType = _productTypeService.GetProductTypeById(_productType);
                productType.Deactivate = "Y";
                productType.DeactivateBy = _productType.EnteredBy;
                productType.DeactivateDate = DateTime.Now;
                _productTypeService.UpdateProductType(productType);
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

        public IHttpActionResult ProductType(ProductTypeMaster _productType)
        {
            ProductTypeMaster productType = _productTypeService.GetProductTypeById(_productType);
            return Json(productType);
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
            return Json(_productTypeService.GetAllProductType().ToList());
        }
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

            IList<ProductTypeMaster> subProductList = _productTypeService.GetSubProductType().ToList();

            var subProductData = subProductList.Select(p => new
            {
                subProductId = p.Id,
                subProductName = p.typeName,
                productName = p.ProductTypeM.typeName
            });
            return Json(SerializeObj.SerializeObject(new { subProductData = subProductData }));
        }

        #endregion

        //Added by Suranjana on 21/07/2016
        #region Get ProductType List and SubProductType List for Master Only
        /// <summary>
        /// Api Method to get ProductType List
        /// </summary>
        /// <returns>Returns list of ProductType</returns>
        [HttpGet]
        public IHttpActionResult GetProductTypeListForMaster()
        {
            var mvarProductTypeList = (from p in _mobjProductTypeMaster.Table.Where(a => a.Deactivate == "N")

                                       join d in _mobjProductMaster.Table.Where(a => a.Deactivate == "N")
                                       on p.Id equals d.ProductTypeId into output
                                       from j in output.DefaultIfEmpty()
                                       select new
                                       {
                                           id = p.Id,
                                           typeName = p.typeName,
                                           Flag = j.ProductTypeId != null ? "1" : "0",
                                           TypeLevel = p.typelevel
                                       }).Distinct().Where(a => a.TypeLevel == 1);
            return Json(mvarProductTypeList);
        }

        /// <summary>
        /// Api Method to get SubProductType List
        /// </summary>
        /// <returns>Returns list of SubProductType</returns>
        [HttpGet]
        public IHttpActionResult GetSubProductTypeListFroMaster()
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
        //End

        #endregion
    }
}
