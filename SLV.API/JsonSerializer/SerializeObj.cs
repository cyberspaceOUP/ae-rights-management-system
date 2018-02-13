using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLV.API.Controllers.JsonSerializer
{
    public static class SerializeObj
    {
        public static object SerializeObject(object obj)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return JsonConvert.DeserializeObject(json);
        }
    }
}