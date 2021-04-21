using System;

namespace App.Tasks.Year2017.Day24
{
    public class ComponentRepository
    {
        public Component[] GetComponents(string input)
        {
            string[] componentsString = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Component[] components = new Component[componentsString.Length];

            for (int i = 0; i < componentsString.Length; i++)
            {
                string[] ports = componentsString[i].Split('/');

                Component component = new Component
                {
                    Port1 = int.Parse(ports[0]),
                    Port2 = int.Parse(ports[1]),
                };

                components[i] = component;
            }

            return components;
        }
    }
}
