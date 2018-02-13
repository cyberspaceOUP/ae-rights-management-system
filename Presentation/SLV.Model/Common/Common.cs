using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLV.Model.Common
{
    public class Common
    {
        public string Value { get; set; }
    }

    public class TickerModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int EnteredBy { get; set; }
        public int? Order { get; set; }
    }

    public class UploadDocumentModel
    {
        public int? Id { get; set; }
        public string MasterName { get; set; }
        public string MasterId { get; set; }
        public int EnteredBy { get; set; }

        public List<UploadFileDetails> FileDetails { get; set; }
    }

    public partial class UploadFileDetails
    {
        public string FileName { get; set; }
        public string UploadFileName { get; set; }
    }

    public partial class CurrencyMastetModel
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public string SymbolName { get; set; }
        public string Symbol { get; set; }
        public int flag { get; set; }
    }

}
