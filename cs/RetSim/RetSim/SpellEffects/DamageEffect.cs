namespace RetSim.SpellEffects
{
    public abstract class DamageEffect : SpellEffect
    {
        public Spell Spell { get; init; }

        public School School { get; init; } = School.Physical;
        public float Coefficient { get; init; } = 0;
        public float HolyCoefficient { get; init; } = 0;
        public DefenseType DefenseCategory { get; init; } = DefenseType.Special;
        public Category CritCategory { get; init; } = Category.Physical;
        public bool Normalized { get; init; } = false;
        public ProcMask OnCast { get; init; } = ProcMask.None;
        public ProcMask OnHit { get; init; } = ProcMask.None;
        public ProcMask OnCrit { get; init; } = ProcMask.None;

        public DamageEffect(Spell spell,
                            School school,
                            DefenseType defense,
                            Category crit,
                            bool normalized,
                            ProcMask onCast,
                            ProcMask onHit,
                            ProcMask onCrit,
                            float coefficient = 0f,
                            float holy = 0f)
        {
            Spell = spell;

            School = school;
            DefenseCategory = defense;
            CritCategory = crit;
            Normalized = normalized;
            OnCast = onCast;
            OnHit = onHit;
            OnCrit = onCrit;
            Coefficient = coefficient;
            HolyCoefficient = holy;
        }

        public DamageEffect() { }
    }
}