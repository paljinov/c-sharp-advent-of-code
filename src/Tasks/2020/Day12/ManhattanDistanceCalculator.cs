using System;
using System.Collections.Generic;

namespace App.Tasks.Year2020.Day12
{
    public class ManhattanDistanceCalculator
    {
        private const int SHIP_START_POSITION_X = 0;
        private const int SHIP_START_POSITION_Y = 0;
        private const Direction SHIP_START_FACING_DIRECTION = Direction.East;
        private const int WAYPOINT_START_POSITION_X = 10;
        private const int WAYPOINT_START_POSITION_Y = 1;

        public int BetweenStartAndEndPosition(List<Action> actions)
        {
            int x = SHIP_START_POSITION_X;
            int y = SHIP_START_POSITION_Y;
            Direction facing = SHIP_START_FACING_DIRECTION;

            foreach (Action action in actions)
            {
                switch (action.Direction)
                {
                    case Direction.Forward:
                        switch (facing)
                        {
                            case Direction.North:
                                y += action.Value;
                                break;
                            case Direction.South:
                                y -= action.Value;
                                break;
                            case Direction.East:
                                x += action.Value;
                                break;
                            case Direction.West:
                                x -= action.Value;
                                break;
                        }
                        break;
                    case Direction.North:
                        y += action.Value;
                        break;
                    case Direction.South:
                        y -= action.Value;
                        break;
                    case Direction.East:
                        x += action.Value;
                        break;
                    case Direction.West:
                        x -= action.Value;
                        break;
                    case Direction.Left:
                        facing = GetFacingDirection(facing, action);
                        break;
                    case Direction.Right:
                        facing = GetFacingDirection(facing, action);
                        break;
                }
            }

            return Math.Abs(x) + Math.Abs(y);
        }

        public int BetweenStartAndEndPositionByMovingWaypoint(List<Action> actions)
        {
            // Ship coordinates
            int x = SHIP_START_POSITION_X;
            int y = SHIP_START_POSITION_Y;

            // Waypoint coordinates relative to the ship's position
            int wx = WAYPOINT_START_POSITION_X;
            int wy = WAYPOINT_START_POSITION_Y;

            foreach (Action action in actions)
            {
                switch (action.Direction)
                {
                    case Direction.Forward:
                        x += wx * action.Value;
                        y += wy * action.Value;
                        break;
                    case Direction.North:
                        wy += action.Value;
                        break;
                    case Direction.South:
                        wy -= action.Value;
                        break;
                    case Direction.East:
                        wx += action.Value;
                        break;
                    case Direction.West:
                        wx -= action.Value;
                        break;
                    case Direction.Left:
                        (wx, wy) = CalculateWaypointCoordinatesAfterRotation(wx, wy, action);
                        break;
                    case Direction.Right:
                        (wx, wy) = CalculateWaypointCoordinatesAfterRotation(wx, wy, action);
                        break;
                }
            }

            return Math.Abs(x) + Math.Abs(y);
        }

        private Direction GetFacingDirection(Direction facing, Action action)
        {
            int directionChanges = action.Value / 90;

            for (int i = 0; i < directionChanges; i++)
            {
                if (action.Direction == Direction.Left)
                {
                    switch (facing)
                    {
                        case Direction.North:
                            facing = Direction.West;
                            break;
                        case Direction.South:
                            facing = Direction.East;
                            break;
                        case Direction.East:
                            facing = Direction.North;
                            break;
                        case Direction.West:
                            facing = Direction.South;
                            break;
                    }
                }
                else if (action.Direction == Direction.Right)
                {
                    switch (facing)
                    {
                        case Direction.North:
                            facing = Direction.East;
                            break;
                        case Direction.South:
                            facing = Direction.West;
                            break;
                        case Direction.East:
                            facing = Direction.South;
                            break;
                        case Direction.West:
                            facing = Direction.North;
                            break;
                    }
                }
            }

            return facing;
        }

        private (int wx, int wy) CalculateWaypointCoordinatesAfterRotation(int wx, int wy, Action action)
        {
            int wxAfterRotation = 0;
            int wyAfterRotation = 0;

            int directionChanges = action.Value / 90;

            for (int i = 0; i < directionChanges; i++)
            {
                // Rotate counter-clockwise
                if (action.Direction == Direction.Left)
                {
                    wxAfterRotation = -wy;
                    wyAfterRotation = wx;
                }
                // Rotate clockwise
                else if (action.Direction == Direction.Right)
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
