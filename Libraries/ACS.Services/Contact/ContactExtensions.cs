using System;
using System.Linq;
using System.Collections.Generic;
namespace ACS.Services.Contact
{
    public static class ContactExtensions
    {
        /// <summary>
        /// Get full name
        /// </summary>
        /// <param name="contact">Customer</param>
        /// <returns>Customer full name</returns>
        public static string FullName(this ACS.Core.Domain.Master.ExecutiveMaster Executive)
        {
            if (Executive == null)
                throw new ArgumentNullException("Executive");
            var firstName = Executive.executiveName ;
           

            string fullName = "";
            if (!String.IsNullOrWhiteSpace(firstName))
                fullName = string.Format("{0}", firstName);
            else
            {
                if (!String.IsNullOrWhiteSpace(firstName))
                    fullName = firstName;

              
            }
            return fullName;
        }

        /// <summary>
        /// Get full name with salutation
        /// </summary>
        /// <param name="contact">Contact</param>
        /// <returns>Resident full name with salutation</returns>
        public static string FullNameWithSalutation(this ACS.Core.Domain.Master.ExecutiveMaster Executive)
        {
            if (Executive == null)
                throw new ArgumentNullException("Executive");
            var firstName = Executive.executiveName;
           

            string fullName = "";
            if (!String.IsNullOrWhiteSpace(firstName) )
                fullName = string.Format("{0}",firstName);
            else
            {
                if (!String.IsNullOrWhiteSpace(firstName))
                    fullName = firstName;

              
            }
            return fullName;
        }

        //public static bool IsRegistered(this ACS.Core.Domain.Master.ExecutiveMaster Executive)
        //{
        //    if (Executive == null)
        //        throw new ArgumentNullException("Executive");

        //    TimeSpan span = (Executive.ModifiedDate - Executive.EntryDate); // if Entry Date and ModifiedDate are same "span.Minutes" return the "0", that validate contact is not registered
        //    if (span.Days>0)
        //        return true;
        //    else if (span.Hours > 0)
        //        return true;
        //    else
        //        return span.Minutes == 0 ? false : true;
            
        //}
    }
}
