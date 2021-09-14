using RetSim.Simulation;
using RetSim.Simulation.CombatLogEntries;
using RetSim.Simulation.Events;
using RetSim.Spells;
using RetSim.Spells.AuraEffects;
using RetSim.Units.UnitStats;

namespace RetSim.Units;

public class Auras : Dictionary<Aura, AuraState>
{
    public Seal CurrentSeal { get; set; } = null;

    private AuraList Buffs { get; init; } = new();
    private AuraList Debuffs { get; init; } = new();
    private AuraList Passives { get; init; } = new();

    public void Add(Aura aura)
    {
        if (aura.IsDebuff)
            Debuffs.Add(aura, new AuraState());

        else if (aura.Duration == 0)
            Passives.Add(aura, new AuraState());

        else
            Buffs.Add(aura, new AuraState());
    }

    public new AuraState this[Aura aura]
    {
        get
        {
            if (aura.IsDebuff)
                return Debuffs[aura];

            else if (aura.Duration == 0)
                return Passives[aura];

            else
                return Buffs[aura];
        }

        set
        {
            if (aura.IsDebuff)
                Debuffs[aura] = value;

            else if (aura.Duration == 0)
                Passives[aura] = value;

            else
                Buffs[aura] = value;
        }
    }

    public bool IsActive(Aura aura)
    {
        return this[aura].Active;
    }

    public int GetRemainingDuration(Aura aura, int time)
    {
        AuraState state = this[aura];

        if (!state.Active || state.End == null)
            return 0;

        else
            return state.End.Timestamp - time;
    }

    public void Apply(Aura aura, Unit caster, Unit target, FightSimulation fight, int extraDuration = 0)
    {
        AuraChangeType result = ApplyAura(aura, caster, target, fight, extraDuration);

        LogResult(aura, fight, result);
    }

    public void Cancel(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        bool result = CancelAura(aura, caster, target, fight);

        if (result)
            LogResult(aura, fight, AuraChangeType.Fade);
    }

    private static void LogResult(Aura aura, FightSimulation fight, AuraChangeType result)
    {
        if (aura.IsDebuff)
            LogDebuff(aura, fight, result);

        else
            LogBuff(aura, fight, result);
    }

    private AuraChangeType ApplyAura(Aura aura, Unit caster, Unit target, FightSimulation fight, int extraDuration = 0)
    {
        if (this[aura].Active)
        {
            if (aura.Duration > 0)
                this[aura].End.Timestamp = fight.Timestamp + aura.Duration + extraDuration;

            if (this[aura].Stacks < aura.MaxStacks)
            {
                ApplyEffects(aura, caster, target, fight);

                return AuraChangeType.Gain;
            }

            else
                return AuraChangeType.Refresh;
        }

        else
        {
            ApplyEffects(aura, caster, target, fight);

            if (aura.Duration > 0)
            {
                this[aura].End = new AuraEndEvent(aura, caster, target, fight, fight.Timestamp + aura.Duration + extraDuration);

                fight.Queue.Add(this[aura].End);
            }

            return AuraChangeType.Gain;
        }
    }

    private void ApplyEffects(Aura aura, Unit caster, Unit target, FightSimulation fight)
    {
        this[aura].Stacks++;

        if (aura.Effects != null)
        {
            foreach (AuraEffect effect in aura.Effects)
            {
                effect.Apply(aura, caster, target, fight);
            }
        }
    }

    private bool CancelAura(Aura aura, Unit caster, Unit target, FightSimulation fight, bool log = true)
    {
        if (this[aura].Active)
        {
            while (this[aura].Stacks > 0)
            {
                foreach (AuraEffect effect in aura.Effects)
                    effect.Remove(aura, caster, target, fight);

                this[aura].Stacks--;
            }

            this[aura].End = null;

            return true;
        }

        else
            return false;
    }

    private static void LogBuff(Aura buff, FightSimulation fight, AuraChangeType type)
    {
        var entry = new BuffEntry()
        {
            Timestamp = fight.Timestamp,
            Mana = (int)fight.Player.Stats[StatName.Mana].Value,
            Source = buff.Parent.Name,
            Stacks = fight.Player.Auras[buff].Stacks,
            Type = type
        };

        fight.CombatLog.Add(entry);
    }

    private static void LogDebuff(Aura debuff, FightSimulation fight, AuraChangeType type)
    {
        var entry = new DebuffEntry()
        {
            Timestamp = fight.Timestamp,
            Mana = (int)fight.Player.Stats[StatName.Mana].Value,
            Source = debuff.Parent.Name,
            Stacks = fight.Player.Auras[debuff].Stacks,
            Type = type
        };

        fight.CombatLog.Add(entry);
    }
}

public class AuraList : Dictionary<Aura, AuraState>
{
    public bool IsActive(Aura aura)
    {
        return this[aura].Active;
    }

    public int GetRemainingDuration(Aura aura, int time)
    {
        if (!this[aura].Active || this[aura].End == null)
            return 0;

        else
            return this[aura].End.Timestamp - time;
    }
}

public class AuraState
{
    public bool Active => Stacks > 0;
    public AuraEndEvent End { get; set; }
    public int Stacks { get; set; }

    public AuraState()
    {
        End = null;
        Stacks = 0;
    }
}