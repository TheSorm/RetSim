using Newtonsoft.Json;
using RetSim.Simulation;
using RetSim.Units.Player;
using RetSim.Units.Player.State;
using RetSim.Units.UnitStats;
using System.ComponentModel;

namespace RetSim.Spells.SpellEffects;

[Serializable]
public class WeaponAttack : Damage
{
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool Normalized { get; init; } = false;


    [DefaultValue(1f)]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]    
    public float Percentage { get; init; } = 1f;

    public override float CalculateDamage(Player player, Attack attack, SpellState state)
    {
        School school = School;

        if (CritCategory == AttackCategory.Physical)
            school |= School.Physical;

        float schoolMultiplier = player.Modifiers.SchoolDamageDone.GetModifier(school);

        float weapon = player.Weapon.Attack(attack.Enemy.Stats[StatName.IncreasedAttackerAttackPower].Value, Normalized);

        return ((weapon + GetSpellPowerBonus(player, state)) * Percentage * state.EffectBonusPercent + state.EffectBonus) * schoolMultiplier;
    }
}