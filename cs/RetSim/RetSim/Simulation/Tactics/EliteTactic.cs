using RetSim.Simulation.Events;
using RetSim.Spells;

namespace RetSim.Simulation.Tactics;

public class EliteTactic : Tactic
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

    public EliteTactic()
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
        var spellGCD = fight.Player.Stats.EffectiveGCD(SealOfBlood);

        var firstAutoAttack = new AutoAttackEvent(fight, spellGCD + 1);
        var onStart = new List<Event>()
            {                
                new CastEvent(SealOfCommand, fight.Player, fight.Player, fight, 0),                
                //new CastEvent(SealOfBlood, fight.Player, fight.Player, fight, 0),
                new CastEvent(AvengingWrath, fight.Player, fight.Player, fight, spellGCD - 1),
                //new CastEvent(CrusaderStrike, fight.Player, fight.Player, fight, spellGCD),
                //new CastEvent(Heroism, fight.Player, fight.Player, fight, 1495),
                //new CastEvent(HastePotion, fight.Player, fight.Player, fight, 1495),
                //new CastEvent(SealOfBlood, fight.Player, fight.Player, fight, 1500),
                firstAutoAttack,
            };
        fight.Player.NextAutoAttack = firstAutoAttack;
        fight.Player.EffectiveNextAuto = firstAutoAttack.Timestamp;

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

        int hasteLeeway = 0;
        int maxCSDelay = 0;

        int swing = fight.Player.EffectiveNextAuto;
        int swingLeeway = swing - hasteLeeway;

        if (fight.Player.GCD.Active)
        {
            if (!fight.Player.Spellbook.IsOnCooldown(Judgement)
                && fight.Player.Auras[SealOfBlood.Aura].Active
                && fight.Player.Spellbook.IsOnCooldown(CrusaderStrike) && fight.Player.Spellbook[CrusaderStrike.ID].CooldownEnd.Timestamp > swingLeeway
                && fight.Player.GCD.GetEnd < swingLeeway)
            {
                return new CastEvent(Judgement, fight.Player, fight.Enemy, fight, start);
            }
        }
        else
        {
            if (fight.Player.Auras[SealOfCommand.Aura].Active)
            {
                int sobTwistWindowStart = swing - 399;
                int sobTwistWindowEnd = swing - 1;
                if (end >= sobTwistWindowStart && start <= sobTwistWindowEnd)
                    return new CastEvent(SealOfBlood, fight.Player, fight.Player, fight, Math.Max(start, sobTwistWindowStart));
            }
            else
            {
                if (fight.Player.Spellbook.IsOnCooldown(CrusaderStrike)
                && !(fight.Player.Spellbook[CrusaderStrike.ID].CooldownEnd.Timestamp < swingLeeway
                     && swingLeeway - 400 + fight.Player.Stats.EffectiveGCD(SealOfBlood) > fight.Player.Spellbook[CrusaderStrike.ID].CooldownEnd.Timestamp + maxCSDelay))
                {
                    if (start + fight.Player.Stats.EffectiveGCD(SealOfBlood) < swingLeeway)
                    {

                        if (fight.Player.Auras[SealOfBlood.Aura].Active && !fight.Player.Spellbook.IsOnCooldown(Judgement))
                        {
                            return new CastEvent(Judgement, fight.Player, fight.Enemy, fight, start);
                        }
                        else if (!fight.Player.Auras[SealOfCommand.Aura].Active
                            && (!fight.Player.Auras[SealOfBlood.Aura].Active
                            || (fight.Player.Spellbook.IsOnCooldown(Judgement) && fight.Player.Spellbook[Judgement.ID].CooldownEnd.Timestamp + fight.Player.Stats.EffectiveGCD(SealOfBlood) > swingLeeway)))
                        {
                            return new CastEvent(SealOfCommand, fight.Player, fight.Enemy, fight, start);
                        }
                    }
                    else
                    {
                        if (!fight.Player.Auras[SealOfBlood.Aura].Active)
                        {
                            return new CastEvent(SealOfBlood, fight.Player, fight.Enemy, fight, start);
                        }
                        else if (!fight.Player.Spellbook.IsOnCooldown(Judgement) && start < swingLeeway)
                        {
                            return new CastEvent(Judgement, fight.Player, fight.Enemy, fight, start);
                        }
                    }
                }
                else if (!fight.Player.Spellbook.IsOnCooldown(CrusaderStrike))
                {
                    if (fight.Player.Auras[SealOfBlood.Aura].Active || start + fight.Player.Stats.EffectiveGCD(CrusaderStrike) < swingLeeway)
                    {
                        return new CastEvent(CrusaderStrike, fight.Player, fight.Enemy, fight, start);
                    }
                    else
                    {
                        return new CastEvent(SealOfBlood, fight.Player, fight.Enemy, fight, start);
                    }
                }
            }
        }

        return null;
    }
}