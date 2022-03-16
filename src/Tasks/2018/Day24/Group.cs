namespace App.Tasks.Year2018.Day24
{
    public struct Group
    {
        public int Units { get; set; }
        public int UnitHitPoints { get; set; }
        public int UnitAttackDamage { get; set; }
        public string AttackType { get; set; }
        public int Initiative { get; set; }
        public string[] Weaknesses { get; set; }
        public string[] Immunities { get; set; }
    }
}
