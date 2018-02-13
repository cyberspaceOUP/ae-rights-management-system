//Create By Saddam
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ACS.Services.Master
{
    public partial interface IApplicationSetUpService
    {
        void UpdateApplication(ApplicationSetUp Application);
        ApplicationSetUp GetApplicationSetUpById(ApplicationSetUp Application);
    }
}
