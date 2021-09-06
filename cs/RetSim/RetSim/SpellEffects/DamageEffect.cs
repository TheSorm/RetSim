﻿using RetSim.Log;

namespace RetSim.SpellEffects
{
    public abstract class DamageEffect : SpellEffect
    {
        public School School { get; init; } = School.Physical;
        public float Coefficient { get; init; } = 0;
        public float HolyCoefficient { get; init; } = 0;
        public DefenseType DefenseCategory { get; init; } = DefenseType.Special;
        public Category CritCategory { get; init; } = Category.Physical;
        public bool Normalized { get; init; } = false;
        public ProcMask OnCast { get; init; } = ProcMask.None;
        public ProcMask OnHit { get; init; } = ProcMask.None;
        public ProcMask OnCrit { get; init; } = ProcMask.None;

        public virtual float GetBaseDamage(Player player)
        {
            return RNG.RollRange(MinEffect, MaxEffect);
        }

        public virtual float CalculateDamage(Player player, Attack attack)
        {
            return (GetBaseDamage(player) + attack.SpellPowerBonus) * attack.SchoolModifier * attack.SpellModifier * attack.DamageModifier;
        }

        public override ProcMask Resolve(FightSimulation fight)
        {
            ProcMask mask = OnCast;

            var attack = new Attack(fight.Player, fight.Enemy, this);

            if (attack.AttackResult == AttackResult.Hit)
            {
                mask |= OnHit;

                if (attack.DamageResult == DamageResult.Crit)
                    mask |= OnCrit;

                attack.ResolveDamage();
            }

            var entry = new DamageEntry()
            {
                Timestamp = fight.Timestamp,
                Mana = fight.Player.Stats.Mana,
                Source = Parent.Name,
                AttackResult = attack.AttackResult,
                Damage = attack.Damage,
                School = School,
                Crit = attack.DamageResult == DamageResult.Crit,
                Glancing = attack.Glancing,
                Mitigation = attack.Mitigation
            };

            fight.CombatLog.Add(entry);

            return mask;
        }
    }
}