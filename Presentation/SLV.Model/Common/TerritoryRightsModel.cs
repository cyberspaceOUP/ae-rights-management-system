using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.Common
{
    public class TerritoryRightsModel
    {
        public int Id { get; set; }
        public string Territoryrights { get; set; }
        public string Flag { get; set; }
        public string Deactivate { get; set; }

        public int? Territoryrightsid1 { get; set; }
        public int? Territoryrightsid2 { get; set; }
        public int? Territoryrightsid3 { get; set; }
        public int? Territoryrightsid4 { get; set; }
    }
}
