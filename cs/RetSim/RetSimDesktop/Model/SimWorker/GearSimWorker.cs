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
            if (e.Argument is ValueTuple<RetSimUIModel, List<DisplayGear>, int>(var retSimUIModel, var gearList, var slotID))
            {
                var race = retSimUIModel.PlayerSettings.SelectedRace;
                var shattrathFaction = retSimUIModel.PlayerSettings.SelectedShattrathFaction;
                var encounterID = retSimUIModel.EncounterSettings.EncounterID;

                var numberOfSimulations = retSimUIModel.SimSettings.SimulationCount;
                var maxCSDelay = retSimUIModel.SimSettings.MaxCSDelay;

                var minDuration = retSimUIModel.EncounterSettings.MinFightDurationMilliseconds;
                var maxDuration = retSimUIModel.EncounterSettings.MaxFightDurationMilliseconds;

                var talents = retSimUIModel.SelectedTalents.GetTalentList();
                var groupTalents = retSimUIModel.SelectedBuffs.GetGroupTalents();
                groupTalents.AddRange(retSimUIModel.SelectedDebuffs.GetGroupTalents());
                var buffs = retSimUIModel.SelectedBuffs.GetBuffs();
                var debuffs = retSimUIModel.SelectedDebuffs.GetDebuffs();
                var consumables = retSimUIModel.SelectedConsumables.GetConsumables();
                var cooldowns = retSimUIModel.SelectedCooldowns.GetCooldowns();

                var useExorcism = retSimUIModel.SimSettings.UseExorcism && Collections.Bosses[encounterID].CreatureType == CreatureType.Demon;
                var useConsecration = retSimUIModel.SimSettings.UseConsecration;

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

                foreach (var item in gearList)
                {
                    if (!item.EnabledForGearSim)
                    {
                        continue;
                    }
                    //TODO: Move out of loop, fetch equipment once, and make copys instead of fetching multiple times
                    Equipment playerEquipment = retSimUIModel.SelectedGear.GetEquipment();
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
                        UseExorcism = useExorcism,
                        UseConsecration = useConsecration,
                        Item = item,
                    };
                    threads[freeThread] = new(new ThreadStart(simExecuter[freeThread].Execute));
                    threads[freeThread].Start();
                }

                foreach (var thread in threads)
                {
                    if (thread != null)
                        thread.Join();
                }

                retSimUIModel.SimButtonStatus.IsSimButtonEnabled = true;
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
                FightSimulation fight = new(new Player("Brave Hero", Race, ShattrathFaction, PlayerEquipment, Talents), new Enemy(Encounter),
                    new EliteTactic(MaxCSDelay, UseExorcism, UseConsecration), GroupTalents, Buffs, Debuffs, Consumables, MinFightDuration, MaxFightDuration, Cooldowns, HeroismUsage);
                fight.Run();
                overallDPS += fight.CombatLog.DPS;
                Item.DPS = overallDPS / (i + 1);
            }
            Item.DPS = overallDPS / NumberOfSimulations;
        }
    }
}
