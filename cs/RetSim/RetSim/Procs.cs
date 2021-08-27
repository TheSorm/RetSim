using RetSim.Events;
using System.Collections.Generic;

namespace RetSim
{
    public class Procs : List<Proc>
    {
        private Player player { get; init; }

        public Procs(Player parent)
        {
            player = parent;
        }

        public void CheckProcs(ProcMask mask, int time, List<Event> results)
        {
            foreach (var proc in this)
            {
                if (!player.Spellbook.IsOnCooldown(proc.Spell) && (proc.ProcMask & mask) != ProcMask.None && RollProc(proc, player))
                {
                    results.Add(new CastEvent(time, player, proc.Spell)); //TODO: Increase Prio of those cast events
                    //Program.Logger.Log($"{proc.Name} procced");
                }
            }
        }

        private static bool RollProc(Proc proc, Player player)
        {
            if (proc.PPM == 0)
                return RNG.Roll100(proc.Chance);

            else
                return RNG.Roll100(Helpers.PPMToChance(proc.PPM, player));

        }
    }
}
