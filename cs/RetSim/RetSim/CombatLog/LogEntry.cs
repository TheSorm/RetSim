namespace RetSim.Log
{
    public abstract class LogEntry
    {
        public string Message => FormatInput();

        public int Timestamp { get; init; }
        public int Mana { get; init; }

        protected abstract string FormatInput();

        public override string ToString()
        {
            return $"{string.Format(Constants.Misc.MillisecondFormatter, Timestamp)} - {Message}";
        }
    }
}