using System.Collections.Generic;

namespace App.Tasks.Year2015.Day22
{
    public class EffectsRepository
    {
        private readonly Dictionary<string, Effect> effects = new Dictionary<string, Effect>()
        {
            { "Magic Missile", new Effect { ManaCost=53, Turns=1, Damage=4, Armor=0, Heal=0, ManaRecharge=0 } },
            { "Drain", new Effect { ManaCost=73, Turns=1, Damage=2, Armor=0, Heal=2, ManaRecharge=0 } },
            { "Shield", new Effect { ManaCost=113, Turns=6, Damage=0, Armor=7, Heal=0, ManaRecharge=0 } },
            { "Poison", new Effect { ManaCost=173, Turns=6, Damage=3, Armor=0, Heal=0, ManaRecharge=0 } },
            { "Recharge", new Effect { ManaCost=229, Turns=5, Damage=0, Armor=0, Heal=0, ManaRecharge=101 } }
        };

        public Dictionary<string, Effect> GetEffects()
        {
            return effects;
        }
    }
}
