//create by saddam 
//date : 27/07/2016

using ACS.Core.Data;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Data;
using System.Data.SqlClient;
using ACS.Services.Security;
using System.Data;
using ACS.Core.Domain.RightsSelling;

namespace ACS.Services.RightsSelling
{
  

    public partial class RightsSelling : IRightsSelling 
    {

        #region Fields
        private readonly IDbContext _dbContext;
        private readonly IRepository<LicenseeMaster> _LicenseeMaster;
        private readonly IRepository<RightsSellingMaster> _RightsSellingMaster;
        private readonly IRepository<RightsSellingRoyalty> _RightsSellingRoyalty;
        private readonly IRepository<RightsSellingDocument> _RightsSellingDocument;
        private readonly IRepository<RightsSellingUpdate> _RightsSellingUpdate;
        private readonly IRepository<RightsSellingHistory> _RightsSellingHistory;
        private readonly IRepository<RightsSellingPaymentTagging> _RightsSellingPaymentTagging;
        private readonly IRepository<ProductCategoryRightMaster> _ProductCategoryRightMaster;
        private readonly IRepository<RightsSellingLanguageMaster> _RightsSellingLanguageMaster;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="GeographyType">Geography</param>        
        public RightsSelling(
        
              IDbContext dbContext
           ,IRepository<LicenseeMaster> LicenseeMaster
            ,IRepository<RightsSellingMaster> RightsSellingMaster
            , IRepository<RightsSellingRoyalty> RightsSellingRoyalty
            , IRepository<RightsSellingDocument> RightsSellingDocument
            , IRepository<RightsSellingUpdate> RightsSellingUpdate
            , IRepository<RightsSellingHistory> RightsSellingHistory
             , IRepository<RightsSellingPaymentTagging> RightsSellingPaymentTagging
            , IRepository<ProductCategoryRightMaster> ProductCategoryRightMaster
            , IRepository<RightsSellingLanguageMaster> RightsSellingLanguageMaster
            )
        {
           
            this._dbContext = dbContext;
            this._LicenseeMaster = LicenseeMaster;
            _RightsSellingMaster = RightsSellingMaster;
            _RightsSellingRoyalty = RightsSellingRoyalty;
            _RightsSellingDocument = RightsSellingDocument;
            _RightsSellingUpdate = RightsSellingUpdate;
            _RightsSellingHistory = RightsSellingHistory;
            _RightsSellingPaymentTagging = RightsSellingPaymentTagging;
            this._ProductCategoryRightMaster = ProductCategoryRightMaster;
            this._RightsSellingLanguageMaster = RightsSellingLanguageMaster;

        }
        #endregion

        public virtual IList<LicenseeMaster> GetLicenseeMasterList()
        {
            return _LicenseeMaster.Table.Where(a => a.Deactivate == "N").OrderBy(c => c.OrganizationName).ToList();

        }

        public LicenseeMaster GetLicenseeById(LicenseeMaster LicenseeMaster)
        {
            return _LicenseeMaster.Table.Where(i => i.Id == LicenseeMaster.Id).FirstOrDefault();
        }

        public RightsSellingMaster GetRightsSellingById(RightsSellingMaster RightsSelling)
        {
            return _RightsSellingMaster.Table.Where(i => i.Id == RightsSelling.Id).FirstOrDefault();
        }
        


        public int InsertRightsSellingMaster(RightsSellingMaster RightsSellingMaster)
        {
            _RightsSellingMaster.Insert(RightsSellingMaster);
            return RightsSellingMaster.Id;
        }

        public void InsertRightsSellingRoyalty(RightsSellingRoyalty RightsSellingRoyalty)
        {
            _RightsSellingRoyalty.Insert(RightsSellingRoyalty);
        }

        public void UpdateRightsSellingMaster(RightsSellingMaster RightsSellingMaster)
        {
            _RightsSellingMaster.Update(RightsSellingMaster);
        }

        public int InsertRightsSellingUpdate(RightsSellingUpdate RightsSellingUpdate)
        {
            _RightsSellingUpdate.Insert(RightsSellingUpdate);
            return RightsSellingUpdate.Id;
        }

        public int UpdateRightsSellingUpdate(RightsSellingUpdate RightsSellingUpdate)
        {
            _RightsSellingUpdate.Update(RightsSellingUpdate);
            return RightsSellingUpdate.Id;
        }

        public IList<RightsSellingDocument> GetRightsSellingDocumentList(int id)
        {
            try
            {
                var UpdateId = _RightsSellingUpdate.Table.Where(a => a.RightsSellingID == id && a.Deactivate == "N").FirstOrDefault().Id;
                var Document = _RightsSellingDocument.Table.Where(a => a.RightsSellingUpdateId == UpdateId && a.Deactivate == "N").ToList();
                return Document;
            }
            catch
            {
                return null;
            }
        }

        public IList<RightsSellingRoyalty> GetRightsSellingRoyaltyList(RightsSellingRoyalty Royalty)
        {
            try
            {
                if(Royalty.RightsSellingID == null)
                {
                Royalty.RightsSellingID =  0;
                }
                 

                if (Royalty.ProductLicenseId != null)
                {
                    var _Royalty = _RightsSellingRoyalty.Table.Where(a => a.ProductLicenseId == Royalty.ProductLicenseId && a.Deactivate == "N" && a.RightsSellingID == Royalty.RightsSellingID).ToList();
                    return _Royalty;
                }
                else
                {
                    var _Royalty = _RightsSellingRoyalty.Table.Where(a => a.ContractId == Royalty.ContractId && a.Deactivate == "N" && a.RightsSellingID == Royalty.RightsSellingID).ToList();
                    return _Royalty;
                }
            }
            catch
            {
                return null;
            }
        }

        public IList<RightsSellingRoyalty> GetRightsSellingRoyaltyList()
        {
            try
            {


                var _Royalty = _RightsSellingRoyalty.Table.Where(a => a.Deactivate == "N").ToList();
                return _Royalty;

            }
            catch
            {
                return null;
            }
        }

        public void InsertRightsSellingDocument(RightsSellingDocument RightsSellingDocument)
        {
            _RightsSellingDocument.Insert(RightsSellingDocument);
        }

        public RightsSellingUpdate GetRightsSellingUpdateById(int id)
        {
            return _RightsSellingUpdate.Table.Where(a => a.RightsSellingID == id).FirstOrDefault();
        }

        public void DeavtivateRightsSellingUpdateById(Int64 id)
        {
            RightsSellingDocument document = _RightsSellingDocument.Table.Where(i => i.Id == id && i.Deactivate == "N").FirstOrDefault();
            document.Deactivate = "Y";
            document.DeactivateBy = document.EnteredBy;
            document.DeactivateDate = DateTime.Now;
            _RightsSellingDocument.Update(document);

        }


        public void InsertRightsSellingHistory(RightsSellingHistory RightsSellingHistory)
        {
            _RightsSellingHistory.Insert(RightsSellingHistory);
        }

        public void InsertRightsSellingPaymentTagging(RightsSellingPaymentTagging RightsSellingPaymentTagging)
        {
            _RightsSellingPaymentTagging.Insert(RightsSellingPaymentTagging);
        }


        public void DeleteRoyaltySlabLink(int? id, int enteredBy, string Type)
        {
            if (Type == "a")
            {
                IList<RightsSellingRoyalty> Linking = _RightsSellingRoyalty.Table.Where(a => a.ContractId == id && a.Deactivate == "N").ToList();
                foreach (var lst in Linking)
                {
                    lst.Deactivate = "Y";
                    lst.EnteredBy = enteredBy;
                    lst.DeactivateDate = DateTime.Now;
                    _RightsSellingRoyalty.Update(lst);
                }
            }
            else if(Type =="p")
            {
                IList<RightsSellingRoyalty> Linking = _RightsSellingRoyalty.Table.Where(a => a.ProductLicenseId == id && a.Deactivate == "N").ToList();
                foreach (var lst in Linking)
                {
                    lst.Deactivate = "Y";
                    lst.EnteredBy = enteredBy;
                    lst.DeactivateDate = DateTime.Now;
                    _RightsSellingRoyalty.Update(lst);
                }
            }
            
            
        }

       
        public virtual IList<ProductCategoryRightMaster> GetRightProductCategory()
        {
          
            return _ProductCategoryRightMaster.Table.OrderBy(c => c.ProductCategory).ToList();


        }



        public void InsertRightsSellingLanguageLink(RightsSellingLanguageMaster RightsSellingLanguageLink)
        {
            RightsSellingLanguageLink.Deactivate = "N";
            RightsSellingLanguageLink.EntryDate = DateTime.Now;
            RightsSellingLanguageLink.ModifiedBy = null;
            RightsSellingLanguageLink.ModifiedDate = null;
            RightsSellingLanguageLink.DeactivateBy = null;
            RightsSellingLanguageLink.DeactivateDate = null;
            _RightsSellingLanguageMaster.Insert(RightsSellingLanguageLink);
        }

        public IList<RightsSellingLanguageMaster> getRightsSellingLanguage(int id)
        {
            return _RightsSellingLanguageMaster.Table.Where(i => i.RightsSellingId == id && i.Deactivate == "N").ToList();
        }

        public void DeleteRightsSellingLanguageLink(int id, int enteredBy)
        {
            IList<RightsSellingLanguageMaster> Linking = getRightsSellingLanguage(id);
            foreach (var lst in Linking)
            {
                lst.Deactivate = "Y";
                lst.EnteredBy = enteredBy;
                lst.DeactivateDate = DateTime.Now;
                _RightsSellingLanguageMaster.Update(lst);
            }
        }

        //Added by Prakash
        public IList<RightsSellingMaster> GetAllRightsSellingMasterList()
        {
            return _RightsSellingMaster.Table.Where(c => c.Deactivate == "N").ToList();
        }

        public void DeleteRightsSellingMaster(RightsSellingMaster RightsSellingMaster)
        {
            _RightsSellingMaster.Update(RightsSellingMaster);
        }

        public RightsSellingPaymentTagging getRightsSellingPaymentTaggingById(int Id)
        {
            return _RightsSellingPaymentTagging.Table.Where(p => p.Deactivate == "N" && p.Id == Id).FirstOrDefault();
        }

        public void DeleteRightsSellingPaymentTagging(RightsSellingPaymentTagging RightsSellingPaymentTagging)
        {
            _RightsSellingPaymentTagging.Update(RightsSellingPaymentTagging);
        }


    }

}
