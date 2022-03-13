using System.Linq;

namespace App.Tasks.Year2018.Day24
{
    public class ImmuneSystemSimulator
    {
        public int CountWinningArmyUnits(Group[] immuneSystemArmy, Group[] infectionArmy)
        {
            while (immuneSystemArmy.Length > 0 && infectionArmy.Length > 0)
            {
                SortAttackersByInitiative(immuneSystemArmy, infectionArmy);
            }

            return immuneSystemArmy.Length + infectionArmy.Length;
        }

        public int CountImmuneSystemUnitsWhichAreLeftAfterGettingTheSmallestBoostNeededToWin(
            Group[] immuneSystemArmy,
            Group[] infectionArmy
        )
        {
            while (immuneSystemArmy.Length > 0 && infectionArmy.Length > 0)
            {
                SortAttackersByInitiative(immuneSystemArmy, infectionArmy);
            }

            return immuneSystemArmy.Length + infectionArmy.Length;
        }

        private void SortAttackersByInitiative(Group[] immuneSystemArmy, Group[] infectionArmy)
        {
            immuneSystemArmy = immuneSystemArmy.OrderByDescending(isa => isa.Initiative).ToArray();
            infectionArmy = infectionArmy.OrderByDescending(ia => ia.Initiative).ToArray();
        }

        private void SortDefendersByEffectivePower(Group[] immuneSystemArmy, Group[] infectionArmy)
        {
            immuneSystemArmy = immuneSystemArmy
                .OrderBy(isa => isa.Units * isa.UnitAttackDamage)
                .ThenByDescending(isa => isa.Initiative)
                .ToArray();

            infectionArmy = infectionArmy
                .OrderBy(ia => ia.Units * ia.UnitAttackDamage)
                .ThenByDescending(ia => ia.Initiative)
                .ToArray();
        }
    }
}
