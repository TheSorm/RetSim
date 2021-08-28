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

        public Socket(string socket)
        {
            switch (socket)
            {
                case "Meta":
                    Type = SocketType.Meta;
                    break;
                case "Red":
                    Type = SocketType.Red;
                    break;
                case "Yellow":
                    Type = SocketType.Yellow;
                    break;
                case "Blue":
                    Type = SocketType.Blue;
                    break;
                default:
                    break;
            }
        }

        internal bool IsActive()
        {
            //TODO Implement
            return false;
        }
    }

    public enum SocketType
    {
        Meta,
        Red,
        Yellow,
        Blue
    }
}
