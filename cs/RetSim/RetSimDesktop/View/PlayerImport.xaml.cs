using RetSim.Data;
using RetSim.Items;
using RetSim.Misc;
using RetSim.Simulation;
using RetSim.Simulation.CombatLogEntries;
using RetSimDesktop.Model;
using RetSimDesktop.ViewModel;
using ScottPlot;
using ScottPlot.Plottable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for PlayerImport.xaml
    /// </summary>
    public partial class PlayerImport : UserControl
    {
        public PlayerImport()
        {
            InitializeComponent();
        }

        private void LoadGems(EquippableItem item, ItemData data, int slot)
        {
            var gem = data.Gems[slot];
            var socket = item.Sockets[slot];

            if (gem <= 0)
                return;

            item.Sockets[slot].SocketedGem = Items.Gems.ContainsKey(gem) ? Items.Gems[gem] : Items.MetaGems[gem];
        }
        private void LoadItemData(Slot slot, PlayerImportData data, bool secondSlot = false)
        {
            var displayItem = GetDisplayFromSlot(slot, secondSlot);
            var itemData = GetImportDataFromSlot(slot, data, secondSlot);

            if (itemData == null)
            {
                displayItem.Item = null;
                return;
            }

            if (displayItem == null)
                return;


            var newItem = GetItemDataFromSlot(slot, itemData.Id); ;

            LoadGems(newItem, itemData, 0);
            LoadGems(newItem, itemData, 1);
            LoadGems(newItem, itemData, 2);

            if (newItem != displayItem.Item)
                displayItem.Item = newItem;

            OverwriteSlot(slot, newItem, secondSlot);
        }

        /*
         * This is ugly as hell, i do not know how to properly update the image displayed by the PlayerPanel
         * Someone with more know how to could fix this easily and remove the function completely.
         */
        private void OverwriteSlot(Slot slot, EquippableItem newItem, bool second = false)
        {
            var uiContext = DataContext as RetSimUIModel;
            var gear = uiContext.SelectedGear;
            DisplayGear replacement = new() { Item = newItem, EnabledForGearSim = true, DPS = 0 };

            switch (slot)
            {
                case Slot.Head:
                    gear.SelectedHead = replacement;
                    break;
                case Slot.Neck:
                    gear.SelectedNeck = replacement;
                    break;
                case Slot.Shoulders:
                    gear.SelectedShoulders = replacement;
                    break;
                case Slot.Back:
                    gear.SelectedBack = replacement;
                    break;
                case Slot.Chest:
                    gear.SelectedChest = replacement;
                    break;
                case Slot.Wrists:
                    gear.SelectedWrists = replacement;
                    break;
                case Slot.Hands:
                    gear.SelectedHands = replacement;
                    break;
                case Slot.Waist:
                    gear.SelectedWaist = replacement;
                    break;
                case Slot.Legs:
                    gear.SelectedLegs = replacement;
                    break;
                case Slot.Feet:
                    gear.SelectedFeet = replacement;
                    break;
                case Slot.Finger:
                    if (second) gear.SelectedFinger2 = replacement;
                    else gear.SelectedFinger1 = replacement;
                    break;
                case Slot.Trinket:
                    if (second) gear.SelectedTrinket2 = replacement;
                    else gear.SelectedTrinket1 = replacement;
                    break;
                case Slot.Relic:
                    gear.SelectedRelic = replacement;
                    break;
                case Slot.Weapon:
                    gear.SelectedWeapon = replacement;
                    break;
            }
        }

        #region Slot Mappers
        private ItemData GetImportDataFromSlot(Slot slot, PlayerImportData data, bool second = false)
        {
            var gear = data.SelectedItems;

            switch (slot)
            {
                case Slot.Head:
                    return gear.Head;
                case Slot.Neck:
                    return gear.Neck;
                case Slot.Shoulders:
                    return gear.Shoulders;
                case Slot.Back:
                    return gear.Back;
                case Slot.Chest:
                    return gear.Chest;
                case Slot.Wrists:
                    return gear.Bracer;
                case Slot.Hands:
                    return gear.Gloves;
                case Slot.Waist:
                    return gear.Belt;
                case Slot.Legs:
                    return gear.Legs;
                case Slot.Feet:
                    return gear.Boots;
                case Slot.Finger:
                    return second ? gear.Ring2 : gear.Ring1;
                case Slot.Trinket:
                    return second ? gear.Trinket2 : gear.Trinket1;
                case Slot.Relic:
                    return gear.Wand;
                case Slot.Weapon:
                    return gear.Mainhand;
            }

            throw new Exception("Unknown slot");
        }

        private EquippableItem GetItemDataFromSlot(Slot slot, int id)
        {

            switch (slot)
            {
                case Slot.Head:
                    return Items.Heads[id];
                case Slot.Neck:
                    return Items.Necks[id];
                case Slot.Shoulders:
                    return Items.Shoulders[id];
                case Slot.Back:
                    return Items.Cloaks[id];
                case Slot.Chest:
                    return Items.Chests[id];
                case Slot.Wrists:
                    return Items.Wrists[id];
                case Slot.Hands:
                    return Items.Hands[id];
                case Slot.Waist:
                    return Items.Waists[id];
                case Slot.Legs:
                    return Items.Legs[id];
                case Slot.Feet:
                    return Items.Feet[id];
                case Slot.Finger:
                    return Items.Fingers[id];
                case Slot.Trinket:
                    return Items.Trinkets[id];
                case Slot.Relic:
                    return Items.Relics[id];
                case Slot.Weapon:
                    return Items.Weapons[id];
            }

            throw new Exception("Unknown slot");
        }

        private DisplayGear GetDisplayFromSlot(Slot slot, bool second = false)
        {
            var uiContext = DataContext as RetSimUIModel;
            var gear = uiContext.SelectedGear;

            switch (slot)
            {
                case Slot.Head: 
                    return gear.SelectedHead;
                case Slot.Neck:
                    return gear.SelectedNeck;
                case Slot.Shoulders:
                    return gear.SelectedShoulders;
                case Slot.Back:
                    return gear.SelectedBack;
                case Slot.Chest:
                    return gear.SelectedChest;
                case Slot.Wrists:
                    return gear.SelectedWrists;
                case Slot.Hands:
                    return gear.SelectedHands;
                case Slot.Waist:
                    return gear.SelectedWaist;
                case Slot.Legs:
                    return gear.SelectedLegs;
                case Slot.Feet:
                    return gear.SelectedFeet;
                case Slot.Finger:
                    return second ? gear.SelectedFinger2 : gear.SelectedFinger1;
                case Slot.Trinket:
                    return second ? gear.SelectedTrinket2 : gear.SelectedTrinket1;
                case Slot.Relic:
                    return gear.SelectedRelic;
                case Slot.Weapon:
                    return gear.SelectedWeapon;
            }

            throw new Exception("Unknown slot");
        }
        #endregion

        private void Import_Click(object sender, RoutedEventArgs e)
        {
            var uiContext = DataContext as RetSimUIModel;
            var gear = uiContext.SelectedGear;

            var importJson = importText.Text;
            var playerImportData = PlayerImportData.FromJson(importJson);

            LoadItemData(Slot.Head, playerImportData);
            LoadItemData(Slot.Shoulders, playerImportData);
            LoadItemData(Slot.Neck, playerImportData);
            LoadItemData(Slot.Back, playerImportData);
            LoadItemData(Slot.Chest, playerImportData);
            LoadItemData(Slot.Wrists, playerImportData);
            LoadItemData(Slot.Hands, playerImportData);
            LoadItemData(Slot.Waist, playerImportData);
            LoadItemData(Slot.Legs, playerImportData);
            LoadItemData(Slot.Feet, playerImportData);
            LoadItemData(Slot.Relic, playerImportData);
            LoadItemData(Slot.Finger, playerImportData);
            LoadItemData(Slot.Finger, playerImportData, true);
            LoadItemData(Slot.Trinket, playerImportData);
            LoadItemData(Slot.Trinket, playerImportData, true);
            LoadItemData(Slot.Weapon, playerImportData);         
        }
    }
}
