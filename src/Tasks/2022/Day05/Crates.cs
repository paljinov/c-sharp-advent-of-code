using System.Collections.Generic;

namespace App.Tasks.Year2022.Day5
{
    public class Crates
    {
        public string GetCratesWhichEndUpOnTopOfEachStack(
            Dictionary<int, Stack<char>> cratesStacks,
            Step[] rearrangementProcedureSteps
        )
        {
            RearrangeCrates(cratesStacks, rearrangementProcedureSteps);
            string cratesWhichEndUpOnTopOfEachStack = GetStacksTopCrates(cratesStacks);

            return cratesWhichEndUpOnTopOfEachStack;
        }

        private void RearrangeCrates(Dictionary<int, Stack<char>> cratesStacks, Step[] rearrangementProcedureSteps)
        {
            foreach (Step step in rearrangementProcedureSteps)
            {
                for (int i = 0; i < step.Quantity; i++)
                {
                    char crate = cratesStacks[step.FromStack].Pop();
                    cratesStacks[step.ToStack].Push(crate);
                }
            }
        }

        private string GetStacksTopCrates(Dictionary<int, Stack<char>> cratesStacks)
        {
            string stacksTopCrates = string.Empty;
            foreach (KeyValuePair<int, Stack<char>> stack in cratesStacks)
            {
                stacksTopCrates += stack.Value.Peek();
            }

            return stacksTopCrates;
        }
    }
}
