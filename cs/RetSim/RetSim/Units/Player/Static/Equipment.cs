using RetSim.Items;
using RetSim.Misc;
using RetSim.Spells;
using RetSim.Units.UnitStats;

namespace RetSim.Units.Player.Static;

public class Equipment
{
    public StatSet Stats => CalculateStats(this);
    public List<Spell> Spells => GenerateAuraList(this);

    public Dictionary<GemColor, int> GemTotals { get; private set; }

    public EquippableItem[] PlayerEquipment { get; init; } = new EquippableItem[Constants.EquipmentSlots.Total];

    public Enchant[] Enchants { get; set; } = new Enchant[Constants.EquipmentSlots.Total];

    #region Equipment

    public EquippableItem Head
    {
        get => PlayerEquipment[Constants.EquipmentSlots.Head];
        set => PlayerEquipment[Constants.EquipmentSlots.Head] = value;
    }

    public EquippableItem Neck
    {
        get => PlayerEquipment[Constants.EquipmentSlots.Neck];
        set => PlayerEquipment[Constants.EquipmentSlots.Neck] = value;
    }
    public EquippableItem Shoulders
    {
        get => PlayerEquipment[Constants.EquipmentSlots.Shoulders];
        set => PlayerEquipment[Constants.EquipmentSlots.Shoulders] = value;
    }

    public EquippableItem Back
    {
        get => PlayerEquipment[Constants.EquipmentSlots.Back];
        set => PlayerEquipment[Constants.EquipmentSlots.Back] = value;
    }
    public EquippableItem Chest

    {
        get => PlayerEquipment[Constants.EquipmentSlots.Chest];
        set => PlayerEquipment[Constants.EquipmentSlots.Chest] = value;
    }
    public EquippableItem Wrists
    {
        get => PlayerEquipment[Constants.EquipmentSlots.Wrists];
        set => PlayerEquipment[Constants.EquipmentSlots.Wrists] = value;
    }
    public EquippableItem Hands
    {
        get => PlayerEquipment[Constants.EquipmentSlots.Hands];
        set => PlayerEquipment[Constants.EquipmentSlots.Hands] = value;
    }

    public EquippableItem Waist
    {
        get => PlayerEquipment[Constants.EquipmentSlots.Waist];
        set => PlayerEquipment[Constants.EquipmentSlots.Waist] = value;
    }
    public EquippableItem Legs
    {
        get => PlayerEquipment[Constants.EquipmentSlots.Legs];
        set => PlayerEquipment[Constants.EquipmentSlots.Legs] = value;
    }
    public EquippableItem Feet
    {
        get => PlayerEquipment[Constants.EquipmentSlots.Feet];
        set => PlayerEquipment[Constants.EquipmentSlots.Feet] = value;
    }

    public EquippableItem Finger1
    {
        get => PlayerEquipment[Constants.EquipmentSlots.Finger1];
        set => PlayerEquipment[Constants.EquipmentSlots.Finger1] = value;
    }
    public EquippableItem Finger2
    {
        get => PlayerEquipment[Constants.EquipmentSlots.Finger2];
        set => PlayerEquipment[Constants.EquipmentSlots.Finger2] = value;
    }
    public EquippableItem Trinket1
    {
        get => PlayerEquipment[Constants.EquipmentSlots.Trinket1];
        set => PlayerEquipment[Constants.EquipmentSlots.Trinket1] = value;
    }
    public EquippableItem Trinket2
    {
        get => PlayerEquipment[Constants.EquipmentSlots.Trinket2];
        set => PlayerEquipment[Constants.EquipmentSlots.Trinket2] = value;
    }
    public EquippableItem Relic
    {
        get => PlayerEquipment[Constants.EquipmentSlots.Relic];
        set => PlayerEquipment[Constants.EquipmentSlots.Relic] = value;
    }

    public EquippableWeapon Weapon
    {
        get => (EquippableWeapon)PlayerEquipment[Constants.EquipmentSlots.Weapon];
        set => PlayerEquipment[Constants.EquipmentSlots.Weapon] = value;
    }

    #endregion

    #region Enchants

    public Enchant HeadEnchant
    {
        get => Enchants[Constants.EquipmentSlots.Head];
        set => Enchants[Constants.EquipmentSlots.Head] = value;
    }

    public Enchant ShouldersEnchant
    {
        get => Enchants[Constants.EquipmentSlots.Shoulders];
        set => Enchants[Constants.EquipmentSlots.Shoulders] = value;
    }

    public Enchant BackEnchant
    {
        get => Enchants[Constants.EquipmentSlots.Back];
        set => Enchants[Constants.EquipmentSlots.Back] = value;
    }
    public Enchant ChestEnchant

    {
        get => Enchants[Constants.EquipmentSlots.Chest];
        set => Enchants[Constants.EquipmentSlots.Chest] = value;
    }
    public Enchant WristsEnchant
    {
        get => Enchants[Constants.EquipmentSlots.Wrists];
        set => Enchants[Constants.EquipmentSlots.Wrists] = value;
    }
    public Enchant HandsEnchant
    {
        get => Enchants[Constants.EquipmentSlots.Hands];
        set => Enchants[Constants.EquipmentSlots.Hands] = value;
    }
    public Enchant LegsEnchant
    {
        get => Enchants[Constants.EquipmentSlots.Legs];
        set => Enchants[Constants.EquipmentSlots.Legs] = value;
    }
    public Enchant FeetEnchant
    {
        get => Enchants[Constants.EquipmentSlots.Feet];
        set => Enchants[Constants.EquipmentSlots.Feet] = value;
    }

    public Enchant Finger1Enchant
    {
        get => Enchants[Constants.EquipmentSlots.Finger1];
        set => Enchants[Constants.EquipmentSlots.Finger1] = value;
    }
    public Enchant Finger2Enchant
    {
        get => Enchants[Constants.EquipmentSlots.Finger2];
        set => Enchants[Constants.EquipmentSlots.Finger2] = value;
    }

    public Enchant WeaponEnchant
    {
        get => Enchants[Constants.EquipmentSlots.Weapon];
        set => Enchants[Constants.EquipmentSlots.Weapon] = value;
    }

#endregion

    public static StatSet CalculateStats(Equipment equipment)
    {
        if (equipment.GemTotals == null)
            equipment.GemTotals = GetGemCount(equipment);

        StatSet result = new();

        var stats = new List<StatName> { StatName.Stamina, StatName.Intellect, StatName.ManaPer5, StatName.AttackPower, StatName.Strength, StatName.CritRating, StatName.Agility,
                StatName.HitRating, StatName.HasteRating, StatName.ExpertiseRating, StatName.ArmorPenetration, StatName.SpellPower, StatName.SpellCrit, StatName.SpellHit, StatName.SpellHaste };

        foreach (var stat in stats)
        {
            result.Add(stat, CalculateStat(stat, equipment, equipment.GemTotals));
        }

        return result;
    }

    private static float CalculateStat(StatName name, Equipment equipment, Dictionary<GemColor, int> gems)
    {
        float total = 0;

        foreach (EquippableItem item in equipment.PlayerEquipment)
        {
            if (item != null)
                total += CalculateStatOfPiece(name, item, gems);
        }

        foreach (Enchant enchant in equipment.Enchants)
        {
            if (enchant != null)
                total += enchant.Stats[name];
        }

        return total;
    }

    private static float CalculateStatOfPiece(StatName name, EquippableItem item, Dictionary<GemColor, int> gems)
    {
        float passive = item.Stats[name];
        float bonus = item.IsSocketBonusActive() ? item.SocketBonus[name] : 0;
        float sockets = 0;

        foreach (Socket socket in item.Sockets)
        {
            if (socket != null && socket.SocketedGem != null)
            {
                if (socket.IsMetaGem() is MetaGem meta && !meta.IsActive(gems[GemColor.Red], gems[GemColor.Blue], gems[GemColor.Yellow]))
                    continue;

                sockets += socket.SocketedGem.Stats[name];
            }
        }

        return passive + bonus + sockets;
    }

    private static List<Spell> GenerateAuraList(Equipment equipment)
    {
        var spells = new List<Spell>();
        var sets = new Dictionary<int, int>();

        foreach (EquippableItem item in equipment.PlayerEquipment)
        {
            if (item != null)
            {
                AddItemAuras(item, equipment.GemTotals, spells);

                if (item.Set != null)
                {
                    if (sets.ContainsKey(item.Set.ID))
                        sets[item.Set.ID]++;
                    else
                        sets.Add(item.Set.ID, 1);
                }
            }
        }

        foreach (var set in sets)
        {
            foreach (var spell in Data.Items.Sets[set.Key].SetSpells)
            {
                if (set.Value >= spell.RequiredCount)
                {
                    if (Data.Spells.ByID.ContainsKey(spell.ID))
                        spells.Add(Data.Spells.ByID[spell.ID]);
                }
            }
        }

        if (equipment.WeaponEnchant != null && equipment.WeaponEnchant.OnEquip != null)
            spells.Add(Data.Spells.ByID[equipment.WeaponEnchant.OnEquip.ID]);

        return spells;
    }
    private static void AddItemAuras(EquippableItem item, Dictionary<GemColor, int> gems, List<Spell> spells)
    {
        if (item.OnEquip != null && Data.Spells.ByID.ContainsKey(item.OnEquip.ID))
            spells.Add(Data.Spells.ByID[item.OnEquip.ID]);

        if (item.Socket1 != null && item.Socket1.IsMetaGem() is MetaGem meta && meta.IsActive(gems[GemColor.Red], gems[GemColor.Blue], gems[GemColor.Yellow]))
        {
            if (Data.Spells.ByID.ContainsKey(meta.Spell.ID))
                spells.Add(Data.Spells.ByID[meta.Spell.ID]);
        }
    }

    private static Dictionary<GemColor, int> GetGemCount(Equipment equipment)
    {
        Dictionary<GemColor, int> totals = new()
        {
            { GemColor.Red, 0 },
            { GemColor.Blue, 0 },
            { GemColor.Yellow, 0 }
        };

        var gems = new List<Gem>();

        foreach (EquippableItem item in equipment.PlayerEquipment)
        {
            if (item != null)
                gems.AddRange(item.GetGems());
        }

        foreach (var gem in gems)
        {
            foreach (GemColor color in totals.Keys)
            {
                if (Convert.ToBoolean(gem.Color & color))
                    totals[color]++;
            }
        }

        return totals;
    }
}