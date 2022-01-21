using RetSim.Spells;
using System.Collections.Generic;
using System.ComponentModel;
using static RetSim.Data.Collections;

namespace RetSimDesktop.Model
{
    public class SelectedBuffs : INotifyPropertyChanged
    {
        private bool battleShoutEnabled = true;
        private BattleShout selectedBattleShout = BattleShout.ImpBattleShout;

        private bool trueshotAuraEnabled = false;
        private bool ferociousInspirationEnabled = false;

        private bool earthTotemEnabled = true;
        private EarthTotem selectedEarthTotem = EarthTotem.ImpStrengthOfEarthTotem;

        private bool airTotem1Enabled = true;
        private AirTotem1 selectedAirTotem1 = AirTotem1.ImpWindfuryTotem;

        private bool airTotem2Enabled = true;
        private AirTotem2 selectedAirTotem2 = AirTotem2.ImpGraceOfAirTotem;

        private bool waterTotemEnabled = true;
        private WaterTotem selectedWaterTotem = WaterTotem.ManaSpringTotem;

        private bool unleashedRageEnabled = true;
        private bool heroismEnabled = true;
        private bool leaderofthePackEnabled = false;
        private bool heroicPresenceEnabled = false;
        private bool inspiringPresenceEnabled = true;

        private bool blessingofMightEnabled = true;
        private BlessingofMight selectedBlessingofMight = BlessingofMight.ImpBlessingofMight;

        private bool blessingofKingsEnabled = true;

        private bool blessingofWisdomEnabled = false;
        private BlessingofWisdom selectedBlessingofWisdom = BlessingofWisdom.ImpBlessingofWisdom;

        private bool markoftheWildEnabled = true;
        private MarkoftheWild selectedMarkoftheWild = MarkoftheWild.ImpMarkoftheWild;

        private bool powerWordFortitudeEnabled = true;
        private PowerWordFortitude selectedPowerWordFortitude = PowerWordFortitude.ImpPowerWordFortitude;

        private bool divineSpiritEnabled = true;
        private DivineSpirit selectedDivineSpirit = DivineSpirit.ImpDivineSpirit;

        private bool arcaneIntellectEnabled = true;

        private bool braidedEterniumChainEnabled = true;
        private bool embraceoftheDawnEnabled = false;
        private bool eyeoftheNightEnabled = false;


        public bool BattleShoutEnabled { get => battleShoutEnabled; set { battleShoutEnabled = value; OnPropertyChanged(nameof(BattleShoutEnabled)); } }
        public BattleShout SelectedBattleShout { get => selectedBattleShout; set { selectedBattleShout = value; OnPropertyChanged(nameof(SelectedBattleShout)); } }
        public bool TrueshotAuraEnabled { get => trueshotAuraEnabled; set { trueshotAuraEnabled = value; OnPropertyChanged(nameof(TrueshotAuraEnabled)); } }
        public bool FerociousInspirationEnabled { get => ferociousInspirationEnabled; set { ferociousInspirationEnabled = value; OnPropertyChanged(nameof(FerociousInspirationEnabled)); } }
        public bool EarthTotemEnabled { get => earthTotemEnabled; set { earthTotemEnabled = value; OnPropertyChanged(nameof(EarthTotemEnabled)); } }
        public EarthTotem SelectedEarthTotem
        {
            get => selectedEarthTotem;
            set
            {
                selectedEarthTotem = value;
                if (value == EarthTotem.ImpStrengthOfEarthTotem)
                {
                    selectedAirTotem2 = AirTotem2.ImpGraceOfAirTotem;
                }
                else
                {
                    selectedAirTotem2 = AirTotem2.GraceOfAirTotem;
                }
                OnPropertyChanged(nameof(SelectedAirTotem2));
                OnPropertyChanged(nameof(SelectedEarthTotem));
            }
        }
        public bool AirTotem1Enabled { get => airTotem1Enabled; set { airTotem1Enabled = value; OnPropertyChanged(nameof(AirTotem1Enabled)); } }
        public AirTotem1 SelectedAirTotem1 { get => selectedAirTotem1; set { selectedAirTotem1 = value; OnPropertyChanged(nameof(SelectedAirTotem1)); } }
        public bool AirTotem2Enabled { get => airTotem2Enabled; set { airTotem2Enabled = value; OnPropertyChanged(nameof(AirTotem2Enabled)); } }
        public AirTotem2 SelectedAirTotem2
        {
            get => selectedAirTotem2;
            set
            {
                selectedAirTotem2 = value;
                if (value == AirTotem2.ImpGraceOfAirTotem)
                {
                    selectedEarthTotem = EarthTotem.ImpStrengthOfEarthTotem;
                }
                else
                {
                    selectedEarthTotem = EarthTotem.StrengthOfEarthTotem;
                }
                OnPropertyChanged(nameof(SelectedEarthTotem));
                OnPropertyChanged(nameof(SelectedAirTotem2));
            }
        }
        public bool WaterTotemEnabled { get => waterTotemEnabled; set { waterTotemEnabled = value; OnPropertyChanged(nameof(WaterTotemEnabled)); } }
        public WaterTotem SelectedWaterTotem { get => selectedWaterTotem; set { selectedWaterTotem = value; OnPropertyChanged(nameof(SelectedWaterTotem)); } }
        public bool UnleashedRageEnabled { get => unleashedRageEnabled; set { unleashedRageEnabled = value; OnPropertyChanged(nameof(UnleashedRageEnabled)); } }
        public bool HeroismEnabled { get => heroismEnabled; set { heroismEnabled = value; OnPropertyChanged(nameof(HeroismEnabled)); } }
        public bool LeaderofthePackEnabled { get => leaderofthePackEnabled; set { leaderofthePackEnabled = value; OnPropertyChanged(nameof(LeaderofthePackEnabled)); } }
        public bool HeroicPresenceEnabled { get => heroicPresenceEnabled; set { heroicPresenceEnabled = value; OnPropertyChanged(nameof(HeroicPresenceEnabled)); } }
        public bool InspiringPresenceEnabled { get => inspiringPresenceEnabled; set { inspiringPresenceEnabled = value; OnPropertyChanged(nameof(InspiringPresenceEnabled)); } }
        public bool BlessingofMightEnabled { get => blessingofMightEnabled; set { blessingofMightEnabled = value; OnPropertyChanged(nameof(BlessingofMightEnabled)); } }
        public BlessingofMight SelectedBlessingofMight { get => selectedBlessingofMight; set { selectedBlessingofMight = value; OnPropertyChanged(nameof(SelectedBlessingofMight)); } }
        public bool BlessingofKingsEnabled { get => blessingofKingsEnabled; set { blessingofKingsEnabled = value; OnPropertyChanged(nameof(BlessingofKingsEnabled)); } }
        public bool BlessingofWisdomEnabled { get => blessingofWisdomEnabled; set { blessingofWisdomEnabled = value; OnPropertyChanged(nameof(BlessingofWisdomEnabled)); } }
        public BlessingofWisdom SelectedBlessingofWisdom { get => selectedBlessingofWisdom; set { selectedBlessingofWisdom = value; OnPropertyChanged(nameof(SelectedBlessingofWisdom)); } }
        public bool MarkoftheWildEnabled { get => markoftheWildEnabled; set { markoftheWildEnabled = value; OnPropertyChanged(nameof(MarkoftheWildEnabled)); } }
        public MarkoftheWild SelectedMarkoftheWild { get => selectedMarkoftheWild; set { selectedMarkoftheWild = value; OnPropertyChanged(nameof(SelectedMarkoftheWild)); } }
        public bool PowerWordFortitudeEnabled { get => powerWordFortitudeEnabled; set { powerWordFortitudeEnabled = value; OnPropertyChanged(nameof(PowerWordFortitudeEnabled)); } }
        public PowerWordFortitude SelectedPowerWordFortitude { get => selectedPowerWordFortitude; set { selectedPowerWordFortitude = value; OnPropertyChanged(nameof(SelectedPowerWordFortitude)); } }
        public bool DivineSpiritEnabled { get => divineSpiritEnabled; set { divineSpiritEnabled = value; OnPropertyChanged(nameof(BattleShoutEnabled)); } }
        public DivineSpirit SelectedDivineSpirit { get => selectedDivineSpirit; set { selectedDivineSpirit = value; OnPropertyChanged(nameof(SelectedDivineSpirit)); } }
        public bool ArcaneIntellectEnabled { get => arcaneIntellectEnabled; set { arcaneIntellectEnabled = value; OnPropertyChanged(nameof(ArcaneIntellectEnabled)); } }
        public bool BraidedEterniumChainEnabled { get => braidedEterniumChainEnabled; set { braidedEterniumChainEnabled = value; OnPropertyChanged(nameof(BraidedEterniumChainEnabled)); } }
        public bool EmbraceoftheDawnEnabled { get => embraceoftheDawnEnabled; set { embraceoftheDawnEnabled = value; OnPropertyChanged(nameof(EmbraceoftheDawnEnabled)); } }
        public bool EyeoftheNightEnabled { get => eyeoftheNightEnabled; set { eyeoftheNightEnabled = value; OnPropertyChanged(nameof(EyeoftheNightEnabled)); } }

        public List<Spell> GetGroupTalents()
        {
            HashSet<Spell> result = new();
            if (battleShoutEnabled)
            {
                switch (selectedBattleShout)
                {
                    case BattleShout.BattleShout:
                        break;
                    case BattleShout.BattleShoutT2:
                        result.Add(Spells[23563]);
                        break;
                    case BattleShout.BattleShoutSolarian:
                        result.Add(Spells[37536]);
                        break;
                    case BattleShout.BattleShoutSolarianAndT2:
                        result.Add(Spells[23563]);
                        result.Add(Spells[37536]);
                        break;
                    case BattleShout.ImpBattleShout:
                        result.Add(Spells[12861]);
                        break;
                    case BattleShout.ImpBattleShoutT2:
                        result.Add(Spells[12861]);
                        result.Add(Spells[23563]);
                        break;
                    case BattleShout.ImpBattleShoutSolarian:
                        result.Add(Spells[12861]);
                        result.Add(Spells[37536]);
                        break;
                    case BattleShout.ImpBattleShoutSolarianAndT2:
                        result.Add(Spells[12861]);
                        result.Add(Spells[37536]);
                        result.Add(Spells[23563]);
                        break;
                }
            }
            if (airTotem1Enabled && selectedAirTotem1 != AirTotem1.WindfuryTotem && Spells.ContainsKey((int)selectedAirTotem1))
            {
                result.Add(Spells[(int)selectedAirTotem1]);
            }
            if ((earthTotemEnabled && selectedEarthTotem == EarthTotem.ImpStrengthOfEarthTotem) || (airTotem2Enabled && selectedAirTotem2 == AirTotem2.ImpGraceOfAirTotem))
            {
                result.Add(Spells[(int)EarthTotem.ImpStrengthOfEarthTotem]);
                result.Add(Spells[(int)AirTotem2.ImpGraceOfAirTotem]);
            }
            if (waterTotemEnabled && selectedWaterTotem != WaterTotem.ManaSpringTotem && Spells.ContainsKey((int)selectedWaterTotem))
            {
                result.Add(Spells[(int)selectedWaterTotem]);
            }
            if (blessingofMightEnabled && selectedBlessingofMight != BlessingofMight.BlessingofMight && Spells.ContainsKey((int)selectedBlessingofMight))
            {
                result.Add(Spells[(int)selectedBlessingofMight]);
            }
            if (blessingofWisdomEnabled && selectedBlessingofWisdom != BlessingofWisdom.BlessingofWisdom && Spells.ContainsKey((int)selectedBlessingofWisdom))
            {
                result.Add(Spells[(int)selectedBlessingofWisdom]);
            }
            if (markoftheWildEnabled && selectedMarkoftheWild != MarkoftheWild.MarkoftheWild && Spells.ContainsKey((int)selectedMarkoftheWild))
            {
                result.Add(Spells[(int)selectedMarkoftheWild]);
            }
            if (powerWordFortitudeEnabled && selectedPowerWordFortitude != PowerWordFortitude.PowerWordFortitude && Spells.ContainsKey((int)selectedPowerWordFortitude))
            {
                result.Add(Spells[(int)selectedPowerWordFortitude]);
            }
            if (divineSpiritEnabled && selectedDivineSpirit != DivineSpirit.DivineSpirit && Spells.ContainsKey((int)selectedDivineSpirit))
            {
                result.Add(Spells[(int)selectedDivineSpirit]);
            }
            return new List<Spell>(result);
        }

        public List<Spell> GetBuffs()
        {
            HashSet<Spell> result = new();
            if (battleShoutEnabled && Spells.ContainsKey((int)BattleShout.BattleShout))
            {
                result.Add(Spells[(int)BattleShout.BattleShout]);
            }
            if (trueshotAuraEnabled && Spells.ContainsKey(27066))
            {
                result.Add(Spells[27066]);
            }
            if (ferociousInspirationEnabled && Spells.ContainsKey(34460))
            {
                result.Add(Spells[34460]);
            }
            if (earthTotemEnabled && Spells.ContainsKey((int)EarthTotem.StrengthOfEarthTotem))
            {
                result.Add(Spells[(int)EarthTotem.StrengthOfEarthTotem]);
            }
            if (airTotem1Enabled && Spells.ContainsKey((int)AirTotem1.WindfuryTotem))
            {
                result.Add(Spells[(int)AirTotem1.WindfuryTotem]);
            }
            if (airTotem2Enabled && Spells.ContainsKey((int)AirTotem2.GraceOfAirTotem))
            {
                result.Add(Spells[(int)AirTotem2.GraceOfAirTotem]);
            }
            if (waterTotemEnabled && Spells.ContainsKey((int)WaterTotem.ManaSpringTotem))
            {
                result.Add(Spells[(int)WaterTotem.ManaSpringTotem]);
            }
            if (unleashedRageEnabled && Spells.ContainsKey(30811))
            {
                result.Add(Spells[30811]);
            }
            //if (heroismEnabled && Spells.ContainsKey(32182))
            //{
            //result.Add(Spells[32182]);
            //}
            if (leaderofthePackEnabled && Spells.ContainsKey(17007))
            {
                result.Add(Spells[17007]);
            }
            if (heroicPresenceEnabled && Spells.ContainsKey(6562))
            {
                result.Add(Spells[6562]);
            }
            if (inspiringPresenceEnabled && Spells.ContainsKey(28878))
            {
                result.Add(Spells[28878]);
            }
            if (blessingofMightEnabled && Spells.ContainsKey((int)BlessingofMight.BlessingofMight))
            {
                result.Add(Spells[(int)BlessingofMight.BlessingofMight]);
            }
            if (blessingofKingsEnabled && Spells.ContainsKey(25898))
            {
                result.Add(Spells[25898]);
            }
            if (blessingofWisdomEnabled && Spells.ContainsKey((int)BlessingofWisdom.BlessingofWisdom))
            {
                result.Add(Spells[(int)BlessingofWisdom.BlessingofWisdom]);
            }
            if (markoftheWildEnabled && Spells.ContainsKey((int)MarkoftheWild.MarkoftheWild))
            {
                result.Add(Spells[(int)MarkoftheWild.MarkoftheWild]);
            }
            if (powerWordFortitudeEnabled && Spells.ContainsKey((int)PowerWordFortitude.PowerWordFortitude))
            {
                result.Add(Spells[(int)PowerWordFortitude.PowerWordFortitude]);
            }
            if (divineSpiritEnabled && Spells.ContainsKey((int)DivineSpirit.DivineSpirit))
            {
                result.Add(Spells[(int)DivineSpirit.DivineSpirit]);
            }
            if (arcaneIntellectEnabled && Spells.ContainsKey(27127))
            {
                result.Add(Spells[27127]);
            }
            if (braidedEterniumChainEnabled && Spells.ContainsKey(31025))
            {
                result.Add(Spells[31025]);
            }
            if (embraceoftheDawnEnabled && Spells.ContainsKey(31026))
            {
                result.Add(Spells[31026]);
            }
            if (eyeoftheNightEnabled && Spells.ContainsKey(31033))
            {
                result.Add(Spells[31033]);
            }
            return new List<Spell>(result);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum BattleShout
    {
        BattleShout = 2048,
        BattleShoutT2 = 23563,
        BattleShoutSolarian = 37536,
        BattleShoutSolarianAndT2 = 37537,
        ImpBattleShout = 12861,
        ImpBattleShoutT2 = 12862,
        ImpBattleShoutSolarian = 12863,
        ImpBattleShoutSolarianAndT2 = 12864,
    }

    public enum EarthTotem
    {
        StrengthOfEarthTotem = 25528,
        ImpStrengthOfEarthTotem = 16295
    }

    public enum AirTotem1
    {
        WindfuryTotem = 25580,
        ImpWindfuryTotem = 29193
    }

    public enum AirTotem2
    {
        GraceOfAirTotem = 25359,
        ImpGraceOfAirTotem = 16295
    }

    public enum WaterTotem
    {
        ManaSpringTotem = 25570,
        ImpManaSpringTotem = 16208
    }

    public enum BlessingofMight
    {
        BlessingofMight = 27141,
        ImpBlessingofMight = 20048
    }
    public enum BlessingofWisdom
    {
        BlessingofWisdom = 27143,
        ImpBlessingofWisdom = 20245
    }
    public enum MarkoftheWild
    {
        MarkoftheWild = 26991,
        ImpMarkoftheWild = 17055
    }
    public enum PowerWordFortitude
    {
        PowerWordFortitude = 25392,
        ImpPowerWordFortitude = 14767
    }
    public enum DivineSpirit
    {
        DivineSpirit = 32999,
        ImpDivineSpirit = 33182
    }
}
