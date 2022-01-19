using RetSim.Data;
using RetSim.Simulation;
using RetSim.Simulation.Tactics;
using RetSim.Units.Enemy;
using RetSim.Units.Player;
using RetSim.Units.Player.Static;
using RetSimDesktop.Model.SimWorker;
using RetSimDesktop.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace RetSimDesktop.View
{
    public class SimWorker : BackgroundWorker
    {
        private static Thread[] threads = new Thread[Environment.ProcessorCount];
        private static SingleSimExecuter[] simExecuter = new SingleSimExecuter[Environment.ProcessorCount];

        public SimWorker()
        {
            DoWork += BackgroundWorker_DoWork;
        }

        static void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            if (e.Argument is RetSimUIModel retSimUIModel)
            {
                var race = retSimUIModel.PlayerSettings.SelectedRace;
                var shattrathFaction = retSimUIModel.PlayerSettings.SelectedShattrathFaction;
                var encounterID = retSimUIModel.EncounterSettings.EncounterID;

                var numberOfSimulations = retSimUIModel.SimSettings.SimulationCount;

                var minDuration = retSimUIModel.EncounterSettings.MinFightDurationMilliseconds;
                var maxDuration = retSimUIModel.EncounterSettings.MaxFightDurationMilliseconds;

                Equipment playerEquipment = retSimUIModel.SelectedGear.GetEquipment();
                var talents = retSimUIModel.SelectedTalents.GetTalentList();
                var buffs = retSimUIModel.SelectedBuffs.GetBuffs();
                var groupTalents = retSimUIModel.SelectedBuffs.GetGroupTalents();
                groupTalents.AddRange(retSimUIModel.SelectedDebuffs.GetGroupTalents());
                var debuffs = retSimUIModel.SelectedDebuffs.GetDebuffs();
                var consumables = retSimUIModel.SelectedConsumables.GetConsumables();
                var cooldowns = retSimUIModel.SelectedCooldowns.GetCooldowns();
                List<int> heroismUsage = new();
                if (retSimUIModel.SelectedBuffs.HeroismEnabled)
                {
                    int time = 8000;
                    if (minDuration < 8000)
                    {
                        time = 0;
                    }

                    while (time < maxDuration)
                    {
                        heroismUsage.Add(time);
                        time += 600000;
                    }

                }

                CombatLog[] combatLogs = new CombatLog[numberOfSimulations];

                var simulationsDistributed = 0;
                var simulationsPerThread = (int)Math.Ceiling(numberOfSimulations / ((float)threads.Length));
                for (int i = 0; i < simExecuter.Length; i++)
                {
                    if (simulationsDistributed < numberOfSimulations)
                    {
                        if (simulationsDistributed + simulationsPerThread <= numberOfSimulations)
                        {
                            simExecuter[i] = new()
                            {
                                Race = Collections.Races[race.ToString()],
                                ShattrathFaction = shattrathFaction,
                                Encounter = Collections.Bosses[encounterID],
                                PlayerEquipment = playerEquipment,
                                Talents = talents,
                                GroupTalents = groupTalents,
                                Buffs = buffs,
                                Debuffs = debuffs,
                                Consumables = consumables,
                                Cooldowns = cooldowns,
                                HeroismUsage = heroismUsage,
                                MinFightDuration = minDuration,
                                MaxFightDuration = maxDuration,
                                CombatLogs = combatLogs,
                                StartIndex = simulationsDistributed,
                                NumberOfSimulations = simulationsPerThread
                            };
                            simulationsDistributed += simulationsPerThread;
                        }
                        else
                        {
                            simExecuter[i] = new()
                            {
                                Race = Collections.Races[race.ToString()],
                                ShattrathFaction = shattrathFaction,
                                Encounter = Collections.Bosses[encounterID],
                                PlayerEquipment = playerEquipment,
                                Talents = talents,
                                GroupTalents = groupTalents,
                                Buffs = buffs,
                                Debuffs = debuffs,
                                Consumables = consumables,
                                Cooldowns = cooldowns,
                                HeroismUsage = heroismUsage,
                                MinFightDuration = minDuration,
                                MaxFightDuration = maxDuration,
                                CombatLogs = combatLogs,
                                StartIndex = simulationsDistributed,
                                NumberOfSimulations = numberOfSimulations - simulationsDistributed
                            };
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
                        if (simExecuter[i] == null)
                        {
                            break;
                        }
                        for (int j = simExecuter[i].StartIndex; j < simExecuter[i].StartIndex + simExecuter[i].NumberOfSimulations; j++)
                        {
                            if (combatLogs[j] == null)
                            {
                                break;
                            }
                            finishedSimulationCount++;
                            damage += combatLogs[j].DPS;
                        }
                    }
                    if (finishedSimulationCount > 0)
                    {
                        retSimUIModel.CurrentSimOutput.Progress = (int)(finishedSimulationCount / ((float)numberOfSimulations) * 100);
                        retSimUIModel.CurrentSimOutput.DPS = (damage / ((float)finishedSimulationCount));
                    }
                    Thread.Sleep(100);
                }

                foreach (var thread in threads)
                {
                    if (thread != null)
                        thread.Join();
                }

                Array.Sort(combatLogs, (x, y) => (int)(x.DPS - y.DPS));
                var minSimulation = combatLogs[0];
                var medianSimulation = combatLogs[(int)(numberOfSimulations / 2f)];
                var maxSimulation = combatLogs[numberOfSimulations - 1];

                List<double> simDps = new List<double>();
                foreach (CombatLog log in combatLogs)
                {
                    simDps.Add(log.DPS);
                }

                retSimUIModel.CurrentSimOutput.DpsResults = simDps;
                retSimUIModel.CurrentSimOutput.MinCombatLog = minSimulation;
                retSimUIModel.CurrentSimOutput.MedianCombatLog = medianSimulation;
                retSimUIModel.CurrentSimOutput.MaxCombatLog = maxSimulation;
                retSimUIModel.CurrentSimOutput.Min = minSimulation.DPS;
                retSimUIModel.CurrentSimOutput.Max = maxSimulation.DPS;
                retSimUIModel.SimButtonStatus.IsSimButtonEnabled = true;

                Array.Clear(combatLogs);
                Array.Clear(threads);
                Array.Clear(simExecuter);
            }
        }
    }

    public class SingleSimExecuter : SimExecuter
    {
        public CombatLog[] CombatLogs { get; init; } = new CombatLog[1];
        public int StartIndex { get; init; }

        public override void Execute()
        {
            for (int i = StartIndex; i < StartIndex + NumberOfSimulations; i++)
            {
                FightSimulation fight = new(new Player("Brave Hero", Race, ShattrathFaction, PlayerEquipment, Talents), new Enemy(Encounter), new EliteTactic(4000), GroupTalents, Buffs, Debuffs, Consumables, MinFightDuration, MaxFightDuration, Cooldowns, HeroismUsage);
                fight.Run();
                CombatLogs[i] = fight.CombatLog;
            }
        }
    }
}
