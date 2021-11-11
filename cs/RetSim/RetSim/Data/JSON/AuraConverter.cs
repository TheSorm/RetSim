using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RetSim.Spells;
using RetSim.Spells.AuraEffects;
using RetSim.Units.UnitStats;
using System.Linq;

namespace RetSim.Data.JSON;

public class AuraConverter : JsonConverter<Aura>
{
    public override Aura ReadJson(JsonReader reader, Type objectType, Aura existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jo = JObject.Load(reader);

        Aura aura;

        string test = (string)jo["AuraType"];

        List<AuraEffect> effects = new();

        if (jo["Effects"] != null)
        {
            for (int i = 0; i < jo["Effects"].Count(); i++)
            {
                switch ((string)jo["Effects"][i]["EffectType"])
                {
                    case "GainProc":
                        effects.Add(new GainProc((int)jo["Effects"][i]["ProcID"]));
                        break;
                }
            }
        }

        int duration = (int)(jo["Duration"] ?? 0);
        int maxStacks = (int)(jo["MaxStacks"] ?? 1);
        bool isDebuff = (bool)(jo["IsDebuff"] ?? false);

        //string test2 = (string)jo["Effects"][0]["EffectType"];

        switch ((string)jo["AuraType"])
        {
            case "Aura":
                aura = new Aura(effects) { Duration =  duration, MaxStacks = maxStacks, IsDebuff = isDebuff, Effects = effects };
                break;

            case "Seal":
                aura = new Seal(effects) { Duration = duration, MaxStacks = maxStacks, IsDebuff = isDebuff, Effects = effects, Persist = (int)(jo["Persist"] ?? 0), };
                break;

            default:
                throw new NotImplementedException();

        }

        aura.Effects = null;



        return aura;
    }

    public override void WriteJson(JsonWriter writer, Aura value, JsonSerializer serializer)
    {
        //serializer.Converters.Add(new AuraEffectConverter());
        
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
            
            writer.WriteIntArray("Exclusives", seals.Cast<int>().ToArray());

            writer.WriteData("JudgementID", seal.Judgement.ID);
        }

        if (value.Effects != null && value.Effects.Count > 0)
        {
            writer.WritePropertyName("Effects");
            writer.WriteStartArray();

            foreach (AuraEffect effect in value.Effects)
            {
                WriteAuraEffects(writer, effect);
            }

            writer.WriteEndArray();

        }

        writer.WriteEndObject();
    }

    public void WriteAuraEffects(JsonWriter writer, AuraEffect value)
    {
        writer.WriteStartObject();        

        if (value is GainSeal gainSeal)
        {
            writer.WriteData("EffectType", AuraEffectType.GainSeal.ToString());
            writer.WriteData("ProcID", gainSeal.Proc.ID);
        }

        else if (value is GainProc gainProc)
        {
            writer.WriteData("EffectType", AuraEffectType.GainProc.ToString());
            writer.WriteData("ProcID", gainProc.Proc.ID);
        }

        else if (value is ModAttackSpeed modAttackSpeed)
        {
            writer.WriteData("EffectType", AuraEffectType.ModAttackSpeed.ToString());
            writer.WriteData("Percent", modAttackSpeed.Percent);
        }

        else if (value is ModDamageCreature modDamageCreature)
        {
            writer.WriteData("EffectType", AuraEffectType.ModDamageCreature.ToString());
            writer.WriteData("Percent", modDamageCreature.Percent);
            writer.WriteData("SchoolMask", modDamageCreature.SchoolMask);

            string[] creatures = new string[modDamageCreature.Types.Count];

            for (int i = 0; i < modDamageCreature.Types.Count; i++)
            {
                creatures[i] = modDamageCreature.Types[i].ToString();
            }

            writer.WriteStringArray("Creatures", creatures);
        }

        else if (value is ModDamageSchool modDamageSchool)
        {
            writer.WriteData("EffectType", AuraEffectType.ModDamageSchool.ToString());
            writer.WriteData("Percent", modDamageSchool.Percent);
            writer.WriteData("SchoolMask", modDamageSchool.SchoolMask);
        }

        else if (value is ModDamageSpell modDamageSpell)
        {
            writer.WriteData("EffectType", AuraEffectType.ModDamageSpell.ToString());
            writer.WriteData("Percent", modDamageSpell.Percent);
            writer.WriteIntArray("Spells", modDamageSpell.Spells.Cast<int>().ToArray());
        }

        else if (value is ModDamageTaken modDamageTaken)
        {
            writer.WriteData("EffectType", AuraEffectType.ModDamageTaken.ToString());
            writer.WriteData("Percent", modDamageTaken.Percent);
            writer.WriteData("SchoolMask", modDamageTaken.SchoolMask);
        }

        else if (value is GainStats modifyStats)
        {
            writer.WriteData("EffectType", AuraEffectType.GainStats.ToString());

            Dictionary<StatName, float> stats = new();

            foreach (KeyValuePair<StatName, float> stat in modifyStats.Stats)
            {
                if (stat.Value != 0f)
                    stats.Add(stat.Key, stat.Value);
            }

            List<string> names = new();
            List<float> values = new();

            foreach (KeyValuePair<StatName, float> stat in modifyStats.Stats)
            {
                if (stat.Value != 0f)
                {
                    names.Add(stat.Key.ToString());
                    values.Add(stat.Value);
                }
            }

            writer.WriteDictionary("Stats", names.ToArray(), values.Cast<object>().ToArray(), "Stat", "Value");
        }

        else if (value is ModSpellCritChance modSpellCritChance)
        {
            writer.WriteData("EffectType", AuraEffectType.ModSpellCritChance.ToString());
            writer.WriteData("Amount", modSpellCritChance.Amount);
            writer.WriteIntArray("Spells", modSpellCritChance.Spells.Cast<int>().ToArray());
        }

        else if (value is ModSpellDamageTaken modSpellDamageTaken)
        {
            writer.WriteData("EffectType", AuraEffectType.ModSpellDamageTaken.ToString());
            writer.WriteData("Amount", modSpellDamageTaken.Amount);
            writer.WriteData("School", modSpellDamageTaken.School.ToString());
        }

        else if (value is ModStat modStat)
        {
            writer.WriteData("EffectType", AuraEffectType.ModStat.ToString());
            writer.WriteData("Percent", modStat.Percent);

            List<string> stats = new();

            foreach (StatName stat in modStat.Stats)
            {
                stats.Add(stat.ToString());
            }

            writer.WriteStringArray("Stats", stats.Cast<string>().ToArray());
        }

        writer.WriteEnd();
    }
}
