namespace RetSim
{
    public class EliteTactic : Tactic
    {
        public EliteTactic()
        {
           
        }

        public override Event GetActionBetween(int start, int end, Player player)
        {
            if (!player.IsSpellOnCooldown(Spellbook.crusaderStrike.ID, start))
            {
                return new CastEvent(start + 0, player, Spellbook.crusaderStrike.ID);
            }
            return null;
        }
    }
}