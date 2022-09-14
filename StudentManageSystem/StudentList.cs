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
                rows += $"<tr><td>{dr[1]}</td><td>{dr[2]}</td></tr>\r\n";
            }
            var html = $@"
<html>
<head>
<style>
  table, th, td {{
    border: 1px solid black;
    border-collapse: collapse;
    padding: 5px 2em;
  }}
</style>
</head>
<body>
  <h3> 学生列表 </h3>
  <table>
<tr><th>姓名</th><th>地址</th></tr>
{rows}
  </table>
</body>
</html>";

            // 第三步，返回给请求者
            context.Response.Write(html);
        }
     }
}