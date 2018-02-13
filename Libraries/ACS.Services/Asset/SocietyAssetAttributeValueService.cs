using ACS.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Asset
{
   public partial class SocietyAssetAttributeValueService:ISocietyAssetAttributeValueService
    {
       private readonly IRepository<ACS.Core.Domain.Asset.SocietyAssetAttributeValue> _societyAssetAttributeValueRepository;
       public SocietyAssetAttributeValueService(
            IRepository<ACS.Core.Domain.Asset.SocietyAssetAttributeValue> societyAssetAttributeValueRepository
            )
        {
            _societyAssetAttributeValueRepository = societyAssetAttributeValueRepository;
        }

       /*''' <summary>
       ''' Assembly Name     :SocietyAssetAttributeValueService
       ''' Description	     :insert SocietyAssetAttributeValue
       ''' Function Name     :insertSocietyAssetAttributeValue
       ''' InputParameter    :SocietyAssetAttributeValue instance
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
       public void insertSocietyAssetAttributeValue(ACS.Core.Domain.Asset.SocietyAssetAttributeValue societyAssetAttributeValue)
       {
           _societyAssetAttributeValueRepository.Insert(societyAssetAttributeValue);

       }

       /*''' <summary>
        ''' Assembly Name     :SocietyAssetAttributeValueService
        ''' Description	      :update SocietyAssetAttributeValue
        ''' Function Name     :updateSocietyAssetAttributeValue
        ''' InputParameter    :SocietyAssetAttributeValue instance
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
       public void updateSocietyAssetAttributeValue(ACS.Core.Domain.Asset.SocietyAssetAttributeValue societyAssetAttributeValue)
       {
           _societyAssetAttributeValueRepository.Update(societyAssetAttributeValue);

       }

       /*''' <summary>
        ''' Assembly Name     :SocietyAssetAttributeValueService
        ''' Description	      :delete SocietyAssetAttributeValue
        ''' Function Name     :deleteSocietyAssetAttributeValue
        ''' InputParameter    :SocietyAssetAttributeValue instance
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
       public void deleteSocietyAssetAttributeValue(ACS.Core.Domain.Asset.SocietyAssetAttributeValue societyAssetAttributeValue)
       {
           _societyAssetAttributeValueRepository.Delete(societyAssetAttributeValue);

       }

       /*''' <summary>
       ''' Assembly Name     :SocietyAssetAttributeValueService
       ''' Description	     :Get SocietyAssetAttributeValue By Id
       ''' Function Name     :getSocietyAssetAttributeValueById
       ''' InputParameter    :societyAssetAttributeValueId
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

       public ACS.Core.Domain.Asset.SocietyAssetAttributeValue getSocietyAssetAttributeValueById(int societyAssetAttributeValueId)
       {
           if (societyAssetAttributeValueId == 0)
               return null;         

               return _societyAssetAttributeValueRepository.Table.Where(i => i.Id == societyAssetAttributeValueId).FirstOrDefault();
         
       }

       /*''' <summary>
       ''' Assembly Name     :SocietyAssetAttributeValueService
       ''' Description	     :Get SocietyAssetAttributeValue List
       ''' Function Name     :getSocietyAssetAttributeValueList
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

       public List<ACS.Core.Domain.Asset.SocietyAssetAttributeValue> getAllSocietyAssetAttributeValues()
       {
           return _societyAssetAttributeValueRepository.Table.ToList();
          
       }


       /*''' <summary>
       ''' Assembly Name     :SocietyAssetAttributeValueService
       ''' Description	     :Get SocietyAssetAttributeValue By SocietyId
       ''' Function Name     :getAllSocietyAssetAttributeValuesBySocityId
       ''' InputParameter    :socityId
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

       public List<ACS.Core.Domain.Asset.SocietyAssetAttributeValue> getAllSocietyAssetAttributeValuesBySocityId(int socityId)
       {
           return _societyAssetAttributeValueRepository.Table.Where(i=>i.SocietyId==socityId).ToList();

       }

       /*''' <summary>
      ''' Assembly Name     :SocietyAssetAttributeValueService
      ''' Description	    :Get SocietyAssetAttributeValue By AssetId
      ''' Function Name     :getAllSocietyAssetAttributeValuesByAssetId
      ''' InputParameter    :assetId
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

       public List<ACS.Core.Domain.Asset.SocietyAssetAttributeValue> getAllSocietyAssetAttributeValuesByAssetId(int assetId)
       {
           return _societyAssetAttributeValueRepository.Table.Where(i => i.AssetId == assetId).ToList();

       }

       /*''' <summary>
      ''' Assembly Name     :SocietyAssetAttributeValueService
      ''' Description	    :Get SocietyAssetAttributeValue By GAAMId
      ''' Function Name     :getAllSocietyAssetAttributeValuesByGAAMId
      ''' InputParameter    :GAAMId
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

       public List<ACS.Core.Domain.Asset.SocietyAssetAttributeValue> getAllSocietyAssetAttributeValuesByGAAMId(int GAAMId)
       {
           return _societyAssetAttributeValueRepository.Table.Where(i => i.AssetId == GAAMId).ToList();

       }
    }
}
