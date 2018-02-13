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
using ACS.Core.Data;
using ACS.Core.Domain.Product;

namespace ACS.Services.Product
{
    public partial class SeriesProductEntryService : ISeriesProductEntryService
    {
        #region Fields
        private readonly IRepository<ProductMaster> _ProductMasterRepositry;
        private readonly IRepository<ProductAuthorLink> _ProductAuthorLinkRepositry;
        private readonly IRepository<ProductCategoryMaster> _ProductCategoryMasterRepository;
        private readonly IRepository<AuthorContractOriginal> _AuthorContract;
        private readonly IRepository<ProductPreviousProductLink> _ProductPreviousProductLinkRepository;
        private readonly IDbContext _dbContext;
        #endregion

        #region Ctor
        public SeriesProductEntryService(
            IRepository<ProductMaster> ProductMasterRepositry
            , IRepository<ProductAuthorLink> ProductAuthorLinkRepositry
            , IRepository<ProductCategoryMaster> ProductCategoryMasterRepository
            ,IRepository<AuthorContractOriginal> AuthorContract
            ,IRepository<ProductPreviousProductLink> ProductPreviousProductLinkRepository
            )
        {
            _ProductMasterRepositry = ProductMasterRepositry;
            _ProductAuthorLinkRepositry = ProductAuthorLinkRepositry;
            _ProductCategoryMasterRepository = ProductCategoryMasterRepository;
            _AuthorContract = AuthorContract;
            _ProductPreviousProductLinkRepository = ProductPreviousProductLinkRepository;
        }
        #endregion

        #region Methods
        public int InsertSeriesProduct(ProductMaster Product)
        {
            //Product.ProductCode = GetProductCode(Product.ProductCategoryId);
            
            //return Product.Id;

            _ProductMasterRepositry.Insert(Product);
            return Product.Id;
        }


        public string GetProductCode(int productCategoryId)
        {
            var ProductCodeNumber = _ProductMasterRepositry.Table.OrderByDescending(a => a.ProductCode.Substring(2, 4)).Select(a => a.ProductCode.Substring(2, 4)).FirstOrDefault();
            if (ProductCodeNumber == null)
            {
                ProductCodeNumber = "0000";
            }
            var productCategory = _ProductCategoryMasterRepository.Table.Where(a => a.Id == productCategoryId).Select(a => a.ProductCategory).FirstOrDefault();
            ProductCodeNumber = "P" + productCategory.Substring(0, 1) + (Convert.ToInt16(ProductCodeNumber) + 1).ToString().PadLeft(4, '0');
            return ProductCodeNumber;

        }

        public void InsertProductAuthorLink(ProductAuthorLink ProductAuthorLink)
        {
            ProductAuthorLink.Deactivate = "N";
            ProductAuthorLink.EntryDate = DateTime.Now;
            ProductAuthorLink.ModifiedBy = null;
            ProductAuthorLink.ModifiedDate = null;
            ProductAuthorLink.DeactivateBy = null;
            ProductAuthorLink.DeactivateDate = null;
            _ProductAuthorLinkRepositry.Insert(ProductAuthorLink);
        }

        public string DuplicateISBNNo(string ISBNno)
        {

            var mstr_returnmsg = "notexist";
            var dupes = (from dup in _ProductMasterRepositry.Table
                         where dup.OUPISBN == ISBNno.ToString() && dup.Deactivate == "N" select dup).FirstOrDefault();
            //var dupes = _ProductMasterRepositry.Table.Where(x => x.OUPISBN && x.OUPISBN != null && x.Deactivate == "N"
            //                                && (Product.Id != 0 ? x.Id : 0) != (Product.Id != 0 ? Product.Id : 1)).FirstOrDefault();
            if (dupes != null)
            {
                mstr_returnmsg = "duplicate";

            }

            return mstr_returnmsg;
        }

        public string DuplicateWorkingPro(string WorkingPro)
        {

            var mstr_returnmsg = "notexist";
            var dupes = (from dup in _ProductMasterRepositry.Table
                         where dup.WorkingProduct == WorkingPro.ToString()
                         select dup).FirstOrDefault();

            if (dupes != null)
            {
                mstr_returnmsg = "duplicate";

            }

            return mstr_returnmsg;
        }


        //Add by Ankush 21/10/2016
        public IList<AuthorContractOriginal> CheckAuthorContract(string ISBN)
        {
            var PreviousProductId = _ProductMasterRepositry.Table.Where(x => x.OUPISBN == ISBN && x.OUPISBN != null && x.Deactivate == "N").Select(x=>x.Id).FirstOrDefault();
            if (PreviousProductId != 0)
            {
                var AuthorContractList = _AuthorContract.Table.Where(x => x.ProductId == PreviousProductId && x.ProductId != null && x.Deactivate == "N").ToList();

                return AuthorContractList;
            }
            else
            {
                return null;
            }
        }

        public void InsertProductPreviousProductLink(ProductPreviousProductLink ProductPreviousProductLink)
        {
            ProductPreviousProductLink.Deactivate = "N";
            ProductPreviousProductLink.EntryDate = DateTime.Now;
            ProductPreviousProductLink.ModifiedBy = null;
            ProductPreviousProductLink.ModifiedDate = null;
            ProductPreviousProductLink.DeactivateBy = null;
            ProductPreviousProductLink.DeactivateDate = null;
            _ProductPreviousProductLinkRepository.Insert(ProductPreviousProductLink);
        }

        #endregion
    }
}
