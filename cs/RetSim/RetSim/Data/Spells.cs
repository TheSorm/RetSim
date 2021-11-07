using RetSim.Spells;
using RetSim.Spells.SpellEffects;

namespace RetSim.Data;

public static partial class Spells
{
    private static void AssignChildren(Spell spell)
    {
        if (spell.Aura != null)
            spell.Aura.Parent = spell;

        if (spell.Effects != null)
        {
            foreach (SpellEffect effect in spell.Effects)
            {
                effect.Parent = spell;
            }
        }
    }

    private static void AssignAura(Spell spell)
    {
        if (Auras.ByID.ContainsKey(spell.ID))
            spell.Aura = Auras.ByID[spell.ID];
    }

    public static readonly Dictionary<int, Spell> ByID = new()
    {
    };

    static Spells()
    {
        ByID = new()
        {
            { Melee.ID, Melee },
            { CrusaderStrike.ID, CrusaderStrike },
            { Judgement.ID, Judgement },
            { SealOfCommandProc.ID, SealOfCommandProc },
            { SealOfCommand.ID, SealOfCommand },
            { JudgementOfCommand.ID, JudgementOfCommand },
            { SealOfBloodProc.ID, SealOfBloodProc },
            { SealOfBlood.ID, SealOfBlood },
            { JudgementOfBlood.ID, JudgementOfBlood },
            { SealOfTheCrusader.ID, SealOfTheCrusader },
            { WindfuryProc.ID, WindfuryProc },
            { WindfuryAttack.ID, WindfuryAttack },
            { WindfuryTotem.ID, WindfuryTotem },
            { AvengingWrath.ID, AvengingWrath },
            { HumanRacial.ID, HumanRacial },

            { MongooseProc.ID, MongooseProc },
            { Mongoose.ID, Mongoose },
            { ExecutionerProc.ID, ExecutionerProc },
            { Executioner.ID, Executioner },
            { DeathfrostProc.ID, DeathfrostProc },
            { Deathfrost.ID, Deathfrost },

            { Relentless.ID, Relentless },
            { DragonspineTrophyProc.ID, DragonspineTrophyProc },
            { DragonspineTrophy.ID, DragonspineTrophy },
            { BloodlustBrooch.ID, BloodlustBrooch },
            { Lionheart.ID, Lionheart },
            { LionheartProc.ID, LionheartProc },
            { LibramOfAvengement.ID, LibramOfAvengement },
            { LibramOfAvengementProc.ID, LibramOfAvengementProc },


            { GreaterBlessingOfMight.ID, GreaterBlessingOfMight },
            { GreaterBlessingOfKings.ID, GreaterBlessingOfKings },
            { GreaterBlessingOfWisdom.ID, GreaterBlessingOfWisdom },
            { BattleShout.ID, BattleShout },
            { TrueshotAura.ID, TrueshotAura },
            { FerociousInspiration.ID, FerociousInspiration },
            { StrengthOfEarthTotem.ID, StrengthOfEarthTotem },
            { GraceOfAirTotem.ID, GraceOfAirTotem },
            { ManaSpringTotem.ID, ManaSpringTotem },
            { UnleashedRage.ID, UnleashedRage },
            { GiftOfTheWild.ID, GiftOfTheWild },
            { LeaderOfThePack.ID, LeaderOfThePack },
            { PrayerOfFortitude.ID, PrayerOfFortitude },
            { PrayerOfSpirit.ID, PrayerOfSpirit },
            { ArcaneBrilliance.ID, ArcaneBrilliance },
            { HeroicPresence.ID, HeroicPresence },
            { InspiringPresence.ID, InspiringPresence },

            { SunderArmor.ID, SunderArmor },
            { ExposeArmor.ID, ExposeArmor },
            { ImprovedExposeArmor.ID, ImprovedExposeArmor },
            { FaerieFire.ID, FaerieFire },
            { ImprovedFaerieFire.ID, ImprovedFaerieFire },
            { CurseOfRecklessness.ID, CurseOfRecklessness },
            { CurseOfTheElements.ID, CurseOfTheElements },
            { ImprovedCurseOfTheElements.ID, ImprovedCurseOfTheElements },
            { JudgementOfWisdom.ID, JudgementOfWisdom },
            { ImprovedHuntersMark.ID, ImprovedHuntersMark },
            { ExposeWeakness.ID, ExposeWeakness },
            { BloodFrenzy.ID, BloodFrenzy },
            { ImprovedShadowBolt.ID, ImprovedShadowBolt },
            { Misery.ID, Misery },
            { ShadowWeaving.ID, ShadowWeaving },
            { ImprovedScorch.ID, ImprovedScorch },

            { ImprovedSealOfTheCrusader.ID, ImprovedSealOfTheCrusader },
            { Conviction.ID, Conviction },
            { Crusade.ID, Crusade },
            { TwoHandedWeaponSpecialization.ID, TwoHandedWeaponSpecialization },
            { SanctityAura.ID, SanctityAura },
            { ImprovedSanctityAura.ID, ImprovedSanctityAura },
            { SanctifiedSeals.ID, SanctifiedSeals },
            { Vengeance.ID, Vengeance },
            { VengeanceProc.ID, VengeanceProc },
            { Fanaticism.ID, Fanaticism },
            { Precision.ID, Precision },
            { DivineStrength.ID, DivineStrength },
            { DivineIntellect.ID, DivineIntellect }
        };

        WindfuryAttack.Effects = new List<SpellEffect> { new ExtraAttacks(WindfuryProc, 1) };
        
        foreach (Spell spell in ByID.Values)
        {
            AssignAura(spell);
            AssignChildren(spell);
        }
    }
}