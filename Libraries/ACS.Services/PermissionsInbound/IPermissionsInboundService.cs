//create by saddam 
//date : 27/07/2016

using ACS.Core.Domain.Master;
using ACS.Core.Domain.OtherContract;
using ACS.Core.Domain.PermissionInbound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ACS.Services.PermissionsInbound
{


    public partial interface IPermissionsInboundService
    {

        IList<OtherContractMaster> GetOtherContractList();

        /// <summary>
        /// service for party details based on othercontractid
        /// </summary>
        /// <param name="showHidden"></param>
        /// <returns>list of all</returns>
        /// <summary>
        OtherContractImageBank GeOtherContractImageBankDetails(int otherContractId);

        /// <summary>
        /// service for getting other rights master as print,ebook,electronic
        /// </summary>
        /// <param name="showHidden"></param>
        /// <returns>list of all</returns>
        /// <summary>
        IList<OtherRightsMaster> GeOtherRightsMaster();

        /// <summary>
        /// service for for inserting permission inbound data into the table
        /// </summary>
        /// <param name="showHidden"></param>
        /// <returns>list of all</returns>
        /// <summary>
        void InsertPermissionInbound(PermissionInbound _InboundTable);


        int InsertPermissionInboundData(PermissionInbound _InboundTable);

        /// <summary>
        /// service for for Updating permission inbound data into the table
        /// </summary>
        /// <param name="showHidden"></param>
        /// <returns>list of all</returns>
        /// <summary>
        void UpdatePermissionInbound(PermissionInbound _InboundTable);
        /// <summary>
        /// getting the record of copyright table based on Id
        /// </summary>
        /// <param name="showHidden"></param>
        /// <returns>list of all</returns>
        /// <summary>
        /// 
        CopyRightHolderMaster getCopyRightHolderById(int id);
        /// <summary>
        /// Dump the copy right master data into table
        /// </summary>
        /// <param name="showHidden"></param>
        /// <returns>none</returns>
        /// <summary>
        /// 
        void insertintoPermissionInboundCopyRightHolderMasterDump(PermissionInboundCopyRightHolderMaster Obj);
        /// <summary>
        /// getting the Inbound Permission based on inbound Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>InboundPermission table object</returns>
        /// <summary>
        /// 
        PermissionInbound getInboundPermissionDetailsById(int Id);


       IList< PermissionInbound> getInboundPermissionDetailsByCode(string  code);


        PermissionInboundOthers getPermissionInboundOthersDetailsCopyRightHolderById(int Id);

        /// <summary>
        /// getting the Copy Right holder which are not used in inbound permission
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Copy Right holder IList</returns>
        /// <summary>
        /// 
        IList<CopyRightHolderMaster> getCopyRightHolderNotUsed(string cpyIds);

        /// <summary>
        /// Insert other inbound permission for copyright holder
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>void</returns>
        /// <summary>
        /// 
        void insertPermissionInboundOthers(PermissionInboundOthers obj);
        /// <summary>
        /// Insert Search Parameter into search history table to maintain search
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>void</returns>
        /// <summary>
        /// 
        void PermissionInboundSearchHistory(PermissionInboundSearchHistory obj);


        PermissionInboundImageVideoBank getPermissionInboundImageVideoBankById(int Id);

        IList<PermissionInboundImageVideoBankData> getPermissionInboundImageVideoBankDataById(int Id);

        OtherContractImageBank OtherContractImageBankById(int id);

        OtherContractMaster OtherContractMasterById(int id);



     IList<PermissionInboundCopyRightHolderMaster> getPermissionInboundCopyRightHolderById(int Id);

        PermissionInboundOthers getPermissionInboundOtherById(int Id);

        IList<OtherContractDateRequest> getOtherContractDateRequestById(int Id);

        int InsertPermissionInboundUpdate(PermissionInboundUpdate PermissionInboundUpdate);

        void InsertPermissionInboundDocuments(PermissionInboundDocuments PermissionInboundDocuments);

        PermissionInboundUpdate getPermissionInboundUpdateById(int Id);

        IList<PermissionInboundDocuments> getPermissionInboundDocumentsById(int Id);

        PermissionInboundDocuments getInboundDocumentsDocumentsById(int Id);

        void DeavtivatePermissionsInboundDocumentById(int id, int enteredBy);


        PermissionInboundImageVideoBankData getPermissionInboundImageVideoBankDetialById(int Id);


        void UpdatePermissionInboundImageVideoBankData(PermissionInboundImageVideoBankData PermissionInboundImageVideoBankData);

        void InsertPermissionInboundImageVideoBankData(PermissionInboundImageVideoBankData PermissionInboundImageVideoBankData);

        void UpdatePermissionInboundImageVideoBank(PermissionInboundImageVideoBank PermissionInboundImageVideoBank);


        void UpdatePermissionInboundData(PermissionInbound PermissionInbound);


        PermissionInbound getPermissionInboundById(int Id);

        PermissionInboundCopyRightHolderMaster getPermissionInboundCopyRightHolderMasterByInboundOthersId(int Id);

        void UpdatePermissionInboundCopyRightHolderMaster(PermissionInboundCopyRightHolderMaster PermissionInboundCopyRightHolderMaster);


        void UpdatePermissionInboundOthers(PermissionInboundOthers PermissionInboundOthers);


        void DeavtivateOtherContractDateRequest(int id, int enteredBy);


        void DeavtivatePermissionInboundOthersRightsLink(int id, int enteredBy);

        void InsertOtherContractDateRequest(OtherContractDateRequest OtherContractDateRequest);
        void InsertPermissionInboundOthersRightsLink(PermissionInboundOthersRightsLink PermissionInboundOthersRightsLink);


        int InsertPermissionInboundOthers(PermissionInboundOthers PermissionInboundOthers);

        void InsertPermissionInboundCopyRightHolderMaster(PermissionInboundCopyRightHolderMaster PermissionInboundCopyRightHolderMaster);
        
        int InsertPermissionInboundImageVideoBank(PermissionInboundImageVideoBank PermissionInboundImageVideoBank);


        void UpdatePermissionInboundUpdate(PermissionInboundUpdate PermissionInboundUpdate);


        int GetisValidPartyName(string PartyName);


        int GetisValidCurrency(string Currency);

       // void InsertPermissionInboundImageVideoBankLink(PermissionInboundImageVideoBank _InboundImageVideoBank);


        int InsertPermissionInboundImageVideoBankLink(PermissionInboundImageVideoBank _InboundImageVideoBank);



        void InsertNewPermissionInboundImageVideoBankData(PermissionInboundImageVideoBankData PermissionInboundImageVideoBankData);


        PermissionInboundCopyRightHolderMaster getPermissionInboundCopyRightHolderMasterById(int Id);


      //  PermissionInboundImageVideoBank getNewPermissionInboundImageVideoBankById(int Id, int PartyId);


        void DeleteInboundOthersRightsLink(PermissionInboundOthersRightsLink PermissionInboundOthersRightsLink);


        IList<PermissionInboundOthers> getPermissionInboundOthersList(string Code);

        IList<PermissionInbound> getInboundPermissionDetailsByProductId(int productId);

        void DeletePermissionInbound(PermissionInbound _PermissionInbound);

        IList<PermissionInbound> getPermissionInboundList();


        /// <summary>
        /// getting the record of copyright table based on Code
        /// </summary>
        /// <param name="showHidden"></param>
        /// <returns>Details by Code</returns>
        /// <summary>
        /// 
        CopyRightHolderMaster getCopyRightHolderByCode(string code);


        #region "Asset Sub-Type"

        /// <summary>
        /// Get /// <param name="city">AssetSubType class object</param>
        /// </summary>
        /// <returns>AssetSubType</returns>
        AssetSubType GetAssetSubTypeById(int Id);

        /// <summary>
        /// Duplicity Check
        /// </summary>
        /// <param name="AssetSubType">AssetSubType class object</param>
        /// <returns></returns>
        string DuplicityCheckAssetSubType(AssetSubType _AssetSubType);

        /// <summary>
        /// Insert AssetSubType 
        /// </summary>
        /// <param name="AssetSubType">AssetSubType class object</param>
        /// <returns></returns>
        void InsertAssetSubType(AssetSubType _AssetSubType);

        /// <summary>
        /// Update AssetSubType 
        /// </summary>
        /// <param name="AssetSubType">AssetSubType class object</param>
        /// <returns></returns>
        void UpdateAssetSubType(AssetSubType _AssetSubType);

        #endregion
        
        #region "Asset Status"

        /// <summary>
        /// Get /// <param name="city">StatusMaster class object</param>
        /// </summary>
        /// <returns>StatusMaster</returns>
        StatusMaster GetStatusMasterById(int Id);

        /// <summary>
        /// Duplicity Check
        /// </summary>
        /// <param name="StatusMaster">StatusMaster class object</param>
        /// <returns></returns>
        string DuplicityCheckStatusMaster(StatusMaster _StatusMaster);

        /// <summary>
        /// Insert StatusMaster 
        /// </summary>
        /// <param name="StatusMaster">StatusMaster class object</param>
        /// <returns></returns>
        void InsertStatusMaster(StatusMaster _StatusMaster);

        /// <summary>
        /// Update StatusMaster 
        /// </summary>
        /// <param name="StatusMaster">StatusMaster class object</param>
        /// <returns></returns>
        void UpdateStatusMaster(StatusMaster _StatusMaster);

        #endregion


    }
}
