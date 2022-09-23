using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace StudentManageSystem
{
    public class StudentList : IHttpHandler
    {
        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            // 第一步，将所有学生，从数据库中读取出来
            var sql = "select id, name, homecity from students";
            var dt = DbHelper.DoExecuteQuery(sql);

            // 第二步，拼接出合适的 html 页面
            var rows = String.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                rows += $@"
<tr>
  <td><a href='/student/detail?id={dr[0]}'>{dr[1]}</a></td>
  <td>{dr[2]}</td>
  <td>
     <a href='/student/update?id={dr[0]}'>编辑</a>
     <a href='#' onclick='doDelete(""{dr[0]}"")'>删除</a>
  </td>
</tr>
";
            }
            var html = PgHelper.GetHtml("student-list.html", rows);

            // 第三步，返回给请求者
            context.Response.Write(html);
        }
     }
}