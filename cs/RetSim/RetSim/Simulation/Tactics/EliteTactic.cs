using RetSim.Simulation.Events;
using RetSim.Spells;

namespace RetSim.Simulation.Tactics;

public class EliteTactic : Tactic
{
    Spell CrusaderStrike;
    Spell SealOfCommand;
    Spell SealOfBlood;
    Spell Judgement;
    Spell Exorcism;
    Spell Consecration;

    private int csDelay = 0;

    public EliteTactic(int maxCSDelay)
    {
        CrusaderStrike = Data.Collections.Spells[35395];
        SealOfCommand = Data.Collections.Spells[27170];
        SealOfBlood = Data.Collections.Spells[31892];
        Judgement = Data.Collections.Spells[20271];
        Exorcism = Data.Collections.Spells[27138];
        Consecration = Data.Collections.Spells[27173];

        csDelay = maxCSDelay;
    }

    public override List<Event> PreFight(FightSimulation fight)
    {
        var spellGCD = fight.Player.Stats.EffectiveGCD(SealOfBlood);

        var firstAutoAttack = new AutoAttackEvent(fight, spellGCD + 1);
        var onStart = new List<Event>()
            {
                new CastEvent(SealOfCommand, fight.Player, fight.Player, fight, 0),                
                //new CastEvent(SealOfBlood, fight.Player, fight.Player, fight, 0),
                //new CastEvent(CrusaderStrike, fight.Player, fight.Player, fight, spellGCD),

                firstAutoAttack,
            };

        fight.Player.NextAutoAttack = firstAutoAttack;
        fight.Player.EffectiveNextAuto = firstAutoAttack.Timestamp;

        return onStart;
    }

    public override Event GetActionBetween(int start, int end, FightSimulation fight)
    {
        int hasteLeeway = 0;
        int maxCSDelay = csDelay;

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
                            return new CastEvent(Judgement, fight.Player, fight.Enemy, fight, start);

                        else if (!fight.Player.Auras[SealOfCommand.Aura].Active
                            && (!fight.Player.Auras[SealOfBlood.Aura].Active
                            || (fight.Player.Spellbook.IsOnCooldown(Judgement) && fight.Player.Spellbook[Judgement.ID].CooldownEnd.Timestamp + fight.Player.Stats.EffectiveGCD(SealOfBlood) > swingLeeway)))
                        {
                            return new CastEvent(SealOfCommand, fight.Player, fight.Player, fight, start);
                        }
                    }

                    else
                    {
                        if (!fight.Player.Auras[SealOfBlood.Aura].Active)
                            return new CastEvent(SealOfBlood, fight.Player, fight.Player, fight, start);

                        else if (!fight.Player.Spellbook.IsOnCooldown(Judgement) && start < swingLeeway)
                            return new CastEvent(Judgement, fight.Player, fight.Enemy, fight, start);
                    }
                }

                else if (!fight.Player.Spellbook.IsOnCooldown(CrusaderStrike))
                {
                    if (fight.Player.Auras[SealOfBlood.Aura].Active || start + fight.Player.Stats.EffectiveGCD(CrusaderStrike) < swingLeeway)
                        return new CastEvent(CrusaderStrike, fight.Player, fight.Enemy, fight, start);

                    else
                        return new CastEvent(SealOfBlood, fight.Player, fight.Player, fight, start);
                }
            }
        }

        return null;
    }
}