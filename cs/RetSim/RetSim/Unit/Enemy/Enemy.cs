namespace RetSim
{
    public class Enemy : Unit
    {
        public ArmorCategory ArmorCategory { get; init; }

        public Enemy(string name, CreatureType type, ArmorCategory armor) : base(name, type)
        {
            ArmorCategory = armor;

            Stats = new EnemyStats(this);
            Modifiers = new Modifiers();

            Auras = new Auras();

            foreach (Aura aura in Data.Auras.ByID.Values)
                Auras.Add(aura);
        }

        public int GetEffectiveArmor(Player player)
        {
            int armor = (int)Stats[StatName.Armor].Value;

            return Math.Max(armor - (int)player.Stats[StatName.ArmorPenetration].Value, 0);
        }
    }


    public enum ArmorCategory
    {
        None = 0,
        Mage = 3878,
        Netherspite = 5474,
        Level71 = 5714,
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