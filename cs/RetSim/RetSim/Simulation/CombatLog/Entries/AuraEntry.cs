namespace RetSim.Log
{
    public class AuraEntry : LogEntry
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
                    return $"[Player] gains [{aura}]";

                case AuraChangeType.Fade:
                    return $"[{aura}] fades from [Player]";

                default:
                    return $"[Player]'s [{aura}] is refreshed";
            }
        }
    }
}
