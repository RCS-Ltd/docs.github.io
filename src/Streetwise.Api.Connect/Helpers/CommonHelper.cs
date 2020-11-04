using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Streetwise.Api.Connect.Helpers
{
    public static class CommonHelper
    {

        public static string MapPath(string path, string Root)
        {
            path = path.Replace("~/", string.Empty).TrimStart('/');

            //if virtual path has slash on the end, it should be after transform the virtual path to physical path too
            var pathEnd = path.EndsWith("/") ? Path.DirectorySeparatorChar.ToString() : string.Empty;

            return Combine(Root ?? string.Empty, path) + pathEnd;
        }

        public static string Combine(params string[] paths)
        {
            return Path.Combine(paths.SelectMany(p => IsUncPath(p) ? new[] { p } : p.Split('\\', '/')).ToArray());
        }

        private static bool IsUncPath(string path)
        {
            return Uri.TryCreate(path, UriKind.Absolute, out var uri) && uri.IsUnc;
        }

        public static StringContent JsonContent(object obj) 
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }        
    }
}
