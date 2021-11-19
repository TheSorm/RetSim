using RetSim.Items;
using RetSimDesktop.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using static RetSim.Data.Items;

namespace RetSimDesktop.ViewModel
{
    public class RetSimUIModel : IJsonOnDeserialized
    {
        private SelectedGear _SelectedGear;
        private SelectedTalents _SelectedTalents;
        private SelectedConsumables _SelectedConsumables;
        private SelectedBuffs _SelectedBuffs;
        private SelectedDebuffs _SelectedDebuffs;
        private SelectedGemWrapper _SelectedGemWrapper;
        private SimOutput _CurrentSimOutput;
        private SelectedPhases _SelectedPhases;
        private SimSettings _SimSettings;
        private SimButtonStatus _SimButtonStatus;
        private TooltipSettings _TooltipSettings;
        private Dictionary<Slot, Dictionary<int, List<DisplayGear>>> _GearByPhases;
        private Dictionary<int, DisplayGear> _AllGear;
        private Dictionary<WeaponType, Dictionary<int, List<DisplayWeapon>>> _WeaponsByPhases;
        private Dictionary<int, DisplayWeapon> _AllWeapons;
        private Dictionary<Slot, List<Enchant>> _EnchantsBySlot;

        public RetSimUIModel()
        {
            _CurrentSimOutput = new SimOutput() { Progress = 0, DPS = 0, Min = 0, Max = 0, MedianCombatLog = new(), MaxCombatLog = new(), MinCombatLog = new() };

            _SelectedTalents = new();

            _SelectedConsumables = new();

            _SelectedBuffs = new();

            _SelectedDebuffs = new();

            _SelectedGemWrapper = new();

            _SelectedPhases = new();

            _SimSettings = new();

            _SimButtonStatus = new()
            {
                IsGearSimButtonEnabled = true,
                IsSimButtonEnabled = true,
            };

            _GearByPhases = new();
            _WeaponsByPhases = new();
            _AllGear = new();
            _AllWeapons = new();
            foreach (var item in AllItems.Values)
            {
                if (item is EquippableWeapon weapon)
                {
                    if (!_WeaponsByPhases.ContainsKey(weapon.Type))
                    {
                        _WeaponsByPhases[weapon.Type] = new();
                    }
                    if (!_WeaponsByPhases[weapon.Type].ContainsKey(weapon.Phase))
                    {
                        _WeaponsByPhases[weapon.Type][weapon.Phase] = new();
                    }
                    DisplayWeapon displayWeapon = new() { Weapon = weapon, EnabledForGearSim = true, DPS = 0 };
                    _WeaponsByPhases[weapon.Type][weapon.Phase].Add(displayWeapon);
                    _AllWeapons.Add(displayWeapon.Weapon.ID, displayWeapon);
                }
                else
                {
                    if (!_GearByPhases.ContainsKey(item.Slot))
                    {
                        _GearByPhases[item.Slot] = new();
                    }

                    if (!_GearByPhases[item.Slot].ContainsKey(item.Phase))
                    {
                        _GearByPhases[item.Slot][item.Phase] = new();
                    }

                    DisplayGear displayGear = new() { Item = item, EnabledForGearSim = true, DPS = 0 };
                    _GearByPhases[item.Slot][item.Phase].Add(displayGear);
                    _AllGear.Add(displayGear.Item.ID, displayGear);
                }
            }

            _EnchantsBySlot = new();
            foreach (var enchant in Enchants.Values)
            {
                if (!_EnchantsBySlot.ContainsKey(enchant.Slot))
                {
                    _EnchantsBySlot[enchant.Slot] = new();
                    _EnchantsBySlot[enchant.Slot].Add(new() { Name = "Unenchanted", ID = -1, Stats = new(), Slot = enchant.Slot, ItemID = -1 });
                }
                _EnchantsBySlot[enchant.Slot].Add(enchant);
            }

            _SelectedGear = new();
            ConnectSelectedGearToListOfItems();

            _TooltipSettings = new()
            {
                HoverItemID = 0,
            };

        }

        private void ConnectSelectedGearToListOfItems()
        {
            _SelectedGear.SelectedHead = _SelectedGear.SelectedHead != null ? _AllGear[_SelectedGear.SelectedHead.Item.ID] : null;
            _SelectedGear.SelectedNeck = _SelectedGear.SelectedNeck != null ? _AllGear[_SelectedGear.SelectedNeck.Item.ID] : null;
            _SelectedGear.SelectedShoulders = _SelectedGear.SelectedShoulders != null ? _AllGear[_SelectedGear.SelectedShoulders.Item.ID] : null;
            _SelectedGear.SelectedBack = _SelectedGear.SelectedBack != null ? _AllGear[_SelectedGear.SelectedBack.Item.ID] : null;
            _SelectedGear.SelectedChest = _SelectedGear.SelectedChest != null ? _AllGear[_SelectedGear.SelectedChest.Item.ID] : null;
            _SelectedGear.SelectedWrists = _SelectedGear.SelectedWrists != null ? _AllGear[_SelectedGear.SelectedWrists.Item.ID] : null;
            _SelectedGear.SelectedHands = _SelectedGear.SelectedHands != null ? _AllGear[_SelectedGear.SelectedHands.Item.ID] : null;
            _SelectedGear.SelectedWaist = _SelectedGear.SelectedWaist != null ? _AllGear[_SelectedGear.SelectedWaist.Item.ID] : null;
            _SelectedGear.SelectedLegs = _SelectedGear.SelectedLegs != null ? _AllGear[_SelectedGear.SelectedLegs.Item.ID] : null;
            _SelectedGear.SelectedFeet = _SelectedGear.SelectedFeet != null ? _AllGear[_SelectedGear.SelectedFeet.Item.ID] : null;
            _SelectedGear.SelectedFinger1 = _SelectedGear.SelectedFinger1 != null ? _AllGear[_SelectedGear.SelectedFinger1.Item.ID] : null;
            _SelectedGear.SelectedFinger2 = _SelectedGear.SelectedFinger2 != null ? _AllGear[_SelectedGear.SelectedFinger2.Item.ID] : null;
            _SelectedGear.SelectedTrinket1 = _SelectedGear.SelectedTrinket1 != null ? _AllGear[_SelectedGear.SelectedTrinket1.Item.ID] : null;
            _SelectedGear.SelectedTrinket2 = _SelectedGear.SelectedTrinket2 != null ? _AllGear[_SelectedGear.SelectedTrinket2.Item.ID] : null;
            _SelectedGear.SelectedRelic = _SelectedGear.SelectedRelic != null ? _AllGear[_SelectedGear.SelectedRelic.Item.ID] : null;
            _SelectedGear.SelectedWeapon = _SelectedGear.SelectedWeapon != null ? _AllWeapons[_SelectedGear.SelectedWeapon.Weapon.ID] : null;
            _SelectedGear.HeadEnchant = _SelectedGear.HeadEnchant != null ? Enchants[_SelectedGear.HeadEnchant.ID] : null;
            _SelectedGear.ShouldersEnchant = _SelectedGear.ShouldersEnchant != null ? Enchants[_SelectedGear.ShouldersEnchant.ID] : null;
            _SelectedGear.BackEnchant = _SelectedGear.BackEnchant != null ? Enchants[_SelectedGear.BackEnchant.ID] : null;
            _SelectedGear.ChestEnchant = _SelectedGear.ChestEnchant != null ? Enchants[_SelectedGear.ChestEnchant.ID] : null;
            _SelectedGear.WristsEnchant = _SelectedGear.WristsEnchant != null ? Enchants[_SelectedGear.WristsEnchant.ID] : null;
            _SelectedGear.HandsEnchant = _SelectedGear.HandsEnchant != null ? Enchants[_SelectedGear.HandsEnchant.ID] : null;
            _SelectedGear.LegsEnchant = _SelectedGear.LegsEnchant != null ? Enchants[_SelectedGear.LegsEnchant.ID] : null;
            _SelectedGear.FeetEnchant = _SelectedGear.FeetEnchant != null ? Enchants[_SelectedGear.FeetEnchant.ID] : null;
            _SelectedGear.Finger1Enchant = _SelectedGear.Finger1Enchant != null ? Enchants[_SelectedGear.Finger1Enchant.ID] : null;
            _SelectedGear.Finger2Enchant = _SelectedGear.Finger2Enchant != null ? Enchants[_SelectedGear.Finger2Enchant.ID] : null;
            _SelectedGear.WeaponEnchant = _SelectedGear.WeaponEnchant != null ? Enchants[_SelectedGear.WeaponEnchant.ID] : null;
        }


        [JsonIgnore]
        public Dictionary<WeaponType, Dictionary<int, List<DisplayWeapon>>> WeaponsByPhases
        {
            get { return _WeaponsByPhases; }
            set { _WeaponsByPhases = value; }
        }

        [JsonIgnore]
        public Dictionary<Slot, Dictionary<int, List<DisplayGear>>> GearByPhases
        {
            get { return _GearByPhases; }
            set { _GearByPhases = value; }
        }

        [JsonIgnore]
        public Dictionary<int, DisplayGear> AllGear
        {
            get { return _AllGear; }
            set { _AllGear = value; }
        }

        [JsonIgnore]
        public Dictionary<int, DisplayWeapon> AllWeapons
        {
            get { return _AllWeapons; }
            set { _AllWeapons = value; }
        }

        [JsonIgnore]
        public Dictionary<Slot, List<Enchant>> EnchantsBySlot
        {
            get { return _EnchantsBySlot; }
            set { _EnchantsBySlot = value; }
        }

        [JsonIgnore]
        public SimButtonStatus SimButtonStatus
        {
            get { return _SimButtonStatus; }
            set { _SimButtonStatus = value; }
        }

        public SelectedGear SelectedGear
        {
            get { return _SelectedGear; }
            set { _SelectedGear = value; }
        }

        [JsonIgnore]
        public SimOutput CurrentSimOutput
        {
            get { return _CurrentSimOutput; }
            set { _CurrentSimOutput = value; }
        }
        public SelectedConsumables SelectedConsumables
        {
            get { return _SelectedConsumables; }
            set { _SelectedConsumables = value; }
        }
        public SelectedTalents SelectedTalents
        {
            get { return _SelectedTalents; }
            set { _SelectedTalents = value; }
        }

        public SelectedPhases SelectedPhases
        {
            get { return _SelectedPhases; }
            set { _SelectedPhases = value; }
        }
        public SimSettings SimSettings
        {
            get { return _SimSettings; }
            set { _SimSettings = value; }
        }

        [JsonIgnore]
        public TooltipSettings TooltipSettings
        {
            get { return _TooltipSettings; }
            set { _TooltipSettings = value; }
        }

        public SelectedBuffs SelectedBuffs
        {
            get => _SelectedBuffs;
            set => _SelectedBuffs = value;
        }
        public SelectedDebuffs SelectedDebuffs
        {
            get => _SelectedDebuffs;
            set => _SelectedDebuffs = value;
        }

        public SelectedGemWrapper SelectedGemWrapper
        {
            get => _SelectedGemWrapper;
            set => _SelectedGemWrapper = value;
        }

        public void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true, Converters = { new SelectedGearJsonConverter(), new SelectedGemJsonConverter() } };
            string jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText($"Properties\\settings.json", jsonString);

        }
        public static RetSimUIModel Load()
        {
            RetSimUIModel? uiModel = null;

            try
            {
                string jsonString = File.ReadAllText($"Properties\\settings.json");
                var options = new JsonSerializerOptions { Converters = { new SelectedGearJsonConverter(), new SelectedGemJsonConverter() } };
                uiModel = JsonSerializer.Deserialize<RetSimUIModel>(jsonString, options);
            }
            catch (Exception) { }


            if (uiModel != null)
            {
                return uiModel;
            }
            else
            {
                return new RetSimUIModel();
            }
        }

        public void OnDeserialized()
        {
            ConnectSelectedGearToListOfItems();
        }
    }
}
