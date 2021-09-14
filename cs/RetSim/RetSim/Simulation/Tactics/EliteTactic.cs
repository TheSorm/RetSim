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
        var swing = fight.Player.TimeOfNextSwing() - start;
        var gcd = fight.Player.GCD.GetDuration(start);
        var cs = fight.Player.Spellbook.IsOnCooldown(CrusaderStrike) ? fight.Player.Spellbook[CrusaderStrike.ID].CooldownEnd.Timestamp - start : 0;

        if (!fight.Player.Spellbook.IsOnCooldown(AvengingWrath) && start > 1500)
            return new CastEvent(AvengingWrath, fight.Player, fight.Player, fight, fight.Timestamp);

        if (trinket1 != null && !fight.Player.Spellbook.IsOnCooldown(trinket1) && start > 21495)
            return new CastEvent(trinket1, fight.Player, fight.Player, fight, fight.Timestamp);

        if (trinket2 != null && !fight.Player.Spellbook.IsOnCooldown(trinket2) && start > 21495)
            return new CastEvent(trinket2, fight.Player, fight.Player, fight, fight.Timestamp);

        if (gcd == 0 && !fight.Player.Auras[Auras.SealOfCommand].Active && swing - gcd > 1510 && end > start + gcd)
        {
            if (fight.Player.Auras.CurrentSeal == Auras.SealOfBlood && !fight.Player.Spellbook.IsOnCooldown(Data.Spells.Judgement))
                return new CastEvent(Data.Spells.Judgement, fight.Player, fight.Enemy, fight, fight.Timestamp);

            return new CastEvent(SealOfCommand, fight.Player, fight.Player, fight, start + gcd);
        }

        if (gcd == 0 && fight.Player.Auras[Auras.SealOfCommand].Active && end > fight.Player.TimeOfNextSwing() - 390)
            return new CastEvent(SealOfBlood, fight.Player, fight.Player, fight, Math.Max(fight.Player.TimeOfNextSwing() - 390, start));
        
        //if (!player.IsOnGCD())
        //{
        //    if (player.Auras.GetRemainingDuration(Glossaries.Auras.SealOfCommand, start) < 5000)
        //        return new CastEvent(start + 0, player, Spells.SealOfCommand);

        //    else if (!player.Spellbook.IsOnCooldown(Spells.CrusaderStrike))
        //        return new CastEvent(start + 0, player, Spells.CrusaderStrike);
        //}

        //if (!fight.Player.GCD.Active && !fight.Player.Spellbook.IsOnCooldown(Spells.CrusaderStrike))
        //    return new CastEvent(Spells.CrusaderStrike, fight, fight.Timestamp);

        else
            return null;
    }
}