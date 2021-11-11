using RetSim.Simulation;
using RetSim.Simulation.Tactics;
using RetSim.Spells;
using RetSim.Units.Enemy;
using RetSim.Units.Player;
using RetSim.Units.Player.Static;
using RetSimDesktop.Model;
using RetSimDesktop.ViewModel;
using System.Collections.Generic;
using System.ComponentModel;
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

                var talents = SelectedTalents.GetTalentList(retSimUIModel);
                var buffs = new List<Spell> { WindfuryTotem, GreaterBlessingOfMight, GreaterBlessingOfKings, BattleShout, StrengthOfEarthTotem, GraceOfAirTotem, ManaSpringTotem, UnleashedRage,
                                          GiftOfTheWild, PrayerOfFortitude, PrayerOfSpirit, ArcaneBrilliance, InspiringPresence };
                var debuffs = new List<Spell> { ImprovedSealOfTheCrusader, ImprovedExposeArmor, ImprovedFaerieFire, CurseOfRecklessness, BloodFrenzy, ImprovedCurseOfTheElements, ImprovedShadowBolt, Misery,
                                        ShadowWeaving, ImprovedScorch, ImprovedHuntersMark, ExposeWeakness };

                float overallDPS = 0;
                List<FightSimulation> fightSimulations = new(10000);
                for (int i = 0; i < 10000; i++)
                {
                    FightSimulation fight = new(new Player("Brave Hero", Races.Human, playerEquipment, talents), new Enemy("Magtheridon", CreatureType.Demon, ArmorCategory.Warrior), new EliteTactic(), buffs, debuffs, 180000, 200000);
                    fight.Run();
                    overallDPS += fight.CombatLog.DPS;
                    if (i % 100 == 0)
                    {
                        retSimUIModel.CurrentSimOutput.Progress = (int)(i / 10000f * 100);
                        retSimUIModel.CurrentSimOutput.DPS = overallDPS / ((float)i);
                    }
                    fightSimulations.Add(fight);
                }

                fightSimulations = fightSimulations.OrderBy(o => o.CombatLog.DPS).ToList();

                retSimUIModel.CurrentSimOutput.MinCombatLog = fightSimulations[0].CombatLog.Log;
                retSimUIModel.CurrentSimOutput.MedianCombatLog = fightSimulations[4999].CombatLog.Log;
                retSimUIModel.CurrentSimOutput.MaxCombatLog = fightSimulations[9999].CombatLog.Log;
                retSimUIModel.CurrentSimOutput.Progress = 100;
                retSimUIModel.CurrentSimOutput.DPS = overallDPS / 10000f;
                retSimUIModel.CurrentSimOutput.Min = fightSimulations[0].CombatLog.DPS;
                retSimUIModel.CurrentSimOutput.Max = fightSimulations[9999].CombatLog.DPS;
            }
        }
    }
}
