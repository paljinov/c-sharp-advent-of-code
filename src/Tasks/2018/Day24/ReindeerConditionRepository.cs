using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2018.Day24
{
    public class ReindeerConditionRepository
    {
        public Group[] GetImmuneSystemArmy(string input)
        {
            string[] inputParts = ParseInput(input);

            Group[] groups = GetArmyGroups(inputParts[0]);

            return groups;
        }

        public Group[] GetInfectionArmy(string input)
        {
            string[] inputParts = ParseInput(input);

            Group[] groups = GetArmyGroups(inputParts[1]);

            return groups;
        }

        private Group[] GetArmyGroups(string input)
        {
            Regex armyRegex = new Regex(@"^(\d+\sunits)\seach\swith\s(\d+\shit\spoints)\s\((weak\sto\sfire;\simmune\sto\scold\))\swith\san\sattack\sthat\sdoes\s(\d+\sradiation\sdamage)\sat\s(initiative\s\d+)$");
            string[] groupsArray = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            Group[] groups = new Group[input.Length - 1];

            for (int i = 1; i < groupsArray.Length; i++)
            {
                Match armyMatch = armyRegex.Match(groupsArray[i]);
                GroupCollection groupCollection = armyMatch.Groups;

                Group group = new Group
                {
                    Units = int.Parse(groupCollection[1].Value),
                    UnitHitPoints = int.Parse(groupCollection[2].Value),
                    UnitAttackDamage = int.Parse(groupCollection[3].Value),
                    AttackType = groupCollection[4].Value,
                    Initiative = int.Parse(groupCollection[1].Value),
                    Weaknesses = new List<string>(),
                    Immunities = new List<string>()
                };

                groups[i - 1] = group;
            }

            return groups;
        }

        private string[] ParseInput(string input)
        {
            string[] inputParts = input.Split(
                new string[] { Environment.NewLine + Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries
            );

            return inputParts;
        }
    }
}
