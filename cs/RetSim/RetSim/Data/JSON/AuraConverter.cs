using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RetSim.Spells;
using RetSim.Spells.AuraEffects;
using RetSim.Units.Enemy;
using RetSim.Units.UnitStats;
using System.Linq;

namespace RetSim.Data.JSON;

public class AuraConverter : JsonConverter<Aura>
{
    private List<AuraEffect> ReadAuraEffects(JObject jo)
    {
        if (jo["Effects"] == null)
            return null;

        else
        {
            List<AuraEffect> effects = new();

            for (int i = 0; i < jo["Effects"].Count(); i++)
            {
                string effectType = (string)jo["Effects"][i]["EffectType"];
                float value = (float)jo["Effects"][i]["Value"];

                switch (effectType)
                {
                    case "GainSeal":
                        effects.Add(new GainSeal(value));
                        break;

                    case "GainProc":
                        effects.Add(new GainProc(value));
                        break;

                    case "ModAttackSpeed":
                        effects.Add(new ModAttackSpeed(value));
                        break;

                    case "ModDamageDoneCreature":

                        List<CreatureType> creatures = new();

                        int count = jo["Effects"][i]["Creatures"].Count();

                        for (int y = 0; y < count; y++)
                        {
                            creatures.Add(Enum.Parse<CreatureType>((string)jo["Effects"][i]["Creatures"][y]));
                        }

                        effects.Add(new ModDamageDoneCreature(value, (School)(int)jo["Effects"][i]["SchoolMask"], creatures));

                        break;

                    case "ModDamageDone":
                        effects.Add(new ModDamageDone(value, (School)(int)jo["Effects"][i]["SchoolMask"]));
                        break;

                    case "ModDamageTaken":
                        effects.Add(new ModDamageTaken(value, Enum.Parse<School>((string)jo["Effects"][i]["School"])));
                        break;

                    case "ModDamageTakenPercent":
                        effects.Add(new ModDamageTakenPercent(value, (School)(int)jo["Effects"][i]["SchoolMask"]));
                        break;

                    case "ModSpellCritChance":

                        List<int> spellCritChance = new();

                        int spellCritCount = jo["Effects"][i]["Spells"].Count();

                        for (int y = 0; y < spellCritCount; y++)
                        {
                            spellCritChance.Add((int)jo["Effects"][i]["Spells"][y]);
                        }

                        effects.Add(new ModSpellCritChance(value, spellCritChance));

                        break;

                    case "ModStatCreature":

                        List<StatName> mscStats = new();
                        int mscStatsCount = jo["Effects"][i]["Stats"].Count();

                        for (int j = 0; j < mscStatsCount; j++)
                        {
                            mscStats.Add(Enum.Parse<StatName>((string)jo["Effects"][i]["Stats"][j]));
                        }

                        List<CreatureType> mscCreatures = new();
                        int mscCreaturesCount = jo["Effects"][i]["Creatures"].Count();

                        for (int y = 0; y < mscCreaturesCount; y++)
                        {
                            mscCreatures.Add(Enum.Parse<CreatureType>((string)jo["Effects"][i]["Creatures"][y]));
                        }

                        effects.Add(new ModStatCreature(value, mscStats, mscCreatures));

                        break;

                    case "ModStat":

                        List<StatName> msStats = new();
                        int msStatsCount = jo["Effects"][i]["Stats"].Count();

                        for (int j = 0; j < msStatsCount; j++)
                        {
                            msStats.Add(Enum.Parse<StatName>((string)jo["Effects"][i]["Stats"][j]));
                        }

                        effects.Add(new ModStat(value, msStats));

                        break;   
                    
                    case "ModStatPercent":

                        List<StatName> mspStats = new();
                        int mspStatsCount = jo["Effects"][i]["Stats"].Count();

                        for (int j = 0; j < mspStatsCount; j++)
                        {
                            mspStats.Add(Enum.Parse<StatName>((string)jo["Effects"][i]["Stats"][j]));
                        }

                        effects.Add(new ModStatPercent(value, mspStats));

                        break;

                    default:
                        throw new Exception($"Failed to deserialize effect of type {effectType} - type not recognized");
                }
            }

            return effects;
        }
    }

    private Aura ReadAura(JObject jo, List<AuraEffect> effects)
    {
        Aura aura;

        int duration = (int)(jo["Duration"] ?? 0);
        int maxStacks = (int)(jo["MaxStacks"] ?? 1);
        bool isDebuff = (bool)(jo["IsDebuff"] ?? false);

        bool seal = (bool)(jo["Seal"] ?? false);

        if (seal)
        {
            int judgement = (int)(jo["JudgementID"] ?? 0);
            int persist = (int)(jo["Persist"] ?? 0);

            List<int> exclusives = new();

            int exclusivesCount = jo["Exclusives"].Count();

            for (int y = 0; y < exclusivesCount; y++)
            {
                exclusives.Add((int)jo["Exclusives"][y]);
            }

            aura = new Seal(judgement, exclusives, persist, duration, maxStacks, isDebuff, effects);
        }

        else
            aura = new Aura(duration, maxStacks, isDebuff) { Duration = duration, MaxStacks = maxStacks, IsDebuff = isDebuff, Effects = effects };

        return aura;
    }

    public override Aura ReadJson(JsonReader reader, Type objectType, Aura existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jo = JObject.Load(reader);

        List<AuraEffect> effects = ReadAuraEffects(jo);

        Aura aura = ReadAura(jo, effects);        

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

        if (value is GainSeal)
        {
            writer.WriteData("EffectType", "GainSeal");
        }

        else if (value is GainProc)
        {
            writer.WriteData("EffectType", "GainProc");
        }

        else if (value is ModAttackSpeed)
        {
            writer.WriteData("EffectType", "ModAttackSpeed");
        }

        else if (value is ModDamageDoneCreature modDamageDoneCreature)
        {
            writer.WriteData("EffectType", "ModDamageDoneCreature");
            writer.WriteData("SchoolMask", modDamageDoneCreature.SchoolMask);

            writer.WriteStringArray("Creatures", modDamageDoneCreature.Creatures.Cast<string>().ToArray());
        }

        else if (value is ModDamageDone modDamageDone)
        {
            writer.WriteData("EffectType", "ModDamageDone");
            writer.WriteData("SchoolMask", modDamageDone.SchoolMask);
        }

        else if (value is ModDamageTakenPercent modDamageTakenPercent)
        {
            writer.WriteData("EffectType", "ModDamageTakenPercent");
            writer.WriteData("SchoolMask", modDamageTakenPercent.SchoolMask);
        }

        else if (value is ModStatCreature modStatCreature)
        {
            writer.WriteData("EffectType", "ModStatCreature");

            writer.WriteStringArray("Stats", modStatCreature.Stats.Cast<string>().ToArray());
            writer.WriteStringArray("Creatures", modStatCreature.Creatures.Cast<string>().ToArray());
        }

        else if (value is ModStat modStat)
        {
            writer.WriteData("EffectType", "ModStat");

            writer.WriteStringArray("Stats", modStat.Stats.Cast<string>().ToArray());
        }

        else if (value is ModSpellCritChance modSpell)
        {
            writer.WriteData("EffectType", "ModSpellCritChance");
            writer.WriteIntArray("Spells", modSpell.Spells.Cast<int>().ToArray());
        }

        else if (value is ModDamageTaken modDamageTaken)
        {
            writer.WriteData("EffectType", "ModDamageTaken");
            writer.WriteData("School", modDamageTaken.School.ToString());
        }

        else if (value is ModStatPercent modStatPercent)
        {
            writer.WriteData("EffectType", "ModStatPercent");

            writer.WriteStringArray("Stats", modStatPercent.Stats.Cast<string>().ToArray());
        }

        writer.WriteData("Value", value.Value);

        writer.WriteEnd();
    }
}
