using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;

namespace MovieShop.Infrastructure.Helpers
{
    public class ScriptFingerprint
    {
        private static Dictionary<string, string> _mimeTypes = new Dictionary<string, string>()
        {
            {".gif","image/gif"},
            {".png","image/png"},
            {".ico","image/x-icon" }
        };

        private static string GetMinieTypeFromExtension(string extension)
        {
            string result = _mimeTypes[extension];
            if (string.IsNullOrEmpty(result))
            {
                return string.Empty;
            }

            return result;
        }

        public static string Tag(string rootRelativePath)
        {
            string absolute = HostingEnvironment.MapPath("~" + rootRelativePath);

            DateTime date = File.GetLastWriteTime(absolute);
            int index = rootRelativePath.LastIndexOf('/');

            string result = rootRelativePath.Insert(index, "/v-" + date.Ticks);
            return result;
        }

        public static string ToBase64(string rootRelativePath)
        {
            string absolute = HostingEnvironment.MapPath(rootRelativePath);
            string extension = Path.GetExtension(absolute);

            byte[] imageArray = System.IO.File.ReadAllBytes(absolute);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);

            string result = string.Format("data:{0};base64,{1}", GetMinieTypeFromExtension(extension), base64ImageRepresentation);
            return result;
        }
    }
}