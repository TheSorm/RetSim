using System;
using System.Collections.Generic;

namespace RetSim.Items
{
    public record Gem
    {
        public int ID { get; init; }
        public string Name { get; init; }

        //TODO: Add "Nickname"

        public int ItemLevel { get; init; }
        public Quality Quality { get; init; }
        public GemColor Color { get; init; }
        public ItemStats Stats { get; init; }
        public bool UniqueEquipped { get; set; }
        public int Phase { get; set; }
    }


    public record MetaGem : Gem
    {
        public MetaRequirements Requirements { get; init; }
        public ItemSpell Spell { get; init; }

        public MetaGem()
        {
            Color = GemColor.Meta;
        }

        public bool IsActive(int red, int blue, int yellow)
        {
            if (Requirements == null)
                return false;

            else
                return Requirements.IsActive(red, blue, yellow);
        }
    }

    public class MetaRequirements
    {
        public int Red { get; init; }
        public int Blue { get; init; }
        public int Yellow { get; init; }
        public MetaRequirementType Type { get; init; }

        private Func<MetaRequirements, int, int, int, bool> Check { get; init; }

        public MetaRequirements()
        {
            Check = GetRequirementCheck[Type];
        }

        public bool IsActive(int red, int blue, int yellow)
        {
            return Check.Invoke(this, red, blue, yellow);
        }

        private static bool Standard(MetaRequirements requirement, int red, int blue, int yellow) => red >= requirement.Red && blue >= requirement.Blue && yellow >= requirement.Yellow;
        private static bool MoreRedThanBlue(MetaRequirements requirement, int red, int blue, int yellow) => red > blue;
        private static bool MoreRedThanYellow(MetaRequirements requirement, int red, int blue, int yellow) => red > yellow;
        private static bool MoreYellowThanRed(MetaRequirements requirement, int red, int blue, int yellow) => yellow > red;
        private static bool MoreYellowThanBlue(MetaRequirements requirement, int red, int blue, int yellow) => yellow > red;
        private static bool MoreBlueThanYellow(MetaRequirements requirement, int red, int blue, int yellow) => blue > yellow;
        private static bool MoreBlueThanRed(MetaRequirements requirement, int red, int blue, int yellow) => blue > red;

        private static readonly Dictionary<MetaRequirementType, Func<MetaRequirements, int, int, int, bool>> GetRequirementCheck = new()
        {
            { MetaRequirementType.Standard, Standard },
            { MetaRequirementType.MoreRedThanBlue, MoreRedThanBlue },
            { MetaRequirementType.MoreRedThanYellow, MoreRedThanYellow },
            { MetaRequirementType.MoreBlueThanRed, MoreBlueThanRed },
            { MetaRequirementType.MoreBlueThanYellow, MoreBlueThanYellow },
            { MetaRequirementType.MoreYellowThanRed, MoreYellowThanRed },
            { MetaRequirementType.MoreYellowThanBlue, MoreYellowThanBlue }
        };
    }

    [Flags]
    public enum GemColor
    {
        Meta = 1,
        Red = 2,
        Blue = 4,
        Yellow = 8,
        Orange = Red + Yellow,
        Purple = Red + Blue,
        Green = Blue + Yellow,
        Prismatic = Red + Blue + Yellow
    }
    public enum MetaRequirementType
    {
        Standard = 0,
        MoreRedThanBlue = 2,
        MoreRedThanYellow = 3,
        MoreBlueThanRed = 4,
        MoreBlueThanYellow = 5,
        MoreYellowThanRed = 6,
        MoreYellowThanBlue = 7,
    }
}