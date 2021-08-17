namespace RetSim
{
    abstract internal class Tactic
    {
        internal abstract Event GetActionBetween(int start, int end, Player player);
    }
}