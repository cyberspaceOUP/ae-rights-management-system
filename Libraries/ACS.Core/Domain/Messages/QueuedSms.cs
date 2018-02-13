using System;

namespace ACS.Core.Domain.Messages
{
    public partial class QueuedSms : BaseEntity
    {
        /// <summary>
        /// Gets or sets the priority
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Gets or sets the To property
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the send tries
        /// </summary>
        public int SentTries { get; set; }

        /// <summary>
        /// Gets or sets the sent date and time
        /// </summary>
        public DateTime? SentOn { get; set; }

        /// <summary>
        /// Gets or sets GUID returned by the SMS Gateway
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Gets or sets Status Code returned by the SMS Gateway
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// Gets or sets Reason Code returned by the SMS Gateway (in case of failure)
        /// </summary>
        public string ReasonCode { get; set; }

        /// <summary>
        /// Gets or sets the used SMS account identifier
        /// </summary>
        public int SmsAccountId { get; set; }

        /// <summary>
        /// Gets or sets the used SMS account identifier
        /// </summary>
        public int SmsTemplateId { get; set; }

        /// <summary>
        /// Gets the SMS account
        /// </summary>
        public virtual SmsAccount SMSAccount { get; set; }

        /// <summary>
        /// Gets the SMS Template
        /// </summary>
        public virtual SmsTemplate SMSTemplate { get; set; }
    }
}
