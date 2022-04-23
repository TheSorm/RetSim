using RetSim.Spells;
using RetSim.Units.Player.State;

namespace RetSim.Units.Enemy
{
    public class Enemy : Unit
    {
        public Boss Boss { get; init; }

        public Enemy(Boss boss) : base(boss.Name, boss.CreatureType)
        {
            Boss = boss;

            Stats = new EnemyStats(this);
            Modifiers = new Modifiers();

            Auras = new Auras();

            foreach (Aura aura in Data.Collections.Auras.Values)
                Auras.Add(aura);
        }

        public override string ToString()
        {
            return Boss.ToString();
        }
    }

    public enum ArmorCategory
    {
        None = 0,
        Veras = 146,
        Mage = 3878,
        Netherspite = 5474,
        Paladin = 6193,
        Demon = 6792,
        Warrior = 7684,
        VoidReaver = 8806
    }

    public enum CreatureType
    {
        Uncategorized = 0,
        Beast = 1,
        Demon = 2,
        Dragonkin = 3,
        Elemental = 4,
        Giant = 5,
        Humanoid = 6,
        Mechanical = 7,
        Undead = 8
    }
}