///Create by Saddam on 16/06/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;

namespace ACS.Core.Domain.OtherContract
{
    public partial class OtherContractLinkDocument : BaseEntity, ILocalizedEntity
    {
        public string DocumentnameLink { get; set; }
        public string documentfileLink { get; set; }
        public int othercontractLinkid { get; set; }

        public virtual OtherContractLink OtherContractLink { get; set; }

        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
    }
}
