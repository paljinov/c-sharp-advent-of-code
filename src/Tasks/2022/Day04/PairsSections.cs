namespace App.Tasks.Year2022.Day4
{
    public class PairsSections
    {
        public int CountPairsWhereOneRangeFullyContainTheOther(Pair[] pairsSections)
        {
            int pairsWhereOneRangeFullyContainTheOther = 0;

            for (int i = 0; i < pairsSections.Length; i++)
            {
                (int From, int To) firstRange = pairsSections[i].FirstSectionsRange;
                (int From, int To) secondRange = pairsSections[i].SecondSectionsRange;

                if (firstRange.From >= secondRange.From && firstRange.To <= secondRange.To
                    || secondRange.From >= firstRange.From && secondRange.To <= firstRange.To)
                {
                    pairsWhereOneRangeFullyContainTheOther++;
                }
            }

            return pairsWhereOneRangeFullyContainTheOther;
        }

        public int CountPairsWhereRangesOverlap(Pair[] pairsSections)
        {
            int pairsWhereOneRangeFullyContainTheOther = 0;

            for (int i = 0; i < pairsSections.Length; i++)
            {
                (int From, int To) firstRange = pairsSections[i].FirstSectionsRange;
                (int From, int To) secondRange = pairsSections[i].SecondSectionsRange;

                if (firstRange.From >= secondRange.From && firstRange.From <= secondRange.To
                    || secondRange.From >= firstRange.From && secondRange.From <= firstRange.To)
                {
                    pairsWhereOneRangeFullyContainTheOther++;
                }
            }

            return pairsWhereOneRangeFullyContainTheOther;
        }
    }
}
