//create by saddam 
//date : 17/05/2016
//purpose : Insert, Update, Delete Records for Author Master
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Master
{
    public partial interface IAuthorService
    {

        /// <summary>
        /// check Duplicity
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        //IList<AuthorMasterDetail> GetAllAuthor(AuthorMasterDetail Author);

       

        String DuplicityCheck(AuthorMaster Author);
        /// <summary>
        /// Insert Division 
        /// </summary>
        /// <param name="city">City class object</param>
        /// <returns></returns>
        int  InsertAuthor(AuthorMaster Author);

        /// <summary>
        /// Get Division
        /// </summary>
        /// <param name="Geography city">Division as object</param>
        /// <returns>Division</returns>
        AuthorMaster GetAuthorById(AuthorMaster Author);

        /// <summary>
        /// Update Division 
        /// </summary>
        /// <param name="Division">Division class object</param>
        /// <returns></returns>
        void UpdateAuthor(AuthorMaster Author);


        /// <summary>
        /// Delete Division 
        /// </summary>
        /// <param name="Division">Delete Division Object</param>
        /// <returns></returns>
        void DeleteAuthor(AuthorMaster Author);

      //IList<AuthorMaster> GetAllAuthor();


        IList<AuthorMaster> GetAuthorist(AuthorMaster AuthorMaster);


        void InsertAuthorDocumentLinking(AuhtorDocument AuhtorDocumentLink);

        void InsertNomineeAuthorDocumentLinking(NomineeAuthorDocumentMaster NomineeAuhtorDocument);


        void DeavtivateAuthorDocument(int id, int enteredBy);


        IList<AuhtorDocument> getAuthorDocument(int id);

        AuhtorDocument getAuhtorDocumentDetail(int DocumentId);

        void DeavtivateAuthorDocumentById(int id, int enteredBy);


        void InsertSearchHistory(AuthorSearchHistory _SearchParam);


        NomineeAuthorDocumentMaster getNomineeAuhtorDocumentDetail(int DocumentId);

        void DeavtivateNomineeAuthorDocumentById(int id, int enteredBy);

        //Added Ankush on 20/07/2016
        IList<AuthorMaster> getAuthorMaster();
    }
   
}
