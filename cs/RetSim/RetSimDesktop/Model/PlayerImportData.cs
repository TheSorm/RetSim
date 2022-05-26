using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RetSimDesktop.Model
{
    public partial class PlayerImportData
    {
        [JsonPropertyName("selectedItems")]
        public SelectedItems SelectedItems { get; set; }

        [JsonPropertyName("talents")]
        public ImportTalents Talents { get; set; }
    }

    public partial class SelectedItems
    {
        [JsonPropertyName("gloves")]
        public ItemData Gloves { get; set; }

        [JsonPropertyName("neck")]
        public ItemData Neck { get; set; }

        [JsonPropertyName("belt")]
        public ItemData Belt { get; set; }

        [JsonPropertyName("ring1")]
        public ItemData Ring1 { get; set; }

        [JsonPropertyName("boots")]
        public ItemData Boots { get; set; }

        [JsonPropertyName("trinket1")]
        public ItemData Trinket1 { get; set; }

        [JsonPropertyName("head")]
        public ItemData Head { get; set; }

        [JsonPropertyName("bracer")]
        public ItemData Bracer { get; set; }

        [JsonPropertyName("chest")]
        public ItemData Chest { get; set; }

        [JsonPropertyName("legs")]
        public ItemData Legs { get; set; }

        [JsonPropertyName("ring2")]
        public ItemData Ring2 { get; set; }

        [JsonPropertyName("trinket2")]
        public ItemData Trinket2 { get; set; }

        [JsonPropertyName("mainhand")]
        public ItemData Mainhand { get; set; }

        [JsonPropertyName("back")]
        public ItemData Back { get; set; }

        [JsonPropertyName("shoulders")]
        public ItemData Shoulders { get; set; }

        [JsonPropertyName("wand")]
        public ItemData Wand { get; set; }
    }

    public partial class ItemData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("enchant")]
        public int Enchant { get; set; }

        [JsonPropertyName("gems")]
        public List<int> Gems { get; set; }
    }

    public partial class ImportTalents
    {
        [JsonPropertyName("divineStrength")]
        public long DivineStrength { get; set; }

        [JsonPropertyName("divineIntellect")]
        public long DivineIntellect { get; set; }

        [JsonPropertyName("spiritualFocus")]
        public long SpiritualFocus { get; set; }

        [JsonPropertyName("improvedSealOfRighteousness")]
        public long ImprovedSealOfRighteousness { get; set; }

        [JsonPropertyName("healingLight")]
        public long HealingLight { get; set; }

        [JsonPropertyName("auraMastery")]
        public long AuraMastery { get; set; }

        [JsonPropertyName("improvedLayOnHands")]
        public long ImprovedLayOnHands { get; set; }

        [JsonPropertyName("unyieldingFaith")]
        public long UnyieldingFaith { get; set; }

        [JsonPropertyName("illumination")]
        public long Illumination { get; set; }

        [JsonPropertyName("improvedBlessingOfWisdom")]
        public long ImprovedBlessingOfWisdom { get; set; }

        [JsonPropertyName("pureOfHeart")]
        public long PureOfHeart { get; set; }

        [JsonPropertyName("divineFavor")]
        public long DivineFavor { get; set; }

        [JsonPropertyName("sanctifiedLight")]
        public long SanctifiedLight { get; set; }

        [JsonPropertyName("purifyingPower")]
        public long PurifyingPower { get; set; }

        [JsonPropertyName("holyPower")]
        public long HolyPower { get; set; }

        [JsonPropertyName("light'sGrace")]
        public long LightSGrace { get; set; }

        [JsonPropertyName("holyShock")]
        public long HolyShock { get; set; }

        [JsonPropertyName("blessedLife")]
        public long BlessedLife { get; set; }

        [JsonPropertyName("holyGuidance")]
        public long HolyGuidance { get; set; }

        [JsonPropertyName("divineIllumination")]
        public long DivineIllumination { get; set; }

        [JsonPropertyName("improvedDevotionAura")]
        public long ImprovedDevotionAura { get; set; }

        [JsonPropertyName("redoubt")]
        public long Redoubt { get; set; }

        [JsonPropertyName("precision")]
        public long Precision { get; set; }

        [JsonPropertyName("guardian'sFavor")]
        public long GuardianSFavor { get; set; }

        [JsonPropertyName("toughness")]
        public long Toughness { get; set; }

        [JsonPropertyName("blessingOfKings")]
        public long BlessingOfKings { get; set; }

        [JsonPropertyName("improvedRighteousFury")]
        public long ImprovedRighteousFury { get; set; }

        [JsonPropertyName("shieldSpecialization")]
        public long ShieldSpecialization { get; set; }

        [JsonPropertyName("anticipation")]
        public long Anticipation { get; set; }

        [JsonPropertyName("stoicism")]
        public long Stoicism { get; set; }

        [JsonPropertyName("improvedHammerOfJustice")]
        public long ImprovedHammerOfJustice { get; set; }

        [JsonPropertyName("improvedConcentrationAura")]
        public long ImprovedConcentrationAura { get; set; }

        [JsonPropertyName("spellWarding")]
        public long SpellWarding { get; set; }

        [JsonPropertyName("blessingOfSanctuary")]
        public long BlessingOfSanctuary { get; set; }

        [JsonPropertyName("reckoning")]
        public long Reckoning { get; set; }

        [JsonPropertyName("sacredDuty")]
        public long SacredDuty { get; set; }

        [JsonPropertyName("one-HandedWeaponSpecialization")]
        public long OneHandedWeaponSpecialization { get; set; }

        [JsonPropertyName("improvedHolyShield")]
        public long ImprovedHolyShield { get; set; }

        [JsonPropertyName("holyShield")]
        public long HolyShield { get; set; }

        [JsonPropertyName("ardentDefender")]
        public long ArdentDefender { get; set; }

        [JsonPropertyName("combatExpertise")]
        public long CombatExpertise { get; set; }

        [JsonPropertyName("avenger'sShield")]
        public long AvengerSShield { get; set; }

        [JsonPropertyName("improvedBlessingOfMight")]
        public long ImprovedBlessingOfMight { get; set; }

        [JsonPropertyName("benediction")]
        public long Benediction { get; set; }

        [JsonPropertyName("improvedJudgement")]
        public long ImprovedJudgement { get; set; }

        [JsonPropertyName("improvedSealOfTheCrusader")]
        public long ImprovedSealOfTheCrusader { get; set; }

        [JsonPropertyName("deflection")]
        public long Deflection { get; set; }

        [JsonPropertyName("vindication")]
        public long Vindication { get; set; }

        [JsonPropertyName("conviction")]
        public long Conviction { get; set; }

        [JsonPropertyName("sealOfCommand")]
        public long SealOfCommand { get; set; }

        [JsonPropertyName("pursuitOfJustice")]
        public long PursuitOfJustice { get; set; }

        [JsonPropertyName("eyeForAnEye")]
        public long EyeForAnEye { get; set; }

        [JsonPropertyName("improvedRetributionAura")]
        public long ImprovedRetributionAura { get; set; }

        [JsonPropertyName("crusade")]
        public long Crusade { get; set; }

        [JsonPropertyName("two-HandedWeaponSpecialization")]
        public long TwoHandedWeaponSpecialization { get; set; }

        [JsonPropertyName("sanctityAura")]
        public long SanctityAura { get; set; }

        [JsonPropertyName("improvedSanctityAura")]
        public long ImprovedSanctityAura { get; set; }

        [JsonPropertyName("vengeance")]
        public long Vengeance { get; set; }

        [JsonPropertyName("sanctifiedJudgement")]
        public long SanctifiedJudgement { get; set; }

        [JsonPropertyName("sanctifiedSeals")]
        public long SanctifiedSeals { get; set; }

        [JsonPropertyName("repentance")]
        public long Repentance { get; set; }

        [JsonPropertyName("divinePurpose")]
        public long DivinePurpose { get; set; }

        [JsonPropertyName("fanaticism")]
        public long Fanaticism { get; set; }

        [JsonPropertyName("crusaderStrike")]
        public long CrusaderStrike { get; set; }
    }

    public partial class PlayerImportData
    {
        public static PlayerImportData FromJson(string json) => JsonSerializer.Deserialize<PlayerImportData>(json);
    }

    public static class Serialize
    {
        public static string ToJson(this PlayerImportData self) => JsonSerializer.Serialize(self);
    }

}
