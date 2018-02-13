using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Asset
{
   public partial interface IAssetAttributeService
    {
       /// <summary>
        /// Insert GlobalAssetAttribute detail
        /// </summary>
        /// <param name="globalAssetAttribute">instance of GlobalAssetAttributeMaster</param>
        /// <returns></returns>
       void insertGlobalAssetAttribute(ACS.Core.Domain.Asset.GlobalAssetAttributeMaster globalAssetAttribute);

        /// <summary>
       /// Update GloabalAssetAttribute
        /// </summary>
       /// <param name="globalAssetAttribute">instance of GlobalAssetAttributeMaster</param>
        /// <returns></returns>
       void updateGlobalAssetAttribute(ACS.Core.Domain.Asset.GlobalAssetAttributeMaster globalAssetAttribute);

        /// <summary>
       /// Delete GlobalAssetAttribute
        /// </summary>
       /// <param name="globalAssetAttribute">instance of GlobalAssetAttributeMaster</param>
        /// <returns></returns>
       void deleteGlobalAssetAttribute(ACS.Core.Domain.Asset.GlobalAssetAttributeMaster globalAssetAttribute);

        /// <summary>
       /// Get All GlobalAssetAttribute List
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
       List<ACS.Core.Domain.Asset.GlobalAssetAttributeMaster> getAllGlobalAssetAttribute();

        /// <summary>
       /// Get AssetAttribute By globalAssetAttributeId
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
       ACS.Core.Domain.Asset.GlobalAssetAttributeMaster getGlobalAssetAttributeById(int globalAssetAttributeId);
    }
    
}
