using RetSim.Events;

namespace RetSim.SpellEffects
{
    class JudgementEffect : SpellEffect
    {
        public override ProcMask Resolve(FightSimulation fight, SpellState state)
        {
            if (fight.Player.Auras.CurrentSeal != null)
            {
                var seal = fight.Player.Auras.CurrentSeal;

                fight.Player.Auras.Cancel(seal, fight.Player, fight.Player, fight);

                fight.Queue.Add(new CastEvent(seal.Judgement, fight.Player, fight.Enemy, fight, fight.Timestamp));
            }
            
            return ProcMask.None;
        }
    }
}
