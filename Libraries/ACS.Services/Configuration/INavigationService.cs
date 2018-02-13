using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Configuration;

namespace ACS.Services.Configuration
{
    public partial interface INavigationService
    {
        //ADDED BY AMAN KUMAR ON DATE 07/03/2016
        IList<ApplicationActivities> GetTopActivities(int ProfileId);

        ApplicationActivities GetSubMenus(int SubMenuId);
    }
}
