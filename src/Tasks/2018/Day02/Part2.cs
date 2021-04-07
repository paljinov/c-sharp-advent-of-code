/*
--- Part Two ---

Confident that your list of box IDs is complete, you're ready to find the boxes
full of prototype fabric.

The boxes will have IDs which differ by exactly one character at the same
position in both strings. For example, given the following box IDs:

abcde
fghij
klmno
pqrst
fguij
axcye
wvxyz

The IDs abcde and axcye are close, but they differ by two characters (the second
and fourth). However, the IDs fghij and fguij differ by exactly one character,
the third (h and u). Those must be the correct boxes.

What letters are common between the two correct box IDs? (In the example above,
this is found by removing the differing character from either ID, producing
fgij.)
*/

namespace App.Tasks.Year2018.Day2
{
    public class Part2 : ITask<string>
    {
        private readonly BoxIdsRepository boxIdsRepository;

        private readonly BoxIds boxIds;

        public Part2()
        {
            boxIdsRepository = new BoxIdsRepository();
            boxIds = new BoxIds();
        }

        public string Solution(string input)
        {
            string[] boxIds = boxIdsRepository.GetBoxIds(input);
            string commonLettersBetweenTwoCorrectBoxids = this.boxIds.GetCommonLettersBetweenTwoCorrectBoxIds(boxIds);

            return commonLettersBetweenTwoCorrectBoxids;
        }
    }
}
