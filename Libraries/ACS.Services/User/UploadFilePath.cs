using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;



namespace ACS.Services.User
{
    public static class UploadFilePath
    {
    

     public static string ExcelUplaodPath(this string keyName)
     {
         if (keyName == null || keyName == "")
             throw new ArgumentNullException("Key Not Found");
         return WebConfigurationManager.AppSettings[""+keyName+""];

     }

   //  public static string IkBookFormDownload = ConfigurationManager.AppSettings["IkBookFormDownload"];
    
    }
}
