/*
--- Part Two ---

For some reason, your simulated results don't match what the experimental energy
source engineers expected. Apparently, the pocket dimension actually has four
spatial dimensions, not three.

The pocket dimension contains an infinite 4-dimensional grid. At every integer
4-dimensional coordinate (x,y,z,w), there exists a single cube (really, a
hypercube) which is still either active or inactive.

Each cube only ever considers its neighbors: any of the 80 other cubes where any
of their coordinates differ by at most 1. For example, given the cube at
x=1,y=2,z=3,w=4, its neighbors include the cube at x=2,y=2,z=3,w=3, the cube at
x=0,y=2,z=3,w=4, and so on.

The initial state of the pocket dimension still consists of a small flat region
of cubes. Furthermore, the same rules for cycle updating still apply: during
each cycle, consider the number of active neighbors of each cube.

For example, consider the same initial state as in the example above. Even
though the pocket dimension is 4-dimensional, this initial state represents a
small 2-dimensional slice of it. (In particular, this initial state defines a
3x3x1x1 region of the 4-dimensional space.)

Simulating a few cycles from this initial state produces the following
configurations, where the result of each cycle is shown layer-by-layer at each
given z and w coordinate:

Before any cycles:

z=0, w=0
.#.
..#
###


After 1 cycle:

z=-1, w=-1
#..
..#
.#.

z=0, w=-1
#..
..#
.#.

z=1, w=-1
#..
..#
.#.

z=-1, w=0
#..
..#
.#.

z=0, w=0
#.#
.##
.#.

z=1, w=0
#..
..#
.#.

z=-1, w=1
#..
..#
.#.

z=0, w=1
#..
..#
.#.

z=1, w=1
#..
..#
.#.


After 2 cycles:

z=-2, w=-2
.....
.....
..#..
.....
.....

z=-1, w=-2
.....
.....
.....
.....
.....

z=0, w=-2
###..
##.##
#...#
.#..#
.###.

z=1, w=-2
.....
.....
.....
.....
.....

z=2, w=-2
.....
.....
..#..
.....
.....

z=-2, w=-1
.....
.....
.....
.....
.....

z=-1, w=-1
.....
.....
.....
.....
.....

z=0, w=-1
.....
.....
.....
.....
.....

z=1, w=-1
.....
.....
.....
.....
.....

z=2, w=-1
.....
.....
.....
.....
.....

z=-2, w=0
###..
##.##
#...#
.#..#
.###.

z=-1, w=0
.....
.....
.....
.....
.....

z=0, w=0
.....
.....
.....
.....
.....

z=1, w=0
.....
.....
.....
.....
.....

z=2, w=0
###..
##.##
#...#
.#..#
.###.

z=-2, w=1
.....
.....
.....
.....
.....

z=-1, w=1
.....
.....
.....
.....
.....

z=0, w=1
.....
.....
.....
.....
.....

z=1, w=1
.....
.....
.....
.....
.....

z=2, w=1
.....
.....
.....
.....
.....

z=-2, w=2
.....
.....
..#..
.....
.....

z=-1, w=2
.....
.....
.....
.....
.....

z=0, w=2
###..
##.##
#...#
.#..#
.###.

z=1, w=2
.....
.....
.....
.....
.....

z=2, w=2
.....
.....
..#..
.....
.....

After the full six-cycle boot process completes, 848 cubes are left in the
active state.

Starting with your given initial configuration, simulate six cycles in a
4-dimensional space. How many cubes are left in the active state after the sixth
cycle?
*/

using System;

namespace App.Tasks.Year2020.Day17
{
    public class Part2 : ITask<int>
    {
        private const int INITIAL_Z = 0;

        private const int INITIAL_Z_LENGTH = 1;

        private const int INITIAL_W = 0;

        private const int INITIAL_W_LENGTH = 1;

        private const int TOTAL_CYCLES = 6;

        private readonly PocketSliceRepository pocketSliceRepository;

        public Part2()
        {
            pocketSliceRepository = new PocketSliceRepository();
        }

        public int Solution(string input)
        {
            bool[,] initialPocket2DimensionalSlice = pocketSliceRepository.GetInitialPocket2DimensionalSlice(input);
            int activeCubes = CountActiveCubesAfterCycles(initialPocket2DimensionalSlice);

            return activeCubes;
        }

        private int CountActiveCubesAfterCycles(bool[,] initialPocket2DimensionalSlice)
        {
            bool[,,,] pocket = GetInitialPocket(initialPocket2DimensionalSlice);

            for (int i = 1; i <= TOTAL_CYCLES; i++)
            {
                int xLength = pocket.GetLength(0) + 2;
                int yLength = pocket.GetLength(1) + 2;
                int zLength = pocket.GetLength(2) + 2;
                int wLength = pocket.GetLength(3) + 2;

                pocket = AdjustPocketToNewDimensions(pocket, xLength, yLength, zLength, wLength);
                bool[,,,] pocketAfterCycle = new bool[xLength, yLength, zLength, wLength];

                for (int x = 0; x < xLength; x++)
                {
                    for (int y = 0; y < yLength; y++)
                    {
                        for (int z = 0; z < zLength; z++)
                        {
                            for (int w = 0; w < wLength; w++)
                            {
                                pocketAfterCycle[x, y, z, w] = pocket[x, y, z, w];

                                int activeNeighbours = CountActiveNeighbours(x, y, z, w, pocket);
                                // If a cube is active and exactly 2 or 3 of its neighbors are also active,
                                // the cube remains active, otherwise the cube becomes inactive
                                if (pocket[x, y, z, w] && activeNeighbours != 2 && activeNeighbours != 3)
                                {
                                    pocketAfterCycle[x, y, z, w] = false;
                                }
                                // If a cube is inactive but exactly 3 of its neighbors are active,
                                // the cube becomes active, otherwise the cube remains inactive
                                else if (!pocket[x, y, z, w] && activeNeighbours == 3)
                                {
                                    pocketAfterCycle[x, y, z, w] = true;
                                }
                            }
                        }
                    }
                }

                pocket = pocketAfterCycle;
            }

            int activeCubes = CountActiveCubes(pocket);

            return activeCubes;
        }

        private bool[,,,] GetInitialPocket(bool[,] initialPocket2DimensionalSlice)
        {
            bool[,,,] pocket = new bool[
                initialPocket2DimensionalSlice.GetLength(0),
                initialPocket2DimensionalSlice.GetLength(1),
                INITIAL_Z_LENGTH,
                INITIAL_W_LENGTH
            ];

            for (int x = 0; x < initialPocket2DimensionalSlice.GetLength(0); x++)
            {
                for (int y = 0; y < initialPocket2DimensionalSlice.GetLength(1); y++)
                {
                    if (initialPocket2DimensionalSlice[x, y])
                    {
                        pocket[x, y, INITIAL_Z, INITIAL_W] = true;
                    }
                }
            }

            return pocket;
        }

        private bool[,,,] AdjustPocketToNewDimensions(
            bool[,,,] pocket,
            int xLength,
            int yLength,
            int zLength,
            int wLength
        )
        {
            bool[,,,] adjustedPocket = new bool[xLength, yLength, zLength, wLength];

            for (int x = 0; x < pocket.GetLength(0); x++)
            {
                for (int y = 0; y < pocket.GetLength(1); y++)
                {
                    for (int z = 0; z < pocket.GetLength(2); z++)
                    {
                        for (int w = 0; w < pocket.GetLength(3); w++)
                        {
                            int adjustedX = x + 1;
                            if (xLength == pocket.GetLength(0))
                            {
                                adjustedX = x;
                            }

                            int adjustedY = y + 1;
                            if (yLength == pocket.GetLength(1))
                            {
                                adjustedY = y;
                            }

                            adjustedPocket[adjustedX, adjustedY, z + 1, w + 1] = pocket[x, y, z, w];
                        }
                    }
                }
            }

            return adjustedPocket;
        }

        private int CountActiveNeighbours(int currentX, int currentY, int currentZ, int currentW, bool[,,,] pocket)
        {
            int activeNeighbours = 0;

            for (int x = 0; x < pocket.GetLength(0); x++)
            {
                for (int y = 0; y < pocket.GetLength(1); y++)
                {
                    for (int z = 0; z < pocket.GetLength(2); z++)
                    {
                        for (int w = 0; w < pocket.GetLength(3); w++)
                        {
                            // If not current cube
                            if (x != currentX || y != currentY || z != currentZ || w != currentW)
                            {
                                // If active neighbour
                                if (Math.Abs(x - currentX) <= 1 && Math.Abs(y - currentY) <= 1
                                    && Math.Abs(z - currentZ) <= 1 && Math.Abs(w - currentW) <= 1 && pocket[x, y, z, w])
                                {
                                    activeNeighbours++;
                                }
                            }
                        }
                    }
                }
            }

            return activeNeighbours;
        }

        private int CountActiveCubes(bool[,,,] pocket)
        {
            int activeCubes = 0;

            for (int x = 0; x < pocket.GetLength(0); x++)
            {
                for (int y = 0; y < pocket.GetLength(1); y++)
                {
                    for (int z = 0; z < pocket.GetLength(2); z++)
                    {
                        for (int w = 0; w < pocket.GetLength(3); w++)
                        {
                            if (pocket[x, y, z, w])
                            {
                                activeCubes++;
                            }
                        }
                    }
                }
            }

            return activeCubes;
        }
    }
}
