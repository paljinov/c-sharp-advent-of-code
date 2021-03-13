using System.Collections.Generic;

namespace App.Tasks.Year2017.Day10
{
    public class LengthsSequenceRepository
    {
        public List<int> GetLengthsSequence(string input)
        {
            List<int> lengthsSequence = new List<int>();

            string[] lengthsSequenceString = input.Split(',');
            foreach (string length in lengthsSequenceString)
            {
                lengthsSequence.Add(int.Parse(length));
            }

            return lengthsSequence;
        }
    }
}
