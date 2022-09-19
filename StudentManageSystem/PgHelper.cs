using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StudentManageSystem
{
    public class PgHelper
    {
        public static string GetTemplate(string path)
        {
            // 1.获取 student-update.html 文件的绝对路径
            path = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "html", path);
            // 2. 通过 API 将文件中所有内容读取为字符串
            var context = File.ReadAllText(path);
            // 3. 将字符串返回
            return context;
        }

        /// <summary>
        /// 不完美的完美实现。基于字符串替换。
        /// </summary>
        public static string GetHtml(string path, params object[] ds)
        {
            var tpl = GetTemplate(path);
            return string.Format(tpl
                .Replace("{{", "<<aaa>>")
                .Replace("}}", "<<bbb>>")
                .Replace("{",  "<<fff>>")
                .Replace("}",  "<<ggg>>")
                .Replace("<<aaa>>",  "{")
                .Replace("<<bbb>>",  "}"),
              ds)
                .Replace("<<fff>>",  "{")
                .Replace("<<ggg>>",  "}");
        }
    }
}