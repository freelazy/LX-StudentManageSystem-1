using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace StudentManageSystem
{
    public class MyShenFenModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += ActiveSession;
            context.PreRequestHandlerExecute += SessionStyle;
        }

        private void ActiveSession(object sender, EventArgs e)
        {
            (sender as HttpApplication).Context
                .SetSessionStateBehavior(SessionStateBehavior.Required);
        }

        private void SessionStyle(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
            if (!app.Request.Url.ToString().Contains("/login"))
            {
                if (app.Context.Session["user"] == null)
                {
                    app.Response.Redirect("/login");
                    app.Response.End();
                }
            }
        }

        private void CookieStyle(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
            if (!app.Request.Url.ToString().Contains("/login"))
            {
                if (app.Request.Cookies["zt"] == null)
                {
                    app.Response.Redirect("/login");
                    app.Response.End();
                }
            }
        }

        public void Dispose()
        {
        }
    }
}