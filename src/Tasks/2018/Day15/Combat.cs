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
        private const int UNIT_HIT_POINTS = 200;

        public int CalculateCombatOutcome(char[,] map)
        {
            int combatOutcome = 0;

            List<Unit> units = InitializeUnits(map);

            return combatOutcome;
        }

        public int CalculateCombatOutcomeOfTheBattleWithoutAnyElvesDying(char[,] map)
        {
            int combatOutcomeOfTheBattleWithoutAnyElvesDying = 0;

            return combatOutcomeOfTheBattleWithoutAnyElvesDying;
        }

        public List<Unit> InitializeUnits(char[,] map)
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
                        int attackPower = UNIT_ATTACK_POWER;

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
    }
}
