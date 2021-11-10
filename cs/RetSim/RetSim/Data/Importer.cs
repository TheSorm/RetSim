using Newtonsoft.Json;
using RetSim.Data.JSON;
using RetSim.Items;
using RetSim.Spells;
using RetSim.Spells.SpellEffects;
using RetSim.Units.Player.Static;
using System.IO;

using static RetSim.Data.Items;

namespace RetSim.Data;

public static class Importer
{
    public static Equipment GetEquipment()
    {
        var data = LoadData();

        Initialize(data.Weapons, data.Armor, data.Sets, data.Gems, data.MetaGems, data.Enchants);

        Gem bold = Gems[24027];
        Gem inscribed = Gems[24058];
        Gem sovereign = Gems[24054];

        return new Equipment()
        {
            Head = EquippableItem.GetItemWithGems(29073, new Gem[] { MetaGems[32409], bold }),
            Neck = EquippableItem.GetItemWithGems(29381, null),
            Shoulders = EquippableItem.GetItemWithGems(29075, new Gem[] { bold, inscribed }),
            Back = EquippableItem.GetItemWithGems(24259, new Gem[] { bold }),
            Chest = EquippableItem.GetItemWithGems(29071, new Gem[] { bold, bold, bold }),
            Wrists = EquippableItem.GetItemWithGems(28795, new Gem[] { bold, sovereign }),
            Hands = EquippableItem.GetItemWithGems(30644, null),
            Waist = EquippableItem.GetItemWithGems(28779, new Gem[] { bold, sovereign }),
            Legs = EquippableItem.GetItemWithGems(31544, null),
            Feet = EquippableItem.GetItemWithGems(28608, new Gem[] { bold, inscribed }),
            Finger1 = EquippableItem.GetItemWithGems(30834, null),
            Finger2 = EquippableItem.GetItemWithGems(28757, null),
            Trinket1 = EquippableItem.GetItemWithGems(29383, null),
            Trinket2 = EquippableItem.GetItemWithGems(28830, null),
            Relic = EquippableItem.GetItemWithGems(27484, null),
            Weapon = Weapons[28429],

            HeadEnchant = Enchants[35452],
            ShouldersEnchant = Enchants[35417],
            BackEnchant = Enchants[34004],
            ChestEnchant = Enchants[27960],
            WristsEnchant = Enchants[27899],
            HandsEnchant = Enchants[33995],
            LegsEnchant = Enchants[35490],
            FeetEnchant = Enchants[27951],
            Finger1Enchant = null,
            Finger2Enchant = null,
            WeaponEnchant = Enchants[27984]
        };
    }

    public static void SerializeSpells()
    {
        JsonSerializer serializer = new JsonSerializer();
        //serializer.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        serializer.Converters.Add(new SpellEffectConverter());
        serializer.Converters.Add(new AuraConverter());
        serializer.Converters.Add(new AuraEffectConverter());
        serializer.NullValueHandling = NullValueHandling.Ignore;
        serializer.DefaultValueHandling = DefaultValueHandling.Ignore;
        serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        serializer.MetadataPropertyHandling = MetadataPropertyHandling.Ignore; 

        using StreamWriter writer = new("spells.json");
        using JsonWriter jwriter = new JsonTextWriter(writer);
        jwriter.Formatting = Formatting.Indented;

        SortedDictionary<int, Spell> sorted = new(Spells.ByID);

        serializer.Serialize(jwriter, sorted);
    }
    
    public static void SerializeProcs()
    {
        JsonSerializer serializer = new JsonSerializer();
        //serializer.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        serializer.Converters.Add(new ProcConverter());
        serializer.NullValueHandling = NullValueHandling.Ignore;
        serializer.DefaultValueHandling = DefaultValueHandling.Ignore;
        serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        serializer.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;

        using StreamWriter writer = new("procs.json");
        using JsonWriter jwriter = new JsonTextWriter(writer);
        jwriter.Formatting = Formatting.Indented;

        SortedDictionary<int, Proc> sorted = new();

        foreach (Proc proc in Procs.ByID.Values)
        {
            sorted.Add(proc.ID, proc);
        }

        serializer.Serialize(jwriter, sorted);
    }


    public static (List<EquippableWeapon> Weapons, List<EquippableItem> Armor, List<ItemSet> Sets, List<Gem> Gems, List<MetaGem> MetaGems, List<Enchant> Enchants) LoadData()
    {
        return (LoadWeaponData(), LoadArmorData(), LoadSetData(), LoadGemData(), LoadMetaGemData(), LoadEnchantData());
    }

    public static Dictionary<int, Spell> LoadSpells()
    {
        using StreamReader reader = new("spells.json");

        return JsonConvert.DeserializeObject<Dictionary<int, Spell>>(reader.ReadToEnd(), new SpellEffectConverter(), new AuraEffectConverter() ,new AuraConverter());
    }

    public static List<EquippableWeapon> LoadWeaponData()
    {
        using StreamReader reader = new("..\\..\\..\\..\\RetSimWeb\\wwwroot\\data\\weapons.json");

        return System.Text.Json.JsonSerializer.Deserialize<List<EquippableWeapon>>(reader.ReadToEnd());
    }

    public static List<EquippableItem> LoadArmorData()
    {
        using StreamReader reader = new("..\\..\\..\\..\\RetSimWeb\\wwwroot\\data\\armor.json");

        return System.Text.Json.JsonSerializer.Deserialize<List<EquippableItem>>(reader.ReadToEnd());
    }
    public static List<ItemSet> LoadSetData()
    {
        using StreamReader reader = new("..\\..\\..\\..\\RetSimWeb\\wwwroot\\data\\sets.json");

        return System.Text.Json.JsonSerializer.Deserialize<List<ItemSet>>(reader.ReadToEnd());
    }

    public static List<Gem> LoadGemData()
    {
        using StreamReader reader = new("..\\..\\..\\..\\RetSimWeb\\wwwroot\\data\\gems.json");

        return System.Text.Json.JsonSerializer.Deserialize<List<Gem>>(reader.ReadToEnd());
    }
    public static List<MetaGem> LoadMetaGemData()
    {
        using StreamReader reader = new("..\\..\\..\\..\\RetSimWeb\\wwwroot\\data\\metaGems.json");

        return System.Text.Json.JsonSerializer.Deserialize<List<MetaGem>>(reader.ReadToEnd());
    }

    public static List<Enchant> LoadEnchantData()
    {
        using StreamReader reader = new("..\\..\\..\\..\\RetSimWeb\\wwwroot\\data\\enchants.json");

        return System.Text.Json.JsonSerializer.Deserialize<List<Enchant>>(reader.ReadToEnd());
    }
}