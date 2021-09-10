using RetSim.EventQueues;
using RetSim.Events;
using RetSim.Log;
using RetSim.Tactics;
using System.Collections.Generic;
using static RetSim.Program;

namespace RetSim
{
    public class FightSimulation
    {
        public readonly Player Player;
        public readonly Enemy Enemy;
        public readonly Tactic Tactic;
        public readonly List<Spell> Buffs;

        public readonly CombatLog CombatLog;
        public readonly IEventQueue Queue;
        public int Timestamp { get; set; }
        public bool Ongoing { get; set; }

        public readonly int Duration;

        public FightSimulation(Player player, Enemy enemy, Tactic tactic, List<Spell> buffs, int minDuration, int maxDuration)
        {
            Player = player;
            Enemy = enemy;
            Tactic = tactic;
            Buffs = buffs;

            CombatLog = new CombatLog();
            Queue = new MinQueue();

            Timestamp = 0;
            Ongoing = true;

            Duration = RNG.RollRange(minDuration, maxDuration);
        }

        public CombatLog Run()
        {
            foreach (Spell spell in Player.Equipment.Spells)
            {
                Queue.Add(new CastEvent(spell, this, Timestamp, -3));
            }

            foreach (Talent talent in Player.Talents)
            {
                Queue.Add(new CastEvent(talent, this, Timestamp, -2));
            }

            foreach (Spell buff in Buffs)
            {
                Queue.Add(new CastEvent(buff, this, Timestamp, -1));
            }

            Queue.AddRange(Tactic.PreFight(this));
            Queue.Add(new SimulationEndEvent(this, Duration));

            while (Ongoing)
            {
                int nextTimestamp = Duration;

                if (!Queue.IsEmpty())
                {
                    Queue.EnsureSorting();
                    Event curent = Queue.RemoveNext();
                    Timestamp = curent.Timestamp;

                    ProcMask mask = curent.Execute();
                    Player.CheckForProcs(mask, this);

                    //Logger.Log(Timestamp + ": Event: " + curent.ToString());
                    
                    if (!Queue.IsEmpty())
                    {
                        Queue.EnsureSorting();
                        nextTimestamp = Queue.GetNext().Timestamp;
                        
                        if (Timestamp == nextTimestamp) 
                            continue;
                    }
                }

                Queue.Add(Tactic.GetActionBetween(Timestamp, nextTimestamp, this));
            }

            return CombatLog;
        }

        public void Output()
        {
            
            foreach (LogEntry entry in CombatLog.Log)
                Logger.Log(entry.ToString());

            Logger.Log($"\nPlayer stats: {Player.Stats[StatName.AttackPower].Value} AP / {Player.Stats[StatName.CritChance].Value.Rounded()}% Crit / {Player.Stats[StatName.HitChance].Value.Rounded()}% Hit / {Player.Stats[StatName.Expertise].Value} Expertise");

            Logger.Log($"\nDuration - Expected: {Duration} / Real: {Timestamp}\n");     

            Logger.Log($"╔════════════════════╦════════════════╦════════════════╦═════╦═════════════╦═════════════╦═════════════╦═════════════╗");
            Logger.Log($"║    Ability Name    ║ #   Damage   % ║ #    DPS     % ║  #  ║ #  Crit   % ║ #   Hit   % ║ #  Miss   % ║ #  Dodge  % ║");
            Logger.Log($"╠════════════════════╬════════════════╬════════════════╬═════╬═════════════╬═════════════╬═════════════╬═════════════╣");

            foreach (string s in CombatLog.DamageBreakdown.Keys)
            {
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

                Logger.Log($"║ {s, -18} ║ {damage, -6} {(damage * 100f / CombatLog.Damage).Rounded(),6}% ║ {dps.Rounded(), -6} {(dps / CombatLog.DPS * 100).Rounded(), 6}% ║ {count, 3} ║ {crit, -3} {(crit / hit * 100).Rounded(), 6}% ║ {hit, -3} {(hit / count * 100).Rounded(),6}% ║ {miss, -3} {(miss / count * 100).Rounded(),6}% ║ {dodge, -3} {(dodge / count * 100).Rounded(), 6}% ║");
                
            }

            Logger.Log($"╠════════════════════╬════════════════╬════════════════╬═════╩═════════════╩═════════════╩═════════════╩═════════════╝");
            Logger.Log($"║       Totals       ║ {CombatLog.Damage,-6} {"Damage", 7} ║ {CombatLog.DPS.Rounded(), -7} {"DPS", 6} ║");
            Logger.Log($"╚════════════════════╩════════════════╩════════════════╝");

        }
    }
}