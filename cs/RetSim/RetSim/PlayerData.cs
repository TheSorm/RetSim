using System;

namespace RetSim
{
    public partial class Player
    {
        public Random random = new Random();
        public string Name { get; set; } = "TestPlayer";

        //public Weapon Weapon { get; set; }

        //public Spellbook Spellbook { get; set; } = new Spellbook();

        //public Stats Stats { get; set; } = new Stats();

        //public int DealDamage()
        //{
        //    return random.Next(Weapon.MinDamage, Weapon.MaxDamage + 1) + Convert.ToInt32(Stats.AttackPower / 14 * Weapon.Speed);
        //}

        //public Player(string name, int attackPower, Weapon weapon, Spellbook spellbook)
        //{
        //    Name = name;
        //    Weapon = weapon;
        //    Spellbook = spellbook;
        //}
    }
}
