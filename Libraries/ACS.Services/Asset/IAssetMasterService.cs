using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Asset
{
   public partial interface IAssetMasterService
    {
        /// <summary>
        /// Insert Asset detail
        /// </summary>
        /// <param name="asset">instance of GlobalAssetMaster</param>
        /// <returns></returns>
       void insertAsset(ACS.Core.Domain.Asset.GlobalAssetMaster asset);

        /// <summary>
       /// Update Asset
        /// </summary>
       /// <param name="asset">instance of GlobalAssetMaster</param>
        /// <returns></returns>
       void updateAsset(ACS.Core.Domain.Asset.GlobalAssetMaster asset);

        /// <summary>
       /// Delete Asset
        /// </summary>
       /// <param name="asset">instance of GlobalAssetMaster</param>
        /// <returns></returns>
       void deleteAsset(ACS.Core.Domain.Asset.GlobalAssetMaster asset);

        /// <summary>
       /// Get All Asset and facility
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
       List<ACS.Core.Domain.Asset.GlobalAssetMaster> getAllGlobalAssets();
        /// <summary>
       /// Get Asset By AssetId
        /// </summary>
       /// <param name="id"></param>
        /// <returns></returns>
       ACS.Core.Domain.Asset.GlobalAssetMaster getGlobalAssetById(int id);

        /// <summary>
        /// Get Asset List
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
       List<ACS.Core.Domain.Asset.GlobalAssetMaster> getGlobalAssetList();
        /// <summary>
        /// Get Facility List
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
       List<ACS.Core.Domain.Asset.GlobalAssetMaster> getGlobalFacilityList();
    }
}
