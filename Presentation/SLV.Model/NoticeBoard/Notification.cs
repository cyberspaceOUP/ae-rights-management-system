using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.NoticeBoard
{
   public class Notification
    {
        public int ContactId { get; set; }

        public int? FlatId { get; set; }

        public string Description { get; set; }

        public int ReferenceTypeId { get; set; } // e.g:- Notice, Bill, Payment, Ticket

        public int ReferenceId { get; set; } // e.g:- NoticeId, BillId, PaymentId.

        public string Area { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string IconClass { get; set; }

    }
}
