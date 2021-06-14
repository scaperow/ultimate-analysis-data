using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Remote
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.RequestType.ToUpper() == "POST")
            {
                var key = Request.Form["key"];

                // 获得Cookie
                var cookie = FormsAuthentication.GetAuthCookie(key, true);

                // 得到ticket凭据
                var ticket = FormsAuthentication.Decrypt(cookie.Value);

                // 根据之前的ticket凭据创建新ticket凭据，然后加入自定义信息
                FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(
                    ticket.Version, ticket.Name, ticket.IssueDate,
                    ticket.Expiration, ticket.IsPersistent, key);

                // 将新的Ticke转变为Cookie值，然后添加到Cookies集合中
                cookie.Value = FormsAuthentication.Encrypt(newTicket);
                HttpContext.Current.Response.Cookies.Add(cookie);
                Response.Clear();
                Response.Write(JsonConvert.SerializeObject(new
                {
                    Result = true
                })); Response.End();
            }
            else
            {
                Response.StatusCode = 401;
                Response.Write(JsonConvert.SerializeObject(new
                {
                    Error = "请登录"
                })); Response.End();
            }
        }
    }
}