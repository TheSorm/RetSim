using RetSim.Data;
using RetSim.Items;
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
    public Player Player { get; init; }
    public Enemy Enemy { get; init; }
    public Tactic Tactic { get; init; }

    public CooldownManager CooldownManager { get; init; }
    public CombatLog CombatLog { get; init; }
    public IEventQueue Queue { get; init; }

    public int Duration { get; init; }
    public int Timestamp { get; private set; }
    public bool Ongoing { get; set; }

    public FightSimulation(Player player, Enemy enemy, Tactic tactic, List<Spell> buffTalents, List<Spell> buffs, List<Spell> debuffs, List<Spell> consumables, int minDuration, int maxDuration, List<Spell> cooldowns, List<int> heroisms)
    {
        Player = player;
        Enemy = enemy;
        Tactic = tactic;

        CombatLog = new CombatLog();
        Queue = new InsertionQueue();

        Timestamp = 0;
        Ongoing = true;

        Duration = RNG.RollRange(minDuration, maxDuration);

        Initialize(buffTalents, buffs, debuffs, consumables);

        List<int> correctedHerosimTimes = new();

        foreach (var time in heroisms)
        {
            if (time < Duration)
            {
                correctedHerosimTimes.Add(time);
            }
        }

        var allCooldowns = new List<Spell>();

        foreach (Spell spell in cooldowns)
        {
            allCooldowns.Add(spell);
        }

        foreach (EquippableItem item in player.Equipment.PlayerEquipment)
        {
            if (item != null && item.OnUse != null && Collections.Spells.ContainsKey(item.OnUse.ID))
                allCooldowns.AddRange(Spell.GetSpells(item.OnUse.ID));
        }

        CooldownManager = new CooldownManager(this, allCooldowns, correctedHerosimTimes);

        var heroism = Collections.Spells[32182];

        foreach (int timestamp in correctedHerosimTimes)
        {
            Queue.Add(new CastEvent(heroism, Player, Player, this, timestamp));
        }
    }

    public void Initialize(List<Spell> buffTalents, List<Spell> buffs, List<Spell> debuffs, List<Spell> consumables)
    {
        Timestamp = -1;

        foreach (Spell spell in Player.Equipment.Spells)
        {
            (new CastEvent(spell, Player, Player, this, Timestamp, -3)).Execute();
        }

        if (Player.Race.Racial != null && Player.Race.Racial.Requirements(Player))
            (new CastEvent(Player.Race.Racial, Player, Player, this, Timestamp, -1)).Execute();

        foreach (Spell buffTalent in buffTalents)
        {
            (new CastEvent(buffTalent, Player, Player, this, Timestamp, -1)).Execute();
        }

        foreach (Spell debuff in debuffs)
        {
            (new CastEvent(debuff, Player, Enemy, this, Timestamp, -1)).Execute();
        }

        foreach (Talent talent in Player.Talents)
        {
            (new CastEvent(talent, Player, Player, this, Timestamp, -2)).Execute();
        }

        Timestamp = 0;

        foreach (Spell buff in buffs)
        {
            (new CastEvent(buff, Player, Player, this, Timestamp, -1)).Execute();
        }

        foreach (Spell consumable in consumables)
        {
            (new CastEvent(consumable, Player, Player, this, Timestamp, -1)).Execute();
        }
    }

    public CombatLog Run()
    {
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

                if (mask != ProcMask.None)
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

        Logger.Log($"╔═════════════════════╦═════════╦═════════╦════════╦═════╦═════════════╦═════════════╦═════════════╦═════════════╗");
        Logger.Log($"║    Ability Name     ║ Damage  ║   DPS   ║   %    ║  #  ║ #  Crit   % ║ #   Hit   % ║ #  Miss   % ║ #  Dodge  % ║");
        Logger.Log($"╠═════════════════════╬═════════╬═════════╬════════╬═════╬═════════════╬═════════════╬═════════════╬═════════════╣");

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

            Logger.Log($"║ {s,-19} ║ {damage,7} ║ {dps.Rounded(),7} ║ {(dps / CombatLog.DPS * 100).Rounded(),5}% ║ {count,3} ║ {crit,-3} {(crit / hit * 100).Rounded(),6}% ║ {hit,-3} {(hit / count * 100).Rounded(),6}% ║ {miss,-3} {(miss / count * 100).Rounded(),6}% ║ {dodge,-3} {(dodge / count * 100).Rounded(),6}% ║");

        }

        Logger.Log($"╠═════════════════════╬═════════╬═════════╬════════╬═════╬═════════════╩═════════════╩═════════════╩═════════════╝");
        Logger.Log($"║       Totals        ║ {CombatLog.Damage,7} ║ {CombatLog.DPS.Rounded(),7} ║ {"100%",6} ║ {totals,3} ║");
        Logger.Log($"╚═════════════════════╩═════════╩═════════╩════════╩═════╝");
    }


    public override string ToString()
    {
        TimeSpan t = TimeSpan.FromMilliseconds(Duration);

        return $"{Player.Name} fights {Enemy.Name} for {t.Minutes}m {t.Seconds}.{t.Milliseconds:D3}s";
    }
}