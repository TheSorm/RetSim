using RetSim.Log;

namespace RetSim.SpellEffects
{
    class ExtraAttacks : SpellEffect
    {
        Spell Proc { get; init; }
        int Number { get; init; }

        public ExtraAttacks(Spell proc, int number)
        {
            Proc = proc;
            Number = number;
        }

        public override ProcMask Resolve(FightSimulation fight)
        {
            for (int i = 0; i < Number; i++)

                fight.CombatLog.Add(new ExtraAttacksEntry()
                {
                    Timestamp = fight.Timestamp,
                    Mana = fight.Player.Stats.Mana,
                    Source = Spell.Name,
                    Number = Number
                });;
            
            return fight.Player.Cast(Proc, fight);
        }
    }
}
