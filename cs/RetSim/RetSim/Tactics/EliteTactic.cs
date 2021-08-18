using RetSim.Events;

namespace RetSim.Tactics
{
    public class EliteTactic : Tactic
    {
        public EliteTactic()
        {

        }

        public override Event GetActionBetween(int start, int end, Player player)
        {
            if (!player.IsSpellOnCooldown(Spellbook.crusaderStrike.ID) && !player.OnGCD())
            {
                return new CastEvent(start + 0, player, Spellbook.crusaderStrike.ID);
            }
            return null;
        }
    }
}