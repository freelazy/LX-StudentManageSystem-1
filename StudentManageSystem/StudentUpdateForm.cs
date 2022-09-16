using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace StudentManageSystem
{
    public class StudentUpdateForm : IHttpHandler
    {
        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            var id = context.Request.Params["id"];

            // 先将要修改的数据查出来
            var student = DbHelper
                .DoExecuteQuery($"select * from students where id = '{id}'")
                .Rows[0];

            // 组装页面
            var html = PgHelper.GetHtml("student-update.html",
                context.Request,
                student["id"],
                student["name"],
                student["telephone"],
                student["homecity"],
                student["duyin"],
                student["state"]);

            // 第三步，返回给请求者
            context.Response.Write(html);
        }
     }




}