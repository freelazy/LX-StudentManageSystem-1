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
}