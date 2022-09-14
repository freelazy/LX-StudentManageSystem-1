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
            var connStr = ConfigurationManager.ConnectionStrings["mydb"].ConnectionString;
            var sql = "select * from students";
            var adapter = new SqlDataAdapter(sql, connStr);

            var dt = new DataTable();
            adapter.Fill(dt);

            // 第二步，拼接出合适的 html 页面
            var rows = String.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                rows += $"<tr><td>{dr[1]}</td><td>{dr[2]}</td></tr>\n";
            }
            var head = "<tr><th>姓名</th><th>地址</th></tr>";
            var table = $"<table>{head}\n{rows}</table>";

            var css = "<style>table, th, td { border: 1px solid black; border-collapse: collapse; padding: 5px 2em }</style>";
            var html = $"<html><head>{css}</head><body>{table}</body></html>";

            // 第三步，返回给请求者
            context.Response.Write(html);
        }
     }
}