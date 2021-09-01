namespace RetSim
{
    public class Enemy
    {
        public int Armor;


        public Enemy(Armor armor)
        {
            Armor = (int)armor;
        }


        public int EffectiveArmor(int ArmorPen)
        {
            int effective = Armor - ArmorPen;

            return effective < 0 ? 0 : effective;
        }
    }
}