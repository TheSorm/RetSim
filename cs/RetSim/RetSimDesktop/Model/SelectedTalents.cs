using RetSim.Spells;
using RetSimDesktop.ViewModel;
using System.Collections.Generic;
using System.ComponentModel;
using static RetSim.Data.Spells;

namespace RetSimDesktop.Model
{
    public class SelectedTalents : INotifyPropertyChanged
    {
        private bool convictionEnabled;
        private bool crusadeEnabled;
        private bool twoHandedWeaponSpecializationEnabled;
        private bool sanctityAuraEnabled;
        private bool improvedSanctityAuraEnabled;
        private bool vengeanceEnabled;
        private bool fanaticismEnabled;
        private bool sanctifiedSealsEnabled;
        private bool precisionEnabled;
        private bool divineStrengthEnabled;

        public bool ConvictionEnabled
        {
            get { return convictionEnabled; }
            set
            {
                convictionEnabled = value;
                OnPropertyChanged(nameof(ConvictionEnabled));
            }
        }

        public bool CrusadeEnabled
        {
            get { return crusadeEnabled; }
            set
            {
                crusadeEnabled = value;
                OnPropertyChanged(nameof(CrusadeEnabled));
            }
        }

        public bool TwoHandedWeaponSpecializationEnabled
        {
            get { return twoHandedWeaponSpecializationEnabled; }
            set
            {
                twoHandedWeaponSpecializationEnabled = value;
                OnPropertyChanged(nameof(TwoHandedWeaponSpecializationEnabled));
            }
        }

        public bool SanctityAuraEnabled
        {
            get { return sanctityAuraEnabled; }
            set
            {
                sanctityAuraEnabled = value;
                OnPropertyChanged(nameof(SanctityAuraEnabled));
            }
        }

        public bool ImprovedSanctityAuraEnabled
        {
            get { return improvedSanctityAuraEnabled; }
            set
            {
                improvedSanctityAuraEnabled = value;
                OnPropertyChanged(nameof(ImprovedSanctityAuraEnabled));
            }
        }

        public bool VengeanceEnabled
        {
            get { return vengeanceEnabled; }
            set
            {
                vengeanceEnabled = value;
                OnPropertyChanged(nameof(VengeanceEnabled));
            }
        }

        public bool FanaticismEnabled
        {
            get { return fanaticismEnabled; }
            set
            {
                fanaticismEnabled = value;
                OnPropertyChanged(nameof(FanaticismEnabled));
            }
        }

        public bool SanctifiedSealsEnabled
        {
            get { return sanctifiedSealsEnabled; }
            set
            {
                sanctifiedSealsEnabled = value;
                OnPropertyChanged(nameof(SanctifiedSealsEnabled));
            }
        }

        public bool PrecisionEnabled
        {
            get { return precisionEnabled; }
            set
            {
                precisionEnabled = value;
                OnPropertyChanged(nameof(PrecisionEnabled));
            }
        }

        public bool DivineStrengthEnabled
        {
            get { return divineStrengthEnabled; }
            set
            {
                divineStrengthEnabled = value;
                OnPropertyChanged(nameof(DivineStrengthEnabled));
            }
        }

        public static List<Talent> GetTalentList(RetSimUIModel retSimUIModel)
        {
            List<Talent> talents = new();
            if (retSimUIModel.SelectedTalents.ConvictionEnabled)
            {
                talents.Add(Conviction);
            }
            if (retSimUIModel.SelectedTalents.CrusadeEnabled)
            {
                talents.Add(Crusade);
            }
            if (retSimUIModel.SelectedTalents.DivineStrengthEnabled)
            {
                talents.Add(DivineStrength);
            }
            if (retSimUIModel.SelectedTalents.FanaticismEnabled)
            {
                talents.Add(Fanaticism);
            }
            if (retSimUIModel.SelectedTalents.ImprovedSanctityAuraEnabled)
            {
                talents.Add(ImprovedSanctityAura);
            }
            if (retSimUIModel.SelectedTalents.PrecisionEnabled)
            {
                talents.Add(Precision);
            }
            if (retSimUIModel.SelectedTalents.SanctifiedSealsEnabled)
            {
                talents.Add(SanctifiedSeals);
            }
            if (retSimUIModel.SelectedTalents.SanctityAuraEnabled)
            {
                talents.Add(SanctityAura);
            }
            if (retSimUIModel.SelectedTalents.TwoHandedWeaponSpecializationEnabled)
            {
                talents.Add(TwoHandedWeaponSpecialization);
            }
            if (retSimUIModel.SelectedTalents.VengeanceEnabled)
            {
                talents.Add(Vengeance);
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
