using RetSim.Items;
using static RetSim.Data.Spells;
using RetSim.Units.Player.Static;
using RetSimDesktop.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RetSim.Spells;
using RetSim.Simulation;
using RetSim.Units.Player;
using RetSim.Units.Enemy;
using RetSim.Simulation.Tactics;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static BackgroundWorker backgroundWorker;
        public MainWindow()
        {
            var (Weapons, Armor, Sets, Gems, MetaGems) = RetSim.Data.Importer.LoadData();
            RetSim.Data.Items.Initialize(Weapons, Armor, Sets, Gems, MetaGems);
            InitializeComponent();

            RetSimUIModel GM = new();
            DataContext = GM;

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
        }


        private void Gear_Click(object sender, RoutedEventArgs e)
        {
            Settings.Visibility = Visibility.Hidden;
            Statistics.Visibility = Visibility.Hidden;
            GearSelect.Visibility = Visibility.Visible;
            GearButton.IsEnabled = false;
            SettingsButton.IsEnabled = true;
            StatisticsButton.IsEnabled = true;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings.Visibility = Visibility.Visible;
            Statistics.Visibility = Visibility.Hidden;
            GearSelect.Visibility = Visibility.Hidden;
            GearButton.IsEnabled = true;
            SettingsButton.IsEnabled = false;
            StatisticsButton.IsEnabled = true;
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            Settings.Visibility = Visibility.Hidden;
            Statistics.Visibility = Visibility.Visible;
            GearSelect.Visibility = Visibility.Hidden;
            GearButton.IsEnabled = true;
            SettingsButton.IsEnabled = true;
            StatisticsButton.IsEnabled = false;
        }

        private void DpsSimClick(object sender, RoutedEventArgs e)
        {
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync(DataContext);
            }
        }

        static void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            RetSimUIModel retSimUIModel = e.Argument as RetSimUIModel; 
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
            var talents = new List<Talent> { Conviction, Crusade, TwoHandedWeaponSpecialization, SanctityAura, ImprovedSanctityAura, Vengeance, Fanaticism,
                                            SanctifiedSeals, Precision, DivineStrength };
            var buffs = new List<Spell> { WindfuryTotem, GreaterBlessingOfMight, GreaterBlessingOfKings, BattleShout, StrengthOfEarthTotem, GraceOfAirTotem, ManaSpringTotem, UnleashedRage,
                                          GiftOfTheWild, PrayerOfFortitude, PrayerOfSpirit, ArcaneBrilliance, InspiringPresence };
            var debuffs = new List<Spell> { ImprovedSealOfTheCrusader, ImprovedExposeArmor, ImprovedFaerieFire, CurseOfRecklessness, BloodFrenzy, ImprovedCurseOfTheElements, ImprovedShadowBolt, Misery,
                                        ShadowWeaving, ImprovedScorch, ImprovedHuntersMark, ExposeWeakness };

            float overallDPS = 0;
            for (int i = 0; i < 10000; i++)
            {
                FightSimulation fight = new(new Player("Brave Hero", Races.Human, playerEquipment, talents), new Enemy("Magtheridon", CreatureType.Demon, ArmorCategory.Warrior), new EliteTactic(), buffs, debuffs, 180000, 200000);
                fight.Run();
                overallDPS += fight.CombatLog.DPS;
                if(i % 100 == 0)
                {
                    retSimUIModel.CurrentSimOutput.Progress = (int)(i / 10000f * 100);
                    retSimUIModel.CurrentSimOutput.DPS = overallDPS / ((float)i);
                }
            }
            retSimUIModel.CurrentSimOutput.Progress = 100;
            retSimUIModel.CurrentSimOutput.DPS = overallDPS / 10000f;
        }
    }
}
