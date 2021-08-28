using System.Collections.Generic;

namespace RetSim.Items
{
    public class EquippableItem
    {
        private readonly Stats baseStats;
        public Stats Stats { get => CalculateStats(); }
        public int ID { get; init; }
        public string Name { get; set; }
        public string InventoryType { get; init; }
        public Set Set { get; init; }
        public List<Spell> Spells { get; init; }
        public Socket Socket1 { get; init; }
        public Socket Socket2 { get; init; }
        public Socket Socket3 { get; init; }
        public SocketBonus Bonus { get; init; }

        public EquippableItem(WowItemData.Item data)
        {
            if (data.Stats != null)
            {
                baseStats = new()
                {
                    Stamina = data.Stats.Stamina,
                    Intellect = data.Stats.Intellect,
                    ManaPer5 = data.Stats.ManaPer5,

                    Strength = data.Stats.Strength,
                    AttackPower = data.Stats.AttackPower,

                    Agility = data.Stats.Agility,
                    CritRating = data.Stats.Crit,

                    HitRating = data.Stats.Hit,
                    HasteRating = data.Stats.Haste,

                    ExpertiseRating = data.Stats.Expertise,

                    ArmorPenetration = data.Stats.ArmorPenetration,

                    SpellPower = data.Stats.SpellDamage,
                    SpellCritRating = data.Stats.SpellCrit,
                    SpellHitRating = data.Stats.SpellHit,
                    SpellHasteRating = data.Stats.SpellHaste,
                };
            }
            else
            {
                baseStats = new();
            }

            ID = data.ID;
            Name = data.Name;
            InventoryType = data.InventoryType;

            if (data.Set != null)
            {
                Set = new() { ID = data.Set.ID, Name = data.Set.Name };
            }

            if (data.Spells != null)
            {
                Spells = new();
                foreach (WowItemData.Spell itemSpell in data.Spells)
                {
                    if (Glossaries.Spells.ByID.ContainsKey(itemSpell.ID))
                    {
                        Spells.Add(Glossaries.Spells.ByID[itemSpell.ID]);
                    }
                }
            }

            if (data.Socket1 != null)
            {
                Socket1 = new Socket(data.Socket1);
            }

            if (data.Socket2 != null)
            {
                Socket2 = new Socket(data.Socket2);
            }

            if (data.Socket3 != null)
            {
                Socket3 = new Socket(data.Socket3);
            }

            if (data.SocketBonus != null)
            {
                Bonus = new() { Stat = data.SocketBonus.Stat, Value = data.SocketBonus.Value };
            }
        }

        private Stats CalculateStats()
        {
            if (Bonus != null)
            {
                if ((Socket1 == null || Socket1.IsActive()) && (Socket2 == null || Socket2.IsActive()) && (Socket3 == null || Socket3.IsActive()))
                {
                    switch (Bonus.Stat)
                    {
                        case "Strength":
                            return baseStats + new Stats() { Strength = Bonus.Value };
                        case "Agility":
                            return baseStats + new Stats() { Agility = Bonus.Value };
                        case "Stamina":
                            return baseStats + new Stats() { Stamina = Bonus.Value };
                        case "Intellect":
                            return baseStats + new Stats() { Intellect = Bonus.Value };
                        case "Critical Strike Rating":
                            return baseStats + new Stats() { CritRating = Bonus.Value };
                        case "Mana per 5 sec":
                            return baseStats + new Stats() { ManaPer5 = Bonus.Value };
                        case "Hit Rating":
                            return baseStats + new Stats() { HitRating = Bonus.Value };
                        case "Haste Rating":
                            return baseStats + new Stats() { HasteRating = Bonus.Value };
                        case "Expertise Rating":
                            return baseStats + new Stats() { ExpertiseRating = Bonus.Value };
                        case "Spell Critical Strike Rating":
                            return baseStats + new Stats() { SpellCritRating = Bonus.Value };
                        case "Spell Hit Rating":
                            return baseStats + new Stats() { SpellHit = Bonus.Value };
                        //case "Spirit":
                        //    return baseStats + new Stats() { Spirit = Bonus.Value };
                        default:
                            break;
                    }
                }
            }

            return baseStats;
        }
    }

    public record SocketBonus
    {
        public string Stat { get; init; }
        public int Value { get; init; }
    }

    public record Set
    {
        public int ID { get; init; }
        public string Name { get; init; }
    }
}
