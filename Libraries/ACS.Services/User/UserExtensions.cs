using System;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;

namespace ACS.Services.User
{
    public static class UserExtensions
    {


        public static bool IsRegistered(this ACS.Core.Domain.Master.ExecutiveMaster user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return !string.IsNullOrEmpty(user.Password);
        }

        public static DateTime toDateTime(this string dateString)
        {
            if (dateString == null || dateString == "")
                throw new ArgumentNullException("Date");
            return DateTime.Parse(dateString, new CultureInfo("en-US"));

        }
        public static string toDDMMYYYY(this DateTime dateString)
        {
            return dateString.ToString("dd/MM/yyyy");

        }






        //public static DateTime SubProduct(this int Id)
        //{


        //}
    }


   
}
