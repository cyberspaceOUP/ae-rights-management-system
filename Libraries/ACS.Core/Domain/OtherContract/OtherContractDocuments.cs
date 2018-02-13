///Create by Saddam on 14/06/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;

namespace ACS.Core.Domain.OtherContract
{
    public partial class OtherContractDocuments : BaseEntity, ILocalizedEntity
    {
        public string Documentname { get; set; }
        public string documentfile { get; set; }
        public int othercontractid { get; set; }
    
        public virtual OtherContractMaster OtherContractMaster { get; set; }


    }
}
