using RetSim.Events;
using System.Collections.Generic;

namespace RetSim.Tactics
{
    abstract public class Tactic
    {
        public abstract List<Event> PreFight(Player player);

        public abstract Event GetActionBetween(int start, int end, Player player);
    }
}