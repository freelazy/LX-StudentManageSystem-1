using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudentManageSystem
{
    public class MyShenFenModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += YanZhengShifouDenglu;
        }

        private void YanZhengShifouDenglu(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
            if (!app.Request.Url.ToString().Contains("/login"))
            {
                var zt = app.Request.Cookies["zt"];

                if (zt == null)
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