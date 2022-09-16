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
            var html = $@"
<html>
<head>
<style>
  table, th, td {{
    border: 1px solid black;
    border-collapse: collapse;
    padding: 5px 2em;
  }}
  a {{
    text-decoration: none;
  }}
</style>
</head>
<body>
  <h3> 学生列表 </h3>
  <div>
     <a href='/student/add'>添加学生</a>
  </div>
  <table>
<tr><th>姓名</th><th>地址</th><th>操作</th></tr>
{rows}
  </table>
  <script>
     function doDelete(id) {{
         if (window.confirm('是不是确定要删除?'))
         {{
              window.location.href = '/student/delete?id=' + id;
         }}
     }}
  </script>
</body>
</html>";

            // 第三步，返回给请求者
            context.Response.Write(html);
        }
     }
}