using System;
using System.Collections.Generic;

namespace App.Tasks.Year2020.Day12
{
    public class ManhattanDistance
    {
        private const int SHIP_START_POSITION_X = 0;
        private const int SHIP_START_POSITION_Y = 0;
        private const CardinalDirection SHIP_START_FACING_DIRECTION = CardinalDirection.East;
        private const int MIN_ROTATION_DEGREES = 90;
        private const int WAYPOINT_START_POSITION_X = 10;
        private const int WAYPOINT_START_POSITION_Y = 1;

        public int CalculateBetweenStartAndEndPosition(List<NavigationInstruction> navigationInstructions)
        {
            int x = SHIP_START_POSITION_X;
            int y = SHIP_START_POSITION_Y;
            CardinalDirection facing = SHIP_START_FACING_DIRECTION;

            foreach (NavigationInstruction navigationInstruction in navigationInstructions)
            {
                switch (navigationInstruction.Action)
                {
                    case Action.MoveForward:
                        switch (facing)
                        {
                            case CardinalDirection.North:
                                y += navigationInstruction.Value;
                                break;
                            case CardinalDirection.South:
                                y -= navigationInstruction.Value;
                                break;
                            case CardinalDirection.East:
                                x += navigationInstruction.Value;
                                break;
                            case CardinalDirection.West:
                                x -= navigationInstruction.Value;
                                break;
                        }
                        break;
                    case Action.MoveNorth:
                        y += navigationInstruction.Value;
                        break;
                    case Action.MoveSouth:
                        y -= navigationInstruction.Value;
                        break;
                    case Action.MoveEast:
                        x += navigationInstruction.Value;
                        break;
                    case Action.MoveWest:
                        x -= navigationInstruction.Value;
                        break;
                    case Action.TurnLeft:
                        facing = GetFacingDirectionAfterTurnAction(facing, navigationInstruction);
                        break;
                    case Action.TurnRight:
                        facing = GetFacingDirectionAfterTurnAction(facing, navigationInstruction);
                        break;
                }
            }

            return Math.Abs(x) + Math.Abs(y);
        }

        public int CalculateBetweenStartAndEndPositionByMovingWaypoint(
            List<NavigationInstruction> navigationInstructions
        )
        {
            // Ship coordinates
            int x = SHIP_START_POSITION_X;
            int y = SHIP_START_POSITION_Y;

            // Waypoint coordinates relative to the ship's position
            int wx = WAYPOINT_START_POSITION_X;
            int wy = WAYPOINT_START_POSITION_Y;

            foreach (NavigationInstruction navigationInstruction in navigationInstructions)
            {
                switch (navigationInstruction.Action)
                {
                    case Action.MoveForward:
                        x += wx * navigationInstruction.Value;
                        y += wy * navigationInstruction.Value;
                        break;
                    case Action.MoveNorth:
                        wy += navigationInstruction.Value;
                        break;
                    case Action.MoveSouth:
                        wy -= navigationInstruction.Value;
                        break;
                    case Action.MoveEast:
                        wx += navigationInstruction.Value;
                        break;
                    case Action.MoveWest:
                        wx -= navigationInstruction.Value;
                        break;
                    case Action.TurnLeft:
                        (wx, wy) = CalculateWaypointCoordinatesAfterRotation(wx, wy, navigationInstruction);
                        break;
                    case Action.TurnRight:
                        (wx, wy) = CalculateWaypointCoordinatesAfterRotation(wx, wy, navigationInstruction);
                        break;
                }
            }

            return Math.Abs(x) + Math.Abs(y);
        }

        private CardinalDirection GetFacingDirectionAfterTurnAction(
            CardinalDirection facing,
            NavigationInstruction navigationInstruction
        )
        {
            int turns = navigationInstruction.Value / MIN_ROTATION_DEGREES;
            for (int i = 0; i < turns; i++)
            {
                if (navigationInstruction.Action == Action.TurnLeft)
                {
                    switch (facing)
                    {
                        case CardinalDirection.North:
                            facing = CardinalDirection.West;
                            break;
                        case CardinalDirection.South:
                            facing = CardinalDirection.East;
                            break;
                        case CardinalDirection.East:
                            facing = CardinalDirection.North;
                            break;
                        case CardinalDirection.West:
                            facing = CardinalDirection.South;
                            break;
                    }
                }
                else if (navigationInstruction.Action == Action.TurnRight)
                {
                    switch (facing)
                    {
                        case CardinalDirection.North:
                            facing = CardinalDirection.East;
                            break;
                        case CardinalDirection.South:
                            facing = CardinalDirection.West;
                            break;
                        case CardinalDirection.East:
                            facing = CardinalDirection.South;
                            break;
                        case CardinalDirection.West:
                            facing = CardinalDirection.North;
                            break;
                    }
                }
            }

            return facing;
        }

        private (int wx, int wy) CalculateWaypointCoordinatesAfterRotation(int wx, int wy, NavigationInstruction navigationInstruction)
        {
            int wxAfterRotation = 0;
            int wyAfterRotation = 0;

            int rotations = navigationInstruction.Value / MIN_ROTATION_DEGREES;
            for (int i = 0; i < rotations; i++)
            {
                // Rotate counter-clockwise
                if (navigationInstruction.Action == Action.TurnLeft)
                {
                    wxAfterRotation = -wy;
                    wyAfterRotation = wx;
                }
                // Rotate clockwise
                else if (navigationInstruction.Action == Action.TurnRight)
                {
                    wxAfterRotation = wy;
                    wyAfterRotation = -wx;
                }

                wx = wxAfterRotation;
                wy = wyAfterRotation;
            }

            return (wxAfterRotation, wyAfterRotation);
        }
    }
}
