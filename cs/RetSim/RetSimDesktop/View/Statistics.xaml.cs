using RetSim.Misc;
using RetSim.Simulation;
using RetSim.Simulation.CombatLogEntries;
using RetSimDesktop.ViewModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

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

        public List<AuraBreakdownElement> AuraBreakdownMinLog { get; set; }
        public List<AuraBreakdownElement> AuraBreakdownMedianLog { get; set; }
        public List<AuraBreakdownElement> AuraBreakdownMaxLog { get; set; }

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

            AuraBreakdownMinLog = new();
            AuraBreakdownMedianLog = new();
            AuraBreakdownMaxLog = new();

            DamageBreakdownTable.ItemsSource = DamageBreakdownMedianLog;
            AuraBreakdownTable.ItemsSource = AuraBreakdownMedianLog;

            WpfPlot1.Configuration.Zoom = false;
            WpfPlot1.Configuration.Pan = false;
            WpfPlot1.Configuration.DoubleClickBenchmark = false;

            WpfPlot1.RightClicked -= WpfPlot1.DefaultRightClickEvent;
            WpfPlot1.Plot.YAxis.Label("");
            WpfPlot1.Plot.XAxis.Label("DPS");
        }

        private void CurrentSimOutputChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MinCombatLog" || e.PropertyName == "MedianCombatLog" || e.PropertyName == "MaxCombatLog")
            {
                DamageBreakdownTable.Dispatcher.Invoke(() =>
                {
                    if (DataContext is RetSimUIModel retSimUIModel)
                    {
                        retSimUIModel.CurrentSimOutput.MinCombatLog.CreateDamageBreakdown();
                        retSimUIModel.CurrentSimOutput.MedianCombatLog.CreateDamageBreakdown();
                        retSimUIModel.CurrentSimOutput.MaxCombatLog.CreateDamageBreakdown();

                        DamageBreakdownMinLog = CreateDamageBreakdown(retSimUIModel.CurrentSimOutput.MinCombatLog);
                        DamageBreakdownMedianLog = CreateDamageBreakdown(retSimUIModel.CurrentSimOutput.MedianCombatLog);
                        DamageBreakdownMaxLog = CreateDamageBreakdown(retSimUIModel.CurrentSimOutput.MaxCombatLog);

                        retSimUIModel.CurrentSimOutput.MinCombatLog.CreateAuraBreakDown();
                        retSimUIModel.CurrentSimOutput.MedianCombatLog.CreateAuraBreakDown();
                        retSimUIModel.CurrentSimOutput.MaxCombatLog.CreateAuraBreakDown();

                        AuraBreakdownMinLog = CreateAuraBreakdown(retSimUIModel.CurrentSimOutput.MinCombatLog);
                        AuraBreakdownMedianLog = CreateAuraBreakdown(retSimUIModel.CurrentSimOutput.MedianCombatLog);
                        AuraBreakdownMaxLog = CreateAuraBreakdown(retSimUIModel.CurrentSimOutput.MaxCombatLog);

                        DamageBreakdownSelection_SelectionChanged(null, null);
                        AuraBreakdownSelection_SelectionChanged(null, null);
                    }
                });
            }
            else if (e.PropertyName == "DpsResults")
            {
                DamageBreakdownTable.Dispatcher.Invoke(() =>
                {
                    if (DataContext is RetSimUIModel retSimUIModel)
                    {
                        double min = retSimUIModel.CurrentSimOutput.DpsResults[0];
                        double max = retSimUIModel.CurrentSimOutput.DpsResults[^1];
                        (double[] counts, double[] binEdges) = ScottPlot.Statistics.Common.Histogram(retSimUIModel.CurrentSimOutput.DpsResults.ToArray(), min: min, max: max, binSize: 10);
                        double[] leftEdges = binEdges.Take(binEdges.Length - 1).ToArray();

                        WpfPlot1.Plot.Clear();
                        var bar = WpfPlot1.Plot.AddBar(values: counts, positions: leftEdges);
                        bar.BarWidth = 10;

                        WpfPlot1.Plot.YAxis.Label("");
                        WpfPlot1.Plot.XAxis.Label("DPS");
                        WpfPlot1.Plot.SetAxisLimits(yMin: 0);

                        WpfPlot1.Refresh();
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

        private static List<AuraBreakdownElement> CreateAuraBreakdown(CombatLog log)
        {
            List<AuraBreakdownElement> result = new();

            foreach (string s in log.AuraBreakdown.Keys)
            {
                int count = 0;
                float uptime = 0;

                float start = -1;
                foreach (BuffEntry entry in log.AuraBreakdown[s])
                {
                    if (entry.Timestamp < 0)
                    {
                        continue;
                    }

                    if (entry.Type == RetSim.Spells.AuraChangeType.Gain || entry.Type == RetSim.Spells.AuraChangeType.Refresh)
                    {
                        count++;
                    }

                    if (entry.Type == RetSim.Spells.AuraChangeType.Gain && start == -1)
                    {
                        start = entry.Timestamp;
                    }

                    if (entry.Type == RetSim.Spells.AuraChangeType.Fade)
                    {
                        uptime += entry.Timestamp - start;
                        start = -1;
                    }
                }

                if (start != -1)
                {
                    uptime += log.Duration - start;
                }

                if (count == 0 && uptime == 0)
                {
                    continue;
                }

                result.Add(new()
                {
                    AuraName = s,
                    Count = count,
                    Uptime = uptime / 1000,
                    UptimePercentage = (uptime / log.Duration * 100f).Rounded(),
                });
            }
            return result;
        }

        private void DamageBreakdownSelection_SelectionChanged(object? sender, SelectionChangedEventArgs? e)
        {
            if (DamageBreakdownSelection != null && DamageBreakdownSelection.SelectedValue != null)
            {
                var value = DamageBreakdownSelection.SelectedValue.ToString();

                if (value == "Min" && DamageBreakdownMinLog.Count > 0)
                {
                    DamageBreakdownTable.ItemsSource = DamageBreakdownMinLog.GetRange(0, DamageBreakdownMinLog.Count - 1);
                    DamageBreakdownTotalTable.ItemsSource = DamageBreakdownMinLog.GetRange(DamageBreakdownMinLog.Count - 1, 1);
                }
                else if (value == "Median" && DamageBreakdownMedianLog.Count > 0)
                {
                    DamageBreakdownTable.ItemsSource = DamageBreakdownMedianLog.GetRange(0, DamageBreakdownMedianLog.Count - 1);
                    DamageBreakdownTotalTable.ItemsSource = DamageBreakdownMedianLog.GetRange(DamageBreakdownMedianLog.Count - 1, 1);
                }
                else if (value == "Max" && DamageBreakdownMaxLog.Count > 0)
                {
                    DamageBreakdownTable.ItemsSource = DamageBreakdownMaxLog.GetRange(0, DamageBreakdownMaxLog.Count - 1);
                    DamageBreakdownTotalTable.ItemsSource = DamageBreakdownMaxLog.GetRange(DamageBreakdownMaxLog.Count - 1, 1);
                }
                DamageBreakdownTable.Items.Refresh();
                DamageBreakdownDamageColumn.SortDirection = ListSortDirection.Descending;
                DamageBreakdownTable.Items.SortDescriptions.Add(new SortDescription(DamageBreakdownDamageColumn.SortMemberPath, ListSortDirection.Descending));
            }
        }

        private void AuraBreakdownSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AuraBreakdownSelection != null && AuraBreakdownSelection.SelectedValue != null)
            {
                var value = AuraBreakdownSelection.SelectedValue.ToString();

                if (value == "Min" && AuraBreakdownMinLog.Count > 0)
                {
                    AuraBreakdownTable.ItemsSource = AuraBreakdownMinLog;
                }
                else if (value == "Median" && AuraBreakdownMedianLog.Count > 0)
                {
                    AuraBreakdownTable.ItemsSource = AuraBreakdownMedianLog;
                }
                else if (value == "Max" && AuraBreakdownMaxLog.Count > 0)
                {
                    AuraBreakdownTable.ItemsSource = AuraBreakdownMaxLog;
                }
                AuraBreakdownTable.Items.Refresh();
                AuraBreakdownUptimeColumn.SortDirection = ListSortDirection.Descending;
                AuraBreakdownTable.Items.SortDescriptions.Add(new SortDescription(AuraBreakdownUptimeColumn.SortMemberPath, ListSortDirection.Descending));
            }
        }

        private void CombatLogSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is RetSimUIModel retSimUIModel)
            {
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

    public class AuraBreakdownElement
    {
        public string AuraName { get; set; }
        public int Count { get; set; }
        public float Uptime { get; set; }
        public float UptimePercentage { get; set; }
    }
}
