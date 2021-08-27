using System;

namespace RetSim
{
    public static class Formulas
    {
        public static class Damage
        {
            public static float GetAPBonus(int ap, float weaponSpeed)
            {
                return ap / (float)Constants.Stats.APPerDPS * weaponSpeed;
            }

            public static float GetAPBonusNormalized(int ap)
            {
                return ap / (float)Constants.Stats.APPerDPS * Constants.Stats.NormalizedWeaponSpeed;
            }

            public static float GetWeaponDamage(int weapon, float ap, int bonus)
            {
                return weapon + ap + bonus;
            }

            public static int Melee(int min, int max, float weaponSpeed, int bonus, int ap, float modifier)
            {
                float apBonus = GetAPBonus(ap, weaponSpeed);

                int weapon = RNG.RollRange(min, max);

                float damage = GetWeaponDamage(weapon, apBonus, bonus) * modifier;

                return RNG.RollDamage(damage);
            }

            public static int NormalizedWeaponDamage(int min, int max, int bonus, int ap, float modifier)
            {
                float apBonus = GetAPBonusNormalized(ap);

                int weapon = RNG.RollRange(min, max);

                float damage = GetWeaponDamage(weapon, apBonus, bonus) * modifier;

                return RNG.RollDamage(damage);
            }

            public static int SealOfBlood(int weaponMin, int weaponMax, int weaponSpeed, int bonusWeaponDamage, int ap, float damageModifier, float holyDamageModifier, float jotc)
            {
                float bonus = GetAPBonus(ap, weaponSpeed);

                int weapon = RNG.RollRange(weaponMin, weaponMax);

                float damage = GetWeaponDamage(weapon, bonus, bonusWeaponDamage) * damageModifier * holyDamageModifier * 0.35f + jotc;

                return RNG.RollDamage(damage);
            }

            public static int SealOfCommand(int weaponMin, int weaponMax, int weaponSpeed, int bonusWeaponDamage, int ap, float damageModifier, float holyDamageModifier, float jotc)
            {
                float bonus = GetAPBonus(ap, weaponSpeed);

                int weapon = RNG.RollRange(weaponMin, weaponMax);

                float damage = GetWeaponDamage(weapon, bonus, bonusWeaponDamage) * damageModifier * holyDamageModifier * 0.7f + jotc;

                return RNG.RollDamage(damage);
            }

            public static int JudgementOfBlood(int sp, float holyDamageModifier, float damageModifier, float jotc)
            {
                float bonus = sp * 0.429f;

                float roll = RNG.RollRange(331.6f, 361.6f);

                float damage = (roll + bonus) * damageModifier * holyDamageModifier + jotc;

                return RNG.RollDamage(damage);
            }

            public static int JudgementOfCommand(int sp, float holyDamageModifier, float damageModifier, float jotc, float jocDamageMod)
            {
                float bonus = sp * 0.429f;

                float roll = RNG.RollRange(456, 504) / 2f;

                float damage = (roll + bonus) * damageModifier * holyDamageModifier * jocDamageMod + jotc;

                return RNG.RollDamage(damage);
            }

            public static int ConsecrationTick(int sp, float holyDamageModifier, float damageModifier, float jotc, int consBonusSP)
            {
                float bonus = (sp + consBonusSP) * 0.119f;

                float damage = (64 + bonus) * damageModifier * holyDamageModifier + jotc;

                return RNG.RollDamage(damage);
            }
        }

        public static class Stats
        {
            public static int GetAPFromStrength(int strength)
            {
                return strength * Constants.Stats.APPerStrength;
            }

            public static float GetCritFromAgility(int agility)
            {
                return agility / Constants.Stats.AgilityPerCrit;
            }
        }

        public static class Misc
        {
            public static int PPMToChance(int ppm, int weapon)
            {
                return ppm * weapon / 60000 * 100;
            }
        }
    }
}
