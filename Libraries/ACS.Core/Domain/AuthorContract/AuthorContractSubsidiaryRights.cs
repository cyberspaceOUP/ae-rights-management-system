using ACS.Core.Domain.Localization;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.AuthorContract
{
    public class AuthorContractSubsidiaryRights : BaseEntity, ILocalizedEntity
    {
        public int AuthorContractid { get; set; }
        public int AuthorId { get; set; }
        public int subsidiaryrightsid{ get; set; }
        public double AuthorPercentage { get; set; }
        public double ouppercentage { get; set; }
        public virtual AuthorContractOriginal AuthorContractOriginal { get; set; }
        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
        public virtual AuthorMaster AuthorMaster { get; set; }
        public virtual SubsidiaryRightsMaster SubsidiaryRightsMaster { get; set; }

    }
}
