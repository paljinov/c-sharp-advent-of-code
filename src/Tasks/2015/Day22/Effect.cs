using System;

namespace App.Tasks.Year2015.Day22
{
    public class Effect : ICloneable
    {
        public int ManaCost { get; set; }
        public int Turns { get; set; }
        public int Damage { get; set; }
        public int Armor { get; set; }
        public int Heal { get; set; }
        public int ManaRecharge { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
