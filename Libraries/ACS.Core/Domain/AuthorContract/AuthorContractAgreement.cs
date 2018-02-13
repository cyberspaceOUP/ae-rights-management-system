using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.AuthorContract
{
    public class AuthorContractAgreement : BaseEntity
    {

        public string contractstatus { get; set; }
        public int? ContractId { get; set; }
        public string SeriesCode { get; set; }
        public DateTime? dateofagreement { get; set; }
        public DateTime? effectiveDate { get; set; }
        public int? periodofagreement { get; set; }
        public DateTime? Expirydate { get; set; }
        public DateTime? SignedContractsentdate { get; set; }
        public DateTime? SignedContractreceived { get; set; }
        public DateTime? Authorcopiessentdate { get; set; }
        public DateTime? Contributorcopiessentdate { get; set; }
        public DateTime? Cancellationdate { get; set; }
        public string Cancellationreason { get; set; }
        public string ContributorRemarks { get; set; }
        public string Remarks { get; set; }
        public virtual AuthorContractOriginal AuthorContractOriginal { get; set; }
        public virtual ICollection<AuthorContractDocument> AuthorContractDocument { get; set; }
        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
    }
}
