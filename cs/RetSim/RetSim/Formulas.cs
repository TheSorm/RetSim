using System;

namespace RetSim
{
    public static class Formulas
    {
        public static Random RNG = new Random();

        public static class Damage
        {
            public static int GetPreciseDamage(float damage)
            {
                int fraction = Helpers.GetFractional(damage);

                int random = RNG.Next(0, 100) < fraction ? 1 : 0;

                return (int)Math.Floor(damage) + random;
            }

            public static float GetAPBonus(int ap, float weaponSpeed)
            {
                return ap / 14f * weaponSpeed;
            }

            public static float GetAPBonusNormalized(int ap)
            {
                return ap / 14f * 3.3f;
            }

            public static int GetRNG(int min, int max)
            { 
                return RNG.Next(min, max + 1);
            }

            public static float GetRNGDecimal(float min, float max)
            {
                return RNG.Next((int)(min * 100), (int)(max * 100)) / 100f;
            }

            public static float GetWeaponDamage(int weaponDamage, float apBonus, int bonusDamage, float weaponDamageModifier)
            {
                return (weaponDamage + apBonus + bonusDamage) * weaponDamageModifier;
            }

            public static int Melee(int weaponMin, int weaponMax, int weaponSpeed, int bonusWeaponDamage, int ap, float weaponDamageModifier, float damageModifier)
            {
                float bonus = GetAPBonus(ap, weaponSpeed);

                int weapon = GetRNG(weaponMin, weaponMax);

                float damage = GetWeaponDamage(weapon, bonus, bonusWeaponDamage, weaponDamageModifier) * damageModifier;

                return GetPreciseDamage(damage);
            }

            public static int SealOfBlood(int weaponMin, int weaponMax, int weaponSpeed, int bonusWeaponDamage, int ap, float weaponDamageModifier, float damageModifier, float holyDamageModifier, float jotc)
            {
                float bonus = GetAPBonus(ap, weaponSpeed);

                int weapon = GetRNG(weaponMin, weaponMax);

                float damage = GetWeaponDamage(weapon, bonus, bonusWeaponDamage, weaponDamageModifier) * damageModifier * holyDamageModifier * 0.35f + jotc;

                return GetPreciseDamage(damage);
            }

            public static int SealOfCommand(int weaponMin, int weaponMax, int weaponSpeed, int bonusWeaponDamage, int ap, float weaponDamageModifier, float damageModifier, float holyDamageModifier, float jotc)
            {
                float bonus = GetAPBonus(ap, weaponSpeed);

                int weapon = GetRNG(weaponMin, weaponMax);

                float damage = GetWeaponDamage(weapon, bonus, bonusWeaponDamage, weaponDamageModifier) * damageModifier * holyDamageModifier * 0.7f + jotc;

                return GetPreciseDamage(damage);
            }

            public static int CrusaderStrike(int weaponMin, int weaponMax, int bonusWeaponDamage, int ap, float weaponDamageModifier, float damageModifier, float csBonusMod, int csBonusDamage)
            {
                float bonus = GetAPBonusNormalized(ap);

                int weapon = GetRNG(weaponMin, weaponMax);

                float damage = GetWeaponDamage(weapon + csBonusDamage, bonus, bonusWeaponDamage, weaponDamageModifier) * damageModifier * 1.1f * csBonusMod;

                return GetPreciseDamage(damage);
            }

            public static int JudgementOfBlood(int sp, float holyDamageModifier, float damageModifier, float jotc)
            {
                float bonus = sp * 0.429f;

                float roll = GetRNGDecimal(331.6f, 361.6f);

                float damage = (roll + bonus) * damageModifier * holyDamageModifier  + jotc;

                return GetPreciseDamage(damage);
            }

            public static int JudgementOfCommand(int sp, float holyDamageModifier, float damageModifier, float jotc, float jocDamageMod)
            {
                float bonus = sp * 0.429f;

                float roll = GetRNG(456, 504) / 2f;

                float damage = (roll + bonus) * damageModifier * holyDamageModifier * jocDamageMod + jotc;

                return GetPreciseDamage(damage);
            }

            public static int ConsecrationTick(int sp, float holyDamageModifier, float damageModifier, float jotc, int consBonusSP)
            {
                float bonus = (sp + consBonusSP) * 0.119f;

                float damage = (64 + bonus) * damageModifier * holyDamageModifier + jotc;

                return GetPreciseDamage(damage);
            }
        }
    }
}
