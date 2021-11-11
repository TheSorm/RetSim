using RetSim.Spells;
using RetSim.Spells.AuraEffects;
using RetSim.Units.UnitStats;

namespace RetSim.Data;

public static partial class Auras
{
    public static readonly Dictionary<int, Aura> ByID;

    public static readonly HashSet<Seal> Seals = new()
    {
        SealOfTheCrusader,
        SealOfCommand,
        SealOfBlood
    };

    static Auras()
    {
        SealOfCommand.Effects = new() { new GainSeal(1) };
        SealOfBlood.Effects = new() { new GainSeal(2) };
        Vengeance.Effects = new() { new GainProc(Procs.Vengeance) };

        WindfuryTotem.Effects = new() { new GainProc(Procs.WindfuryAttack) };

        Mongoose.Effects = new() { new GainProc(Procs.Mongoose) };
        Executioner.Effects = new() { new GainProc(Procs.Executioner) };
        Deathfrost.Effects = new() { new GainProc(Procs.Deathfrost) };

        DragonspineTrophy.Effects = new() { new GainProc(Procs.DragonspineTrophy) };
        Lionheart.Effects = new() { new GainProc(Procs.Lionheart) };
        LibramOfAvengement.Effects = new() { new GainProc(Procs.LibramOfAvengement) };

        Fanaticism.Effects = new()
        {
            new ModSpellCritChance
            {
                Amount = 15,
                Spells = new() { Spells.JudgementOfCommand.ID, Spells.JudgementOfBlood.ID }
            }
        };

        ExposeWeakness.Effects = new()
        {
             new GainStats { Stats = new() { { StatName.IncreasedAttackerAttackPower, Program.HunterAgility * 0.25f } } } 
        };

        SealOfCommand.ExclusiveWith = new() { SealOfBlood, SealOfTheCrusader };
        SealOfCommand.Judgement = Spells.JudgementOfCommand;

        SealOfBlood.ExclusiveWith = new() { SealOfCommand, SealOfTheCrusader };
        SealOfBlood.Judgement = Spells.JudgementOfBlood;

        SealOfTheCrusader.ExclusiveWith = new() { SealOfCommand, SealOfBlood };
        SealOfTheCrusader.Judgement = Spells.JudgementOfCommand;

        ByID = new()
        {
            { Spells.SealOfTheCrusader.ID, SealOfTheCrusader },
            { Spells.SealOfCommand.ID, SealOfCommand },
            { Spells.SealOfBlood.ID, SealOfBlood },
            { Spells.WindfuryTotem.ID, WindfuryTotem },
            { Spells.WindfuryAttack.ID, WindfuryAttack },
            { Spells.AvengingWrath.ID, AvengingWrath },
            { Spells.HumanRacial.ID, HumanRacial },

            { Spells.GreaterBlessingOfMight.ID, GreaterBlessingOfMight },
            { Spells.GreaterBlessingOfKings.ID, GreaterBlessingOfKings },
            { Spells.GreaterBlessingOfWisdom.ID, GreaterBlessingOfWisdom },
            { Spells.BattleShout.ID, BattleShout },
            { Spells.TrueshotAura.ID, TrueshotAura },
            { Spells.FerociousInspiration.ID, FerociousInspiration },
            { Spells.StrengthOfEarthTotem.ID, StrengthOfEarthTotem },
            { Spells.GraceOfAirTotem.ID, GraceOfAirTotem },
            { Spells.ManaSpringTotem.ID, ManaSpringTotem },
            { Spells.UnleashedRage.ID, UnleashedRage },
            { Spells.GiftOfTheWild.ID, MarkOfTheWild },
            { Spells.LeaderOfThePack.ID, LeaderOfThePack },
            { Spells.PrayerOfFortitude.ID, PrayerOfFortitude },
            { Spells.PrayerOfSpirit.ID, PrayerOfSpirit },
            { Spells.ArcaneBrilliance.ID, ArcaneBrilliance },
            { Spells.HeroicPresence.ID, HeroicPresence },
            { Spells.InspiringPresence.ID, InspiringPresence },
            { Spells.Heroism.ID, Heroism },

            { Spells.ImprovedSealOfTheCrusader.ID, ImprovedSealOfTheCrusader },
            { Spells.Conviction.ID, Conviction },
            { Spells.Crusade.ID, Crusade },
            { Spells.TwoHandedWeaponSpecialization.ID, TwoHandedWeaponSpecialization },
            { Spells.SanctityAura.ID, SanctityAura },
            { Spells.ImprovedSanctityAura.ID, ImprovedSanctityAura },
            { Spells.SanctifiedSeals.ID, SanctifiedSeals },
            { Spells.Vengeance.ID, Vengeance },
            { Spells.VengeanceProc.ID, VengeanceProc },
            { Spells.Fanaticism.ID, Fanaticism },
            { Spells.Precision.ID, Precision },
            { Spells.DivineStrength.ID, DivineStrength },
            { Spells.DivineIntellect.ID, DivineIntellect },

            { Spells.SunderArmor.ID, SunderArmor },
            { Spells.ExposeArmor.ID, ExposeArmor },
            { Spells.ImprovedExposeArmor.ID, ImprovedExposeArmor },
            { Spells.FaerieFire.ID, FaerieFire },
            { Spells.ImprovedFaerieFire.ID, ImprovedFaerieFire },
            { Spells.CurseOfRecklessness.ID, CurseOfRecklessness },
            { Spells.CurseOfTheElements.ID, CurseOfTheElements },
            { Spells.ImprovedCurseOfTheElements.ID, ImprovedCurseOfTheElements },
            { Spells.JudgementOfWisdom.ID, JudgementOfWisdom },
            { Spells.ImprovedHuntersMark.ID, ImprovedHuntersMark },
            { Spells.ExposeWeakness.ID, ExposeWeakness },
            { Spells.BloodFrenzy.ID, BloodFrenzy },
            { Spells.ImprovedShadowBolt.ID, ImprovedShadowBolt },
            { Spells.Misery.ID, Misery },
            { Spells.ShadowWeaving.ID, ShadowWeaving },
            { Spells.ImprovedScorch.ID, ImprovedScorch },


            { Spells.Mongoose.ID, Mongoose },
            { Spells.MongooseProc.ID, MongooseProc },
            { Spells.Executioner.ID, Executioner },
            { Spells.ExecutionerProc.ID, ExecutionerProc },
            { Spells.Deathfrost.ID, Deathfrost },
            { Spells.DeathfrostProc.ID, DeathfrostProc },

            { Spells.Relentless.ID, Relentless },
            { Spells.DragonspineTrophy.ID, DragonspineTrophy },
            { Spells.DragonspineTrophyProc.ID, DragonspineTrophyProc },
            { Spells.BloodlustBrooch.ID, BloodlustBrooch },
            { Spells.Lionheart.ID, Lionheart },
            { Spells.LionheartProc.ID, LionheartProc },
            { Spells.LibramOfAvengement.ID, LibramOfAvengement },
            { Spells.LibramOfAvengementProc.ID, LibramOfAvengementProc },
        };
    }
}