using System;
using System.Collections.Generic;

namespace RetSim.Items
{
    public record MetaGem : Gem
    {
        public GemRequirements Requirements { get; init; }
        public ItemAura Aura { get; init; }

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

    public record Gem
    {
        public int ID { get; init; }
        public string Name { get; init; }

        //TODO: Add "Nickname"

        public int ItemLevel { get; init; }
        public string Quality { get; init; }
        public GemColor Color { get; init; }
        public ItemStats Stats { get; init; }
        public bool UniqueEquipped { get; set; }
        public int Phase { get; set; }
    }

    public enum RequirementType
    {
        Standard = 0,
        MoreRedThanBlue = 2,
        MoreRedThanYellow = 3,
        MoreBlueThanRed = 4,
        MoreBlueThanYellow = 5,
        MoreYellowThanRed = 6,
        MoreYellowThanBlue = 7,
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

    public class GemRequirements
    {
        public int Red { get; init; }
        public int Blue { get; init; }
        public int Yellow { get; init; }
        public RequirementType Type { get; init; }

        private Func<GemRequirements, int, int, int, bool> Check { get; init; }

        public GemRequirements()
        {
            Check = GetRequirementCheck[Type];
        }

        public bool IsActive(int red, int blue, int yellow)
        {
            return Check.Invoke(this, red, blue, yellow);
        }

        private static bool Standard(GemRequirements requirement, int red, int blue, int yellow) => red >= requirement.Red && blue >= requirement.Blue && yellow >= requirement.Yellow;
        private static bool MoreRedThanBlue(GemRequirements requirement, int red, int blue, int yellow) => red > blue;
        private static bool MoreRedThanYellow(GemRequirements requirement, int red, int blue, int yellow) => red > yellow;
        private static bool MoreYellowThanRed(GemRequirements requirement, int red, int blue, int yellow) => yellow > red;
        private static bool MoreYellowThanBlue(GemRequirements requirement, int red, int blue, int yellow) => yellow > red;
        private static bool MoreBlueThanYellow(GemRequirements requirement, int red, int blue, int yellow) => blue > yellow;
        private static bool MoreBlueThanRed(GemRequirements requirement, int red, int blue, int yellow) => blue > red;

        private static readonly Dictionary<RequirementType, Func<GemRequirements, int, int, int, bool>> GetRequirementCheck = new()
        {
            { RequirementType.Standard, Standard },
            { RequirementType.MoreRedThanBlue, MoreRedThanBlue },
            { RequirementType.MoreRedThanYellow, MoreRedThanYellow },
            { RequirementType.MoreBlueThanRed, MoreBlueThanRed },
            { RequirementType.MoreBlueThanYellow, MoreBlueThanYellow },
            { RequirementType.MoreYellowThanRed, MoreYellowThanRed },
            { RequirementType.MoreYellowThanBlue, MoreYellowThanBlue }
        };
    }
}