using RetSim.Spells;

namespace RetSim.Simulation.Events;

public abstract class Event : IComparable<Event>
{
    private int timestamp;
    public int Timestamp { get { return timestamp; } set { UpdateTimestamp(value); } }
    public int Priority { get; init; }

    protected FightSimulation Fight { get; init; }

    protected Event(FightSimulation fight, int timestamp, int priority = 0)
    {
        Fight = fight;

        this.timestamp = timestamp;
        Priority = priority;
    }

    public abstract ProcMask Execute(object arguments = null);

    public void SetTimeStemp(int value)
    {
        timestamp = value;
    }

    private void UpdateTimestamp(int value)
    {
        Fight.Queue.UpdateRemove(this);
        timestamp = value;
        Fight.Queue.UpdateAdd(this);
    }

    public int CompareTo(Event other)
    {
        int result = Timestamp.CompareTo(other.Timestamp);
        if (result == 0) 
            return Priority.CompareTo(other.Priority);
        return result;
    }
}