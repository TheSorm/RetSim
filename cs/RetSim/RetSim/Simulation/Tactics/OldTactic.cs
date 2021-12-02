using RetSim.Simulation.Events;
using RetSim.Spells;

namespace RetSim.Simulation.Tactics;

public class OldTactic : Tactic
{
    Spell trinket1 = null;
    Spell trinket2 = null;

    Spell CrusaderStrike;
    Spell SealOfCommand;
    Spell SealOfBlood;
    Spell Judgement;
    Spell AvengingWrath;
    Spell Heroism;
    Spell HastePotion;

    public OldTactic()
    {
        CrusaderStrike = Data.Collections.Spells[35395];
        SealOfCommand = Data.Collections.Spells[27170];
        SealOfBlood = Data.Collections.Spells[31892];
        Heroism = Data.Collections.Spells[32182];
        AvengingWrath = Data.Collections.Spells[31884];
        Judgement = Data.Collections.Spells[20271];

        HastePotion = Data.Collections.Spells[28507];

    }

    public override List<Event> PreFight(FightSimulation fight)
    {
        var onStart = new List<Event>()
            {
                new CastEvent(SealOfCommand, fight.Player, fight.Player, fight, 0),
                new CastEvent(AvengingWrath, fight.Player, fight.Player, fight, 1495),
                new CastEvent(Heroism, fight.Player, fight.Player, fight, 1495),
                new CastEvent(HastePotion, fight.Player, fight.Player, fight, 1495),
                new CastEvent(SealOfBlood, fight.Player, fight.Player, fight, 1500),
                new AutoAttackEvent(fight, 1501)
            };

        if (fight.Player.Equipment.Trinket1 != null && fight.Player.Equipment.Trinket1.OnUse != null && Data.Collections.Spells.ContainsKey(fight.Player.Equipment.Trinket1.OnUse.ID))
            trinket1 = Data.Collections.Spells[fight.Player.Equipment.Trinket1.OnUse.ID];

        if (fight.Player.Equipment.Trinket2 != null && fight.Player.Equipment.Trinket2.OnUse != null && Data.Collections.Spells.ContainsKey(fight.Player.Equipment.Trinket2.OnUse.ID))
            trinket2 = Data.Collections.Spells[fight.Player.Equipment.Trinket2.OnUse.ID];

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


        int gcdDuration = fight.Player.Stats.EffectiveGCD(SealOfBlood);
        int gcdRemaining = fight.Player.GCD.GetDuration(start);
        int crusaderStrikeCooldownEnd = fight.Player.Spellbook.IsOnCooldown(CrusaderStrike) ? fight.Player.Spellbook[CrusaderStrike.ID].CooldownEnd.Timestamp : start;
        int twistWindowEnd = fight.Player.TimeOfNextSwing() - gcdDuration + 1;

        if (!fight.Player.Auras[SealOfCommand.Aura].Active && start < twistWindowEnd && crusaderStrikeCooldownEnd > fight.Player.TimeOfNextSwing())
        {
            if (!fight.Player.Spellbook.IsOnCooldown(Judgement) && fight.Player.Auras[SealOfBlood.Aura].Active && start + gcdRemaining < twistWindowEnd)
                return new CastEvent(Judgement, fight.Player, fight.Enemy, fight, fight.Timestamp);

            if (!fight.Player.GCD.Active && fight.Player.Spellbook.IsOnCooldown(Judgement) && fight.Player.Spellbook[Judgement.ID].CooldownEnd.Timestamp > twistWindowEnd)
                return new CastEvent(SealOfCommand, fight.Player, fight.Player, fight, start);
        }

        if (!fight.Player.GCD.Active && !fight.Player.Auras[SealOfCommand.Aura].Active && !fight.Player.Spellbook.IsOnCooldown(CrusaderStrike))
        {
            return new CastEvent(CrusaderStrike, fight.Player, fight.Player, fight, start);
        }

        int sobTwistWindowStart = fight.Player.TimeOfNextSwing() - 390;
        int sobTwistWindowEnd = fight.Player.TimeOfNextSwing() - 1;

        /** 
        //Cast SoC At the last possible moment before the swing
        if (fight.Player.Auras[SealOfCommand.Aura].Active && end >= sobTwistWindowEnd && start <= sobTwistWindowEnd) 
            return new CastEvent(SealOfBlood, fight.Player, fight.Player, fight, sobTwistWindowEnd);
        */

        // Cast SoB at the start of the tactic window where the next event is the auto attack
        if (!fight.Player.GCD.Active && ((fight.Player.Auras[SealOfCommand.Aura].Active && end >= sobTwistWindowEnd && start <= sobTwistWindowEnd) || (!fight.Player.Auras[SealOfCommand.Aura].Active && !fight.Player.Auras[SealOfBlood.Aura].Active) && fight.Player.TimeOfNextSwing() < gcdDuration + 1))
            return new CastEvent(SealOfBlood, fight.Player, fight.Player, fight, Math.Max(start, sobTwistWindowStart));

        /**
        // Cast SoB as early as possible (Can lead to missed twists due to haste changes before auto attack)
        if (fight.Player.Auras[SealOfCommand.Aura].Active && end >= sobTwistWindowEnd && start <= sobTwistWindowEnd || end >= sobTwistWindowStart && start <= sobTwistWindowEnd)
            return new CastEvent(SealOfBlood, fight.Player, fight.Player, fight, Math.Max(start, sobTwistWindowStart));
        */

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