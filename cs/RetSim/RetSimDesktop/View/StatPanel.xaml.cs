using RetSim.Simulation;
using RetSim.Simulation.Tactics;
using RetSim.Spells;
using RetSim.Units.Enemy;
using RetSim.Units.Player;
using RetSim.Units.Player.Static;
using RetSim.Units.UnitStats;
using RetSimDesktop.Model;
using RetSimDesktop.ViewModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for StatPanel.xaml
    /// </summary>
    public partial class StatPanel : UserControl
    {
        public StatPanel()
        {
            this.DataContextChanged += (o, e) =>
            {
                if (DataContext is RetSimUIModel retSimUIModel)
                {
                    retSimUIModel.SelectedGear.PropertyChanged += Model_PropertyChanged;
                    retSimUIModel.SelectedTalents.PropertyChanged += Model_PropertyChanged;

                    Model_PropertyChanged(this, new PropertyChangedEventArgs(""));
                }
            };
            InitializeComponent();
        }

        private void Model_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
                var player = new Player("Brave Hero", RetSim.Data.Collections.Races["Human"], SelectedGear.GetEquipment(retSimUIModel), SelectedTalents.GetTalentList(retSimUIModel));
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
}
