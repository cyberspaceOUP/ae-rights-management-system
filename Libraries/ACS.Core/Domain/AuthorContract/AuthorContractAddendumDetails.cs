using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.AuthorContract
{
    public class AuthorContractAddendumDetails : BaseEntity
    {
        public DateTime AddendumDate { get; set; }
        public string AddendumType { get; set; }
        public int? Periodofagreement { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Remarks { get; set; }
        public int? AuthorContractId { get; set; }
        public string SameAsEntery { get; set; }
        public string SeriesCode { get; set; }
        public string AddendumCode { get; set; }

        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
    }
}
