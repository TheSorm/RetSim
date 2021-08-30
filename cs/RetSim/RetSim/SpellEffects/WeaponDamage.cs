using RetSim.Log;
using System;

namespace RetSim.SpellEffects
{
    public class WeaponDamage : DamageEffect
    {
        public float Percentage { get; set; }

        public WeaponDamage(Spell spell,
                            School school,
                            DefenseType defense,
                            Category crit,
                            bool normalized,
                            ProcMask onCast,
                            ProcMask onHit,
                            ProcMask onCrit,
                            float percentage,
                            float coefficient = 0f,
                            float holy = 0f) 
                            : base(spell, school, defense, crit, normalized, onCast, onHit, onCrit, coefficient, holy)
        {
            Percentage = percentage;
        }

        public WeaponDamage() { }

        public override ProcMask Resolve(FightSimulation fight)
        {
            ProcMask mask = ProcMask.None;

            int damage = 0;
            float glancing = 0;
            float resist = 0;
            
            float critChance = Formulas.Damage.GetCritChance(CritCategory, fight.Player);

            Tuple<AttackResult, DamageResult> results = Formulas.HitChecks.CheckToFunction[DefenseCategory].Invoke(fight.Player.Stats.EffectiveMissChance,
                                                                                                    fight.Player.Stats.EffectiveDodgeChance,
                                                                                                    critChance);

            mask |= OnCast;

            if (results.Item1 == AttackResult.Hit)
            {
                mask |= OnHit;

                float damageModifier = Formulas.Damage.GetDamageModifier(results.Item2, CritCategory, fight.Player);

                if (results.Item2 == DamageResult.Glancing)
                {
                    glancing = damageModifier * 100;
                    damageModifier = 1 - damageModifier;
                }

                if (results.Item2 == DamageResult.Crit)
                {
                    mask |= OnCrit;
                }

                float schoolModifier = Formulas.Damage.GetSchoolModifier(School, fight.Player);

                float vengeanceModifier = School == School.Physical || School == School.Holy ? 1.15f : 1f;

                float weaponDamage = Normalized ? fight.Player.Weapon.NormalizedAttack() : fight.Player.Weapon.Attack();

                float formula = (weaponDamage * Percentage + (float)(Coefficient * fight.Player.Stats.SpellPower)) * schoolModifier * damageModifier * vengeanceModifier;

                damage = RNG.RollDamage(formula);
            }

            DamageEntry entry = new(fight.Timestamp,
                                    fight.Player.Stats.Mana,
                                    Spell.Name,
                                    results.Item1,
                                    damage,
                                    School,
                                    results.Item2 == DamageResult.Crit,
                                    glancing,
                                    resist);

            fight.CombatLog.Add(entry);

            return mask; // TODO: Is that all?
        }        
    }
}
