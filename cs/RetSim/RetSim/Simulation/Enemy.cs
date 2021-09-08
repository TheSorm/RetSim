namespace RetSim
{
    public class Enemy
    {
        public CreatureType Type {  get; set; }

        public int Armor;
        public static int ShreddedArmor = 4485;

        public Enemy(Armor armor, CreatureType type)
        {
            Armor = (int)armor;
            Type = type;
        }


        public int EffectiveArmor(int ArmorPen)
        {
            int effective = Armor - ShreddedArmor - ArmorPen;

            return effective < 0 ? 0 : effective;
        }
    }


    public enum Armor
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