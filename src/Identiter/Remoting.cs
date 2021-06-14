using GoldSoft.Identiter.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Analysis
{
    public  class Remoting
    {
        private Remoting _Self;
        public string Message;
        private Remoting()
        {

        }

        private void Login(string key)
        {
            var client = new WebClient();
            var args = new System.Collections.Specialized.NameValueCollection();
            args.Add("key", key);

            var buffer = client.UploadValues(@"http://localhost:1030/home/identity", "GET", args);
            var response = Encoding.UTF8.GetString(buffer);
            var result = JsonConvert.DeserializeObject<JsonResponse>(response);
            //var a = result["result"];
            if (string.IsNullOrEmpty(result.Error))
            {
                MessageBox.Show(result.Error);
            }
        }

        public Remoting Instance(string key)
        {
            var remoting = new Remoting();
            return remoting;


            
        }
    }
}
