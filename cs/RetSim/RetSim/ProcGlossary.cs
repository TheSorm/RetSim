
using System;
using System.Collections.Generic;

namespace RetSim
{
    public record Proc
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public ProcMask ProcMask { get; init; }
        public Spell Spell { get; init; }
        public int Chance { get; init; }
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
        // TODO: Update and enhance
    }
    class ProcGlossary
    {
        public static readonly Proc MagtheridonMeleeTrinket = new()
        {
            ID = 34774,
            Name = "Magtheridon Melee Trinket",
            ProcMask = ProcMask.OnMeleeAutoAttack | ProcMask.OnMeleeSpecialAttack,
            //Spell = SpellGlossary.Haste,
            Chance = 15,
            //Cooldown = 20 * 1000,
        };

        public static readonly Dictionary<int, Proc> ByID = new()
        {
            { MagtheridonMeleeTrinket.ID, MagtheridonMeleeTrinket },
        };
    }
}
