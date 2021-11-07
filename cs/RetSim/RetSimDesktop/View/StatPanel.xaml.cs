using RetSim.Simulation;
using RetSim.Simulation.Tactics;
using RetSim.Spells;
using RetSim.Units.Enemy;
using RetSim.Units.Player;
using RetSim.Units.Player.Static;
using RetSim.Units.UnitStats;
using RetSimDesktop.View;
using RetSimDesktop.ViewModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for StatPanel.xaml
    /// </summary>
    public partial class StatPanel : UserControl
    {
        private Player player;
        public StatPanel()
        {
            this.DataContextChanged += (o, e) =>
            {
                if (DataContext is RetSimUIModel)
                {
                    var viewModel = DataContext;
                    (DataContext as RetSimUIModel).SelectedGear.PropertyChanged += Model_PropertyChanged;
                    (DataContext as RetSimUIModel).SelectedTalents.PropertyChanged += Model_PropertyChanged;

                    Model_PropertyChanged(this, new PropertyChangedEventArgs(""));
                }
            };
            InitializeComponent();
        }


        private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            RetSimUIModel retSimUIModel = DataContext as RetSimUIModel;
            Equipment playerEquipment = new()
            {
                Head = retSimUIModel.SelectedGear.SelectedHead,
                Neck = retSimUIModel.SelectedGear.SelectedNeck,
                Shoulders = retSimUIModel.SelectedGear.SelectedShoulders,
                Back = retSimUIModel.SelectedGear.SelectedBack,
                Chest = retSimUIModel.SelectedGear.SelectedChest,
                Wrists = retSimUIModel.SelectedGear.SelectedWrists,
                Hands = retSimUIModel.SelectedGear.SelectedHands,
                Waist = retSimUIModel.SelectedGear.SelectedWaist,
                Legs = retSimUIModel.SelectedGear.SelectedLegs,
                Feet = retSimUIModel.SelectedGear.SelectedFeet,
                Finger1 = retSimUIModel.SelectedGear.SelectedFinger1,
                Finger2 = retSimUIModel.SelectedGear.SelectedFinger2,
                Trinket1 = retSimUIModel.SelectedGear.SelectedTrinket1,
                Trinket2 = retSimUIModel.SelectedGear.SelectedTrinket2,
                Relic = retSimUIModel.SelectedGear.SelectedRelic,
                Weapon = retSimUIModel.SelectedGear.SelectedWeapon,
            };
            player = new Player("Brave Hero", Races.Human, playerEquipment, SimWorker.GetSelectedTalentList(retSimUIModel));
            FightSimulation fight = new(player, new Enemy("Magtheridon", CreatureType.Demon, ArmorCategory.Warrior), new EliteTactic(), new List<Spell>(), new List<Spell>(), 0, 0);

            Strength.Content = player.Stats[StatName.Strength].Value;
            AttackPower.Content = player.Stats[StatName.AttackPower].Value;
            Agility.Content = player.Stats[StatName.Agility].Value;
            Crit.Content = player.Stats[StatName.CritRating].Value;
            Hit.Content = player.Stats[StatName.HitRating].Value;
            Haste.Content = player.Stats[StatName.HasteRating].Value;
            Expertise.Content = player.Stats[StatName.ExpertiseRating].Value;
            ArmorPenetration.Content = player.Stats[StatName.ArmorPenetration].Value;
            WeaponDamage.Content = player.Stats[StatName.WeaponDamage].Value;
            Stamina.Content = player.Stats[StatName.Stamina].Value;
            Intellect.Content = player.Stats[StatName.Intellect].Value;
            ManaPer5.Content = player.Stats[StatName.ManaPer5].Value;
            SpellPower.Content = player.Stats[StatName.SpellPower].Value;
            SpellCrit.Content = player.Stats[StatName.SpellCritRating].Value;
            SpellHit.Content = player.Stats[StatName.SpellHitRating].Value;
            SpellHaste.Content = player.Stats[StatName.SpellHasteRating].Value;

            CritPercentage.Content = player.Stats[StatName.CritChance].Value.ToString("0.0") + "%";
            HitPercentage.Content = player.Stats[StatName.HitChance].Value.ToString("0.0") + "%";
            HastePercentage.Content = player.Stats[StatName.Haste].Value.ToString("0.0") + "%";
            ExpertisePercentage.Content = player.Stats[StatName.Expertise].Value.ToString("0.0") + "%";
            SpellCritPercentage.Content = player.Stats[StatName.SpellCrit].Value.ToString("0.0") + "%";
            SpellHitPercentage.Content = player.Stats[StatName.SpellHit].Value.ToString("0.0") + "%";
            SpellHastePercentage.Content = player.Stats[StatName.SpellHaste].Value.ToString("0.0") + "%";
        }
    }
}
