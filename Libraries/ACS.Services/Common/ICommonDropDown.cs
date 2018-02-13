using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Common
{
    public partial interface ICommonDropDown
    {
        /// <summary>
        /// Gets all Country
        /// </summary>
        /// <param name="showHidden">It will used to populate the country</param>
        /// <returns>Country collection</returns>
        IList<GeographicalMaster> GetAllGeog(GeographicalMaster geog);
    }
}
