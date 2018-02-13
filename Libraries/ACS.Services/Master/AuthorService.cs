//create by saddam 
//date : 17/05/2016
//purpose : Insert, Update, Delete Records for Author Master

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


namespace ACS.Services.Master
{
    public partial class AuthorService  : IAuthorService  
    {
        #region Fields
        private readonly IDbContext _dbContext;
            private readonly IRepository<AuthorMaster> _AuthorRepository;
            private readonly IRepository<GeographicalMaster> _GeographicalMasterRepository;
           private readonly IEncryptionService _encryptionService;
           private readonly IRepository<AuhtorDocument> _AuhtorDocument;
           private readonly IRepository<AuthorSearchHistory> _AuthorSearchHistory;
           private readonly IRepository<NomineeAuthorDocumentMaster> _NomineeAuthorDocumentMaster;
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="GeographyType">Geography</param>        
           public AuthorService(
            IRepository<AuthorMaster> AuthorRepository
               , IEncryptionService encryptionService
               , IRepository<GeographicalMaster> GeographicalService
                  ,IDbContext dbContext
               , IRepository<AuhtorDocument> AuhtorDocument
               , IRepository<AuthorSearchHistory> AuthorSearchHistory
               ,IRepository<NomineeAuthorDocumentMaster> NomineeAuthorDocumentMaster
               )
        {
            _AuthorRepository = AuthorRepository;
            _GeographicalMasterRepository = GeographicalService;
            _AuhtorDocument = AuhtorDocument;
           this._encryptionService = encryptionService;
           this._dbContext = dbContext;
           _AuthorSearchHistory = AuthorSearchHistory;
           _NomineeAuthorDocumentMaster = NomineeAuthorDocumentMaster;
        }

        

        #endregion

        #region Methods


          // public virtual IList<AuthorMaster> GetAllAuthor()
          // {
          // var Author = _AuthorRepository.Table.Where(a => a.LastName != null && a.Deactivate == "N").OrderBy(c => c.LastName).ToList();
          // return Author;

          
          //}


            public string DuplicityCheck(AuthorMaster  Author)
            {

                var dupes = _AuthorRepository.Table.Where(x => x.Email == Author.Email  
                                                         && x.Deactivate == "N"
                                                          && (Author.Id != 0 ? x.Id : 0) != (Author.Id != 0 ? Author.Id : 1)).FirstOrDefault();
                if (dupes != null)
                {
                    return "N";

                }
                else
                {
                    return "Y";
                }
            }



            public int InsertAuthor(AuthorMaster Author)
        {


            Author.Deactivate = "N";
          //  Author.EnteredBy = 10;
            Author.EntryDate = DateTime.Now;
            Author.ModifiedBy = null;
            Author.ModifiedDate = null;
            Author.DeactivateBy = null;
            Author.DeactivateDate = null;
            
            _AuthorRepository.Insert(Author);
            return Author.Id;
        }

            public AuthorMaster  GetAuthorById(AuthorMaster Author)
        {
            return _AuthorRepository.Table.Where(i => i.Id == Author.Id && i.Deactivate=="N").FirstOrDefault();
        }

            public void UpdateAuthor(AuthorMaster Author)
        {
            _AuthorRepository.Update(Author);
        }

            public void DeleteAuthor(AuthorMaster Author)
        {
            _AuthorRepository.Delete(Author);
        }

        #endregion


            public virtual IList<AuthorMaster> GetAuthorist(AuthorMaster Author)
            {
                return _AuthorRepository.Table.Where(a => a.Deactivate == "N" && a.Id == Author.Id).OrderBy(c => c.LastName).ToList();

            }


            //public IList<AuthorMasterDetail> GetAllAuthor(AuthorMasterDetail Author)
            //{

            //    try
            //    {


            //        var _GetAuthorReport = _dbContext.ExecuteStoredProcedureListNewData<AuthorMasterDetail>("Proc_AuthorSerchReport_get", Author).ToList();

            //        return _GetAuthorReport;
            //    }
            //    catch (Exception ex)
            //    {
            //        return null;
            //    }
            //}


            public void InsertAuthorDocumentLinking(AuhtorDocument AuhtorDocument)
            {
                AuhtorDocument.Deactivate = "N";
                AuhtorDocument.EntryDate = DateTime.Now;
                AuhtorDocument.ModifiedBy = null;
                AuhtorDocument.ModifiedDate = null;
                AuhtorDocument.DeactivateBy = null;
                AuhtorDocument.DeactivateDate = null;
                _AuhtorDocument.Insert(AuhtorDocument);
            }



            public void InsertNomineeAuthorDocumentLinking(NomineeAuthorDocumentMaster NomineeAuhtorDocument)
            {
                NomineeAuhtorDocument.Deactivate = "N";
                NomineeAuhtorDocument.EntryDate = DateTime.Now;
                NomineeAuhtorDocument.ModifiedBy = null;
                NomineeAuhtorDocument.ModifiedDate = null;
                NomineeAuhtorDocument.DeactivateBy = null;
                NomineeAuhtorDocument.DeactivateDate = null;
                _NomineeAuthorDocumentMaster.Insert(NomineeAuhtorDocument);
            }


            public IList<AuhtorDocument> getAuthorDocument(int id)
            {
                return _AuhtorDocument.Table.Where(i => i.AuhtorId == id && i.Deactivate == "N").ToList();
            }

            public void DeavtivateAuthorDocument(int id, int enteredBy)

            {
                IList<AuhtorDocument> Linking = getAuthorDocument(id);
                foreach (var lst in Linking)
                {
                    lst.Deactivate = "Y";
                    lst.DeactivateBy = enteredBy;
                    lst.DeactivateDate = DateTime.Now;
                    _AuhtorDocument.Update(lst);
                }

            }

            public AuhtorDocument getAuhtorDocumentDetail(int DocumentId)
            {
                return _AuhtorDocument.Table.Where(i => i.Id == DocumentId).FirstOrDefault();
            }

            public NomineeAuthorDocumentMaster getNomineeAuhtorDocumentDetail(int DocumentId)
            {
                return _NomineeAuthorDocumentMaster.Table.Where(x => x.Id == DocumentId).FirstOrDefault();
            }

            public void DeavtivateAuthorDocumentById(int id, int enteredBy)
            {
                IList<AuhtorDocument> Linking = _AuhtorDocument.Table.Where(i => i.Id == id && i.Deactivate == "N").ToList();
                foreach (var lst in Linking)
                {
                    lst.Deactivate = "Y";
                    lst.DeactivateBy = enteredBy;
                    lst.DeactivateDate = DateTime.Now;
                    _AuhtorDocument.Update(lst);
                }

            }


            public void DeavtivateNomineeAuthorDocumentById(int id, int enteredBy)
            {
                IList<NomineeAuthorDocumentMaster> Linking = _NomineeAuthorDocumentMaster.Table.Where(i => i.Id == id && i.Deactivate == "N").ToList();
                foreach (var lst in Linking)
                {
                    lst.Deactivate = "Y";
                    lst.DeactivateBy = enteredBy;
                    lst.DeactivateDate = DateTime.Now;
                    _NomineeAuthorDocumentMaster.Update(lst);
                }

            }


            public void InsertSearchHistory(AuthorSearchHistory _SerachHistory)
            {
                _AuthorSearchHistory.Insert(_SerachHistory);

            }

            //Added Ankush on 20/07/2016
            public IList<AuthorMaster> getAuthorMaster()
            {
                return _AuthorRepository.Table.Where(i => i.Deactivate == "N").ToList();
            }
    }
}
