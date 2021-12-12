using RetSim.Spells;
using System.Collections.Generic;
using System.ComponentModel;
using static RetSim.Data.Collections;

namespace RetSimDesktop.Model
{
    public class SelectedConsumables : INotifyPropertyChanged
    {
        private static readonly HashSet<BattleElixir> flasks = new() { BattleElixir.FlaskofRelentlessAssault };

        private BattleElixir selectedBattleElixir = BattleElixir.FlaskofRelentlessAssault;
        private GuardianElixir selectedGuardianElixir = GuardianElixir.None;
        private TemporaryWeaponEnchantment selectedTemporaryWeaponEnchantment = TemporaryWeaponEnchantment.None;
        private Food selectedFood = Food.Strength20Food;

        private bool scrollOfStrengthEnabled = true;
        private ScrollOfStrength selectedScrollOfStrength = ScrollOfStrength.ScrollofStrengthV;

        private bool scrollOfAgilityEnabled = true;
        private ScrollOfAgility selectedScrollOfAgility = ScrollOfAgility.ScrollOfAgilityV;

        private bool gnomishBattleChickenEnabled = true;

        public BattleElixir SelectedBattleElixir
        {
            get { return selectedBattleElixir; }
            set
            {
                selectedBattleElixir = value;
                if (flasks.Contains(selectedBattleElixir))
                {
                    selectedGuardianElixir = GuardianElixir.None;
                    OnPropertyChanged(nameof(SelectedGuardianElixir));
                }
                OnPropertyChanged(nameof(SelectedBattleElixir));
            }
        }

        public GuardianElixir SelectedGuardianElixir
        {
            get { return selectedGuardianElixir; }
            set
            {
                selectedGuardianElixir = value;
                if (flasks.Contains(selectedBattleElixir) && selectedGuardianElixir != GuardianElixir.None)
                {
                    selectedBattleElixir = BattleElixir.None;
                    OnPropertyChanged(nameof(SelectedBattleElixir));
                }
                OnPropertyChanged(nameof(SelectedGuardianElixir));
            }
        }

        public TemporaryWeaponEnchantment SelectedTemporaryWeaponEnchantment
        {
            get { return selectedTemporaryWeaponEnchantment; }
            set
            {
                selectedTemporaryWeaponEnchantment = value;
                OnPropertyChanged(nameof(SelectedTemporaryWeaponEnchantment));
            }
        }

        public Food SelectedFood
        {
            get { return selectedFood; }
            set
            {
                selectedFood = value;
                OnPropertyChanged(nameof(SelectedFood));
            }
        }

        public bool ScrollOfStrengthEnabled
        {
            get { return scrollOfStrengthEnabled; }
            set
            {
                scrollOfStrengthEnabled = value;
                OnPropertyChanged(nameof(ScrollOfStrengthEnabled));
            }
        }
        public ScrollOfStrength SelectedScrollOfStrength
        {
            get { return selectedScrollOfStrength; }
            set
            {
                selectedScrollOfStrength = value;
                OnPropertyChanged(nameof(SelectedScrollOfStrength));
            }
        }

        public bool ScrollOfAgilityEnabled
        {
            get { return scrollOfAgilityEnabled; }
            set
            {
                scrollOfAgilityEnabled = value;
                OnPropertyChanged(nameof(ScrollOfAgilityEnabled));
            }
        }
        public ScrollOfAgility SelectedScrollOfAgility
        {
            get { return selectedScrollOfAgility; }
            set
            {
                selectedScrollOfAgility = value;
                OnPropertyChanged(nameof(SelectedScrollOfAgility));
            }
        }

        public bool GnomishBattleChickenEnabled
        {
            get { return gnomishBattleChickenEnabled; }
            set
            {
                gnomishBattleChickenEnabled = value;
                OnPropertyChanged(nameof(GnomishBattleChickenEnabled));
            }
        }
        public List<Spell> GetConsumables()
        {
            List<Spell> result = new();
            if (selectedBattleElixir != BattleElixir.None && Spells.ContainsKey((int)selectedBattleElixir))
            {
                result.Add(Spells[(int)selectedBattleElixir]);
            }
            if (selectedGuardianElixir != GuardianElixir.None && Spells.ContainsKey((int)selectedGuardianElixir))
            {
                result.Add(Spells[(int)selectedGuardianElixir]);
            }
            if (selectedTemporaryWeaponEnchantment != TemporaryWeaponEnchantment.None && Spells.ContainsKey((int)selectedTemporaryWeaponEnchantment))
            {
                result.Add(Spells[(int)selectedTemporaryWeaponEnchantment]);
            }
            if (selectedFood != Food.None && Spells.ContainsKey((int)selectedFood))
            {
                result.Add(Spells[(int)selectedFood]);
            }
            if (scrollOfStrengthEnabled && Spells.ContainsKey((int)selectedScrollOfStrength))
            {
                result.Add(Spells[(int)selectedScrollOfStrength]);
            }
            if (scrollOfAgilityEnabled && Spells.ContainsKey((int)selectedScrollOfAgility))
            {
                result.Add(Spells[(int)selectedScrollOfAgility]);
            }
            if (gnomishBattleChickenEnabled && Spells.ContainsKey(23060))
            {
                result.Add(Spells[23060]);
            }
            return result;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum BattleElixir
    {
        None = 0,
        FlaskofRelentlessAssault = 28520,
        ElixirofDemonslaying = 11406,
        FelStrengthElixir = 38954,
        ElixirofMajorStrength = 28490,
        ElixirofMajorAgility = 28497,
        ElixirofMastery = 33726,
        ElixiroftheMongoose = 17538
    }

    public enum GuardianElixir
    {
        None = 0,
        ElixirofMajorMageblood = 28509,
        ElixirofDraenicWisdom = 39627,
        ElixirofMajorFortitude = 39625,
        SpiritofZanza = 24382,
    }
    public enum TemporaryWeaponEnchantment
    {
        None = 0,
        AdamantiteSharpeningStone = 29453,
        ElementalSharpeningStone = 22756,
        SuperiorWizardOil = 28017,
        BrilliantWizardOil = 25122,
    }

    public enum Food
    {
        None = 0,
        Strength20Food = 33256,
        Agility20Food = 33261,
        SpellPower23Food = 33263,
        HitRating20Food = 43763,
        MP58Food = 33265,
    }
    public enum ScrollOfStrength
    {
        ScrollofStrength = 8118,
        ScrollofStrengthII = 8119,
        ScrollofStrengthIII = 8120,
        ScrollofStrengthIV = 12179,
        ScrollofStrengthV = 33082,
    }
    public enum ScrollOfAgility
    {
        ScrollOfAgility = 8115,
        ScrollOfAgilityII = 8116,
        ScrollOfAgilityIII = 8117,
        ScrollOfAgilityIV = 12174,
        ScrollOfAgilityV = 33077,
    }
}
