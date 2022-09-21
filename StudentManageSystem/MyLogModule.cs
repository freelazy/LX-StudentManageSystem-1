using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace StudentManageSystem
{
    public class MyLogModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += MyRequestLog;
        }

        private void MyRequestLog(object sender, EventArgs e)
        {
            var application = (HttpApplication)sender;
            var req = application.Request;

            var ip = req.UserHostAddress;
            var ua = req.UserAgent;
            var url = req.Url.ToString();
            var now = DateTime.Now;

            // create table request_log (id int identity primary key, ip varchar(100), ua varchar(200), url varchar(400), created datetime);
            DbHelper.DoExecuteNonQuery(
                "insert into request_log (ip, ua, url, created) values (@ip, @ua, @url, @created)",
                new SqlParameter("ip", ip),
                new SqlParameter("ua", ua),
                new SqlParameter("url", url),
                new SqlParameter("created", now)
            );

            // req.Headers["User-Agent"]
            //if (req.UserAgent.Contains("Edg"))
            //{
            //    throw new HttpException("我不欢迎你");
            //}

            //application.Response.Write($"<h1>{req.RawUrl}</h1>");
        }

        public void Dispose()
        {
        }
    }


    public class AModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += (s, e) =>
            {
                var app = (HttpApplication)s;
                app.Response.Write("我是在 handler 被执行前触发的 1111");
            };
            context.PreRequestHandlerExecute += (s, e) =>
            {
                var app = (HttpApplication)s;
                app.Response.Write("我是在 handler 被执行前触发的 2222");
            };
            context.PostRequestHandlerExecute += (s, e) =>
            {
                var app = (HttpApplication)s;
                app.Response.Write("我是在 handler 被执行后触发");
            };
        }
    }
}