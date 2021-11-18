using RetSim.Data;
using RetSim.Simulation;
using RetSim.Simulation.Tactics;
using RetSim.Spells;
using RetSim.Units.Enemy;
using RetSim.Units.Player;
using RetSim.Units.Player.Static;
using RetSimDesktop.Model;
using RetSimDesktop.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RetSimDesktop.View
{
    public class GearSim : BackgroundWorker
    {
        public GearSim()
        {
            DoWork += BackgroundWorker_DoWork;
        }

        static void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            if (e.Argument is (RetSimUIModel, List<DisplayGear>, int))
            {
                Tuple<RetSimUIModel, IEnumerable<DisplayGear>, int> input = (Tuple<RetSimUIModel, IEnumerable<DisplayGear>, int>)e.Argument;
                foreach (var item in input.Item2)
                {
                    if (!item.EnabledForGearSim)
                    {
                        continue;
                    }
                    Equipment playerEquipment = SelectedGear.GetEquipment(input.Item1);

                    playerEquipment.PlayerEquipment[input.Item3] = item.Item;

                    var numberOfSimulations = input.Item1.SimSettings.SimulationCount;

                    var talents = input.Item1.SelectedTalents.GetTalentList();
                    var buffs = input.Item1.SelectedBuffs.GetBuffs();
                    var debuffs = input.Item1.SelectedDebuffs.GetDebuffs();
                    var consumables = input.Item1.SelectedConsumables.GetConsumables();

                    float overallDPS = 0;
                    for (int i = 0; i < numberOfSimulations; i++)
                    {
                        FightSimulation fight = new(
                            new Player("Brave Hero", Collections.Races["Human"], playerEquipment, talents),
                            new Enemy(Collections.Bosses[17]),
                            new EliteTactic(), buffs, debuffs, consumables, input.Item1.SimSettings.MinFightDuration, input.Item1.SimSettings.MaxFightDuration);
                        fight.Run();
                        overallDPS += fight.CombatLog.DPS;
                        if (i % (numberOfSimulations / 100 + 1) == 0)
                        {
                            item.DPS = overallDPS / (i + 1);
                        }
                    }
                    item.DPS = overallDPS / numberOfSimulations;
                }
                input.Item1.SimButtonStatus.IsGearSimButtonEnabled = true;
            }
        }
    }
}
