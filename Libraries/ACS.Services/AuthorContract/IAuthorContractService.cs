using ACS.Core.Domain.AuthorContract;
using ACS.Core.Domain.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.AuthorContract
{
    public partial interface IAuthorContractService
    {

        /// <summary>
        /// Insert the Authorcontract
        /// </summary>
        /// <param name="showHidden">It will used to insert author contract</param>
        /// <returns>Country collection</returns>
        /// <summary>
        int InsertAuthorContract(AuthorContractOriginal AuthorContract);
        /// <summary>
        /// Insert the Authorcontract
        /// </summary>
        /// <param name="showHidden">Insert the product license if author contract is other that category</param>
        /// <returns>Country collection</returns>
        /// <summary>
        void ProductLicenceAuthorContractLink(ProductLicenceAuthorContractLink Obj);

        /// <summary>
        /// update author contract
        /// </summary>
        /// <param name="showHidden"> update author contract</param>
        /// <returns> update author contract</returns>
        /// <summary>
        void UpdateAuthorContract(AuthorContractOriginal AuthorContract);

        /// <summary>
        /// Get the Author Contract Product Details based on Id
        /// </summary>
        /// <param name="showHidden">contract Id</param>
        /// <returns>dheeraj Kumar sharma</returns>
        /// <summary>
        AuthorContractOriginal GetAuthorContractById(Int64 Id);

        /// <summary>
        /// Get the Author Contract Product Details based SeriesCode
        /// </summary>
        /// <param name="showHidden">contract Id</param>
        /// <returns>dheeraj Kumar sharma</returns>
        /// <summary>
        IList<AuthorContractOriginal> GetAuthorContractBySeriesId(String SeriesCode);


        /// <summary>
        /// Get the Author Contract Product Details based SeriesId
        /// </summary>
        /// <param name="showHidden">contract Id</param>
        /// <returns>Ankush kumar</returns>
        /// <summary>
        IList<AuthorContractOriginal> GetAuthorContractBySeriesId1(int SeriesId);


        IList<AuthorContractOriginal> GetAllAuthorContract();

         /// <summary>
        /// Insert the Authorcontractagreement
        /// </summary>
        /// <param name="showHidden">It will used to insert author contract agreement</param>
        /// <returns>Country collection</returns>
        /// <summary>

        void InsertAuthorContractAgreement(AuthorContractAgreement Agreement);


        /// <summary>
        /// updateAuthor agreement
        /// </summary>
        /// <param name="showHidden">it will used to update database</param>
        /// <returns>updateAuthor agreement</returns>
        /// <summary>

       
        AuthorContractAgreement getAgreementByContractId(Int64 Id);
        AuthorContractAgreement getAgreementByContractId(Int64 Id, string SeriesCode);

        /// <summary>
        /// Iget the document details
        /// </summary>
        /// <param name="showHidden">Iget the author contract agreement by contract id</param>
        /// <returns>Country collection</returns>
        /// <summary>
        AuthorContractDocument getAuthorcontractDocument(Int64 Id);

        /// <summary>
        /// delete the document
        /// </summary>
        /// <param name="showHidden">delete the document</param>
        /// <returns>delete the document</returns>
        /// <summary>
        void DeavtivateauthorContractDocumentsLinkById(Int64 id);


        /// <summary>
        /// get the document form the database based on agreement id
        /// </summary>
        /// <param name="showHidden">get the document form the database based on agreement id</param>
        /// <returns>document table type ilist</returns>
        /// <summary>
        IList<AuthorContractDocument> getAuthorDocument(Int64 id);

        
        /// <summary>
        /// insert document of agreement in update case
        /// </summary>
        /// <param name="showHidden"></param>
        /// <returns>document table type ilist</returns>
        /// <summary>
        void InsertAuthorAgreementDocument(AuthorContractDocument Agreement);


        //Added by Saddam for Addendum Upload insert
        void InsertAddendumFileUpload(AddendumFileUpload AddendumFileUpload);

        IList<AddendumFileUpload> getAddendumFileUpload(Int64 id);

        IList<AddendumFileUpload> getAddendumFileUploadBySeries(string SeriesCode);

        AuthorContractAddendumDetails getAddendumDetails(Int64 id);

        AuthorContractAddendumDetails getAddendumDetailsView(Int64 id, Int32 addendumId);

        AuthorContractAddendumDetails getAddendumDetailsBySeries(string SeriesCode);

        AuthorContractAddendumDetails getAddendumDetailsBySeriesView(string SeriesCode, Int32 addendumId);

        void DeavtivateauthorAddendumFileUploadById(Int64 id);  
        //end by saddam

        //Added by Ankush for Addendum Details insert
        int InsertAuthorContractAddendumDetails(AuthorContractAddendumDetails AuthorContractAddendumDetails);

        int UpdateAuthorContractAddendumDetails(AuthorContractAddendumDetails AuthorContractAddendumDetails);

        void InsertAuthorContractAddendumRoyality(AuthorContractAddendumRoyality AuthorContractRoyality);

        AuthorContractAddendumRoyality getAuthorContractAddendumRoyalityById(int Id);

        void UpdateAuthorContractAddendumRoyality(AuthorContractAddendumRoyality AuthorContractRoyality);


        /// <summary>
        /// delete the contributor document
        /// </summary>
        /// <param name="showHidden"></param>
        /// <returns>delete the contributor document bbased on contract Id</returns>
        /// <summary>
        void DeactivateAuthorContributor(IList<AuthorContractContributor> _ListContributor);
        /// <summary>
        /// delete the author contract material
        /// </summary>
        /// <param name="showHidden"></param>
        /// <returns>delete the author contract material</returns>
        /// <summary>

        void DeactivateAuthorMaterial(IList<AuthorContractmaterialdetails> _listMaterial);

         /// <summary>
        /// delete the author DeativateAuthorContractSubsidiaryRights
        /// </summary>
        /// <param name="showHidden"></param>
        /// <returns>delete the author DeativateAuthorContractSubsidiaryRights</returns>
        /// <summary>

        void DeativateAuthorContractSubsidiaryRights(IList<AuthorContractSubsidiaryRights> _lstSupply);
        /// <summary>
        /// delete the author and its royalty slab based on contract Id
        /// </summary>
        /// <param name="showHidden"></param>
        /// <returns>delete the author and its royalty slab based on contract Id</returns>
        /// <summary>

        void DeativateAuthorandRoyaltySlab(IList<AuthorContractauthordetails> _lstAuthor);

        /// <summary>
        ///delete all the records of menu script for partcular author contract
        /// </summary>
        /// <param name="showHidden"></param>
        /// <returns>void</returns>
        /// <summary>

        void DeativateMenuscriptDeliveryLink(IList<AuthorContractMenuscriptDeliveryLink> _lstMenuIds);


        //Added by Saddam on 19/07/2016
        void InsertSearchHistory(AuthorContractHistory _AuthorContractSearch);


        //Added By Ankush on 21/07/2016 For Get all List of AuthorContractSubsidiaryRights
        /// <summary>
        /// GetAll AuthorContractSubsidiaryRights List
        /// </summary>
        /// <param name="Geographical">GetAll AuthorContractSubsidiaryRights Object</param>
        /// <returns>AuthorContractSubsidiaryRights List</returns>
        IList<AuthorContractSubsidiaryRights> GetAuthorContractSubsidiaryRightsList();

        //Added By Ankush on 21/07/2016 For Get all List of AuthorContractSubsidiaryRights
        /// <summary>
        ///Deactivate by series code aUTHOR CONTRACT DOCUMENTS
        /// </summary>

        void deactivateAgreementbySeriesCode(string SeriesCode, int deactivateBy);

        //Added By Ankush on 21/07/2016 For Get all List of AuthorContractSubsidiaryRights
        /// <summary>
        ///deactivate document and agreement uploaded by admin
        /// </summary>
        IList<AuthorContractAgreement> AuthorContractAgreementbySeriesCode(string SeriesCode);


        //Added By Ankush on 23/09/2016 For Get Royality acording to AuthorContractId
        IList<AuthorContractRoyality> AuthorContractRoyalityByAuthorcontract(Int64 AuthorContractId);

        //Added By Ankush on 23/09/2016 For Get Royality acording to AuthorContractId
        IList<AuthorContractRoyality> AuthorContractRoyality();


        //Added By Ankush on 24/10/2016 For Get List of Derivative Product based on Previous Product Id
        IList<ProductPreviousProductLink> ProductPreviousProductLinkList(int PreviousProductId);


        void DeavtivateAuthorDocument(int id, int enteredBy);

        void InsertAuthorAmendmentDocumentLinking(AuthorAmendmentDocument AuthorAmendmentDocument);


        IList<AuthorAmendmentDocument> getAuthorAmendmentDocumentDocumentsById(int Id);

        AuthorContractDocument AuthorContractDocumentsById(int id);

        AuthorAmendmentDocument getAuthorAmendmentDocumentsById(int Id);

        void DeavtivateAuthorAmendmentDocumentsDocumentById(int id, int enteredBy);

        void DeavtivateAuthorContractDocumentsDocumentById(int id, int enteredBy);

        /// <summary>
        /// Get the Author Contract Series List
        /// </summary>
        /// <param name="showHidden"></param>
        /// <returns>Author Contract Contributer List</returns>
        /// <summary>
        IList<AuthorContractOriginal> GetAuthorContractSeries();

        /// <summary>
        /// Get the Author Contract Contributer List
        /// </summary>
        /// <param name="showHidden"></param>
        /// <returns>Author Contract Contributer List</returns>
        /// <summary>
        IList<AuthorContractContributor> GetAuthorContractContributorListByContractId(int contractId);

        AuthorContractContributor GetAuthorContractContributorDetailById(int id);

        void UpdateAuthorContractContributorDetails(AuthorContractContributor mobj_AuthorContractContributor);

        void InsertAuthorContractContributorDetails(AuthorContractContributor mobj_AuthorContractContributor);

    }
}
