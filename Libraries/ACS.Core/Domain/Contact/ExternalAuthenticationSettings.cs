using System.Collections.Generic;
using ACS.Core.Configuration;

namespace ACS.Core.Domain.Contact
{
    public class ExternalAuthenticationSettings : ISettings
    {
        public ExternalAuthenticationSettings()
        {
            ActiveAuthenticationMethodSystemNames = new List<string>();
        }

        public bool AutoRegisterEnabled { get; set; }
        /// <summary>
        /// Gets or sets an system names of active payment methods
        /// </summary>
        public List<string> ActiveAuthenticationMethodSystemNames { get; set; }
    }
}
