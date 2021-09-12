using RetSim.AuraEffects;

namespace RetSim.Data
{
    public static partial class Auras
    {
        public static readonly Aura SunderArmor = new()
        {
            IsDebuff = true,
            Effects = new() { new ModifyStats() { Stats = new() { { StatName.Armor, -2600 } } } }
        };

        public static readonly Aura ExposeArmor = new()
        {
            IsDebuff = true,
            Effects = new() { new ModifyStats() { Stats = new() { { StatName.Armor, -2050 } } } }
        };

        public static readonly Aura ImprovedExposeArmor = new()
        {
            IsDebuff = true,
            Effects = new() { new ModifyStats() { Stats = new() { { StatName.Armor, -3075 } } } }
        };

        public static readonly Aura FaerieFire = new()
        {
            IsDebuff = true,
            Effects = new() { new ModifyStats() { Stats = new() { { StatName.Armor, -610 } } } }
        };

        public static readonly Aura ImprovedFaerieFire = new()
        {
            IsDebuff = true,
            Effects = new() { new ModifyStats() { Stats = new() { { StatName.Armor, -610 } } } }
            //TODO: Add improved  hit chance
        };

        public static readonly Aura CurseOfRecklessness = new()
        {
            IsDebuff = true,
            Effects = new() { new ModifyStats() { Stats = new() { { StatName.Armor, -800 } } } }
        };

        public static readonly Aura CurseOfTheElements = new()
        {
            IsDebuff = true,
            Effects = new() {  }

            //TODO: Add increased damage taken for school
        };

        public static readonly Aura ImprovedCurseOfTheElements = new()
        {
            IsDebuff = true,
            Effects = new() { }

            //TODO: Add increased damage taken for school
        };

        public static readonly Aura ImprovedSealOfTheCrusaderz = new() //TODO: Fix
        {
            IsDebuff = true,
            Effects = new() { }

            //TODO: Add increased damage taken for school
        };

        public static readonly Aura JudgementOfWisdom = new()
        {
            IsDebuff = true,
            Effects = new() { }

            //TODO: Add proc
        };

        public static readonly Aura ImprovedHuntersMark = new()
        {
            IsDebuff = true,
            Effects = new() { }

            //TODO: Add increased attack damage for attacker
        };

        public static readonly Aura ExposeWeakness = new()
        {
            IsDebuff = true,
            Effects = new() { }

            //TODO: Add increased attack damage for attacker
        };

        public static readonly Aura BloodFrenzy = new()
        {
            IsDebuff = true,
            Effects = new() { }

            //TODO: Add increased damage taken for school
        };

        public static readonly Aura ImprovedShadowBolt = new()
        {
            IsDebuff = true,
            Effects = new() { }

            //TODO: Add increased damage taken for school
        };

        public static readonly Aura Misery = new()
        {
            IsDebuff = true,
            Effects = new() { }

            //TODO: Add increased damage taken for school
        };

        public static readonly Aura ShadowWeaving = new()
        {
            IsDebuff = true,
            Effects = new() { }

            //TODO: Add increased damage taken for school
        };

        public static readonly Aura ImprovedScorch = new()
        {
            IsDebuff = true,
            Effects = new() { }

            //TODO: Add increased damage taken for school
        };
    }
}
