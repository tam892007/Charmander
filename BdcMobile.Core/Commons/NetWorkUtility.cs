using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BdcMobile.Core.Commons
{
    public static class NetWorkUtility
    {

        public static async Task<string> MakeRequestAsync(string url, string method)
        {
            Log.Info(Constants.AppConfig.LogTag, method + ": " + url);
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
                
                Log.Error(Constants.AppConfig.LogTag, ex.ToString());
                Log.Error(Constants.AppConfig.LogTag, ex.StackTrace);
                Log.Error(Constants.AppConfig.LogTag, method + ": " + url);
            }
            return content;
        }

        public static async Task<string> SendFile(string url, MemoryStream memStream)
        {
            Log.Info(Constants.AppConfig.LogTag,  "POST: " + url);
            var content = string.Empty;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                using (Stream requestStream = request.GetRequestStream())
                {
                    memStream.Position = 0;
                    byte[] tempBuffer = new byte[memStream.Length];
                    memStream.Read(tempBuffer, 0, tempBuffer.Length);
                    memStream.Close();
                    requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                }

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
                Log.Error(Constants.AppConfig.LogTag, ex.ToString());
                Log.Error(Constants.AppConfig.LogTag, ex.StackTrace);
                Log.Error(Constants.AppConfig.LogTag, "POST: " + url);
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
                Log.Error(Constants.AppConfig.LogTag, ex.ToString());
                Log.Error(Constants.AppConfig.LogTag, ex.StackTrace);
                Log.Error(Constants.AppConfig.LogTag, method + ": " + url);
            }
            return content;
        }


    }
}
