using RetSim.Items;
using System;
using System.Collections.Generic;

namespace RetSim
{
    public record Equipment
    {
        public StatSet Stats => CalculateStats(this);
        public List<Spell> Spells => GenerateAuraList(this);

        public Dictionary<GemColor, int> GemTotals { get; private set; }

        public EquippableItem[] PlayerEquipment { get; init; } = new EquippableItem[Constants.EquipmentSlots.Total];

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

        private static StatSet CalculateStats(Equipment equipment)
        {
            if (equipment.GemTotals == null)
                equipment.GemTotals = GetGemCount(equipment);

            return new()
            {
                Stamina = CalculateStat(StatName.Stamina, equipment, equipment.GemTotals),

                Intellect = CalculateStat(StatName.Intellect, equipment, equipment.GemTotals),
                ManaPer5 = CalculateStat(StatName.ManaPer5, equipment, equipment.GemTotals),

                Strength = CalculateStat(StatName.Strength, equipment, equipment.GemTotals),
                AttackPower = CalculateStat(StatName.AttackPower, equipment, equipment.GemTotals),

                Agility = CalculateStat(StatName.Agility, equipment, equipment.GemTotals),
                CritRating = CalculateStat(StatName.CritRating, equipment, equipment.GemTotals),

                HitRating = CalculateStat(StatName.HitRating, equipment, equipment.GemTotals),
                HasteRating = CalculateStat(StatName.Haste, equipment, equipment.GemTotals),

                ExpertiseRating = CalculateStat(StatName.Expertise, equipment, equipment.GemTotals),

                ArmorPenetration = CalculateStat(StatName.ArmorPenetration, equipment, equipment.GemTotals),

                SpellPower = CalculateStat(StatName.SpellPower, equipment, equipment.GemTotals),
                SpellCritRating = CalculateStat(StatName.SpellCrit, equipment, equipment.GemTotals),
                SpellHitRating = CalculateStat(StatName.SpellHit, equipment, equipment.GemTotals),
                SpellHasteRating = CalculateStat(StatName.SpellHaste, equipment, equipment.GemTotals)
            };
        }

        private static int CalculateStat(StatName name, Equipment equipment, Dictionary<GemColor, int> gems)
        {
            int total = 0;

            foreach (EquippableItem item in equipment.PlayerEquipment)
            {
                if (item != null)
                    total += CalculateStatOfPiece(name, item, gems);
            }

            return total;
        }

        private static int CalculateStatOfPiece(StatName name, EquippableItem item, Dictionary<GemColor, int> gems)
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

            return spells;
        }
        private static void AddItemAuras(EquippableItem item, Dictionary<GemColor, int> gems, List<Spell> spells)
        {
            if (Data.Spells.ByID.ContainsKey(item.OnEquip.ID))
                    spells.Add(Data.Spells.ByID[item.OnEquip.ID]);

            if (item.Socket1 != null && item.Socket1.IsMetaGem() is MetaGem meta && meta.IsActive(gems[GemColor.Red], gems[GemColor.Blue], gems[GemColor.Yellow]))
            {
                if (Data.Spells.ByID.ContainsKey(meta.Spell.ID))
                    spells.Add(Data.Spells.ByID[(meta.Spell.ID)]);
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
