namespace RetSim.Items
{
    public class EquippableWeapon : EquippableItem
    {
        public int MinDamage { get; init; }
        public int MaxDamage { get; init; }
        public int AttackSpeed { get; init; }
        public float DPS { get; init; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
