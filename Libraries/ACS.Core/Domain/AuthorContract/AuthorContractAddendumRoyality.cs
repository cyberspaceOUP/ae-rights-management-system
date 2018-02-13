using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.AuthorContract
{
    public class AuthorContractAddendumRoyality : BaseEntity
    {

        public int ProductSubTypeId { get; set; }
        public int copiesfrom { get; set; }
        public int copiesto { get; set; }
        public decimal percentage { get; set; }
        public int AuthorContractId { get; set; }
        public int AddendumDetailsId { get; set; }
        public int AuthorId { get; set; }

        public virtual AuthorContractOriginal AuthorContractOriginal { get; set; }
        public virtual AuthorContractAddendumDetails AuthorContractAddendumDetails { get; set; }

        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }

    }
}
