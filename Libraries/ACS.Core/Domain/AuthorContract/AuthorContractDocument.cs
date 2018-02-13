using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.AuthorContract
{
    public class AuthorContractDocument:BaseEntity
    {
        public string FileNameEntered { get; set; }
        public int DocumentTypeId { get; set; }
        public int AgreementId { get; set; }
        public string FileName { get; set; }
        public virtual AuthorContractAgreement AuthorContractAgreement { get; set; }
        public virtual DocumentTypeMaster DocumentTypeMaster { get; set; }
        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }

    }
}
