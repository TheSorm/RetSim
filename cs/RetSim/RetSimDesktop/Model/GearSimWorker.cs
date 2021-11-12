using RetSim.Items;
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
using System.Linq;

namespace RetSimDesktop.View
{
    public class GearSim : BackgroundWorker
    {
        public GearSim()
        {
            DoWork += BackgroundWorker_DoWork;
        }

        static void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            if (e.Argument is (RetSimUIModel, List<ItemDPS>, int))
            {
                Tuple<RetSimUIModel, IEnumerable<ItemDPS>, int> input = (Tuple<RetSimUIModel, IEnumerable<ItemDPS>, int>) e.Argument;
                foreach (var item in input.Item2)
                {
                    Equipment playerEquipment = SelectedGear.GetEquipment(input.Item1);

                    playerEquipment.PlayerEquipment[input.Item3] = item.Item;

                    var numberOfSimulations = input.Item1.SimSettings.SimulationCount;

                    var talents = SelectedTalents.GetTalentList(input.Item1);
                    var buffs = Spell.GetSpells(25580, 27141, 25898, 2048, 25528, 25359, 25570, 30811, 26991, 25392, 32999, 27127, 28878);
                    var debuffs = Spell.GetSpells(20337, 14169, 33602, 27226, 30070, 32484, 17800, 33200, 15258, 22959, 14325, 34501);

                    float overallDPS = 0;
                    for (int i = 0; i < numberOfSimulations; i++)
                    {
                        FightSimulation fight = new(
                            new Player("Brave Hero", RetSim.Data.Collections.Races["Human"], playerEquipment, talents), 
                            new Enemy("Magtheridon", CreatureType.Demon, ArmorCategory.Warrior), 
                            new EliteTactic(), buffs, debuffs, input.Item1.SimSettings.MinFightDuration, input.Item1.SimSettings.MaxFightDuration);
                        fight.Run();
                        overallDPS += fight.CombatLog.DPS;
                        if (i % (numberOfSimulations / 100 + 1) == 0)
                        {
                            item.DPS = overallDPS / (i + 1);
                        }
                    }
                    item.DPS = overallDPS / numberOfSimulations;
                }
                input.Item1.SimButtonStatus.IsGearSimButtonEnabled = true;
            }
        }
    }
}
