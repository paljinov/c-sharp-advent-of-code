using System;

namespace App.Tasks.Year2019.Day1
{
    public class ModulesMassesRepository
    {
        public int[] GetModulesMasses(string input)
        {
            string[] modulesMassesString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            int[] modulesMasses = new int[modulesMassesString.Length];
            for (int i = 0; i < modulesMassesString.Length; i++)
            {
                modulesMasses[i] = int.Parse(modulesMassesString[i]);
            }

            return modulesMasses;
        }
    }
}
