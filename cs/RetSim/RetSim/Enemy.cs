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
}