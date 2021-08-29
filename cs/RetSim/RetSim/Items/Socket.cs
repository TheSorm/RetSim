namespace RetSim.Items
{
    public class Socket
    {
        public SocketType Type { get; init; }
        public Gem SocketedGem { get; set; }

        public Socket(SocketType type)
        {
            Type = type;
        }

        internal bool IsActive()
        {
            //TODO Implement
            return false;
        }
    }

    public enum SocketType
    {
        Meta = 1,
        Red = 2,
        Yellow = 3,
        Blue = 4
    }
}
