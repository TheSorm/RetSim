using RetSim.Simulation;
using RetSim.Units;
using RetSim.Units.Player;

namespace RetSim.Spells.AuraEffects;

public class GainProcFaction : AuraEffect
{
    public Proc Proc { get; private set; }

    public Proc Aldor { get; init; }
    public Proc Scryer { get; init; }

    public GainProcFaction(float aldor, float scryer) : base(0)
    {
        Aldor = Data.Collections.Procs[(int)aldor];
        Scryer = Data.Collections.Procs[(int)scryer];
    }

    public override void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        if (Proc == null && fight.Player.Faction != ShattrathFaction.None)
        {
            if (fight.Player.Faction == ShattrathFaction.Aldor)
                Proc = Aldor;

            else
                Proc = Scryer;
        }

        if (Proc != null)
            fight.Player.Procs.Add(Proc);
    }

    public override void Remove(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        if (Proc != null)
            fight.Player.Procs.Remove(Proc);
    }
}