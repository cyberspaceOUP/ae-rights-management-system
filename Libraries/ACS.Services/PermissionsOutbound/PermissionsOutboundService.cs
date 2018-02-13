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
using ACS.Core.Domain.PermissionsOutbound;

namespace ACS.Services.PermissionsOutbound
{
    

    public partial class PermissionsOutboundService : IPermissionsOutboundService 
    {

        #region Fields
        private readonly IDbContext _dbContext;
        private readonly IRepository<LicenseeMaster> _LicenseeMaster;
        private readonly IRepository<PermissionsOutboundMaster> _PermissionsOutboundMaster;
        private readonly IRepository<PermissionsOutboundTypeOfRightsMaster> _PermissionsOutboundTypeOfRightsMaster;
        private readonly IRepository<PermissionsOutboundUpdate> _PermissionsOutboundUpdate;
        private readonly IRepository<PermissionsOutboundDocument> _PermissionsOutboundDocument;

        private readonly IRepository<PermissionsOutboundSearchHistory> _PermissionsOutboundSearchHistory;
        private readonly IRepository<PermissionsOutboundPaymentTagging> _PermissionsOutboundPaymentTagging;

        private readonly IRepository<PermissionsOutboundLanguageMaster> _PermissionsOutboundLanguageMaster;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="GeographyType">Geography</param>        
        public PermissionsOutboundService(

              IDbContext dbContext
           , IRepository<LicenseeMaster> LicenseeMaster
            ,IRepository<PermissionsOutboundMaster> PermissionsOutboundMaster
            ,IRepository<PermissionsOutboundTypeOfRightsMaster> PermissionsOutboundTypeOfRightsMaster
            , IRepository<PermissionsOutboundUpdate> PermissionsOutboundUpdate
           ,IRepository<PermissionsOutboundDocument> PermissionsOutboundDocument
            ,IRepository<PermissionsOutboundSearchHistory> PermissionsOutboundSearchHistory
            ,IRepository<PermissionsOutboundPaymentTagging> PermissionsOutboundPaymentTagging
            , IRepository<PermissionsOutboundLanguageMaster> PermissionsOutboundLanguageMaster
            )
        {

            this._dbContext = dbContext;
            this._LicenseeMaster = LicenseeMaster;
            this._PermissionsOutboundMaster = PermissionsOutboundMaster;
            this._PermissionsOutboundTypeOfRightsMaster = PermissionsOutboundTypeOfRightsMaster;
            this._PermissionsOutboundUpdate = PermissionsOutboundUpdate;
            this._PermissionsOutboundDocument = PermissionsOutboundDocument;
            this._PermissionsOutboundSearchHistory = PermissionsOutboundSearchHistory;
            _PermissionsOutboundPaymentTagging = PermissionsOutboundPaymentTagging;
            this._PermissionsOutboundLanguageMaster = PermissionsOutboundLanguageMaster;
        }



        #endregion





        public virtual IList<LicenseeMaster> GetLicenseeMasterList()
        {
            return _LicenseeMaster.Table.Where(a => a.Deactivate == "N").OrderBy(c => c.ContactPerson).ToList();

        }

        public LicenseeMaster GetLicenseeById(LicenseeMaster LicenseeMaster)
        {
            return _LicenseeMaster.Table.Where(i => i.Id == LicenseeMaster.Id).FirstOrDefault();
        }

        public int InsertPermissionsOutbound(PermissionsOutboundMaster PermissionsOutbound)
        {


            PermissionsOutbound.Deactivate = "N";
            //  Author.EnteredBy = 10;
            PermissionsOutbound.EntryDate = DateTime.Now;
            PermissionsOutbound.ModifiedBy = null;
            PermissionsOutbound.ModifiedDate = null;
            PermissionsOutbound.DeactivateBy = null;
            PermissionsOutbound.DeactivateDate = null;

            _PermissionsOutboundMaster.Insert(PermissionsOutbound);
            return PermissionsOutbound.Id;
        }



        public int InsertPermissionsOutboundUpdate(PermissionsOutboundUpdate PermissionsOutboundUpdate)
        {


            PermissionsOutboundUpdate.Deactivate = "N";
            //  Author.EnteredBy = 10;
            PermissionsOutboundUpdate.EntryDate = DateTime.Now;
            PermissionsOutboundUpdate.ModifiedBy = null;
            PermissionsOutboundUpdate.ModifiedDate = null;
            PermissionsOutboundUpdate.DeactivateBy = null;
            PermissionsOutboundUpdate.DeactivateDate = null;

            _PermissionsOutboundUpdate.Insert(PermissionsOutboundUpdate);
            return PermissionsOutboundUpdate.Id;
        }


        public void InsertPermissionsOutboundDocument(PermissionsOutboundDocument PermissionsOutboundDocument)
        {
            PermissionsOutboundDocument.Deactivate = "N";
            PermissionsOutboundDocument.EntryDate = DateTime.Now;
            PermissionsOutboundDocument.ModifiedBy = null;
            PermissionsOutboundDocument.ModifiedDate = null;
            PermissionsOutboundDocument.DeactivateBy = null;
            PermissionsOutboundDocument.DeactivateDate = null;
            _PermissionsOutboundDocument.Insert(PermissionsOutboundDocument);
        }


        public void InsertSearchHistory(PermissionsOutboundSearchHistory _PermissionsOutbound)
        {
            _PermissionsOutboundSearchHistory.Insert(_PermissionsOutbound);
        }


        public void DeavtivatePermissionsOutboundDocumentById(int id, int enteredBy)
        {
            IList<PermissionsOutboundDocument> Linking = _PermissionsOutboundDocument.Table.Where(i => i.Id == id && i.Deactivate == "N").ToList();
            foreach (var lst in Linking)
            {
                lst.Deactivate = "Y";
                lst.DeactivateBy = enteredBy;
                lst.DeactivateDate = DateTime.Now;
                _PermissionsOutboundDocument.Update(lst);
            }

        }


        public void InsertPermissionsOutboundTypeOfRightsLinking(PermissionsOutboundTypeOfRightsMaster PermissionsOutboundTypeOfRights)
        {
            PermissionsOutboundTypeOfRights.Deactivate = "N";
            PermissionsOutboundTypeOfRights.EntryDate = DateTime.Now;
            PermissionsOutboundTypeOfRights.ModifiedBy = null;
            PermissionsOutboundTypeOfRights.ModifiedDate = null;
            PermissionsOutboundTypeOfRights.DeactivateBy = null;
            PermissionsOutboundTypeOfRights.DeactivateDate = null;
            _PermissionsOutboundTypeOfRightsMaster.Insert(PermissionsOutboundTypeOfRights);
        }

        public PermissionsOutboundMaster GetPermissionsOutboundById(PermissionsOutboundMaster PermissionsOutbound)
        {
            return _PermissionsOutboundMaster.Table.Where(a => a.Deactivate == "N" && a.Id == PermissionsOutbound.Id).FirstOrDefault();
        }

        public PermissionsOutboundUpdate GetPermissionsOutboundUpdateById(PermissionsOutboundUpdate PermissionsOutboundUpdate)
        {
            return _PermissionsOutboundUpdate.Table.Where(a => a.Deactivate == "N" && a.PermissionsOutboundID == PermissionsOutboundUpdate.PermissionsOutboundID).FirstOrDefault();
        }


        public void UpdatePermissionsOutbound(PermissionsOutboundMaster PermissionsOutbound)
        {
            PermissionsOutbound.ModifiedBy = PermissionsOutbound.EnteredBy;
            PermissionsOutbound.ModifiedDate = DateTime.Now;
            PermissionsOutbound.DeactivateBy = null;
            PermissionsOutbound.DeactivateDate = null;

            _PermissionsOutboundMaster.Update(PermissionsOutbound);
        }



        public void UpdatePermissionsOutboundUpdate(PermissionsOutboundUpdate PermissionsOutboundUpdate)
        {
          
            PermissionsOutboundUpdate.ModifiedDate = DateTime.Now;
            PermissionsOutboundUpdate.DeactivateBy = null;
            PermissionsOutboundUpdate.DeactivateDate = null;

            _PermissionsOutboundUpdate.Update(PermissionsOutboundUpdate);
        }


        public IList<PermissionsOutboundTypeOfRightsMaster> getPermissionsOutboundTypeOfRights(int id)
        {
            return _PermissionsOutboundTypeOfRightsMaster.Table.Where(i => i.PermissionsOutboundId == id && i.Deactivate == "N").ToList();
        }

        public void DeavtivatePermissionsOutboundTypeOfRights(int id, int enteredBy)
        {
            IList<PermissionsOutboundTypeOfRightsMaster> Linking = getPermissionsOutboundTypeOfRights(id);
            foreach (var lst in Linking)
            {
                lst.Deactivate = "Y";
                lst.DeactivateBy = enteredBy;
                lst.DeactivateDate = DateTime.Now;
                _PermissionsOutboundTypeOfRightsMaster.Update(lst);
            }

        }

        public void InsertPermissionsOutboundPaymentTagging(PermissionsOutboundPaymentTagging PermissionsOutboundPaymentTagging)
        {
            _PermissionsOutboundPaymentTagging.Insert(PermissionsOutboundPaymentTagging);
        }


        public void InsertPermissionsOutboundLanguageLink(PermissionsOutboundLanguageMaster PermissionsOutboundLanguageLink)
        {
            PermissionsOutboundLanguageLink.Deactivate = "N";
            PermissionsOutboundLanguageLink.EntryDate = DateTime.Now;
            PermissionsOutboundLanguageLink.ModifiedBy = null;
            PermissionsOutboundLanguageLink.ModifiedDate = null;
            PermissionsOutboundLanguageLink.DeactivateBy = null;
            PermissionsOutboundLanguageLink.DeactivateDate = null;
            _PermissionsOutboundLanguageMaster.Insert(PermissionsOutboundLanguageLink);
        }


        public IList<PermissionsOutboundLanguageMaster> getPermissionsOutboundLanguage(int id)
        {
            return _PermissionsOutboundLanguageMaster.Table.Where(i => i.PermissionsOutboundId == id && i.Deactivate == "N").ToList();
        }

        public void DeletePermissionsOutboundLanguageLink(int id, int enteredBy)
        {
            IList<PermissionsOutboundLanguageMaster> Linking = getPermissionsOutboundLanguage(id);
            foreach (var lst in Linking)
            {
                lst.Deactivate = "Y";
                lst.EnteredBy = enteredBy;
                lst.DeactivateDate = DateTime.Now;
                _PermissionsOutboundLanguageMaster.Update(lst);
            }
        }

        //Added by Prakash
        public IList<PermissionsOutboundMaster> getAllPermissionsOutboundMasterList()
        {
            return _PermissionsOutboundMaster.Table.Where(i => i.Deactivate == "N").ToList();
        }


        public void DeletePermissionsOutbound(PermissionsOutboundMaster PermissionsOutbound)
        {
            _PermissionsOutboundMaster.Update(PermissionsOutbound);
        }

        public PermissionsOutboundPaymentTagging getPermissionsOutboundPaymentTaggingById(int Id)
        {
            return _PermissionsOutboundPaymentTagging.Table.Where(p => p.Deactivate == "N" && p.Id == Id).FirstOrDefault();
        }

        public void DeletePermissionsOutboundPaymentTagging(PermissionsOutboundPaymentTagging PermissionsOutboundPaymentTagging)
        {
            _PermissionsOutboundPaymentTagging.Update(PermissionsOutboundPaymentTagging);
        }

    }

}
