using RetSim.Misc;
using RetSim.Spells;

namespace RetSim.Simulation.CombatLogEntries
{
    public class DamageEntry : LogEntry
    {
        public string Source { get; init; }
        public AttackResult AttackResult { get; init; }
        public bool Crit { get; init; }
        public float Glancing { get; init; }
        public float Mitigation { get; init; }
        public int Damage { get; init; }
        public School School { get; init; }

        protected override string FormatInput()
        {
            string result = $"[Player] [{Source}] {AttackResult} [Enemy]";

            if (Damage > 0)
                result += $" for {Damage} {School} Damage";

            if (Crit)
                result += $" (Crit)";

            if (Glancing > 0)
                result += $" ({Glancing.Rounded()}% Glancing)";

            if (Mitigation > 0)
            {
                if (School == School.Physical)
                    result += $" ({Mitigation.Rounded()}% DR)";

                else
                    result += $" ({Mitigation.Rounded()}% Resist)";
            }

            return result;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

}