///Create by Saddam on 14/06/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.PermissionInbound;

namespace ACS.Core.Domain.OtherContract
{
    public partial class OtherContractImageBank : BaseEntity, ILocalizedEntity
    {
        public int? Printrunquantity { get; set; }
        public string PrintRights { get; set; }
        public string electronicrights   { get; set; }
        public string ebookrights  { get; set; }
        public string cost { get; set; }
        public int? currencyid { get; set; }
        public string restriction { get; set; }
        public int othercontractid { get; set; }
        public virtual OtherContractMaster OtherContractMaster { get; set; }
        public virtual ICollection<VideoImageBank> VideoImageBank { get; set; }
        
    }

    public partial class VideoImageBank:BaseEntity, ILocalizedEntity
    {
        public int ImageBankId { get; set; }
        public string Type { get; set; }
        public string ShortName { get; set; }
        public string Fullname { get; set; }
        public int CurrencyId { get; set; }
        public double Cost { get; set; }
        public virtual OtherContractImageBank OtherContractImageBank { get; set; }
        public virtual CurrencyMaster CurrencyMaster { get; set; }
       
    }

}
