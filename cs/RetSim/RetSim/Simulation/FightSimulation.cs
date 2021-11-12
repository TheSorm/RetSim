using RetSim.Misc;
using RetSim.Simulation.CombatLogEntries;
using RetSim.Simulation.EventQueues;
using RetSim.Simulation.Events;
using RetSim.Simulation.Tactics;
using RetSim.Spells;
using RetSim.Units.Enemy;
using RetSim.Units.Player;

using static RetSim.Program;

namespace RetSim.Simulation;

public class FightSimulation
{

    public override string ToString()
    {
        TimeSpan t = TimeSpan.FromMilliseconds(Duration);

        return $"{Player.Name} fights {Enemy.Name} for {t.Minutes}m {t.Seconds}.{t.Milliseconds:D3}s";
    }

    public readonly Player Player;
    public readonly Enemy Enemy;
    public readonly Tactic Tactic;

    public readonly List<Spell> Buffs;
    public readonly List<Spell> Debuffs;
    public readonly List<Spell> Consumables;

    public readonly CombatLog CombatLog;
    public readonly IEventQueue Queue;
    public int Timestamp { get; set; }
    public bool Ongoing { get; set; }

    public readonly int Duration;

    public FightSimulation(Player player, Enemy enemy, Tactic tactic, List<Spell> buffs, List<Spell> debuffs, List<Spell> consumables, int minDuration, int maxDuration)
    {
        Player = player;
        Enemy = enemy;
        Tactic = tactic;
        Buffs = buffs;
        Debuffs = debuffs;
        Consumables = consumables;

        CombatLog = new CombatLog();
        Queue = new MinQueue();

        Timestamp = 0;
        Ongoing = true;

        Duration = RNG.RollRange(minDuration, maxDuration);

        Initialize();
    }

    public void Initialize()
    {
        foreach (Spell spell in Player.Equipment.Spells)
        {
            Queue.Add(new CastEvent(spell, Player, Player, this, Timestamp, -3));
        }

        foreach (Talent talent in Player.Talents)
        {
            Queue.Add(new CastEvent(talent, Player, Player, this, Timestamp, -2));
        }

        foreach (Spell buff in Buffs)
        {
            Queue.Add(new CastEvent(buff, Player, Player, this, Timestamp, -1));
        }

        foreach (Spell debuff in Debuffs)
        {
            Queue.Add(new CastEvent(debuff, Player, Enemy, this, Timestamp, -1));
        }

        foreach (Spell consumable in Consumables)
        {
            Queue.Add(new CastEvent(consumable, Player, Player, this, Timestamp, -1));
        }

        if (Player.Race.Racial != null && Player.Race.Racial.Requirements(Player))
            Queue.Add(new CastEvent(Player.Race.Racial, Player, Player, this, Timestamp, -1));

        while (!Queue.IsEmpty())
        {
            Event current = Queue.RemoveNext();

            if (current is AuraEndEvent)
                ends.Add((AuraEndEvent)current);

            else
                current.Execute();
        }
    }

    private List<AuraEndEvent> ends = new();

    public CombatLog Run()
    {
        Queue.AddRange(Tactic.PreFight(this));

        foreach (AuraEndEvent end in ends)
            Queue.Add(end);

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
        if (CombatLog.DamageBreakdown.Count == 0)
            CombatLog.CreateDamageBreakdown();

        foreach (LogEntry entry in CombatLog.Log)
            Logger.Log(entry.ToString());

        Logger.Log($"\nDuration - Expected: {Duration} / Real: {Timestamp}\n");

        Logger.Log($"╔════════════════════╦═════════╦═════════╦════════╦═════╦═════════════╦═════════════╦═════════════╦═════════════╗");
        Logger.Log($"║    Ability Name    ║ Damage  ║   DPS   ║   %    ║  #  ║ #  Crit   % ║ #   Hit   % ║ #  Miss   % ║ #  Dodge  % ║");
        Logger.Log($"╠════════════════════╬═════════╬═════════╬════════╬═════╬═════════════╬═════════════╬═════════════╬═════════════╣");

        int totals = 0;

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

                totals++;
            }

            float dps = (float)damage / CombatLog.Duration * 1000;
            float hit = count - miss - dodge;

            Logger.Log($"║ {s,-18} ║ {damage,7} ║ {dps.Rounded(),7} ║ {(dps / CombatLog.DPS * 100).Rounded(),5}% ║ {count,3} ║ {crit,-3} {(crit / hit * 100).Rounded(),6}% ║ {hit,-3} {(hit / count * 100).Rounded(),6}% ║ {miss,-3} {(miss / count * 100).Rounded(),6}% ║ {dodge,-3} {(dodge / count * 100).Rounded(),6}% ║");

        }

        Logger.Log($"╠════════════════════╬═════════╬═════════╬════════╬═════╬═════════════╩═════════════╩═════════════╩═════════════╝");
        Logger.Log($"║       Totals       ║ {CombatLog.Damage,7} ║ {CombatLog.DPS.Rounded(),7} ║ {"100%",6} ║ {totals,3} ║");
        Logger.Log($"╚════════════════════╩═════════╩═════════╩════════╩═════╝");
    }
}