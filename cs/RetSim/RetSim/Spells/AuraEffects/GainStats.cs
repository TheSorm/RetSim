using System.Collections.Generic;

namespace RetSim.AuraEffects
{
    public class GainStats : AuraEffect
    {
        public StatSet Stats { get; init; }

        public override void Apply(Aura aura, FightSimulation fight)
        {
            foreach (KeyValuePair<StatName, float> stat in Stats)
                fight.Player.Stats[stat.Key].Bonus += stat.Value;
        }

        public override void Remove(Aura aura, FightSimulation fight)
        {
            foreach (KeyValuePair<StatName, float> stat in Stats)
                fight.Player.Stats[stat.Key].Bonus -= stat.Value;
        }
    }
}
