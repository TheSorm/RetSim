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

                var minDuration = input.Item1.EncounterSettings.MinFightDuration;
                var maxDuration = input.Item1.EncounterSettings.MaxFightDuration;

                var talents = input.Item1.SelectedTalents.GetTalentList();
                var buffs = input.Item1.SelectedBuffs.GetBuffs();
                var debuffs = input.Item1.SelectedDebuffs.GetDebuffs();
                var consumables = input.Item1.SelectedConsumables.GetConsumables();

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

                    simExecuter[freeThread] = new(Collections.Races[race.ToString()], shattrathFaction, Collections.Bosses[encounterID], playerEquipment, talents, buffs, debuffs, consumables,
                                minDuration, maxDuration, numberOfSimulations, item);
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

    public class GearSimExecuter
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
        public readonly int numberOfSimulations;
        private readonly DisplayGear item;

        public GearSimExecuter(Race race, ShattrathFaction shattrathFaction, Boss encounter, Equipment playerEquipment, List<Talent> talents, List<Spell> buffs, List<Spell> debuffs, List<Spell> consumables, int minFightDuration, int maxFightDuration, int numberOfSimulations, DisplayGear item)
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
            this.numberOfSimulations = numberOfSimulations;
            this.item = item;
        }

        public void Execute()
        {
            float overallDPS = 0;
            for (int i = 0; i < numberOfSimulations; i++)
            {
                FightSimulation fight = new(new Player("Brave Hero", race, shattrathFaction, playerEquipment, talents), new Enemy(encounter), new EliteTactic(), buffs, debuffs, consumables, minFightDuration, maxFightDuration, new List<Spell>(), new List<int>()); //TODO: Add cooldowns + heroism timings
                fight.Run();
                overallDPS += fight.CombatLog.DPS;
                item.DPS = overallDPS / (i + 1);
            }
            item.DPS = overallDPS / numberOfSimulations;
        }
    }
}
