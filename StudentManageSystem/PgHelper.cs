using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace StudentManageSystem
{
    public class PgHelper
    {
        public static string GetTemplate(string path, HttpRequest request)
        {
            // 1.获取 student-update.html 文件的绝对路径
            string file = $"{request.PhysicalApplicationPath}html\\{path}";
            // 2. 通过 API 将文件中所有内容读取为字符串
            string context = File.ReadAllText(file);
            // 3. 将字符串返回
            return context;
        }

        public static string GetHtml(string path, HttpRequest request, params object[] ds)
        {
            var tpl = GetTemplate(path, request);
            return string.Format(tpl, ds)
                .Replace('[', '{')
                .Replace(']', '}');
        }
    }
}