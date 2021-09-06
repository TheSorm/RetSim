using System;

namespace RetSim.Items
{
    public class Socket
    {
        public SocketColor Color { get; init; }
        public Gem SocketedGem { get; set; }

        public bool IsActive()
        {
            if (SocketedGem == null)
                return false;

            else
                return (SocketedGem.Color & (GemColor)Color) != 0;
        }

        public MetaGem IsMetaGem()
        {
            if (SocketedGem.Color != GemColor.Meta)
                return null;

            else
                return SocketedGem as MetaGem;
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