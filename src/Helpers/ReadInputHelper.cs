using System.IO;

namespace App.Helpers
{
    class ReadInputHelper
    {
        public static string ReadTaskInput(int year, int day)
        {
            string projectRootPath = PathHelper.GetProjectRootPath();

            string taskInputPath = $"{projectRootPath}/src/Tasks/{year}/Day{day}/input.txt";
            if (!File.Exists(taskInputPath))
            {
                throw new FileNotFoundException($"Task input with year {year} and day {day} does not exists.");
            }

            string input = File.ReadAllText(taskInputPath);
            input = input.TrimEnd();

            return input;
        }
    }
}
