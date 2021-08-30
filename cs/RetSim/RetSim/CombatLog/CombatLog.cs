using System.Collections.Generic;

namespace RetSim.Log
{
    public class CombatLog
    {
        public int Damage { get; private set; }
        public int Duration { get; private set; }
        public float DPS { get; private set; }

        public Dictionary<string, List<DamageEntry>> DamageBreakdown { get; init; }

        public List<string> Log { get; init; }

        public CombatLog()
        {
            Damage = 0;
            DPS = 0f;

            DamageBreakdown = new Dictionary<string, List<DamageEntry>>();
            Log = new List<string>();
        }

        public void Add(LogEntry entry)
        {
            AddToLog(entry);
        }

        public void Add(DamageEntry entry)
        {
            Damage += entry.Damage;

            Duration = entry.Timestamp;

            DPS = Duration == 0 ? Damage : (float) Damage / Duration * 1000;

            if (DamageBreakdown.ContainsKey(entry.Source))
                DamageBreakdown[entry.Source].Add(entry);

            else
                DamageBreakdown.Add(entry.Source, new List<DamageEntry> { entry });

            AddToLog(entry);
        }

        private void AddToLog(LogEntry entry)
        {
            Log.Add(entry.ToString());
        }
    }
}
