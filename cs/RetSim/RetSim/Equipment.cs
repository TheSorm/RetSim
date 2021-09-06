using RetSim.Items;
using System;
using System.Collections.Generic;

namespace RetSim
{
    public record Equipment
    {
        public Stats Stats => CalculateStats(this);
        public List<Aura> Auras => GenerateAuraList(this);

        public Dictionary<GemColor, int> GemTotals { get; private set; }

        public EquippableItem[] PlayerEquipment { get; init; } = new EquippableItem[Constants.EquipmentSlots.Total];

        #region Equipment

        public EquippableArmor Head
        {
            get => (EquippableArmor)PlayerEquipment[Constants.EquipmentSlots.Head];
            set => PlayerEquipment[Constants.EquipmentSlots.Head] = value;
        }

        public EquippableArmor Neck
        {
            get => (EquippableArmor)PlayerEquipment[Constants.EquipmentSlots.Neck];
            set => PlayerEquipment[Constants.EquipmentSlots.Neck] = value;
        }
        public EquippableArmor Shoulders
        {
            get => (EquippableArmor)PlayerEquipment[Constants.EquipmentSlots.Shoulders];
            set => PlayerEquipment[Constants.EquipmentSlots.Shoulders] = value;
        }

        public EquippableArmor Back
        {
            get => (EquippableArmor)PlayerEquipment[Constants.EquipmentSlots.Back];
            set => PlayerEquipment[Constants.EquipmentSlots.Back] = value;
        }
        public EquippableArmor Chest

        {
            get => (EquippableArmor)PlayerEquipment[Constants.EquipmentSlots.Chest];
            set => PlayerEquipment[Constants.EquipmentSlots.Chest] = value;
        }
        public EquippableArmor Wrists
        {
            get => (EquippableArmor)PlayerEquipment[Constants.EquipmentSlots.Wrists];
            set => PlayerEquipment[Constants.EquipmentSlots.Wrists] = value;
        }
        public EquippableArmor Hands
        {
            get => (EquippableArmor)PlayerEquipment[Constants.EquipmentSlots.Hands];
            set => PlayerEquipment[Constants.EquipmentSlots.Hands] = value;
        }

        public EquippableArmor Waist
        {
            get => (EquippableArmor)PlayerEquipment[Constants.EquipmentSlots.Waist];
            set => PlayerEquipment[Constants.EquipmentSlots.Waist] = value;
        }
        public EquippableArmor Legs
        {
            get => (EquippableArmor)PlayerEquipment[Constants.EquipmentSlots.Legs];
            set => PlayerEquipment[Constants.EquipmentSlots.Legs] = value;
        }
        public EquippableArmor Feet
        {
            get => (EquippableArmor)PlayerEquipment[Constants.EquipmentSlots.Feet];
            set => PlayerEquipment[Constants.EquipmentSlots.Feet] = value;
        }

        public EquippableArmor Finger1
        {
            get => (EquippableArmor)PlayerEquipment[Constants.EquipmentSlots.Finger1];
            set => PlayerEquipment[Constants.EquipmentSlots.Finger1] = value;
        }
        public EquippableArmor Finger2
        {
            get => (EquippableArmor)PlayerEquipment[Constants.EquipmentSlots.Finger2];
            set => PlayerEquipment[Constants.EquipmentSlots.Finger2] = value;
        }
        public EquippableArmor Trinket1
        {
            get => (EquippableArmor)PlayerEquipment[Constants.EquipmentSlots.Trinket1];
            set => PlayerEquipment[Constants.EquipmentSlots.Trinket1] = value;
        }
        public EquippableArmor Trinket2
        {
            get => (EquippableArmor)PlayerEquipment[Constants.EquipmentSlots.Trinket2];
            set => PlayerEquipment[Constants.EquipmentSlots.Trinket2] = value;
        }
        public EquippableArmor Relic
        {
            get => (EquippableArmor)PlayerEquipment[Constants.EquipmentSlots.Relic];
            set => PlayerEquipment[Constants.EquipmentSlots.Relic] = value;
        }

        public EquippableWeapon Weapon
        {
            get => (EquippableWeapon)PlayerEquipment[Constants.EquipmentSlots.Weapon];
            set => PlayerEquipment[Constants.EquipmentSlots.Weapon] = value;
        }

        #endregion

        private static Stats CalculateStats(Equipment equipment)
        {
            if (equipment.GemTotals == null)
                equipment.GemTotals = GetGemCount(equipment);

            return new()
            {
                Stamina = CalculateStat(ItemStatNames.Stamina, equipment, equipment.GemTotals),

                Intellect = CalculateStat(ItemStatNames.Intellect, equipment, equipment.GemTotals),
                ManaPer5 = CalculateStat(ItemStatNames.ManaPer5, equipment, equipment.GemTotals),

                Strength = CalculateStat(ItemStatNames.Strength, equipment, equipment.GemTotals),
                AttackPower = CalculateStat(ItemStatNames.AttackPower, equipment, equipment.GemTotals),

                Agility = CalculateStat(ItemStatNames.Agility, equipment, equipment.GemTotals),
                CritRating = CalculateStat(ItemStatNames.Crit, equipment, equipment.GemTotals),

                HitRating = CalculateStat(ItemStatNames.Hit, equipment, equipment.GemTotals),
                HasteRating = CalculateStat(ItemStatNames.Haste, equipment, equipment.GemTotals),

                ExpertiseRating = CalculateStat(ItemStatNames.Expertise, equipment, equipment.GemTotals),

                ArmorPenetration = CalculateStat(ItemStatNames.ArmorPenetration, equipment, equipment.GemTotals),

                SpellPower = CalculateStat(ItemStatNames.SpellDamage, equipment, equipment.GemTotals),
                SpellCritRating = CalculateStat(ItemStatNames.SpellCrit, equipment, equipment.GemTotals),
                SpellHitRating = CalculateStat(ItemStatNames.SpellHit, equipment, equipment.GemTotals),
                SpellHasteRating = CalculateStat(ItemStatNames.SpellHaste, equipment, equipment.GemTotals)
            };
        }

        private static int CalculateStat(ItemStatNames name, Equipment equipment, Dictionary<GemColor, int> gems)
        {
            int total = 0;

            foreach (EquippableItem item in equipment.PlayerEquipment)
            {
                if (item != null)
                    total += CalculateStatOfPiece(name, item, gems);
            }

            return total;
        }

        private static int CalculateStatOfPiece(ItemStatNames name, EquippableItem item, Dictionary<GemColor, int> gems)
        {
            int passive = item.Stats[name];
            int bonus = item.IsSocketBonusActive() ? item.SocketBonus[name] : 0;
            int sockets = 0;

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

        private static List<Aura> GenerateAuraList(Equipment equipment)
        {
            var auras = new List<Aura>();
            var sets = new Dictionary<int, int>();

            foreach (EquippableItem item in equipment.PlayerEquipment)
            {
                if (item != null)
                {
                    AddItemAuras(item, equipment.GemTotals, auras);

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
                foreach (var aura in Glossaries.Items.Sets[set.Key].SetSpells)
                {
                    if (set.Value >= aura.RequiredCount)
                    {
                        if (Glossaries.Auras.ByID.ContainsKey(aura.ID))
                            auras.Add(Glossaries.Auras.ByID[aura.ID]);
                    }

                }
            }

            return auras;
        }
        private static void AddItemAuras(EquippableItem item, Dictionary<GemColor, int> gems, List<Aura> auras)
        {
            foreach (ItemSpell aura in item.Spells)
            {
                if (Glossaries.Auras.ByID.ContainsKey(aura.ID))
                    auras.Add(Glossaries.Auras.ByID[aura.ID]);
            }

            if (item.Socket1 != null && item.Socket1.IsMetaGem() is MetaGem meta && meta.IsActive(gems[GemColor.Red], gems[GemColor.Blue], gems[GemColor.Yellow]))
            {
                if (Glossaries.Auras.ByID.ContainsKey(meta.Spell.ID))
                    auras.Add(Glossaries.Auras.ByID[(meta.Spell.ID)]);

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
}
