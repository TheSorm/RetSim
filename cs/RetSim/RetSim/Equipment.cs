using RetSim.Items;
using System;
using System.Collections.Generic;

namespace RetSim
{
    public record Equipment
    {
        public Stats Stats { get => CalculateStats(); }
        public List<Spell> Spells { get => GenerateSpellList(); }

        public EquippableArmor Head { get; init; }
        public EquippableArmor Neck { get; init; }
        public EquippableArmor Shoulder { get; init; }
        public EquippableArmor Cloak { get; init; }
        public EquippableArmor Chest { get; init; }
        public EquippableArmor Wrist { get; init; }
        public EquippableArmor Hand { get; init; }
        public EquippableArmor Waist { get; init; }
        public EquippableArmor Legs { get; init; }
        public EquippableArmor Feet { get; init; }
        public EquippableArmor Finger1 { get; init; }
        public EquippableArmor Finger2 { get; init; }
        public EquippableArmor Trinket1 { get; init; }
        public EquippableArmor Trinket2 { get; init; }
        public EquippableArmor Relic { get; init; }
        public EquippableWeapon Weapon { get; init; }

        private Stats CalculateStats()
        {
            return new()
            {
                Stamina = CalculateStat(ItemStatNames.Stamina),

                Intellect = CalculateStat(ItemStatNames.Intellect),
                ManaPer5 = CalculateStat(ItemStatNames.ManaPer5),

                Strength = CalculateStat(ItemStatNames.Strength),
                AttackPower = CalculateStat(ItemStatNames.AttackPower),

                Agility = CalculateStat(ItemStatNames.Agility),
                CritRating = CalculateStat(ItemStatNames.Crit),

                HitRating = CalculateStat(ItemStatNames.Hit),
                HasteRating = CalculateStat(ItemStatNames.Haste),

                ExpertiseRating = CalculateStat(ItemStatNames.Expertise),

                ArmorPenetration = CalculateStat(ItemStatNames.ArmorPenetration),

                SpellPower = CalculateStat(ItemStatNames.SpellDamage),
                SpellCritRating = CalculateStat(ItemStatNames.SpellCrit),
                SpellHitRating = CalculateStat(ItemStatNames.SpellHit),
                SpellHasteRating = CalculateStat(ItemStatNames.SpellHaste)
            };
            //TODO Calculate Set Boni
        }

        private int CalculateStat(ItemStatNames name)
        {
            return CalculateStatOfPiece(Head, name) + CalculateStatOfPiece(Neck, name) + CalculateStatOfPiece(Shoulder, name) + CalculateStatOfPiece(Cloak, name) + CalculateStatOfPiece(Chest, name)
                + CalculateStatOfPiece(Wrist, name) + CalculateStatOfPiece(Hand, name) + CalculateStatOfPiece(Waist, name) + CalculateStatOfPiece(Legs, name) + CalculateStatOfPiece(Feet, name)
                + CalculateStatOfPiece(Finger1, name) + CalculateStatOfPiece(Finger2, name) + CalculateStatOfPiece(Trinket1, name) + CalculateStatOfPiece(Trinket2, name) + CalculateStatOfPiece(Relic, name)
                + CalculateStatOfPiece(Weapon, name);
        }

        private static int CalculateStatOfPiece(EquippableItem item, ItemStatNames name) 
        {
            return item.ItemStats == null? (item.IsSocketBonusActive() ? item.SocketBonus[name] : 0) : item.ItemStats[name] + (item.IsSocketBonusActive() ? item.SocketBonus[name] : 0);
        }

        private List<Spell> GenerateSpellList()
        {
            List<Spell> spells = new();
            GenerateSpellListOf(Head, spells);
            GenerateSpellListOf(Neck, spells);
            GenerateSpellListOf(Shoulder, spells);
            GenerateSpellListOf(Cloak, spells);
            GenerateSpellListOf(Chest, spells);
            GenerateSpellListOf(Wrist, spells);
            GenerateSpellListOf(Hand, spells);
            GenerateSpellListOf(Waist, spells);
            GenerateSpellListOf(Legs, spells);
            GenerateSpellListOf(Finger1, spells);
            GenerateSpellListOf(Finger2, spells);
            GenerateSpellListOf(Trinket1, spells);
            GenerateSpellListOf(Trinket2, spells);
            GenerateSpellListOf(Relic, spells);
            GenerateSpellListOf(Weapon, spells);
            return spells;
        }
        private static void GenerateSpellListOf(EquippableItem item, List<Spell> resultSpells)
        {
            foreach (var itemSpell in item.Spells)
            {
                if(Glossaries.Spells.ByID.ContainsKey(itemSpell.ID))
                    resultSpells.Add(Glossaries.Spells.ByID[itemSpell.ID]);
            }
        }

    }
}
