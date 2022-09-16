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
            var html = $@"
<html>
<head>
<style>
</style>
</head>
<body>
    <h3> 学生详细信息 </h3>
    <ul>
       <li>学号: {student[0]}</li>
       <li>姓名: {student[1]}</li>
       <li>地址: {student[2]}</li>
       <li>电话: {student[7]}</li>
    </ul>
    <div>
       <button onclick='goHome()'>点我返回首页</button>
    </div>
    <script>
       function goHome () {{
          //if (window.confirm('是不是确定要返回首页?')) {{
             window.location.href = '/students';
          //}}
       }}
    </script>
</body>
</html>";

            // 第三步，返回给请求者
            context.Response.Write(html);
        }
     }
}