using RetSim.EventQueues;
using RetSim.Events;
using RetSim.Log;
using RetSim.Tactics;
using static RetSim.Program;

namespace RetSim
{
    public class FightSimulation
    {
        public readonly Player Player;
        public readonly Enemy Enemy;
        public readonly Tactic Tactic;

        public readonly CombatLog CombatLog;
        public readonly IEventQueue Queue;
        public int Timestamp { get; set; }

        public readonly int Duration;

        public FightSimulation(Player player, Enemy enemy, Tactic tactic, int minDuration, int maxDuration)
        {
            Player = player;
            Enemy = enemy;
            Tactic = tactic;

            CombatLog = new CombatLog();
            Queue = new MinQueue();

            Timestamp = 0;

            Duration = RNG.RollRange(minDuration, maxDuration);
        }

        public CombatLog Run()
        {
            Player.Procs.Add(Glossaries.Procs.DragonspineTrophy);
            Player.Cast(Glossaries.Spells.WindfuryTotem, this);
            //TODO add auras from player equipment

            Queue.AddRange(Tactic.PreFight(this));

            while (Timestamp <= Duration)
            {
                int nextTimestamp = Duration;

                if (!Queue.IsEmpty())
                {
                    Queue.EnsureSorting();
                    Event curent = Queue.RemoveNext();
                    Timestamp = curent.Timestamp;

                    ProcMask mask = curent.Execute();
                    Player.CheckForProcs(mask, this);

                    Logger.Log(Timestamp + ": Event: " + curent.ToString());

                    if (!Queue.IsEmpty())
                    {
                        Queue.EnsureSorting();
                        nextTimestamp = Queue.GetNext().Timestamp;
                        if (Timestamp == nextTimestamp) continue;
                    }
                }

                Queue.Add(Tactic.GetActionBetween(Timestamp, nextTimestamp, this));
            }

            return CombatLog;
        }

        public void Output()
        {
            Logger.Log($"\nPlayer stats: {Player.Stats.AttackPower} AP / {Player.Stats.CritChance.Rounded()}% Crit / {Player.Stats.HitChance.Rounded()}% Hit / {Player.Stats.Expertise} Expertise");

            Logger.Log($"\nExpected duration: {Duration}");
            Logger.Log($"Real duration: {Timestamp}\n");

            foreach (LogEntry entry in CombatLog.Log)
                Logger.Log(entry.ToString());

            Logger.Log($"\nTotal DPS: {CombatLog.DPS.Rounded()}");
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

                Logger.Log($"DPS: {dps.Rounded()} - {(dps / CombatLog.DPS * 100).Rounded()}%");
                Logger.Log($"Damage: {damage} - {(damage * 100f / CombatLog.Damage).Rounded()}%");
                Logger.Log($"{count} Casts: {crit} Crit ({(crit / hit * 100).Rounded()}%) / {hit} Hit ({(hit / count * 100).Rounded()}%) / {miss} Miss ({(miss / count * 100).Rounded()}%) / {dodge} Dodge ({(dodge / count * 100).Rounded()}%)");

            }
        }

    }
}