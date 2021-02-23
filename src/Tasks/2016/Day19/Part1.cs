/*
--- Day 19: An Elephant Named Joseph ---

The Elves contact you over a highly secure emergency channel. Back at the North
Pole, the Elves are busy misunderstanding White Elephant parties.

Each Elf brings a present. They all sit in a circle, numbered starting with
position 1. Then, starting with the first Elf, they take turns stealing all the
presents from the Elf to their left. An Elf with no presents is removed from the
circle and does not take turns.

For example, with five Elves (numbered 1 to 5):

  1
5   2
 4 3

- Elf 1 takes Elf 2's present.
- Elf 2 has no presents and is skipped.
- Elf 3 takes Elf 4's present.
- Elf 4 has no presents and is also skipped.
- Elf 5 takes Elf 1's two presents.
- Neither Elf 1 nor Elf 2 have any presents, so both are skipped.
- Elf 3 takes Elf 5's three presents.

So, with five Elves, the Elf that sits starting in position 3 gets all the
presents.

With the number of Elves given in your puzzle input, which Elf gets all the
presents?
*/

namespace App.Tasks.Year2016.Day19
{
    public class Part1 : ITask<int>
    {
        private readonly ElvesRepository elvesRepository;

        private readonly Elves elves;

        public Part1()
        {
            elvesRepository = new ElvesRepository();
            elves = new Elves();
        }

        public int Solution(string input)
        {
            int totalElves = elvesRepository.GetTotalElves(input);
            int elfWhichGetsAllThePresents = elves.FindElfWhichGetsAllThePresents(totalElves);

            return elfWhichGetsAllThePresents;
        }
    }
}
