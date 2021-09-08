using RetSim.AuraEffects;
using System.Collections.Generic;

namespace RetSim.Data
{
    public static partial class Auras
    {
        public static readonly Aura GreaterBlessingOfMight = new()
        {
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { AttackPower = 220 } }
            }
        };

        public static readonly Aura GreaterBlessingOfKings = new()
        {
            Effects = new List<AuraEffect>
            {
                new ModStat() { All = true }
            }
        };

        public static readonly Aura GreaterBlessingOfWisdom = new()
        {
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { ManaPer5 = 41 } }
            }
        };

        public static readonly Aura BattleShout = new()
        {
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { AttackPower = 306 } }
            }
        };

        public static readonly Aura TrueshotAura = new()
        {
            Effects = new List<AuraEffect>
            {
                new GainStats() { Stats = new StatSet() { AttackPower = 125 } }
            }
        };

        public static readonly Aura FerociousInspiration = new()
        {
            Effects = new List<AuraEffect>()
            {
                new ModDamageSchool()
                {
                    Percentage = 3,
                    Schools = new List<School> { School.Typeless, School.Physical, School.Holy, School.Fire, School.Nature, School.Frost, School.Shadow, School.Arcane }
                }
            }
        };
    }
}