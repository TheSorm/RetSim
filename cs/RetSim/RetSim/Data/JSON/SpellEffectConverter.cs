using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RetSim.Spells.SpellEffects;

namespace RetSim.Data.JSON;

public class SpellEffectConverter : JsonConverter<SpellEffect>
{

    public override SpellEffect ReadJson(JsonReader reader, Type objectType, SpellEffect existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jo = JObject.Load(reader);

        SpellEffect effect;

        switch ((string)jo["EffectType"])
        {
            case "WeaponAttack":
                effect = new WeaponAttack();
                break;

            case "Damage":
                effect = new Damage();
                break;

            case "ExtraAttacks":
                effect = new ExtraAttacks();
                break;

            case "Judgement":
                effect = new Judgement();
                break;

            default:
                throw new NotImplementedException();

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

        else if (value is Judgement judgement)
        {
            writer.WriteData("EffectType", SpellEffectType.Judgement.ToString());
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

        if (value.MinEffect != 0)
            writer.WriteData("MinEffect", value.MinEffect);

        if (value.MaxEffect != 0)
            writer.WriteData("MaxEffect", value.MaxEffect);

        writer.WriteEnd();
    }
}


