using System;
using System.IO;

namespace App.Helpers
{
    public class PathHelper
    {
        /// <summary>
        /// Get project root path.
        /// </summary>
        /// <returns></returns>
        public static string GetProjectRootPath()
        {
            string baseDirectory = AppContext.BaseDirectory;
            int index = baseDirectory.IndexOf("bin");

            string projectRoot = baseDirectory;
            // If bin directory index is found
            if (index >= 0)
            {
                projectRoot = baseDirectory[..index];
            }

            return projectRoot;
        }

        /// <summary>
        /// Get environment file path. Doesn't ensure that environment file really exists.
        /// </summary>
        /// <returns></returns>
        public static string GetEnvironmentFilePath()
        {
            string projectRootPath = GetProjectRootPath();
            string envFilePath = Path.Combine(projectRootPath, ".env");

            return envFilePath;
        }
    }
}
