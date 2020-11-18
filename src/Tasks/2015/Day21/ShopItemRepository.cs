using System.Collections.Generic;

namespace App.Tasks.Year2015.Day21
{
    class ShopItemRepository
    {
        private readonly Dictionary<string, Item> weapons = new Dictionary<string, Item>()
        {
            { "Dagger", new Item { Cost=8, Damage=4, Armor=0 } },
            { "Shortsword", new Item { Cost=10, Damage=5, Armor=0 } },
            { "Warhammer", new Item { Cost=25, Damage=6, Armor=0 } },
            { "Longsword", new Item { Cost=40, Damage=7, Armor=0 } },
            { "Greataxe", new Item { Cost=74, Damage=8, Armor=0 } }
        };

        private readonly Dictionary<string, Item> armors = new Dictionary<string, Item>()
        {
            { "Leather", new Item { Cost=13, Damage=0, Armor=1 } },
            { "Chainmail", new Item { Cost=31, Damage=0, Armor=2 } },
            { "Splintmail", new Item { Cost=53, Damage=0, Armor=3 } },
            { "Bandedmail", new Item { Cost=75, Damage=0, Armor=4 } },
            { "Platemail", new Item { Cost=102, Damage=0, Armor=5 } }
        };

        private readonly Dictionary<string, Item> rings = new Dictionary<string, Item>()
        {
            { "Damage +1", new Item { Cost=25, Damage=1, Armor=0 } },
            { "Damage +2", new Item { Cost=50, Damage=2, Armor=0 } },
            { "Damage +3", new Item { Cost=100, Damage=3, Armor=0 } },
            { "Defense +1", new Item { Cost=20, Damage=0, Armor=1 } },
            { "Defense +2", new Item { Cost=40, Damage=0, Armor=2 } },
            { "Defense +3", new Item { Cost=80, Damage=0, Armor=3 } }
        };

        public Dictionary<string, Item> GetWeapons()
        {
            return weapons;
        }

        public Dictionary<string, Item> GetArmors()
        {
            Dictionary<string, Item> armors = new Dictionary<string, Item>(this.armors)
            {
                // Armor is optional
                { "None", new Item { Cost = 0, Damage = 0, Armor = 0 } }
            };

            return armors;
        }

        public Dictionary<string, Item> GetRings()
        {
            Dictionary<string, Item> rings = new Dictionary<string, Item>(this.rings)
            {
                // Ring is optional
                { "None", new Item { Cost = 0, Damage = 0, Armor = 0 } }
            };

            return rings;
        }
    }
}
