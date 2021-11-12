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

                switch (effectType)
                {
                    case "GainSeal":
                        effects.Add(new GainSeal((int)jo["Effects"][i]["ProcID"]));
                        break;

                    case "GainProc":
                        effects.Add(new GainProc((int)jo["Effects"][i]["ProcID"]));
                        break;

                    case "ModAttackSpeed":
                        effects.Add(new ModAttackSpeed((int)jo["Effects"][i]["Percent"]));
                        break;

                    case "ModDamageCreature":

                        List<CreatureType> creatures = new();

                        int count = jo["Effects"][i]["Creatures"].Count();

                        for (int y = 0; y < count; y++)
                        {
                            creatures.Add(Enum.Parse<CreatureType>((string)jo["Effects"][i]["Creatures"][y]));
                        }

                        effects.Add(new ModDamageCreature((int)jo["Effects"][i]["Percent"], (School)(int)jo["Effects"][i]["SchoolMask"], creatures));

                        break;

                    case "ModDamageSchool":
                        effects.Add(new ModDamageSchool((int)jo["Effects"][i]["Percent"], (School)(int)jo["Effects"][i]["SchoolMask"]));
                        break;

                    case "ModDamageSpell":

                        List<int> spells = new();

                        int spellCount = jo["Effects"][i]["Spells"].Count();

                        for (int y = 0; y < spellCount; y++)
                        {
                            spells.Add((int)jo["Effects"][i]["Spells"][y]);
                        }

                        effects.Add(new ModDamageSpell((int)jo["Effects"][i]["Percent"], spells));

                        break;

                    case "ModDamageTaken":
                        effects.Add(new ModDamageTaken((int)jo["Effects"][i]["Percent"], (School)(int)jo["Effects"][i]["SchoolMask"]));
                        break;

                    case "GainStats":

                        StatSet stats = new();

                        int statCount = jo["Effects"][i]["Stats"].Count();

                        for (int y = 0; y < statCount; y++)
                        {
                            stats[Enum.Parse<StatName>((string)jo["Effects"][i]["Stats"][y]["Stat"])] = (float)jo["Effects"][i]["Stats"][y]["Value"];
                        }

                        effects.Add(new GainStats(stats));

                        break;

                    case "ModSpellCritChance":

                        List<int> spellCritChance = new();

                        int spellCritCount = jo["Effects"][i]["Spells"].Count();

                        for (int y = 0; y < spellCritCount; y++)
                        {
                            spellCritChance.Add((int)jo["Effects"][i]["Spells"][y]);
                        }

                        effects.Add(new ModSpellCritChance((int)jo["Effects"][i]["Amount"], spellCritChance));

                        break;

                    case "ModSpellDamageTaken":
                        effects.Add(new ModSpellDamageTaken((int)jo["Effects"][i]["Amount"], Enum.Parse<School>((string)jo["Effects"][i]["School"])));
                        break;

                    case "ModStat":

                        List<StatName> statList = new();

                        int statListCount = jo["Effects"][i]["Stats"].Count();

                        for (int y = 0; y < statListCount; y++)
                        {
                            statList.Add(Enum.Parse<StatName>((string)jo["Effects"][i]["Stats"][y]));
                        }

                        effects.Add(new ModStat((int)jo["Effects"][i]["Percent"], statList));

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

            string[] creatures = new string[modDamageCreature.Creatures.Count];

            for (int i = 0; i < modDamageCreature.Creatures.Count; i++)
            {
                creatures[i] = modDamageCreature.Creatures[i].ToString();
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
