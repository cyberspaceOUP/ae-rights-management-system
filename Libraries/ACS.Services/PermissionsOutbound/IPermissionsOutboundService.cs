//create by saddam 
//date : 27/07/2016

using ACS.Core.Domain.Master;
using ACS.Core.Domain.PermissionsOutbound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ACS.Services.PermissionsOutbound
{
    

    public partial interface IPermissionsOutboundService
    {
        IList<LicenseeMaster> GetLicenseeMasterList();

        LicenseeMaster GetLicenseeById(LicenseeMaster LicenseeMaster);

        int InsertPermissionsOutbound(PermissionsOutboundMaster PermissionsOutbound);
        void InsertPermissionsOutboundTypeOfRightsLinking(PermissionsOutboundTypeOfRightsMaster PermissionsOutboundTypeOfRights);

        PermissionsOutboundMaster GetPermissionsOutboundById(PermissionsOutboundMaster PermissionsOutbound);

       

        void UpdatePermissionsOutbound(PermissionsOutboundMaster PermissionsOutbound);

        void DeavtivatePermissionsOutboundTypeOfRights(int id, int enteredBy);

        IList<PermissionsOutboundTypeOfRightsMaster> getPermissionsOutboundTypeOfRights(int id);


        PermissionsOutboundUpdate GetPermissionsOutboundUpdateById(PermissionsOutboundUpdate PermissionsOutboundUpdate);

        int InsertPermissionsOutboundUpdate(PermissionsOutboundUpdate PermissionsOutboundUpdate);

        void InsertPermissionsOutboundDocument(PermissionsOutboundDocument PermissionsOutboundDocument);

        void DeavtivatePermissionsOutboundDocumentById(int id, int enteredBy);


        void InsertSearchHistory(PermissionsOutboundSearchHistory _PermissionsOutboundSearch);

        void UpdatePermissionsOutboundUpdate(PermissionsOutboundUpdate PermissionsOutboundUpdate);

        void InsertPermissionsOutboundPaymentTagging(PermissionsOutboundPaymentTagging PermissionsOutboundPaymentTagging);

        void InsertPermissionsOutboundLanguageLink(PermissionsOutboundLanguageMaster PermissionsOutboundLanguageLink);


        void DeletePermissionsOutboundLanguageLink(int LinkingId, int enteredBy);

        //Added by Prakash
        IList<PermissionsOutboundMaster> getAllPermissionsOutboundMasterList();

        void DeletePermissionsOutbound(PermissionsOutboundMaster PermissionsOutbound);

        PermissionsOutboundPaymentTagging getPermissionsOutboundPaymentTaggingById(int Id);

        void DeletePermissionsOutboundPaymentTagging(PermissionsOutboundPaymentTagging PermissionsOutboundPaymentTagging);

    }
}
