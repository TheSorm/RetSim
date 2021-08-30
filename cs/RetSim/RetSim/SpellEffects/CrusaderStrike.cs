namespace RetSim.SpellEffects
{
    public class CrusaderStrike : WeaponDamage
    {
        public const float CSModifier = 1.1f;

        public CrusaderStrike(School school,
                            DefenseType defense,
                            Category crit,
                            bool normalized,
                            ProcMask onCast,
                            ProcMask onHit,
                            ProcMask onCrit,
                            float coefficient = 0f,
                            float holy = 0f)
                            : base(Glossaries.Spells.CrusaderStrike, school, defense, crit, normalized, onCast, onHit, onCrit, coefficient, holy)
        {
        }

        public CrusaderStrike() { }

        public override ProcMask Resolve(FightSimulation fight)
        {
            Percentage = CSModifier * fight.Player.Modifiers.CrusaderStrike;

            //int damage = Formulas.Damage.NormalizedWeaponDamage(341, 513, 0, 2000, Modifier);

            return ProcMask.OnMeleeSpecialAttack; // TODO: Is that all?
        }
    }
}
