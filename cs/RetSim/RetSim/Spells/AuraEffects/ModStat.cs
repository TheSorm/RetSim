using System.Collections.Generic;

namespace RetSim.AuraEffects
{
    class ModStat : ModifyPercent
    {
        public List<StatName> Stats { get; init; }

        public override void Apply(Aura aura, FightSimulation fight)
        {
            base.Apply(aura, fight);

            foreach (StatName stat in Stats)
            {
                fight.Player.Stats[stat].Modifier *= RelativeDifference;
            }
        }

        public override void Remove(Aura aura, FightSimulation fight)
        {
            foreach (StatName stat in Stats)
            {
                fight.Player.Stats[stat].Modifier /= RelativeDifference;
            }

            base.Remove(aura, fight);
        }

    }
}
