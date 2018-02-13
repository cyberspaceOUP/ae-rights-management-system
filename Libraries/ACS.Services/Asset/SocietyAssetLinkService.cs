using ACS.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Asset
{
   public partial class SocietyAssetLinkService:ISocietyAssetLinkService
    {
       private readonly IRepository<ACS.Core.Domain.Asset.SocietyAssetLink> _societyAssetLinkRepository;

       public SocietyAssetLinkService(
            IRepository<ACS.Core.Domain.Asset.SocietyAssetLink> societyAssetLinkRepository
            )
        {
            _societyAssetLinkRepository = societyAssetLinkRepository;
        }

       /*''' <summary>
        ''' Assembly Name     :SocietyAssetLinkService
        ''' Description	      :Insert SocietyAssetLink Details
        ''' Function Name     :insertSocietyAssetLink
        ''' InputParameter    :SocietyAssetLink instance
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
       public void insertSocietyAssetLink(ACS.Core.Domain.Asset.SocietyAssetLink societyAssetLink)
       {
           _societyAssetLinkRepository.Insert(societyAssetLink);

       }

       /*''' <summary>
        ''' Assembly Name     :SocietyAssetLinkService
        ''' Description	      :update SocietyAssetLink
        ''' Function Name     :updateSocietyAssetLink
        ''' InputParameter    :SocietyAssetLink instance
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
       public void updateSocietyAssetLink(ACS.Core.Domain.Asset.SocietyAssetLink societyAssetLink)
       {
           _societyAssetLinkRepository.Update(societyAssetLink);

       }

       /*''' <summary>
        ''' Assembly Name     :SocietyAssetLinkService
        ''' Description	      :delete SocietyAssetLink
        ''' Function Name     :deleteSocietyAssetLink
        ''' InputParameter    :SocietyAssetLink instance
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
       public void deleteSocietyAssetLink(ACS.Core.Domain.Asset.SocietyAssetLink societyAssetLink)
       {
           _societyAssetLinkRepository.Delete(societyAssetLink);

       }


       /*''' <summary>
       ''' Assembly Name     :SocietyAssetLinkService
       ''' Description	     :Get All SocietyAssetLinks
       ''' Function Name     :getAllSocietyAssetLinks
       ''' InputParameter    :''
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

       public List<ACS.Core.Domain.Asset.SocietyAssetLink> getAllSocietyAssetLinks()
       {
           return _societyAssetLinkRepository.Table.ToList();

       }

       /*''' <summary>
       ''' Assembly Name     :SocietyAssetLinkService
       ''' Description	     :Get SocietyAssetLink By societyAssetLinkId
       ''' Function Name     :getSocietyAssetLinkById
       ''' InputParameter    :societyAssetLinkId
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

       public ACS.Core.Domain.Asset.SocietyAssetLink getSocietyAssetLinkById(int societyAssetLinkId)
       {
           if (societyAssetLinkId == 0)
               return null;           

               return _societyAssetLinkRepository.Table.Where(i => i.Id == societyAssetLinkId).FirstOrDefault();
          
       }



       /*''' <summary>
       ''' Assembly Name     :SocietyAssetLinkService
       ''' Description	     :Get SocietyAssetLink By societyId
       ''' Function Name     :getSocietyAssetLinksBySocityId
       ''' InputParameter    :societyId
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

       public List<Core.Domain.Asset.SocietyAssetLink> getSocietyAssetLinksBySocityId(int societyId)
       {
           if (societyId == 0)
               return null;

           return _societyAssetLinkRepository.Table.Where(i => i.SocietyId == societyId).ToList();

       }
    }
}
