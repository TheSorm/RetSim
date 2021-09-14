namespace RetSim.Items;

public class Socket
{
    public SocketColor Color { get; init; }

    private Gem socketedGem;
    public Gem SocketedGem
    {
        get
        {
            return socketedGem;
        }

        set
        {
            if (Color != SocketColor.Meta || value.Color == GemColor.Meta)
                socketedGem = value;
        }
    }

    public bool IsActive()
    {
        if (SocketedGem == null)
            return false;

        else
            return (SocketedGem.Color & (GemColor)Color) != 0;
    }

    public MetaGem IsMetaGem()
    {
        if (SocketedGem == null || SocketedGem.Color != GemColor.Meta)
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