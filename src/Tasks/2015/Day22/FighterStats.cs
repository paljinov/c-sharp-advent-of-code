using System;

namespace App.Tasks.Year2015.Day22
{
    public class FighterStats : ICloneable
    {
        public int HitPoints { get; set; }
        public int Damage { get; set; }
        public int ManaPoints { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
