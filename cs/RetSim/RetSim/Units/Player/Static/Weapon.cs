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

    public Weapon(Player owner)
    {
        player = owner;

        Type = player.Equipment.Weapon.Type;
        MinDamage = player.Equipment.Weapon.MinDamage;
        MaxDamage = player.Equipment.Weapon.MaxDamage;
        BaseSpeed = player.Equipment.Weapon.AttackSpeed;
    }

    public float Attack(float bonusAP, bool normalized)
    {
        return RNG.RollRange(MinDamage, MaxDamage) + GetAPBonus(bonusAP, normalized); ;
    }

    public float GetAPBonus(float bonusAP, bool normalized)
    {
        if (normalized)
            return (player.Stats[StatName.AttackPower].Value + bonusAP) * Constants.Stats.NormalizedWeaponSpeed / Constants.Numbers.MillisecondsPerSec / Constants.Stats.APPerDPS;

        else
            return (player.Stats[StatName.AttackPower].Value + bonusAP) * BaseSpeed / Constants.Numbers.MillisecondsPerSec / Constants.Stats.APPerDPS;
    }
}