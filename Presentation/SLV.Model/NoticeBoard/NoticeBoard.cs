using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.NoticeBoard
{
    public class NoticeBoard
    {
        public int Id { get; set; }// Notice Board Id

        public string AccessibleTo { get; set; }

        public int AccessibleToId { get; set; }

        public int[] AccessibleTypesIdsArray { get; set; }

        public string Heading { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public string ExpiryDate { get; set; }

        public bool IsFlash { get; set; }

        public bool Validated { get; set; }

        public bool Published { get; set; }

        public string Remarks { get; set; }

        public int Type  { get; set; }

        public bool IsPaid { get; set; }

        public decimal Amount { get; set; }

        public int? ContactId { get; set; }

        public int VendorId { get; set; }

        public int SocietyId { get; set; }

        public int VisibleTo { get; set; }

        public string DocumentURL { get; set; }

        public string[] DocumentName { get; set; }

        public string DocumentType { get; set; }

        public int[] NoticeBoardVisibilityIds { get; set; }

        public int[] NoticeBoardDocumentIds { get; set; }

        public string ContactName { get; set; }

    }
}
