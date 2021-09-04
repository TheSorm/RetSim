using System;

namespace RetSim.Items
{
    public class Socket
    {
        public SocketColor Type { get; init; } //TODO: Rename to Color
        public Gem SocketedGem { get; set; }

        public Socket(SocketColor type)
        {
            Type = type;
        }

        public bool IsActive()
        {
            if (SocketedGem == null)
                return false;

            else
                return (SocketedGem.Color & (GemColor)Type) != 0;
        }

        public bool IsGemActive(int red, int blue, int yellow)
        {
            if (SocketedGem == null)
                return false;

            else
                return SocketedGem.IsActive(red, blue, yellow);
        }
    }

    [Flags]
    public enum SocketColor
    {
        Meta = 1,
        Red = 2,
        Blue = 4,
        Yellow = 8
    }
}
