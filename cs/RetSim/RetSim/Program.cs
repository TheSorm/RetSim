global using System;
global using System.Collections.Generic;
using RetSim.Data;
using RetSim.Items;
using RetSim.Misc;
using RetSim.Misc.Loggers;
using RetSim.Simulation;
using RetSim.Simulation.Tactics;
using RetSim.Spells;
using RetSim.Units.Enemy;
using RetSim.Units.Player;
using RetSim.Units.Player.Static;
using RetSim.Units.UnitStats;
using System.Diagnostics;
using System.Linq;

namespace RetSim;

class Program
{
    public static AbstractLogger Logger = new ConsoleLogger();

    static Equipment Equipment;
    static List<Talent> Talents;
    static List<Spell> Buffs, Debuffs, Consumables, Cooldowns;
    static List<int> Heroisms;
    static string Name = "Brave Hero";
    static Race Race;
    static ShattrathFaction Faction = ShattrathFaction.Aldor;

    static Boss Boss;

    static int MinDuration = 180000;
    static int MaxDuration = 200000;


    static void Main(string[] args)
    {
        Manager.InstantiateData();

        Equipment = Manager.GetEquipment();

        Talents = Talent.GetTalents(20105, 25957, 20121, 31868, 20113, 20218, 31870, 20059, 35397, 31883, 20193, 20266);
        Buffs = Spell.GetSpells(25392, 14767, 25570, 27127, 32999, 26991, 17055, 28878, 30811, 27141, 20048, 25898, 25580, 29193, 25359, 25528, 16295, 2048, 12861);
        Debuffs = Spell.GetSpells(17800, 15258, 27228, 32484, 33200, 34501, 14325, 30070, 27159, 20337, 26993, 33602, 27226, 26866, 14169);
        Consumables = Spell.GetSpells(28520, 33256, 33082, 33077, 35476, 23060);

        Cooldowns = Spell.GetSpells(31884, 28507, 30486);
        Heroisms = new List<int> { 1400 };

        Race = Collections.Races["Human"];
        Boss = Collections.Bosses.First(x => x.Name == "Magtheridon");


        Logger.Log("Press Enter to run a single, detailed sim, or any other key to run many, non-detailed sims.");

        bool once = Console.ReadKey(false).Key == ConsoleKey.Enter;

        Console.Clear();

        Logger.DisableInput();

        if (once)
            RunOnce();

        else
            RunMany();

        //PrintEquipment(equipment);

        Logger.EnableInput();
    }

    static void PrintEquipment()
    {
        Logger.Log("");

        Logger.Log($"╔═══════════╦═══════╦═══════════════════════════════╦═════════════════════════════════════════════════════════════════════╗");
        Logger.Log($"║   Slot    ║  ID   ║             Item              ║                              Gems                                   ║");
        Logger.Log($"╠═══════════╬═══════╬═══════════════════════════════╬═══╦═════════════════════════════════════════════════════════════════╣");

        foreach (EquippableItem item in Equipment.PlayerEquipment)
        {
            if (item != null)
                Logger.Log($"{item}");
        }

        Logger.Log($"╚═══════════╩═══════╩═══════════════════════════════╩═══╩═════════════════════════════════════════════════════════════════╝");
    }

    static void RunOnce()
    {
        FightSimulation fight = new(new Player(Name, Race, Faction, Equipment, Talents, null), new Enemy(Boss), new EliteTactic(), Buffs, Debuffs, Consumables, MinDuration, MaxDuration, Cooldowns, Heroisms);

        PrintStats(fight.Player.Stats.All);

        fight.Run();

        fight.Output();
    }

    static void RunMany()
    {
        float iterations = 10000;
        int outerIterations = 10;

        (float AverageDPS, TimeSpan TimeSpan)[] results = new (float, TimeSpan)[outerIterations];


        float dps = 0;
        TimeSpan time = TimeSpan.Zero;

        float maxDPS = 0;
        float minDPS = float.MaxValue;

        TimeSpan maxTime = TimeSpan.Zero;
        TimeSpan minTime = TimeSpan.MaxValue;

        Logger.Log($"╔═════╦═══════════════╦════════╦════════════╦══════════════════╗");
        Logger.Log($"║  #  ║   Progress    ║   %    ║    DPS     ║   Elapsed Time   ║");
        Logger.Log($"╠═════╬═══════════════╬════════╬════════════╬══════════════════╣");

        for (int i = 0; i < outerIterations; i++)
        {
            Logger.Log($"║ {i + 1,3} ║ {$"0/{iterations}",13} ║     0% ║          0 ║ 00:00:00.0000000 ║");
        }

        Logger.Log($"╠═════╩═══════════════╩════════╬════════════╬══════════════════╣");
        Logger.Log($"║           Average            ║          0 ║ 00:00:00.0000000 ║");
        Logger.Log($"╠══════════════════════════════╬════════════╬══════════════════╣");
        Logger.Log($"║           Maximum            ║          0 ║ 00:00:00.0000000 ║");
        Logger.Log($"╠══════════════════════════════╬════════════╬══════════════════╣");
        Logger.Log($"║          Mininimum           ║          0 ║ 00:00:00.0000000 ║");
        Logger.Log($"╚══════════════════════════════╩════════════╩══════════════════╝");

        for (int i = 0; i < outerIterations; i++)
        {
            results[i] = Run(iterations, i, false);

            dps += results[i].AverageDPS;
            time += results[i].TimeSpan;

            if (results[i].AverageDPS > maxDPS)
            {
                maxDPS = results[i].AverageDPS;
            }

            if (results[i].AverageDPS < minDPS)
            {
                minDPS = results[i].AverageDPS;
            }

            if (results[i].TimeSpan.TotalMilliseconds > maxTime.TotalMilliseconds)
            {
                maxTime = results[i].TimeSpan;
            }

            if (results[i].TimeSpan.TotalMilliseconds < minTime.TotalMilliseconds)
            {
                minTime = results[i].TimeSpan;
            }
        }

        Logger.Log($"╠═════╩═══════════════╩════════╬════════════╬══════════════════╣");
        Logger.Log($"║           Average            ║ {dps / outerIterations,10} ║ {time / outerIterations,16} ║");
        Logger.Log($"╠══════════════════════════════╬════════════╬══════════════════╣");
        Logger.Log($"║           Maximum            ║ {maxDPS,10} ║ {maxTime,16} ║");
        Logger.Log($"╠══════════════════════════════╬════════════╬══════════════════╣");
        Logger.Log($"║          Mininimum           ║ {minDPS,10} ║ {minTime,16} ║");
        Logger.Log($"╚══════════════════════════════╩════════════╩══════════════════╝");
    }

    static (float AverageDPS, TimeSpan span) Run(float iterations, int outer, bool log)
    {
        var dps = 0f;
        var row = outer + 3;

        Stopwatch watch = new();

        watch.Start();

        for (int i = 0; i < iterations; i++)
        {
            FightSimulation fight = new(new Player(Name, Race, Faction, Equipment, Talents, null), new Enemy(Boss), new EliteTactic(), Buffs, Debuffs, Consumables, MinDuration, MaxDuration, Cooldowns, Heroisms);

            fight.Run();

            var index = i + 1;

            var progress = Math.Round(index / iterations * 100, 2);

            dps += fight.CombatLog.DPS;

            Console.SetCursorPosition(0, row);
            Logger.Log($"║ {outer + 1,3} ║ {$"{index}/{iterations}",13} ║ {progress,5}% ║ {dps / index,10} ║ {watch.Elapsed,16} ║");
        }

        watch.Stop();

        return (dps / iterations, watch.Elapsed);
    }

    static void PrintStats(Stat[] stats)
    {
        Logger.Log($"╔══════════════════╦═══════╦══════╦═══════╦═══════╦═══════╦═══════╦═══════╗");
        Logger.Log($"║    Stat Name     ║ Total ║ Mod  ║ Base  ║ Gear  ║ Buffs ║ Ratng ║ Suprt ║");
        Logger.Log($"╠══════════════════╬═══════╬══════╬═══════╬═══════╬═══════╬═══════╬═══════╣");

        foreach (Stat stat in stats)
        {
            if (stat != null)
                Logger.Log($"║ {stat.Name,-16} ║ {$"{stat.Value.Rounded()}",5} ║ {stat.Modifier.Rounded(),4} ║ {stat.Race.Rounded(),5} ║ {stat.Gear.Rounded(),5} ║ {stat.Bonus.Rounded(),5} ║ {stat.RatingValue.Rounded(),5} ║ {stat.SupportValue.Rounded(),5} ║");
        }

        Logger.Log($"╚══════════════════╩═══════╩══════╩═══════╩═══════╩═══════╩═══════╩═══════╝");
    }
}