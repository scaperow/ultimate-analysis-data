﻿using GoldSoft.Identiter.Common;
using GoldSoft.Identiter.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Remote
{
    public partial class start : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.RequestType.ToUpper() == "POST")
            {
                var args = Request.Form["args"];
                var result = new JsonResponse();


                if (string.IsNullOrEmpty(args))
                {
                    Response.Write(JsonConvert.SerializeObject(
                        (new
                    {
                        Error = "参数不正确"
                    }))); Response.End();
                }


                Excel[] list = null;
                try
                {
                    list = JsonConvert.DeserializeObject<Excel[]>(args);
                }
                catch { }

                if (list == null)
                {
                    Response.Write(JsonConvert.SerializeObject(
                     (new
                 {
                     Error = "数据不正确"
                 }))); Response.End();
                }

                var identity = new Identity(new RulesAdapter(Server.MapPath("~/rule.accdb")));
                var results = identity.IdentityQuotaOnly(ProfessionalEnum.Decoration, list);
                result.Result = results;

                Response.Write(JsonConvert.SerializeObject(result)); Response.End();
            }
        }
    }
}