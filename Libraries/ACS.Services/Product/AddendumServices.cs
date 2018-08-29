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
    public partial class AddendumServices : IAddendumServices
    {
        
        #region Fields
        private readonly IRepository<AddendumDetails> _AddendumDetailsRepository;
        private readonly IRepository<ProductLicense> _ProductLicenseRepository;
        private readonly IRepository<AddendumRoyaltySlab> _AddendumRoyaltySlabRepository;
        private readonly IRepository<AddendumFileDetails> _AddendumFileDetailsRepository;
        private readonly IRepository<ProductLicenseAddendumLink> _ProductLicenseAddendumLinkRepository;
        private readonly IRepository<ISBNBag> _ISBNBagRepository;
        private readonly IRepository<ImpressionDetails> _ImpressionDetailsRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="GeographyType">Geography</param>        
        public AddendumServices(
            IRepository<AddendumDetails> AddendumDetailsRepository
            , IRepository<ProductLicense> ProductLicenseRepository
            , IRepository<AddendumRoyaltySlab> AddendumRoyaltySlabRepository
            , IRepository<AddendumFileDetails> AddendumFileDetailsRepository
            , IRepository<ProductLicenseAddendumLink> ProductLicenseAddendumLinkRepository
            , IRepository<ISBNBag> ISBNBagRepository
           , IRepository<ImpressionDetails> ImpressionDetailsRepository
        )
        {
            _AddendumDetailsRepository = AddendumDetailsRepository;
            _ProductLicenseRepository = ProductLicenseRepository;
            _AddendumRoyaltySlabRepository = AddendumRoyaltySlabRepository;
            _AddendumFileDetailsRepository = AddendumFileDetailsRepository;
            _ProductLicenseAddendumLinkRepository = ProductLicenseAddendumLinkRepository;
            _ISBNBagRepository = ISBNBagRepository;
            _ImpressionDetailsRepository = ImpressionDetailsRepository;
        }



        #endregion

        #region Methods

        public string GetAddendumCode(int LicenseId)
        {
            var ProductLicenseCode = _ProductLicenseRepository.Table.Where(a => a.Id == LicenseId && a.Deactivate == "N").Select(a => a.ProductLicensecode).FirstOrDefault();
            var AddendumNumber = _AddendumDetailsRepository.Table.OrderByDescending(a => a.AddendumCode.Substring(10, 4)).Select(a => a.AddendumCode.Substring(10, 4)).FirstOrDefault();
            if (AddendumNumber == null)
            {
                AddendumNumber = "0000";
            }
            AddendumNumber = ProductLicenseCode + "AD" + (Convert.ToInt16(AddendumNumber) + 1).ToString().PadLeft(4, '0');
            return AddendumNumber;

        }

        public int InsertAddendumDetails(AddendumDetails AddendumDetails)
        {

            AddendumDetails.AddendumCode = GetAddendumCode(AddendumDetails.LicenseId);
            _AddendumDetailsRepository.Insert(AddendumDetails);
            return AddendumDetails.Id;
        }


        public AddendumDetails GetAddendumDetailsById(AddendumDetails Addendum)
        {

            return _AddendumDetailsRepository.Table.Where(x => x.Id == Addendum.Id && x.Deactivate == "N").FirstOrDefault();
        }

        public IList<AddendumDetails> GetAddendumDetailsByLicenseId(int LicenseId)
        {

            return _AddendumDetailsRepository.Table.Where(x => x.LicenseId == LicenseId && x.Deactivate == "N").ToList();
        }

        public void DeleteAddendumRoyaltySlab(AddendumRoyaltySlab AddendumRoyaltySlab)
        {
            _AddendumRoyaltySlabRepository.Delete(AddendumRoyaltySlab);
        }


        public void DeleteAddendumFileDetails(AddendumFileDetails AddendumFileDetails)
        {
            _AddendumFileDetailsRepository.Delete(AddendumFileDetails);
        }

        public void UpdateAddendumDetails(AddendumDetails Addendum)
        {
            _AddendumDetailsRepository.Update(Addendum);
        }

        public AddendumFileDetails GetFileDetailsById(AddendumFileDetails FileDetails)
        {

            return _AddendumFileDetailsRepository.Table.Where(x => x.Id == FileDetails.Id && x.Deactivate == "N").FirstOrDefault();
        }

        public ISBNBag GetISBNBagById(ISBNBag ISBNBag)
        {
            return _ISBNBagRepository.Table.Where(x => x.Id == ISBNBag.Id && x.Deactivate == "N" && x.Used == "N").FirstOrDefault();
        }

        public ISBNBag GetISBNBagByISBN(string ISBN)
        {
            return _ISBNBagRepository.Table.Where(x => x.ISBN.Trim() == ISBN && x.Deactivate == "N" && x.Used == "N").FirstOrDefault();
        }

        public void UpdateISBNBag(ISBNBag ISBNBag)
        {
            _ISBNBagRepository.Update(ISBNBag);
        }

        public IList<ImpressionDetails> GetImpressionDetails(ImpressionDetails ImpressionDetails)
        {
            if (ImpressionDetails.ContractId == null || ImpressionDetails.ContractId == 0)
            {
                return _ImpressionDetailsRepository.Table.Where(x => x.ProductId == ImpressionDetails.ProductId && (ImpressionDetails.LicenseId > 0 ? x.LicenseId : 0) == (ImpressionDetails.LicenseId > 0 ? ImpressionDetails.LicenseId : 0) && x.Deactivate == "N").ToList();
            }
            else
            {
                return _ImpressionDetailsRepository.Table.Where(x => x.ProductId == ImpressionDetails.ProductId && (ImpressionDetails.ContractId > 0 ? x.ContractId : 0) == (ImpressionDetails.ContractId > 0 ? ImpressionDetails.ContractId : 0) && x.Deactivate == "N").ToList();
            }
        }

        public void ImsertImpressionDetails(ImpressionDetails ImpressionDetails)
        {

            _ImpressionDetailsRepository.Insert(ImpressionDetails);
        }


        public void InsertAddendumFileDetails(AddendumFileDetails AddendumFileDetails)
        {

            //AddendumDetails.AddendumCode = GetAddendumCode(AddendumDetails.LicenseId);
            _AddendumFileDetailsRepository.Insert(AddendumFileDetails);
            //return AddendumDetails.Id;
        }

        #endregion


    }
}

