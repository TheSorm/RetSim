using RetSim.Spells;

namespace RetSim.Data
{
    public static class Collections
    {
        public static readonly Dictionary<int, Spell> Spells = new();
        public static readonly Dictionary<int, Spell> Seals = new();
        public static readonly Dictionary<int, Judgement> Judgements = new();
        public static readonly Dictionary<int, Talent> Talents = new();

        public static readonly Dictionary<int, Proc> Procs = new();
        public static readonly Dictionary<int, Aura> Auras = new();
    }
}
