namespace RetSim.AuraEffects
{
    class ModAttackSpeed : ModifyPercent
    {
        public bool Snapshots { get; init; }

        public override void Apply(Aura aura, FightSimulation fight)
        {
            base.Apply(aura, fight);

            float previousAttackSpeed = fight.Player.Weapon.EffectiveSpeed;
            
            fight.Player.Modifiers.AttackSpeed *= RelativeDifference;

            if (!Snapshots)
                fight.Player.RecalculateAttack(fight, previousAttackSpeed);
        }

        public override void Remove(Aura aura, FightSimulation fight)
        {
            fight.Player.Modifiers.AttackSpeed /= RelativeDifference;

            base.Remove(aura, fight);
        }
    }
}
