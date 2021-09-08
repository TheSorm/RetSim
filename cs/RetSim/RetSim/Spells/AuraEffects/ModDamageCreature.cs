using System.Collections.Generic;

namespace RetSim.AuraEffects
{
    class ModDamageCreature : ModDamageSchool
    {
        public List<CreatureType> Types { get; init; }

        protected bool MatchingType = false;
        public override void Apply(Aura aura, FightSimulation fight)
        {
            if (Types.Contains(fight.Enemy.Type))
            {
                MatchingType = true;
                base.Apply(aura, fight);
            }
        }

        public override void Remove(Aura aura, FightSimulation fight)
        {
            if (MatchingType)
                base.Remove(aura, fight);
        }
    }
}
