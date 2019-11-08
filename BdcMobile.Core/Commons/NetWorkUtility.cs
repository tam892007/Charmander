using Android.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BdcMobile.Core.Commons
{
    public static class NetWorkUtility
    {

        public static async Task<string> MakeRequestAsync(string url, string method)
        {
            var content = string.Empty;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;
                
                var response = await request.GetResponseAsync();

                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }                
            }
            catch (Exception ex)
            {
                Log.Error("BdcMobile", ex.ToString());
                Log.Error("BdcMobile", method + ": " + url);
            }
            return content;
        }

        public static string MakeRequestSync(string url, string method)
        {
            var content = string.Empty;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;
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
            }
            catch (Exception ex)
            {
                Log.Error("BdcMobile", ex.ToString());
                Log.Error("BdcMobile", method + ": " + url);
            }
            return content;
        }


    }
}
