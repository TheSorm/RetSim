using RetSim.Data;
using RetSim.Items;
using RetSimDesktop.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static RetSim.Misc.Constants;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for GearPlannerInputField.xaml
    /// </summary>
    public partial class GearPlannerInputField : UserControl
    {

        private static Dictionary<int, byte> SlotNumberToAlternativeSlotNumber = new()
        {
            { EquipmentSlots.Head, 1 },
            { EquipmentSlots.Neck, 2 },
            { EquipmentSlots.Shoulders, 3 },
            { EquipmentSlots.Back, 15 },
            { EquipmentSlots.Chest, 5 },
            { EquipmentSlots.Wrists, 9 },
            { EquipmentSlots.Hands, 10 },
            { EquipmentSlots.Waist, 6 },
            { EquipmentSlots.Legs, 7 },
            { EquipmentSlots.Feet, 8 },
            { EquipmentSlots.Finger1, 11 },
            { EquipmentSlots.Finger2, 12 },
            { EquipmentSlots.Trinket1, 13 },
            { EquipmentSlots.Trinket2, 14 },
            { EquipmentSlots.Relic, 18 },
            { EquipmentSlots.Weapon, 16 },
        };

        private bool disable = false;

        public GearPlannerInputField()
        {
            this.DataContextChanged += (o, e) =>
            {
                if (DataContext is RetSimUIModel retSimUIModel)
                {
                    retSimUIModel.SelectedGear.PropertyChanged += Model_PropertyChanged;

                    Model_PropertyChanged(this, new PropertyChangedEventArgs(""));
                }
            };
            InitializeComponent();
        }

        private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel && retSimUIModel.SelectedGear != null && MediaMetaData.ItemsMetaData != null && !disable)
            {
                disable = true;
                List<byte> encodedGear = new();
                encodedGear.Add(0b00000011); // Header (3)
                encodedGear.Add(0b01000110); // Player Level (70)
                encodedGear.Add(0b00000000); // Number of Talent Bytes (0)


                var equipment = retSimUIModel.SelectedGear.GetEquipment();

                for (int i = 0; i < equipment.PlayerEquipment.Length; i++)
                {
                    if (equipment.PlayerEquipment[i] == null)
                    {
                        continue;
                    }

                    byte slot = SlotNumberToAlternativeSlotNumber[i]; // Item Slot
                    if (equipment.Enchants[i] != null)
                    {
                        slot |= 0b10000000; // Enchanting Bit
                    }
                    encodedGear.Add(slot);

                    byte gemCount = (byte)equipment.PlayerEquipment[i].GetGems().Count;
                    gemCount <<= 5;
                    encodedGear.Add(gemCount);

                    byte[] itemID = BitConverter.GetBytes(equipment.PlayerEquipment[i].ID);
                    encodedGear.Add(itemID[1]);
                    encodedGear.Add(itemID[0]);

                    if (equipment.Enchants[i] != null)
                    {
                        byte[] enchantID = BitConverter.GetBytes(equipment.Enchants[i].ID);
                        encodedGear.Add(enchantID[1]);
                        encodedGear.Add(enchantID[0]);
                    }

                    if (equipment.PlayerEquipment[i].Socket1 != null)
                    {
                        List<Socket> sockets = new(equipment.PlayerEquipment[i].Sockets);
                        byte gemPosition = 0;
                        foreach (var socketColor in MediaMetaData.ItemsMetaData[equipment.PlayerEquipment[i].ID].AlternativeGemOrder)
                        {
                            for (int j = 0; j < sockets.Count; j++)
                            {
                                if (sockets[j].Color == socketColor)
                                {
                                    if (sockets[j].SocketedGem != null)
                                    {
                                        encodedGear.Add((byte)(gemPosition << 5));

                                        byte[] gemID = BitConverter.GetBytes(sockets[j].SocketedGem.ID);
                                        encodedGear.Add(gemID[1]);
                                        encodedGear.Add(gemID[0]);
                                    }
                                    sockets.RemoveAt(j);
                                    break;
                                }
                            }
                            gemPosition++;
                        }
                    }
                }
                EncodedGearTextBox.Text = Convert.ToBase64String(encodedGear.ToArray()).TrimEnd('=').Replace('+', '-').Replace('/', '_');
                disable = false;
            }
        }

        private void EncodedGearTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel && retSimUIModel.SelectedGear != null && MediaMetaData.ItemsMetaData != null && !disable)
            {
                try
                {
                    disable = true;

                    foreach (int alternativeSlot in SlotNumberToAlternativeSlotNumber.Values)
                    {
                        SetSelectedGearAlternativeSlotToID(alternativeSlot, 0, -5);
                    }
                    retSimUIModel.SelectedGear.ClearSelectedGear();

                    string incoming = EncodedGearTextBox.Text.Replace('_', '/').Replace('-', '+');
                    switch (EncodedGearTextBox.Text.Length % 4)
                    {
                        case 2: incoming += "=="; break;
                        case 3: incoming += "="; break;
                    }
                    List<byte> encodedGear = new(Convert.FromBase64String(incoming));

                    encodedGear.RemoveAt(0); // Remove Header (3)
                    encodedGear.RemoveAt(0); // Remove Player Level (70)
                    int numberOfTalentByte = encodedGear[0];
                    encodedGear.RemoveAt(0); // Remove Number of Talent Bytes (0)
                    for (int i = 0; i < numberOfTalentByte; i++)
                    {
                        encodedGear.RemoveAt(0); // Remove Talent Bytes
                    }

                    while (encodedGear.Count > 0)
                    {
                        int slot = encodedGear[0] & 0b01111111;
                        bool isEnchanted = (encodedGear[0] & 0b10000000) != 0;
                        encodedGear.RemoveAt(0);

                        int gemCount = encodedGear[0] >> 5;
                        encodedGear.RemoveAt(0);

                        List<byte> itemIdBytes = encodedGear.GetRange(0, 2);
                        itemIdBytes.Reverse();
                        int itemId = BitConverter.ToUInt16(itemIdBytes.ToArray());
                        encodedGear.RemoveRange(0, 2);

                        int enchantId = -1;
                        if (isEnchanted)
                        {
                            List<byte> enchantIdBytes = encodedGear.GetRange(0, 2);
                            enchantIdBytes.Reverse();
                            enchantId = BitConverter.ToUInt16(enchantIdBytes.ToArray());
                            encodedGear.RemoveRange(0, 2);
                        }

                        SetSelectedGearAlternativeSlotToID(slot, itemId, enchantId);

                        if (gemCount > 0)
                        {
                            List<Socket> itemSockets = new(Items.AllItems[itemId].Sockets);

                            for (int i = 0; i < gemCount; i++)
                            {
                                int gemPosition = encodedGear[0] >> 5;
                                encodedGear.RemoveAt(0);

                                List<byte> gemIdBytes = encodedGear.GetRange(0, 2);
                                gemIdBytes.Reverse();
                                int gemId = BitConverter.ToUInt16(gemIdBytes.ToArray());
                                encodedGear.RemoveRange(0, 2);

                                SocketColor color = MediaMetaData.ItemsMetaData[itemId].AlternativeGemOrder[gemPosition];

                                for (int j = 0; j < itemSockets.Count; j++)
                                {
                                    if (color == itemSockets[j].Color)
                                    {
                                        if (color == SocketColor.Meta && Items.MetaGems.ContainsKey(gemId))
                                        {
                                            itemSockets[j].SocketedGem = Items.MetaGems[gemId];
                                        }
                                        else if (Items.Gems.ContainsKey(gemId))
                                        {
                                            itemSockets[j].SocketedGem = Items.Gems[gemId];
                                        }
                                        itemSockets.RemoveAt(j);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    disable = false;
                }
                catch (Exception ex)
                {
                    disable = false;
                    retSimUIModel.SelectedGear.OnPropertyChanged("");
                }
            }
        }

        private void SetSelectedGearAlternativeSlotToID(int slot, int id, int enchantId)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                switch (slot)
                {
                    case 1:
                        if (retSimUIModel.AllGear.ContainsKey(id))
                            retSimUIModel.SelectedGear.SelectedHead = retSimUIModel.AllGear[id];
                        if (Items.Enchants.ContainsKey(enchantId))
                            retSimUIModel.SelectedGear.HeadEnchant = Items.Enchants[enchantId];
                        else
                        {
                            retSimUIModel.SelectedGear.HeadEnchant = retSimUIModel.EnchantsBySlot[Slot.Head][0];
                        }

                        break;
                    case 2:
                        if (retSimUIModel.AllGear.ContainsKey(id))
                        {
                            retSimUIModel.SelectedGear.SelectedNeck = retSimUIModel.AllGear[id];
                        }
                        break;
                    case 3:
                        if (retSimUIModel.AllGear.ContainsKey(id))
                            retSimUIModel.SelectedGear.SelectedShoulders = retSimUIModel.AllGear[id];
                        if (Items.Enchants.ContainsKey(enchantId))
                            retSimUIModel.SelectedGear.ShouldersEnchant = Items.Enchants[enchantId];
                        else
                        {
                            retSimUIModel.SelectedGear.ShouldersEnchant = retSimUIModel.EnchantsBySlot[Slot.Shoulders][0];
                        }

                        break;
                    case 5:
                        if (retSimUIModel.AllGear.ContainsKey(id))
                            retSimUIModel.SelectedGear.SelectedChest = retSimUIModel.AllGear[id];
                        if (Items.Enchants.ContainsKey(enchantId))
                            retSimUIModel.SelectedGear.ChestEnchant = Items.Enchants[enchantId];
                        else
                        {
                            retSimUIModel.SelectedGear.ChestEnchant = retSimUIModel.EnchantsBySlot[Slot.Chest][0];
                        }

                        break;
                    case 6:
                        if (retSimUIModel.AllGear.ContainsKey(id))
                            retSimUIModel.SelectedGear.SelectedWaist = retSimUIModel.AllGear[id];
                        break;
                    case 7:
                        if (retSimUIModel.AllGear.ContainsKey(id))
                            retSimUIModel.SelectedGear.SelectedLegs = retSimUIModel.AllGear[id];
                        if (Items.Enchants.ContainsKey(enchantId))
                            retSimUIModel.SelectedGear.LegsEnchant = Items.Enchants[enchantId];
                        else
                        {
                            retSimUIModel.SelectedGear.LegsEnchant = retSimUIModel.EnchantsBySlot[Slot.Legs][0];
                        }

                        break;
                    case 8:
                        if (retSimUIModel.AllGear.ContainsKey(id))
                            retSimUIModel.SelectedGear.SelectedFeet = retSimUIModel.AllGear[id];
                        if (Items.Enchants.ContainsKey(enchantId))
                            retSimUIModel.SelectedGear.FeetEnchant = Items.Enchants[enchantId];
                        else
                        {
                            retSimUIModel.SelectedGear.FeetEnchant = retSimUIModel.EnchantsBySlot[Slot.Feet][0];
                        }

                        break;
                    case 9:
                        if (retSimUIModel.AllGear.ContainsKey(id))
                            retSimUIModel.SelectedGear.SelectedWrists = retSimUIModel.AllGear[id];
                        if (Items.Enchants.ContainsKey(enchantId))
                            retSimUIModel.SelectedGear.WristsEnchant = Items.Enchants[enchantId];
                        else
                        {
                            retSimUIModel.SelectedGear.WristsEnchant = retSimUIModel.EnchantsBySlot[Slot.Wrists][0];
                        }

                        break;
                    case 10:
                        if (retSimUIModel.AllGear.ContainsKey(id))
                            retSimUIModel.SelectedGear.SelectedHands = retSimUIModel.AllGear[id];
                        if (Items.Enchants.ContainsKey(enchantId))
                            retSimUIModel.SelectedGear.HandsEnchant = Items.Enchants[enchantId];
                        else
                        {
                            retSimUIModel.SelectedGear.HandsEnchant = retSimUIModel.EnchantsBySlot[Slot.Hands][0];
                        }

                        break;
                    case 11:
                        if (retSimUIModel.AllGear.ContainsKey(id))
                            retSimUIModel.SelectedGear.SelectedFinger1 = retSimUIModel.AllGear[id];
                        if (Items.Enchants.ContainsKey(enchantId))
                            retSimUIModel.SelectedGear.Finger1Enchant = Items.Enchants[enchantId];
                        else
                        {
                            retSimUIModel.SelectedGear.Finger1Enchant = retSimUIModel.EnchantsBySlot[Slot.Finger][0];
                        }

                        break;
                    case 12:
                        if (retSimUIModel.AllGear.ContainsKey(id))
                            retSimUIModel.SelectedGear.SelectedFinger2 = retSimUIModel.AllGear[id];
                        if (Items.Enchants.ContainsKey(enchantId))
                            retSimUIModel.SelectedGear.Finger2Enchant = Items.Enchants[enchantId];
                        else
                        {
                            retSimUIModel.SelectedGear.Finger2Enchant = retSimUIModel.EnchantsBySlot[Slot.Finger][0];
                        }

                        break;
                    case 13:
                        if (retSimUIModel.AllGear.ContainsKey(id))
                            retSimUIModel.SelectedGear.SelectedTrinket1 = retSimUIModel.AllGear[id];
                        break;
                    case 14:
                        if (retSimUIModel.AllGear.ContainsKey(id))
                            retSimUIModel.SelectedGear.SelectedTrinket2 = retSimUIModel.AllGear[id];
                        break;
                    case 15:
                        if (retSimUIModel.AllGear.ContainsKey(id))
                            retSimUIModel.SelectedGear.SelectedBack = retSimUIModel.AllGear[id];
                        if (Items.Enchants.ContainsKey(enchantId))
                            retSimUIModel.SelectedGear.BackEnchant = Items.Enchants[enchantId];
                        else
                        {
                            retSimUIModel.SelectedGear.BackEnchant = retSimUIModel.EnchantsBySlot[Slot.Back][0];
                        }

                        break;
                    case 16:
                        if (retSimUIModel.AllWeapons.ContainsKey(id))
                            retSimUIModel.SelectedGear.SelectedWeapon = retSimUIModel.AllWeapons[id];
                        if (Items.Enchants.ContainsKey(enchantId))
                            retSimUIModel.SelectedGear.WeaponEnchant = Items.Enchants[enchantId];
                        else
                        {
                            retSimUIModel.SelectedGear.WeaponEnchant = retSimUIModel.EnchantsBySlot[Slot.Weapon][0];
                        }

                        break;
                    case 18:
                        if (retSimUIModel.AllGear.ContainsKey(id))
                            retSimUIModel.SelectedGear.SelectedRelic = retSimUIModel.AllGear[id];
                        break;
                }
            }
        }

        private void EncodedGearTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                // Enable copy/paste and selection of all text.
                case Key.C:
                case Key.V:
                case Key.A:
                    if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                        return;
                    break;

                // Enable keyboard navigation/selection.
                case Key.Left:
                case Key.Up:
                case Key.Right:
                case Key.Down:
                case Key.PageUp:
                case Key.PageDown:
                case Key.Home:
                case Key.End:
                    return;
            }
            e.Handled = true;
        }


        private void SelectAll(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textbox)
            {
                textbox.SelectAll();
            }
        }



        private void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBox textbox)
            {
                if (!textbox.IsKeyboardFocusWithin)
                {
                    e.Handled = true;
                    textbox.Focus();
                }
            }
        }
    }
}
