using GoldSoft.Identiter.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace GoldSoft.Identiter.UI
{
    public class Remoting
    {
        private WebClient Web;
        private Remoting _Self;
        public string Message;
        private Remoting()
        {
            Web = new WebClient();
        }
        public static Remoting Instance(string key, out string error)
        {
            Remoting remoting = null;
            error = "认证失败";
            try
            {
                var client = new WebClient();
                var args = new System.Collections.Specialized.NameValueCollection();
                args.Add("key", key);

                //var buffer = client.DownloadData(App.Default.ServiceURI);
                var buffer = client.UploadValues(App.Default.ServiceURI + "/login.aspx", "POST", args);
                var response = Encoding.UTF8.GetString(buffer);
                var result = JsonConvert.DeserializeObject<JsonResponse>(response);
                error = result.Error;
                if (string.IsNullOrEmpty(result.Error))
                {
                    error = "";
                    client.Headers.Add("Cookie", client.ResponseHeaders["Set-Cookie"]);
                    remoting = new Remoting()
                    {
                         Web = client
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return remoting;
        }


        public string[] Check(Excel[] excels, out string error)
        {
            string[] result = null;
            error = "没有获取到数据";

            try
            {
                var args = new System.Collections.Specialized.NameValueCollection();
                args.Add("args", JsonConvert.SerializeObject(Convert(excels)));
                var buffer = Web.UploadValues(App.Default.ServiceURI + "/check.aspx", "POST", args);
                var response = JsonConvert.DeserializeObject<JsonResponse>(Encoding.Default.GetString(buffer));
                error = response.Error;
                if (string.IsNullOrEmpty(error))
                {
                    error = "";
                    result = JsonConvert.DeserializeObject<string[]>(response.Result.ToString());
                }
            }
            catch (Exception e)
            {
                error = e.ToString();
                Console.WriteLine(e.ToString());
            }

            return result;
        }

        public IdentityResult[] Identity(Excel[] excels, out string error)
        {
            IdentityResult[] result = null;
            error = "没有获取到数据";

            try
            {
                var args = new System.Collections.Specialized.NameValueCollection();
                args.Add("args", JsonConvert.SerializeObject(Convert(excels)));
                var buffer = Web.UploadValues(App.Default.ServiceURI + "/start.aspx", "POST", args);
                var response = JsonConvert.DeserializeObject<JsonResponse>(Encoding.UTF8.GetString(buffer));
                error = response.Error;
                if (string.IsNullOrEmpty(error))
                {
                    error = "";
                    result = JsonConvert.DeserializeObject<IdentityResult[]>(response.Result.ToString());
                }
            }
            catch (Exception e)
            {
                error = e.ToString();
                Console.WriteLine(e.ToString());
            }

            return result;

        }

        private object[] Convert(Excel[] excels)
        {
            var result = new object[excels.Length];
            for (var i = 0; i < excels.Length; i++)
            {
                var excel = excels[i];
                result[i] = new 
                {
                    DW = excel.DW,
                    GLC = excel.GCL,
                    GCLXS = excel.GCLXS,
                    XH = excel.XH,
                    ZY = excel.ZY,
                    QDMC = excel.QDMC,
                    QDBH = excel.QDBH,
                    ID = excel.ID
                };
            }

            return result;
        }
    }
}
