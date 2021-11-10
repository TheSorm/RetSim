using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RetSim.Spells;
using System.Linq;

namespace RetSim.Data.JSON;

public class AuraConverter : JsonConverter<Aura>
{
    public override Aura ReadJson(JsonReader reader, Type objectType, Aura existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jo = JObject.Load(reader);

        Aura aura;

        string test = (string)jo["AuraType"];

        switch ((string)jo["AuraType"])
        {
            case "Aura":
                aura = new Aura();
                break;

            case "Seal":
                aura = new Seal();
                break;

            default:
                throw new NotImplementedException();

        }

        serializer.Populate(jo.CreateReader(), aura);

        return aura;
    }

    public override void WriteJson(JsonWriter writer, Aura value, JsonSerializer serializer)
    {
        writer.WriteStartObject();

        Seal seal = null;
        bool isSeal = false;

        if (value is Seal s)
        {
            seal = s;
            isSeal = true;
        }

        if (isSeal)
            writer.WriteData("AuraType", AuraType.Seal.ToString());

        else
            writer.WriteData("AuraType", AuraType.Aura.ToString());

        if (value.Duration != 0)
            writer.WriteData("Duration", value.Duration);

        if (value.MaxStacks != 1)
            writer.WriteData("MaxStacks", value.MaxStacks);

        if (value.IsDebuff)
            writer.WriteData("IsDebuff", value.IsDebuff);


        if (isSeal)
        {
            if (seal.Persist != 0)
                writer.WriteData("Persist", seal.Persist);

            List<int> seals = new();

            foreach (Seal other in seal.ExclusiveWith)
                seals.Add(other.Parent.ID);
            
            writer.WriteArray("Exclusives", seals.Cast<object>().ToArray(), "SealID");

            writer.WriteData("JudgementID", seal.Judgement.ID);
        }

        writer.WriteEndObject();
    }
}
