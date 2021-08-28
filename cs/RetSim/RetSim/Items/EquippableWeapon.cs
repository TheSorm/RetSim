namespace RetSim.Items
{
    public class EquippableWeapon : EquippableItem
    {
        public int MinDamag { get; init; }
        public int MaxDamag { get; init; }
        public int AttackSpeed { get; init; }
        public float DPS { get; init; }
        public EquippableWeapon(WowItemData.Weapon data) : base(data)
        {
            MinDamag = data.MinDamag;
            MaxDamag = data.MaxDamag;
            AttackSpeed = data.AttackSpeed;
            DPS = (float)data.DPS;
        }
    }
}
