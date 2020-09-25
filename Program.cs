﻿using System;
using System.Text.RegularExpressions;

namespace App
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Advent Of Code");
            Console.WriteLine("====================================");

            Regex yearRegex = new Regex(@"^20\d{2}$");
            Console.WriteLine("Year:");
            string year = Console.ReadLine();
            if (!yearRegex.Match(year).Success)
            {
                Console.WriteLine($"Year {year} is not valid.");
                return;
            }

            Regex dayRegex = new Regex(@"^\d{1,2}$");
            Console.WriteLine("Day:");
            string day = Console.ReadLine();
            if (!dayRegex.Match(day).Success)
            {
                Console.WriteLine($"Day {day} is not valid.");
                return;
            }

            Regex partRegex = new Regex(@"^[1-2]{1}$");
            Console.WriteLine("Part:");
            string part = Console.ReadLine();
            if (!partRegex.Match(part).Success)
            {
                Console.WriteLine($"Part {part} is not valid.");
                return;
            }

            Type taskType = Type.GetType($"App.Tasks.Year{year}.Day{day}.Part{part}");
            if (taskType is null)
            {
                Console.WriteLine($"Task with year: {year}, day: {day} and part: {part} doesn't exist.");
                return;
            }

            dynamic task = Activator.CreateInstance(taskType);
            var result = task.Program();

            Console.WriteLine($"Result is {result}.");
        }
    }
}
