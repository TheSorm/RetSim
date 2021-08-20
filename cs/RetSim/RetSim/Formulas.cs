﻿using System;

namespace RetSim
{
    public static class Formulas
    {
        public static readonly Random RNG = new();

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
                return ap / (float)Constants.Stats.APPerDPS * weaponSpeed;
            }

            public static float GetAPBonusNormalized(int ap)
            {
                return ap / (float)Constants.Stats.APPerDPS * Constants.Stats.NormalizedWeaponSpeed;
            }

            public static int GetRNG(int min, int max)
            {
                return RNG.Next(min, max + 1);
            }

            public static float GetRNGDecimal(float min, float max)
            {
                return RNG.Next((int)(min * 100), (int)(max * 100)) / 100f;
            }

            public static float GetWeaponDamage(int weapon, float ap, int bonus)
            {
                return weapon + ap + bonus;
            }

            public static int Melee(int min, int max, float weaponSpeed, int bonus, int ap, float modifier)
            {
                float apBonus = GetAPBonus(ap, weaponSpeed);

                int weapon = GetRNG(min, max);

                float damage = GetWeaponDamage(weapon, apBonus, bonus) * modifier;

                return GetPreciseDamage(damage);
            }

            public static int NormalizedWeaponDamage(int min, int max, int bonus, int ap, float modifier)
            {
                float apBonus = GetAPBonusNormalized(ap);

                int weapon = GetRNG(min, max);

                float damage = GetWeaponDamage(weapon, apBonus, bonus) * modifier;

                return GetPreciseDamage(damage);
            }

            public static int SealOfBlood(int weaponMin, int weaponMax, int weaponSpeed, int bonusWeaponDamage, int ap, float damageModifier, float holyDamageModifier, float jotc)
            {
                float bonus = GetAPBonus(ap, weaponSpeed);

                int weapon = GetRNG(weaponMin, weaponMax);

                float damage = GetWeaponDamage(weapon, bonus, bonusWeaponDamage) * damageModifier * holyDamageModifier * 0.35f + jotc;

                return GetPreciseDamage(damage);
            }

            public static int SealOfCommand(int weaponMin, int weaponMax, int weaponSpeed, int bonusWeaponDamage, int ap, float damageModifier, float holyDamageModifier, float jotc)
            {
                float bonus = GetAPBonus(ap, weaponSpeed);

                int weapon = GetRNG(weaponMin, weaponMax);

                float damage = GetWeaponDamage(weapon, bonus, bonusWeaponDamage) * damageModifier * holyDamageModifier * 0.7f + jotc;

                return GetPreciseDamage(damage);
            }

            public static int JudgementOfBlood(int sp, float holyDamageModifier, float damageModifier, float jotc)
            {
                float bonus = sp * 0.429f;

                float roll = GetRNGDecimal(331.6f, 361.6f);

                float damage = (roll + bonus) * damageModifier * holyDamageModifier + jotc;

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
    }
}