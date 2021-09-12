namespace RetSim
{
    public class Proc
    {
        public Spell Spell { get; set; }
        public ProcMask ProcMask { get; init; }
        public bool GuaranteedProc { get; init; } = false;
        public int Chance { get; init; } = 0;
        public float PPM { get; init; } = 0f;
        public int Cooldown { get; init; } = 0;
    }

    [Flags]
    public enum ProcMask
    {
        None = 0,
        OnCrit = 1,
        OnAutoAttack = 2,
        OnExtraAttack = 4,
        OnWhiteAttack = OnAutoAttack + OnExtraAttack,
        OnSealOfCommand = 8,
        OnBasicAttack = OnWhiteAttack + OnSealOfCommand,
        OnSpecialAttack = 16,
        OnMeleeAttack = OnBasicAttack + OnSpecialAttack,
        OnMeleeCrit = 32,
        OnRangedAttack = 64,
        OnJudgement = 128,
        OnAnyAttack = OnMeleeAttack + OnRangedAttack,
        OnSpellCastSuccess = 256
        // TODO: Update and enhance
    }
}