namespace RetSim.Log
{
    public class ExtraAttacksEntry : LogEntry
    {
        public string Source { get; init; }
        public int Number { get; init; }

        protected override string FormatInput()
        {
            if (Number == 1)
                return $"[Player] gains 1 extra attack from [{Source}]";

            else
                return $"[Player] gains {Number} extra attacks from [{Source}]";
        }
    }
}
