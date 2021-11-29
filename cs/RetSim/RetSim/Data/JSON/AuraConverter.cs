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
                float value = (float)(jo["Effects"][i]["Value"] ?? 0);

                switch (effectType)
                {
                    case "GainSeal":
                        effects.Add(new GainSeal(value));
                        break;

                    case "GainProcFaction":

                        int aldor = (int)jo["Effects"][i]["Aldor"];
                        int scryer = (int)jo["Effects"][i]["Scryer"];

                        effects.Add(new GainProcFaction(aldor, scryer));

                        break;

                    case "GainProc":
                        effects.Add(new GainProc(value));
                        break;

                    case "ModAttackSpeed":

                        int hasteRating = (int)(jo["Effects"][i]["HasteRating"] ?? 0);

                        effects.Add(new ModAttackSpeed(value, hasteRating));
                        break;

                    case "ModCastSpeed":
                        effects.Add(new ModCastSpeed(value));
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

                    case "ModSpell":

                        List<int> msSpells = new();

                        int msCount = jo["Effects"][i]["Spells"].Count();

                        for (int y = 0; y < msCount; y++)
                        {
                            msSpells.Add((int)jo["Effects"][i]["Spells"][y]);
                        }

                        int cooldown = (int)(jo["Effects"][i]["Cooldown"] ?? 0);
                        int manaCost = (int)(jo["Effects"][i]["ManaCost"] ?? 0);
                        float critChance = (float)(jo["Effects"][i]["CritChance"] ?? 0);

                        float effect1 = (float)(jo["Effects"][i]["Effect1"] ?? 0);
                        float effect2 = (float)(jo["Effects"][i]["Effect2"] ?? 0);
                        float effect3 = (float)(jo["Effects"][i]["Effect3"] ?? 0);

                        float auraEffect1 = (float)(jo["Effects"][i]["AuraEffect1"] ?? 0);
                        float auraEffect2 = (float)(jo["Effects"][i]["AuraEffect2"] ?? 0);
                        float auraEffect3 = (float)(jo["Effects"][i]["AuraEffect3"] ?? 0);

                        effects.Add(new ModSpell(0, cooldown, manaCost, critChance, effect1, effect2, effect3, auraEffect1, auraEffect2, auraEffect3, msSpells));

                        break;

                    case "ModSpellPercent":

                        List<int> mspSpells = new();

                        int mspCount = jo["Effects"][i]["Spells"].Count();

                        for (int y = 0; y < mspCount; y++)
                        {
                            mspSpells.Add((int)jo["Effects"][i]["Spells"][y]);
                        }

                        int manaCostPercent = (int)(jo["Effects"][i]["ManaCost"] ?? 0);

                        float effect1Percent = (float)(jo["Effects"][i]["Effect1"] ?? 0);
                        float effect2Percent = (float)(jo["Effects"][i]["Effect2"] ?? 0);
                        float effect3Percent = (float)(jo["Effects"][i]["Effect3"] ?? 0);

                        float auraEffect1Percent = (float)(jo["Effects"][i]["AuraEffect1"] ?? 0);
                        float auraEffect2Percent = (float)(jo["Effects"][i]["AuraEffect2"] ?? 0);
                        float auraEffect3Percent = (float)(jo["Effects"][i]["AuraEffect3"] ?? 0);

                        effects.Add(new ModSpellPercent(0, manaCostPercent, effect1Percent, effect2Percent, effect3Percent, auraEffect1Percent, auraEffect2Percent, auraEffect3Percent, mspSpells));

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

                            if (msStats[j] == StatName.HasteRating)
                                throw new Exception("NOOO WHY IS THERE HASTE HERE");
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


                    case "CancelAuraOnRemove":

                        List<int> caorSpells = new();

                        int caorCount = jo["Effects"][i]["Spells"].Count();

                        for (int y = 0; y < caorCount; y++)
                        {
                            caorSpells.Add((int)jo["Effects"][i]["Spells"][y]);
                        }

                        effects.Add(new CancelAuraOnRemove(caorSpells));

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

        else if (value is GainProcFaction gainProcFaction)
        {
            writer.WriteData("EffectType", "GainProcFaction");
            writer.WriteData("Aldor", gainProcFaction.Aldor);
            writer.WriteData("Scryer", gainProcFaction.Scryer);
        }

        else if (value is GainProc)
        {
            writer.WriteData("EffectType", "GainProc");
        }

        else if (value is ModAttackSpeed)
        {
            writer.WriteData("EffectType", "ModAttackSpeed");
        }

        else if (value is ModCastSpeed)
        {
            writer.WriteData("EffectType", "ModCastSpeed");
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

        else if (value is ModSpell modSpell)
        {
            writer.WriteData("EffectType", "ModSpell");
            writer.WriteIntArray("Spells", modSpell.Spells.Cast<int>().ToArray());

            if (modSpell.Cooldown != 0)
                writer.WriteData("Cooldown", modSpell.Cooldown);

            if (modSpell.ManaCost != 0)
                writer.WriteData("ManaCost", modSpell.ManaCost);

            if (modSpell.CritChance != 0)
                writer.WriteData("CritChance", modSpell.CritChance);

            if (modSpell.Effect1 != 0)
                writer.WriteData("Effect1", modSpell.Effect1);

            if (modSpell.Effect2 != 0)
                writer.WriteData("Effect2", modSpell.Effect2);

            if (modSpell.Effect3 != 0)
                writer.WriteData("Effect3", modSpell.Effect3);

            if (modSpell.AuraEffect1 != 0)
                writer.WriteData("AuraEffect1", modSpell.AuraEffect1);

            if (modSpell.AuraEffect2 != 0)
                writer.WriteData("AuraEffect2", modSpell.AuraEffect2);

            if (modSpell.AuraEffect3 != 0)
                writer.WriteData("AuraEffect3", modSpell.AuraEffect3);

        }

        else if (value is ModSpellPercent modSpellPercent)
        {
            writer.WriteData("EffectType", "ModSpellPercent");
            writer.WriteIntArray("Spells", modSpellPercent.Spells.Cast<int>().ToArray());

            if (modSpellPercent.ManaCost != 0)
                writer.WriteData("ManaCost", modSpellPercent.ManaCost);

            if (modSpellPercent.Effect1 != 0)
                writer.WriteData("Effect1", modSpellPercent.Effect1);

            if (modSpellPercent.Effect2 != 0)
                writer.WriteData("Effect2", modSpellPercent.Effect2);

            if (modSpellPercent.Effect3 != 0)
                writer.WriteData("Effect3", modSpellPercent.Effect3);

            if (modSpellPercent.AuraEffect1 != 0)
                writer.WriteData("AuraEffect1", modSpellPercent.AuraEffect1);

            if (modSpellPercent.AuraEffect2 != 0)
                writer.WriteData("AuraEffect2", modSpellPercent.AuraEffect2);

            if (modSpellPercent.AuraEffect3 != 0)
                writer.WriteData("AuraEffect3", modSpellPercent.AuraEffect3);

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

        else if (value is CancelAuraOnRemove cancelAuraOnRemove)
        {
            writer.WriteData("EffectType", "CancelAuraOnRemove");
            writer.WriteIntArray("Spells", cancelAuraOnRemove.Spells.ToArray());
        }

        writer.WriteData("Value", value.Value);

        writer.WriteEnd();
    }
}
