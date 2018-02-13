//create by saddam 
//date : 14/06/2016
//purpose : Insert, Update, Delete Records for Other Contract Master

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
using ACS.Core.Domain.OtherContract;

namespace ACS.Services.Other_Contract
{
   public partial   class OtherContractService : IOtherContractService
    {

         #region Fields
        private readonly IDbContext _dbContext;
        private readonly IRepository<OtherContractMaster> _OtherContractMaster;
            private readonly IRepository<GeographicalMaster> _GeographicalMasterRepository;
           private readonly IEncryptionService _encryptionService;
           private readonly IRepository<OtherContractDocuments> _OtherContractDocuments;
           private readonly IRepository<OtherContractImageBank> _OtherContractImageBank;
           private readonly IRepository<OtherContractLink> _OtherContractLink;
           private readonly IRepository<OtherContractLinkDocument> _OtherContractLinkDocument;
           private readonly IRepository<OtherContractSearch> _OtherContractSearch;
           public readonly IRepository<OtherContractDivisionLink> _OtherContractDivisionLink;
           public readonly IRepository<VideoImageBank> _VideoImageBank;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="GeographyType">Geography</param>        
           public OtherContractService(
            IRepository<OtherContractMaster> OtherContractMaster
               , IEncryptionService encryptionService
               , IRepository<GeographicalMaster> GeographicalService
                  ,IDbContext dbContext
               , IRepository<OtherContractDocuments> OtherContractDocuments
                , IRepository<OtherContractImageBank> OtherContractImageBank
               , IRepository<OtherContractLink> OtherContractLink
                 , IRepository<OtherContractLinkDocument> OtherContractLinkDocument
               , IRepository<OtherContractSearch> OtherContractSearch
               , IRepository<OtherContractDivisionLink> OtherContractDivisionLink
               , IRepository<VideoImageBank> VideoImageBank
               )
        {
            _OtherContractMaster = OtherContractMaster;
            _GeographicalMasterRepository = GeographicalService;
            _OtherContractDocuments = OtherContractDocuments;
            _OtherContractImageBank = OtherContractImageBank;
            _OtherContractLink = OtherContractLink;
            _OtherContractLinkDocument = OtherContractLinkDocument;
           this._encryptionService = encryptionService;
           this._dbContext = dbContext;
           _OtherContractSearch = OtherContractSearch;
           this._OtherContractDivisionLink = OtherContractDivisionLink;
           this._VideoImageBank = VideoImageBank;
        }

        

        #endregion

           public int InsertOtherContract(OtherContractMaster OtherContract)
       {


           OtherContract.Deactivate = "N";
           //  Author.EnteredBy = 10;
           OtherContract.EntryDate = DateTime.Now;
           OtherContract.ModifiedBy = null;
           OtherContract.ModifiedDate = null;
           OtherContract.DeactivateBy = null;
           OtherContract.DeactivateDate = null;

           _OtherContractMaster.Insert(OtherContract);
           return OtherContract.Id;
       }

           public void InsertOtherContractDocumentsLinking(OtherContractDocuments ContractDocuments)
           {
               ContractDocuments.Deactivate = "N";
               ContractDocuments.EntryDate = DateTime.Now;
               ContractDocuments.ModifiedBy = null;
               ContractDocuments.ModifiedDate = null;
               ContractDocuments.DeactivateBy = null;
               ContractDocuments.DeactivateDate = null;
               _OtherContractDocuments.Insert(ContractDocuments);
           }

           public void InsertOtherContractImageBank(OtherContractImageBank OtherContractImageBank)
           {
               OtherContractImageBank.Deactivate = "N";
               OtherContractImageBank.EntryDate = DateTime.Now;
               OtherContractImageBank.ModifiedBy = null;
               OtherContractImageBank.ModifiedDate = null;
               OtherContractImageBank.DeactivateBy = null;
               OtherContractImageBank.DeactivateDate = null;
               _OtherContractImageBank.Insert(OtherContractImageBank);
           }

           public OtherContractMaster GetOtherContractMasterById(OtherContractMaster ContractMaster)
           {
               return _OtherContractMaster.Table.Where(i => i.Id == ContractMaster.Id && i.Deactivate == "N").FirstOrDefault();
           }

           public int InsertOtherContractLink(OtherContractLink OtherContractLink)
           {


               OtherContractLink.Deactivate = "N";
              
               OtherContractLink.EntryDate = DateTime.Now;
               OtherContractLink.ModifiedBy = null;
               OtherContractLink.ModifiedDate = null;
               OtherContractLink.DeactivateBy = null;
               OtherContractLink.DeactivateDate = null;

               _OtherContractLink.Insert(OtherContractLink);
               return OtherContractLink.Id;
           }

           public void InsertOtherContractDocumentsLinkingLink(OtherContractLinkDocument OtherContractLinkDocument)
           {
               OtherContractLinkDocument.Deactivate = "N";
               OtherContractLinkDocument.EntryDate = DateTime.Now;
               OtherContractLinkDocument.ModifiedBy = null;
               OtherContractLinkDocument.ModifiedDate = null;
               OtherContractLinkDocument.DeactivateBy = null;
               OtherContractLinkDocument.DeactivateDate = null;
               _OtherContractLinkDocument.Insert(OtherContractLinkDocument);
           }

           public OtherContractMaster GetOtherContractMasterId(int Id)
           {
               return _OtherContractMaster.Table.Where(i => i.Id == Id && i.Deactivate == "N").FirstOrDefault();
           }

           public void UpdateOtherContractMaster(OtherContractMaster OtherContract)
           {
               OtherContract.Deactivate = "N";
              
               OtherContract.ModifiedDate = DateTime.Now;
               OtherContract.DeactivateBy = null;
               OtherContract.DeactivateDate = null;
               _OtherContractMaster.Update(OtherContract);
           }
           public void UpdateOtherContractImageBank(OtherContractImageBank OtherContractImageBank)
           {
               OtherContractImageBank.Deactivate = "N";

               OtherContractImageBank.ModifiedDate = DateTime.Now;
               OtherContractImageBank.DeactivateBy = null;
               OtherContractImageBank.DeactivateDate = null;
               _OtherContractImageBank.Update(OtherContractImageBank);
           }

           public OtherContractDocuments getOtherContractDocumentsDetail(int DocumentId)
           {
               return _OtherContractDocuments.Table.Where(i => i.Id == DocumentId).FirstOrDefault();
           }
           public void DeavtivateOtherContractDocumentsById(int id, int enteredBy)
           {
               IList<OtherContractDocuments> Linking = _OtherContractDocuments.Table.Where(i => i.Id == id && i.Deactivate == "N").ToList();
               foreach (var lst in Linking)
               {
                   lst.Deactivate = "Y";
                   lst.DeactivateBy = enteredBy;
                   lst.DeactivateDate = DateTime.Now;
                   _OtherContractDocuments.Update(lst);
               }

           }


           public void DeavtivateOtherContractDocumentsLinkById(int id, int enteredBy)
           {
               IList<OtherContractLinkDocument> Linking = _OtherContractLinkDocument.Table.Where(i => i.Id == id && i.Deactivate == "N").ToList();
               foreach (var lst in Linking)
               {
                   lst.Deactivate = "Y";
                   lst.DeactivateBy = enteredBy;
                   lst.DeactivateDate = DateTime.Now;
                   _OtherContractLinkDocument.Update(lst);
               }

           }

           public void UpdateOtherContractLink(OtherContractLink OtherContractLink)
           {
               OtherContractLink.Deactivate = "N";

               OtherContractLink.ModifiedDate = DateTime.Now;
               OtherContractLink.DeactivateBy = null;
               OtherContractLink.DeactivateDate = null;
               _OtherContractLink.Update(OtherContractLink);
           }

           public void InsertVideoImageBankLink(VideoImageBank VideoImageBank)
           {
           
            
               VideoImageBank.DeactivateBy = null;
               VideoImageBank.DeactivateDate = null;
               _VideoImageBank.Insert(VideoImageBank);
           }
       

           public void InsertSearchHistory(OtherContractSearch _Search)
           {
               _OtherContractSearch.Insert(_Search);
           }

           public void InsertOtherContractDivisionLink(OtherContractDivisionLink DivisionLink)
           {
               DivisionLink.Deactivate = "N";
               DivisionLink.EntryDate = DateTime.Now;
               DivisionLink.ModifiedBy = null;
               DivisionLink.ModifiedDate = null;
               DivisionLink.DeactivateBy = null;
               DivisionLink.DeactivateDate = null;
               _OtherContractDivisionLink.Insert(DivisionLink);
           }

           public void UpdateVideoImageBankLink(VideoImageBank VideoImageBank)
           {
               VideoImageBank.Deactivate = "N";

               VideoImageBank.ModifiedDate = DateTime.Now;
               VideoImageBank.DeactivateBy = null;
               VideoImageBank.DeactivateDate = null;
           
               _VideoImageBank.Update(VideoImageBank);
           }
       

           public IList<OtherContractDivisionLink> getDivisionLinking(int id)
           {
               return _OtherContractDivisionLink.Table.Where(i => i.othercontractid == id && i.Deactivate == "N").ToList();
           }
           public void DeleteOtherContractDivisionLink(int id, int enteredBy)
           {
               IList<OtherContractDivisionLink> Linking = getDivisionLinking(id);
               foreach (var lst in Linking)
               {
                   lst.Deactivate = "Y";
                   lst.EnteredBy = enteredBy;
                   lst.DeactivateDate = DateTime.Now;
                   _OtherContractDivisionLink.Update(lst);
               }
           }

           public IList<VideoImageBank> getVideoImageBank(int id)
           {
               return _VideoImageBank.Table.Where(i => i.ImageBankId == id && i.Deactivate == "N").ToList();
           }
           public void DeleteVideoImageBankLink(int id, int enteredBy)
           {
               IList<VideoImageBank> Linking = getVideoImageBank(id);
               foreach (var lst in Linking)
               {
                   lst.Deactivate = "Y";
                   lst.EnteredBy = enteredBy;
                   lst.DeactivateDate = DateTime.Now;
                   _VideoImageBank.Update(lst);
               }
           }
       
           public void DeleteOtherContractMaster(OtherContractMaster OtherContract)
           {
               _OtherContractMaster.Update(OtherContract);
           }

    }
}
