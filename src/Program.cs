using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using App.Helpers;
using App.Repository;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            int year;
            int day;
            int part;

            Console.WriteLine("Advent Of Code");
            Console.WriteLine("====================================");

            try
            {
                EnvironmentRepository environmentRepository = new EnvironmentRepository();
                string yearEnv = environmentRepository.ReadYearInput();
                string dayEnv = environmentRepository.ReadDayInput();
                string partEnv = environmentRepository.ReadPartInput();

                if (string.IsNullOrEmpty(yearEnv))
                {
                    Console.WriteLine("Task year is not set.");
                    return;
                }
                if (string.IsNullOrEmpty(dayEnv))
                {
                    Console.WriteLine("Task day is not set.");
                    return;
                }
                if (string.IsNullOrEmpty(partEnv))
                {
                    Console.WriteLine("Task part is not set.");
                    return;
                }

                year = ParseYearInput(yearEnv);
                day = ParseDayInput(dayEnv);
                part = ParsePartInput(partEnv);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            Type taskType = Type.GetType($"App.Tasks.Year{year}.Day{day}.Part{part}");
            if (taskType is null)
            {
                Console.WriteLine($"Task with year: {year}, day: {day} and part: {part} doesn't exist.");
                return;
            }

            string input = ReadInputHelper.ReadTaskInput(year, day);
            dynamic task = Activator.CreateInstance(taskType);

            Stopwatch stopwatch = Stopwatch.StartNew();
            var result = task.Solution(input);
            stopwatch.Stop();

            Console.WriteLine("====================================");
            Console.WriteLine($"Execution time: {stopwatch.ElapsedMilliseconds} miliseconds");
            Console.WriteLine($"Result: {result}");
        }

        private static int ParseYearInput(string year)
        {
            int currentYear = DateTime.UtcNow.Year;

            Regex regex = new Regex($@"^201[5-9]|202[0-{currentYear.ToString().Last()}]$");
            if (!regex.Match(year).Success)
            {
                throw new ArgumentException($"Year {year} is not valid.");
            }

            return int.Parse(year);
        }

        private static int ParseDayInput(string day)
        {
            Regex regex = new Regex(@"^0*([1-9]|1[0-9]|2[0-5])$");
            if (!regex.Match(day).Success)
            {
                throw new ArgumentException($"Day {day} is not valid.");
            }

            return int.Parse(day);
        }

        private static int ParsePartInput(string part)
        {
            Regex regex = new Regex(@"^[1-2]{1}$");
            if (!regex.Match(part).Success)
            {
                throw new ArgumentException($"Part {part} is not valid.");
            }

            return int.Parse(part);
        }
    }
}
