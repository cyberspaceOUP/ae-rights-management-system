//Create By Saddam on 13/07/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;
using ACS.Core.Domain.AuthorContract;
using ACS.Core.Domain.Product;
using ACS.Core.Domain.RightsSelling;
namespace ACS.Core.Domain.RightsSelling
{
    public partial   class RightsSellingDocument : BaseEntity , ILocalizedEntity
    {
        public string Documentname { get; set; }
        public string DocumentFile { get; set; }
        public int RightsSellingUpdateId { get; set; }

        public virtual RightsSellingUpdate RightsSellingUpdate { get; set; }


        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
    }
}
