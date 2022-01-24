using RetSim.Units.UnitStats;

namespace RetSim.Items;

public class Enchant
{
    public int ID { get; init; }
    public int EnchantmentID { get; init; }
    public int ItemID { get; init; }
    public string Name { get; init; }
    public string Nickname { get; init; }
    public Slot Slot { get; init; }
    public StatSet Stats { get; init; }
    public SpellReference OnEquip { get; init; }
    public int Phase { get; set; }

    public override string ToString()
    {
        if (string.IsNullOrEmpty(Nickname))
            return Name;

        else
            return Nickname;
    }
}