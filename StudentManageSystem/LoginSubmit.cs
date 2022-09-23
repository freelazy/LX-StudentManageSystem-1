using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace StudentManageSystem
{
    public class LoginSubmit : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            SessionStyle(context);
        }

        void SessionStyle(HttpContext context)
        {
            var username = context.Request["username"];
            var password = context.Request["password"];

            if (username.Count() > 3 && password == "admin")
            {
                context.Session["user"] = new User
                {
                    Username = username,
                    Password = password
                };
                context.Response.Redirect("/students");
            }
            else
            {
                context.Response.Redirect("/login");
            }
        }

        void CookieStyle(HttpContext context)
        {
            var username = context.Request["username"];
            var password = context.Request["password"];

            if (username.Count() > 3 && password == "admin")
            {
                context.Response.Cookies.Add(new HttpCookie("zt")
                {
                    Value = $"SFID: {username}"
                });
                context.Response.Redirect("/students");
            }
            else
            {
                context.Response.Redirect("/login");
            }
        }
     }
}