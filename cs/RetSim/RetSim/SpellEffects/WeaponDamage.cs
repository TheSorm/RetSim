using RetSim.Log;

namespace RetSim.SpellEffects
{
    public class WeaponDamage : DamageEffect
    {
        public float Percentage { get; init; }

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

            //DamageEntry entry = new(fight.Timestamp,
            //                        fight.Player.Stats.Mana,
            //                        Spell.Name,
            //                        attack.AttackResult,
            //                        attack.Damage,
            //                        School,
            //                        attack.DamageResult == DamageResult.Crit,
            //                        attack.Glancing,
            //                        attack.Mitigation );

            var entry = new DamageEntry()
            {
                Timestamp = fight.Timestamp,
                Mana = fight.Player.Stats.Mana,
                Source = Spell.Name,
                AttackResult = attack.AttackResult,
                Damage = attack.Damage,
                School = School,
                Crit = attack.DamageResult == DamageResult.Crit,
                Glancing = attack.Glancing,
                Mitigation = attack.Mitigation
            };

            fight.CombatLog.Add(entry);

            return mask; // TODO: Is that all?
        }

        public override float GetBaseDamage(Player player)
        {
            return GetWeaponDamage(player) * Percentage + player.Modifiers.Bonuses[Spell];
        }

        protected float GetWeaponDamage(Player player)
        {
            return Normalized ? player.Weapon.NormalizedAttack() : player.Weapon.Attack();
        }
    }
}
