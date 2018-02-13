using ACS.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Asset
{
    public partial class SocietyAssetImageService:ISocietyAssetImageService
    {
        private readonly IRepository<ACS.Core.Domain.Asset.SocietyAssetImage> _societyAssetImageRepository;
        public SocietyAssetImageService(
            IRepository<ACS.Core.Domain.Asset.SocietyAssetImage> societyAssetImageRepository
            )
        {
            _societyAssetImageRepository = societyAssetImageRepository;
        }

        /*''' <summary>
       ''' Assembly Name     :SocietyAssetImageService
       ''' Description	     :insert SocietyAssetImage Details
       ''' Function Name     :insertSocietyAssetImage
       ''' InputParameter    :SocietyAssetImage instance
       ''' OutPut parameter  :
       ''' Create Date	   :19.04.2016
       ''' Author Name	   :<a href="mukeemm@vrvirtual.com">Mohammad Mukeem</a> 
       ''' </summary>   
       ''' <remarks>
       '''****************************  Modification History  *************************************************************
       ''' Sr No:	Date		    Modified by	    Tracker                 Description
       '''*****************************************************************************************************************
        
       '''*****************************************************************************************************************
       '''</remarks>
       ''' */
        public void insertSocietyAssetImage(ACS.Core.Domain.Asset.SocietyAssetImage societyAssetImage)
        {
            _societyAssetImageRepository.Insert(societyAssetImage);

        }

        /*''' <summary>
         ''' Assembly Name     :SocietyAssetImageService
         ''' Description	   :update SocietyAssetImage
         ''' Function Name     :updateSocietyAssetImage
         ''' InputParameter    :SocietyAssetImage instance
         ''' OutPut parameter  :
         ''' Create Date	   :19.04.2016
         ''' Author Name	   :<a href="mukeemm@vrvirtual.com">Mohammad Mukeem</a> 
         ''' </summary>   
         ''' <remarks>
         '''****************************  Modification History  *************************************************************
         ''' Sr No:	Date		    Modified by	    Tracker                 Description
         '''*****************************************************************************************************************
        
         '''*****************************************************************************************************************
         '''</remarks>
         ''' */
        public void updateSocietyAssetImage(ACS.Core.Domain.Asset.SocietyAssetImage societyAssetImage)
        {
            _societyAssetImageRepository.Update(societyAssetImage);

        }

        /*''' <summary>
         ''' Assembly Name     :SocietyAssetImageService
         ''' Description	   :delete SocietyAssetImage
         ''' Function Name     :deleteSocietyAssetImage
         ''' InputParameter    :SocietyAssetImage instance
         ''' OutPut parameter  :
         ''' Create Date	   :19.04.2016
         ''' Author Name	   :<a href="mukeemm@vrvirtual.com">Mohammad Mukeem</a> 
         ''' </summary>   
         ''' <remarks>
         '''****************************  Modification History  *************************************************************
         ''' Sr No:	Date		    Modified by	    Tracker                 Description
         '''*****************************************************************************************************************
        
         '''*****************************************************************************************************************
         '''</remarks>
         ''' */
        public void deleteSocietyAssetImage(ACS.Core.Domain.Asset.SocietyAssetImage societyAssetImage)
        {
            _societyAssetImageRepository.Delete(societyAssetImage);

        }

        /*''' <summary>
        ''' Assembly Name     :SocietyAssetImageService
        ''' Description	      :Get SocietyAssetImage By Id
        ''' Function Name     :getSocietyAssetImageById
        ''' InputParameter    :societyAssetImageId
        ''' OutPut parameter  :
        ''' Create Date	   :19.04.2016
        ''' Author Name	   :<a href="mukeemm@vrvirtual.com">Mohammad Mukeem</a> 
        ''' </summary>   
        ''' <remarks>
        '''****************************  Modification History  *************************************************************
        ''' Sr No:	Date		    Modified by	    Tracker                 Description
        '''*****************************************************************************************************************
        
        '''*****************************************************************************************************************
        '''</remarks>
        ''' */

        public ACS.Core.Domain.Asset.SocietyAssetImage getSocietyAssetImageById(int societyAssetImageId)
        {
            if (societyAssetImageId == 0)
                return null;           

                return _societyAssetImageRepository.Table.Where(i => i.Id == societyAssetImageId).FirstOrDefault();
            
        }

        /*''' <summary>
        ''' Assembly Name     :SocietyAssetImageService
        ''' Description	      :Get SocietyAssetImage by SocietyAssetId
        ''' Function Name     :getSocietyAssetImageByAssetId
        ''' InputParameter    :societyAssetId
        ''' OutPut parameter  :
        ''' Create Date	   :19.04.2016
        ''' Author Name	   :<a href="mukeemm@vrvirtual.com">Mohammad Mukeem</a> 
        ''' </summary>   
        ''' <remarks>
        '''****************************  Modification History  *************************************************************
        ''' Sr No:	Date		    Modified by	    Tracker                 Description
        '''*****************************************************************************************************************
        
        '''*****************************************************************************************************************
        '''</remarks>
        ''' */

        public List<ACS.Core.Domain.Asset.SocietyAssetImage> getSocietyAssetImageByAssetId(int societyAssetId)
        {
            return _societyAssetImageRepository.Table.Where(i=>i.SocietyAssetId==societyAssetId).ToList();
            
        }
    }
}
