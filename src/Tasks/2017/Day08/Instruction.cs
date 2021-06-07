namespace App.Tasks.Year2017.Day8
{
    public class Instruction
    {
        public string Register { get; set; }
        public InstructionType InstructionType { get; set; }
        public int Amount { get; set; }
        public string ConditionRegister { get; set; }
        public ComparisonOperator ComparisonOperator { get; set; }
        public int ConditionAmount { get; set; }
    }
}
