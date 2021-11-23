using RetSim.Spells;
using System.Collections.Generic;
using System.ComponentModel;
using static RetSim.Data.Collections;

namespace RetSimDesktop.Model
{
    public class SelectedTalents : INotifyPropertyChanged
    {
        private bool divineStrengthEnabled = true;
        private bool divineIntellectEnabled = true;
        private bool precisionEnabled = true;
        private bool benedictionEnabled = true;
        private bool improvedJudgementEnabled = true;
        private bool convictionEnabled = true;
        private bool crusadeEnabled = true;
        private bool twoHandedWeaponSpecializationEnabled = true;
        private bool sanctityAuraEnabled = true;
        private bool improvedSanctityAuraEnabled = true;
        private bool vengeanceEnabled = true;
        private bool sanctifiedJudgementEnabled = true;
        private bool sanctifiedSealsEnabled = true;
        private bool fanaticismEnabled = true;

        private int divineStrengthRank = 5;
        private int divineIntellectRank = 5;
        private int precisionRank = 3;
        private int benedictionRank = 5;
        private int improvedJudgementRank = 2;
        private int convictionRank = 5;
        private int crusadeRank = 3;
        private int twoHandedWeaponSpecializationRank = 3;
        private int improvedSanctityAuraRank = 2;
        private int vengeanceRank = 5;
        private int sanctifiedJudgementRank = 3;
        private int sanctifiedSealsRank = 3;
        private int fanaticismRank = 5;


        private static readonly int divineStrengthID = 20262;
        private static readonly int divineIntellectID = 20257;
        private static readonly int precisionID = 20189;
        private static readonly int benedictionID = 20101;
        private static readonly int improvedJudgementID = 25956;
        private static readonly int convictionID = 20117;
        private static readonly int crusadeID = 31866;
        private static readonly int twoHandedWeaponSpecializationID = 20111;
        private static readonly int sanctityAuraID = 20218;
        private static readonly int improvedSanctityAuraID = 31869;
        private static readonly int vengeanceID = 20049;
        private static readonly int sanctifiedJudgementID = 31876;
        private static readonly int sanctifiedSealsID = 32043;
        private static readonly int fanaticismID = 31879;

        public bool DivineStrengthEnabled { get => divineStrengthEnabled; set { divineStrengthEnabled = value; OnPropertyChanged(nameof(DivineStrengthEnabled)); } }
        public bool DivineIntellectEnabled { get => divineIntellectEnabled; set { divineIntellectEnabled = value; OnPropertyChanged(nameof(DivineIntellectEnabled)); } }
        public bool PrecisionEnabled { get => precisionEnabled; set { precisionEnabled = value; OnPropertyChanged(nameof(PrecisionEnabled)); } }
        public bool BenedictionEnabled { get => benedictionEnabled; set { benedictionEnabled = value; OnPropertyChanged(nameof(BenedictionEnabled)); } }
        public bool ImprovedJudgementEnabled { get => improvedJudgementEnabled; set { improvedJudgementEnabled = value; OnPropertyChanged(nameof(ImprovedJudgementEnabled)); } }
        public bool ConvictionEnabled { get => convictionEnabled; set { convictionEnabled = value; OnPropertyChanged(nameof(ConvictionEnabled)); } }
        public bool CrusadeEnabled { get => crusadeEnabled; set { crusadeEnabled = value; OnPropertyChanged(nameof(CrusadeEnabled)); } }
        public bool TwoHandedWeaponSpecializationEnabled { get => twoHandedWeaponSpecializationEnabled; set { twoHandedWeaponSpecializationEnabled = value; OnPropertyChanged(nameof(TwoHandedWeaponSpecializationEnabled)); } }
        public bool SanctityAuraEnabled { get => sanctityAuraEnabled; set { sanctityAuraEnabled = value; OnPropertyChanged(nameof(SanctityAuraEnabled)); } }
        public bool ImprovedSanctityAuraEnabled { get => improvedSanctityAuraEnabled; set { improvedSanctityAuraEnabled = value; OnPropertyChanged(nameof(ImprovedSanctityAuraEnabled)); } }
        public bool VengeanceEnabled { get => vengeanceEnabled; set { vengeanceEnabled = value; OnPropertyChanged(nameof(VengeanceEnabled)); } }
        public bool SanctifiedJudgementEnabled { get => sanctifiedJudgementEnabled; set { sanctifiedJudgementEnabled = value; OnPropertyChanged(nameof(SanctifiedJudgementEnabled)); } }
        public bool SanctifiedSealsEnabled { get => sanctifiedSealsEnabled; set { sanctifiedSealsEnabled = value; OnPropertyChanged(nameof(SanctifiedSealsEnabled)); } }
        public bool FanaticismEnabled { get => fanaticismEnabled; set { fanaticismEnabled = value; OnPropertyChanged(nameof(FanaticismEnabled)); } }
        public int DivineStrengthRank { get => divineStrengthRank; set { divineStrengthRank = value; OnPropertyChanged(nameof(DivineStrengthRank)); } }
        public int DivineIntellectRank { get => divineIntellectRank; set { divineIntellectRank = value; OnPropertyChanged(nameof(DivineIntellectRank)); } }
        public int PrecisionRank { get => precisionRank; set { precisionRank = value; OnPropertyChanged(nameof(PrecisionRank)); } }
        public int BenedictionRank { get => benedictionRank; set { benedictionRank = value; OnPropertyChanged(nameof(BenedictionRank)); } }
        public int ImprovedJudgementRank { get => improvedJudgementRank; set { improvedJudgementRank = value; OnPropertyChanged(nameof(ImprovedJudgementRank)); } }
        public int ConvictionRank { get => convictionRank; set { convictionRank = value; OnPropertyChanged(nameof(ConvictionRank)); } }
        public int CrusadeRank { get => crusadeRank; set { crusadeRank = value; OnPropertyChanged(nameof(CrusadeRank)); } }
        public int TwoHandedWeaponSpecializationRank { get => twoHandedWeaponSpecializationRank; set { twoHandedWeaponSpecializationRank = value; OnPropertyChanged(nameof(TwoHandedWeaponSpecializationRank)); } }
        public int ImprovedSanctityAuraRank { get => improvedSanctityAuraRank; set { improvedSanctityAuraRank = value; OnPropertyChanged(nameof(ImprovedSanctityAuraRank)); } }
        public int VengeanceRank { get => vengeanceRank; set { vengeanceRank = value; OnPropertyChanged(nameof(VengeanceRank)); } }
        public int SanctifiedJudgementRank { get => sanctifiedJudgementRank; set { sanctifiedJudgementRank = value; OnPropertyChanged(nameof(SanctifiedJudgementRank)); } }
        public int SanctifiedSealsRank { get => sanctifiedSealsRank; set { sanctifiedSealsRank = value; OnPropertyChanged(nameof(SanctifiedSealsRank)); } }
        public int FanaticismRank { get => fanaticismRank; set { fanaticismRank = value; OnPropertyChanged(nameof(FanaticismRank)); } }

        public List<Talent> GetTalentList()
        {
            List<Talent> talents = new();
            if (divineStrengthEnabled && Talents.ContainsKey(divineStrengthID + divineStrengthRank - 1))
            {
                talents.Add(Talents[divineStrengthID + divineStrengthRank - 1]);
            }
            if (divineIntellectEnabled && Talents.ContainsKey(divineIntellectID + divineIntellectRank - 1))
            {
                talents.Add(Talents[divineIntellectID + divineIntellectRank - 1]);
            }
            if (precisionEnabled && precisionRank == 1 && Talents.ContainsKey(precisionID))
            {
                talents.Add(Talents[precisionID]);
            }
            else if (precisionEnabled && Talents.ContainsKey(precisionID + 1 + precisionRank))
            {
                talents.Add(Talents[precisionID + 1 + precisionRank]);
            }
            if (benedictionEnabled && Talents.ContainsKey(benedictionID + benedictionRank - 1))
            {
                talents.Add(Talents[benedictionID + benedictionRank - 1]);
            }
            if (improvedJudgementEnabled && Talents.ContainsKey(improvedJudgementID + improvedJudgementRank - 1))
            {
                talents.Add(Talents[improvedJudgementID + improvedJudgementRank - 1]);
            }
            if (convictionEnabled && Talents.ContainsKey(convictionID + convictionRank - 1))
            {
                talents.Add(Talents[convictionID + convictionRank - 1]);
            }
            if (crusadeEnabled && Talents.ContainsKey(crusadeID + crusadeRank - 1))
            {
                talents.Add(Talents[crusadeID + crusadeRank - 1]);
            }
            if (twoHandedWeaponSpecializationEnabled && Talents.ContainsKey(twoHandedWeaponSpecializationID + twoHandedWeaponSpecializationRank - 1))
            {
                talents.Add(Talents[twoHandedWeaponSpecializationID + twoHandedWeaponSpecializationRank - 1]);
            }
            if (sanctityAuraEnabled && Talents.ContainsKey(sanctityAuraID))
            {
                talents.Add(Talents[sanctityAuraID]);
            }
            if (improvedSanctityAuraEnabled && Talents.ContainsKey(improvedSanctityAuraID + improvedSanctityAuraRank - 1))
            {
                talents.Add(Talents[improvedSanctityAuraID + improvedSanctityAuraRank - 1]);
            }
            if (vengeanceEnabled && vengeanceRank == 1 && Talents.ContainsKey(vengeanceID))
            {
                talents.Add(Talents[vengeanceID]);
            }
            else if (vengeanceEnabled && Talents.ContainsKey(vengeanceID + 5 + vengeanceRank))
            {
                talents.Add(Talents[vengeanceID + 5 + vengeanceRank]);
            }
            if (sanctifiedJudgementEnabled && Talents.ContainsKey(sanctifiedJudgementID + sanctifiedJudgementRank - 1))
            {
                talents.Add(Talents[sanctifiedJudgementID + sanctifiedJudgementRank - 1]);
            }
            if (sanctifiedSealsEnabled && sanctifiedSealsRank == 1 && Talents.ContainsKey(sanctifiedSealsID))
            {
                talents.Add(Talents[sanctifiedSealsID]);
            }
            else if (sanctifiedSealsEnabled && Talents.ContainsKey(sanctifiedSealsID + 3351 + sanctifiedSealsRank))
            {
                talents.Add(Talents[sanctifiedSealsID + 3351 + sanctifiedSealsRank]);
            }
            if (fanaticismEnabled && Talents.ContainsKey(fanaticismID + fanaticismRank - 1))
            {
                talents.Add(Talents[fanaticismID + fanaticismRank - 1]);
            }
            return talents;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
