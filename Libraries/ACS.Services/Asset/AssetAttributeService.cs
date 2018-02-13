using ACS.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Asset
{
    public partial class AssetAttributeService:IAssetAttributeService
    {
        private readonly IRepository<ACS.Core.Domain.Asset.GlobalAssetAttributeMaster> _globalAssetAttributeRepository;

        public AssetAttributeService(
            IRepository<ACS.Core.Domain.Asset.GlobalAssetAttributeMaster> globalAssetAttributeRepository
            )
        {
            _globalAssetAttributeRepository = globalAssetAttributeRepository;
        }


        /*''' <summary>
         ''' Assembly Name     :AssetAttributeService
         ''' Description	   :insert Global Asset Attribute
         ''' Function Name     :insertGlobalAssetAttribute
         ''' InputParameter    :GlobalAssetAttributeMaster instance
         ''' OutPut parameter  :
         ''' Create Date	   :18.04.2016
         ''' Author Name	   :<a href="mukeemm@vrvirtual.com">Mohammad Mukeem</a> 
         ''' </summary>   
         ''' <remarks>
         '''****************************  Modification History  *************************************************************
         ''' Sr No:	Date		    Modified by	    Tracker                 Description
         '''*****************************************************************************************************************
        
         '''*****************************************************************************************************************
         '''</remarks>
         ''' */
        public void insertGlobalAssetAttribute(ACS.Core.Domain.Asset.GlobalAssetAttributeMaster globalAssetAttribute)
        {
            _globalAssetAttributeRepository.Insert(globalAssetAttribute);

        }


        /*''' <summary>
         ''' Assembly Name     :AssetAttributeService
         ''' Description	   :update GlobalAssetAttribute
         ''' Function Name     :updateGlobalAssetAttribute
         ''' InputParameter    :GlobalAssetAttribute instance
         ''' OutPut parameter  :
         ''' Create Date	   :18.04.2016
         ''' Author Name	   :<a href="mukeemm@vrvirtual.com">Mohammad Mukeem</a> 
         ''' </summary>   
         ''' <remarks>
         '''****************************  Modification History  *************************************************************
         ''' Sr No:	Date		    Modified by	    Tracker                 Description
         '''*****************************************************************************************************************
        
         '''*****************************************************************************************************************
         '''</remarks>
         ''' */
        public void updateGlobalAssetAttribute(ACS.Core.Domain.Asset.GlobalAssetAttributeMaster globalAssetAttribute)
        {
            _globalAssetAttributeRepository.Update(globalAssetAttribute);

        }


        /*''' <summary>
         ''' Assembly Name     :AssetAttributeService
         ''' Description	   :delete GlobalAssetAttribute
         ''' Function Name     :deleteAssetAttribute
         ''' InputParameter    :GlobalAssetAttribute instance
         ''' OutPut parameter  :
         ''' Create Date	   :18.04.2016
         ''' Author Name	   :<a href="mukeemm@vrvirtual.com">Mohammad Mukeem</a> 
         ''' </summary>   
         ''' <remarks>
         '''****************************  Modification History  *************************************************************
         ''' Sr No:	Date		    Modified by	    Tracker                 Description
         '''*****************************************************************************************************************
        
         '''*****************************************************************************************************************
         '''</remarks>
         ''' */
        public void deleteGlobalAssetAttribute(ACS.Core.Domain.Asset.GlobalAssetAttributeMaster globalAssetAttribute)
        {
            _globalAssetAttributeRepository.Delete(globalAssetAttribute);

        }

        /*''' <summary>
        ''' Assembly Name     :AssetAttributeService
        ''' Description	      :Get All GlobalAssetAttribute
        ''' Function Name     :getAssetAttributeById
        ''' InputParameter    :int globalAssetAttributeId
        ''' OutPut parameter  :
        ''' Create Date	   :18.04.2016
        ''' Author Name	   :<a href="mukeemm@vrvirtual.com">Mohammad Mukeem</a> 
        ''' </summary>   
        ''' <remarks>
        '''****************************  Modification History  *************************************************************
        ''' Sr No:	Date		    Modified by	    Tracker                 Description
        '''*****************************************************************************************************************
        
        '''*****************************************************************************************************************
        '''</remarks>
        ''' */

        public ACS.Core.Domain.Asset.GlobalAssetAttributeMaster getGlobalAssetAttributeById(int globalAssetAttributeId)
        {
            if (globalAssetAttributeId == 0)
                return null;

            return _globalAssetAttributeRepository.Table.Where(i => i.Id == globalAssetAttributeId).FirstOrDefault();          


        }

        /*''' <summary>
         ''' Assembly Name     :AssetAttributeService
         ''' Description	   :Get All GolobalAssetAttribute
         ''' Function Name     :getAllGlobalAssetAttribute
         ''' InputParameter    :''
         ''' OutPut parameter  :
         ''' Create Date	   :18.04.2016
         ''' Author Name	   :<a href="mukeemm@vrvirtual.com">Mohammad Mukeem</a> 
         ''' </summary>   
         ''' <remarks>
         '''****************************  Modification History  *************************************************************
         ''' Sr No:	Date		    Modified by	    Tracker                 Description
         '''*****************************************************************************************************************
        
         '''*****************************************************************************************************************
         '''</remarks>
         ''' */
        public List<ACS.Core.Domain.Asset.GlobalAssetAttributeMaster> getAllGlobalAssetAttribute()
        {
            var query = _globalAssetAttributeRepository.Table.ToList(); 
           
            return query;           
           
        }
    }
}
