namespace RetSim.Items;

public class ItemSet
{
    public int ID { get; init; }
    public string Name { get; init; }
    public List<SetSpell> SetSpells { get; init; }
}

public class SetSpell
{
    public string Name { get; init; }
    public int ID { get; init; }
    public int RequiredCount { get; init; }
}