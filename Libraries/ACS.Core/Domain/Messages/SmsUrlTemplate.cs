using System;

namespace ACS.Core.Domain.Messages
{
    public partial class SmsUrlTemplate : BaseEntity
    {
        public string XmlName { get; set; }

        public string XmlURL { get; set; }

        public bool DeactTag { get; set; }
    }
}
