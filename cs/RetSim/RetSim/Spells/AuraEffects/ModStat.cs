namespace RetSim.AuraEffects
{
    class ModStat : Modify
    {
        public bool All = false;
        public bool Strength = false;
        public bool Intellect = false;
        public bool AttackPower = false;

        public override void Apply(Aura aura, FightSimulation fight)
        {
            base.Apply(aura, fight);

            if (All)
                fight.Player.Modifiers.Stats.All *= RelativeDifference;

            if (Strength)
                fight.Player.Modifiers.Stats.Strength *= RelativeDifference;

            if (Intellect)
                fight.Player.Modifiers.Stats.Intellect *= RelativeDifference;

            if (AttackPower)
                fight.Player.Modifiers.Stats.All *= RelativeDifference;
        }

        public override void Remove(Aura aura, FightSimulation fight)
        {
            if (All)
                fight.Player.Modifiers.Stats.All /= RelativeDifference;

            if (Strength)
                fight.Player.Modifiers.Stats.Strength /= RelativeDifference;

            if (Intellect)
                fight.Player.Modifiers.Stats.Intellect /= RelativeDifference;

            if (AttackPower)
                fight.Player.Modifiers.Stats.AttackPower /= RelativeDifference;

            base.Remove(aura, fight);
        }

    }
}
