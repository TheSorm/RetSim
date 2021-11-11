using RetSim.Misc;
using RetSim.Simulation;
using RetSim.Simulation.CombatLogEntries;
using RetSimDesktop.ViewModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;

namespace RetSimDesktop
{
    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : UserControl
    {
        public List<DamageBreakdownElement> DamageBreakdownMinLog { get; set; }
        public List<DamageBreakdownElement> DamageBreakdownMedianLog { get; set; }
        public List<DamageBreakdownElement> DamageBreakdownMaxLog { get; set; }

        public Statistics()
        {
            InitializeComponent();

            this.DataContextChanged += (o, e) =>
            {
                if (DataContext is RetSimUIModel retSimUIModel)
                {
                    retSimUIModel.CurrentSimOutput.PropertyChanged += CurrentSimOutputChanged;
                    CurrentSimOutputChanged(this, new(""));
                }
            };

            DamageBreakdownMinLog = new();
            DamageBreakdownMedianLog = new();
            DamageBreakdownMaxLog = new();

            DamageBreakdownTable.ItemsSource = DamageBreakdownMedianLog;
        }

        private void CurrentSimOutputChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MinCombatLog" || e.PropertyName == "MedianCombatLog" || e.PropertyName == "MaxCombatLog")
            {
                DamageBreakdownTable.Dispatcher.Invoke(() =>
                {
                    if (DataContext is RetSimUIModel retSimUIModel)
                    {
                        DamageBreakdownMinLog = CreateDamageBreakdown(retSimUIModel.CurrentSimOutput.MinCombatLog);
                        DamageBreakdownMedianLog = CreateDamageBreakdown(retSimUIModel.CurrentSimOutput.MedianCombatLog);
                        DamageBreakdownMaxLog = CreateDamageBreakdown(retSimUIModel.CurrentSimOutput.MaxCombatLog);

                        DamageBreakdownSelection_SelectionChanged(null, null);
                    }
                });
            }
        }

        private static List<DamageBreakdownElement> CreateDamageBreakdown(CombatLog log)
        {
            List<DamageBreakdownElement> result = new();
            int totalCasts = 0;
            int totalHits = 0;
            int totalCrits = 0;
            int totalDodges = 0;
            int totalMisses = 0;

            foreach (string s in log.DamageBreakdown.Keys)
            {
                int casts = 0;
                int count = log.DamageBreakdown[s].Count;
                int miss = 0;
                int dodge = 0;
                int crit = 0;

                int damage = 0;

                foreach (DamageEntry entry in log.DamageBreakdown[s])
                {
                    damage += entry.Damage;

                    if (entry.AttackResult == AttackResult.Miss)
                        miss++;

                    if (entry.AttackResult == AttackResult.Dodge)
                        dodge++;

                    if (entry.Crit)
                        crit++;

                    casts++;
                }

                float dps = (float)damage / log.Duration * 1000;
                int hit = count - miss - dodge;
                totalCasts += casts;
                totalHits += hit;
                totalCrits += crit;
                totalDodges += dodge;
                totalMisses += miss;

                result.Add(new()
                {
                    AbilityName = s,
                    Damage = damage,
                    DPS = dps.Rounded(),
                    DPSPercentage = (dps / log.DPS * 100f).Rounded(),
                    Casts = count,
                    Crits = crit,
                    CritPercentage = (crit / ((float)hit) * 100).Rounded(),
                    Hits = hit,
                    HitPercentage = (hit / ((float)count) * 100).Rounded(),
                    Misses = miss,
                    MissPercentage = (miss / ((float)count) * 100).Rounded(),
                    Dodges = dodge,
                    DodgePercentage = (dodge / ((float)count) * 100).Rounded()
                });
            }
            result.Add(new()
            {
                AbilityName = "Total",
                Damage = log.Damage,
                DPS = log.DPS.Rounded(),
                DPSPercentage = 100,
                Casts = totalCasts,
                Crits = totalCrits,
                CritPercentage = (totalCrits / ((float)totalCasts) * 100).Rounded(),
                Hits = totalHits,
                HitPercentage = (totalHits / ((float)totalCasts) * 100).Rounded(),
                Misses = totalMisses,
                MissPercentage = (totalMisses / ((float)totalCasts) * 100).Rounded(),
                Dodges = totalDodges,
                DodgePercentage = (totalDodges / ((float)totalCasts) * 100).Rounded()
            });
            return result;
        }

        private void DamageBreakdownSelection_SelectionChanged(object? sender, SelectionChangedEventArgs? e)
        {
            if (DamageBreakdownSelection != null && DamageBreakdownSelection.SelectedValue != null)
            {
                var value = DamageBreakdownSelection.SelectedValue.ToString();

                if (value == "Min")
                {
                    DamageBreakdownTable.ItemsSource = DamageBreakdownMinLog;
                }
                else if (value == "Median")
                {
                    DamageBreakdownTable.ItemsSource = DamageBreakdownMedianLog;
                }
                else if (value == "Max")
                {
                    DamageBreakdownTable.ItemsSource = DamageBreakdownMaxLog;
                }
                DamageBreakdownTable.Items.Refresh();
            }
        }
        private void CombatLogSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel) { 
                if (CombatLogSelection.SelectedValue != null)
                {
                    var value = CombatLogSelection.SelectedValue.ToString();

                    if (value == "Min")
                    {
                        CombatLogTable.ItemsSource = retSimUIModel.CurrentSimOutput.MinCombatLog.Log;
                    }
                    else if (value == "Median")
                    {
                        CombatLogTable.ItemsSource = retSimUIModel.CurrentSimOutput.MedianCombatLog.Log;
                    }
                    else if (value == "Max")
                    {
                        CombatLogTable.ItemsSource = retSimUIModel.CurrentSimOutput.MaxCombatLog.Log;
                    }
                }
            }
        }
    }

    public class DamageBreakdownElement
    {
        public string AbilityName { get; set; }
        public int Damage { get; set; }
        public float DPS { get; set; }
        public float DPSPercentage { get; set; }
        public int Casts { get; set; }
        public int Crits { get; set; }
        public float CritPercentage { get; set; }
        public int Hits { get; set; }
        public float HitPercentage { get; set; }
        public int Misses { get; set; }
        public float MissPercentage { get; set; }
        public int Dodges { get; set; }
        public float DodgePercentage { get; set; }

    }
}
