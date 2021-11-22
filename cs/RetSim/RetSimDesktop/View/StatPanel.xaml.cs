using RetSim.Data;
using RetSim.Simulation;
using RetSim.Simulation.Tactics;
using RetSim.Spells;
using RetSim.Units.Enemy;
using RetSim.Units.Player;
using RetSim.Units.UnitStats;
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
                var player = new Player("Brave Hero", Collections.Races["Human"], retSimUIModel.SelectedGear.GetEquipment(), retSimUIModel.SelectedTalents.GetTalentList());
                FightSimulation fight = new(player, new Enemy(Collections.Bosses[17]), new EliteTactic(), new List<Spell>(), new List<Spell>(), new List<Spell>(), 0, 0);

                Health.Content = player.Stats[StatName.Health].Value;
                Mana.Content = player.Stats[StatName.Mana].Value;
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

                CritPercentage.Content = player.Stats[StatName.CritChance].Value.ToString("0.0") + "%";
                HitPercentage.Content = player.Stats[StatName.HitChance].Value.ToString("0.0") + "%";
                HastePercentage.Content = player.Stats[StatName.Haste].Value.ToString("0.0") + "%";
                ExpertisePercentage.Content = player.Stats[StatName.Expertise].Value.ToString("0.0") + "%";

            }
        }
    }
}
