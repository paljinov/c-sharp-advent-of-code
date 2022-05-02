using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Tasks.Year2018.Day24
{
    public class ImmuneSystemSimulator
    {
        public int CountWinningArmyUnits(List<Group> armiesGroups)
        {
            List<Group> winnerGroups = Fight(armiesGroups);
            int winningArmyUnits = winnerGroups.Sum(g => g.Units);

            return winningArmyUnits;
        }

        public int CountImmuneSystemUnitsWhichAreLeftAfterGettingTheSmallestBoostNeededToWin(List<Group> armiesGroups)
        {
            return 0;
        }

        private List<Group> Fight(List<Group> groups)
        {
            int previousTotalUnits = groups.Sum(g => g.Units);

            while (!IsFightFinished(groups))
            {
                Dictionary<Group, Group> selectedTargets = TargetSelection(groups);
                Attack(groups, selectedTargets);

                int totalUnits = groups.Sum(g => g.Units);
                // If number of units didn't change deadlock happened
                if (previousTotalUnits == totalUnits)
                {
                    // No winners in case of deadlock
                    return new List<Group>();
                }

                previousTotalUnits = totalUnits;
            }

            return groups;
        }

        public bool IsFightFinished(List<Group> groups)
        {
            // Fight is finished when only one group type remains
            return groups.GroupBy(g => g.GroupType).Count() == 1;
        }

        private Dictionary<Group, Group> TargetSelection(List<Group> groups)
        {
            // In decreasing order of effective power, groups choose their selectedTargets; in a tie,
            // the group with the higher initiative chooses first
            IEnumerable<Group> attackers = groups.ToList()
               .OrderByDescending(g => g.EffectivePower)
               .ThenByDescending(g => g.Initiative);

            List<Group> potentialTargets = groups.ToList();
            Dictionary<Group, Group> selectedTargets = new Dictionary<Group, Group>();

            foreach (Group attacker in attackers)
            {
                Group target = potentialTargets
                    .Where(pt => attacker.GroupType != pt.GroupType)
                    // If it cannot deal any defending groups damage, it does not choose a target
                    .Where(pt => CalculateDamage(attacker, pt) > 0)
                    // The attacking group chooses to target the group in the enemy army
                    // to which it would deal the most damage
                    .OrderByDescending(pt => CalculateDamage(attacker, pt))
                    // If an attacking group is considering two defending groups to which it would deal equal damage,
                    // it chooses to target the defending group with the largest effective power;
                    // if there is still a tie, it chooses the defending group with the highest initiative
                    .ThenByDescending(pt => pt.EffectivePower)
                    .ThenByDescending(pt => pt.Initiative)
                    .FirstOrDefault();

                if (target != null)
                {
                    selectedTargets[attacker] = target;
                    potentialTargets.Remove(target);
                }
            }

            return selectedTargets;
        }

        private void Attack(List<Group> groups, Dictionary<Group, Group> selectedTargets)
        {
            // Groups attack in decreasing order of initiative
            IEnumerable<Group> attackers = groups.OrderByDescending(g => g.Initiative);

            foreach (Group attacker in attackers)
            {
                if (selectedTargets.ContainsKey(attacker))
                {
                    Group defender = selectedTargets[attacker];

                    int damage = CalculateDamage(attacker, defender);
                    // The defending group only loses whole units from damage
                    int killedUnits = Math.Min(defender.Units, damage / defender.UnitHitPoints);

                    defender.Units -= killedUnits;
                    if (defender.Units <= 0)
                    {
                        groups.Remove(selectedTargets[attacker]);
                    }
                }
            }
        }

        public int CalculateDamage(Group attacker, Group defender)
        {
            if (defender.Immunities.Contains(attacker.AttackType))
            {
                return 0;
            }

            if (defender.Weaknesses.Contains(attacker.AttackType))
            {
                return 2 * attacker.EffectivePower;
            }

            return attacker.EffectivePower;
        }
    }
}
