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
            var html = $@"
<html>
<head>
<style>
    .btn-group {{
       padding: 2em;
       display: flex;
       justify-content: space-around;
       width: 200px;
    }}
</style>
</head>
<body>
    <h3> 添加学生 </h3>
    <form action='/student/update' method='POST'>
        <div>
            <div>
                 <span>学号</span>
                 <span><input name='id' value={student["id"]} readonly></span>
            </div>
            <div>
                 <span>姓名</span>
                 <span><input name='name' value={student["name"]}></span>
            </div>
            <div>
                 <span>电话</span>
                 <span><input name='telephone' value={student["telephone"]}></span>
            </div>
            <div>
                 <span>地址</span>
                 <span><input name='homecity' value={student["homecity"]}></span>
            </div>
            <div>
                 <span>读音</span>
                 <span><input name='duyin' value={student["duyin"]}></span>
            </div>
            <div>
                 <span>状态</span>
                 <span><input name='state' value={student["state"]}></span>
            </div>
        </div>
        <div class='btn-group'>
           <button onclick='goHome()'>返回首页</button>
           <button>保存</button>
        </div>
    </form>
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