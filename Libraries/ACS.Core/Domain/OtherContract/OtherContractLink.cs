///Create by Saddam on 16/06/2016
using ACS.Core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACS.Core.Domain.Master;
namespace ACS.Core.Domain.OtherContract
{
   
    public partial class OtherContractLink : BaseEntity, ILocalizedEntity
    {
        public string Contractstatus  { get; set; }
        public DateTime? SignedContractSentDate { get; set; }
        public DateTime? SignedContractReceived_Date { get; set; }
        public DateTime? CancellationDate { get; set; }
        public string Cancellation_Reason { get; set; }
        public int othercontractid { get; set; }

        public string Status { get; set; }

        public string Remarks { get; set; }



        public DateTime? AgreementDate { get; set; }
        public DateTime? Effectivedate { get; set; }
        public int? Contractperiodinmonth { get; set; }
        public DateTime? Expirydate { get; set; }


        public virtual OtherContractMaster OtherContractMaster { get; set; }
        public virtual ICollection<OtherContractLinkDocument> OtherContractLinkDocument { get; set; }

        public virtual ExecutiveMaster EnteredByForeignKey { get; set; }
        public virtual ExecutiveMaster ModifiedByForeignKey { get; set; }
        public virtual ExecutiveMaster DeactivateByForeignKey { get; set; }
    }
}
