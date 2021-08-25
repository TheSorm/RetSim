using RetSim.Events;
using System.Collections.Generic;

namespace RetSim.Tactics
{
    public class EliteTactic : Tactic
    {
        public EliteTactic()
        {

        }

        public override List<Event> PreFight(Player player)
        {
            return new List<Event>()
            {
                new CastEvent(0, player, Glossaries.Spells.SealOfCommand),
                new CastEvent(1500, player, Glossaries.Spells.SealOfBlood),
                new AutoAttackEvent(1500, player)
            };
        }

        public override Event GetActionBetween(int start, int end, Player player)
        {
            if (!player.IsOnGCD())
            {
                if (player.Auras.GetRemainingDuration(Glossaries.Auras.SealOfCommand, start) < 5000)
                    return new CastEvent(start + 0, player, Glossaries.Spells.SealOfCommand);

                else if (!player.Spellbook.IsOnCooldown(Glossaries.Spells.CrusaderStrike))                
                    return new CastEvent(start + 0, player, Glossaries.Spells.CrusaderStrike);                
            }

            return null;
        }
    }
}