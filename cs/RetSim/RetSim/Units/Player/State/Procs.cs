using RetSim.Misc;
using RetSim.Simulation;
using RetSim.Simulation.Events;
using RetSim.Spells;

namespace RetSim.Units.Player.State;

public class Procs : Dictionary<Proc, ProcCooldownEndEvent>
{
    private readonly Player player;

    public Procs(Player parent)
    {
        player = parent;
    }

    public new void Add(Proc proc, ProcCooldownEndEvent cooldown = null)
    {
        if (!ContainsKey(proc))
            base.Add(proc, null);
    }

    public void CheckProcs(ProcMask mask, FightSimulation fight)
    {
        foreach (var proc in Keys)
        {
            if (!IsOnCooldown(proc) && (proc.ProcMask & mask) != ProcMask.None && RollProc(proc, player))
            {
                if (proc.Cooldown > 0)
                    fight.Queue.Add(new ProcCooldownEndEvent(proc, fight, fight.Timestamp + proc.Cooldown));

                fight.Queue.Add(new CastEvent(proc.Spell, fight.Player, fight.Player, fight, fight.Timestamp)); //TODO: Increase Prio of those cast events

                //Program.Logger.Log($"{proc.Name} procced");
            }
        }
    }

    private static bool RollProc(Proc proc, Player player)
    {
        if (proc.GuaranteedProc)
            return true;

        else if (proc.PPM == 0f)
            return RNG.Roll100(proc.Chance);

        else
            return RNG.Roll100(PPMToChance(proc.PPM, player));
    }

    /// <summary>
    /// Converts a given PPM to its respective proc chance %, based on the given player's weapon speed.
    /// </summary>
    /// <param name="ppm">The PPM value of the proc to be converted into % chance.</param>
    /// <param name="player">The player whose weapon to be used to calculate the proc chance.</param>
    /// <returns>The proc chance of the proc in %, expressed as an integer number between 0 and 100.</returns>
    private static float PPMToChance(float ppm, Player player)
    {
        float chance = player.Weapon.BaseSpeed * ppm / 600;

        if (chance < 0)
            return 0;

        else if (chance > 100)
            return 100;

        else
            return chance;
    }

    public void StartCooldown(Proc proc, ProcCooldownEndEvent cooldown)
    {
        this[proc] = cooldown;
    }

    public void EndCooldown(Proc proc)
    {
        if (ContainsKey(proc))
        {
            this[proc] = null;
        }
    }

    public bool IsOnCooldown(Proc proc)
    {
        return this[proc] != null;
    }
}