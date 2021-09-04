using System;
using System.Collections.Generic;

namespace RetSim.Items
{
    public record MetaGem : Gem
    {
        //TODO: Move requirements here so ordinary gems don't have a bunch of useless data

        public override bool IsActive(int red, int blue, int yellow)
        {
            if (Requirements == null || Color != GemColor.Meta)
                return true;

            else
                return Requirements.IsActive(red, blue, yellow);
        }
    }

    public record Gem
    {
        public int ID { get; init; }
        public string Name { get; init; }

        //TODO: Add "Nickname"
        
        //Normal stat gems (str, str + stam, str + crit, crit, hit, etc etc) - 6 str / 8 / 10
        //JC epic gems
        //Dungeon drop gems - 8

        public int ItemLevel { get; init; } //TODO: Remove?
        public string Quality { get; init; } //TODO: Remove or convert to enum?
        public GemColor Color { get; init; }
        public ItemStats Stats { get; init; }
        public List<ItemAuras> Auras { get; init; }
        public GemRequirements Requirements { get; init; } //TODO: Add
        public bool UniqueEquipped { get; set; }
        public int Phase { get; set; }

        public virtual bool IsActive(int red, int blue, int yellow)
        {
            return true;
        }
    }

    public enum RequirementType
    {
        Standard = 1,
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
        private int? Red { get; init; }
        private int? Blue { get; init; }
        private int? Yellow { get; init; }
        private RequirementType Type { get; init; }

        private Func<GemRequirements, int, int, int, bool> Check { get; init; }

        public GemRequirements()
        {
            Check = TypeToFunc[Type];
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

        private static readonly Dictionary<RequirementType, Func<GemRequirements, int, int, int, bool>> TypeToFunc = new()
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