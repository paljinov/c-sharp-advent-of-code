using System;
using System.IO;

namespace App.Helpers
{
    public class PathHelper
    {
        public static string GetProjectRootPath()
        {
            string assemblyResolverBaseDir = AppDomain.CurrentDomain.BaseDirectory;
            string projectRootPath = Directory.GetParent(assemblyResolverBaseDir).Parent.Parent.Parent.FullName;

            return projectRootPath;
        }
    }
}
