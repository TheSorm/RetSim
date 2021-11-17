using RetSim.Spells;
using System.Collections.Generic;
using System.ComponentModel;
using static RetSim.Data.Collections;

namespace RetSimDesktop.Model
{
    public class SelectedBuffs : INotifyPropertyChanged
    {
        private bool battleShoutEnabled;
        private BattleShout selectedBattleShout;

        private bool trueshotAuraEnabled;
        private bool ferociousInspirationEnabled;

        private bool earthTotemEnabled;
        private EarthTotem selectedEarthTotem;

        private bool airTotem1Enabled;
        private AirTotem1 selectedAirTotem1;

        private bool airTotem2Enabled;
        private AirTotem2 selectedAirTotem2;

        private bool waterTotemEnabled;
        private WaterTotem selectedWaterTotem;

        private bool unleashedRageEnabled;
        private bool heroismEnabled;
        private bool leaderofthePackEnabled;
        private bool heroicPresenceEnabled;
        private bool inspiringPresenceEnabled;

        private bool blessingofMightEnabled;
        private BlessingofMight selectedBlessingofMight;

        private bool blessingofKingsEnabled;

        private bool blessingofWisdomEnabled;
        private BlessingofWisdom selectedBlessingofWisdom;

        private bool markoftheWildEnabled;
        private MarkoftheWild selectedMarkoftheWild;

        private bool powerWordFortitudeEnabled;
        private PowerWordFortitude selectedPowerWordFortitude;

        private bool divineSpiritEnabled;
        private DivineSpirit selectedDivineSpirit;

        private bool arcaneIntellectEnabled;

        public bool BattleShoutEnabled { get => battleShoutEnabled; set { battleShoutEnabled = value; OnPropertyChanged(nameof(BattleShoutEnabled)); } }
        public BattleShout SelectedBattleShout { get => selectedBattleShout; set { selectedBattleShout = value; OnPropertyChanged(nameof(SelectedBattleShout)); } }
        public bool TrueshotAuraEnabled { get => trueshotAuraEnabled; set { trueshotAuraEnabled = value; OnPropertyChanged(nameof(TrueshotAuraEnabled)); } }
        public bool FerociousInspirationEnabled { get => ferociousInspirationEnabled; set { ferociousInspirationEnabled = value; OnPropertyChanged(nameof(FerociousInspirationEnabled)); } }
        public bool EarthTotemEnabled { get => earthTotemEnabled; set { earthTotemEnabled = value; OnPropertyChanged(nameof(EarthTotemEnabled)); } }
        public EarthTotem SelectedEarthTotem { get => selectedEarthTotem; set { selectedEarthTotem = value; OnPropertyChanged(nameof(SelectedEarthTotem)); } }
        public bool AirTotem1Enabled { get => airTotem1Enabled; set { airTotem1Enabled = value; OnPropertyChanged(nameof(AirTotem1Enabled)); } }
        public AirTotem1 SelectedAirTotem1 { get => selectedAirTotem1; set { selectedAirTotem1 = value; OnPropertyChanged(nameof(SelectedAirTotem1)); } }
        public bool AirTotem2Enabled { get => airTotem2Enabled; set { airTotem2Enabled = value; OnPropertyChanged(nameof(AirTotem2Enabled)); } }
        public AirTotem2 SelectedAirTotem2 { get => selectedAirTotem2; set { selectedAirTotem2 = value; OnPropertyChanged(nameof(SelectedAirTotem2)); } }
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


        public List<Spell> GetBuffs()
        {
            List<Spell> result = new();
            if (battleShoutEnabled && Spells.ContainsKey((int)selectedBattleShout))
            {
                result.Add(Spells[(int)selectedBattleShout]);
            }
            if (trueshotAuraEnabled && Spells.ContainsKey(27066))
            {
                result.Add(Spells[27066] );
            }
            if (ferociousInspirationEnabled && Spells.ContainsKey(34460))
            {
                result.Add(Spells[34460]);
            }
            if (earthTotemEnabled && Spells.ContainsKey((int)selectedEarthTotem))
            {
                result.Add(Spells[(int)selectedEarthTotem]);
            }
            if (airTotem1Enabled && Spells.ContainsKey((int)selectedAirTotem1))
            {
                result.Add(Spells[(int)selectedAirTotem1]);
            }
            if (airTotem2Enabled && Spells.ContainsKey((int)selectedAirTotem2))
            {
                result.Add(Spells[(int)selectedAirTotem2]);
            }
            if (waterTotemEnabled && Spells.ContainsKey((int)selectedWaterTotem))
            {
                result.Add(Spells[(int)selectedWaterTotem]);
            }
            if (unleashedRageEnabled && Spells.ContainsKey(30811))
            {
                result.Add(Spells[30811]);
            }
            if (heroismEnabled && Spells.ContainsKey(32182))
            {
                result.Add(Spells[32182]);
            }
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
            if (blessingofMightEnabled && Spells.ContainsKey((int)selectedBlessingofMight))
            {
                result.Add(Spells[(int)selectedBlessingofMight]);
            }
            if (blessingofKingsEnabled && Spells.ContainsKey(25898))
            {
                result.Add(Spells[25898]);
            }
            if (blessingofWisdomEnabled && Spells.ContainsKey((int)selectedBlessingofWisdom))
            {
                result.Add(Spells[(int)selectedBlessingofWisdom]);
            }
            if (markoftheWildEnabled && Spells.ContainsKey((int)selectedMarkoftheWild))
            {
                result.Add(Spells[(int)selectedMarkoftheWild]);
            }
            if (powerWordFortitudeEnabled && Spells.ContainsKey((int)selectedPowerWordFortitude))
            {
                result.Add(Spells[(int)selectedPowerWordFortitude]);
            }
            if (divineSpiritEnabled && Spells.ContainsKey((int)selectedDivineSpirit))
            {
                result.Add(Spells[(int)selectedDivineSpirit]);
            }
            if (arcaneIntellectEnabled && Spells.ContainsKey(27127))
            {
                result.Add(Spells[27127]);
            }
            return result;
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
        ImpStrengthOfEarthTotem = 25364
    }

    public enum AirTotem1
    {
        WindfuryTotem = 25580,
        ImpWindfuryTotem = 29193
    }

    public enum AirTotem2
    {
        GraceOfAirTotem = 25359,
        ImpGraceOfAirTotem = 25363
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
