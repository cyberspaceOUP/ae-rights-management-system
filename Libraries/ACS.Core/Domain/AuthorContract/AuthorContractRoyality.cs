using ACS.Core.Domain.Localization;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.AuthorContract
{
    public class AuthorContractRoyality : BaseEntity, ILocalizedEntity
    {
        public int AuthorId { get; set; }
        public int AuthorContractid{ get; set; }
        public int subproducttypeid { get; set; }
        public int CopiesFrom { get; set; }
        public int CopiesTo { get; set; }
        public double Percentage { get; set; }
        public virtual AuthorContractauthordetails AuthorContractauthordetails { get; set; }
        public virtual AuthorContractOriginal AuthorContractOriginal { get; set; }
        public virtual ProductTypeMaster ProductTypeMaster { get; set; }
        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
        
    }
}
