using RetSim.Misc;
using RetSim.Simulation;
using RetSim.Simulation.CombatLogEntries;
using RetSimDesktop.ViewModel;
using ScottPlot;
using ScottPlot.Plottable;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

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

            DPSHistogram.Configuration.Zoom = false;
            DPSHistogram.Configuration.Pan = false;
            DPSHistogram.Configuration.DoubleClickBenchmark = false;
            DPSHistogram.Configuration.LeftClickDragPan = false;
            DPSHistogram.Configuration.LockHorizontalAxis = true;
            DPSHistogram.MouseLeftButtonUp += (o, e) =>
            {
                if (DataContext is RetSimUIModel retSimUIModel && retSimUIModel.CurrentSimOutput.DpsResults != null)
                    DPSHistogram.Plot.SetAxisLimits(yMin: 0, xMin: retSimUIModel.CurrentSimOutput.DpsResults[0], xMax: retSimUIModel.CurrentSimOutput.DpsResults[^1]);
            };
            DPSHistogram.SizeChanged += (o, e) =>
            {
                if (DataContext is RetSimUIModel retSimUIModel && retSimUIModel.CurrentSimOutput.DpsResults != null)
                    DPSHistogram.Plot.SetAxisLimits(yMin: 0, xMin: retSimUIModel.CurrentSimOutput.DpsResults[0], xMax: retSimUIModel.CurrentSimOutput.DpsResults[^1]);
            };

            DPSHistogram.RightClicked -= DPSHistogram.DefaultRightClickEvent;
            DPSHistogram.Plot.YAxis.Label("");
            DPSHistogram.Plot.XAxis.Label("DPS");

            DPSGraph.Configuration.Zoom = true;
            DPSGraph.Configuration.Pan = true;
            DPSGraph.ClipToBounds = true;
            DPSGraph.Configuration.DoubleClickBenchmark = false;

            DPSGraph.MouseMove += DPSGraph_MouseMove;
            DPSGraph.MouseLeave += DPSGraph_MouseLeave;

            DPSGraph.Plot.YAxis.Label("Damage");
            DPSGraph.Plot.XAxis.Label("Time");
            DPSGraph.Plot.SetAxisLimits(yMin: 0, xMin: 0);

            DamageChart.Configuration.Zoom = false;
            DamageChart.Configuration.Pan = false;
            DamageChart.Configuration.DoubleClickBenchmark = false;
            DamageChart.RightClicked -= DamageChart.DefaultRightClickEvent;
            DamageChart.Visibility = System.Windows.Visibility.Hidden;

            AuraChart.Configuration.Zoom = false;
            AuraChart.Configuration.Pan = false;
            AuraChart.Configuration.DoubleClickBenchmark = false;
            AuraChart.RightClicked -= AuraChart.DefaultRightClickEvent;
            AuraChart.Visibility = System.Windows.Visibility.Hidden;

        }

        private void CurrentSimOutputChanged(object? sender, PropertyChangedEventArgs e)
        {
            DamageBreakdownTable.Dispatcher.Invoke(() =>
            {
                if (DataContext is RetSimUIModel retSimUIModel)
                {
                    if (e.PropertyName == "MinCombatLog" || e.PropertyName == "MaxCombatLog")
                    {

                        retSimUIModel.CurrentSimOutput.MinCombatLog.CreateDamageBreakdown();
                        retSimUIModel.CurrentSimOutput.MaxCombatLog.CreateDamageBreakdown();

                        DamageBreakdownMinLog = CreateDamageBreakdown(retSimUIModel.CurrentSimOutput.MinCombatLog);
                        DamageBreakdownMaxLog = CreateDamageBreakdown(retSimUIModel.CurrentSimOutput.MaxCombatLog);

                        retSimUIModel.CurrentSimOutput.MinCombatLog.CreateAuraBreakDown();
                        retSimUIModel.CurrentSimOutput.MaxCombatLog.CreateAuraBreakDown();

                        AuraBreakdownMinLog = CreateAuraBreakdown(retSimUIModel.CurrentSimOutput.MinCombatLog);
                        AuraBreakdownMaxLog = CreateAuraBreakdown(retSimUIModel.CurrentSimOutput.MaxCombatLog);

                        DamageBreakdownSelection_SelectionChanged(null, null);
                        AuraBreakdownSelection_SelectionChanged(null, null);

                    }
                    else if (e.PropertyName == "MedianCombatLog")
                    {
                        retSimUIModel.CurrentSimOutput.MedianCombatLog.CreateDamageBreakdown();
                        DamageBreakdownMedianLog = CreateDamageBreakdown(retSimUIModel.CurrentSimOutput.MedianCombatLog);
                        retSimUIModel.CurrentSimOutput.MedianCombatLog.CreateAuraBreakDown();
                        AuraBreakdownMedianLog = CreateAuraBreakdown(retSimUIModel.CurrentSimOutput.MedianCombatLog);
                        AuraBreakdownMedianLog.Sort((a, b) => b.Uptime.CompareTo(a.Uptime));

                        if (DamageBreakdownSelection.SelectedValue.ToString() == "Median")
                        {
                            DamageBreakdownSelection_SelectionChanged(null, null);
                        }
                        if (AuraBreakdownSelection.SelectedValue.ToString() == "Median")
                        {
                            AuraBreakdownSelection_SelectionChanged(null, null);
                        }

                        DamageChart.Plot.Clear();
                        List<double> damage = new();
                        List<string> damageLabels = new();
                        for (int i = 0; i < DamageBreakdownMedianLog.Count - 1; i++)
                        {
                            damage.Add(DamageBreakdownMedianLog[i].Damage);
                            damageLabels.Add(DamageBreakdownMedianLog[i].AbilityName);
                        }

                        var DamageChartPie = DamageChart.Plot.AddPie(damage.ToArray());
                        DamageChartPie.ShowPercentages = true;
                        DamageChartPie.SliceLabels = damageLabels.ToArray();
                        DamageChart.Plot.Legend();
                        DamageChart.Refresh();

                        AuraChart.Plot.Clear();
                        List<double[]> uptime = new();
                        List<double[]> errors = new();
                        List<string> uptimeLabels = new();
                        for (int i = 0; i < AuraBreakdownMedianLog.Count; i++)
                        {
                            if (AuraBreakdownMedianLog[i].UptimePercentage == 100 && AuraBreakdownMedianLog[i].Count == 1)
                            {
                                continue;
                            }
                            uptime.Add(new double[] { AuraBreakdownMedianLog[i].UptimePercentage });
                            errors.Add(new double[] { 0 });
                            uptimeLabels.Add(AuraBreakdownMedianLog[i].AuraName);
                        }

                        var bars = AuraChart.Plot.AddBarGroups(new string[] { "" }, uptimeLabels.ToArray(), uptime.ToArray(), errors.ToArray());
                        var legend = AuraChart.Plot.Legend(location: Alignment.UpperRight);
                        legend.FontSize = 10;
                        legend.ReverseOrder = true;
                        foreach (var bar in bars)
                        {
                            bar.Orientation = ScottPlot.Orientation.Horizontal;
                        }

                        double[] xPositions = { 0, 25, 50, 75, 100 };
                        string[] xLabels = { "0%", "25%", "50%", "75%", "100%" };
                        AuraChart.Plot.XAxis.ManualTickPositions(xPositions, xLabels);

                        AuraChart.Plot.SetAxisLimits(xMin: 0);
                        AuraChart.Plot.XAxis.Ticks(true);
                        AuraChart.Plot.XAxis.MajorGrid(enable: false);
                        AuraChart.Plot.YAxis.Ticks(false);
                        AuraChart.Plot.YAxis.MajorGrid(enable: false);
                        AuraChart.Plot.YAxis2.Line(false);
                        AuraChart.Plot.XAxis2.Line(false);
                        AuraChart.Refresh();


                        DamageChart.Visibility = System.Windows.Visibility.Visible;
                        AuraChart.Visibility = System.Windows.Visibility.Visible;

                    }
                    else if (e.PropertyName == "DpsResults")
                    {
                        double min = retSimUIModel.CurrentSimOutput.DpsResults[0];
                        double max = retSimUIModel.CurrentSimOutput.DpsResults[^1];
                        (double[] counts, double[] binEdges) = ScottPlot.Statistics.Common.Histogram(retSimUIModel.CurrentSimOutput.DpsResults.ToArray(), min: min, max: max, binSize: 10);
                        double[] leftEdges = binEdges.Take(binEdges.Length - 1).ToArray();

                        DPSHistogram.Plot.Clear();
                        var bar = DPSHistogram.Plot.AddBar(values: counts, positions: leftEdges);
                        bar.BarWidth = 10;

                        DPSHistogram.Plot.YAxis.Label("");
                        DPSHistogram.Plot.XAxis.Label("DPS");
                        DPSHistogram.Plot.SetAxisLimits(yMin: 0, xMin: min, xMax: max);

                        DPSHistogram.Refresh();
                    }
                }
            });
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

        CombatLog newLog = new();

        ScatterPlot scatterPlot;
        MarkerPlot highlight;
        Tooltip tooltip;

        private void DamageBreakdownSelection_SelectionChanged(object? sender, SelectionChangedEventArgs? e)
        {
            if (DamageBreakdownSelection != null && DamageBreakdownSelection.SelectedValue != null)
            {
                if (DataContext is RetSimUIModel retSimUIModel)
                {
                    var value = DamageBreakdownSelection.SelectedValue.ToString();

                    if (value == "Min" && DamageBreakdownMinLog.Count > 0)
                    {
                        DamageBreakdownTable.ItemsSource = DamageBreakdownMinLog.GetRange(0, DamageBreakdownMinLog.Count - 1);
                        DamageBreakdownTotalTable.ItemsSource = DamageBreakdownMinLog.GetRange(DamageBreakdownMinLog.Count - 1, 1);
                        newLog = retSimUIModel.CurrentSimOutput.MinCombatLog;
                    }
                    else if (value == "Median" && DamageBreakdownMedianLog.Count > 0)
                    {
                        DamageBreakdownTable.ItemsSource = DamageBreakdownMedianLog.GetRange(0, DamageBreakdownMedianLog.Count - 1);
                        DamageBreakdownTotalTable.ItemsSource = DamageBreakdownMedianLog.GetRange(DamageBreakdownMedianLog.Count - 1, 1);
                        newLog = retSimUIModel.CurrentSimOutput.MedianCombatLog;
                    }
                    else if (value == "Max" && DamageBreakdownMaxLog.Count > 0)
                    {
                        DamageBreakdownTable.ItemsSource = DamageBreakdownMaxLog.GetRange(0, DamageBreakdownMaxLog.Count - 1);
                        DamageBreakdownTotalTable.ItemsSource = DamageBreakdownMaxLog.GetRange(DamageBreakdownMaxLog.Count - 1, 1);
                        newLog = retSimUIModel.CurrentSimOutput.MaxCombatLog;
                    }


                    List<double> damage;
                    List<double> time;

                    double totalDamage = 0;

                    if (newLog.DamageLog.Count > 0)
                    {
                        damage = new() { 0, 0 };
                        time = new() { 0, newLog.DamageLog[0].Timestamp / 1000.0 };
                    }

                    else
                    {
                        damage = new();
                        time = new();
                    }

                    foreach (DamageEntry entry in newLog.DamageLog)
                    {
                        totalDamage += entry.Damage;
                        time.Add(entry.Timestamp / 1000.0);
                        damage.Add(totalDamage);
                    }

                    if (time.Count > 0)
                    {
                        DPSGraph.Plot.Clear();
                        DPSGraph.Plot.AddScatterStep(time.ToArray(), damage.ToArray());
                        scatterPlot = DPSGraph.Plot.AddScatterPoints(time.ToArray(), damage.ToArray(), Color.Blue, 5, Marker.FilledSquare);

                        DPSHistogram.Plot.SetAxisLimits(yMin: 0, xMin: 0);
                        DPSGraph.Plot.SetOuterViewLimits(yMin: -totalDamage * 0.1, xMin: -2, yMax: totalDamage * 1.1, xMax: time[^1] + 2);

                        highlight = DPSGraph.Plot.AddPoint(0, 0);
                        highlight.Color = Color.Red;
                        highlight.MarkerSize = 8;
                        highlight.MarkerShape = MarkerShape.openSquare;
                        highlight.IsVisible = false;

                        tooltip = DPSGraph.Plot.AddTooltip("Test", 0, 0);
                        tooltip.IsVisible = false;

                        DPSGraph.Refresh();
                    }

                    DamageBreakdownTable.Items.Refresh();
                    DamageBreakdownDamageColumn.SortDirection = ListSortDirection.Descending;
                    DamageBreakdownTable.Items.SortDescriptions.Add(new SortDescription(DamageBreakdownDamageColumn.SortMemberPath, ListSortDirection.Descending));
                }
            }
        }

        int lastIndex;

        private void DPSGraph_MouseMove(object sender, MouseEventArgs e)
        {
            if (scatterPlot == null)
                return;

            (double mouseCoordX, double mouseCoordY) = DPSGraph.GetMouseCoordinates();
            double xyRatio = DPSGraph.Plot.XAxis.Dims.PxPerUnit / DPSGraph.Plot.YAxis.Dims.PxPerUnit;
            (double pointX, double pointY, int pointIndex) = scatterPlot.GetPointNearest(mouseCoordX, mouseCoordY, xyRatio);

            if (pointIndex <= 1)
                return;

            if (lastIndex != pointIndex || !tooltip.IsVisible)
            {
                tooltip.X = pointX;
                tooltip.Y = pointY;
                tooltip.Label = newLog.DamageLog[pointIndex - 2].ToString();
                tooltip.IsVisible = true;


                highlight.X = pointX;
                highlight.Y = pointY;
                highlight.IsVisible = true;


                lastIndex = pointIndex;

                DPSGraph.Refresh();
            }
        }

        private void DPSGraph_MouseLeave(object sender, MouseEventArgs e)
        {
            if (scatterPlot != null)
            {
                tooltip.IsVisible = false;
                highlight.IsVisible = false;

                DPSGraph.Refresh();
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
