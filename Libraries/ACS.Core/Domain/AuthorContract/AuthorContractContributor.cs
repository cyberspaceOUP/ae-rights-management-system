using ACS.Core.Domain.Localization;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.AuthorContract
{
    public class AuthorContractContributor : BaseEntity, ILocalizedEntity
    {
        public int AuthorContractId { get; set; }
        public string ContributorName { get; set; }
        public virtual AuthorContractOriginal AuthorContractOriginal { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
        
    }
}
