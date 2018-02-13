using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Http;
using ACS.Core.Domain.Master;
using ACS.Services.Logging;
using ACS.Services.Localization;
using ACS.Services.Master;
using ACS.Services.Product;
using ACS.Core;
using SLV.Model.Common;
using Autofac.Integration.WebApi;
using SLV.API.Controllers.JsonSerializer;
using ACS.Core.Infrastructure;
using ACS.Core.Data;
using ACS.Core.Domain.Product;
using ACS.Data;
using System.Data;
using System.Data.SqlClient;
using SLV.Model.Product;
using ACS.Core.Domain.AuthorContract;
using System.Text;

namespace SLV.API.Controllers.ProductMaster
{
    public class SeriesProductEntryController : ApiController
    {
        private readonly ISeriesProductEntryService _SeriesProductEntryService;
        private readonly ILocalizationService _localizationService;
        private readonly IDbContext _dbContext;

        public SeriesProductEntryController(
            ISeriesProductEntryService SeriesProductEntryService
            , ILocalizationService localizationService
            , IDbContext dbContext)
        {
            _SeriesProductEntryService = SeriesProductEntryService;
            _localizationService = localizationService;
            _dbContext = dbContext;
        }
        //
        // GET: /SeriesProductEntry/
        //public ActionResult Index()
        //{
        //    return View();
        //}

        #region InsertSeries

        public IHttpActionResult InsertSeries(SeriesProductEntryModel Object)
        {

            string status = "notOK";
            //int Id = 0;
            int ProductId = 0;
            try
            {
                
                //Check Duplicate Value
                bool value = true;
                foreach (var dup in Object.testArr)
                {
                    if (value == true)
                    {
                        // get Ilist and check if any of the value contains duplicate WorkingProduct
                        //if (dup.WorkingProduct != "")
                        //{
                        //    status = _SeriesProductEntryService.DuplicateWorkingPro(dup.WorkingProduct);
                        //    if (status == "duplicate")
                        //    {
                        //        value = false;
                        //        status = "Duplicate WorkingProduct (" + dup.WorkingProduct + "). already exist !";
                        //        break;
                        //    }
                        //}

                        //// get Ilist and check if any of the value contains duplicate ISBN
                        //if (dup.OUPISBN != "")
                        //{
                        //    status = _SeriesProductEntryService.DuplicateISBNNo(dup.OUPISBN);
                        //    if (status == "duplicate")
                        //    {
                        //        value = false;
                        //        status = "Duplicate ISBN (" + dup.OUPISBN + "). already exist !";
                        //        break;
                        //    }
                        //}

                        // get Ilist and check OrgISBN has Author Contract or not
                        if (dup.OrgISBN != null)
                        {
                            IList<AuthorContractOriginal> AuthorContractList = _SeriesProductEntryService.CheckAuthorContract(dup.OrgISBN);

                            if (AuthorContractList != null)
                            {
                                if (AuthorContractList.Count == 0)
                                {
                                    value = false;
                                    status = "Author Contract Not Avaiblable for " + dup.OrgISBN + " Derivative ISBN";
                                    break;
                                }
                            }

                            
                        }
                    }
                }

                // value "true" means no duplicate isbn found
                if (value == true)
                {

                    foreach (var item in Object.testArr)
                    {
                        //------------start Capital First Letter
                        string product_WorkingProduct = "";
                        string product_WorkingSubProduct = "";
                        StringBuilder sb;

                        if (item.WorkingProduct != null)
                        {
                            string str_WorkingProduct = item.WorkingProduct.Trim();
                            sb = new StringBuilder(str_WorkingProduct.Length);
                            bool capitalize = true;
                            foreach (char c in str_WorkingProduct)
                            {
                                sb.Append(capitalize ? Char.ToUpper(c) : Char.ToLower(c));
                                //capitalize = !Char.IsLetter(c);
                                capitalize = Char.IsWhiteSpace(c);
                            }
                            product_WorkingProduct = sb.ToString().Trim();
                        }

                        if (item.WorkingSubProduct != null)
                        {
                            string str_WorkingSubProduct = item.WorkingSubProduct.Trim();
                            sb = new StringBuilder(str_WorkingSubProduct.Length);
                            bool capitalize = true;
                            foreach (char c in str_WorkingSubProduct)
                            {
                                sb.Append(capitalize ? Char.ToUpper(c) : Char.ToLower(c));
                                //capitalize = !Char.IsLetter(c);
                                capitalize = Char.IsWhiteSpace(c);
                            }
                            product_WorkingSubProduct = sb.ToString().Trim();

                        }
                        //---------end Capital First Letter


                        //if (status != "duplicate")
                        //{
                        ACS.Core.Domain.Product.ProductMaster mobj_productMaster = new ACS.Core.Domain.Product.ProductMaster();
                        mobj_productMaster.DivisionId = item.DivisionId;
                        mobj_productMaster.SubdivisionId = item.SubdivisionId;
                        mobj_productMaster.ProductCategoryId = item.ProductCategoryId;
                        mobj_productMaster.ProductTypeId = item.ProductTypeId;
                        mobj_productMaster.SubProductTypeId = item.SubProductTypeId;
                        mobj_productMaster.SeriesId = item.SeriesId;
                        mobj_productMaster.OUPISBN = item.OUPISBN;
                        mobj_productMaster.WorkingProduct = item.WorkingProduct != null ? product_WorkingProduct : item.WorkingProduct;
                        mobj_productMaster.WorkingSubProduct = item.WorkingSubProduct != null ? product_WorkingSubProduct : item.WorkingSubProduct;
                        mobj_productMaster.OUPEdition = item.OUPEdition;
                        mobj_productMaster.ProjectedPublishingDate = item.ProjectPublishingDate;
                        mobj_productMaster.CopyrightYear = item.CopyrightYear;
                        mobj_productMaster.ImprintId = item.ImprintId;
                        mobj_productMaster.LanguageId = item.LanguageId;
                        mobj_productMaster.OrgISBN = item.OrgISBN;
                        mobj_productMaster.Deactivate = "N";
                        mobj_productMaster.EnteredBy = item.EnteredBy;
                        mobj_productMaster.EntryDate = DateTime.Now;
                        mobj_productMaster.ModifiedBy = null;
                        mobj_productMaster.ModifiedDate = null;
                        mobj_productMaster.DeactivateBy = null;
                        mobj_productMaster.DeactivateDate = null;
                        //mobj_productMaster.ProductCode = item.ProductCode;
                        mobj_productMaster.Derivatives = item.Derivatives;
                        mobj_productMaster.ProductCode = _SeriesProductEntryService.GetProductCode(item.ProductCategoryId);

                        mobj_productMaster.ProjectedPrice = item.ProjectedPrice;
                        mobj_productMaster.ProjectedCurrencyId = item.ProjectedCurrencyId;

                        mobj_productMaster.ThirdPartyPermission = item.ThirdPartyPermission;

                        ProductId = _SeriesProductEntryService.InsertSeriesProduct(mobj_productMaster);

                        if (ProductId != 0)
                        {
                            int j = 0;
                            ProductAuthorLink mobj_ProductAuthorLink = new ProductAuthorLink();
                            foreach (var auth in item.AuthorObject)
                            {
                                if (auth != null)
                                {
                                    mobj_ProductAuthorLink.AuthorId = auth.AuthorId;
                                    mobj_ProductAuthorLink.EnteredBy = item.EnteredBy;
                                    mobj_ProductAuthorLink.ProductId = ProductId;
                                    _SeriesProductEntryService.InsertProductAuthorLink(mobj_ProductAuthorLink);
                                }

                            }

                            IList<AuthorContractOriginal> AuthorContractList = _SeriesProductEntryService.CheckAuthorContract(item.OrgISBN);
                            if (AuthorContractList != null)
                            {
                                foreach (AuthorContractOriginal AuthorContract in AuthorContractList)
                                {
                                    ProductPreviousProductLink _ProductPreviousProductLink = new ProductPreviousProductLink();
                                    _ProductPreviousProductLink.ProductId = ProductId;
                                    _ProductPreviousProductLink.PreviousProductId = AuthorContract.ProductId;
                                    _ProductPreviousProductLink.AuthorContractId = AuthorContract.Id;
                                    _ProductPreviousProductLink.Deactivate = "N";
                                    _ProductPreviousProductLink.EnteredBy = item.EnteredBy;
                                    _ProductPreviousProductLink.EntryDate = DateTime.Now;
                                    _ProductPreviousProductLink.ModifiedBy = null;
                                    _ProductPreviousProductLink.ModifiedDate = null;
                                    _ProductPreviousProductLink.DeactivateBy = null;
                                    _ProductPreviousProductLink.DeactivateDate = null;
                                    _SeriesProductEntryService.InsertProductPreviousProductLink(_ProductPreviousProductLink);
                                }
                            }

                        }
                        //}
                    }
                    status = _localizationService.GetResource("Master.API.Success.Message");
                }
                //else
                //{
                //    status = "Duplicate";
                //}
            }
            catch (ACSException ex)
            {
                status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                status = ex.InnerException.Message;
            }
            return Json(new { status });
        }
        
        #endregion


    }
}