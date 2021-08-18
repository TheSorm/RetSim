using RetSim.Events;

namespace RetSim.Tactics
{
    abstract public class Tactic
    {
        public abstract Event GetActionBetween(int start, int end, Player player);
    }
}