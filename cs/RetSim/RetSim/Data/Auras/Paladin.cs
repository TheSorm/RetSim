using RetSim.AuraEffects;
using System.Collections.Generic;

namespace RetSim.Data
{
    public static partial class Auras
    {
        public static readonly Seal SealOfCommand = new()
        {
            Persist = 400
        };

        public static readonly Seal SealOfBlood = new()
        {
        };

        public static readonly Seal SealOfTheCrusader = new()
        {
        };

       

        public static readonly Aura WindfuryTotem = new()
        {
        };

        public static readonly Aura WindfuryAttack = new()
        {
            Duration = 10,
            Effects = new() { new GainStats() { Stats = new() { { StatName.AttackPower, 445 } } } }
        };

        public static readonly Aura AvengingWrath = new()
        {
            Duration = 20000,
            Effects = new() { new ModDamageSchool() { Percentage = 30, Schools = Spells.AllSchools } }
        };

        public static readonly Aura HumanRacial = new()
        {
            Effects = new() { new GainStats() { Stats = new() { { StatName.Expertise, 5 } } } }
        };
    }
}