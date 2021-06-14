using GoldSoft.Identiter.Common;
using GoldSoft.Identiter.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Server.Controllers
{
    public class IdentityController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RequireLogin()
        {
            Response.StatusCode = 304 ;
            return Json(new
            {
                Error = "请登录"
            }, JsonRequestBehavior.AllowGet);
        }

        

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            var key = form["key"];

            var ticket = new FormsAuthenticationTicket(key, true, 30);
            var encrypt = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypt);

            Response.Cookies.Add(cookie);
            return Json(new
            {
                Result = true
            });
        }

        [Authorize]
        [HttpPost]
        public ActionResult Check(FormCollection form)
        {
            //JavaScript
            var args = form["args"];
            var result = new JsonResponse();


            if (string.IsNullOrEmpty(args))
            {
                return Json(new
                {
                    Error = "参数不正确"
                });
            }


            Excel[] list = null;
            try
            {
                list = JsonConvert.DeserializeObject<Excel[]>(args);
            }
            catch { }

            if (list == null)
            {
                return Json(new
                {
                    Error = "数据不正确"
                });
            }

            var identity = new Identity(new RulesAdapter(Server.MapPath("~/rule.accdb")));
            var results = identity.IdentityQuotaOnly(ProfessionalEnum.Decoration, list);
            var relations = new List<string>();
            if (results != null && results.Length > 0)
            {
                var success= results.Where(m => m.State == IdentityResultStateEnum.Success);
                foreach (var item in success)
                {
                    relations.Add(item.Relation);
                }
            }

            result.Result = relations.ToArray();

            return Content(JsonConvert.SerializeObject(result));
        }

        [Authorize]
        [HttpPost]
        public ActionResult StartIdentity(FormCollection form)
        {
            //JavaScript
            var args = form["args"];
            var result = new JsonResponse();


            if (string.IsNullOrEmpty(args))
            {
                return Json(new
                {
                    Error = "参数不正确"
                });
            }


            Excel[] list = null;
            try
            {
                list = JsonConvert.DeserializeObject<Excel[]>(args);
            }
            catch { }

            if (list == null)
            {
                return Json(new
                {
                    Error = "数据不正确"
                });
            }

            var identity = new Identity(new RulesAdapter(Server.MapPath("~/rule.accdb")));
            var results = identity.IdentityQuotaOnly(ProfessionalEnum.Decoration, list);
            result.Result = results;

            return Content(JsonConvert.SerializeObject(result));
        }
    }
}
