using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.Common
{
    public class SeriesByDividionSubdivisionModel
    {
        public int Id { get; set; }

        public int DivisionId { get; set; }

        public int SubDivisionId { get; set; }

        public string SeriesName { get; set; }

        public string DivisionName { get; set; }

        public string SubdivisionName { get; set; }

    }
}
