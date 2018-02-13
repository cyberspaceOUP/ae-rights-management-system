using System;

namespace ACS.Services.Common
{
    public static class DateTimeExtensions
    {
        public static string toDate(this System.DateTime? dt)
        {
            if (dt != null)
            {
                DateTime dateTime = Convert.ToDateTime(dt);
                return dateTime.ToString("dd/MM/yyyy");
            }
            else
                return "";
        }

        public static string toDate(this System.DateTime dt)
        {
            return dt.ToString("dd/MM/yyyy");
        }

        public static string toLongDate(this System.DateTime dt)
        {
            return dt.ToString("dd, MMM yyyy");
        }

        public static string toLongDate(this System.DateTime? dt)
        {
            if (dt != null)
            {
                DateTime dateTime = Convert.ToDateTime(dt);
                return dateTime.ToString("dd, MMM yyyy");
            }
            else
                return "";
            
        }

        public static string toTime(this System.DateTime? dt)
        {
            if (dt != null)
            {
                DateTime dateTime = Convert.ToDateTime(dt);
                return dateTime.ToString("HH:mm");
            }
            else
                return "";
        }

        public static string toTime(this System.DateTime dt)
        {
            return dt.ToString("HH:mm");
        }
      

        public static string toDateTime(this System.DateTime dt)
        {
            return dt.ToString("dd/MM/yyyy HH:mm");
        }

        public static string toDateTime(this System.DateTime? dt)
        {
            if (dt != null)
            {
                DateTime dateTime = Convert.ToDateTime(dt);
                return dateTime.ToString("dd/MM/yyyy HH:mm");
            }
            else
                return "";
        }


        //public static string toDateNotNull(this System.DateTime dt)
        //{
        //    return dt.ToString("dd/MM/yyyy");
        //}
        //public static string toDateTimeNotNull(this System.DateTime dt)
        //{

        //    return dt.ToString("dd/MM/yyyy hh:mm");
        //}
        public static string toLayOutDateTime(this System.DateTime dt)
        {

            return dt.ToString("dddd, dd MMM yyyy HH:mm");
        }

        public static string toLongDateTime(this System.DateTime dt)
        {

            return dt.ToString("dd MMM yyyy HH:mm");
        }

        public static string toInputDate(this System.DateTime dt)
        {
            return dt.ToString("MM/dd/yyyy");
        }

        public static string toInputDate(this System.DateTime? dt)
        {
            if (dt != null)
            {
                DateTime dateTime = Convert.ToDateTime(dt);
                return dateTime.ToString("yyyy/MM/dd");
            }
            else
                return "";
        }



        public static string toInputDateTime(this System.DateTime dt)
        {
            return dt.ToString("MM/dd/yyyy HH:mm");
        }
        public static string toInputDateTime(this System.DateTime? dt)
        {
            if (dt != null)
            {
                DateTime dateTime = Convert.ToDateTime(dt);
                return dateTime.ToString("MM/dd/yyyy HH:mm");
            }
            else
                return "";

        }

        public static string toInputDate(this string dt)
        {
            string[] splitDate = dt.Split('/');
            return splitDate[1] + "/" + splitDate[0] + "/" + splitDate[2];  // return MM/dd/yyyy
        }

        public static string toInputDateTime(this string  dt)
        {
            string[] splitDate = dt.Split('/');
            return splitDate[1] + "/" + splitDate[0] + "/" + splitDate[2];  // return MM/dd/yyyy
        }

    }
}
