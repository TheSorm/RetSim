namespace RetSim
{
    public class Enemy
    {
        public int Armor;
        public static int ShreddedArmor = 4485;

        public Enemy(Armor armor)
        {
            Armor = (int)armor;
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
}