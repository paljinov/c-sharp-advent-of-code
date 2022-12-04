namespace App.Tasks.Year2022.Day4
{
    public class SectionAssignmentPairs
    {
        public int CountSectionAssignmentPairsWhereOneRangeFullyContainTheOther(Pair[] sectionAssignmentPairs)
        {
            int pairsWhereOneRangeFullyContainTheOther = 0;

            for (int i = 0; i < sectionAssignmentPairs.Length; i++)
            {
                (int From, int To) firstRange = sectionAssignmentPairs[i].FirstSectionsRange;
                (int From, int To) secondRange = sectionAssignmentPairs[i].SecondSectionsRange;

                if (firstRange.From >= secondRange.From && firstRange.To <= secondRange.To
                    || secondRange.From >= firstRange.From && secondRange.To <= firstRange.To)
                {
                    pairsWhereOneRangeFullyContainTheOther++;
                }
            }

            return pairsWhereOneRangeFullyContainTheOther;
        }

        public int CountSectionAssignmentPairsWhereRangesOverlap(Pair[] sectionAssignmentPairs)
        {
            int pairsWhereOneRangeFullyContainTheOther = 0;

            for (int i = 0; i < sectionAssignmentPairs.Length; i++)
            {
                (int From, int To) firstRange = sectionAssignmentPairs[i].FirstSectionsRange;
                (int From, int To) secondRange = sectionAssignmentPairs[i].SecondSectionsRange;

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
