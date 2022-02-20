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

        public string FindFirstCrashLocation(char[,] tracksMap)
        {
            (int X, int Y)? firstCrashLocation = null;

            Dictionary<(int X, int Y), (Facing, IntersectionTurnOption)> carts = FindCarts(tracksMap);

            while (!firstCrashLocation.HasValue)
            {
                Dictionary<(int X, int Y), (Facing, IntersectionTurnOption)> cartsAfterMove =
                    new Dictionary<(int X, int Y), (Facing, IntersectionTurnOption)>();

                foreach (KeyValuePair<(int X, int Y), (Facing, IntersectionTurnOption)> cart in carts)
                {
                    ((int X, int Y) location, Facing facing, IntersectionTurnOption intersectionTurnOption)
                        = MoveCart(tracksMap, cart.Key, cart.Value.Item1, cart.Value.Item2);

                    if (carts.ContainsKey(location) || cartsAfterMove.ContainsKey(location))
                    {
                        firstCrashLocation = location;
                        break;
                    }

                    cartsAfterMove[location] = (facing, intersectionTurnOption);
                }

                carts = cartsAfterMove;
            }

            return $"{firstCrashLocation.Value.Y},{firstCrashLocation.Value.X}";
        }

        public string FindLocationOfTheLastCartThatHasNotCrashed(char[,] tracksMap)
        {
            return string.Empty;
        }

        private Dictionary<(int X, int Y), (Facing, IntersectionTurnOption)> FindCarts(char[,] tracksMap)
        {
            Dictionary<(int X, int Y), (Facing, IntersectionTurnOption)> carts =
                new Dictionary<(int X, int Y), (Facing, IntersectionTurnOption)>();

            for (int x = 0; x < tracksMap.GetLength(0); x++)
            {
                for (int y = 0; y < tracksMap.GetLength(1); y++)
                {
                    switch (tracksMap[x, y])
                    {
                        case FACE_UP:
                            carts[(x, y)] = (Facing.Up, IntersectionTurnOption.Left);
                            tracksMap[x, y] = VERTICAL_STRAIGHT_PATH;
                            break;
                        case FACE_DOWN:
                            carts[(x, y)] = (Facing.Down, IntersectionTurnOption.Left);
                            tracksMap[x, y] = VERTICAL_STRAIGHT_PATH;
                            break;
                        case FACE_LEFT:
                            carts[(x, y)] = (Facing.Left, IntersectionTurnOption.Left);
                            tracksMap[x, y] = HORIZONTAL_STRAIGHT_PATH;
                            break;
                        case FACE_RIGHT:
                            carts[(x, y)] = (Facing.Right, IntersectionTurnOption.Left);
                            tracksMap[x, y] = HORIZONTAL_STRAIGHT_PATH;
                            break;
                    }
                }
            }

            return carts;
        }

        private ((int X, int Y) location, Facing facing, IntersectionTurnOption intersectionTurnOption) MoveCart(
            char[,] tracksMap,
            (int X, int Y) currentCartLocation,
            Facing currentCartFacing,
            IntersectionTurnOption currentCartNextTurn
        )
        {
            (int X, int Y) nextLocation = currentCartLocation;
            Facing nextFacing = currentCartFacing;
            int nextTurn = (int)currentCartNextTurn;

            switch (currentCartFacing)
            {
                case Facing.Up:
                    nextLocation = (currentCartLocation.X - 1, currentCartLocation.Y);

                    if (tracksMap[nextLocation.X, nextLocation.Y] == SLASH_CURVE)
                    {
                        nextFacing = Facing.Right;
                    }
                    else if (tracksMap[nextLocation.X, nextLocation.Y] == BACKSLASH_CURVE)
                    {
                        nextFacing = Facing.Left;
                    }
                    else if (tracksMap[nextLocation.X, nextLocation.Y] == INTERSECTION)
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
                    break;
                case Facing.Down:
                    nextLocation = (currentCartLocation.X + 1, currentCartLocation.Y);

                    if (tracksMap[nextLocation.X, nextLocation.Y] == SLASH_CURVE)
                    {
                        nextFacing = Facing.Left;
                    }
                    else if (tracksMap[nextLocation.X, nextLocation.Y] == BACKSLASH_CURVE)
                    {
                        nextFacing = Facing.Right;
                    }
                    else if (tracksMap[nextLocation.X, nextLocation.Y] == INTERSECTION)
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
                    break;
                case Facing.Left:
                    nextLocation = (currentCartLocation.X, currentCartLocation.Y - 1);

                    if (tracksMap[nextLocation.X, nextLocation.Y] == SLASH_CURVE)
                    {
                        nextFacing = Facing.Down;
                    }
                    else if (tracksMap[nextLocation.X, nextLocation.Y] == BACKSLASH_CURVE)
                    {
                        nextFacing = Facing.Up;
                    }
                    else if (tracksMap[nextLocation.X, nextLocation.Y] == INTERSECTION)
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
                    break;
                case Facing.Right:
                    nextLocation = (currentCartLocation.X, currentCartLocation.Y + 1);

                    if (tracksMap[nextLocation.X, nextLocation.Y] == SLASH_CURVE)
                    {
                        nextFacing = Facing.Up;
                    }
                    else if (tracksMap[nextLocation.X, nextLocation.Y] == BACKSLASH_CURVE)
                    {
                        nextFacing = Facing.Down;
                    }
                    else if (tracksMap[nextLocation.X, nextLocation.Y] == INTERSECTION)
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
                    break;
            }

            return (nextLocation, nextFacing, (IntersectionTurnOption)nextTurn);
        }
    }
}
