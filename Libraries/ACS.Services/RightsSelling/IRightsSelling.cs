//create by saddam 
//date : 27/07/2016

using ACS.Core.Domain.Master;
using ACS.Core.Domain.OtherContract;
using ACS.Core.Domain.RightsSelling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.RightsSelling
{
   

    public partial interface IRightsSelling
    {
        IList<LicenseeMaster> GetLicenseeMasterList();

        LicenseeMaster GetLicenseeById(LicenseeMaster LicenseeMaster);

        int InsertRightsSellingMaster(RightsSellingMaster RightsSellingMaster);

       

        int InsertRightsSellingUpdate(RightsSellingUpdate RightsSellingUpdate);

        int UpdateRightsSellingUpdate(RightsSellingUpdate RightsSellingUpdate);

        void InsertRightsSellingRoyalty(RightsSellingRoyalty RightsSellingRoyalty);

        IList<RightsSellingDocument> GetRightsSellingDocumentList(int id);

        IList<RightsSellingRoyalty> GetRightsSellingRoyaltyList(RightsSellingRoyalty Royalty);

        IList<RightsSellingRoyalty> GetRightsSellingRoyaltyList();

        void InsertRightsSellingDocument(RightsSellingDocument RightsSellingDocument);

        RightsSellingUpdate GetRightsSellingUpdateById(int id);

        void DeavtivateRightsSellingUpdateById(Int64 id);

        void InsertRightsSellingHistory(RightsSellingHistory RightsSellingHistory);

        void InsertRightsSellingPaymentTagging(RightsSellingPaymentTagging RightsSellingPaymentTagging);

        void UpdateRightsSellingMaster(RightsSellingMaster RightsSellingMaster);
        void DeleteRoyaltySlabLink(int? LinkingId, int enteredBy, string Type);

        RightsSellingMaster GetRightsSellingById(RightsSellingMaster RightsSelling);

        IList<ProductCategoryRightMaster> GetRightProductCategory();


        void InsertRightsSellingLanguageLink(RightsSellingLanguageMaster RightsSellingLanguageLink);

        void DeleteRightsSellingLanguageLink(int LinkingId, int enteredBy);

        //Added by Prakash
        IList<RightsSellingMaster> GetAllRightsSellingMasterList();

        void DeleteRightsSellingMaster(RightsSellingMaster RightsSellingMaster);

        RightsSellingPaymentTagging getRightsSellingPaymentTaggingById(int Id);

        void DeleteRightsSellingPaymentTagging(RightsSellingPaymentTagging RightsSellingPaymentTagging);

    }
}
