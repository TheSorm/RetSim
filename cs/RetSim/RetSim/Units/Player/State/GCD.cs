using RetSim.Simulation.Events;

namespace RetSim.Units.Player.State;

public class GCD
{
    private GCDEndEvent gcd;

    public bool Active => gcd != null;
    public int GetEnd => Active ? gcd.Timestamp : -1;

    public GCD()
    {
        gcd = null;
    }

    public void Start(GCDEndEvent newGCD)
    {
        gcd = newGCD;
    }

    public void End()
    {
        gcd = null;
    }

    public int GetDuration(int timestamp)
    {
        return Active ? GetEnd - timestamp : 0;
    }
}