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
            if (!player.Spellbook.IsOnCooldown(Spells.CrusaderStrike) && !player.IsOnGCD())
            {
                return new CastEvent(start + 0, player, Spells.CrusaderStrike.ID);
            }

            return null;
        }
    }
}