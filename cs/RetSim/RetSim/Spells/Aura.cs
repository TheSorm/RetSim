using RetSim.Spells.AuraEffects;
using System.Text.Json.Serialization;
using System.ComponentModel;

namespace RetSim.Spells;

public class Aura
{
    [JsonIgnore]
    public Spell Parent { get; set; }
    public virtual int Duration { get; init; } = 0;

    [DefaultValue(1)]
    public int MaxStacks { get; init; } = 1;

    public bool IsDebuff { get; init; } = false;

    [JsonConverter(typeof(Data.JSON.AuraEffectConverter))]
    public List<AuraEffect> Effects { get; set; } = null;

    public override string ToString()
    {
        return Parent.Name;
    }
}

public class Seal : Aura
{
    public int SealID { get; init; }
    public override int Duration { get; init; } = 30000;
    public int Persist { get; init; } = 0;

    [JsonIgnore]
    public List<Seal> ExclusiveWith { get; set; }

    [JsonIgnore]
    public Judgement Judgement { get; set; }
}

public enum AuraChangeType
{
    Gain = 1,
    Fade = 2,
    Refresh = 3
}