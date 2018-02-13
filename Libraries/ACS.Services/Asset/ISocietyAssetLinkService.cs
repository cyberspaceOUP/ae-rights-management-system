using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Asset
{
    public partial interface ISocietyAssetLinkService
    {
        /// <summary>
        /// Insert SocietyAssetLink details
        /// </summary>
        /// <param name="societyAssetLink">instance of SocietyAssetLink</param>
        /// <returns></returns>
        void insertSocietyAssetLink(ACS.Core.Domain.Asset.SocietyAssetLink societyAssetLink);

        /// <summary>
        /// Update SocietyAssetLink
        /// </summary>
        /// <param name="asset">instance of SocietyAssetLink</param>
        /// <returns></returns>
        void updateSocietyAssetLink(ACS.Core.Domain.Asset.SocietyAssetLink societyAssetLink);

        /// <summary>
        /// Delete SocietyAssetLink
        /// </summary>
        /// <param name="societyAssetLink">instance of SocietyAssetLink</param>
        /// <returns></returns>
        void deleteSocietyAssetLink(ACS.Core.Domain.Asset.SocietyAssetLink societyAssetLink);

        /// <summary>
        /// Get All SocietyAssetLink 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// 
        List<ACS.Core.Domain.Asset.SocietyAssetLink> getAllSocietyAssetLinks();
        /// <summary>
        /// Get SocietyAssetLink By societyAssetLinkId
        /// </summary>
        /// <param name="societyAssetLinkId"></param>
        /// <returns></returns>
        ACS.Core.Domain.Asset.SocietyAssetLink getSocietyAssetLinkById(int societyAssetLinkId);

        /// <summary>
        /// Get SocietyAssetLink By societyId
        /// </summary>
        /// <param name="societyId"></param>
        /// <returns></returns>
        List<Core.Domain.Asset.SocietyAssetLink> getSocietyAssetLinksBySocityId(int societyId);
    }
}
