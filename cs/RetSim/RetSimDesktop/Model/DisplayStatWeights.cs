using RetSim.Units.UnitStats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetSimDesktop.Model
{
    public class DisplayStatWeights : INotifyPropertyChanged
    {
        private StatName stat;
        private string name = "";
        private float dpsDelta;
        private float statPerDps;

        public StatName Stat
        {
            get { return stat; }
            set
            {
                stat = value;
                OnPropertyChanged(nameof(Stat));
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
