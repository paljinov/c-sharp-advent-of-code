using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day15
{
    public class Combat
    {
        private const char OPEN_SPACE = '.';
        private const char WALL = '#';
        private const int UNIT_ATTACK_POWER = 3;
        private const int ELVES_LEAST_ATTACK_POWER_WITHOUT_ANY_DYING = 4;
        private const int UNIT_HIT_POINTS = 200;

        // Steps are ordered in reading order: top-to-bottom, then left-to-right
        private static readonly (int X, int Y)[] steps =
        {
            (-1, 0),
            (0, -1),
            (0, 1),
            (1, 0)
        };

        public int CalculateCombatOutcome(char[,] map)
        {
            List<Unit> units = InitializeUnits(map);
            int combatOutcome = Fight(map, units).Value;

            return combatOutcome;
        }

        public int CalculateCombatOutcomeOfTheBattleWithoutAnyElvesDying(char[,] initialMap)
        {
            return 0;
        }

        public List<Unit> InitializeUnits(char[,] map, int elvesAttackPower = UNIT_ATTACK_POWER)
        {
            List<Unit> units = new List<Unit>();

            List<char> openSpaceOrWall = new List<char>() { OPEN_SPACE, WALL };
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (!openSpaceOrWall.Contains(map[x, y]))
                    {
                        UnitType unitType = (UnitType)map[x, y];
                        int attackPower = unitType == UnitType.Goblin ? UNIT_ATTACK_POWER : elvesAttackPower;

                        Unit unit = new Unit
                        {
                            Position = (x, y),
                            UnitType = unitType,
                            HitPoints = UNIT_HIT_POINTS,
                            AttackPower = attackPower
                        };

                        units.Add(unit);
                    }
                }
            }

            return units;
        }

        public int? Fight(char[,] map, List<Unit> units, bool elvesWinWithoutSingleDeath = false)
        {
            int rounds = 0;
            while (true)
            {
                // Order units by reading order
                units = units.OrderBy(u => u.Position.X).ThenBy(u => u.Position.Y).ToList();
                List<Unit> killedUnits = new List<Unit>();

                foreach (Unit unit in units.ToList())
                {
                    // If unit was killed
                    if (killedUnits.Contains(unit))
                    {
                        continue;
                    }

                    // Find all targets of opposite unit types
                    IEnumerable<Unit> targets = units.Where(t => t.UnitType != unit.UnitType);
                    // If no targets remain, combat ends
                    if (!targets.Any())
                    {
                        int combatOutcome = rounds * units.Sum(u => u.HitPoints);
                        return combatOutcome;
                    }

                    // If the unit is not in range of a target
                    if (!targets.Any(t => AreUnitsAdjacent(unit, t)))
                    {
                        Move(map, unit, targets);
                    }

                    Unit adjacentTarget = Attack(unit, targets);

                    // If adjacent target was attacked, and it was killed
                    if (adjacentTarget != null && adjacentTarget.HitPoints <= 0)
                    {
                        // If elf should not die
                        if (elvesWinWithoutSingleDeath && adjacentTarget.UnitType == UnitType.Elf)
                        {
                            return null;
                        }

                        // If target is killed remove it from the map
                        units.Remove(adjacentTarget);
                        map[adjacentTarget.Position.X, adjacentTarget.Position.Y] = OPEN_SPACE;
                        killedUnits.Add(adjacentTarget);
                    }
                }

                rounds++;
            }
        }

        private void Move(char[,] map, Unit unit, IEnumerable<Unit> targets)
        {
            List<(int X, int Y)> positionsAdjacentToTargets = GetOpenPositionsAdjacentToTargets(map, targets);
            Dictionary<(int X, int Y), List<(int X, int Y)>> reacheablePositionsStartingFrom =
                GetReacheablePositionsStartingFrom(map, unit.Position);

            Dictionary<(int X, int Y), List<(int X, int Y)>> pathsToAdjacentPositions =
                new Dictionary<(int X, int Y), List<(int X, int Y)>>();
            foreach ((int X, int Y) positionAdjacentToTarget in positionsAdjacentToTargets)
            {
                List<(int X, int Y)> path = GetPathFromStartToEndPosition(
                    unit.Position, positionAdjacentToTarget, reacheablePositionsStartingFrom);

                // If there is path from start to end position
                if (path != null)
                {
                    pathsToAdjacentPositions[positionAdjacentToTarget] = path;
                }
            }

            // The unit takes a single step toward the chosen square along the shortest path to that square.
            // If multiple steps would put the unit equally closer to its destination, the unit chooses the step
            // which is first in reading order.
            (int X, int Y)? chosenPath = pathsToAdjacentPositions
                .Where(p => p.Value.Count > 0)
                .OrderBy(p => p.Value.Count)
                .ThenBy(p => p.Key.X)
                .ThenBy(p => p.Key.Y)
                .FirstOrDefault().Value?.FirstOrDefault();

            if (chosenPath != null)
            {
                // Move unit and update map
                map[unit.Position.X, unit.Position.Y] = OPEN_SPACE;
                unit.Position = chosenPath.Value;
                map[unit.Position.X, unit.Position.Y] = (char)unit.UnitType;
            }
        }

        private Unit Attack(Unit unit, IEnumerable<Unit> targets)
        {
            // To attack, the unit first determines all of the targets that are in range of it by being
            // immediately adjacent to it
            IEnumerable<Unit> adjacentTargets = targets.Where(t => AreUnitsAdjacent(unit, t));

            // If there are no adjacent targets, the unit ends its turn
            if (!adjacentTargets.Any())
            {
                return null;
            }

            // The adjacent target with the fewest hit points is selected; in a tie, the adjacent
            // target with the fewest hit points which is first in reading order is selected
            Unit adjacentTarget = adjacentTargets
                .OrderBy(at => at.HitPoints)
                .ThenBy(at => at.Position.X)
                .ThenBy(at => at.Position.Y)
                .First();

            adjacentTarget.HitPoints -= unit.AttackPower;

            return adjacentTarget;
        }

        private List<(int X, int Y)> GetOpenPositionsAdjacentToTargets(char[,] map, IEnumerable<Unit> targets)
        {
            List<(int X, int Y)> positionsAdjacentToTargets = new List<(int X, int Y)>();

            foreach (Unit target in targets)
            {
                foreach ((int X, int Y) step in steps)
                {
                    (int X, int Y) positionAdjacentToTarget = (target.Position.X + step.X, target.Position.Y + step.Y);
                    if (IsOpenSpace(map, positionAdjacentToTarget))
                    {
                        positionsAdjacentToTargets.Add(positionAdjacentToTarget);
                    }
                }
            }

            return positionsAdjacentToTargets;
        }

        private Dictionary<(int X, int Y), List<(int X, int Y)>> GetReacheablePositionsStartingFrom(
            char[,] map,
            (int X, int Y) from
        )
        {
            Dictionary<(int X, int Y), List<(int X, int Y)>> reacheablePositionsStartingFrom =
                new Dictionary<(int X, int Y), List<(int X, int Y)>>();

            Queue<(int X, int Y)> nextPositions = new Queue<(int X, int Y)>();
            nextPositions.Enqueue(from);

            while (nextPositions.Count > 0)
            {
                (int X, int Y) currentPosition = nextPositions.Dequeue();
                // Check possible steps
                foreach ((int X, int Y) step in steps)
                {
                    (int X, int Y) nextPosition = (currentPosition.X + step.X, currentPosition.Y + step.Y);
                    if (!reacheablePositionsStartingFrom.ContainsKey(nextPosition) && IsOpenSpace(map, nextPosition))
                    {
                        nextPositions.Enqueue(nextPosition);
                        if (!reacheablePositionsStartingFrom.ContainsKey(currentPosition))
                        {
                            reacheablePositionsStartingFrom[currentPosition] = new List<(int X, int Y)>();
                        }

                        reacheablePositionsStartingFrom[currentPosition].Add(nextPosition);
                    }
                }
            }

            return reacheablePositionsStartingFrom;
        }

        private List<(int X, int Y)> GetPathFromStartToEndPosition(
            (int X, int Y) from,
            (int X, int Y) to,
            Dictionary<(int X, int Y), List<(int X, int Y)>> reacheablePositionsStartingFrom
        )
        {
            List<(int X, int Y)> path = new List<(int X, int Y)>();

            // If end position is not reacheable, path is not possible
            if (!reacheablePositionsStartingFrom.Any(r => r.Value.Contains(to)))
            {
                return null;
            }

            (int x, int y) = to;
            // Until start position is reached
            while (x != from.X || y != from.Y)
            {
                path.Insert(0, (x, y));
                (x, y) = reacheablePositionsStartingFrom.First(r => r.Value.Contains((x, y))).Key;
            }

            return path;
        }

        private bool IsOpenSpace(char[,] map, (int X, int Y) position)
        {
            if (map[position.X, position.Y] != OPEN_SPACE)
            {
                return false;
            }

            return true;
        }

        private bool AreUnitsAdjacent(Unit first, Unit second)
        {
            int manhattanDistance = Math.Abs(first.Position.X - second.Position.X)
                + Math.Abs(first.Position.Y - second.Position.Y);

            return manhattanDistance == 1;
        }
    }
}
