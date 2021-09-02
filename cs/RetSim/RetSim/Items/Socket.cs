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

        public bool IsActive()
        {
            if (SocketedGem != null)
            {
                switch (Type)
                {
                    case SocketType.Meta:
                        return SocketedGem.Color == GemColor.Meta;
                    case SocketType.Red:
                        return SocketedGem.Color == GemColor.Red || SocketedGem.Color == GemColor.Purple || SocketedGem.Color == GemColor.Orange;
                    case SocketType.Blue:
                        return SocketedGem.Color == GemColor.Blue || SocketedGem.Color == GemColor.Purple || SocketedGem.Color == GemColor.Green;
                    case SocketType.Yellow:
                        return SocketedGem.Color == GemColor.Yellow || SocketedGem.Color == GemColor.Green || SocketedGem.Color == GemColor.Orange;
                    default:
                        break;
                }
            }
            return false;
        }

        public bool IsGemActive(int redGems, int blueGems, int yellowGems)
        {
            if (SocketedGem.Color == GemColor.Meta)
            {
                if (SocketedGem.Requirments.Count != 0)
                {
                    return (SocketedGem.Requirments.ContainsKey(GemColor.Red) ? redGems >= SocketedGem.Requirments[GemColor.Red] : true) &&
                        (SocketedGem.Requirments.ContainsKey(GemColor.Blue) ? redGems >= SocketedGem.Requirments[GemColor.Blue] : true) &&
                        (SocketedGem.Requirments.ContainsKey(GemColor.Yellow) ? redGems >= SocketedGem.Requirments[GemColor.Yellow] : true);
                }
                else if (SocketedGem.SpecialRequirment != SpecialGemRequirment.None)
                {
                    switch (SocketedGem.SpecialRequirment)
                    {
                        case SpecialGemRequirment.None:
                            break;
                        case SpecialGemRequirment.RedGreaterBlue:
                            return redGems > blueGems;
                        case SpecialGemRequirment.RedGreaterYellow:
                            return redGems > yellowGems;
                        case SpecialGemRequirment.BlueGreaterRed:
                            return blueGems > redGems;
                        case SpecialGemRequirment.BlueGreaterYellow:
                            return blueGems > yellowGems;
                        case SpecialGemRequirment.YellowGreaterRed:
                            return yellowGems > redGems;
                        case SpecialGemRequirment.YellowGreaterBlue:
                            return yellowGems > blueGems;
                        default:
                            break;
                    }
                }
            }
            return true;
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
