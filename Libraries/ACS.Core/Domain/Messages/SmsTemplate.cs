using ACS.Core.Domain.Localization;
using System;

namespace ACS.Core.Domain.Messages
{
    public partial class SmsTemplate : BaseEntity, ILocalizedEntity
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the template is active
        /// </summary>
        public bool DeactTag { get; set; }

        /// <summary>
        /// Gets or sets the used SMS account identifier
        /// </summary>
        public int SmsAccountId { get; set; }

        public virtual SmsAccount SMSAccount { get; set; }

    }
}
