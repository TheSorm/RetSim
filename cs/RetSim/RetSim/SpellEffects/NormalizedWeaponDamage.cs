namespace RetSim.SpellEffects
{
    public class NormalizedWeaponDamage : SpellEffect
    {
        public float Modifier { get; init; }

        public NormalizedWeaponDamage(float modifier) : base()
        {
            Modifier = modifier;
        }

        public override object Resolve(Player caster)
        {
            float damage = Formulas.Damage.NormalizedWeaponDamage(1000, 2000, 0, 2000, Modifier);

            return new { Damage = damage };
        }
    }
}
