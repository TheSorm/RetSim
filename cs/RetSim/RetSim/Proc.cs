using System;

namespace RetSim
{
    public record Proc
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public ProcMask ProcMask { get; init; }
        public Spell Spell { get; set; }
        public int Chance { get; init; }
        public float PPM { get; init; }
        public int Cooldown { get; init; } 
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