using RetSim.Items;
using RetSim.Units.Player.Static;
using RetSimDesktop.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using static RetSim.Data.Items;

namespace RetSimDesktop.Model
{
    public class SelectedGear : INotifyPropertyChanged
    {
        private DisplayGear? selectedHead = new() { Item = AllItems[32461], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedNeck = new() { Item = AllItems[30022], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedShoulders = new() { Item = AllItems[29075], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedBack = new() { Item = AllItems[30098], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedChest = new() { Item = AllItems[30129], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedWrists = new() { Item = AllItems[28795], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedHands = new() { Item = AllItems[29947], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedWaist = new() { Item = AllItems[30032], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedLegs = new() { Item = AllItems[30257], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedFeet = new() { Item = AllItems[29951], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedFinger1 = new() { Item = AllItems[30834], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedFinger2 = new() { Item = AllItems[28730], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedTrinket1 = new() { Item = AllItems[29383], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedTrinket2 = new() { Item = AllItems[28830], DPS = 0, EnabledForGearSim = true };
        private DisplayGear? selectedRelic = new() { Item = AllItems[27484], DPS = 0, EnabledForGearSim = true };
        private DisplayWeapon? selectedWeapon = new() { Weapon = Weapons[29993], DPS = 0, EnabledForGearSim = true };

        private Enchant? headEnchant = Enchants[35452];
        private Enchant? shouldersEnchant = Enchants[35417];
        private Enchant? backEnchant = Enchants[34004];
        private Enchant? chestEnchant = Enchants[27960];
        private Enchant? wristsEnchant = Enchants[27899];
        private Enchant? handsEnchant = Enchants[33995];
        private Enchant? legsEnchant = Enchants[35490];
        private Enchant? feetEnchant = Enchants[27951];
        private Enchant? finger1Enchant = null;
        private Enchant? finger2Enchant = null;
        private Enchant? weaponEnchant = Enchants[27984];

        public DisplayGear? SelectedHead
        {
            get { return selectedHead; }
            set
            {
                selectedHead = value;
                OnPropertyChanged(nameof(SelectedHead));
            }
        }

        public DisplayGear? SelectedNeck
        {
            get { return selectedNeck; }
            set
            {
                selectedNeck = value;
                OnPropertyChanged(nameof(SelectedNeck));
            }
        }

        public DisplayGear? SelectedShoulders
        {
            get { return selectedShoulders; }
            set
            {
                selectedShoulders = value;
                OnPropertyChanged(nameof(SelectedShoulders));
            }
        }

        public DisplayGear? SelectedBack
        {
            get { return selectedBack; }
            set
            {
                selectedBack = value;
                OnPropertyChanged(nameof(SelectedBack));
            }
        }

        public DisplayGear? SelectedChest
        {
            get { return selectedChest; }
            set
            {
                selectedChest = value;
                OnPropertyChanged(nameof(SelectedChest));
            }
        }

        public DisplayGear? SelectedWrists
        {
            get { return selectedWrists; }
            set
            {
                selectedWrists = value;
                OnPropertyChanged(nameof(SelectedWrists));
            }
        }

        public DisplayGear? SelectedHands
        {
            get { return selectedHands; }
            set
            {
                selectedHands = value;
                OnPropertyChanged(nameof(SelectedHands));
            }
        }

        public DisplayGear? SelectedWaist
        {
            get { return selectedWaist; }
            set
            {
                selectedWaist = value;
                OnPropertyChanged(nameof(SelectedWaist));
            }
        }

        public DisplayGear? SelectedLegs
        {
            get { return selectedLegs; }
            set
            {
                selectedLegs = value;
                OnPropertyChanged(nameof(SelectedLegs));
            }
        }

        public DisplayGear? SelectedFeet
        {
            get { return selectedFeet; }
            set
            {
                selectedFeet = value;
                OnPropertyChanged(nameof(SelectedFeet));
            }
        }

        public DisplayGear? SelectedFinger1
        {
            get { return selectedFinger1; }
            set
            {
                selectedFinger1 = value;
                OnPropertyChanged(nameof(SelectedFinger1));
            }
        }

        public DisplayGear? SelectedFinger2
        {
            get { return selectedFinger2; }
            set
            {
                selectedFinger2 = value;
                OnPropertyChanged(nameof(SelectedFinger2));
            }
        }

        public DisplayGear? SelectedTrinket1
        {
            get { return selectedTrinket1; }
            set
            {
                selectedTrinket1 = value;
                OnPropertyChanged(nameof(SelectedTrinket1));
            }
        }

        public DisplayGear? SelectedTrinket2
        {
            get { return selectedTrinket2; }
            set
            {
                selectedTrinket2 = value;
                OnPropertyChanged(nameof(SelectedTrinket2));
            }
        }
        public DisplayGear? SelectedRelic
        {
            get { return selectedRelic; }
            set
            {
                selectedRelic = value;
                OnPropertyChanged(nameof(SelectedRelic));
            }
        }

        public DisplayWeapon? SelectedWeapon
        {
            get { return selectedWeapon; }
            set
            {
                selectedWeapon = value;
                OnPropertyChanged(nameof(SelectedWeapon));
            }
        }

        public Enchant? HeadEnchant { get { return headEnchant; } set { headEnchant = value; OnPropertyChanged(nameof(HeadEnchant)); } }
        public Enchant? ShouldersEnchant { get { return shouldersEnchant; } set { shouldersEnchant = value; OnPropertyChanged(nameof(ShouldersEnchant)); } }
        public Enchant? BackEnchant { get { return backEnchant; } set { backEnchant = value; OnPropertyChanged(nameof(BackEnchant)); } }
        public Enchant? ChestEnchant { get { return chestEnchant; } set { chestEnchant = value; OnPropertyChanged(nameof(ChestEnchant)); } }
        public Enchant? WristsEnchant { get { return wristsEnchant; } set { wristsEnchant = value; OnPropertyChanged(nameof(WristsEnchant)); } }
        public Enchant? HandsEnchant { get { return handsEnchant; } set { handsEnchant = value; OnPropertyChanged(nameof(HandsEnchant)); } }
        public Enchant? LegsEnchant { get { return legsEnchant; } set { legsEnchant = value; OnPropertyChanged(nameof(LegsEnchant)); } }
        public Enchant? FeetEnchant { get { return feetEnchant; } set { feetEnchant = value; OnPropertyChanged(nameof(FeetEnchant)); } }
        public Enchant? Finger1Enchant { get { return finger1Enchant; } set { finger1Enchant = value; OnPropertyChanged(nameof(Finger1Enchant)); } }
        public Enchant? Finger2Enchant { get { return finger2Enchant; } set { finger2Enchant = value; OnPropertyChanged(nameof(Finger2Enchant)); } }
        public Enchant? WeaponEnchant { get { return weaponEnchant; } set { weaponEnchant = value; OnPropertyChanged(nameof(WeaponEnchant)); } }


        public static Equipment GetEquipment(RetSimUIModel retSimUIModel)
        {
            return new()
            {
                Head = retSimUIModel.SelectedGear.SelectedHead.Item,
                Neck = retSimUIModel.SelectedGear.SelectedNeck.Item,
                Shoulders = retSimUIModel.SelectedGear.SelectedShoulders.Item,
                Back = retSimUIModel.SelectedGear.SelectedBack.Item,
                Chest = retSimUIModel.SelectedGear.SelectedChest.Item,
                Wrists = retSimUIModel.SelectedGear.SelectedWrists.Item,
                Hands = retSimUIModel.SelectedGear.SelectedHands.Item,
                Waist = retSimUIModel.SelectedGear.SelectedWaist.Item,
                Legs = retSimUIModel.SelectedGear.SelectedLegs.Item,
                Feet = retSimUIModel.SelectedGear.SelectedFeet.Item,
                Finger1 = retSimUIModel.SelectedGear.SelectedFinger1.Item,
                Finger2 = retSimUIModel.SelectedGear.SelectedFinger2.Item,
                Trinket1 = retSimUIModel.SelectedGear.SelectedTrinket1.Item,
                Trinket2 = retSimUIModel.SelectedGear.SelectedTrinket2.Item,
                Relic = retSimUIModel.SelectedGear.SelectedRelic.Item,
                Weapon = retSimUIModel.SelectedGear.SelectedWeapon.Weapon,

                HeadEnchant = retSimUIModel.SelectedGear.HeadEnchant,
                ShouldersEnchant = retSimUIModel.SelectedGear.ShouldersEnchant,
                BackEnchant = retSimUIModel.SelectedGear.BackEnchant,
                ChestEnchant = retSimUIModel.SelectedGear.ChestEnchant,
                WristsEnchant = retSimUIModel.SelectedGear.WristsEnchant,
                HandsEnchant = retSimUIModel.SelectedGear.HandsEnchant,
                LegsEnchant = retSimUIModel.SelectedGear.LegsEnchant,
                FeetEnchant = retSimUIModel.SelectedGear.FeetEnchant,
                Finger1Enchant = retSimUIModel.SelectedGear.Finger1Enchant,
                Finger2Enchant = retSimUIModel.SelectedGear.Finger2Enchant,
                WeaponEnchant = retSimUIModel.SelectedGear.WeaponEnchant,
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SelectedGearJsonConverter : JsonConverter<SelectedGear>
    {

        public override SelectedGear? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Dictionary<string, int> properties = new();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Not a PropertyName: " + reader.TokenType);
                }

                string propertyName = reader.GetString();

                reader.Read();

                properties[propertyName] = reader.GetInt32();
            }

            SelectedGear result = new();

            if (properties.ContainsKey("SelectedHead"))
            {
                result.SelectedHead = new() { Item = AllItems[properties["SelectedHead"]], DPS = 0, EnabledForGearSim = true };
            }
            if (properties.ContainsKey("SelectedNeck"))
            {
                result.SelectedNeck = new() { Item = AllItems[properties["SelectedNeck"]], DPS = 0, EnabledForGearSim = true };
            }
            if (properties.ContainsKey("SelectedShoulders"))
            {
                result.SelectedShoulders = new() { Item = AllItems[properties["SelectedShoulders"]], DPS = 0, EnabledForGearSim = true };
            }
            if (properties.ContainsKey("SelectedBack"))
            {
                result.SelectedBack = new() { Item = AllItems[properties["SelectedBack"]], DPS = 0, EnabledForGearSim = true };
            }
            if (properties.ContainsKey("SelectedChest"))
            {
                result.SelectedChest = new() { Item = AllItems[properties["SelectedChest"]], DPS = 0, EnabledForGearSim = true };
            }
            if (properties.ContainsKey("SelectedWrists"))
            {
                result.SelectedWrists = new() { Item = AllItems[properties["SelectedWrists"]], DPS = 0, EnabledForGearSim = true };
            }
            if (properties.ContainsKey("SelectedHands"))
            {
                result.SelectedHands = new() { Item = AllItems[properties["SelectedHands"]], DPS = 0, EnabledForGearSim = true };
            }
            if (properties.ContainsKey("SelectedWaist"))
            {
                result.SelectedWaist = new() { Item = AllItems[properties["SelectedWaist"]], DPS = 0, EnabledForGearSim = true };
            }
            if (properties.ContainsKey("SelectedLegs"))
            {
                result.SelectedLegs = new() { Item = AllItems[properties["SelectedLegs"]], DPS = 0, EnabledForGearSim = true };
            }
            if (properties.ContainsKey("SelectedFeet"))
            {
                result.SelectedFeet = new() { Item = AllItems[properties["SelectedFeet"]], DPS = 0, EnabledForGearSim = true };
            }
            if (properties.ContainsKey("SelectedFinger1"))
            {
                result.SelectedFinger1 = new() { Item = AllItems[properties["SelectedFinger1"]], DPS = 0, EnabledForGearSim = true };
            }
            if (properties.ContainsKey("SelectedFinger2"))
            {
                result.SelectedFinger2 = new() { Item = AllItems[properties["SelectedFinger2"]], DPS = 0, EnabledForGearSim = true };
            }
            if (properties.ContainsKey("SelectedTrinket1"))
            {
                result.SelectedTrinket1 = new() { Item = AllItems[properties["SelectedTrinket1"]], DPS = 0, EnabledForGearSim = true };
            }
            if (properties.ContainsKey("SelectedTrinket2"))
            {
                result.SelectedTrinket2 = new() { Item = AllItems[properties["SelectedTrinket2"]], DPS = 0, EnabledForGearSim = true };
            }
            if (properties.ContainsKey("SelectedRelic"))
            {
                result.SelectedRelic = new() { Item = AllItems[properties["SelectedRelic"]], DPS = 0, EnabledForGearSim = true };
            }
            if (properties.ContainsKey("SelectedWeapon"))
            {
                result.SelectedWeapon = new() { Weapon = Weapons[properties["SelectedWeapon"]], DPS = 0, EnabledForGearSim = true };
            }

            if (properties.ContainsKey("HeadEnchant"))
            {
                result.HeadEnchant = Enchants[properties["HeadEnchant"]];
            }
            if (properties.ContainsKey("ShouldersEnchant"))
            {
                result.ShouldersEnchant = Enchants[properties["ShouldersEnchant"]];
            }
            if (properties.ContainsKey("BackEnchant"))
            {
                result.BackEnchant = Enchants[properties["BackEnchant"]];
            }
            if (properties.ContainsKey("ChestEnchant"))
            {
                result.ChestEnchant = Enchants[properties["ChestEnchant"]];
            }
            if (properties.ContainsKey("WristsEnchant"))
            {
                result.WristsEnchant = Enchants[properties["WristsEnchant"]];
            }
            if (properties.ContainsKey("HandsEnchant"))
            {
                result.HandsEnchant = Enchants[properties["HandsEnchant"]];
            }
            if (properties.ContainsKey("LegsEnchant"))
            {
                result.LegsEnchant = Enchants[properties["LegsEnchant"]];
            }
            if (properties.ContainsKey("FeetEnchant"))
            {
                result.FeetEnchant = Enchants[properties["FeetEnchant"]];
            }
            if (properties.ContainsKey("Finger1Enchant"))
            {
                result.Finger1Enchant = Enchants[properties["Finger1Enchant"]];
            }
            if (properties.ContainsKey("Finger2Enchant"))
            {
                result.Finger2Enchant = Enchants[properties["Finger2Enchant"]];
            }
            if (properties.ContainsKey("WeaponEnchant"))
            {
                result.WeaponEnchant = Enchants[properties["WeaponEnchant"]];
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, SelectedGear value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            if (value.SelectedHead != null)
            {
                writer.WriteNumber("SelectedHead", value.SelectedHead.Item.ID);
            }
            if (value.SelectedNeck != null)
            {
                writer.WriteNumber("SelectedNeck", value.SelectedNeck.Item.ID);
            }
            if (value.SelectedShoulders != null)
            {
                writer.WriteNumber("SelectedShoulders", value.SelectedShoulders.Item.ID);
            }
            if (value.SelectedBack != null)
            {
                writer.WriteNumber("SelectedBack", value.SelectedBack.Item.ID);
            }
            if (value.SelectedChest != null)
            {
                writer.WriteNumber("SelectedChest", value.SelectedChest.Item.ID);
            }
            if (value.SelectedWrists != null)
            {
                writer.WriteNumber("SelectedWrists", value.SelectedWrists.Item.ID);
            }
            if (value.SelectedHands != null)
            {
                writer.WriteNumber("SelectedHands", value.SelectedHands.Item.ID);
            }
            if (value.SelectedWaist != null)
            {
                writer.WriteNumber("SelectedWaist", value.SelectedWaist.Item.ID);
            }
            if (value.SelectedLegs != null)
            {
                writer.WriteNumber("SelectedLegs", value.SelectedLegs.Item.ID);
            }
            if (value.SelectedFeet != null)
            {
                writer.WriteNumber("SelectedFeet", value.SelectedFeet.Item.ID);
            }
            if (value.SelectedFinger1 != null)
            {
                writer.WriteNumber("SelectedFinger1", value.SelectedFinger1.Item.ID);
            }
            if (value.SelectedFinger2 != null)
            {
                writer.WriteNumber("SelectedFinger2", value.SelectedFinger2.Item.ID);
            }
            if (value.SelectedTrinket1 != null)
            {
                writer.WriteNumber("SelectedTrinket1", value.SelectedTrinket1.Item.ID);
            }
            if (value.SelectedTrinket2 != null)
            {
                writer.WriteNumber("SelectedTrinket2", value.SelectedTrinket2.Item.ID);
            }
            if (value.SelectedRelic != null)
            {
                writer.WriteNumber("SelectedRelic", value.SelectedRelic.Item.ID);
            }
            if (value.SelectedWeapon != null)
            {
                writer.WriteNumber("SelectedWeapon", value.SelectedWeapon.Weapon.ID);
            }

            if (value.HeadEnchant != null)
            {
                writer.WriteNumber("HeadEnchant", value.HeadEnchant.ID);
            }
            if (value.ShouldersEnchant != null)
            {
                writer.WriteNumber("ShouldersEnchant", value.ShouldersEnchant.ID);
            }
            if (value.BackEnchant != null)
            {
                writer.WriteNumber("BackEnchant", value.BackEnchant.ID);
            }
            if (value.ChestEnchant != null)
            {
                writer.WriteNumber("ChestEnchant", value.ChestEnchant.ID);
            }
            if (value.WristsEnchant != null)
            {
                writer.WriteNumber("WristsEnchant", value.WristsEnchant.ID);
            }
            if (value.HandsEnchant != null)
            {
                writer.WriteNumber("HandsEnchant", value.HandsEnchant.ID);
            }
            if (value.LegsEnchant != null)
            {
                writer.WriteNumber("LegsEnchant", value.LegsEnchant.ID);
            }
            if (value.FeetEnchant != null)
            {
                writer.WriteNumber("FeetEnchant", value.FeetEnchant.ID);
            }
            if (value.Finger1Enchant != null)
            {
                writer.WriteNumber("Finger1Enchant", value.Finger1Enchant.ID);
            }
            if (value.Finger2Enchant != null)
            {
                writer.WriteNumber("Finger2Enchant", value.Finger2Enchant.ID);
            }
            if (value.WeaponEnchant != null)
            {
                writer.WriteNumber("WeaponEnchant", value.WeaponEnchant.ID);
            }

            writer.WriteEndObject();
        }
    }
}
