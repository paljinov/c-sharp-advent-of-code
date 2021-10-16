using System;

namespace App.Tasks.Year2018.Day16
{
    public class SamplesRepository
    {
        public string[] GetIntegers(string input)
        {
            string[] samples = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            return samples;
        }
    }
}
