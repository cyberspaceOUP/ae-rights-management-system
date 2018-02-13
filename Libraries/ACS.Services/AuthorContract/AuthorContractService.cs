using ACS.Core.Data;
using ACS.Core.Domain.AuthorContract;
using ACS.Core.Domain.Product;
using ACS.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.AuthorContract
{
    public partial class AuthorContractService : IAuthorContractService
    {

        #region Fields
        private readonly IRepository<AuthorContractOriginal> _AuthorContractOriginal;
        private readonly IRepository<AuthorContractAgreement> _AuthorContractAgreement;
        private readonly IRepository<AuthorContractDocument> _AuthorContractDocument;
        private readonly IRepository<AuthorContractContributor> _AuthorContractContributor;
        private readonly IRepository<AuthorContractSubsidiaryRights> _AuthorContractSubsidiaryRights;
        private readonly IRepository<AuthorContractmaterialdetails> _AuthorContractmaterialdetails;
        private readonly IRepository<AddendumFileUpload> _AddendumFileUpload;
        private readonly IRepository<AuthorContractAddendumDetails> _AuthorContractAddendumDetails;
        private readonly IRepository<AuthorContractAddendumRoyality> _AuthorContractAddendumRoyality;
        private readonly IRepository<AuthorContractauthordetails> _AuthorContractauthordetails;
        private readonly IRepository<AuthorContractRoyality> _AuthorContractRoyality;
        private readonly IRepository<AuthorContractHistory> _AuthorContractHistory;
        private readonly IRepository<ProductLicenceAuthorContractLink> _ProductLicenceAuthorContractLink;
        private readonly IRepository<AuthorContractMenuscriptDeliveryLink> _AuthorContractMenuscriptDeliveryLink;
        private readonly IRepository<ProductPreviousProductLink> _ProductPreviousProductLinkRepository;
        private readonly IRepository<AuthorAmendmentDocument> _AuthorAmendmentDocument;
        private readonly IRepository<DocumentTypeMaster> _DocumentTypeMaster;
        
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="GeographyType">Geography</param>        
        public AuthorContractService(IRepository<AuthorContractOriginal> AuthorContractOriginal
                                     , IRepository<AuthorContractAgreement> AuthorContractAgreement
                                     , IRepository<AuthorContractDocument> AuthorContractDocument
                                     , IRepository<AddendumFileUpload> AddendumFileUpload
                                     , IRepository<AuthorContractContributor> AuthorContractContributor
                                     , IRepository<AuthorContractmaterialdetails> AuthorContractmaterialdetails
                                     , IRepository<AuthorContractSubsidiaryRights> AuthorContractSubsidiaryRights
                                     , IRepository<AuthorContractauthordetails> AuthorContractauthordetails
                                     , IRepository<AuthorContractRoyality> AuthorContractRoyality
                                     , IRepository<AuthorContractHistory> AuthorContractHistory
                                     , IRepository<ProductLicenceAuthorContractLink> ProductLicenceAuthorContractLink
            , IRepository<AuthorContractAddendumDetails> AuthorContractAddendumDetails
            , IRepository<AuthorContractAddendumRoyality> AuthorContractAddendumRoyality
            , IRepository<AuthorContractMenuscriptDeliveryLink> AuthorContractMenuscriptDeliveryLink
            , IRepository<ProductPreviousProductLink> ProductPreviousProductLinkRepository
            , IRepository<AuthorAmendmentDocument> AuthorAmendmentDocument
            , IRepository<DocumentTypeMaster> DocumentTypeMaster
            )
        {
            _AuthorContractOriginal = AuthorContractOriginal;
            _AuthorContractAgreement = AuthorContractAgreement;
            _AuthorContractDocument = AuthorContractDocument;
            _AddendumFileUpload = AddendumFileUpload;
            _AuthorContractContributor = AuthorContractContributor;
            _AuthorContractmaterialdetails = AuthorContractmaterialdetails;
            _AuthorContractSubsidiaryRights = AuthorContractSubsidiaryRights;
            _AuthorContractauthordetails = AuthorContractauthordetails;
            _AuthorContractRoyality = AuthorContractRoyality;
            _AuthorContractHistory = AuthorContractHistory;
            _ProductLicenceAuthorContractLink = ProductLicenceAuthorContractLink;
            _AuthorContractAddendumDetails = AuthorContractAddendumDetails;
            _AuthorContractAddendumRoyality = AuthorContractAddendumRoyality;
            _AuthorContractMenuscriptDeliveryLink = AuthorContractMenuscriptDeliveryLink;
            _ProductPreviousProductLinkRepository = ProductPreviousProductLinkRepository;
            this._AuthorAmendmentDocument = AuthorAmendmentDocument;
            _DocumentTypeMaster = DocumentTypeMaster;
        }



        #endregion

        /*
         Created By :   Dheeraj kumar Sharma
         Create on  :   16th june 2016
         Created For :  Function for insert Author contract into the database
         */

        public int InsertAuthorContract(AuthorContractOriginal AuthorContract)
        {
           AuthorContract.Deactivate = "N";
            AuthorContract.EntryDate = DateTime.Now;
            AuthorContract.ModifiedBy = null;
            AuthorContract.ModifiedDate = null;
            AuthorContract.DeactivateBy = null;
            AuthorContract.DeactivateDate = null;
            _AuthorContractOriginal.Insert(AuthorContract);
          return AuthorContract.Id;
        }
        public void ProductLicenceAuthorContractLink(ProductLicenceAuthorContractLink Obj)
        {
            Obj.Deactivate = "N";
            Obj.EntryDate = DateTime.Now;
            Obj.ModifiedBy = null;
            Obj.ModifiedDate = null;
            Obj.DeactivateBy = null;
            Obj.DeactivateDate = null;
            _ProductLicenceAuthorContractLink.Insert(Obj);
        }


        /*
        Created By :   Dheeraj kumar Sharma
        Create on  :   4th july 
        Created For :  Function for insert Author contract agreement into the database
        */

        public void InsertAuthorContractAgreement(AuthorContractAgreement Agreement)
        {
            Agreement.Deactivate = "N";
            Agreement.EntryDate = DateTime.Now;
            Agreement.ModifiedBy = null;
            Agreement.ModifiedDate = null;
            Agreement.DeactivateBy = null;
            Agreement.DeactivateDate = null;
            _AuthorContractAgreement.Insert(Agreement);
        }
        public void InsertAuthorAgreementDocument(AuthorContractDocument Agreement)
        {
            Agreement.Deactivate = "N";
            Agreement.EntryDate = DateTime.Now;
            Agreement.ModifiedBy = null;
            Agreement.ModifiedDate = null;
            Agreement.DeactivateBy = null;
            Agreement.DeactivateDate = null;
            _AuthorContractDocument.Insert(Agreement);
        }
        public void UpdateAuthorContract(AuthorContractOriginal AuthorContract)
        {
            _AuthorContractOriginal.Update(AuthorContract);

        }
        
        public AuthorContractOriginal GetAuthorContractById(Int64 Id)
        {
            return _AuthorContractOriginal.Table.Where(i => i.Id == Id && i.Deactivate == "N").FirstOrDefault();
        }

        public IList<AuthorContractOriginal> GetAuthorContractBySeriesId(String SeriesCode)
        {
            return _AuthorContractOriginal.Table.Where(i => i.SeriesCode == SeriesCode && i.Deactivate == "N").ToList();
        }

        public  IList<AuthorContractOriginal> GetAuthorContractBySeriesId1(int SeriesId)
        {
            return _AuthorContractOriginal.Table.Where(i => i.SeriesId == SeriesId && i.Deactivate == "N").ToList();
        }

        public IList<AuthorContractOriginal> GetAllAuthorContract()
        {
            return _AuthorContractOriginal.Table.Where(i => i.Deactivate == "N").ToList();
        }

        public AuthorContractAgreement getAgreementByContractId(Int64 Id)
        {
            return _AuthorContractAgreement.Table.Where(i => i.ContractId == Id && i.Deactivate == "N").FirstOrDefault();
        }
        public AuthorContractAgreement getAgreementByContractId(Int64 Id, string SeriesCode)
        {
            if (SeriesCode == null || SeriesCode == "")
            {
                return _AuthorContractAgreement.Table.Where(i => i.ContractId == Id && i.Deactivate == "N").FirstOrDefault();
            }
            else
            {
                return _AuthorContractAgreement.Table.Where(i => i.SeriesCode == SeriesCode && i.Deactivate == "N").FirstOrDefault();
            }
        }

        public AuthorContractDocument getAuthorcontractDocument(Int64 Id)
        {
            return _AuthorContractDocument.Table.Where(i => i.Id == Id && i.Deactivate == "N").FirstOrDefault();
        }
        public void DeavtivateauthorContractDocumentsLinkById(Int64 id)
        {
            AuthorContractDocument document = getAuthorcontractDocument(id);
            document.Deactivate = "Y";
            document.DeactivateBy = document.EnteredBy;
            document.DeactivateDate = DateTime.Now;
            _AuthorContractDocument.Update(document);

        }


        public IList<AuthorContractDocument> getAuthorDocument(Int64 id)
        {
            
            return _AuthorContractDocument.Table.Where(i => i.AgreementId == id && i.Deactivate == "N").ToList();
        }



        //Added by Saddam for Addendum file upload insert 
        public void InsertAddendumFileUpload(AddendumFileUpload Addendum)
        {
            Addendum.Deactivate = "N";
            Addendum.EntryDate = DateTime.Now;
            Addendum.ModifiedBy = null;
            Addendum.ModifiedDate = null;
            Addendum.DeactivateBy = null;
            Addendum.DeactivateDate = null;
            _AddendumFileUpload.Insert(Addendum);
        }
        
        //Added by Saddam for Addendum file Display 


        //Added by Ankush for Addendum Details insert
        public int UpdateAuthorContractAddendumDetails(AuthorContractAddendumDetails AuthorContractAddendumDetails)
        {
            _AuthorContractAddendumDetails.Update(AuthorContractAddendumDetails);
            return AuthorContractAddendumDetails.Id;
        }

        public int InsertAuthorContractAddendumDetails(AuthorContractAddendumDetails AuthorContractAddendumDetails)
        {
            AuthorContractAddendumDetails.AddendumCode = GetAddendumCode(Convert.ToInt32(AuthorContractAddendumDetails.AuthorContractId), AuthorContractAddendumDetails.SeriesCode);
            _AuthorContractAddendumDetails.Insert(AuthorContractAddendumDetails);
            return AuthorContractAddendumDetails.Id;
        }

        public string GetAddendumCode(int AuthorContractId = 0, string seriesCode = "")
        {
            if (seriesCode != "" && seriesCode != null)
            {
                var AddendumNumber = _AuthorContractAddendumDetails.Table.OrderByDescending(a => a.AddendumCode.Substring(11, 4)).Select(a => a.AddendumCode.Substring(11, 4)).FirstOrDefault();
                if (AddendumNumber == null || AddendumNumber == "")
                {
                    AddendumNumber = "0000";
                }
                AddendumNumber = seriesCode + "AD" + (Convert.ToInt16(AddendumNumber) + 1).ToString().PadLeft(4, '0');
                return AddendumNumber;
            }
            else
            {
                var AuthorContractCode = _AuthorContractOriginal.Table.Where(a => a.Id == AuthorContractId && a.Deactivate == "N").Select(a => a.AuthorContractCode).FirstOrDefault();
                var AddendumNumber = _AuthorContractAddendumDetails.Table.OrderByDescending(a => a.AddendumCode.Substring(11, 4)).Select(a => a.AddendumCode.Substring(11, 4)).FirstOrDefault();
                if (AddendumNumber == null || AddendumNumber == "")
                {
                    AddendumNumber = "0000";
                }
                AddendumNumber = AuthorContractCode + "AD" + (Convert.ToInt16(AddendumNumber) + 1).ToString().PadLeft(4, '0');
                return AddendumNumber;
            }
            return null;
        }

        public void InsertAuthorContractAddendumRoyality(AuthorContractAddendumRoyality Addendum)
        {
            _AuthorContractAddendumRoyality.Insert(Addendum);
        }

        public AuthorContractAddendumRoyality getAuthorContractAddendumRoyalityById(int Id)
        {
            return _AuthorContractAddendumRoyality.Table.Where(a => a.Deactivate == "N" && a.Id == Id).FirstOrDefault();
        }

        public void UpdateAuthorContractAddendumRoyality(AuthorContractAddendumRoyality Addendum)
        {
            _AuthorContractAddendumRoyality.Update(Addendum);
        }

        public IList<AddendumFileUpload> getAddendumFileUpload(Int64 id)
        {
            var AddendumDetails = _AuthorContractAddendumDetails.Table.Where(a => a.Deactivate == "N" &&  a.AuthorContractId == id).ToList();
            var AddendumFileUpload = _AddendumFileUpload.Table.Where(i => i.Deactivate == "N").ToList();

            var mvarAddendumFileUploadList = (from addendumFileUpload in AddendumFileUpload
                            join addendumDetails in AddendumDetails
                            on addendumFileUpload.AddendumDetailsId equals addendumDetails.Id

                                  select new AddendumFileUpload
                                  {
                                      Id = addendumFileUpload.Id,
                                      Documentname = addendumFileUpload.Documentname,
                                      documentfile = addendumFileUpload.documentfile,
                                      AddendumDetailsId = addendumFileUpload.AddendumDetailsId,
                                  }).ToList();

            return mvarAddendumFileUploadList;

        }

        public AuthorContractAddendumDetails getAddendumDetails(Int64 id)
        {
            //var AddendumDetails = _AuthorContractAddendumDetails.Table.Where(a => a.AuthorContractId == id && a.Deactivate == "N").FirstOrDefault();
            var AddendumDetails = _AuthorContractAddendumDetails.Table.Where(a => a.AuthorContractId == id && a.Deactivate == "N").OrderByDescending(a => a.EntryDate).FirstOrDefault();
            return AddendumDetails;
        }

        public AuthorContractAddendumDetails getAddendumDetailsView(Int64 id, Int32 addendumId)
        {
            var AddendumDetails = _AuthorContractAddendumDetails.Table.Where(a => a.AuthorContractId == id && a.Deactivate == "N" && a.Id == addendumId).FirstOrDefault();
            return AddendumDetails;
        }

        public IList<AddendumFileUpload> getAddendumFileUploadBySeries(string SeriesCode)
        {
            var AddendumDetails = _AuthorContractAddendumDetails.Table.Where(a => a.Deactivate == "N" && a.SeriesCode == SeriesCode).ToList();
            var AddendumFileUpload = _AddendumFileUpload.Table.Where(i => i.Deactivate == "N").ToList();

            var mvarAddendumFileUploadList = (from addendumFileUpload in AddendumFileUpload
                                              join addendumDetails in AddendumDetails
                                              on addendumFileUpload.AddendumDetailsId equals addendumDetails.Id

                                              select new AddendumFileUpload
                                              {
                                                  Id = addendumFileUpload.Id,
                                                  Documentname = addendumFileUpload.Documentname,
                                                  documentfile = addendumFileUpload.documentfile,
                                                  AddendumDetailsId = addendumFileUpload.AddendumDetailsId,
                                              }).ToList();

            return mvarAddendumFileUploadList;

        }

        public AuthorContractAddendumDetails getAddendumDetailsBySeries(string SeriesCode)
        {
            //var AddendumDetails = _AuthorContractAddendumDetails.Table.Where(a => a.Deactivate == "N" && a.SeriesCode == SeriesCode).FirstOrDefault();
            var AddendumDetails = _AuthorContractAddendumDetails.Table.Where(a => a.Deactivate == "N" && a.SeriesCode == SeriesCode).OrderByDescending(a => a.EntryDate).FirstOrDefault();

            return AddendumDetails;

        }

        public AuthorContractAddendumDetails getAddendumDetailsBySeriesView(string SeriesCode, Int32 addendumId)
        {
            var AddendumDetails = _AuthorContractAddendumDetails.Table.Where(a => a.Deactivate == "N" && a.SeriesCode == SeriesCode && a.Id == addendumId).FirstOrDefault();

            return AddendumDetails;

        }


        public void DeavtivateauthorAddendumFileUploadById(Int64 id)
        {
            AddendumFileUpload document = _AddendumFileUpload.Table.Where(i => i.Id == id && i.Deactivate == "N").FirstOrDefault();
            document.Deactivate = "Y";
            document.DeactivateBy = document.EnteredBy;
            document.DeactivateDate = DateTime.Now;
            _AddendumFileUpload.Update(document);

        }
        //End By Ankush

        public void DeactivateAuthorContributor(IList<AuthorContractContributor> _ListContributor)
        {

            foreach (AuthorContractContributor list in _ListContributor)
            {
                _AuthorContractContributor.Delete(list);
            }
        }
        public void DeativateMenuscriptDeliveryLink(IList<AuthorContractMenuscriptDeliveryLink> _ListMenu)
        {

            foreach (AuthorContractMenuscriptDeliveryLink list in _ListMenu)
            {
                _AuthorContractMenuscriptDeliveryLink.Delete(list);
            }
        }
        public void DeactivateAuthorMaterial(IList<AuthorContractmaterialdetails> _listMaterial)
        {
            foreach (AuthorContractmaterialdetails list in _listMaterial)
            {
                _AuthorContractmaterialdetails.Delete(list);
            }
        }

        public void DeativateAuthorContractSubsidiaryRights(IList<AuthorContractSubsidiaryRights> _lstSupply)
        {
            foreach (AuthorContractSubsidiaryRights list in _lstSupply)
            {
                _AuthorContractSubsidiaryRights.Delete(list);
            }
        }
        public void DeativateAuthorandRoyaltySlab(IList<AuthorContractauthordetails> _lstAuthor)
        {
            foreach (var list in _lstAuthor)
            {
                IList<AuthorContractRoyality> Royalty = list.AuthorContractRoyality.ToList();
                foreach (AuthorContractRoyality _royalty in Royalty)
                {
                    _AuthorContractRoyality.Delete(_royalty);
                }
               _AuthorContractauthordetails.Delete(list);
            }
        }

        public AuthorContractContributor GetContractById(int Id)
        {
            return _AuthorContractContributor.Table.Where(i => i.Id == Id).FirstOrDefault();
        }


        public void InsertSearchHistory(AuthorContractHistory _AuthorContractSearch)
        {
            _AuthorContractHistory.Insert(_AuthorContractSearch);
        }

        public IList<AuthorContractSubsidiaryRights> GetAuthorContractSubsidiaryRightsList()
        {
            return _AuthorContractSubsidiaryRights.Table.ToList();
        }

        public void deactivateAgreementbySeriesCode(string SeriesCode,int deactivateBy)
        {
            //AuthorContractAgreement agreement = get

            IList<AuthorContractAgreement> _list = AuthorContractAgreementbySeriesCode(SeriesCode);
            if (_list!=null)
            {
                foreach (var lst in _list)
                {
                    lst.DeactivateBy = deactivateBy;
                    lst.DeactivateDate = DateTime.Now;
                    lst.Deactivate = "Y";
                    _AuthorContractAgreement.Update(lst);
                }
            }
           
        }

        public IList<AuthorContractAgreement> AuthorContractAgreementbySeriesCode(string SeriesCode)
        {
            //IList<AuthorContractAgreement> _agreement = new List<AuthorContractAgreement>();
            return  _AuthorContractAgreement.Table.Where(i => i.SeriesCode == SeriesCode && i.Deactivate=="N").ToList();
        }

        public IList<AuthorContractRoyality> AuthorContractRoyalityByAuthorcontract(Int64 AuthorContractId)
        {
           return _AuthorContractRoyality.Table.Where(a => a.Deactivate == "N" && a.AuthorContractid == AuthorContractId).ToList();
        }

        public IList<AuthorContractRoyality> AuthorContractRoyality()
        {
            return _AuthorContractRoyality.Table.Where(a => a.Deactivate == "N").ToList();
        }

        //Added by Ankush dated 24/10/2016
        public IList<ProductPreviousProductLink> ProductPreviousProductLinkList(int PreviousProductId)
        {
            var list = _ProductPreviousProductLinkRepository.Table.Where(x => x.PreviousProductId == PreviousProductId && x.PreviousProductId != 0 && x.Deactivate == "N").ToList();
            return list;
        }


        public IList<AuthorAmendmentDocument> getAuthorDocument(int id)
        {
            return _AuthorAmendmentDocument.Table.Where(i => i.AuthorContractId == id && i.Deactivate == "N").ToList();
        }

        public void DeavtivateAuthorDocument(int id, int enteredBy)
        {
            IList<AuthorAmendmentDocument> Linking = getAuthorDocument(id);
            foreach (var lst in Linking)
            {
                lst.Deactivate = "Y";
                lst.DeactivateBy = enteredBy;
                lst.DeactivateDate = DateTime.Now;
                _AuthorAmendmentDocument.Update(lst);
            }

        }


        public void InsertAuthorAmendmentDocumentLinking(AuthorAmendmentDocument AuthorAmendmentDocument)
        {
            AuthorAmendmentDocument.Deactivate = "N";
            AuthorAmendmentDocument.EntryDate = DateTime.Now;
            AuthorAmendmentDocument.ModifiedBy = null;
            AuthorAmendmentDocument.ModifiedDate = null;
            AuthorAmendmentDocument.DeactivateBy = null;
            AuthorAmendmentDocument.DeactivateDate = null;
            _AuthorAmendmentDocument.Insert(AuthorAmendmentDocument);
        }


        public IList<AuthorAmendmentDocument> getAuthorAmendmentDocumentDocumentsById(int id)
        {
            return _AuthorAmendmentDocument.Table.Where(i => i.AuthorContractId == id && i.Deactivate == "N").ToList();
        }

        public AuthorAmendmentDocument getAuthorAmendmentDocumentsById(int id)
        {
            return _AuthorAmendmentDocument.Table.Where(i => i.Id == id && i.Deactivate == "N").FirstOrDefault();
        }

        public AuthorContractDocument AuthorContractDocumentsById(int id)
        {
            return _AuthorContractDocument.Table.Where(i => i.Id == id && i.Deactivate == "N").FirstOrDefault();
        }


        public void DeavtivateAuthorAmendmentDocumentsDocumentById(int id, int enteredBy)
        {
            IList<AuthorAmendmentDocument> Linking = _AuthorAmendmentDocument.Table.Where(i => i.Id == id && i.Deactivate == "N").ToList();
            foreach (var lst in Linking)
            {
                lst.Deactivate = "Y";
                lst.DeactivateBy = enteredBy;
                lst.DeactivateDate = DateTime.Now;
                _AuthorAmendmentDocument.Update(lst);
            }

        }

        public void DeavtivateAuthorContractDocumentsDocumentById(int id, int enteredBy)
        {
            IList<AuthorContractDocument> Linking = _AuthorContractDocument.Table.Where(i => i.Id == id && i.Deactivate == "N").ToList();
            foreach (var lst in Linking)
            {
                lst.Deactivate = "Y";
                lst.DeactivateBy = enteredBy;
                lst.DeactivateDate = DateTime.Now;
                _AuthorContractDocument.Update(lst);
            }

        }

        public IList<AuthorContractOriginal> GetAuthorContractSeries()
        {
            return _AuthorContractOriginal.Table.Where(i =>i.Deactivate == "N").ToList();
        }

        public IList<AuthorContractContributor> GetAuthorContractContributorListByContractId(int contractId)
        {
            return _AuthorContractContributor.Table.Where(c => c.AuthorContractId == contractId && c.Deactivate == "N").ToList();
        }

        public AuthorContractContributor GetAuthorContractContributorDetailById(int id)
        {
            return _AuthorContractContributor.Table.Where(c => c.Id == id && c.Deactivate == "N").FirstOrDefault();
        }


        public void UpdateAuthorContractContributorDetails(AuthorContractContributor mobj_AuthorContractContributor)
        {
            _AuthorContractContributor.Update(mobj_AuthorContractContributor);
        }

        public void InsertAuthorContractContributorDetails(AuthorContractContributor mobj_AuthorContractContributor)
        {
            _AuthorContractContributor.Insert(mobj_AuthorContractContributor);
        }


    }

}
