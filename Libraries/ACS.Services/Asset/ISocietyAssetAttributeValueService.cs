using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Asset
{
   public partial interface ISocietyAssetAttributeValueService
    {
        /// <summary>
        /// Insert SocietyAssetAttrubuteValue details
        /// </summary>
        /// <param name="societyAssetAttributeValue">instance of SocietyAssetAttributeValue</param>
        /// <returns></returns>
        void insertSocietyAssetAttributeValue(ACS.Core.Domain.Asset.SocietyAssetAttributeValue societyAssetAttributeValue);

        /// <summary>
        /// Update SocietyAssetAttributeValue
        /// </summary>
        /// <param name="societyAssetAttributeValue">instance of SocietyAssetAttributeValue</param>
        /// <returns></returns>
        void updateSocietyAssetAttributeValue(ACS.Core.Domain.Asset.SocietyAssetAttributeValue societyAssetAttributeValue);

        /// <summary>
        /// Delete SocietyAssetAttributeValue
        /// </summary>
        /// <param name="societyAssetAttributeValue">instance of SocietyAssetAttributeValue</param>
        /// <returns></returns>
        void deleteSocietyAssetAttributeValue(ACS.Core.Domain.Asset.SocietyAssetAttributeValue societyAssetAttributeValue);

        /// <summary>
        /// Get All SocietyAssetAttributeValues
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<ACS.Core.Domain.Asset.SocietyAssetAttributeValue> getAllSocietyAssetAttributeValues();
        /// <summary>
        /// Get SocietyAssetAttributeValue By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ACS.Core.Domain.Asset.SocietyAssetAttributeValue getSocietyAssetAttributeValueById(int id);

        /// <summary>
        /// Get SocietyAssetAttributeValues by SocityId
        /// </summary>
        /// <param name="societyId"></param>
        /// <returns></returns>
        List<ACS.Core.Domain.Asset.SocietyAssetAttributeValue> getAllSocietyAssetAttributeValuesBySocityId(int societyId);

        /// <summary>
        /// Get SocietyAssetAttributeValues By AssetId
        /// </summary>
        /// <param name="assetId"></param>
        /// <returns></returns>
        List<ACS.Core.Domain.Asset.SocietyAssetAttributeValue> getAllSocietyAssetAttributeValuesByAssetId(int assetId);

        /// <summary>
        /// Get SocietyAssetAttributeValues by GAAMId (Global Asset Attribute Master Id)
        /// </summary>
        /// <param name="GAAMId"></param>
        /// <returns></returns>
        List<ACS.Core.Domain.Asset.SocietyAssetAttributeValue> getAllSocietyAssetAttributeValuesByGAAMId(int GAAMId);
    }
}
