using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace StudentManageSystem
{
    public class StudentDelete : IHttpHandler
    {
        public bool IsReusable => false;

        public void ProcessRequest(HttpContext context)
        {
            // 获取请求者发送的数据
            var id = context.Request.Params["id"];

            // 操作数据库，执行添加任务
            var connStr = ConfigurationManager.ConnectionStrings["mydb"].ConnectionString;
            using (var connection = new SqlConnection(connStr))
            {
                connection.Open();
                var cmdText = $@"delete from students where id = @id";
                using (var command = new SqlCommand(cmdText,connection))
                {
                    command.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter("@id",id)
                    });
                    command.ExecuteNonQuery();
                }
            }

            // 返回相应页面
            context.Response.Redirect($"/students");
        }
     }
}