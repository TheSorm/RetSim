using Newtonsoft.Json;
using RetSim.Data.JSON;
using RetSim.Items;
using RetSim.Spells;
using RetSim.Spells.SpellEffects;
using RetSim.Units.Enemy;
using RetSim.Units.Player;
using RetSim.Units.Player.Static;
using System.IO;
using static RetSim.Data.Items;

namespace RetSim.Data;

public static class Manager
{
    public static void InstantiateData()
    {
        Dictionary<int, Proc> procs = LoadProcs();

        foreach (KeyValuePair<int, Proc> proc in procs)
        {
            Collections.Procs[proc.Value.ID] = proc.Value;
        }

        Dictionary<int, Spell> paladin = LoadSpells("paladin");
        Dictionary<int, Spell> buffs = LoadSpells("buffs");
        Dictionary<int, Spell> debuffs = LoadSpells("debuffs");
        Dictionary<int, Spell> consumables = LoadSpells("consumables");
        Dictionary<int, Spell> misc = LoadSpells("misc");

        Dictionary<int, Spell> seals = LoadSeals();
        Dictionary<int, Judgement> judgements = LoadJudgements();
        Dictionary<int, Talent> talents = LoadTalents();

        foreach (KeyValuePair<int, Talent> talent in talents)
        {
            Collections.Talents.Add(talent.Key, talent.Value);
            Collections.Spells.Add(talent.Key, talent.Value);
        }

        foreach (KeyValuePair<int, Judgement> judgement in judgements)
        {
            Collections.Judgements.Add(judgement.Key, judgement.Value);
            Collections.Spells.Add(judgement.Key, judgement.Value);
        }

        foreach (KeyValuePair<int, Spell> seal in seals)
        {
            Collections.Seals.Add(seal.Key, seal.Value);
            Collections.Spells.Add(seal.Key, seal.Value);
        }

        foreach (KeyValuePair<int, Spell> spell in paladin)
        {
            Collections.Spells.Add(spell.Key, spell.Value);
        }

        foreach (KeyValuePair<int, Spell> buff in buffs)
        {
            Collections.Spells.Add(buff.Key, buff.Value);
        }

        foreach (KeyValuePair<int, Spell> debuff in debuffs)
        {
            Collections.Spells.Add(debuff.Key, debuff.Value);
        }

        foreach (KeyValuePair<int, Spell> consumable in consumables)
        {
            Collections.Spells.Add(consumable.Key, consumable.Value);
        }

        foreach (KeyValuePair<int, Spell> spell in misc)
        {
            Collections.Spells.Add(spell.Key, spell.Value);
        }

        foreach (KeyValuePair<int, Spell> spell in Collections.Spells)
        {
            if (spell.Value.Aura != null)
            {
                Aura.Instantiate(spell.Value.Aura, spell.Value);

                Collections.Auras[spell.Key] = spell.Value.Aura;
            }

            if (spell.Value.Effects != null)
            {
                foreach (SpellEffect effect in spell.Value.Effects)
                {
                    effect.Parent = spell.Value;
                }
            }
        }

        foreach (KeyValuePair<int, Spell> seal in seals)
        {
            Seal.Instantiate((Seal)seal.Value.Aura, seal.Value);
        }

        foreach (KeyValuePair<int, Proc> proc in procs)
        {
            proc.Value.Spell = Collections.Spells[proc.Value.SpellID];
        }

        Dictionary<string, Race> races = LoadRaces();

        foreach (KeyValuePair<string, Race> race in races)
        {
            Collections.Races.Add(race.Key, race.Value);
        }

        Race human = Collections.Races["Human"];

        human.Racial = Collections.Spells[human.RacialID];
        human.Racial.Requirements = (Player player) => (player.Weapon.Type == WeaponType.Sword || player.Weapon.Type == WeaponType.Mace) && player.Race.Name == Races.Human.ToString();

        Boss[] bosses = LoadBosses();

        foreach (Boss boss in bosses)
            Collections.Bosses[boss.ID] = boss;
    }

    public static Equipment GetEquipment()
    {
        var data = LoadData();

        Initialize(data.Weapons, data.Armor, data.Sets, data.Gems, data.MetaGems, data.Enchants);

        Gem bold = Gems[24027];
        Gem inscribed = Gems[24058];
        Gem sovereign = Gems[24054];

        return new Equipment()
        {
            Head = EquippableItem.GetItemWithGems(32461, new Gem[] { MetaGems[32409], sovereign }),
            Neck = EquippableItem.GetItemWithGems(30022, null),
            Shoulders = EquippableItem.GetItemWithGems(29075, new Gem[] { bold, inscribed }),
            Back = EquippableItem.GetItemWithGems(30098, null),
            Chest = EquippableItem.GetItemWithGems(30129, new Gem[] { bold, inscribed, inscribed }),
            Wrists = EquippableItem.GetItemWithGems(28795, new Gem[] { bold, sovereign }),
            Hands = EquippableItem.GetItemWithGems(29947, null),
            Waist = EquippableItem.GetItemWithGems(30032, new Gem[] { sovereign, inscribed }),
            Legs = EquippableItem.GetItemWithGems(30257, null),
            Feet = EquippableItem.GetItemWithGems(29951, new Gem[] { bold, bold }),
            Finger1 = EquippableItem.GetItemWithGems(30834, null),
            Finger2 = EquippableItem.GetItemWithGems(28730, null),
            Trinket1 = EquippableItem.GetItemWithGems(29383, null),
            Trinket2 = EquippableItem.GetItemWithGems(28830, null),
            Relic = EquippableItem.GetItemWithGems(27484, null),
            Weapon = (EquippableWeapon)EquippableItem.GetItemWithGems(29993, new Gem[] { bold, sovereign, inscribed }),

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
        //serializer.Converters.Add(new AuraEffectConverter());
        serializer.NullValueHandling = NullValueHandling.Ignore;
        serializer.DefaultValueHandling = DefaultValueHandling.Ignore;
        serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        serializer.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;

        using StreamWriter writer = new("spells.json");
        using JsonWriter jwriter = new JsonTextWriter(writer);
        jwriter.Formatting = Formatting.Indented;

        SortedDictionary<int, Spell> sorted = new(Collections.Spells);

        serializer.Serialize(jwriter, sorted);
    }

    public static void SerializeRaces()
    {
        JsonSerializer serializer = new JsonSerializer();
        //serializer.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        serializer.Converters.Add(new SpellEffectConverter());
        serializer.Converters.Add(new AuraConverter());
        //serializer.Converters.Add(new AuraEffectConverter());
        serializer.NullValueHandling = NullValueHandling.Ignore;
        serializer.DefaultValueHandling = DefaultValueHandling.Ignore;
        serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        serializer.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;

        using StreamWriter writer = new("races.json");
        using JsonWriter jwriter = new JsonTextWriter(writer);
        jwriter.Formatting = Formatting.Indented;

        SortedDictionary<int, Spell> sorted = new(Collections.Spells);

        serializer.Serialize(jwriter, Collections.Races);
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

        foreach (Proc proc in Data.Collections.Procs.Values)
        {
            sorted.Add(proc.ID, proc);
        }

        serializer.Serialize(jwriter, sorted);
    }


    public static (List<EquippableWeapon> Weapons, List<EquippableItem> Armor, List<ItemSet> Sets, List<Gem> Gems, List<MetaGem> MetaGems, List<Enchant> Enchants) LoadData()
    {
        return (LoadWeaponData(), LoadArmorData(), LoadSetData(), LoadGemData(), LoadMetaGemData(), LoadEnchantData());
    }

    public static Dictionary<int, Proc> LoadProcs()
    {
        using StreamReader reader = new("Properties\\Data\\Spells\\procs.json");

        return JsonConvert.DeserializeObject<Dictionary<int, Proc>>(reader.ReadToEnd(), new ProcConverter());
    }

    public static Dictionary<int, Spell> LoadSpells(string file)
    {
        using StreamReader reader = new($"Properties\\Data\\Spells\\{file}.json");

        return JsonConvert.DeserializeObject<Dictionary<int, Spell>>(reader.ReadToEnd(), new SpellEffectConverter(), new AuraConverter());
    }

    public static Dictionary<int, Spell> LoadSeals()
    {
        using StreamReader reader = new("Properties\\Data\\Spells\\seals.json");

        return JsonConvert.DeserializeObject<Dictionary<int, Spell>>(reader.ReadToEnd(), new SpellEffectConverter(), new AuraConverter());
    }

    public static Dictionary<int, RetSim.Spells.Judgement> LoadJudgements()
    {
        using StreamReader reader = new("Properties\\Data\\Spells\\judgements.json");

        return JsonConvert.DeserializeObject<Dictionary<int, Judgement>>(reader.ReadToEnd(), new SpellEffectConverter(), new AuraConverter());
    }

    public static Dictionary<int, Talent> LoadTalents()
    {
        using StreamReader reader = new("Properties\\Data\\Spells\\talents.json");

        return JsonConvert.DeserializeObject<Dictionary<int, Talent>>(reader.ReadToEnd(), new SpellEffectConverter(), new AuraConverter());
    }

    public static Dictionary<string, Race> LoadRaces()
    {
        using StreamReader reader = new("Properties\\Data\\races.json");

        return JsonConvert.DeserializeObject<Dictionary<string, Race>>(reader.ReadToEnd());
    }

    public static Boss[] LoadBosses()
    {
        using StreamReader reader = new("Properties\\Data\\bosses.json");

        return JsonConvert.DeserializeObject<Boss[]>(reader.ReadToEnd());
    }

    public static List<EquippableWeapon> LoadWeaponData()
    {
        using StreamReader reader = new("Properties\\Data\\Equipment\\weapons.json");

        return System.Text.Json.JsonSerializer.Deserialize<List<EquippableWeapon>>(reader.ReadToEnd());
    }

    public static List<EquippableItem> LoadArmorData()
    {
        using StreamReader reader = new("Properties\\Data\\Equipment\\armor.json");

        return System.Text.Json.JsonSerializer.Deserialize<List<EquippableItem>>(reader.ReadToEnd());
    }
    public static List<ItemSet> LoadSetData()
    {
        using StreamReader reader = new("Properties\\Data\\Equipment\\sets.json");

        return System.Text.Json.JsonSerializer.Deserialize<List<ItemSet>>(reader.ReadToEnd());
    }

    public static List<Gem> LoadGemData()
    {
        using StreamReader reader = new("Properties\\Data\\Equipment\\gems.json");

        return System.Text.Json.JsonSerializer.Deserialize<List<Gem>>(reader.ReadToEnd());
    }
    public static List<MetaGem> LoadMetaGemData()
    {
        using StreamReader reader = new("Properties\\Data\\Equipment\\metaGems.json");

        return System.Text.Json.JsonSerializer.Deserialize<List<MetaGem>>(reader.ReadToEnd());
    }

    public static List<Enchant> LoadEnchantData()
    {
        using StreamReader reader = new("Properties\\Data\\Equipment\\enchants.json");

        return System.Text.Json.JsonSerializer.Deserialize<List<Enchant>>(reader.ReadToEnd());
    }
}