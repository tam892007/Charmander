using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BdcMobile.Core.Utility
{
    public static class Common
    {

        public static string MakeRequest(string url, string method)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = method;

            var content = string.Empty;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }
            return content;
        }
    }
}
