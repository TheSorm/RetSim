using RetSim.Simulation;
using RetSim.Simulation.Tactics;
using RetSim.Spells;
using RetSim.Units.Enemy;
using RetSim.Units.Player;
using RetSim.Units.Player.Static;
using RetSimDesktop.Model;
using RetSimDesktop.ViewModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RetSimDesktop.View
{
    public class SimWorker : BackgroundWorker
    {
        public SimWorker()
        {
            DoWork += BackgroundWorker_DoWork;
        }

        static void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            if (e.Argument is RetSimUIModel retSimUIModel)
            {
                Equipment playerEquipment = SelectedGear.GetEquipment(retSimUIModel);
                var numberOfSimulations = retSimUIModel.SimSettings.SimulationCount;

                var talents = retSimUIModel.SelectedTalents.GetTalentList();
                var buffs = Spell.GetSpells(25580, 27141, 25898, 2048, 25528, 25359, 25570, 30811, 26991, 25392, 32999, 27127, 28878);
                var debuffs = Spell.GetSpells(20337, 14169, 33602, 27226, 30070, 32484, 17800, 33200, 15258, 22959, 14325, 34501);
                var consumables = retSimUIModel.SelectedConsumables.GetConsumables();

                float overallDPS = 0;
                List<CombatLog> fightSimulationCombatLogs = new(numberOfSimulations);
                for (int i = 0; i < numberOfSimulations; i++)
                {
                    FightSimulation fight = new(new Player("Brave Hero", RetSim.Data.Collections.Races["Human"], playerEquipment, talents), new Enemy("Magtheridon", CreatureType.Demon, ArmorCategory.Warrior), new EliteTactic(), buffs, debuffs, consumables, retSimUIModel.SimSettings.MinFightDuration, retSimUIModel.SimSettings.MaxFightDuration);
                    fight.Run();
                    overallDPS += fight.CombatLog.DPS;
                    if (i % (numberOfSimulations / 100 + 1) == 0)
                    {
                        retSimUIModel.CurrentSimOutput.Progress = (int)(i / ((float)numberOfSimulations) * 100);
                        retSimUIModel.CurrentSimOutput.DPS = overallDPS / (i + 1);
                    }
                    fightSimulationCombatLogs.Add(fight.CombatLog);
                }

                fightSimulationCombatLogs = fightSimulationCombatLogs.OrderBy(o => o.DPS).ToList();
                var minSimulation = fightSimulationCombatLogs[0];
                var medianSimulation = fightSimulationCombatLogs[(int)(numberOfSimulations / 2f)];
                var maxSimulation = fightSimulationCombatLogs[numberOfSimulations - 1];

                minSimulation.CreateDamageBreakdown();
                medianSimulation.CreateDamageBreakdown();
                maxSimulation.CreateDamageBreakdown();

                retSimUIModel.CurrentSimOutput.MinCombatLog = minSimulation;
                retSimUIModel.CurrentSimOutput.MedianCombatLog = medianSimulation;
                retSimUIModel.CurrentSimOutput.MaxCombatLog = maxSimulation;
                retSimUIModel.CurrentSimOutput.Progress = 100;
                retSimUIModel.CurrentSimOutput.DPS = overallDPS / numberOfSimulations;
                retSimUIModel.CurrentSimOutput.Min = minSimulation.DPS;
                retSimUIModel.CurrentSimOutput.Max = maxSimulation.DPS;
                retSimUIModel.SimButtonStatus.IsSimButtonEnabled = true;
            }
        }
    }
}
