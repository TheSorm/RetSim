using RetSim.Items;
using RetSim.Spells;
using RetSim.Spells.SpellEffects;
using RetSim.Units.Player.Static;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using static RetSim.Data.Items;

namespace RetSim.Data;

public static class Importer
{
    public static Equipment GetEquipment()
    {
        var data = LoadData();

        Initialize(data.Weapons, data.Armor, data.Sets, data.Gems, data.MetaGems);

        Gem strength = Gems[24027];
        Gem crit = Gems[24058];
        Gem stamina = Gems[24054];

        return new Equipment()
        {
            Head = EquippableItem.GetItemWithGems(29073, new Gem[] { MetaGems[32409], strength }),
            Neck = EquippableItem.GetItemWithGems(29381, null),
            Shoulders = EquippableItem.GetItemWithGems(29075, new Gem[] { strength, crit }),
            Back = EquippableItem.GetItemWithGems(24259, new Gem[] { strength }),
            Chest = EquippableItem.GetItemWithGems(29071, new Gem[] { strength, strength, strength }),
            Wrists = EquippableItem.GetItemWithGems(28795, new Gem[] { strength, stamina }),
            Hands = EquippableItem.GetItemWithGems(30644, null),
            Waist = EquippableItem.GetItemWithGems(28779, new Gem[] { strength, stamina }),
            Legs = EquippableItem.GetItemWithGems(31544, null),
            Feet = EquippableItem.GetItemWithGems(28608, new Gem[] { strength, crit }),
            Finger1 = EquippableItem.GetItemWithGems(30834, null),
            Finger2 = EquippableItem.GetItemWithGems(28757, null),
            Trinket1 = EquippableItem.GetItemWithGems(29383, null),
            Trinket2 = EquippableItem.GetItemWithGems(28830, null),
            Relic = EquippableItem.GetItemWithGems(27484, null),
            Weapon = Weapons[28429],
        };
    }

    public static void SerializeSpells()
    {
        var options = new JsonSerializerOptions { 
            WriteIndented = true, 
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new JsonStringEnumConverter(), new SpellEffectConverterWithTypeDiscriminator() }
        };

        string serialized = JsonSerializer.Serialize(Spells.ByID.Values, options);

        using StreamWriter writer = new("spells.json");

        writer.WriteLine(serialized);

        //using StreamReader reader = new("spells.json");
        //List<Spell> de = JsonSerializer.Deserialize<List<Spell>>(reader.ReadToEnd(), options);
        //Program.Logger.Log(de[0].ID.ToString());
    }

    public class SpellEffectConverterWithTypeDiscriminator : JsonConverter<SpellEffect>
    {
        enum TypeDiscriminator
        {
            DamageEffect = 1,
            ExtraAttacks = 2,
            JudgementEffect = 3,
            WeaponDamage = 4
        }

        public override bool CanConvert(Type typeToConvert) =>
            typeof(SpellEffect).IsAssignableFrom(typeToConvert);

        public override SpellEffect Read(
    ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader readerClone = reader;

            if (readerClone.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            readerClone.Read();
            if (readerClone.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string propertyName = readerClone.GetString();
            if (propertyName != "TypeDiscriminator")
            {
                throw new JsonException();
            }

            readerClone.Read();
            if (readerClone.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            TypeDiscriminator typeDiscriminator = (TypeDiscriminator)readerClone.GetInt32();
            SpellEffect spellEffect = typeDiscriminator switch
            {
                TypeDiscriminator.DamageEffect => JsonSerializer.Deserialize<DamageEffect>(ref reader),
                TypeDiscriminator.ExtraAttacks => JsonSerializer.Deserialize<ExtraAttacks>(ref reader),
                TypeDiscriminator.JudgementEffect => JsonSerializer.Deserialize<JudgementEffect>(ref reader),
                TypeDiscriminator.WeaponDamage => JsonSerializer.Deserialize<WeaponDamage>(ref reader),

                _ => throw new JsonException()
            };
            return spellEffect;
        }

        public override void Write(
            Utf8JsonWriter writer, SpellEffect spellEffect, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            if (spellEffect is DamageEffect damageEffect)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.DamageEffect);
                writer.WriteNumber("School", (int)damageEffect.School);
                // Write every single property of DamageEffect here.
            }
            else if (spellEffect is ExtraAttacks extraAttacks)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.ExtraAttacks);
                // Write every single property of ExtraAttacks here.
            }
            else if (spellEffect is JudgementEffect judgementEffect)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.JudgementEffect);
                // Write every single property of JudgementEffect here.
            }
            else if (spellEffect is WeaponDamage weaponDamage)
            {
                writer.WriteNumber("TypeDiscriminator", (int)TypeDiscriminator.WeaponDamage);
                // Write every single property of WeaponDamage here.
            }

            writer.WriteEndObject();
        }
    }

    public static (List<EquippableWeapon> Weapons, List<EquippableItem> Armor, List<ItemSet> Sets, List<Gem> Gems, List<MetaGem> MetaGems) LoadData()
    {
        return (LoadWeaponData(), LoadArmorData(), LoadSetData(), LoadGemData(), LoadMetaGemData());
    }

    public static List<EquippableWeapon> LoadWeaponData()
    {
        using StreamReader reader = new("..\\..\\..\\..\\RetSimWeb\\wwwroot\\data\\weapons.json");

        return JsonSerializer.Deserialize<List<EquippableWeapon>>(reader.ReadToEnd());
    }

    public static List<EquippableItem> LoadArmorData()
    {
        using StreamReader reader = new("..\\..\\..\\..\\RetSimWeb\\wwwroot\\data\\armor.json");

        return JsonSerializer.Deserialize<List<EquippableItem>>(reader.ReadToEnd());
    }
    public static List<ItemSet> LoadSetData()
    {
        using StreamReader reader = new("..\\..\\..\\..\\RetSimWeb\\wwwroot\\data\\sets.json");

        return JsonSerializer.Deserialize<List<ItemSet>>(reader.ReadToEnd());
    }

    public static List<Gem> LoadGemData()
    {
        using StreamReader reader = new("..\\..\\..\\..\\RetSimWeb\\wwwroot\\data\\gems.json");

        return JsonSerializer.Deserialize<List<Gem>>(reader.ReadToEnd());
    }
    public static List<MetaGem> LoadMetaGemData()
    {
        using StreamReader reader = new("..\\..\\..\\..\\RetSimWeb\\wwwroot\\data\\metaGems.json");

        return JsonSerializer.Deserialize<List<MetaGem>>(reader.ReadToEnd());
    }
}