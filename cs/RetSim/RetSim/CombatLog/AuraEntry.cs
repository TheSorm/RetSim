namespace RetSim.Log
{
    public class AuraEntry : LogEntry
    {
        public string Source { get; init; }
        public AuraChangeType Type {  get; init; }

        protected override string FormatInput()
        {
            switch (Type)
            {

                case AuraChangeType.Gain:
                    return $"[Player] gains [{Source}]";

                case AuraChangeType.Fade:
                    return $"[{Source}] fades from [Player]";

                default:
                    return $"[Player]'s [{Source}] is refreshed";
            }
        }
    }
}
