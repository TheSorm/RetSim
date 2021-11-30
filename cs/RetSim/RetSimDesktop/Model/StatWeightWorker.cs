using RetSim.Data;
using RetSim.Misc;
using RetSim.Simulation;
using RetSim.Simulation.Tactics;
using RetSim.Spells;
using RetSim.Units.Enemy;
using RetSim.Units.Player;
using RetSim.Units.Player.Static;
using RetSim.Units.UnitStats;
using RetSimDesktop.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace RetSimDesktop.Model
{
    public class StatWeightWorker : BackgroundWorker
    {
        private static Thread[] threads = new Thread[Environment.ProcessorCount];
        private static StatWeightsSimExecuter[] simExecuter = new StatWeightsSimExecuter[Environment.ProcessorCount];

        public StatWeightWorker()
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

                var minDuration = retSimUIModel.EncounterSettings.MinFightDuration;
                var maxDuration = retSimUIModel.EncounterSettings.MaxFightDuration;

                Equipment playerEquipment = retSimUIModel.SelectedGear.GetEquipment();
                var talents = retSimUIModel.SelectedTalents.GetTalentList();
                var buffs = retSimUIModel.SelectedBuffs.GetBuffs();
                var debuffs = retSimUIModel.SelectedDebuffs.GetDebuffs();
                var consumables = retSimUIModel.SelectedConsumables.GetConsumables();

                float baseDps = 0;
                int baseSeed = RNG.global.Next();
                for (int i = 0; i < numberOfSimulations; i++)
                {
                    RNG.local = new(baseSeed + i);
                    FightSimulation fight = new(new Player("Brave Hero", Collections.Races[race.ToString()], shattrathFaction, playerEquipment, talents), new Enemy(Collections.Bosses[encounterID]), new EliteTactic(), buffs, debuffs, consumables, minDuration, maxDuration);
                    fight.Run();
                    baseDps += fight.CombatLog.DPS;
                    retSimUIModel.DisplayStatWeights[0].DpsDelta = baseDps / i;
                }
                baseDps /= numberOfSimulations;
                retSimUIModel.DisplayStatWeights[0].DpsDelta = baseDps;

                foreach (var item in retSimUIModel.DisplayStatWeights)
                {
                    if (item.Stat == StatName.Stamina || !item.EnabledForStatWeight)
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
                    }

                    simExecuter[freeThread] = new(Collections.Races[race.ToString()], shattrathFaction, Collections.Bosses[encounterID], playerEquipment, talents, buffs, debuffs, consumables,
                                minDuration, maxDuration, item, baseDps, numberOfSimulations, baseSeed);
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

    public class StatWeightsSimExecuter
    {
        private readonly Race race;
        private readonly ShattrathFaction shattrathFaction;
        private readonly Boss encounter;
        private readonly Equipment playerEquipment;
        private readonly List<Talent> talents;
        private readonly List<Spell> buffs;
        private readonly List<Spell> debuffs;
        private readonly List<Spell> consumables;
        private readonly int minFightDuration;
        private readonly int maxFightDuration;
        private readonly DisplayStatWeights statWeightsDisplay;
        private readonly float baseDps;
        public readonly int iterationCount;
        public readonly int baseSeed;

        public StatWeightsSimExecuter(Race race, ShattrathFaction shattrathFaction, Boss encounter, Equipment playerEquipment, List<Talent> talents, List<Spell> buffs, List<Spell> debuffs, List<Spell> consumables, int minFightDuration, int maxFightDuration, DisplayStatWeights statWeightsDisplay, float baseDps, int iterationCount, int baseSeed)
        {
            this.race = race;
            this.shattrathFaction = shattrathFaction;
            this.encounter = encounter;
            this.playerEquipment = playerEquipment;
            this.talents = talents;
            this.buffs = buffs;
            this.debuffs = debuffs;
            this.consumables = consumables;
            this.minFightDuration = minFightDuration;
            this.maxFightDuration = maxFightDuration;
            this.statWeightsDisplay = statWeightsDisplay;
            this.baseDps = baseDps;
            this.iterationCount = iterationCount;
            this.baseSeed = baseSeed;
        }

        public void Execute()
        {
            StatSet extraStats = new();
            extraStats[statWeightsDisplay.Stat] += statWeightsDisplay.IncreasedAmount;
            float dps = 0;
            for (int i = 0; i < iterationCount; i++)
            {
                RNG.local = new(baseSeed + i);
                FightSimulation fight = new(new Player("Brave Hero", race, shattrathFaction, playerEquipment, talents, extraStats), new Enemy(encounter), new EliteTactic(), buffs, debuffs, consumables, minFightDuration, maxFightDuration);
                fight.Run();
                dps += fight.CombatLog.DPS;
                statWeightsDisplay.DpsDelta = ((dps / i) - baseDps) / statWeightsDisplay.IncreasedAmount;
                if (statWeightsDisplay.DpsDelta != 0)
                {
                    statWeightsDisplay.StatPerDps = 1f / statWeightsDisplay.DpsDelta;
                }
                else
                {
                    statWeightsDisplay.StatPerDps = 0;
                }
            }
            dps /= iterationCount;
            statWeightsDisplay.DpsDelta = (dps - baseDps) / statWeightsDisplay.IncreasedAmount;
            if (statWeightsDisplay.DpsDelta != 0)
            {
                statWeightsDisplay.StatPerDps = 1f / statWeightsDisplay.DpsDelta;
            }
            else
            {
                statWeightsDisplay.StatPerDps = 0;
            }
        }
    }
}
