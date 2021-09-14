using RetSim.Misc;

namespace RetSim.Simulation.CombatLogEntries
{
    public abstract class LogEntry
    {
        public string Message => FormatInput();

        public int Timestamp { get; init; }
        public int Mana { get; init; }

        protected abstract string FormatInput();

        public override string ToString()
        {
            return $"{string.Format(Constants.Numbers.MillisecondFormatter, Timestamp)} - {Message}";
        }
    }
}