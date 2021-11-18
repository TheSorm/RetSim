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

        float value = (float)(jo["Value"] ?? 0);
        float dieSides = (float)(jo["DieSides"] ?? 0);

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
                    effect = new Damage(value, dieSides, school, coefficient, defense, crit, ignoresDefense, onCast, onHit, onCrit);

                else
                {
                    bool normalized = (bool)(jo["Normalized"] ?? false);

                    effect = new WeaponAttack(value, dieSides, school, coefficient, defense, crit, ignoresDefense, onCast, onHit, onCrit, normalized);
                }

                break;

            case "ExtraAttacks":
                effect = new ExtraAttacks((int)(jo["ProcID"] ?? 1), (int)(jo["Amount"] ?? 1));
                break;

            case "JudgementEffect":
                effect = new JudgementEffect();
                break;

            case "CancelAura":
                effect = new CancelAura(value);
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
            writer.WriteData("EffectType", "ExtraAttacks");
            writer.WriteData("ProcID", extraAttacks.ProcID);
        }

        else if (value is JudgementEffect)
        {
            writer.WriteData("EffectType", "JudgementEffect");
        }

        else if (value is WeaponAttack weaponAttack)
        {
            writer.WriteData("EffectType", "WeaponAttack");

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

        else if (value is Damage damage)
        {
            writer.WriteData("EffectType", "Damage");
            writer.WriteData("School", damage.School.ToString());

            if (damage.Coefficient != 0)
                writer.WriteData("Coefficient", damage.Coefficient);

            writer.WriteData("DefenseCategory", damage.DefenseCategory.ToString());
            writer.WriteData("CritCategory", damage.CritCategory.ToString());

            if (damage.IgnoresDefenses)
                writer.WriteData("IgnoresDefenses", damage.IgnoresDefenses);

            writer.WriteData("OnCast", (int)damage.OnCast);
            writer.WriteData("OnHit", (int)damage.OnHit);
            writer.WriteData("OnCrit", (int)damage.OnCrit);
        }

        else if (value is CancelAura)
        {
            writer.WriteData("EffectType", "CancelAura");
        }

        if (value.Value != 0)
            writer.WriteData("Value", value.Value);

        if (value.DieSides != 0)
            writer.WriteData("DieSides", value.DieSides);

        writer.WriteEnd();
    }
}


