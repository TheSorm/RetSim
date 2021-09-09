using RetSim.SpellEffects;
using System.Collections.Generic;
using System.Linq;

namespace RetSim.Data
{
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

        public static readonly List<School> AllSchools = new() { School.Typeless, School.Physical, School.Holy, School.Fire, School.Nature, School.Frost, School.Shadow, School.Arcane };

        static Spells()
        {
            ByID = new Dictionary<int, Spell>
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

                { DragonspineTrophyProc.ID, DragonspineTrophyProc },
                { DragonspineTrophy.ID, DragonspineTrophy },
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

            ByID.OrderBy(key => key.Key);

            WindfuryAttack.Effects = new List<SpellEffect> { new ExtraAttacks(WindfuryProc, 1) };

            foreach (Spell spell in ByID.Values)
            {
                AssignAura(spell);
                AssignChildren(spell);
            }
        }
    }

}