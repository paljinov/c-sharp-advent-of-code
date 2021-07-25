/*
--- Part Two ---

You aren't sure how large Santa's ship is. You aren't even sure if you'll need
to use this thing on Santa's ship, but it doesn't hurt to be prepared. You
figure Santa's ship might fit in a 100x100 square.

The beam gets wider as it travels away from the emitter; you'll need to be a
minimum distance away to fit a square of that size into the beam fully. (Don't
rotate the square; it should be aligned to the same axes as the drone grid.)

For example, suppose you have the following tractor beam readings:

#.......................................
.#......................................
..##....................................
...###..................................
....###.................................
.....####...............................
......#####.............................
......######............................
.......#######..........................
........########........................
.........#########......................
..........#########.....................
...........##########...................
...........############.................
............############................
.............#############..............
..............##############............
...............###############..........
................###############.........
................#################.......
.................########OOOOOOOOOO.....
..................#######OOOOOOOOOO#....
...................######OOOOOOOOOO###..
....................#####OOOOOOOOOO#####
.....................####OOOOOOOOOO#####
.....................####OOOOOOOOOO#####
......................###OOOOOOOOOO#####
.......................##OOOOOOOOOO#####
........................#OOOOOOOOOO#####
.........................OOOOOOOOOO#####
..........................##############
..........................##############
...........................#############
............................############
.............................###########

In this example, the 10x10 square closest to the emitter that fits entirely
within the tractor beam has been marked O. Within it, the point closest to the
emitter (the only highlighted O) is at X=25, Y=20.

Find the 100x100 square closest to the emitter that fits entirely within the
tractor beam; within that square, find the point closest to the emitter. What
value do you get if you take that point's X coordinate, multiply it by 10000,
then add the point's Y coordinate? (In the example above, this would be 250020.)
*/

namespace App.Tasks.Year2019.Day19
{
    public class Part2 : ITask<int>
    {
        private const int SQUARE_SIZE = 100;

        private const int MULTIPLIER = 10000;

        private readonly IntegersRepository integersRepository;

        private readonly TractorBeam tractorBeam;

        public Part2()
        {
            integersRepository = new IntegersRepository();
            tractorBeam = new TractorBeam();
        }

        public int Solution(string input)
        {
            long[] integers = integersRepository.GetIntegers(input);
            int result = tractorBeam.CalculateResultForClosestPointToEmitterOfSquareThatFitsEntirelyWithinTractorBeam(
                integers, SQUARE_SIZE, MULTIPLIER);

            return result;
        }
    }
}
