using RetSim.Items;
using RetSimDesktop.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using static RetSim.Data.Items;

namespace RetSimDesktop.ViewModel
{
    public class RetSimUIModel
    {
        private SelectedGear _SelectedGear;
        private SelectedTalents _SelectedTalents;
        private SimOutput _CurrentSimOutput;
        private SelectedPhases _SelectedPhases;
        private SimSettings _SimSettings;
        private SimButtonStatus _SimButtonStatus;
        private Dictionary<Slot, Dictionary<int, List<ItemDPS>>> _GearByPhases;
        private Dictionary<int, ItemDPS> _AllGear;
        private Dictionary<WeaponType, Dictionary<int, List<WeaponDPS>>> _WeaponsByPhases;
        private Dictionary<int, WeaponDPS> _AllWeapons;
        private Dictionary<Slot, List<Enchant>> _EnchantsBySlot;


        public RetSimUIModel()
        {
            _CurrentSimOutput = new SimOutput() { Progress = 0, DPS = 0, Min = 0, Max = 0, MedianCombatLog = new(), MaxCombatLog = new(), MinCombatLog = new() };

            _SelectedTalents = new SelectedTalents()
            {
                ConvictionEnabled = true,
                CrusadeEnabled = true,
                DivineStrengthEnabled = true,
                FanaticismEnabled = true,
                ImprovedSanctityAuraEnabled = true,
                PrecisionEnabled = true,
                SanctifiedSealsEnabled = true,
                SanctityAuraEnabled = true,
                TwoHandedWeaponSpecializationEnabled = true,
                VengeanceEnabled = true
            };

            _SelectedPhases = new SelectedPhases()
            {
                Phase1Selected = true,
                Phase2Selected = true,
                Phase3Selected = false,
                Phase4Selected = false,
                Phase5Selected = false
            };

            _SimSettings = new SimSettings()
            {
                SimulationCountSetting = "10000",
                MinFightDurationSetting = "180000",
                MaxFightDurationSetting = "200000",
            };

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
                    WeaponDPS weaponDPS = new() { Weapon = weapon, DPS = 0 };
                    _WeaponsByPhases[weapon.Type][weapon.Phase].Add(weaponDPS);
                    _AllWeapons.Add(weaponDPS.Weapon.ID, weaponDPS);
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

                    ItemDPS itemDPS = new() { Item = item, DPS = 0 };
                    _GearByPhases[item.Slot][item.Phase].Add(itemDPS);
                    _AllGear.Add(itemDPS.Item.ID, itemDPS);
                }
            }

            _EnchantsBySlot = new();
            foreach(var enchant in Enchants.Values)
            {
                if (!_EnchantsBySlot.ContainsKey(enchant.Slot))
                {
                    _EnchantsBySlot[enchant.Slot] = new();
                    _EnchantsBySlot[enchant.Slot].Add(new() { Name = "Unenchanted", ID = -1, Stats = new(), Slot = enchant.Slot, ItemID = -1});
                }
                _EnchantsBySlot[enchant.Slot].Add(enchant);
            }

            _SelectedGear = new SelectedGear
            {
                SelectedHead = _AllGear[32461],
                SelectedNeck = _AllGear[30022],
                SelectedShoulders = _AllGear[29075],
                SelectedBack = _AllGear[30098],
                SelectedChest = _AllGear[30129],
                SelectedWrists = _AllGear[28795],
                SelectedHands = _AllGear[29947],
                SelectedWaist = _AllGear[30032],
                SelectedLegs = _AllGear[30257],
                SelectedFeet = _AllGear[29951],
                SelectedFinger1 = _AllGear[30834],
                SelectedFinger2 = _AllGear[28730],
                SelectedTrinket1 = _AllGear[29383],
                SelectedTrinket2 = _AllGear[28830],
                SelectedRelic = _AllGear[27484],
                SelectedWeapon = _AllWeapons[29993],

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

        public Dictionary<WeaponType, Dictionary<int, List<WeaponDPS>>> WeaponsByPhases
        {
            get { return _WeaponsByPhases; }
            set { _WeaponsByPhases = value; }
        }
        public Dictionary<Slot, Dictionary<int, List<ItemDPS>>> GearByPhases
        {
            get { return _GearByPhases; }
            set { _GearByPhases = value; }
        }
        public Dictionary<int, ItemDPS> AllGear
        {
            get { return _AllGear; }
            set { _AllGear = value; }
        }
        public Dictionary<int, WeaponDPS> AllWeapons
        {
            get { return _AllWeapons; }
            set { _AllWeapons = value; }
        }

        public Dictionary<Slot, List<Enchant>> EnchantsBySlot
        {
            get { return _EnchantsBySlot; }
            set { _EnchantsBySlot = value; }
        }

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

        public SimOutput CurrentSimOutput
        {
            get { return _CurrentSimOutput; }
            set { _CurrentSimOutput = value; }
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
    }
}
