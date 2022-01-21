using RetSim.Misc;
using RetSim.Simulation;
using RetSim.Simulation.CombatLogEntries;
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
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : UserControl
    {
        int simulations;

        public List<DamageBreakdownElement> DamageBreakdownMinLog { get; set; }
        public List<DamageBreakdownElement> DamageBreakdownMedianLog { get; set; }
        public List<DamageBreakdownElement> DamageBreakdownMaxLog { get; set; }

        public List<AuraBreakdownElement> AuraBreakdownMinLog { get; set; }
        public List<AuraBreakdownElement> AuraBreakdownMedianLog { get; set; }
        public List<AuraBreakdownElement> AuraBreakdownMaxLog { get; set; }

        private List<DamageEntry> currentDamageBreakdownCombatLog = new();
        private List<DamageEntry> currentFilteredDamageBreakdownCombatLog = new();

        private ScatterPlot damageBreakdownScatterPlot;
        private MarkerPlot damageBreakdownScatterPlotHighlight;
        private Tooltip damageBreakdownScatterPlotTooltip;
        private int damageBreakdownScatterPlotLastIndex;

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

            DPSHistogram.RightClicked -= DPSHistogram.DefaultRightClickEvent;
            DPSHistogram.Plot.YAxis.Label("");
            DPSHistogram.Plot.XAxis.Label("DPS");

            DPSHistogram.MouseMove += DPSHistograph_MouseMove;

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
            DamageChart.Visibility = Visibility.Hidden;

            AuraChart.Configuration.Zoom = false;
            AuraChart.Configuration.Pan = false;
            AuraChart.Configuration.DoubleClickBenchmark = false;
            AuraChart.RightClicked -= AuraChart.DefaultRightClickEvent;
            AuraChart.Visibility = Visibility.Hidden;

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


                        DamageChart.Visibility = Visibility.Visible;
                        AuraChart.Visibility = Visibility.Visible;

                    }
                    else if (e.PropertyName == "DpsResults")
                    {
                        double min = (int)Math.Floor(retSimUIModel.CurrentSimOutput.DpsResults[0] / 10) * 10;
                        double max = (int)Math.Ceiling(retSimUIModel.CurrentSimOutput.DpsResults[^1] / 10) * 10;
                        (double[] counts, double[] binEdges) = ScottPlot.Statistics.Common.Histogram(retSimUIModel.CurrentSimOutput.DpsResults.ToArray(), min: min, max: max, binSize: 10);
                        double[] leftEdges = binEdges.Take(binEdges.Length - 1).ToArray();

                        DPSHistogram.Plot.Clear();
                        barPlot = DPSHistogram.Plot.AddBar(values: counts, positions: leftEdges);
                        barPlot.BarWidth = 10;

                        DPSHistogram.Plot.YAxis.Label("");
                        DPSHistogram.Plot.XAxis.Label("DPS");
                        DPSHistogram.Plot.SetAxisLimits(yMin: 0, xMin: min - 10, xMax: max);

                        barTooltip = DPSHistogram.Plot.AddTooltip("Test", 0, 0);
                        barTooltip.IsVisible = false;

                        DPSHistogram.Refresh();

                        simulations = retSimUIModel.CurrentSimOutput.DpsResults.Count;
                    }
                }
            });
        }

        BarPlot barPlot;
        Tooltip barTooltip;

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
                    IsChecked = true,
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
                IsChecked = false,
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
                if (DataContext is RetSimUIModel retSimUIModel)
                {
                    var value = DamageBreakdownSelection.SelectedValue.ToString();

                    if (value == "Min" && DamageBreakdownMinLog.Count > 0)
                    {
                        DamageBreakdownTable.ItemsSource = DamageBreakdownMinLog.GetRange(0, DamageBreakdownMinLog.Count - 1);
                        DamageBreakdownTotalTable.ItemsSource = DamageBreakdownMinLog.GetRange(DamageBreakdownMinLog.Count - 1, 1);
                        currentDamageBreakdownCombatLog = retSimUIModel.CurrentSimOutput.MinCombatLog.DamageLog;
                    }
                    else if (value == "Median" && DamageBreakdownMedianLog.Count > 0)
                    {
                        DamageBreakdownTable.ItemsSource = DamageBreakdownMedianLog.GetRange(0, DamageBreakdownMedianLog.Count - 1);
                        DamageBreakdownTotalTable.ItemsSource = DamageBreakdownMedianLog.GetRange(DamageBreakdownMedianLog.Count - 1, 1);
                        currentDamageBreakdownCombatLog = retSimUIModel.CurrentSimOutput.MedianCombatLog.DamageLog;
                    }
                    else if (value == "Max" && DamageBreakdownMaxLog.Count > 0)
                    {
                        DamageBreakdownTable.ItemsSource = DamageBreakdownMaxLog.GetRange(0, DamageBreakdownMaxLog.Count - 1);
                        DamageBreakdownTotalTable.ItemsSource = DamageBreakdownMaxLog.GetRange(DamageBreakdownMaxLog.Count - 1, 1);
                        currentDamageBreakdownCombatLog = retSimUIModel.CurrentSimOutput.MaxCombatLog.DamageLog;
                    }
                    currentFilteredDamageBreakdownCombatLog = currentDamageBreakdownCombatLog;
                    UpdateDamageGraph();

                    DamageBreakdownTable.Items.Refresh();
                    DamageBreakdownDamageColumn.SortDirection = ListSortDirection.Descending;
                    DamageBreakdownTable.Items.SortDescriptions.Add(new SortDescription(DamageBreakdownDamageColumn.SortMemberPath, ListSortDirection.Descending));
                }
            }
        }

        private void UpdateDamageGraph()
        {

            List<double> damage;
            List<double> time;

            double totalDamage = 0;

            if (currentFilteredDamageBreakdownCombatLog.Count > 0)
            {
                damage = new() { 0, 0 };
                time = new() { 0, currentFilteredDamageBreakdownCombatLog[0].Timestamp / 1000.0 };
            }
            else
            {
                damage = new();
                time = new();
            }

            Dictionary<string, (List<double>, List<double>)> damageByName = new();
            foreach (DamageEntry entry in currentFilteredDamageBreakdownCombatLog)
            {
                totalDamage += entry.Damage;
                time.Add(entry.Timestamp / 1000.0);
                damage.Add(totalDamage);

                if (!damageByName.ContainsKey(entry.Source))
                {
                    damageByName[entry.Source] = (new(), new());
                }

                damageByName[entry.Source].Item1.Add(entry.Timestamp / 1000.0);
                damageByName[entry.Source].Item2.Add(totalDamage);
            }

            if (time.Count > 0)
            {
                DPSGraph.Plot.Clear();
                DPSGraph.Plot.AddScatterStep(time.ToArray(), damage.ToArray());

                var legend = DPSGraph.Plot.Legend(true);
                foreach (var damageOfAbility in damageByName)
                {
                    var plotable = DPSGraph.Plot.AddScatterPoints(damageOfAbility.Value.Item1.ToArray(), damageOfAbility.Value.Item2.ToArray(), markerSize: 5, markerShape: Marker.FilledSquare, label: damageOfAbility.Key);
                }
                damageBreakdownScatterPlot = DPSGraph.Plot.AddScatterPoints(time.ToArray(), damage.ToArray(), markerSize: 0, markerShape: Marker.FilledSquare);

                DPSGraph.Plot.SetOuterViewLimits(yMin: -totalDamage * 0.1, xMin: -2, yMax: totalDamage * 1.1, xMax: time[^1] + 2);

                damageBreakdownScatterPlotHighlight = DPSGraph.Plot.AddPoint(0, 0);
                damageBreakdownScatterPlotHighlight.Color = Color.Red;
                damageBreakdownScatterPlotHighlight.MarkerSize = 8;
                damageBreakdownScatterPlotHighlight.MarkerShape = MarkerShape.openSquare;
                damageBreakdownScatterPlotHighlight.IsVisible = false;

                damageBreakdownScatterPlotTooltip = DPSGraph.Plot.AddTooltip("Test", 0, 0);
                damageBreakdownScatterPlotTooltip.IsVisible = false;

                DPSGraph.Refresh();
            }
            else
            {
                DPSGraph.Plot.Clear();
                DPSGraph.Refresh();
            }
        }

        private void DPSHistograph_MouseMove(object sender, MouseEventArgs e)
        {
            if (barPlot == null)
                return;

            (double mouseCoordX, double mouseCoordY) = DPSHistogram.GetMouseCoordinates();

            int index = -1;
            double up = 0, left = 0, right = 0;

            for (int i = 0; i < barPlot.Positions.Length; i++)
            {
                left = barPlot.Positions[i];
                double halfWidth = barPlot.BarWidth / 2;
                right = left + halfWidth;
                

                if (mouseCoordX < right && mouseCoordX > left - halfWidth)
                {
                    index = i;

                    up = barPlot.Values[i];

                    if (mouseCoordY < up && mouseCoordY > 0)
                        break;

                    else
                        index = -1;
                }
            }

            if (index != -1)
            {
                barTooltip.X = mouseCoordX;
                barTooltip.Y = mouseCoordY;

                if (barTooltip.Y < 3)
                    barTooltip.Y = 3;

                int min = (int)(Math.Round(left / 10, 0) * 10);
                double max = min + barPlot.BarWidth - 0.1;
                double percentage = up / simulations;

                barTooltip.Label = $"{min}-{max} DPS: {up} instances ({percentage:###.##%} of all results)";

                barTooltip.IsVisible = true;
            }

            else
                barTooltip.IsVisible = false;


            DPSHistogram.Render();

            //if ((damageBreakdownScatterPlotLastIndex != pointIndex || !damageBreakdownScatterPlotTooltip.IsVisible) && currentFilteredDamageBreakdownCombatLog.Count > pointIndex - 2)
            //{
            //    damageBreakdownScatterPlotTooltip.X = pointX;
            //    damageBreakdownScatterPlotTooltip.Y = pointY;
            //    damageBreakdownScatterPlotTooltip.Label = currentFilteredDamageBreakdownCombatLog[pointIndex - 2].ToString();
            //    damageBreakdownScatterPlotTooltip.IsVisible = true;


            //    damageBreakdownScatterPlotHighlight.X = pointX;
            //    damageBreakdownScatterPlotHighlight.Y = pointY;
            //    damageBreakdownScatterPlotHighlight.IsVisible = true;


            //    damageBreakdownScatterPlotLastIndex = pointIndex;

            //    DPSGraph.Refresh();
            //}
        }


        private void DPSGraph_MouseMove(object sender, MouseEventArgs e)
        {
            if (damageBreakdownScatterPlot == null)
                return;

            (double mouseCoordX, double mouseCoordY) = DPSGraph.GetMouseCoordinates();
            double xyRatio = DPSGraph.Plot.XAxis.Dims.PxPerUnit / DPSGraph.Plot.YAxis.Dims.PxPerUnit;
            (double pointX, double pointY, int pointIndex) = damageBreakdownScatterPlot.GetPointNearest(mouseCoordX, mouseCoordY, xyRatio);

            if (pointIndex <= 1)
                return;

            if ((damageBreakdownScatterPlotLastIndex != pointIndex || !damageBreakdownScatterPlotTooltip.IsVisible) && currentFilteredDamageBreakdownCombatLog.Count > pointIndex - 2)
            {
                damageBreakdownScatterPlotTooltip.X = pointX;
                damageBreakdownScatterPlotTooltip.Y = pointY;
                damageBreakdownScatterPlotTooltip.Label = currentFilteredDamageBreakdownCombatLog[pointIndex - 2].ToString();
                damageBreakdownScatterPlotTooltip.IsVisible = true;


                damageBreakdownScatterPlotHighlight.X = pointX;
                damageBreakdownScatterPlotHighlight.Y = pointY;
                damageBreakdownScatterPlotHighlight.IsVisible = true;


                damageBreakdownScatterPlotLastIndex = pointIndex;

                DPSGraph.Refresh();
            }
        }

        private void DPSGraph_MouseLeave(object sender, MouseEventArgs e)
        {
            if (damageBreakdownScatterPlot != null)
            {
                damageBreakdownScatterPlotTooltip.IsVisible = false;
                damageBreakdownScatterPlotHighlight.IsVisible = false;

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

        private void DamageBreakdownElementChecked(object sender, RoutedEventArgs e)
        {
            Dictionary<string, bool> enabledDamageEvents = new();
            foreach (var element in (List<DamageBreakdownElement>)DamageBreakdownTable.ItemsSource)
            {
                enabledDamageEvents.Add(element.AbilityName, element.IsChecked);
            }

            List<DamageEntry> damageEntries = new();

            foreach (var element in currentDamageBreakdownCombatLog)
            {
                if (enabledDamageEvents[element.Source])
                {
                    damageEntries.Add(element);
                }
            }
            currentFilteredDamageBreakdownCombatLog = damageEntries;

            UpdateDamageGraph();
        }
    }

    public class DamageBreakdownElement
    {
        public bool IsChecked { get; set; }
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
