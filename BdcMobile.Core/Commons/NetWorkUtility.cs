using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BdcMobile.Core.Commons
{
    public static class NetWorkUtility
    {

        public static async System.Threading.Tasks.Task<string> MakeRequestAsync(string url, string method)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = method;

            var content = string.Empty;
            var response = await request.GetResponseAsync();

          
            using (var stream = response.GetResponseStream())
            {
                using (var sr = new StreamReader(stream))
                {
                    content = sr.ReadToEnd();
                }
            }
            
            return content;
        }
    }
}
