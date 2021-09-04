namespace RetSim.SpellEffects
{
    class ExtraAttacks : SpellEffect
    {
        int Number { get; init; }

        public ExtraAttacks(int number)
        {
            Number = number;
        }

        public override ProcMask Resolve(FightSimulation fight)
        {
            for (int i = 0; i < Number; i++)
                fight.Player.Cast(Glossaries.Spells.Melee, fight);

            return ProcMask.None;
        }
    }
}
