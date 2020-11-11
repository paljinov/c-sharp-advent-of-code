using System;

namespace App.Helpers
{
    public class PathHelper
    {
        public static string GetProjectRootPath()
        {
            string baseDirectory = AppContext.BaseDirectory;
            int index = baseDirectory.IndexOf("bin");

            string projectRoot = baseDirectory;
            // If bin directory index is found
            if (index >= 0)
            {
                projectRoot = baseDirectory.Substring(0, index);
            }

            return projectRoot;
        }
    }
}
