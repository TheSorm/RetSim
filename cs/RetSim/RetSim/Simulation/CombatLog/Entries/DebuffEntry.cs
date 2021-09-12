namespace RetSim.Log
{
    public class DebuffEntry : LogEntry
    {
        public string Source { get; init; } //TODO: Change Source to ID, add Name
        public int Stacks { get; init; }
        public AuraChangeType Type { get; init; }

        protected override string FormatInput()
        {
            string aura = Stacks > 1 ? $"{Source} ({Stacks})" : Source;

            switch (Type)
            {
                case AuraChangeType.Gain:
                    return $"[Enemy] is afflicted by [{aura}]";

                case AuraChangeType.Fade:
                    return $"[{aura}] fades from [Enemy]";

                default:
                    return $"[{aura}] is refreshed on [Enemy]";
            }
        }
    }
}
