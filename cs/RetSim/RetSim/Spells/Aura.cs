using RetSim.Spells.AuraEffects;
using System.Text.Json.Serialization;

namespace RetSim.Spells;

public class Aura
{
    [JsonIgnore]
    public Spell Parent { get; private set; }
    public int Duration { get; init; }
    public int MaxStacks { get; init; }
    public bool IsDebuff { get; init; }

    public List<AuraEffect> Effects { get; init; }

    public Aura(int duration = 0, int maxStacks = 1, bool debuff = false, List<AuraEffect> effects = null)
    {
        Duration = duration;
        MaxStacks = maxStacks;
        IsDebuff = debuff;
        Effects = effects;
    }

    public static void Instantiate(Aura aura, Spell spell)
    {
        aura.Parent = spell;
    }

    public override string ToString()
    {
        string duration = (Duration == 0) ? "Toggle" : $"{Duration / 1000}";

        return $"{Parent.Name} - Duration: {duration}s  / Max Stacks: {MaxStacks}";
    }
}

public class Seal : Aura
{
    [JsonIgnore]
    public Judgement Judgement { get; private set; }
    public int JudgementID { get; init; }

    [JsonIgnore]
    public List<Seal> ExclusiveWith { get; private set; }
    public List<int> ExclusiveIDs { get; init; }

    public int Persist { get; init; }

    public Seal(int judgement, List<int> exclusiveWith, int persist = 0, int duration = 0, int maxStacks = 1, bool debuff = false, List<AuraEffect> effects = null) : base(duration, maxStacks, debuff, effects)
    {
        JudgementID = judgement;
        ExclusiveIDs = exclusiveWith;
        Persist = persist;
    }

    public static void Instantiate(Seal seal, Spell spell)
    {
        Aura.Instantiate(seal, spell);

        seal.Judgement = Data.Collections.Judgements[seal.JudgementID];

        seal.ExclusiveWith = new();

        foreach (int other in seal.ExclusiveIDs)
        {
            seal.ExclusiveWith.Add((Seal)Data.Collections.Seals[other].Aura);
        }
    }
}

public enum AuraChangeType
{
    Gain = 1,
    Fade = 2,
    Refresh = 3
}