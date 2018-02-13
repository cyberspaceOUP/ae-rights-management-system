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
using ACS.Core.Domain.AuthorContract;
using ACS.Services.AuthorContract;
using SLV.Model.AuthorContract;
using SLV.Model.Product;
using ACS.Core.Domain.RightsSelling;
using ACS.Services.RightsSelling;
using ACS.Services.PermissionsOutbound;
using System.Text;
using Logger;

namespace SLV.API.Controllers.ProductMaster
{
    public class ProductMasterController : ApiController
    {

        #region Variables
        private Logger.ILogger _ILog = LoggerFactory.getLogger();
        #endregion

        private readonly IProductMasterService _ProductMasterService;
        private readonly ILocalizationService _localizationService;
        private readonly IDbContext _dbContext;
        private readonly IAuthorContractService _IAuthorContractService;
        private readonly IProductLicenseService _IProductLicenseService;
        private readonly IRightsSelling _IRightsSelling;
        private readonly IPermissionsOutboundService _IPermissionsOutboundService;

        private readonly IRepository<ACS.Core.Domain.Product.ProductMaster> _ProductMaster;
        private readonly IRepository<SeriesMaster> _SeriesMaster;
        private readonly IRepository<ProductCategoryMaster> _ProductCategoryMaster;
        private readonly IRepository<AuthorContractOriginal> _AuthorContractOriginal;
        private readonly IRepository<ProductPreviousProductLink> _ProductPreviousProductLinkRepository;
        private readonly IRepository<KitISBN> _kitISBN;

        private readonly IRepository<ProductTypeMaster> _ProductTypeMaster;
        private readonly IRepository<ProductAuthorLink> _ProductAuthorLink;
        private readonly IRepository<AuthorMaster> _AuthorMaster;
        private readonly IRepository<ProductSAPAgreementMaster> _ProductSAPAgreementMaster;

        private readonly IRepository<ProductLicense> _ProductLicense;
        private readonly IAddendumServices _AddendumServices;
        private readonly IRepository<AddendumDetails> _AddendumDetails;


        public ProductMasterController(

             IProductMasterService ProductMasterService
              , ILocalizationService localizationService
            , IDbContext dbContext
            , IAuthorContractService IAuthorContractService
            , IProductLicenseService IProductLicenseService
            , IRightsSelling IRightsSelling
            , IPermissionsOutboundService IPermissionsOutboundService

            , IRepository<ACS.Core.Domain.Product.ProductMaster> ProductMaster
            , IRepository<SeriesMaster> SeriesMaster
            , IRepository<ProductCategoryMaster> ProductCategoryMaster
            , IRepository<AuthorContractOriginal> AuthorContractOriginal
            , IRepository<ProductPreviousProductLink> ProductPreviousProductLink
            , IRepository<ProductTypeMaster> ProductTypeMaster
            , IRepository<ProductAuthorLink> ProductAuthorLink
            , IRepository<AuthorMaster> AuthorMaster
            ,IRepository<ProductSAPAgreementMaster> ProductSAPAgreementMaster

            , IRepository<ProductLicense> ProductLicense
            ,IAddendumServices AddendumServices
            , IRepository<AddendumDetails> AddendumDetails

            )
        {

            _ProductMasterService = ProductMasterService;
            _localizationService = localizationService;
            this._dbContext = dbContext;
            _IAuthorContractService = IAuthorContractService;
            _IProductLicenseService = IProductLicenseService;
            _IRightsSelling = IRightsSelling;
            _IPermissionsOutboundService = IPermissionsOutboundService;

            _ProductMaster = ProductMaster;
            _SeriesMaster = SeriesMaster;
            _ProductCategoryMaster = ProductCategoryMaster;
            _AuthorContractOriginal = AuthorContractOriginal;
            _ProductPreviousProductLinkRepository = ProductPreviousProductLink;
            _ProductTypeMaster = ProductTypeMaster;
            _ProductAuthorLink = ProductAuthorLink;
            _AuthorMaster = AuthorMaster;
            _ProductSAPAgreementMaster = ProductSAPAgreementMaster;
            this._ProductLicense = ProductLicense;
            this._AddendumServices = AddendumServices;
            this._AddendumDetails = AddendumDetails;
        }

        public IHttpActionResult InsertProduct(ACS.Core.Domain.Product.ProductMaster Product)
        {
            string status = "";
            IList<AuthorContractOriginal> AuthorContractList = null;
            IList<ProductLicense> ProductLicenseList = null;
            int Id = 0;
            try
            {
                //------------start Capital First Letter
                string product_WorkingProduct = "";
                string product_WorkingSubProduct = "";
                StringBuilder sb;

                if (Product.WorkingProduct != null)
                {
                    string str_WorkingProduct = Product.WorkingProduct.Trim();
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

                if (Product.WorkingSubProduct != null)
                {
                    string str_WorkingSubProduct = Product.WorkingSubProduct.Trim();
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
                //-----------------------end 

                //status = _ProductMasterService.DuplicityProjectCodeCheck(Product);
                //if (status != "duplicate")
                //{
                status = _ProductMasterService.DuplicityISBNCheck(Product);
                if (status != "duplicateISBN")
                {
                    if (Product.ProductPreviousProductLink != null)
                    {
                        AuthorContractList = _ProductMasterService.CheckAuthorContract(Product.ProductPreviousProductLink.FirstOrDefault().PreviousProductId);
                        ProductLicenseList = _ProductMasterService.CheckProductLicense(Product.ProductPreviousProductLink.FirstOrDefault().PreviousProductId);
                        if (AuthorContractList.Count == 0 && ProductLicenseList.Count == 0)
                        {
                            status = "NoAuthorCode";
                            return Json(new { status, Id });
                        }
                    }
                    if (Product.Id == 0)
                    {
                        ACS.Core.Domain.Product.ProductMaster mobj_productMaster = new ACS.Core.Domain.Product.ProductMaster();

                        IList<ProprietorMaster> _IProprietorMaster = new List<ProprietorMaster>();

                        mobj_productMaster.DivisionId = Product.DivisionId;
                        mobj_productMaster.SubdivisionId = Product.SubdivisionId;
                        mobj_productMaster.ProductCategoryId = Product.ProductCategoryId;
                        mobj_productMaster.ProductTypeId = Product.ProductTypeId;
                        mobj_productMaster.SubProductTypeId = Product.SubProductTypeId;
                        mobj_productMaster.ProjectCode = Product.ProjectCode;
                        mobj_productMaster.ProjectCode = Product.ProjectCode;
                        mobj_productMaster.OUPISBN = Product.OUPISBN;
                        mobj_productMaster.WorkingProduct = product_WorkingProduct;// Product.WorkingProduct;
                        mobj_productMaster.WorkingSubProduct = product_WorkingSubProduct; // Product.WorkingSubProduct;
                        mobj_productMaster.OUPEdition = Product.OUPEdition;
                        mobj_productMaster.Volume = Product.Volume;
                        mobj_productMaster.CopyrightYear = Product.CopyrightYear;
                        mobj_productMaster.ImprintId = Product.ImprintId;
                        mobj_productMaster.LanguageId = Product.LanguageId;
                        mobj_productMaster.SeriesId = Product.SeriesId;
                        mobj_productMaster.Derivatives = Product.Derivatives;
                        mobj_productMaster.OrgISBN = Product.OrgISBN;
                        mobj_productMaster.ProjectedPublishingDate = Product.ProjectedPublishingDate;
                        mobj_productMaster.ProjectedPrice = Product.ProjectedPrice;
                        mobj_productMaster.ProjectedCurrencyId = Product.ProjectedCurrencyId;
                        mobj_productMaster.PubCenterId = Product.PubCenterId;
                        mobj_productMaster.ThirdPartyPermission = Convert.ToInt32(Product.ThirdPartyPermission);


                        IList<ProductAuthorLink> _IProductOUPAuthor = new List<ProductAuthorLink>();

                        //OUP Author Details Set
                        if (Product.ProductProductAuthorLink != null)
                        {
                            foreach (ProductAuthorLink prdctAuthor in Product.ProductProductAuthorLink)
                            {
                                //OUP Author Details Set
                                ProductAuthorLink _ProductOUPAuthor = new ProductAuthorLink();

                                _ProductOUPAuthor.AuthorId = prdctAuthor.AuthorId;
                                _ProductOUPAuthor.Deactivate = "N";
                                _ProductOUPAuthor.EnteredBy = Product.EnteredBy;
                                _ProductOUPAuthor.EntryDate = DateTime.Now;
                                _ProductOUPAuthor.ModifiedBy = null;
                                _ProductOUPAuthor.ModifiedDate = null;
                                _ProductOUPAuthor.DeactivateBy = null;
                                _ProductOUPAuthor.DeactivateDate = null;
                                _IProductOUPAuthor.Add(_ProductOUPAuthor);
                            }
                        }


                        //Proprietor Details Set
                        if (Product.ProductProprietorMaster != null)
                        {

                            int procount = 0;
                            foreach (ProprietorMaster propM in Product.ProductProprietorMaster)
                            {
                                //Set ProrCount 
                                procount = procount + 1;

                                //Proprietor Details Set
                                ProprietorMaster _ProprietorMaster = new ProprietorMaster();

                                //IList<ProprietorAuthorLink> _IProprietorAuthor = new List<ProprietorAuthorLink>();
                                //////Proprietor Author Details Set
                                //foreach (ProprietorAuthorLink PropAuthor in propM.ProprietorAuthorLink)
                                //{
                                //    //Proprietor Author Details Set
                                //    ProprietorAuthorLink _ProprietorAuthor = new ProprietorAuthorLink();

                                //    _ProprietorAuthor.AuthorId = PropAuthor.AuthorId;
                                //    _ProprietorAuthor.Deactivate = "N";
                                //    _ProprietorAuthor.EnteredBy = Product.EnteredBy;
                                //    _ProprietorAuthor.EntryDate = DateTime.Now;
                                //    _ProprietorAuthor.ModifiedBy = null;
                                //    _ProprietorAuthor.ModifiedDate = null;
                                //    _ProprietorAuthor.DeactivateBy = null;
                                //    _ProprietorAuthor.DeactivateDate = null;
                                //    _IProprietorAuthor.Add(_ProprietorAuthor);
                                //}

                                //_ProprietorMaster.ProprietorAuthorLink = _IProprietorAuthor;
                                _ProprietorMaster.ProprietorISBN = propM.ProprietorISBN;
                                _ProprietorMaster.ProprietorProduct = propM.ProprietorProduct;
                                _ProprietorMaster.ProprietorEdition = propM.ProprietorEdition;
                                _ProprietorMaster.ProprietorCopyrightYear = propM.ProprietorCopyrightYear;
                                _ProprietorMaster.PublishingCompanyId = propM.PublishingCompanyId;
                                _ProprietorMaster.ProprietorPubCenterId = propM.ProprietorPubCenterId;
                                _ProprietorMaster.ProprietorImPrintId = propM.ProprietorImPrintId;
                                _ProprietorMaster.Main = (procount == 1 ? "Y" : "N");
                                _ProprietorMaster.Deactivate = "N";
                                _ProprietorMaster.EnteredBy = Product.EnteredBy;
                                _ProprietorMaster.EntryDate = DateTime.Now;
                                _ProprietorMaster.ModifiedBy = null;
                                _ProprietorMaster.ModifiedDate = null;
                                _ProprietorMaster.DeactivateBy = null;
                                _ProprietorMaster.DeactivateDate = null;
                                _ProprietorMaster.ProprietorAuthorName = propM.ProprietorAuthorName;
                                _IProprietorMaster.Add(_ProprietorMaster);
                            }
                        }
                        IList<ProductPreviousProductLink> _IProductPreviousProductLink = new List<ProductPreviousProductLink>();
                        if (Product.ProductPreviousProductLink != null)
                        {
                            if (AuthorContractList.Count == 0)
                            {
                                ProductPreviousProductLink _ProductPreviousProductLink = new ProductPreviousProductLink();
                                _ProductPreviousProductLink.PreviousProductId = Product.ProductPreviousProductLink.FirstOrDefault().PreviousProductId;
                                _ProductPreviousProductLink.AuthorContractId = null;
                                _ProductPreviousProductLink.Deactivate = "N";
                                _ProductPreviousProductLink.EnteredBy = Product.EnteredBy;
                                _ProductPreviousProductLink.EntryDate = DateTime.Now;
                                _ProductPreviousProductLink.ModifiedBy = null;
                                _ProductPreviousProductLink.ModifiedDate = null;
                                _ProductPreviousProductLink.DeactivateBy = null;
                                _ProductPreviousProductLink.DeactivateDate = null;
                                _IProductPreviousProductLink.Add(_ProductPreviousProductLink);
                            }
                            else
                            {
                                foreach (AuthorContractOriginal AuthorContract in AuthorContractList)
                                {
                                    ProductPreviousProductLink _ProductPreviousProductLink = new ProductPreviousProductLink();
                                    _ProductPreviousProductLink.PreviousProductId = Product.ProductPreviousProductLink.FirstOrDefault().PreviousProductId;
                                    _ProductPreviousProductLink.AuthorContractId = AuthorContract.Id;
                                    _ProductPreviousProductLink.Deactivate = "N";
                                    _ProductPreviousProductLink.EnteredBy = Product.EnteredBy;
                                    _ProductPreviousProductLink.EntryDate = DateTime.Now;
                                    _ProductPreviousProductLink.ModifiedBy = null;
                                    _ProductPreviousProductLink.ModifiedDate = null;
                                    _ProductPreviousProductLink.DeactivateBy = null;
                                    _ProductPreviousProductLink.DeactivateDate = null;
                                    _IProductPreviousProductLink.Add(_ProductPreviousProductLink);
                                }
                            }
                        }

                        mobj_productMaster.ProductProductAuthorLink = _IProductOUPAuthor;
                        mobj_productMaster.ProductProprietorMaster = _IProprietorMaster;
                        mobj_productMaster.ProductPreviousProductLink = _IProductPreviousProductLink;
                        mobj_productMaster.Deactivate = "N";
                        mobj_productMaster.EnteredBy = Product.EnteredBy;
                        mobj_productMaster.EntryDate = DateTime.Now;
                        mobj_productMaster.ModifiedBy = null;
                        mobj_productMaster.ModifiedDate = null;
                        mobj_productMaster.DeactivateBy = null;
                        mobj_productMaster.DeactivateDate = null;
                        Id = _ProductMasterService.InsertProductMaster(mobj_productMaster);

                    }
                    status = "OK";
                }
                else
                {
                    status = "DuplicateISBN";
                }
                //}
                //else
                //{
                //    status = "Duplicate";
                //}



            }
            catch (ACSException ex)
            {
                status = ex.ToString();
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }

            return Json(new { status, Id });
        }
        
        public IHttpActionResult UpdateProduct(ACS.Core.Domain.Product.ProductMaster Product)
        {
            int Id = 0;
            string status = "";
            IList<AuthorContractOriginal> AuthorContractList = null;
            IList<ProductLicense> ProductLicenseList = null;
            try
            {
                //------------start Capital First Letter
                string product_WorkingProduct = "";
                string product_WorkingSubProduct = "";
                StringBuilder sb;

                if (Product.WorkingProduct != null)
                {
                    string str_WorkingProduct = Product.WorkingProduct.Trim();
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

                if (Product.WorkingSubProduct != null)
                {
                    string str_WorkingSubProduct = Product.WorkingSubProduct.Trim();
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
                //-----------------------end 


                //status = _ProductMasterService.DuplicityProjectCodeCheck(Product);
                //if (status != "duplicate")
                //{
                status = _ProductMasterService.DuplicityISBNCheck(Product);
                if (status != "duplicateISBN")
                {
                    if (Product.ProductPreviousProductLink != null)
                    {
                        AuthorContractList = _ProductMasterService.CheckAuthorContract(Product.ProductPreviousProductLink.FirstOrDefault().PreviousProductId);
                        ProductLicenseList = _ProductMasterService.CheckProductLicense(Product.ProductPreviousProductLink.FirstOrDefault().PreviousProductId);
                        if (AuthorContractList.Count == 0 && ProductLicenseList.Count == 0)
                        {
                            status = "NoAuthorCode";
                            return Json(new { status, Id });
                        }
                    }
                    if (Product.Id > 0)
                    {
                        ACS.Core.Domain.Product.ProductMaster mobj_productMaster = _ProductMasterService.GetProductById(Product);
                        List<ProductAuthorLink> _IProductOUPAuthor = mobj_productMaster.ProductProductAuthorLink.ToList();
                        List<ProductPreviousProductLink> _ILProductPreviousProductLink = mobj_productMaster.ProductPreviousProductLink.ToList();


                        foreach (ProductAuthorLink prlink in _IProductOUPAuthor)
                        {
                            _ProductMasterService.DeleteProductAuthorLink(prlink);
                        }

                        foreach (ProductPreviousProductLink prevlink in _ILProductPreviousProductLink)
                        {
                            _ProductMasterService.DeletePreviousProductLink(prevlink);
                        }

                        mobj_productMaster.DivisionId = Product.DivisionId;
                        mobj_productMaster.SubdivisionId = Product.SubdivisionId;
                        mobj_productMaster.ProductCategoryId = Product.ProductCategoryId;
                        mobj_productMaster.ProductTypeId = Product.ProductTypeId;
                        mobj_productMaster.SubProductTypeId = Product.SubProductTypeId;
                        //mobj_productMaster.ProductCode = Product.ProductCode;
                        mobj_productMaster.ProjectCode = Product.ProjectCode;
                        mobj_productMaster.OUPISBN = Product.OUPISBN;
                        mobj_productMaster.WorkingProduct = product_WorkingProduct; // Product.WorkingProduct;
                        mobj_productMaster.WorkingSubProduct = product_WorkingSubProduct; // Product.WorkingSubProduct;
                        mobj_productMaster.OUPEdition = Product.OUPEdition;
                        mobj_productMaster.Volume = Product.Volume;
                        mobj_productMaster.CopyrightYear = Product.CopyrightYear;
                        mobj_productMaster.ImprintId = Product.ImprintId;
                        mobj_productMaster.LanguageId = Product.LanguageId;
                        mobj_productMaster.SeriesId = Product.SeriesId;
                        mobj_productMaster.Derivatives = Product.Derivatives;
                        mobj_productMaster.OrgISBN = Product.OrgISBN;
                        mobj_productMaster.ProjectedPublishingDate = Product.ProjectedPublishingDate;
                        mobj_productMaster.ProjectedPrice = Product.ProjectedPrice;
                        mobj_productMaster.ProjectedCurrencyId = Product.ProjectedCurrencyId;
                        mobj_productMaster.PubCenterId = Product.PubCenterId;

                        mobj_productMaster.ThirdPartyPermission = Product.ThirdPartyPermission;

                        mobj_productMaster.ModifiedBy = Product.EnteredBy;
                        mobj_productMaster.ModifiedDate = DateTime.Now;

                        IList<ProductAuthorLink> _INewProductOUPAuthor = new List<ProductAuthorLink>();
                        //OUP Author Details Set

                        if (Product.ProductProductAuthorLink != null)
                        {
                            foreach (ProductAuthorLink prdctAuthor in Product.ProductProductAuthorLink)
                            {
                                //OUP Author Details Set
                                ProductAuthorLink _ProductOUPAuthor = new ProductAuthorLink();

                                _ProductOUPAuthor.AuthorId = prdctAuthor.AuthorId;
                                _ProductOUPAuthor.Deactivate = "N";
                                _ProductOUPAuthor.EnteredBy = Product.EnteredBy;
                                _ProductOUPAuthor.EntryDate = DateTime.Now;
                                _ProductOUPAuthor.ModifiedBy = null;
                                _ProductOUPAuthor.ModifiedDate = null;
                                _ProductOUPAuthor.DeactivateBy = null;
                                _ProductOUPAuthor.DeactivateDate = null;
                                _INewProductOUPAuthor.Add(_ProductOUPAuthor);
                            }

                        }

                        IList<ProductPreviousProductLink> _IProductPreviousProductLink = new List<ProductPreviousProductLink>();
                        if (Product.ProductPreviousProductLink != null)
                        {
                            if (AuthorContractList.Count == 0)
                            {
                                ProductPreviousProductLink _ProductPreviousProductLink = new ProductPreviousProductLink();
                                _ProductPreviousProductLink.PreviousProductId = Product.ProductPreviousProductLink.FirstOrDefault().PreviousProductId;
                                _ProductPreviousProductLink.AuthorContractId = null;
                                _ProductPreviousProductLink.Deactivate = "N";
                                _ProductPreviousProductLink.EnteredBy = Product.EnteredBy;
                                _ProductPreviousProductLink.EntryDate = DateTime.Now;
                                _ProductPreviousProductLink.ModifiedBy = null;
                                _ProductPreviousProductLink.ModifiedDate = null;
                                _ProductPreviousProductLink.DeactivateBy = null;
                                _ProductPreviousProductLink.DeactivateDate = null;
                                _IProductPreviousProductLink.Add(_ProductPreviousProductLink);
                            }
                            else
                            {
                                foreach (AuthorContractOriginal AuthorContract in AuthorContractList)
                                {
                                    ProductPreviousProductLink _ProductPreviousProductLink = new ProductPreviousProductLink();
                                    _ProductPreviousProductLink.PreviousProductId = Product.ProductPreviousProductLink.FirstOrDefault().PreviousProductId;
                                    _ProductPreviousProductLink.AuthorContractId = AuthorContract.Id;
                                    _ProductPreviousProductLink.Deactivate = "N";
                                    _ProductPreviousProductLink.EnteredBy = Product.EnteredBy;
                                    _ProductPreviousProductLink.EntryDate = DateTime.Now;
                                    _ProductPreviousProductLink.ModifiedBy = null;
                                    _ProductPreviousProductLink.ModifiedDate = null;
                                    _ProductPreviousProductLink.DeactivateBy = null;
                                    _ProductPreviousProductLink.DeactivateDate = null;
                                    _IProductPreviousProductLink.Add(_ProductPreviousProductLink);
                                }
                            }
                        }

                        mobj_productMaster.ProductPreviousProductLink = _IProductPreviousProductLink;
                        mobj_productMaster.ProductProductAuthorLink = _INewProductOUPAuthor;
                        mobj_productMaster.ProductProprietorMaster = mobj_productMaster.ProductProprietorMaster;
                        _ProductMasterService.UpdateProductMaster(mobj_productMaster);

                        if (Product.ProductProprietorMaster != null)
                        {
                            //Proprietor Details Set
                            ProprietorMaster propM = Product.ProductProprietorMaster.FirstOrDefault();
                            ProprietorMaster _ProprietorMaster = mobj_productMaster.ProductProprietorMaster.Where(p => p.ProductId == mobj_productMaster.Id && p.Main == "Y").FirstOrDefault();

                            if (_ProprietorMaster != null)
                            {
                                List<ProprietorAuthorLink> _IProprietorAuthor = _ProprietorMaster.ProprietorAuthorLink.ToList();

                                foreach (ProprietorAuthorLink prAuthor in _IProprietorAuthor)
                                {
                                    _ProductMasterService.DeleteProprietorAuthorLink(prAuthor);
                                }

                                //IList<ProprietorAuthorLink> _INewProprietorAuthor = new List<ProprietorAuthorLink>();

                                ////Proprietor Author Details Set
                                //foreach (ProprietorAuthorLink PropAuthor in propM.ProprietorAuthorLink)
                                //{
                                //    //Proprietor Author Details Set
                                //    ProprietorAuthorLink _ProprietorAuthor = new ProprietorAuthorLink();

                                //    _ProprietorAuthor.AuthorId = PropAuthor.AuthorId;
                                //    _ProprietorAuthor.Deactivate = "N";
                                //    _ProprietorAuthor.EnteredBy = Product.EnteredBy;
                                //    _ProprietorAuthor.EntryDate = DateTime.Now;
                                //    _ProprietorAuthor.ModifiedBy = null;
                                //    _ProprietorAuthor.ModifiedDate = null;
                                //    _ProprietorAuthor.DeactivateBy = null;
                                //    _ProprietorAuthor.DeactivateDate = null;
                                //    _INewProprietorAuthor.Add(_ProprietorAuthor);
                                //}

                                //_ProprietorMaster.ProprietorAuthorLink = _INewProprietorAuthor;
                                _ProprietorMaster.ProprietorISBN = propM.ProprietorISBN;
                                _ProprietorMaster.ProprietorProduct = propM.ProprietorProduct;
                                _ProprietorMaster.ProprietorEdition = propM.ProprietorEdition;
                                _ProprietorMaster.ProprietorCopyrightYear = propM.ProprietorCopyrightYear;
                                _ProprietorMaster.PublishingCompanyId = propM.PublishingCompanyId;
                                _ProprietorMaster.ProprietorPubCenterId = propM.ProprietorPubCenterId;
                                _ProprietorMaster.ProprietorImPrintId = propM.ProprietorImPrintId;
                                _ProprietorMaster.ProprietorAuthorName = propM.ProprietorAuthorName;
                                _ProprietorMaster.ModifiedBy = Product.EnteredBy;
                                _ProprietorMaster.ModifiedDate = DateTime.Now;
                                _ProductMasterService.UpdateProprietorMaster(_ProprietorMaster);
                            }
                            else
                            {
                                // added by prakash on 28 june, 2017
                                int procount = 0;

                                //Proprietor Details Set
                                IList<ProprietorMaster> _IProprietorMaster_set = new List<ProprietorMaster>();
                                ProprietorMaster _ProprietorMaster_set = new ProprietorMaster();

                                foreach (ProprietorMaster propM_set in Product.ProductProprietorMaster)
                                {
                                    //Set ProrCount 
                                    procount = procount + 1;

                                    _ProprietorMaster_set.ProductId = Product.Id;
                                    _ProprietorMaster_set.ProprietorISBN = propM_set.ProprietorISBN;
                                    _ProprietorMaster_set.ProprietorProduct = propM_set.ProprietorProduct;
                                    _ProprietorMaster_set.ProprietorEdition = propM_set.ProprietorEdition;
                                    _ProprietorMaster_set.ProprietorCopyrightYear = propM_set.ProprietorCopyrightYear;
                                    _ProprietorMaster_set.PublishingCompanyId = propM_set.PublishingCompanyId;
                                    _ProprietorMaster_set.ProprietorPubCenterId = propM_set.ProprietorPubCenterId;
                                    _ProprietorMaster_set.ProprietorImPrintId = propM_set.ProprietorImPrintId;
                                    _ProprietorMaster_set.Main = (procount == 1 ? "Y" : "N");
                                    _ProprietorMaster_set.Deactivate = "N";
                                    _ProprietorMaster_set.EnteredBy = Product.EnteredBy;
                                    _ProprietorMaster_set.EntryDate = DateTime.Now;
                                    _ProprietorMaster_set.ModifiedBy = null;
                                    _ProprietorMaster_set.ModifiedDate = null;
                                    _ProprietorMaster_set.DeactivateBy = null;
                                    _ProprietorMaster_set.DeactivateDate = null;
                                    _ProprietorMaster_set.ProprietorAuthorName = propM_set.ProprietorAuthorName;
                                    _ProductMasterService.InsertProprietorMaster(_ProprietorMaster_set);
                                }
                            }

                        }
                    }
                    Id = Product.Id;
                    status = "OK";
                }
                else
                {
                    status = "DuplicateISBN";
                }
                //}
                //else
                //{
                //    status = "Duplicate";
                //}



            }
            catch (ACSException ex)
            {
                status = ex.ToString();
            }
            catch (Exception ex)
            {
                status = ex.ToString();
            }

            return Json(new { status, Id });
        }
        
        //public IHttpActionResult ProductDetails(ProductSearchDetails productDetail)
        //{
        //    SqlParameter[] parameters = new SqlParameter[1];

        //    try
        //    {
        //        if (productDetail != null)
        //        {
        //            parameters[0] = new SqlParameter("ProductId", SqlDbType.NVarChar, 100);
        //            if (productDetail.MultipleId == null)
        //            {
        //                parameters[0].Value = System.Data.SqlTypes.SqlString.Null;
        //            }
        //            else
        //            {
        //                parameters[0].Value = productDetail.MultipleId;
        //            }


        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<ProductSearchDetails>("Proc_ProductDetails_get", parameters).ToList();


        //    return Json(_GetAuthorReport);
        //}
                
        public IHttpActionResult ProductDetails(ProductSearchDetails productDetail)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                if (productDetail != null)
                {
                    parameters[0] = new SqlParameter("ProductId", SqlDbType.VarChar, 50);
                    if (productDetail.Id == 0)
                    {
                        parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[0].Value = productDetail.Id;
                    }

                    //parameters[1] = new SqlParameter("AuthorContract", SqlDbType.VarChar, 50);
                    //if (productDetail.AuthorContract == null)
                    //{
                    //    parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
                    //}
                    //else
                    //{
                    //    parameters[1].Value = "'" + productDetail.AuthorContract + "'";
                    //}

                    //parameters[1] = new SqlParameter("ProductLicCode", SqlDbType.VarChar, 50);
                    //if (productDetail.ProductLicCode == null)
                    //{
                    //    parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
                    //}
                    //else
                    //{
                    //    parameters[1].Value = "'"+ productDetail.ProductLicCode +"'";
                    //}
                    
                    //parameters[3] = new SqlParameter("LicenseId", SqlDbType.VarChar, 50);
                    //if (productDetail.LicenseId == null)
                    //{
                    //    parameters[3].Value = System.Data.SqlTypes.SqlInt32.Null;
                    //}
                    //else
                    //{
                    //    parameters[3].Value = "'" + productDetail.LicenseId + "'";
                    //}
                    
                }                
            }
            catch (Exception ex)
            {
            }
            var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<ProductSearchDetails>("Proc_ProductDetails_get", parameters).ToList();
            
            return Json(_GetAuthorReport);
        }

        public IHttpActionResult ProductDetailsForContract(ProductSearchDetails productDetail)
        {
            SqlParameter[] parameters = new SqlParameter[2];

            try
            {
                if (productDetail != null)
                {
                    parameters[0] = new SqlParameter("ProductId", SqlDbType.VarChar, 50);
                    if (productDetail.Id == 0)
                    {
                        parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[0].Value = productDetail.Id;
                    }

                    parameters[1] = new SqlParameter("AuthorContract", SqlDbType.VarChar, 50);
                    if (productDetail.AuthorContract == null)
                    {
                        parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[1].Value = "'" + productDetail.AuthorContract + "'";
                    }

                    //parameters[1] = new SqlParameter("ProductLicCode", SqlDbType.VarChar, 50);
                    //if (productDetail.ProductLicCode == null)
                    //{
                    //    parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
                    //}
                    //else
                    //{
                    //    parameters[1].Value = "'" + productDetail.ProductLicCode + "'";
                    //}



                    //parameters[3] = new SqlParameter("LicenseId", SqlDbType.VarChar, 50);
                    //if (productDetail.LicenseId == null)
                    //{
                    //    parameters[3].Value = System.Data.SqlTypes.SqlInt32.Null;
                    //}
                    //else
                    //{
                    //    parameters[3].Value = "'" + productDetail.LicenseId + "'";
                    //}
                    
                }
            }
            catch (Exception ex)
            {
            }
            var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<ProductSearchDetails>("Proc_productdetails_ForContract_get", parameters).ToList();
            
            return Json(_GetAuthorReport);
        }

        public IHttpActionResult ProductDetailsForLicense(ProductSearchDetails productDetail)
        {
            SqlParameter[] parameters = new SqlParameter[2];

            try
            {
                if (productDetail != null)
                {
                    parameters[0] = new SqlParameter("ProductId", SqlDbType.VarChar, 50);
                    if (productDetail.Id == 0)
                    {
                        parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[0].Value = productDetail.Id;
                    }
                    
                    parameters[1] = new SqlParameter("ProductLicCode", SqlDbType.VarChar, 50);
                    if (productDetail.ProductLicCode == null)
                    {
                        parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[1].Value = "'" + productDetail.ProductLicCode + "'";
                    }


                    //parameters[1] = new SqlParameter("AuthorContract", SqlDbType.VarChar, 50);
                    //if (productDetail.AuthorContract == null)
                    //{
                    //    parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
                    //}
                    //else
                    //{
                    //    parameters[1].Value = "'" + productDetail.AuthorContract + "'";
                    //}

                    //parameters[3] = new SqlParameter("LicenseId", SqlDbType.VarChar, 50);
                    //if (productDetail.LicenseId == null)
                    //{
                    //    parameters[3].Value = System.Data.SqlTypes.SqlInt32.Null;
                    //}
                    //else
                    //{
                    //    parameters[3].Value = "'" + productDetail.LicenseId + "'";
                    //}

                }
            }
            catch (Exception ex)
            {
            }
            var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<ProductSearchDetails>("Proc_productdetails_ForLicense_get", parameters).ToList();

            return Json(_GetAuthorReport);
        }
        
        [HttpGet]
        public IHttpActionResult MultipleProductDetails(String Ids = "", String SeriesCode = "", string For = "")
        {
            SqlParameter[] parameters = new SqlParameter[3];

            try
            {
                ////Commented and added by for product List according to Series contract List // Prakash on 04 July, 2017
                if (Ids != "" || SeriesCode != "")
                {
                    parameters[0] = new SqlParameter("ProductId", SqlDbType.VarChar, 200); 
                    parameters[0].Value = "'" + Ids + "'";
                    parameters[1] = new SqlParameter("SeriesCode", SqlDbType.VarChar, 100);
                    parameters[1].Value = "'" + SeriesCode + "'";
                    parameters[2] = new SqlParameter("For", SqlDbType.VarChar, 100);
                    parameters[2].Value = "'" + For + "'";
                }
                
                //var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<ProductSearchDetails>("Proc_ProductDetails_get", parameters).ToList();
                var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<ProductSearchDetails>("Proc_ProductDetailsForSeries_get", parameters).ToList();
                return Json(_GetAuthorReport);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }

        }
        
        [HttpPost]
        public IHttpActionResult getProductById(ACS.Core.Domain.Product.ProductMaster Product)
        {
            ACS.Core.Domain.Product.ProductMaster ProductM1 = _ProductMasterService.GetProductById(Product);
            ACS.Core.Domain.Product.ProductMaster ProductM = new ACS.Core.Domain.Product.ProductMaster();
            ProductM.Id = ProductM1.Id;
            ProductM.ProductProductCategory = ProductM1.ProductProductCategory;
            ProductM.DivisionId = ProductM1.DivisionId;
            ProductM.SubdivisionId = ProductM1.SubdivisionId;
            ProductM.ProductCategoryId = ProductM1.ProductCategoryId;
            ProductM.ProductTypeId = ProductM1.ProductTypeId;
            ProductM.SubProductTypeId = ProductM1.SubProductTypeId;
            ProductM.ProjectCode = ProductM1.ProjectCode;
            ProductM.ProductCode = ProductM1.ProductCode;
            ProductM.OUPISBN = ProductM1.OUPISBN;
            ProductM.WorkingProduct = ProductM1.WorkingProduct;
            ProductM.WorkingSubProduct = ProductM1.WorkingSubProduct;
            ProductM.OUPEdition = ProductM1.OUPEdition;
            ProductM.Volume = ProductM1.Volume;
            ProductM.CopyrightYear = ProductM1.CopyrightYear;
            ProductM.ImprintId = ProductM1.ImprintId;
            ProductM.LanguageId = ProductM1.LanguageId;
            ProductM.SeriesId = ProductM1.SeriesId;
            ProductM.Derivatives = ProductM1.Derivatives;
            ProductM.OrgISBN = ProductM1.OrgISBN;
            ProductM.ProjectedPublishingDate = ProductM1.ProjectedPublishingDate;
            ProductM.ProjectedPrice = ProductM1.ProjectedPrice;
            ProductM.ProjectedCurrencyId = ProductM1.ProjectedCurrencyId;
            ProductM.PubCenterId = ProductM1.PubCenterId;
            ProductM.Deactivate = ProductM1.Deactivate;
            ProductM.EnteredBy = ProductM1.EnteredBy;
            ProductM.EntryDate = ProductM1.EntryDate;
            ProductM.ModifiedBy = ProductM1.ModifiedBy;
            ProductM.ModifiedDate = ProductM1.ModifiedDate;
            ProductM.DeactivateBy = ProductM1.DeactivateBy;
            ProductM.DeactivateDate = ProductM1.DeactivateDate;
            ProductM.ProductCode = ProductM1.ProductCode;

            ProductM.FinalProductName = ProductM1.FinalProductName;

            ProductM.ThirdPartyPermission = ProductM1.ThirdPartyPermission;

            var ProprietorM = ProductM1.ProductProprietorMaster.Where(a => a.Main == "Y").FirstOrDefault();



            IList<ProductAuthorLink> _ProductAuthorList = ProductM1.ProductProductAuthorLink.ToList();
            var ProductAuthor = _ProductAuthorList.Select(pu => new
            {
                AuthorId = pu.AuthorId,
                AuthorName = pu.ProductAuthorLinkAuthor.FirstName + " " + pu.ProductAuthorLinkAuthor.LastName,
                ProductAuthorCategory = pu.ProductAuthorLinkAuthor.Type

            });

            IList<ProductPreviousProductLink> _ProductPreviousProductLink = ProductM1.ProductPreviousProductLink.ToList();
            var ProductPreviousProduct = _ProductPreviousProductLink.Select(pu => new
            {
                PreviousISBN = _ProductMasterService.GetProductISBNById(pu.PreviousProductId),
            }).Distinct().ToList();

            if (ProprietorM != null)
            {

                IList<ProprietorAuthorLink> _ProprietorAuthorList = ProprietorM.ProprietorAuthorLink.ToList();

                var ProprietorAuthor = _ProprietorAuthorList.Select(pu => new
                {
                    AuthorId = pu.AuthorId,
                    AuthorName = pu.ProprietorAuthorLinkAuthor.FirstName + " " + pu.ProprietorAuthorLinkAuthor.LastName,
                    ProprietorAuthorCategory = pu.ProprietorAuthorLinkAuthor.Type

                });
                return Json(SerializeObj.SerializeObject(new { ProductM, ProprietorM, ProductAuthor, ProprietorAuthor, ProductPreviousProduct }));
            }
            else
            {

                var ProprietorAuthor = "";

                return Json(SerializeObj.SerializeObject(new { ProductM, ProprietorM, ProductAuthor, ProprietorAuthor, ProductPreviousProduct }));
            }


        }

        [HttpPost]
        public IHttpActionResult ValidISBN(ACS.Core.Domain.Product.ProductMaster Product)
        {
            var productId = _ProductMasterService.ValidISBN(Product.OUPISBN);
            return Json(productId);
        }

        public IHttpActionResult InsertFinalProductDetails(ACS.Core.Domain.Product.ProductMaster Product)
        {

            if (Product.Id > 0)
            {
                ACS.Core.Domain.Product.ProductMaster mobj_productMaster = _ProductMasterService.GetProductById(Product);
                mobj_productMaster.FinalProductName = Product.FinalProductName;
                mobj_productMaster.PublishingDate = Product.PublishingDate;
                //mobj_productMaster.PubCenterId = Product.PubCenterId;
                mobj_productMaster.CopyrightYear = Product.CopyrightYear;
                mobj_productMaster.ModifiedBy = Product.EnteredBy;
                mobj_productMaster.ModifiedDate = DateTime.Now;
                _ProductMasterService.UpdateProductMaster(mobj_productMaster);
                return Json("OK");
            }
            else
            {
                return Json("Opps");
            }


        }

        [HttpPost]
        public IHttpActionResult getProductAuthor(ACS.Core.Domain.Product.ProductAuthorLink ProductAuthorLink)
        {
            IList<ProductAuthorLink> _ProductAuthorLink = _ProductMasterService.ProductAuthorList(ProductAuthorLink);

            var ProductAuthor = _ProductAuthorLink.Select(pu => new
            {
                Id = pu.Id,
                ProductId = pu.ProductId,
                AuthorId = pu.AuthorId
            });

            return Json(SerializeObj.SerializeObject(ProductAuthor));

        }
        
        public IHttpActionResult InsertSAPAggrementDetails(IList<ProductSAPAgreementMaster> IProductSAPAgreement)
        {
            string SAPagreementNo = "";
            string AuthorCode = "";

            //foreach (ProductSAPAgreementMaster SAPAgrrement in IProductSAPAgreement)
            //{
            //    //string Duplicates = _ProductMasterService.DuplicityProductSAPAgreementMasterCheck(SAPAgrrement);
            //    //if (Duplicates != "")
            //    //{
            //    //    if (SAPAgrrement.AuthorCode == null)
            //    //    {
            //    //        AuthorCode += Duplicates + ",";
            //    //    }
            //    //    else
            //    //    {
            //    //        SAPagreementNo += Duplicates + ",";
            //    //    }
            //    //}
            //}

            if (AuthorCode != "")
            {
                AuthorCode = "Duplicate Author Code are : " + AuthorCode.Substring(0, AuthorCode.Length - 1);
            }
            if (SAPagreementNo != "")
            {
                SAPagreementNo = "Duplicate SAP agreement Nos. are : " + SAPagreementNo.Substring(0, SAPagreementNo.Length - 1);
            }

            if (AuthorCode != "" || SAPagreementNo != "")
            {
                return Json(AuthorCode + "\n" + SAPagreementNo);
            }
            else
            {
                foreach (ProductSAPAgreementMaster SAPAgrrement in IProductSAPAgreement)
                {
                    ProductSAPAgreementMaster mobj_SAPAggrement;

                    if (SAPAgrrement.ProductCategory.ToLower() == "reprint")
                    {
                        mobj_SAPAggrement = new ProductSAPAgreementMaster();
                        mobj_SAPAggrement.OUPISBN = SAPAgrrement.OUPISBN;
                        if (SAPAgrrement.AuthorCode == null)
                        {
                            mobj_SAPAggrement.SAPagreementNo = SAPAgrrement.SAPagreementNo;
                            //mobj_SAPAggrement.AuthorId = SAPAgrrement.AuthorId;
                            mobj_SAPAggrement.ProductCategory = SAPAgrrement.ProductCategory;
                        }
                        else
                        {
                            mobj_SAPAggrement.AuthorCode = SAPAgrrement.AuthorCode;
                            mobj_SAPAggrement.ProprietorAuthorId = SAPAgrrement.AuthorId;
                            mobj_SAPAggrement.ProductCategory = SAPAgrrement.ProductCategory;
                        }
                    }
                    else
                    {
                        mobj_SAPAggrement = new ProductSAPAgreementMaster();
                        mobj_SAPAggrement.OUPISBN = SAPAgrrement.OUPISBN;
                        if (SAPAgrrement.AuthorCode == null)
                        {
                            mobj_SAPAggrement.SAPagreementNo = SAPAgrrement.SAPagreementNo;
                            //mobj_SAPAggrement.AuthorId = SAPAgrrement.AuthorId;
                        }
                        else
                        {
                            mobj_SAPAggrement.AuthorCode = SAPAgrrement.AuthorCode;
                            mobj_SAPAggrement.AuthorId = SAPAgrrement.AuthorId;
                        }
                    }


                    mobj_SAPAggrement.Deactivate = "N";
                    mobj_SAPAggrement.EnteredBy = SAPAgrrement.EnteredBy;
                    mobj_SAPAggrement.EntryDate = DateTime.Now;
                    mobj_SAPAggrement.ModifiedBy = null;
                    mobj_SAPAggrement.ModifiedDate = null;
                    mobj_SAPAggrement.DeactivateBy = null;
                    mobj_SAPAggrement.DeactivateDate = null;
                    _ProductMasterService.InsertProductSAPAggreentDetails(mobj_SAPAggrement);

                }
            }
            return Json("OK");

        }
        
        /** Create By    :   Saddam
          Create On    :   14th july 2016 
          Created For   :   Fetching the details of Product License contract
       */
        
        public IHttpActionResult ProductLicenseDetails(ProductLicenseDetail ProductLicense)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                if (ProductLicense != null)
                {
                    parameters[0] = new SqlParameter("ProductLicenseId", SqlDbType.VarChar, 50);
                    if (ProductLicense.ProductLicenseId == 0)
                    {
                        parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[0].Value = ProductLicense.ProductLicenseId;
                    }


                }


            }
            catch (Exception ex)
            {
            }
            var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<ProductLicenseDetail>("Proc_ProductLicense_Detail_get", parameters).ToList();


            return Json(_GetAuthorReport);
        }

        //Added by Suranjana on 27/07/2016 to GetProductDetails
        #region GetProductDetails
        [HttpPost]
        public IHttpActionResult GetProductDetails(ProductSearchDetails Product)
        {

            SqlParameter[] parameters = new SqlParameter[1];

            parameters[0] = new SqlParameter("ProductId", SqlDbType.VarChar, 50);
            if (Product.Id == 0)
            {
                parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
            }
            else
            {
                parameters[0].Value = Product.Id;
            }

            var _GetProductReport = _dbContext.ExecuteStoredProcedureListNewData<ProductSearchDetails>("Proc_ProductDetailsView_get", parameters).ToList();

            return Json(_GetProductReport);

        }

        //addded by dheeraj sharma
        [HttpGet]
        public IHttpActionResult getSeriesDetails(int ProductId)
        {
            var _list = _ProductMasterService.getSeriesDetails(ProductId).ProductSeries;
            var _SeriesDetails = new
            {
                SeriesId = _list.Id,
                SeriesName = _list.Seriesname
            };
            return Json(_SeriesDetails);
        }
        #endregion
        
        /* Create By  : Saddam
         * Create on  : 26st July 2016
         * Create for : Fetaching Details for Series Contract Using Product Master, Series Master and Product Category Master Tables
         */
        public IHttpActionResult GetProductSeriesList(int ProductId)
        {
            var Mobj_ProductSeriesList = (from Pm in _ProductMaster.Table.Where(a => a.Deactivate == "N")
                                          join SM in _SeriesMaster.Table.Where(a => a.Deactivate == "N")
                                          on Pm.SeriesId equals SM.Id into firtTbl
                                          from d in firtTbl.DefaultIfEmpty()
                                          join PCM in _ProductCategoryMaster.Table.Where(a => a.Deactivate == "N")
                                          on Pm.ProductCategoryId equals PCM.Id into output
                                          from g in output.DefaultIfEmpty()

                                          //join AC in _AuthorContractOriginal.Table.Where(a => a.Deactivate == "N" && a.SeriesId != null)
                                          //on Pm.Id equals AC.ProductId into output1
                                          //from AC in output1.DefaultIfEmpty()

                                          //added by Prakash on 11 July, 2017
                                          join ptm in _ProductTypeMaster.Table.Where(a => a.Deactivate == "N")
                                          on Pm.ProductTypeId equals ptm.Id into ptmoutput
                                          from ptm in ptmoutput.DefaultIfEmpty()
                                          
                                          select new
                                          {
                                              Id = Pm.Id,
                                              ProductCode = Pm.ProductCode.ToUpper(),
                                              WorkingProduct = Pm.WorkingProduct,
                                              WorkingSubProduct = Pm.WorkingSubProduct,
                                              ISBN = Pm.OUPISBN,
                                              ProductCategory = g.ProductCategory,
                                              Deative = Pm.Deactivate,
                                              Contract_Type = "Author",
                                              SeriesId = Pm.SeriesId,
                                              //SeriesCode = AC.SeriesCode,
                                              //Flag = AC.SeriesId == null ? true : false,

                                              //added by Prakash on 11 July, 2017
                                              ProductType = ptm.typeName
                                              
                                              ,AuthorDetails = (from pal in _ProductAuthorLink.Table.Where(a => a.Deactivate == "N" && a.ProductId == Pm.Id)
                                                                join am in _AuthorMaster.Table.Where(a => a.Deactivate == "N")
                                                                on pal.AuthorId equals am.Id into amoutput
                                                                from am in amoutput.DefaultIfEmpty()
                                                                select new 
                                                                {
                                                                    AuthorCode = am.AuthorCode == null ? " " : am.AuthorCode,
                                                                    AuthorName = (am.FirstName == null ? " " : am.FirstName) + " " + (am.LastName == null ? " " : am.LastName)
                                                                }).ToList()
                                                                        
                                            ,SAPAgreementDetails = (from psam in _ProductSAPAgreementMaster.Table.Where(a => a.Deactivate == "N" && a.SAPagreementNo != null && a.OUPISBN == Pm.OUPISBN)
                                                                    select new 
                                                                    {
                                                                        SAPagreementNo = psam.SAPagreementNo == null ? " " : psam.SAPagreementNo,
                                                                    }).ToList()
                                              
                                          }

                ).Where(a => a.Deative == "N" && a.SeriesId == ProductId).OrderBy(a => a.ProductCode);

            return Json(Mobj_ProductSeriesList);

            /*var mobj_AuthorContract = _IAuthorContractService.GetAllAuthorContract();

            var Mobj_ProductSeriesList = (from Pm in _ProductMaster.Table.Where(a => a.Deactivate == "N")
                                          join SM in _SeriesMaster.Table.Where(a => a.Deactivate == "N")
                                          on Pm.SeriesId equals SM.Id into firtTbl
                                          from d in firtTbl.DefaultIfEmpty()

                                          join PCM in _ProductCategoryMaster.Table.Where(a => a.Deactivate == "N")
                                          on Pm.ProductCategoryId equals PCM.Id into output
                                          from g in output.DefaultIfEmpty()

                                          select new
                                          {
                                              Id = Pm.Id,
                                              ProductCode = Pm.ProductCode.ToUpper(),
                                              WorkingProduct = Pm.WorkingProduct,
                                              ProductCategory = g.ProductCategory,
                                              Deative = Pm.Deactivate,
                                              Contract_Type = "Author",
                                              SeriesId = Pm.SeriesId,
                                              //Flag = mobj_AuthorContract.Where(a=>a.ProductId == Pm.Id)==null?true:false
                                          }

                ).Distinct().Where(a => a.Deative == "N" && a.SeriesId == ProductId).OrderBy(a => a.ProductCode);

            return Json(Mobj_ProductSeriesList);*/

        }

        //Added by Prakash
        #region Product View Links
        [HttpPost]
        public IHttpActionResult GetAuthorContractLinks(ProductSearchDetails Product)
        {
            if (Product.Id == 0)
            {
                return null;
            }
            else
            {
                var parentId = _ProductPreviousProductLinkRepository.Table.Where(x => x.ProductId == Product.Id && x.Deactivate == "N").Select(x => x.PreviousProductId).FirstOrDefault();
                if (parentId == 0)
                {
                    var mobj_AuthorContract = _IAuthorContractService.GetAllAuthorContract();
                    List<ProductLinkModel> _objAuthorList = (from aco in mobj_AuthorContract
                                                             where aco.ProductId == Product.Id && aco.Deactivate == "N"
                                                             select new ProductLinkModel
                                                             {
                                                                 AuthorContractId = aco.Id,
                                                                 AuthorContractCode = aco.AuthorContractCode
                                                             }).ToList();

                    return Json(_objAuthorList);
                }
                else
                {
                    var mobj_AuthorContract = _IAuthorContractService.GetAllAuthorContract();
                    List<ProductLinkModel> _objAuthorList = (from aco in mobj_AuthorContract
                                                             where (aco.ProductId == Product.Id || aco.ProductId == parentId) && aco.Deactivate == "N"
                                                             select new ProductLinkModel
                                                             {
                                                                 AuthorContractId = aco.Id,
                                                                 AuthorContractCode = aco.AuthorContractCode
                                                             }).ToList();

                    return Json(_objAuthorList);
                }
            }
        }

        [HttpPost]
        public IHttpActionResult GetProductLicenseLinks(ProductSearchDetails Product)
        {
            if (Product.Id == 0)
            {
                return null;
            }
            else
            {
                var parentId = _ProductPreviousProductLinkRepository.Table.Where(x => x.ProductId == Product.Id && x.Deactivate == "N").Select(x => x.PreviousProductId).FirstOrDefault();
                if (parentId == 0)
                {
                    var mobj_ProductLicense = _IProductLicenseService.GetAllProductLicenseList();
                    List<ProductLinkModel> _objProLicenseList = (from pl in mobj_ProductLicense
                                                                 where pl.productid == Product.Id && pl.Deactivate == "N"
                                                                 select new ProductLinkModel
                                                                 {
                                                                     ProductLicenseId = pl.Id,
                                                                     ProductLicenseCode = pl.ProductLicensecode
                                                                 }).ToList();

                    return Json(_objProLicenseList);
                }
                else
                {
                    var mobj_ProductLicense = _IProductLicenseService.GetAllProductLicenseList();
                    List<ProductLinkModel> _objProLicenseList = (from pl in mobj_ProductLicense
                                                                 where (pl.productid == Product.Id || pl.productid == parentId) && pl.Deactivate == "N"
                                                                 select new ProductLinkModel
                                                                 {
                                                                     ProductLicenseId = pl.Id,
                                                                     ProductLicenseCode = pl.ProductLicensecode
                                                                 }).ToList();

                    return Json(_objProLicenseList);
                }
            }
        }

        [HttpPost]
        public IHttpActionResult GetRightsSellingMasterLinks(ProductSearchDetails Product)
        {
            if (Product.Id == 0)
            {
                return null;
            }
            else
            {
                var parentId = _ProductPreviousProductLinkRepository.Table.Where(x => x.ProductId == Product.Id && x.Deactivate == "N").Select(x => x.PreviousProductId).FirstOrDefault();
                if (parentId == 0)
                {
                    var mobj_RightsSellingMaster = _IRightsSelling.GetAllRightsSellingMasterList();
                    List<ProductLinkModel> _objRightsSellingList = (from rsm in mobj_RightsSellingMaster
                                                                    where rsm.ProuductId == Product.Id && rsm.Deactivate == "N"
                                                                    select new ProductLinkModel
                                                                    {
                                                                        RightsSellingId = rsm.Id,
                                                                        RightsSellingCode = rsm.RightsSellingCode,
                                                                        AuthorContractId = rsm.ContractId,
                                                                        ProductLicenseId = rsm.ProuductId
                                                                    }).ToList();

                    return Json(_objRightsSellingList);
                }
                else
                {
                    var mobj_RightsSellingMaster = _IRightsSelling.GetAllRightsSellingMasterList();
                    List<ProductLinkModel> _objRightsSellingList = (from rsm in mobj_RightsSellingMaster
                                                                    where (rsm.ProuductId == Product.Id || rsm.ProuductId == parentId) && rsm.Deactivate == "N"
                                                                    select new ProductLinkModel
                                                                    {
                                                                        RightsSellingId = rsm.Id,
                                                                        RightsSellingCode = rsm.RightsSellingCode,
                                                                        AuthorContractId = rsm.ContractId,
                                                                        ProductLicenseId = rsm.ProuductId
                                                                    }).ToList();

                    return Json(_objRightsSellingList);
                }
            }
        }

        [HttpPost]
        public IHttpActionResult GetPermissionsOutboundMasterLinks(ProductSearchDetails Product)
        {
            if (Product.Id == 0)
            {
                return null;
            }
            else
            {
                var parentId = _ProductPreviousProductLinkRepository.Table.Where(x => x.ProductId == Product.Id && x.Deactivate == "N").Select(x => x.PreviousProductId).FirstOrDefault();
                if (parentId == 0)
                {
                    var mobj_PermissionsOutboundMaster = _IPermissionsOutboundService.getAllPermissionsOutboundMasterList();
                    List<ProductLinkModel> _objPermissionsOutboundList = (from pom in mobj_PermissionsOutboundMaster
                                                                          where pom.productid == Product.Id && pom.Deactivate == "N"
                                                                          select new ProductLinkModel
                                                                          {
                                                                              PermissionsOutboundId = pom.Id,
                                                                              PermissionsOutboundCode = pom.PermissionsOutboundCode,
                                                                              AuthorContractId = pom.ContactId,
                                                                              ProductLicenseId = pom.productid
                                                                          }).ToList();

                    return Json(_objPermissionsOutboundList);
                }
                else
                {
                    var mobj_PermissionsOutboundMaster = _IPermissionsOutboundService.getAllPermissionsOutboundMasterList();
                    List<ProductLinkModel> _objPermissionsOutboundList = (from pom in mobj_PermissionsOutboundMaster
                                                                          where (pom.productid == Product.Id || pom.productid == parentId) && pom.Deactivate == "N"
                                                                          select new ProductLinkModel
                                                                          {
                                                                              PermissionsOutboundId = pom.Id,
                                                                              PermissionsOutboundCode = pom.PermissionsOutboundCode,
                                                                              AuthorContractId = pom.ContactId,
                                                                              ProductLicenseId = pom.productid
                                                                          }).ToList();

                    return Json(_objPermissionsOutboundList);
                }
            }
        }
        #endregion
        
        //Added by Saddam on 25/10/2016
        public IHttpActionResult TopSearch(String Code)
        {
            string foo = Code;
            bool isLetter = char.IsLetter(foo, 0);

            if (isLetter == true)
            {
                ACS.Core.Domain.Product.ProductMaster ProductMaster = _ProductMaster.Table.Where(a => a.ProductCode == Code && a.Deactivate == "N").FirstOrDefault();


                var _ProductMasterValue = new
                {
                    Id = ProductMaster.Id
                };


                return Json(SerializeObj.SerializeObject(new { _ProductMasterValue }));
            }
            else
            {

                ACS.Core.Domain.Product.ProductMaster ProductMaster = _ProductMaster.Table.Where(a => a.OUPISBN == Code && a.Deactivate == "N").FirstOrDefault();


                var _ProductMasterValue = new
                {
                    Id = ProductMaster.Id
                };


                return Json(SerializeObj.SerializeObject(new { _ProductMasterValue }));
            }



        }
        //ended by saddam
        
        [HttpPost]
        public IHttpActionResult GetProductCopyrightYear(ProductSearchDetails Product)
        {
            if (Product.Id == 0)
            {
                return null;
            }
            else
            {


                ACS.Core.Domain.Product.ProductMaster ProductMasterDetail = _ProductMaster.Table.Where(a => a.Id == Product.Id && a.Deactivate == "N").FirstOrDefault();

                var CopyrightYearVlaue = new
                {

                    CopyrightYear = ProductMasterDetail.CopyrightYear
                };


                return Json(CopyrightYearVlaue);
            }
        }
        
        public IHttpActionResult SAPAggreementAuthorDetails(SAPAgreement SAPAgreement)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                if (SAPAgreement != null)
                {
                    parameters[0] = new SqlParameter("ProductId", SqlDbType.VarChar, 50);
                    if (SAPAgreement.id == 0)
                    {
                        parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[0].Value = SAPAgreement.id;
                    }
                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "ProductMasterController.cs", "SAPAggreementAuthorDetails", ex);
                return Json(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "ProductMasterController.cs", "SAPAggreementAuthorDetails", ex);
                return Json(ex.InnerException.Message);
            }

            var _GetSAPAgreementAuthorDetails = _dbContext.ExecuteStoredProcedureListNewData<SAPAgreement>("Proc_SAPAggreementAuthorDetails_get", parameters).ToList();
            
            return Json(_GetSAPAgreementAuthorDetails);
        }
        
        /* Create By  : Prakash
        * Create on  : 05 May, 2017
        * Create for : Delete Product
        */
        [HttpPost]
        public IHttpActionResult DeleteProductSet(ProductSearchDetails mobj_ProductMaster)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            string status = string.Empty;

            try
            {
                if (mobj_ProductMaster.ProductId != 0)
                {
                    parameters[0] = new SqlParameter("ProductId", SqlDbType.VarChar, 100);
                    parameters[0].Value = "'" + mobj_ProductMaster.ProductId + "'";

                    parameters[1] = new SqlParameter("Role", SqlDbType.VarChar, 100);
                    parameters[1].Value = "'" + mobj_ProductMaster.Role + "'";

                    parameters[2] = new SqlParameter("DeactivatedBy", SqlDbType.VarChar, 100);
                    parameters[2].Value = "'" + mobj_ProductMaster.DeactivatedBy + "'";
                }

                var _GetResult = _dbContext.ExecuteStoredProcedureListNewData<ProductSearchDetails>("Proc_Productdelete_set", parameters).ToList();

                if (_GetResult[0].flag == 1)
                    status = "OK";
                else
                    status = "EXIST";

                return Json(status);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }

        }

        /* Create By  : Ankush kumar
         * Create on  : 20 June, 2016
         * Create for : Insert Kit ISBN
         */
        [HttpPost]
        public IHttpActionResult InsertKitISBN(KitISBNModel kitIsbnModel)
        {
            string status = "";
            string ISBN = "";
            string KitISBNId = "";
            IList<ProductLicense> ProductLicenseList = null;
            IList<AuthorContractOriginal> AuthorContractList = null;
            KitISBN KitData = new ACS.Core.Domain.Product.KitISBN();

            foreach (var items in kitIsbnModel.ProductIds)
            {
                AuthorContractList = _ProductMasterService.CheckAuthorContract(items);
                ProductLicenseList = _ProductMasterService.CheckProductLicense(items);
                if (ProductLicenseList.Count == 0 && AuthorContractList.Count == 0)
                {
                    status = "NotLicenseorContractEntered";
                    ISBN = _ProductMasterService.GetProductISBNById(items);
                    return Json(new { status, ISBN });
                }
            }

            ACS.Core.Domain.Master.ISBNBag mobj_ISBNbag = _AddendumServices.GetISBNBagByISBN(kitIsbnModel.ISBN.Trim().ToString());

            if (kitIsbnModel.Id == 0)
            {
                //check exist or not in ISBN bag table
                if (mobj_ISBNbag != null)
                {
                    //check isbn block from any user
                    if (mobj_ISBNbag.Blocked == "Y" && mobj_ISBNbag.ModifiedBy == kitIsbnModel.EnteredBy && mobj_ISBNbag.Used == "N")
                    {

                        KitData.ISBN = kitIsbnModel.ISBN;
                        KitData.Division = kitIsbnModel.Division;
                        KitData.SubDivision = kitIsbnModel.SubDivision;
                        KitData.ProductCategory = kitIsbnModel.ProductCategory;
                        KitData.WorkingProduct = kitIsbnModel.WorkingProduct;
                        KitData.SubWorkingProduct = kitIsbnModel.SubWorkingProduct;
                        KitData.ProjectedPrice = kitIsbnModel.ProjectedPrice;
                        KitData.ProjectedCurrency = kitIsbnModel.ProjectedCurrency;
                        KitData.ProductTypeId = kitIsbnModel.ProductTypeId;

                        List<KitProductLink> KitProductLinkdataList = new List<ACS.Core.Domain.Product.KitProductLink>();
                        for (int i = 0; i < kitIsbnModel.ProductIds.Length; i++)
                        {
                            KitProductLink KitProductLinkdata = new ACS.Core.Domain.Product.KitProductLink();
                            KitProductLinkdata.ProductId = kitIsbnModel.ProductIds[i];
                            KitProductLinkdata.Deactivate = "N";
                            KitProductLinkdata.EnteredBy = kitIsbnModel.EnteredBy;
                            KitProductLinkdata.EntryDate = DateTime.Now;
                            KitProductLinkdata.ModifiedBy = null;
                            KitProductLinkdata.ModifiedDate = null;
                            KitProductLinkdata.DeactivateBy = null;
                            KitProductLinkdata.DeactivateDate = null;
                            KitProductLinkdataList.Add(KitProductLinkdata);
                        }
                        KitData.KitProductLink = KitProductLinkdataList;
                        KitData.Deactivate = "N";
                        KitData.EnteredBy = kitIsbnModel.EnteredBy;
                        KitData.EntryDate = DateTime.Now;
                        KitData.ModifiedBy = null;
                        KitData.ModifiedDate = null;
                        KitData.DeactivateBy = null;
                        KitData.DeactivateDate = null;

                        status = _ProductMasterService.DuplicityKitISBNCheck(KitData);
                        if (status != "duplicateISBN")
                        {
                            int Id = _ProductMasterService.InsertKitMaster(KitData);
                            KitISBNId = Id.ToString();
                            status = "OK";

                            //update ISBN Bag // for Used ISBN
                            mobj_ISBNbag.KitISBNId = Id;
                            mobj_ISBNbag.Used = "Y";
                            mobj_ISBNbag.ModifiedBy = kitIsbnModel.EnteredBy;
                            mobj_ISBNbag.ModifiedDate = DateTime.Now;
                            _AddendumServices.UpdateISBNBag(mobj_ISBNbag);
                        }
                        else
                        {
                            status = "duplicateISBN";
                        }
                    }
                    else
                    {
                        status = "otheruser";
                    }
                }
                else
                {
                    status = "notexistinisbnbag";
                }
            }
            else
            {
                KitISBNId = kitIsbnModel.Id.ToString();

                KitData.Id = kitIsbnModel.Id;
                KitData = _ProductMasterService.GetKitISBNById(KitData);

                //KitData.ISBN = kitIsbnModel.ISBN;
                KitData.Division = kitIsbnModel.Division;
                KitData.SubDivision = kitIsbnModel.SubDivision;
                KitData.ProductCategory = kitIsbnModel.ProductCategory;
                KitData.WorkingProduct = kitIsbnModel.WorkingProduct;
                KitData.SubWorkingProduct = kitIsbnModel.SubWorkingProduct;
                KitData.ProjectedPrice = kitIsbnModel.ProjectedPrice;
                KitData.ProjectedCurrency = kitIsbnModel.ProjectedCurrency;
                KitData.ProductTypeId = kitIsbnModel.ProductTypeId;

                List<KitProductLink> KitProductLinkdataList = new List<ACS.Core.Domain.Product.KitProductLink>();
                KitData.KitProductLink.Clear();
                for (int i = 0; i < kitIsbnModel.ProductIds.Length; i++)
                {
                    KitProductLink KitProductLinkdata = new ACS.Core.Domain.Product.KitProductLink();
                    KitProductLinkdata.KitId = KitData.Id;
                    KitProductLinkdata.ProductId = kitIsbnModel.ProductIds[i];
                    KitProductLinkdata.Deactivate = "N";
                    KitProductLinkdata.EnteredBy = kitIsbnModel.EnteredBy;
                    KitProductLinkdata.EntryDate = DateTime.Now;
                    KitProductLinkdata.ModifiedBy = null;
                    KitProductLinkdata.ModifiedDate = null;
                    KitProductLinkdata.DeactivateBy = null;
                    KitProductLinkdata.DeactivateDate = null;
                    KitProductLinkdataList.Add(KitProductLinkdata);
                }
                KitData.KitProductLink = KitProductLinkdataList;
                KitData.ModifiedBy = kitIsbnModel.EnteredBy;
                KitData.ModifiedDate = DateTime.Now;

                status = _ProductMasterService.DuplicityKitISBNCheck(KitData);
                if (status != "duplicateISBN")
                {
                    int Id = _ProductMasterService.InsertKitMaster(KitData);
                    status = "OK";
                }
                else
                {
                    status = "duplicateISBN";
                }

            }                                

            //return Json(status);
            return Json(new { status, KitISBNId });

        }


        /* Create By  : Ankush kumar
        * Create on  : 21 June, 2016
        * Create for : Insert Kit ISBN
        */
        [HttpPost]
        public IHttpActionResult DeleteKitISBN(KitISBNModel kitIsbnModel)
        {
            string status = "";

            KitISBN KitData = new ACS.Core.Domain.Product.KitISBN();

            KitData.Id = kitIsbnModel.Id;
            KitData = _ProductMasterService.GetKitISBNById(KitData);
            int Id = _ProductMasterService.DeleteKitMaster(KitData);
            status = "OK";



            return Json(status);

        }

        /* Create By  : Prakash
       * Create on  : 13 Oct, 2017
       * Create for : Delete Product
       */
        //[HttpGet]
        [HttpPost]
        public IHttpActionResult GetKitIsbnData(KitISBNModel kitIsbnModel) //string mstr_ISBN
        {
            SqlParameter[] parameters = new SqlParameter[8];

            try
            {

                //if (mstr_ISBN == "autocomplete")
                //{
                //    mstr_ISBN = "0";
                //    parameters[0] = new SqlParameter("KitISBN", SqlDbType.VarChar, 100);
                //    parameters[0].Value = "'" + mstr_ISBN + "'";

                //    var _GetResult = _dbContext.ExecuteStoredProcedureListNewData<KitISBNModel>("Proc_SearchKitIsbnData_get", parameters).ToList();
                //    var _GetISBN = _GetResult.Select(a => a.ISBN).Distinct().ToList();
                //    return Json(_GetISBN);
                //}
                //else
                //{
                //    parameters[0] = new SqlParameter("KitISBN", SqlDbType.VarChar, 100);
                //    parameters[0].Value = "'" + mstr_ISBN + "'";

                //    var _GetResult = _dbContext.ExecuteStoredProcedureListNewData<KitISBNModel>("Proc_SearchKitIsbnData_get", parameters).ToList();
                //    return Json(_GetResult);
                //}

                parameters[0] = new SqlParameter("Division", SqlDbType.VarChar, 100);
                parameters[0].Value = "'" + kitIsbnModel.Division + "'";

                parameters[1] = new SqlParameter("Subdivision", SqlDbType.VarChar, 100);
                parameters[1].Value = "'" + kitIsbnModel.SubDivision + "'";

                parameters[2] = new SqlParameter("ProductCategory", SqlDbType.VarChar, 100);
                parameters[2].Value = "'" + kitIsbnModel.ProductCategory + "'";

                parameters[3] = new SqlParameter("WorkingProduct", SqlDbType.VarChar, 100);
                parameters[3].Value = "'" + kitIsbnModel.WorkingProduct + "'";

                parameters[4] = new SqlParameter("SubWorkingProduct", SqlDbType.VarChar, 100);
                parameters[4].Value = "'" + kitIsbnModel.SubWorkingProduct + "'";

                parameters[5] = new SqlParameter("KitISBN", SqlDbType.VarChar, 100);
                parameters[5].Value = "'" + kitIsbnModel.ISBN + "'";

                parameters[6] = new SqlParameter("ProductISBN", SqlDbType.VarChar, 100);
                parameters[6].Value = "'" + kitIsbnModel.ProductISBN + "'";

                parameters[7] = new SqlParameter("ProductTypeId", SqlDbType.VarChar, 100);
                parameters[7].Value = "'" + kitIsbnModel.ProductTypeId + "'";

                var _GetResult = _dbContext.ExecuteStoredProcedureListNewData<KitISBNModel>("Proc_SearchKitIsbnData_get", parameters).ToList();
                return Json(_GetResult);
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "ProductMasterController.cs", "GetKitIsbnData", ex);
                return Json(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "ProductMasterController.cs", "GetKitIsbnData", ex);
                return Json(ex.InnerException.Message);
            }
        }


        /* Create By  : Prakash
      * Create on  : 13 Oct, 2017
      * Create for : Delete Product
      */
        [HttpGet]
        public IHttpActionResult GetKitIsbnDataById(string Id = "") 
        {
            SqlParameter[] parameters = new SqlParameter[1];
            try
            {
                if (Id != "")
                {
                    parameters[0] = new SqlParameter("Id", SqlDbType.VarChar, 100);
                    parameters[0].Value = "'" + Id + "'";

                    var _GetResult = _dbContext.ExecuteStoredProcedureListNewData<KitISBNModel>("Proc_KitIsbnDetails_get", parameters).ToList();
                    return Json(_GetResult);
                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "ProductMasterController.cs", "GetKitIsbnDataById", ex);
                return Json(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "ProductMasterController.cs", "GetKitIsbnDataById", ex);
                return Json(ex.InnerException.Message);
            }
            return null;
        }

        
        //Create by : Saddam
        //Purpose : Display SAP Aggrement List
        [HttpGet]
        public IHttpActionResult getProductSapAggrementList()
        {
            try
            {
                var _SAPAgreementdetails = _dbContext.ExecuteStoredProcedureListNewData<SAPAggrementList>("Proc_ProductSAPAgreementdetails_get").ToList();
                return Json(_SAPAgreementdetails);
            }
            catch (Exception ex)
            {
                return Json("error");
                //return null;
            }

        }
        
        ///Added by Saddam on 22/08/2017
        [HttpPost]
        public IHttpActionResult getSapProductDetails(ProductSAPAgreementMaster _SAPProductDetails)
        {


            ACS.Core.Domain.Product.ProductMaster ProductMaster = _ProductMaster.Table.Where(i => i.OUPISBN == _SAPProductDetails.OUPISBN && i.Deactivate == "N").FirstOrDefault();

            int ProductID = ProductMaster.Id;
            return Json(ProductID);

        }
        //Ended by Saddam
        
        ///Added by Saddam on 25/08/2017
        [HttpPost]
        public IHttpActionResult getProductAuthorFill(ProductSAPAgreementMaster _SAPAggrementAssignFill)
        {
            IList<ProductSAPAgreementMaster> ProductSAPAgreementMaster = _ProductSAPAgreementMaster.Table.Where(i => i.OUPISBN == _SAPAggrementAssignFill.OUPISBN && i.Deactivate == "N").OrderBy(i=>i.Id).ToList();

            if (ProductSAPAgreementMaster.Count > 0)
            {
                var _SAPagreementNo = ProductSAPAgreementMaster.Select(i => new
                {
                    Id = i.Id,
                    SAPagreementNo = i.SAPagreementNo,
                   
                }).Where(i => i.SAPagreementNo != null);


                var _AuthorCode = ProductSAPAgreementMaster.Select(i => new
               {
                   Id = i.Id,
                   AuthorCode = i.AuthorCode,
                  
               }).Where(i => i.AuthorCode != null).Distinct();


                return Json(new { _SAPagreementNo, _AuthorCode });
            }
            else
            {
                var _SAPagreementNo = string.Empty;
                var _AuthorCode = string.Empty;
                var _SapAggrementId = string.Empty;
                return Json(new { _SAPagreementNo, _AuthorCode });

            }



        }
       
        //Ended by Saddam



        //Added by Saddam on 28/08/2017
        public IHttpActionResult SAPAggreementAuthor_viewDetails(SAPAggrementList SAPAgreement)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                if (SAPAgreement != null)
                {
                    parameters[0] = new SqlParameter("OUPISBN", SqlDbType.VarChar, 50);
                    if (SAPAgreement.OUPISBN == "" && SAPAgreement.OUPISBN == null)
                    {
                        parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[0].Value = SAPAgreement.OUPISBN;
                    }


                }


            }
            catch (Exception ex)
            {
            }
            var _GetSAPAgreementAuthorDetails = _dbContext.ExecuteStoredProcedureListNewData<SAPAggrementList>("Proc_SAPAggreementAuthorDetails_View_get", parameters).ToList();


            return Json(_GetSAPAgreementAuthorDetails);
        }

        //ended by Saddam

        //Added by Saddam on 28/08/2017
        public IHttpActionResult UpdateSAPAggrementDetails(IList<ProductSAPAgreementMaster> IProductSAPAgreement)
        {
            string SAPagreementNo = "";
            string AuthorCode = "";

            if (AuthorCode != "")
            {
                AuthorCode = "Duplicate Author Code are : " + AuthorCode.Substring(0, AuthorCode.Length - 1);
            }
            if (SAPagreementNo != "")
            {
                SAPagreementNo = "Duplicate SAP agreement Nos. are : " + SAPagreementNo.Substring(0, SAPagreementNo.Length - 1);
            }

            if (AuthorCode != "" || SAPagreementNo != "")
            {
                return Json(AuthorCode + "\n" + SAPagreementNo);
            }
            else
            {
                foreach (ProductSAPAgreementMaster SAPAgrrement in IProductSAPAgreement)
                {
                    ProductSAPAgreementMaster mobj_SAPAggrement = _ProductMasterService.GetSapAgreementNumberById(SAPAgrrement);

                    ////mobj_SAPAggrement.Id = SAPAgrrement.Id;
                    mobj_SAPAggrement.OUPISBN = SAPAgrrement.OUPISBN;

                    if (SAPAgrrement.AuthorCode == null)
                    {
                        mobj_SAPAggrement.SAPagreementNo = SAPAgrrement.SAPagreementNo;
                        //mobj_SAPAggrement.AuthorId = SAPAgrrement.AuthorId;
                    }
                    else
                    {
                        mobj_SAPAggrement.AuthorCode = SAPAgrrement.AuthorCode;
                        //mobj_SAPAggrement.AuthorId = SAPAgrrement.AuthorId;
                    }
                    mobj_SAPAggrement.ModifiedBy = SAPAgrrement.EnteredBy;
                    mobj_SAPAggrement.ModifiedDate = DateTime.Now;
                    _ProductMasterService.UpdateSapAggrement(mobj_SAPAggrement);
                }
            }
            return Json("OK");

        }
        //Ended by Saddam

        /* Create By  : Prakash
       * Create on  : 05 May, 2017
       * Create for : Delete Product
       */
        [HttpPost]
        public IHttpActionResult DeleteProductSAPAgreementMasterSet(ProductSAPAgreementMaster mobj_ProductSAPAgreementMaster)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            string status = string.Empty;

            try
            {
                if (mobj_ProductSAPAgreementMaster.OUPISBN != "")
                {
                    parameters[0] = new SqlParameter("OUPISBN", SqlDbType.VarChar, 100);
                    parameters[0].Value = "'" + mobj_ProductSAPAgreementMaster.OUPISBN + "'";

                    parameters[1] = new SqlParameter("DeactivateBy", SqlDbType.VarChar, 100);
                    parameters[1].Value = "'" + mobj_ProductSAPAgreementMaster.DeactivateBy + "'";

                    var _GetResult = _dbContext.ExecuteStoredProcedureListNewData<SAPAggrementModel>("Proc_ProductSAPAgreementMasterdelete_set", parameters).ToList();

                    status = "OK";
                }

                return Json(status);
            }
            catch (Exception ex)
            {
                return Json(ex.ToString());
            }

        }



        /** Create By    :   Saddam
         Create On    :   11/10/2017
         Created For   :   Fetching the details of Kit ISBN List
   */

        public IHttpActionResult KitISBNDetails(KitISBNListModel KitISBN)
        {
            SqlParameter[] parameters = new SqlParameter[2];

            try
            {
                if (KitISBN != null)
                {
                    parameters[0] = new SqlParameter("KitISBNId", SqlDbType.VarChar, 50);
                    if (KitISBN.Id == 0)
                    {
                        parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[0].Value = KitISBN.Id;
                    }


                    parameters[1] = new SqlParameter("Flag", SqlDbType.VarChar, 50);
                    if (KitISBN.Flag == "")
                    {
                        parameters[1].Value = System.Data.SqlTypes.SqlInt32.Null;
                    }
                    else
                    {
                        parameters[1].Value = KitISBN.Flag;
                    }

                }


            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "ProductMasterController.cs", "KitISBNDetails", ex);
                 return Json(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "ProductMasterController.cs", "KitISBNDetails", ex);
                return Json(ex.InnerException.Message);
            }

            var _GetKitISBNList = _dbContext.ExecuteStoredProcedureListNewData<KitISBNListModel>("Proc_KitISBNProductList_get", parameters).ToList();


            return Json(_GetKitISBNList);
        }


        public IHttpActionResult InsertProductKitISBN(KitISBNListModel Object)
        {
            
            string status = "notOK";
            using (var scope = new System.Transactions.TransactionScope())
            { 
            
            try
            {


                    foreach (var item in Object.ProductKitIsbn)
                    {
                      
                        if(item.ProductLicenseId != 0)
                        {
                         ACS.Core.Domain.Product.ProductLicense mobj_ProductLicense = new ACS.Core.Domain.Product.ProductLicense();

                        mobj_ProductLicense = _ProductLicense.Table.Where(i => i.Id == item.ProductLicenseId && i.Deactivate == "N").FirstOrDefault();

                        if (item.Check_NoOfImpressions.ToLower() == "p")
                        {
                            mobj_ProductLicense.noofimpressions = item.NoOfImpressions;
                        }
                        if (item.Unrestricted_Check.ToLower() != "unrestricted")
                        {
                            mobj_ProductLicense.balanceqty = item.Balanceqty;
                        }

                      
                        mobj_ProductLicense.ModifiedBy = item.EnteredBy;
                        mobj_ProductLicense.ModifiedDate = DateTime.Now;
                                             
                        _ProductMasterService.UpdateProductLicenseForKit(mobj_ProductLicense);

                        }

                

                        ImpressionDetails mobj_ImpressionDetails = new ImpressionDetails();
                        mobj_ImpressionDetails.ProductId = item.ProductId;

                        if (item.ProductLicenseId != 0)
                        {
                            mobj_ImpressionDetails.LicenseId = item.ProductLicenseId;
                        }

                       

                        if (item.ContractId != 0)
                        {
                            mobj_ImpressionDetails.ContractId = item.ContractId;
                        }

                        mobj_ImpressionDetails.ImpressionNo = item.ImpressionNo;
                        mobj_ImpressionDetails.ImpressionDate = item.ImpressionDate;
                        mobj_ImpressionDetails.QunatityPrinted = item.QunatityPrinted;

                        if (item.Unrestricted_Check.ToLower() != "unrestricted")
                        {
                            mobj_ImpressionDetails.BalanceQty = item.Balanceqty;
                        }

                       

                        mobj_ImpressionDetails.Deactivate = "N";
                        mobj_ImpressionDetails.EnteredBy = item.EnteredBy;
                        mobj_ImpressionDetails.EntryDate = DateTime.Now;
                        mobj_ImpressionDetails.ModifiedBy = null;
                        mobj_ImpressionDetails.ModifiedDate = null;
                        mobj_ImpressionDetails.DeactivateBy = null;
                        mobj_ImpressionDetails.DeactivateDate = null;
                        mobj_ImpressionDetails.KitISBNId = item.KitISBNId;

                        _AddendumServices.ImsertImpressionDetails(mobj_ImpressionDetails);

                        if (item.Unrestricted_Check.ToLower() != "unrestricted")
                        {

                            AddendumDetails mobj_AddendumDetails;

                            IList<AddendumDetails> _AddemDumList = new List<AddendumDetails>();

                            _AddemDumList = _AddendumDetails.Table.Where(i => i.LicenseId == item.ProductLicenseId && i.Deactivate == "N").ToList();


                            if (_AddemDumList.Count > 0)
                            {
                                mobj_AddendumDetails = _AddemDumList.LastOrDefault();


                                if (item.Check_NoOfImpressions.ToLower() == "a")
                                {
                                    mobj_AddendumDetails.NoOfImpressions = item.NoOfImpressions.GetValueOrDefault();
                                }

                                mobj_AddendumDetails.BalanceQuantity = item.Balanceqty;

                                _AddendumServices.UpdateAddendumDetails(mobj_AddendumDetails);
                            }


                           
                        
                        }

                        

                    }
                    scope.Complete();
                    status = "OK";
                   
            }


            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "ProductMasterController.cs", "InsertProductKitISBN", ex);
                 status = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "ProductMasterController.cs", "InsertProductKitISBN", ex);
                status = ex.InnerException.Message;
            }
           
            return Json(new { status });
        }
    }


        /* Create By  : Prakash
      * Create on  : 13 Oct, 2017
      * Create for : Delete Product
      */
        [HttpGet]
        public IHttpActionResult GetIsbnConvert(string isbn13 = "")
        {
            string status = string.Empty;
            try
            {
                if (isbn13 != "")
                {
                    bool IsValid = IsValidIsbn13(isbn13);
                    if (IsValid == false)
                    {
                        status = "INVALID";
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(isbn13))
                            throw new ArgumentNullException("isbn13");
                        isbn13 = isbn13.Replace("-", "").Replace(" ", "");
                        if (isbn13.Length != 13)
                        {
                            status = "INVALID";
                        }
                        String isbn10 = isbn13.Substring(3, 9);
                        int checksum = 0;
                        int weight = 10;

                        foreach (Char c in isbn10)
                        {
                            checksum += (int)Char.GetNumericValue(c) * weight;
                            weight--;
                        }

                        checksum = 11 - (checksum % 11);
                        if (checksum == 10)
                            isbn10 += "X";
                        else if (checksum == 11)
                            isbn10 += "0";
                        else
                            isbn10 += checksum;

                        status = isbn10;
                    }
                    return Json(status);
                }
            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "ProductMasterController.cs", "GetKitIsbnConvert", ex);
                return Json(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "ProductMasterController.cs", "GetKitIsbnConvert", ex);
                return Json(ex.InnerException.Message);
            }
            return null;
        }

        private static bool IsValidIsbn13(string isbn13)
        {
            bool result = false;

            if (isbn13.Length != 13)
                return false;

            if (!string.IsNullOrEmpty(isbn13))
            {
                long j;
                if (isbn13.Contains('-')) isbn13 = isbn13.Replace("-", "");

                // Check if it contains any non numeric chars, if yes, return false
                if (!Int64.TryParse(isbn13, out j))
                    result = false;

                int sum = 0;
                for (int i = 0; i < 12; i++)
                {
                    sum += Int32.Parse(isbn13[i].ToString()) * (i % 2 == 1 ? 3 : 1);
                }

                int remainder = sum % 10;
                int checkDigit = 10 - remainder;
                if (checkDigit == 10) checkDigit = 0;
                result = (checkDigit == int.Parse(isbn13[12].ToString()));
            }
            return result;
        }


        ////fetch Kit Details List //added by Prakash on 13 Nov, 2017
        public IHttpActionResult KitISBNDetailsListByProductId(int ProductId)
        {
            SqlParameter[] parameters = new SqlParameter[1];

            try
            {
                parameters[0] = new SqlParameter("ProductId", SqlDbType.Int);
                if (ProductId == 0)
                {
                    parameters[0].Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    parameters[0].Value = ProductId;
                }

            }
            catch (ACSException ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "ProductMasterController.cs", "KitISBNDetailsListByProductId", ex);
                return Json(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                _ILog.LogException("", Severity.ProcessingError, "ProductMasterController.cs", "KitISBNDetailsListByProductId", ex);
                return Json(ex.InnerException.Message);
            }

            var _GetKitISBNList = _dbContext.ExecuteStoredProcedureListNewData<KitISBNListModel>("Proc_KitISBNDetailsList_get", parameters).ToList();


            return Json(_GetKitISBNList);
        }


    }

}

