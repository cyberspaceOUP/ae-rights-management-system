using ACS.Core.Data;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Product
{
    public partial class ProductLicenseService : IProductLicenseService 
    {
        #region Fields
        private readonly IRepository<ProductLicense> _ProductLicenseRepository;
        private readonly IRepository<ProductMaster> _ProductMasterRepository;
        private readonly IRepository<ProductCategoryMaster> _ProductCategoryMasterRepository;
        private readonly IRepository<ProductLicenseRoyality> _ProductLicenseRoyalityRepository;
        private readonly IRepository<ProductLicenseSubsidiaryRights> _ProductLicenseSubsidiaryRightsRepository;
        private readonly IRepository<ProductLicenseUpdateDetails> _ProductLicenseUpdateDetailsRepository;
        private readonly IRepository<ProductLicenseFileDetails> _ProductLicenseFileDetailsRepository;
        private readonly IRepository<ProductLicenseAddendumLink> _ProductLicenseAddendumLinkRepository;
        private readonly IRepository<ProductLicenseHistory> _ProductLicenseHistory;
        private readonly IRepository<AddendumDetails> _AddendumDetails;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="GeographyType">Geography</param>        
        public ProductLicenseService(
            IRepository<ProductLicense> ProductLicenseRepository,
            IRepository<ProductMaster> ProductMasterRepository
             ,IRepository<ProductCategoryMaster> ProductCategoryMasterRepository
             ,IRepository<ProductLicenseRoyality> ProductLicenseRoyalityRepository
             , IRepository<ProductLicenseSubsidiaryRights> ProductLicenseSubsidiaryRightsRepository
             , IRepository<ProductLicenseUpdateDetails> ProductLicenseUpdateDetailsRepository
            , IRepository<ProductLicenseFileDetails> ProductLicenseFileDetailsRepository
            , IRepository<ProductLicenseAddendumLink> ProductLicenseAddendumLinkRepository
            , IRepository<ProductLicenseHistory> ProductLicenseHistory
            , IRepository<AddendumDetails> AddendumDetails
        )
        {
            _ProductLicenseRepository = ProductLicenseRepository;
            _ProductMasterRepository = ProductMasterRepository;
            _ProductCategoryMasterRepository = ProductCategoryMasterRepository;
            _ProductLicenseRoyalityRepository = ProductLicenseRoyalityRepository;
            _ProductLicenseSubsidiaryRightsRepository = ProductLicenseSubsidiaryRightsRepository;
            _ProductLicenseUpdateDetailsRepository = ProductLicenseUpdateDetailsRepository;
            _ProductLicenseFileDetailsRepository = ProductLicenseFileDetailsRepository;
            _ProductLicenseAddendumLinkRepository = ProductLicenseAddendumLinkRepository;
            _ProductLicenseHistory = ProductLicenseHistory;
            _AddendumDetails = AddendumDetails;
        }



        #endregion

        #region Methods



        public ProductLicense InsertProductLicenseMaster(ProductLicense ProductLicense)
        {

            ProductLicense.ProductLicensecode = GetProductLicenseCode(ProductLicense.productid);
            _ProductLicenseRepository.Insert(ProductLicense);
            return ProductLicense;
        }

        public void UpdateProductLicenseMaster(ProductLicense ProductLicense)
        {
            _ProductLicenseRepository.Update(ProductLicense);
        }

        public string GetProductLicenseCode(int ProductId)
        {
            var ProductLicenseNumber = _ProductLicenseRepository.Table.OrderByDescending(a => a.ProductLicensecode.Substring(4, 4)).Select(a => a.ProductLicensecode.Substring(4, 4)).FirstOrDefault();
            if (ProductLicenseNumber == null)
            {
                ProductLicenseNumber = "0000";
            }
            var productCategory = _ProductMasterRepository.Table.Where(a => a.Id == ProductId).Select(a => a.ProductProductCategory.ProductCategoryCode).FirstOrDefault();
            ProductLicenseNumber = "PL" + productCategory + (Convert.ToInt16(ProductLicenseNumber) + 1).ToString().PadLeft(4, '0');
            return ProductLicenseNumber;

        }

        public ProductLicense GetProductLicenseById(ProductLicense ProductLicense)
        {

            return _ProductLicenseRepository.Table.Where(x => x.Id == ProductLicense.Id && x.Deactivate == "N").FirstOrDefault();
        }

        public ProductLicense GetProductLicenseById(int ProductLicenseId)
        {
            return _ProductLicenseRepository.Table.Where(x => x.Id == ProductLicenseId && x.Deactivate == "N").FirstOrDefault();
        }

        public AddendumDetails GetAddendumDetailById(int ProductLicenseId)
        {
            return _AddendumDetails.Table.Where(x => x.LicenseId == ProductLicenseId && x.Deactivate == "N").OrderByDescending(a => a.EntryDate).FirstOrDefault();
        }

        public void DeleteProDuctLicenseRoyality(ProductLicenseRoyality ProductLicenseRoyality)
        {
            _ProductLicenseRoyalityRepository.Delete(ProductLicenseRoyality);
        }


        public void DeleteProductLicenseSubsidiaryRights(ProductLicenseSubsidiaryRights ProductLicenseSubsidiaryRights)
        {
            _ProductLicenseSubsidiaryRightsRepository.Delete(ProductLicenseSubsidiaryRights);
        }


        public void UpdateProductLicense(ProductLicense ProductLicense)
        {
            _ProductLicenseRepository.Update(ProductLicense);
        }

        public void DeleteProductLicenseFileDetails(ProductLicenseFileDetails FileDetails)
        {
            _ProductLicenseFileDetailsRepository.Delete(FileDetails);
        }

        public void DeleteProductLicenseUpdateDetails(ProductLicenseUpdateDetails UpdateDetails)
        {
            _ProductLicenseUpdateDetailsRepository.Delete(UpdateDetails);
        }

        public ProductLicenseFileDetails GetFileDetailsById(ProductLicenseFileDetails FileDetails)
        {

            return _ProductLicenseFileDetailsRepository.Table.Where(x => x.Id == FileDetails.Id && x.Deactivate == "N").FirstOrDefault();
        }

        public void InsertProductLicenseUpdateDetails(ProductLicenseUpdateDetails ProductLicenseUpdate)
        {
             _ProductLicenseUpdateDetailsRepository.Insert(ProductLicenseUpdate);
        }

        public string DuplicityLicenseUpdateDetails(ProductLicenseUpdateDetails LicenseUpdateDetails)
        {

            var mstr_returnmsg = "notexist";
            var dupes = _ProductLicenseUpdateDetailsRepository.Table.Where(x => x.LicenseId == LicenseUpdateDetails.LicenseId && x.Deactivate == "N").FirstOrDefault();
            if (dupes != null)
            {
                mstr_returnmsg = "duplicate";

            }

            return mstr_returnmsg;
        }

        public int CheckLicenseCode(ProductLicense ProductLicense)
        {

            var dupes = _ProductLicenseRepository.Table.Where(x => x.ProductLicensecode == ProductLicense.ProductLicensecode && x.Deactivate == "N").Select(a => a.Id).FirstOrDefault();

            return dupes;
        }

        public void InsertMultipleProductLink(ProductLicenseAddendumLink AddendumLink)
        {
            int LicenseId = AddendumLink.LicenseId;
            IList<ProductLicenseAddendumLink> list = GetProductLicenseAddendumLinkList(LicenseId);

            foreach(ProductLicenseAddendumLink Link in list)
            {
                Link.Active = "N";
                Link.ModifiedBy = Link.EnteredBy;
                Link.ModifiedDate = Link.EntryDate;

                _ProductLicenseAddendumLinkRepository.Update(Link);
            }
            _ProductLicenseAddendumLinkRepository.Insert(AddendumLink);
        }

        public IList<ProductLicenseAddendumLink> GetProductLicenseAddendumLinkList(int LicenseId)
        {
            IList<ProductLicenseAddendumLink> list = _ProductLicenseAddendumLinkRepository.Table.Where(a => a.LicenseId == LicenseId && a.Deactivate == "N" && a.Active == "Y").ToList();
            return list;
        }

        public void InsertSearchHistory(ProductLicenseHistory _SaerchHistory)
        {
            _ProductLicenseHistory.Insert(_SaerchHistory);
        }

        public IList<ProductLicenseSubsidiaryRights> GetProductLicenseSubsidiaryRightsList()
        {
            return _ProductLicenseSubsidiaryRightsRepository.Table.ToList();
        }

        #endregion

        //added by prakash
        public IList<ProductLicense> GetAllProductLicenseList()
        {
            return _ProductLicenseRepository.Table.Where(i => i.Deactivate == "N").ToList();
        }

        //Added By Ankush
        public IList<ProductLicenseRoyality> GetProductLicenseRoyalityList()
        {
            return _ProductLicenseRoyalityRepository.Table.Where(a => a.Deactivate == "N").ToList();
        }

    }
}
