using ACS.Core.Data;
using ACS.Core.Domain.Product;
using ACS.Core.Domain.AuthorContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Web.Mvc;
using ACS.Core.Domain.Master;
using ACS.Data;
using ACS.Core.Domain.AuthorContract;

namespace ACS.Services.Product
{
    public partial class ProductMasterService : IProductMasterService
    {
        #region Fields
        private readonly IRepository<ProductMaster> _ProductRepository;
        private readonly IRepository<ProductCategoryMaster> _ProductCategoryMasterRepository;
        private readonly IRepository<ProductAuthorLink> _ProductAuthorLinkRepository;
        private readonly IRepository<ProprietorMaster> _ProprietorMasterRepository;
        private readonly IRepository<ProprietorAuthorLink> _ProprietorAuthorLinkRepository;
        private readonly IRepository<ProductPreviousProductLink> _ProductPreviousProductLink;
        private readonly IRepository<SearchHistory> _SearchHistory;
        private readonly IRepository<ProductSAPAgreementMaster> _ProductSAPAgreementMaster;
        private readonly IRepository<AuthorContractOriginal> _AuthorContract;
        private readonly IRepository<ProductLicense> _ProductLicense;
        private readonly IDbContext _dbContext;
        private readonly IRepository<KitISBN> _KitISBNRepository;
        private readonly IRepository<KitProductLink> _KitProductLinkRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="GeographyType">Geography</param>        
        public ProductMasterService(
        IRepository<ProductMaster> ProductRepository
        , IRepository<ProductCategoryMaster> ProductCategoryMasterRepository
        , IRepository<ProductAuthorLink> ProductAuthorLinkRepository
        , IRepository<ProprietorMaster> ProprietorMasterRepository
        , IRepository<ProprietorAuthorLink> ProprietorAuthorLinkRepository
        , IRepository<ProductPreviousProductLink> ProductPreviousProductLink
        , IRepository<ProductSAPAgreementMaster> ProductSAPAgreementMaster
        , IDbContext dbContext
        , IRepository<AuthorContractOriginal> AuthorContract
        , IRepository<ProductLicense> ProductLicense
        , IRepository<SearchHistory> SearchHistory
         , IRepository<KitISBN> KitISBNRepository
            , IRepository<KitProductLink> KitProductLinkRepository
        )
        {
            _ProductRepository = ProductRepository;
            _ProductCategoryMasterRepository = ProductCategoryMasterRepository;
            _ProductAuthorLinkRepository = ProductAuthorLinkRepository;
            _ProprietorMasterRepository = ProprietorMasterRepository;
            _ProprietorAuthorLinkRepository = ProprietorAuthorLinkRepository;
            _ProductPreviousProductLink = ProductPreviousProductLink;
            _ProductSAPAgreementMaster = ProductSAPAgreementMaster;
            _dbContext = dbContext;
            _AuthorContract = AuthorContract;
            _ProductLicense = ProductLicense;
            _SearchHistory = SearchHistory;
            _KitISBNRepository = KitISBNRepository;
            _KitProductLinkRepository = KitProductLinkRepository;
        }



        #endregion

        #region Methods



        public int InsertProductMaster(ProductMaster Product)
        {

            Product.ProductCode = GetProductCode(Product.ProductCategoryId);
            _ProductRepository.Insert(Product);
            return Product.Id;
        }

        public void UpdateProductMaster(ProductMaster Product)
        {
            _ProductRepository.Update(Product);
        }

        public void UpdateProprietorMaster(ProprietorMaster Proprietor)
        {
            _ProprietorMasterRepository.Update(Proprietor);
        }

        public void DeleteProductProprietorMaster(ProprietorMaster Proprietor)
        {
            _ProprietorMasterRepository.Delete(Proprietor);
        }

        public void DeleteProprietorAuthorLink(ProprietorAuthorLink ProprietorAuthor)
        {
            _ProprietorAuthorLinkRepository.Delete(ProprietorAuthor);
        }

        public void DeleteProductAuthorLink(ProductAuthorLink ProductAuthor)
        {
            _ProductAuthorLinkRepository.Delete(ProductAuthor);
        }

        public string DuplicityProjectCodeCheck(ProductMaster Product)
        {

            var mstr_returnmsg = "notexist";
            var dupes = _ProductRepository.Table.Where(x => x.WorkingProduct == Product.WorkingProduct && x.WorkingProduct != null && x.Deactivate == "N"
                                            && (Product.Id != 0 ? x.Id : 0) != (Product.Id != 0 ? Product.Id : 1)).FirstOrDefault();
            if (dupes != null)
            {
                mstr_returnmsg = "duplicate";

            }

            return mstr_returnmsg;
        }

        //Add by Ankush 02/09/2016
        public string DuplicityISBNCheck(ProductMaster Product)
        {

            var mstr_returnmsg = "notexist";
            var dupes = _ProductRepository.Table.Where(x => x.OUPISBN == Product.OUPISBN && x.OUPISBN != null && x.Deactivate == "N"
                                            && (Product.Id != 0 ? x.Id : 0) != (Product.Id != 0 ? Product.Id : 1)).FirstOrDefault();
            if (dupes != null)
            {
                mstr_returnmsg = "duplicateISBN";

            }

            return mstr_returnmsg;
        }

        //Add by Ankush 19/10/2016
        public IList<AuthorContractOriginal> CheckAuthorContract(int ProductId)
        {
            var AuthorContractList = _AuthorContract.Table.Where(x => x.ProductId == ProductId && x.ProductId != null && x.Deactivate == "N").ToList();

            return AuthorContractList;
        }

        //Add by Prakash on 16 Aug, 2017
        public IList<ProductLicense> CheckProductLicense(int ProductId)
        {
            var productLicenseList = _ProductLicense.Table.Where(x => x.productid == ProductId && x.Deactivate == "N").ToList();

            return productLicenseList;
        }

        public string GetProductCode(int productCategoryId)
        {
            var ProductCodeNumber = _ProductRepository.Table.OrderByDescending(a => a.ProductCode.Substring(2, 4)).Select(a => a.ProductCode.Substring(2, 4)).FirstOrDefault();
            if (ProductCodeNumber == null)
            {
                ProductCodeNumber = "0000";
            }
            var productCategory = _ProductCategoryMasterRepository.Table.Where(a => a.Id == productCategoryId).Select(a => a.ProductCategory).FirstOrDefault();
            ProductCodeNumber = "P" + productCategory.Substring(0, 1) + (Convert.ToInt16(ProductCodeNumber) + 1).ToString().PadLeft(4, '0');
            return ProductCodeNumber;

        }

        public ProductMaster GetProductById(ProductMaster Product)
        {

            return _ProductRepository.Table.Where(x => x.Id == Product.Id && x.Deactivate == "N").FirstOrDefault();
        }

        public IList<ProprietorAuthorLink> GetProprietorAuthorByProprietorId(ProprietorMaster Proprietor)
        {

            return _ProprietorAuthorLinkRepository.Table.Where(x => x.ProprietorId == Proprietor.Id && x.Deactivate == "N").ToList();
        }

        public int ValidISBN(string ISBN)
        {
            var ProductDetail = _ProductRepository.Table.Where(x => x.OUPISBN == ISBN && x.Deactivate == "N").FirstOrDefault();
            if (ProductDetail != null)
            {
                return ProductDetail.Id;
            }
            else
            {
                return 0;
            }
        }

        public string GetProductISBNById(int ProductId)
        {
            var OUPISBN = _ProductRepository.Table.Where(x => x.Id == ProductId && x.Deactivate == "N").Select(x => x.OUPISBN).FirstOrDefault();
            return OUPISBN;
        }

        public void DeletePreviousProductLink(ProductPreviousProductLink ProductPreviousProductLink)
        {
            _ProductPreviousProductLink.Delete(ProductPreviousProductLink);
        }

        public IList<ProductSapAgreementTemp> NotEnteredSAPAgreementNoList()
        {

            //var NotEnteredSAPAgreementNoList = (_ProductRepository.TableNoTracking
            //    .Join(_ProductSAPAgreementMaster.TableNoTracking,
            //        x => new { OUPISBN = x.OUPISBN },
            //        y => new { OUPISBN = y.OUPISBN },
            //          (x, y) => new { ProductMas = x, ProductSap = y }
            //        )
            //    .Where(p => p.ProductMas.Deactivate == "N"
            //        && p.ProductSap.Deactivate == "N"))
            //    .ToList();

            //return NotEnteredSAPAgreementNoList.Select(
            //    c => new ProductSapAgreementTemp()
            //    {
            //        ProductCode = c.ProductMas.ProductCode,
            //        POUPISBN = c.ProductMas.OUPISBN,
            //        WorkingProduct = c.ProductMas.WorkingProduct
            //    }
            //    ).OrderBy(c => c.ProductCode)
            //    .Distinct().ToList();

            var NotEnteredSAPAgreementNoList = _dbContext.ExecuteStoredProcedureListNewData<ProductSapAgreementTemp>("Proc_ProductListForSAPNumberNotEntered_get").ToList();
            return NotEnteredSAPAgreementNoList;
        }

        public IList<ProductSapAgreementTemp> NotEnteredISBNForProductList()
        {

            //var NotEnteredISBNForProductList = (_ProductRepository.TableNoTracking
            //    .Join(_ProductSAPAgreementMaster.TableNoTracking,
            //        x => new { OUPISBN = x.OUPISBN },
            //        y => new { OUPISBN = y.OUPISBN },
            //          (x, y) => new { ProductMas = x, ProductSap = y }
            //        )
            //    .Where(p => p.ProductMas.Deactivate == "N"
            //        && p.ProductSap.Deactivate == "N"))
            //    .ToList();

            //return NotEnteredISBNForProductList.Select(
            //    c => new ProductSapAgreementTemp()
            //    {
            //        ProductCode = c.ProductMas.ProductCode,
            //        POUPISBN = c.ProductMas.OUPISBN,
            //        WorkingProduct = c.ProductMas.WorkingProduct
            //    }
            //    ).OrderBy(c => c.ProductCode)
            //    .Distinct().ToList();


            var NotEnteredISBNForProductList = _dbContext.ExecuteStoredProcedureListNewData<ProductSapAgreementTemp>("Proc_ProductListForISBNNotEntered_get").ToList();
            return NotEnteredISBNForProductList;
        }

        public IEnumerable<object> AuthorProductNotSigned()
        {
            return _ProductAuthorLinkRepository.Table.Where(x => !_AuthorContract.Table.Any(y => y.ProductId == x.ProductAuthorLinkProduct.Id)
                                                                && x.ProductAuthorLinkProduct.Deactivate == "N")
                                                                  .Select(x => new
                                                                  {
                                                                      Id = x.ProductId,
                                                                      ProductCode = x.ProductAuthorLinkProduct.ProductCode,
                                                                      ISBN = x.ProductAuthorLinkProduct.OUPISBN,
                                                                      WorkingProduct = x.ProductAuthorLinkProduct.WorkingProduct,
                                                                      AuthorName = x.ProductAuthorLinkAuthor.FirstName + " " + x.ProductAuthorLinkAuthor.LastName
                                                                  }).OrderBy(i => i.ProductCode).ToList();

        }

        public IEnumerable<object> LicenceProductNotSigned()
        {
            return _ProductAuthorLinkRepository.Table.Where(x => !_ProductLicense.Table.Any(y => y.productid == x.ProductAuthorLinkProduct.Id)
                                                                && x.ProductAuthorLinkProduct.Deactivate == "N")
                                                                  .Select(x => new
                                                                  {
                                                                      Id = x.ProductId,
                                                                      ProductCode = x.ProductAuthorLinkProduct.ProductCode,
                                                                      ISBN = x.ProductAuthorLinkProduct.OUPISBN,
                                                                      WorkingProduct = x.ProductAuthorLinkProduct.WorkingProduct,
                                                                      AuthorName = x.ProductAuthorLinkAuthor.FirstName + " " + x.ProductAuthorLinkAuthor.LastName
                                                                  }).OrderBy(i => i.ProductCode).ToList();

        }


        public IList<ProductAuthorLink> ProductAuthorList(ProductAuthorLink PAuthorLink)
        {
            return _ProductAuthorLinkRepository.Table.Where(x => x.ProductId == PAuthorLink.ProductId && x.Deactivate == "N").ToList();

        }

        public void InsertProductSAPAggreentDetails(ProductSAPAgreementMaster ProductSAPAgreementMaster)
        {
            _ProductSAPAgreementMaster.Insert(ProductSAPAgreementMaster);

        }

        /*
         Created By     :   Dheeraj Kumar sharma
         * Created on   :   17/07/2016
         * Created for  :   Inserting the
         
         */
        public void InsertSearchHistory(SearchHistory _SearchParam)
        {
            _SearchHistory.Insert(_SearchParam);

        }

        //Add By Ankush On 21/07/2016 For getting all list of Product Master
        public IList<ProductMaster> GetProductMasterList()
        {
            return _ProductRepository.Table.Where(a => a.Deactivate == "N").ToList();
        }

        //Add By Ankush on 28/07/2016 For Check Duplicate or Note
        public string DuplicityProductSAPAgreementMasterCheck(ProductSAPAgreementMaster productSAPAgreement)
        {
            var mstr_returnmsg = "";
            if (!string.IsNullOrEmpty(productSAPAgreement.SAPagreementNo))
            {
                var dupes = _ProductSAPAgreementMaster.Table.Where(x => x.SAPagreementNo == productSAPAgreement.SAPagreementNo && x.Deactivate == "N").FirstOrDefault();
                if (dupes != null)
                {
                    mstr_returnmsg = productSAPAgreement.SAPagreementNo;
                }
            }

            if (!string.IsNullOrEmpty(productSAPAgreement.AuthorCode))
            {
                var dupes = _ProductSAPAgreementMaster.Table.Where(x => x.AuthorCode == productSAPAgreement.AuthorCode && x.Deactivate == "N").FirstOrDefault();
                if (dupes != null)
                {
                    mstr_returnmsg = productSAPAgreement.AuthorCode;
                }
            }

            return mstr_returnmsg;
        }



        //Add By Dheeraj On 21/07/2016 For getting series detail based productid
        public ProductMaster getSeriesDetails(int ProductId)
        {
            return _ProductRepository.Table.Where(i => i.Id == ProductId).FirstOrDefault();

        }

        public KitISBN GetKitISBNById(KitISBN kitISBN)
        {

            return _KitISBNRepository.Table.Where(x => x.Id == kitISBN.Id && x.Deactivate == "N").FirstOrDefault();
        }

        public KitProductLink GetKitProductLinkById(KitProductLink kitProductLink)
        {

            return _KitProductLinkRepository.Table.Where(x => x.Id == kitProductLink.Id && x.Deactivate == "N").FirstOrDefault();
        }

        public int InsertKitMaster(KitISBN kitIsbndata)
        {
            if (kitIsbndata.Id == 0)
            {
                _KitISBNRepository.Insert(kitIsbndata);
            }
            else
            {
                List<KitProductLink> dataList = _KitProductLinkRepository.Table.Where(i => i.KitId == kitIsbndata.Id && i.Deactivate == "N").ToList();

                foreach (KitProductLink list in dataList)
                {
                    list.Deactivate = "Y";
                    list.DeactivateBy = kitIsbndata.EnteredBy;
                    list.DeactivateDate = DateTime.Now;
                    _KitProductLinkRepository.Update(list);
                    //_KitProductLinkRepository.Delete(list);
                }

                //foreach (KitProductLink list in kitIsbndata.KitProductLink)
                //{
                //    list.Deactivate = "N";
                //    list.EnteredBy = kitIsbndata.EnteredBy;
                //    list.EntryDate = DateTime.Now;
                //    list.KitId = kitIsbndata.Id;
                //    _KitProductLinkRepository.Insert(list);
                //}

                _KitISBNRepository.Update(kitIsbndata);
            }
            return kitIsbndata.Id;
        }

        //Add by Ankush 20/06/2016
        public string DuplicityKitISBNCheck(KitISBN kitIsbndata)
        {

            var mstr_returnmsg = "notexist";
            var dupes = _KitISBNRepository.Table.Where(x => x.ISBN == kitIsbndata.ISBN && x.ISBN != null && x.Deactivate == "N"
                                            && (kitIsbndata.Id != 0 ? x.Id : 0) != (kitIsbndata.Id != 0 ? kitIsbndata.Id : 1)).FirstOrDefault();
            if (dupes != null)
            {
                mstr_returnmsg = "duplicateISBN";

            }

            return mstr_returnmsg;
        }

        public int DeleteKitMaster(KitISBN kitIsbndata)
        {
            List<KitProductLink> dataList = _KitProductLinkRepository.Table.Where(i => i.KitId == kitIsbndata.Id && i.Deactivate == "N").ToList();

            foreach (KitProductLink list in dataList)
            {
                list.Deactivate = "Y";
                list.DeactivateBy = kitIsbndata.EnteredBy;
                list.DeactivateDate = DateTime.Now;
                _KitProductLinkRepository.Update(list);
                //_KitProductLinkRepository.Delete(list);
            }

            kitIsbndata.Deactivate = "Y";
            kitIsbndata.DeactivateBy = kitIsbndata.EnteredBy;
            kitIsbndata.DeactivateDate = DateTime.Now;
            _KitISBNRepository.Update(kitIsbndata);

            return kitIsbndata.Id;
        }

        #endregion

        public void InsertProprietorMaster(ProprietorMaster Proprietor)
        {
            _ProprietorMasterRepository.Insert(Proprietor);
        }

        public ProductSAPAgreementMaster GetSapAgreementNumberById(ProductSAPAgreementMaster ProductSAPAgreement)
        {
            return _ProductSAPAgreementMaster.Table.Where(x => x.Id == ProductSAPAgreement.Id && x.Deactivate == "N").FirstOrDefault();
        }

        //Added by Saddam on 28/08/2017
        public void UpdateSapAggrement(ProductSAPAgreementMaster ProductSAPAgreement)
        {
            _ProductSAPAgreementMaster.Update(ProductSAPAgreement);
        }

        //Enedd By Saddam



        //Added by Saddam on 12/10/2017 for Product License For Kit  Update
        public void UpdateProductLicenseForKit(ProductLicense ProductLicense)
        {
            _ProductLicense.Update(ProductLicense);
        }

        //Enedd By Saddam
        
    }
}



