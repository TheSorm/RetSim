﻿using RetSim.Events;
using System;
using System.Collections.Generic;
using static RetSim.Glossaries;
using static RetSim.Glossaries.Auras;

namespace RetSim.Tactics
{
    public class EliteTactic : Tactic
    {
        public EliteTactic()
        {
        }

        public override List<Event> PreFight(FightSimulation fight)
        {
            return new List<Event>()
            {
                new CastEvent(Spells.SealOfCommand, fight, 0),
                new CastEvent(Spells.SealOfBlood, fight, 1500),
                new AutoAttackEvent(fight, 1500)
            };
        }

        public override Event GetActionBetween(int start, int end, FightSimulation fight)
        {
            var swing = fight.Player.TimeOfNextSwing() - start;
            var gcd = fight.Player.GCD.GetDuration(start);
            var cs = fight.Player.Spellbook.IsOnCooldown(Spells.CrusaderStrike) ? fight.Player.Spellbook[Spells.CrusaderStrike].Timestamp - start : 0;

            if (gcd == 0 && !fight.Player.Auras[SealOfCommand].Active && swing - gcd > 1510 && end > start + gcd)
                return new CastEvent(Spells.SealOfCommand, fight, start + gcd);

            if (gcd == 0 && fight.Player.Auras[SealOfCommand].Active && end > fight.Player.TimeOfNextSwing() - 390)
                return new CastEvent(Spells.SealOfBlood, fight, Math.Max(fight.Player.TimeOfNextSwing() - 390, start));



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