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

        public bool IsMetaGemActive(int red, int blue, int yellow)
        {
            if (SocketedGem == null || SocketedGem.Color != GemColor.Meta)
                return false;

            else
            {
                var gem = SocketedGem as MetaGem;
                return gem.IsActive(red, blue, yellow);
            }
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