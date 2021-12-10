using System;

namespace App.Tasks.Year2021.Day10
{
    public class NavigationSubsystemRepository
    {
        public string[] GetNavigationSubsystem(string input)
        {
            string[] navigationSubsystem = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            return navigationSubsystem;
        }
    }
}
