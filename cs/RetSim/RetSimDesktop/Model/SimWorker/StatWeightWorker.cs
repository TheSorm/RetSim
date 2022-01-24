using RetSim.Data;
using RetSim.Misc;
using RetSim.Simulation;
using RetSim.Simulation.Tactics;
using RetSim.Units.Enemy;
using RetSim.Units.Player;
using RetSim.Units.Player.Static;
using RetSim.Units.UnitStats;
using RetSimDesktop.Model.SimWorker;
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
                var maxCSDelay = retSimUIModel.SimSettings.MaxCSDelay;

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
                        new EliteTactic(maxCSDelay), groupTalents, buffs, debuffs, consumables, minDuration, maxDuration, cooldowns, heroismUsage);
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
                        BaseSeed = baseSeed,
                        BaseDps = baseDps,
                        StatWeightsDisplay = item,
                        IgnoreExpertiseCap = item.IgnoreExpertiseCap
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

    public class StatWeightsSimExecuter : SimExecuter
    {
        public DisplayStatWeights StatWeightsDisplay { get; init; } = new();
        public float BaseDps { get; init; }
        public int BaseSeed { get; init; }
        public bool IgnoreExpertiseCap { get; init; }

        private const float ExpertiseDecrease = 8f;

        public override void Execute()
        {
            float dps = 0;
            float delta = 0;            
            float increase = StatWeightsDisplay.IncreasedAmount;

            StatSet extraStats = new();
            extraStats[StatWeightsDisplay.Stat] += StatWeightsDisplay.IncreasedAmount;
            
            for (int i = 0; i < NumberOfSimulations; i++)
            {
                RNG.local = new(BaseSeed + i);

                FightSimulation fight = new(new Player("Brave Hero", Race, ShattrathFaction, PlayerEquipment, Talents, extraStats), new Enemy(Encounter), new EliteTactic(MaxCSDelay), GroupTalents, Buffs, Debuffs, Consumables, MinFightDuration, MaxFightDuration, Cooldowns, HeroismUsage);
                
                if (IgnoreExpertiseCap && fight.Player.Stats[StatName.Expertise].Value > Constants.Stats.ExpertiseCap) 
                {
                    fight.Player.Stats[StatName.ExpertiseRating].Permanent = 0;
                    fight.Player.Stats[StatName.ExpertiseRating].Bonus = 0;
                    fight.Player.Stats[StatName.ExpertiseRating].Update();
                    fight.Player.Stats[StatName.Expertise].Permanent = 0;
                    fight.Player.Stats[StatName.Expertise].Bonus = Constants.Stats.ExpertiseCap - ExpertiseDecrease;
                    fight.Player.Stats[StatName.Expertise].Update();

                    increase = -Constants.Ratings.Expertise * ExpertiseDecrease;
                }

                fight.Run();

                dps += fight.CombatLog.DPS;

                delta = ((dps / i) - BaseDps) / increase;

                UpdateInterface(delta);
            }

            dps /= NumberOfSimulations;

            delta = (dps - BaseDps) / increase;

            UpdateInterface(delta);
        }

        private void UpdateInterface(float delta)
        {
            StatWeightsDisplay.DpsDelta = delta;

            if (StatWeightsDisplay.DpsDelta > 0)
                StatWeightsDisplay.StatPerDps = 1f / StatWeightsDisplay.DpsDelta;

            else
                StatWeightsDisplay.StatPerDps = 0;
        }
    }
}
