namespace RetSim
{
    public class Stats
    {
        public virtual int Stamina { get; private set; }
        public virtual int Health { get; private set; }

        public int Intellect { get; private set; }
        public int Mana { get; private set; }
        public int ManaPer5 { get; private set; }

        public int Strength { get; private set; }
        public int AttackPower { get; private set; }

        public int Agility { get; private set; }
        public float CritChance { get; private set; }
        public int CritRating { get; private set; }

        public float HitChance { get; private set; }
        public int HitRating { get; private set; }

        public float Haste { get; private set; }
        public int HasteRating { get; private set; }

        public int Expertise { get; private set; }
        public int ExpertiseRating { get; private set; }

        public int ArmorPenetration { get; private set; }
        public int WeaponDamage { get; private set; }

        public int SpellPower { get; private set; }

        public float SpellCrit { get; private set; }
        public int SpellCritRating { get; private set; }
        
        public float SpellHit { get; private set; }
        public int SpellHitRating {  get; private set; }

        public float SpellHaste { get; private set; }
        public int SpellHasteRating {  get; private set; }
    }
}
