namespace App.Tasks.Year2016.Day10
{
    public class GiveChipsInstruction : IBotInstruction
    {
        public int BotNumber { get; set; }

        public bool LowerValueChipToOutput { get; set; }

        public int LowerValueChipTo { get; set; }

        public bool HigherValueChipToOutput { get; set; }

        public int HigherValueChipTo { get; set; }
    }
}
