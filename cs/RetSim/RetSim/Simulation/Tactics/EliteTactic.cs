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
    private bool useExorcism = true;
    private bool useConsecration = true;

    public EliteTactic(int maxCSDelay, bool useExorcism, bool useConsecration)
    {
        CrusaderStrike = Data.Collections.Spells[35395];
        SealOfCommand = Data.Collections.Spells[27170];
        SealOfBlood = Data.Collections.Spells[31892];
        Judgement = Data.Collections.Spells[20271];
        Exorcism = Data.Collections.Spells[27138];
        Consecration = Data.Collections.Spells[27173];

        csDelay = maxCSDelay;
        this.useExorcism = useExorcism;
        this.useConsecration = useConsecration;
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

        int spellGCD = fight.Player.Stats.EffectiveGCD(SealOfBlood);

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
                if(fight.Player.Weapon.EffectiveSpeed > 2 * spellGCD && start + spellGCD < swingLeeway)
                {
                    return CastFiller(start, end, fight);
                }
                else
                {
                    int sobTwistWindowStart = swing - 399;
                    int sobTwistWindowEnd = swing - 1;

                    if (end >= sobTwistWindowStart && start <= sobTwistWindowEnd)
                        return new CastEvent(SealOfBlood, fight.Player, fight.Player, fight, Math.Max(start, sobTwistWindowStart));
                } 
            }
            else
            {
                if (fight.Player.Spellbook.IsOnCooldown(CrusaderStrike)
                && !(fight.Player.Spellbook[CrusaderStrike.ID].CooldownEnd.Timestamp < swingLeeway
                     && swingLeeway - 400 + spellGCD > fight.Player.Spellbook[CrusaderStrike.ID].CooldownEnd.Timestamp + maxCSDelay))
                {
                    if (start + spellGCD < swingLeeway)
                    {
                        if (fight.Player.Auras[SealOfBlood.Aura].Active && !fight.Player.Spellbook.IsOnCooldown(Judgement))
                            return new CastEvent(Judgement, fight.Player, fight.Enemy, fight, start);

                        else if (!fight.Player.Auras[SealOfCommand.Aura].Active
                            && (!fight.Player.Auras[SealOfBlood.Aura].Active
                            || (fight.Player.Spellbook.IsOnCooldown(Judgement) && fight.Player.Spellbook[Judgement.ID].CooldownEnd.Timestamp + spellGCD > swingLeeway)))
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

                        else if(fight.Player.Weapon.EffectiveSpeed > 2 * spellGCD && start + spellGCD < fight.Player.Spellbook[CrusaderStrike.ID].CooldownEnd.Timestamp) { }
                            return CastFiller(start, end, fight);
                    }
                }
                else if (!fight.Player.Spellbook.IsOnCooldown(CrusaderStrike))
                {
                    if (fight.Player.Auras[SealOfBlood.Aura].Active || start + spellGCD < swingLeeway)
                        return new CastEvent(CrusaderStrike, fight.Player, fight.Enemy, fight, start);

                    else
                        return new CastEvent(SealOfBlood, fight.Player, fight.Player, fight, start);
                }
                else if (fight.Player.Weapon.EffectiveSpeed > 2 * spellGCD && start + spellGCD < fight.Player.Spellbook[CrusaderStrike.ID].CooldownEnd.Timestamp)
                {
                    return CastFiller(start, end, fight);
                }
            }
        }

        return null;
    }

    private Event CastFiller(int start, int end, FightSimulation fight)
    {
        if (!fight.Player.Spellbook.IsOnCooldown(Exorcism) && useExorcism)
        {
            return new CastEvent(Exorcism, fight.Player, fight.Player, fight, start);
        }
        else if(!fight.Player.Spellbook.IsOnCooldown(Consecration) && useConsecration)
        {
            return new CastEvent(Consecration, fight.Player, fight.Player, fight, start);
        }
        return null;
    }
}