using RetSim.Items;
using System.Collections.Generic;

namespace RetSim
{
    public record Equipment
    {
        public Stats Stats { get => CalculateStats(); }
        public List<Aura> Auras { get => GenerateAuraList(); }
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
            var gemCount = GetGemCount();
            return new()
            {
                Stamina = CalculateStat(ItemStatNames.Stamina, gemCount),

                Intellect = CalculateStat(ItemStatNames.Intellect, gemCount),
                ManaPer5 = CalculateStat(ItemStatNames.ManaPer5, gemCount),

                Strength = CalculateStat(ItemStatNames.Strength, gemCount),
                AttackPower = CalculateStat(ItemStatNames.AttackPower, gemCount),

                Agility = CalculateStat(ItemStatNames.Agility, gemCount),
                CritRating = CalculateStat(ItemStatNames.Crit, gemCount),

                HitRating = CalculateStat(ItemStatNames.Hit, gemCount),
                HasteRating = CalculateStat(ItemStatNames.Haste, gemCount),

                ExpertiseRating = CalculateStat(ItemStatNames.Expertise, gemCount),

                ArmorPenetration = CalculateStat(ItemStatNames.ArmorPenetration, gemCount),

                SpellPower = CalculateStat(ItemStatNames.SpellDamage, gemCount),
                SpellCritRating = CalculateStat(ItemStatNames.SpellCrit, gemCount),
                SpellHitRating = CalculateStat(ItemStatNames.SpellHit, gemCount),
                SpellHasteRating = CalculateStat(ItemStatNames.SpellHaste, gemCount)
            };
        }

        private int CalculateStat(ItemStatNames name, Dictionary<GemColor, int> gemCount)
        {
            return CalculateStatOfPiece(Head, name, gemCount) + CalculateStatOfPiece(Neck, name, gemCount) + CalculateStatOfPiece(Shoulder, name, gemCount) + CalculateStatOfPiece(Cloak, name, gemCount) + CalculateStatOfPiece(Chest, name, gemCount)
                + CalculateStatOfPiece(Wrist, name, gemCount) + CalculateStatOfPiece(Hand, name, gemCount) + CalculateStatOfPiece(Waist, name, gemCount) + CalculateStatOfPiece(Legs, name, gemCount) + CalculateStatOfPiece(Feet, name, gemCount)
                + CalculateStatOfPiece(Finger1, name, gemCount) + CalculateStatOfPiece(Finger2, name, gemCount) + CalculateStatOfPiece(Trinket1, name, gemCount) + CalculateStatOfPiece(Trinket2, name, gemCount) + CalculateStatOfPiece(Relic, name, gemCount)
                + CalculateStatOfPiece(Weapon, name, gemCount);
        }

        private static int CalculateStatOfPiece(EquippableItem item, ItemStatNames name, Dictionary<GemColor, int> gemCount)
        {
            return item.Stats[name] + (item.IsSocketBonusActive() ? item.SocketBonus[name] : 0) +
                (item.Socket1 != null && item.Socket1.SocketedGem != null && item.Socket1.IsGemActive(gemCount[GemColor.Red], gemCount[GemColor.Blue], gemCount[GemColor.Yellow]) ? item.Socket1.SocketedGem.Stats[name] : 0) +
                (item.Socket2 != null && item.Socket2.SocketedGem != null && item.Socket2.IsGemActive(gemCount[GemColor.Red], gemCount[GemColor.Blue], gemCount[GemColor.Yellow]) ? item.Socket2.SocketedGem.Stats[name] : 0) +
                (item.Socket3 != null && item.Socket3.SocketedGem != null && item.Socket3.IsGemActive(gemCount[GemColor.Red], gemCount[GemColor.Blue], gemCount[GemColor.Yellow]) ? item.Socket3.SocketedGem.Stats[name] : 0);
        }

        private List<Aura> GenerateAuraList()
        {
            var gemCount = GetGemCount();

            List<Aura> auras = new();
            GenerateAuraListOf(Head, auras, gemCount);
            GenerateAuraListOf(Neck, auras, gemCount);
            GenerateAuraListOf(Shoulder, auras, gemCount);
            GenerateAuraListOf(Cloak, auras, gemCount);
            GenerateAuraListOf(Chest, auras, gemCount);
            GenerateAuraListOf(Wrist, auras, gemCount);
            GenerateAuraListOf(Hand, auras, gemCount);
            GenerateAuraListOf(Waist, auras, gemCount);
            GenerateAuraListOf(Legs, auras, gemCount);
            GenerateAuraListOf(Finger1, auras, gemCount);
            GenerateAuraListOf(Finger2, auras, gemCount);
            GenerateAuraListOf(Trinket1, auras, gemCount);
            GenerateAuraListOf(Trinket2, auras, gemCount);
            GenerateAuraListOf(Relic, auras, gemCount);
            GenerateAuraListOf(Weapon, auras, gemCount);

            Dictionary<int, int> SetCounts = new();
            if (Head.Set != null) if (SetCounts.ContainsKey(Head.Set.ID)) SetCounts[Head.Set.ID]++; else SetCounts.Add(Head.Set.ID, 1);
            if (Neck.Set != null) if (SetCounts.ContainsKey(Neck.Set.ID)) SetCounts[Neck.Set.ID]++; else SetCounts.Add(Neck.Set.ID, 1);
            if (Shoulder.Set != null) if (SetCounts.ContainsKey(Shoulder.Set.ID)) SetCounts[Shoulder.Set.ID]++; else SetCounts.Add(Shoulder.Set.ID, 1);
            if (Cloak.Set != null) if (SetCounts.ContainsKey(Cloak.Set.ID)) SetCounts[Cloak.Set.ID]++; else SetCounts.Add(Cloak.Set.ID, 1);
            if (Chest.Set != null) if (SetCounts.ContainsKey(Chest.Set.ID)) SetCounts[Chest.Set.ID]++; else SetCounts.Add(Chest.Set.ID, 1);
            if (Wrist.Set != null) if (SetCounts.ContainsKey(Wrist.Set.ID)) SetCounts[Wrist.Set.ID]++; else SetCounts.Add(Wrist.Set.ID, 1);
            if (Hand.Set != null) if (SetCounts.ContainsKey(Hand.Set.ID)) SetCounts[Hand.Set.ID]++; else SetCounts.Add(Hand.Set.ID, 1);
            if (Waist.Set != null) if (SetCounts.ContainsKey(Waist.Set.ID)) SetCounts[Waist.Set.ID]++; else SetCounts.Add(Waist.Set.ID, 1);
            if (Legs.Set != null) if (SetCounts.ContainsKey(Legs.Set.ID)) SetCounts[Legs.Set.ID]++; else SetCounts.Add(Legs.Set.ID, 1);
            if (Finger1.Set != null) if (SetCounts.ContainsKey(Finger1.Set.ID)) SetCounts[Finger1.Set.ID]++; else SetCounts.Add(Finger1.Set.ID, 1);
            if (Finger2.Set != null) if (SetCounts.ContainsKey(Finger2.Set.ID)) SetCounts[Finger2.Set.ID]++; else SetCounts.Add(Finger2.Set.ID, 1);
            if (Trinket1.Set != null) if (SetCounts.ContainsKey(Trinket1.Set.ID)) SetCounts[Trinket1.Set.ID]++; else SetCounts.Add(Trinket1.Set.ID, 1);
            if (Trinket2.Set != null) if (SetCounts.ContainsKey(Trinket2.Set.ID)) SetCounts[Trinket2.Set.ID]++; else SetCounts.Add(Trinket2.Set.ID, 1);
            if (Relic.Set != null) if (SetCounts.ContainsKey(Relic.Set.ID)) SetCounts[Relic.Set.ID]++; else SetCounts.Add(Relic.Set.ID, 1);
            if (Weapon.Set != null) if (SetCounts.ContainsKey(Weapon.Set.ID)) SetCounts[Weapon.Set.ID]++; else SetCounts.Add(Weapon.Set.ID, 1);

            foreach (var setCount in SetCounts)
            {
                foreach (var setEffect in Glossaries.Items.SetsByID[setCount.Key].SetEffects)
                {
                    if (setCount.Value >= setEffect.RequiredCount)
                    {
                        if (Glossaries.Auras.ByID.ContainsKey(setEffect.ID))
                            auras.Add(Glossaries.Auras.ByID[setEffect.ID]);
                    }

                }
            }

            return auras;
        }
        private static void GenerateAuraListOf(EquippableItem item, List<Aura> auras, Dictionary<GemColor, int> gemCount)
        {
            foreach (var itemSpell in item.Spells)
            {
                if (Glossaries.Auras.ByID.ContainsKey(itemSpell.ID))
                    auras.Add(Glossaries.Auras.ByID[itemSpell.ID]);
            }

            if (item.Socket1 != null && item.Socket1.SocketedGem != null && item.Socket1.IsGemActive(gemCount[GemColor.Red], gemCount[GemColor.Blue], gemCount[GemColor.Yellow]))
            {
                foreach (var gem1Spell in item.Socket1.SocketedGem.Spells)
                    if (Glossaries.Auras.ByID.ContainsKey(gem1Spell.ID))
                        auras.Add(Glossaries.Auras.ByID[gem1Spell.ID]);
            }

            if (item.Socket2 != null && item.Socket2.SocketedGem != null && item.Socket2.IsGemActive(gemCount[GemColor.Red], gemCount[GemColor.Blue], gemCount[GemColor.Yellow]))
            {
                foreach (var gem2Spell in item.Socket2.SocketedGem.Spells)
                    if (Glossaries.Auras.ByID.ContainsKey(gem2Spell.ID))
                        auras.Add(Glossaries.Auras.ByID[gem2Spell.ID]);
            }

            if (item.Socket3 != null && item.Socket3.SocketedGem != null && item.Socket3.IsGemActive(gemCount[GemColor.Red], gemCount[GemColor.Blue], gemCount[GemColor.Yellow]))
            {
                foreach (var gem3Spell in item.Socket3.SocketedGem.Spells)
                    if (Glossaries.Auras.ByID.ContainsKey(gem3Spell.ID))
                        auras.Add(Glossaries.Auras.ByID[gem3Spell.ID]);
            }
        }

        private Dictionary<GemColor, int> GetGemCount()
        {
            Dictionary<GemColor, int> gemCount = new();
            gemCount.Add(GemColor.Red, 0);
            gemCount.Add(GemColor.Blue, 0);
            gemCount.Add(GemColor.Yellow, 0);

            List<Gem> gems = Head.GetGems();
            gems.AddRange(Neck.GetGems());
            gems.AddRange(Shoulder.GetGems());
            gems.AddRange(Cloak.GetGems());
            gems.AddRange(Chest.GetGems());
            gems.AddRange(Wrist.GetGems());
            gems.AddRange(Hand.GetGems());
            gems.AddRange(Waist.GetGems());
            gems.AddRange(Legs.GetGems());
            gems.AddRange(Finger1.GetGems());
            gems.AddRange(Finger2.GetGems());
            gems.AddRange(Trinket1.GetGems());
            gems.AddRange(Trinket2.GetGems());
            gems.AddRange(Relic.GetGems());
            gems.AddRange(Weapon.GetGems());

            foreach (var gem in gems)
            {
                switch (gem.Color)
                {
                    case GemColor.Red:
                        gemCount[GemColor.Red]++;
                        break;
                    case GemColor.Blue:
                        gemCount[GemColor.Blue]++;
                        break;
                    case GemColor.Yellow:
                        gemCount[GemColor.Yellow]++;
                        break;
                    case GemColor.Purple:
                        gemCount[GemColor.Red]++;
                        gemCount[GemColor.Blue]++;
                        break;
                    case GemColor.Green:
                        gemCount[GemColor.Yellow]++;
                        gemCount[GemColor.Blue]++;
                        break;
                    case GemColor.Orange:
                        gemCount[GemColor.Red]++;
                        gemCount[GemColor.Yellow]++;
                        break;
                    case GemColor.Meta:
                        break;
                    case GemColor.Prismatic:
                        gemCount[GemColor.Red]++;
                        gemCount[GemColor.Blue]++;
                        gemCount[GemColor.Yellow]++;
                        break;
                    default:
                        break;
                }
            }
            return gemCount;
        }

    }
}
