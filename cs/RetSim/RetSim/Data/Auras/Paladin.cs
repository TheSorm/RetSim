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
            Effects = new List<AuraEffect>
            {
                    new GainStats() { Stats = new StatSet() { AttackPower = 445 } }
            }
        };

        public static readonly Aura AvengingWrath = new()
        {
            Duration = 20000,
            Effects = new List<AuraEffect>()
            {
                new ModDamageSchool()
                {
                    Percentage = 30,
                    Schools = new List<School> { School.Typeless, School.Physical, School.Holy, School.Fire, School.Nature, School.Frost, School.Shadow, School.Arcane }
                }
            }
        };

        public static readonly Aura DragonspineTrophy = new()
        {
        };

        public static readonly Aura DragonspineTrophyProc = new()
        {
            Duration = 10000,
            Effects = new List<AuraEffect>
            {
                    new GainStats() { Stats = new StatSet() { HasteRating = 325 } }
            }
        };        
    }
}