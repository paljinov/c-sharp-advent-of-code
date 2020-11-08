using System;

namespace App.Helpers
{
    public class PathHelper
    {
        public static string GetProjectRootPath()
        {
            string projectRoot = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
            return projectRoot;
        }
    }
}
