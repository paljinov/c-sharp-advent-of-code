using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day13
{
    public class Carts
    {
        private const char FACE_UP = '^';

        private const char FACE_DOWN = 'v';

        private const char FACE_LEFT = '<';

        private const char FACE_RIGHT = '>';

        private const char INTERSECTION = '+';

        private const char VERTICAL_STRAIGHT_PATH = '|';

        private const char HORIZONTAL_STRAIGHT_PATH = '-';

        private const char SLASH_CURVE = '/';

        private const char BACKSLASH_CURVE = '\\';

        private const int TURN_OPTIONS = 3;

        private readonly char[] straightPaths = new char[] { VERTICAL_STRAIGHT_PATH, HORIZONTAL_STRAIGHT_PATH };

        private readonly char[] curves = new char[] { SLASH_CURVE, BACKSLASH_CURVE };

        public string FindFirstCrashLocation(char[,] tracksMap)
        {
            Dictionary<(int X, int Y), Facing> carts = FindCarts(tracksMap);
            (int X, int Y) firstCrashLocation = DoFindFirstCrashLocation(
                carts,
                tracksMap,
                carts.First().Key,
                carts.First().Value,
                IntersectionTurnOption.Left
            );

            return $"{firstCrashLocation.Y},{firstCrashLocation.X}";
        }

        public string FindLocationOfTheLastCartThatHasNotCrashed(char[,] tracksMap)
        {
            return string.Empty;
        }

        private Dictionary<(int X, int Y), Facing> FindCarts(char[,] tracksMap)
        {
            Dictionary<(int X, int Y), Facing> carts = new Dictionary<(int X, int Y), Facing>();

            for (int x = 0; x < tracksMap.GetLength(0); x++)
            {
                for (int y = 0; y < tracksMap.GetLength(1); y++)
                {
                    switch (tracksMap[x, y])
                    {
                        case FACE_UP:
                            carts[(x, y)] = Facing.Up;
                            tracksMap[x, y] = VERTICAL_STRAIGHT_PATH;
                            break;
                        case FACE_DOWN:
                            carts[(x, y)] = Facing.Down;
                            tracksMap[x, y] = VERTICAL_STRAIGHT_PATH;
                            break;
                        case FACE_LEFT:
                            carts[(x, y)] = Facing.Left;
                            tracksMap[x, y] = HORIZONTAL_STRAIGHT_PATH;
                            break;
                        case FACE_RIGHT:
                            carts[(x, y)] = Facing.Right;
                            tracksMap[x, y] = HORIZONTAL_STRAIGHT_PATH;
                            break;
                    }
                }
            }

            return carts;
        }

        private (int X, int Y) DoFindFirstCrashLocation(
            Dictionary<(int X, int Y), Facing> carts,
            char[,] tracksMap,
            (int X, int Y) currentCartLocation,
            Facing currentCartFacing,
            IntersectionTurnOption currentCartNextTurn
        )
        {
            (int X, int Y) firstCrashLocation = (0, 0);

            Facing nextFacing = currentCartFacing;
            int nextTurn = (int)currentCartNextTurn;

            switch (currentCartFacing)
            {
                case Facing.Up:
                    if (carts.ContainsKey((currentCartLocation.X, currentCartLocation.Y - 1)))
                    {
                        firstCrashLocation = (currentCartLocation.X, currentCartLocation.Y - 1);
                        break;
                    }

                    if (tracksMap[currentCartLocation.X, currentCartLocation.Y - 1] == SLASH_CURVE)
                    {
                        nextFacing = Facing.Right;
                    }
                    else if (tracksMap[currentCartLocation.X, currentCartLocation.Y - 1] == BACKSLASH_CURVE)
                    {
                        nextFacing = Facing.Left;
                    }
                    else if (tracksMap[currentCartLocation.X, currentCartLocation.Y - 1] == INTERSECTION)
                    {
                        nextTurn += 1;
                        if (nextTurn >= TURN_OPTIONS)
                        {
                            nextTurn = 0;
                        }

                        switch (currentCartNextTurn)
                        {
                            case IntersectionTurnOption.Left:
                                nextFacing = Facing.Left;
                                break;
                            case IntersectionTurnOption.Right:
                                nextFacing = Facing.Right;
                                break;
                        }
                    }

                    firstCrashLocation = DoFindFirstCrashLocation(
                        carts,
                        tracksMap,
                        (currentCartLocation.X, currentCartLocation.Y - 1),
                        nextFacing,
                        (IntersectionTurnOption)nextTurn
                    );
                    break;
                case Facing.Down:
                    if (carts.ContainsKey((currentCartLocation.X, currentCartLocation.Y + 1)))
                    {
                        firstCrashLocation = (currentCartLocation.X, currentCartLocation.Y + 1);
                        break;
                    }

                    if (tracksMap[currentCartLocation.X, currentCartLocation.Y + 1] == SLASH_CURVE)
                    {
                        nextFacing = Facing.Left;
                    }
                    else if (tracksMap[currentCartLocation.X, currentCartLocation.Y + 1] == BACKSLASH_CURVE)
                    {
                        nextFacing = Facing.Right;
                    }
                    else if (tracksMap[currentCartLocation.X, currentCartLocation.Y + 1] == INTERSECTION)
                    {
                        nextTurn += 1;
                        if (nextTurn >= TURN_OPTIONS)
                        {
                            nextTurn = 0;
                        }

                        switch (currentCartNextTurn)
                        {
                            case IntersectionTurnOption.Left:
                                nextFacing = Facing.Right;
                                break;
                            case IntersectionTurnOption.Right:
                                nextFacing = Facing.Left;
                                break;
                        }
                    }

                    firstCrashLocation = DoFindFirstCrashLocation(
                        carts,
                        tracksMap,
                        (currentCartLocation.X, currentCartLocation.Y + 1),
                        nextFacing,
                        (IntersectionTurnOption)nextTurn
                    );
                    break;
                case Facing.Left:
                    if (carts.ContainsKey((currentCartLocation.X - 1, currentCartLocation.Y)))
                    {
                        firstCrashLocation = (currentCartLocation.X - 1, currentCartLocation.Y);
                        break;
                    }

                    if (tracksMap[currentCartLocation.X - 1, currentCartLocation.Y] == SLASH_CURVE)
                    {
                        nextFacing = Facing.Down;
                    }
                    else if (tracksMap[currentCartLocation.X - 1, currentCartLocation.Y] == BACKSLASH_CURVE)
                    {
                        nextFacing = Facing.Up;
                    }
                    else if (tracksMap[currentCartLocation.X - 1, currentCartLocation.Y] == INTERSECTION)
                    {
                        nextTurn += 1;
                        if (nextTurn >= TURN_OPTIONS)
                        {
                            nextTurn = 0;
                        }

                        switch (currentCartNextTurn)
                        {
                            case IntersectionTurnOption.Left:
                                nextFacing = Facing.Down;
                                break;
                            case IntersectionTurnOption.Right:
                                nextFacing = Facing.Up;
                                break;
                        }
                    }

                    firstCrashLocation = DoFindFirstCrashLocation(
                        carts,
                        tracksMap,
                        (currentCartLocation.X - 1, currentCartLocation.Y),
                        nextFacing,
                        (IntersectionTurnOption)nextTurn
                    );
                    break;
                case Facing.Right:
                    if (carts.ContainsKey((currentCartLocation.X + 1, currentCartLocation.Y)))
                    {
                        firstCrashLocation = (currentCartLocation.X + 1, currentCartLocation.Y);
                        break;
                    }

                    if (tracksMap[currentCartLocation.X + 1, currentCartLocation.Y] == SLASH_CURVE)
                    {
                        nextFacing = Facing.Up;
                    }
                    else if (tracksMap[currentCartLocation.X + 1, currentCartLocation.Y] == BACKSLASH_CURVE)
                    {
                        nextFacing = Facing.Down;
                    }
                    else if (tracksMap[currentCartLocation.X + 1, currentCartLocation.Y] == INTERSECTION)
                    {
                        nextTurn += 1;
                        if (nextTurn >= TURN_OPTIONS)
                        {
                            nextTurn = 0;
                        }

                        switch (currentCartNextTurn)
                        {
                            case IntersectionTurnOption.Left:
                                nextFacing = Facing.Up;
                                break;
                            case IntersectionTurnOption.Right:
                                nextFacing = Facing.Down;
                                break;
                        }
                    }

                    firstCrashLocation = DoFindFirstCrashLocation(
                        carts,
                        tracksMap,
                        (currentCartLocation.X + 1, currentCartLocation.Y),
                        nextFacing,
                        (IntersectionTurnOption)nextTurn
                    );
                    break;
            }

            return firstCrashLocation;
        }
    }
}
