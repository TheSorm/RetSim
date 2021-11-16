using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RetSim.Spells;
using RetSim.Spells.SpellEffects;

namespace RetSim.Data.JSON;

public class SpellEffectConverter : JsonConverter<SpellEffect>
{
    public override SpellEffect ReadJson(JsonReader reader, Type objectType, SpellEffect existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jo = JObject.Load(reader);

        SpellEffect effect;

        float min = (float)(jo["MinEffect"] ?? 0);
        float max = (float)(jo["MaxEffect"] ?? 0);

        string type = (string)jo["EffectType"];

        switch (type)
        {
            case "WeaponAttack":
            case "Damage":
                
                School school = Enum.Parse<School>((string)jo["School"]);
                DefenseType defense = Enum.Parse<DefenseType>((string)jo["DefenseCategory"]);
                AttackCategory crit = Enum.Parse<AttackCategory>((string)jo["CritCategory"]);

                float coefficient = (float)(jo["Coefficient"] ?? 0);
                bool ignoresDefense = (bool)(jo["IgnoresDefenses"] ?? false);

                ProcMask onCast = (ProcMask)(int)jo["OnCast"];
                ProcMask onHit = (ProcMask)(int)jo["OnCast"];
                ProcMask onCrit = (ProcMask)(int)jo["OnCast"];

                if (type == "Damage")
                    effect = new Damage(min, max, school, coefficient, defense, crit, ignoresDefense, onCast, onHit, onCrit);

                else
                {
                    bool normalized = (bool)(jo["Normalized"] ?? false);
                    float percentage = (float)(jo["Percentage"] ?? 1);

                    effect = new WeaponAttack(min, max, school, coefficient, defense, crit, ignoresDefense, onCast, onHit, onCrit, normalized, percentage);
                }

                break;

            case "ExtraAttacks":
                effect = new ExtraAttacks((int)(jo["ProcID"] ?? 1), (int)(jo["Amount"] ?? 1));
                break;

            case "JudgementEffect":
                effect = new JudgementEffect();
                break;

            case "CancelAura":
                effect = new CancelAura();
                break;

            default:
                throw new Exception($"The specified spell effect \"{type}\" does not exist.");

        }

        serializer.Populate(jo.CreateReader(), effect);

        return effect;
    }

    public override void WriteJson(JsonWriter writer, SpellEffect value, JsonSerializer serializer)
    {
        writer.WriteStartObject();

        if (value is ExtraAttacks extraAttacks)
        {
            writer.WriteData("EffectType", SpellEffectType.ExtraAttacks.ToString());
            writer.WriteData("ProcID", extraAttacks.ProcID);

            if (extraAttacks.Amount != 1)
                writer.WriteData("Amount", extraAttacks.Amount);
        }

        else if (value is JudgementEffect judgement)
        {
            writer.WriteData("EffectType", SpellEffectType.JudgementEffect.ToString());
        }

        else if (value is WeaponAttack weaponAttack)
        {
            writer.WriteData("EffectType", SpellEffectType.WeaponAttack.ToString());

            if (weaponAttack.Percentage != 1)
                writer.WriteData("Percentage", weaponAttack.Percentage);

            if (weaponAttack.Normalized)
                writer.WriteData("Normalized", weaponAttack.Normalized);

            writer.WriteData("School", weaponAttack.School.ToString());

            if (weaponAttack.Coefficient != 0)
                writer.WriteData("Coefficient", weaponAttack.Coefficient);

            writer.WriteData("DefenseCategory", weaponAttack.DefenseCategory.ToString());
            writer.WriteData("CritCategory", weaponAttack.CritCategory.ToString());

            if (weaponAttack.IgnoresDefenses)
                writer.WriteData("IgnoresDefenses", weaponAttack.IgnoresDefenses);

            writer.WriteData("OnCast", (int)weaponAttack.OnCast);
            writer.WriteData("OnHit", (int)weaponAttack.OnHit);
            writer.WriteData("OnCrit", (int)weaponAttack.OnCrit);
        }

        else if (value is Damage damageEffect)
        {

            writer.WriteData("EffectType", SpellEffectType.Damage.ToString());
            writer.WriteData("School", damageEffect.School.ToString());

            if (damageEffect.Coefficient != 0)
                writer.WriteData("Coefficient", damageEffect.Coefficient);

            writer.WriteData("DefenseCategory", damageEffect.DefenseCategory.ToString());
            writer.WriteData("CritCategory", damageEffect.CritCategory.ToString());

            if (damageEffect.IgnoresDefenses)
                writer.WriteData("IgnoresDefenses", damageEffect.IgnoresDefenses);

            writer.WriteData("OnCast", (int)damageEffect.OnCast);
            writer.WriteData("OnHit", (int)damageEffect.OnHit);
            writer.WriteData("OnCrit", (int)damageEffect.OnCrit);
        }

        else if (value is CancelAura cancelaura)
        {
            writer.WriteData("EffectType", SpellEffectType.CancelAura.ToString());
        }

        if (value.MinEffect != 0)
            writer.WriteData("MinEffect", value.MinEffect);

        if (value.MaxEffect != 0)
            writer.WriteData("MaxEffect", value.MaxEffect);

        writer.WriteEnd();
    }
}


