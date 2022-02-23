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
            ((int X, int Y)? firstCrashLocation, _) = MoveCarts(tracksMap);
            return $"{firstCrashLocation.Value.Y},{firstCrashLocation.Value.X}";
        }

        public string FindLocationOfTheLastCartThatHasNotCrashed(char[,] tracksMap)
        {
            (_, (int X, int Y)? lastNonCrashedCartLocation) = MoveCarts(tracksMap);
            return $"{lastNonCrashedCartLocation.Value.Y},{lastNonCrashedCartLocation.Value.X}";
        }

        private ((int X, int Y)? FirstCrashLocation, (int X, int Y)? LastNonCrashedCartLocation) MoveCarts(
            char[,] tracksMap
        )
        {
            (int X, int Y)? firstCrashLocation = null;
            (int X, int Y)? lastNonCrashedCartLocation = null;

            Dictionary<int, Cart> carts = FindCarts(tracksMap)
                .Select((cart, index) => new { cart, index })
                .ToDictionary(c => c.index, c => c.cart);

            while (carts.Count > 1)
            {
                foreach (int cartIndex in carts.Keys)
                {
                    Cart cartAfterMove = MoveCart(tracksMap, carts[cartIndex]);

                    // Check if crash location
                    if (carts.Select(c => c.Value.Location).Contains(cartAfterMove.Location))
                    {
                        if (!firstCrashLocation.HasValue)
                        {
                            firstCrashLocation = (cartAfterMove.Location.X, cartAfterMove.Location.Y);
                        }

                        int secondCrashedCartIndex = carts.First(c => c.Value.Location == cartAfterMove.Location).Key;
                        // Remove crashed pair
                        carts.Remove(cartIndex);
                        carts.Remove(secondCrashedCartIndex);
                    }
                    else
                    {
                        carts[cartIndex] = cartAfterMove;
                    }
                }

                // Carts on the top row move first (acting from left to right)
                carts = carts
                    .OrderBy(c => c.Value.Location.Y)
                    .ThenBy(c => c.Value.Location.X)
                    .ToDictionary(c => c.Key, c => c.Value);
            }


            if (carts.Count > 0)
            {
                lastNonCrashedCartLocation = carts.First().Value.Location;
            }

            return (firstCrashLocation, lastNonCrashedCartLocation);
        }

        private List<Cart> FindCarts(char[,] tracksMap)
        {
            List<Cart> carts = new List<Cart>();

            char[] cartsFacings = new char[] { FACE_UP, FACE_DOWN, FACE_LEFT, FACE_RIGHT };

            for (int x = 0; x < tracksMap.GetLength(0); x++)
            {
                for (int y = 0; y < tracksMap.GetLength(1); y++)
                {
                    if (cartsFacings.Contains(tracksMap[x, y]))
                    {
                        Facing facing = Facing.Up;

                        switch (tracksMap[x, y])
                        {
                            case FACE_UP:
                                facing = Facing.Up;
                                tracksMap[x, y] = VERTICAL_STRAIGHT_PATH;
                                break;
                            case FACE_DOWN:
                                facing = Facing.Down;
                                tracksMap[x, y] = VERTICAL_STRAIGHT_PATH;
                                break;
                            case FACE_LEFT:
                                facing = Facing.Left;
                                tracksMap[x, y] = HORIZONTAL_STRAIGHT_PATH;
                                break;
                            case FACE_RIGHT:
                                facing = Facing.Right;
                                tracksMap[x, y] = HORIZONTAL_STRAIGHT_PATH;
                                break;
                        }

                        Cart cart = new Cart
                        {
                            Location = (x, y),
                            Facing = facing,
                            IntersectionTurnOption = IntersectionTurnOption.Left
                        };

                        carts.Add(cart);
                    }
                }
            }

            return carts;
        }

        private Cart MoveCart(char[,] tracksMap, Cart cart)
        {
            (int X, int Y) nextLocation = cart.Location;
            Facing nextFacing = cart.Facing;
            int nextTurn = (int)cart.IntersectionTurnOption;

            switch (cart.Facing)
            {
                case Facing.Up:
                    nextLocation = (cart.Location.X - 1, cart.Location.Y);

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

                        switch (cart.IntersectionTurnOption)
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
                    nextLocation = (cart.Location.X + 1, cart.Location.Y);

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

                        switch (cart.IntersectionTurnOption)
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
                    nextLocation = (cart.Location.X, cart.Location.Y - 1);

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

                        switch (cart.IntersectionTurnOption)
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
                    nextLocation = (cart.Location.X, cart.Location.Y + 1);

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

                        switch (cart.IntersectionTurnOption)
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

            Cart cartAfterMove = new Cart
            {
                Location = nextLocation,
                Facing = nextFacing,
                IntersectionTurnOption = (IntersectionTurnOption)nextTurn
            };

            return cartAfterMove;
        }
    }
}
