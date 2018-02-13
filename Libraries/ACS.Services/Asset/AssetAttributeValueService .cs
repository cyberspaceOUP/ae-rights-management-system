using ACS.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Asset
{
   public partial class AssetAttributeValueService:IAssetAttributeValueService
    {
       private readonly IRepository<ACS.Core.Domain.Asset.GlobalAssetAttributeValue> _globalAssetAttrValueRepository;

       public AssetAttributeValueService(
            IRepository<ACS.Core.Domain.Asset.GlobalAssetAttributeValue> globalAssetAttrValueRepository
            )
        {
            _globalAssetAttrValueRepository = globalAssetAttrValueRepository;
        }

       /*''' <summary>
        ''' Assembly Name     :AssetAttributeValueService
        ''' Description	      :Insert GlobalAssetAttributeValue
        ''' Function Name     :insertGlobalAssetAttributeValue
        ''' InputParameter    :GlobalAssetAttributeValue instance 
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
       public void insertGlobalAssetAttributeValue(ACS.Core.Domain.Asset.GlobalAssetAttributeValue globalAssetAttributeValue)
       {
           _globalAssetAttrValueRepository.Insert(globalAssetAttributeValue);

       }

       /*''' <summary>
         ''' Assembly Name     :AssetAttributeValueService
         ''' Description	   :Update AssetAttributeValue
         ''' Function Name     :updateGlobalAssetAttributeValue
         ''' InputParameter    :GlobalAssetAttributeValue instance 
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
       public void updateGlobalAssetAttributeValue(ACS.Core.Domain.Asset.GlobalAssetAttributeValue globalAssetAttributeValue)
       {
           _globalAssetAttrValueRepository.Update(globalAssetAttributeValue);

       }

       /*''' <summary>
        ''' Assembly Name     :AssetAttributeValueService
        ''' Description	      :Delete AssetAttributeValue
        ''' Function Name     :deleteGlobalAssetAttributeValue
        ''' InputParameter    :GlobalAssetAttributeValue instance 'assetAttrValue'
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
       public void deleteGlobalAssetAttributeValue(ACS.Core.Domain.Asset.GlobalAssetAttributeValue globalAssetAttributeValue)
       {
           _globalAssetAttrValueRepository.Delete(globalAssetAttributeValue);

       }

       /*''' <summary>
       ''' Assembly Name     :AssetAttributeValueService
       ''' Description	     :Get All GlobalAttributeValues
       ''' Function Name     :getAllGlobalAssetAttrValues
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
       public List<ACS.Core.Domain.Asset.GlobalAssetAttributeValue> getAllGlobalAssetAttributeValues()
       {
           return _globalAssetAttrValueRepository.Table.ToList();
           
       }

       /*''' <summary>
      ''' Assembly Name     :AssetAttributeValueService
      ''' Description	    :Get GlobalAssetAttributeValue by GAAVId
      ''' Function Name     :getGlobalAssetAttrValueById
      ''' InputParameter    :int GAAVId (GlobalAssetAttributeValueId)
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
       public ACS.Core.Domain.Asset.GlobalAssetAttributeValue getGlobalAssetAttributeValueById(int GAAVId)
        {
            if (GAAVId == 0)
                return null;         

                return _globalAssetAttrValueRepository.Table.Where(i => i.Id == GAAVId).FirstOrDefault();           

        }       
    }
}
