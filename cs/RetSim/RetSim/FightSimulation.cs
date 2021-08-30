using RetSim.Events;
using RetSim.Tactics;
using RetSim.Log;
using System.Collections.Generic;
using static RetSim.Program;

namespace RetSim
{
    public class FightSimulation
    {
        public readonly Player Player;
        public readonly Enemy Enemy;
        public readonly Tactic Tactic;

        public readonly CombatLog CombatLog;
        public readonly EventQueue Queue;
        public int Timestamp { get; set; }

        public readonly int Duration;

        public FightSimulation(Player player, Enemy enemy, Tactic tactic, int minDuration, int maxDuration)
        {
            Player = player;
            Enemy = enemy;
            Tactic = tactic;

            CombatLog = new CombatLog();
            Queue = new EventQueue();

            Timestamp = 0;

            Duration = RNG.RollRange(minDuration, maxDuration);
        }

        public CombatLog Run()
        {
            Player.Procs.Add(Glossaries.Procs.DragonspineTrophy);

            Queue.AddRange(Tactic.PreFight(this));

            while (Timestamp <= Duration)
            {
                int nextTimestamp = Duration;

                if (!Queue.IsEmpty())
                {
                    Queue.Sort();
                    Event curent = Queue.GetNext();
                    Queue.RemoveNext();
                    Timestamp = curent.Timestamp;

                    List<Event> results = new();
                    ProcMask mask = curent.Execute();
                    Player.CheckForProcs(mask, this);
                    Queue.AddRange(results);

                    Logger.Log(Timestamp + ": Event: " + curent.ToString());

                    if (!Queue.IsEmpty())
                    {
                        Queue.Sort();
                        nextTimestamp = Queue.GetNext().Timestamp;
                        if (Timestamp == nextTimestamp) continue;
                    }
                }

                Event playerAction = Tactic.GetActionBetween(Timestamp, nextTimestamp, this);

                if (playerAction != null)
                    Queue.Add(playerAction);
            }

            return CombatLog;
        }

        public void Output()
        {
            Logger.Log($"\nPlayer stats: {Player.Stats.AttackPower} AP / {Player.Stats.CritChance}% Crit / {Player.Stats.HitChance}% Hit / {Player.Stats.Expertise} Expertise");

            Logger.Log($"\nExpected duration: {Duration}");
            Logger.Log($"Real duration: {Timestamp}\n");

            foreach (string s in CombatLog.Log)
                Logger.Log(s);

            Logger.Log($"\nTotal DPS: {CombatLog.DPS}");
            Logger.Log($"Total Damage: {CombatLog.Damage}");

            foreach (string s in CombatLog.DamageBreakdown.Keys)
            {
                Logger.Log($"\nAbility: {s}");

                float count = CombatLog.DamageBreakdown[s].Count;
                float miss = 0;
                float dodge = 0;
                float crit = 0;

                int damage = 0;

                foreach (DamageEntry entry in CombatLog.DamageBreakdown[s])
                {
                    damage += entry.Damage;

                    if (entry.AttackResult == AttackResult.Miss)
                        miss++;

                    if (entry.AttackResult == AttackResult.Dodge)
                        dodge++;

                    if (entry.Crit)
                        crit++;
                }

                float dps = (float)damage / CombatLog.Duration * 1000;
                float hit = count - miss - dodge;

                Logger.Log($"DPS: {dps} - {dps / CombatLog.DPS * 100}%");
                Logger.Log($"Damage: {damage} - {damage / CombatLog.Damage * 100}%");
                Logger.Log($"{count} Casts: {crit} Crit ({crit / count * 100}%) / {hit} Hit ({hit / count * 100}%) / {miss} Miss ({miss / count * 100}%) / {dodge} Dodge ({dodge / count * 100}%)");

            }
        }

    }
}