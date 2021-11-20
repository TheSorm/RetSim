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
using System.Diagnostics;
using System.Threading;

namespace RetSimDesktop.View
{
    public class SimWorker : BackgroundWorker
    {
        private static Thread[] threads = new Thread[Environment.ProcessorCount];
        private static SimExecuter[] simExecuter = new SimExecuter[Environment.ProcessorCount];

        public SimWorker()
        {
            DoWork += BackgroundWorker_DoWork;
        }

        static void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            if (e.Argument is RetSimUIModel retSimUIModel)
            {
                var numberOfSimulations = retSimUIModel.SimSettings.SimulationCount;

                Equipment playerEquipment = retSimUIModel.SelectedGear.GetEquipment();
                var talents = retSimUIModel.SelectedTalents.GetTalentList();
                var buffs = retSimUIModel.SelectedBuffs.GetBuffs();
                var debuffs = retSimUIModel.SelectedDebuffs.GetDebuffs();
                var consumables = retSimUIModel.SelectedConsumables.GetConsumables();

                CombatLog[] combatLogs = new CombatLog[numberOfSimulations];

                var simulationsDistributed = 0;
                var simulationsPerThread = (int)Math.Ceiling(numberOfSimulations / ((float)threads.Length));
                for (int i = 0; i < simExecuter.Length; i++)
                {
                    if (simulationsDistributed < numberOfSimulations)
                    {
                        if (simulationsDistributed + simulationsPerThread <= numberOfSimulations)
                        {
                            simExecuter[i] = new(playerEquipment, talents, buffs, debuffs, consumables,
                                retSimUIModel.SimSettings.MinFightDuration, retSimUIModel.SimSettings.MaxFightDuration,
                                combatLogs, simulationsDistributed, simulationsPerThread);
                            simulationsDistributed += simulationsPerThread;
                        }
                        else
                        {
                            simExecuter[i] = new(playerEquipment, talents, buffs, debuffs, consumables,
                                retSimUIModel.SimSettings.MinFightDuration, retSimUIModel.SimSettings.MaxFightDuration,
                                combatLogs, simulationsDistributed, numberOfSimulations - simulationsDistributed);
                            simulationsDistributed += numberOfSimulations - simulationsDistributed;
                        }
                        threads[i] = new(new ThreadStart(simExecuter[i].Execute));
                        threads[i].Start();
                    }
                }

                int finishedSimulationCount = 0;
                float damage = 0;
                while (finishedSimulationCount != numberOfSimulations)
                {
                    finishedSimulationCount = 0;
                    damage = 0;
                    for (int i = 0; i < simExecuter.Length; i++)
                    {
                        if(simExecuter[i] == null)
                        {
                            break;
                        }
                        for (int j = simExecuter[i].startIndex; j < simExecuter[i].startIndex + simExecuter[i].length; j++)
                        {
                            if(combatLogs[j] == null)
                            {
                                break;
                            }
                            finishedSimulationCount++;
                            damage += combatLogs[j].DPS;
                        }
                    }
                    retSimUIModel.CurrentSimOutput.Progress = (int) (finishedSimulationCount / ((float)numberOfSimulations) * 100);
                    retSimUIModel.CurrentSimOutput.DPS = (damage / ((float)finishedSimulationCount));
                    Thread.Sleep(100);
                }

                foreach (var thread in threads)
                {
                    thread.Join();
                }

                Array.Sort(combatLogs, (x, y) => (int)(x.DPS - y.DPS));
                var minSimulation = combatLogs[0];
                var medianSimulation = combatLogs[(int)(numberOfSimulations / 2f)];
                var maxSimulation = combatLogs[numberOfSimulations - 1];

                minSimulation.CreateDamageBreakdown();
                medianSimulation.CreateDamageBreakdown();
                maxSimulation.CreateDamageBreakdown();

                retSimUIModel.CurrentSimOutput.MinCombatLog = minSimulation;
                retSimUIModel.CurrentSimOutput.MedianCombatLog = medianSimulation;
                retSimUIModel.CurrentSimOutput.MaxCombatLog = maxSimulation;
                retSimUIModel.CurrentSimOutput.Min = minSimulation.DPS;
                retSimUIModel.CurrentSimOutput.Max = maxSimulation.DPS;
                retSimUIModel.SimButtonStatus.IsSimButtonEnabled = true;
                retSimUIModel.SimButtonStatus.IsGearSimButtonEnabled = true;

                Array.Clear(combatLogs);
                Array.Clear(threads);
                Array.Clear(simExecuter);
            }
        }
    }

    public class SimExecuter
    {
        private readonly Equipment playerEquipment;
        private readonly List<Talent> talents;
        private readonly List<Spell> buffs;
        private readonly List<Spell> debuffs;
        private readonly List<Spell> consumables;
        private readonly int minFightDuration;
        private readonly int maxFightDuration;
        private readonly CombatLog[] combatLogs;
        public readonly int startIndex;
        public readonly int length;

        public SimExecuter(Equipment playerEquipment, List<Talent> talents, List<Spell> buffs, List<Spell> debuffs, List<Spell> consumables, int minFightDuration, int maxFightDuration, CombatLog[] combatLogs, int startIndex, int length)
        {
            this.playerEquipment = playerEquipment;
            this.talents = talents;
            this.buffs = buffs;
            this.debuffs = debuffs;
            this.consumables = consumables;
            this.minFightDuration = minFightDuration;
            this.maxFightDuration = maxFightDuration;
            this.combatLogs = combatLogs;
            this.startIndex = startIndex;
            this.length = length;
        }

        public void Execute()
        {
            for (int i = startIndex; i < startIndex + length; i++)
            {
                FightSimulation fight = new(new Player("Brave Hero", Collections.Races["Human"], playerEquipment, talents), new Enemy(Collections.Bosses[17]), new EliteTactic(), buffs, debuffs, consumables, minFightDuration, maxFightDuration);
                fight.Run();
                combatLogs[i] = fight.CombatLog;
            }
        }
    }
}
