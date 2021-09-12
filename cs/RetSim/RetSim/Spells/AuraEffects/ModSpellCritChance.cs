namespace RetSim.AuraEffects
{
    class ModSpellCritChance : ModifyFlat
    {
        public List<int> Spells { get; init; }

        public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
        {
            base.Apply(aura, caster, target, fight);

            foreach (int spell in Spells)
            {
                fight.Player.Spellbook[spell].BonusCritChance += Difference;
            }
        }

        public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
        {
            foreach (int spell in Spells)
            {
                fight.Player.Spellbook[spell].BonusCritChance -= Difference;
            }

            base.Remove(aura, caster, target, fight);
        }
    }
}