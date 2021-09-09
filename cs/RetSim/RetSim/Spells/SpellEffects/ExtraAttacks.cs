using RetSim.Events;
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

        public override ProcMask Resolve(FightSimulation fight, SpellState state)
        {
            fight.CombatLog.Add(new ExtraAttacksEntry()
            {
                Timestamp = fight.Timestamp,
                Mana = fight.Player.Stats.Mana,
                Source = Parent.Name,
                Number = Number
            });

            for (int i = 0; i < Number; i++)
            {
                fight.Queue.Add(new CastEvent(Proc, fight, fight.Timestamp));
            }

            return ProcMask.None;
        }
    }
}
