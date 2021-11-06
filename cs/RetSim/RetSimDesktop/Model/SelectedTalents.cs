using System.ComponentModel;

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

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
