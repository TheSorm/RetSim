using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RetSim.Spells.AuraEffects;
using RetSim.Units.UnitStats;
using System.Linq;

namespace RetSim.Data.JSON;

public class AuraEffectConverter : JsonConverter<AuraEffect>
{
    public override AuraEffect ReadJson(JsonReader reader, Type objectType, AuraEffect existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
    {
        JObject jo = JObject.Load(reader);

        AuraEffect aura = null;

        string test = (string)jo["AuraType"];

        switch ((string)jo["AuraType"])
        {

        }

        serializer.Populate(jo.CreateReader(), aura);

        return aura;
    }

    public override void WriteJson(JsonWriter writer, AuraEffect value, Newtonsoft.Json.JsonSerializer serializer)
    {
        writer.WriteStartObject();

        if (value is GainProc gainProc)
        {
            writer.WriteData("EffectType", AuraEffectType.GainProc.ToString());
            writer.WriteData("ProcID", gainProc.Proc.ID);
        }

        else if (value is GainSeal gainSeal)
        {
            writer.WriteData("EffectType", AuraEffectType.GainSeal.ToString());
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

            writer.WriteArray("Creatures", creatures, "Creature");
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
            writer.WriteArray("Spells", modDamageSpell.Spells.Cast<object>().ToArray(), "Spell");
        }

        else if (value is ModDamageTaken modDamageTaken)
        {
            writer.WriteData("EffectType", AuraEffectType.ModDamageTaken.ToString());
            writer.WriteData("Percent", modDamageTaken.Percent);
            writer.WriteData("SchoolMask", modDamageTaken.SchoolMask);
        }

        else if (value is ModifyStats modifyStats)
        {
            writer.WriteData("EffectType", AuraEffectType.ModifyStats.ToString());

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
            writer.WriteArray("Spells", modSpellCritChance.Spells.Cast<object>().ToArray(), "Spell");
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

            writer.WriteArray("Stats", stats.Cast<object>().ToArray(), "Stat");
        }

        writer.WriteEnd();
    }
}


