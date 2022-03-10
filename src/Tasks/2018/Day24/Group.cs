using System.Collections.Generic;

namespace App.Tasks.Year2018.Day24
{
    public class Group
    {
        public int Units { get; set; }
        public int UnitHitPoints { get; set; }
        public int UnitAttackDamage { get; set; }
        public string AttackType { get; set; }
        public int Initiative { get; set; }
        public List<string> Weaknesses { get; set; }
        public List<string> Immunities { get; set; }
    }
}
