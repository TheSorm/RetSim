using System;

namespace RetSim
{
    public record Proc
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public ProcMask ProcMask { get; init; }
        public Spell Spell { get; init; }
        public int Chance { get; init; }
        public int PPM { get; init; }
        // public int Cooldown { get; init; } TODO: Cooldown on spell instead of proc?
    }

    [Flags]
    public enum ProcMask
    {
        None = 0,
        OnMeleeAutoAttack = 1,
        OnMeleeSpecialAttack = 2,
        OnCrit = 4,
        OnMeleeCrit = 8,
        OnSpellCastSuccess = 16
        // TODO: Update and enhance
    }
}
