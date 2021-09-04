using RetSim.Items;
using System;
using System.Collections.Generic;

namespace RetSim
{
    public record Equipment
    {
        public Stats Stats => CalculateStats();
        public List<Aura> Auras => GenerateAuraList();

        public Dictionary<GemColor, int> GemTotals = new()
        {
            { GemColor.Red, 0 },
            { GemColor.Blue, 0 },
            { GemColor.Yellow, 0 }
        };

        private EquippableArmor[] AllEquipment { get; init; } = new EquippableArmor[Constants.EquipmentSlots.Total];

        public EquippableArmor Head 
        { 
            get => AllEquipment[Constants.EquipmentSlots.Head];
            set => AllEquipment[Constants.EquipmentSlots.Head] = value;
        }

        public EquippableArmor Neck
        {
            get => AllEquipment[Constants.EquipmentSlots.Neck];
            set => AllEquipment[Constants.EquipmentSlots.Neck] = value;
        }
        public EquippableArmor Shoulders
        {
            get => AllEquipment[Constants.EquipmentSlots.Shoulders];
            set => AllEquipment[Constants.EquipmentSlots.Shoulders] = value;
        }

        public EquippableArmor Back
        {
            get => AllEquipment[Constants.EquipmentSlots.Back];
            set => AllEquipment[Constants.EquipmentSlots.Back] = value;
        }
        public EquippableArmor Chest

        {
            get => AllEquipment[Constants.EquipmentSlots.Chest];
            set => AllEquipment[Constants.EquipmentSlots.Chest] = value;
        }
        public EquippableArmor Wrists
        {
            get => AllEquipment[Constants.EquipmentSlots.Wrists];
            set => AllEquipment[Constants.EquipmentSlots.Wrists] = value;
        }
        public EquippableArmor Hands
        {
            get => AllEquipment[Constants.EquipmentSlots.Hands];
            set => AllEquipment[Constants.EquipmentSlots.Hands] = value;
        }

        public EquippableArmor Waist
        {
            get => AllEquipment[Constants.EquipmentSlots.Waist];
            set => AllEquipment[Constants.EquipmentSlots.Waist] = value;
        }
        public EquippableArmor Legs
        {
            get => AllEquipment[Constants.EquipmentSlots.Legs];
            set => AllEquipment[Constants.EquipmentSlots.Legs] = value;
        }
        public EquippableArmor Feet
        {
            get => AllEquipment[Constants.EquipmentSlots.Feet];
            set => AllEquipment[Constants.EquipmentSlots.Feet] = value;
        }

        public EquippableArmor Finger1
        {
            get => AllEquipment[Constants.EquipmentSlots.Finger1];
            set => AllEquipment[Constants.EquipmentSlots.Finger1] = value;
        }
        public EquippableArmor Finger2
        {
            get => AllEquipment[Constants.EquipmentSlots.Finger2];
            set => AllEquipment[Constants.EquipmentSlots.Finger2] = value;
        }
        public EquippableArmor Trinket1
        {
            get => AllEquipment[Constants.EquipmentSlots.Trinket1];
            set => AllEquipment[Constants.EquipmentSlots.Trinket1] = value;
        }
        public EquippableArmor Trinket2
        {
            get => AllEquipment[Constants.EquipmentSlots.Trinket2];
            set => AllEquipment[Constants.EquipmentSlots.Trinket2] = value;
        }
        public EquippableArmor Relic
        {
            get => AllEquipment[Constants.EquipmentSlots.Relic];
            set => AllEquipment[Constants.EquipmentSlots.Relic] = value;
        }

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
            int total = CalculateStatOfPiece(Weapon, name, gemCount);

            foreach (EquippableItem item in AllEquipment)
                total += CalculateStatOfPiece(item, name, gemCount);

            return total;
        }

        private static int CalculateStatOfPiece(EquippableItem item, ItemStatNames name, Dictionary<GemColor, int> gemCount)
        {
            int passive = item.Stats[name];
            int bonus = item.IsSocketBonusActive() ? item.SocketBonus[name] : 0;
            int sockets = 0;

            foreach (Socket socket in item.Sockets)
            {
                if (socket != null && socket.SocketedGem != null)
                {
                    if (socket.SocketedGem.Color == GemColor.Meta)
                    {
                        if (!socket.IsGemActive(gemCount[GemColor.Red], gemCount[GemColor.Blue], gemCount[GemColor.Yellow]))
                            break;
                    }
                    
                    sockets += socket.SocketedGem.Stats[name];
                }
            }

            return passive + bonus + sockets;
        }

        private List<Aura> GenerateAuraList()
        {
            var gemCount = GetGemCount();

            List<Aura> auras = new();
            GenerateAuraListOf(Head, auras, gemCount);
            GenerateAuraListOf(Neck, auras, gemCount);
            GenerateAuraListOf(Shoulders, auras, gemCount);
            GenerateAuraListOf(Back, auras, gemCount);
            GenerateAuraListOf(Chest, auras, gemCount);
            GenerateAuraListOf(Wrists, auras, gemCount);
            GenerateAuraListOf(Hands, auras, gemCount);
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
            if (Shoulders.Set != null) if (SetCounts.ContainsKey(Shoulders.Set.ID)) SetCounts[Shoulders.Set.ID]++; else SetCounts.Add(Shoulders.Set.ID, 1);
            if (Back.Set != null) if (SetCounts.ContainsKey(Back.Set.ID)) SetCounts[Back.Set.ID]++; else SetCounts.Add(Back.Set.ID, 1);
            if (Chest.Set != null) if (SetCounts.ContainsKey(Chest.Set.ID)) SetCounts[Chest.Set.ID]++; else SetCounts.Add(Chest.Set.ID, 1);
            if (Wrists.Set != null) if (SetCounts.ContainsKey(Wrists.Set.ID)) SetCounts[Wrists.Set.ID]++; else SetCounts.Add(Wrists.Set.ID, 1);
            if (Hands.Set != null) if (SetCounts.ContainsKey(Hands.Set.ID)) SetCounts[Hands.Set.ID]++; else SetCounts.Add(Hands.Set.ID, 1);
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
                foreach (var setAura in Glossaries.Items.SetsByID[setCount.Key].SetAuras)
                {
                    if (setCount.Value >= setAura.RequiredCount)
                    {
                        if (Glossaries.Auras.ByID.ContainsKey(setAura.ID))
                            auras.Add(Glossaries.Auras.ByID[setAura.ID]);
                    }

                }
            }

            return auras;
        }
        private static void GenerateAuraListOf(EquippableItem item, List<Aura> auras, Dictionary<GemColor, int> gemCount)
        {
            foreach (var aura in item.Auras)
            {
                if (Glossaries.Auras.ByID.ContainsKey(aura.ID))
                    auras.Add(Glossaries.Auras.ByID[aura.ID]);
            }

            if (item.Socket1 != null && item.Socket1.SocketedGem != null && item.Socket1.IsGemActive(gemCount[GemColor.Red], gemCount[GemColor.Blue], gemCount[GemColor.Yellow]))
            {
                foreach (var gem1Aura in item.Socket1.SocketedGem.Auras)
                {
                    if (Glossaries.Auras.ByID.ContainsKey(gem1Aura.ID))
                        auras.Add(Glossaries.Auras.ByID[gem1Aura.ID]);
                }
            }

            //TODO: Make Meta Gem only appear on socket 1

            if (item.Socket2 != null && item.Socket2.SocketedGem != null && item.Socket2.IsGemActive(gemCount[GemColor.Red], gemCount[GemColor.Blue], gemCount[GemColor.Yellow]))
            {
                foreach (var gem2Aura in item.Socket2.SocketedGem.Auras)
                    if (Glossaries.Auras.ByID.ContainsKey(gem2Aura.ID))
                        auras.Add(Glossaries.Auras.ByID[gem2Aura.ID]);
            }

            if (item.Socket3 != null && item.Socket3.SocketedGem != null && item.Socket3.IsGemActive(gemCount[GemColor.Red], gemCount[GemColor.Blue], gemCount[GemColor.Yellow]))
            {
                foreach (var gem3Aura in item.Socket3.SocketedGem.Auras)
                    if (Glossaries.Auras.ByID.ContainsKey(gem3Aura.ID))
                        auras.Add(Glossaries.Auras.ByID[gem3Aura.ID]);
            }
        }

        private Dictionary<GemColor, int> GetGemCount()
        {
            Dictionary<GemColor, int> totals = new()
            {
                { GemColor.Red, 0 },
                { GemColor.Blue, 0 },
                { GemColor.Yellow, 0 }
            };

            List<Gem> gems = Head.GetGems();
            gems.AddRange(Neck.GetGems());
            gems.AddRange(Shoulders.GetGems());
            gems.AddRange(Back.GetGems());
            gems.AddRange(Chest.GetGems());
            gems.AddRange(Wrists.GetGems());
            gems.AddRange(Hands.GetGems());
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
                foreach (GemColor color in totals.Keys)
                {
                    if (Convert.ToBoolean(gem.Color & color))
                        totals[color]++;
                }
            }

            return totals;
        }

    }
}
