using ACS.Core.Data;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Common
{
    public partial class CommonDropDown : ICommonDropDown
    {
            #region Fields
                private readonly IRepository<GeographicalMaster> _GeogRepository;
            #endregion
            #region Ctor

            /// <summary>
            /// Ctor
            /// </summary>
            /// <param name="GeographyType">Geography</param>    .
            /// 

            public CommonDropDown(IRepository<GeographicalMaster> GeogRepository)
            {
                _GeogRepository = GeogRepository;
            }

            #endregion
        #region Methods
        /// <summary>
        ///methos for selecting the geogType
        /// </summary>
        /// <param name="GeographyType">Geography</param>    
        /// 
             public IList<GeographicalMaster> GetAllGeog(GeographicalMaster geog)
            {
             
                     return _GeogRepository.Table.Where(c => c.Deactivate == "N"
                                                          && c.geogtype == geog.geogtype
                                                          && c.parentid == geog.parentid ).Distinct().OrderBy(c => c.geogName).ToList();
               
                

               
            }
        #endregion

            
    }
    
}
