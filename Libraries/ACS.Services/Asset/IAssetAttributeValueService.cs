using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Asset
{
   public partial interface IAssetAttributeValueService
    {
        /// <summary>
        /// Insert GlobalAssetAttributeValue detail
        /// </summary>
        /// <param name="globalAssetAttributeValue">instance of GlobalAssetAttributeValue</param>
        /// <returns></returns>
        /// 
        void insertGlobalAssetAttributeValue(ACS.Core.Domain.Asset.GlobalAssetAttributeValue globalAssetAttributeValue);

        /// <summary>
        /// Update GlobalAssetAttributeValue
        /// </summary>
        /// <param name="globalAssetAttributeValue">instance of GlobalAssetMaster</param>
        /// <returns></returns>
        /// 
        void updateGlobalAssetAttributeValue(ACS.Core.Domain.Asset.GlobalAssetAttributeValue globalAssetAttributeValue);

        /// <summary>
        /// Delete GlobalAssetAttributeValue
        /// </summary>
        /// <param name="globalAssetAttributeValue">instance of GlobalAssetMaster</param>
        /// <returns></returns>
        /// 
        void deleteGlobalAssetAttributeValue(ACS.Core.Domain.Asset.GlobalAssetAttributeValue globalAssetAttributeValue);

        /// <summary>
        /// Get All GlobalAssetAttributeValue List
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// 
        List<ACS.Core.Domain.Asset.GlobalAssetAttributeValue> getAllGlobalAssetAttributeValues();
        /// <summary>
        /// Get GlobalAssetAttributeValue By globalAssetAttributeValueId
        /// </summary>
        /// <param name="globalAssetAttributeValueId"></param>
        /// <returns></returns>
        /// 
        ACS.Core.Domain.Asset.GlobalAssetAttributeValue getGlobalAssetAttributeValueById(int globalAssetAttributeValueId);
        
    }
}
