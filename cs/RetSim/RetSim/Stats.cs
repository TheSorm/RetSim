namespace RetSim
{
    public class Stats
    {
        public int Stamina { get; init; }

        public int Intellect { get; init; }
        public int ManaPer5 { get; init; }

        public int Strength { get; init; }
        public int AttackPower { get; init; }

        public int Agility { get; init; }
        public float CritChance { get; init; }
        public int CritRating { get; init; }

        public float HitChance { get; init; }
        public int HitRating { get; init; }

        public float Haste { get; init; }
        public int HasteRating { get; init; }

        public int Expertise { get; init; }
        public int ExpertiseRating { get; init; }

        public int ArmorPenetration { get; init; }
        public int WeaponDamage { get; init; }

        public int SpellPower { get; init; }

        public float SpellCrit { get; init; }
        public int SpellCritRating { get; init; }

        public float SpellHit { get; init; }
        public int SpellHitRating { get; init; }

        public float SpellHaste { get; init; }
        public int SpellHasteRating { get; init; }

        public Stats()
        {
            Stamina = 0;

            Intellect = 0;
            ManaPer5 = 0;

            Strength = 0;
            AttackPower = 0;

            Agility = 0;
            CritChance = 0;
            CritRating = 0;

            HitChance = 0;
            HitRating = 0;

            Haste = 0;
            HasteRating = 0;

            Expertise = 0;
            ExpertiseRating = 0;

            ArmorPenetration = 0;
            WeaponDamage = 0;

            SpellPower = 0;

            SpellCrit = 0;
            SpellCritRating = 0;

            SpellHit = 0;
            SpellHitRating = 0;

            SpellHaste = 0;
            SpellHasteRating = 0;
        }

        public static Stats operator +(Stats a, Stats b)
        {
            return new Stats
            {
                Stamina = a.Stamina + b.Stamina,

                Intellect = a.Intellect + b.Intellect,
                ManaPer5 = a.ManaPer5 + b.ManaPer5,

                Strength = a.Strength + b.Strength,
                AttackPower = a.AttackPower + b.AttackPower,

                Agility = a.Agility + b.Agility,
                CritChance = a.CritChance + b.CritChance,
                CritRating = a.CritRating + b.CritRating,

                HitChance = a.HitChance + b.HitChance,
                HitRating = a.HitRating + b.HitRating,

                Haste = a.Haste + b.Haste,
                HasteRating = a.HasteRating + b.HasteRating,

                Expertise = a.Expertise + b.Expertise,
                ExpertiseRating = a.ExpertiseRating + b.ExpertiseRating,

                ArmorPenetration = a.ArmorPenetration + b.ArmorPenetration,
                WeaponDamage = a.WeaponDamage + b.WeaponDamage,

                SpellPower = a.SpellPower + b.SpellPower,

                SpellCrit = a.SpellCrit + b.SpellCrit,
                SpellCritRating = a.SpellCritRating + b.SpellCritRating,

                SpellHit = a.SpellHit + b.SpellHit,
                SpellHitRating = a.SpellHitRating + b.SpellHitRating,

                SpellHaste = a.SpellHaste + b.SpellHaste,
                SpellHasteRating = a.SpellHasteRating + b.SpellHasteRating
            };
        }
    }
}
