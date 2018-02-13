using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Vehicle;

namespace ACS.Services.Vehicle
{
    public partial interface IVehicleDetailService
    {
        /// <summary>
        /// Gets all Vehicles
        /// </summary>
        /// <param name=""></param>
        /// <returns>Vehicle Details collection</returns>
        IList<VehicleDetail> GetAllVehicles();

        /// <summary>
        /// Inset Vehicle Detail
        /// </summary>
        /// <param name="Vehicle">Takes the Vehicle Class Object</param>
        /// <returns></returns>
        void insertVehicle(VehicleDetail vehicle);


        /// <summary>
        /// Update Vehicle Detail
        /// </summary>
        /// <param name="Vehicle">Takes the Vehicle Class Object</param>
        /// <returns></returns>
        void updateVehicle(VehicleDetail vehicle);

        /// <summary>
        /// Delete Vehicle Detail
        /// </summary>
        /// <param name="Vehicle">Takes the Vehicle Class Object</param>
        /// <returns></returns>
        void deleteVehicle(VehicleDetail vehicle);

        /// <summary>
        /// Get Vehicel Vehicle Detail
        /// </summary>
        /// <param name="VehicleId">Takes the Vehicle Id</param>
        /// <returns></returns>
        VehicleDetail getVehicleDetailById(int vehicleId);

        /// <summary>
        /// Gets all Vehicles
        /// </summary>
        /// <param name="flatId"></param>
        /// <returns>Vehicle Details collection</returns>
        IList<VehicleDetail> GetAllVehiclesByFlat(int flatId);





    }
}
