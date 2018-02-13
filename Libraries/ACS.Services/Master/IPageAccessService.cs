using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Master
{
   public partial  interface IPageAccessService
    {
        /// <summary>
        /// Insert Page Access data 
        /// </summary>
        /// <returns></returns>

       void InsertPageAccess(PageAccessMaster  pageAccess);

    }
}
