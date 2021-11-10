using Newtonsoft.Json;
using RetSim.Spells;

namespace RetSim.Data.JSON;

public class ProcConverter : JsonConverter<Proc>
{
    public override Proc ReadJson(JsonReader reader, Type objectType, Proc existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override void WriteJson(JsonWriter writer, Proc value, JsonSerializer serializer)
    {
        writer.WriteStartObject();

        writer.WriteData("ID", value.ID);
        writer.WriteData("Name", value.Spell.Name);
        writer.WriteData("SpellID", value.Spell.ID);
        writer.WriteData("ProcMask", value.ProcMask);
        
        if (value.GuaranteedProc)
            writer.WriteData("GuaranteedProc", value.GuaranteedProc);

        if (value.Chance != 0)
            writer.WriteData("Chance", value.Chance);

        if (value.PPM != 0)
            writer.WriteData("PPM", value.PPM);

        if (value.Cooldown != 0)
            writer.WriteData("Cooldown", value.Cooldown);

        writer.WriteEndObject();
    }
}
