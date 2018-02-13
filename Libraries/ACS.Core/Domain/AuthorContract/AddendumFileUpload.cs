using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.AuthorContract
{
    public class AddendumFileUpload : BaseEntity
    {

        public string Documentname { get; set; }
        public string documentfile { get; set; }
        public int AddendumDetailsId { get; set; }

      //  public virtual AuthorContractOriginal AuthorContractOriginal { get; set; }

        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }


       public virtual AuthorContractAddendumDetails AuthorContractAddendumDetails { get; set; }

    }
}
