using System;

namespace ACS.Core.Domain.Messages
{
    public partial class SmsAccount : BaseEntity
    {
        /// <summary>
        /// Gets or sets Vendor Name (E.g.: Value First)
        /// </summary>
        public string Vendor { get; set; }

        /// <summary>
        /// Gets or sets an email user name
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets an email password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets an email from name
        /// </summary>
        public string FromName { get; set; }

        /// <summary>
        /// Gets or sets Deactivation Tag
        /// </summary>
        public bool DeactTag { get; set; }
    }
}
