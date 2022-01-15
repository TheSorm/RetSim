using RetSim.Data;
using RetSim.Simulation;
using RetSim.Simulation.Tactics;
using RetSim.Units.Enemy;
using RetSim.Units.Player;
using RetSim.Units.Player.Static;
using RetSimDesktop.Model;
using RetSimDesktop.Model.SimWorker;
using RetSimDesktop.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace RetSimDesktop.View
{
    public class GearSim : BackgroundWorker
    {
        private static Thread[] threads = new Thread[Environment.ProcessorCount];
        private static GearSimExecuter[] simExecuter = new GearSimExecuter[Environment.ProcessorCount];
        public GearSim()
        {
            DoWork += BackgroundWorker_DoWork;
        }

        static void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            if (e.Argument is (RetSimUIModel, List<DisplayGear>, int))
            {
                Tuple<RetSimUIModel, IEnumerable<DisplayGear>, int> input = (Tuple<RetSimUIModel, IEnumerable<DisplayGear>, int>)e.Argument;

                var race = input.Item1.PlayerSettings.SelectedRace;
                var shattrathFaction = input.Item1.PlayerSettings.SelectedShattrathFaction;
                var encounterID = input.Item1.EncounterSettings.EncounterID;

                var numberOfSimulations = input.Item1.SimSettings.SimulationCount;

                var minDuration = input.Item1.EncounterSettings.MinFightDurationMilliseconds;
                var maxDuration = input.Item1.EncounterSettings.MaxFightDurationMilliseconds;

                var talents = input.Item1.SelectedTalents.GetTalentList();
                var buffs = input.Item1.SelectedBuffs.GetBuffs();
                var debuffs = input.Item1.SelectedDebuffs.GetDebuffs();
                var consumables = input.Item1.SelectedConsumables.GetConsumables();
                var cooldowns = input.Item1.SelectedCooldowns.GetCooldowns();
                List<int> heroismUsage = new();
                if (input.Item1.SelectedBuffs.HeroismEnabled)
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

                foreach (var item in input.Item2)
                {
                    if (!item.EnabledForGearSim)
                    {
                        continue;
                    }
                    Equipment playerEquipment = input.Item1.SelectedGear.GetEquipment();
                    playerEquipment.PlayerEquipment[input.Item3] = item.Item;

                    int freeThread = -1;
                    while (freeThread == -1)
                    {
                        for (int i = 0; i < threads.Length; i++)
                        {
                            if (threads[i] == null || !threads[i].IsAlive)
                            {
                                freeThread = i;
                                break;
                            }
                        }
                        Thread.Sleep(100);
                    }
                    simExecuter[freeThread] = new()
                    {
                        Race = Collections.Races[race.ToString()],
                        ShattrathFaction = shattrathFaction,
                        Encounter = Collections.Bosses[encounterID],
                        PlayerEquipment = playerEquipment,
                        Talents = talents,
                        Buffs = buffs,
                        Debuffs = debuffs,
                        Consumables = consumables,
                        Cooldowns = cooldowns,
                        HeroismUsage = heroismUsage,
                        MinFightDuration = minDuration,
                        MaxFightDuration = maxDuration,
                        NumberOfSimulations = numberOfSimulations,
                        Item = item
                    };
                    threads[freeThread] = new(new ThreadStart(simExecuter[freeThread].Execute));
                    threads[freeThread].Start();
                }

                foreach (var thread in threads)
                {
                    if (thread != null)
                        thread.Join();
                }

                input.Item1.SimButtonStatus.IsSimButtonEnabled = true;
            }
        }
    }

    public class GearSimExecuter : SimExecuter
    {
        public DisplayGear Item { get; init; } = new();

        public override void Execute()
        {
            float overallDPS = 0;
            for (int i = 0; i < NumberOfSimulations; i++)
            {
                FightSimulation fight = new(new Player("Brave Hero", Race, ShattrathFaction, PlayerEquipment, Talents), new Enemy(Encounter), new EliteTactic(), Buffs, Debuffs, Consumables, MinFightDuration, MaxFightDuration, Cooldowns, HeroismUsage);
                fight.Run();
                overallDPS += fight.CombatLog.DPS;
                Item.DPS = overallDPS / (i + 1);
            }
            Item.DPS = overallDPS / NumberOfSimulations;
        }
    }
}
