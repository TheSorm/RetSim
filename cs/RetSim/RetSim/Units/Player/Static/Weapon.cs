using RetSim.Items;
using RetSim.Misc;
using RetSim.Units.UnitStats;

namespace RetSim.Units.Player.Static;

public class Weapon
{
    private readonly Player player;

    public WeaponType Type { get; init; }
    public int MinDamage { get; init; }
    public int MaxDamage { get; init; }
    public int BaseSpeed { get; init; }

    public int EffectiveSpeed => (int)(BaseSpeed / player.Stats.EffectiveAttackSpeed);

    public float APBonus => player.Stats[StatName.AttackPower].Value * BaseSpeed / Constants.Numbers.MillisecondsPerSec / Constants.Stats.APPerDPS;
    public float NormalizedAPBonus => player.Stats[StatName.AttackPower].Value * Constants.Stats.NormalizedWeaponSpeed / Constants.Numbers.MillisecondsPerSec / Constants.Stats.APPerDPS;

    public Weapon(Player owner)
    {
        player = owner;

        Type = player.Equipment.Weapon.Type;
        MinDamage = player.Equipment.Weapon.MinDamage;
        MaxDamage = player.Equipment.Weapon.MaxDamage;
        BaseSpeed = player.Equipment.Weapon.AttackSpeed;
    }

    public float Attack()
    {
        return (RNG.RollRange(MinDamage, MaxDamage) + APBonus) * player.Modifiers.WeaponDamage;
    }

    public float NormalizedAttack()
    {
        return (RNG.RollRange(MinDamage, MaxDamage) + NormalizedAPBonus) * player.Modifiers.WeaponDamage;
    }
}