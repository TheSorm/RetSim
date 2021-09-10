using RetSim.Items;
using RetSim.Loggers;
using RetSim.Tactics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static RetSim.Data.Spells;

namespace RetSim
{
    class Program
    {
        public static AbstractLogger Logger = new ConsoleLogger();

        static void Main(string[] args)
        {
            var equipment = Data.Importer.GetEquipment();

            var talents = new List<Talent> { ImprovedSealOfTheCrusader, Conviction, Crusade, TwoHandedWeaponSpecialization, SanctityAura, ImprovedSanctityAura, Vengeance, Fanaticism, SanctifiedSeals, Precision, DivineStrength };
            var buffs = new List<Spell> { WindfuryTotem, GreaterBlessingOfMight, GreaterBlessingOfKings, BattleShout, StrengthOfEarthTotem, GraceOfAirTotem, ManaSpringTotem, UnleashedRage,
                                            GiftOfTheWild, PrayerOfFortitude, PrayerOfSpirit, ArcaneBrilliance, InspiringPresence };

            RunOnce(equipment, talents, buffs);
            //RunMany(equipment, talents, buffs);

            PrintEquipment(equipment);            
        }

        static void PrintEquipment(Equipment equipment)
        {
            Logger.Log("");

            Logger.Log($"╔═══════════╦═══════╦═══════════════════════════╦══════╦═════════════════════════════════════════════════════════════╗");
            Logger.Log($"║   Slot    ║  ID   ║           Item            ║ Gems ║                          Gem Names                          ║");
            Logger.Log($"╠═══════════╬═══════╬═══════════════════════════╬══════╬═════════════════════════════════════════════════════════════╣");

            //Logger.Log($"╔{"".PadRight(11, '═')}╦{"".PadRight(7, '═')}╦{"".PadRight(27, '═')}╦{"".PadRight(6, '═')}╦{"".PadRight(61, '═')}╗");
            //Logger.Log($"║ {"",-9} ║ {"",-5} ║ {"Item",-25} ║ {"Gems", -4} ║ {"Gem Names", -59} ║");
            //Logger.Log($"╠{"".PadRight(11, '═')}╬{"".PadRight(7, '═')}╬{"".PadRight(27, '═')}╬{"".PadRight(6, '═')}╬{"".PadRight(61, '═')}╣");

            foreach (EquippableItem item in equipment.PlayerEquipment)
            {
                if (item != null)
                    Logger.Log($"{item}");
            }

            Logger.Log($"╚═══════════╩═══════╩═══════════════════════════╩══════╩═════════════════════════════════════════════════════════════╝");
        }

        static void RunOnce(Equipment equipment, List<Talent> talents, List<Spell> buffs)
        {
            FightSimulation fight = new(new Player(Races.Human, equipment, talents), new Enemy(Armor.Warrior, CreatureType.Demon), new EliteTactic(), buffs, 180000, 200000);
            //TODO: Add human racial tho

            fight.Run();

            fight.Output();
        }

        static void RunMany(Equipment equipment, List<Talent> talents, List<Spell> buffs)
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

            for (int i = 0; i < outerIterations; i++)
            {
                results[i] = Run(iterations, equipment, talents, buffs, i, false);

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

            Logger.Log($"DPS  - Average: {(dps / outerIterations)} / Max: {maxDPS} / Min: {minDPS}");
            Logger.Log($"Time - Average: {(time / outerIterations)} / Max: {maxTime} / Min: {minTime}");
        }

        static (float AverageDPS, TimeSpan span) Run(float iterations, Equipment equipment, List<Talent> talents, List<Spell> buffs, int outer, bool log)
        {
            var dps = 0f;

            Stopwatch watch = new Stopwatch();
            watch.Start();

            for (int i = 0; i < iterations; i++)
            {
                FightSimulation fight = new(new Player(Races.Human, equipment, talents), new Enemy(Armor.Warrior, CreatureType.Demon), new EliteTactic(), buffs, 180000, 190000);
                //TODO: Add human racial tho

                fight.Run();

                //var index = i + 1;

                //var progress = Math.Round(index / iterations * 100, 2);

                //Console.SetCursorPosition(0, outer);

                //Console.WriteLine($"Iteration #{outer + 1} - Progress: {index}/{iterations} / {progress}%");

                dps += fight.CombatLog.DPS;
            }

            watch.Stop();

            if (log)
            {
                Logger.Log($"Average DPS: {(dps / iterations)}");
                Logger.Log($"Elapsed time: {watch.Elapsed }");
            }

            return (dps / iterations, watch.Elapsed);
        }
    }
}
