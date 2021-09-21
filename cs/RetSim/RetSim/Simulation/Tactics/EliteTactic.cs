using RetSim.Data;
using RetSim.Simulation.Events;
using RetSim.Spells;

using static RetSim.Data.Spells;

namespace RetSim.Simulation.Tactics;

public class EliteTactic : Tactic
{
    Spell trinket1 = null;
    Spell trinket2 = null;

    public EliteTactic()
    {
    }

    public override List<Event> PreFight(FightSimulation fight)
    {
        var onStart = new List<Event>()
            {
                new CastEvent(SealOfCommand, fight.Player, fight.Player, fight, 0),
                new CastEvent(AvengingWrath, fight.Player, fight.Player, fight, 1495),
                new CastEvent(SealOfBlood, fight.Player, fight.Player, fight, 1500),
                new AutoAttackEvent(fight, 1501)
            };

        if (fight.Player.Equipment.Trinket1 != null && fight.Player.Equipment.Trinket1.OnUse != null && ByID.ContainsKey(fight.Player.Equipment.Trinket1.OnUse.ID))
            trinket1 = ByID[fight.Player.Equipment.Trinket1.OnUse.ID];

        if (fight.Player.Equipment.Trinket2 != null && fight.Player.Equipment.Trinket2.OnUse != null && ByID.ContainsKey(fight.Player.Equipment.Trinket2.OnUse.ID))
            trinket2 = ByID[fight.Player.Equipment.Trinket2.OnUse.ID];

        if (trinket1 != null)
            onStart.Add(new CastEvent(trinket1, fight.Player, fight.Player, fight, 1495));

        else if (trinket2 != null)
            onStart.Add(new CastEvent(trinket2, fight.Player, fight.Player, fight, 1495));

        return onStart;
    }

    public override Event GetActionBetween(int start, int end, FightSimulation fight)
    {
        if (!fight.Player.Spellbook.IsOnCooldown(AvengingWrath) && start > 1500)
            return new CastEvent(AvengingWrath, fight.Player, fight.Player, fight, start);

        if (trinket1 != null && !fight.Player.Spellbook.IsOnCooldown(trinket1) && start > 21495)
            return new CastEvent(trinket1, fight.Player, fight.Player, fight, start);

        if (trinket2 != null && !fight.Player.Spellbook.IsOnCooldown(trinket2) && start > 21495)
            return new CastEvent(trinket2, fight.Player, fight.Player, fight, start);

        if (!fight.Player.GCD.Active)
        {
            int crusaderStrikeCooldownEnd = fight.Player.Spellbook.IsOnCooldown(CrusaderStrike) ? fight.Player.Spellbook[CrusaderStrike.ID].CooldownEnd.Timestamp : start;
            int twistWindowEnd = fight.Player.TimeOfNextSwing() - 1510;

            if (!fight.Player.Auras[Auras.SealOfCommand].Active && start < twistWindowEnd && crusaderStrikeCooldownEnd > fight.Player.TimeOfNextSwing())
            {
                if (!fight.Player.Spellbook.IsOnCooldown(Data.Spells.Judgement))
                    return new CastEvent(Data.Spells.Judgement, fight.Player, fight.Enemy, fight, fight.Timestamp);

                if (fight.Player.Spellbook.IsOnCooldown(Data.Spells.Judgement) && fight.Player.Spellbook[Data.Spells.Judgement.ID].CooldownEnd.Timestamp > twistWindowEnd)
                    return new CastEvent(SealOfCommand, fight.Player, fight.Player, fight, start);
            }

            if (!fight.Player.Auras[Auras.SealOfCommand].Active && !fight.Player.Spellbook.IsOnCooldown(CrusaderStrike))
            {
                return new CastEvent(CrusaderStrike, fight.Player, fight.Player, fight, start);
            }

            int twistTime = fight.Player.TimeOfNextSwing() - 390;
            if (fight.Player.Auras[Auras.SealOfCommand].Active && start < twistTime && end > twistTime)
                return new CastEvent(SealOfBlood, fight.Player, fight.Player, fight, twistTime, start);
        }

        //if (!player.IsOnGCD())
        //{
        //    if (player.Auras.GetRemainingDuration(Glossaries.Auras.SealOfCommand, start) < 5000)
        //        return new CastEvent(start + 0, player, Spells.SealOfCommand);

        //    else if (!player.Spellbook.IsOnCooldown(Spells.CrusaderStrike))
        //        return new CastEvent(start + 0, player, Spells.CrusaderStrike);
        //}

        //if (!fight.Player.GCD.Active && !fight.Player.Spellbook.IsOnCooldown(Spells.CrusaderStrike))
        //    return new CastEvent(Spells.CrusaderStrike, fight, fight.Timestamp);

        return null;
    }
}