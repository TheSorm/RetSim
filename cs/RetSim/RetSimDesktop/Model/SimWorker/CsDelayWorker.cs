using RetSim.Data;
using RetSim.Misc;
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

namespace RetSimDesktop.Model
{
    public class CsDelayWorker : BackgroundWorker
    {
        private static Thread[] threads = new Thread[Environment.ProcessorCount];
        private static CsDelaySimExecuter[] simExecuter = new CsDelaySimExecuter[Environment.ProcessorCount];

        public CsDelayWorker()
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
                var groupTalents = retSimUIModel.SelectedBuffs.GetGroupTalents();
                groupTalents.AddRange(retSimUIModel.SelectedDebuffs.GetGroupTalents());
                var buffs = retSimUIModel.SelectedBuffs.GetBuffs();
                var debuffs = retSimUIModel.SelectedDebuffs.GetDebuffs();
                var consumables = retSimUIModel.SelectedConsumables.GetConsumables();
                var cooldowns = retSimUIModel.SelectedCooldowns.GetCooldowns();
                var delayElements = retSimUIModel.DisplayCsDelay;

                var useExorcism = retSimUIModel.SimSettings.UseExorcism && (Collections.Bosses[encounterID].CreatureType == CreatureType.Demon || Collections.Bosses[encounterID].CreatureType == CreatureType.Undead);
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
                float baseDps = 0;
                int baseSeed = RNG.global.Next();
                for (int i = 0; i < numberOfSimulations; i++)
                {
                    RNG.local = new(baseSeed + i);
                    FightSimulation fight = new(
                        new Player("Brave Hero", Collections.Races[race.ToString()], shattrathFaction, playerEquipment, talents),
                        new Enemy(Collections.Bosses[encounterID]),
                        new EliteTactic(0, useExorcism, useConsecration), groupTalents, buffs, debuffs, consumables, minDuration, maxDuration, cooldowns, heroismUsage);
                    fight.Run();
                    baseDps += fight.CombatLog.DPS;
                    retSimUIModel.DisplayCsDelay[0].DpsDelta = baseDps / i;
                }
                baseDps /= numberOfSimulations;
                retSimUIModel.DisplayCsDelay[0].DpsDelta = baseDps;

                foreach (var item in delayElements)
                {
                    if (item.Delay == 0 || !item.EnabledForCsDelay)
                    {
                        continue;
                    }
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
                        BaseSeed = baseSeed,
                        BaseDps = baseDps,
                        CsDelayDisplay = item,
                        UseExorcism = useExorcism,
                        UseConsecration = useConsecration,
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

    public class CsDelaySimExecuter : SimExecuter
    {
        public DisplayCsDelay CsDelayDisplay { get; init; } = new();
        public float BaseDps { get; init; }
        public int BaseSeed { get; init; }

        public override void Execute()
        {
            float dps = 0;
            for (int i = 0; i < NumberOfSimulations; i++)
            {
                RNG.local = new(BaseSeed + i);
                FightSimulation fight = new(new Player("Brave Hero", Race, ShattrathFaction, PlayerEquipment, Talents), new Enemy(Encounter),
                    new EliteTactic((int)(CsDelayDisplay.Delay * 1000), UseExorcism, UseConsecration),
                    GroupTalents, Buffs, Debuffs, Consumables, MinFightDuration, MaxFightDuration, Cooldowns, HeroismUsage);
                fight.Run();
                dps += fight.CombatLog.DPS;
                CsDelayDisplay.DpsDelta = ((dps / i) - BaseDps);
            }
            dps /= NumberOfSimulations;
            CsDelayDisplay.DpsDelta = (dps - BaseDps);
        }
    }
}
