///Create by Saddam on 26/09/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.OtherContract;


namespace ACS.Core.Domain.Alert
{
    public partial class ApplicationEmailSetup : BaseEntity, ILocalizedEntity
    {
        public string EmailType { get; set; }
        public string subject { get; set; }
        public string EmailTo { get; set; }
        public string EmailCCTo { get; set; }
        public string EmailBCCTo { get; set; }

    }
}
