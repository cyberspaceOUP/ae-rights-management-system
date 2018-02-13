using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Asset
{
    public partial interface ISocietyAssetImageService
    {
        /// <summary>
        /// Insert SocietyAssetImage details
        /// </summary>
        /// <param name="societyAssetImage">instance of SocietyAssetImage</param>
        /// <returns></returns>
        void insertSocietyAssetImage(ACS.Core.Domain.Asset.SocietyAssetImage societyAssetImage);

        /// <summary>
        /// Update SocietyAssetImage
        /// </summary>
        /// <param name="societyAssetImage">instance of SocietyAssetImage</param>
        /// <returns></returns>
        void updateSocietyAssetImage(ACS.Core.Domain.Asset.SocietyAssetImage societyAssetImage);

        /// <summary>
        /// Delete SocietyAssetImage
        /// </summary>
        /// <param name="societyAssetImage">instance of SocietyAssetImage</param>
        /// <returns></returns>
        void deleteSocietyAssetImage(ACS.Core.Domain.Asset.SocietyAssetImage societyAssetImage);

        
        /// <summary>
        /// Get SocietyAssetImage By societyAssetImageId
        /// </summary>
        /// <param name="societyAssetImageId"></param>
        /// <returns></returns>
        ACS.Core.Domain.Asset.SocietyAssetImage getSocietyAssetImageById(int societyAssetImageId);

        /// <summary>
        /// Get All SocietyAssetImage By socityAssetId
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        List<ACS.Core.Domain.Asset.SocietyAssetImage> getSocietyAssetImageByAssetId(int societyAssetId);
    }
}
