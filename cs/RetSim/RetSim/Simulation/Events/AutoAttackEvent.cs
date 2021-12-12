using RetSim.Spells;

namespace RetSim.Simulation.Events;

public class AutoAttackEvent : Event
{
    private const int BasePriority = 4;

    public AutoAttackEvent(FightSimulation fight, int timestamp, int priority = 0) : base(fight, timestamp, priority + BasePriority)
    {
    }

    public override ProcMask Execute(object arguments = null)
    {
        Fight.Player.NextAutoAttack = new AutoAttackEvent(Fight, Timestamp + Fight.Player.Weapon.EffectiveSpeed);

        Fight.Player.RecalculateNext();

        Fight.Queue.Add(Fight.Player.NextAutoAttack);

        return Fight.Player.Cast(Data.Collections.Spells[1], Fight);
    }

    public override string ToString()
    {
        return "Melee swing lands";
    }
}