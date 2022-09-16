using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace StudentManageSystem
{
    public class StudentAddSubmit : IHttpHandler
    {
        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            // 获取请求者发送的数据
            var id = context.Request.Params["id"];
            var name = context.Request.Params["name"];
            var telephone = context.Request.Params["telephone"];
            var homecity = context.Request.Params["homecity"];
            var duyin = context.Request.Params["duyin"];
            var state = context.Request.Params["state"];

            // 操作数据库，执行添加任务
            DbHelper.DoExecuteNonQuery(
                @"insert into students (id, [name], homecity, [state], telephone, duyin) VALUES(@id, @name, @homecity, @state, @telephone, @duyin)",
                new SqlParameter("@id", id),
                new SqlParameter("@name", name),
                new SqlParameter("@homecity", homecity),
                new SqlParameter("@state", state),
                new SqlParameter("@telephone", telephone),
                new SqlParameter("@duyin", duyin));

            // 返回相应页面
            // /student/add , detail => student/detail
            // /student/add , student/detail => student/student/detail
            // /student/add , /student/detail => student/detail
            context.Response.Redirect($"/student/detail?id={id}");
        }
     }
}