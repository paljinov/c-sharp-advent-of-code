using System;
using System.Linq;

namespace App.Tasks.Year2018.Day24
{
    public class ImmuneSystemSimulator
    {
        public int CountWinningArmyUnits(Group[] immuneSystemArmy, Group[] infectionArmy)
        {
            while (immuneSystemArmy.Length > 0 && infectionArmy.Length > 0)
            {
                SortAttackers(immuneSystemArmy, infectionArmy);

                int remainingUnits = CalculateRemainingDefenderUnits(infectionArmy.First(), immuneSystemArmy.First());
            }

            return immuneSystemArmy.Length + infectionArmy.Length;
        }

        public int CountImmuneSystemUnitsWhichAreLeftAfterGettingTheSmallestBoostNeededToWin(
            Group[] immuneSystemArmy,
            Group[] infectionArmy
        )
        {
            return immuneSystemArmy.Length + infectionArmy.Length;
        }

        private void SortAttackers(Group[] immuneSystemArmy, Group[] infectionArmy)
        {
            immuneSystemArmy = immuneSystemArmy
                .OrderBy(g => g.Units * g.UnitAttackDamage)
                .ThenByDescending(g => g.Initiative)
                .ToArray();

            infectionArmy = infectionArmy
               .OrderBy(g => g.Units * g.UnitAttackDamage)
               .ThenByDescending(g => g.Initiative)
               .ToArray();
        }

        private int CalculateDamage(Group attacker, Group defender)
        {
            if (defender.Immunities.Contains(attacker.AttackType))
            {
                return 0;
            }

            int damage = attacker.Units * attacker.UnitAttackDamage;
            if (defender.Weaknesses.Contains(attacker.AttackType))
            {
                damage *= 2;
            }

            return damage;
        }

        private int CalculateRemainingDefenderUnits(Group attacker, Group defender)
        {
            int damage = CalculateDamage(attacker, defender);
            int remainingHitPoints = defender.Units * defender.UnitHitPoints - damage;

            int remainingUnits = (int)Math.Ceiling((double)remainingHitPoints / defender.UnitHitPoints);

            return remainingUnits;
        }
    }
}
