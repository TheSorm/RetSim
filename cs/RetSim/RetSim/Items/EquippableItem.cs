using RetSim.Units.UnitStats;

namespace RetSim.Items;

public class EquippableItem
{
    public int ID { get; init; }
    public string Name { get; init; }
    public int ItemLevel { get; init; }
    public Quality Quality { get; init; }
    public Slot Slot { get; init; }
    public StatSet Stats { get; init; }
    public Set Set { get; init; }
    public ItemSpell OnUse { get; set; }
    public ItemSpell OnEquip { get; init; }

    public Socket[] Sockets = new Socket[3];
    public Socket Socket1 { get => Sockets[0]; init => Sockets[0] = value; }
    public Socket Socket2 { get => Sockets[1]; init => Sockets[1] = value; }
    public Socket Socket3 { get => Sockets[2]; init => Sockets[2] = value; }
    public StatSet SocketBonus { get; init; }
    public bool UniqueEquipped { get; set; }
    public int Phase { get; init; }

    public static EquippableItem GetItemWithGems(int id, Gem[] gems)
    {
        var item = Data.Items.AllItems[id];

        if (gems != null)
        {
            for (int i = 0; i < Math.Min(item.Sockets.Length, gems.Length); i++)
            {
                if (item.Sockets[i] != null && gems[i] != null)
                    item.Sockets[i].SocketedGem = gems[i];

                else
                    break;
            }
        }

        return item;
    }
    
    public bool IsSocketBonusActive()
    {
        if (SocketBonus == null)
            return false;

        else
        {
            bool result = true;

            foreach (Socket socket in Sockets)
            {
                if (socket == null)
                    continue;

                else if (!socket.IsActive())
                {
                    result = false;
                    break;
                }

            }

            return result;
        }
    }

    public List<Gem> GetGems()
    {
        var gems = new List<Gem>();

        foreach (Socket socket in Sockets)
        {
            if (socket != null && socket.SocketedGem != null)
                gems.Add(socket.SocketedGem);
        }

        return gems;
    }

    public override string ToString()
    {
        var gems = GetGems();

        string gems2 = "";

        if (gems.Count > 0)
        {
            for (int i = 0; i < gems.Count; i++)
            {
                if (i + 1 < gems.Count)
                    gems2 += $"{gems[i].Name}, ";

                else
                    gems2 += $"{gems[i].Name}";
            }
        }

        return $"║ {Slot, -9} ║ {ID, -5} ║ {Name, -25} ║ {gems.Count}    ║ {gems2, -59} ║";
    }
}

public class Set
{
    public int ID { get; init; }
    public string Name { get; init; }
}

public class SocketBonus
{
    public StatName Stat { get; init; }
    public int Value { get; init; }
}

public class ItemSpell
{
    public int ID { get; init; }
    public string Name { get; init; }
}

public enum Quality
{
    Poor = 0,
    Common = 1,
    Uncommon = 2,
    Rare = 3,
    Epic = 4,
    Legendary = 5,
    Artifact = 6
}

public enum Slot
{
    Head = 0,
    Neck = 1,
    Shoulders = 2,
    Back = 3,
    Chest = 4,
    Wrists = 5,
    Hands = 6,
    Waist = 7,
    Legs = 8,
    Feet = 9,
    Finger = 10,
    Trinket = 11,
    Relic = 12,
    Weapon = 13
}