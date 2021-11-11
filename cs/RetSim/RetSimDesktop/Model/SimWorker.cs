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
using System.Linq;
using static RetSim.Data.Spells;

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

                var talents = SelectedTalents.GetTalentList(retSimUIModel);
                var buffs = new List<Spell> { WindfuryTotem, GreaterBlessingOfMight, GreaterBlessingOfKings, BattleShout, StrengthOfEarthTotem, GraceOfAirTotem, ManaSpringTotem, UnleashedRage,
                                          GiftOfTheWild, PrayerOfFortitude, PrayerOfSpirit, ArcaneBrilliance, InspiringPresence };
                var debuffs = new List<Spell> { ImprovedSealOfTheCrusader, ImprovedExposeArmor, ImprovedFaerieFire, CurseOfRecklessness, BloodFrenzy, ImprovedCurseOfTheElements, ImprovedShadowBolt, Misery,
                                        ShadowWeaving, ImprovedScorch, ImprovedHuntersMark, ExposeWeakness };

                float overallDPS = 0;
                List<FightSimulation> fightSimulations = new(numberOfSimulations);
                for (int i = 0; i < numberOfSimulations; i++)
                {
                    FightSimulation fight = new(new Player("Brave Hero", Races.Human, playerEquipment, talents), new Enemy("Magtheridon", CreatureType.Demon, ArmorCategory.Warrior), new EliteTactic(), buffs, debuffs, retSimUIModel.SimSettings.MinFightDuration, retSimUIModel.SimSettings.MaxFightDuration);
                    fight.Run();
                    overallDPS += fight.CombatLog.DPS;
                    if (i % (numberOfSimulations / 100 + 1) == 0)
                    {
                        retSimUIModel.CurrentSimOutput.Progress = (int)(i / ((float)numberOfSimulations) * 100);
                        retSimUIModel.CurrentSimOutput.DPS = overallDPS / (i + 1);
                    }
                    fightSimulations.Add(fight);
                }

                fightSimulations = fightSimulations.OrderBy(o => o.CombatLog.DPS).ToList();
                var minSimulation = fightSimulations[0];
                var medianSimulation = fightSimulations[(int)(numberOfSimulations / 2f)];
                var maxSimulation = fightSimulations[numberOfSimulations - 1];

                minSimulation.CombatLog.CreateDamageBreakdown();
                medianSimulation.CombatLog.CreateDamageBreakdown();
                maxSimulation.CombatLog.CreateDamageBreakdown();

                retSimUIModel.CurrentSimOutput.MinCombatLog = minSimulation.CombatLog;
                retSimUIModel.CurrentSimOutput.MedianCombatLog = medianSimulation.CombatLog;
                retSimUIModel.CurrentSimOutput.MaxCombatLog = maxSimulation.CombatLog;
                retSimUIModel.CurrentSimOutput.Progress = 100;
                retSimUIModel.CurrentSimOutput.DPS = overallDPS / numberOfSimulations;
                retSimUIModel.CurrentSimOutput.Min = minSimulation.CombatLog.DPS;
                retSimUIModel.CurrentSimOutput.Max = maxSimulation.CombatLog.DPS;
            }
        }
    }
}
