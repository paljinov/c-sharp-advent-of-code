/*
--- Part Two ---

There isn't much you can do to prevent crashes in this ridiculous system.
However, by predicting the crashes, the Elves know where to be in advance and
instantly remove the two crashing carts the moment any crash occurs.

They can proceed like this for a while, but eventually, they're going to run out
of carts. It could be useful to figure out where the last cart that hasn't
crashed will end up.

For example:

/>-<\
|   |
| /<+-\
| | | v
\>+</ |
  |   ^
  \<->/

/---\
|   |
| v-+-\
| | | |
\-+-/ |
  |   |
  ^---^

/---\
|   |
| /-+-\
| v | |
\-+-/ |
  ^   ^
  \---/

/---\
|   |
| /-+-\
| | | |
\-+-/ ^
  |   |
  \---/

After four very expensive crashes, a tick ends with only one cart remaining; its
final location is 6,4.

What is the location of the last cart at the end of the first tick where it is
the only cart left?
*/

namespace App.Tasks.Year2018.Day13
{
    public class Part2 : ITask<string>
    {
        private readonly TracksMapRepository tracksMapRepository;

        private readonly Carts carts;

        public Part2()
        {
            tracksMapRepository = new TracksMapRepository();
            carts = new Carts();
        }
        public string Solution(string input)
        {
            char[,] tracksMap = tracksMapRepository.GetTracksMap(input);
            string locationOfTheLastCartThatHasNotCrashed = carts.FindLocationOfTheLastCartThatHasNotCrashed(tracksMap);

            return locationOfTheLastCartThatHasNotCrashed;
        }
    }
}
