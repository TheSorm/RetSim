using RetSim.SpellEffects;

namespace RetSim.Log
{
    public class DamageEntry : LogEntry
    {
        public string Source { get; init; }
        public AttackResult AttackResult { get; init; }
        public bool Crit { get; init; }
        public float Glancing { get; init; }
        public float PartialResist { get; init; }
        public int Damage { get; init; }
        public School School { get; init; }

        
        public DamageEntry(int timestamp,
                           int mana,
                           string source,
                           AttackResult attack = AttackResult.Miss,
                           int damage = 0,
                           School school = School.Typeless,
                           bool crit = false,
                           float glancing = 0,
                           float partial = 0) 
            : base(timestamp, mana)
        {
            Source = source;
            AttackResult = attack;
            Crit = crit;
            Glancing = glancing;
            PartialResist = partial;
            Damage = damage;
            School = school;
        }

        protected override string FormatInput()
        {
            string result = $"[Player] [{Source}] {AttackResult} [Enemy]";

            if (Damage > 0)
                result += $" for {Damage} {School} Damage";

            if (Crit)
                result += $" (Crit)";

            if (Glancing > 0)
                result += $" ({Glancing}% Glancing)";

            if (PartialResist > 0)
                result += $" ({PartialResist}% Resist)";

            return result;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

}