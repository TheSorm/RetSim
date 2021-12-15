using RetSim.Spells;
using System.Collections.Generic;
using System.ComponentModel;
using static RetSim.Data.Collections;

namespace RetSimDesktop.Model
{
    public class SelectedCooldowns : INotifyPropertyChanged
    {
        private PrimaryPotion selectedPrimaryPotion = PrimaryPotion.HastePotion;
        private SecondaryPotion selectedSecondaryPotion = SecondaryPotion.DemonicDarkRune;

        private bool drumEnabled = true;
        private Drum selectedDrum = Drum.DrumsOfBattle;

        private bool superSapperChargeEnabled = false;

        public PrimaryPotion SelectedPrimaryPotion
        {
            get { return selectedPrimaryPotion; }
            set
            {
                selectedPrimaryPotion = value;
                OnPropertyChanged(nameof(SelectedPrimaryPotion));
            }
        }

        public SecondaryPotion SelectedSecondaryPotion
        {
            get { return selectedSecondaryPotion; }
            set
            {
                selectedSecondaryPotion = value;
                OnPropertyChanged(nameof(SelectedSecondaryPotion));
            }
        }

        public bool DrumEnabled
        {
            get { return drumEnabled; }
            set
            {
                drumEnabled = value;
                OnPropertyChanged(nameof(DrumEnabled));
            }
        }
        public Drum SelectedDrum
        {
            get { return selectedDrum; }
            set
            {
                selectedDrum = value;
                OnPropertyChanged(nameof(SelectedDrum));
            }
        }

        public bool SuperSapperChargeEnabled
        {
            get { return superSapperChargeEnabled; }
            set
            {
                superSapperChargeEnabled = value;
                OnPropertyChanged(nameof(SuperSapperChargeEnabled));
            }
        }

        public List<Spell> GetCooldowns()
        {
            List<Spell> result = new() { Spells[31884] }; //Avenging Wrath
            if (selectedPrimaryPotion != PrimaryPotion.None && Spells.ContainsKey((int)selectedPrimaryPotion))
            {
                result.Add(Spells[(int)selectedPrimaryPotion]);
            }
            if (selectedSecondaryPotion != SecondaryPotion.None && Spells.ContainsKey((int)selectedSecondaryPotion))
            {
                result.Add(Spells[(int)selectedSecondaryPotion]);
            }
            if (drumEnabled && Spells.ContainsKey((int)selectedDrum))
            {
                result.Add(Spells[(int)selectedDrum]);
            }
            if (superSapperChargeEnabled && Spells.ContainsKey(30486))
            {
                result.Add(Spells[30486]);
            }

            return result;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum PrimaryPotion
    {
        None = 0,
        HastePotion = 28507,
        InsaneStrengthPotion = 28494,
        SuperManaPotion = 28499,
        MadAlchemistsPotion = 45051,
    }
    public enum SecondaryPotion
    {
        None = 0,
        DemonicDarkRune = 27869,
        FlameCap = 28714,
    }
    public enum Drum
    {
        DrumsOfBattle = 35476,
        DrumsOfWar = 35475,
    }
}
