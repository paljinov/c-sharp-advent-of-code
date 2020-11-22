using System;
using System.IO;
using App.Helpers;
using DotNetEnv;

namespace App.Repository
{
    public class EnvironmentRepository
    {
        private bool isEnvironmentFileAlreadyRead = false;

        public string ReadYearInput()
        {
            string year = Environment.GetEnvironmentVariable("AOC_YEAR");
            if (string.IsNullOrEmpty(year))
            {
                LoadEnvironmentFile();
                year = Env.GetString("AOC_YEAR");
            }

            return year;
        }

        public string ReadDayInput()
        {
            string day = Environment.GetEnvironmentVariable("AOC_DAY");
            if (string.IsNullOrEmpty(day))
            {
                LoadEnvironmentFile();
                day = Env.GetString("AOC_DAY");
            }

            return day;
        }

        public string ReadPartInput()
        {
            string part = Environment.GetEnvironmentVariable("AOC_PART");
            if (string.IsNullOrEmpty(part))
            {
                LoadEnvironmentFile();
                part = Env.GetString("AOC_PART");
            }

            return part;
        }

        private void LoadEnvironmentFile()
        {
            if (!isEnvironmentFileAlreadyRead)
            {
                string envFilePath = PathHelper.GetEnvironmentFilePath();
                if (File.Exists(envFilePath))
                {
                    Env.Load(envFilePath);
                    isEnvironmentFileAlreadyRead = true;
                }
            }
        }
    }
}
