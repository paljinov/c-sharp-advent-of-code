namespace App.Tasks.Year2016.Day1
{
    class InstructionsRepository
    {
        public string[] GetInstructions(string input)
        {
            string[] instructions = input.Split(", ");
            return instructions;
        }
    }
}
