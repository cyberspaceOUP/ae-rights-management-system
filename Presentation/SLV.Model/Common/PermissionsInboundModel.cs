//Create by Saddam 02/08/2016
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.Common
{
    public  class PermissionsInboundModel
    {
        public int Id { get; set; }
        public string Restriction { get; set; }
        public string PrintRights { get; set; }
        public string Electronicrights { get; set; }
        public string Ebookrights { get; set; }
        public int? Cost { get; set; }
        public string CurrencyName { get; set; }
    }
}
