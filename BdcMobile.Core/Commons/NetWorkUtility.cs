using BdcMobile.Core.Extensions;
using MvvmCross;
using MvvmCross.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
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
                log.Info("Finish: " + method + ": " + url);
            }
            catch (WebException ex)
            {
                log.Error(ex.ToString());
                log.Error(ex.StackTrace);
                log.Error(method + ": " + url);
                var webResponse = ex.Response as HttpWebResponse;
                if (webResponse != null &&
                    webResponse.StatusCode == HttpStatusCode.Unauthorized)
                {
                    
                    
                }
                else
                    throw;
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

                var response = await request.GetResponseAsync(ct);

                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }

                log.Info("Finish: " + method + ": " + url);
            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
                log.Error(ex.StackTrace);
                log.Error(method + ": " + url);
            }

            return content;
        }

        public static async Task<string> SendFile(string url, IList<byte[]> data, IList<string> fileNames)
        {
            var ilog = Mvx.IoCProvider.Resolve<IMvxLogProvider>();
            var log = ilog.GetLogFor(Constants.AppConfig.LogTag);
            log.Info("POST: " + url);
            var content = string.Empty;
            try
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                for (int i = 0; i < data.Count; i++)
                {
                    var fileParameter = new FileParameter(data[i], fileNames[i]);
                    dict.Add($"file[{i}]", fileParameter);
                }

                var response = await FormUpload.MultipartFormDataPost(url, string.Empty, dict);

                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }

                log.Info("Finish: POST: " + url);
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
