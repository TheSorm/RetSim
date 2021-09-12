namespace RetSim.AuraEffects
{
    public class ModifyStats : AuraEffect
    {
        public StatSet Stats { get; init; }

        public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
        {
            foreach (KeyValuePair<StatName, float> stat in Stats)
            {
                target.Stats[stat.Key].Bonus += stat.Value;
                //Program.Logger.Log($"{fight.Timestamp} - {target.Name}'s {stat.Key} was set to {target.Stats[stat.Key].Value} (previous: {target.Stats[stat.Key].Value - stat.Value})");
            }
        }

        public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
        {
            foreach (KeyValuePair<StatName, float> stat in Stats)
            {
                target.Stats[stat.Key].Bonus -= stat.Value;
                //Program.Logger.Log($"{fight.Timestamp} - {target.Name}'s {stat.Key} was set to {target.Stats[stat.Key].Value} (previous: {target.Stats[stat.Key].Value + stat.Value})");
            }
        }
    }
}
