using ACS.Core.Domain.Localization;
using ACS.Core.Domain.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Core.Domain.AuthorContract
{
    public class AuthorContractauthordetails : BaseEntity, ILocalizedEntity
    {
        public int AuthorContractid{ get; set; }
        public int Authortype{ get; set; }
        public int ContractTypeId { get; set; }
        public int AuthorId { get; set; }
        public int? paymentperiodid{ get; set; }
        public int? AuthorCopies { get; set; }
        public double? Seedmoney { get; set; }
        public double? onetimepayment { get; set; }
        public double? advanceroyality{ get; set; }
         
        public virtual ICollection<AuthorContractRoyality> AuthorContractRoyality { get; set; }
        public virtual AuthorContractOriginal AuthorContractOriginal { get; set; }
        public virtual AuthorMaster AuthorMaster { get; set; }
        public virtual ContractMaster ContractMaster { get; set; }
        public virtual PaymentPeriod PaymentPeriod { get; set; }
        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
        public virtual AuthorType AuthorTypeMaster { get; set; }
          
    }
}
