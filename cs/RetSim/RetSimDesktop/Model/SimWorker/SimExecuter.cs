using RetSim.Data;
using RetSim.Spells;
using RetSim.Units.Enemy;
using RetSim.Units.Player;
using RetSim.Units.Player.Static;
using System.Collections.Generic;

namespace RetSimDesktop.Model.SimWorker
{
    public abstract class SimExecuter
    {
        public Race Race { get; init; } = Collections.Races["Human"];
        public ShattrathFaction ShattrathFaction { get; init; }
        public Boss Encounter { get; init; } = Collections.Bosses[0];
        public Equipment PlayerEquipment { get; init; } = new Equipment();
        public List<Talent> Talents { get; init; } = new();
        public List<Spell> GroupTalents { get; init; } = new();
        public List<Spell> Buffs { get; init; } = new();
        public List<Spell> Debuffs { get; init; } = new();
        public List<Spell> Consumables { get; init; } = new();
        public List<Spell> Cooldowns { get; init; } = new();
        public List<int> HeroismUsage { get; init; } = new();
        public int MinFightDuration { get; init; }
        public int MaxFightDuration { get; init; }
        public int NumberOfSimulations { get; init; }
        public int MaxCSDelay { get; init; }


        public abstract void Execute();

    }
}
