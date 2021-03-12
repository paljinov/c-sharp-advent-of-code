namespace App.Tasks.Year2017.Day9
{
    public class ProcessStream
    {
        public int CalculateAllGroupsTotalScore(string stream)
        {
            (int totalScore, _) = Calculate(stream);
            return totalScore;
        }

        public int CalculateNonCanceledCharactersWithinGarbage(string stream)
        {
            (_, int nonCanceledCharactersWithinGarbage) = Calculate(stream);
            return nonCanceledCharactersWithinGarbage;
        }

        private (int totalScore, int nonCanceledCharactersWithinGarbage) Calculate(string stream)
        {
            int depth = 1;
            int totalScore = 0;

            bool garbageStarted = false;
            int nonCanceledCharactersWithinGarbage = 0;

            for (int i = 0; i < stream.Length; i++)
            {
                char c = stream[i];

                if (c == '!')
                {
                    i++;
                }
                else if (garbageStarted && c != '>')
                {
                    nonCanceledCharactersWithinGarbage++;
                }
                else if (c == '<')
                {
                    garbageStarted = true;
                }
                else if (c == '>')
                {
                    garbageStarted = false;
                }
                else if (c == '{')
                {
                    totalScore += depth;
                    depth++;
                }
                else if (c == '}')
                {
                    depth--;
                }
            }

            return (totalScore, nonCanceledCharactersWithinGarbage);
        }
    }
}
