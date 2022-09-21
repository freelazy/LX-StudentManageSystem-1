using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace StudentManageSystem
{
    public class LogView : IHttpHandler
    {
        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            var rows = DbHelper
                .DoExecuteQuery("select * from request_log order by created desc")
                .Rows
                .Cast<DataRow>()
                .Select(dr => $@"<div class='log-item'><span>{dr[1]}</span><span>{dr[3]}</span><span>{dr[4]}</span><span>{dr[2]}</span></div>")
                .ToArray();

            var html = PgHelper.GetHtml("log-view.html", string.Join("\n", rows));

            context.Response.Write(html);
        }
     }
}