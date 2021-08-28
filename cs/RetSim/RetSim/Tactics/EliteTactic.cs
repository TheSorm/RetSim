using RetSim.Events;
using System.Collections.Generic;
using static RetSim.Glossaries;

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
                new CastEvent(0, player, Spells.SealOfCommand),
                new CastEvent(1500, player, Spells.SealOfBlood),
                new AutoAttackEvent(1500, player)
            };
        }

        public override Event GetActionBetween(int start, int end, Player player)
        {
            var swing = player.TimeOfNextSwing() - start;
            var gcd = player.IsOnGCD() ? player.GetGCDEnd() - start : 0;
            var cs = player.Spellbook.IsOnCooldown(Spells.CrusaderStrike) ? player.Spellbook[Spells.CrusaderStrike].ExpirationTime - start : 0;

            if (gcd == 0 && !player.Auras[Glossaries.Auras.SealOfCommand].Active && swing - gcd > 1510 && end > start + gcd)
                return new CastEvent(start + gcd, player, Spells.SealOfCommand);

            if (gcd == 0 && player.Auras[Glossaries.Auras.SealOfCommand].Active && end > player.TimeOfNextSwing() - 390 && start < player.TimeOfNextSwing() - 390)
                return new CastEvent(player.TimeOfNextSwing() - 390, player, Spells.SealOfBlood);



            //if (!player.IsOnGCD())
            //{
            //    if (player.Auras.GetRemainingDuration(Glossaries.Auras.SealOfCommand, start) < 5000)
            //        return new CastEvent(start + 0, player, Spells.SealOfCommand);

            //    else if (!player.Spellbook.IsOnCooldown(Spells.CrusaderStrike))
            //        return new CastEvent(start + 0, player, Spells.CrusaderStrike);
            //}

            return null;
        }
    }
}