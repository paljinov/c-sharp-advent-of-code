namespace App.Tasks.Year2022.Day4
{
    public class PairsSections
    {
        public int CountPairsWhereOneRangeFullyContainTheOther(Pair[] pairsSections)
        {
            int pairsWhereOneRangeFullyContainTheOther = 0;

            for (int i = 0; i < pairsSections.Length; i++)
            {
                (int From, int To) firstSectionRange = pairsSections[i].FirstSectionRange;
                (int From, int To) secondSectionRange = pairsSections[i].SecondSectionRange;

                if (firstSectionRange.From <= secondSectionRange.From && firstSectionRange.To >= secondSectionRange.To
                 || secondSectionRange.From <= firstSectionRange.From && secondSectionRange.To >= firstSectionRange.To)
                {
                    pairsWhereOneRangeFullyContainTheOther++;
                }
            }

            return pairsWhereOneRangeFullyContainTheOther;
        }
    }
}
