namespace RetSim
{
    internal class EliteTactic : Tactic
    {
        internal EliteTactic()
        {
           
        }

        internal override Event getActionBetween(int timeFrameStart, int timeFrameEnd, Player player)
        {
            if (!player.IsSpellOnCooldown(Spellbook.crusaderStrike.SpellId, timeFrameStart))
            {
                return new CastEvent(timeFrameStart + 0, player, Spellbook.crusaderStrike.SpellId);
            }
            return null;
        }
    }
}