using System;

namespace RetSim
{
    public record Proc
    {
        public Spell Spell { get; set; }
        public ProcMask ProcMask { get; init; }
        public int Chance { get; init; } = 100;
        public float PPM { get; init; } = 0f;
        public int Cooldown { get; init; } = 0;
    }

    [Flags]
    public enum ProcMask
    {
        None = 0,
        OnAutoAttack = 1,
        OnWindfury = 2,
        OnSpecialAttack = 4,
        OnSealOfCommand = 8,
        OnMeleeCrit = 16,
        OnAnyCrit = 32,
        OnSpellCastSuccess = 64,
        OnBasicAttack = OnAutoAttack + OnWindfury + OnSealOfCommand,
        OnAnyAttack = OnBasicAttack + OnSpecialAttack
        // TODO: Update and enhance
    }
}