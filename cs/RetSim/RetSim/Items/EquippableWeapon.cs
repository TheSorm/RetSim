namespace RetSim.Items
{
    public class EquippableWeapon : EquippableItem
    {
        public int MinDamag { get; init; }
        public int MaxDamag { get; init; }
        public int AttackSpeed { get; init; }
        public float DPS { get; init; }
    }
}
