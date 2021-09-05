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
        public ItemSpell OnUseSpell { get; set; }
        public List<ItemAura> Auras { get; init; }

        public Socket[] Sockets = new Socket[3];
        public Socket Socket1 { get => Sockets[0]; init => Sockets[0] = value; }
        public Socket Socket2 { get => Sockets[1]; init => Sockets[1] = value; }
        public Socket Socket3 { get => Sockets[2]; init => Sockets[2] = value; }
        public ItemStats SocketBonus { get; init; }
        public bool UniqueEquipped { get; set; }
        public int Phase { get; init; }

        public bool IsSocketBonusActive()
        {
            bool result = true;

            if (SocketBonus == null)
                return false;

            else
            {
                foreach (Socket socket in Sockets)
                {
                    if (socket == null)
                        continue;

                    else if (!socket.IsActive())
                    {
                        result = false;
                        break;
                    }

                }

                return result;
            }
        }

        public List<Gem> GetGems()
        {
            var gems = new List<Gem>();

            foreach (Socket socket in Sockets)
            {
                if (socket != null && socket.SocketedGem != null)
                    gems.Add(socket.SocketedGem);
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
                    return base[key];

                else
                    return 0;
            }

            set
            {
                if (ContainsKey(key))
                    base[key] = value;

                else
                    Add(key, value);
            }
        }
    }

    public record SocketBonus
    {
        public string Stat { get; init; }
        public int Value { get; init; }
    }

    public record ItemAura
    {
        public int ID { get; init; }
        public string Name { get; init; }
    }

    public record ItemSpell
    {
        public int ID { get; init; }
        public string Name { get; init; }
    }
}
