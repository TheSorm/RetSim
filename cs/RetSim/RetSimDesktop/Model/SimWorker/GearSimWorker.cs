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
            if (e.Argument is ValueTuple<RetSimUIModel, List<DisplayGear>, int> (var retSimUiModel, var gearList, var slotID))
            {
                var race = retSimUiModel.PlayerSettings.SelectedRace;
                var shattrathFaction = retSimUiModel.PlayerSettings.SelectedShattrathFaction;
                var encounterID = retSimUiModel.EncounterSettings.EncounterID;

                var numberOfSimulations = retSimUiModel.SimSettings.SimulationCount;
                var maxCSDelay = retSimUiModel.SimSettings.MaxCSDelay;

                var minDuration = retSimUiModel.EncounterSettings.MinFightDurationMilliseconds;
                var maxDuration = retSimUiModel.EncounterSettings.MaxFightDurationMilliseconds;

                var talents = retSimUiModel.SelectedTalents.GetTalentList();
                var groupTalents = retSimUiModel.SelectedBuffs.GetGroupTalents();
                groupTalents.AddRange(retSimUiModel.SelectedDebuffs.GetGroupTalents());
                var buffs = retSimUiModel.SelectedBuffs.GetBuffs();
                var debuffs = retSimUiModel.SelectedDebuffs.GetDebuffs();
                var consumables = retSimUiModel.SelectedConsumables.GetConsumables();
                var cooldowns = retSimUiModel.SelectedCooldowns.GetCooldowns();
                List<int> heroismUsage = new();
                if (retSimUiModel.SelectedBuffs.HeroismEnabled)
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

                foreach (var item in gearList)
                {
                    if (!item.EnabledForGearSim)
                    {
                        continue;
                    }
                    //TODO: Move out of loop, fetch equipment once, and make copys instead of fetching multiple times (Also change weapon sim)
                    Equipment playerEquipment = retSimUiModel.SelectedGear.GetEquipment();
                    playerEquipment.PlayerEquipment[slotID] = item.Item;

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
                        GroupTalents = groupTalents,
                        Buffs = buffs,
                        Debuffs = debuffs,
                        Consumables = consumables,
                        Cooldowns = cooldowns,
                        HeroismUsage = heroismUsage,
                        MinFightDuration = minDuration,
                        MaxFightDuration = maxDuration,
                        NumberOfSimulations = numberOfSimulations,
                        MaxCSDelay = maxCSDelay,
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

                retSimUiModel.SimButtonStatus.IsSimButtonEnabled = true;
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
                FightSimulation fight = new(new Player("Brave Hero", Race, ShattrathFaction, PlayerEquipment, Talents), new Enemy(Encounter), new EliteTactic(MaxCSDelay), GroupTalents, Buffs, Debuffs, Consumables, MinFightDuration, MaxFightDuration, Cooldowns, HeroismUsage);
                fight.Run();
                overallDPS += fight.CombatLog.DPS;
                Item.DPS = overallDPS / (i + 1);
            }
            Item.DPS = overallDPS / NumberOfSimulations;
        }
    }
}
