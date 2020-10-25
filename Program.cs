﻿using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using App.Helpers;

namespace App
{
    class Program
    {
        static void Main()
        {
            int year;
            int day;
            int part;

            Console.WriteLine("Advent Of Code");
            Console.WriteLine("====================================");

            try
            {
                Console.WriteLine("Year:");
                year = ParseYearInput(Console.ReadLine());

                Console.WriteLine("Day:");
                day = ParseDayInput(Console.ReadLine());

                Console.WriteLine("Part:");
                part = ParsePartInput(Console.ReadLine());
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
            Regex regex = new Regex(@"^20\d{2}$");
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
