using MvvmCross;
using MvvmCross.Logging;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace BdcMobile.Core.Commons
{
    public static class NetWorkUtility
    {


        public static async Task<string> MakeRequestAsync(string url, string method)
        {
            var ilog = Mvx.IoCProvider.Resolve<IMvxLogProvider>();
            var log = ilog.GetLogFor(Constants.AppConfig.LogTag);
            log.Info(method + ": " + url);
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

                log.Error(ex.ToString());
                log.Error(ex.StackTrace);
                log.Error(method + ": " + url);
            }
            return content;
        }



        public static async Task<string> MakeRequestAsync(string url, string method, CancellationToken ct)
        {
            var ilog = Mvx.IoCProvider.Resolve<IMvxLogProvider>();
            var log = ilog.GetLogFor(Constants.AppConfig.LogTag);
            log.Info(method + ": " + url);
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

                log.Error(ex.ToString());
                log.Error(ex.StackTrace);
                log.Error(method + ": " + url);
            }
            return content;
        }

        public static async Task<string> SendFile(string url, MemoryStream memStream)
        {
            var ilog = Mvx.IoCProvider.Resolve<IMvxLogProvider>();
            var log = ilog.GetLogFor(Constants.AppConfig.LogTag);
            log.Info("POST: " + url);
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
                log.Error(ex.ToString());
                log.Error(ex.StackTrace);
                log.Error("POST: " + url);
            }
            return content;
        }

        public static string MakeRequestSync(string url, string method)
        {
            var ilog = Mvx.IoCProvider.Resolve<IMvxLogProvider>();
            var log = ilog.GetLogFor(Constants.AppConfig.LogTag);
            log.Info("POST: " + url);
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
                log.Error(ex.ToString());
                log.Error(ex.StackTrace);
                log.Error(method + ": " + url);
            }
            return content;
        }


    }
}
