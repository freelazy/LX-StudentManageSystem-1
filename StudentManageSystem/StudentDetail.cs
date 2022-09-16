using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace StudentManageSystem
{
    public class StudentDetail : IHttpHandler
    {
        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            string id = context.Request.Params["id"];

            // 第一步，将所有学生，从数据库中读取出来
            var student = DbHelper
                .DoExecuteQuery($"select * from students where id = '{id}'")
                .Rows[0];

            // 第二步，拼接出合适的 html 页面
            var html = PgHelper.GetHtml("student-detail.html",
                context.Request,
                student[0],
                student[1],
                student[2],
                student[7]);

            // 第三步，返回给请求者
            context.Response.Write(html);
        }
     }
}