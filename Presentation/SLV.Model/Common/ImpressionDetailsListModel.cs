using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model
{
    public class ImpressionDetailsListModel
    {
        public int ProductId { get; set; }
        public int LicenseId { get; set; }
        public int ContractId { get; set; }
        public int AddendumId { get; set; }
        public string ImpressionDate { get; set; }
        public int QunatityPrinted { get; set; }
        public int BalanceQty { get; set; }
        public string ISBN { get; set; }
        public string ProductCode { get; set; }
    }
}
