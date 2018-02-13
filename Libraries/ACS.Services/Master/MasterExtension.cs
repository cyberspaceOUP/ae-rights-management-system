using ACS.Services.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Services.Master
{
    public static class MasterExtension
    {
        public static string DecryptPassword(this string value)
        {
            try
            {
                Exeutive executive = new Exeutive();
                EncryptionService EncService = new EncryptionService();
                
                if (value.Length > 0)
                {
                    value = EncService.DecryptText(value.ToString(), executive.KeyValue("encriptionkey"));
                }
                return value;
            }
            catch (Exception ex)
            {
                return value;
            }
          
        }
        public static string EncryptPassword(this string value)
        {
            try
            {
                if (value.Length > 0)
                {
                    value = new EncryptionService().EncryptText(value.ToString(), new Exeutive().KeyValue("encriptionkey"));
                }
                return value;
            }
            catch (Exception ex)
            {
                return value;
            }
            
        }
    }
}
