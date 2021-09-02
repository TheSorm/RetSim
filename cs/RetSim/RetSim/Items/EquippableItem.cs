using System.Collections.Generic;

namespace RetSim.Items
{
    public class EquippableItem
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public int ItemLevel { get; init; }
        public string Quality { get; init; }
        public string InventoryType { get; init; }
        public ItemStats Stats { get; init; }
        public Set Set { get; init; }
        public List<ItemSpell> Spells { get; init; }
        public Socket Socket1 { get; init; }
        public Socket Socket2 { get; init; }
        public Socket Socket3 { get; init; }
        public ItemStats SocketBonus { get; init; }
        public int Phase { get; init; }

        public bool IsSocketBonusActive()
        {
            if (Socket1 == null || SocketBonus == null)
            {
                return false;
            }

            return Socket1.IsActive() && (Socket2 == null || Socket2.IsActive()) && (Socket3 == null || Socket3.IsActive());
        }

        public List<Gem> GetGems()
        {
            List<Gem> gems = new();
            if (Socket1 != null && Socket1.SocketedGem != null)
            {
                gems.Add(Socket1.SocketedGem);
            }

            if (Socket2 != null && Socket2.SocketedGem != null)
            {
                gems.Add(Socket2.SocketedGem);
            }

            if (Socket3 != null && Socket3.SocketedGem != null)
            {
                gems.Add(Socket3.SocketedGem);
            }
            return gems;
        }
    }

    public record Set
    {
        public int ID { get; init; }
        public string Name { get; init; }
    }

    public enum ItemStatNames
    {
        Strength = 0,
        AttackPower = 1,
        Agility = 2,
        Crit = 3,
        Hit = 4,
        Haste = 5,
        Expertise = 6,
        ArmorPenetration = 7,
        SpellDamage = 8,
        SpellHealing = 9,
        SpellCrit = 10,
        SpellHit = 11,
        SpellHaste = 12,
        Intellect = 13,
        Spirit = 14,
        ManaPer5 = 15,
        Stamina = 16,
        Defense = 17,
        Dodge = 18,
        Parry = 19,
        Block = 20,
        Resilience = 21,
    }

    public class ItemStats : Dictionary<ItemStatNames, int>
    {
        public new int this[ItemStatNames key]
        {
            get
            {
                if (ContainsKey(key))
                {
                    return base[key];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (ContainsKey(key))
                {
                    base[key] = value;
                }
                else
                {
                    Add(key, value);
                }
            }
        }
    }

    public record SocketBonus
    {
        public string Stat { get; init; }
        public int Value { get; init; }
    }

    public record ItemSpell
    {
        public int ID { get; init; }
        public string Name { get; init; }
    }
}
