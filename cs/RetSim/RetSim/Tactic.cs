using System;

namespace RetSim
{
    abstract internal class Tactic
    {
        internal abstract Event getActionBetween(int time, int timeUntilNextEvent, Player player);
    }
}