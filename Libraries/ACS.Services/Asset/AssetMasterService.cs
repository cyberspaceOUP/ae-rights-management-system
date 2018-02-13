using ACS.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Asset
{
    public partial class AssetMasterService:IAssetMasterService
    {
        private readonly IRepository<ACS.Core.Domain.Asset.GlobalAssetMaster> _globalAssetRepository;

        public AssetMasterService(
            IRepository<ACS.Core.Domain.Asset.GlobalAssetMaster> globalAssetRepository
            )
        {
            _globalAssetRepository = globalAssetRepository;
        }


        /*''' <summary>
         ''' Assembly Name     :AssetMasterService
         ''' Description	   :insert glabal asset 
         ''' Function Name     :insertAsset
         ''' InputParameter    :GlobalAssetMaster instance
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
        public void insertAsset(ACS.Core.Domain.Asset.GlobalAssetMaster globalAsset)
        {
            _globalAssetRepository.Insert(globalAsset);
        }


        /*''' <summary>
         ''' Assembly Name     :AssetMasterService
         ''' Description	   :update global Asset
         ''' Function Name     :updateAsset
         ''' InputParameter    :GlobalAssetMaster instance
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
        public void updateAsset(ACS.Core.Domain.Asset.GlobalAssetMaster globalAsset)
        {
            _globalAssetRepository.Update(globalAsset);

        }


        /*''' <summary>
         ''' Assembly Name     :AssetMasterService
         ''' Description	   :delete global asset
         ''' Function Name     :deleteAsset
         ''' InputParameter    :GlobalAssetMaster instance
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
        public void deleteAsset(ACS.Core.Domain.Asset.GlobalAssetMaster globalAsset)
        {
            _globalAssetRepository.Delete(globalAsset);

        }

        /*''' <summary>
        ''' Assembly Name     :AssetMasterService
        ''' Description	      :Get Global Asset by globalAssetId
        ''' Function Name     :getGlobalAssetById
        ''' InputParameter    :int globalAssestId
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

        public ACS.Core.Domain.Asset.GlobalAssetMaster getGlobalAssetById(int globalAssestId)
        {
            if (globalAssestId == 0)
                return null;

            return _globalAssetRepository.Table.Where(i => i.Id == globalAssestId).FirstOrDefault();
        }

        /*''' <summary>
       ''' Assembly Name     :AssetMasterService
       ''' Description	      :Get all global assets
       ''' Function Name     :getAllGlobalAssets
       ''' InputParameter    :
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
        public List<ACS.Core.Domain.Asset.GlobalAssetMaster> getAllGlobalAssets()
        {
            return _globalAssetRepository.Table.ToList();
        }

        /*''' <summary>
      ''' Assembly Name     :AssetMasterService
      ''' Description	    :Get Global Asset type ='Asset'
      ''' Function Name     :getAssetList
      ''' InputParameter    :
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
        public List<ACS.Core.Domain.Asset.GlobalAssetMaster> getGlobalAssetList()
        {  
            return  _globalAssetRepository.Table.Where(i => i.AssetType ==false).ToList();          

        }

        /*''' <summary>
     ''' Assembly Name     :AssetMasterService
     ''' Description	   :Get Global Facility Type='Facility'
     ''' Function Name     :getGlobalFacilityList
     ''' InputParameter    :
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
        public List<ACS.Core.Domain.Asset.GlobalAssetMaster> getGlobalFacilityList()
        {
           return _globalAssetRepository.Table.Where(i => i.AssetType == true).ToList();
           

        }
    }
}
