using ACS.Core.Data;
using ACS.Core.Domain.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Vehicle
{
    public partial class VehicleDetailService : IVehicleDetailService
    {
        #region Fields

        private readonly IRepository<VehicleDetail> _vehicleDetailRepository;        

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="settingRepository">VehicleDetail repository</param>
        public VehicleDetailService(IRepository<VehicleDetail> vehicleDetailRepository)
        {
            this._vehicleDetailRepository = vehicleDetailRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a VehicleDetail
        /// </summary>
        /// <param name="VehicleDetail">VehicleDetail</param>
        public virtual void insertVehicle(VehicleDetail vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException("vehicle");

            _vehicleDetailRepository.Insert(vehicle);
        }

        /// <summary>
        /// Updates a setting
        /// </summary>
        /// <param name="setting">Setting</param>
        /// <param name="clearCache">A value indicating whether to clear cache after setting update</param>
        public virtual void updateVehicle(VehicleDetail vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException("vehicle");

            _vehicleDetailRepository.Update(vehicle);
        }

        /// <summary>
        /// Deletes a VehicleDetail
        /// </summary>
        /// <param name="VehicleDetail">VehicleDetail</param>
        public virtual void deleteVehicle(VehicleDetail vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException("vehicle");

            _vehicleDetailRepository.Delete(vehicle);

        }

        /*''' <summary>
        ''' Assembly Name     :AssetAttributeValueService
        ''' Description	      :get All vechiel by the vehicleId
        ''' Function Name     :getVehicleDetailById
        ''' InputParameter    :vehicleId
        ''' OutPut parameter  :
        ''' Create Date	   :10.03.2016
        ''' Author Name	   :<a href="sachins@acsinfotech.com">Sachin Saxena</a> 
        ''' </summary>   
        ''' <remarks>
        '''****************************  Modification History  *************************************************************
        ''' Sr No:	Date		    Modified by	    Tracker                 Description
        '''*****************************************************************************************************************
        
        '''*****************************************************************************************************************
        '''</remarks>
        ''' */
        public virtual VehicleDetail getVehicleDetailById(int vehicleId)
        {
            if (vehicleId == 0)
                return null;

            return _vehicleDetailRepository.GetById(vehicleId);
        }



        /*''' <summary>
        ''' Assembly Name     :AssetAttributeValueService
        ''' Description	      :get All vechiel parking detail
        ''' Function Name     :GetAllVehicles
        ''' InputParameter    :
        ''' OutPut parameter  :
        ''' Create Date	   :10.03.2016
        ''' Author Name	   :<a href="sachins@acsinfotech.com">Sachin Saxena</a> 
        ''' </summary>   
        ''' <remarks>
        '''****************************  Modification History  *************************************************************
        ''' Sr No:	Date		    Modified by	    Tracker                 Description
        '''*****************************************************************************************************************
        
        '''*****************************************************************************************************************
        '''</remarks>
        ''' */
        public virtual IList<VehicleDetail> GetAllVehicles()
        {
            return  _vehicleDetailRepository.Table.ToList();   
        }

        /*''' <summary>
        ''' Assembly Name     :AssetAttributeValueService
        ''' Description	      :get All vechiel parking by the flat id 
        ''' Function Name     :GetAllVehiclesByFlat
        ''' InputParameter    :flatId
        ''' OutPut parameter  :
        ''' Create Date	   :10.03.2016
        ''' Author Name	   :<a href="sachins@acsinfotech.com">Sachin Saxena</a> 
        ''' </summary>   
        ''' <remarks>
        '''****************************  Modification History  *************************************************************
        ''' Sr No:	Date		    Modified by	    Tracker                 Description
        '''*****************************************************************************************************************
        
        '''*****************************************************************************************************************
        '''</remarks>
        ''' */
        public IList<VehicleDetail> GetAllVehiclesByFlat(int flatId)
        {
            return _vehicleDetailRepository.Table.Where(i=>i.FlatId==flatId).ToList();
        }

        

        #endregion
    }
}
