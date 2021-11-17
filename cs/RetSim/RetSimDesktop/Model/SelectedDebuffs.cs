using RetSim.Spells;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RetSim.Data.Collections;

namespace RetSimDesktop.Model
{
    public class SelectedDebuffs : INotifyPropertyChanged
    {
        private bool judgementoftheCrusaderEnabled;
        private JudgementoftheCrusader selectedJudgementoftheCrusader;

        private bool judgementofWisdomEnabled;

        private bool armorDebuffEnabled;
        private ArmorDebuff selectedArmorDebuff;

        private bool bloodFrenzyEnabled;
        private bool huntersMarkEnabled;
        private bool exposeWeaknessEnabled;

        private bool faerieFireEnabled;
        private FaerieFire selectedFaerieFire;

        private bool curseofRecklessnessEnabled;

        private bool curseoftheElementsEnabled;
        private CurseoftheElements selectedCurseoftheElements;

        private bool improvedShadowBoltEnabled;
        private bool miseryEnabled;
        private bool shadowWeavingEnabled;
        private bool improvedScorchEnabled;

        public bool JudgementoftheCrusaderEnabled { get => judgementoftheCrusaderEnabled; set { judgementoftheCrusaderEnabled = value; OnPropertyChanged(nameof(JudgementoftheCrusaderEnabled)); } }
        public JudgementoftheCrusader SelectedJudgementoftheCrusader { get => selectedJudgementoftheCrusader; set { selectedJudgementoftheCrusader = value; OnPropertyChanged(nameof(SelectedJudgementoftheCrusader)); } }
        public bool JudgementofWisdomEnabled { get => judgementofWisdomEnabled; set { judgementofWisdomEnabled = value; OnPropertyChanged(nameof(JudgementofWisdomEnabled)); } }
        public bool ArmorDebuffEnabled { get => armorDebuffEnabled; set { armorDebuffEnabled = value; OnPropertyChanged(nameof(ArmorDebuffEnabled)); } }
        public ArmorDebuff SelectedArmorDebuff { get => selectedArmorDebuff; set { selectedArmorDebuff = value; OnPropertyChanged(nameof(SelectedArmorDebuff)); } }
        public bool BloodFrenzyEnabled { get => bloodFrenzyEnabled; set { bloodFrenzyEnabled = value; OnPropertyChanged(nameof(BloodFrenzyEnabled)); } }
        public bool HuntersMarkEnabled { get => huntersMarkEnabled; set { huntersMarkEnabled = value; OnPropertyChanged(nameof(HuntersMarkEnabled)); } }
        public bool ExposeWeaknessEnabled { get => exposeWeaknessEnabled; set { exposeWeaknessEnabled = value; OnPropertyChanged(nameof(ExposeWeaknessEnabled)); } }
        public bool FaerieFireEnabled { get => faerieFireEnabled; set { faerieFireEnabled = value; OnPropertyChanged(nameof(FaerieFireEnabled)); } }
        public FaerieFire SelectedFaerieFire { get => selectedFaerieFire; set { selectedFaerieFire = value; OnPropertyChanged(nameof(SelectedFaerieFire)); } }
        public bool CurseofRecklessnessEnabled { get => curseofRecklessnessEnabled; set { curseofRecklessnessEnabled = value; OnPropertyChanged(nameof(CurseofRecklessnessEnabled)); } }
        public bool CurseoftheElementsEnabled { get => curseoftheElementsEnabled; set { curseoftheElementsEnabled = value; OnPropertyChanged(nameof(CurseoftheElementsEnabled)); } }
        public CurseoftheElements SelectedCurseoftheElements { get => selectedCurseoftheElements; set { selectedCurseoftheElements = value; OnPropertyChanged(nameof(SelectedCurseoftheElements)); } }
        public bool ImprovedShadowBoltEnabled { get => improvedShadowBoltEnabled; set { improvedShadowBoltEnabled = value; OnPropertyChanged(nameof(ImprovedShadowBoltEnabled)); } }
        public bool MiseryEnabled { get => miseryEnabled; set { miseryEnabled = value; OnPropertyChanged(nameof(MiseryEnabled)); } }
        public bool ShadowWeavingEnabled { get => shadowWeavingEnabled; set { shadowWeavingEnabled = value; OnPropertyChanged(nameof(ShadowWeavingEnabled)); } }
        public bool ImprovedScorchEnabled { get => improvedScorchEnabled; set { improvedScorchEnabled = value; OnPropertyChanged(nameof(ImprovedScorchEnabled)); } }


        public List<Spell> GetDebuffs()
        {
            List<Spell> result = new();
            if (judgementoftheCrusaderEnabled && Spells.ContainsKey((int)selectedJudgementoftheCrusader))
            {
                result.Add(Spells[(int)selectedJudgementoftheCrusader]);
            }
            if (judgementofWisdomEnabled && Spells.ContainsKey(20354))
            {
                result.Add(Spells[20354]);
            }
            if (armorDebuffEnabled && Spells.ContainsKey((int)selectedArmorDebuff))
            {
                result.Add(Spells[(int)selectedArmorDebuff]);
            }
            if (bloodFrenzyEnabled && Spells.ContainsKey(30070))
            {
                result.Add(Spells[30070]);
            }
            if (huntersMarkEnabled && Spells.ContainsKey(14325))
            {
                result.Add(Spells[14325]);
            }
            if (exposeWeaknessEnabled && Spells.ContainsKey(34501))
            {
                result.Add(Spells[34501]);
            }
            if (faerieFireEnabled && Spells.ContainsKey((int)selectedFaerieFire))
            {
                result.Add(Spells[(int)selectedFaerieFire]);
            }
            if (curseofRecklessnessEnabled && Spells.ContainsKey(27226))
            {
                result.Add(Spells[27226]);
            }
            if (curseoftheElementsEnabled && Spells.ContainsKey((int)selectedCurseoftheElements))
            {
                result.Add(Spells[(int)selectedCurseoftheElements]);
            }
            if (improvedShadowBoltEnabled && Spells.ContainsKey(17800))
            {
                result.Add(Spells[17800]);
            }
            if (miseryEnabled && Spells.ContainsKey(33200))
            {
                result.Add(Spells[33200]);
            }
            if (shadowWeavingEnabled && Spells.ContainsKey(15258))
            {
                result.Add(Spells[15258]);
            }
            if (improvedScorchEnabled && Spells.ContainsKey(22959))
            {
                result.Add(Spells[22959]);
            }

            return result;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum JudgementoftheCrusader
    {
        JudgementoftheCrusader = 27159,
        ImpJudgementoftheCrusader = 20337
    }
    public enum ArmorDebuff
    {
        ExposeArmor = 26866,
        SunderArmor = 25225,
        ImpExposeArmor = 14169
    }

    public enum FaerieFire
    {
        FaerieFire = 27011,
        ImpFaerieFire = 33602
    }

    public enum CurseoftheElements
    {
        CurseoftheElements = 27228,
        Malediction = 32484
    }
}
