using System.Collections.Generic;

namespace RetSim.WowItemData
{
    public record Stats
    {
        public int Strength { get; set; }
        public int AttackPower { get; set; }
        public int Agility { get; set; }
        public int Crit { get; set; }
        public int Hit { get; set; }
        public int Haste { get; set; }
        public int Expertise { get; set; }
        public int ArmorPenetration { get; set; }
        public int SpellDamage { get; set; }
        public int SpellHealing { get; set; }
        public int SpellCrit { get; set; }
        public int SpellHit { get; set; }
        public int SpellHaste { get; set; }
        public int Intellect { get; set; }
        public int Spirit { get; set; }
        public int ManaPer5 { get; set; }
        public int Stamina { get; set; }
        public int Defense { get; set; }
        public int Dodge { get; set; }
        public int Parry { get; set; }
        public int Block { get; set; }
        public int Resilience { get; set; }
    }
    public record Item
    {
        public int ID { get; init; }
        public string Name { get; set; }
        public int ItemLevel { get; init; }
        public string Quality { get; init; }
        public string InventoryType { get; init; }
        public Set Set { get; init; }
        public Stats Stats { get; set; }
        public List<Spell> Spells { get; set; }
        public int Phase { get; set; }
        public string Socket1 { get; set; }
        public string Socket2 { get; set; }
        public string Socket3 { get; set; }
        public SocketBonus SocketBonus { get; set; }
    }

    public record Weapon : Item
    {
        public int MinDamag { get; init; }
        public int MaxDamag { get; init; }
        public int AttackSpeed { get; init; }
        public double DPS { get; init; }
    }
    public record Armor : Item
    {
        public int ArmorValue { get; init; }
    }

    public record SocketBonus
    {
        public string Stat { get; init; }
        public int Value { get; init; }
    }

    public record Spell
    {
        public int ID { get; init; }
        public string Name { get; init; }
    }

    public record Set
    {
        public int ID { get; init; }
        public string Name { get; init; }
    }
}
