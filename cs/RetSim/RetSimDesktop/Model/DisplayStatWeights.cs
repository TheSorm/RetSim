using RetSim.Units.UnitStats;
using System.ComponentModel;

namespace RetSimDesktop.Model
{
    public class DisplayStatWeights : INotifyPropertyChanged
    {
        private StatName stat;
        private bool enabledForStatWeight;
        private float increasedAmount;
        private string name = "";
        private float dpsDelta;
        private float statPerDps;
        private bool ignoreExpertiseCap = false;

        public bool IgnoreExpertiseCap
        {
            get { return ignoreExpertiseCap; }
            set
            {
                ignoreExpertiseCap = value;
                OnPropertyChanged(nameof(IgnoreExpertiseCap));
            }
        }

        public StatName Stat
        {
            get { return stat; }
            set
            {
                stat = value;
                OnPropertyChanged(nameof(Stat));
            }
        }

        public bool EnabledForStatWeight
        {
            get { return enabledForStatWeight; }
            set
            {
                enabledForStatWeight = value;
                OnPropertyChanged(nameof(EnabledForStatWeight));
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public float IncreasedAmount
        {
            get { return increasedAmount; }
            set
            {
                increasedAmount = value;
                OnPropertyChanged(nameof(IncreasedAmount));
            }
        }

        public float DpsDelta
        {
            get { return dpsDelta; }
            set
            {
                dpsDelta = value;
                OnPropertyChanged(nameof(DpsDelta));
            }
        }
        public float StatPerDps
        {
            get { return statPerDps; }
            set
            {
                statPerDps = value;
                OnPropertyChanged(nameof(StatPerDps));
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
