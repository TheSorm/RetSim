using System.Collections.Generic;

namespace RetSim.Items
{
    public record Gem
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public int ItemLevel { get; init; }
        public string Quality { get; init; }
        public GemColor Color { get; init; }
        public ItemStats Stats { get; init; }
        public List<ItemAuras> Auras { get; init; }
        public Dictionary<GemColor, int> Requirments { get; set; }
        public SpecialGemRequirment SpecialRequirment { get; set; }
        public bool UniqueEquipped { get; set; }
        public int Phase { get; set; }
    }

    public enum SpecialGemRequirment
    {
        None = 0,
        RedGreaterBlue = 1,
        RedGreaterYellow = 2,
        BlueGreaterRed = 3,
        BlueGreaterYellow = 4,
        YellowGreaterRed = 5,
        YellowGreaterBlue = 6,
    }
    public enum GemColor
    {
        Red = 1,
        Blue = 2,
        Yellow = 3,
        Purple = 4,
        Green = 5,
        Orange = 6,
        Meta = 7,
        Prismatic = 9
    }
}
