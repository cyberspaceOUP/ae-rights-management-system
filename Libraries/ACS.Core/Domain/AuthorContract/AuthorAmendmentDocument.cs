//create by saddam 08/12/2016 
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.AuthorContract
{


    public class AuthorAmendmentDocument : BaseEntity
    {

        public string Documentname { get; set; }
        public string documentfile { get; set; }
        public int AuthorContractId { get; set; }

        
        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }


        public virtual AuthorContractOriginal AuthorContractOriginal { get; set; }

    }

    
}
