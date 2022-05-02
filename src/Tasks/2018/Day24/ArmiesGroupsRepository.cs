using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace App.Tasks.Year2018.Day24
{
    public class ArmiesGroupsRepository
    {
        public List<Group> GetArmiesGroups(string input)
        {
            List<Group> armiesGroups = new List<Group>();

            string[] inputParts = ParseInput(input);

            GroupType[] groupTypes = Enum.GetValues(typeof(GroupType)).Cast<GroupType>().ToArray();
            for (int i = 0; i < groupTypes.Length; i++)
            {
                Group[] groups = GetArmyGroups(inputParts[i], groupTypes[i]);
                armiesGroups.AddRange(groups);
            }

            return armiesGroups;
        }

        private Group[] GetArmyGroups(string input, GroupType groupType)
        {
            string[] groupsArray = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Group[] armyGroups = new Group[groupsArray.Length - 1];

            Regex armyGroupRegex = new Regex(@"^(\d+)\sunits\seach\swith\s(\d+)\shit\spoints"
                + @"(?:\s\((.+)\))?\swith\san\sattack\sthat\sdoes\s(\d+)\s(\w+)\sdamage\sat\sinitiative\s(\d+)$");

            Regex weaknessesOrImmunitiesRegex = new Regex(@"^(\w+)\sto\s(.+)$");

            for (int i = 1; i < groupsArray.Length; i++)
            {
                Match armyMatch = armyGroupRegex.Match(groupsArray[i]);
                GroupCollection groups = armyMatch.Groups;

                string[] weaknesses = Array.Empty<string>();
                string[] immunities = Array.Empty<string>();
                string[] weaknessesAndImmunities = groups[3].Value.Split(';');

                for (int j = 0; j < weaknessesAndImmunities.Length; j++)
                {
                    Match weaknessesOrImmunitiesMatch =
                        weaknessesOrImmunitiesRegex.Match(weaknessesAndImmunities[j].Trim());
                    GroupCollection weaknessesOrImmunitiesGroups = weaknessesOrImmunitiesMatch.Groups;

                    string[] weakOrImmuneTo = weaknessesOrImmunitiesGroups[2].Value.Split(", ").ToArray();

                    if (weaknessesOrImmunitiesGroups[1].Value == "weak")
                    {
                        weaknesses = weakOrImmuneTo;
                    }
                    else if (weaknessesOrImmunitiesGroups[1].Value == "immune")
                    {
                        immunities = weakOrImmuneTo;
                    }
                }

                Group armyGroup = new Group
                {
                    GroupType = groupType,
                    Units = int.Parse(groups[1].Value),
                    UnitHitPoints = int.Parse(groups[2].Value),
                    UnitAttackDamage = int.Parse(groups[4].Value),
                    AttackType = groups[5].Value,
                    Initiative = int.Parse(groups[6].Value),
                    Weaknesses = weaknesses,
                    Immunities = immunities
                };

                armyGroups[i - 1] = armyGroup;
            }

            return armyGroups;
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
