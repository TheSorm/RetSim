namespace RetSim.SpellEffects
{
    public abstract class SpellEffect
    {
        public SpellEffect() { }

        public abstract object Resolve(Player caster);
    }
}