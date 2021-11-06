using RetSim.Simulation;
using RetSim.Simulation.Tactics;
using RetSim.Spells;
using RetSim.Units.Enemy;
using RetSim.Units.Player;
using RetSim.Units.Player.Static;
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

        static void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            RetSimUIModel retSimUIModel = e.Argument as RetSimUIModel;
            Equipment playerEquipment = new()
            {
                Head = retSimUIModel.SelectedGear.SelectedHead,
                Neck = retSimUIModel.SelectedGear.SelectedNeck,
                Shoulders = retSimUIModel.SelectedGear.SelectedShoulders,
                Back = retSimUIModel.SelectedGear.SelectedBack,
                Chest = retSimUIModel.SelectedGear.SelectedChest,
                Wrists = retSimUIModel.SelectedGear.SelectedWrists,
                Hands = retSimUIModel.SelectedGear.SelectedHands,
                Waist = retSimUIModel.SelectedGear.SelectedWaist,
                Legs = retSimUIModel.SelectedGear.SelectedLegs,
                Feet = retSimUIModel.SelectedGear.SelectedFeet,
                Finger1 = retSimUIModel.SelectedGear.SelectedFinger1,
                Finger2 = retSimUIModel.SelectedGear.SelectedFinger2,
                Trinket1 = retSimUIModel.SelectedGear.SelectedTrinket1,
                Trinket2 = retSimUIModel.SelectedGear.SelectedTrinket2,
                Relic = retSimUIModel.SelectedGear.SelectedRelic,
                Weapon = retSimUIModel.SelectedGear.SelectedWeapon,
            };

            var talents = GetSelectedTalentList(retSimUIModel);
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

        public static List<Talent> GetSelectedTalentList(RetSimUIModel retSimUIModel)
        {
            List<Talent> talents = new();
            if (retSimUIModel.SelectedTalents.ConvictionEnabled)
            {
                talents.Add(Conviction);
            }
            if (retSimUIModel.SelectedTalents.CrusadeEnabled)
            {
                talents.Add(Crusade);
            }
            if (retSimUIModel.SelectedTalents.DivineStrengthEnabled)
            {
                talents.Add(DivineStrength);
            }
            if (retSimUIModel.SelectedTalents.FanaticismEnabled)
            {
                talents.Add(Fanaticism);
            }
            if (retSimUIModel.SelectedTalents.ImprovedSanctityAuraEnabled)
            {
                talents.Add(ImprovedSanctityAura);
            }
            if (retSimUIModel.SelectedTalents.PrecisionEnabled)
            {
                talents.Add(Precision);
            }
            if (retSimUIModel.SelectedTalents.SanctifiedSealsEnabled)
            {
                talents.Add(SanctifiedSeals);
            }
            if (retSimUIModel.SelectedTalents.SanctityAuraEnabled)
            {
                talents.Add(SanctityAura);
            }
            if (retSimUIModel.SelectedTalents.TwoHandedWeaponSpecializationEnabled)
            {
                talents.Add(TwoHandedWeaponSpecialization);
            }
            if (retSimUIModel.SelectedTalents.VengeanceEnabled)
            {
                talents.Add(Vengeance);
            }

            return talents;
        }
    }
}
