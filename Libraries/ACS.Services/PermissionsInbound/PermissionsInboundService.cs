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
using ACS.Core.Domain.OtherContract;
using ACS.Core.Domain.PermissionInbound;

namespace ACS.Services.PermissionsInbound
{
 
  public partial class PermissionsInboundService : IPermissionsInboundService
  {

      #region Fields
      private readonly IDbContext _dbContext;
      private readonly IRepository<OtherContractMaster> _OtherContractMaster;
      private readonly IRepository<PermissionsOutboundMaster> _PermissionsOutboundMaster;
      private readonly IRepository<PermissionsOutboundTypeOfRightsMaster> _PermissionsOutboundTypeOfRightsMaster;
      private readonly IRepository<OtherContractImageBank> _OtherContractImageBank;
      private readonly IRepository<OtherRightsMaster> _OtherRightsMaster;
      private readonly IRepository<PermissionInbound> _PermissionInbound;
      private readonly IRepository<CopyRightHolderMaster> _CopyRightHolderMaster;
      private readonly IRepository<PermissionInboundOthers> _PermissionInboundOthers;
      private readonly IRepository<PermissionInboundCopyRightHolderMaster> _PermissionInboundCopyRightHolderMaster;
      private readonly IRepository<PermissionInboundSearchHistory> _PermissionInboundSearchHistory;
      private readonly IRepository<PermissionInboundImageVideoBank> _PermissionInboundImageVideoBank;
      private readonly IRepository<PermissionInboundImageVideoBankData> _PermissionInboundImageVideoBankData;
      private readonly IRepository<OtherContractDateRequest> _OtherContractDateRequest;
      private readonly IRepository<PermissionInboundUpdate> _PermissionInboundUpdate;
      private readonly IRepository<PermissionInboundDocuments> _PermissionInboundDocuments;

      private readonly IRepository<PermissionInboundOthersRightsLink> _PermissionInboundOthersRightsLink;

      private readonly IRepository<CurrencyMaster> _CurrencyMaster;

      private readonly IRepository<AssetSubType> _AssetSubTypeService;
      private readonly IRepository<StatusMaster> _StatusMasterService;

      #endregion

      #region Ctor

      /// <summary>
      /// Ctor
      /// </summary>
      /// <param name="GeographyType">Geography</param>        
      public PermissionsInboundService(

            IDbContext dbContext
          , IRepository<OtherContractMaster> OtherContractMaster
          , IRepository<PermissionsOutboundMaster> PermissionsOutboundMaster
          , IRepository<PermissionsOutboundTypeOfRightsMaster> PermissionsOutboundTypeOfRightsMaster
          , IRepository<OtherContractImageBank> OtherContractImageBank
          , IRepository<OtherRightsMaster> OtherRightsMaster
          , IRepository<PermissionInbound> PermissionInbound
          , IRepository<CopyRightHolderMaster> CopyRightHolderMaster
          , IRepository<PermissionInboundCopyRightHolderMaster> PermissionInboundCopyRightHolderMaster
          , IRepository<PermissionInboundOthers> PermissionInboundOthers
          , IRepository<PermissionInboundSearchHistory> PermissionInboundSearchHistory
          , IRepository<PermissionInboundImageVideoBank> PermissionInboundImageVideoBank
          , IRepository<PermissionInboundImageVideoBankData> PermissionInboundImageVideoBankData
          , IRepository<OtherContractDateRequest> OtherContractDateRequest
          , IRepository<PermissionInboundUpdate> PermissionInboundUpdate
          , IRepository<PermissionInboundDocuments> PermissionInboundDocuments
          , IRepository<PermissionInboundOthersRightsLink> PermissionInboundOthersRightsLink
          , IRepository<CurrencyMaster> CurrencyMaster

          , IRepository<AssetSubType> AssetSubType
          , IRepository<StatusMaster> StatusMaster
          )
      {

          this._dbContext = dbContext;
          this._OtherContractMaster = OtherContractMaster;
          this._PermissionsOutboundMaster = PermissionsOutboundMaster;
          this._PermissionsOutboundTypeOfRightsMaster = PermissionsOutboundTypeOfRightsMaster;
          this._OtherContractImageBank = OtherContractImageBank;
          this._OtherRightsMaster = OtherRightsMaster;
          this._PermissionInbound = PermissionInbound;
          this._CopyRightHolderMaster = CopyRightHolderMaster;
          this._PermissionInboundCopyRightHolderMaster = PermissionInboundCopyRightHolderMaster;
          this._PermissionInboundOthers = PermissionInboundOthers;
          this._PermissionInboundSearchHistory = PermissionInboundSearchHistory;
          this._PermissionInboundImageVideoBank = PermissionInboundImageVideoBank;
          this._PermissionInboundImageVideoBankData = PermissionInboundImageVideoBankData;
          this._OtherContractDateRequest = OtherContractDateRequest;
          this._PermissionInboundUpdate = PermissionInboundUpdate;
          this._PermissionInboundDocuments = PermissionInboundDocuments;
          this._PermissionInboundOthersRightsLink = PermissionInboundOthersRightsLink;
          this._CurrencyMaster = CurrencyMaster;

          this._AssetSubTypeService = AssetSubType;
          this._StatusMasterService = StatusMaster;
      }



      #endregion




      public virtual IList<OtherContractMaster> GetOtherContractList()
      {
          return _OtherContractMaster.Table.Where(a => a.Deactivate == "N").OrderBy(c => c.partyname).ToList();

      }
      /****************************************************************************
       Created By :  Dheeraj Kumar Sharma
       Created For : Getting the image/video bank value based on other contract id party details
       *****************************************************************************************/
      public OtherContractImageBank GeOtherContractImageBankDetails(int otherContractId)
      {
         return  _OtherContractImageBank.Table.Where(i => i.Deactivate == "N" && i.othercontractid == otherContractId).FirstOrDefault();
      }

      /****************************************************************************
      Created By :  Dheeraj Kumar Sharma
      Created For : Getting the other rights master used for others inbound process
      *****************************************************************************************/
      public IList<OtherRightsMaster> GeOtherRightsMaster()
      {
         return  _OtherRightsMaster.Table.Where(i => i.Deactivate == "N").ToList();
      }
     
      /****************************************************************************
     Created By :  Dheeraj Kumar Sharma
     Created For : Service to insert data into inbound process
     *****************************************************************************************/
      public void InsertPermissionInbound(PermissionInbound _InboundTable)
      {
           _PermissionInbound.Insert(_InboundTable);
      }
      /****************************************************************************
    Created By :  Dheeraj Kumar Sharma
    Created For : Service to insert data into inbound process
    *****************************************************************************************/
      public void UpdatePermissionInbound(PermissionInbound _InboundTable)
      {
          _PermissionInbound.Update(_InboundTable);
      }

     /****************************************************************************
     Created By :  Dheeraj Kumar Sharma
     Created For : Service to insert data into inbound process
     *****************************************************************************************/
      public CopyRightHolderMaster getCopyRightHolderById(int id)
      {
          return _CopyRightHolderMaster.Table.Where(i => i.Id == id).FirstOrDefault();
      }



      /****************************************************************************
     Created By :  Saddam
     Created For : Service for display PermissionInboundImageVideoBankLink Details 
     *****************************************************************************************/
      public PermissionInboundImageVideoBank getPermissionInboundImageVideoBankById(int id)
      {
          return _PermissionInboundImageVideoBank.Table.Where(i => i.PermissionInboundId == id && i.Deactivate == "N").FirstOrDefault();
      }



      /****************************************************************************
     Created By :  Saddam
     Created For : Service for display PermissionInboundUpdate Details 
     *****************************************************************************************/
      public PermissionInboundUpdate getPermissionInboundUpdateById(int id)
      {
          return _PermissionInboundUpdate.Table.Where(i => i.PermissionInboundId == id && i.Deactivate == "N").FirstOrDefault();
      }
      

      
      /****************************************************************************
     Created By :  Saddam
     Created For : Service for display PermissionInboundUpdate Details 
     *****************************************************************************************/
      //public PermissionInboundDocuments getPermissionInboundDocumentsById(int id)
      //{
      //    return _PermissionInboundDocuments.Table.Where(i => i.Id == id && i.Deactivate == "N").FirstOrDefault();
      //}
      

      /****************************************************************************
   Created By :  Saddam
   Created For : Service for display getPermissionInboundCopyRightHolderById Details 
   *****************************************************************************************/
      public IList<PermissionInboundCopyRightHolderMaster> getPermissionInboundCopyRightHolderById(int id)
      {
          return _PermissionInboundCopyRightHolderMaster.Table.Where(i => i.InboundOthersId == id && i.Deactivate == "N").ToList();
      }




      //public void DeavtivatePermissionsInboundDocumentById(int id, int enteredBy)
      //{
      //    IList<PermissionInboundDocuments> Linking = _PermissionInboundDocuments.Table.Where(i => i.Id == id && i.Deactivate == "N").ToList();
      //    foreach (var lst in Linking)
      //    {
      //        lst.Deactivate = "Y";
      //        lst.DeactivateBy = enteredBy;
      //        lst.DeactivateDate = DateTime.Now;
      //        _PermissionInboundDocuments.Update(lst);
      //    }

      //}


      /****************************************************************************
Created By :  Saddam
Created For : Service for display PermissionInboundOthers Details 
*****************************************************************************************/
      public PermissionInboundOthers getInboundDocumentsById(int id)
      {
          return _PermissionInboundOthers.Table.Where(i => i.PermissionInboundId == id && i.Deactivate == "N").FirstOrDefault();
      }

      
      

      /****************************************************************************
     Created By :  Saddam
     Created For : Service for display OtherContractImageBank Details 
     *****************************************************************************************/
      public OtherContractImageBank OtherContractImageBankById(int id)
      {
          return _OtherContractImageBank.Table.Where(i => i.Id == id && i.Deactivate == "N").FirstOrDefault();
      }


      /****************************************************************************
    Created By :  Saddam
    Created For : Service for display OtherContractMaster Details 
    *****************************************************************************************/
      public OtherContractMaster OtherContractMasterById(int id)
      {
          return _OtherContractMaster.Table.Where(i => i.Id == id && i.Deactivate == "N").FirstOrDefault();
      }
      

      /****************************************************************************
   Created By :  Saddam
   Created For : Service for display PermissionInboundImageVideoBankData Details 
   *****************************************************************************************/
      public IList<PermissionInboundImageVideoBankData> getPermissionInboundImageVideoBankDataById(int id)
      {
          return _PermissionInboundImageVideoBankData.Table.Where(i => i.IVBId == id && i.Deactivate =="N").ToList();
      }


      /****************************************************************************
Created By :  Saddam
Created For : Service for display OtherContractDateRequest Details 
*****************************************************************************************/
      public IList<OtherContractDateRequest> getOtherContractDateRequestById(int id)
      {
          return _OtherContractDateRequest.Table.Where(i => i.PIOID == id && i.Deactivate == "N").ToList();
      }


         /****************************************************************************
Created By :  Saddam
Created For : Service for display OtherContractDateRequest Details 
*****************************************************************************************/
      public IList<PermissionInboundDocuments> getPermissionInboundDocumentsById(int id)
      {
          return _PermissionInboundDocuments.Table.Where(i => i.PermissionsInboundUpdateId == id && i.Deactivate == "N").ToList();
      }


      

      /****************************************************************************
Created By :  Saddam
Created For : Service for InsertPermissionsOutboundUpdate 
*****************************************************************************************/
      public int InsertPermissionInboundUpdate(PermissionInboundUpdate PermissionInboundUpdate)
      {


          PermissionInboundUpdate.Deactivate = "N";
          //  Author.EnteredBy = 10;
          PermissionInboundUpdate.EntryDate = DateTime.Now;
          PermissionInboundUpdate.ModifiedBy = null;
          PermissionInboundUpdate.ModifiedDate = null;
          PermissionInboundUpdate.DeactivateBy = null;
          PermissionInboundUpdate.DeactivateDate = null;

          _PermissionInboundUpdate.Insert(PermissionInboundUpdate);
          return PermissionInboundUpdate.Id;
      }


      /****************************************************************************
Created By :  Saddam
Created For : Service for InsertPermissionInboundDocuments 
*****************************************************************************************/
      public void InsertPermissionInboundDocuments(PermissionInboundDocuments PermissionInboundDocuments)
      {
          PermissionInboundDocuments.Deactivate = "N";
          PermissionInboundDocuments.EntryDate = DateTime.Now;
          PermissionInboundDocuments.ModifiedBy = null;
          PermissionInboundDocuments.ModifiedDate = null;
          PermissionInboundDocuments.DeactivateBy = null;
          PermissionInboundDocuments.DeactivateDate = null;
          _PermissionInboundDocuments.Insert(PermissionInboundDocuments);
      }

      /****************************************************************************
            Created By :  Dheeraj Kumar Sharma
            Created For : function is usaed for inserting copyright data into the dump table of copyright holder
        *****************************************************************************************/
      public void insertintoPermissionInboundCopyRightHolderMasterDump(PermissionInboundCopyRightHolderMaster Obj)
      {
          _PermissionInboundCopyRightHolderMaster.Insert(Obj);
      }
      /****************************************************************************
            Created By :  Dheeraj Kumar Sharma
            Created For : func tion for getting the inbiund permissio n details based on primary id
        *****************************************************************************************/
      public PermissionInbound getInboundPermissionDetailsById(int id)
      {
          return _PermissionInbound.Table.Where(i => i.Deactivate == "N" && i.Id == id).FirstOrDefault();
      }


       /****************************************************************************
            Created By :  Dheeraj Kumar Sharma
            Created For : func tion for getting the inbiund permissio n details based on primary id
        *****************************************************************************************/
      public IList<PermissionInbound> getInboundPermissionDetailsByCode(string  Code)
      {
          if (Code != null && Code != "")
          {

              return _PermissionInbound.Table.Where(i => i.Deactivate == "N" && i.Code == Code).ToList();
          }
          else 
          {
          return null;
          }
          
      }


      

       /****************************************************************************
            Created By :  Dheeraj Kumar Sharma
            Created For : func tion for getting the inbiund permissio by CopyRight Holder Master n details based on primary id
        *****************************************************************************************/
      public PermissionInboundOthers getPermissionInboundOthersDetailsCopyRightHolderById(int id)
      {
          return _PermissionInboundOthers.Table.Where(i => i.Deactivate == "N" && i.Id == id).FirstOrDefault();
      }

      


      /****************************************************************************
            Created By :  Dheeraj Kumar Sharma
            Created For : function for getting copyright which Pemission inbound is not created
        *****************************************************************************************/
      public IList<CopyRightHolderMaster> getCopyRightHolderNotUsed(string cpyIds)
      {
          if (cpyIds == null || cpyIds=="")
          {
              return _CopyRightHolderMaster.Table.Where(i => i.Deactivate == "N").ToList();
          }
          else
          {
              return _CopyRightHolderMaster.Table.Where(i => i.Deactivate == "N" && !cpyIds.Contains(i.CopyRightHolderCode.ToString())).ToList();
          }
         
      }
      /****************************************************************************
            Created By :  Dheeraj Kumar Sharma
            Created For : Insert into PermissionInboundOthers
        *****************************************************************************************/
      public void insertPermissionInboundOthers(PermissionInboundOthers obj)
      {
          _PermissionInboundOthers.Insert(obj);
      }
      /****************************************************************************
            Created By :  Dheeraj Kumar Sharma
            Created For : Insert into PermissionInboundOthers
        *****************************************************************************************/
      public void PermissionInboundSearchHistory(PermissionInboundSearchHistory obj)
      {
          _PermissionInboundSearchHistory.Insert(obj);
      }


      /****************************************************************************
Created By :  Saddam
Created For : Service for display PermissionInboundOthers Details 
*****************************************************************************************/
      public PermissionInboundOthers getPermissionInboundOtherById(int id)
      {
          return _PermissionInboundOthers.Table.Where(i => i.PermissionInboundId == id && i.Deactivate == "N").FirstOrDefault();
      }

        /****************************************************************************
Created By :  Saddam
Created For : Service for display PermissionInboundOthers Details 
*****************************************************************************************/
      public PermissionInboundDocuments getInboundDocumentsDocumentsById(int id)
      {
          return _PermissionInboundDocuments.Table.Where(i => i.Id == id && i.Deactivate == "N").FirstOrDefault();
      }

      /****************************************************************************
Created By :  Saddam
Created For : Service for Deactivate PermissionInboundDocuments By Id Details 
*****************************************************************************************/
      public void DeavtivatePermissionsInboundDocumentById(int id, int enteredBy)
      {
          IList<PermissionInboundDocuments> Linking = _PermissionInboundDocuments.Table.Where(i => i.Id == id && i.Deactivate == "N").ToList();
          foreach (var lst in Linking)
          {
              lst.Deactivate = "Y";
              lst.DeactivateBy = enteredBy;
              lst.DeactivateDate = DateTime.Now;
              _PermissionInboundDocuments.Update(lst);
          }

      }


      /****************************************************************************
Created By :  Saddam
Created For : Service for display PermissionInboundImageVideoBankData  Details 
*****************************************************************************************/
      public PermissionInboundImageVideoBankData getPermissionInboundImageVideoBankDetialById(int id)
      {
          return _PermissionInboundImageVideoBankData.Table.Where(i => i.Id == id && i.Deactivate == "N").FirstOrDefault();
      }


      /****************************************************************************
Created By :  Saddam
Created For : Service for display getPermissionInboundById  Details 
*****************************************************************************************/
      public PermissionInbound getPermissionInboundById(int id)
      {
          return _PermissionInbound.Table.Where(i => i.Id == id && i.Deactivate == "N").FirstOrDefault();
      }
      

      
      /****************************************************************************
Created By :  Saddam
Created For : Service for display getPermissionInboundById  Details 
*****************************************************************************************/
      public PermissionInboundCopyRightHolderMaster getPermissionInboundCopyRightHolderMasterByInboundOthersId(int id)
      {
          return _PermissionInboundCopyRightHolderMaster.Table.Where(i => i.InboundOthersId == id && i.Deactivate == "N").FirstOrDefault();
      }
      

      /****************************************************************************
Created By :  Saddam
Created For : Service for Update PermissionInboundImageVideoBankData 
*****************************************************************************************/
      public void UpdatePermissionInboundImageVideoBankData(PermissionInboundImageVideoBankData PermissionInboundImageVideoBankData)
      {
          
          PermissionInboundImageVideoBankData.ModifiedDate = DateTime.Now;

          PermissionInboundImageVideoBankData.DeactivateBy = null;
          PermissionInboundImageVideoBankData.DeactivateDate = null;
          _PermissionInboundImageVideoBankData.Update(PermissionInboundImageVideoBankData);
      }


      /****************************************************************************
Created By :  Saddam
Created For : Service for Update UpdatePermissionInboundCopyRightHolderMaster 
*****************************************************************************************/
      public void UpdatePermissionInboundCopyRightHolderMaster(PermissionInboundCopyRightHolderMaster PermissionInboundCopyRightHolderMaster)
      {

          PermissionInboundCopyRightHolderMaster.ModifiedDate = DateTime.Now;

          PermissionInboundCopyRightHolderMaster.DeactivateBy = null;
          PermissionInboundCopyRightHolderMaster.DeactivateDate = null;
          _PermissionInboundCopyRightHolderMaster.Update(PermissionInboundCopyRightHolderMaster);
      }


      /****************************************************************************
Created By :  Saddam
Created For : Service for Update UpdatePermissionInboundUpdate 
*****************************************************************************************/
      public void UpdatePermissionInboundUpdate(PermissionInboundUpdate PermissionInboundUpdate)
      {

          PermissionInboundUpdate.ModifiedDate = DateTime.Now;

          PermissionInboundUpdate.DeactivateBy = null;
          PermissionInboundUpdate.DeactivateDate = null;
          _PermissionInboundUpdate.Update(PermissionInboundUpdate);
      }

      

      /****************************************************************************
Created By :  Saddam
Created For : Service for Update UpdatePermissionInboundOthers 
*****************************************************************************************/
      public void UpdatePermissionInboundOthers(PermissionInboundOthers PermissionInboundOthers)
      {

          PermissionInboundOthers.ModifiedDate = DateTime.Now;

          PermissionInboundOthers.DeactivateBy = null;
          PermissionInboundOthers.DeactivateDate = null;
          _PermissionInboundOthers.Update(PermissionInboundOthers);
      }

      

      
      /****************************************************************************
Created By :  Saddam
Created For : Service for Update UpdatePermissionInbound 
*****************************************************************************************/
      public void UpdatePermissionInboundData(PermissionInbound PermissionInbound)
      {

          PermissionInbound.ModifiedDate = DateTime.Now;

          PermissionInbound.DeactivateBy = null;
          PermissionInbound.DeactivateDate = null;
          _PermissionInbound.Update(PermissionInbound);
      }

      



      

         /****************************************************************************
Created By :  Saddam
Created For : Service for Update PermissionInboundImageVideoBankData 
*****************************************************************************************/
      public void InsertPermissionInboundImageVideoBankData(PermissionInboundImageVideoBankData PermissionInboundImageVideoBankData)
      {

          PermissionInboundImageVideoBankData.Deactivate = "N";
          PermissionInboundImageVideoBankData.EntryDate = DateTime.Now;
          PermissionInboundImageVideoBankData.ModifiedBy = null;
          PermissionInboundImageVideoBankData.ModifiedDate = null;
          PermissionInboundImageVideoBankData.DeactivateBy = null;
          PermissionInboundImageVideoBankData.DeactivateDate = null;

          _PermissionInboundImageVideoBankData.Insert(PermissionInboundImageVideoBankData);
      }




      /****************************************************************************
Created By :  Saddam
Created For : Service for Update InsertOtherContractDateRequest 
*****************************************************************************************/
      public void InsertOtherContractDateRequest(OtherContractDateRequest OtherContractDateRequest)
      {

          OtherContractDateRequest.Deactivate = "N";
          OtherContractDateRequest.EntryDate = DateTime.Now;
          OtherContractDateRequest.ModifiedBy = null;
          OtherContractDateRequest.ModifiedDate = null;
          OtherContractDateRequest.DeactivateBy = null;
          OtherContractDateRequest.DeactivateDate = null;

          _OtherContractDateRequest.Insert(OtherContractDateRequest);
      }

      /****************************************************************************
Created By :  Saddam
Created For : Service for Insert PermissionInboundOthersRightsLink 
*****************************************************************************************/
      public void InsertPermissionInboundOthersRightsLink(PermissionInboundOthersRightsLink PermissionInboundOthersRightsLink)
      {

          PermissionInboundOthersRightsLink.Deactivate = "N";
          PermissionInboundOthersRightsLink.EntryDate = DateTime.Now;
          PermissionInboundOthersRightsLink.ModifiedBy = null;
          PermissionInboundOthersRightsLink.ModifiedDate = null;
          PermissionInboundOthersRightsLink.DeactivateBy = null;
          PermissionInboundOthersRightsLink.DeactivateDate = null;

          _PermissionInboundOthersRightsLink.Insert(PermissionInboundOthersRightsLink);
      }



      /****************************************************************************
Created By :  Saddam
Created For : Service for Insert InsertPermissionInboundCopyRightHolderMaster 
*****************************************************************************************/
      public void InsertPermissionInboundCopyRightHolderMaster(PermissionInboundCopyRightHolderMaster PermissionInboundCopyRightHolderMaster)
      {

          PermissionInboundCopyRightHolderMaster.Deactivate = "N";
          PermissionInboundCopyRightHolderMaster.EntryDate = DateTime.Now;
          PermissionInboundCopyRightHolderMaster.ModifiedBy = null;
          PermissionInboundCopyRightHolderMaster.ModifiedDate = null;
          PermissionInboundCopyRightHolderMaster.DeactivateBy = null;
          PermissionInboundCopyRightHolderMaster.DeactivateDate = null;

          _PermissionInboundCopyRightHolderMaster.Insert(PermissionInboundCopyRightHolderMaster);
      }
      
      
      /****************************************************************************
Created By :  Saddam
Created For : Service for Update PermissionInboundImageVideoBank 
*****************************************************************************************/
      public void UpdatePermissionInboundImageVideoBank(PermissionInboundImageVideoBank PermissionInboundImageVideoBank)
      {

          PermissionInboundImageVideoBank.ModifiedDate = DateTime.Now;

          PermissionInboundImageVideoBank.DeactivateBy = null;
          PermissionInboundImageVideoBank.DeactivateDate = null;
          _PermissionInboundImageVideoBank.Update(PermissionInboundImageVideoBank);
      }

      /****************************************************************************
Created By :  Saddam
Created For : Service for Update PermissionInboundImageVideoBank 
*****************************************************************************************/

      public IList<OtherContractDateRequest> getOtherContractDateRequest(int id)
      {
          return _OtherContractDateRequest.Table.Where(i => i.PIOID == id && i.Deactivate == "N").ToList();
      }

      public void DeavtivateOtherContractDateRequest(int id, int enteredBy)
      {
          IList<OtherContractDateRequest> Linking = getOtherContractDateRequest(id);
          foreach (var lst in Linking)
          {
              lst.Deactivate = "Y";
              lst.DeactivateBy = enteredBy;
              lst.DeactivateDate = DateTime.Now;
              _OtherContractDateRequest.Update(lst);
             // _OtherContractDateRequest.Delete(lst);
              
          }

      }


      
      /****************************************************************************
Created By :  Saddam
Created For : Service for Update PermissionInboundImageVideoBank 
*****************************************************************************************/

      public IList<PermissionInboundOthersRightsLink> getPermissionInboundOthersRightsLink(int id)
      {
          return _PermissionInboundOthersRightsLink.Table.Where(i => i.PIOID == id && i.Deactivate == "N").ToList();
      }

      public void DeavtivatePermissionInboundOthersRightsLink(int id, int enteredBy)
      {
          IList<PermissionInboundOthersRightsLink> Linking = getPermissionInboundOthersRightsLink(id);
          foreach (var lst in Linking)
          {
              lst.Deactivate = "Y";
              lst.DeactivateBy = enteredBy;
              lst.DeactivateDate = DateTime.Now;
              _PermissionInboundOthersRightsLink.Update(lst);
              //_PermissionInboundOthersRightsLink.Delete(lst);
          }

      }

      public int InsertPermissionInboundOthers(PermissionInboundOthers PermissionInboundOthers)
        {


            PermissionInboundOthers.Deactivate = "N";
            PermissionInboundOthers.EntryDate = DateTime.Now;
            PermissionInboundOthers.ModifiedBy = null;
            PermissionInboundOthers.ModifiedDate = null;
            PermissionInboundOthers.DeactivateBy = null;
            PermissionInboundOthers.DeactivateDate = null;

            _PermissionInboundOthers.Insert(PermissionInboundOthers);
            return PermissionInboundOthers.Id;
        }

      public int InsertPermissionInboundImageVideoBank(PermissionInboundImageVideoBank PermissionInboundImageVideoBank)
        {


            PermissionInboundImageVideoBank.Deactivate = "N";
            PermissionInboundImageVideoBank.EntryDate = DateTime.Now;
            PermissionInboundImageVideoBank.ModifiedBy = null;
            PermissionInboundImageVideoBank.ModifiedDate = null;
            PermissionInboundImageVideoBank.DeactivateBy = null;
            PermissionInboundImageVideoBank.DeactivateDate = null;

            _PermissionInboundImageVideoBank.Insert(PermissionInboundImageVideoBank);
            return PermissionInboundImageVideoBank.Id;
        }


      public int GetisValidPartyName(string PartyName)
      {
          var query = _OtherContractMaster.Table.Where(i => i.Deactivate == "N" && i.partyname == PartyName).FirstOrDefault();
          if (query != null)
          {
              var queryValue = _OtherContractImageBank.Table.Where(a => a.Deactivate == "N" && a.othercontractid == query.Id).FirstOrDefault();

              if (queryValue != null)
              {
                  return queryValue.Id;
              }
              else
              {
                  return 0;
              }
           
          }
          else
          {
              return 0;
          }
      }

      public int GetisValidCurrency(string Currency)
      {
          var query = _CurrencyMaster.Table.Where(i => i.Deactivate == "N" && i.CurrencyName == Currency).FirstOrDefault();
          if (query != null)
          {
              return query.Id;
          }
          else
          {
              return 0;
          }
      }


      

      public int InsertPermissionInboundData(PermissionInbound _InboundTable)
      {
          _PermissionInbound.Insert(_InboundTable);
          return _InboundTable.Id;
      }

      //public void InsertPermissionInboundImageVideoBankLink(PermissionInboundImageVideoBank _InboundImageVideoBank)
      //{

      //    _InboundImageVideoBank.Deactivate = "N";
      //    _InboundImageVideoBank.EntryDate = DateTime.Now;
      //    _InboundImageVideoBank.ModifiedBy = null;
      //    _InboundImageVideoBank.ModifiedDate = null;
      //    _InboundImageVideoBank.DeactivateBy = null;
      //    _InboundImageVideoBank.DeactivateDate = null;

      //    _PermissionInboundImageVideoBank.Insert(_InboundImageVideoBank);
      //}

      public int InsertPermissionInboundImageVideoBankLink(PermissionInboundImageVideoBank _InboundImageVideoBank)
      {

          _InboundImageVideoBank.Deactivate = "N";
          _InboundImageVideoBank.EntryDate = DateTime.Now;
          _InboundImageVideoBank.ModifiedBy = null;
          _InboundImageVideoBank.ModifiedDate = null;
          _InboundImageVideoBank.DeactivateBy = null;
          _InboundImageVideoBank.DeactivateDate = null;

          _PermissionInboundImageVideoBank.Insert(_InboundImageVideoBank);
          return _InboundImageVideoBank.Id;
      }



      //public PermissionInboundImageVideoBank getNewPermissionInboundImageVideoBankById(int id, int PartyId)
      //{
      //    return _PermissionInboundImageVideoBank.Table.Where(i => i.PermissionInboundId == id && i.ImageBankId == PartyId && i.Deactivate == "N").FirstOrDefault();
      //}



      public void InsertNewPermissionInboundImageVideoBankData(PermissionInboundImageVideoBankData PermissionInboundImageVideoBankData)
      {

          PermissionInboundImageVideoBankData.Deactivate = "N";
          PermissionInboundImageVideoBankData.EntryDate = DateTime.Now;
          PermissionInboundImageVideoBankData.ModifiedBy = null;
          PermissionInboundImageVideoBankData.ModifiedDate = null;
          PermissionInboundImageVideoBankData.DeactivateBy = null;
          PermissionInboundImageVideoBankData.DeactivateDate = null;

          _PermissionInboundImageVideoBankData.Insert(PermissionInboundImageVideoBankData);
      }

      public PermissionInboundCopyRightHolderMaster getPermissionInboundCopyRightHolderMasterById(int id)
      {
          return _PermissionInboundCopyRightHolderMaster.Table.Where(i => i.Id == id && i.Deactivate == "N").FirstOrDefault();
      }

      public void DeleteInboundOthersRightsLink(PermissionInboundOthersRightsLink PermissionInboundOthersRightsLink)
      {
          _PermissionInboundOthersRightsLink.Delete(PermissionInboundOthersRightsLink);
      }



      public IList<PermissionInboundOthers> getPermissionInboundOthersList(string code)
      {
          if (code != null || code != "")
          {
              return _PermissionInboundOthers.Table.Where(i => i.Deactivate == "N" && !code.Contains(i.PermissionInbound.ToString())).ToList();
             
          }
          else
          {
              return null;
          }

      }

      public IList<PermissionInbound> getInboundPermissionDetailsByProductId(int productId)
      {
          return _PermissionInbound.Table.Where(p => p.ProductId == productId && p.Deactivate == "N").ToList();
      }
      
      public void DeletePermissionInbound(PermissionInbound _InboundTable)
      {
          _PermissionInbound.Update(_InboundTable);
      }

      #region "Asset Sub-Type"

      public AssetSubType GetAssetSubTypeById(int Id)
      {
          return _AssetSubTypeService.Table.Where(i => i.Id == Id).FirstOrDefault();
      }

      public string DuplicityCheckAssetSubType(AssetSubType _AssetSubType)
      {

          var dupes = _AssetSubTypeService.Table.Where(x => x.AssetName == _AssetSubType.AssetName
                                                          && x.Deactivate == "N"
                                                          && (_AssetSubType.Id != 0 ? x.Id : 0) != (_AssetSubType.Id != 0 ? _AssetSubType.Id : 1)).FirstOrDefault();
          if (dupes != null)
          {
              return "N";

          }
          else
          {
              return "Y";
          }
      }

      public void InsertAssetSubType(AssetSubType _AssetSubType)
      {
          _AssetSubType.Deactivate = "N";
          _AssetSubType.AssetName = _AssetSubType.AssetName.Trim();
          _AssetSubType.EntryDate = DateTime.Now;
          _AssetSubType.ModifiedBy = null;
          _AssetSubType.ModifiedDate = null;
          _AssetSubType.DeactivateBy = null;
          _AssetSubType.DeactivateDate = null;
          _AssetSubTypeService.Insert(_AssetSubType);
      }

      public void UpdateAssetSubType(AssetSubType _AssetSubType)
      {
          _AssetSubTypeService.Update(_AssetSubType);
      }
      
      #endregion

      #region "Asset Status"

      public StatusMaster GetStatusMasterById(int Id)
      {
          return _StatusMasterService.Table.Where(i => i.Id == Id).FirstOrDefault();
      }

      public string DuplicityCheckStatusMaster(StatusMaster _StatusMaster)
      {

          var dupes = _StatusMasterService.Table.Where(x => x.Status == _StatusMaster.Status
                                                          && x.Deactivate == "N"
                                                          && (_StatusMaster.Id != 0 ? x.Id : 0) != (_StatusMaster.Id != 0 ? _StatusMaster.Id : 1)).FirstOrDefault();
          if (dupes != null)
          {
              return "N";

          }
          else
          {
              return "Y";
          }
      }

      public void InsertStatusMaster(StatusMaster _StatusMaster)
      {
          _StatusMaster.Deactivate = "N";
          _StatusMaster.Status = _StatusMaster.Status.Trim();
          _StatusMaster.EntryDate = DateTime.Now;
          _StatusMaster.ModifiedBy = null;
          _StatusMaster.ModifiedDate = null;
          _StatusMaster.DeactivateBy = null;
          _StatusMaster.DeactivateDate = null;
          _StatusMasterService.Insert(_StatusMaster);
      }

      public void UpdateStatusMaster(StatusMaster _StatusMaster)
      {
          _StatusMasterService.Update(_StatusMaster);
      }

      #endregion

  }
}